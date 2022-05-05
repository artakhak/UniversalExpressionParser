// Copyright (c) UniversalExpressionParser Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using JetBrains.Annotations;

namespace UniversalExpressionParser.ExpressionItems
{
    /// <summary>
    /// Expression item parsed from operator part. For example lets say there is an operator in <see cref="IExpressionLanguageProvider.Operators"/> for which
    /// <see cref="IOperatorInfo.NameParts"/> contains list ["IS" "NOT", "NULL"]. In this case, the expression "x IS NOT NULL" will be parsed to a unary operator expression item of type <see cref="IOperatorInfoExpressionItem"/>,
    /// in which <see cref="IOperatorInfoExpressionItem.OperatorNameParts"/> will contain three items of typ <see cref="IOperatorNamePartExpressionItem"/> for "IS", "NOT", and "NULL".
    /// </summary>
    public interface IOperatorNamePartExpressionItem : ITextExpressionItem
    {

    }

    /// <summary>
    /// Default implementation for <see cref="IOperatorNamePartExpressionItem"/>.
    /// </summary>
    public class OperatorNamePartExpressionItem : NameExpressionItem, IOperatorNamePartExpressionItem
    {
        /// <inheritdoc />
        public OperatorNamePartExpressionItem([NotNull] string name, int indexInText) : base(name, indexInText)
        {
        }
    }
}