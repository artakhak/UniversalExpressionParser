using JetBrains.Annotations;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestsSharedLibrary.TestSimulation;
using TestsSharedLibraryForCodeParsers.CodeGeneration;
using UniversalExpressionParser.ExpressionItems;
using UniversalExpressionParser.ExpressionItems.Custom;
using UniversalExpressionParser.Tests.OperatorTemplates;
using UniversalExpressionParser.DemoExpressionLanguageProviders;
using UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions;

namespace UniversalExpressionParser.Tests
{
    public class CodeGenerator
    {
        //private readonly HashSet<string> _wordsThatCannotBeUsedAsLiterals;
        private readonly List<ILanguageKeywordInfo> _randomKeywordsList = new List<ILanguageKeywordInfo>();

        private readonly List<ILanguageKeywordInfo> _prefixCustomExpressionPickerKeywordsList = new List<ILanguageKeywordInfo>();
        private readonly List<ILanguageKeywordInfo> _regularCustomExpressionPickerKeywordsList = new List<ILanguageKeywordInfo>();
        private readonly List<ILanguageKeywordInfo> _postfixCustomExpressionPickerKeywordsList = new List<ILanguageKeywordInfo>();

        private readonly Dictionary<OperatorType, OperatorInfosPerPriority> _operatorTypeToOperatorInfosMap = new Dictionary<OperatorType, OperatorInfosPerPriority>();

        [NotNull]
        private readonly ISimulationRandomNumberGenerator _simulationRandomNumberGenerator;

        [NotNull]
        private readonly StringBuilder _generatedCode;

        private readonly Stack<IComplexExpressionItem> _containerExpressionsStack = new Stack<IComplexExpressionItem>();

        [NotNull]
        private readonly IExpressionLanguageProviderWrapper _expressionLanguageProviderWrapper;

        private int _numberOfGeneratedExpressionItems;

        private readonly List<ICommentedTextData> _commentedTextData = new List<ICommentedTextData>();

        public CodeGenerator([NotNull] IExpressionLanguageProviderWrapper expressionLanguageProviderWrapper, [NotNull] StringBuilder generatedCode)
        {

            _expressionLanguageProviderWrapper = expressionLanguageProviderWrapper;
            _generatedCode = generatedCode;
            _simulationRandomNumberGenerator = TestSetup.SimulationRandomNumberGenerator;

            //_wordsThatCannotBeUsedAsLiterals = new HashSet<string>(_expressionLanguageProvider.IsLanguageCaseSensitive ?
            //    StringComparer.Ordinal : StringComparer.OrdinalIgnoreCase);

            //if (_expressionLanguageProvider.LineCommentMarker != null)
            //    _wordsThatCannotBeUsedAsLiterals.Add(_expressionLanguageProvider.LineCommentMarker);

            //if (_expressionLanguageProvider.MultilineCommentStartMarker != null)
            //{
            //    _wordsThatCannotBeUsedAsLiterals.Add(_expressionLanguageProvider.MultilineCommentStartMarker);
            //    _wordsThatCannotBeUsedAsLiterals.Add(_expressionLanguageProvider.MultilineCommentEndMarker);
            //}

            //if (_expressionLanguageProvider.ExpressionSeparatorCharacter != '\0')
            //{
            //    _wordsThatCannotBeUsedAsLiterals.Add(_expressionLanguageProvider.ExpressionSeparatorCharacter.ToString());

            //    if (_expressionLanguageProvider.CodeBlockStartMarker != null)
            //    {
            //        _wordsThatCannotBeUsedAsLiterals.Add(_expressionLanguageProvider.CodeBlockStartMarker);
            //        _wordsThatCannotBeUsedAsLiterals.Add(_expressionLanguageProvider.CodeBlockEndMarker);
            //    }
            //}

            foreach (var keywordInfo in _expressionLanguageProviderWrapper.ExpressionLanguageProvider.Keywords)
            {
                //_wordsThatCannotBeUsedAsLiterals.Add(keywordInfo.Keyword);

                switch (keywordInfo.Id)
                {
                    case KeywordIds.GenericTypes:
                    case KeywordIds.Performance:
                        _prefixCustomExpressionPickerKeywordsList.Add(keywordInfo);
                        break;

                    case KeywordIds.Metadata:
                        if (_expressionLanguageProviderWrapper.ExpressionLanguageProvider.CodeBlockStartMarker != null)
                            _prefixCustomExpressionPickerKeywordsList.Add(keywordInfo);
                        break;

                    case KeywordIds.Pragma:
                        _regularCustomExpressionPickerKeywordsList.Add(keywordInfo);
                        break;

                    case KeywordIds.Where:
                        _postfixCustomExpressionPickerKeywordsList.Add(keywordInfo);
                        break;

                    default:
                        _randomKeywordsList.Add(keywordInfo);
                        break;
                }
            }

            _operatorTypeToOperatorInfosMap[OperatorType.PrefixUnaryOperator] = new OperatorInfosPerPriority();
            _operatorTypeToOperatorInfosMap[OperatorType.BinaryOperator] = new OperatorInfosPerPriority();
            _operatorTypeToOperatorInfosMap[OperatorType.PostfixUnaryOperator] = new OperatorInfosPerPriority();

            foreach (var operatorInfo in _expressionLanguageProviderWrapper.ExpressionLanguageProvider.Operators)
            {
                var operatorInfosPerPriority = _operatorTypeToOperatorInfosMap[operatorInfo.OperatorType];

                var operatorPriority = OperatorPriorities.GetPriority(operatorInfo.Priority);

                if (!operatorInfosPerPriority.OperatorPriorityOperatorInfosMap.TryGetValue(operatorPriority, out var operatorInfosWithPriority))
                {
                    operatorInfosWithPriority = new List<IOperatorInfo>();
                    operatorInfosPerPriority.OperatorPriorityOperatorInfosMap[operatorPriority] = operatorInfosWithPriority;
                }

                operatorInfosWithPriority.Add(operatorInfo);

                //foreach (var operatorPart in operatorInfo.NameParts)
                //{
                //    if (!_wordsThatCannotBeUsedAsLiterals.Contains(operatorPart))
                //        _wordsThatCannotBeUsedAsLiterals.Add(operatorPart);
                //}
            }
        }

        private void AddCommentedTextDataToParseExpressionResultOnParsingComplete([NotNull] IParseExpressionResult parseExpressionResult)
        {
            foreach (var commentedTextData in _commentedTextData)
                parseExpressionResult.AddCommentedTextData(commentedTextData);
        }

        /// <summary>
        /// Generates root expression with only operators. Example "x+y*z++". 
        /// </summary>
        /// <returns></returns>
        [NotNull]
        public ParseExpressionResult GenerateSimpleParseExpressionResultWithOperatorsOnly([NotNull] OperatorTemplateBase operatorTemplate)
        {
            return GenerateParseExpressionResult(rootExpressionItem =>
            {
                rootExpressionItem.AddChildExpressionItem(GenerateOperatorsExpressionItem(operatorTemplate, true));
            });
        }

        [NotNull]
        public ParseExpressionResult GenerateParseExpressionResult()
        {
            return GenerateParseExpressionResult(AddChildExpressions);
        }

        [NotNull]
        private ParseExpressionResult GenerateParseExpressionResult(Action<RootExpressionItem> addChildExpressionsToRootExpressionItem)
        {
            AddWhitespacesAndComments(false);

            var rootExpressionItem = new RootExpressionItem();
            _containerExpressionsStack.Push(rootExpressionItem);

            addChildExpressionsToRootExpressionItem(rootExpressionItem);
            
            _containerExpressionsStack.Pop();

            AddWhitespacesAndComments(false);

            if (TestSetup.CurrentCommentMarkersData != null && TestSetup.SimulationRandomNumberGenerator.Next(100) <= 30)
            {
                TestSetup.CodeGenerationHelper.GenerateComment(this._generatedCode,
                    commentedTextData => this._commentedTextData.Add(commentedTextData),
                    TestSetup.SimulateNiceCode, true);
            }

            var parseExpressionResult = new ParseExpressionResult(rootExpressionItem, new ParseErrorData());
            AddCommentedTextDataToParseExpressionResultOnParsingComplete(parseExpressionResult);

            parseExpressionResult.InitializeForTest(_generatedCode.ToString(),
                parseExpressionResult.GetIndexInText(), parseExpressionResult.GetItemLength(),
                _generatedCode.Length);

            return parseExpressionResult;
        }

        [NotNull]
        public ParseExpressionResult GenerateParseBracesExpressionResult()
        {
            bool isSquareBraces = TestSetup.SimulationRandomNumberGenerator.Next(100) <= 50;

            var bracesExpressionItem = new BracesExpressionItem(
                new List<IExpressionItemBase>(0),
                new List<IKeywordExpressionItem>(0), null,
                isSquareBraces, _generatedCode.Length);

            var rootExpressionItem = new RootExpressionItem();
            _containerExpressionsStack.Push(rootExpressionItem);

            rootExpressionItem.AddChildExpressionItem(bracesExpressionItem);

            _generatedCode.Append(bracesExpressionItem.OpeningBrace.Text);

            _containerExpressionsStack.Push(bracesExpressionItem);

            PopulateBracesExpressionItem(bracesExpressionItem, false);

            _containerExpressionsStack.Pop();
            _containerExpressionsStack.Pop();

            var parseBracesExpressionResult = new ParseExpressionResult(rootExpressionItem, new ParseErrorData());
            AddCommentedTextDataToParseExpressionResultOnParsingComplete(parseBracesExpressionResult);

            parseBracesExpressionResult.InitializeForTest(_generatedCode.ToString(),
                bracesExpressionItem.OpeningBrace.IndexInText, _generatedCode.Length - bracesExpressionItem.OpeningBrace.IndexInText,
                _generatedCode.Length);

            return parseBracesExpressionResult;
        }

