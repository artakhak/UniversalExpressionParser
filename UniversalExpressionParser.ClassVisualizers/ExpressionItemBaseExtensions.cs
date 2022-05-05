using System;
using UniversalExpressionParser.ExpressionItems;
using UniversalExpressionParser.ExpressionItems.Custom;

namespace UniversalExpressionParser.ClassVisualizers
{
    public static class ExpressionItemBaseExtensions
    {
        public static string GetVisualizedExpressionItemElementName(this IExpressionItemBase expressionItem)
        {
            if (expressionItem is IOperatorExpressionItem operatorExpressionItem)
                return $"{nameof(IOperatorExpressionItem)}.{operatorExpressionItem.OperatorInfoExpressionItem.OperatorInfo.OperatorType.GetDisplayValue(true)}";
            
            return GetMainInterface(expressionItem).Name;
        }

        public static Type GetMainInterface(this IExpressionItemBase expressionItem)
        {
            if (expressionItem is ICustomExpressionItem)
                return typeof(ICustomExpressionItem);

            if (expressionItem is IRootExpressionItem)
                return typeof(IRootExpressionItem);

            if (expressionItem is IBracesExpressionItem)
                return typeof(IBracesExpressionItem);

            if (expressionItem is IOpeningBraceExpressionItem)
                return typeof(IOpeningBraceExpressionItem);

            if (expressionItem is IClosingBraceExpressionItem)
                return typeof(IClosingBraceExpressionItem);

            if (expressionItem is ICommaExpressionItem)
                return typeof(ICommaExpressionItem);

            if (expressionItem is ICodeBlockExpressionItem)
                return typeof(ICodeBlockExpressionItem);

            if (expressionItem is ICodeBlockStartMarkerExpressionItem)
                return typeof(ICodeBlockStartMarkerExpressionItem);

            if (expressionItem is ICodeBlockEndMarkerExpressionItem)
                return typeof(ICodeBlockEndMarkerExpressionItem);

            if (expressionItem is ISeparatorCharacterExpressionItem)
                return typeof(ISeparatorCharacterExpressionItem);

            if (expressionItem is ILiteralExpressionItem)
                return typeof(ILiteralExpressionItem);

            if (expressionItem is ILiteralNameExpressionItem)
                return typeof(ILiteralNameExpressionItem);

            if (expressionItem is IConstantTextExpressionItem)
                return typeof(IConstantTextExpressionItem);

            if (expressionItem is IConstantTextValueExpressionItem)
                return typeof(IConstantTextValueExpressionItem);

            if (expressionItem is INumericExpressionItem)
                return typeof(INumericExpressionItem);

            if (expressionItem is INumericExpressionValueItem)
                return typeof(INumericExpressionValueItem);

            if (expressionItem is IKeywordExpressionItem)
                return typeof(IKeywordExpressionItem);

            if (expressionItem is ISeriesOfExpressionItemsWithErrors)
                return typeof(ISeriesOfExpressionItemsWithErrors);

            if (expressionItem is IOperatorExpressionItem)
                return typeof(IOperatorExpressionItem);

            if (expressionItem is IOperatorInfoExpressionItem)
                return typeof(IOperatorInfoExpressionItem);

            if (expressionItem is IOperatorNamePartExpressionItem)
                return typeof(IOperatorNamePartExpressionItem);

            if (expressionItem is ITextExpressionItem)
                return typeof(ITextExpressionItem);

            if (expressionItem is IComplexExpressionItem)
                return typeof(IComplexExpressionItem);

            return typeof(IExpressionItemBase);
        }
    }
}

