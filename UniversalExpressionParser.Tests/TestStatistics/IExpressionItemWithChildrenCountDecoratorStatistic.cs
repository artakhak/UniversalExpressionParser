using JetBrains.Annotations;

namespace UniversalExpressionParser.Tests.TestStatistics
{
    public interface IExpressionItemWithChildrenCountDecoratorStatistic : IExpressionItemStatistic
    {
        int NumberOfChildren { get; }

        [NotNull]
        IExpressionItemStatistic DecoratedExpressionItemStatistic { get; }
    }
}