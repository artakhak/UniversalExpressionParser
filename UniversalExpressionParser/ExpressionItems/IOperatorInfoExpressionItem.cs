// Copyright (c) UniversalExpressionParser Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using System.Collections.Generic;
using JetBrains.Annotations;

namespace UniversalExpressionParser.ExpressionItems
{
    /// <summary>
    /// Expression item for storing operator data. For example in expression "x = y+z*10" the data on operator "=" will be stored in
    /// one instance of <see cref="IOperatorInfoExpressionItem"/> and data on "+" will be stored in another instance of <see cref="IOperatorInfoExpressionItem"/>.
    /// On the other hand, the entire expression "x = y+z*10" will be stored in <see cref="IOperatorExpressionItem"/>, which has a
    /// property <see cref="IOperatorExpressionItem.OperatorInfoExpressionItem"/> of type <see cref="IOperatorExpressionItem"/>. 
    /// </summary>
    public interface IOperatorInfoExpressionItem: IComplexExpressionItem
    {
        /// <summary>
        /// Expression item for operator
        /// </summary>
        [NotNull]
        IOperatorInfo OperatorInfo { get; }

        /// <summary>
        /// Expression item parsed from operator parts. For example in x IS NOT /*comment*/ NULL, the text "IS NOT /*comment*/ NULL" will be parsed to a collection of three items
        /// of type <see cref="IOperatorNamePartExpressionItem"/> and will be assigned to <see cref="OperatorNameParts"/> property.
        /// </summary>
        [NotNull, ItemNotNull]
        IReadOnlyList<IOperatorNamePartExpressionItem> OperatorNameParts { get; }
    }
}