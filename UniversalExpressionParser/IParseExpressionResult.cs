// Copyright (c) UniversalExpressionParser Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using System;
using System.Collections.Generic;
using System.Text;
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

    /// <summary>
    /// Extension methods for <see cref="IParseExpressionResult"/>.
    /// </summary>
    public static class ParseExpressionResultExtensionMethods
    {
        private static void ValidateTextItem([NotNull] ITextItem textItem)
        {
            if (textItem.IndexInText < 0)
                throw new ArgumentException($"The value '{nameof(ITextItem.IndexInText)}' cannot be negative.");

            if (textItem.ItemLength < 0)
                throw new ArgumentException($"The value '{nameof(ITextItem.ItemLength)}' should be positive.");
        }

        /// <summary>
        /// Returns the first item in <see cref="IParseExpressionResult"/> using the following logic:<br/>
        /// -If the parsed expression starts with comments, returns an instance of <see cref="ITextItem"/> pared from the first
        /// comment.<br/>
        /// -If the expression does not start with comments, returns an instance of <see cref="IExpressionItemBase"/> parsed from
        /// the first expression.<br/>
        /// The returned value can be null, if no comments or expression items were parsed from the expression.
        /// </summary>
        /// <param name="parseExpressionResult">Parsed expression result.</param>
        [CanBeNull]
        public static ITextItem GetFirstTextItem([NotNull] this IParseExpressionResult parseExpressionResult)
        {
            var firstExpressionItem = parseExpressionResult.RootExpressionItem.GetFirstExpressionItem();

            if (firstExpressionItem != null)
                ValidateTextItem(firstExpressionItem);

            if (parseExpressionResult.SortedCommentedTextData.Count > 0)
            {
                var firstComment = parseExpressionResult.SortedCommentedTextData[0];

                ValidateTextItem(firstComment);

                if (firstExpressionItem == null || firstExpressionItem.IndexInText > firstComment.IndexInText)
                    return firstComment;
            }

            return firstExpressionItem;
        }

        /// <summary>
        /// Returns the last item in <see cref="IParseExpressionResult"/> using the following logic:<br/>
        /// -If the parsed expression ends with comments, returns an instance of <see cref="ITextItem"/> pared from the last
        /// comment.<br/>
        /// -If the expression does not end with comments, returns an instance of <see cref="IExpressionItemBase"/> parsed from
        /// the last expression.<br/>
        /// The returned value can be null, if no comments or expression items were parsed from the expression.
        /// </summary>
        /// <param name="parseExpressionResult">Parsed expression result.</param>
        [CanBeNull]
        public static ITextItem GetLastTextItem([NotNull] this IParseExpressionResult parseExpressionResult)
        {
            var lastExpressionItem = parseExpressionResult.RootExpressionItem.GetLastExpressionItem();

            if (lastExpressionItem != null)
                ValidateTextItem(lastExpressionItem);

            if (parseExpressionResult.SortedCommentedTextData.Count > 0)
            {
                var lastComment = parseExpressionResult.SortedCommentedTextData[parseExpressionResult.SortedCommentedTextData.Count - 1];
                ValidateTextItem(lastComment);

                if (lastExpressionItem == null || lastExpressionItem.IndexInText < lastComment.IndexInText)
                    return lastComment;
            }

            return lastExpressionItem;
        }

        internal static int GetIndexInText([NotNull] this IParseExpressionResult parseExpressionResult)
        {
            var firstTextItem = GetFirstTextItem(parseExpressionResult);
            return firstTextItem?.IndexInText ?? 0;
        }

        internal static int GetItemLength([NotNull] this IParseExpressionResult parseExpressionResult)
        {
            var lastTextItem = GetLastTextItem(parseExpressionResult);
            if (lastTextItem == null)
                return 0;

            return lastTextItem.IndexInText + lastTextItem.ItemLength - parseExpressionResult.GetIndexInText();
        }

        /// <summary>
        /// Returns a text that contains all parse error messages for errors in property <see cref="IParseExpressionResult.ParseErrorData"/> in parameter
        /// <paramref name="parseParseExpressionResult"/>, along with contextual information for each error message, such as text
        /// preceding or succeeding the position in
        /// parsed expression text where error happens, along with an arrow pointing to the position of the error in text.
        /// </summary>
        /// <param name="parseParseExpressionResult">Parsed expression result.</param>
        /// <param name="parsedTextStartPosition">Start position in parsed text where parsing was started.
        /// Normally this is the value of <see cref="IParseExpressionOptions.StartIndex"/> which by default is 0.</param>
        /// <param name="parsedTextEnd">Position in parsed text where parsing should end.
        /// Normally this is the length of parsed text, unless parsing is stopped by
        /// <see cref="IParseExpressionOptions.IsExpressionParsingComplete"/> is non-null and returns true, before the end of the parsed text is reached.</param>
        /// <param name="maxNumberOfCharactersToShowBeforeOrAfterErrorPosition">Maximum length of displayed text from parsed expression
        /// that precedes and succeeds the error position.</param>
        [NotNull]
        public static string GetErrorTextWithContextualInformation([NotNull] this IParseExpressionResult parseParseExpressionResult,
                                             int parsedTextStartPosition, int parsedTextEnd,
                                             int maxNumberOfCharactersToShowBeforeOrAfterErrorPosition = 50)
        {
            return parseParseExpressionResult.GetErrorTextWithContextualInformation(
                parseParseExpressionResult.ParseErrorData.GetAllSortedParseErrorItems(),
                parsedTextStartPosition, parsedTextEnd, maxNumberOfCharactersToShowBeforeOrAfterErrorPosition);
        }

        /// <summary>
        /// Returns a text that contains all parse error messages in <paramref name="parseErrors"/> along with
        /// contextual information for each error message, such as text preceding or succeeding the position in
        /// parsed expression text where error happens, along with an arrow pointing to the position of the error in text.
        /// </summary>
        /// <param name="parseParseExpressionResult">Parsed expression result.</param>
        /// <param name="parseErrors">List of parse error items.</param>
        /// <param name="parsedTextStartPosition">Start position in parsed text where parsing was started.
        /// Normally this is the value of <see cref="IParseExpressionOptions.StartIndex"/> which by default is 0.</param>
        /// <param name="parsedTextEnd">Position in parsed text where parsing should end.
        /// Normally this is the length of parsed text, unless parsing is stopped by
        /// <see cref="IParseExpressionOptions.IsExpressionParsingComplete"/> is non-null and returns true, before the end of the parsed text is reached.</param>
        /// <param name="maxNumberOfCharactersToShowBeforeOrAfterErrorPosition">Maximum length of displayed text from parsed expression
        /// that precedes and succeeds the error position.</param>
        public static string GetErrorTextWithContextualInformation([NotNull] this IParseExpressionResult parseParseExpressionResult,
                                             [NotNull, ItemNotNull] IEnumerable<IParseErrorItem> parseErrors,
                                             int parsedTextStartPosition, int parsedTextEnd,
                                             int maxNumberOfCharactersToShowBeforeOrAfterErrorPosition = 50)
        {
            var errorMessage = new StringBuilder();
            var errorIndex = 0;

            foreach (var parseErrorData in parseErrors)
            {
                if (errorIndex == 0)
                {
                    errorMessage.AppendLine("Expression parse errors:");
                    errorMessage.AppendLine();
                }

                errorMessage.AppendLine(string.Format("Parse error details: Error code: {0}, Error index: {1}. Error message: [{2}]",
                    parseErrorData.ParseErrorItemCode, parseErrorData.ErrorIndexInParsedText,
                    parseErrorData.ErrorMessage));

                errorMessage.AppendLine(string.Format("Error context:{0}{1}{0}",
                    Environment.NewLine,
                    parseParseExpressionResult.GetErrorContextualInformation(parseErrorData,
                        parsedTextStartPosition, parsedTextEnd,
                        maxNumberOfCharactersToShowBeforeOrAfterErrorPosition)));

                ++errorIndex;
            }

            return errorMessage.ToString();
        }

        /// <summary>
        /// Returns text which contains error contextual data for an error in <paramref name="parseErrorItem"/>, such as text preceding or succeeding the position in
        /// parsed expression text where error happens, along with an arrow pointing to the position of the error in text.
        /// </summary>
        /// <param name="parseExpressionResult">Parsed expression result.</param>
        /// <param name="parseErrorItem">Parsed error item.</param>
        /// <param name="parsedTextStartPosition">Start position in parsed text where parsing was started.
        /// Normally this is the value of <see cref="IParseExpressionOptions.StartIndex"/> which by default is 0.</param>
        /// <param name="parsedTextEnd">Position in parsed text where parsing should end.
        /// Normally this is the length of parsed text, unless parsing is stopped by
        /// <see cref="IParseExpressionOptions.IsExpressionParsingComplete"/> is non-null and returns true, before the end of the parsed text is reached.</param>
        /// <param name="maxNumberOfCharactersToShowBeforeOrAfterErrorPosition">Maximum length of displayed text from parsed expression
        /// that precedes and succeeds the error position.</param>
        /// <exception cref="ArgumentException">Throws this exception.</exception>
        public static string GetErrorContextualInformation([NotNull] this IParseExpressionResult parseExpressionResult, 
                                             [NotNull] IParseErrorItem parseErrorItem,
                                             int parsedTextStartPosition, int parsedTextEnd,
                                             int maxNumberOfCharactersToShowBeforeOrAfterErrorPosition = 50)
        {
            if (parsedTextEnd - parsedTextStartPosition <= 0 || parseExpressionResult.ParsedExpression.Length < parsedTextEnd)
                throw new ArgumentException($"The values of the following parameters are incorrect: {nameof(parseExpressionResult.ParsedExpression)}, {nameof(parsedTextStartPosition)}, {nameof(parseExpressionResult.ParsedExpression)}.");

            if (parseErrorItem.ErrorIndexInParsedText < parsedTextStartPosition || parseErrorItem.ErrorIndexInParsedText > parsedTextEnd)

                return
                    $"Could not generate error context: The value of parameter {nameof(IParseErrorItem.ErrorIndexInParsedText)}={parseErrorItem.ErrorIndexInParsedText} should be greater or equal than the value of parameter {nameof(parsedTextStartPosition)}={parsedTextStartPosition} and should be less than or equal the value of parameter {nameof(parsedTextEnd)}={parsedTextEnd}.";

            var startIndex = Math.Max(parsedTextStartPosition, parseErrorItem.ErrorIndexInParsedText - maxNumberOfCharactersToShowBeforeOrAfterErrorPosition);
            var contextEnd = Math.Min(parsedTextEnd, parseErrorItem.ErrorIndexInParsedText + maxNumberOfCharactersToShowBeforeOrAfterErrorPosition);

            var errorContext = new StringBuilder();

            var currentIndexInText = startIndex;

            var errorPositionInLineStrBldr = new StringBuilder();

            void TryAddErrorPositionHint()
            {
                if (errorPositionInLineStrBldr.Length == 0)
                    return;

                errorContext.AppendLine();

                errorContext.Append(errorPositionInLineStrBldr);
                errorPositionInLineStrBldr.Clear();
            }

            while (currentIndexInText < contextEnd)
            {
                if (Helpers.StartsWithSymbol(parseExpressionResult.ParsedExpression, Environment.NewLine, currentIndexInText, currentIndexInText + Environment.NewLine.Length,
                    false, (text, position) => true))
                {
                    TryAddErrorPositionHint();
                    errorContext.Append(Environment.NewLine);
                    currentIndexInText += Environment.NewLine.Length;
                    errorPositionInLineStrBldr.Clear();
                    continue;
                }

                var currentChar = parseExpressionResult.ParsedExpression[currentIndexInText];

                errorContext.Append(parseExpressionResult.ParsedExpression[currentIndexInText]);

                if (currentIndexInText < parseErrorItem.ErrorIndexInParsedText)
                {
                    if (currentChar == '\t')
                        errorPositionInLineStrBldr.Append(currentChar);
                    else
                        errorPositionInLineStrBldr.Append('-');
                }
                else if (currentIndexInText == parseErrorItem.ErrorIndexInParsedText)
                {
                    errorPositionInLineStrBldr.Append(char.ConvertFromUtf32(0x2191));
                }

                ++currentIndexInText;
            }

            TryAddErrorPositionHint();
            return errorContext.ToString();
        }
    }
}