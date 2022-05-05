// Copyright (c) UniversalExpressionParser Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using JetBrains.Annotations;

namespace UniversalExpressionParser.ExpressionItems
{
    /// <summary>
    /// Base interface for parsed expression items. Subclasses of this interface include interfaces that store data for expression item parts,
    /// say interface <see cref="ITextExpressionItem"/> that stores data for brace '(' in expression "F(x, y)", as well as interfaces that store data for expression that consists other expressions,.
    /// For example <see cref="IBracesExpressionItem"/> stores data for braces expression "F(x, y)" which consists of function name in property <see cref="IBracesExpressionItem.NameLiteral"/> as well as of data about braces and parameters in properties <see cref="IBracesExpressionItem.OpeningBrace"/>, <see cref="IBracesExpressionItem.ClosingBrace"/>, <see cref="IBracesExpressionItem.Parameters"/>.
    /// Normally, more complex expressions are stored in a subclass <see cref="IComplexExpressionItem"/> (or one of its subclasses, such a <see cref="IBracesExpressionItem"/>), and expression parts, such as function name, comma, opening brace, closing brace in complex expression <see cref="IBracesExpressionItem"/> are stored in <see cref="ITextExpressionItem"/> (the default implementation is <see cref="NameExpressionItem"/>).
    /// </summary>
    public interface IExpressionItemBase : ITextItem
    {
        /// <summary>
        /// Unique identifier.
        /// </summary>
        long Id { get; }

        /// <summary>
        /// Parent expression item.
        /// </summary>
        [CanBeNull]
        IComplexExpressionItem Parent { get; set; }
    }
}