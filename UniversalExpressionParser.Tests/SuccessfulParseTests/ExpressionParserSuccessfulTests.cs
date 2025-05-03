using JetBrains.Annotations;
using NUnit.Framework;
using OROptimizer;
using OROptimizer.Diagnostics.Log;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ClassVisualizer;
using TestsSharedLibrary;
using TestsSharedLibrary.TestSimulation;
using TestsSharedLibrary.TestSimulation.Statistics;
using TestsSharedLibraryForCodeParsers.CodeGeneration;
using UniversalExpressionParser.ClassVisualizers;
using UniversalExpressionParser.ExpressionItems;
using UniversalExpressionParser.Tests.OperatorTemplates;
using UniversalExpressionParser.Tests.TestLanguage;
using UniversalExpressionParser.Tests.TestStatistics;

namespace UniversalExpressionParser.Tests.SuccessfulParseTests
{
    [TestFixture]
    public class ExpressionParserSuccessfulTests : TestsBase
    {

        private delegate void OnExpectedRootExpressionGeneratedDelegate([NotNull] IParseExpressionResult parseExpressionResult, [NotNull] string generatedCode, int expectedPositionInTextOnCompletion);
        private delegate void OnExpressionItemEvaluatedDelegate([NotNull] IParseExpressionResult parseExpressionResult);

        private delegate Task RunOneSimulationAsync([NotNull] string simulationIdentifier, [NotNull] string simulationIterationIdentifier,
                                               [NotNull] IExpressionLanguageProviderWrapper expressionLanguageProviderWrapper,
                                               [NotNull] ExpressionParser expressionParser,
                                               [NotNull] OnExpectedRootExpressionGeneratedDelegate onExpectedRootExpressionGenerated,
                                               [NotNull] Action beforeGeneratedCodeParsed,
                                               [NotNull] OnExpressionItemEvaluatedDelegate onExpressionItemEvaluated);

        /// <summary>
        /// Set this true only if expected and actual expressions validation results in infinite loop.
        /// Will log some diagnostics data about expressions being validated.
        ///  Make sure to run only one iteration by setting <see cref="NumberOfSimulationRuns"/> to 1, and also to
        /// set <see cref="TurnOnTimeBasedTaskCancellation"/> to false when this value is true;
        /// </summary>
        private static readonly bool LogValidatedExpressionData = false;

        /// <summary>
        /// Saves the randoms for all simulations to re-run later on.
        /// This option is not used anymore, since the random number seed for each simulation run is logged now, and all we need to do is to
        /// set <see cref="_presetRandomNumberGeneratorSeed"/> to non-null from the seed from the log and re-run the simulation.
        /// </summary>
        [Obsolete("Use _presetRandomNumberGeneratorSeed")]
        private static bool SaveRandomNumbersToRerunSimulations = false;

        /// <summary>
        /// If the value is true, parsing, test expression language generation, result validation (and so on)
        /// will be canceled with error logs, if these tasks take to long long to complete.
        /// Set this value to false when debugging. 
        /// </summary>
        private static bool TurnOnTimeBasedTaskCancellation =

#if DEBUG
            false;
#else
            true;
#endif

        private static int NumberOfIterationsToAlwaysSave = 0; // Keep this at 0 and set this to positive number to troubleshoot.
        private static readonly int NumberOfSimulationRuns = // 5000; //2000; //2000; // 100;
#if DEBUG
            1000;
#else
            1000;
#endif
        private static readonly bool TurnOffParsing = false;
        private static bool OutputExpressionItemIds = false;

        #region Set these values in SetupRerunSimulation() to re-run the test using the same random numbers when the last test failed (or just re-rerunning a simulation for debugging)
        /// <summary>
        /// Leave RandomNumberGeneratorSeed at null to randomly generate seed. Set the value to a number to use the specific seed to
        /// re-run the simulation with the same seed.
        /// The seed is re-set per each simulation run, unless this value is non-null.
        /// If test fails, just set the value to the value in log files and re-run the simultation.
        /// NOTE: The value is set in <see cref="SetupRerunSimulation()"/>
        /// </summary>
        private static int? _presetRandomNumberGeneratorSeed = null;

        /// <summary>
        /// If the value of <see cref="_reRunSimulationFromSavedRandomNumbers"/> is true, the values <see cref="_simulationIdentifierToReRun"/> and
        /// <see cref="_simulationIterationIndexToReRun"/> should be set.
        /// </summary>
        [Obsolete("Use non null value of _presetRandomNumberGeneratorSeed")]

        private static bool _reRunSimulationFromSavedRandomNumbers =
#if DEBUG
            false;
#else
            false;
#endif

        [Obsolete("Use non null value of _presetRandomNumberGeneratorSeed")]
        private static string _simulationIdentifierToReRun;

        [Obsolete("Use non null value of _presetRandomNumberGeneratorSeed")]
        private static int? _simulationIterationIndexToReRun; // 4; // null;

        /// <summary>
        /// Normally this value should be false. Sometimes we might set this to true, to save test results even when re-running simulation.
        /// </summary>
        [Obsolete]
        private static bool _saveTestDataWhenReRunningSimulation = false;
        #endregion

        // ReSharper disable once InconsistentNaming
        private static bool StopSimulation = false;
        private static ISaveTestDataCriteria _saveTestDataCriteria;
        private static bool _testDataWasSaved;
        private static bool _simulationFailed;

        private DateTime _currentTestStartTime;

        [NotNull, ItemNotNull]
        private static readonly List<TestStatisticValidationData> TestStatisticValidations = new List<TestStatisticValidationData>();

        static ExpressionParserSuccessfulTests()
        {
            LogHelper.RemoveContext();
            TestHelpers.RegisterLogger();

            TestSetup.SimulateNiceCode = false;
            TestSetup.MustSupportCodeBlocks = false;
            TestSetup.OperatorNameType = OperatorNameType.NotNiceOperatorNamesWithMultipleParts;
            TestSetup.NeverAddExtraSpaces = false;
            //TestSetup.OperatorTemplateIndex = 5;
            //TestSetup.EnableCommentsAlways = true;

            SetupRerunSimulation();
            SetupSavedIterationCriteria();

            //if (_generateSingleRandomNumberSeedForAllTests)
            //    TestSetup.SetupSimulationRandomNumberGenerator(_presetRandomNumberGeneratorSeed);

            if (LogValidatedExpressionData)
            {
                Assert.IsTrue(!TurnOnTimeBasedTaskCancellation && NumberOfSimulationRuns == 1);
            }
        }

