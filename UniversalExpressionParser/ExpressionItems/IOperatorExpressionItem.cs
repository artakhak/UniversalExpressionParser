// Copyright (c) UniversalExpressionParser Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using JetBrains.Annotations;

namespace UniversalExpressionParser.ExpressionItems
{
    /// <summary>
    /// Expression item parsed from operators expression.
    /// For example if <see cref="IExpressionLanguageProvider.Operators"/> defines operators "*" with <see cref="IOperatorInfo.Priority"/> equal to 1 and operator
    /// and operator "+" with <see cref="IOperatorInfo.Priority"/> equal to 2 (operators with lower value of <see cref="IOperatorInfo.Priority"/> are applied first).
    /// In this case the expression x+y*f() will be parsed to an instance of <see cref="IOperatorExpressionItem"/> for binary operator "+"
    /// with <see cref="IOperatorExpressionItem.Operand1"/> being an expression of type <see cref="ILiteralExpressionItem"/> parsed from "x" and
    /// <see cref="IOperatorExpressionItem.Operand2"/> being an expression of type <see cref="IOperatorExpressionItem"/> for binary operator "y*f()".
    /// The instance <see cref="IOperatorExpressionItem"/> in <see cref="IOperatorExpressionItem.Operand2"/> parsed from "y*f()" will have two operands.
    /// The first operand will be an instance of <see cref="ILiteralExpressionItem"/> for an expression item parsed from "y", and the second operand (property <see cref="Operand2"/>) will
    /// be an instance of <see cref="IBracesExpressionItem"/> for an expression item parsed from "f()".
    /// Therefore, the expression "x+y*f()" can be represented using this tree:<br/>
    ///      +              <br/>
    ///     / \             <br/>
    ///    x   *            <br/>
    ///       / \           <br/>
    ///      y   f()        <br/>
    /// Other examples of expressions that are parsed to <see cref="IOperatorInfoExpressionItem"/> are:
    /// "var x=!y". In this example "!" is a unary prefix operator applied to "y".
    /// "x=y++". In this example "++" is a unary postfix operator applied to "y".
    /// </summary>
    public interface IOperatorExpressionItem : IComplexExpressionItem
    {
        /// <summary>
        /// Expression item for operator. For example in x+y, + will be parsed to <see cref="IOperatorInfoExpressionItem"/>.
        /// On the other hand in expression ++x, ++ will be parsed to <see cref="IOperatorInfoExpressionItem"/>.
        /// </summary>
        [NotNull]
        IOperatorInfoExpressionItem OperatorInfoExpressionItem { get; }

        /// <summary>
        /// Operand 1. The value cannot be null
        /// </summary>
        [NotNull]
        IExpressionItemBase Operand1 { get; }

        /// <summary>
        /// Operand 2. The value is null if either the operator is unary operator, or if the second operator failed to parse.
        /// </summary>
        [CanBeNull]
        IExpressionItemBase Operand2 { get; }
    }
}