        [NotNull]
        public ParseExpressionResult GenerateParseCodeBlockExpressionResult()
        {
            if (_expressionLanguageProviderWrapper.ExpressionLanguageProvider.CodeBlockStartMarker == null || 
                _expressionLanguageProviderWrapper.ExpressionLanguageProvider.CodeBlockEndMarker == null)
                throw new NotSupportedException();

            var codeBlockStartMarker = new CodeBlockStartMarkerExpressionItem(
                TestSetup.CodeGenerationHelper.ApplyRandomCapitalization(
                    _expressionLanguageProviderWrapper.ExpressionLanguageProvider.CodeBlockStartMarker),
                _generatedCode.Length);

            _generatedCode.Append(codeBlockStartMarker.Text);

            var codeBlockExpressionItem = new CodeBlockExpressionItem(
                Array.Empty<IExpressionItemBase>(), Array.Empty<IKeywordExpressionItem>(),
                codeBlockStartMarker);

            var rootExpressionItem = new RootExpressionItem();
            _containerExpressionsStack.Push(rootExpressionItem);

            rootExpressionItem.AddChildExpressionItem(codeBlockExpressionItem);

            _containerExpressionsStack.Push(codeBlockExpressionItem);
            AddChildExpressions(codeBlockExpressionItem);
            _containerExpressionsStack.Pop();
            _containerExpressionsStack.Pop();

            AddWhitespacesAndCommentsIfNecessary(_expressionLanguageProviderWrapper.ExpressionLanguageProvider.CodeBlockEndMarker);
            var codeBlockEndMarker = new CodeBlockEndMarkerExpressionItem(TestSetup.CodeGenerationHelper.ApplyRandomCapitalization(
                _expressionLanguageProviderWrapper.ExpressionLanguageProvider.CodeBlockEndMarker), _generatedCode.Length);
            _generatedCode.Append(codeBlockEndMarker.Text);

            codeBlockExpressionItem.CodeBlockEndMarker = codeBlockEndMarker;

            var parseCodeBlockExpressionResult = new ParseExpressionResult(rootExpressionItem, new ParseErrorData());
            AddCommentedTextDataToParseExpressionResultOnParsingComplete(parseCodeBlockExpressionResult);

            parseCodeBlockExpressionResult.InitializeForTest(_generatedCode.ToString(), codeBlockEndMarker.IndexInText,
                _generatedCode.Length - codeBlockEndMarker.IndexInText,
                _generatedCode.Length);

            return parseCodeBlockExpressionResult;
        }

        private IOperatorInfo GetRandomOperatorInfo(OperatorType operatorType, OperatorPriority operatorPriority)
        {
            var operatorInfosPerPriority = _operatorTypeToOperatorInfosMap[operatorType];
            var operatorInfos = operatorInfosPerPriority.OperatorPriorityOperatorInfosMap[operatorPriority];

            var bestOperatorInfo = operatorInfos[_simulationRandomNumberGenerator.Next(operatorInfos.Count - 1)];

            // Lets get the operator that has the same operator type and priority as operatorInfo, but which possibly has
            // more parts or the last part is longer

            var stringComparison = _expressionLanguageProviderWrapper.ExpressionLanguageProvider.IsLanguageCaseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;
            foreach (var operatorInfo in operatorInfos)
            {
                //if (operatorInfo == bestOperatorInfo || operatorInfo.OperatorType == operatorType ||
                //    operatorInfo.Priority != bestOperatorInfo.Priority ||
                //    operatorInfo.NameParts.Count < bestOperatorInfo.NameParts.Count)
                //    continue;

                if (operatorInfo == bestOperatorInfo || operatorInfo.NameParts.Count < bestOperatorInfo.NameParts.Count)
                    continue;


                bool skipCurrentOperatorInfo = false;

                for (var namePartIndex = 0; namePartIndex < bestOperatorInfo.NameParts.Count - 1; ++namePartIndex)
                {
                    if (!string.Equals(bestOperatorInfo.NameParts[namePartIndex], operatorInfo.NameParts[namePartIndex],
                        stringComparison))
                    {
                        skipCurrentOperatorInfo = true;
                        break;
                    }
                }

                if (skipCurrentOperatorInfo)
                    continue;

                if (operatorInfo.NameParts.Count == bestOperatorInfo.NameParts.Count)
                {
                    var bestOperatorInfoLastNamePart = bestOperatorInfo.NameParts[bestOperatorInfo.NameParts.Count - 1];
                    var operatorInfoLastNamePart = operatorInfo.NameParts[operatorInfo.NameParts.Count - 1];

                    if (operatorInfoLastNamePart.Length <= bestOperatorInfoLastNamePart.Length ||
                        !operatorInfoLastNamePart.StartsWith(bestOperatorInfoLastNamePart, stringComparison))
                        continue;
                }

                // If we got here, the new operator has more parts
                bestOperatorInfo = operatorInfo;
            }

            return bestOperatorInfo;
        }

        //private string TransformTextForCaseInsensitivityTest(string text)
        //{
        //    if (_expressionLanguageProvider.IsLanguageCaseSensitive)
        //        return text;

        //    bool hasLowerCaseChars = text.Any(x =>
        //    {
        //        var textToString = x.ToString();
        //        return textToString != textToString.ToUpper();
        //    });

        //    if (hasLowerCaseChars)
        //        return text.ToUpper();

        //    return text.ToLower();
        //}

        private KeywordExpressionItem AddKeywordExpressionItem([NotNull, ItemNotNull] List<IKeywordExpressionItem> keywordExpressionItems,
                                                               [NotNull] ILanguageKeywordInfo keywordInfo)
        {
            AddWhitespacesAndCommentsIfNecessary(keywordInfo.Keyword);

            var keywordExpressionItem = new KeywordExpressionItem(keywordInfo, TestSetup.CodeGenerationHelper.ApplyRandomCapitalization(keywordInfo.Keyword), _generatedCode.Length);
            _generatedCode.Append(keywordExpressionItem.Text);

            // ReSharper disable once PossibleNullReferenceException
            keywordExpressionItems.Add(keywordExpressionItem);

            return keywordExpressionItem;
        }


        [NotNull, ItemNotNull]
        private List<IKeywordExpressionItem> AddKeywordExpressionItems()
        {
            var keywordExpressionItems = new List<IKeywordExpressionItem>();

            if (_simulationRandomNumberGenerator.Next(100) >= 10)
                return keywordExpressionItems;

            var numberOfKeywords = _simulationRandomNumberGenerator.Next(0, TestSetup.MaxNumberOfKeywords);

            if (numberOfKeywords == 0)
                return keywordExpressionItems;

            var candidateKeywords = _randomKeywordsList;

            while (true)
            {
                var keywordInfo = candidateKeywords[_simulationRandomNumberGenerator.Next(0, candidateKeywords.Count - 1)];

                AddKeywordExpressionItem(keywordExpressionItems, keywordInfo);

                if (keywordExpressionItems.Count == numberOfKeywords)
                    break;

                candidateKeywords = candidateKeywords.Where(x => x.Id != keywordInfo.Id).ToList();
            }

            return keywordExpressionItems;
        }

        private int GetDepth(bool countTopItemInStack = true)
        {
            if (_containerExpressionsStack.Count == 0)
                return 0;

            var currentDepth = _containerExpressionsStack.Count;

            if (currentDepth == 0)
                return currentDepth;

            // Lets not count the root expression item
            --currentDepth;

            if (currentDepth > 0 && !countTopItemInStack)
                --currentDepth;

            return currentDepth;
        }

        private class ExpressionItemGenerationOptions
        {
            public bool CanAddPrefixes { get; set; } = true;
            public bool CanAddKeywords { get; set; } = true;
            public bool CanAddCodeBlockPostfix { get; set; } = true;
            //int? ExpectedChildItemsCount { get; set; } = null;
        }

        private ComplexExpressionItemBase GenerateExpressionItem(ExpressionItemTypeFlags expressionItemTypeFlags,
                                                                 [CanBeNull] ExpressionItemGenerationOptions expressionItemGenerationOptions = null)

