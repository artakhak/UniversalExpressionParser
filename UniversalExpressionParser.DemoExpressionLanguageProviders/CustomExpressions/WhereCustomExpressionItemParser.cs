namespace UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions
{
    public class WhereCustomExpressionItemParser : WhereCustomExpressionItemParserBase
    {
        /// <inheritdoc />
        protected override bool IsWhereEndReached(IParseExpressionItemContext context)
        {
            var expressionLanguageProvider = context.ExpressionLanguageProviderWrapper;
            return context.StartsWithSymbol("=>") || 
                   context.StartsWithSymbol(expressionLanguageProvider.ExpressionLanguageProvider.ExpressionSeparatorCharacter.ToString()) ||
                   context.StartsWithSymbol(expressionLanguageProvider.ExpressionLanguageProvider.CodeBlockStartMarker);
        }
    }
}
