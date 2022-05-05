using UniversalExpressionParser.ExpressionItems.Custom;

namespace UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions
{
    public interface ITestLanguageCustomExpression: ICustomExpressionItem
    {
        long KeywordId { get; }
    }
}