using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using TestsSharedLibrary.TestSimulation;
using TestsSharedLibraryForCodeParsers.CodeGeneration;
using TextParser;
using UniversalExpressionParser.DemoExpressionLanguageProviders;


namespace UniversalExpressionParser.Tests
{
    public enum OperatorNameType
    {
        NiceOperatorNamesWithSinglePart,
        NiceOperatorNamesWithMultipleParts,
        NotNiceOperatorNamesWithMultipleParts
    }

    public static class TestSetup
    {
        private static ISimulationRandomNumberGenerator _simulationRandomNumberGenerator;

        private static int _maxNumberOfAdditionalWhitespaces;
        private static int _maxNumberOfAdditionalComments;

        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        private static readonly List<char> _specialOperatorCharactersUsedInLiterals = new List<char>();
    

        private static readonly List<ILanguageKeywordInfo> _predefinedKeywords = new List<ILanguageKeywordInfo>
        {
            KeywordHelpers.CreateKeyword(KeywordIds.GenericTypes),
            KeywordHelpers.CreateKeyword(KeywordIds.Where),
            KeywordHelpers.CreateKeyword(KeywordIds.Performance),
            KeywordHelpers.CreateKeyword(KeywordIds.Pragma),
            KeywordHelpers.CreateKeyword(KeywordIds.Metadata),
            //KeywordHelpers.CreateKeyword(KeywordIds.GlobalIntInlineVarDeclaration),
        };

        [CanBeNull]
        public static CommentMarkersData CurrentCommentMarkersData { get; private set; }

        public static bool IsLanguageCaseSensitive { get; private set; }

        static TestSetup()
        {
            ResetToDefaults();
        }

        public static CodeGenerationHelper CodeGenerationHelper { get; } = new CodeGenerationHelper(new CodeGeneratorParametersProvider());

        public static void ResetToDefaults()
        {
            SimulateNiceCode = false;
            NeverAddExtraSpaces = false;
            MustSupportCodeBlocks = false;

            MaxNumberOfAdditionalWhitespaces = 6;
            MaxNumberOfAdditionalComments = 3;
            MaxLengthOfComment = 6;
            EnableCommentsAlways = false;

            MaxNumberOfExpressionsInCodeBlock = 3; // 10; // 5;
            MaxNumberOfParametersInFunctionOrMatrix = 3; // 5;
            MaxDepthOfExpression = 5;
            MaxNumberOfExpressionItems = int.MaxValue; // 200; // 1000; // int.MaxValue;
            MaxNumberOfPrefixes = 5;
            MaxNumberOfKeywords = 3;

            MaxLengthOfSmallWords = 8;
            MaxLengthOfLongWords = 25;
        }

        public static void SetupSimulationRandomNumberGenerator(int? randomNumberSeed)
        {
            if (randomNumberSeed == null)
                _simulationRandomNumberGenerator =
                    TestsSharedLibrary.TestSimulation.SimulationRandomNumberGenerator.CreateWithRandomlyGeneratedSeed(() => TestHelpers.TestFilesFolderPath);
            else
                _simulationRandomNumberGenerator =
                    TestsSharedLibrary.TestSimulation.SimulationRandomNumberGenerator.CreateWithSeed(() => TestHelpers.TestFilesFolderPath, randomNumberSeed.Value);
        }

