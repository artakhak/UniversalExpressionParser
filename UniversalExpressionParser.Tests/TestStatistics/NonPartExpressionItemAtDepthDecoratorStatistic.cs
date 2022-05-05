using JetBrains.Annotations;
using UniversalExpressionParser.ExpressionItems;

namespace UniversalExpressionParser.Tests.TestStatistics
{
    public class NonPartExpressionItemAtDepthDecoratorStatistic : NonPartExpressionItemAtDepthStatistic, IDecoratorStatistic
    {
      

        public NonPartExpressionItemAtDepthDecoratorStatistic([NotNull] ITextItemStatistic expressionItemStatistic, int expressionItemDepth) : base(expressionItemDepth)
        {
            DecoratedExpressionItemStatistic = expressionItemStatistic;

            StatisticName = $"ExpressionItemDepth={this.ExpressionItemDepth}, {DecoratedExpressionItemStatistic.StatisticName}";
        }

        public override bool IsStatisticSourceAMatch(ITextItem statisticSource)
        {
            return base.IsStatisticSourceAMatch(statisticSource) && DecoratedExpressionItemStatistic.IsStatisticSourceAMatch(statisticSource);
        }

        public override string StatisticName { get; }

        /// <inheritdoc />
        public ITextItemStatistic DecoratedExpressionItemStatistic { get; }
    }
}