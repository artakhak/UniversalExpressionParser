using JetBrains.Annotations;
using NUnit.Framework;
using System;
using System.IO;
using UniversalExpressionParser.DemoExpressionLanguageProviders;

namespace UniversalExpressionParser.Tests.Demos
{
    /// <summary>
    /// Demos for C# like languages (not C# but languages that are pretty similar to C#)
    /// </summary>
    [TestFixture]
    internal class ParseExpressionDemos : ParseExpressionFromFileBase
    {
        protected override string BaseFolder => @"Demos\DemoExpressions";

        #region Summary
        [Test]
        public void Summary_SummaryExpression()
        {
            var summaryDemo = new SummaryDemo();

            var parsedExpression = summaryDemo.ParseNonVerboseExpression(this.LoadExpressionToParse(null, "SummaryExpression"));

            ProcessParsedExpression(parsedExpression, null, "SummaryExpression");
        }
        #endregion

        #region Temp diagnoistics
        [Test]
        public void TemporaryDiagnostics_DiagnosticsExpression()
        {

            //InterfaceVisualizerSettingsAmbientContext.Context = new InterfaceVisualizerSettings(Array.Empty<string>())
            //{
            //    DoNotVisualizeDerivedInterface = false
            //};

            //ExpressionItemVisualizerSettingsAmbientContext.Context = new ExpressionItemVisualizerSettings(true, false);

            IExpressionLanguageProvider expressionLanguageProvider = new NonVerboseCaseSensitiveExpressionLanguageProvider();
            var expectedNumberOfErrors = 0;
            //expressionLanguageProvider = new VerboseCaseInsensitiveExpressionLanguageProvider();

            var parseResult = ParseExpressionFromFile("TemporaryDiagnostics", "DiagnosticsExpression",
                expectedNumberOfErrors, expressionLanguageProvider);

            Assert.IsNotNull(parseResult);

            //if (parseResult.RootExpressionItem.RegularItems[0] is ExpressionItems.IConstantTextExpressionItem constantTextExpressionItem)
            //{
            //    var test = constantTextExpressionItem.TextValue.Text;
            //}
        }
        #endregion

        #region Literals
        [Test]
        public void Literals_Literals()
        {
            ParseExpressionFromFile("Literals", "Literals");
        }
        #endregion

        #region FunctionsAndBraces
        [Test]
        public void FunctionsAndBraces_RoundAndSquareBraces()
        {
            ParseExpressionFromFile("FunctionsAndBraces", "RoundAndSquareBraces");
        }

        [Test]
        public void FunctionsAndBraces_FunctionsWithRoundAndSquareBraces()
        {
            ParseExpressionFromFile("FunctionsAndBraces", "FunctionsWithRoundAndSquareBraces");
        }
        #endregion

        #region NumericValues
        [Test]
        public void NumericValues_NumericValues()
        {
            ParseExpressionFromFile("NumericValues", "NumericValues");
        }
        #endregion

        #region Texts
        [Test]
        public void Texts_Texts()
        {
            ParseExpressionFromFile("Texts", "Texts");
        }
        #endregion
        
        #region Operators
        [Test]
        public void Operators_OperatorPriorities()
        {
            ParseExpressionFromFile("Operators", "OperatorPriorities");
        }

        [Test]
        public void Operators_NoSpacesBetweenOperators()
        {
            ParseExpressionFromFile("Operators", "NoSpacesBetweenOperators");
        }

        [Test]
        public void Operators_BracesToChangeOperatorEvaluationOrder()
        {
            ParseExpressionFromFile("Operators", "BracesToChangeOperatorEvaluationOrder");
        }

        [Test]
        public void Operators_UnaryPrefixOperatorUsedForReturnStatement()
        {
            ParseExpressionFromFile("Operators", "UnaryPrefixOperatorUsedForReturnStatement");
        }

        [Test]
        public void Operators_MultipartOperators()
        {
            ParseExpressionFromFile("Operators", "MultipartOperators");
        }
        #endregion

        #region Keywords
       
        [Test]
        public void Keywords_Keywords()
        {
            ParseExpressionFromFile("Keywords", "Keywords");
        }

        #endregion

        #region Prefixes
        [Test]
        
        public void Prefixes_BracesPrefixesSimpleDemo()
        {
            ParseExpressionFromFile("Prefixes", "BracesPrefixesSimpleDemo");
        }
        
        [Test]
        public void Prefixes_CustomExpressionItemsAsPrefixesSimpleDemo()
        {
            ParseExpressionFromFile("Prefixes", "CustomExpressionItemsAsPrefixesSimpleDemo");
        }
        
        [Test]
        public void Prefixes_MoreComplexPrefixesDemo()
        {
            ParseExpressionFromFile("Prefixes", "MoreComplexPrefixesDemo");
        }
        
        [Test]
        public void Prefixes_PrefixesUsedWithDifferentExpressionItems()
        {
            ParseExpressionFromFile("Prefixes", "PrefixesUsedWithDifferentExpressionItems");
        }

        #endregion

        #region Postfixes
        [Test]
        
        public void Postfixes_SimpleCodeBlockPostfixAfterLiteral()
        {
            ParseExpressionFromFile("Postfixes", "SimpleCodeBlockPostfixAfterLiteral");
        }
        
        [Test]
        
        public void Postfixes_SimpleCustomExpressionItemAsPostfixAfterLiteral()
        {
            ParseExpressionFromFile("Postfixes", "SimpleCustomExpressionItemAsPostfixAfterLiteral");
        }

        [Test]
        public void Postfixes_CodeBlockPostfixToModelFunctionBody()
        {
            ParseExpressionFromFile("Postfixes", "CodeBlockPostfixToModelFunctionBody");
        }

