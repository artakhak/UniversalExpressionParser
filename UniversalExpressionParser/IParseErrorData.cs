// Copyright (c) UniversalExpressionParser Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using System.Collections.Generic;
using JetBrains.Annotations;

namespace UniversalExpressionParser
{
    /// <summary>
    /// Parse error data.
    /// </summary>
    public interface IParseErrorData
    {
        /// <summary>
        /// List of all errors per listed in the order the errors were added by the parser <see cref="IExpressionParser"/>. In other words the errors are not sorted.
        /// Call <see cref="GetAllSortedParseErrorItems()"/> to get the list of all errors sorted by error position <see cref="IParseErrorItem.ErrorIndexInParsedText"/>.
        /// </summary>
        [NotNull, ItemNotNull]
        IReadOnlyList<IParseErrorItem> AllParseErrorItems { get; }

        /// <summary>
        /// List of errors per for code items sorted by the value of <see cref="IParseErrorItem.ErrorIndexInParsedText"/>.
        /// </summary>
        [NotNull, ItemNotNull]
        IReadOnlyList<IParseErrorItem> GetAllSortedParseErrorItems();

        /// <summary>
        /// Returns collection of errors sorted by the value of <see cref="IParseErrorItem.ErrorIndexInParsedText"/> for which
        /// <see cref="IParseErrorItem.ErrorIndexInParsedText"/> is between <paramref name="errorPositionStart"/> and <paramref name="errorPositionEnd"/>
        /// inclusively.
        /// </summary>
        [NotNull, ItemNotNull]
        IReadOnlyList<IParseErrorItem> GetAllSortedParseErrorItems(int errorPositionStart, int errorPositionEnd);

        /// <summary>
        /// Returns collection of errors sorted by the value of <see cref="IParseErrorItem.ErrorIndexInParsedText"/>
        /// for which <see cref="IParseErrorItem.ErrorIndexInParsedText"/> is equal to <paramref name="errorPosition"/>.
        /// </summary>
        [NotNull, ItemNotNull]
        IReadOnlyList<IParseErrorItem> GetAllSortedParseErrorItems(int errorPosition);

        /// <summary>
        /// If true, has critical errors that result in parser stopping.
        /// </summary>
        bool HasCriticalErrors { get; }
    }
}
