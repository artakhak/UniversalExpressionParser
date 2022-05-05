// Copyright (c) UniversalExpressionParser Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using System.Collections.Generic;
using JetBrains.Annotations;
using UniversalExpressionParser.ExpressionItems;

namespace UniversalExpressionParser
{
    // Documented
    /// <summary>
    /// Numeric type descriptor. Provides rules for the parser to use when trying to parse an expression to an instance of <see cref="INumericExpressionItem"/>.
    /// </summary>
    public class NumericTypeDescriptor
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="numberTypeId">
        /// Numeric value type, such as int, double, exponent format int, etc.
        /// The value can be one of <see cref="KnownNumericTypeDescriptorIds"/>, or new values can be used.</param>
        /// <param name="regularExpressions"> Regular expressions used to try to parse numbers of this format.
        /// Each regular expression should start with '^' character and cannot end with '$'.
        /// In additional, it is preferred that the regular expressions do not contain '|' character, so that
        /// the format of the parsed numeric value can be identified using <see cref="INumericExpressionItem.IndexOfSucceededRegularExpression"/>.
        /// Examples of valid regular expressions are: "^\d*[.]*\d*[E|e]{1,1}([+|-]?\d+)", "^\d*[.]\d+", "^\d+[.]\d*", "^[0-9]+", or "^\d+"</param>
        public NumericTypeDescriptor(long numberTypeId, [NotNull, ItemNotNull] IReadOnlyList<string> regularExpressions)
        {
            NumberTypeId = numberTypeId;
            RegularExpressions = regularExpressions;
        }

        /// <summary>
        /// Numeric value type, such as int, double, exponent format int, etc.
        /// The value can be one of <see cref="KnownNumericTypeDescriptorIds"/>, or new values can be used.
        /// </summary>
        public long NumberTypeId { get; }

        /// <summary>
        /// Regular expressions used to try to parse numbers of this format.
        /// Each regular expression should start with '^' character and cannot end with '$'.
        /// In additional, it is preferred that the regular expressions do not contain '|' character, so that
        /// the format of the parsed numeric value can be identified using <see cref="INumericExpressionItem.IndexOfSucceededRegularExpression"/>.
        /// Examples of valid regular expressions are: "^\d*[.]*\d*[E|e]{1,1}([+|-]?\d+)", "^\d*[.]\d+", "^\d+[.]\d*", "^[0-9]+", or "^\d+"
        /// </summary>
        [NotNull, ItemNotNull]
        public IReadOnlyList<string> RegularExpressions { get; }
    }
}