// Copyright (c) UniversalExpressionParser Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

namespace UniversalExpressionParser.ExpressionItems
{
    /// <summary>
    /// An expression item parsed either from either a closing brace ')' or ']'
    /// </summary>
    public interface IClosingBraceExpressionItem : ITextExpressionItem
    {
        /// <summary>
        /// If the value is true, the expression was parsed from a closing round brace ')'. Otherwise, the expression was parsed from a closing square brace ']'.
        /// </summary>
        bool IsRoundBrace { get; }
    }

    /// <summary>
    /// A default implementation for <see cref="IOpeningBraceExpressionItem"/>
    /// </summary>
    public class ClosingBraceExpressionItem : NameExpressionItem, IClosingBraceExpressionItem
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="isRoundBrace">If the value is true, the expression was parsed from a closing round brace ')'. Otherwise, the expression was parsed from a closing square brace ']'.</param>
        /// <param name="indexInText">Index of the brace in parsed text.</param>
        public ClosingBraceExpressionItem(bool isRoundBrace, int indexInText) : base(isRoundBrace ? ")" : "]", indexInText)
        {
            IsRoundBrace = isRoundBrace;
        }

        /// <inheritdoc />
        public bool IsRoundBrace { get; }
    }
}