        private static void SetupRerunSimulation()
        {
            // Once diagnostics are done, set _presetRandomNumberGeneratorSeed to null, and _generateSingleRandomNumberSeedForAllTests to false.

            _presetRandomNumberGeneratorSeed = null; //342974655; //null

#pragma warning disable CS0618
            if (_reRunSimulationFromSavedRandomNumbers)

            {
                _simulationIdentifierToReRun = nameof(IExpressionParser.ParseExpression);
                _simulationIterationIndexToReRun = 0;
            }
            else
            {
                _simulationIdentifierToReRun = null;
                _simulationIterationIndexToReRun = null;
            }
#pragma warning restore CS0618
        }

        private enum SavedIterationType
        {
            None,
            Any,
            LastIteration,
            SpecificIteration,
            OperatorSelector,
            IsLanguageCaseSensitiveSelector,
        }

        private static void SetupSavedIterationCriteria()
        {
            var savedIterationType = SavedIterationType.LastIteration;

            if (NumberOfIterationsToAlwaysSave > 0)
                savedIterationType = SavedIterationType.Any;

            _saveTestDataCriteria = null;

            switch (savedIterationType)
            {
                case SavedIterationType.Any:
                    _saveTestDataCriteria =
                        new SaveAnyIterationTestDataCriteria();
                    break;

                case SavedIterationType.SpecificIteration:
                    _saveTestDataCriteria =
                        new SaveSimulationIterationCriteria(nameof(IExpressionParser.ParseCodeBlockExpression),
                            GetSimulationIterationIdentifier(1));
                    break;

                case SavedIterationType.LastIteration:
                    _saveTestDataCriteria =
                        new SaveSimulationIterationCriteria(nameof(IExpressionParser.ParseCodeBlockExpression),
                            GetSimulationIterationIdentifier(NumberOfSimulationRuns - 1));
                    break;

                case SavedIterationType.OperatorSelector:

                    _saveTestDataCriteria = new SaveIfExpectedRootExpressionSatisfiesCriteria(x =>
                    {
                        return x is IOperatorExpressionItem;
                    });

                    break;

                case SavedIterationType.IsLanguageCaseSensitiveSelector:

                    _saveTestDataCriteria = new SaveIfExpectedRootExpressionSatisfiesCriteria(x => false);

                    break;
            }
        }

        [SetUp]
        public void TestInitialize()
        {
            _testDataWasSaved = false;
            _currentTestStartTime = DateTime.Now;

            // LogHelper.Context.Log.InfoFormat("Random number seed is {0}", (PresetRandomNumberGeneratorSeed != null ? PresetRandomNumberGeneratorSeed.ToString() : "null"));

            List<string> notVisualizedProperties = new List<string>();
            if (!OutputExpressionItemIds)
            {
                // We do not want to visualize IExpressionItemBase.Id since the Ids in expected and parsed IParsedExpressionResult instances are different, since the
                // expression items are generated in a different order. This results in differences in visualized XML files. 
                // We use WinMerge to compare the XML files for actual and expected root expression items, therefore, we want this files to be similar when test
                // succeeds.
                // The visualized objects will show ObjectId, which will be the same.
                // However, set OutputExpressionItemIds to true to troubleshoot errors.
                notVisualizedProperties.Add(nameof(IExpressionItemBase.Id));
            }

            InterfaceVisualizerSettingsAmbientContext.Context = new InterfaceVisualizerSettings(notVisualizedProperties)
            {
                DoNotVisualizeDerivedInterface = false
            };

            ExpressionItemVisualizerSettingsAmbientContext.Context = new ExpressionItemVisualizerSettings(true, false);
        }

        [TearDown]
        public void TestCleanup()
        {
            ExpressionItemVisualizerSettingsAmbientContext.SetDefaultContext();
            InterfaceVisualizerSettingsAmbientContext.SetDefaultContext();
            GlobalsCoreAmbientContext.SetDefaultContext();

            TestSetup.ResetToDefaults();
        }

        private bool TrySaveTestData([NotNull] string simulationIdentifier, [NotNull] string simulationIterationIdentifier,
                                  [NotNull] IExpressionLanguageProvider expressionLanguageProvider,
                                  [NotNull] IParseExpressionResult expectedParseExpressionResult,
                                  [CanBeNull] IParseExpressionResult actualParseParseExpressionResult,
                                  [NotNull] string parsedText, bool isSavedOnFailure, bool saveOnlyIfNotAlreadySaved = true)
        {

#pragma warning disable CS0618, CS0612
            if (IsReRunningSavedSimulation() &&
                !_saveTestDataWhenReRunningSimulation)
                return true;
#pragma warning restore CS0618, CS0612

            if (!isSavedOnFailure)
            {
                if (_testDataWasSaved && saveOnlyIfNotAlreadySaved)
                    return false;

                if (_saveTestDataCriteria == null || !_saveTestDataCriteria.TestDataShouldBeSaved(expectedParseExpressionResult, simulationIdentifier, simulationIterationIdentifier))
                    return false;
            }

            _testDataWasSaved = true;

            LogHelper.Context.Log.InfoFormat("Saving test data for {0}={1}, {2}={3}, {4}={5}.",
                nameof(simulationIdentifier), simulationIdentifier,
                nameof(simulationIterationIdentifier), simulationIterationIdentifier,
                nameof(SimulationRandomNumberGenerator.CreateWithRandomlyGeneratedSeed),
                TestSetup.SimulationRandomNumberGenerator.RandomNumberSeed != null ? TestSetup.SimulationRandomNumberGenerator.RandomNumberSeed.ToString() : "null");

            TestSetup.SimulationRandomNumberGenerator.SaveRandomNumbers();

            TestHelpers.SaveTestData(isSavedOnFailure, _currentTestStartTime, simulationIdentifier, simulationIterationIdentifier,
                parsedText, expressionLanguageProvider, expectedParseExpressionResult, actualParseParseExpressionResult);
            return true;
        }

        // Enable this test to debug the simulated values in TestLanguageProvider
        //[Test]
        //[TestMethod]
        public Task TestRandomValuesAsync()
        {
            for (int i = 0; i < 1000; ++i)
            {
                try
                {
                    TestSetup.SetupSimulationRandomNumberGenerator(_presetRandomNumberGeneratorSeed);
                    TestSetup.PrepareRandomSetupPerTest();
                }
                catch
                {

                }
            }

            return Task.CompletedTask;
        }

