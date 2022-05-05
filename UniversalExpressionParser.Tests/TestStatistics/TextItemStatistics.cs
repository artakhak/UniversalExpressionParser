using JetBrains.Annotations;
using System.Collections.Generic;
using System.Linq;
using TestsSharedLibrary.TestSimulation.Statistics;
using UniversalExpressionParser.ExpressionItems;

namespace UniversalExpressionParser.Tests.TestStatistics
{
    public class TextItemStatistics: TestStatistics<ITextItem>, ITextItemStatistics
    {
        /// <inheritdoc />
        public TextItemStatistics(string statisticName, [NotNull] [ItemNotNull] IEnumerable<ITextItemStatisticGroup> testStatistics) : base(statisticName, testStatistics)
        {
        }
    }
}