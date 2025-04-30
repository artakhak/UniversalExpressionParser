// Copyright (c) UniversalExpressionParser Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using System.Collections.Generic;
using JetBrains.Annotations;
using UniversalExpressionParser.ExpressionItems;

namespace UniversalExpressionParser.Parser
{
    internal class ParseKeywordsHelper
    {
        [NotNull, ItemNotNull]
        internal List<IKeywordExpressionItem> TryParseKeywords([NotNull] ParseExpressionItemContext context)
        {
            var keywordExpressionItems = new List<IKeywordExpressionItem>(10);
            var textSymbolsParser = context.TextSymbolsParser;

            var allParsedKeywords = new Dictionary<long, IKeywordExpressionItem>();
            while (Helpers.TryParseComments(context))
            {
                var keywordFoundInCurrentPass = false;

                foreach (var keywordInfo in context.ExpressionLanguageProviderWrapper.SortedKeywordInfos)
                {
                    if (Helpers.StartsWithSymbol(textSymbolsParser.TextToParse, keywordInfo.Keyword,
                        textSymbolsParser.PositionInText, textSymbolsParser.ParsedTextEnd,
                        context.ExpressionLanguageProviderWrapper.ExpressionLanguageProvider, null, out var matchedText))
                    {
                        keywordFoundInCurrentPass = true;

                        if (allParsedKeywords.ContainsKey(keywordInfo.Id))
                        {
                            context.AddParseErrorItem(new ParseErrorItem(
                                textSymbolsParser.PositionInText, () => $"Multiple occurrences of keyword '{keywordInfo.Keyword}'.", ParseErrorItemCode.MultipleOccurrencesOfKeyword));
                        }
                        else
                        {
                            KeywordExpressionItem keywordExpressionItem = new KeywordExpressionItem(keywordInfo, matchedText, textSymbolsParser.PositionInText);
                            keywordExpressionItems.Add(keywordExpressionItem);
                            allParsedKeywords[keywordInfo.Id] = keywordExpressionItem;
                        }

                        textSymbolsParser.SkipCharacters(keywordInfo.Keyword.Length);

                        if (!textSymbolsParser.SkipSpaces())
                            break;
                    }
                }

                if (!keywordFoundInCurrentPass)
                    break;
            }

            return keywordExpressionItems;
        }
    }
}
