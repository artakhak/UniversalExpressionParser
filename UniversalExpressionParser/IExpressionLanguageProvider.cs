// Copyright (c) UniversalExpressionParser Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using JetBrains.Annotations;
using System.Collections.Generic;
using TextParser;
using UniversalExpressionParser.ExpressionItems;
using UniversalExpressionParser.ExpressionItems.Custom;

namespace UniversalExpressionParser
{
    /// <summary>
    /// Defines the format of correct expressions that expression parser <see cref="IExpressionParser"/> will parse without errors.
    /// </summary>
    public interface IExpressionLanguageProvider
    {
        /// <summary>
        /// Language name that this expression language provider parses. Example SQL, C#, etc.
        /// This name is used to register expression language providers using method <see cref="IExpressionLanguageProviderCache.RegisterExpressionLanguageProvider(IExpressionLanguageProvider)"/>.
        /// The name should be passed to methods in <see cref="IExpressionParser"/> such as <see cref="IExpressionParser.ParseCodeBlockExpression(string, string, IParseExpressionOptions)"/>.
        /// The value of <see cref="LanguageName"/> should be unique for all the instances of <see cref="IExpressionLanguageProvider"/> registered using <see cref="IExpressionLanguageProviderCache.RegisterExpressionLanguageProvider(IExpressionLanguageProvider)"/>.
        /// </summary>
        [NotNull]
        string LanguageName { get; }

        /// <summary>
        /// Description of the expression language provider.
        /// </summary>
        [NotNull]
        string Description { get; }

        /// <summary>
        /// Text used to mark commented out line. Examples is "//".
        /// The value should be either null, or should be a text that does not contain spaces.
        /// Also, the value should not be the same as <see cref="MultilineCommentStartMarker"/>
        /// The comment marker preferably should start with special characters (e.g., //), since it makes the code more efficient,
        /// when differentiating between regular literals (function or variable names), and comment markers.
        /// The value cannot be null if <see cref="MultilineCommentStartMarker"/> is not null.
        /// </summary>
        [CanBeNull]
        string LineCommentMarker { get; }

        /// <summary>
        /// Text used to mark commented out code start. Examples is "/*".
        /// The value should be either null, or should be a text that does not contain spaces.
        /// The value cannot be null if <see cref="MultilineCommentEndMarker"/> is not null.
        /// The multi-line comment marker preferably should start with special characters (e.g., */), since it makes the code more efficient,
        /// when differentiating between regular literals (function or variable names), and comment markers.
        /// However, other values can be used such as "rem".
        /// </summary>
        [CanBeNull]
        string MultilineCommentStartMarker { get; }

        /// <summary>
        /// Text used to mark commented out code end. Examples is "*/".
        /// The value should be either null, or should be a text that does not contain spaces.
        /// The comment marker preferably should start with special characters (e.g., /*), since it makes the code more efficient,
        /// when differentiating between regular literals (function or variable names), and comment markers.
        /// The value cannot be null if <see cref="MultilineCommentStartMarker"/> is not null.
        /// However, other values can be used such as "remend".
        /// </summary>
        [CanBeNull]
        string MultilineCommentEndMarker { get; }

        /// <summary>
        /// A separator character to mark end of an expression. Most popular example is ";" used in number of languages.
        /// Use '\0' if the language does not support multiple expressions.
        /// The value cannot be '\0' if <see cref="CodeBlockStartMarker"/> and <see cref="CodeBlockEndMarker"/> are not null.
        /// </summary>
        char ExpressionSeparatorCharacter { get; }

        /// <summary>
        /// Marks the beginning of a code block. Example is "{" in C# or "begin" in Visual Basic.
        /// If <see cref="ExpressionSeparatorCharacter"/> is '\0', the value of <see cref="CodeBlockStartMarker"/> should be null. 
        /// If <see cref="CodeBlockEndMarker"/> is not null, the value of <see cref="CodeBlockStartMarker"/> should not be null.
        /// </summary>
        [CanBeNull]
        string CodeBlockStartMarker { get; }

        /// <summary>
        /// Marks the end of a code block. Example is "}" in C# or "begin" in Visual Basic.
        /// If <see cref="ExpressionSeparatorCharacter"/> is '\0', the value of <see cref="CodeBlockEndMarker"/> should be null. 
        /// If <see cref="CodeBlockStartMarker"/> is not null, the value of <see cref="CodeBlockEndMarker"/> should not be null.
        /// </summary>
        [CanBeNull]
        string CodeBlockEndMarker { get; }

