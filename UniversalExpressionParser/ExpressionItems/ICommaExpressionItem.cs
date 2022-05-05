// Copyright (c) UniversalExpressionParser Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

namespace UniversalExpressionParser.ExpressionItems
{
    /// <summary>
    /// An expression item for comma ',' that separates parameters in <see cref="IBracesExpressionItem"/>.
    /// </summary>
    public interface ICommaExpressionItem : ITextExpressionItem
    {
        
    }

    /// <summary>
    /// A default implementation for <see cref="ICommaExpressionItem"/>.
    /// </summary>
    public class CommaExpressionItem : NameExpressionItem, ICommaExpressionItem
    {
        /// <inheritdoc />
        public CommaExpressionItem(int indexInText) : base(",", indexInText)
        {
        }
    }
}