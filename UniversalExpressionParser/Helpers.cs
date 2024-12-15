// Copyright (c) UniversalExpressionParser Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using UniversalExpressionParser.ExpressionItems;

namespace UniversalExpressionParser
{
    /// <summary>
    /// Helper methods.
    /// </summary>
    public static class Helpers
    {
        /// <summary>
        /// Character code for 'a'.
        /// </summary>
        public const int LowerACode = 'a';

        /// <summary>
        /// Character code for 'z'.
        /// </summary>
        public const int LowerZCode = 'z';

        /// <summary>
        /// Character code for 'A'.
        /// </summary>
        public const int CapitalACode = 'A';

        /// <summary>
        /// Character code for 'Z'.
        /// </summary>
        public const int CapitalZCode = 'Z';

        [NotNull]
        // ReSharper disable once InconsistentNaming
        private static readonly HashSet<char> _specialOperatorCharactersSet = new HashSet<char>();

        [NotNull]
        // ReSharper disable once InconsistentNaming
        private static readonly HashSet<char> _specialNonOperatorCharactersSet = new HashSet<char>();

        [NotNull]
        // ReSharper disable once InconsistentNaming
        private static readonly HashSet<char> _specialCharactersSet;

        static Helpers()
        {
            _specialOperatorCharactersSet.Add('~');
            _specialOperatorCharactersSet.Add('!');

            _specialOperatorCharactersSet.Add('@');
            _specialOperatorCharactersSet.Add('#');
            _specialOperatorCharactersSet.Add('$');
            _specialOperatorCharactersSet.Add('%');
            _specialOperatorCharactersSet.Add('^');
            _specialOperatorCharactersSet.Add('&');
            _specialOperatorCharactersSet.Add('|');

            _specialOperatorCharactersSet.Add('-');
            _specialOperatorCharactersSet.Add('+');

            _specialOperatorCharactersSet.Add('*');
            _specialOperatorCharactersSet.Add('/');

            _specialOperatorCharactersSet.Add('<');
            _specialOperatorCharactersSet.Add('>');
            _specialOperatorCharactersSet.Add('?');
            _specialOperatorCharactersSet.Add(':');

            _specialOperatorCharactersSet.Add('=');

            _specialNonOperatorCharactersSet.Add('`');
            _specialNonOperatorCharactersSet.Add('\'');
            _specialNonOperatorCharactersSet.Add('"');
            _specialNonOperatorCharactersSet.Add('(');
            _specialNonOperatorCharactersSet.Add(')');
            _specialNonOperatorCharactersSet.Add('[');
            _specialNonOperatorCharactersSet.Add(']');
            _specialNonOperatorCharactersSet.Add('{');
            _specialNonOperatorCharactersSet.Add('}');
            _specialNonOperatorCharactersSet.Add(',');
            _specialNonOperatorCharactersSet.Add(';');
            _specialNonOperatorCharactersSet.Add('\\');
            // Uncomment after testing
            //_specialOperatorCharactersSet.Add('.');

            _specialCharactersSet = new HashSet<char>(_specialOperatorCharactersSet);

            foreach (var character in _specialNonOperatorCharactersSet)
                _specialCharactersSet.Add(character);
        }

        /// <summary>
        /// Special operator characters such as '+', '-', ':', '*', '~', '!', etc.
        /// </summary>
        public static IEnumerable<char> SpecialOperatorCharacters => _specialOperatorCharactersSet;

        /// <summary>
        /// Special non-operator characters such as '`', '\', '/', '(', ')', '[', ']', ';', ',', etc.
        /// </summary>
        public static IEnumerable<char> SpecialNonOperatorCharacters => _specialNonOperatorCharactersSet;

        /// <summary>
        /// Returns true, if <paramref name="character"/> is a special operator character such as '+', '-', ':', '*', '~', '!', etc.
        /// </summary>
        /// <param name="character">Character to check.</param>
        public static bool IsSpecialOperatorCharacter(char character) => _specialOperatorCharactersSet.Contains(character);

        /// <summary>
        /// Returns true, if <paramref name="character"/> is a special non-operator character such '`', '\', '/', '(', ')', '[', ']', ';', ',', etc.
        /// </summary>
        /// <param name="character">Character to check.</param>
        public static bool IsSpecialNonOperatorCharacter(char character) => _specialNonOperatorCharactersSet.Contains(character);

        /// <summary>
        /// List of all special characters that include both operator characters such as '+', '-', ':', '*', '~', '!', etc, as well as
        /// special non-operator character such '`', '\', '/', '(', ')', '[', ']', ';', ',', etc.
        /// </summary>
        public static IEnumerable<char> SpecialCharacters => _specialCharactersSet;

        /// <summary>
        /// Returns true, if <paramref name="character"/> is either a special operator character such as '+', '-', ':', '*', '~', '!', etc,
        /// or is a special non-operator character such '`', '\', '/', '(', ')', '[', ']', ';', ',', etc.
        /// </summary>
        /// <param name="character">Character to check.</param>
        public static bool IsSpecialCharacter(char character) => _specialCharactersSet.Contains(character);

        internal static bool IsValidTextAfterTextToMatchDefault(char characterAfterMatchedText, int positionInText)
        {
            return _specialCharactersSet.Contains(characterAfterMatchedText);
        }

