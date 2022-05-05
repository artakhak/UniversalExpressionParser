using JetBrains.Annotations;
using System.Collections.Generic;
using UniversalExpressionParser.ExpressionItems;
using UniversalExpressionParser.ExpressionItems.Custom;

namespace UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions
{
    public class TestLanguageCustomExpressionBase: KeywordBasedCustomExpressionItem, ITestLanguageCustomExpression
    {
        public TestLanguageCustomExpressionBase([NotNull] [ItemNotNull] IEnumerable<IExpressionItemBase> parsedPrefixExpressionItems, [NotNull] [ItemNotNull] IEnumerable<IKeywordExpressionItem> parsedKeywordExpressionItemsWithoutLastKeyword, [NotNull] IKeywordExpressionItem lastKeywordExpressionItem, CustomExpressionItemCategory customExpressionItemCategory) : base(parsedPrefixExpressionItems, parsedKeywordExpressionItemsWithoutLastKeyword, lastKeywordExpressionItem, customExpressionItemCategory)
        {
        }

        public long KeywordId => this.LastKeywordExpressionItem.LanguageKeywordInfo.Id;
    }
}
