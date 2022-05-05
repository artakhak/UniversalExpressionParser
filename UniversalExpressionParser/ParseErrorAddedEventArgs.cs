// Copyright (c) UniversalExpressionParser Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

namespace UniversalExpressionParser
{
    /// <summary>
    /// Event arguments for an event that <see cref="IParseErrorItem"/> was added when parsing an expression.
    /// </summary>
    public class ParseErrorAddedEventArgs
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="parseErrorItem">Added parse error.</param>
        public ParseErrorAddedEventArgs(IParseErrorItem parseErrorItem)
        {
            ParseErrorItem = parseErrorItem;
        }

        /// <summary>
        /// Added parse error.
        /// </summary>
        public IParseErrorItem ParseErrorItem { get; }
    }
}