// Copyright (c) UniversalExpressionParser Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UniversalExpressionParser.ExpressionItems;

namespace UniversalExpressionParser.Parser
{
    internal class ParseOperatorsHelper
    {
        [NotNull] 
        private readonly ParseKeywordsHelper _parseKeywordsHelper;

        [NotNull]
        private readonly Dictionary<int, WhitespaceOrCommentData> _positionInTextToWhitespaceOrCommentData = new Dictionary<int, WhitespaceOrCommentData>();

        public ParseOperatorsHelper([NotNull] ParseKeywordsHelper parseKeywordsHelper)
        {
            _parseKeywordsHelper = parseKeywordsHelper;
        }
        internal void ProcessUnprocessedOperators([NotNull] ParseExpressionItemContext context,
                                                 [NotNull] ParseExpressionItemData parseExpressionItemData,
                                                 [CanBeNull] IExpressionItemBase newOperand,
                                                 [CanBeNull] IgnoreBinaryOperatorWhenBreakingPostfixPrefixTieDelegate ignoreBinaryOperatorWhenBreakingPostfixPrefixTie,
                                                 ref int numberOfOperators)
        {
            var allExpressionItems = parseExpressionItemData.AllExpressionItems;
            var parsedUnprocessedOperatorInfoItems = parseExpressionItemData.ParsedUnprocessedOperatorInfoItems;

            // ReSharper disable once AccessToModifiedClosure
            if (parsedUnprocessedOperatorInfoItems == null)
                return;

            if (parsedUnprocessedOperatorInfoItems.Count == 0)
            {
                parseExpressionItemData.ParsedUnprocessedOperatorInfoItems = null;
                return;
            }

            bool ShouldAddErrorMessages() => !(parseExpressionItemData.OperatorsCannotBeEvaluated || context.ParseErrorData.HasCriticalErrors);

            numberOfOperators += parsedUnprocessedOperatorInfoItems.Count;

            var invalidOperatorsFoundCurrent = false;

            // We might have a case like (x++ - !y), and we might have two items for ++, and two items for +, since ++ can be 
            // used both as a prefix as well as a postfix, and - can be either binary or prefix.
            // We iterate trough lists of operator items, and determine what the operator should be, based on current context.
            IExpressionItemBase operand1 = null;

            if (allExpressionItems.Count > 0)
            {
                operand1 = allExpressionItems[allExpressionItems.Count - 1];

                if (operand1 is IKeywordExpressionItem)
                {
                    // We already added an error of type ParseErrorItemCode.InvalidUseOfKeywords (or a similar error).
                    // Lets ignore the keyword, and find the last non-keyword expression.
                    operand1 = null;

                    if (allExpressionItems.Count >= 2)
                    {
                        for (var i = allExpressionItems.Count - 2; i >= 0; --i)
                        {
                            var potentialOperand1 = allExpressionItems[i];

                            if (!(potentialOperand1 is IKeywordExpressionItem))
                            {
                                operand1 = potentialOperand1;
                                break;
                            }
                        }
                    }
                }
            }

            if (operand1 != null)
            {
                var operand2 = newOperand;

                if (operand2 != null)
                {
                    if (operand2 == operand1 || operand2.IndexInText <= operand1.IndexInText)
                        throw new ArgumentException($"Invalid parameter '{nameof(newOperand)}'. The operand should be after previous operand.", nameof(newOperand));

                    var binaryOperatorsData = new List<OperatorPositionData>(parsedUnprocessedOperatorInfoItems.Count);

                    var indexOfLeftMostPrefixOperatorInfoExpressionItem = parsedUnprocessedOperatorInfoItems.Count; // - 1;
                    var indexOfRightMostPostfixOperatorInfoExpressionItem = -1;

                    var nonAmbiguousBinaryOperatorFound = false;

                    for (var operatorIndex = 0; operatorIndex < parsedUnprocessedOperatorInfoItems.Count; ++operatorIndex)
                    {
                        var parsedOperatorInfoExpressionItems = parsedUnprocessedOperatorInfoItems[operatorIndex];

                        IOperatorInfoExpressionItem binaryOperatorInfoExpressionItem = null;
                        IOperatorInfoExpressionItem prefixOperatorExpressionItem = null;

                        foreach (var operatorInfoExpressionItem in parsedOperatorInfoExpressionItems)
                        {
                            switch (operatorInfoExpressionItem.OperatorInfo.OperatorType)
                            {
                                case OperatorType.BinaryOperator:
                                    if (ignoreBinaryOperatorWhenBreakingPostfixPrefixTie != null && ignoreBinaryOperatorWhenBreakingPostfixPrefixTie(operatorInfoExpressionItem))
                                        continue;

                                    binaryOperatorInfoExpressionItem = operatorInfoExpressionItem;
                                    break;

                                case OperatorType.PrefixUnaryOperator:

                                    prefixOperatorExpressionItem = operatorInfoExpressionItem;

                                    if (indexOfLeftMostPrefixOperatorInfoExpressionItem == parsedUnprocessedOperatorInfoItems.Count)
                                        indexOfLeftMostPrefixOperatorInfoExpressionItem = operatorIndex;

                                    break;

                                case OperatorType.PostfixUnaryOperator:

                                    if (indexOfRightMostPostfixOperatorInfoExpressionItem == operatorIndex - 1)
                                        indexOfRightMostPostfixOperatorInfoExpressionItem = operatorIndex;
                                    break;
                            }
                        }

                        if (binaryOperatorInfoExpressionItem != null)
                        {
                            if (parsedOperatorInfoExpressionItems.Count == 1 || parsedUnprocessedOperatorInfoItems.Count == 1)
                            {
                                nonAmbiguousBinaryOperatorFound = true;
                                binaryOperatorsData.Clear();
                                binaryOperatorsData.Add(new OperatorPositionData(binaryOperatorInfoExpressionItem, operatorIndex));
                                break;
                            }

                            binaryOperatorsData.Add(new OperatorPositionData(binaryOperatorInfoExpressionItem, operatorIndex));
                        }

                        if (prefixOperatorExpressionItem == null)
                            indexOfLeftMostPrefixOperatorInfoExpressionItem = parsedUnprocessedOperatorInfoItems.Count;
                    }

                    OperatorPositionData selectedBinaryOperatorData = null;

                    if (nonAmbiguousBinaryOperatorFound)
                    {
                        selectedBinaryOperatorData = binaryOperatorsData[0];
                    }
                    else
                    {
                        foreach (var binaryOperatorPositionData in binaryOperatorsData)
                        {
                            if (binaryOperatorPositionData.IndexInOperators <= indexOfRightMostPostfixOperatorInfoExpressionItem + 1 &&
                                binaryOperatorPositionData.IndexInOperators >= indexOfLeftMostPrefixOperatorInfoExpressionItem - 1)
                            {
                                selectedBinaryOperatorData = binaryOperatorPositionData;
                                break;
                            }
                        }
                    }

                    var binaryOperatorMissingErrorPositionInText = -1;

                    if (selectedBinaryOperatorData != null)
                    {
                        indexOfRightMostPostfixOperatorInfoExpressionItem = selectedBinaryOperatorData.IndexInOperators - 1;
                        indexOfLeftMostPrefixOperatorInfoExpressionItem = selectedBinaryOperatorData.IndexInOperators + 1;
                    }
                    else
                    {
                        // We have a situation when we didn't parse to operators like x post1 post2 bin pref1 pref1.
                        // However we want the calls to AddUnaryOperatorExpressionItems() to proces all operators, so that
                        // all operator expression items are added to our list.
                        // Therefore, lets try find some index before which all operators are asumed to be prefixe, and after which all operators are assumed to be postfixes.
                        if (indexOfRightMostPostfixOperatorInfoExpressionItem >= 0)
                        {
                            if (indexOfRightMostPostfixOperatorInfoExpressionItem < parsedUnprocessedOperatorInfoItems.Count - 1 &&
                                indexOfLeftMostPrefixOperatorInfoExpressionItem <= indexOfRightMostPostfixOperatorInfoExpressionItem + 1)
                            {
                                indexOfLeftMostPrefixOperatorInfoExpressionItem = indexOfRightMostPostfixOperatorInfoExpressionItem + 1;
                                binaryOperatorMissingErrorPositionInText = parsedUnprocessedOperatorInfoItems[indexOfLeftMostPrefixOperatorInfoExpressionItem][0].IndexInText;
                            }
                            else
                            {
                                indexOfRightMostPostfixOperatorInfoExpressionItem = -1;
                                binaryOperatorMissingErrorPositionInText = operand2.IndexInText;
                            }
                        }
                        else
                        {
                            indexOfLeftMostPrefixOperatorInfoExpressionItem = 0;
                            binaryOperatorMissingErrorPositionInText = parsedUnprocessedOperatorInfoItems[indexOfLeftMostPrefixOperatorInfoExpressionItem][0].IndexInText;
                        }
                    }

                    if (indexOfRightMostPostfixOperatorInfoExpressionItem >= 0)
                        AddUnaryOperatorExpressionItems(false, 0, indexOfRightMostPostfixOperatorInfoExpressionItem);

                    if (binaryOperatorMissingErrorPositionInText >= 0)
                    {
                        invalidOperatorsFoundCurrent = true;

                        if (ShouldAddErrorMessages())
                            context.AddParseErrorItem(new ParseErrorItem(
                                binaryOperatorMissingErrorPositionInText, () => ExpressionParserMessages.BinaryOperatorMissingError,
                                ParseErrorItemCode.BinaryOperatorMissing));
                    }
                    else if (selectedBinaryOperatorData != null)
                    {
                        allExpressionItems.Add(selectedBinaryOperatorData.OperatorInfoExpressionItem);
                    }
                    else
                    {
                        invalidOperatorsFoundCurrent = true;

                        if (ShouldAddErrorMessages())
                            context.AddParseErrorItem(new ParseErrorItem(
                                parsedUnprocessedOperatorInfoItems[0][0].IndexInText,
                            () => "Binary operator is missing. This is an implementation error and normally should not happen.",
                                ParseErrorItemCode.ParserImplementationError));
                    }


                    if (indexOfLeftMostPrefixOperatorInfoExpressionItem > 0 && indexOfLeftMostPrefixOperatorInfoExpressionItem < parsedUnprocessedOperatorInfoItems.Count)
                        AddUnaryOperatorExpressionItems(true, indexOfLeftMostPrefixOperatorInfoExpressionItem,
                            parsedUnprocessedOperatorInfoItems.Count - 1);
                }
                else
                {
                    AddUnaryOperatorExpressionItems(false, 0, parsedUnprocessedOperatorInfoItems.Count - 1);
                }
            }
            else
            {
                AddUnaryOperatorExpressionItems(true, 0, parsedUnprocessedOperatorInfoItems.Count - 1);
            }

            void AddUnaryOperatorExpressionItems(bool isAddPrefixOperators, int operatorIndexStart, int operatorIndexEnd)
            {
                OperatorType expectedOperatorType = isAddPrefixOperators ? OperatorType.PrefixUnaryOperator : OperatorType.PostfixUnaryOperator;

                for (var operatorIndex = operatorIndexStart; operatorIndex <= operatorIndexEnd; ++operatorIndex)
                {
                    var parsedOperatorInfoExpressionItems = parsedUnprocessedOperatorInfoItems[operatorIndex];

                    var operatorInfoExpressionItem = parsedOperatorInfoExpressionItems.FirstOrDefault(
                        x => x.OperatorInfo.OperatorType == expectedOperatorType);

                    if (operatorInfoExpressionItem == null)
                    {
                        if (!invalidOperatorsFoundCurrent)
                        {
                            // Add error message
                            invalidOperatorsFoundCurrent = true;

                            operatorInfoExpressionItem = parsedOperatorInfoExpressionItems[0];

                            if (ShouldAddErrorMessages())
                            {
                                string errorMessage;
                                int parseErrorItemCode;
                                switch (expectedOperatorType)
                                {
                                    // expectedOperatorType will never be OperatorType.BinaryOperator if e get here
                                    case OperatorType.PrefixUnaryOperator:
                                        parseErrorItemCode = ParseErrorItemCode.ExpectedPrefixOperator;
                                        errorMessage = ExpressionParserMessages.ExpectedPrefixOperatorError;
                                        break;

                                    case OperatorType.PostfixUnaryOperator:
                                        errorMessage = ExpressionParserMessages.ExpectedPostfixOperatorError;
                                        parseErrorItemCode = ParseErrorItemCode.ExpectedPostfixOperator;
                                        break;

                                    default:
                                        // expectedOperatorType will never be OperatorType.BinaryOperator if we get here.
                                        throw new ArgumentException($"Invalid value {expectedOperatorType}");
                                }

                                context.AddParseErrorItem(new ParseErrorItem(
                                    operatorInfoExpressionItem.IndexInText, () => errorMessage, parseErrorItemCode));
                            }
                        }
                    }

                    if (operatorInfoExpressionItem == null)
                    {
                        // Just take the first one;
                        operatorInfoExpressionItem = parsedOperatorInfoExpressionItems[0];
                    }

                    allExpressionItems.Add(operatorInfoExpressionItem);
                }
            }

            if (invalidOperatorsFoundCurrent)
                parseExpressionItemData.OperatorsCannotBeEvaluated = true;

            // ReSharper disable once AccessToModifiedClosure
            parseExpressionItemData.ParsedUnprocessedOperatorInfoItems = null;
        }

