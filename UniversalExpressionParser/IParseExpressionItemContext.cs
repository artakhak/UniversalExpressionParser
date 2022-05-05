// Copyright (c) UniversalExpressionParser Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using JetBrains.Annotations;
using TextParser;
using UniversalExpressionParser.ExpressionItems;

namespace UniversalExpressionParser
{
    /// <summary>
    /// Parser context data.
    /// </summary>
    public interface IParseExpressionItemContext
    {
        /// <summary>
        /// Event that is raised when new error is added by parser.
        /// </summary>
        event ParseErrorAddedDelegate ParseErrorAddedEvent;

        /// <summary>
        /// An instance of <see cref="IExpressionLanguageProviderWrapper"/> currently used by parser.
        /// </summary>
        [NotNull]
        IExpressionLanguageProviderWrapper ExpressionLanguageProviderWrapper { get; }

        /// <summary>
        /// Pare expression result.
        /// </summary>
        [NotNull]
        IParseExpressionResult ParseExpressionResult { get; }

        /// <summary>
        /// Error data. Use <see cref="ParseExpressionItemContext.AddParseErrorItem"/> to add errors.
        /// Default implementation of <see cref="IParseErrorItem"/> is <see cref="ParseErrorItem"/>.
        /// </summary>
        [NotNull]
        IParseErrorData ParseErrorData { get; }

        /// <summary>
        /// Text symbols parser that can be used to parse the symbols at current position.
        /// </summary>
        [NotNull]
        ITextSymbolsParser TextSymbolsParser { get; }

        /// <summary>
        /// Returns true, if parsing was completed before reaching end of text due to some condition met, and not due to critical error encountered.
        /// </summary>
        bool IsEarlyParseStopEncountered { get; }

        /// <summary>
        /// Tries to parse braces if the text at current position starts with either '(' or '['.
        /// If parsing is successful, the position after parsing will be after ')' or ']'
        /// </summary>
        /// <param name="literalExpressionItem">If the parameter is not null, the braces expression item will have a
        /// value <see cref="IBracesExpressionItem.NameLiteral"/> equal to <paramref name="literalExpressionItem"/>.
        /// </param>
        /// <exception cref="ParseTextException">Throws this exception if the text at current position <see cref="ITextSymbolsParserState.PositionInText"/> in
        /// of property <see cref="TextSymbolsParser"/> is not '(' or '['
        /// </exception>
        [NotNull]
        IBracesExpressionItem ParseBracesExpression([CanBeNull] ILiteralExpressionItem literalExpressionItem);

        /// <summary>
        /// Tries to parse an expression at current position to <see cref="ICodeBlockExpressionItem"/>, if text at current position starts with
        /// <see cref="IExpressionLanguageProvider.CodeBlockStartMarker"/>.
        /// If parsing is successful, the position after parsing will be after the code block end marker <see cref="IExpressionLanguageProvider.CodeBlockEndMarker"/>
        /// </summary>
        /// <exception cref="ParseTextException">Throws this exception if the text at current position at current position <see cref="ITextSymbolsParserState.PositionInText"/> in
        /// of property <see cref="TextSymbolsParser"/> is not block start marker <see cref="IExpressionLanguageProvider.CodeBlockStartMarker"/>.
        /// </exception>
        [NotNull]
        ICodeBlockExpressionItem ParseCodeBlockExpression();

        /// <summary>
        /// Skips comments and spaces. Comments will be added to <see cref="IParseExpressionResult.SortedCommentedTextData"/>.
        /// </summary>
        /// <returns>Returns true, if text end is not reached (i.e. the value of <see cref="ITextSymbolsParserState.PositionInText"/> of <see cref="TextSymbolsParser"/> is not past the
        /// parsed text end). Returns false otherwise.</returns>
        bool SkipSpacesAndComments();

        /// <summary>
        /// Tries to parse a symbol at current position. For example if parsed text is "var var1=8;", and the value
        /// of <see cref="ITextSymbolsParserState.PositionInText"/> of <see cref="TextSymbolsParser"/> is 4 (index of "var1"), then
        /// this method will return true, and the value of <paramref name="parsedLiteral"/> will be set to "var1".
        /// </summary>
        /// <param name="parsedLiteral">Parsed symbol. The value is null, if the returned value is false. Otherwise the value is not null.</param>
        /// <returns>Returns true if valid literal was parsed. Returns false otherwise.</returns>
        bool TryParseSymbol(out string parsedLiteral);