        /// <summary>
        /// Use this test to debug issues with operator templates only. 
        /// </summary>
        /// <param name="operatorTemplateIndex">Index of operator template in <see cref="OperatorTemplatesCollection.OperatorTemplates"/></param>
        //[TestCase(0)]
        //[TestCase(1)]
        //[TestCase(2)]
        //[TestCase(3)]
        //[TestCase(4)]
        //[TestCase(5)]
        //[TestCase(6)]
        //[TestCase(7)]
        //[TestCase(8)]
        //[TestCase(9)]
        //[TestCase(10)]
        //[TestCase(11)]
        //[TestCase(12)]
        public Task TestParseSimpleExpressionWithOperatorsOnlyAsync(int operatorTemplateIndex)
        {
            Task RunOneSimulationAsync(string simulationIdentifier, string simulationIterationIdentifier,
                                       IExpressionLanguageProviderWrapper expressionLanguageProvider,
                                       ExpressionParser expressionParser,
                                       OnExpectedRootExpressionGeneratedDelegate onExpectedRootExpressionGenerated,
                                       Action beforeGeneratedCodeParsed,
                                       OnExpressionItemEvaluatedDelegate onExpressionItemEvaluated)
            {
                var generatedCodeStrBldr = new StringBuilder();
                AddRandomText(generatedCodeStrBldr);

                var startIndex = generatedCodeStrBldr.Length;

                var codeGenerator = new CodeGenerator(expressionLanguageProvider, generatedCodeStrBldr);
                var expectedParseExpressionResult = codeGenerator.GenerateSimpleParseExpressionResultWithOperatorsOnly(
                    OperatorTemplatesCollection.OperatorTemplates[operatorTemplateIndex]);

                var expectedPositionInTextOnCompletion = generatedCodeStrBldr.Length;

                var generatedCode = generatedCodeStrBldr.ToString();

                onExpectedRootExpressionGenerated(expectedParseExpressionResult, generatedCode, expectedPositionInTextOnCompletion);

                if (!TurnOffParsing)
                {
                    beforeGeneratedCodeParsed();
                    var actualParseExpressionResult = expressionParser.ParseExpression(
                        ExpressionLanguageProvider.LanguageName, generatedCode,
                            new ParseExpressionOptions
                            {
                                StartIndex = startIndex
                            });


                    onExpressionItemEvaluated(actualParseExpressionResult);
                }

                return Task.CompletedTask;
            }

            return DoRunSimulationAsync(RunOneSimulationAsync, nameof(IExpressionParser.ParseExpression),
                _ => true,
                _ => true);
        }

        private IExpressionLanguageProvider ExpressionLanguageProvider =>
            ExpressionLanguageProviders.TestLanguageProviderForSimulatedSuccessfulParseTests;


        [Test, Order(1)]
        public Task TestSimulation_ParseExpressionAsync()
        {
            Task RunOneSimulationAsync(string simulationIdentifier, string simulationIterationIdentifier,
                                       IExpressionLanguageProviderWrapper expressionLanguageProvider,
                                       ExpressionParser expressionParser,
                                       OnExpectedRootExpressionGeneratedDelegate onExpectedRootExpressionGenerated,
                                       Action beforeGeneratedCodeParsed,
                                       OnExpressionItemEvaluatedDelegate onExpressionItemEvaluated)
            {
                bool simulateEarlyTermination = TestSetup.SimulationRandomNumberGenerator.Next(100) <= 20;

                var generatedCodeStrBldr = new StringBuilder();

                AddRandomText(generatedCodeStrBldr);

                var startIndex = generatedCodeStrBldr.Length;

                var codeGenerator = new CodeGenerator(expressionLanguageProvider, generatedCodeStrBldr);
                var expectedParseExpressionResult = codeGenerator.GenerateParseExpressionResult();

                var expectedPositionInTextOnCompletion = generatedCodeStrBldr.Length;

                if (simulateEarlyTermination)
                {
                    if (expectedParseExpressionResult.SortedCommentedTextData.Count > 0)
                    {
                        var lastComment = expectedParseExpressionResult.SortedCommentedTextData[expectedParseExpressionResult.SortedCommentedTextData.Count - 1];

                        if (lastComment.IsLineComment)
                        {
                            var lastCommentEnd = lastComment.IndexInText + lastComment.ItemLength;

                            bool hasLineBreakAfterComment = false;

                            if (lastCommentEnd < generatedCodeStrBldr.Length)
                            {
                                var charactersAfterLastComment = new char[generatedCodeStrBldr.Length - lastCommentEnd];
                                generatedCodeStrBldr.CopyTo(lastCommentEnd, charactersAfterLastComment, 0, charactersAfterLastComment.Length);
                                hasLineBreakAfterComment = new string(charactersAfterLastComment).Contains(Environment.NewLine);
                            }

                            if (!hasLineBreakAfterComment)
                                generatedCodeStrBldr.Append(Environment.NewLine);
                        }
                    }

                    expectedPositionInTextOnCompletion = generatedCodeStrBldr.Length;

                    generatedCodeStrBldr.Append(TestSetup.EarlyTerminationText);
                    AddRandomText(generatedCodeStrBldr);
                }

                bool IsParsingComplete(IParseExpressionItemContext context, char character, int characterIndex)
                {
                    if (!simulateEarlyTermination)
                        return false;

                    if (context.ParseErrorData.HasCriticalErrors)
                        return false;

                    var parsedText = context.TextSymbolsParser.TextToParse;

                    var earlyTerminationText = TestSetup.EarlyTerminationText;

                    if (characterIndex + earlyTerminationText.Length > parsedText.Length)
                        return false;

                    for (int i = 0; i < earlyTerminationText.Length; ++i)
                    {
                        if (parsedText[characterIndex + i] != earlyTerminationText[i])
                            return false;
                    }

                    return true;
                }

                var generatedCode = generatedCodeStrBldr.ToString();

                onExpectedRootExpressionGenerated(expectedParseExpressionResult, generatedCode, expectedPositionInTextOnCompletion);

                if (!TurnOffParsing)
                {
                    beforeGeneratedCodeParsed();
                    IParseExpressionResult actualParseExpressionResult;
                    if (simulateEarlyTermination || startIndex > 0)
                        actualParseExpressionResult = expressionParser.ParseExpression(ExpressionLanguageProvider.LanguageName,
                            generatedCode,
                            new ParseExpressionOptions
                            {
                                StartIndex = startIndex,
                                IsExpressionParsingComplete = IsParsingComplete
                            });
                    else
                        actualParseExpressionResult = expressionParser.ParseExpression(ExpressionLanguageProvider.LanguageName,
                            generatedCode,
                            new ParseExpressionOptions());

                    onExpressionItemEvaluated(actualParseExpressionResult);
                }

                return Task.CompletedTask;
            }

            return DoRunSimulationAsync(RunOneSimulationAsync, nameof(IExpressionParser.ParseExpression),
                textItemStatistics => false,
                testStatistics => true, true);
        }