        [Test]
        public void Postfixes_CodeBlockPostfixUsedToModelAClassDefinition()
        {
            ParseExpressionFromFile("Postfixes", "CodeBlockPostfixUsedToModelAClassDefinition");
        }

        [Test]
        public void Postfixes_CodeBlockPostfixForDifferentExpressionItems()
        {
            ParseExpressionFromFile("Postfixes", "CodeBlockPostfixForDifferentExpressionItems");
        }
        
        #endregion

        #region CodeSeparatorsAndCodeBlocks
        [Test]
        public void CodeSeparatorsAndCodeBlocks_SimpleExample()
        {
            ParseExpressionFromFile("CodeSeparatorsAndCodeBlocks", "SimpleExample");
        }

        [Test]
        public void CodeSeparatorsAndCodeBlocks_MoreComplexExample()
        {
            ParseExpressionFromFile("CodeSeparatorsAndCodeBlocks", "MoreComplexExample");
        }
        #endregion

        #region CustomExpressionItemParsers
        [Test]
        public void CustomExpressionItemParsers_SimpleCustomExpressionItems()
        {
            ParseExpressionFromFile("CustomExpressionItemParsers", "SimpleCustomExpressionItems");
        }

        [Test]
        public void CustomExpressionItemParsers_MultipleAdjacentPrefixPostfixCustomExpressionItems()
        {
            ParseExpressionFromFile("CustomExpressionItemParsers", "MultipleAdjacentPrefixPostfixCustomExpressionItems");
        }
        #endregion

        #region Comments
        [Test]
        public void Comments_Comments()
        {
            ParseExpressionFromFile("Comments", "Comments");
        }
        #endregion

        #region ErrorReporting
        [Test]
        public void ErrorReporting_ExpressionWithErrors()
        {
            ParseExpressionFromFile("ErrorReporting", "ExpressionWithErrors", 5);
        }
        #endregion

        #region ParsingSectionInText
        [Test]
        public void ParsingSectionInText_ParseSingleSquareBracesExpressionDemo()
        {
            var parsePartOfTextDemo = new ParseSingleBracesExpressionAtPositionDemo();

            var folderRelativePath = "ParsingSectionInText";
            var fileNameWithoutExtension = "ParseSingleSquareBracesExpressionDemo";

            var expression = this.LoadExpressionToParse(folderRelativePath, fileNameWithoutExtension);

            var parseBracesExpressionResult = parsePartOfTextDemo.ParseBracesAtCurrentPosition(expression, expression.IndexOf('['));

            ProcessParsedExpression(parseBracesExpressionResult, folderRelativePath, fileNameWithoutExtension);
        }

        [Test]
        public void ParsingSectionInText_ParseSingleRoundBracesExpressionDemo()
        {
            var parseBracesAtPositionDemo = new ParseSingleBracesExpressionAtPositionDemo();

            var folderRelativePath = "ParsingSectionInText";
            var fileNameWithoutExtension = "ParseSingleRoundBracesExpressionDemo";

            var expression = this.LoadExpressionToParse(folderRelativePath, fileNameWithoutExtension);

            var indexOfBraces = expression.IndexOf("(", expression.IndexOf("*/") + 2, StringComparison.Ordinal);

            var parseBracesExpressionResult = parseBracesAtPositionDemo.ParseBracesAtCurrentPosition(expression, indexOfBraces);
            Assert.AreEqual(parseBracesExpressionResult.PositionInTextOnCompletion, expression.IndexOf(")),", StringComparison.Ordinal) + 2);

            ProcessParsedExpression(parseBracesExpressionResult, folderRelativePath, fileNameWithoutExtension);
        }

        [Test]
        public void ParsingSectionInText_ParseSingleCodeBlockExpressionDemo()
        {
            var parseCodeBlockAtPositionDemo = new ParseSingleCodeBlockExpressionAtPositionDemo();

            var folderRelativePath = "ParsingSectionInText";
            var fileNameWithoutExtension = "ParseSingleCodeBlockExpressionDemo";

            var expression = this.LoadExpressionToParse(folderRelativePath, fileNameWithoutExtension);

            var parseCodeBlockExpressionResult = parseCodeBlockAtPositionDemo.ParseCodeBlockExpressionAtCurrentPosition(expression, expression.IndexOf('{'));

            Assert.AreEqual(parseCodeBlockExpressionResult.PositionInTextOnCompletion, expression.IndexOf("any text after code block", StringComparison.Ordinal));
            ProcessParsedExpression(parseCodeBlockExpressionResult, folderRelativePath, fileNameWithoutExtension);
        }
        #endregion

        #region CaseSensitivityAndNonStandardLanguageFeatures
        [Test]
        public void CaseSensitivityAndNonStandardLanguageFeatures_CaseSensitivityAndNonStandardLanguageFeatures()
        {
            ParseExpressionFromFile("CaseSensitivityAndNonStandardLanguageFeatures", "CaseSensitivityAndNonStandardLanguageFeatures", 0, new VerboseCaseInsensitiveExpressionLanguageProvider());
        }
        #endregion

        private string GetDemoFileFolderPath([CanBeNull] string folderRelativePath)
        {
            var assemblyFilePath = Path.GetDirectoryName(typeof(TestHelpers).Assembly.Location)!;
            var demoExpressionsFolderRelativePath = @$"Demos\DemoExpressions";

            return Path.Join(assemblyFilePath.Substring(0, assemblyFilePath.IndexOf(@"\bin\")),
                folderRelativePath == null ? demoExpressionsFolderRelativePath : @$"{demoExpressionsFolderRelativePath}\{folderRelativePath}");
        }
    }
}