        /// <summary>
        /// Adds an error data.
        /// </summary>
        /// <param name="parseErrorItem">Code item error data.</param>
        void AddParseErrorItem([NotNull] IParseErrorItem parseErrorItem);
    }

    /// <summary>
    /// Extension methods for <see cref="IParseExpressionItemContext"/>
    /// </summary>
    public static class ParseExpressionItemContextExtensionMethods
    {
        /// <summary>
        /// Returns true if parsed text in property <see cref="ITextSymbolsParserState.TextToParse"/> in <see cref="IParseExpressionItemContext.TextSymbolsParser"/>
        /// starts with symbol <paramref name="symbolToMatch"/>.
        /// For example if parsed text is "f(x) begin++x;end;" and <paramref name="symbolToMatch"/> is "begin" and property
        /// <see cref="ITextSymbolsParserState.PositionInText"/> in <see cref="IParseExpressionItemContext.TextSymbolsParser"/>
        /// is 5 (index of "begin"), then this method will return true. On the other hand if we change the parsed text is "f(x) beginy+x;end;", with all the other
        /// values being the same, the returned value will be false.
        /// </summary>
        /// <param name="parseExpressionItemContext">Pare expression context data.</param>
        /// <param name="symbolToMatch">Symbol to match.</param>
        public static bool StartsWithSymbol([NotNull] this IParseExpressionItemContext parseExpressionItemContext, string symbolToMatch)
        {
            var textSymbolsParser = parseExpressionItemContext.TextSymbolsParser;
            return Helpers.StartsWithSymbol(textSymbolsParser.TextToParse, symbolToMatch, textSymbolsParser.PositionInText, textSymbolsParser.ParsedTextEnd,
                parseExpressionItemContext.ExpressionLanguageProviderWrapper.ExpressionLanguageProvider);
        }

        /// <summary>
        /// Returns true if parsed text in property <see cref="ITextSymbolsParserState.TextToParse"/> in <see cref="IParseExpressionItemContext.TextSymbolsParser"/>
        /// starts with symbol <paramref name="symbolToMatch"/>.
        /// For example if parsed text is "f(x) begin++x;end;" and <paramref name="symbolToMatch"/> is "begin" and property
        /// <see cref="ITextSymbolsParserState.PositionInText"/> in <see cref="IParseExpressionItemContext.TextSymbolsParser"/>
        /// is 5 (index of "begin"), then this method will return true. On the other hand if we change the parsed text is "f(x) beginy+x;end;", with all the other
        /// values being the same, the returned value will be false.
        /// </summary>
        /// <param name="parseExpressionItemContext">Pare expression context data.</param>
        /// <param name="symbolToMatch">Symbol to match.</param>
        /// <param name="matchedSymbol">
        /// An output parameter for the matched symbol. If the returned value is true, the value is not null.
        /// Otherwise the value is null.<br/>
        /// If the value of <see cref="IExpressionLanguageProvider.IsLanguageCaseSensitive"/> is true, the value is the same as
        /// <paramref name="symbolToMatch"/>, otherwise, the value is equal to <paramref name="matchedSymbol"/> when case is ignored. 
        /// </param>
        public static bool StartsWithSymbol([NotNull] this IParseExpressionItemContext parseExpressionItemContext, string symbolToMatch, 
            out string matchedSymbol)
        {
            var textSymbolsParser = parseExpressionItemContext.TextSymbolsParser;
            return Helpers.StartsWithSymbol(textSymbolsParser.TextToParse, symbolToMatch, textSymbolsParser.PositionInText, textSymbolsParser.ParsedTextEnd,
                parseExpressionItemContext.ExpressionLanguageProviderWrapper.ExpressionLanguageProvider, null, out matchedSymbol);
        }

