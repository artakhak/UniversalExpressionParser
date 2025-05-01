// Copyright (c) UniversalExpressionParser Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using System;
using JetBrains.Annotations;

namespace UniversalExpressionParser.Parser
{
    internal static class ExpressionParserMessages
    {
        [NotNull]
        internal const string InvalidSymbolError = "Invalid symbol.";

        [NotNull]
        internal const string InvalidStartTextWhenParsingBracesError = "When parsing braces it is expected that the character at current position is either '(' or '['.";

        [NotNull]
        internal const string InvalidStartTextWhenParsingCodeBlockError = "When parsing code block it is expected that the character at current position is '{0}'.";

        [NotNull]
        internal const string CustomPrefixExpressionHasNoTargetError = "No target expression for this custom prefix expression.";

        [NotNull]
        internal const string CustomPostfixIsInvalidError = "Invalid symbol in current context.";

        [NotNull]
        internal const string CodeBlockCannotFollowThePrecedingExpressionError = "Code block cannot follow the preceding expression.";

        //[NotNull]
        //internal const string MultipleOccurrencesOfKeywordError = "Multiple occurrences of the same keyword. Example of this error is \"f1(ref ref x);\".";

        [NotNull]
        internal const string InvalidUseOfKeywordsError = "Keywords cannot be used in this context.";

        [NotNull]
        internal const string KeywordsNotSupportedError = "Keywords can be used only as part of custom expressions.";

        [NotNull]
        internal const string InvalidUseOfKeywordsBeforeOperatorsError = "Keywords cannot be used before operators.";

        [NotNull]
        internal static string InvalidUseOfKeywordsBeforeCommaError = string.Concat(
            "Keywords cannot be used before operators, unless the keywords are part of custom expression.", Environment.NewLine,
            "Example when this error might happen is \"F(x kwd1, y)\".");

        [NotNull]
        internal static string InvalidUseOfKeywordsBeforeClosingBracesError = string.Concat(
            "Keywords cannot be used before closing round or square braes, unless the keywords are part of custom expression.", Environment.NewLine,
            "Example when this error might happen is \"F(x kwd1)\" or \"x[i kwd]\".");

        [NotNull]
        internal static string InvalidUseOfKeywordsBeforeCodeSeparatorError = string.Concat(
            "Keywords cannot be used before code separators, unless the keywords are part of custom expression.", Environment.NewLine,
            "Example when this error might happen is \"x += y kwd1;\".");

        [NotNull]
        internal static string InvalidUseOfKeywordsBeforeCodeBlockEndMarkerError = string.Concat(
            "Keywords cannot be used before code block end markers, unless the keywords are part of custom expression.", Environment.NewLine,
            "Example when this error might happen is \"{{x += y kwd1;}}\".");

        [NotNull]
        internal const string BinaryOperatorMissingError = "Binary operator is missing.";

        [NotNull]
        internal const string ExpectedPostfixOperatorError = "Expected a postfix operator.";

        [NotNull]
        internal const string ExpectedPrefixOperatorError = "Expected a prefix operator.";

        [NotNull]
        internal static readonly string NoSeparationBetweenSymbolsError =
            String.Concat("No separation between two symbols. Here are some invalid expressions that would result in this error, and correct expressions resulted from invalid expressions by small correction.", Environment.NewLine,
            "Invalid expression: \"F(x y)\". Fixed valid expressions: \"F(x + y)\", \"F(x, y)\".", Environment.NewLine,
            "Invalid expression: \"{x=F1(x) F2(y)}\". Fixed valid expressions: \"{x=F1(x) + F2(y)}\", \"{x=F1(x); F2(y)}\".");

        //[NotNull]
        //internal static readonly string OperatorMisuseError =
        //    string.Concat("Operator is not used correctly. Examples are using binary operator as a prefix operator.", System.Environment.NewLine,
        //    "The following usage of binary operator \"+=\" will result in this error: \"+=z+y\".",
        //    "The error in this example might be fixed by changing the expression to \"x+=z+y+\"");

        [NotNull]
        internal const string ExpressionMissingBeforeCommaError = "Valid expression is missing before comma.";

        [NotNull]
        internal const string ExpressionMissingAfterCommaError = "Valid expression is missing after comma.";
    }
}
