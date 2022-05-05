using System.Collections.Generic;
using JetBrains.Annotations;
using UniversalExpressionParser;
using UniversalExpressionParser.ExpressionItems;
using UniversalExpressionParser.ExpressionItems.Custom;
using UniversalExpressionParser.DemoExpressionLanguageProviders;

namespace UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions
{
    /// <summary>
    ///  Example: F1(x, ::performance("AnonymousFuncStatistics1") () =>  x^2 + factorial(x)}
    /// </summary>
    public class PerformanceCustomExpressionItemParser : CustomExpressionItemParserByKeywordId
    {
        public PerformanceCustomExpressionItemParser() : base(KeywordIds.Performance)
        {
        }

        /// <inheritdoc />
        protected override ICustomExpressionItem DoParseCustomExpressionItem(IParseExpressionItemContext context, IReadOnlyList<IExpressionItemBase> parsedPrefixExpressionItems, IReadOnlyList<IKeywordExpressionItem> parsedKeywordExpressionItemsWithoutLastKeyword, IKeywordExpressionItem performanceKeywordExpressionItem)
        {
            var performanceKeywordInfo = performanceKeywordExpressionItem.LanguageKeywordInfo;

            var textSymbolsParser = context.TextSymbolsParser;

            if (!context.SkipSpacesAndComments() || textSymbolsParser.CurrentChar != '(')
            {
                if (!context.ParseErrorData.HasCriticalErrors)
                    context.AddParseErrorItem(new ParseErrorItem(textSymbolsParser.PositionInText,
                        () => $"Performance keyword '{performanceKeywordInfo.Keyword}' should be followed with round braces that list the parameters. Example: {performanceKeywordInfo.Keyword}(\"AnonymousFuncStatistics1\") () =>  x^2 + factorial(x);",
                    CustomExpressionParseErrorCodes.PerformanceKeywordShouldBeFollowedByRoundBraces));

                return null;
            }

            return new PerformanceCustomExpressionItem(parsedPrefixExpressionItems, parsedKeywordExpressionItemsWithoutLastKeyword,
                performanceKeywordExpressionItem,
                context.ParseBracesExpression(null));
        }
    }
}