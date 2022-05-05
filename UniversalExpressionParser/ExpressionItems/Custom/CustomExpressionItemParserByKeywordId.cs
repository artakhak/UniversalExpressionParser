// Copyright (c) UniversalExpressionParser Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using System.Collections.Generic;
using JetBrains.Annotations;

namespace UniversalExpressionParser.ExpressionItems.Custom
{
    /// <inheritdoc />
    public abstract class CustomExpressionItemParserByKeywordId: ICustomExpressionItemParserByKeywordId
    {
        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="parsedKeywordId">Keyword Id that the custom expression item parser registers to handle. This value is used by <see cref="AggregateCustomExpressionItemParser"/> to initialize a dictionary for mapping of keyword Ids to instances of <see cref="ICustomExpressionItemParserByKeywordId"/> that handle the keyword for optimization purposes.</param>
        protected CustomExpressionItemParserByKeywordId(long parsedKeywordId)
        {
            ParsedKeywordId = parsedKeywordId;
        }

        /// <inheritdoc />
        public long ParsedKeywordId { get; }

        /// <inheritdoc />
        public ICustomExpressionItem TryParseCustomExpressionItem(IParseExpressionItemContext context, 
            IReadOnlyList<IExpressionItemBase> parsedPrefixExpressionItems, 
            IReadOnlyList<IKeywordExpressionItem> parsedKeywordExpressionItemsWithoutLastKeyword, 
            IKeywordExpressionItem lastKeywordExpressionItem)
        {
            if (lastKeywordExpressionItem.LanguageKeywordInfo.Id != ParsedKeywordId)
                return null;

            return DoParseCustomExpressionItem(context, parsedPrefixExpressionItems, parsedKeywordExpressionItemsWithoutLastKeyword, lastKeywordExpressionItem);
        }

        /// <summary>
        /// Tries to parse a keyword in <paramref name="lastKeywordExpressionItem"/> or any text at current position into an instance of
        /// <see cref="ICustomExpressionItem"/>.
        /// Returns null, if the expression at current position cannot be parsed to <see cref="ICustomExpressionItem"/>.
        /// Note, the value of property <see cref="IKeywordExpressionItem.LanguageKeywordInfo"/>.Id in <paramref name="lastKeywordExpressionItem"/> is
        /// expected to be equal to <see cref="ParsedKeywordId"/> when this method is called.
        /// </summary>
        /// <param name="context">Stores context data at current position.
        /// use the helpers in <see cref="IParseExpressionItemContext"/> (in parameter <paramref name="context"/>), such as <see cref="IParseExpressionItemContext.ParseCodeBlockExpression"/>, <see cref="IParseExpressionItemContext.ParseBracesExpression"/>,
        /// <see cref="IParseExpressionItemContext.TryParseSymbol"/>, <see cref="IParseExpressionItemContext.SkipSpacesAndComments()"/> and others to parse text current position.
        /// See at examples in projects "UniversalExpressionParser.Tests" and "UniversalExpressionParser.DemoExpressionLanguageProviders" in repository https://github.com/artakhak/UniversalExpressionParser for examples
        /// </param>
        /// <param name="parsedPrefixExpressionItems">Prefixes before the keywords.</param>
        /// <param name="parsedKeywordExpressionItemsWithoutLastKeyword">All the keywords preceding the keyword in parameter <paramref name="lastKeywordExpressionItem"/>.</param>
        /// <param name="lastKeywordExpressionItem">Last keyword in list of keywords parsed at current position.</param>
        [CanBeNull]
        protected abstract ICustomExpressionItem DoParseCustomExpressionItem([NotNull] IParseExpressionItemContext context,
            [NotNull, ItemNotNull] IReadOnlyList<IExpressionItemBase> parsedPrefixExpressionItems,
            [NotNull, ItemNotNull] IReadOnlyList<IKeywordExpressionItem> parsedKeywordExpressionItemsWithoutLastKeyword,
            [NotNull] IKeywordExpressionItem lastKeywordExpressionItem);
    }
}