namespace UniversalExpressionParser.Tests.TestStatistics
{
    public interface INonPartExpressionItemAtDepthStatistic : ITextItemStatistic
    {
        int ExpressionItemDepth { get; }
    }
}