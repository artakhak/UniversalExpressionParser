using System.Collections.Generic;
using JetBrains.Annotations;
using UniversalExpressionParser.ExpressionItems;
using UniversalExpressionParser.ExpressionItems.Custom;

namespace UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions
{
    public class PerformanceCustomExpressionItem: TestLanguageCustomExpressionBase, IPerformanceCustomExpressionItem
    {
        /// <inheritdoc />
        public PerformanceCustomExpressionItem([NotNull] [ItemNotNull] IEnumerable<IExpressionItemBase> prefixExpressionItems,
                                               [NotNull][ItemNotNull] IEnumerable<IKeywordExpressionItem> parsedKeywordExpressionItemsWithoutLastKeyword,
                                               [NotNull] IKeywordExpressionItem performanceKeywordExpressionItem,
                                               [NotNull] IBracesExpressionItem parametersBracesExpressionItem) :
            base(prefixExpressionItems, parsedKeywordExpressionItemsWithoutLastKeyword, performanceKeywordExpressionItem, CustomExpressionItemCategory.Prefix)
        {
            ParametersBracesExpressionItem = parametersBracesExpressionItem;
            AddRegularItem(parametersBracesExpressionItem);
        }

        /// <inheritdoc />
        public IBracesExpressionItem ParametersBracesExpressionItem { get; }
    }
}