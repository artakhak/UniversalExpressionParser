using JetBrains.Annotations;

namespace UniversalExpressionParser.Tests.TestStatistics
{
    public interface IDecoratorStatistic
    {
        [NotNull]
        ITextItemStatistic DecoratedExpressionItemStatistic { get; }
    }
}