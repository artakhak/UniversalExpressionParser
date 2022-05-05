// Copyright (c) UniversalExpressionParser Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using UniversalExpressionParser.ExpressionItems;
using UniversalExpressionParser.ExpressionItems.Custom;

namespace UniversalExpressionParser
{
    // TODO: The code docs should be fine, but go over them again later.
    /// <summary>
    /// Error codes.
    /// </summary>
    public static class ParseErrorItemCode
    {
        /// <summary>"!"
        /// Example: "x+y ф", where 'ф' i not valid literal based on implementation of <see cref="IExpressionLanguageProvider.IsValidLiteralCharacter(char, int, TextParser.ITextSymbolsParserState)"/> . In this example, 'ф'  will result in error.
        /// </summary>
        public const int InvalidSymbol = 0;

        /// <summary>
        /// These type of errors should never happen. The error is indicative of parser implementation error.
        /// </summary>
        public const int ParserImplementationError = 1;

        /// <summary>
        /// Custom expression item has <see cref="ICustomExpressionItem.CustomExpressionItemCategory"/>==<see cref="CustomExpressionItemCategory.Postfix"/>,
        /// and the preceding expression item is a custom expression item for which
        /// <see cref="ICustomExpressionItem.CustomExpressionItemCategory"/>!=<see cref="CustomExpressionItemCategory.Regular"/>
        /// </summary>
        public const int CustomPostfixExpressionItemAfterNonRegularExpressionItem = 100;

        /// <summary>
        /// Custom expression item has <see cref="ICustomExpressionItem.CustomExpressionItemCategory"/>==<see cref="CustomExpressionItemCategory.Postfix"/>,
        /// and the preceding expression item is a custom expression item has
        /// <see cref="ICustomExpressionItem.CustomExpressionItemCategory"/>==<see cref="CustomExpressionItemCategory.Regular"/>,
        /// however the preceding custom expression rejected the postfix custom expression item (i.e., the call to
        /// <see cref="ICustomExpressionItem.IsValidPostfix(IExpressionItemBase, out string)"/> returned false).
        /// </summary>
        public const int CustomPostfixExpressionItemRejectedByPrecedingCustomExpressionItem = 101;

        /// <summary>
        /// Custom expression item has <see cref="ICustomExpressionItem.CustomExpressionItemCategory"/>==<see cref="CustomExpressionItemCategory.Postfix"/>,
        /// and the preceding expression item is not valid item to apply postfix to.
        /// Custom expression with <see cref="ICustomExpressionItem.CustomExpressionItemCategory"/>==<see cref="CustomExpressionItemCategory.Postfix"/> can follow only
        /// braces (i.e., [], ()), or a literal (e.g., MyVarName, etc), or another custom expression for which
        /// <see cref="ICustomExpressionItem.CustomExpressionItemCategory"/>==<see cref="CustomExpressionItemCategory.Regular"/>.
        /// For example this expression will generate error <see cref="CustomPostfixExpressionItemHasNoTargetExpression"/>: "15 postfix A", where "postfix A" is parsed
        /// to a postfix expression. This error might be fixed by changing the expression to "x15 postfix A".
        /// </summary>
        public const int CustomPostfixExpressionItemFollowsInvalidExpression = 102;

        /// <summary>
        /// Custom expression item has <see cref="ICustomExpressionItem.CustomExpressionItemCategory"/>==<see cref="CustomExpressionItemCategory.Postfix"/>,
        /// and the preceding symbol is not valid item to apply postfix to.
        /// Custom expression with <see cref="ICustomExpressionItem.CustomExpressionItemCategory"/>==<see cref="CustomExpressionItemCategory.Postfix"/> can follow only
        /// braces (i.e., [], ()), or a literal (e.g., MyVarName, etc), or another custom expression for which
        /// <see cref="ICustomExpressionItem.CustomExpressionItemCategory"/>==<see cref="CustomExpressionItemCategory.Regular"/>
        /// For example this expression will generate error <see cref="CustomPostfixExpressionItemHasNoTargetExpression"/>: "x+y; postfix A", where "postfix A" is parsed
        /// to a postfix expression. This error might be fixed by changing the expression to "x+y postfix A".
        /// </summary>
        public const int CustomPostfixExpressionItemHasNoTargetExpression = 103;