        /// <summary>
        /// Parses the expression.
        /// </summary>
        /// <param name="context">Context.</param>
        /// <param name="sortedCandidateOperators">
        /// Operators descended sorted by number of operator parts. For example operator "++ //" will be before "--".
        /// </param>
        /// <param name="hasOperand1">True, if the operators are preceded by an operand.</param>
        /// <param name="ignoreOperatorInfoDelegate">Used for tests only. Might be removed in the future.</param>
        /// <param name="parsedKeywords">When we parse an expression
        /// like x++ +::pragma ::marker y, where ++, + and : are operators, and ::pragma and ::marker are keywords,
        /// we want to parse operators only up to a point when we encounter keywords (++, and), and we want to parse all the keywords that appear after
        /// the operators.
        /// In this example, the method will return "++, +" as operators, and "::pragma, ::marker" in <paramref name="parsedKeywords"/>.
        /// </param>
        internal List<List<IOperatorInfoExpressionItem>> TryParseOperatorsAndKeywords([NotNull] ParseExpressionItemContext context,
            [NotNull] IReadOnlyList<IOperatorInfo> sortedCandidateOperators, bool hasOperand1,
            [CanBeNull] IgnoreOperatorInfoDelegate ignoreOperatorInfoDelegate,
            out List<IKeywordExpressionItem> parsedKeywords)
        {
            parsedKeywords = null;

            //++Counter;
            //LogContextData();

            OperatorTypeFlags operatorTypeFlags;
            //OperatorTypeFlags lastMatchedOperatorFlags = OperatorTypeFlags.None;

            if (hasOperand1)
                operatorTypeFlags = OperatorTypeFlags.BinaryOperator | OperatorTypeFlags.PostfixUnaryOperator;
            else
                operatorTypeFlags = OperatorTypeFlags.PrefixUnaryOperator;

            List<List<IOperatorInfoExpressionItem>> parsedOperators = null;

            var textSymbolsParser = context.TextSymbolsParser;
            var expressionLanguageProvider = context.ExpressionLanguageProviderWrapper.ExpressionLanguageProvider;

            var stringComparison = expressionLanguageProvider.IsLanguageCaseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;

            bool IsValidTextAfterMatchedTextMiddleOperatorName(char characterAfterMatchedText, int positionInText)
            {
                return false;
            }

            bool TryParseComments()
            {
                int currentPositionInText = textSymbolsParser.PositionInText;

                if (_positionInTextToWhitespaceOrCommentData.TryGetValue(currentPositionInText, out var whitespaceOrCommentData))
                {
                    if (whitespaceOrCommentData.Length > 0)
                        textSymbolsParser.SkipCharacters(whitespaceOrCommentData.Length);

                    return true;
                }

                var tryParseCommentsResult = Helpers.TryParseComments(context);

                var whitespaceOrCommentLength = textSymbolsParser.PositionInText - currentPositionInText;

                if (whitespaceOrCommentLength > 0)
                {
                    _positionInTextToWhitespaceOrCommentData[currentPositionInText] =
                        new WhitespaceOrCommentData(currentPositionInText, textSymbolsParser.PositionInText - currentPositionInText);
                }

                return tryParseCommentsResult && !textSymbolsParser.IsEndOfTextReached;
            }

            // Start matching next operator
            while (TryParseComments())
            {
                if (parsedOperators != null)
                {
                    parsedKeywords = _parseKeywordsHelper.TryParseKeywords(context);
                    if ((parsedKeywords?.Count ?? 0) > 0)
                        break;
                }

                var startingIndexInText = textSymbolsParser.PositionInText;

                List<IOperatorInfoExpressionItem> operatorInfoExpressionItems = null;

                var lastMatchedOperatorFlags = OperatorTypeFlags.None;
                IOperatorInfo lastMatchedOperatorInfo = null;
                var lastMatchedOperatorFlagsMatch = 0;
                IOperatorInfoExpressionItem lastOperatorInfoExpressionItem = null;

                // Try to find a list of best matched operators
                foreach (var operatorInfo in sortedCandidateOperators)
                {
                    var operatorTypeFlagsMatch = ((int)operatorInfo.OperatorType & (int)operatorTypeFlags) > 0 ? 1 : 0;
                    var startsWithNamePartsSimilarToLastMatchedOperator = false;

                    if (lastMatchedOperatorInfo != null)
                    {
                        if (operatorTypeFlagsMatch <= lastMatchedOperatorFlagsMatch)
                        {
                            // We will not get anything better
                            if (operatorInfo.NameParts.Count < lastMatchedOperatorInfo.NameParts.Count)
                                break;

                            if (operatorTypeFlagsMatch < lastMatchedOperatorFlagsMatch)
                                continue;

                            // After this point operatorTypeFlagsMatch = lastMatchedOperatorFlagsMatch
                            var ignoreOperator = false;

                            for (var namePartsIndex = 0; namePartsIndex < lastMatchedOperatorInfo.NameParts.Count - 1; ++namePartsIndex)
                            {
                                if (!string.Equals(operatorInfo.NameParts[namePartsIndex], lastMatchedOperatorInfo.NameParts[namePartsIndex],
                                    stringComparison))
                                {
                                    ignoreOperator = true;
                                    break;
                                }
                            }

                            if (ignoreOperator)
                                continue;

                            startsWithNamePartsSimilarToLastMatchedOperator = true;
                        }
                    }

                    if (lastMatchedOperatorInfo == null ||
                        operatorTypeFlagsMatch > lastMatchedOperatorFlagsMatch ||
                        (operatorTypeFlagsMatch == lastMatchedOperatorFlagsMatch && operatorInfo.NameParts.Count > lastMatchedOperatorInfo.NameParts.Count))
                    {
                        var namePartsIndex = 0;

                        if (startsWithNamePartsSimilarToLastMatchedOperator && lastOperatorInfoExpressionItem != null && lastMatchedOperatorInfo.NameParts.Count > 1)
                        {
                            namePartsIndex = lastMatchedOperatorInfo.NameParts.Count - 1;
                            textSymbolsParser.MoveToPosition(lastOperatorInfoExpressionItem.OperatorNameParts[namePartsIndex].IndexInText);
                        }
                        else
                        {
                            textSymbolsParser.MoveToPosition(startingIndexInText);
                        }

                        List<IOperatorNamePartExpressionItem> operatorNamePartExpressionItems = null;

                        for (; namePartsIndex < operatorInfo.NameParts.Count; ++namePartsIndex)
                        {
                            if (namePartsIndex > 0)
                            {
                                if (!TryParseComments())
                                    break;

                                var prevOperatorNamePartExpressionItem = operatorNamePartExpressionItems[namePartsIndex - 1];

                                if (textSymbolsParser.PositionInText == prevOperatorNamePartExpressionItem.IndexInText + prevOperatorNamePartExpressionItem.ItemLength)
                                {
                                    // There should be space, line break or comment between operator name parts.
                                    break;
                                }
                            }

                            var namePart = operatorInfo.NameParts[namePartsIndex];

                            IsValidTextAfterMatchedTextDelegate isValidTextAfterTextToMatch;
                            if (namePartsIndex == operatorInfo.NameParts.Count - 1)
                                isValidTextAfterTextToMatch = Helpers.IsValidTextAfterTextToMatchDefault;
                            else
                                isValidTextAfterTextToMatch = IsValidTextAfterMatchedTextMiddleOperatorName;

                            if (!Helpers.StartsWithSymbol(textSymbolsParser.TextToParse, namePart,
                                textSymbolsParser.PositionInText, textSymbolsParser.ParsedTextEnd,
                                expressionLanguageProvider,
                                isValidTextAfterTextToMatch, out var matchedText))
                                break;

                            var operatorNamePartExpressionItem = new OperatorNamePartExpressionItem(matchedText, textSymbolsParser.PositionInText);

                            if (operatorNamePartExpressionItems == null)
                                operatorNamePartExpressionItems = new List<IOperatorNamePartExpressionItem>(operatorInfo.NameParts.Count);

                            operatorNamePartExpressionItems.Add(operatorNamePartExpressionItem);

                            textSymbolsParser.SkipCharacters(matchedText.Length);
                        }

                        if (operatorNamePartExpressionItems == null || operatorNamePartExpressionItems.Count < operatorInfo.NameParts.Count)
                        {
                            textSymbolsParser.MoveToPosition(startingIndexInText);
                            continue;
                        }

                        var currentOperatorInfoExpressionItem = new OperatorInfoExpressionItem(operatorInfo, operatorNamePartExpressionItems);

                        if (ignoreOperatorInfoDelegate != null && ignoreOperatorInfoDelegate(currentOperatorInfoExpressionItem))
                            continue;

                        if (operatorInfoExpressionItems == null)
                        {
                            operatorInfoExpressionItems = new List<IOperatorInfoExpressionItem>(10);

                            if (parsedOperators == null)
                                parsedOperators = new List<List<IOperatorInfoExpressionItem>>(10);

                            parsedOperators.Add(operatorInfoExpressionItems);
                        }
                        else
                        {
                            // If we got here, lastOperatorInfoExpressionItem is not null, and operatorInfo is better than 
                            // lastOperatorInfoExpressionItem (either the flags match better, or has more name parts), so lets clear operatorInfoExpressionItems.
                            operatorInfoExpressionItems.Clear();
                        }

                        lastOperatorInfoExpressionItem = currentOperatorInfoExpressionItem;
                    }
                    else
                    {
                        // If we got here, then lastMatchedOperatorInfo is not null, and 
                        // (lastMatchedOperatorFlagsMatch == operatorTypeFlagsMatch && operatorInfo.NameParts.Count == lastMatchedOperatorInfo.NameParts.Count) is true
#if DEBUG
                        if (!(lastMatchedOperatorFlagsMatch == operatorTypeFlagsMatch && operatorInfo.NameParts.Count == lastMatchedOperatorInfo.NameParts.Count))
                            throw new Exception("Invalid state.");
#endif
                        var lastMatchedOperatorInfoLastNamePart = lastMatchedOperatorInfo.NameParts[lastMatchedOperatorInfo.NameParts.Count - 1];
                        var currentOperatorInfoLastNamePart = operatorInfo.NameParts[operatorInfo.NameParts.Count - 1];

                        if (lastMatchedOperatorInfoLastNamePart.Length > currentOperatorInfoLastNamePart.Length)
                            continue;

                        var lastPartPositionInText = lastOperatorInfoExpressionItem.OperatorNameParts[lastOperatorInfoExpressionItem.OperatorNameParts.Count - 1].IndexInText;

                        if (!Helpers.StartsWithSymbol(textSymbolsParser.TextToParse, currentOperatorInfoLastNamePart,
                            lastPartPositionInText, textSymbolsParser.ParsedTextEnd,
                            expressionLanguageProvider,
                            null, out var matchedText))
                            continue;

                        var operatorNamePartExpressionItems = new List<IOperatorNamePartExpressionItem>(operatorInfo.NameParts.Count);

                        for (var i = 0; i < lastOperatorInfoExpressionItem.OperatorNameParts.Count - 1; ++i)
                            operatorNamePartExpressionItems.Add(lastOperatorInfoExpressionItem.OperatorNameParts[i]);

                        operatorNamePartExpressionItems.Add(
                            new OperatorNamePartExpressionItem(matchedText, lastPartPositionInText));

                        var currentOperatorInfoExpressionItem = new OperatorInfoExpressionItem(operatorInfo, operatorNamePartExpressionItems);
                        if (ignoreOperatorInfoDelegate != null && ignoreOperatorInfoDelegate(currentOperatorInfoExpressionItem))
                            continue;

                        if (currentOperatorInfoLastNamePart.Length > lastMatchedOperatorInfoLastNamePart.Length)
                            operatorInfoExpressionItems.Clear();

                        lastOperatorInfoExpressionItem = currentOperatorInfoExpressionItem;
                    }

                    operatorInfoExpressionItems.Add(lastOperatorInfoExpressionItem);
                    lastMatchedOperatorInfo = operatorInfo;
                    lastMatchedOperatorFlagsMatch = operatorTypeFlagsMatch;

                    lastMatchedOperatorFlags |= (OperatorTypeFlags)lastMatchedOperatorInfo.OperatorType;
                }

                if (lastMatchedOperatorInfo == null)
                    break;

                var lastOperatorNamePartExpressionItem = lastOperatorInfoExpressionItem.OperatorNameParts[lastOperatorInfoExpressionItem.OperatorNameParts.Count - 1];

                textSymbolsParser.MoveToPosition(lastOperatorNamePartExpressionItem.IndexInText + lastOperatorNamePartExpressionItem.ItemLength);

                if (hasOperand1)
                {
                    if ((lastMatchedOperatorFlags & OperatorTypeFlags.PostfixUnaryOperator) > 0)
                    {
                        if ((lastMatchedOperatorFlags & OperatorTypeFlags.BinaryOperator) > 0)
                            operatorTypeFlags = OperatorTypeFlags.PrefixUnaryOperator;

                        operatorTypeFlags |= (OperatorTypeFlags.BinaryOperator |
                                              OperatorTypeFlags.PostfixUnaryOperator);
                    }
                    else
                    {
                        // If we got here, the last matched operators list contains only prefix. So we can continue only with prefixes.
                        operatorTypeFlags = OperatorTypeFlags.PrefixUnaryOperator;
                    }
                }
            }

            _positionInTextToWhitespaceOrCommentData.Clear();
            return parsedOperators;
        }
    }
}
