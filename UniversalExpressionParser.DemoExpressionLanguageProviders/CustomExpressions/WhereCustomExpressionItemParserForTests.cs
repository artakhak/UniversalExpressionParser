using JetBrains.Annotations;
using UniversalExpressionParser;
using UniversalExpressionParser.ExpressionItems;

namespace UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions
{
    public class WhereCustomExpressionItemParserForTests: WhereCustomExpressionItemParserBase
    {
        /// <inheritdoc />
        protected override bool IsWhereEndReached(IParseExpressionItemContext context)
        {
            return context.StartsWithSymbol(Constants.WhereEndMarker);
        }

        protected override void OnWhereExpressionParsed(IWhereCustomExpressionItem whereCustomExpressionItem, IParseExpressionItemContext context)
        {
            base.OnWhereExpressionParsed(whereCustomExpressionItem, context);

            whereCustomExpressionItem.WhereEndMarker = new NameExpressionItem(
                context.TextSymbolsParser.TextToParse.Substring(context.TextSymbolsParser.PositionInText, Constants.WhereEndMarker.Length),
                context.TextSymbolsParser.PositionInText);

            context.TextSymbolsParser.SkipCharacters(Constants.WhereEndMarker.Length);
            context.TextSymbolsParser.SkipSpaces();
        }
    }
}