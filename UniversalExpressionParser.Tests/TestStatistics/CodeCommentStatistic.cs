using UniversalExpressionParser.ExpressionItems;

namespace UniversalExpressionParser.Tests.TestStatistics
{
    public class CodeCommentStatistic: TextItemStatistic
    {
        private readonly bool _isLineComment;

        /// <inheritdoc />
        public CodeCommentStatistic(bool isLineComment)
        {
            _isLineComment = isLineComment;

            StatisticName = isLineComment ? "LineComment" : "CodeBlockComment";
        }

        public override bool IsStatisticSourceAMatch(ITextItem statisticSource)
        {
            return statisticSource is ICommentedTextData commentedTextData &&
                   commentedTextData.IsLineComment == _isLineComment;
        }

        /// <inheritdoc />
        public override string StatisticName { get; }
    }
}