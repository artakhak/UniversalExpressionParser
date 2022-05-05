// Copyright (c) UniversalExpressionParser Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using JetBrains.Annotations;
using System.Collections.Generic;
using UniversalExpressionParser.ExpressionItems;

namespace UniversalExpressionParser.Parser
{
    internal class ParserHelper
    {
        /// <summary>
        /// Gets a list of prefixes that consists of <paramref name="prefixExpressionItem"/>.<see cref="IComplexExpressionItem.Prefixes"/>
        /// plus <paramref name="prefixExpressionItem"/> itself.
        /// Removes the prefixes from <paramref name="prefixExpressionItem"/>.
        /// </summary>
        internal IReadOnlyList<IExpressionItemBase> ConvertLastExpressionItemToPrefixes([CanBeNull] IComplexExpressionItem prefixExpressionItem)
        {
            if (prefixExpressionItem == null)
                return new List<IExpressionItemBase>(0);

            if (prefixExpressionItem.Prefixes.Count == 0)
                return new IExpressionItemBase[] { prefixExpressionItem };

            List<IExpressionItemBase> prefixes = new List<IExpressionItemBase>(prefixExpressionItem.Prefixes.Count + 1);
            prefixes.AddRange(prefixExpressionItem.Prefixes);
            prefixes.Add(prefixExpressionItem);

            prefixExpressionItem.RemovePrefixes();
            return prefixes;
        }
    }
}
