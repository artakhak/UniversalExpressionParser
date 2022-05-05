// Copyright (c) UniversalExpressionParser Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using System.Collections.Generic;
using JetBrains.Annotations;

namespace UniversalExpressionParser.ExpressionItems
{
    /// <summary>
    /// Expression item parsed from numeric expressions.
    /// The value of the constant value will be in <see cref="ITextExpressionItem.Text"/>. Examples of values parsed to this class
    /// instance are "123456789012345678901234567890123456789", "-2.3", "-2.3e+1003", "-2.3exp+1003", -2.3EXP+1003, "TRUE", "False", etc.
    /// </summary>
    public interface INumericExpressionItem : IComplexExpressionItem
    {
        /// <summary>
        /// Stores the parsed numeric value as text.
        /// </summary>
        [NotNull]
        INumericExpressionValueItem Value { get; }

        /// <summary>
        /// An instance of <see cref="NumericTypeDescriptor"/> that succeeded in parsing the text to numeric value.
        /// </summary>
        [NotNull]
        NumericTypeDescriptor SucceededNumericTypeDescriptor { get; }

        /// <summary>
        /// Index or regular expression in <see cref="NumericTypeDescriptor.RegularExpressions"/> used to match the numeric value.
        /// </summary>
        int IndexOfSucceededRegularExpression { get; }
    }

    /// <summary>
    /// Default implementation of <see cref="INumericExpressionItem"/>
    /// </summary>
    public class NumericExpressionItem : ComplexExpressionItemBase, INumericExpressionItem
    {
        
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="prefixExpressionItems">
        /// For example the expression "[Metadata1] 2.3e25" will be parsed to <see cref="INumericExpressionItem"/> with the list <see cref="IComplexExpressionItem.Prefixes"/> containing
        /// the expression parsed from "[Metadata1]", and
        /// <see cref="INumericExpressionItem.Value"/> of type <see cref="INumericExpressionValueItem"/> will parsed from the number "2.3e25". 
        /// </param>
        /// <param name="keywordExpressionItems">Applied keyword expression items.
        /// For example in "keyword1 2.3e25", "keyword1" will be parsed to a keyword and added to <see cref="IComplexExpressionItem.AppliedKeywords"/> in
        /// <see cref="INumericExpressionItem"/> (provided "keyword1" is in <see cref="IExpressionLanguageProvider"/>), and "2.3e25" will be parsed to
        /// <see cref="INumericExpressionValueItem"/> and stored in <see cref="INumericExpressionItem.Value"/> property.
        /// </param>
        /// <param name="value">Expression item parsed from the numeric value.</param>
        /// <param name="succeedNumericTypeDescriptor">An instance of <see cref="NumericTypeDescriptor"/> that succeeded in parsing the text to numeric value.</param>
        /// <param name="indexOfSucceededRegularExpression">Index or regular expression in <see cref="NumericTypeDescriptor.RegularExpressions"/> used to match the numeric value.</param>
        public NumericExpressionItem([NotNull][ItemNotNull] IEnumerable<IExpressionItemBase> prefixExpressionItems,
                                          [NotNull][ItemNotNull] IEnumerable<IKeywordExpressionItem> keywordExpressionItems,
                                          [NotNull] INumericExpressionValueItem value,
                                          [NotNull] NumericTypeDescriptor succeedNumericTypeDescriptor,
                                          int indexOfSucceededRegularExpression) :
            base(prefixExpressionItems, keywordExpressionItems)
        {
            SucceededNumericTypeDescriptor = succeedNumericTypeDescriptor;
            IndexOfSucceededRegularExpression = indexOfSucceededRegularExpression;

            Value = value;
            AddRegularItem(Value);
        }

        /// <inheritdoc />
        public INumericExpressionValueItem Value { get; }

        /// <inheritdoc />
        public NumericTypeDescriptor SucceededNumericTypeDescriptor { get; }

        /// <inheritdoc />
        public int IndexOfSucceededRegularExpression { get; }
    }
}