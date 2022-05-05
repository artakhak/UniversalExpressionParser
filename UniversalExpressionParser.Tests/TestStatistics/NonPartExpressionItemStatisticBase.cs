using UniversalExpressionParser.ExpressionItems;

namespace UniversalExpressionParser.Tests.TestStatistics
{
    public abstract class NonPartExpressionItemStatisticBase : TextItemStatistic
    {
        /// <inheritdoc />
        public override bool IsStatisticSourceAMatch(ITextItem statisticSource)
        {
            return statisticSource is IExpressionItemBase expressionItem && expressionItem.GetItemType().IsMainExpressionType();
        }
    }
}