        /// <summary>
        /// List of characters can be used for constant texts. Examples of characters that can be used in this list are "", ', `.
        /// The list can be empty if the language does not support constant texts.
        /// Any text that starts with some character in this list and ends with the ame character will be parsed to <see cref="IConstantTextExpressionItem"/>.
        /// Examples are "var x='Texts can include other text start/end markers, if the apostrophes are types twice. Examples: "", ``, ''.'"
        /// </summary>
        [NotNull]
        IReadOnlyList<char> ConstantTextStartEndMarkerCharacters { get; }

        /// <summary>
        /// List of all operators.
        /// The parser will pick the first operator in this list that matches. Therefore, the order is important.
        /// Consider the following three operators: "IS NULL", "IS NULL NOT", "IS NEGATIVE" and following expression.
        /// "X IS NULL NOT IS NEGATIVE".
        /// If the operators are sorted in this order: "IS NULL", "IS NULL NOT", "IS NEGATIVE"
        /// the parser will parse "X" to <see cref="ILiteralExpressionItem"/>,
        /// "IS NULL" to <see cref="IOperatorInfoExpressionItem"/>, will parse "NOT" to <see cref="ILiteralExpressionItem"/> , then will parse "IS NEGATIVE" to
        /// <see cref="IOperatorInfoExpressionItem"/>.
        /// However, if we add the operators in this order: "IS NULL NOT", "IS NULL", and "IS NEGATIVE", then "X" will be parsed to <see cref="ILiteralExpressionItem"/>
        /// "IS NULL NOT" will be parsed to <see cref="IOperatorInfoExpressionItem"/> and "IS NEGATIVE" will be parsed to another instance of <see cref="IOperatorInfoExpressionItem"/>.
        /// </summary>
        [NotNull, ItemNotNull]
        IReadOnlyList<IOperatorInfo> Operators { get; }

        /// <summary>
        /// List of possible keywords that can be used before expression items.
        /// Examples are "public", "private", "static", "const", "var", "out", "_", "ref", etc.
        /// Examples of this keywords in expressions are "private static var x =  getValue(ref _);".
        /// </summary>
        [NotNull, ItemNotNull]
        IReadOnlyList<ILanguageKeywordInfo> Keywords { get; }

        /// <summary>
        /// This function is executed by the parser <see cref="IExpressionParser"/>, when the parser
        /// tries to parse the expression at current position to <see cref="ILiteralExpressionItem"/>.
        /// Returns true, if a literal name (variable, function, constant, and other names, such as
        /// SQL column name, or "AND", "IS", "IS NOT" operators) can contain character <paramref name="character"/> at
        /// position <paramref name="positionInLiteral"/>. 
        /// Most languages (C#, Java, SQL, etc) allow alpha-numeric characters, underscore and dot '.' character in literals, and literals cannot start with
        /// number of '.'.
        /// See the default implementation of this method in <see cref="ExpressionLanguageProviderBase"/>.
        /// This method should either follow the same rules, or can allow other non standard characters in literals (for example literals that can start with '$').
        /// <see cref="IExpressionParser"/> calls this method to determine if character is part of literal name, if the expression at current position was not already parsed
        /// to some other expression item tyupe, such as <see cref="IOpeningBraceExpressionItem"/>, <see cref="IClosingBraceExpressionItem"/>,
        /// <see cref="IOperatorNamePartExpressionItem"/>, <see cref="ICommaExpressionItem"/>, etc.
        /// </summary>
        /// <param name="character">Character to check.</param>
        /// <param name="positionInLiteral">Position of character in name. For example assume "myOtherVar" is a valid literal in
        /// the parsed text is "var x myOtherVar;" and the parser is currently at position of character 'O'.
        /// In this example <paramref name="positionInLiteral"/> will be 2 (index of 'O' in "myOtherVar").
        /// In other words, <paramref name="positionInLiteral"/> is the offset of current position <see cref="ITextSymbolsParser"/>.PositionInText from the start position of symbol "myOtherVar"
        /// currently being parsed.
        /// On the other hand, the value of <paramref name="textSymbolsParserState"/>.PositionInText will be the actual position of 'O' from the start of
        /// the parsed text.
        /// </param>
        /// <param name="textSymbolsParserState">Contextual data of the parser that can be used to determine if current character is a valid literal character.
        /// Use methods and properties in <see cref="ITextSymbolsParserState"/> such as <see cref="ITextSymbolsParserState.TextToParse"/>,
        /// <see cref="ITextSymbolsParserState.PositionInText"/> to get contextual data.
        /// The value can be null, when call is made to validate <see cref="IExpressionLanguageProvider"/>. In all other cases the value is not null.
        /// </param>
        /// <returns>
        /// Returns true, if <paramref name="character"/> at current position is a valid literal character in name at position. Returns false otherwise.
        /// </returns>
        bool IsValidLiteralCharacter(char character, int positionInLiteral, [CanBeNull] ITextSymbolsParserState textSymbolsParserState);

