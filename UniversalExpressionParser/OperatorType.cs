// Copyright (c) UniversalExpressionParser Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

namespace UniversalExpressionParser
{
    // Documented.
    /// <summary>
    /// Operator types.
    /// </summary>
    public enum OperatorType
    {
        /// <summary>
        /// Binary operator. Example: x+y.
        /// </summary>
        BinaryOperator = 1,

        /// <summary>
        /// Unary prefix operator applied placed the operand. Example ++x
        /// </summary>
        PrefixUnaryOperator = 2,

        /// <summary>
        /// Unary postfix operator placed after the operand. Example x++
        /// </summary>
        PostfixUnaryOperator = 4
    }
}