using System.Collections.Generic;
using UniversalExpressionParser;
using UniversalExpressionParser.ExpressionItems;
using UniversalExpressionParser.ExpressionItems.Custom;

namespace UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions
{
    public class KeywordOnlyCustomExpressionItemParser: CustomExpressionItemParserByKeywordId
    {
        private readonly CustomExpressionItemCategory _customExpressionItemCategory;

        public KeywordOnlyCustomExpressionItemParser(long parsedKeywordId, CustomExpressionItemCategory customExpressionItemCategory) : base(parsedKeywordId)
        {
            _customExpressionItemCategory = customExpressionItemCategory;
        }

        protected override ICustomExpressionItem DoParseCustomExpressionItem(IParseExpressionItemContext context, 
            IReadOnlyList<IExpressionItemBase> parsedPrefixExpressionItems, 
            IReadOnlyList<IKeywordExpressionItem> parsedKeywordExpressionItemsWithoutLastKeyword, IKeywordExpressionItem lastKeywordExpressionItem)
        {
            return new KeywordOnlyCustomExpressionItem(parsedPrefixExpressionItems, parsedKeywordExpressionItemsWithoutLastKeyword, lastKeywordExpressionItem, _customExpressionItemCategory);
        }
    }
}