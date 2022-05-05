// Copyright (c) UniversalExpressionParser Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using JetBrains.Annotations;

namespace UniversalExpressionParser
{
    // Documented.
    /// <summary>
    /// Provides options to use when parsing an expression using <see cref="IExpressionParser"/>.
    /// </summary>
    public interface IParseExpressionOptions
    {
        /// <summary>
        /// Start position to start parsing at. Default to 0. Non-zero value can be used to parse portion of text.
        /// </summary>
        int StartIndex { get; }

        /// <summary>
        /// A delegate that checks if parsing is complete. If the value is not null, the parser <see cref="IExpressionParser"/> will execute this
        /// to check if parsing should stop. For example <see cref="IsExpressionParsingComplete"/> can check if the text at current position starts with some special symbol (sey text "$parseend$"), that
        /// marks the end of parsed text.
        /// </summary>
        [CanBeNull] 
        IsParsingCompleteDelegate IsExpressionParsingComplete { get; }
    }

    /// <inheritdoc />
    public class ParseExpressionOptions : IParseExpressionOptions
    {
        /// <inheritdoc />
        public int StartIndex { get; set; } = 0;

        /// <inheritdoc />
        public IsParsingCompleteDelegate IsExpressionParsingComplete { get; set; }
    }
}