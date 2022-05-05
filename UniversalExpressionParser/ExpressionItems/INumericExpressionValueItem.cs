// Copyright (c) UniversalExpressionParser Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using JetBrains.Annotations;

namespace UniversalExpressionParser.ExpressionItems
{
    /// <summary>
    /// Expression item to which <see cref="INumericExpressionItem.Value"/> is parsed.
    /// For example the expression "[Metadata1] 2.3e25" will be parsed to <see cref="INumericExpressionItem"/> with the list <see cref="IComplexExpressionItem.Prefixes"/> containing
    /// the expression item parsed from "[Metadata1]", and
    /// <see cref="INumericExpressionItem.Value"/> of type <see cref="INumericExpressionValueItem"/> will parsed from the number "2.3e25". 
    /// </summary>
    public interface INumericExpressionValueItem : IExpressionItemBase
    {
        /// <summary>
        /// Numeric value.
        /// </summary>
        [NotNull]
        string NumericValue { get; }
    }

    /// <summary>
    /// Default implementation of <see cref="INumericExpressionValueItem"/>
    /// </summary>
    public class NumericExpressionValueItem : NameExpressionItem, INumericExpressionValueItem
    {
        /// <inheritdoc />
        public NumericExpressionValueItem([NotNull] string numericValue, int indexInText) : base(numericValue, indexInText)
        {
            NumericValue = numericValue;
        }

        /// <inheritdoc />
        public string NumericValue { get; }
    }
}