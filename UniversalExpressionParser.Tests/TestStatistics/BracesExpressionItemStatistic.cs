using System.Text;
using UniversalExpressionParser.ExpressionItems;

namespace UniversalExpressionParser.Tests.TestStatistics
{
    public class BracesExpressionItemStatistic : ExpressionItemStatistic, IBracesExpressionItemStatistic
    {
        /// <inheritdoc />
        public BracesExpressionItemStatistic(bool isNamedBraces, bool isRoundBraces, int numberOfParameters) : base(ExpressionItemType.Braces)
        {
            IsNamedBraces = isNamedBraces;
            IsRoundBraces = isRoundBraces;
            NumberOfParameters = numberOfParameters;

            var statisticName = new StringBuilder();

            if (isNamedBraces)
                statisticName.Append("Named");

            statisticName.Append(isRoundBraces ? "Round" : "Square");

            statisticName.Append("Braces");
            statisticName.Append("(");
            statisticName.Append($"ParamsCount={numberOfParameters}");
            statisticName.Append(")");

            StatisticName = statisticName.ToString();
        }

        public override bool IsStatisticSourceAMatch(ITextItem statisticSource)
        {
            return statisticSource is IBracesExpressionItem bracesExpressionItem &&
                   (bracesExpressionItem.NameLiteral == null) != IsNamedBraces &&
                   (bracesExpressionItem.OpeningBrace.IsRoundBrace == IsRoundBraces) &&
                   bracesExpressionItem.Parameters.Count == NumberOfParameters;
        }

        public override string StatisticName { get; }

        /// <inheritdoc />
        public bool IsNamedBraces { get; }

        /// <inheritdoc />
        public bool IsRoundBraces { get; }

        /// <inheritdoc />
        public int NumberOfParameters { get; }
    }
}