using JetBrains.Annotations;
using NUnit.Framework;
using OROptimizer;
using OROptimizer.Diagnostics.Log;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ClassVisualizer;
using OROptimizer.Log4Net;
using TestsSharedLibrary.TestSimulation.Statistics;
using TestsSharedLibraryForCodeParsers.CodeGeneration;
using UniversalExpressionParser.ExpressionItems;
using UniversalExpressionParser.Tests.TestLanguage;
using UniversalExpressionParser.Tests.TestStatistics;
using UniversalExpressionParser.Tests.Utilities.ExpressionItemVisualizers;

namespace UniversalExpressionParser.Tests
{
    public static class TestHelpers
    {
        static TestHelpers()
        {

            TestFilesFolderPath = Path.Combine(Path.GetDirectoryName(typeof(Helpers).Assembly.Location)!, "TestFiles");
            FailedTestDataFolderPath = Path.Combine(TestFilesFolderPath, "FailedTestData");

            EnsureDirectoryExists(TestFilesFolderPath);
            EnsureDirectoryExists(FailedTestDataFolderPath);

            ValueVisualizerFactory = new ValueVisualizerFactoryForTests(new InterfacePropertyVisualizationHelper(), new AttributeValueSanitizer());
            SaveVisualizedInterface = new SaveVisualizedInterface(ValueVisualizerFactory, new ObjectVisualizationContextFactory());
        }

        public static void RegisterLogger()
        {
            if (LogHelper.IsContextInitialized)
            {
                if (!(LogHelper.Context is Log4NetHelperContext))
                    throw new Exception($"The value of LogHelper.Context should be of type {typeof(Log4NetHelperContext)}.");

                return;
            }

            LogHelper.RegisterContext(new Log4NetHelperContext("UniversalExpressionParser.Tests.log4net.config"));
        }

        public static ISaveVisualizedInterface SaveVisualizedInterface { get; }
        public static IValueVisualizerFactory ValueVisualizerFactory { get; }

        public static void EnsureDirectoryExists(string directoryPath)
        {
            var directoryInfo = new DirectoryInfo(directoryPath);

            LinkedList<DirectoryInfo> directoryInfosToCreate = new LinkedList<DirectoryInfo>();

            while (directoryInfo != null && !directoryInfo.Exists)
            {
                directoryInfosToCreate.AddFirst(directoryInfo);
                directoryInfo = directoryInfo.Parent;
            }

            foreach (var directoryInfoToCreate in directoryInfosToCreate)
                directoryInfoToCreate.Create();
        }

        public static string TestFilesFolderPath { get; }
        public static string FailedTestDataFolderPath { get; }

        public static string GenerateRandomWord([NotNull] IReadOnlyList<char> allowedOperatorCharacters,
                                                [NotNull] Func<string, bool> hasConflictsWithOtherIdentifiers)
        {
            var numberOfCharacters = TestSetup.SimulationRandomNumberGenerator.Next(100) <= 80 ?
                TestSetup.SimulationRandomNumberGenerator.Next(1, TestSetup.MaxLengthOfSmallWords) :
                TestSetup.SimulationRandomNumberGenerator.Next(TestSetup.MaxLengthOfSmallWords + 1, TestSetup.MaxLengthOfLongWords);

            return GenerateRandomWord(numberOfCharacters, allowedOperatorCharacters, allowedOperatorCharacters,
                allowedOperatorCharacters, hasConflictsWithOtherIdentifiers);
        }

