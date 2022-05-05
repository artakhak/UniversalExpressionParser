// Copyright (c) UniversalExpressionParser Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace UniversalExpressionParser.ExpressionItems.Custom
{
    /// <summary>
    /// Default implementation of <see cref="ICustomExpressionItemParser"/> that uses the last keyword in list of parsed keywords of type <see cref="IKeywordExpressionItem"/> to determine which custom parser of type <see cref="ICustomExpressionItemParserByKeywordId"/> to use to parse a custom expression item.
    /// </summary>
    public class AggregateCustomExpressionItemParser : ICustomExpressionItemParser
    {
        [NotNull]
        private readonly Dictionary<long, List<ICustomExpressionItemParserByKeywordId>> _parseKeywordToParersList = new Dictionary<long, List<ICustomExpressionItemParserByKeywordId>>();

        /// <summary>
        /// List of parsers of type <see cref="ICustomExpressionItemParserByKeywordId"/> that will be evaluated to determine which custom parser to use to parse a custom expression item.
        /// </summary>
        /// <param name="customExpressionItemParsers"></param>
        public AggregateCustomExpressionItemParser([NotNull, ItemNotNull] IEnumerable<ICustomExpressionItemParserByKeywordId> customExpressionItemParsers)
        {
            foreach (var customExpressionItemParser in customExpressionItemParsers)
            {
                if (!_parseKeywordToParersList.TryGetValue(customExpressionItemParser.ParsedKeywordId, out var keywordParsers))
                {
                    keywordParsers = new List<ICustomExpressionItemParserByKeywordId>(10);
                    _parseKeywordToParersList[customExpressionItemParser.ParsedKeywordId] = keywordParsers;
                }

                keywordParsers.Add(customExpressionItemParser);
            }
        }

        /// <inheritdoc />
        public ICustomExpressionItem TryParseCustomExpressionItem(IParseExpressionItemContext context,
            IReadOnlyList<IExpressionItemBase> parsedPrefixExpressionItems,
            IReadOnlyList<IKeywordExpressionItem> parsedKeywordExpressionItems)
        {
            if (parsedKeywordExpressionItems.Count == 0)
                return null;

            var lastKeywordExpressionItem = parsedKeywordExpressionItems[parsedKeywordExpressionItems.Count - 1];

            if (!_parseKeywordToParersList.TryGetValue(lastKeywordExpressionItem.LanguageKeywordInfo.Id, out var keywordParsers))
                return null;

            foreach (var keywordParser in keywordParsers)
            {
                var customExpressionItem = keywordParser.TryParseCustomExpressionItem(context, parsedPrefixExpressionItems,
                    parsedKeywordExpressionItems.Where(x => x != lastKeywordExpressionItem).ToList(),
                    lastKeywordExpressionItem);

                if (customExpressionItem != null)
                    return customExpressionItem;
            }

            return null;
        }
    }
}