        /// <summary>
        /// This error is reported when the value of <see cref="ITextItem.IndexInText"/> in parsed custom expression
        /// item is incorrect. The parsing will always stop on this error (the error is considered critical), since invalid position might result in continuous loop in
        /// parser, if the parser continues parsing after thi error.
        /// </summary>
        public const int ParsedCustomExpressionItemHasInvalidIndex = 105;

        /// <summary>
        /// This error is reported when the value of <see cref="ITextItem.ItemLength"/> in parsed custom expression
        /// item is 0 or negative. The parsing will always stop on this error (the error is considered critical), since invalid position might result in continuous loop in
        /// parser, if the parser continues parsing after thi error.
        /// </summary>
        public const int ParsedCustomExpressionItemHasNonPositiveLength = 106;

        /// <summary>
        /// Call to <see cref="ICustomExpressionItemParser.TryParseCustomExpressionItem(IParseExpressionItemContext, System.Collections.Generic.IReadOnlyList{IExpressionItemBase}, System.Collections.Generic.IReadOnlyList{IKeywordExpressionItem})"/>
        /// thew an exception.
        /// </summary>
        public const int CustomExpressionParserThrewAnException = 107;

        /// <summary>
        /// No separation between two symbols. Here are some invalid expressions that would result in this error, and expressions resulted from invalid expressions by small correction.
        /// Invalid expression: "F(x y)". Fixed valid expressions: "F(x + y)", "F(x, y)".
        /// Invalid expression: "{x=F1(x) F2(y)}". Fixed valid expressions: "{x=F1(x) + F2(y)}", "{x=F1(x); F2(y)}".
        /// </summary>
        public const int NoSeparationBetweenSymbols = 200;

        /// <summary>
        /// A code block closing marker without corresponding code block opening marker. Example
        /// var i = 0;
        /// {
        ///     println(i);
        /// }
        /// }
        /// 
        /// </summary>
        public const int CodeBlockClosingMarkerWithoutOpeningMarker = 250;

        /// <summary>
        /// Example "F1(x,y))" or "x+y)"
        /// </summary>
        public const int ClosingBraceWithoutOpeningBrace = 300;

        /// <summary>
        /// Comma characters can be only used to separate items within braces.
        /// Valid examples of comma usage are F1(x,y+x) or [1, y+z*i].
        /// Invalid examples are Example "x+y,z"
        /// </summary>
        public const int CommaWithoutParentBraces = 350;

        /// <summary>
        /// Indicates that the code block starting marker is used inappropriately.
        /// Code blocks can be preceded by one of the following symbols. In other cases parsing will will result in an error:
        /// -Operator. Example: "(x, y) => { return x+y;}"
        /// -Another code block. Example: "{ ++x;} { y=x+1; }"
        /// -Code separator symbol. Example: "{ ++x; {++y;}}"
        /// -keyword expressions. Example "keyword1 { ++x; }"
        /// -Expression that one of custom expression parsers <see cref="ICustomExpressionItemParser"/> evaluates to custom expression item
        /// of type <see cref="ICustomExpressionItem"/> for which <see cref="ICustomExpressionItem.CustomExpressionItemCategory"/> is
        /// <see cref="CustomExpressionItemCategory.Prefix"/>. Example: "namedprefix Prefix1 {++x;}". In this example the <see cref="ICustomExpressionItemParser"/> parses "namedprefix Prefix1" to
        /// custom prefix expression item.
        /// -Expression that one of custom expression parsers <see cref="ICustomExpressionItemParser"/> evaluates to custom expression item
        /// of type <see cref="ICustomExpressionItem"/> for which <see cref="ICustomExpressionItem.CustomExpressionItemCategory"/> is
        /// <see cref="CustomExpressionItemCategory.Regular"/>, and <see cref="ICustomExpressionItem.IsValidPostfix(IExpressionItemBase, out string)"/> returns true for the code block expression item.
        /// Example: "debug {++x}". In this example "debug" is evaluated to <see cref="ICustomExpressionItem"/>, and <see cref="ICustomExpressionItem.IsValidPostfix(IExpressionItemBase, out string)"/> returns true
        /// for code block expression parsed from "{++x}".
        /// -Code block is preceded by a literal or round or square braces (both named, or unnamed).
        /// Examples: "A {++x;}", "A[x, y] {++x;}", "A(x, y) {++x;}", "A[x, y] {++x;}", "A[x, y] {++x;}", "A[x, y] {++x;}"
        /// </summary>
        public const int InvalidCodeBlock = 400;