        public static string GenerateRandomWord(int numberOfCharacters,
                                                [NotNull] IReadOnlyList<char> allowedStartingOperatorCharacters,
                                                [NotNull] IReadOnlyList<char> allowedMiddleOperatorCharacters,
                                                [NotNull] IReadOnlyList<char> allowedEndingOperatorCharacters,
                                                [NotNull] Func<string, bool> hasConflictsWithOtherIdentifiers)
        {
            int currentNumberOfCharacters = numberOfCharacters;

            while (true)
            {
                for (var numberOfTrials = 0; numberOfTrials < 20; ++numberOfTrials)
                {
                    var generatedTextStrBldr = new StringBuilder();

                    while (generatedTextStrBldr.Length < currentNumberOfCharacters)
                    {
                        char generatedCharacter;
                        if (generatedTextStrBldr.Length == 0)
                        {
                            if (allowedStartingOperatorCharacters.Count > 0 && TestSetup.SimulationRandomNumberGenerator.Next(100) <= 33)
                            {
                                generatedCharacter = allowedStartingOperatorCharacters[TestSetup.SimulationRandomNumberGenerator.Next(
                                    allowedStartingOperatorCharacters.Count - 1)];
                            }
                            else
                            {
                                generatedCharacter = TestSetup.CodeGenerationHelper.GenerateCharacter(
                                    new CharacterTypeProbabilityData(GeneratedCharacterType.Letter, 60),
                                    new CharacterTypeProbabilityData(GeneratedCharacterType.Underscore, 40));
                            }
                        }
                        else
                        {
                            var allowedOperatorCharacters = generatedTextStrBldr.Length == currentNumberOfCharacters - 1 ? allowedEndingOperatorCharacters : allowedMiddleOperatorCharacters;

                            if (allowedOperatorCharacters.Count > 0 && TestSetup.SimulationRandomNumberGenerator.Next(100) <= 33)
                            {
                                generatedCharacter = allowedOperatorCharacters[TestSetup.SimulationRandomNumberGenerator.Next(
                                    allowedOperatorCharacters.Count - 1)];
                            }
                            else
                            {
                                generatedCharacter = TestSetup.CodeGenerationHelper.GenerateCharacter(
                                    new CharacterTypeProbabilityData(GeneratedCharacterType.Letter, 60),
                                    new CharacterTypeProbabilityData(GeneratedCharacterType.Underscore, 20),
                                    new CharacterTypeProbabilityData(GeneratedCharacterType.Number, 10),
                                    new CharacterTypeProbabilityData(GeneratedCharacterType.Dot, 10));
                            }
                        }

                        generatedTextStrBldr.Append(generatedCharacter);
                    }

                    var generatedText = generatedTextStrBldr.ToString();

                    if (!hasConflictsWithOtherIdentifiers(generatedText))
                        return generatedText;
                }

                // If we could not generate a word that does not conflict with other identifiers, lets increase currentNumberOfCharacters.
                ++currentNumberOfCharacters;
            }
        }

        public static void ValidateExpressionItem([NotNull] IParseExpressionResult parseExpressionResult, [NotNull] string parsedCode)
        {
            Dictionary<long, IExpressionItemBase> processedExpressionItems = new Dictionary<long, IExpressionItemBase>();
            HashSet<long> parenExpressionItemsWithValidateStartIndex = new HashSet<long>();

            Assert.IsTrue(parseExpressionResult.IndexInText >= 0 && parseExpressionResult.ItemLength >= 0 &&
                          parseExpressionResult.IndexInText + parseExpressionResult.ItemLength <= parsedCode.Length);
            Assert.IsTrue(parseExpressionResult.PositionInTextOnCompletion >= 0 &&
                          parseExpressionResult.PositionInTextOnCompletion <= parsedCode.Length);

            ICommentedTextData previousCommentedTextData = null;

            foreach (var commentedTextData in parseExpressionResult.SortedCommentedTextData)
            {
                if (previousCommentedTextData != null)
                {
                    Assert.IsTrue(previousCommentedTextData.IndexInText + previousCommentedTextData.ItemLength <=
                                  commentedTextData.IndexInText);
                }

                Assert.IsTrue(commentedTextData.IndexInText >= 0 && commentedTextData.ItemLength >= 0);

                previousCommentedTextData = commentedTextData;
            }

            ITextExpressionItem lastTextExpressionItem = null;
            Helpers.ProcessExpressionItem(parseExpressionResult.RootExpressionItem, (expressionItem) =>
            {
                if (processedExpressionItems.TryGetValue(expressionItem.Id, out var processedExpressionItemsWithSameId))
                {
                    Assert.AreSame(expressionItem, processedExpressionItemsWithSameId);
                }
                else
                {
                    processedExpressionItems[expressionItem.Id] = expressionItem;
                }

                if (expressionItem is ITextExpressionItem nameExpressionItem)
                {
                    Assert.IsTrue(nameExpressionItem.IndexInText >= 0 && nameExpressionItem.ItemLength > 0
                                                                      && nameExpressionItem.IndexInText + nameExpressionItem.ItemLength <= parsedCode.Length);

                    Assert.IsTrue(string.Equals(nameExpressionItem.Text, parsedCode.Substring(nameExpressionItem.IndexInText, nameExpressionItem.ItemLength), StringComparison.Ordinal));

                    if (lastTextExpressionItem != null)
                        Assert.IsTrue(lastTextExpressionItem.IndexInText + lastTextExpressionItem.ItemLength <= nameExpressionItem.IndexInText);

                    var parentExpressionItem = nameExpressionItem.Parent;

                    while (parentExpressionItem != null)
                    {
                        if (parenExpressionItemsWithValidateStartIndex.Contains(parentExpressionItem.Id))
                            break;

                        Assert.AreEqual(nameExpressionItem.IndexInText, parentExpressionItem.IndexInText);

                        parenExpressionItemsWithValidateStartIndex.Add(parentExpressionItem.Id);

                        parentExpressionItem = parentExpressionItem.Parent;
                    }

                    lastTextExpressionItem = nameExpressionItem;
                }
                else if (expressionItem is IComplexExpressionItem complexExpressionItem)
                {
                    int indexInAllItems = 0;
                    IExpressionItemBase previousExpressionItem = null;
                    foreach (var currentExpressionItem in complexExpressionItem.AllItems)
                    {
                        Assert.IsTrue(currentExpressionItem.IndexInText >= 0 && currentExpressionItem.ItemLength > 0);

                        if (previousExpressionItem != null)
                            Assert.IsTrue(currentExpressionItem.IndexInText >= previousExpressionItem.IndexInText + previousExpressionItem.ItemLength);

                        IExpressionItemBase expressionItemInSpecificList;

                        if (indexInAllItems < complexExpressionItem.Prefixes.Count)
                            expressionItemInSpecificList = complexExpressionItem.Prefixes[indexInAllItems];
                        else if (indexInAllItems < complexExpressionItem.Prefixes.Count + complexExpressionItem.AppliedKeywords.Count)
                            expressionItemInSpecificList = complexExpressionItem.AppliedKeywords[indexInAllItems - complexExpressionItem.Prefixes.Count];
                        else if (indexInAllItems < complexExpressionItem.Prefixes.Count + complexExpressionItem.AppliedKeywords.Count + complexExpressionItem.RegularItems.Count)
                            expressionItemInSpecificList = complexExpressionItem.RegularItems[indexInAllItems -
                                                                                                 (complexExpressionItem.Prefixes.Count +
                                                                                                  complexExpressionItem.AppliedKeywords.Count)];
                        else
                            expressionItemInSpecificList = complexExpressionItem.Postfixes[indexInAllItems -
                                                                                                 (complexExpressionItem.Prefixes.Count +
                                                                                                  complexExpressionItem.AppliedKeywords.Count +
                                                                                                  complexExpressionItem.RegularItems.Count)];

                        Assert.IsNotNull(expressionItemInSpecificList);
                        Assert.AreSame(expressionItemInSpecificList, currentExpressionItem);

                        previousExpressionItem = currentExpressionItem;
                        ++indexInAllItems;
                    }
                }

                return true;
            }, null,
            (complexExpressionItem) =>
            {
                if (lastTextExpressionItem == null)
                {
                    Assert.IsTrue(complexExpressionItem is IRootExpressionItem rootExpressionItem &&
                                  rootExpressionItem.Children.Count == 0);
                }
                else
                {
                    Assert.IsTrue(complexExpressionItem.IndexInText + complexExpressionItem.ItemLength ==
                                  lastTextExpressionItem.IndexInText + lastTextExpressionItem.ItemLength);
                }

                return true;
            });
        }