        /// <summary>
        /// Returns true if parsed text in property <see cref="ITextSymbolsParserState.TextToParse"/> in <see cref="IParseExpressionItemContext.TextSymbolsParser"/>
        /// starts with symbol <paramref name="symbolToMatch"/>.
        /// For example if parsed text is "f(x) begin++x;end;" and <paramref name="symbolToMatch"/> is "begin" and property
        /// <see cref="ITextSymbolsParserState.PositionInText"/> in <see cref="IParseExpressionItemContext.TextSymbolsParser"/>
        /// is 5 (index of "begin"), then this method will return true. On the other hand if we change the parsed text is "f(x) beginy+x;end;", with all the other
        /// values being the same, the returned value will be false.
        /// </summary>
        /// <param name="parseExpressionItemContext">Pare expression context data.</param>
        /// <param name="symbolToMatch">Symbol to match.</param>
        /// <param name="isValidTextAfterTextToMatch">A delegate that specifies if the text after the matched symbol is not part of the same symbol.
        /// For example if parsed text is "var var1+", <paramref name="symbolToMatch"/> is "var1", and property
        /// <see cref="ITextSymbolsParserState.PositionInText"/> in <see cref="IParseExpressionItemContext.TextSymbolsParser"/> is 4 (i.e., index of "var1"),
        /// then if this delegate returns true for character '+', then the method <see cref="StartsWithSymbol(IParseExpressionItemContext, string, IsValidTextAfterMatchedTextDelegate)"/>
        /// will return true, otherwise, the method call will return false.
        /// </param>
        public static bool StartsWithSymbol([NotNull] this IParseExpressionItemContext parseExpressionItemContext, string symbolToMatch,
                                      IsValidTextAfterMatchedTextDelegate isValidTextAfterTextToMatch)
        {
            var textSymbolsParser = parseExpressionItemContext.TextSymbolsParser;
            return Helpers.StartsWithSymbol(textSymbolsParser.TextToParse, symbolToMatch, textSymbolsParser.PositionInText, textSymbolsParser.ParsedTextEnd,
                                        parseExpressionItemContext.ExpressionLanguageProviderWrapper.ExpressionLanguageProvider, isValidTextAfterTextToMatch);
        }

        /// <summary>
        /// Returns true if parsed text in property <see cref="ITextSymbolsParserState.TextToParse"/> in <see cref="IParseExpressionItemContext.TextSymbolsParser"/>
        /// starts with symbol <paramref name="symbolToMatch"/>.
        /// For example if parsed text is "f(x) begin++x;end;" and <paramref name="symbolToMatch"/> is "begin" and property
        /// <see cref="ITextSymbolsParserState.PositionInText"/> in <see cref="IParseExpressionItemContext.TextSymbolsParser"/>
        /// is 5 (index of "begin"), then this method will return true. On the other hand if we change the parsed text is "f(x) beginy+x;end;", with all the other
        /// values being the same, the returned value will be false.
        /// </summary>
        /// <param name="parseExpressionItemContext">Pare expression context data.</param>
        /// <param name="symbolToMatch">Symbol to match.</param>
        /// <param name="isValidTextAfterTextToMatch">A delegate that specifies if the text after the matched symbol is not part of the same symbol.
        /// For example if parsed text is "var var1+", <paramref name="symbolToMatch"/> is "var1", and property
        /// <see cref="ITextSymbolsParserState.PositionInText"/> in <see cref="IParseExpressionItemContext.TextSymbolsParser"/> is 4 (i.e., index of "var1"),
        /// then if this delegate returns true for character '+', then the method <see cref="StartsWithSymbol(IParseExpressionItemContext, string, IsValidTextAfterMatchedTextDelegate)"/>
        /// will return true, otherwise, the method call will return false.
        /// </param>
        /// <param name="matchedSymbol">
        /// An output parameter for the matched symbol. If the returned value is true, the value is not null.
        /// Otherwise the value is null.<br/>
        /// If the value of <see cref="IExpressionLanguageProvider.IsLanguageCaseSensitive"/> is true, the value is the same as
        /// <paramref name="symbolToMatch"/>, otherwise, the value is equal to <paramref name="matchedSymbol"/> when case is ignored. 
        /// </param>
        public static bool StartsWithSymbol([NotNull] this IParseExpressionItemContext parseExpressionItemContext, string symbolToMatch,
                                      IsValidTextAfterMatchedTextDelegate isValidTextAfterTextToMatch, out string matchedSymbol)
        {
            var textSymbolsParser = parseExpressionItemContext.TextSymbolsParser;
            return Helpers.StartsWithSymbol(textSymbolsParser.TextToParse, symbolToMatch, textSymbolsParser.PositionInText, textSymbolsParser.ParsedTextEnd,
                parseExpressionItemContext.ExpressionLanguageProviderWrapper.ExpressionLanguageProvider, isValidTextAfterTextToMatch, out matchedSymbol);
        }
    }
}