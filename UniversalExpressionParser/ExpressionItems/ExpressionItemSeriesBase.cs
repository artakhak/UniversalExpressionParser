// Copyright (c) UniversalExpressionParser Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using System.Collections.Generic;
using JetBrains.Annotations;

namespace UniversalExpressionParser.ExpressionItems
{
    /// <summary>
    /// Base class for expressions parsed sequence of multiple expression items.
    /// Example subclasses are <see cref="CodeBlockExpressionItem"/> (an implementation of <see cref="ICodeBlockExpressionItem"/>) are code blocks expression, as well as
    /// <see cref="RootExpressionItem"/>  (an implementation of <see cref="IRootExpressionItem"/>).
    /// </summary>
    public abstract class ExpressionItemSeriesBase : ComplexExpressionItemBase, ICanAddChildExpressionItem, ICanAddSeparatorCharacterExpressionItem
    {
        /// <inheritdoc />
        protected ExpressionItemSeriesBase([NotNull, ItemNotNull] IEnumerable<IExpressionItemBase> prefixExpressionItems,
                                           [NotNull, ItemNotNull] IEnumerable<IKeywordExpressionItem> keywordExpressionItems) : base(prefixExpressionItems, keywordExpressionItems)
        {
            
        }

        /// <inheritdoc />
        public void AddChildExpressionItem(IExpressionItemBase childExpressionItem)
        {
            AddChild(childExpressionItem);
        }

        /// <inheritdoc />
        public void AddSeparatorCharacterExpressionItem(ISeparatorCharacterExpressionItem separatorCharacterExpressionItem)
        {
            AddRegularItem(separatorCharacterExpressionItem);
        }
    }
}