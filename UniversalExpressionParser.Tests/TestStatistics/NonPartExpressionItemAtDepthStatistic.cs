using UniversalExpressionParser.ExpressionItems;

namespace UniversalExpressionParser.Tests.TestStatistics
{
    public class NonPartExpressionItemAtDepthStatistic : NonPartExpressionItemStatisticBase, INonPartExpressionItemAtDepthStatistic
    {
        public NonPartExpressionItemAtDepthStatistic(int expressionItemDepth)
        {
            ExpressionItemDepth = expressionItemDepth;
            StatisticName = $"ExpressionItem(Depth={ExpressionItemDepth})";
        }

        public int ExpressionItemDepth { get; }

        /// <inheritdoc />
        public override string StatisticName { get; }

        /// <inheritdoc />
        public override bool IsStatisticSourceAMatch(ITextItem statisticSource)
        {
            if (!base.IsStatisticSourceAMatch(statisticSource))
                return false;

            if (!(statisticSource is IExpressionItemBase expressionItem))
                return false;

            
            var expressionItemDepth = TestHelpers.GetExpressionItemDepth(expressionItem);

            return expressionItemDepth == ExpressionItemDepth;
        }
    }
}