// Copyright (c) UniversalExpressionParser Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using System;

namespace UniversalExpressionParser.Parser
{
    [Flags]
    internal enum OperatorTypeFlags
    {
        /// <summary>
        /// No flags
        /// </summary>
        None = 0,

        /// <summary>
        /// Binary operator. Example: x+y.
        /// </summary>
        BinaryOperator = OperatorType.BinaryOperator,

        /// <summary>
        /// Unary prefix operator applied placed the operand. Example ++x
        /// </summary>
        PrefixUnaryOperator = OperatorType.PrefixUnaryOperator,

        /// <summary>
        /// Unary postfix operator placed after the operand. Example x++
        /// </summary>
        PostfixUnaryOperator = OperatorType.PostfixUnaryOperator
    }
}