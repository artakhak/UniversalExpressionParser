// Copyright (c) UniversalExpressionParser Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using System.Collections.Generic;
using System.Text.RegularExpressions;
using JetBrains.Annotations;
using UniversalExpressionParser.ExpressionItems;

namespace UniversalExpressionParser.Parser
{
    internal class ParseConstantNumericValueHelper
    {
        [NotNull] private readonly ParserHelper _parserHelper;

        internal ParseConstantNumericValueHelper([NotNull] ParserHelper parserHelper)
        {
            _parserHelper = parserHelper;
        }

        [CanBeNull]
        internal INumericExpressionItem TryParseConstantNumericValueExpressionItem([NotNull] ParseExpressionItemContext context,
                                                                  [CanBeNull] IComplexExpressionItem potentialPrefixExpressionItem,
                                                                  [NotNull][ItemNotNull] IEnumerable<IKeywordExpressionItem> keywordExpressionItems)
        {
            var textSymbolsParser = context.TextSymbolsParser;
            var expressionLanguageProvider = context.ExpressionLanguageProviderWrapper.ExpressionLanguageProvider;
            foreach (var numericTypeDescriptor in expressionLanguageProvider.NumericTypeDescriptors)
            {
                for (var regexInd = 0; regexInd < numericTypeDescriptor.RegularExpressions.Count; ++regexInd)
                {
                    var regularExpression = numericTypeDescriptor.RegularExpressions[regexInd];

                    var regex = new Regex(regularExpression, RegexOptions.Compiled);

                    var matches = regex.Match(textSymbolsParser.TextToParse, textSymbolsParser.PositionInText, textSymbolsParser.ParsedTextEnd - textSymbolsParser.PositionInText);

                    if (matches.Success)
                    {
                        if (matches.Index == textSymbolsParser.PositionInText)
                        {
                            var numericValueExpressionItem = new NumericExpressionItem(
                                _parserHelper.ConvertLastExpressionItemToPrefixes(potentialPrefixExpressionItem), keywordExpressionItems,
                                new NumericExpressionValueItem(matches.Value, matches.Index),
                                numericTypeDescriptor, regexInd);

                            textSymbolsParser.SkipCharacters(matches.Value.Length);
                            return numericValueExpressionItem;
                        }

                        context.AddParseErrorItem(new ParseErrorItem(
                            textSymbolsParser.PositionInText,
                            () => $"Regular expression in '{typeof(NumericTypeDescriptor).FullName}.{nameof(NumericTypeDescriptor.RegularExpressions)}' should either not match any value, or the matched value should be at the position, where matching started.",
                            ParseErrorItemCode.InvalidRegularExpressionForNumericValueParsing));
                    }
                }
            }

            return null;
        }
    }
}
