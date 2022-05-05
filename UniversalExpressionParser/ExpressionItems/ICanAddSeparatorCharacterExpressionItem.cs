// Copyright (c) UniversalExpressionParser Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using JetBrains.Annotations;

namespace UniversalExpressionParser.ExpressionItems
{
    /// <summary>
    /// A sub-class of <see cref="IComplexExpressionItem"/> that allows adding expression item
    /// <see cref="ISeparatorCharacterExpressionItem"/> to <see cref="IComplexExpressionItem.RegularItems"/>.
    /// This interface can be implemented by expression items that are parsed from code block expressions (e.g., expression like "{var x=15;}"), such as
    /// <see cref="ICodeBlockExpressionItem"/>, or root expression item that can have multiple expressions separated by expression characters,
    /// such as <see cref="IRootExpressionItem"/>.
    /// </summary>
    public interface ICanAddSeparatorCharacterExpressionItem : IComplexExpressionItem
    {
        /// <summary>
        /// Adds <see cref="ISeparatorCharacterExpressionItem"/> in parameter <paramref name="separatorCharacterExpressionItem"/> to <see cref="IComplexExpressionItem.RegularItems"/>.
        /// </summary>
        /// <param name="separatorCharacterExpressionItem">Separator character expression item to add.</param>
        void AddSeparatorCharacterExpressionItem([NotNull] ISeparatorCharacterExpressionItem separatorCharacterExpressionItem);
    }
}