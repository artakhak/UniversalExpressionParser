// Copyright (c) UniversalExpressionParser Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using System;
using JetBrains.Annotations;

namespace UniversalExpressionParser.ExpressionItems
{
    /// <summary>
    /// An expression item parsed from expression with errors that result when expected separators are missing
    /// between expressions (e.g, commas, operators, code separators).
    /// For example in expression "F1(a b)+F2(x, y x)" expressions "a b" and "y x" will be parsed to instances of
    /// <see cref="ISeriesOfExpressionItemsWithErrors"/> and will be added as parameters to braces expression items
    /// of type <see cref="IBracesExpressionItem"/> parsed from "F1(...)" and F2(...).
    /// The errors in this expression can be fixed if we add operators or commas between the operands
    /// as shown in this expression: "F1(a, b)+F2(x, y+x)". 
    /// </summary>
    public interface ISeriesOfExpressionItemsWithErrors : IComplexExpressionItem
    {

    }

    /// <summary>
    /// Default implementation for <see cref="ISeriesOfExpressionItemsWithErrors"/>.
    /// </summary>
    public class SeriesOfExpressionItemsWithErrors : ComplexExpressionItemBase, ISeriesOfExpressionItemsWithErrors
    {
        /// <inheritdoc />
        public SeriesOfExpressionItemsWithErrors() : base(
            Array.Empty<IExpressionItemBase>(), Array.Empty<IKeywordExpressionItem>())
        {
        }

        /// <summary>
        /// Adds an expression item to a series of invalid expression items.
        /// For example in invalid expression "F1(a b c)" the expression "a b c" is parsed to <see cref="ISeriesOfExpressionItemsWithErrors"/>
        /// and expression items parsed from "a", "b" and "c" can be added to <see cref="ISeriesOfExpressionItemsWithErrors"/> using
        /// <see cref="AddExpressionItem(IExpressionItemBase)"/>
        /// </summary>
        /// <param name="operatorOrOperand"></param>
        public void AddExpressionItem([NotNull] IExpressionItemBase operatorOrOperand)
        {
            this.AddRegularItem(operatorOrOperand);
        }
    }
}