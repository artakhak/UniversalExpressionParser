using System.Collections.Generic;
using UniversalExpressionParser.ExpressionItems;
using UniversalExpressionParser.ExpressionItems.Custom;

namespace UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions
{
    /// <summary>
    ///  Example: ::pragma x
    /// </summary>
    public class PragmaCustomExpressionItemParser : CustomExpressionItemParserByKeywordId
    {
        public PragmaCustomExpressionItemParser() : base(KeywordIds.Pragma)
        {
        }

        /// <inheritdoc />
        protected override ICustomExpressionItem DoParseCustomExpressionItem(IParseExpressionItemContext context, IReadOnlyList<IExpressionItemBase> parsedPrefixExpressionItems, 
                                                                           IReadOnlyList<IKeywordExpressionItem> parsedKeywordExpressionItemsWithoutLastKeyword,
                                                                           IKeywordExpressionItem pragmaKeywordExpressionItem)
        {
            var pragmaKeywordInfo = pragmaKeywordExpressionItem.LanguageKeywordInfo;

            var textSymbolsParser = context.TextSymbolsParser;

            if (!context.SkipSpacesAndComments() || !context.TryParseSymbol(out var literalExpressionItem))
            {
                if (!context.ParseErrorData.HasCriticalErrors)
                {
                    // Example: print("Is in debug mode=" + ::pragma IsDebugMode)
                    context.AddParseErrorItem(new ParseErrorItem(textSymbolsParser.PositionInText,
                        () => $"Pragma keyword '{pragmaKeywordInfo.Keyword}' should be followed with pragma symbol. Example: println(\"Is in debug mode = \" + {pragmaKeywordInfo.Keyword} IsDebug);",
                        CustomExpressionParseErrorCodes.PragmaKeywordShouldBeFollowedByValidSymbol));
                }

                return null;
            }

            return new PragmaCustomExpressionItem(parsedPrefixExpressionItems, parsedKeywordExpressionItemsWithoutLastKeyword,
                pragmaKeywordExpressionItem,
                new NameExpressionItem(literalExpressionItem, textSymbolsParser.PositionInText - literalExpressionItem.Length));
        }
    }
}