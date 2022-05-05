using JetBrains.Annotations;
using System.Collections.Generic;
using UniversalExpressionParser.ExpressionItems;
using UniversalExpressionParser.ExpressionItems.Custom;

namespace UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions
{
    public class GenericTypesCustomExpressionItem : TestLanguageCustomExpressionBase, IGenericTypesCustomExpressionItem
    {
        /// <inheritdoc />
        public GenericTypesCustomExpressionItem([NotNull] [ItemNotNull] IEnumerable<IExpressionItemBase> prefixExpressionItems, 
                                                [NotNull] [ItemNotNull] IEnumerable<IKeywordExpressionItem> parsedKeywordExpressionItemsWithoutLastKeyword,
                                                [NotNull] IKeywordExpressionItem genericTypesKeywordExpressionItem,
                                                [NotNull] IBracesExpressionItem typesBracesExpressionItem) : 
            base(prefixExpressionItems,
                parsedKeywordExpressionItemsWithoutLastKeyword, genericTypesKeywordExpressionItem, CustomExpressionItemCategory.Prefix)
        {
            TypesBracesExpressionItem = typesBracesExpressionItem;
            AddRegularItem(TypesBracesExpressionItem);
        }

        public IBracesExpressionItem TypesBracesExpressionItem { get; }
    }
}