        /// <summary>
        /// If true, language is case sensitive. Otherwise, the language is not case sensitive. For example if the value is false, and <see cref="Operators"/>
        /// contains an operator data for "IS NULL", then the expression "x is null" will be parsed to a literal "x" and operator "IS NULL", otherwise, if the value
        /// is true, the parser will fail to parse "is null" to operator <see cref="IOperatorInfoExpressionItem"/>.
        /// </summary>
        bool IsLanguageCaseSensitive { get; }

        /// <summary>
        /// List of custom expression item parsers. The parsers should be sorted by priority.
        /// The first parser that returns non-null value will be used to parse custom expression item.
        /// </summary>
        [NotNull, ItemNotNull]
        IEnumerable<ICustomExpressionItemParser> CustomExpressionItemParsers { get; }

        /// <summary>
        /// Implement this property to allow the parser to try parse numeric values.
        /// Note, the first regular expression in the first instance of <see cref="NumericTypeDescriptor"/> in
        /// <see cref="NumericTypeDescriptors"/> that succeeds will be used. Therefore, the items in <see cref="NumericTypeDescriptors"/>
        /// and the regular expressions in <see cref="NumericTypeDescriptor.RegularExpressions"/> should be sorted from more specific to more generic.
        /// For example the regular expression "^\d*[.]*\d*[E|e]{1,1}([+|-]?\d+)" for matching numbers like 1.5E+5 should be before the regular
        /// expression "^(\d*[.]\d+)|(\d+[.]\d*)|(\d+)" used for floating point or whole numbers like 5.3 or .5, .5, or 5
        /// </summary>
        IReadOnlyList<NumericTypeDescriptor> NumericTypeDescriptors { get; }

        ///// <summary>
        ///// Returns true, if <paramref name="parseErrorItemCode"/> if parsing cannot continue after this type of error occurs.
        ///// Otherwise, code parsing can continue.
        ///// </summary>
        ///// <param name="parseErrorItemCode">
        ///// Error code Id. The default parse errors can be found in <see cref="ParseErrorItemCode"/> class. The implementation can add new error Ids.
        ///// </param>
        //bool IsCriticalError(int parseErrorItemCode);

