// Copyright (c) UniversalExpressionParser Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using System.Collections.Generic;
using JetBrains.Annotations;
using UniversalExpressionParser.ExpressionItems;

namespace UniversalExpressionParser.Parser
{
    internal class ParseExpressionItemData
    {
        internal ParseExpressionItemData([NotNull] ICanAddChildExpressionItem parentExpressionItem)
        {
            ParentExpressionItem = parentExpressionItem;
        }

        [NotNull]
        internal ICanAddChildExpressionItem ParentExpressionItem { get; }

        [NotNull, ItemNotNull]
        internal List<IExpressionItemBase> AllExpressionItems { get; } = new List<IExpressionItemBase>();

        [CanBeNull, ItemNotNull]
        internal List<List<IOperatorInfoExpressionItem>> ParsedUnprocessedOperatorInfoItems { get; set; }

        internal bool OperatorsCannotBeEvaluated { get; set; }
    }
}