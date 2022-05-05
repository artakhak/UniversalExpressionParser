using UniversalExpressionParser.ExpressionItems;

namespace UniversalExpressionParser.Tests.TestStatistics
{
    public class ParsedCodeCounterStatistics : TextItemStatistic
    {
        public ParsedCodeCounterStatistics()
        {
            StatisticName = "ParsedCode";
        }
        /// <inheritdoc />
        public override string StatisticName { get; }

        /// <inheritdoc />
        public override bool IsStatisticSourceAMatch(ITextItem statisticSource)
        {
            return statisticSource is IParseExpressionResult;
        }
    }
}