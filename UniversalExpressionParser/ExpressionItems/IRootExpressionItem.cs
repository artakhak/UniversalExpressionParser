// Copyright (c) UniversalExpressionParser Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

namespace UniversalExpressionParser.ExpressionItems
{
    /// <summary>
    /// Parsed top level expression items in <see cref="IComplexExpressionItem.Children"/>.
    /// For example in expression "{var x=3+4;} myFunc(var x:int, y: int): int { return x+y; } var result=myFunc(4, 5*f+3)"
    /// the list <see cref="IComplexExpressionItem.Children"/> will contain three top level items:
    /// 1) Code block item {var x=3+4;}
    /// 2) Function myFunc(var x:int, y: int): int { return x+y; }
    /// 3) The operators item var result=myFunc(4, 5*f+3).
    /// </summary>
    public interface IRootExpressionItem: ICanAddChildExpressionItem, ICanAddSeparatorCharacterExpressionItem
    {
        
    }
}