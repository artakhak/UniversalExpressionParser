// Copyright (c) UniversalExpressionParser Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace UniversalExpressionParser
{
    /// <summary>
    /// Helpers methods for <see cref="IExpressionLanguageProvider"/>.
    /// </summary>
    public static class ExpressionLanguageProviderHelpers
    {
        /// <summary>
        /// Default list of <see cref="NumericTypeDescriptor"/> that can be used in <see cref="IExpressionLanguageProvider.NumericTypeDescriptors"/>.
        /// </summary>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyList<NumericTypeDescriptor> GetDefaultNumericTypeDescriptors()
        {
            return new List<NumericTypeDescriptor>
            {
                // Example exp values (can have EXP, exp, or E instead of e in these examples
                // 1.2e3.4 .2e3.4   1.e3.4   1.2e.4   1.2e3.
                // 1.2e-3.4 .2e-3.4  1.e-3.4  1.2e-.4  1.2e-3.
                new NumericTypeDescriptor(KnownNumericTypeDescriptorIds.ExponentFormatValueId, new[]
                {
                    @"^(\d+\.\d+|\d+\.|\.\d+|\d+)(EXP|exp|E|e)[+|-]?(\d+\.\d+|\d+\.|\.\d+|\d+)"
                }),

                // Examples of floating number 1.23, .23, 1. 
                new NumericTypeDescriptor(KnownNumericTypeDescriptorIds.FloatingPointValueId, new[] { @"^(\d+\.\d+|\d+\.|\.\d+)"}),
                
                // Examples of int values 123
                new NumericTypeDescriptor(KnownNumericTypeDescriptorIds.IntegerValueId, new[] { @"^\d+" })
            };
        }
    }
}