        [Test, Order(2)]
        public Task TestSimulation_ParseBracesExpressionAsync()
        {
            Task RunOneSimulationAsync(string simulationIdentifier, string simulationIterationIdentifier,
                                  IExpressionLanguageProviderWrapper expressionLanguageProvider, ExpressionParser expressionParser,
                                  OnExpectedRootExpressionGeneratedDelegate onExpectedRootExpressionGenerated,
                                  Action beforeGeneratedCodeParsed,
                                  OnExpressionItemEvaluatedDelegate onExpressionItemEvaluated)
            {
                var generatedCodeStrBldr = new StringBuilder();

                AddRandomText(generatedCodeStrBldr);

                var startIndex = generatedCodeStrBldr.Length;

                var codeGenerator = new CodeGenerator(expressionLanguageProvider, generatedCodeStrBldr);

                var expectedParseBracesExpressionResult = codeGenerator.GenerateParseBracesExpressionResult();

                var expectedPositionInTextOnCompletion = generatedCodeStrBldr.Length;

                AddRandomText(generatedCodeStrBldr);

                var generatedCode = generatedCodeStrBldr.ToString();

                onExpectedRootExpressionGenerated(expectedParseBracesExpressionResult, generatedCode, expectedPositionInTextOnCompletion);

                if (!TurnOffParsing)
                {
                    beforeGeneratedCodeParsed();
                    var parseBracesExpressionResult = expressionParser.ParseBracesExpression(ExpressionLanguageProvider.LanguageName,
                        generatedCode, new ParseExpressionOptions { StartIndex = startIndex });

                    onExpressionItemEvaluated(parseBracesExpressionResult);
                }

                return Task.CompletedTask;
            }

            return DoRunSimulationAsync(RunOneSimulationAsync, nameof(IExpressionParser.ParseBracesExpression),
                textItemStatistics =>
                    textItemStatistics.StatisticName == nameof(ExpressionItemType.RootExpressionItem),
                textItemStatistics =>
                {
                    if (!textItemStatistics.IsStatisticsPathAMatch(
                        false, true,
                        "DepthStatistics-Details", "DepthStatisticsAtDepth=0"))
                    {
                        return true;
                    }

                    return textItemStatistics.IsStatisticsPathAMatch(
                        false, true,
                        "DepthStatistics-Details", "DepthStatisticsAtDepth=0", "Braces", "RoundBraces") ||
                           textItemStatistics.IsStatisticsPathAMatch(
                               false, true,
                               "DepthStatistics-Details", "DepthStatisticsAtDepth=0", "Braces", "SquareBraces");
                }, true);
        }

        [Test, Order(3)]
        public Task TestSimulation_ParseCodeBlockExpressionAsync()
        {
            var specialOperatorCharacters = new List<char>(SpecialCharactersCacheThreadStaticContext.Context.SpecialOperatorCharacters);

            TestSetup.MustSupportCodeBlocks = true;

            Task RunOneSimulationAsync(string simulationIdentifier, string simulationIterationIdentifier,
                                       IExpressionLanguageProviderWrapper expressionLanguageProvider, ExpressionParser expressionParser,
                                       OnExpectedRootExpressionGeneratedDelegate onExpectedRootExpressionGenerated,
                                       Action beforeGeneratedCodeParsed,
                                       OnExpressionItemEvaluatedDelegate onExpressionItemEvaluated)
            {
                var generatedCodeStrBldr = new StringBuilder();

                AddRandomText(generatedCodeStrBldr);

                var startIndex = generatedCodeStrBldr.Length;

                var codeGenerator = new CodeGenerator(expressionLanguageProvider, generatedCodeStrBldr);
                var expectedParseCodeBlockExpressionResult = codeGenerator.GenerateParseCodeBlockExpressionResult();

                var expectedPositionInTextOnCompletion = generatedCodeStrBldr.Length;

                if (TestSetup.SimulationRandomNumberGenerator.Next(100) <= 50)
                {
                    if (!SpecialCharactersCacheThreadStaticContext.Context.IsSpecialCharacter(generatedCodeStrBldr[generatedCodeStrBldr.Length - 1]))
                    {
                        if (TestSetup.SimulationRandomNumberGenerator.Next(100) <= 50)
                            TestSetup.CodeGenerationHelper.GenerateWhitespacesAndComments(generatedCodeStrBldr, true);
                        else
                            generatedCodeStrBldr.Append(specialOperatorCharacters[TestSetup.SimulationRandomNumberGenerator.Next(specialOperatorCharacters.Count - 1)]);
                    }

                    AddRandomText(generatedCodeStrBldr, true);
                }

                var generatedCode = generatedCodeStrBldr.ToString();

                onExpectedRootExpressionGenerated(expectedParseCodeBlockExpressionResult, generatedCode, expectedPositionInTextOnCompletion);

                if (!TurnOffParsing)
                {
                    var parseCodeBlockExpressionResult = expressionParser.ParseCodeBlockExpression(ExpressionLanguageProvider.LanguageName,
                        generatedCode, new ParseExpressionOptions { StartIndex = startIndex });

                    onExpressionItemEvaluated(parseCodeBlockExpressionResult);
                }

                return Task.CompletedTask;
            }

            return DoRunSimulationAsync(RunOneSimulationAsync, nameof(IExpressionParser.ParseCodeBlockExpression),
                textItemStatistics =>
                    textItemStatistics.StatisticName == nameof(ExpressionItemType.RootExpressionItem),
                textItemStatistics =>
                {
                    if (!textItemStatistics.IsStatisticsPathAMatch(
                        false, true,
                        "DepthStatistics-Details", "DepthStatisticsAtDepth=0"))
                    {
                        return true;
                    }

                    return textItemStatistics.IsStatisticsPathAMatch(
                        false, true,
                        "DepthStatistics-Details", "DepthStatisticsAtDepth=0", "CodeBlock", "CodeBlock");
                }, true);
        }