        /// <summary>
        /// Returns true, if text in <paramref name="text"/> starts with symbol <paramref name="symbolToMatch"/> at position <paramref name="positionInText"/>,
        /// and either <paramref name="symbolToMatch"/> ends with some special character in <see cref="SpecialOperatorCharacters"/> or
        /// text <paramref name="isValidTextAfterTextToMatch"/> is either followed by a
        /// whitespace character (space, tab, line break, etc), or is followed by other special text (say is followed by comment start
        /// marker <see cref="IExpressionLanguageProvider.LineCommentMarker"/> or <see cref="IExpressionLanguageProvider.MultilineCommentStartMarker"/>), or
        /// is followed by some character for which the check <see cref="IsValidTextAfterMatchedTextDelegate"/> is true, when <paramref name="isValidTextAfterTextToMatch"/> is not null.<br/>
        /// If the value of <paramref name="isValidTextAfterTextToMatch"/> is null, the value is defaulted to a check <see cref="DefaultExpressionLanguageProviderValidator"/> which
        /// will check if <paramref name="symbolToMatch"/> is followed by some special character in <see cref="SpecialCharacters"/>.<br/>
        /// For example if <paramref name="text"/> is "f1(x)begin/*some comment*/++x;end" and <paramref name="positionInText"/> is 4 (index of "begin"),
        /// and <paramref name="symbolToMatch"/> is equal to "begin", and <see cref="IExpressionLanguageProvider.MultilineCommentStartMarker"/> is "/*",
        /// then the method will detect that text "begin" is followed by a comment, and will return true.<br/>
        /// On the other hand, if <paramref name="text"/> is "beginx++ end", <paramref name="positionInText"/> is 0 (i.e., index of "begin" in <paramref name="text"/>),
        /// then the call to this method will return false, since "begin" is not followed by a whitespace, a special character, comment, ect.
        /// </summary>
        /// <param name="text">Text to search.</param>
        /// <param name="symbolToMatch">Symbol to match.</param>
        /// <param name="positionInText">
        /// Position in text to co check for text match. For example if <paramref name="text"/> is "f1(x)begin++x;end",
        /// <paramref name="positionInText"/> is 4 (index of "begin"), and <paramref name="symbolToMatch"/> equal to
        /// "begin", then the call to <see cref="StartsWithSymbol(string,string,int,int,bool,UniversalExpressionParser.ExpressionItems.IsValidTextAfterMatchedTextDelegate)"/> with <paramref name="symbolToMatch"/> will return true. 
        /// </param>
        /// <param name="textEnd">Position of text end, where matching evaluation should stop. Normally this will be the length of <paramref name="text"/>, however
        /// smaller value can be used as well.
        /// </param>
        /// <param name="expressionLanguageProvider">
        /// Expression language provider. Some property values in <see cref="IExpressionLanguageProvider"/> such as <see cref="IExpressionLanguageProvider.LineCommentMarker"/> and
        /// <see cref="IExpressionLanguageProvider.MultilineCommentStartMarker"/> are evaluated when executing this method.
        /// For example if <paramref name="text"/> is "f1(x)begin/*some comment*/++x;end" and <paramref name="positionInText"/> is 4 (index of "begin"),
        /// and <paramref name="symbolToMatch"/> is equal to "begin", and <see cref="IExpressionLanguageProvider.MultilineCommentStartMarker"/> is "/*",
        /// then the method will detect that text "begin" is followed by a comment, and will return true.<br/>
        /// Also, the value of <see cref="IExpressionLanguageProvider.IsLanguageCaseSensitive"/> is used to determine if case sensitive search should be executed.
        /// </param>
        /// <param name="isValidTextAfterTextToMatch">A delegate that does additional check for the text that follows <paramref name="symbolToMatch"/>.
        /// The value is defaulted to <see cref="DefaultExpressionLanguageProviderValidator"/> which checks if <paramref name="symbolToMatch"/> is followed with some special character in
        /// <see cref="SpecialCharacters"/>.
        /// For example if <paramref name="text"/> is "begin++x end", <paramref name="symbolToMatch"/> is "begin", and <paramref name="positionInText"/> is 0, and
        /// <paramref name="expressionLanguageProvider"/> is <see cref="DefaultExpressionLanguageProviderValidator"/>, then the method will return true, since text "begin" is followed
        /// by a special character '+'. On the other hand, if <paramref name="isValidTextAfterTextToMatch"/> is a non null value that returns true for '/', and false for '+',
        /// then the call to this method will return false. 
        /// </param>
        public static bool StartsWithSymbol([NotNull] string text, [NotNull] string symbolToMatch,
                                      int positionInText, int textEnd,
                                      [NotNull] IExpressionLanguageProvider expressionLanguageProvider,
                                      [CanBeNull] IsValidTextAfterMatchedTextDelegate isValidTextAfterTextToMatch = null) =>
            StartsWithSymbol(text, symbolToMatch, positionInText, textEnd, expressionLanguageProvider.IsLanguageCaseSensitive, isValidTextAfterTextToMatch ?? IsValidTextAfterTextToMatchDefault, expressionLanguageProvider, false).isTextFound;


