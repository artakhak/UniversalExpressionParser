// Copyright (c) UniversalExpressionParser Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using JetBrains.Annotations;

namespace UniversalExpressionParser.ExpressionItems
{
    /// <summary>
    /// A delegate that returns true.
    /// </summary>
    /// <param name="characterAfterMatchedText">Character.</param>
    /// <param name="positionInText">Position in text.</param>
    public delegate bool IsValidTextAfterMatchedTextDelegate(char characterAfterMatchedText, int positionInText);

    /// <summary>
    /// A delegate that processes an expression item.
    /// </summary>
    /// <param name="expressionItem">Expression item being processed.</param>
    /// <returns>
    /// Returns true, if processing other items should continue.
    /// Returns false, if processing other items should stop
    /// </returns>
    public delegate bool ProcessExpressionItem([NotNull] IExpressionItemBase expressionItem);

    /// <summary>
    /// A delegate executed after all items in <see cref="IComplexExpressionItem.AllItems"/> are processed.
    /// </summary>
    /// <param name="complexExpressionItem">Expression item being processed.</param>

    public delegate bool OnAllComplexExpressionItemPartsProcessed([NotNull] IComplexExpressionItem complexExpressionItem);

    /// <summary>
    /// A delegate executed after an item in <see cref="IComplexExpressionItem.AllItems"/> is processed.
    /// </summary>
    /// <param name="complexExpressionItem">Expression item.</param>
    /// <param name="complexExpressionItemPart">Complex expression item part.</param>
    /// <param name="indexInAllItems">Index of complexExpressionItemPart in <see cref="IComplexExpressionItem.AllItems"/>.</param>
    /// <param name="indexInChildItems">If complexExpressionItemPart is in <see cref="IComplexExpressionItem.Children"/>
    /// indexInChildItems is the index of complexExpressionItemPart in <see cref="IComplexExpressionItem.Children"/>.
    /// Otherwise, the value is -1.
    /// </param>
    internal delegate bool OnComplexExpressionItemPartProcessed([NotNull] IComplexExpressionItem complexExpressionItem,
                                                              [NotNull] IExpressionItemBase complexExpressionItemPart,
                                                              int indexInAllItems, int indexInChildItems);
}