        {
            if (expressionItemGenerationOptions == null)
                expressionItemGenerationOptions = new ExpressionItemGenerationOptions();

            ++_numberOfGeneratedExpressionItems;

            var currentDepth = GetDepth();

            if (currentDepth >= TestSetup.MaxDepthOfExpression || _numberOfGeneratedExpressionItems >= TestSetup.MaxNumberOfExpressionItems)
                expressionItemGenerationOptions.CanAddPrefixes = false;

            var prefixExpressionItems = new List<IExpressionItemBase>(0);

            List<ExpressionItemTypeFlags> possibleExpressionItemTypes = new List<ExpressionItemTypeFlags>();
            Dictionary<ExpressionItemTypeFlags, int> expressionItemTypeToIndex = new Dictionary<ExpressionItemTypeFlags, int>();

            ExpressionItemTypeFlags expressionItemType;
            foreach (var candidateExpressionItemType in Enum.GetValues(typeof(ExpressionItemTypeFlags)))
            {
                expressionItemType = (ExpressionItemTypeFlags)candidateExpressionItemType;

                if (expressionItemType == ExpressionItemTypeFlags.All)
                    continue;

                if ((expressionItemType & expressionItemTypeFlags) > 0)
                {
                    expressionItemTypeToIndex[expressionItemType] = expressionItemTypeToIndex.Count;
                    possibleExpressionItemTypes.Add(expressionItemType);
                }
            }

            if (expressionItemTypeToIndex.Count == 1)
            {
                expressionItemType = possibleExpressionItemTypes[0];
            }
            else
            {
                var probabilityGenerator = CreateProbabilityGenerator(1000);

                HashSet<ExpressionItemTypeFlags> accountedForExpressionItemsTypes = new HashSet<ExpressionItemTypeFlags>();

                void TryAddExpressionItemIndexToList(List<int> expressionItemIndexesParam,
                                                     ExpressionItemTypeFlags expressionItemTypeParam)
                {
                    if (expressionItemTypeToIndex.TryGetValue(expressionItemTypeParam, out var expressionItemIndex))
                    {
                        accountedForExpressionItemsTypes.Add(expressionItemTypeParam);
                        expressionItemIndexesParam.Add(expressionItemIndex);
                    }
                }

                int probabilityOfBracesAndCodeBlocks;
                int probabilityOfOperators;
                int probabilityOfCustomExpressionItems;

                if (currentDepth == 0)
                {
                    probabilityOfBracesAndCodeBlocks = 400;
                    probabilityOfOperators = 200;
                    probabilityOfCustomExpressionItems = 50;
                }
                else if (currentDepth == 1)
                {
                    probabilityOfBracesAndCodeBlocks = 200;
                    probabilityOfOperators = 150;
                    probabilityOfCustomExpressionItems = 50;
                }
                else if (currentDepth == 2)
                {
                    probabilityOfBracesAndCodeBlocks = 50;
                    probabilityOfOperators = 50;
                    probabilityOfCustomExpressionItems = 20;
                }
                else if (currentDepth >= TestSetup.MaxDepthOfExpression)
                {
                    probabilityOfBracesAndCodeBlocks = 0;
                    probabilityOfOperators = 0;
                    probabilityOfCustomExpressionItems = 0;
                }
                else
                {
                    probabilityOfBracesAndCodeBlocks = 20;
                    probabilityOfOperators = 20;
                    probabilityOfCustomExpressionItems = 20;
                }

                var expressionItemIndexes = new List<int>();

                // Braces and code blocks
                TryAddExpressionItemIndexToList(expressionItemIndexes, ExpressionItemTypeFlags.Braces);
                TryAddExpressionItemIndexToList(expressionItemIndexes, ExpressionItemTypeFlags.NamedBraces);
                TryAddExpressionItemIndexToList(expressionItemIndexes, ExpressionItemTypeFlags.CodeBlock);

                if (expressionItemIndexes.Count > 0)
                    probabilityGenerator.AddRandomNumbersForProbability(probabilityOfBracesAndCodeBlocks, expressionItemIndexes);

                // Operators
                expressionItemIndexes = new List<int>();
                TryAddExpressionItemIndexToList(expressionItemIndexes, ExpressionItemTypeFlags.Operators);

                if (expressionItemIndexes.Count > 0)
                    probabilityGenerator.AddRandomNumbersForProbability(probabilityOfOperators, expressionItemIndexes);

                // Custom expression items
                expressionItemIndexes = new List<int>();
                TryAddExpressionItemIndexToList(expressionItemIndexes, ExpressionItemTypeFlags.RegularCustomExpressionItem);

                if (expressionItemIndexes.Count > 0)
                    probabilityGenerator.AddRandomNumbersForProbability(probabilityOfCustomExpressionItems, expressionItemIndexes);

                expressionItemIndexes = expressionItemTypeToIndex.Where(x => !accountedForExpressionItemsTypes.Contains(x.Key)).Select(x => x.Value).ToList(); ;

                if (expressionItemIndexes.Count > 0)
                    probabilityGenerator.AddRandomNumbers(expressionItemIndexes);

                var selectedExpressionItemIndex = probabilityGenerator.GetRandomNumber();
                expressionItemType = possibleExpressionItemTypes[selectedExpressionItemIndex];
            }

            if (expressionItemGenerationOptions.CanAddPrefixes)
            {
                switch (expressionItemType)
                {
                    case ExpressionItemTypeFlags.Operators:
                        break;

                    default:

                        if (_simulationRandomNumberGenerator.Next(100) <= 5)
                        {

                            var numberOfPrefixes = expressionItemType == ExpressionItemTypeFlags.CodeBlock ?
                                2 : _simulationRandomNumberGenerator.Next(0, TestSetup.MaxNumberOfPrefixes);

                            for (var i = 0; i < numberOfPrefixes; ++i)
                            {
                                IComplexExpressionItem currentPrefixExpressionItem;

                                var prefixKeywordExpressionItems = AddKeywordExpressionItems();

                                //Code blocks can have only custom expression items as prefixes, for which CustomExpressionItemCategory is Prefix.
                                if (expressionItemType != ExpressionItemTypeFlags.CodeBlock && _simulationRandomNumberGenerator.Next(100) <= 50)
                                {
                                    currentPrefixExpressionItem = GenerateBracesExpressionItem(new List<IExpressionItemBase>(0),
                                        prefixKeywordExpressionItems, null, true);
                                }
                                else
                                {
                                    var keywordInfo = _prefixCustomExpressionPickerKeywordsList[_simulationRandomNumberGenerator.Next(_prefixCustomExpressionPickerKeywordsList.Count - 1)];

                                    AddKeywordExpressionItem(prefixKeywordExpressionItems, keywordInfo);

                                    currentPrefixExpressionItem = GenerateCustomExpressionItem(new List<IExpressionItemBase>(0),
                                        prefixKeywordExpressionItems, CustomExpressionItemCategory.Prefix);
                                }

                                prefixExpressionItems.Add(currentPrefixExpressionItem);
                            }
                        }

                        break;
                }
            }

            var keywordExpressionItems = new List<IKeywordExpressionItem>(0);

            if (expressionItemGenerationOptions.CanAddKeywords)
            {
                switch (expressionItemType)
                {
                    case ExpressionItemTypeFlags.Operators:
                        break;

                    default:
                        keywordExpressionItems = AddKeywordExpressionItems();
                        break;
                }
            }

            ComplexExpressionItemBase generatedExpressionItem;
           
            switch (expressionItemType)
            {
                case ExpressionItemTypeFlags.ConstantText:
                    generatedExpressionItem = GenerateConstantText(prefixExpressionItems, keywordExpressionItems);
                    break;

                case ExpressionItemTypeFlags.ConstantNumericValue:
                    generatedExpressionItem = GenerateConstantNumber(prefixExpressionItems, keywordExpressionItems);
                    break;

                case ExpressionItemTypeFlags.Literal:
                    generatedExpressionItem = GenerateLiteralExpressionItem(prefixExpressionItems, keywordExpressionItems);
                    break;

                case ExpressionItemTypeFlags.RegularCustomExpressionItem:
                    {
                        var keywordInfo = _regularCustomExpressionPickerKeywordsList[
                            _simulationRandomNumberGenerator.Next(_regularCustomExpressionPickerKeywordsList.Count - 1)];

                        AddKeywordExpressionItem(keywordExpressionItems, keywordInfo);
                        generatedExpressionItem = GenerateCustomExpressionItem(prefixExpressionItems, keywordExpressionItems,
                            CustomExpressionItemCategory.Regular);
                        break;
                    }

                case ExpressionItemTypeFlags.NamedBraces:
                    var literalExpressionItem = GenerateLiteralExpressionItem(new List<IExpressionItemBase>(0), new List<IKeywordExpressionItem>(0));

                    generatedExpressionItem = GenerateBracesExpressionItem(prefixExpressionItems, keywordExpressionItems, literalExpressionItem, false);
                    break;

                case ExpressionItemTypeFlags.Braces:
                    generatedExpressionItem = GenerateBracesExpressionItem(prefixExpressionItems, keywordExpressionItems, null, false);
                    break;
                case ExpressionItemTypeFlags.CodeBlock:
                    generatedExpressionItem = GenerateCodeBlockExpressionItem(prefixExpressionItems, keywordExpressionItems);
                    break;

                case ExpressionItemTypeFlags.Operators:
                    generatedExpressionItem = GenerateOperatorsExpressionItem();
                    break;

                default:
                    throw new ArgumentException($"Unsupported value '{typeof(ExpressionItemTypeFlags).FullName}.{expressionItemType}'",
                        nameof(expressionItemType));
            }

            if (currentDepth < TestSetup.MaxDepthOfExpression && _numberOfGeneratedExpressionItems < TestSetup.MaxNumberOfExpressionItems)
            {
                var canApplyPostfix =
                    generatedExpressionItem is IBracesExpressionItem ||
                    generatedExpressionItem is ILiteralExpressionItem || 
                    //generatedExpressionItem is INumericExpressionItem ||
                    //generatedExpressionItem is IConstantTextExpressionItem ||
                    generatedExpressionItem is ICustomExpressionItem {CustomExpressionItemCategory: CustomExpressionItemCategory.Regular};
                

                if (canApplyPostfix && _simulationRandomNumberGenerator.Next(100) <= 10)
                {
                    IComplexExpressionItem postfixExpressionItem;
                    if (_expressionLanguageProviderWrapper.ExpressionLanguageProvider.CodeBlockStartMarker != null &&
                        expressionItemGenerationOptions.CanAddCodeBlockPostfix &&
                        _simulationRandomNumberGenerator.Next(100) <= 70)
                    {
                        postfixExpressionItem = GenerateExpressionItem(ExpressionItemTypeFlags.CodeBlock,
                            new ExpressionItemGenerationOptions
                            {
                                CanAddPrefixes = false
                            });
                    }
                    else
                    {
                        var postfixKeywordExpressionItems = AddKeywordExpressionItems();

                        var keywordInfo = _postfixCustomExpressionPickerKeywordsList[
                            _simulationRandomNumberGenerator.Next(_postfixCustomExpressionPickerKeywordsList.Count - 1)];

                        AddKeywordExpressionItem(postfixKeywordExpressionItems, keywordInfo);

                        postfixExpressionItem = GenerateCustomExpressionItem(
                            new List<IExpressionItemBase>(0), postfixKeywordExpressionItems, CustomExpressionItemCategory.Postfix);
                    }

                    generatedExpressionItem.AddPostfix(postfixExpressionItem);
                }
            }

            return generatedExpressionItem;
        }

