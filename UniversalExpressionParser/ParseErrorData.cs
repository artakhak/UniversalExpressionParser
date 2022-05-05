// Copyright (c) UniversalExpressionParser Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using JetBrains.Annotations;
using System.Collections.Generic;

namespace UniversalExpressionParser
{
    /// <inheritdoc />
    public class ParseErrorData : IParseErrorData
    {
        [NotNull]
        private readonly SortedList<int, LinkedList<IParseErrorItem>> _allSortedParseErrorItems = new SortedList<int, LinkedList<IParseErrorItem>>();

        [NotNull, ItemNotNull]
        private readonly List<IParseErrorItem> _allParseErrorItems = new List<IParseErrorItem>();

        /// <inheritdoc />
        public IReadOnlyList<IParseErrorItem> AllParseErrorItems => _allParseErrorItems;

        /// <inheritdoc />
        public IReadOnlyList<IParseErrorItem> GetAllSortedParseErrorItems()
        {
            var sortedErrors = new List<IParseErrorItem>();

            foreach (var errorsAtPosition in _allSortedParseErrorItems.Values)
                sortedErrors.AddRange(errorsAtPosition);

            return sortedErrors;
        }

        /// <inheritdoc />
        public IReadOnlyList<IParseErrorItem> GetAllSortedParseErrorItems(int errorPositionStart, int errorPositionEnd)
        {
            // TODO: Use binary search algorithm to find the first index 
            var sortedErrors = new List<IParseErrorItem>();

            foreach (var errorsAtPosition in _allSortedParseErrorItems.Values)
            {
                if (errorsAtPosition.First.Value.ErrorIndexInParsedText < errorPositionStart)
                    continue;

                if (errorsAtPosition.First.Value.ErrorIndexInParsedText > errorPositionEnd)
                    break;

                sortedErrors.AddRange(errorsAtPosition);
            }

            return sortedErrors;
        }

        /// <inheritdoc />
        public IReadOnlyList<IParseErrorItem> GetAllSortedParseErrorItems(int errorPosition)
        {
            return _allSortedParseErrorItems.TryGetValue(errorPosition, out var errors)
                ? new List<IParseErrorItem>(errors)
                : new List<IParseErrorItem>(0);
        }

        /// <inheritdoc />
        public bool HasCriticalErrors { get; internal set; }

        /// <summary>
        /// Adds an error.
        /// </summary>
        /// <param name="parseErrorItem">Error data to add.</param>
        internal void AddParseErrorItem([NotNull] IParseErrorItem parseErrorItem)
        {
            _allParseErrorItems.Add(parseErrorItem);

            if (!_allSortedParseErrorItems.TryGetValue(parseErrorItem.ErrorIndexInParsedText, out var sortedParseErrorItem))
            {
                sortedParseErrorItem = new LinkedList<IParseErrorItem>();
                _allSortedParseErrorItems[parseErrorItem.ErrorIndexInParsedText] = sortedParseErrorItem;
            }

            sortedParseErrorItem.AddLast(parseErrorItem);
        }
    }
}