        /// <summary>
        /// This test should run after <see cref="TestSimulation_ParseExpressionAsync"/>, <see cref="TestSimulation_ParseBracesExpressionAsync"/>,
        /// and <see cref="TestSimulation_ParseCodeBlockExpressionAsync"/>
        /// so that the statistics are ready.
        /// </summary>
        [Test, Order(4)]
        public void ValidateSimulationTestStatistics()
        {
            var allStatisticsSucceeded = true;
            foreach (var testStatisticValidationData in TestStatisticValidations)
            {
                try
                {
                    testStatisticValidationData.ValidateTestStatistics();
                }
                catch //(Exception e)
                {
                    //LogHelper.Context.Log.Error(e)
                    allStatisticsSucceeded = false;
                }
            }

            TestStatisticValidations.Clear();

            if (!allStatisticsSucceeded)
                Assert.Fail("Statistics validation failed.");
        }

        private void AddRandomText([NotNull] StringBuilder generatedCode, bool addRandomTextAlways = false)
        {
            if (TestSetup.SimulateNiceCode || (!addRandomTextAlways && TestSetup.SimulationRandomNumberGenerator.Next(100) <= 50))
                return;

            var randomTextLength = TestSetup.SimulationRandomNumberGenerator.Next(0, 100);

            var textLengthOnStart = generatedCode.Length;

            while (generatedCode.Length - textLengthOnStart < randomTextLength)
            {
                if (TestSetup.SimulationRandomNumberGenerator.Next(100) <= 20)
                {
                    TestSetup.CodeGenerationHelper.GenerateWhitespacesAndComments(generatedCode, true, null, WhitespaceCommentFlags.WhiteSpace);
                }
                else
                {
                    generatedCode.Append(TestSetup.CodeGenerationHelper.GenerateCharacter(
                        new CharacterTypeProbabilityData(GeneratedCharacterType.Dot, 10),
                        new CharacterTypeProbabilityData(GeneratedCharacterType.Letter, 20),
                        new CharacterTypeProbabilityData(GeneratedCharacterType.NonLatinCharacter, 10),
                        new CharacterTypeProbabilityData(GeneratedCharacterType.Number, 20),
                        new CharacterTypeProbabilityData(GeneratedCharacterType.Apostrophe, 5),
                        new CharacterTypeProbabilityData(GeneratedCharacterType.SpecialNonOperatorCharacter, 5),
                        new CharacterTypeProbabilityData(GeneratedCharacterType.SpecialOperatorCharacter, 20),
                        new CharacterTypeProbabilityData(GeneratedCharacterType.Underscore, 10)));
                }
            }
        }

        private async Task ValidateGeneratedExpressionAsync([NotNull] IParseExpressionResult expectedParseExpressionResult,
                                                 [CanBeNull] IParseExpressionResult actualParseParseExpressionResult)
        {
            Assert.IsNotNull(actualParseParseExpressionResult);
            Assert.IsNotNull(expectedParseExpressionResult);

            if (actualParseParseExpressionResult.ParseErrorData.AllParseErrorItems.Count > 0)
            {
                LogHelper.Context.Log.Error(actualParseParseExpressionResult.GetErrorTextWithContextualInformation(actualParseParseExpressionResult.IndexInText, actualParseParseExpressionResult.PositionInTextOnCompletion));
                Assert.Fail("Evaluated expression had parse errors.");
            }

            void OnObjectsAreNotEqual(object expectedObject, object actualObject)
            {
                if (expectedObject is IExpressionItemBase expectedExpressionItem)
                {
                    LogHelper.Context.Log.ErrorFormat("Failed expected expression item data: {0}={1}, {2}={3}.",
                        nameof(IExpressionItemBase.Id), expectedExpressionItem.Id,
                        nameof(IExpressionItemBase.IndexInText), expectedExpressionItem.IndexInText);
                }

                if (actualObject is IExpressionItemBase actualExpressionItem)
                {
                    LogHelper.Context.Log.ErrorFormat("Failed actual expression item data: {0}={1}, {2}={3}.",
                        nameof(IExpressionItemBase.Id), actualExpressionItem.Id,
                        nameof(IExpressionItemBase.IndexInText), actualExpressionItem.IndexInText);
                }
            }

            await TestsHelper.ValidateObjectsAreEqualAsync(expectedParseExpressionResult, actualParseParseExpressionResult,
                GetTaskTimeoutMilliseconds(3000),
                () => "Validate expected and parsed root expression items are equal.",
                (memberInfo) =>
                {
                    switch (memberInfo.Name)
                    {
                        case nameof(IOperatorInfoForTesting.SpecialOperatorNameType):
                        case nameof(IExpressionItemBase.Id):
                            return true;
                        case nameof(IExpressionItemBase.Parent):

                            if (typeof(IExpressionItemBase).IsAssignableFrom(memberInfo.DeclaringType))
                                return true;

                            return false;

                        default:
                            return false;
                    }
                }, OnObjectsAreNotEqual,
                validatedObjectData =>
                {
                    if (!LogValidatedExpressionData)
                        return;

                    if (validatedObjectData.ExpectedParentObjectInfo.Object is IExpressionItemBase expressionItemBase)
                    {
                        LogHelper.Context.Log.InfoFormat("Validating expression. Type={0}, Id={1}, Interface={2}, IndexInText={3}, ItemLength={4}, ObjectId={5}, MemberInfo={6}.",
                            expressionItemBase.GetType().FullName ?? "",
                            expressionItemBase.Id,
                            expressionItemBase.GetMainInterface().Name,
                            expressionItemBase.IndexInText,
                            expressionItemBase.ItemLength,
                            validatedObjectData.ExpectedParentObjectInfo.ObjectId,
                            validatedObjectData.MemberInfo.Name);
                    }
                }).ConfigureAwait(false);

            #region Note, the asserts below will be checked by a call to TestHelpers.ValidateObjectsAreEqual() as well, however lets check this explicitly, 
            // in case the call to TestHelpers.ValidateObjectsAreEqual() misses this bugs
            var errorMessage = "This error should not have happened if we got here.";
            //Assert.AreEqual(0, actualParsedRootExpressionItem.ParseErrorData.ErrorsCount, errorMessage);
            Assert.AreEqual(expectedParseExpressionResult.PositionInTextOnCompletion, actualParseParseExpressionResult.PositionInTextOnCompletion, errorMessage);
            Assert.AreEqual(expectedParseExpressionResult.RootExpressionItem.RegularItems.Count, actualParseParseExpressionResult.RootExpressionItem.RegularItems.Count, errorMessage);
            #endregion
        }

        [Obsolete("Use non null value of _presetRandomNumberGeneratorSeed")]
        private bool IsReRunningSavedSimulation() => _simulationIdentifierToReRun != null && _simulationIterationIndexToReRun != null;

        [NotNull]
        private static string GetSimulationIterationIdentifier(int simulationIterationIndex) => $"Iteration_{simulationIterationIndex}";