        private static string GetTestDataFolder()
        {
            return Path.Combine(TestHelpers.TestFilesFolderPath, "CurrentTestData");
        }

        private static string GetTestDataFolderForFailedTests([NotNull] string simulationIdentifier, [NotNull] string simulationIterationIdentifier,
                                                              DateTime testDateTime)
        {
            return Path.Combine(TestHelpers.FailedTestDataFolderPath,
                string.Format("{0}_{1}_{2}", simulationIdentifier, simulationIterationIdentifier, testDateTime.ToString("MM-dd-yyyy_HH-mm-ss")));
        }

        private static void SaveExpressionItem([NotNull] IParseExpressionResult parseExpressionResult, [NotNull]Type mainInterfaceType, [NotNull] string savedFilePath)
        {
            var globalsCoreTestCurrent = GlobalsCoreAmbientContext.Context;

            try
            {
                GlobalsCoreAmbientContext.Context = new GlobalsCoreTest(new GlobalsCore());
                SaveVisualizedInterface.Save(parseExpressionResult, mainInterfaceType, savedFilePath);
            }
            finally
            {
                GlobalsCoreAmbientContext.Context = globalsCoreTestCurrent;
            }
        }

        public static void SaveTestData(bool isSavedOnTestFailure,
                                        DateTime testStartDateTime,
                                        [NotNull] string simulationIdentifier, [NotNull] string simulationIterationIdentifier,
                                        [NotNull] string codeToParse,
                                        [NotNull] IExpressionLanguageProvider expressionLanguageProvider,
                                        [NotNull] IParseExpressionResult expectedParseExpressionResult,
                                        [CanBeNull] IParseExpressionResult actualParseParseExpressionResult)
        {
            var directoryPath = GetTestDataFolder();

            EnsureDirectoryExists(directoryPath);

            var mainInterfaceType = typeof(IParseExpressionResult);

            SaveVisualizedInterface.Save(expressionLanguageProvider, typeof(IExpressionLanguageProvider), Path.Combine(directoryPath, "ExpressionLanguageProvider.xml"));

            SaveExpressionItem(expectedParseExpressionResult, mainInterfaceType, Path.Combine(directoryPath, "ExpectedRootExpression.xml"));

            if (actualParseParseExpressionResult != null)
                SaveExpressionItem(actualParseParseExpressionResult, mainInterfaceType, Path.Combine(directoryPath, "ActualRootExpression.xml"));

            var fileName = isSavedOnTestFailure ? "CodeToParse.txt" : $"CodeToParse_{simulationIdentifier}_{simulationIterationIdentifier}.txt";

            using (var fileStream = new StreamWriter(Path.Combine(directoryPath, fileName), false))
            {
                fileStream.Write(codeToParse);
            }

            if (isSavedOnTestFailure)
                TestSetup.SimulationRandomNumberGenerator.SaveRandomNumbers(Path.Combine(directoryPath, "SimulationData.xml"));
        }

