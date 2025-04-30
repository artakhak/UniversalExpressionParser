// Copyright (c) UniversalExpressionParser Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using OROptimizer.Diagnostics.Log;
using UniversalExpressionParser.ExpressionItems;
using UniversalExpressionParser.ExpressionItems.Custom;

namespace UniversalExpressionParser.Parser
{
    internal class ParseCustomExpressionHelper
    {
        [NotNull] private readonly ParserHelper _parserHelper;
        [NotNull] private readonly ErrorsHelper _errorsHelper;

        public ParseCustomExpressionHelper([NotNull] ParserHelper parserHelper, [NotNull] ErrorsHelper errorsHelper)
        {
            _parserHelper = parserHelper;
            _errorsHelper = errorsHelper;
        }

        [CanBeNull]
        internal ICustomExpressionItem TryParseCustomExpression([NotNull] ParseExpressionItemContext context,
            [NotNull] Action<IComplexExpressionItem> addExpressionItem,
            [NotNull] ParseExpressionItemData parseExpressionItemData,
            [NotNull] List<IKeywordExpressionItem> keywordExpressionItems,
            [CanBeNull] IComplexExpressionItem potentialPrefixExpressionItem,
            [CanBeNull] IComplexExpressionItem lastParsedComplexExpressionItem,
            [CanBeNull] ICustomExpressionItem lastParsedCustomExpressionItem)
        {
            var textSymbolsParser = context.TextSymbolsParser;
            var expressionLanguageProvider = context.ExpressionLanguageProviderWrapper.ExpressionLanguageProvider;
            var currentPositionInText = textSymbolsParser.PositionInText;

            foreach (var customExpressionItemParser in expressionLanguageProvider.CustomExpressionItemParsers)
            {
                textSymbolsParser.MoveToPosition(currentPositionInText);

                ICustomExpressionItem customExpressionItem;

                try
                {
                    customExpressionItem = customExpressionItemParser.TryParseCustomExpressionItem(context,
                        new List<IExpressionItemBase>(0), keywordExpressionItems);
                }
                catch (Exception e)
                {
                    var errorMessage = $"Custom expression parser '{customExpressionItemParser.GetType().FullName}' threw an exception.";

                    LogHelper.Context.Log.Error(errorMessage, e);

                    context.AddParseErrorItem(new ParseErrorItem(
                        currentPositionInText,
                        () => $"{errorMessage}{Environment.NewLine}See the logs for exception details. If no logs are available, use ${typeof(LogHelper).FullName}.{nameof(LogHelper.RegisterContext)}({typeof(ILogHelperContext).FullName}) method call to register a logger.",
                        ParseErrorItemCode.CustomExpressionParserThrewAnException, true));

                    return null;
                }

                if (customExpressionItem != null)
                {
                    if (context.ParseErrorData.HasCriticalErrors)
                    {
                        if (lastParsedComplexExpressionItem != null)
                            parseExpressionItemData.OperatorsCannotBeEvaluated = true;

                        return customExpressionItem;
                    }

                    var expectedCustomExpressionItemIndex = keywordExpressionItems.Count > 0 ? keywordExpressionItems[0].IndexInText : currentPositionInText;
                    int GetCustomExpressionErrorPosition() => customExpressionItem.ErrorsPositionDisplayValue ?? expectedCustomExpressionItemIndex;

                    string GetCustomExpressionImplementationErrorContext()
                    {
                        return $"Failed custom expression: {customExpressionItemParser.GetType().FullName}, parsed custom expression type: '{customExpressionItem.GetType().FullName}'.";
                    }

                    if (customExpressionItem.ItemLength <= 0)
                    {
                        if (lastParsedComplexExpressionItem != null)
                            parseExpressionItemData.OperatorsCannotBeEvaluated = true;

                        context.AddParseErrorItem(new ParseErrorItem(
                            GetCustomExpressionErrorPosition(),
                            () => $"The value of '{nameof(ICustomExpressionItem.ItemLength)}' is invalid. The value should be positive. {GetCustomExpressionImplementationErrorContext()}",
                            ParseErrorItemCode.ParsedCustomExpressionItemHasNonPositiveLength, true));

                        return customExpressionItem;
                    }

                    switch (customExpressionItem.CustomExpressionItemCategory)
                    {
                        case CustomExpressionItemCategory.Postfix:
                            // Lets find the last non-postfix, non-prefix, and add a postfix to that item
                            IComplexExpressionItem postfixTargetExpressionItem = null;
                            if (lastParsedCustomExpressionItem != null)
                            {
                                if (lastParsedCustomExpressionItem.CustomExpressionItemCategory != CustomExpressionItemCategory.Regular)
                                {
                                    parseExpressionItemData.OperatorsCannotBeEvaluated = true;
                                    context.AddParseErrorItem(new ParseErrorItem(
                                        GetCustomExpressionErrorPosition(),
                                        () => ExpressionParserMessages.CustomPostfixIsInvalidError,
                                        ParseErrorItemCode.CustomPostfixExpressionItemAfterNonRegularExpressionItem));

                                    addExpressionItem(customExpressionItem);
                                    return customExpressionItem;
                                }

                                if (!lastParsedCustomExpressionItem.IsValidPostfix(customExpressionItem, out var errorMessage))
                                {
                                    parseExpressionItemData.OperatorsCannotBeEvaluated = true;
                                    context.AddParseErrorItem(new ParseErrorItem(
                                        GetCustomExpressionErrorPosition(),
                                        () =>
                                        errorMessage ?? ExpressionParserMessages.CustomPostfixIsInvalidError,
                                        ParseErrorItemCode.CustomPostfixExpressionItemRejectedByPrecedingCustomExpressionItem));

                                    addExpressionItem(customExpressionItem);
                                    return customExpressionItem;
                                }

                                postfixTargetExpressionItem = lastParsedCustomExpressionItem;
                            }
                            else if (lastParsedComplexExpressionItem != null)
                            {
                                if (!(lastParsedComplexExpressionItem is IBracesExpressionItem ||
                                      lastParsedComplexExpressionItem is ILiteralExpressionItem))
                                {
                                    parseExpressionItemData.OperatorsCannotBeEvaluated = true;
                                    context.AddParseErrorItem(new ParseErrorItem(
                                        GetCustomExpressionErrorPosition(),
                                        () => ExpressionParserMessages.CustomPostfixIsInvalidError,
                                        ParseErrorItemCode.CustomPostfixExpressionItemFollowsInvalidExpression));

                                    addExpressionItem(customExpressionItem);
                                    return customExpressionItem;
                                }

                                postfixTargetExpressionItem = lastParsedComplexExpressionItem;
                            }
                            else
                            {
                                // If we got here postfixTargetExpressionItem is null.
                                parseExpressionItemData.OperatorsCannotBeEvaluated = true;
                                context.AddParseErrorItem(new ParseErrorItem(
                                    GetCustomExpressionErrorPosition(),
                                    () => ExpressionParserMessages.CustomPostfixIsInvalidError,
                                    ParseErrorItemCode.CustomPostfixExpressionItemHasNoTargetExpression));

                                addExpressionItem(customExpressionItem);
                                return customExpressionItem;
                            }

                            postfixTargetExpressionItem.AddPostfix(customExpressionItem);
                            break;

                        case CustomExpressionItemCategory.Prefix:
                        case CustomExpressionItemCategory.Regular:

                            // Prefixes will be removed and added to prefix target later on, or an error will be reported, if there is no target
                            // after a prefix.
                            if (potentialPrefixExpressionItem != null)
                            {
                                parseExpressionItemData.AllExpressionItems.RemoveAt(parseExpressionItemData.AllExpressionItems.Count - 1);

                                List<IExpressionItemBase> customExpressionItemCurrentPrefixes = null;
                                if (customExpressionItem.Prefixes.Count > 0)
                                {
                                    customExpressionItemCurrentPrefixes = new List<IExpressionItemBase>(customExpressionItem.Prefixes);
                                    customExpressionItem.RemovePrefixes();
                                }

                                var customExpressionItemPrefixes = _parserHelper.ConvertLastExpressionItemToPrefixes(potentialPrefixExpressionItem);

                                if (customExpressionItemPrefixes.Count > 0)
                                {
                                    foreach (var prefixExpressionItem in customExpressionItemPrefixes)
                                        customExpressionItem.AddPrefix(prefixExpressionItem);

                                    expectedCustomExpressionItemIndex = customExpressionItemPrefixes[0].IndexInText;
                                }

                                if (customExpressionItemCurrentPrefixes != null)
                                {
                                    foreach (var prefixExpressionItem in customExpressionItemCurrentPrefixes)
                                        customExpressionItem.AddPrefix(prefixExpressionItem);
                                }
                            }
                            else if (lastParsedComplexExpressionItem != null)
                            {
                                // If we got here, there is no operator before current symbol, and there is a non-prefix expression
                                _errorsHelper.AddNoSeparationBetweenSymbolsError(context, parseExpressionItemData, expectedCustomExpressionItemIndex);
                            }

                            addExpressionItem(customExpressionItem);
                            break;

                        default:
                            parseExpressionItemData.OperatorsCannotBeEvaluated = true;
                            context.AddParseErrorItem(new ParseErrorItem(
                                GetCustomExpressionErrorPosition(), () =>
                                    $"Invalid value '{typeof(CustomExpressionItemCategory).FullName}.{customExpressionItem.CustomExpressionItemCategory}'",
                                ParseErrorItemCode.ParserImplementationError, true));

                            addExpressionItem(customExpressionItem);
                            return customExpressionItem;
                    }

                    if (customExpressionItem.IndexInText != expectedCustomExpressionItemIndex)
                        context.AddParseErrorItem(new ParseErrorItem(GetCustomExpressionErrorPosition(),
                            () => $"The value of '{nameof(ICustomExpressionItem.IndexInText)}' is invalid. The invalid value is {customExpressionItem.IndexInText}. The value should be {expectedCustomExpressionItemIndex}. {GetCustomExpressionImplementationErrorContext()}",
                            ParseErrorItemCode.ParsedCustomExpressionItemHasInvalidIndex, true));

                    customExpressionItem.OnCustomExpressionItemParsed(context);
                    return customExpressionItem;
                }

                // If customExpressionItem is null but errors were reported, lets not continue.
                if (context.NewlyAddedErrors.Count > 0)
                    return null;
            }

            return null;
        }
    }
}
