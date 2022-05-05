// Copyright (c) UniversalExpressionParser Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using System.Collections.Generic;
using JetBrains.Annotations;

namespace UniversalExpressionParser.ExpressionItems
{
    /// <summary>
    /// Expression item parsed from expression that starts with a name followed by round or square braces, or from an expression that starts and end with round or square braces (nameless braces expression).
    /// Examples of expressions that will be parsed to this type of expression are "(y+z)" in "x*(y+z)",
    /// or "(#x)" in "!(#x)", "[x1, 1, y+3]", "Func1(x, 2)", "matrix1[0, i]".
    /// </summary>
    public interface IBracesExpressionItem : ICanAddChildExpressionItem
    {
        /// <summary>
        /// Expression parsed from the name that appears before the braces. Example of expression that will be parsed to <see cref="NameLiteral"/> is "F1" in "F1(x,y)", or "Matrix1" in "Matrix1[i, j]".
        /// The value can be null, which indicates that there is no name before braces. Examples are "var x=[1, y, 5]", or "(x, y) => 5";
        /// </summary>
        [CanBeNull]
        ILiteralExpressionItem NameLiteral { get; }

        /// <summary>
        /// Expression item parsed from the opening brace info.
        /// </summary>
        [NotNull]
        IOpeningBraceExpressionItem OpeningBrace { get; }

        /// <summary>
        /// Expression item parsed from the closing brace info. The value is null if the closing brace is missing.
        /// </summary>
        [CanBeNull]
        IClosingBraceExpressionItem ClosingBrace { get; }

        /// <summary>
        /// Collection of expression items parsed from comma separated expressions. The list of expression items might contain null values if the parameter expressions are missing, such as in this expression "F1(,x1,,x3+1)".
        /// In this example the collection will have null for the first parameter, an expression item of type <see cref="ILiteralExpressionItem"/> for the second parameter "x1", null for the third parameter, and expression item of type <see cref="IOperatorExpressionItem"/> for binary operator "x3+1".
        /// </summary>
        [NotNull, ItemCanBeNull]
        IReadOnlyList<IExpressionItemBase> Parameters { get; }
    }
}