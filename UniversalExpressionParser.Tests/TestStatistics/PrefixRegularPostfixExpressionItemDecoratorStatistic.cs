using JetBrains.Annotations;
using UniversalExpressionParser.ExpressionItems;

namespace UniversalExpressionParser.Tests.TestStatistics
{
    public class PrefixRegularPostfixExpressionItemDecoratorStatistic : PrefixRegularPostfixExpressionItemStatistic, IDecoratorStatistic
    {
        public PrefixRegularPostfixExpressionItemDecoratorStatistic([NotNull] ITextItemStatistic expressionItemStatistic, 
                                                                    SpecialExpressionItemType specialExpressionItemType) : base(specialExpressionItemType)
        {
            DecoratedExpressionItemStatistic = expressionItemStatistic;
        }

        public override bool IsStatisticSourceAMatch(ITextItem statisticSource)
        {
            return base.IsStatisticSourceAMatch(statisticSource) && DecoratedExpressionItemStatistic.IsStatisticSourceAMatch(statisticSource);
        }

        public override string StatisticName => $"{SpecialExpressionItemType}:{DecoratedExpressionItemStatistic.StatisticName}";

        [NotNull]
        public ITextItemStatistic DecoratedExpressionItemStatistic { get; }
    }
}