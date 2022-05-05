// Copyright (c) UniversalExpressionParser Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using System.Collections.Generic;
using JetBrains.Annotations;

namespace UniversalExpressionParser.ExpressionItems.Custom
{
    /// <summary>
    /// Default implementation of <see cref="IKeywordBasedCustomExpressionItem"/>
    /// </summary>
    public class KeywordBasedCustomExpressionItem : CustomExpressionItem, IKeywordBasedCustomExpressionItem
    {
        /// <inheritdoc />
        public IKeywordExpressionItem LastKeywordExpressionItem { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="parsedPrefixExpressionItems">Parsed prefixes not assigned to any expression item yet.</param>
        /// <param name="parsedKeywordExpressionItemsWithoutLastKeyword">Parsed keywords excluding the last keyword not assigned to any expression item yet.</param>
        /// <param name="lastKeywordExpressionItem">The last parsed keyword in the lit of keywords not assigned to any expression item yet.</param>
        /// <param name="customExpressionItemCategory">Custom expression item categoty.</param>
        public KeywordBasedCustomExpressionItem([NotNull, ItemNotNull] IEnumerable<IExpressionItemBase> parsedPrefixExpressionItems,
            [NotNull, ItemNotNull] IEnumerable<IKeywordExpressionItem> parsedKeywordExpressionItemsWithoutLastKeyword,
            [NotNull] IKeywordExpressionItem lastKeywordExpressionItem,
            CustomExpressionItemCategory customExpressionItemCategory) :
            base(parsedPrefixExpressionItems, parsedKeywordExpressionItemsWithoutLastKeyword, customExpressionItemCategory)
        {
            LastKeywordExpressionItem = lastKeywordExpressionItem;
            AddRegularItem(lastKeywordExpressionItem);
        }

        /// <inheritdoc />
        public override int? ErrorsPositionDisplayValue => LastKeywordExpressionItem.IndexInText;
    }
}