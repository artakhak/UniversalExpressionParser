// Copyright (c) UniversalExpressionParser Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using System.Collections.Generic;
using JetBrains.Annotations;

namespace UniversalExpressionParser.ExpressionItems.Custom
{
    /// <summary>
    /// A default implementation of <see cref="ICustomExpressionItem"/>.
    /// </summary>
    public class CustomExpressionItem : ComplexExpressionItemBase, ICustomExpressionItem
    {
        /// <inheritdoc />
        public CustomExpressionItem([NotNull] [ItemNotNull] IEnumerable<IExpressionItemBase> prefixExpressionItems, 
                                    [NotNull] [ItemNotNull] IEnumerable<IKeywordExpressionItem> keywordExpressionItems,
                                    CustomExpressionItemCategory customExpressionItemCategory) :
            base(prefixExpressionItems, keywordExpressionItems)
        {
            this.CustomExpressionItemCategory = customExpressionItemCategory;
        }

        /// <inheritdoc />
        public CustomExpressionItemCategory CustomExpressionItemCategory { get; }

        /// <inheritdoc />
        public virtual bool IsValidPostfix(IExpressionItemBase postfixExpressionItem, out string customErrorMessage)
        {
            customErrorMessage = null;

            if (CustomExpressionItemCategory != CustomExpressionItemCategory.Regular)
                return false;

            if (postfixExpressionItem is ICodeBlockExpressionItem)
                return true;

            return postfixExpressionItem is ICustomExpressionItem customExpressionItem &&
                   customExpressionItem.CustomExpressionItemCategory == CustomExpressionItemCategory.Postfix;
        }

        /// <inheritdoc />
        public virtual void OnCustomExpressionItemParsed(IParseExpressionItemContext context)
        {
            
        }

        /// <inheritdoc />
        public virtual int? ErrorsPositionDisplayValue => null;
    }
}