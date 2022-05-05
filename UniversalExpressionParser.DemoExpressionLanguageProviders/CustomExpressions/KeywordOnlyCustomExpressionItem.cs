using System.Collections.Generic;
using JetBrains.Annotations;
using UniversalExpressionParser.ExpressionItems;
using UniversalExpressionParser.ExpressionItems.Custom;

namespace UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions
{
    public class KeywordOnlyCustomExpressionItem : TestLanguageCustomExpressionBase
    {
        public KeywordOnlyCustomExpressionItem([NotNull] [ItemNotNull] IEnumerable<IExpressionItemBase> parsedPrefixExpressionItems, 
            [NotNull] [ItemNotNull] IEnumerable<IKeywordExpressionItem> parsedKeywordExpressionItemsWithoutLastKeyword, 
            [NotNull] IKeywordExpressionItem lastKeywordExpressionItem, CustomExpressionItemCategory customExpressionItemCategory) : base(parsedPrefixExpressionItems, parsedKeywordExpressionItemsWithoutLastKeyword, lastKeywordExpressionItem, customExpressionItemCategory)
        {
        }
    }
}