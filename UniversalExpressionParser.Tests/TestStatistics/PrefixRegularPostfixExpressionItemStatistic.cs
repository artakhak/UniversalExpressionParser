using System.Linq;
using UniversalExpressionParser.ExpressionItems;

namespace UniversalExpressionParser.Tests.TestStatistics
{
    public class PrefixRegularPostfixExpressionItemStatistic : NonPartExpressionItemStatisticBase, IPrefixRegularPostfixExpressionItemStatistic
    {
        public PrefixRegularPostfixExpressionItemStatistic(SpecialExpressionItemType specialExpressionItemType)
        {
            SpecialExpressionItemType = specialExpressionItemType;
        }

        public SpecialExpressionItemType SpecialExpressionItemType { get; }

        public override bool IsStatisticSourceAMatch(ITextItem statisticSource)
        {
            if (!base.IsStatisticSourceAMatch(statisticSource))
                return false;

            if (!(statisticSource is IExpressionItemBase expressionItem) ||
                expressionItem.GetItemType() == ExpressionItemType.Keyword)
                return false;

            switch(SpecialExpressionItemType)
            {
                case SpecialExpressionItemType.Regular:

                    if (expressionItem.Parent == null)
                        return true;

                    return !(expressionItem.Parent.Prefixes.Contains(expressionItem) || expressionItem.Parent.Postfixes.Contains(expressionItem));

                case SpecialExpressionItemType.Prefix:
                    return expressionItem.Parent != null && expressionItem.Parent.Prefixes.Contains(expressionItem);

                case SpecialExpressionItemType.Postfix:
                    return expressionItem.Parent != null && expressionItem.Parent.Postfixes.Contains(expressionItem);

                default:
                    return false;
            }
        }

        public override string StatisticName => $"{SpecialExpressionItemType}ExpressionItem";
    }
}