        public static void SaveTestStatistics([NotNull] ITextItemStatistics textItemStatistics, [NotNull] string simulationIdentifier)
        {
            TestHelpers.SaveVisualizedInterface.Save(textItemStatistics, typeof(ITextItemStatistics),
                Path.Combine(TestFilesFolderPath, $"TestStatistics_{simulationIdentifier}.xml"));
        }

        private static void AddBracesStatisticsToGroup([NotNull] ITextItemStatisticGroup bracesExpressionItemsGroup,
                                                       [NotNull] TextItemStatisticsIsFilteredOut<ITextItem> textItemStatisticsIsFilteredOut,
                                                       [CanBeNull] Func<ITextItemStatistic, ITextItemStatistic> transformBracesStatistic = null)
        {
            AddBracesStatisticsToGroup(bracesExpressionItemsGroup, true, true, textItemStatisticsIsFilteredOut, transformBracesStatistic);
            AddBracesStatisticsToGroup(bracesExpressionItemsGroup, true, false, textItemStatisticsIsFilteredOut, transformBracesStatistic);
            AddBracesStatisticsToGroup(bracesExpressionItemsGroup, false, true, textItemStatisticsIsFilteredOut, transformBracesStatistic);
            AddBracesStatisticsToGroup(bracesExpressionItemsGroup, false, false, textItemStatisticsIsFilteredOut, transformBracesStatistic);
        }

        private static void AddBracesStatisticsToGroup([NotNull] ITextItemStatisticGroup bracesExpressionItemsGroup,
                                                       bool isNamedBraces, bool isRoundBraces,
                                                       [NotNull] TextItemStatisticsIsFilteredOut<ITextItem> textItemStatisticsIsFilteredOut,
                                                       [CanBeNull] Func<BracesExpressionItemStatistic, ITextItemStatistic> transformBracesStatistic = null)
        {
            var subgroupName = new StringBuilder();

            if (isNamedBraces)
                subgroupName.Append("Named");

            subgroupName.Append(isRoundBraces ? "Round" : "Square");

            subgroupName.Append("Braces");

            var bracesExpressionItemsSubGroup = new ExclusiveTextItemStatisticGroup(subgroupName.ToString(), textItemStatisticsIsFilteredOut);
            bracesExpressionItemsGroup.AddChildStatistic(bracesExpressionItemsSubGroup);

            for (var paramCount = 0; paramCount <= TestSetup.MaxNumberOfParametersInFunctionOrMatrix; ++paramCount)
            {
                var bracesExpressionItemStatistic = new BracesExpressionItemStatistic(isNamedBraces, isRoundBraces, paramCount);

                bracesExpressionItemsSubGroup.AddChildStatistic(transformBracesStatistic == null ? bracesExpressionItemStatistic :
                    transformBracesStatistic(bracesExpressionItemStatistic));
            }
        }

        private static void AddCodeBlockStatisticsToGroup([NotNull] ITextItemStatisticGroup codeBlockExpressionItemsGroup,
                                                          bool isPostfixCodeBlock,
                                                          [NotNull] TextItemStatisticsIsFilteredOut<ITextItem> textItemStatisticsIsFilteredOut,
                                                          [CanBeNull] Func<ITextItemStatistic, ITextItemStatistic> transformCodeBlockStatistic = null)
        {
            var subgroupName = new StringBuilder();

            if (isPostfixCodeBlock)
                subgroupName.Append("Postfix:");

            subgroupName.Append(ExpressionItemType.CodeBlock);

            var codeBlockExpressionItemsSubGroup = new ExclusiveTextItemStatisticGroup(subgroupName.ToString(), textItemStatisticsIsFilteredOut);
            codeBlockExpressionItemsGroup.AddChildStatistic(codeBlockExpressionItemsSubGroup);

            for (var childrenCount = 0; childrenCount <= TestSetup.MaxNumberOfExpressionsInCodeBlock; ++childrenCount)
            {
                TextItemStatistic codeBlockStatistic = new ExpressionItemWithChildrenCountDecoratorStatistic(new ExpressionItemStatistic(ExpressionItemType.CodeBlock), childrenCount);

                codeBlockStatistic = new PrefixRegularPostfixExpressionItemDecoratorStatistic(codeBlockStatistic,
                    isPostfixCodeBlock ? SpecialExpressionItemType.Postfix : SpecialExpressionItemType.Regular);

                codeBlockExpressionItemsSubGroup.AddChildStatistic(transformCodeBlockStatistic == null ? codeBlockStatistic :
                    transformCodeBlockStatistic(codeBlockStatistic));
            }
        }