        delegate void InitializeParseExpressionResultForTestDelegate(string parsedCode, int indexInText, int itemLength, int positionInTextOnCompletion);

        /// <summary>
        /// Executes the task <paramref name="getTask"/> and cancels after <paramref name="millisecondsAfterWhichToCancel"/>
        /// </summary>
        /// <param name="getTask">A function that returns a task to execute.</param>
        /// <param name="millisecondsAfterWhichToCancel">Maximum milliseconds to allow for the task execution</param>
        /// <param name="taskName">Task name.</param>
        /// <exception cref="OperationCanceledException">Throws this exception.</exception>
        /// <exception cref="Exception">Throws this exception if <paramref name="getTask"/> throws an exception.</exception>
        private Task ExecuteTaskWithCancellationAsync(Func<Task> getTask, int millisecondsAfterWhichToCancel, [NotNull] string taskName)
        {
            try
            {
                var cancellationTokenSource = new CancellationTokenSource();
                cancellationTokenSource.CancelAfter(millisecondsAfterWhichToCancel);
                return getTask().ContinueWith(ignored =>
                {
                }, cancellationTokenSource.Token);
            }
            catch (OperationCanceledException ex)
            {
                LogHelper.Context.Log.Error($"Task {taskName} was canceled after {millisecondsAfterWhichToCancel} milliseconds.", ex);
                throw;
            }
        }
        private int GetTaskTimeoutMilliseconds(int millisecondsAfterWhichToCancel) => TurnOnTimeBasedTaskCancellation ? millisecondsAfterWhichToCancel : int.MaxValue;
        private async Task DoRunSimulationAsync([NotNull] RunOneSimulationAsync runOneSimulationAsync,
                                     [NotNull] string simulationIdentifier,
                                     [NotNull] TextItemStatisticsIsFilteredOut<ITextItem> textItemStatisticsIsFilteredOut,
                                     [NotNull] Func<ITextItemStatistic, bool> getStatisticsIsExpectedToBeNonZero,
                                     bool evaluateTestStatistics = false)
        {
            if (_testDataWasSaved || _simulationFailed)
            {
                LogHelper.Context.Log.InfoFormat("Skipping simulation {0} since previous simulation saved or failed.",
                    simulationIdentifier);
                return;
            }

#pragma warning disable CS0618
            if (IsReRunningSavedSimulation() && _simulationIdentifierToReRun != simulationIdentifier)

            {
                LogHelper.Context.Log.InfoFormat("Skipping simulation {0} since currently re-running saved iteration '{1}' of simulation '{2}'.",
                    simulationIdentifier,
                    // ReSharper disable once AssignNullToNotNullAttribute
                    _simulationIterationIndexToReRun, _simulationIdentifierToReRun);
                return;
            }
#pragma warning restore CS0618

            ITextItemStatistics textItemStatistics = evaluateTestStatistics ? TestHelpers.CreateTextItemStatistics(simulationIdentifier, textItemStatisticsIsFilteredOut) : null;

            var iterationStart = 0;
            var iterationEnd = NumberOfSimulationRuns;

#pragma warning disable CS0618
            var reuseSavedRandomNumbers = false;
            if (_simulationIdentifierToReRun == simulationIdentifier &&
                _simulationIterationIndexToReRun != null)
            {

                reuseSavedRandomNumbers = SaveRandomNumbersToRerunSimulations;

                iterationStart = _simulationIterationIndexToReRun.Value;
                iterationEnd = iterationStart + 1;
            }
#pragma warning restore CS0618

            // We generate new seed per test and if the seed is preset, all iterations will result in same simulated
            // code. No need to repeat multiple times.
            if (_presetRandomNumberGeneratorSeed != null)
            {
                iterationStart = 0;
                iterationEnd = 1;
                //iterationEnd = iterationStart + 1;
            }
                

            LogHelper.Context.Log.InfoFormat("Started simulation {0}. IterationStart={1}. IterationEnd={2}",
                          simulationIdentifier, iterationStart, iterationEnd);

            string lastIterationIdentifier = null;

            IExpressionLanguageProviderWrapper lastIterationExpressionLanguageProviderWrapper = null;
            IParseExpressionResult lastIterationExpectedParseExpressionResult = null;
            IParseExpressionResult lastIterationActualParseExpressionResult = null;
            string lastIterationGeneratedCode = null;

            try
            {
                for (var simulationIterationIndex = iterationStart; simulationIterationIndex < iterationEnd; ++simulationIterationIndex)
                {
                    try
                    {
                        if (StopSimulation)
                            return;

                        /*#region TEMP-DELETE
                        if (simulationIterationIndex < seeds.Length)
                            _presetRandomNumberGeneratorSeed = seeds[simulationIterationIndex];
                        else
                            _presetRandomNumberGeneratorSeed = randomSeedGenerator.Next(0, int.MaxValue);
                        #endregion*/

                        TestSetup.SetupSimulationRandomNumberGenerator(_presetRandomNumberGeneratorSeed);

                        lastIterationIdentifier = GetSimulationIterationIdentifier(simulationIterationIndex);
                        TestSetup.SimulationRandomNumberGenerator.OnSimulationIterationStarting(simulationIdentifier, lastIterationIdentifier,
                            reuseSavedRandomNumbers);

                        TestSetup.PrepareRandomSetupPerTest();

                        if ((simulationIterationIndex - iterationStart) % 20 == 0)
                        {
                            LogHelper.Context.Log.InfoFormat("{0}={1}, {2}={3}",
                                nameof(simulationIterationIndex), simulationIterationIndex,
                                nameof(SimulationRandomNumberGenerator.RandomNumberSeed),
                                TestSetup.SimulationRandomNumberGenerator.RandomNumberSeed ?? -1);
                        }

                        GlobalsCoreAmbientContext.Context = new GlobalsCoreTest(new GlobalsCore());

                        Task CreateExpressionLanguageProviderCacheAsync()
                        {
                            // TestSetup.PrepareRandomSetupPerTest() will create a new instance of ExpressionLanguageProviderCache, so each run 
                            // will use a new instance of IExpressionLanguageProviderWrapper.
                            lastIterationExpressionLanguageProviderWrapper = TestSetup.ExpressionLanguageProviderCache.GetExpressionLanguageProviderWrapper(ExpressionLanguageProvider.LanguageName);
                            return Task.CompletedTask;
                        }

                        lastIterationExpressionLanguageProviderWrapper = null;

                        await TestsHelper.ExecuteTaskWithCancellationAsync(CreateExpressionLanguageProviderCacheAsync,
                            GetTaskTimeoutMilliseconds(5000), nameof(CreateExpressionLanguageProviderCacheAsync)).ConfigureAwait(false);

                        Task RunOneSimulationAsync()
                        {
                            return runOneSimulationAsync(simulationIdentifier, lastIterationIdentifier, lastIterationExpressionLanguageProviderWrapper,
                            TestSetup.ExpressionParser,

                            (parseExpressionResult, generatedCode, expectedPositionInTextOnCompletion) =>
                            {
                                lastIterationExpectedParseExpressionResult = parseExpressionResult;
                                lastIterationGeneratedCode = generatedCode;

                                InitializeParseExpressionResultForTestDelegate initializeParseExpressionResultForTestDelegate;

                                if (parseExpressionResult is ParseExpressionResult parseExpressionResultImpl)
                                    initializeParseExpressionResultForTestDelegate = parseExpressionResultImpl.InitializeForTest;
                                else
                                    throw new Exception();

                                initializeParseExpressionResultForTestDelegate(generatedCode,
                                    parseExpressionResult.GetIndexInText(),
                                    parseExpressionResult.GetItemLength(), expectedPositionInTextOnCompletion);

                                TestHelpers.ValidateExpressionItem(parseExpressionResult, generatedCode);

                                textItemStatistics?.UpdateStatisticsFromParseExpressionResult(parseExpressionResult);

                                SetupForParse(ExpressionLanguageProvider, TestSetup.ExpressionParser, parseExpressionResult);

                                //// Set the condition below to correct simulation identifier and iteration to save the simulation data, before running the simulation.
                                //if (simulationIdentifier == nameof(IExpressionParser.EvaluateExpression)  &&
                                //    simulationIterationIndex == 0 &&
                                //    _saveTestDataCriteria is SaveAnyIterationTestDataCriteria)
                                //{
                                //    TrySaveTestData(simulationIdentifier, lastIterationIdentifier,
                                //        ExpressionLanguageProvider,
                                //        lastIterationExpectedParseExpressionResult, null,
                                //        lastIterationGeneratedCode, false);
                                //}

                            },
                            () => GlobalsCoreAmbientContext.Context = new GlobalsCoreTest(new GlobalsCore()),
                            actualRootExpression => lastIterationActualParseExpressionResult = actualRootExpression);

                        }

                        GlobalsCoreAmbientContext.Context = new GlobalsCoreTest(new GlobalsCore());
                        await TestsHelper.ExecuteTaskWithCancellationAsync(RunOneSimulationAsync,
                            GetTaskTimeoutMilliseconds(10000), nameof(RunOneSimulationAsync)).ConfigureAwait(false);

                        await ValidateGeneratedExpressionAsync(lastIterationExpectedParseExpressionResult, lastIterationActualParseExpressionResult).ConfigureAwait(false);

                        if (simulationIterationIndex - iterationStart < NumberOfIterationsToAlwaysSave)
                            TrySaveTestData(simulationIdentifier, lastIterationIdentifier,
                                ExpressionLanguageProvider,
                                lastIterationExpectedParseExpressionResult, lastIterationActualParseExpressionResult,
                                lastIterationGeneratedCode, false, false);
                        else if (TrySaveTestData(simulationIdentifier, lastIterationIdentifier,
                            ExpressionLanguageProvider,
                            lastIterationExpectedParseExpressionResult, lastIterationActualParseExpressionResult,
                            lastIterationGeneratedCode, false))
                        {
                            break;
                        }

                    }
                    finally
                    {
                        GlobalsCoreAmbientContext.SetDefaultContext();
                    }
                }

                if (textItemStatistics != null &&
#pragma warning disable CS0618
                    !IsReRunningSavedSimulation()
#pragma warning restore CS0618
                   )
                {
                    TestHelpers.SaveTestStatistics(textItemStatistics, simulationIdentifier);

                    if (iterationEnd - iterationStart >= 100 && TestSetup.OperatorNameType == OperatorNameType.NotNiceOperatorNamesWithMultipleParts)
                    {
                        TestStatisticValidations.Add(new TestStatisticValidationData(simulationIdentifier, iterationEnd - iterationStart,
                            textItemStatistics, getStatisticsIsExpectedToBeNonZero));
                        //TestHelpers.ValidateTestStatistics(textItemStatistics, getStatisticsIsExpectedToBeNonZero);
                    }
                }

                LogHelper.Context.Log.InfoFormat("Completed simulation {0}.", simulationIdentifier);
            }
            catch (Exception e)
            {
                _simulationFailed = true;
                LogHelper.Context.Log.Error(e);

                if (lastIterationIdentifier != null)
                    LogHelper.Context.Log.InfoFormat("Failed simulation data:  {0}={1}, {2}={3}, {4}={5}.",
                        nameof(simulationIdentifier), simulationIdentifier,
                        nameof(lastIterationIdentifier), lastIterationIdentifier,
                        nameof(SimulationRandomNumberGenerator.CreateWithRandomlyGeneratedSeed),
                        TestSetup.SimulationRandomNumberGenerator.RandomNumberSeed != null ?
                            TestSetup.SimulationRandomNumberGenerator.RandomNumberSeed.ToString() : "null");

                if (!TrySaveTestData(simulationIdentifier, lastIterationIdentifier,
                    ExpressionLanguageProvider,
                    lastIterationExpectedParseExpressionResult, lastIterationActualParseExpressionResult,
                    lastIterationGeneratedCode, true))
                    Assert.Fail("Test data should always be saved on test failure.");

                throw;
            }
        }

