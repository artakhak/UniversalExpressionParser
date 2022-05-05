// Copyright (c) UniversalExpressionParser Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

namespace UniversalExpressionParser.ExpressionItems
{
    /// <summary>
    /// An expression item parsed either from either an opening brace '(' or '['
    /// </summary>
    public interface IOpeningBraceExpressionItem : ITextExpressionItem
    {
        /// <summary>
        /// If the value is true, the expression was parsed from an opening round brace '('. Otherwise, the expression was parsed from an opening square brace '['.
        /// </summary>
        bool IsRoundBrace { get; }
    }

    /// <summary>
    /// A default implementation for <see cref="IOpeningBraceExpressionItem"/>
    /// </summary>
    public class OpeningBraceExpressionItem : NameExpressionItem, IOpeningBraceExpressionItem
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="isRoundBrace"> If the value is true, the expression was parsed from an opening round brace '('. Otherwise, the expression was parsed from an opening square brace '['.</param>
        /// <param name="indexInText">Index of the brace in parsed text.</param>
        public OpeningBraceExpressionItem(bool isRoundBrace, int indexInText) : base(isRoundBrace ? "(" : "[", indexInText)
        {
            IsRoundBrace = isRoundBrace;
        }

        /// <inheritdoc />
        public bool IsRoundBrace { get; }
    }
}