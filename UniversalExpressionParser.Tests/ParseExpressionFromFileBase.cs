using ClassVisualizer;
using JetBrains.Annotations;
using NUnit.Framework;
using OROptimizer.Diagnostics.Log;
using System.IO;
using OROptimizer;
using TextParser;
using UniversalExpressionParser.DemoExpressionLanguageProviders;
using UniversalExpressionParser.ExpressionItems;
using UniversalExpressionParser.Tests.Utilities.ExpressionItemVisualizersForDemos;

namespace UniversalExpressionParser.Tests
{
    internal abstract class ParseExpressionFromFileBase
    {
        static ParseExpressionFromFileBase()
        {
            LogHelper.RemoveContext();
            TestHelpers.RegisterLogger();
        }

        protected virtual ISaveVisualizedInterface CreateInterfaceSaveVisualizedInterface()
        {
            InterfaceVisualizerSettingsAmbientContext.Context = new InterfaceVisualizerSettings()
            {
                DoNotVisualizeDerivedInterface = false
            };

            //ExpressionItemVisualizerSettingsAmbientContext.Context = new ExpressionItemVisualizerSettings(true, true);

            return new SaveVisualizedInterface(
                new ValueVisualizerFactoryForDemos(new InterfacePropertyVisualizationHelper(),
                    new HtmlEncodeBasedAttributeValueSanitizer()
                    //new AttributeValueSanitizer()
                    ),
                new ObjectVisualizationContextFactory());
        }

        protected abstract string BaseFolder { get; }

        protected string GetFileDirectory([CanBeNull] string folderRelativePath)
        {
            var assemblyFilePath = Path.GetDirectoryName(typeof(TestHelpers).Assembly.Location)!;
            var demoExpressionsFolderRelativePath = BaseFolder;

            return Path.Join(assemblyFilePath.Substring(0, assemblyFilePath.IndexOf(@"\bin\")),
                folderRelativePath == null ? demoExpressionsFolderRelativePath : @$"{demoExpressionsFolderRelativePath}\{folderRelativePath}");
        }

        protected string LoadExpressionToParse([CanBeNull] string folderRelativePath, [NotNull] string fileNameWithoutExtension)
        {
            using (var streamReader = new StreamReader(
                       Path.Join(GetFileDirectory(folderRelativePath),
                           $"{fileNameWithoutExtension}.expr")))
            {
                return streamReader.ReadToEnd();
            }
        }

        protected IParseExpressionResult ParseExpressionFromFile([CanBeNull] string folderRelativePath,
            [NotNull] string fileNameWithoutExtension, int expectedNumberOfErrors = 0,
            [CanBeNull] IExpressionLanguageProvider expressionLanguageProvider = null)
        {
            if (expressionLanguageProvider == null)
                expressionLanguageProvider = new NonVerboseCaseSensitiveExpressionLanguageProvider();

            var parsedCode = LoadExpressionToParse(folderRelativePath, fileNameWithoutExtension);

            Assert.IsNotNull(parsedCode);

            IExpressionLanguageProviderCache expressionLanguageProviderCache = new ExpressionLanguageProviderCache(new DefaultExpressionLanguageProviderValidator());

            expressionLanguageProviderCache.RegisterExpressionLanguageProvider(expressionLanguageProvider);

            var textSymbolsParserFactory = new TextSymbolsParserFactory();
            var expressionParser = new ExpressionParser(textSymbolsParserFactory, expressionLanguageProviderCache, LogHelper.Context.Log);

            var parsedExpression = expressionParser.ParseExpression(expressionLanguageProvider.LanguageName, parsedCode,
                new ParseExpressionOptions());

            ProcessParsedExpression(parsedExpression, folderRelativePath, fileNameWithoutExtension, expectedNumberOfErrors);
            return parsedExpression;
        }

        protected void ProcessParsedExpression(IParseExpressionResult parseExpression,
            [CanBeNull] string folderRelativePath,
            [NotNull] string fileNameWithoutExtension,
            int expectedNumberOfErrors = 0)
        {
            var saveVisualizedInterface = CreateInterfaceSaveVisualizedInterface();

            var globalsCoreTestCurrent = GlobalsCoreAmbientContext.Context;

            try
            {
                GlobalsCoreAmbientContext.Context = new GlobalsCoreTest(globalsCoreTestCurrent);
                saveVisualizedInterface.Save(parseExpression, typeof(IParseExpressionResult),
                    Path.Join(GetFileDirectory(folderRelativePath), $"{fileNameWithoutExtension}.parsed"));
            }
            finally
            {
                GlobalsCoreAmbientContext.Context = globalsCoreTestCurrent;
            }


            if (parseExpression.ParseErrorData.AllParseErrorItems.Count > 0)
                LogHelper.Context.Log.Error(parseExpression.GetErrorTextWithContextualInformation(parseExpression.IndexInText, parseExpression.PositionInTextOnCompletion));

            Assert.AreEqual(expectedNumberOfErrors, parseExpression.ParseErrorData.AllParseErrorItems.Count);
        }
    }
}