        public static void PrepareRandomSetupPerTest()
        {
            IsLanguageCaseSensitive = TestSetup.SimulationRandomNumberGenerator.Next(100) <= 70;

            OperatorPriorities.Initialize();

            #region Comment markers

            CurrentCommentMarkersData = null;

            if (EnableCommentsAlways || SimulationRandomNumberGenerator.Next(100) > 50)
            {
                if (SimulationRandomNumberGenerator.Next(100) <= 60)
                    CurrentCommentMarkersData = new CommentMarkersData("//", "/*", "*/", CommentMarkerType.CSharpStyle);
                else
                    CurrentCommentMarkersData = new CommentMarkersData("rem", "rem*", "*rem", CommentMarkerType.RemarkText);
            }
            #endregion

            _specialOperatorCharactersUsedInLiterals.Clear();

            if (SimulateNiceCode)
            {
                _specialOperatorCharactersUsedInLiterals.Add('$');
            }
            else
            {
                int numberOfSpecialOperatorCharacters = SimulationRandomNumberGenerator.Next(100) <= 50 ? 0 : 3;

                var specialOperatorCharacters = Helpers.SpecialOperatorCharacters.Where(x =>
                    !(CurrentCommentMarkersData != null && (CurrentCommentMarkersData.LineCommentMarker.Contains(x) ||
                    CurrentCommentMarkersData.MultilineCommentStartMarker.Contains(x) ||
                    CurrentCommentMarkersData.MultilineCommentEndMarker.Contains(x))) &&
                    x != '|' &&
                    !_predefinedKeywords.Any(keyword => keyword.Keyword.Contains(x))).ToList();

                while (_specialOperatorCharactersUsedInLiterals.Count < numberOfSpecialOperatorCharacters)
                {
                    var currentChar = specialOperatorCharacters[SimulationRandomNumberGenerator.Next(specialOperatorCharacters.Count - 1)];

                    _specialOperatorCharactersUsedInLiterals.Add(currentChar);
                    specialOperatorCharacters = specialOperatorCharacters.Where(x => x != currentChar).ToList();
                }
            }

            // ExpressionLanguageProviderCache is re-created with each test to assure that random set up is used
            // in TestLanguageProvider
            ExpressionLanguageProviderCache = new ExpressionLanguageProviderCache(new ExpressionLanguageProviderValidatorForSimulations());

            ExpressionLanguageProviders.Regenerate();

            ExpressionLanguageProviderCache.RegisterExpressionLanguageProvider(ExpressionLanguageProviders.TestLanguageProviderForSimulatedSuccessfulParseTests);
            ExpressionLanguageProviderCache.RegisterExpressionLanguageProvider(ExpressionLanguageProviders.TestLanguageProviderForLanguageProviderValidationTests);

            var textSymbolsParserFactory = new TextSymbolsParserFactory();

            ExpressionParser = new ExpressionParser(textSymbolsParserFactory, ExpressionLanguageProviderCache);

#if TRACE_PARSING_ON
            ExpressionParser.TraceParsing = (context, evaluationType) =>
            {
                var contextTextPositionStart = context.TextSymbolsParser.PositionInText;
                var contextTextPositionEnd = Math.Min(context.TextSymbolsParser.PositionInText + 30, context.TextSymbolsParser.ParsedTextEnd);

                LogHelper.Context.Log.InfoFormat("Parsing text at position: {0}. Evaluation type: {1}, Context text: '{2}...'.",
                    context.TextSymbolsParser.PositionInText,
                    evaluationType,
                    context.TextSymbolsParser.TextToParse.Substring(contextTextPositionStart, contextTextPositionEnd - contextTextPositionStart));
            };
#endif
        }

        public static bool MustSupportCodeBlocks { get; set; }
        public static IReadOnlyList<ILanguageKeywordInfo> PredefinedKeywords => _predefinedKeywords;

        public static List<char> SpecialOperatorCharactersUsedInLiterals => _specialOperatorCharactersUsedInLiterals;

        public static int MaxNumberOfExpressionsInCodeBlock { get; set; }
        public static int MaxNumberOfParametersInFunctionOrMatrix { get; set; }
        
        public static int MaxDepthOfExpression { get; set; }
        public static int MaxNumberOfExpressionItems { get; set; }
        public static int MaxNumberOfPrefixes { get; set; }
        public static int MaxNumberOfKeywords { get; set; }
        public static int MaxLengthOfSmallWords { get; set; }
        public static int MaxLengthOfLongWords { get; set; }