        private ExpressionItemTypeFlags GetPossibleExpressionItemTypeFlags()
        {
            var possibleExpressionItemTypes = ExpressionItemTypeFlags.All;

            if (_expressionLanguageProviderWrapper.ExpressionLanguageProvider.ConstantTextStartEndMarkerCharacters.Count == 0)
                possibleExpressionItemTypes &= (~ExpressionItemTypeFlags.ConstantText);

            if (_expressionLanguageProviderWrapper.ExpressionLanguageProvider.CodeBlockStartMarker == null || 
                _expressionLanguageProviderWrapper.ExpressionLanguageProvider.CodeBlockEndMarker == null)
                possibleExpressionItemTypes &= (~ExpressionItemTypeFlags.CodeBlock);

            if (GetDepth() + 1 >= TestSetup.MaxDepthOfExpression || _numberOfGeneratedExpressionItems >= TestSetup.MaxNumberOfExpressionItems)
                ExcludeExpressionItemsWithChildren(ref possibleExpressionItemTypes);

            return possibleExpressionItemTypes;
        }

        private void ExcludeExpressionItemsWithChildren(ref ExpressionItemTypeFlags expressionItemTypeFlags)
        {
            expressionItemTypeFlags &= (~ExpressionItemTypeFlags.Braces) & (~ExpressionItemTypeFlags.NamedBraces) &
                                       (~ExpressionItemTypeFlags.CodeBlock) & (~ExpressionItemTypeFlags.RegularCustomExpressionItem) &
                                       (~ExpressionItemTypeFlags.Operators);
        }

        [NotNull]
        private OperatorExpressionItem GenerateOperatorsExpressionItem()
        {
            var operatorTemplateIndex = TestSetup.OperatorTemplateIndex != null ? TestSetup.OperatorTemplateIndex.Value :
                _simulationRandomNumberGenerator.Next(OperatorTemplatesCollection.OperatorTemplates.Count - 1);

            return GenerateOperatorsExpressionItem(OperatorTemplatesCollection.OperatorTemplates[operatorTemplateIndex], false);
        }

        [NotNull]
        private OperatorExpressionItem GenerateOperatorsExpressionItem([NotNull] OperatorTemplateBase operatorTemplate, bool operandsAreLiterals)
        {
            var operatorTemplateWrapper = new OperatorTemplateWrapper(this, null, operatorTemplate);

            OperatorTemplateWrapper rightMostOperatorTemplateWrapper = operatorTemplateWrapper;

            while (rightMostOperatorTemplateWrapper.RightOperatorTemplateWrapper != null)
                rightMostOperatorTemplateWrapper = rightMostOperatorTemplateWrapper.RightOperatorTemplateWrapper;

            return operatorTemplateWrapper.GenerateOperatorExpressionItem(rightMostOperatorTemplateWrapper, operandsAreLiterals);
        }

        private class OperatorTemplateWrapper
        {
            private OperatorExpressionItem _operatorExpressionItem;

            [NotNull]
            private readonly CodeGenerator _codeGenerator;

            public OperatorTemplateWrapper([NotNull] CodeGenerator codeGenerator,
                                           [CanBeNull] OperatorTemplateWrapper parentTemplateWrapper,
                                           [NotNull] OperatorTemplateBase operatorTemplate)
            {
                _codeGenerator = codeGenerator;
                ParentTemplateWrapper = parentTemplateWrapper;
                OperatorTemplate = operatorTemplate;
            }

            [CanBeNull]
            public OperatorTemplateWrapper ParentTemplateWrapper { get; }

            [NotNull]
            public OperatorTemplateBase OperatorTemplate { get; }

            [CanBeNull]
            public OperatorTemplateWrapper LeftOperatorTemplateWrapper { get; private set; }

            [CanBeNull]
            public OperatorTemplateWrapper RightOperatorTemplateWrapper { get; private set; }

            public OperatorExpressionItem GenerateOperatorExpressionItem([NotNull] OperatorTemplateWrapper rightMostOperatorTemplateWrapper,
                bool operandsAreLiterals = false)
            {
                if (_operatorExpressionItem != null)
                    return _operatorExpressionItem;

                LiteralExpressionItem GenerateSimpleLiteralExpressionItem()
                {
                    return _codeGenerator.GenerateLiteralExpressionItem(new List<IExpressionItemBase>(0), new List<IKeywordExpressionItem>(0));
                }

                var canHaveLeftOperand = false;
                var canHaveRightOperand = false;

                if (OperatorTemplate is BinaryOperatorTemplate binaryOperatorTemplate)
                {
                    canHaveLeftOperand = true;
                    canHaveRightOperand = true;

                    if (binaryOperatorTemplate.LeftOperatorTemplate != null)
                        LeftOperatorTemplateWrapper =
                            new OperatorTemplateWrapper(_codeGenerator, this, binaryOperatorTemplate.LeftOperatorTemplate);

                    if (binaryOperatorTemplate.RightOperatorTemplate != null)
                        RightOperatorTemplateWrapper =
                            new OperatorTemplateWrapper(_codeGenerator, this, binaryOperatorTemplate.RightOperatorTemplate);
                }
                else
                {
                    var unaryOperatorTemplate = (UnaryOperatorTemplateBase)OperatorTemplate;

                    if (unaryOperatorTemplate.OperatorType == OperatorType.PrefixUnaryOperator)
                    {
                        canHaveRightOperand = true;

                        if (unaryOperatorTemplate.ChildOperatorTemplate != null)
                            RightOperatorTemplateWrapper =
                                new OperatorTemplateWrapper(_codeGenerator, this, unaryOperatorTemplate.ChildOperatorTemplate);
                    }
                    else
                    {
                        canHaveLeftOperand = true;

                        if (unaryOperatorTemplate.ChildOperatorTemplate != null)
                            LeftOperatorTemplateWrapper =
                                new OperatorTemplateWrapper(_codeGenerator, this, unaryOperatorTemplate.ChildOperatorTemplate);
                    }
                }

                ComplexExpressionItemBase leftOperandExpressionItem = null;
                ComplexExpressionItemBase rightOperandExpressionItem = null;

                var candidateNonOperatorExpressionItemType = _codeGenerator.GetPossibleExpressionItemTypeFlags() & (~ExpressionItemTypeFlags.Operators);

                if (LeftOperatorTemplateWrapper != null)
                {
                    leftOperandExpressionItem = LeftOperatorTemplateWrapper.GenerateOperatorExpressionItem(rightMostOperatorTemplateWrapper, operandsAreLiterals);
                }
                else if (canHaveLeftOperand)
                {
                    var nonOperatorLeftOperandType = candidateNonOperatorExpressionItemType & (~ExpressionItemTypeFlags.CodeBlock);

                    if (!operandsAreLiterals)
                    {
                        leftOperandExpressionItem = _codeGenerator.GenerateExpressionItem(nonOperatorLeftOperandType, new ExpressionItemGenerationOptions
                        {
                            CanAddCodeBlockPostfix = false
                        });
                    }
                    else
                    {
                        leftOperandExpressionItem = GenerateSimpleLiteralExpressionItem();
                    }
                }

                var operatorInfo = _codeGenerator.GetRandomOperatorInfo(OperatorTemplate.OperatorType, OperatorTemplate.OperatorPriority);

                var operatorNamePartExpressionItems = new List<IOperatorNamePartExpressionItem>();

                for (int i = 0; i < operatorInfo.NameParts.Count; ++i)
                {
                    var namePart = TestSetup.CodeGenerationHelper.ApplyRandomCapitalization(operatorInfo.NameParts[i]);

                    _codeGenerator.AddWhitespacesAndComments(true);
                    operatorNamePartExpressionItems.Add(new OperatorNamePartExpressionItem(namePart, _codeGenerator._generatedCode.Length));

                    _codeGenerator._generatedCode.Append(namePart);
                }

                _operatorExpressionItem = new OperatorExpressionItem(new OperatorInfoExpressionItem(operatorInfo, operatorNamePartExpressionItems));

                if (leftOperandExpressionItem != null)
                    _operatorExpressionItem.Operand1 = leftOperandExpressionItem;

                if (RightOperatorTemplateWrapper != null)
                {
                    rightOperandExpressionItem = RightOperatorTemplateWrapper.GenerateOperatorExpressionItem(rightMostOperatorTemplateWrapper, operandsAreLiterals);
                }
                else if (canHaveRightOperand)
                {
                    var nonOperatorRightOperandType = candidateNonOperatorExpressionItemType;

                    bool isRightOperandRightMost = true;

                    var currentParent = this.ParentTemplateWrapper;
                    var currChild = this;
                    while (currentParent != null)
                    {
                        if (currentParent.LeftOperatorTemplateWrapper == currChild)
                        {
                            isRightOperandRightMost = false;
                            break;
                        }

                        currChild = currentParent;
                        currentParent = currentParent.ParentTemplateWrapper;
                    }

                    if (!isRightOperandRightMost)
                        nonOperatorRightOperandType &= (~ExpressionItemTypeFlags.CodeBlock);

                    if (!operandsAreLiterals)
                    {
                        rightOperandExpressionItem = _codeGenerator.GenerateExpressionItem(nonOperatorRightOperandType,
                            new ExpressionItemGenerationOptions
                            {
                                CanAddCodeBlockPostfix = (this == rightMostOperatorTemplateWrapper)
                            });
                    }
                    else
                    {
                        rightOperandExpressionItem = GenerateSimpleLiteralExpressionItem();
                    }
                }

                if (rightOperandExpressionItem != null)
                {
                    if (operatorInfo.OperatorType == OperatorType.BinaryOperator)
                        _operatorExpressionItem.Operand2 = rightOperandExpressionItem;
                    else
                        _operatorExpressionItem.Operand1 = rightOperandExpressionItem;
                }

                return _operatorExpressionItem;
            }
        }

