// Copyright (c) UniversalExpressionParser Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using System;

namespace UniversalExpressionParser.ExpressionItems
{
    /// <summary>
    /// Default implementation for <see cref="IRootExpressionItem"/>.
    /// </summary>
    public class RootExpressionItem: ExpressionItemSeriesBase, IRootExpressionItem
    {
        /// <inheritdoc />
        public RootExpressionItem() : base(Array.Empty<IExpressionItemBase>(), Array.Empty<IKeywordExpressionItem>())
        {
        }
    }
}