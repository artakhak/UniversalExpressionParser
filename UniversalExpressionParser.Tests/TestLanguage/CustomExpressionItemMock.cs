using System.Collections.Generic;
using JetBrains.Annotations;
using UniversalExpressionParser.ExpressionItems;
using UniversalExpressionParser.ExpressionItems.Custom;

namespace UniversalExpressionParser.Tests.TestLanguage
{
    public delegate bool IsValidPostfixDelegate([NotNull] IExpressionItemBase postfixExpressionItem, out string customErrorMessage);
    public delegate void OnCustomExpressionItemParsedDelegate([NotNull] IParseExpressionItemContext context);

    public delegate int? GetErrorsPositionDisplayValueDelegate();

    public class CustomExpressionItemMock : KeywordBasedCustomExpressionItem
    {
        /// <inheritdoc />
        public CustomExpressionItemMock([NotNull] [ItemNotNull] IEnumerable<IExpressionItemBase> prefixExpressionItems, 
            [NotNull] [ItemNotNull] IEnumerable<IKeywordExpressionItem> keywordExpressionItems, 
            [NotNull] IKeywordExpressionItem lastKeywordExpressionItem,
            CustomExpressionItemCategory customExpressionItemCategory) : 
            base(prefixExpressionItems, keywordExpressionItems, lastKeywordExpressionItem, customExpressionItemCategory)
        {
           
        }

        public IsValidPostfixDelegate IsValidPostfixDelegate { get; set; }
        public OnCustomExpressionItemParsedDelegate OnCustomExpressionItemParsedDelegate { get; set; }

        public void AddRegularExpressionItem([NotNull] IComplexExpressionItem childExpressionItem)
        {
            AddRegularItem(childExpressionItem);
        }

        public void AddChildExpressionItem([NotNull] IComplexExpressionItem childExpressionItem)
        {
            AddChild(childExpressionItem);
        }

        public override bool IsValidPostfix(IExpressionItemBase postfixExpressionItem, out string customErrorMessage)
        {

            if (!base.IsValidPostfix(postfixExpressionItem, out customErrorMessage) )
                return false;

            if (IsValidPostfixDelegate == null)
                return true;

            return IsValidPostfixDelegate(postfixExpressionItem, out customErrorMessage);
        }

        public override void OnCustomExpressionItemParsed(IParseExpressionItemContext context)
        {
            base.OnCustomExpressionItemParsed(context);

            OnCustomExpressionItemParsedDelegate?.Invoke(context);
        }

        public int? IndexInTextOverride { get; set; }
        public override int IndexInText => IndexInTextOverride ?? base.IndexInText;

        public int? ItemLengthOverride { get; set; }
        public override int ItemLength => ItemLengthOverride ?? base.ItemLength;
        public GetErrorsPositionDisplayValueDelegate GetErrorsPositionDisplayValueDelegate { get; set; }

        public override int? ErrorsPositionDisplayValue
        {
            get
            {
                if (GetErrorsPositionDisplayValueDelegate != null)
                    return GetErrorsPositionDisplayValueDelegate();

                return base.ErrorsPositionDisplayValue;
            }
        }
    }
}