        private BracesExpressionItem GenerateBracesExpressionItem([NotNull, ItemNotNull] List<IExpressionItemBase> prefixExpressionItems,
                                                                  [NotNull, ItemNotNull] List<IKeywordExpressionItem> keywordExpressionItems,
                                                                  [CanBeNull] ILiteralExpressionItem nameExpressionItem,
                                                                  bool isPrefix)

        {
            return GenerateBracesExpressionItem(prefixExpressionItems, keywordExpressionItems, nameExpressionItem,
                _simulationRandomNumberGenerator.Next(100) <= 50, isPrefix);
        }

        private BracesExpressionItem GenerateBracesExpressionItem([NotNull, ItemNotNull] List<IExpressionItemBase> prefixExpressionItems,
                                                                  [NotNull, ItemNotNull] List<IKeywordExpressionItem> keywordExpressionItems,
                                                                  [CanBeNull] ILiteralExpressionItem nameExpressionItem,
                                                                  bool isRoundBraces, bool isPrefix)

        {
            AddWhitespacesAndComments(false);

            var bracesExpressionItem = new BracesExpressionItem(prefixExpressionItems, keywordExpressionItems,
                nameExpressionItem, isRoundBraces, _generatedCode.Length);

            _generatedCode.Append(bracesExpressionItem.OpeningBrace.Text);

            _containerExpressionsStack.Push(bracesExpressionItem);
            PopulateBracesExpressionItem(bracesExpressionItem, isPrefix);
            _containerExpressionsStack.Pop();

            return bracesExpressionItem;
        }

        private void PopulateBracesExpressionItem([NotNull] BracesExpressionItem bracesExpressionItem, bool isPrefix)
        {
            var currentDepth = GetDepth(false);

            bool hasNoParent = _containerExpressionsStack.Count == 1;

            var probabilityGenerator = CreateProbabilityGenerator(1000);

            bool useMinimumPossibleNumberOfParameters = currentDepth >= TestSetup.MaxDepthOfExpression ||
                                                        _numberOfGeneratedExpressionItems >= TestSetup.MaxNumberOfExpressionItems;

            if (isPrefix)
            {
                probabilityGenerator.AddRandomNumbers(new RandomNumbersRange(1,
                    useMinimumPossibleNumberOfParameters ? 1 : TestSetup.MaxNumberOfParametersInFunctionOrMatrix));
            }
            else if (useMinimumPossibleNumberOfParameters)
            {
                probabilityGenerator.AddRandomNumbers(new[] { 0 });
            }
            else if (hasNoParent)
            {
                probabilityGenerator.AddRandomNumbersForProbability(5, new[] { 0 });
                probabilityGenerator.AddRandomNumbers(new RandomNumbersRange(1, TestSetup.MaxNumberOfParametersInFunctionOrMatrix));
            }
            else
            {
                probabilityGenerator.AddRandomNumbers(new RandomNumbersRange(0, TestSetup.MaxNumberOfParametersInFunctionOrMatrix));
            }

            var numberOfParameters = probabilityGenerator.GetRandomNumber();

            for (int i = 0; i < numberOfParameters; ++i)
            {
                if (i > 0)
                {
                    AddWhitespacesAndComments(false);

                    bracesExpressionItem.AddComma(_generatedCode.Length);
                    _generatedCode.Append(",");
                }

                var parameterExpressionItem = GenerateExpressionItem(GetPossibleExpressionItemTypeFlags());

                bracesExpressionItem.AddParameter(parameterExpressionItem);
            }

            AddWhitespacesAndComments(false);

            var braceExpressionItem = new ClosingBraceExpressionItem(bracesExpressionItem.OpeningBrace.IsRoundBrace, _generatedCode.Length);

            bracesExpressionItem.SetClosingBraceInfo(braceExpressionItem);

            _generatedCode.Append(braceExpressionItem.Text);
        }

        private IProbabilityBasedRandomNumberGenerator CreateProbabilityGenerator(int probabilityValueFor100Percent)
        {
            return new ProbabilityBasedRandomNumberGenerator(TestSetup.SimulationRandomNumberGenerator, probabilityValueFor100Percent);
        }

        private void AddChildExpressions([NotNull] ExpressionItemSeriesBase parentExpressionItemSeries)
        {
            var hasNoParent = _containerExpressionsStack.Count == 1;

            var currentDepth = GetDepth(false);

            int numberOfExpressionItems;

            if (currentDepth >= TestSetup.MaxDepthOfExpression ||
                _numberOfGeneratedExpressionItems >= TestSetup.MaxNumberOfExpressionItems)
            {
                numberOfExpressionItems = 0;
            }
            else
            {
                var probabilityGenerator = CreateProbabilityGenerator(1000);

                int maxNumberOfExpressionsInCodeBlock = _expressionLanguageProviderWrapper.ExpressionLanguageProvider.SupportsMultipleExpressions() ? TestSetup.MaxNumberOfExpressionsInCodeBlock : 1;

                if (hasNoParent)
                {
                    probabilityGenerator.AddRandomNumbersForProbability(10, new[] { 0 });
                    probabilityGenerator.AddRandomNumbers(new RandomNumbersRange(1, maxNumberOfExpressionsInCodeBlock));
                }
                else
                {
                    probabilityGenerator.AddRandomNumbers(new RandomNumbersRange(0, maxNumberOfExpressionsInCodeBlock));
                }

                numberOfExpressionItems = probabilityGenerator.GetRandomNumber();
            }

            IComplexExpressionItem prevExpressionItem = null;
            for (var i = 0; i < numberOfExpressionItems; ++i)
            {
                if (prevExpressionItem != null)
                {
                    if (_expressionLanguageProviderWrapper.ExpressionLanguageProvider.ExpressionSeparatorCharacter != '\0')
                    {
                        if (prevExpressionItem is ICodeBlockExpressionItem)
                        {
                            if (_simulationRandomNumberGenerator.Next(100) <= 30)
                                AddCodeSeparator(parentExpressionItemSeries);
                        }
                        else
                        {
                            AddCodeSeparator(parentExpressionItemSeries);
                        }
                    }
                }

                var expressionItem = GenerateExpressionItem(GetPossibleExpressionItemTypeFlags());

                parentExpressionItemSeries.AddChildExpressionItem(expressionItem);

                prevExpressionItem = expressionItem;
            }
        }
        private CodeBlockExpressionItem GenerateCodeBlockExpressionItem([NotNull, ItemNotNull] List<IExpressionItemBase> prefixExpressionItems,
                                                                        [NotNull, ItemNotNull] List<IKeywordExpressionItem> keywordExpressionItems)

