// Copyright (c) UniversalExpressionParser Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using System.Collections.Generic;
using JetBrains.Annotations;

namespace UniversalExpressionParser.ExpressionItems
{
    /// <inheritdoc />
    public class ParseExpressionResult : IParseExpressionResult
    {
        private int _indexInText = -1;
        private int _itemLength;

        [NotNull, ItemNotNull]
        private readonly List<ICommentedTextData> _sortedCommentedTextData = new List<ICommentedTextData>();

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="rootRootExpressionItem">Parsed top level expression item.</param>
        /// <param name="parseErrorData">Contains parse errors.</param>
        public ParseExpressionResult([NotNull] IRootExpressionItem rootRootExpressionItem, [NotNull] IParseErrorData parseErrorData)
        {
            RootExpressionItem = rootRootExpressionItem;
            ParseErrorData = parseErrorData;
        }

        /// <inheritdoc />
        public int IndexInText => _indexInText;

        /// <inheritdoc />
        public int ItemLength => _itemLength;

        /// <inheritdoc />
        public string ParsedExpression { get; private set; }

        /// <inheritdoc />
        public IRootExpressionItem RootExpressionItem { get; }

        /// <inheritdoc />
        public IParseErrorData ParseErrorData { get; }

        /// <inheritdoc />
        public int PositionInTextOnCompletion { get; private set; }

        /// <inheritdoc />
        public IReadOnlyList<ICommentedTextData> SortedCommentedTextData => _sortedCommentedTextData;

        /// <inheritdoc />
        public void AddCommentedTextData(ICommentedTextData commentedTextData)
        {
            Helpers.AddItemSorted(_sortedCommentedTextData, commentedTextData);
        }

        /// <inheritdoc />
        public void OnParsingComplete(string parsedExpression, int positionInTextOnCompletion)
        {
            _indexInText = this.GetIndexInText();
            _itemLength = this.GetItemLength();
            ParsedExpression = parsedExpression;
            PositionInTextOnCompletion = positionInTextOnCompletion;
        }

        /// <summary>
        /// This method is added for tests only. Not the best, but quick way to do this. Might be changed later. 
        /// </summary>
        internal void InitializeForTest([NotNull] string parsedCode, int indexInText, int itemLength, int positionInTextOnCompletion)
        {
            ParsedExpression = parsedCode;
            _indexInText = indexInText;
            _itemLength = itemLength;
            PositionInTextOnCompletion = positionInTextOnCompletion;
        }
    }
}