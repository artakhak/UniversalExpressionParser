namespace UniversalExpressionParser.Tests.TestStatistics
{
    public interface IBracesExpressionItemStatistic: IExpressionItemStatistic
    {
        bool IsNamedBraces { get; }
        bool IsRoundBraces { get; }
        int NumberOfParameters { get; }
    }
}