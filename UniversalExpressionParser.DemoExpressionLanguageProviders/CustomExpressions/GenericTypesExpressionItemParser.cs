using System.Collections.Generic;
using UniversalExpressionParser.ExpressionItems;
using UniversalExpressionParser.ExpressionItems.Custom;

namespace UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions
{
    // Example: public abstract ::types(T1, T2, T3) Func1(x: T1, y:T2) : T3
    //                  where T1: class where T2: IInterface2, new() where T3: IInterface3;
    public class GenericTypesExpressionItemParser : CustomExpressionItemParserByKeywordId
    {
        public GenericTypesExpressionItemParser() : base(KeywordIds.GenericTypes)
        {
        }

        /// <inheritdoc />
        protected override ICustomExpressionItem DoParseCustomExpressionItem(IParseExpressionItemContext context,
                                                                                   IReadOnlyList<IExpressionItemBase> parsedPrefixExpressionItems, 
                                                                                   IReadOnlyList<IKeywordExpressionItem> parsedKeywordExpressionItemsWithoutLastKeyword, 
                                                                                   IKeywordExpressionItem genericTypesKeywordExpressionItem)
        {
            var genericTypesKeywordInfo = genericTypesKeywordExpressionItem.LanguageKeywordInfo;

            var textSymbolsParser = context.TextSymbolsParser;

            if (!context.SkipSpacesAndComments() || textSymbolsParser.CurrentChar != '[')
            {
                if (!context.ParseErrorData.HasCriticalErrors)
                    context.AddParseErrorItem(new ParseErrorItem(textSymbolsParser.PositionInText,
                    () => $"Generic types keyword '{genericTypesKeywordInfo.Keyword}' should be followed with square braces that list the generic type names. Example: {genericTypesKeywordInfo.Keyword}[T1, T2] Func1(x: T1, y: T2) where T1: class, IInterface1, T2: new(), IInterface2",
                    CustomExpressionParseErrorCodes.GenericTypesKeywordShouldBeFollowedByRoundBraces));
                return null;
            }

            var genericTypesBracesExpressionItem = context.ParseBracesExpression(null);

            return new GenericTypesCustomExpressionItem(parsedPrefixExpressionItems, parsedKeywordExpressionItemsWithoutLastKeyword,
                genericTypesKeywordExpressionItem, genericTypesBracesExpressionItem);
        }
    }
}