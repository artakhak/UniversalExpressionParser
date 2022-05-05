using UniversalExpressionParser.ExpressionItems;

namespace UniversalExpressionParser.Tests.TestStatistics
{
    public interface IExpressionItemStatistic: ITextItemStatistic
    {
        ExpressionItemType ExpressionItemType { get; }
    }
}