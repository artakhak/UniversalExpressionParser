using System.Collections.Generic;
using JetBrains.Annotations;
using UniversalExpressionParser.ExpressionItems;
using UniversalExpressionParser.ExpressionItems.Custom;

namespace UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions
{
    public class MetadataCustomExpressionItem: TestLanguageCustomExpressionBase, IMetadataCustomExpressionItem
    {
        /// <inheritdoc />
        public MetadataCustomExpressionItem([NotNull] [ItemNotNull] IEnumerable<IExpressionItemBase> prefixExpressionItems,
                                            [NotNull][ItemNotNull] IEnumerable<IKeywordExpressionItem> parsedKeywordExpressionItemsWithoutLastKeyword,
                                            [NotNull] IKeywordExpressionItem metadataKeywordExpressionItem,
                                            [NotNull] ICodeBlockExpressionItem metadataExpressionItem) : 
            base(prefixExpressionItems, parsedKeywordExpressionItemsWithoutLastKeyword, metadataKeywordExpressionItem, CustomExpressionItemCategory.Prefix)
        {
            MetadataExpressionItem = metadataExpressionItem;
            AddRegularItem(MetadataExpressionItem);

        }

        /// <inheritdoc />
        public ICodeBlockExpressionItem MetadataExpressionItem { get; }
    }
}