        //private static int TemporarilyDisableStats = 1;

        private void SetupForParse([NotNull] IExpressionLanguageProvider expressionLanguageProvider, [NotNull] ExpressionParser expressionParser, [NotNull] IParseExpressionResult parseExpressionResult)
        {
            var operatorPositionToOperatorInfoExpressionItem = new Dictionary<int, IOperatorInfoExpressionItem>();

            Helpers.ProcessExpressionItem(parseExpressionResult.RootExpressionItem, (expressionItem) =>
            {
                if (expressionItem is IOperatorInfoExpressionItem operatorInfoExpressionItem)
                    operatorPositionToOperatorInfoExpressionItem[operatorInfoExpressionItem.IndexInText] = operatorInfoExpressionItem;
                return true;
            });


            expressionParser.IgnoreOperatorInfoDelegate = (operatorInfoExpressionItem) =>
            {
                // We are parsing non-operator. Let the parser handle this in its own way.
                if (!operatorPositionToOperatorInfoExpressionItem.TryGetValue(operatorInfoExpressionItem.IndexInText, out var expectedOperatorInfoExpressionItem))
                    return false;

                // If operator has less parts, expression parser will not pick it over the expected operator anyway.
                if (operatorInfoExpressionItem.OperatorInfo.NameParts.Count < expectedOperatorInfoExpressionItem.OperatorInfo.NameParts.Count)
                    return false;

                var stringComparison = expressionLanguageProvider.IsLanguageCaseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;

                for (var namePartIndex = 0; namePartIndex < expectedOperatorInfoExpressionItem.OperatorInfo.NameParts.Count - 1; ++namePartIndex)
                {
                    if (operatorInfoExpressionItem.OperatorNameParts[namePartIndex].IndexInText != expectedOperatorInfoExpressionItem.OperatorNameParts[namePartIndex].IndexInText ||
                       !string.Equals(operatorInfoExpressionItem.OperatorNameParts[namePartIndex].Text, expectedOperatorInfoExpressionItem.OperatorNameParts[namePartIndex].Text, stringComparison))
                        return false;
                }

                var lastActualOperatorNamePart = operatorInfoExpressionItem.OperatorNameParts[expectedOperatorInfoExpressionItem.OperatorNameParts.Count - 1];
                var lastExpectedOperatorNamePart = expectedOperatorInfoExpressionItem.OperatorNameParts[expectedOperatorInfoExpressionItem.OperatorNameParts.Count - 1];

                if (lastActualOperatorNamePart.IndexInText != lastExpectedOperatorNamePart.IndexInText)
                    return false;

                if (!lastActualOperatorNamePart.Text.StartsWith(lastExpectedOperatorNamePart.Text, stringComparison) ||
                    !(lastActualOperatorNamePart.Text.Length > lastExpectedOperatorNamePart.Text.Length ||
                      operatorInfoExpressionItem.OperatorInfo.NameParts.Count > expectedOperatorInfoExpressionItem.OperatorInfo.NameParts.Count))
                    return false;

                // If we got here the actual operator expression item has at least as many parts as the expected expression item,
                // the first expectedOperatorInfoExpressionItem.OperatorNameParts.Count parts start at the same positions and are equal to expected expression items,
                // the last actual expression item part at position expectedOperatorInfoExpressionItem.OperatorNameParts.Count contains the last part in expected expression item,
                // and either the last part in actual expression item is longer than the last part in expected expression item, or the actual expression item has more parts.

                return true;
            };

            expressionParser.IgnoreBinaryOperatorWhenBreakingPostfixPrefixTie += (operatorInfoExpressionItem) =>
                !operatorPositionToOperatorInfoExpressionItem.TryGetValue(operatorInfoExpressionItem.IndexInText, out var expectedOperatorInfoExpressionItem) ||
                expectedOperatorInfoExpressionItem.OperatorInfo.OperatorType != OperatorType.BinaryOperator;
        }