        {
            if (_expressionLanguageProviderWrapper.ExpressionLanguageProvider.CodeBlockStartMarker == null)
                throw new ArgumentNullException(nameof(IExpressionLanguageProvider.CodeBlockStartMarker));

            if (_expressionLanguageProviderWrapper.ExpressionLanguageProvider.CodeBlockEndMarker == null)
                throw new ArgumentNullException(nameof(IExpressionLanguageProvider.CodeBlockEndMarker));

            AddWhitespacesAndCommentsIfNecessary(_expressionLanguageProviderWrapper.ExpressionLanguageProvider.CodeBlockStartMarker);
            var codeBlockStartMarker = new CodeBlockStartMarkerExpressionItem(TestSetup.CodeGenerationHelper.ApplyRandomCapitalization(
                    _expressionLanguageProviderWrapper.ExpressionLanguageProvider.CodeBlockStartMarker),
                _generatedCode.Length);
            _generatedCode.Append(codeBlockStartMarker.Text);

            var codeBlockExpressionItem = new CodeBlockExpressionItem(prefixExpressionItems, keywordExpressionItems, codeBlockStartMarker);

            _containerExpressionsStack.Push(codeBlockExpressionItem);
            AddChildExpressions(codeBlockExpressionItem);
            _containerExpressionsStack.Pop();

            AddWhitespacesAndCommentsIfNecessary(_expressionLanguageProviderWrapper.ExpressionLanguageProvider.CodeBlockEndMarker);
            var codeBlockEndMarker = new CodeBlockEndMarkerExpressionItem(TestSetup.CodeGenerationHelper.ApplyRandomCapitalization(
                _expressionLanguageProviderWrapper.ExpressionLanguageProvider.CodeBlockEndMarker), _generatedCode.Length);
            _generatedCode.Append(codeBlockEndMarker.Text);

            codeBlockExpressionItem.CodeBlockEndMarker = codeBlockEndMarker;
            return codeBlockExpressionItem;
        }

        private ComplexExpressionItemBase GenerateCustomExpressionItem([NotNull, ItemNotNull] List<IExpressionItemBase> prefixExpressionItems,
                                                                              [NotNull, ItemNotNull] List<IKeywordExpressionItem> keywordExpressionItems,
                                                                              CustomExpressionItemCategory expectedCustomExpressionItemCategory)
        {
            var selectorKeywordExpressionItem = keywordExpressionItems.Count == 0 ? null : keywordExpressionItems[keywordExpressionItems.Count - 1];

            var keywordId = selectorKeywordExpressionItem?.LanguageKeywordInfo.Id;

            ComplexExpressionItemBase complexExpressionItem = null;

            if (keywordId != null)
            {
                var keywordInfo = this._expressionLanguageProviderWrapper.GetKeywordInfo(keywordId.Value);

                var keywordExpressionItemsWithoutSelector = keywordExpressionItems.Where(x => x.LanguageKeywordInfo.Id != keywordInfo.Id).ToList();

                switch (keywordInfo.Id)
                {
                    case KeywordIds.GenericTypes:

                        complexExpressionItem = GenerateGenericTypesCustomExpressionItem(
                            prefixExpressionItems, keywordExpressionItemsWithoutSelector,
                            selectorKeywordExpressionItem);
                        break;

                    case KeywordIds.Metadata:

                        complexExpressionItem = GenerateMetadataCustomExpressionItem(
                            prefixExpressionItems, keywordExpressionItemsWithoutSelector,
                            selectorKeywordExpressionItem);
                        break;

                    case KeywordIds.Performance:

                        complexExpressionItem = GeneratePerformanceCustomExpressionItem(
                            prefixExpressionItems, keywordExpressionItemsWithoutSelector,
                            selectorKeywordExpressionItem);
                        break;

                    case KeywordIds.Pragma:

                        complexExpressionItem = GeneratePragmaCustomExpressionItem(
                            prefixExpressionItems, keywordExpressionItemsWithoutSelector,
                            selectorKeywordExpressionItem);
                        break;

                    case KeywordIds.Where:

                        complexExpressionItem = GenerateWhereCustomExpressionItem(
                            prefixExpressionItems, keywordExpressionItemsWithoutSelector,
                            selectorKeywordExpressionItem);
                        break;
                }
            }

            if (complexExpressionItem == null || !(complexExpressionItem is ICustomExpressionItem customExpressionItem))
                throw new ArgumentException($"Invalid custom expression keyword {keywordId}", nameof(keywordExpressionItems));

            Assert.AreEqual(expectedCustomExpressionItemCategory, customExpressionItem.CustomExpressionItemCategory);

            if (customExpressionItem.CustomExpressionItemCategory == CustomExpressionItemCategory.Prefix)
                Assert.AreEqual(0, prefixExpressionItems.Count);

            return complexExpressionItem;
        }

        [NotNull]
        private TestLanguageCustomExpressionBase GenerateGenericTypesCustomExpressionItem(
                                            [NotNull, ItemNotNull] List<IExpressionItemBase> prefixExpressionItems,
                                            [NotNull, ItemNotNull] List<IKeywordExpressionItem> keywordExpressionItemsWithoutSelector,
                                            [NotNull] IKeywordExpressionItem selectorKeywordExpressionItem)
        {
            var bracesExpressionItem = GenerateBracesExpressionItem(new List<IExpressionItemBase>(0), new List<IKeywordExpressionItem>(0),
                 null, false, true);

            return new GenericTypesCustomExpressionItem(prefixExpressionItems, keywordExpressionItemsWithoutSelector,
                selectorKeywordExpressionItem, bracesExpressionItem);
        }

        [NotNull]
        private TestLanguageCustomExpressionBase GenerateMetadataCustomExpressionItem(
                                            [NotNull, ItemNotNull] List<IExpressionItemBase> prefixExpressionItems,
                                            [NotNull, ItemNotNull] List<IKeywordExpressionItem> keywordExpressionItemsWithoutSelector,
                                            [NotNull] IKeywordExpressionItem selectorKeywordExpressionItem)
        {
            var metadataExpressionItem =
                GenerateCodeBlockExpressionItem(new List<IExpressionItemBase>(0), new List<IKeywordExpressionItem>(0));

            return new MetadataCustomExpressionItem(prefixExpressionItems, keywordExpressionItemsWithoutSelector,
                selectorKeywordExpressionItem, metadataExpressionItem);
        }

        [NotNull]
        private TestLanguageCustomExpressionBase GeneratePerformanceCustomExpressionItem(
                                            [NotNull, ItemNotNull] List<IExpressionItemBase> prefixExpressionItems,
                                            [NotNull, ItemNotNull] List<IKeywordExpressionItem> keywordExpressionItemsWithoutSelector,
                                            [NotNull] IKeywordExpressionItem selectorKeywordExpressionItem)
        {
            var bracesExpressionItem = GenerateBracesExpressionItem(new List<IExpressionItemBase>(0), new List<IKeywordExpressionItem>(0),
                    null, true, true);

            return new PerformanceCustomExpressionItem(prefixExpressionItems, keywordExpressionItemsWithoutSelector,
                selectorKeywordExpressionItem, bracesExpressionItem);
        }

        [NotNull]
        private TestLanguageCustomExpressionBase GeneratePragmaCustomExpressionItem(
                                            [NotNull, ItemNotNull] List<IExpressionItemBase> prefixExpressionItems,
                                            [NotNull, ItemNotNull] List<IKeywordExpressionItem> keywordExpressionItemsWithoutSelector,
                                            [NotNull] IKeywordExpressionItem selectorKeywordExpressionItem)
        {
            var pragmaName = GenerateLiteral();

            AddWhitespacesAndCommentsIfNecessary(pragmaName);
            var pragmaExpressionItem = new NameExpressionItem(pragmaName, _generatedCode.Length);
            _generatedCode.Append(pragmaExpressionItem.Text);

            return new PragmaCustomExpressionItem(prefixExpressionItems, keywordExpressionItemsWithoutSelector,
                selectorKeywordExpressionItem, pragmaExpressionItem);
        }

        [NotNull]
        private ComplexExpressionItemBase GenerateWhereCustomExpressionItem(
        [NotNull, ItemNotNull] List<IExpressionItemBase> prefixExpressionItems,
        [NotNull, ItemNotNull] List<IKeywordExpressionItem> keywordExpressionItemsWithoutSelector,
        [NotNull] IKeywordExpressionItem selectorKeywordExpressionItem)
        {
            WhereCustomExpressionItem whereCustomExpressionItem = new WhereCustomExpressionItem(prefixExpressionItems, keywordExpressionItemsWithoutSelector);

            int numberOfWhereStatements = _simulationRandomNumberGenerator.Next(1, 5);

            HashSet<string> generatedTypeNames = new HashSet<string>(_expressionLanguageProviderWrapper.ExpressionLanguageProvider.IsLanguageCaseSensitive ?
                            StringComparer.Ordinal : StringComparer.OrdinalIgnoreCase);

            for (int whereInd = 0; whereInd < numberOfWhereStatements; ++whereInd)
            {
                if (whereInd > 0)
                    selectorKeywordExpressionItem = AddKeywordExpressionItem(new List<IKeywordExpressionItem>(0), selectorKeywordExpressionItem.LanguageKeywordInfo);

                GenericTypeDataExpressionItem genericTypeDataExpressionItem = new GenericTypeDataExpressionItem(selectorKeywordExpressionItem);

                whereCustomExpressionItem.AddGenericTypeData(genericTypeDataExpressionItem);

                string typeName;

                while (true)
                {
                    typeName = GenerateLiteral();

                    if (!generatedTypeNames.Contains(typeName))
                    {
                        generatedTypeNames.Add(typeName);
                        break;
                    }
                }

                AddWhitespacesAndCommentsIfNecessary(typeName);
                genericTypeDataExpressionItem.AddTypeName(new NameExpressionItem(typeName, _generatedCode.Length));
                _generatedCode.Append(typeName);

                AddWhitespacesAndCommentsIfNecessary(":");
                genericTypeDataExpressionItem.AddColon(_generatedCode.Length);
                _generatedCode.Append(':');
                AddWhitespacesAndComments(false);

                var numberOfTypeConstraints = _simulationRandomNumberGenerator.Next(1, 5);

                for (int constraintInd = 0; constraintInd < numberOfTypeConstraints; ++constraintInd)
                {
                    if (constraintInd > 0)
                    {
                        AddWhitespacesAndComments(false);
                        genericTypeDataExpressionItem.AddComma(_generatedCode.Length);
                        _generatedCode.Append(",");
                    }

                    var literalExpressionItem = GenerateLiteralExpressionItem(Array.Empty<IExpressionItemBase>(), Array.Empty<IKeywordExpressionItem>());

                    IExpressionItemBase typeConstraintExpressionItem;

                    if (_simulationRandomNumberGenerator.Next(100) <= 50)
                    {
                        typeConstraintExpressionItem = literalExpressionItem;
                    }
                    else
                    {
                        typeConstraintExpressionItem = GenerateBracesExpressionItem(new List<IExpressionItemBase>(0),
                            new List<IKeywordExpressionItem>(0), literalExpressionItem,
                            _simulationRandomNumberGenerator.Next(100) <= 50);
                    }

                    genericTypeDataExpressionItem.AddTypeConstraint(typeConstraintExpressionItem);
                }
            }

            AddWhitespacesAndCommentsIfNecessary(Constants.WhereEndMarker);

            whereCustomExpressionItem.WhereEndMarker = new NameExpressionItem(TestSetup.CodeGenerationHelper.ApplyRandomCapitalization(Constants.WhereEndMarker),
                _generatedCode.Length);
            
            _generatedCode.Append(whereCustomExpressionItem.WhereEndMarker.Text);

            return whereCustomExpressionItem;
        }

