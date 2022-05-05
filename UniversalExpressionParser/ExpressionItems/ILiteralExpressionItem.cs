// Copyright (c) UniversalExpressionParser Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using JetBrains.Annotations;

namespace UniversalExpressionParser.ExpressionItems
{
    /// <summary>
    /// An expression item for expression items parsed from literal. For example in expression "var _x = f$(x1, x2) + m1[1, x3];"
    /// the following expressions will be parsed to <see cref="ILiteralExpressionItem"/>:
    /// - "_x" <br/>
    /// - "f$": will be the value of property <see cref="IBracesExpressionItem.NameLiteral"/> in <see cref="IBracesExpressionItem"/> parsed from function f$(x1, x2)<br/>
    /// - "x1" and "x2": will be included in <see cref="IBracesExpressionItem.Parameters"/> in  <see cref="IBracesExpressionItem"/> parsed from function f$(x1, x2)<br/>
    /// - "m1": will be the value of property <see cref="IBracesExpressionItem.NameLiteral"/> in <see cref="IBracesExpressionItem"/> parsed from matrix m1[1, x3]<br/>
    /// - "x3": will be included in <see cref="IBracesExpressionItem.Parameters"/> in  <see cref="IBracesExpressionItem"/> parsed from function f$(x1, x2)<br/>
    /// </summary>
    public interface ILiteralExpressionItem : IComplexExpressionItem
    {
        /// <summary>
        /// An expression item for the literal name.
        /// </summary>
        [NotNull]
        ILiteralNameExpressionItem LiteralName { get; }
    }
}