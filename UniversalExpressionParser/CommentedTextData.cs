// Copyright (c) UniversalExpressionParser Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

namespace UniversalExpressionParser
{
    /// <inheritdoc />
    public class CommentedTextData: ICommentedTextData
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="indexInText">Index of the code item in evaluated expression text.</param>
        /// <param name="commentLength">
        /// Normally this is the entire length of code item including children. Fore example
        /// the length for F1(x, y) will be 8 (includes closing parenthesis).
        /// However, if the expression item is not closed (say the closing parenthesis of function call is missing,
        /// the length will be smaller.</param>
        /// <param name="isLineComment">If true, the comment is a line comment. Otherwise, it is a block comment.</param>
        public CommentedTextData(int indexInText, int commentLength, bool isLineComment)
        {
            IndexInText = indexInText;
            ItemLength = commentLength;
            IsLineComment = isLineComment;
        }

        /// <inheritdoc />
        public bool IsLineComment { get; }

        /// <inheritdoc />
        public int IndexInText { get; }

        /// <inheritdoc />
        public int ItemLength { get; }
    }
}