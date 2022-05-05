using JetBrains.Annotations;

namespace UniversalExpressionParser.Tests.TestStatistics
{
    public static class TextItemStatisticsExtensions
    {
        public static void UpdateStatisticsFromParseExpressionResult([NotNull] this ITextItemStatistics textItemStatistics, [NotNull] IParseExpressionResult parseExpressionResult)
        {
            textItemStatistics.UpdateStatistics(parseExpressionResult);

            foreach (var commentedTextData in parseExpressionResult.SortedCommentedTextData)
            {
                textItemStatistics.UpdateStatistics(commentedTextData);
            }
            
            Helpers.ProcessExpressionItem(parseExpressionResult.RootExpressionItem, (expressionItem) =>
            {
                textItemStatistics.UpdateStatistics(expressionItem);
                return true;
            });
        }

        //public static void ValidateAllStatisticsUsed([NotNull] this ITextItemStatistics textItemStatistics)
        //{
        //    foreach (var textItemStatistic in textItemStatistics.TestStatisticsCollection)
        //    {
        //        Assert.IsTrue(textItemStatistic.Counter > 0, $"The counter for text statistic {textItemStatistic.StatisticName} is 0..");
        //    }
        //}
    }
}