        /// <summary>
        /// If true is returned, unnamed round or square braces expressions will be interpreted as prefixes for an expression that immediately follows the unnamed
        /// braces expressions. Otherwise, and error of type <see cref="ParseErrorItemCode.NoSeparationBetweenSymbols"/> (or some other similar error) will be reported if
        /// multiple braces expressions are next to each other without separators between them (code separators such as ';', ',' or operators).
        /// Unnamed brace expressions are expressions that start and end with square or round braces and do not have any name that precede the expression.
        /// For example expressions "[NotNull, ItemNotNull]" or "(NotNull)" are unnamed brace expressions. On the other side "F[NotNull, ItemNotNull]"  or "F(NotNull)"
        /// are named brace expressions.<br/>
        /// Note, regardless of this property value, expressions that are parsed to custom expression of type <see cref="ICustomExpressionItem"/> for which
        /// <see cref="ICustomExpressionItem.CustomExpressionItemCategory"/> is <see cref="CustomExpressionItemCategory.Prefix"/> will be interpreted as
        /// prefixes to the most recently parsed expression.
        /// For example, when "[NotNull, ItemNotNull] custom_prefix CustomPrefix1 (MustUseReturnValue) F(x, y) => x+y;" is parsed, and <see cref="SupportsPrefixes"/> is true,
        /// then the list of expressions parsed from "[NotNull, ItemNotNull]", "custom_prefix CustomPrefix1", and "(MustUseReturnValue)" will be added
        /// as prefixes to an expression "F(x, y)". The prefixes expressions will be in list in property <see cref="IComplexExpressionItem.Prefixes"/> of expression item of type
        /// <see cref="IBracesExpressionItem"/> to which expression "F(x, y)" will be parsed.<br/>
        /// Otherwise, the parser will report errors that there is no separation between "[NotNull, ItemNotNull]" and "custom_prefix CustomPrefix1", and another error that there is no
        /// separation between expression "(MustUseReturnValue)" and "F(x, y)".<br/>
        /// Note, in this case, the custom prefix expression "custom_prefix CustomPrefix1" will be added as a prefix to "(MustUseReturnValue)", even though the value of <see cref="SupportsPrefixes"/>
        /// is false, since the property applies only to non-custom unnamed brace expressions.<br/>
        /// The implementation can return false for this property for simple languages that do not need a support for prefixes (say simple arithmetic operators parsing, etc),
        /// and the returned value can be true for more complex languages. For example C# like language might want to support method attributes.<br/>
        /// Normally, more complex languages can do additional processing of parsed expressions to do more fine tuned error checking and additional language specific processing and
        /// transformations of expressions.
        /// </summary>
        bool SupportsPrefixes { get; }

        /// <summary>
        /// If true is returned, expressions that are parsed to <see cref="IKeywordExpressionItem"/> using keywords defined in <see cref="Keywords"/> will be added
        /// to <see cref="IComplexExpressionItem.AppliedKeywords"/> of an expression item that is parsed from a symbol that that immediately follows the list of keywords.
        /// Otherwise, if none of custom parsers defined in <see cref="IExpressionLanguageProvider.CustomExpressionItemParsers"/> parses the keywords (and possibly some expressions that follows
        /// the keywords) to some custom expression of type <see cref="ICustomExpressionItem"/>, an error will be reported that keywords are used incorrectly.
        /// Lets consider the following example "public static class A; var x=4;", and lets suppose that keywords "public", "static", "class", and "var" are in <see cref="Keywords"/>.
        /// Also, lets suppose that the list <see cref="IExpressionLanguageProvider.CustomExpressionItemParsers"/> is empty.
        /// If the value of <see cref="SupportsKeywords"/> is true, then "A" will be parsed to expression of type <see cref="ILiteralExpressionItem"/>, and the keywords
        /// "public", "static", and "class" will be parsed to expressions of type <see cref="IKeywordExpressionItem"/> and will be added to <see cref="IComplexExpressionItem.AppliedKeywords"/>
        /// in <see cref="ILiteralExpressionItem"/> parsed from "A".
        /// Otherwise, if <see cref="SupportsKeywords"/> is false, an error of type <see cref="ParseErrorItemCode.InvalidUseOfKeywords"/> will be reported.
        /// Now lets suppose that <see cref="CustomExpressionItemParsers"/> contains a custom expression parser that parses any expression that is preceded with keyword
        /// "class" to a custom expression item of type <see cref="ICustomExpressionItem"/>. In this case the expression "public static class A" will be parsed to a custom expression, and
        /// no error will be reported, regardless of the value of <see cref="SupportsKeywords"/>
        /// </summary>
        bool SupportsKeywords { get; }
    }

    /// <summary>
    /// Extension methods for <see cref="IExpressionLanguageProvider"/>
    /// </summary>
    public static class ExpressionLanguageProviderExtensionMethods
    {
        private static void ThrowValidationException([NotNull] this IExpressionLanguageProvider expressionLanguageProvider,
                                                     string validationErrorMessage) =>
            throw new ExpressionLanguageProviderException(expressionLanguageProvider, validationErrorMessage);


        /// <summary>
        /// Returns true, if code separators in <see cref="IExpressionLanguageProvider.ExpressionSeparatorCharacter"/> can be used to use multiple expressions.
        /// Example of such an expression is "varx=y;++y;f1(x+y);"
        /// </summary>
        /// <param name="expressionLanguageProvider">Expression language provider.</param>
        public static bool SupportsMultipleExpressions([NotNull] this IExpressionLanguageProvider expressionLanguageProvider)
        {
            return expressionLanguageProvider.ExpressionSeparatorCharacter != '\0';
        }
    }
}