        private static void AddNonPartExpressionItemStatistics([NotNull] ITextItemStatisticGroup nonPartExpressionItemStatisticGroup,
                                                               [NotNull] TextItemStatisticsIsFilteredOut<ITextItem> textItemStatisticsIsFilteredOut,
                                                               int? expressionItemsDepth = null)
        {
            ITextItemStatistic TransformStatistic(ITextItemStatistic textItemStatistic)
            {
                if (expressionItemsDepth == null)
                    return textItemStatistic;

                return new NonPartExpressionItemAtDepthDecoratorStatistic(textItemStatistic, expressionItemsDepth.Value);
            }

            ITextItemStatistic CreateExpressionItemTypeStatistic(ExpressionItemType expressionItemType) =>
                TransformStatistic(new ExpressionItemStatistic(expressionItemType));

            #region Complex expression item types  expression item types group

            nonPartExpressionItemStatisticGroup.AddChildStatistic(CreateExpressionItemTypeStatistic(ExpressionItemType.ConstantNumericValue));
            nonPartExpressionItemStatisticGroup.AddChildStatistic(CreateExpressionItemTypeStatistic(ExpressionItemType.ConstantText));
            nonPartExpressionItemStatisticGroup.AddChildStatistic(CreateExpressionItemTypeStatistic(ExpressionItemType.Keyword));
            nonPartExpressionItemStatisticGroup.AddChildStatistic(CreateExpressionItemTypeStatistic(ExpressionItemType.Literal));

            var codeBlockExpressionItemsGroup = new ExclusiveTextItemStatisticGroup(ExpressionItemType.CodeBlock.ToString(), textItemStatisticsIsFilteredOut);

            nonPartExpressionItemStatisticGroup.AddChildStatistic(codeBlockExpressionItemsGroup);

            AddCodeBlockStatisticsToGroup(codeBlockExpressionItemsGroup, true, textItemStatisticsIsFilteredOut, TransformStatistic);
            AddCodeBlockStatisticsToGroup(codeBlockExpressionItemsGroup, false, textItemStatisticsIsFilteredOut, TransformStatistic);

            #region Operator Group
            var operatorExpressionItemsGroup = new ExclusiveTextItemStatisticGroup("Operators", textItemStatisticsIsFilteredOut);
            nonPartExpressionItemStatisticGroup.AddChildStatistic(operatorExpressionItemsGroup);

            operatorExpressionItemsGroup.AddChildStatistic(CreateExpressionItemTypeStatistic(ExpressionItemType.PostfixUnaryOperator));
            operatorExpressionItemsGroup.AddChildStatistic(CreateExpressionItemTypeStatistic(ExpressionItemType.PrefixUnaryOperator));
            operatorExpressionItemsGroup.AddChildStatistic(CreateExpressionItemTypeStatistic(ExpressionItemType.BinaryOperator));
            #endregion

            #region Braces Group
            var bracesExpressionItemsGroup = new ExclusiveTextItemStatisticGroup(ExpressionItemType.Braces.ToString(), textItemStatisticsIsFilteredOut);
            nonPartExpressionItemStatisticGroup.AddChildStatistic(bracesExpressionItemsGroup);

            AddBracesStatisticsToGroup(bracesExpressionItemsGroup, textItemStatisticsIsFilteredOut, TransformStatistic);

            #endregion

            #region CustomExpressionItems
            var customExpressionItemsGroup = new ExclusiveTextItemStatisticGroup(ExpressionItemType.Custom.ToString(), textItemStatisticsIsFilteredOut);
            nonPartExpressionItemStatisticGroup.AddChildStatistic(customExpressionItemsGroup);

            var customExpressionItemStatistic = new ExpressionItemStatistic(ExpressionItemType.Custom);

            customExpressionItemsGroup.AddChildStatistic(
                TransformStatistic(new PrefixRegularPostfixExpressionItemDecoratorStatistic(customExpressionItemStatistic, SpecialExpressionItemType.Prefix)));
            customExpressionItemsGroup.AddChildStatistic(
                TransformStatistic(new PrefixRegularPostfixExpressionItemDecoratorStatistic(customExpressionItemStatistic, SpecialExpressionItemType.Regular)));
            customExpressionItemsGroup.AddChildStatistic(
                TransformStatistic(new PrefixRegularPostfixExpressionItemDecoratorStatistic(customExpressionItemStatistic, SpecialExpressionItemType.Postfix)));

            #endregion
        }

