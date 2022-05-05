// Copyright (c) UniversalExpressionParser Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using JetBrains.Annotations;

namespace UniversalExpressionParser.ExpressionItems
{
    /// <summary>
    /// Simple expression item for storing text items that are part of other expression items. For example <see cref="IBracesExpressionItem"/> parsed
    /// from "f1(x1, x2)" has items of this type in <see cref="IComplexExpressionItem.RegularItems"/> for opening brace "(", comma ",",
    /// and closing brace ")" (i.e., expression items of types <see cref="IOpeningBraceExpressionItem"/>, <see cref="ICommaExpressionItem"/>,
    /// <see cref="IClosingBraceExpressionItem"/> that extend <see cref="ITextExpressionItem"/>).
    /// Also, in this expression "f1", "x1", and "x2" are parsed to <see cref="ILiteralExpressionItem"/> which has a property <see cref="IComplexExpressionItem.RegularItems"/>
    /// that stores items of type <see cref="ITextExpressionItem"/> for these
    /// literals (i.e., instances of <see cref="ILiteralNameExpressionItem"/> that extend <see cref="ITextExpressionItem"/>).
    /// </summary>
    public interface ITextExpressionItem: IExpressionItemBase
    {
        /// <summary>
        /// Examples of values of this property are "x1", "AND", "OR", ",", "(", ")", etc.
        /// </summary>
        [NotNull]
        string Text { get; }
    }
}