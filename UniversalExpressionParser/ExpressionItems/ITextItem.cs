// Copyright (c) UniversalExpressionParser Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

namespace UniversalExpressionParser.ExpressionItems
{
    /// <summary>
    /// Stores data for an expression item parsed from text.
    /// </summary>
    public interface ITextItem
    {
        /// <summary>
        /// Index of the expression item in parsed expression text.
        /// </summary>
        int IndexInText { get; }

        /// <summary>
        /// Normally this is the entire length of the expression item including children. Fore example
        /// the length for "F1(x, y)" will be 8 (includes closing parenthesis).
        /// However, if the expression item has errors, and th expression was not fully parsed,
        /// the length will be  than the entire expression length.
        /// </summary>
        int ItemLength { get; }
    }
}