        public static int GetExpressionItemDepth([NotNull] IExpressionItemBase expressionItem)
        {
            var expressionItemDepth = 0;

            var currentParent = expressionItem.Parent;

            while (currentParent != null)
            {
                if (currentParent is IRootExpressionItem)
                    break;

                if (currentParent is IBracesExpressionItem || currentParent is ICodeBlockExpressionItem)
                    ++expressionItemDepth;

                currentParent = currentParent.Parent;
            }

            return expressionItemDepth;
        }

        public static void ValidateTestStatistics([NotNull] ITextItemStatistics textItemStatistics,
                                                  [NotNull] Func<ITextItemStatistic, bool> getStatisticsIsExpectedToBeNonZero)
        {
            var zeroNonZeroValidationFailed = false;
            void DoValidateStatistics(ITextItemStatistic textItemStatisticToValidate)
            {
                if (getStatisticsIsExpectedToBeNonZero(textItemStatisticToValidate))
                {
                    if (textItemStatisticToValidate.Counter == 0)
                    {
                        zeroNonZeroValidationFailed = true;
                        LogHelper.Context.Log.ErrorFormat($"Counter should be positive. Test statistic group: {textItemStatisticToValidate.GetPath()}");
                    }
                }
                else if (textItemStatisticToValidate.Counter > 0)
                {
                    zeroNonZeroValidationFailed = true;
                    LogHelper.Context.Log.ErrorFormat($"Counter should be zero. Test statistic group: {textItemStatisticToValidate.GetPath()}");
                }
            }

            void ValidateTestStatisticsGroup(ITextItemStatisticGroup textItemStatisticGroup)
            {
                int childStatisticsTotalCounter = 0;

                foreach (var childStatistic in textItemStatisticGroup.ChildStatisticsCollection)
                {
                    childStatisticsTotalCounter += childStatistic.Counter;

                    if (childStatistic is ITextItemStatistic textItemStatistic)
                    {
                        if (childStatistic is IPrefixRegularPostfixExpressionItemStatistic prefixRegularPostfixStatistics)
                        {
                            if (prefixRegularPostfixStatistics.SpecialExpressionItemType == SpecialExpressionItemType.Prefix)
                            {
                                if (childStatistic is IDecoratorStatistic decoratorStatistic &&
                                    decoratorStatistic.DecoratedExpressionItemStatistic is IBracesExpressionItemStatistic bracesExpressionItemStatistic &&
                                    bracesExpressionItemStatistic.NumberOfParameters == 0)
                                    continue;
                            }
                        }

                        if (childStatistic is INonPartExpressionItemAtDepthStatistic itemAtDepthStatistic)
                        {
                            if (itemAtDepthStatistic.ExpressionItemDepth <= TestSetup.MaxDepthOfExpression - 1)
                                DoValidateStatistics(itemAtDepthStatistic);
                            else if (itemAtDepthStatistic.ExpressionItemDepth > TestSetup.MaxDepthOfExpression + 3 &&
                                     itemAtDepthStatistic.Counter > 0)
                                LogHelper.Context.Log.WarnFormat("The statistics is expected to be zero for {0}<={1}. Statistics: {2}.",
                                    nameof(itemAtDepthStatistic.ExpressionItemDepth), TestSetup.MaxDepthOfExpression,
                                    itemAtDepthStatistic.GetPath());
                        }
                        else
                            DoValidateStatistics(textItemStatistic);

                    }
                    else if (childStatistic is ITextItemStatisticGroup childStatisticGroup)
                    {
                        // We will validate that the group counter is a sum of counters of its children.
                        ValidateTestStatisticsGroup(childStatisticGroup);
                    }
                    else
                    {
                        Assert.Fail();
                    }
                }


                if (textItemStatisticGroup is ExclusiveTextItemStatisticGroup)
                    Assert.AreEqual(childStatisticsTotalCounter, textItemStatisticGroup.Counter,
                        $"The statistics counter of exclusive statistics group '{typeof(ITextItemStatisticGroup)}' should be the same as the summation of '{nameof(ITextItemStatisticGroup.Counter)}' values of its children. Test statistic group: {textItemStatisticGroup.GetPath()}.");
                else if (textItemStatisticGroup is NonExclusiveTextItemStatisticGroup)
                    Assert.IsTrue(textItemStatisticGroup.Counter >= textItemStatisticGroup.ChildStatisticsCollection.Max(x => x.Counter),
                        $"The statistics counter of non-exclusive statistics group '{typeof(ITextItemStatisticGroup)}' should be greater the counter of any of its children. Test statistic group: {textItemStatisticGroup.GetPath()}.");
                else
                    Assert.Fail($"Invalid statistics group type. Test statistic group: {textItemStatisticGroup.GetPath()}.");
            }

            foreach (var testStatistic in textItemStatistics.TestStatisticsCollection)
            {
                if (!(testStatistic is ITextItemStatisticGroup textItemStatisticGroup))
                {
                    Assert.Fail($"Expected an instance of '{typeof(ITextItemStatisticGroup)}' as child statistics of root statistics. Test statistics path: {testStatistic.GetPath()}");
                    return;
                }

                ValidateTestStatisticsGroup(textItemStatisticGroup);
            }

            Assert.IsFalse(zeroNonZeroValidationFailed, "Test statistics validation failed. Please look at error logs above.");
        }