        /// <summary>
        /// Returns true, if text in <paramref name="text"/> starts with symbol <paramref name="symbolToMatch"/> at position <paramref name="positionInText"/>,
        /// and either <paramref name="symbolToMatch"/> ends with some special character in <see cref="SpecialOperatorCharacters"/> or
        /// text <paramref name="isValidTextAfterTextToMatch"/> is either followed by a
        /// whitespace character (space, tab, line break, etc), or is followed by other special text (say is followed by comment start
        /// marker <see cref="IExpressionLanguageProvider.LineCommentMarker"/> or <see cref="IExpressionLanguageProvider.MultilineCommentStartMarker"/>), or
        /// is followed by some character for which the check <see cref="IsValidTextAfterMatchedTextDelegate"/> is true, when <paramref name="isValidTextAfterTextToMatch"/> is not null.<br/>
        /// If the value of <paramref name="isValidTextAfterTextToMatch"/> is null, the value is defaulted to a check <see cref="DefaultExpressionLanguageProviderValidator"/> which
        /// will check if <paramref name="symbolToMatch"/> is followed by some special character in <see cref="SpecialCharacters"/>.<br/>
        /// For example if <paramref name="text"/> is "f1(x)begin/*some comment*/++x;end" and <paramref name="positionInText"/> is 4 (index of "begin"),
        /// and <paramref name="symbolToMatch"/> is equal to "begin", and <see cref="IExpressionLanguageProvider.MultilineCommentStartMarker"/> is "/*",
        /// then the method will detect that text "begin" is followed by a comment, and will return true.<br/>
        /// On the other hand, if <paramref name="text"/> is "beginx++ end", <paramref name="positionInText"/> is 0 (i.e., index of "begin" in <paramref name="text"/>),
        /// then the call to this method will return false, since "begin" is not followed by a whitespace, a special character, comment, ect.
        /// </summary>
        /// <param name="text">Text to search.</param>
        /// <param name="symbolToMatch">Symbol to match.</param>
        /// <param name="positionInText">
        /// Position in text to co check for text match. For example if <paramref name="text"/> is "f1(x)begin++x;end",
        /// <paramref name="positionInText"/> is 4 (index of "begin"), and <paramref name="symbolToMatch"/> equal to
        /// "begin", then the call to <see cref="StartsWithSymbol(string,string,int,int,bool,UniversalExpressionParser.ExpressionItems.IsValidTextAfterMatchedTextDelegate)"/> with <paramref name="symbolToMatch"/> will return true. 
        /// </param>
        /// <param name="textEnd">Position of text end, where matching evaluation should stop. Normally this will be the length of <paramref name="text"/>, however
        /// smaller value can be used as well.
        /// </param>
        /// <param name="expressionLanguageProvider">
        /// Expression language provider. Some property values in <see cref="IExpressionLanguageProvider"/> such as <see cref="IExpressionLanguageProvider.LineCommentMarker"/> and
        /// <see cref="IExpressionLanguageProvider.MultilineCommentStartMarker"/> are evaluated when executing this method.
        /// For example if <paramref name="text"/> is "f1(x)begin/*some comment*/++x;end" and <paramref name="positionInText"/> is 4 (index of "begin"),
        /// and <paramref name="symbolToMatch"/> is equal to "begin", and <see cref="IExpressionLanguageProvider.MultilineCommentStartMarker"/> is "/*",
        /// then the method will detect that text "begin" is followed by a comment, and will return true.<br/>
        /// Also, the value of <see cref="IExpressionLanguageProvider.IsLanguageCaseSensitive"/> is used to determine if case sensitive search should be executed.
        /// </param>
        /// <param name="isValidTextAfterTextToMatch">A delegate that does additional check for the text that follows <paramref name="symbolToMatch"/>.
        /// The value is defaulted to <see cref="DefaultExpressionLanguageProviderValidator"/> which checks if <paramref name="symbolToMatch"/> is followed with some special character in
        /// <see cref="SpecialCharacters"/>.
        /// For example if <paramref name="text"/> is "begin++x end", <paramref name="symbolToMatch"/> is "begin", and <paramref name="positionInText"/> is 0, and
        /// <paramref name="expressionLanguageProvider"/> is <see cref="DefaultExpressionLanguageProviderValidator"/>, then the method will return true, since text "begin" is followed
        /// by a special character '+'. On the other hand, if <paramref name="isValidTextAfterTextToMatch"/> is a non null value that returns true for '/', and false for '+',
        /// then the call to this method will return false. 
        /// </param>
        /// <param name="matchedSymbol">
        /// An output parameter for the matched symbol. If the returned value is true, the value is not null.
        /// Otherwise the value is null.<br/>
        /// If the value of <see cref="IExpressionLanguageProvider.IsLanguageCaseSensitive"/> is true, the value is the same as
        /// <paramref name="symbolToMatch"/>, otherwise, the value is equal to <paramref name="matchedSymbol"/> when case is ignored. 
        /// </param>
        public static bool StartsWithSymbol([NotNull] string text, [NotNull] string symbolToMatch,
                                      int positionInText, int textEnd,
                                      [NotNull] IExpressionLanguageProvider expressionLanguageProvider,
                                      [CanBeNull] IsValidTextAfterMatchedTextDelegate isValidTextAfterTextToMatch,
                                      out string matchedSymbol)
        {
            var result = StartsWithSymbol(text, symbolToMatch, positionInText, textEnd, expressionLanguageProvider.IsLanguageCaseSensitive,
                isValidTextAfterTextToMatch ?? IsValidTextAfterTextToMatchDefault, expressionLanguageProvider, true);
            matchedSymbol = result.matchedText;
            return result.isTextFound;
        }

        /// <summary>
        /// Returns true, if text in <paramref name="text"/> starts with symbol <paramref name="symbolToMatch"/> at position <paramref name="positionInText"/>,
        /// and either <paramref name="symbolToMatch"/> ends with some special character in <see cref="SpecialOperatorCharacters"/> or
        /// text <paramref name="isValidTextAfterTextToMatch"/> is either followed by a
        /// whitespace character (space, tab, line break, etc), or
        /// is followed by some character for which the check <see cref="IsValidTextAfterMatchedTextDelegate"/> is true, when <paramref name="isValidTextAfterTextToMatch"/> is not null.<br/>
        /// If the value of <paramref name="isValidTextAfterTextToMatch"/> is null, the value is defaulted to a check <see cref="DefaultExpressionLanguageProviderValidator"/> which
        /// will check if <paramref name="symbolToMatch"/> is followed by some special character in <see cref="SpecialCharacters"/>.<br/>
        /// For example if <paramref name="text"/> is "f1(x)begin++x;end" and <paramref name="positionInText"/> is 4 (index of "begin"),
        /// and <paramref name="symbolToMatch"/> is equal to "begin",
        /// then the method will return true, since "begin" is followed by a special character '+' that will be approved by <paramref name="isValidTextAfterTextToMatch"/>.<br/>
        /// On the other hand, if <paramref name="text"/> is "beginx++ end", <paramref name="positionInText"/> is 0 (i.e., index of "begin" in <paramref name="text"/>),
        /// then the call to this method will return false, since "begin" is not followed by a whitespace, a special character, ect.
        /// </summary>
        /// <param name="text">Text to search.</param>
        /// <param name="symbolToMatch">Text to match.</param>
        /// <param name="positionInText">
        /// Position in text to co check for text match. For example if <paramref name="text"/> is "f1(x)begin++x;end",
        /// <paramref name="positionInText"/> is 4 (index of "begin"), and <paramref name="symbolToMatch"/> equal to
        /// "begin", then the call to <see cref="StartsWithSymbol(string,string,int,int,bool,UniversalExpressionParser.ExpressionItems.IsValidTextAfterMatchedTextDelegate)"/> with <paramref name="symbolToMatch"/> will return true. 
        /// </param>
        /// <param name="textEnd">Position of text end, where matching evaluation should stop. Normally this will be the length of <paramref name="text"/>, however
        /// smaller value can be used as well.
        /// </param>
        /// <param name="isCaseSensitiveSearch">Determines is search should be case sensitive or not.</param>
        /// <param name="isValidTextAfterTextToMatch">A delegate that does additional check for the text that follows <paramref name="symbolToMatch"/>.
        /// The value is defaulted to <see cref="DefaultExpressionLanguageProviderValidator"/> which checks if <paramref name="symbolToMatch"/> is followed with some special character in
        /// <see cref="SpecialCharacters"/>.
        /// </param>
        public static bool StartsWithSymbol([NotNull] string text, [NotNull] string symbolToMatch,
                                      int positionInText, int textEnd,
                                      bool isCaseSensitiveSearch,
                                      [NotNull] IsValidTextAfterMatchedTextDelegate isValidTextAfterTextToMatch) =>
            StartsWithSymbol(text, symbolToMatch, positionInText, textEnd, isCaseSensitiveSearch, isValidTextAfterTextToMatch, null, false).isTextFound;


