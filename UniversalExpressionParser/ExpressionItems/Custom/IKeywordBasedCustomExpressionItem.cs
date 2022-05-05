// Copyright (c) UniversalExpressionParser Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using JetBrains.Annotations;

namespace UniversalExpressionParser.ExpressionItems.Custom
{
    /// <summary>
    /// A subclass of <see cref="ICustomExpressionItem"/> to which custom expression item is parsed, if the expression is preceded with expressions parsed to list of keywords of type <see cref="IKeywordExpressionItem"/>.
    /// </summary>
    public interface IKeywordBasedCustomExpressionItem : ICustomExpressionItem
    {
        /// <summary>
        /// Last keyword expression item that precedes the custom expression item.
        /// </summary>
        [NotNull]
        IKeywordExpressionItem LastKeywordExpressionItem { get; }
    }
}