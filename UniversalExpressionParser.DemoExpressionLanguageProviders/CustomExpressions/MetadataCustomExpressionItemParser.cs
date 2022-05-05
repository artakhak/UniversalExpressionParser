using System.Collections.Generic;
using JetBrains.Annotations;
using UniversalExpressionParser;
using UniversalExpressionParser.ExpressionItems;
using UniversalExpressionParser.ExpressionItems.Custom;
using UniversalExpressionParser.DemoExpressionLanguageProviders;

namespace UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions
{
    /// <summary>
    ///  Example: public ::metadata({attributes: [Attribute1, Attribute2]; DisplayName: "Factorial"}) Fact(x: int): int
    /// </summary>
    public class MetadataCustomExpressionItemParser : CustomExpressionItemParserByKeywordId
    {
        public MetadataCustomExpressionItemParser() : base(KeywordIds.Metadata)
        {
        }

        /// <inheritdoc />
        protected override ICustomExpressionItem DoParseCustomExpressionItem(IParseExpressionItemContext context, IReadOnlyList<IExpressionItemBase> parsedPrefixExpressionItems, IReadOnlyList<IKeywordExpressionItem> parsedKeywordExpressionItemsWithoutLastKeyword, IKeywordExpressionItem metadataKeywordExpressionItem)
        {
            var metadataKeywordInfo = metadataKeywordExpressionItem.LanguageKeywordInfo;

            var textSymbolsParser = context.TextSymbolsParser;

            if (!context.SkipSpacesAndComments() || !context.StartsWithSymbol(
                    context.ExpressionLanguageProviderWrapper.ExpressionLanguageProvider.CodeBlockStartMarker))
            {
                if (!context.ParseErrorData.HasCriticalErrors)
                    context.AddParseErrorItem(new ParseErrorItem(textSymbolsParser.PositionInText,
                    () => $"Metadata keyword '{metadataKeywordInfo.Keyword}' should be followed with code block opening brace that contains the metadata.",
                    CustomExpressionParseErrorCodes.MetadataKeywordShouldBeFollowedByRoundBraces));

                return null;
            }

            return new MetadataCustomExpressionItem(parsedPrefixExpressionItems, parsedKeywordExpressionItemsWithoutLastKeyword,
                metadataKeywordExpressionItem, context.ParseCodeBlockExpression());
        }
    }
}