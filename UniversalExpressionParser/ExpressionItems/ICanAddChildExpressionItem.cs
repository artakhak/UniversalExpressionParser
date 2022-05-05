// Copyright (c) UniversalExpressionParser Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using JetBrains.Annotations;

namespace UniversalExpressionParser.ExpressionItems
{
    /// <summary>
    /// A sub-class of <see cref="IComplexExpressionItem"/> that allows adding child expression items to <see cref="IComplexExpressionItem.Children"/>.
    /// </summary>
    public interface ICanAddChildExpressionItem: IComplexExpressionItem
    {
        /// <summary>
        /// Adds a child expression item to property <see cref="IComplexExpressionItem.Children"/> in <paramref name="childExpressionItem"/>.
        /// </summary>
        /// <param name="childExpressionItem">Child expression item to add.</param>
        void AddChildExpressionItem([NotNull] IExpressionItemBase childExpressionItem);
    }
}