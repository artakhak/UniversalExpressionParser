using System.Collections.Generic;
using JetBrains.Annotations;
using UniversalExpressionParser.ExpressionItems;
using UniversalExpressionParser.ExpressionItems.Custom;

namespace UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions
{
    public class PragmaCustomExpressionItem: TestLanguageCustomExpressionBase, IPragmaCustomExpressionItem
    {
        /// <inheritdoc />
        public PragmaCustomExpressionItem([NotNull] [ItemNotNull] IEnumerable<IExpressionItemBase> prefixExpressionItems,
                                          [NotNull][ItemNotNull] IEnumerable<IKeywordExpressionItem> parsedKeywordExpressionItemsWithoutLastKeyword,
                                          [NotNull] IKeywordExpressionItem pragmaKeywordExpressionItem, 
                                          [NotNull] ITextExpressionItem pragmaSymbolExpressionItem) : 
            base(prefixExpressionItems, parsedKeywordExpressionItemsWithoutLastKeyword, pragmaKeywordExpressionItem, CustomExpressionItemCategory.Regular)
        {
            PragmaSymbolExpressionItem = pragmaSymbolExpressionItem;
            AddRegularItem(PragmaSymbolExpressionItem);
        }

        /// <inheritdoc />
        public ITextExpressionItem PragmaSymbolExpressionItem { get; }
    }
}