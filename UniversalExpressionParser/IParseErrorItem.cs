// Copyright (c) UniversalExpressionParser Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using JetBrains.Annotations;
using UniversalExpressionParser.ExpressionItems.Custom;

namespace UniversalExpressionParser
{
    // Documented
    /// <summary>
    /// Stores data on single parse error item.
    /// </summary>
    public interface IParseErrorItem
    {
        /// <summary>
        /// Error index in evaluated text.
        /// </summary>
        int ErrorIndexInParsedText { get; }

        /// <summary>
        /// Error message.
        /// </summary>
        [NotNull]
        string ErrorMessage { get; }

        /// <summary>
        /// Parse error code. Look at <see cref="UniversalExpressionParser.ParseErrorItemCode"/> for predefined error codes. Other custom values can be used as well.
        /// </summary>
        int ParseErrorItemCode { get; }

        /// <summary>
        /// If the value, parsing will stop after this error is added by the custom expression parser <see cref="ICustomExpressionItemParser"/>,
        /// parsing will stop on error and rest of the expression will not be parsed ny <see cref="IExpressionParser"/>.
        /// Note, the same error code might be considered as critical error in one context, and non-critical in some other context.
        /// </summary>
        bool IsCriticalError { get; }
    }
}