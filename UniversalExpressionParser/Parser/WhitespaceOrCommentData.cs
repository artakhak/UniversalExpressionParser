// Copyright (c) UniversalExpressionParser Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

namespace UniversalExpressionParser.Parser
{
    internal class WhitespaceOrCommentData
    {
        internal WhitespaceOrCommentData(int positionInText, int length)
        {
            PositionInText = positionInText;
            Length = length;
        }

        internal int PositionInText { get; }
        internal int Length { get; }
    }
}