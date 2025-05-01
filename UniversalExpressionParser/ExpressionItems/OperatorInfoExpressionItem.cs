// Copyright (c) UniversalExpressionParser Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace UniversalExpressionParser.ExpressionItems
{
    /// <summary>
    /// Default implementation of <see cref="IOperatorInfoExpressionItem"/>
    /// </summary>
    public class OperatorInfoExpressionItem: ComplexExpressionItemBase, IOperatorInfoExpressionItem
    {
        /// <inheritdoc />
        public OperatorInfoExpressionItem([NotNull] IOperatorInfo operatorInfo,
                                          [NotNull, ItemNotNull] IReadOnlyList<IOperatorNamePartExpressionItem> operatorNameParts) : base(new List<IExpressionItemBase>(0), new List<IKeywordExpressionItem>(0))
        {
            foreach (var namePart in operatorNameParts)
                AddRegularItem(namePart);

            OperatorInfo = operatorInfo;

            if (operatorNameParts.Count == 0)
                throw new ArgumentException($"Collection '{nameof(operatorNameParts)}' cannot be empty.", nameof(operatorNameParts));

            OperatorNameParts = operatorNameParts;
        }

        /// <inheritdoc />
        public IOperatorInfo OperatorInfo { get; }

        /// <inheritdoc />
        public IReadOnlyList<IOperatorNamePartExpressionItem> OperatorNameParts { get; }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"Operator:{OperatorInfo}, {nameof(IndexInText)}:{IndexInText}, OperatorType:{OperatorInfo.OperatorType}, {this.GetType().FullName}.";
        }
    }
}