        /// <summary>
        /// If the value is not null, only operator template <see cref="OperatorTemplates.OperatorTemplatesCollection"/>[<see cref="OperatorTemplateIndex"/>]
        /// will be used when generating templates.
        /// </summary>
        public static int? OperatorTemplateIndex { get; set; }

        /// <summary>
        /// If the value is <see cref="UniversalExpressionParser.Tests.OperatorNameType.NiceOperatorNamesWithSinglePart"/>, the generated operator names
        /// will have format like "op1_Pr2_pref", "op3_Pr1_post", "op5_Pr7_bin", etc.
        /// If the value is <see cref="UniversalExpressionParser.Tests.OperatorNameType.NiceOperatorNamesWithMultipleParts"/>, the generated operator names
        /// will have format like "op1_Pr2_pref op1_Pr2_pref_part2" , "op3_Pr1_post op3_Pr1_post_part2", "op5_Pr7_bin", etc.
        ///
        /// If the value is <see cref="UniversalExpressionParser.Tests.OperatorNameType.NotNiceOperatorNamesWithMultipleParts"/>, the generated operator names
        /// will have have any format, like "%th$ %8&", "++&*", etc.
        /// </summary>
        public static OperatorNameType OperatorNameType { get; set; } = OperatorNameType.NotNiceOperatorNamesWithMultipleParts;

        public static int MaxNumberOfAdditionalWhitespaces
        {
            get
            {
                return SimulateNiceCode ? 0 : _maxNumberOfAdditionalWhitespaces;
            }
            set => _maxNumberOfAdditionalWhitespaces = value;
        }

        public static int MaxNumberOfAdditionalComments
        {
            get
            {
                return SimulateNiceCode ? 0 : _maxNumberOfAdditionalComments;
            }
            set => _maxNumberOfAdditionalComments = value;
        }

        public static bool EnableCommentsAlways { get; set; }

        public static int MaxLengthOfComment { get; set; } = 10;

        public static bool NeverAddExtraSpaces { get; set; }
        public static bool SimulateNiceCode { get; set; }

        public static ISimulationRandomNumberGenerator SimulationRandomNumberGenerator
        {
            get
            {
                //if (_simulationRandomNumberGenerator == null)
                //    PrepareRandomSetupForAllTests(null);

                if (_simulationRandomNumberGenerator == null)
                    throw new ArgumentException($"Call {nameof(PrepareRandomSetupPerTest)} first to initialize the value of {nameof(SimulationRandomNumberGenerator)}.");

                return _simulationRandomNumberGenerator;
            }
        }

        public static string EarlyTerminationText { get; } = "::END::";

        public static ExpressionParser ExpressionParser { get; private set; }
        public static ExpressionLanguageProviderCache ExpressionLanguageProviderCache { get; private set; }

        private class CodeGeneratorParametersProvider : ICodeGeneratorParametersProvider
        {
            public IRandomNumberGenerator RandomNumberGenerator => SimulationRandomNumberGenerator;
            
            // ReSharper disable once MemberHidesStaticFromOuterClass
            public int MaxNumberOfAdditionalWhitespaces => TestSetup.MaxNumberOfAdditionalWhitespaces;

            // ReSharper disable once MemberHidesStaticFromOuterClass
            public int MaxNumberOfAdditionalComments => TestSetup.MaxNumberOfAdditionalComments;

            // ReSharper disable once MemberHidesStaticFromOuterClass
            public int MaxLengthOfComment => TestSetup.MaxLengthOfComment;

            // ReSharper disable once MemberHidesStaticFromOuterClass
            public CommentMarkersData CommentMarkersData => TestSetup.CurrentCommentMarkersData;

            // ReSharper disable once MemberHidesStaticFromOuterClass
            public bool SimulateNiceCode => TestSetup.SimulateNiceCode;

            // ReSharper disable once MemberHidesStaticFromOuterClass
            public bool IsLanguageCaseSensitive => TestSetup.IsLanguageCaseSensitive;
        }
    }
}