        private interface ISaveTestDataCriteria
        {
            bool TestDataShouldBeSaved([NotNull] IParseExpressionResult parseExpressionResult, [NotNull] string simulationIdentifier, [NotNull] string simulationIterationIdentifier);
        }

        public class SaveAnyIterationTestDataCriteria : ISaveTestDataCriteria
        {
            /// <inheritdoc />
            public bool TestDataShouldBeSaved(IParseExpressionResult parseExpressionResult, string simulationIdentifier,
                string simulationIterationIdentifier)
            {
                return true;
            }
        }

        public class SaveSimulationIterationCriteria : ISaveTestDataCriteria
        {
            [NotNull]
            private readonly string _simulationIdentifierToSave;

            private readonly string _simulationIterationIdentifierToSave;

            public SaveSimulationIterationCriteria([NotNull] string simulationIdentifierToSave, string simulationIterationIdentifierToSave)
            {
                _simulationIdentifierToSave = simulationIdentifierToSave;
                _simulationIterationIdentifierToSave = simulationIterationIdentifierToSave;
            }
            /// <inheritdoc />
            public bool TestDataShouldBeSaved(IParseExpressionResult parseExpressionResult, string simulationIdentifier, string simulationIterationIdentifier)
            {
                return simulationIdentifier == _simulationIdentifierToSave && simulationIterationIdentifier == _simulationIterationIdentifierToSave;
            }
        }

        public class SaveIfExpectedRootExpressionSatisfiesCriteria : ISaveTestDataCriteria
        {
            [NotNull]
            private readonly Func<IExpressionItemBase, bool> _predicate;

            public SaveIfExpectedRootExpressionSatisfiesCriteria([NotNull] Func<IExpressionItemBase, bool> predicate)
            {
                _predicate = predicate;
            }

            /// <inheritdoc />
            public bool TestDataShouldBeSaved(IParseExpressionResult parseExpressionResult, string simulationIdentifier, string simulationIterationIdentifier)
            {
                bool criteriaSatisfied = false;
                bool ProcessExpressionItem(IExpressionItemBase expressionItem)
                {
                    if (criteriaSatisfied)
                        return true;

                    criteriaSatisfied = _predicate(expressionItem);
                    return !criteriaSatisfied;
                }

                Helpers.ProcessExpressionItem(parseExpressionResult.RootExpressionItem, ProcessExpressionItem);

                return criteriaSatisfied;
            }
        }

        public class TestStatisticValidationData
        {
            [NotNull]
            private readonly string _simulationIdentifier;

            private readonly int _numberOfSimulationIterations;

            [NotNull]
            private readonly ITextItemStatistics _textItemStatistics;

            [NotNull]
            private readonly Func<ITextItemStatistic, bool> _getStatisticsIsExpectedToBeNonZero;

            public TestStatisticValidationData([NotNull] string simulationIdentifier, int numberOfSimulationIterations,
                                               [NotNull] ITextItemStatistics textItemStatistics,
                                               [NotNull] Func<ITextItemStatistic, bool> getStatisticsIsExpectedToBeNonZero)
            {
                _simulationIdentifier = simulationIdentifier;
                _numberOfSimulationIterations = numberOfSimulationIterations;
                _textItemStatistics = textItemStatistics;
                _getStatisticsIsExpectedToBeNonZero = getStatisticsIsExpectedToBeNonZero;
            }

            public void ValidateTestStatistics()
            {
                try
                {
                    TestHelpers.ValidateTestStatistics(_textItemStatistics, _getStatisticsIsExpectedToBeNonZero);
                }
                catch
                {
                    LogHelper.Context.Log.ErrorFormat("Validation of statistics for simulation '{0}' failed. Number of iterations was {1}.",
                        _simulationIdentifier, _numberOfSimulationIterations);
                    throw;
                }
            }
        }
    }
}