        [NotNull]
        public static ITextItemStatistics CreateTextItemStatistics([NotNull] string simulationName,
                                                                   [NotNull] TextItemStatisticsIsFilteredOut<ITextItem> textItemStatisticsIsFilteredOut)
        {
            var allTextItemStatisticGroups = new List<ITextItemStatisticGroup>();

            #region Parsed code overview statistics
            var parsedCodeOverviewStatisticGroup = new ExclusiveTextItemStatisticGroup(StatisticGroupNames.ParsedCodeOverview, textItemStatisticsIsFilteredOut);

            allTextItemStatisticGroups.Add(parsedCodeOverviewStatisticGroup);
            parsedCodeOverviewStatisticGroup.AddChildStatistic(new ParsedCodeCounterStatistics());

            #endregion

            #region All expression item types group
            // 
            var textItemStatisticGroup = new ExclusiveTextItemStatisticGroup(StatisticGroupNames.AllExpressionTypes, textItemStatisticsIsFilteredOut);

            allTextItemStatisticGroups.Add(textItemStatisticGroup);

            foreach (var expressionItemTypeObj in Enum.GetValues(typeof(ExpressionItemType)))
            {
                var expressionItemType = (ExpressionItemType)expressionItemTypeObj;

                if (expressionItemType == ExpressionItemType.SeriesOfExpressionItemsWithErrors)
                    continue;

                textItemStatisticGroup.AddChildStatistic(new ExpressionItemStatistic(expressionItemType));
            }

            #endregion

            var nonPartExpressionItemStatisticGroup = new ExclusiveTextItemStatisticGroup(StatisticGroupNames.NonPartExpressionTypes, textItemStatisticsIsFilteredOut);
            allTextItemStatisticGroups.Add(nonPartExpressionItemStatisticGroup);
            AddNonPartExpressionItemStatistics(nonPartExpressionItemStatisticGroup, textItemStatisticsIsFilteredOut);


            #region Main expressions broken by category
            var expressionsByCategoryGroup = new ExclusiveTextItemStatisticGroup("ExpressionsByCategory", textItemStatisticsIsFilteredOut);

            allTextItemStatisticGroups.Add(expressionsByCategoryGroup);
            expressionsByCategoryGroup.AddChildStatistic(new PrefixRegularPostfixExpressionItemStatistic(SpecialExpressionItemType.Prefix));
            expressionsByCategoryGroup.AddChildStatistic(new PrefixRegularPostfixExpressionItemStatistic(SpecialExpressionItemType.Regular));
            expressionsByCategoryGroup.AddChildStatistic(new PrefixRegularPostfixExpressionItemStatistic(SpecialExpressionItemType.Postfix));

            #endregion

            #region  Braces grouped as prefixes vs non prefixes
            var prefixAndNonPrefixBracesGroup = new ExclusiveTextItemStatisticGroup("PrefixAndNonPrefixBraces", textItemStatisticsIsFilteredOut);

            allTextItemStatisticGroups.Add(prefixAndNonPrefixBracesGroup);

            var nonPrefixBracesGroup = new ExclusiveTextItemStatisticGroup("NonPrefixBraces", textItemStatisticsIsFilteredOut);
            prefixAndNonPrefixBracesGroup.AddChildStatistic(nonPrefixBracesGroup);

            AddBracesStatisticsToGroup(nonPrefixBracesGroup, textItemStatisticsIsFilteredOut,
                (expressionItemStatistic) => new PrefixRegularPostfixExpressionItemDecoratorStatistic(expressionItemStatistic, SpecialExpressionItemType.Regular));

            var prefixBracesGroup = new ExclusiveTextItemStatisticGroup("PrefixBraces", textItemStatisticsIsFilteredOut);
            prefixAndNonPrefixBracesGroup.AddChildStatistic(prefixBracesGroup);

            ITextItemStatistic TransformToPrefixBracesStatistic(ITextItemStatistic textItemStatistic) => new PrefixRegularPostfixExpressionItemDecoratorStatistic(textItemStatistic, SpecialExpressionItemType.Prefix);

            AddBracesStatisticsToGroup(prefixBracesGroup, false, true, textItemStatisticsIsFilteredOut, TransformToPrefixBracesStatistic);
            AddBracesStatisticsToGroup(prefixBracesGroup, false, false, textItemStatisticsIsFilteredOut, TransformToPrefixBracesStatistic);

            #endregion

            #region Operator name type statistics
            var operatorNameTypesGroup = new NonExclusiveTextItemStatisticGroup("Operators by Name Type", textItemStatisticsIsFilteredOut);

            allTextItemStatisticGroups.Add(operatorNameTypesGroup);

            foreach (var operatorTypeObj in Enum.GetValues(typeof(OperatorType)))
            {
                var operatorType = (OperatorType)operatorTypeObj;
                foreach (var specialOperatorNameTypeObj in Enum.GetValues(typeof(SpecialOperatorNameType)))
                {
                    var specialOperatorNameType = (SpecialOperatorNameType)specialOperatorNameTypeObj;

                    if (operatorType == OperatorType.BinaryOperator)
                    {
                        if (specialOperatorNameType == SpecialOperatorNameType.NameSimilarToOtherBinaryOperator)
                            continue;
                    }
                    else if (operatorType == OperatorType.PrefixUnaryOperator)
                    {
                        if (specialOperatorNameType == SpecialOperatorNameType.NameSimilarToOtherPrefixUnaryOperator)
                            continue;
                    }
                    else if (operatorType == OperatorType.PostfixUnaryOperator)
                    {
                        if (specialOperatorNameType == SpecialOperatorNameType.NameSimilarToOtherPostfixUnaryOperator)
                            continue;
                    }

                    operatorNameTypesGroup.AddChildStatistic(new OperatorNameTypeStatistics(operatorType, specialOperatorNameType));
                }
            }

            #endregion

            #region Depth statistics
            var depthStatisticsSummaryGroup = new ExclusiveTextItemStatisticGroup(StatisticGroupNames.DepthStatisticsSummary, textItemStatisticsIsFilteredOut);

            allTextItemStatisticGroups.Add(depthStatisticsSummaryGroup);

            var depthStatisticsDetailsGroup = new ExclusiveTextItemStatisticGroup(StatisticGroupNames.DepthStatisticsDetails, textItemStatisticsIsFilteredOut);
            allTextItemStatisticGroups.Add(depthStatisticsDetailsGroup);

            for (int depth = 0; depth <= TestSetup.MaxDepthOfExpression + 4; ++depth)
            {
                var expressionItemsAtDepthStatisticGroup = new ExclusiveTextItemStatisticGroup($"DepthStatisticsAtDepth={depth}", textItemStatisticsIsFilteredOut);
                depthStatisticsDetailsGroup.AddChildStatistic(expressionItemsAtDepthStatisticGroup);

                AddNonPartExpressionItemStatistics(expressionItemsAtDepthStatisticGroup, textItemStatisticsIsFilteredOut, depth);

                depthStatisticsSummaryGroup.AddChildStatistic(new NonPartExpressionItemAtDepthStatistic(depth));
            }
            #endregion

            #endregion
            return new TextItemStatistics(simulationName, allTextItemStatisticGroups);
        }

        //public static void LogExpressionParseErrors([NotNull] IParsedExpressionResult parsedExpressionResult)
        //{
        //    if (parsedExpressionResult.ParseErrorData.ErrorsCount > 0)
        //    {
        //        foreach (var parseErrorData in parsedExpressionResult.ParseErrorData.AllSortedCodeItemErrors)
        //        {
        //            LogHelper.Context.Log.ErrorFormat("Error type: {0}, Error index: {1}. Error message: [{2}]",
        //                parseErrorItem.ParseErrorItemCode, parseErrorData.ErrorIndexInParsedText, parseErrorData.ErrorMessage);
        //            LogHelper.Context.Log.ErrorFormat("Error context:{0}{1}{0}",
        //                Environment.NewLine,
        //                parseErrorData.GetErrorContext(parsedExpressionResult.ParsedCode,
        //                    parsedExpressionResult.IndexInText, parsedExpressionResult.PositionInTextOnCompletion));
        //        }

        //        Assert.Fail("Evaluated expression had parse errors.");
        //    }
        //}
    }
}