        /// <summary>
        /// Code block is preceded by an expression that evaluates to <see cref="ICustomExpressionItem"/> by one of the provided <see cref="ICustomExpressionItemParser"/>
        /// for which <see cref="ICustomExpressionItem.CustomExpressionItemCategory"/> is <see cref="CustomExpressionItemCategory.Regular"/>,
        /// and <see cref="ICustomExpressionItem.IsValidPostfix(IExpressionItemBase, out string)"/> returns false for the expression parsed from the code block.
        /// Example: "custom_named_expression ExprName1 {++x;}".
        /// In this example the <see cref="ICustomExpressionItem"/> parses "custom_named_expression ExprName1" to
        /// custom prefix expression item which fails the validation of the code block that follows it.
        /// </summary>
        public const int CodeBlockRejectedByOwnerCustomExpression = 401;

        /// <summary>
        /// Code block is preceded by an expression that evaluates to <see cref="ICustomExpressionItem"/> by one of the provided <see cref="ICustomExpressionItemParser"/>
        /// for which <see cref="ICustomExpressionItem.CustomExpressionItemCategory"/> is <see cref="CustomExpressionItemCategory.Regular"/>,
        /// and <see cref="ICustomExpressionItem.IsValidPostfix(IExpressionItemBase, out string)"/> returns false for the expression parsed from the code block.
        /// Example: "custom_named_expression ExprName1 {++x;}".
        /// In this example the <see cref="ICustomExpressionItem"/> parses "custom_named_expression ExprName1" to
        /// custom prefix expression item which fails the validation of the code block that follows it.
        /// </summary>
        public const int CodeBlockUsedAfterPostfixCustomExpression = 402;

        /// <summary>
        /// Example of invalid usage of separator ';': "f1(x;y)".
        /// Example of valid usage of separator: {var x=5;y=3;}
        /// </summary>
        public const int ExpressionSeparatorWithoutParentCodeBlock = 403;

        /// <summary>
        /// Example f1(x,y; var x=1;
        /// </summary>
        public const int ClosingBraceMissing = 404;

        /// <summary>
        /// Example f1(x,y]
        /// </summary>
        public const int ClosingBraceDoesNotMatchOpeningBrace = 405;

        /// <summary>
        /// Example { x =y;
        /// </summary>
        public const int CodeBlockEndMarkerMissing = 406;


        /// <summary>
        /// Keywords can be used only as part of custom expressions. The value of <see cref="IExpressionLanguageProvider.SupportsKeywords"/>
        /// should be true to support keywords with non-custom expressions.
        /// </summary>
        public const int KeywordsNotSupported = 450;

        /// <summary>
        /// Multiple occurrences of the same keyword. Example f1(ref ref x); or public public var x;
        /// </summary>
        public const int MultipleOccurrencesOfKeyword = 451;

        /// <summary>
        /// Keywords are used in wrong context.
        /// </summary>
        public const int InvalidUseOfKeywords = 452;

        /// <summary>
        /// Keywords are used before an operator.
        /// Keywords can be used before operators only if they are are parsed to a custom expression by one of parsers in <see cref="IExpressionLanguageProvider.CustomExpressionItemParsers"/>.
        /// Example when this error will be reported "kwd1 ++x" when "kwd1" is not used by any parsers in <see cref="IExpressionLanguageProvider.CustomExpressionItemParsers"/> to parse to a
        /// custom expression item.
        /// On the other hand, this expression will be successfully parsed: "parse_me_to_value ++x", if some parser in <see cref="IExpressionLanguageProvider.CustomExpressionItemParsers"/>
        /// parses "parse_me_to_value" keyword to some value.
        /// </summary>
        public const int InvalidUseOfKeywordsBeforeOperators = 453;

        /// <summary>
        /// Keywords cannot be used before operators, unless the keywords are part of custom expression.
        /// Example when this error might happen is "F(x kwd1, y)".
        /// </summary>
        public const int InvalidUseOfKeywordsBeforeComma = 454;

        /// <summary>
        /// Keywords cannot be used before closing round or square braes, unless the keywords are part of custom expression.
        /// Example when this error might happen is "F(x kwd1)" or "x[i kwd]".
        /// </summary>
        public const int InvalidUseOfKeywordsBeforeClosingBraces = 455;

