using System;
using UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions;
using UniversalExpressionParser.ExpressionItems;
using UniversalExpressionParser.ExpressionItems.Custom;

namespace UniversalExpressionParser.Tests;

public static class ExpressionItemBaseExtensions
{
    public static ExpressionItemType GetItemType(this IExpressionItemBase expressionItem)
    {
        if (expressionItem is ICustomExpressionItem)
            return ExpressionItemType.Custom;

        if (expressionItem is ICustomExpressionItemPart)
            return ExpressionItemType.CustomExpressionItemPart;

        if (expressionItem is IRootExpressionItem)
            return ExpressionItemType.RootExpressionItem;

        if (expressionItem is IOpeningBraceExpressionItem openingBraceExpressionItem)
            return openingBraceExpressionItem.IsRoundBrace ? ExpressionItemType.OpeningRoundBrace : ExpressionItemType.OpeningSquareBrace;

        if (expressionItem is IClosingBraceExpressionItem closingBraceExpressionItem)
            return closingBraceExpressionItem.IsRoundBrace ? ExpressionItemType.ClosingRoundBrace : ExpressionItemType.ClosingSquareBrace;

        if (expressionItem is ICodeBlockStartMarkerExpressionItem)
            return ExpressionItemType.CodeBlockStartMarker;

        if (expressionItem is ICodeBlockEndMarkerExpressionItem)
            return ExpressionItemType.CodeBlockEndMarker;

        if (expressionItem is ICommaExpressionItem)
            return ExpressionItemType.Comma;

        if (expressionItem is ISeparatorCharacterExpressionItem)
            return ExpressionItemType.ExpressionSeparator;

        if (expressionItem is ICodeBlockExpressionItem)
            return ExpressionItemType.CodeBlock;

        if (expressionItem is IConstantTextExpressionItem)
            return ExpressionItemType.ConstantText;

        if (expressionItem is INumericExpressionItem)
            return ExpressionItemType.ConstantNumericValue;

        if (expressionItem is ILiteralExpressionItem)
            return ExpressionItemType.Literal;

        if (expressionItem is IKeywordExpressionItem)
            return ExpressionItemType.Keyword;

        if (expressionItem is IBracesExpressionItem)
            return ExpressionItemType.Braces;

        if (expressionItem is ISeriesOfExpressionItemsWithErrors)
            return ExpressionItemType.SeriesOfExpressionItemsWithErrors;

        if (expressionItem is IOperatorExpressionItem operatorExpressionItem)
        {
            switch (operatorExpressionItem.OperatorInfoExpressionItem.OperatorInfo.OperatorType)
            {
                case OperatorType.BinaryOperator:
                    return ExpressionItemType.BinaryOperator;

                case OperatorType.PostfixUnaryOperator:
                    return ExpressionItemType.PostfixUnaryOperator;

                case OperatorType.PrefixUnaryOperator:
                    return ExpressionItemType.PrefixUnaryOperator;

                default:
                    throw new System.Exception("Invalid value");
            }
        }

        if (expressionItem is IOperatorInfoExpressionItem)
            return ExpressionItemType.OperatorInfo;

        if (expressionItem is ITextExpressionItem)
            return ExpressionItemType.Name;

        throw new ArgumentException($"Invalid value {expressionItem}.");
    }
}