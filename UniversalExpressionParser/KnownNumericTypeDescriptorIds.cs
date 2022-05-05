// Copyright (c) UniversalExpressionParser Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

namespace UniversalExpressionParser
{
    // Documented
    /// <summary>
    /// List of types of numeric value types stored in property <see cref="NumericTypeDescriptor.NumberTypeId"/> in <see cref="ExpressionItems.INumericExpressionItem.SucceededNumericTypeDescriptor"/>.
    /// </summary>
    public static class KnownNumericTypeDescriptorIds
    {
        /// <summary>
        /// Numeric value that can have fractional part. Examples are 2.3, 12345678911111.00000000000000000000000000000000001.
        /// </summary>
        public const long FloatingPointValueId = 1581134136626;

        /// <summary>
        /// Numeric value without fractional part. Examples are 10, 123456789123456789123456789.
        /// </summary>
        public const long IntegerValueId = 1581134183960;

        /// <summary>
        /// Exponent value. Examples are 1.3exp3.2
        /// </summary>
        public const long ExponentFormatValueId = 1581134225786;
    }
}