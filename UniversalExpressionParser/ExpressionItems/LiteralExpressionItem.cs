// Copyright (c) UniversalExpressionParser Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using System.Collections.Generic;
using JetBrains.Annotations;

namespace UniversalExpressionParser.ExpressionItems
{
    /// <summary>
    /// Default implementation of <see cref="ILiteralExpressionItem"/>.
    /// </summary>
    public class LiteralExpressionItem : ComplexExpressionItemBase, ILiteralExpressionItem

    {
        /// <inheritdoc />
        public LiteralExpressionItem([NotNull] [ItemNotNull] IEnumerable<IExpressionItemBase> prefixExpressionItems, [NotNull] [ItemNotNull] IEnumerable<IKeywordExpressionItem> keywordExpressionItems, [NotNull] ILiteralNameExpressionItem literalName) : 
            base(prefixExpressionItems, keywordExpressionItems)
        {
            LiteralName = literalName;
            AddRegularItem(LiteralName);
        }

        /// <inheritdoc />
        public ILiteralNameExpressionItem LiteralName  { get; }

        /// <inheritdoc />
        public override string ToString()
        {
            return $" {this.GetType().FullName}, {nameof(LiteralName)}:{LiteralName}, {nameof(IndexInText)}:{IndexInText}";
        }
    }
}