// Copyright (c) UniversalExpressionParser Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using JetBrains.Annotations;

namespace UniversalExpressionParser.ExpressionItems.Custom
{
    /// <summary>
    /// Custom expression item parsed from expression by one of custom expression item parser of type <see cref="IExpressionParser"/> in <see cref="IExpressionLanguageProvider.CustomExpressionItemParsers"/>.
    /// The easiest way to implement this is to subclass from <see cref="CustomExpressionItem"/>.
    /// Custom expression item parsed by custom expression item parser <see cref="ICustomExpressionItemParser"/>. <br/>
    /// For example in expression "::types[T1,T2] F1(x:T1, y:T2) where T1:int where T2:double whereend" one of custom expression parsers might parse text "::types[T1,T2]" as a prefix custom expression item to be added to <see cref="IComplexExpressionItem.Prefixes"/> in expression of type <see cref="IBracesExpressionItem"/> parsed from "F1(x:T1, y:T2)".
    /// Also, some other custom expression parser might parse text "where T1:int where T2:double whereend" as a postfix custom expression item to be added to <see cref="IComplexExpressionItem.Postfixes"/> in expression of type <see cref="IBracesExpressionItem"/> parsed from "F1(x:T1, y:T2)"
    /// Another example is "::pragma x+y". In this example one of custom expression parser might parse "::pragma x" as a regular custom expression item (i.e., expression item for which <see cref="ICustomExpressionItem.CustomExpressionItemCategory"/> is <see cref="UniversalExpressionParser.ExpressionItems.Custom.CustomExpressionItemCategory.Regular"/>) which will be one of the operands in binary operator "+".
    /// </summary>
    public interface ICustomExpressionItem: IComplexExpressionItem
    {
        /// <summary>
        /// Custom expression category, such as <see cref="UniversalExpressionParser.ExpressionItems.Custom.CustomExpressionItemCategory.Prefix"/>, <see cref="UniversalExpressionParser.ExpressionItems.Custom.CustomExpressionItemCategory.Regular"/>, or <see cref="UniversalExpressionParser.ExpressionItems.Custom.CustomExpressionItemCategory.Postfix"/>.
        /// </summary>
        CustomExpressionItemCategory CustomExpressionItemCategory { get; }

        /// <summary>
        /// Checks if an expression item <paramref name="postfixExpressionItem"/> can be used as a postfix for this expression item.
        /// If false is returned, the parser will add an error, if the <paramref name="postfixExpressionItem"/> will be used as a postfix for this custom expression item.
        /// </summary>
        /// <param name="postfixExpressionItem">Postfix expression item to check.</param>
        /// <param name="customErrorMessage">
        /// Is set to custom error message if the returned value is false. In this case, if the output parameter is null, the parer will default to a generic error message.
        /// Otherwise, if the returned value is true, the value of output parameter is null.
        /// </param>
        bool IsValidPostfix([NotNull] IExpressionItemBase postfixExpressionItem, out string customErrorMessage);

        /// <summary>
        /// Method is called by the parser, when the custom expression item is parsed, and prefixes and keywords are added (if any).
        /// The implementation should not throw an exception and should add errors using <see cref="IParseExpressionItemContext.AddParseErrorItem"/> if necessary,
        /// that will be checked by <see cref="IExpressionParser"/>. If for any of the added errors the value <see cref="IParseErrorItem.ParseErrorItemCode"/>, the call to <see cref="IExpressionLanguageProvider.IsValidLiteralCharacter(char, int, TextParser.ITextSymbolsParserState)"/> is true, the parser will stop parsing the rest of the expression.
        /// </summary>
        /// <param name="context">An instance of <see cref="IParseExpressionItemContext"/> that contains context data.</param>
        void OnCustomExpressionItemParsed([NotNull] IParseExpressionItemContext context);

        /// <summary>
        /// When <see cref="ICustomExpressionItemParser"/> parses an expression into <see cref="ICustomExpressionItem"/>, the <see cref="IExpressionParser"/>
        /// might report errors, if the custom expression item is invalid in current context (for example an error of
        /// type see <see cref="ParseErrorItemCode.CustomPostfixExpressionItemAfterNonRegularExpressionItem"/>).
        /// If that happens, the <see cref="IExpressionParser"/> determines the position to report the error at.
        /// Normally, one would expect that the value of property <see cref="ITextItem.IndexInText"/> of <see cref="ICustomExpressionItem"/> could be used.
        /// However, the value of <see cref="ITextItem.IndexInText"/> might be misleading, if the custom expression
        /// item has prefixes, or multiple keywords. For example in this expression "[Prefix1] keyword1 keyword2 MyCustomExpression"
        /// the <see cref="ICustomExpressionItemParser"/> might chose to parse an expression MyCustomExpression as custom expression by evaluating keyword keyword2.
        /// In this case, prefix [Prefix1], and keywords keyword1 and keyword2 might be passed to the parsed custom expression, and the
        /// value of property <see cref="ITextItem.IndexInText"/> in <see cref="ICustomExpressionItem"/> will be the position of prefix [Prefix1] in parsed text.
        /// In this case reporting error position as <see cref="ITextItem.IndexInText"/> as <see cref="IParseErrorItem.ErrorIndexInParsedText"/>
        /// will be confusing, and the the implementation of <see cref="ICustomExpressionItem.ErrorsPositionDisplayValue"/> might chose to return
        /// the index of keyword "keyword2" or the index of text "MyCustomExpression".
        /// If the property value is null, <see cref="IExpressionParser"/> will determine the position of error based on context data.
        /// </summary>
        int? ErrorsPositionDisplayValue { get; }
    }
}