        /// <summary>
        /// Returns true, if text in <paramref name="text"/> starts with symbol <paramref name="symbolToMatch"/> at position <paramref name="positionInText"/>,
        /// and either <paramref name="symbolToMatch"/> ends with some special character in <see cref="SpecialOperatorCharacters"/> or
        /// text <paramref name="isValidTextAfterTextToMatch"/> is either followed by a
        /// whitespace character (space, tab, line break, etc), or
        /// is followed by some character for which the check <see cref="IsValidTextAfterMatchedTextDelegate"/> is true, when <paramref name="isValidTextAfterTextToMatch"/> is not null.<br/>
        /// If the value of <paramref name="isValidTextAfterTextToMatch"/> is null, the value is defaulted to a check <see cref="DefaultExpressionLanguageProviderValidator"/> which
        /// will check if <paramref name="symbolToMatch"/> is followed by some special character in <see cref="SpecialCharacters"/>.<br/>
        /// For example if <paramref name="text"/> is "f1(x)begin++x;end" and <paramref name="positionInText"/> is 4 (index of "begin"),
        /// and <paramref name="symbolToMatch"/> is equal to "begin",
        /// then the method will return true, since "begin" is followed by a special character '+' that will be approved by <paramref name="isValidTextAfterTextToMatch"/>.<br/>
        /// On the other hand, if <paramref name="text"/> is "beginx++ end", <paramref name="positionInText"/> is 0 (i.e., index of "begin" in <paramref name="text"/>),
        /// then the call to this method will return false, since "begin" is not followed by a whitespace, a special character, ect.
        /// </summary>
        /// <param name="text">Text to search.</param>
        /// <param name="symbolToMatch">Symbol to match.</param>
        /// <param name="positionInText">
        /// Position in text to co check for text match. For example if <paramref name="text"/> is "f1(x)begin++x;end",
        /// <paramref name="positionInText"/> is 4 (index of "begin"), and <paramref name="symbolToMatch"/> equal to
        /// "begin", then the call to <see cref="StartsWithSymbol(string,string,int,int,bool,UniversalExpressionParser.ExpressionItems.IsValidTextAfterMatchedTextDelegate)"/> with <paramref name="symbolToMatch"/> will return true. 
        /// </param>
        /// <param name="textEnd">Position of text end, where matching evaluation should stop. Normally this will be the length of <paramref name="text"/>, however
        /// smaller value can be used as well.
        /// </param>
        /// <param name="isCaseSensitiveSearch">Determines is search should be case sensitive or not.</param>
        /// <param name="isValidTextAfterTextToMatch">A delegate that does additional check for the text that follows <paramref name="symbolToMatch"/>.
        /// The value is defaulted to <see cref="DefaultExpressionLanguageProviderValidator"/> which checks if <paramref name="symbolToMatch"/> is followed with some special character in
        /// <see cref="SpecialCharacters"/>.
        /// </param>
        /// <param name="matchedSymbol">
        /// An output parameter for the matched symbol. If the returned value is true, the value is not null.
        /// Otherwise the value is null.<br/>
        /// If the value of <paramref name="isCaseSensitiveSearch"/> is true, the value is the same as
        /// <paramref name="symbolToMatch"/>, otherwise, the value is equal to <paramref name="matchedSymbol"/> when case is ignored. 
        /// </param>
        public static bool StartsWithSymbol([NotNull] string text, [NotNull] string symbolToMatch,
                                      int positionInText, int textEnd,
                                      bool isCaseSensitiveSearch, [NotNull] IsValidTextAfterMatchedTextDelegate isValidTextAfterTextToMatch, out string matchedSymbol)
        {
            var result = StartsWithSymbol(text, symbolToMatch, positionInText, textEnd, isCaseSensitiveSearch, isValidTextAfterTextToMatch, null, true);
            matchedSymbol = result.matchedText;
            return result.isTextFound;
        }

        private static (bool isTextFound, string matchedText) StartsWithSymbol([NotNull] string text, [NotNull] string symbolToMatch,
                                        int positionInText, int textEnd,
                                        bool isCaseSensitiveSearch,
                                        [CanBeNull] IsValidTextAfterMatchedTextDelegate isValidTextAfterTextToMatch,
                                        [CanBeNull] IExpressionLanguageProvider expressionLanguageProvider,
                                        bool returnFoundText)
        {

            if (textEnd > text.Length)
                textEnd = text.Length;

            int endPosition = positionInText + symbolToMatch.Length;

            if (endPosition == positionInText || endPosition > textEnd)
                return (false, null);

            for (var currPositionInText = positionInText; currPositionInText < endPosition; ++currPositionInText)
            {
                var currCharInText = text[currPositionInText];
                var currCharInTextToMatch = symbolToMatch[currPositionInText - positionInText];

                if (isCaseSensitiveSearch)
                {
                    if (currCharInText != currCharInTextToMatch)
                        return (false, null);
                }
                else
                {
                    if (Char.ToUpper(currCharInText) != Char.ToUpper(currCharInTextToMatch))
                        return (false, null);
                }
            }

            if (endPosition == textEnd || IsSpecialCharacter(symbolToMatch[symbolToMatch.Length - 1]))
                return (true, returnFoundText ? (isCaseSensitiveSearch ? symbolToMatch : text.Substring(positionInText, symbolToMatch.Length)) : null);

            var characterAfterMatchedText = text[endPosition];

            if (Char.IsWhiteSpace(characterAfterMatchedText) ||
                (isValidTextAfterTextToMatch?.Invoke(characterAfterMatchedText, endPosition) ?? false) ||

                // Check if the text is followed by single-line or multi-line comment.
                // Examples are "xxxTextToMatch//comments..." or "xxxTextToMatch/*comments...*/"
                expressionLanguageProvider != null &&

                // Check if the text is followed by line comment marker
                (expressionLanguageProvider.LineCommentMarker != null &&
                 endPosition + expressionLanguageProvider.LineCommentMarker.Length <= textEnd &&
                 text.Substring(endPosition, expressionLanguageProvider.LineCommentMarker.Length)
                     .Equals(expressionLanguageProvider.LineCommentMarker,
                         isCaseSensitiveSearch ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal) ||

                // Check if the text is followed by multi-line comment marker
                expressionLanguageProvider.MultilineCommentStartMarker != null &&
                 endPosition + expressionLanguageProvider.MultilineCommentStartMarker.Length <= textEnd &&
                 text.Substring(endPosition, expressionLanguageProvider.MultilineCommentStartMarker.Length)
                     .Equals(expressionLanguageProvider.MultilineCommentStartMarker,
                         isCaseSensitiveSearch ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal))
                )
                return (true, returnFoundText ?
                    isCaseSensitiveSearch ? symbolToMatch : text.Substring(positionInText, symbolToMatch.Length) : null);

            return (false, null);
        }