        private NumericExpressionItem GenerateConstantNumber([NotNull][ItemNotNull] IEnumerable<IExpressionItemBase> prefixExpressionItems,
                                                                 [NotNull][ItemNotNull] IEnumerable<IKeywordExpressionItem> keywordExpressionItems)
        {
            // Regular expressions are:
            // [@"^(\d+\.\d+|\d+\.|\.\d+|\d+)(EXP|exp|E|e)-?(\d+\.\d+|\d+\.|\.\d+|\d+)"]
            // [@"^(\d+\.\d+|\d+\.|\.\d+)"]
            // [ @"^\d+"]
            
            //var indexOfSucceededRegularExpression = 0;
            //var hasDot = _simulationRandomNumberGenerator.Next(0, 100) <= 50;
            var usesExpNotation = _simulationRandomNumberGenerator.Next(0, 100) <= 50;

            string GenerateWholeNumber()
            {
                var wholePartLength = _simulationRandomNumberGenerator.Next(1, 20);

                var wholeNumberStrBldr = new StringBuilder();

                AppendNumber(wholeNumberStrBldr, wholePartLength);

                return wholeNumberStrBldr.ToString();
            }

            string GenerateFractionalNumber()
            {
                var fractionalNumberStrBldr = new StringBuilder();
                var randomNumber = _simulationRandomNumberGenerator.Next(0, 100);

                if (randomNumber >= 66)
                {
                    // Example 2.3
                    fractionalNumberStrBldr.Append(GenerateWholeNumber());
                    fractionalNumberStrBldr.Append('.');
                    fractionalNumberStrBldr.Append(GenerateWholeNumber());

                }
                else if (randomNumber >= 33)
                {
                    // Example 2.
                    fractionalNumberStrBldr.Append(GenerateWholeNumber());
                    fractionalNumberStrBldr.Append('.');
                }
                else
                {
                    // Example .3
                    fractionalNumberStrBldr.Append('.');
                    fractionalNumberStrBldr.Append(GenerateWholeNumber());
                }

                return fractionalNumberStrBldr.ToString();
            }

            long numericTypeDescriptorId;

            string generatedNumberText;
            if (usesExpNotation)
            {
                // [@"^(\d+\.\d+|\d+\.|\.\d+|\d+)(EXP|exp|E|e)-?(\d+\.\d+|\d+\.|\.\d+|\d+)"]

                // Example exp values (can have EXP, exp, or E instead of e in these examples
                // 1.2e3.4 .2e3.4   1.e3.4   1.2e.4   1.2e3.
                // 1.2e-3.4 .2e-3.4  1.e-3.4  1.2e-.4  1.2e-3.

                numericTypeDescriptorId = KnownNumericTypeDescriptorIds.ExponentFormatValueId;

                var generatedEpxText = new StringBuilder();

                generatedEpxText.Append(_simulationRandomNumberGenerator.Next(0, 100) <= 50 ? 
                    GenerateWholeNumber() : GenerateFractionalNumber());

                var randNumber = _simulationRandomNumberGenerator.Next(0, 100);

                if (randNumber >= 75)
                    generatedEpxText.Append("EXP");
                else if (randNumber >= 50)
                    generatedEpxText.Append("exp");
                else if (randNumber >= 25)
                    generatedEpxText.Append("E");
                else 
                    generatedEpxText.Append("e");

                if (_simulationRandomNumberGenerator.Next(0, 100) <= 50)
                    generatedEpxText.Append(_simulationRandomNumberGenerator.Next(0, 100) <= 50 ? "-" : "+");

                generatedEpxText.Append(_simulationRandomNumberGenerator.Next(0, 100) <= 50 ?
                    GenerateWholeNumber() : GenerateFractionalNumber());

                generatedNumberText = generatedEpxText.ToString();
            }
            else
            {
                if (_simulationRandomNumberGenerator.Next(0, 100) <= 50)
                {
                    //[@"^\d+"]
                    numericTypeDescriptorId = KnownNumericTypeDescriptorIds.IntegerValueId;
                    generatedNumberText = GenerateWholeNumber();
                }
                else
                {
                    // [@"^(\d+\.\d+|\d+\.|\.\d+)"]
                    numericTypeDescriptorId = KnownNumericTypeDescriptorIds.FloatingPointValueId;
                    generatedNumberText = GenerateFractionalNumber();
                }
            }

            AddWhitespacesAndCommentsIfNecessary(generatedNumberText);

            var numericValueExpressionItem = new NumericExpressionItem(prefixExpressionItems, keywordExpressionItems,
                new NumericExpressionValueItem(generatedNumberText, _generatedCode.Length),
                _expressionLanguageProviderWrapper.ExpressionLanguageProvider.NumericTypeDescriptors.First(x => x.NumberTypeId == numericTypeDescriptorId), 0);
            _generatedCode.Append(generatedNumberText);

            return numericValueExpressionItem;
        }

        private void AppendNumber(StringBuilder strBldr, int numberOfDigits)
        {
            for (int i = 0; i < numberOfDigits; ++i)
                strBldr.Append(TestSetup.CodeGenerationHelper.GenerateCharacter(new CharacterTypeProbabilityData(GeneratedCharacterType.Number, 100)));
        }

        private char GetLastCharacter() => _generatedCode.Length == 0 ? '\0' : _generatedCode[_generatedCode.Length - 1];

        private ConstantTextExpressionItem GenerateConstantText([NotNull] [ItemNotNull] IEnumerable<IExpressionItemBase> prefixExpressionItems,
                                                       [NotNull] [ItemNotNull] IEnumerable<IKeywordExpressionItem> keywordExpressionItems,
                                                       [CanBeNull, ItemNotNull] IEnumerable<CharacterTypeProbabilityData> characterTypeProbabilityData = null)
        {
            AddWhitespacesAndComments(false);

            var textStartEndMarker = _expressionLanguageProviderWrapper.ExpressionLanguageProvider.ConstantTextStartEndMarkerCharacters[
                _simulationRandomNumberGenerator.Next(0, _expressionLanguageProviderWrapper.ExpressionLanguageProvider.ConstantTextStartEndMarkerCharacters.Count - 1)];

            var constantTextStrBldr = new StringBuilder();
            constantTextStrBldr.Append(textStartEndMarker);

            constantTextStrBldr.Append(TestSetup.CodeGenerationHelper.GenerateText(_simulationRandomNumberGenerator.Next(100), WhitespaceFlags.AnyWhitespace, textStartEndMarker, characterTypeProbabilityData));
                //TestHelpers.GenerateText(_simulationRandomNumberGenerator.Next(100), WhitespaceFlags.AnyWhitespace, textStartEndMarker));

            constantTextStrBldr.Append(textStartEndMarker);

            var constantText = constantTextStrBldr.ToString();

            var cSharpText = new StringBuilder();

            var currentCharInd = 1;

            while(currentCharInd < constantText.Length - 1)
            {
                var currentChar = constantText[currentCharInd];

                cSharpText.Append(currentChar);

                // The expression start/end marker character always appears twice in valid expression.
                // However, IConstantTextValueExpressionItem.CSharpText includes only once character in text.
               
                if (currentChar == textStartEndMarker)
                    currentCharInd += 2;
                else
                    ++currentCharInd;
            }

            var constantTextExpressionItem = new ConstantTextExpressionItem(prefixExpressionItems,
                keywordExpressionItems, new ConstantTextValueExpressionItem(constantText, cSharpText.ToString(),
                    _generatedCode.Length));

            _generatedCode.Append(constantText);

            return constantTextExpressionItem;
        }

