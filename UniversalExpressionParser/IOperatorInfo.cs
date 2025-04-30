// Copyright (c) UniversalExpressionParser Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using System.Collections.Generic;
using JetBrains.Annotations;
using UniversalExpressionParser.ExpressionItems;

namespace UniversalExpressionParser
{
    /// <summary>
    /// Stores operator info. Is used in <see cref="IExpressionLanguageProvider.Operators"/> to provide operators that expressions can use.
    /// The following implementations can be used: <see cref="OperatorInfo"/>, <see cref="OperatorInfoWithAutoId"/>
    /// </summary>
    public interface IOperatorInfo
    {
        /// <summary>
        /// Unique value identifying the operator.
        /// </summary>
        long Id { get; }

        /// <summary>
        /// Priority. Lower value means higher priority.
        /// For example if the priority of multiplication and division operators "*" and "/" are set to 2, and the
        /// priority of addition and subtraction operators '+', '-' are set to 3, then the expression
        /// x+y*z will be evaluated in such a way, that multiplication of y and z will be applied before addition of x and y*z.
        /// In other words the parsed expression will parse this expression to <see cref="IOperatorExpressionItem"/> for binary operator "+", which will have value
        /// of <see cref="IOperatorExpressionItem.Operand1"/> equal to an instance of <see cref="ILiteralExpressionItem"/> parsed from "x", and
        /// the value of <see cref="IOperatorExpressionItem.Operand2"/> will be another instance of <see cref="IOperatorExpressionItem"/> parsed from an expression
        /// "y*z".
        /// </summary>
        int Priority { get; }

        /// <summary>
        /// In most cases this list will contain just one item (say "+" or "-").
        /// However, in some cases the list might contain multiple items.
        /// For example the SQL operator "IS NOT" will have two items: "IS" and "NOT".
        /// </summary>
        [NotNull, ItemNotNull]
        IReadOnlyList<string> NameParts { get; }

        /// <summary>
        /// Operator type, such as binary, prefix unary, or postfix unary.
        /// </summary>
        OperatorType OperatorType { get; }

        /// <summary>
        /// Returns operator name generated from the values in <see cref="IOperatorInfo.NameParts"/>
        /// For example if <see cref="IOperatorInfo.NameParts"/> contains "IS", "NOT", and "NULL", the returned value will be "IS NOT NULL".
        /// The implementation can generate value of <see cref="Name"/> as concatenation of items in <see cref="NameParts"/> separated by spaces or commas.
        /// </summary>
        string Name { get; }
    }
}