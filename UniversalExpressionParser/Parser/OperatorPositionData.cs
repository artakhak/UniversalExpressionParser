// Copyright (c) UniversalExpressionParser Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using JetBrains.Annotations;
using UniversalExpressionParser.ExpressionItems;

namespace UniversalExpressionParser.Parser
{
    internal class OperatorPositionData
    {
        internal OperatorPositionData([NotNull] IOperatorInfoExpressionItem operatorInfoExpressionItem, int indexInOperators)
        {
            OperatorInfoExpressionItem = operatorInfoExpressionItem;
            IndexInOperators = indexInOperators;
        }

        internal int IndexInOperators { get; }

        [NotNull]
        internal IOperatorInfoExpressionItem OperatorInfoExpressionItem { get; }
    }
}