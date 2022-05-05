// Copyright (c) UniversalExpressionParser Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using UniversalExpressionParser.ExpressionItems;

namespace UniversalExpressionParser
{
    /// <summary>
    /// Info on commented out code block.
    /// </summary>
    public interface ICommentedTextData: ITextItem
    {
        /// <summary>
        /// If true, the comment is a line comment. Otherwise, it is a block comment.
        /// </summary>
        bool IsLineComment { get; }
    }
}