        /// <summary>
        /// Returns true, if <paramref name="character"/> is a latin character between 'a' and 'z' or 'A' and 'Z'.
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsLatinLetter(char character) => character >= LowerACode && character <= LowerZCode || character >= CapitalACode && character <= CapitalZCode;

        /// <summary>
        /// Processes expression item <paramref name="expressionItem"/>. If <paramref name="expressionItem"/> is an instance of <see cref="IComplexExpressionItem"/>,
        /// its components in <see cref="IComplexExpressionItem.AllItems"/> will be processed too.
        /// </summary>
        /// <param name="expressionItem">Expression item to process.</param>
        /// <param name="processItem">A delegate that processes the expression items.</param>
        public static void ProcessExpressionItem([NotNull] IExpressionItemBase expressionItem,
                                                 [NotNull] ProcessExpressionItem processItem)
        {
            ProcessExpressionItem(expressionItem, processItem, null, null);
        }

        internal static void ProcessExpressionItem([NotNull] IExpressionItemBase expressionItem,
                                                 [NotNull] ProcessExpressionItem processItem,
                                                 [CanBeNull] OnComplexExpressionItemPartProcessed onComplexExpressionItemPartProcessed,
                                                 [CanBeNull] OnAllComplexExpressionItemPartsProcessed onAllComplexExpressionItemPartsProcessed)
        {
            bool stopProcessing = false;

            ProcessExpressionItem(expressionItem, processItem,
                onComplexExpressionItemPartProcessed, onAllComplexExpressionItemPartsProcessed, ref stopProcessing);

        }

        private static void ProcessExpressionItem([NotNull] IExpressionItemBase expressionItem,
                                                  [NotNull] ProcessExpressionItem processItem,
                                                  [CanBeNull] OnComplexExpressionItemPartProcessed onComplexExpressionItemPartProcessed,
                                                  [CanBeNull] OnAllComplexExpressionItemPartsProcessed onAllComplexExpressionItemPartsProcessed,
                                                  ref bool stopProcessing)
        {
            if (stopProcessing)
                return;

            if (!processItem(expressionItem))
            {
                stopProcessing = true;
                return;
            }

            if (expressionItem is IComplexExpressionItem complexExpressionItem)
            {
                int currentChildExpressionItemInd = 0;

                int partInd = 0;
                foreach (var currentExpressionItem in complexExpressionItem.AllItems)
                {
                    var childExpressionIndex = -1;

                    for (; currentChildExpressionItemInd < complexExpressionItem.Children.Count; ++currentChildExpressionItemInd)
                    {
                        var currentChildExpressionItem = complexExpressionItem.Children[currentChildExpressionItemInd];

                        if (currentChildExpressionItem.IndexInText == currentExpressionItem.IndexInText)
                        {
                            childExpressionIndex = currentChildExpressionItemInd;
                            break;
                        }

                        if (currentChildExpressionItem.IndexInText > currentExpressionItem.IndexInText)
                            break;
                    }

                    ProcessExpressionItem(currentExpressionItem, processItem, onComplexExpressionItemPartProcessed, onAllComplexExpressionItemPartsProcessed, ref stopProcessing);

                    if (stopProcessing)
                        break;

                    if (onComplexExpressionItemPartProcessed != null && !onComplexExpressionItemPartProcessed(complexExpressionItem, currentExpressionItem,
                            partInd, childExpressionIndex))
                    {
                        stopProcessing = true;
                        break;
                    }

                    ++partInd;
                }


                if (onAllComplexExpressionItemPartsProcessed != null && !onAllComplexExpressionItemPartsProcessed(complexExpressionItem))
                    stopProcessing = true;
            }
        }

