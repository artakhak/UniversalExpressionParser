// Copyright (c) UniversalExpressionParser Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

namespace UniversalExpressionParser.ExpressionItems.Custom
{
    /// <summary>
    /// Custom expression category.
    /// </summary>
    public enum CustomExpressionItemCategory
    {
        /// <summary>
        /// Custom expression item should be used as a prefix for another expression item that follows the custom expression item.
        /// For example in expression "::types[T1,T2] F1(x:T1, y:T2) where T1:int where T2:double whereend" one of custom expression parsers might parse text "::types[T1,T2]" as a prefix custom expression item to be added to <see cref="IComplexExpressionItem.Prefixes"/> in expression of type <see cref="IBracesExpressionItem"/> parsed from "F1(x:T1, y:T2)".
        /// </summary>
        Prefix,

        /// <summary>
        /// Regular custom expression item type.
        /// For example in "::pragma x+y" one of custom expression parser might parse "::pragma x" as a regular custom expression item which will be one of the operands in binary operator "+".
        /// </summary>
        Regular,

        /// <summary>
        /// Custom expression item should be used as a prefix for another expression item that precedes the custom expression item.
        /// For example in expression "::types[T1,T2] F1(x:T1, y:T2) where T1:int where T2:double whereend" one of custom expression parsers might parse text "where T1:int where T2:double whereend" as a postfix custom expression item to be added to <see cref="IComplexExpressionItem.Postfixes"/> in expression of type <see cref="IBracesExpressionItem"/> parsed from "F1(x:T1, y:T2)".
        /// </summary>
        Postfix
    }
}