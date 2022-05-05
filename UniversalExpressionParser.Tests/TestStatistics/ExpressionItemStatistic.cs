using UniversalExpressionParser.ExpressionItems;

namespace UniversalExpressionParser.Tests.TestStatistics
{
    public class ExpressionItemStatistic : TextItemStatistic, IExpressionItemStatistic
    {
        private string _statisticName;

        public ExpressionItemStatistic(ExpressionItemType expressionItemType)
        {
            ExpressionItemType = expressionItemType;
        }

        public ExpressionItemType ExpressionItemType { get; }

        public override string StatisticName =>
            _statisticName ??= ExpressionItemType.ToString();

        /// <inheritdoc />
        public override bool IsStatisticSourceAMatch(ITextItem statisticSource)
        {
            return statisticSource is IExpressionItemBase expressionItemBase && expressionItemBase.GetItemType() == ExpressionItemType;
        }
    }
}