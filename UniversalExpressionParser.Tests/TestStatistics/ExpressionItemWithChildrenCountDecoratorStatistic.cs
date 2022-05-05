using JetBrains.Annotations;
using UniversalExpressionParser.ExpressionItems;

namespace UniversalExpressionParser.Tests.TestStatistics
{
    public class ExpressionItemWithChildrenCountDecoratorStatistic : NonPartExpressionItemStatisticBase, IExpressionItemWithChildrenCountDecoratorStatistic //: ExpressionItemWithChildrenCountStatistic
    {
        public ExpressionItemWithChildrenCountDecoratorStatistic([NotNull] IExpressionItemStatistic decoratedExpressionItemStatistic, int numberOfChildren) 
        {
            DecoratedExpressionItemStatistic = decoratedExpressionItemStatistic;
            NumberOfChildren = numberOfChildren;

            StatisticName = $"{DecoratedExpressionItemStatistic.StatisticName}, ChildrenCount={this.NumberOfChildren}";
        }

        public override bool IsStatisticSourceAMatch(ITextItem statisticSource)
        {
            if (!(base.IsStatisticSourceAMatch(statisticSource) &&
                   DecoratedExpressionItemStatistic.IsStatisticSourceAMatch(statisticSource)))
                return false;

            return statisticSource is IComplexExpressionItem complexExpressionItem && complexExpressionItem.Children.Count == NumberOfChildren;
        }

        public override string StatisticName { get; }

        /// <inheritdoc />
        public ExpressionItemType ExpressionItemType => DecoratedExpressionItemStatistic.ExpressionItemType;

        /// <inheritdoc />
        public int NumberOfChildren { get; }

        /// <inheritdoc />
        public IExpressionItemStatistic DecoratedExpressionItemStatistic { get; }
    }
}