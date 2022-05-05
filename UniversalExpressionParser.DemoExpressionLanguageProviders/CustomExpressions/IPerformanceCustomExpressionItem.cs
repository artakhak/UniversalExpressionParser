using JetBrains.Annotations;
using UniversalExpressionParser.ExpressionItems;

namespace UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions
{
    /// <summary>
    /// Example: F1(x, ::performance("AnonymousFuncStatistics1") () =>  x^2 + factorial(x)}
    /// </summary>
    public interface IPerformanceCustomExpressionItem : ITestLanguageCustomExpression
    {
        [NotNull]
        IBracesExpressionItem ParametersBracesExpressionItem { get; }
    }
}