        internal static ICommentedTextData TryGetCommentedOutCode([NotNull] string codeText,
                                                                     int positionInText, int textEnd,
                                                                     [NotNull] IExpressionLanguageProvider expressionLanguageProvider,
                                                                     out bool isCommentClosed)
        {
            isCommentClosed = false;

            // We can have situations when line comment marker is contained in block contain marker or vice versa.
            // Example is rem for line comment, and rem* for block comment start marker.
            // In ths case, we will first try the block comment, since it is larger (and might contain line marker), then we will try
            // to parse line comment, if parsing block comment didn't work out.
            if (expressionLanguageProvider.MultilineCommentStartMarker != null &&
                (expressionLanguageProvider.LineCommentMarker == null ||
                 expressionLanguageProvider.MultilineCommentStartMarker.Length >=
                 expressionLanguageProvider.LineCommentMarker.Length))
            {
                var commentedTextData = TryGetBlockCommentData(codeText, positionInText, textEnd, expressionLanguageProvider, ref isCommentClosed);

                if (commentedTextData != null)
                    return commentedTextData;

                return TryGetLineComment(codeText, positionInText, textEnd, expressionLanguageProvider, ref isCommentClosed);
            }


            if (expressionLanguageProvider.LineCommentMarker != null)
            {
                // If we got here, either expressionLanguageProvider.MultilineCommentStartMarker is null or line comment has more characters.
                // Lets first try to parse line comment, then block comment.

                var commentedTextData = TryGetLineComment(codeText, positionInText, textEnd, expressionLanguageProvider, ref isCommentClosed);

                if (commentedTextData != null)
                    return commentedTextData;

                return TryGetBlockCommentData(codeText, positionInText, textEnd, expressionLanguageProvider, ref isCommentClosed);
            }

            return null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static ICommentedTextData TryGetLineComment([NotNull] string codeText,
            int positionInText, int textEnd,
            [NotNull] IExpressionLanguageProvider expressionLanguageProvider,
            ref bool isCommentClosed)
        {
            if (expressionLanguageProvider.LineCommentMarker != null)
            {
                var commentedTextData = TryGetCommentedOutCode(codeText, positionInText, textEnd, true, expressionLanguageProvider.IsLanguageCaseSensitive,
                    expressionLanguageProvider.LineCommentMarker, Environment.NewLine, out isCommentClosed);

                if (commentedTextData != null)
                    return commentedTextData;
            }

            return null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static ICommentedTextData TryGetBlockCommentData([NotNull] string codeText,
            int positionInText, int textEnd,
            [NotNull] IExpressionLanguageProvider expressionLanguageProvider,
            ref bool isCommentClosed)
        {
            if (expressionLanguageProvider.MultilineCommentStartMarker != null && expressionLanguageProvider.MultilineCommentEndMarker != null)
            {
                var commentedTextData = TryGetCommentedOutCode(codeText, positionInText, textEnd, false, expressionLanguageProvider.IsLanguageCaseSensitive,
                    expressionLanguageProvider.MultilineCommentStartMarker, expressionLanguageProvider.MultilineCommentEndMarker, out isCommentClosed);

                if (commentedTextData != null)
                    return commentedTextData;
            }

            return null;
        }

        internal static ICommentedTextData TryGetCommentedOutCode([NotNull] string codeText,
            int positionInText, int textEnd,
            bool isLineComment,
            bool isLanguageCaseSensitive,
            [NotNull] string commentCodeStartMarker,
            [NotNull] string commentCodeEndMarker,
            out bool isCommentClosed)
        {
            isCommentClosed = false;

            if (StartsWithSymbol(codeText, commentCodeStartMarker, positionInText, textEnd, isLanguageCaseSensitive, (character, positionInTex2) => true, null, false).isTextFound)
            {
                var indexOfEndCommentBlockText = codeText.IndexOf(
                    commentCodeEndMarker,
                    positionInText + commentCodeStartMarker.Length,
                    isLanguageCaseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase);

                int commentEnd;

                if (indexOfEndCommentBlockText >= 0)
                {
                    isCommentClosed = true;

                    commentEnd = indexOfEndCommentBlockText;

                    if (!isLineComment)
                        commentEnd += commentCodeEndMarker.Length;
                }
                else
                {
                    if (isLineComment)
                        isCommentClosed = true;

                    commentEnd = textEnd;
                }

                return new CommentedTextData(positionInText, commentEnd - positionInText, isLineComment);
            }

            return null;
        }

        /// <summary>
        /// Skips spaces and tries to parse comments. The parsed comments are added to <see cref="IParseExpressionResult.SortedCommentedTextData"/>.
        /// </summary>
        /// <returns>Returns false, if end of text is reached. Returns true otherwise.</returns>
        public static bool TryParseComments([NotNull] IParseExpressionItemContext context,
                                            [CanBeNull] Action<ICommentedTextData> onCommentParsed = null)
        {
            var textSymbolsParser = context.TextSymbolsParser;

            while (textSymbolsParser.SkipSpaces())
            {
                var commentedCodeInfo = TryGetCommentedOutCode(textSymbolsParser.TextToParse,
                    textSymbolsParser.PositionInText, textSymbolsParser.ParsedTextEnd, context.ExpressionLanguageProviderWrapper.ExpressionLanguageProvider, out var isCommentClosed);

                if (commentedCodeInfo != null)
                {
                    context.ParseExpressionResult.AddCommentedTextData(commentedCodeInfo);

                    onCommentParsed?.Invoke(commentedCodeInfo);

                    if (!isCommentClosed)
                    {
                        context.AddParseErrorItem(new ParseErrorItem(
                            textSymbolsParser.PositionInText, () => "Comment is not closed.", ParseErrorItemCode.CommentNotClosed));
                    }

                    textSymbolsParser.SkipCharacters(commentedCodeInfo.ItemLength);
                }
                else
                {
                    break;
                }
            }

            return !textSymbolsParser.IsEndOfTextReached;
        }

        /// <summary>
        /// Returns true if identifiers specified in parameters <paramref name="identifier1"/> and <paramref name="identifier2"/> conflict.
        /// Used when validating <see cref="IExpressionLanguageProvider"/>.
        ///  </summary>
        public static bool CheckIfIdentifiersConflict([NotNull] IExpressionLanguageProvider expressionLanguageProvider,
            [NotNull] string identifier1, [NotNull] string identifier2)
        {
            return CheckIfIdentifiersConflict(expressionLanguageProvider, identifier1, identifier2,
                nameof(identifier1), nameof(identifier2),
                ConflictCheckType.Identifier1IsContainedInIdentifier2OrViceVersa, false, out _);
        }

        /// <summary>
        /// Returns true if identifiers specified in parameters <paramref name="identifier1"/> and <paramref name="identifier2"/> conflict.
        /// Used when validating <see cref="IExpressionLanguageProvider"/>.
        /// </summary>
        public static bool CheckIfIdentifiersConflict([NotNull] IExpressionLanguageProvider expressionLanguageProvider,
            [NotNull] string identifier1, [NotNull] string identifier2,
            [NotNull] string identifierName1, [NotNull] string identifierName2, out string errorMessage)
        {
            return CheckIfIdentifiersConflict(expressionLanguageProvider, identifier1, identifier2,
                identifierName1, identifierName2, ConflictCheckType.Identifier1IsContainedInIdentifier2OrViceVersa, true, out errorMessage);
        }

        /// <summary>
        /// Returns true if identifier <paramref name="containedIdentifier"/> is contained in <paramref name="containingIdentifier"/> using the special rules.
        /// Used when validating <see cref="IExpressionLanguageProvider"/>.
        ///  </summary>
        public static bool CheckIfIdentifierIsInAnotherIdentifier([NotNull] IExpressionLanguageProvider expressionLanguageProvider,
                                [NotNull] string containedIdentifier, [NotNull] string containingIdentifier)
        {
            return CheckIfIdentifiersConflict(expressionLanguageProvider, containedIdentifier, containingIdentifier,
                nameof(containedIdentifier), nameof(containingIdentifier),
                ConflictCheckType.Identifier1IsContainedInIdentifier2, false, out _);
        }
        
        /// <summary>
        /// Returns true if identifier <paramref name="containedIdentifier"/> is contained in <paramref name="containingIdentifier"/> using the special rules.
        /// Used when validating <see cref="IExpressionLanguageProvider"/>.
        ///  </summary>
        public static bool CheckIfIdentifierIsInAnotherIdentifier([NotNull] IExpressionLanguageProvider expressionLanguageProvider,
            [NotNull] string containedIdentifier, [NotNull] string containingIdentifier,
            [NotNull] string containedIdentifierName, [NotNull] string containingIdentifierName, out string errorMessage)
        {
            return CheckIfIdentifiersConflict(expressionLanguageProvider, containedIdentifier, containingIdentifier, containedIdentifierName,
                containingIdentifierName, ConflictCheckType.Identifier1IsContainedInIdentifier2, true, out errorMessage);
        }

        /// <summary>
        /// Returns true if identifier <paramref name="containedIdentifier"/> is contained in <paramref name="containingIdentifier"/> at zero position using the special rules.
        /// Used when validating <see cref="IExpressionLanguageProvider"/>.
        ///  </summary>
        public static bool CheckIfIdentifierIsInAnotherIdentifierAtZeroPosition([NotNull] IExpressionLanguageProvider expressionLanguageProvider,
            [NotNull] string containedIdentifier, [NotNull] string containingIdentifier)
        {
            return CheckIfIdentifiersConflict(expressionLanguageProvider, containedIdentifier, containingIdentifier,
                nameof(containedIdentifier), nameof(containingIdentifier),
                ConflictCheckType.Identifier1IsContainedInIdentifier2AtZeroPosition, false, out _);
        }

        /// <summary>
        /// Returns true if identifier <paramref name="containedIdentifier"/> is contained in <paramref name="containingIdentifier"/> at zero position using the special rules.
        /// Used when validating <see cref="IExpressionLanguageProvider"/>.
        ///  </summary>
        public static bool CheckIfIdentifierIsInAnotherIdentifierAtZeroPosition([NotNull] IExpressionLanguageProvider expressionLanguageProvider,
            [NotNull] string containedIdentifier, [NotNull] string containingIdentifier,
            [NotNull] string containedIdentifierName, [NotNull] string containingIdentifierName, out string errorMessage)
        {
            return CheckIfIdentifiersConflict(expressionLanguageProvider, containedIdentifier, containingIdentifier, containedIdentifierName,
                containingIdentifierName, ConflictCheckType.Identifier1IsContainedInIdentifier2AtZeroPosition, true, out errorMessage);
        }

        ///// <summary>
        ///// Returns true if identifiers specified in parameters <paramref name="identifier1"/> and <paramref name="identifier2"/> are equal.
        ///// Used when validating <see cref="IExpressionLanguageProvider"/>.
        /////  </summary>
        //internal static bool CheckIfIdentifiersAreEqual([NotNull] IExpressionLanguageProvider expressionLanguageProvider,
        //    [NotNull] string identifier1, [NotNull] string identifier2)
        //{
        //    return CheckIfIdentifiersConflict(expressionLanguageProvider, identifier1, identifier2, 
        //        nameof(identifier1), nameof(identifier2), ConflictCheckType.IdentifiersAreEqual,
        //        false, out _);
        //}


        ///// <summary>
        ///// Returns true if identifiers specified in parameters <paramref name="identifier1"/> and <paramref name="identifier2"/> are equal.
        ///// Used when validating <see cref="IExpressionLanguageProvider"/>.
        /////  </summary>
        //internal static bool CheckIfIdentifiersAreEqual([NotNull] IExpressionLanguageProvider expressionLanguageProvider, 
        //    [NotNull] string identifier1, [NotNull] string identifier2,
        //    [NotNull] string identifierName1, [NotNull] string identifierName2, out string errorMessage)
        //{
        //    return CheckIfIdentifiersConflict(expressionLanguageProvider, identifier1, identifier2, identifierName1,
        //        identifierName2, ConflictCheckType.IdentifiersAreEqual, true, out errorMessage);
        //}

        private enum ConflictCheckType
        {
            IdentifiersAreEqual,
            Identifier1IsContainedInIdentifier2,
            Identifier1IsContainedInIdentifier2AtZeroPosition,
            Identifier1IsContainedInIdentifier2OrViceVersa
        }

        private static bool CheckIfIdentifiersConflict([NotNull] IExpressionLanguageProvider expressionLanguageProvider,
            [NotNull] string identifier1, [NotNull] string identifier2,
            [NotNull] string identifierName1, [NotNull] string identifierName2,
            ConflictCheckType conflictCheckType, bool generateErrorMessage, out string errorMessage)
        {
            errorMessage = null;

            var stringComparison = expressionLanguageProvider.IsLanguageCaseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;

            if (identifier1.Length == identifier2.Length)
            {
                if (!string.Equals(identifier1, identifier2, stringComparison))
                    return false;

                if (generateErrorMessage)
                    errorMessage = $"The value of {identifierName1}=\"{identifier1}\" cannot be the same as {identifierName2}=\"{identifier2}\".";
                return true;
            }

            if (conflictCheckType == ConflictCheckType.IdentifiersAreEqual)
                return false;

            string textToSearch;
            string textToMatch;
            string textToSearchName;
            string textToMatchName;

            if (identifier1.Length < identifier2.Length)
            {
                textToSearch = identifier2;
                textToSearchName = identifierName2;

                textToMatch = identifier1;
                textToMatchName = identifierName1;
            }
            else
            {
                if (conflictCheckType != ConflictCheckType.Identifier1IsContainedInIdentifier2OrViceVersa)
                    return false;

                textToSearch = identifier1;
                textToSearchName = identifierName1;

                textToMatch = identifier2;
                textToMatchName = identifierName2;
            }

            var matchedIndex = -1;

            if (textToSearch.Length <= textToMatch.Length)
                throw new ArgumentException();

            //var textToMatchHasSpecialOperatorCharacters = textToMatch.Any(IsSpecialCharacter);

            var matchedTextStartsWithSpecialCharacter = IsSpecialCharacter(textToMatch[0]);

            var matchedTextEndsWithSpecialCharacter = textToMatch.Length == 1 ?
                matchedTextStartsWithSpecialCharacter : IsSpecialCharacter(textToMatch[textToMatch.Length - 1]);

            var startIndex = 0;
            while (startIndex + textToMatch.Length <= textToSearch.Length)
            {
                var matchedIndexCurrent = textToSearch.IndexOf(textToMatch, startIndex, stringComparison);

                if (matchedIndexCurrent < 0)
                    return false;

                // Examples: 
                // textToMatch=":", textToSearch="test:2" (matched index is 4)
                // textToMatch="xyz", textToSearch="#xyz:" (matched index is 1)
                // textToMatch="xyz", textToSearch="axyz:" (matched index is -1)
                // textToMatch="xyz", textToSearch="#xyzA" (matched index is -1)
                // textToMatch="@xyz", textToSearch="a@xyz:t" (matched index is 1)
                // textToMatch="@xyz", textToSearch="a@xyzt" (matched index is -1)
                // textToMatch="xyz@", textToSearch="#xyz@t" (matched index is 1)
                // textToMatch="xyz@", textToSearch="axyz@t" (matched index is -1)
                var matchShouldBeIgnored = false;

                // If match is not at zero index, then either textToMatch should start with special character, or textToSearch should have
                // a special character before the matched index.
                if (matchedIndexCurrent > 0)
                {
                    if (conflictCheckType == ConflictCheckType.Identifier1IsContainedInIdentifier2AtZeroPosition)
                        return false;

                    if (!(matchedTextStartsWithSpecialCharacter || IsSpecialCharacter(textToSearch[matchedIndexCurrent - 1])))
                        matchShouldBeIgnored = true;
                }

                // If matched text is not ending the searched text, then either textToMatch should end with special character, or textToSearch should have
                // a special character after the matched text.
                if (matchedIndexCurrent + textToMatch.Length < textToSearch.Length)
                {
                    if (!(matchedTextEndsWithSpecialCharacter || IsSpecialCharacter(textToSearch[matchedIndexCurrent + textToMatch.Length])))
                        matchShouldBeIgnored = true;
                }

                if (!matchShouldBeIgnored)
                {
                    matchedIndex = matchedIndexCurrent;
                    break;
                }

                if (conflictCheckType == ConflictCheckType.Identifier1IsContainedInIdentifier2AtZeroPosition)
                    return false;

                // Lets try again
                startIndex = matchedIndexCurrent + textToMatch.Length;

                if (startIndex >= textToSearch.Length)
                    return false;

                /*if (textToMatchHasSpecialOperatorCharacters)
                {
                    matchedIndex = matchedIndexCurrent;
                    break;
                }

                if (matchedIndexCurrent > 0)
                {
                    if (conflictCheckType == ConflictCheckType.Identifier1IsContainedInIdentifier2AtZeroPosition)
                        return false;

                    if (IsSpecialCharacter(textToSearch[matchedIndexCurrent - 1]))
                    {
                        matchedIndex = matchedIndexCurrent;
                        break;
                    }
                }

                startIndex = matchedIndexCurrent + textToMatch.Length;

                if (startIndex >= textToSearch.Length)
                    break;

                if (matchedIndexCurrent == 0 && IsSpecialCharacter(textToSearch[startIndex]))
                {
                    matchedIndex = startIndex - textToMatch.Length;
                    break;
                }*/
            }

            if (matchedIndex >= 0)
            {
                if (generateErrorMessage)
                {
                    var errorMessageStringBuilder = new StringBuilder();
                    errorMessageStringBuilder.Append(
                        $"The value of {textToMatchName}=\"{textToMatch}\" is contained in {textToSearchName}=\"{textToSearch}\" at index {matchedIndex} as in '");

                    if (matchedIndex > 0)
                        errorMessageStringBuilder.Append(textToSearch.Substring(0, matchedIndex));

                    errorMessageStringBuilder.Append($"[{textToMatch}]");

                    var matchedTextEnd = matchedIndex + textToMatch.Length;

                    if (matchedTextEnd < textToSearch.Length)
                        errorMessageStringBuilder.Append(textToSearch.Substring(matchedTextEnd, textToSearch.Length - matchedTextEnd));

                    errorMessageStringBuilder.AppendLine("'.");

                    errorMessageStringBuilder.AppendLine($"The value of \"{textToMatchName}\" can be contained in \"{textToSearchName}\" only if the length of \"{textToMatchName}\" is smaller then the length of \"{textToSearchName}\" and one of the following is true:");
                    errorMessageStringBuilder.AppendLine($"-The occurrence of \"{textToMatchName}\" in \"{textToSearchName}\" is not at position 0 and is not preceded by any of the special characters [{string.Join(",", Helpers.SpecialCharacters)}].");
                    errorMessageStringBuilder.AppendLine($"\tValid example is: \"{textToMatchName}\"=\"bcd\", and \"{textToSearchName}\"=\"abcde\".");
                    errorMessageStringBuilder.AppendLine($"\tExample which will fail the validation is: \"{textToMatchName}\"=\"bcd\", and \"{textToSearchName}\"=\"a$bcd\".");

                    errorMessageStringBuilder.AppendLine($"-The occurrence of \"{textToMatchName}\" in \"{textToSearchName}\" is at position 0 and \"{textToMatchName}\" is not succeeded by any of the special characters [{string.Join(",", Helpers.SpecialCharacters)}] in \"{textToSearchName}\".");
                    errorMessageStringBuilder.AppendLine($"\tValid example is: \"{textToMatchName}\"=\"bcd\", and \"{textToSearchName}\"=\"bcde#\".");
                    errorMessageStringBuilder.AppendLine($"\tExample which will fail the validation is: \"{textToMatchName}\"=\"bcd\", and \"{textToSearchName}\"=\"bcd$e\".");
                    errorMessage = errorMessageStringBuilder.ToString();
                }

                return true;
            }


            return false;
        }

        internal static bool IsNullOrEmptyOrHasSpaceCharacters([CanBeNull] string text) =>
            string.IsNullOrWhiteSpace(text) || text.Any(char.IsWhiteSpace);

        internal static bool ValidateTextValueIsNotNulOrEmptyAndHasNoSpaces([CanBeNull] string validatedValue, [NotNull] string validatedValueName, out string errorMessage)
        {
            // ReSharper disable once ReplaceWithStringIsNullOrEmpty
            if (Helpers.IsNullOrEmptyOrHasSpaceCharacters(validatedValue))
            {
                errorMessage = $"The value of {validatedValueName} cannot be null or empty text, and cannot contain space characters (i.e., space, tab, line break). The invalid value is '{validatedValue}'.";
                return false;
            }

            errorMessage = null;
            return true;
        }
        internal static string GetPropertyName([NotNull] object objectWithProperty, [NotNull] string propertyName) =>
            GetPropertyName(objectWithProperty.GetType(), propertyName);

        internal static string GetPropertyName([NotNull] Type type, [NotNull] string propertyName) => $"[{type.FullName}.{propertyName}]";

        internal static void AddItemSorted<TTextItem>([NotNull] List<TTextItem> items, [NotNull] TTextItem textItem) where TTextItem : ITextItem
        {
            if (textItem.IndexInText < 0 || textItem.ItemLength == 0)
                throw new ArgumentException($"The value of '{typeof(ITextItem).FullName}.{nameof(ITextItem.IndexInText)}' cannot be negative and '{typeof(IExpressionItemBase).FullName}.{nameof(IExpressionItemBase.ItemLength)}' should be positive.", nameof(textItem));

            if (items.Count > 0)
            {
                var lastItem = items[items.Count - 1];
                if (textItem.IndexInText <= lastItem.IndexInText)
                    throw new ArgumentException($"Text items should be added in a sorted manner. In other words items with smaller value of '{typeof(ITextItem).FullName}.{nameof(ITextItem.IndexInText)}' should be added first.");
            }

            items.Add(textItem);
        }
    }
}
