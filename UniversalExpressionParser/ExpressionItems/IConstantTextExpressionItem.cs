// Copyright (c) UniversalExpressionParser Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using JetBrains.Annotations;

namespace UniversalExpressionParser.ExpressionItems
{
    /// <summary>
    /// An expression item for expression items parsed from constant text. Examples of expression that will be parsed to <see cref="IConstantTextExpressionItem"/> are:
    /// x="This is will be parsed to text": The value in "This is will be parsed to text" will be parsed to text (including apostrophes).
    /// y='This will be parsed to text too': The value in 'This will be parsed to text too' will be parsed to text (including apostrophes). 
    /// </summary>
    public interface IConstantTextExpressionItem : IComplexExpressionItem
    {
        /// <summary>
        ///  An expression item for storing the text (including the apostrophes).
        /// Examples stored in <see cref="TextValue"/> are "This is will be parsed to text" or 'This is will be parsed to text'.
        /// </summary>
        [NotNull]
        IConstantTextValueExpressionItem TextValue { get; }
    }
}