        private LiteralExpressionItem GenerateLiteralExpressionItem([NotNull] [ItemNotNull] IEnumerable<IExpressionItemBase> prefixExpressionItems,
                                                                        [NotNull] [ItemNotNull] IEnumerable<IKeywordExpressionItem> keywordExpressionItems)
        {
            var literal = GenerateLiteral();

            AddWhitespacesAndCommentsIfNecessary(literal);
            var literalNameExpressionItem = new LiteralNameExpressionItem(literal, _generatedCode.Length);
            _generatedCode.Append(literal);

            return new LiteralExpressionItem(prefixExpressionItems, keywordExpressionItems, literalNameExpressionItem);
        }

        private string GenerateLiteral()
        {
            bool LiteralHasConflictsWithOtherIdentifiers(string identifier)
            {
                if (ContainsSpecialNonOperatorCharacters(identifier) || ConflictsWithReservedWords(_expressionLanguageProviderWrapper.ExpressionLanguageProvider, identifier) ||
                    ConflictsWithComments(_expressionLanguageProviderWrapper.ExpressionLanguageProvider, identifier) || 
                    ConflictsWithCodeSeparatorMarkers(_expressionLanguageProviderWrapper.ExpressionLanguageProvider, identifier) ||
                    ConflictsWithOperatorNameParts(_expressionLanguageProviderWrapper.ExpressionLanguageProvider, identifier) ||
                    ConflictsWithKeywords(_expressionLanguageProviderWrapper.ExpressionLanguageProvider, identifier))
                    return true;

                return false;
            }

            while (true)
            {
                string literal;
                if (TestSetup.SimulateNiceCode)
                {
                    var literalStrBldr = new StringBuilder();

                    literalStrBldr.Append(TestSetup.CodeGenerationHelper.GenerateCharacter(
                        new CharacterTypeProbabilityData(GeneratedCharacterType.Letter, 90),
                        new CharacterTypeProbabilityData(GeneratedCharacterType.Underscore, 10)));

                    while (literalStrBldr.Length < _simulationRandomNumberGenerator.Next(0, 8))
                    {
                        literalStrBldr.Append(TestSetup.CodeGenerationHelper.GenerateCharacter(
                            new CharacterTypeProbabilityData(GeneratedCharacterType.Letter, 60),
                            new CharacterTypeProbabilityData(GeneratedCharacterType.Dot, 10),
                            new CharacterTypeProbabilityData(GeneratedCharacterType.Underscore, 10),
                            new CharacterTypeProbabilityData(GeneratedCharacterType.Number, 20)));
                    }

                    if (_simulationRandomNumberGenerator.Next(100) <= 10)
                        literalStrBldr.Append('$');

                    literal = literalStrBldr.ToString();

                    if (LiteralHasConflictsWithOtherIdentifiers(literal))
                        continue;
                }
                else
                {
                    literal = TestHelpers.GenerateRandomWord(TestSetup.SpecialOperatorCharactersUsedInLiterals, LiteralHasConflictsWithOtherIdentifiers);
                }

                return literal;
            }

        }

        private void AddWhitespacesAndCommentsIfNecessary([NotNull] string addedExpressionItemText, WhitespaceCommentFlags whitespaceCommentFlags = WhitespaceCommentFlags.WhiteSpacesAndComments)
        {
            if (addedExpressionItemText.Length == 0)
                throw new ArgumentNullException(nameof(addedExpressionItemText));

            if (_generatedCode.Length > 0)
            {
                var lastChar = GetLastCharacter();

                if (!(SpecialCharactersCacheThreadStaticContext.Context.IsSpecialCharacter(lastChar) &&
                      !TestSetup.SpecialOperatorCharactersUsedInLiterals.Contains(lastChar)
                      || SpecialCharactersCacheThreadStaticContext.Context.IsSpecialCharacter(addedExpressionItemText[0])))
                {
                    AddWhitespacesAndComments(true, whitespaceCommentFlags);
                    return;
                }
            }

            AddWhitespacesAndComments(false);
        }

        private void AddWhitespacesAndComments(bool addAtLeastOneWhitespaceOrComment, WhitespaceCommentFlags whitespaceCommentFlags = WhitespaceCommentFlags.WhiteSpacesAndComments)
        {
            TestSetup.CodeGenerationHelper.GenerateWhitespacesAndComments(_generatedCode, addAtLeastOneWhitespaceOrComment,
                commentedTextData => this._commentedTextData.Add(commentedTextData), whitespaceCommentFlags);
        }

        private void AddCodeSeparator([NotNull] ICanAddSeparatorCharacterExpressionItem canAddSeparatorCharacterExpressionItem)
        {
            if (_expressionLanguageProviderWrapper.ExpressionLanguageProvider.ExpressionSeparatorCharacter == '\0')
                throw new NotSupportedException();

            AddWhitespacesAndComments(false);

            var separatorExpressionItem = new SeparatorCharacterExpressionItem(_expressionLanguageProviderWrapper.ExpressionLanguageProvider.ExpressionSeparatorCharacter,
                _generatedCode.Length);

            canAddSeparatorCharacterExpressionItem.AddSeparatorCharacterExpressionItem(separatorExpressionItem);

            _generatedCode.Append(separatorExpressionItem.Text);

            if (TestSetup.SimulateNiceCode)
                _generatedCode.Append(Environment.NewLine);
        }

        public static bool ContainsSpecialNonOperatorCharacters([NotNull] string word, params char[] allowedNonOperatorSpecialCharacters)
        {
            return word.Any(x =>
                (allowedNonOperatorSpecialCharacters == null || !allowedNonOperatorSpecialCharacters.Contains(x)) &&
                SpecialCharactersCacheThreadStaticContext.Context.IsSpecialNonOperatorCharacter(x));
        }

        public static bool ConflictsWithReservedWords([NotNull] IExpressionLanguageProvider expressionLanguageProvider,
            [NotNull] string identifier)
        {
            return Helpers.CheckIfIdentifiersConflict(expressionLanguageProvider, identifier,
                Constants.WhereEndMarker);
        }
        public static bool ConflictsWithComments([NotNull] IExpressionLanguageProvider expressionLanguageProvider, [NotNull] string identifier)
        {
            if (expressionLanguageProvider.LineCommentMarker != null && 
                Helpers.CheckIfIdentifierIsInAnotherIdentifier(expressionLanguageProvider, expressionLanguageProvider.LineCommentMarker, identifier))
                return true;

            if (expressionLanguageProvider.MultilineCommentStartMarker != null && 
                Helpers.CheckIfIdentifierIsInAnotherIdentifier(expressionLanguageProvider, expressionLanguageProvider.MultilineCommentStartMarker, identifier))
                return true;

            if (expressionLanguageProvider.MultilineCommentEndMarker != null && 
                Helpers.CheckIfIdentifierIsInAnotherIdentifier(expressionLanguageProvider, expressionLanguageProvider.MultilineCommentEndMarker, identifier))
                return true;

            return false;
        }

        public static bool ConflictsWithCodeSeparatorMarkers([NotNull] IExpressionLanguageProvider expressionLanguageProvider, [NotNull] string identifier)
        {
            if (expressionLanguageProvider.ExpressionSeparatorCharacter != '\0' && 
                Helpers.CheckIfIdentifiersConflict(expressionLanguageProvider, expressionLanguageProvider.ExpressionSeparatorCharacter.ToString(), identifier))
                return true;

            if (expressionLanguageProvider.CodeBlockStartMarker != null && 
                Helpers.CheckIfIdentifiersConflict(expressionLanguageProvider, expressionLanguageProvider.CodeBlockStartMarker, identifier))
                return true;

            if (expressionLanguageProvider.CodeBlockEndMarker != null && 
                Helpers.CheckIfIdentifiersConflict(expressionLanguageProvider, expressionLanguageProvider.CodeBlockEndMarker, identifier))
                return true;

            return false;
        }

        public static bool ConflictsWithKeywords([NotNull] IExpressionLanguageProvider expressionLanguageProvider, [NotNull] string identifier)
        {
            foreach (var keyword in expressionLanguageProvider.Keywords)
            {
                if (Helpers.CheckIfIdentifiersConflict(expressionLanguageProvider, identifier, keyword.Keyword))
                    return true;
            }

            return false;
        }

        public static bool ConflictsWithOperatorNameParts([NotNull] IExpressionLanguageProvider expressionLanguageProvider, [NotNull] string identifier)
        {
            foreach (var operatorInfo in expressionLanguageProvider.Operators)
            {
                if (operatorInfo.NameParts.Any(namePart => 
                    Helpers.CheckIfIdentifiersConflict(expressionLanguageProvider, namePart, identifier)))
                    return true;
            }

            return false;
        }


        [Flags]
        public enum ExpressionItemTypeFlags : ulong
        {
            All = ulong.MaxValue,

            Literal = 1,
            ConstantText = Literal << 1,
            ConstantNumericValue = ConstantText << 1,

            Braces = ConstantNumericValue << 1,
            NamedBraces = Braces << 1,
            CodeBlock = NamedBraces << 1,

            RegularCustomExpressionItem = CodeBlock << 1,
            Operators = RegularCustomExpressionItem << 1
        }

        public class OperatorInfosPerPriority
        {
            public Dictionary<OperatorPriority, List<IOperatorInfo>> OperatorPriorityOperatorInfosMap { get; } = new Dictionary<OperatorPriority, List<IOperatorInfo>>();
        }

    }
}