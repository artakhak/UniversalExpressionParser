using JetBrains.Annotations;
using TestsSharedLibrary.TestSimulation.Statistics;
using UniversalExpressionParser.ExpressionItems;

namespace UniversalExpressionParser.Tests.TestStatistics
{
    public class ExclusiveTextItemStatisticGroup: ExclusiveTestStatisticGroup<ITextItem>, ITextItemStatisticGroup
    {
        /// <inheritdoc />
        public ExclusiveTextItemStatisticGroup([NotNull] string statisticName, [CanBeNull] TextItemStatisticsIsFilteredOut<ITextItem> textItemStatisticsIsFilteredOut) : base(textItemStatisticsIsFilteredOut)
        {
            StatisticName = statisticName;
        }

        /// <inheritdoc />
        public override string StatisticName { get; }
    }
}