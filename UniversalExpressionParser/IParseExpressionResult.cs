// Copyright (c) UniversalExpressionParser Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using System.Collections.Generic;
using JetBrains.Annotations;
using UniversalExpressionParser.ExpressionItems;

namespace UniversalExpressionParser
{
    /// <summary>
    /// Stores parsed expression data returned by methods in <see cref="IExpressionParser"/>.
    /// </summary>
    public interface IParseExpressionResult : ITextItem
    {
        /// <summary>
        /// Parsed expression.
        /// </summary>
        [CanBeNull]
        string ParsedExpression { get; }

        /// <summary>
        /// Contains parse errors.
        /// </summary>
        [NotNull]
        IParseErrorData ParseErrorData { get; }

        /// <summary>
        /// Position in <see cref="ParsedExpression"/> when parsing is complete. Normally, the value will be equal to the length of text <see cref="ParsedExpression"/>,
        /// however the value might be smaller, if parsing was stopped because of parse errors, or if partial parsing was done using methods 
        /// <see cref="IExpressionParser.ParseBracesExpression"/> or <see cref="IExpressionParser.ParseCodeBlockExpression"/>
        /// or if parsing was stopped by a check done by<see cref="IParseExpressionOptions.IsExpressionParsingComplete"/> (<see cref="IParseExpressionOptions"/> is passed as a parameter to parse methods in <see cref="IExpressionParser"/>).
        /// </summary>
        int PositionInTextOnCompletion { get; }

        /// <summary>
        /// Stores data on commented out text items. For example in expression "var x=/*block comments*/5; // this is line comment",
        /// the data on comments  in "/*block comments*/" and "/*block comments*/" will be stored in this list.
        /// Note comment data (i.e., position in text and length of comment) also include the comment start/end marks defined in <see cref="IExpressionLanguageProvider.LineCommentMarker"/>,
        /// <see cref="IExpressionLanguageProvider.MultilineCommentStartMarker"/> and <see cref="IExpressionLanguageProvider.MultilineCommentEndMarker"/>.
        /// </summary>
        [NotNull, ItemNotNull]
        IReadOnlyList<ICommentedTextData> SortedCommentedTextData { get; }

        /// <summary>
        /// Adds commented text data to <see cref="SortedCommentedTextData"/>.
        /// </summary>
        /// <param name="commentedTextData"></param>
        void AddCommentedTextData([NotNull] ICommentedTextData commentedTextData);

        /// <summary>
        /// The parser <see cref="IExpressionParser"/> executes this method to finalize the data in parse result when parsing is complete.
        /// </summary>
        /// <param name="parsedExpression">Parsed expression.</param>
        /// <param name="positionInTextOnCompletion">Position in <see cref="ParsedExpression"/> when parsing is complete.</param>
        void OnParsingComplete([NotNull] string parsedExpression, int positionInTextOnCompletion);

        /// <summary>
        /// Parsed top level expression item.
        /// </summary>
        [NotNull]
        IRootExpressionItem RootExpressionItem { get; }
    }
}