        /// <summary>
        /// Keywords cannot be used before code separators, unless the keywords are part of custom expression.
        /// Example when this error might happen is "x+=y kwd1;".
        /// </summary>
        public const int InvalidUseOfKeywordsBeforeCodeSeparator = 456;

        /// <summary>
        /// Keywords cannot be used before code block end markers, unless the keywords are part of custom expression.
        /// Example when this error might happen is "{x+=y kwd1;}".
        /// </summary>
        public const int InvalidUseOfKeywordsBeforeCodeBlockEndMarker = 457;

        ///// <summary>
        ///// Operator is not used correctly. Examples are using binary operator as a prefix operator,
        ///// or the parser not being able to determine if the operator should be used as binary or postfix operator in current
        ///// context.
        ///// The following usage of binary operator "+=" will result in this error: "+=y+z".
        ///// The error in this example might be fixed by changing the expression to "x+=y+z". 
        ///// </summary>
        //public const int OperatorMisuse = 500;

        ////// <summary>
        ///// Expects binary operator between operands.
        ///// For example the following expression will result in this error: "x++y". The expression can be corrected by modifying it to "x++ +y".
        ///// </summary>
        //public const int ExpectedBinaryOperator = 501;

        /// <summary>
        /// Binary operator missing between operands.
        /// Example when this error might happen is in this expression: "x-- ++y" or "x y".
        /// This expression can be corrected by adding a binary operator between operands, such as in "x-- + ++y".
        /// </summary>
        public const int BinaryOperatorMissing = 501;

        /// <summary>
        /// Expected a prefix unary operator.
        /// For example this error might happen in expression: "F(*x)", where '*' is a binary multiplication operator.
        /// This expression can be corrected by changing the expression to "F(++x)".
        /// </summary>
        public const int ExpectedPrefixOperator = 502;

        /// <summary>
        /// Expected a postfix unary operator.
        /// For example this error might happen in expression: "F(x+)".
        /// This expression can be corrected by changing the expression to "F(x++)".
        /// </summary>
        public const int ExpectedPostfixOperator = 503;

        /// <summary>
        /// Prefixes are used are used in wrong context.
        /// </summary>
        public const int InvalidUseOfPrefixes = 600;

        ///// <summary>
        ///// Prefixes cannot be followed by an operator.
        ///// For example if the implementation of <see cref="IExpressionLanguageProvider"/> parses MyCustomClassPrefix to be a prefix
        ///// that can be used in class definitions, this prefix cannot be followed by an operator, and should be followed by non-operator expression item.
        ///// Here is a valid example usage of this prefix:
        ///// public stat class MyCustomClassPrefix Class1 {}
        ///// Here is an invalid usage of the prefix:
        ///// public stat class MyCustomClassPrefix + Class1 {}
        ///// </summary>
        //public const int PrefixFollowedByOperator = 170;

        /// <summary>
        /// Example of this error "var x = 10; /* this is an unclosed comment".
        /// Example when the comment is properly closed: "var x = 10; /* this is a closed comment*/".
        /// </summary>
        public const int CommentNotClosed = 650;

        /// <summary>
        /// Example of this error: "var myText='This is a text that is not closed".
        /// Example when the text is valid:  "var myText='This is a text that is properly closed'".
        /// </summary>
        public const int ConstantTextNotClosed = 651;

        /// <summary>
        /// Example of this error is F1(x,)
        /// </summary>
        public const int ExpressionMissingBeforeComma = 700;

        /// <summary>
        /// Example of this error is F1(,x)
        /// </summary>
        public const int ExpressionMissingAfterComma = 701;

        /// <summary>
        /// Examples of this error are "{;" or "{var x = 3;;"
        /// </summary>
        public const int ExpressionMissingBeforeCodeItemSeparator = 702;

        ///// <summary>
        ///// Examples of this error are F1(x, y;z). ',' is expected instead of code separator';'
        ///// </summary>
        //CodeItemSeparatorUSedInInvalidContext,

        /// <summary>
        /// The regular expression provided in <see cref="NumericTypeDescriptor.RegularExpressions"/> in
        /// <see cref="IExpressionLanguageProvider.NumericTypeDescriptors"/> is invalid.
        /// </summary>
        public const int InvalidRegularExpressionForNumericValueParsing = 750;
    }
}