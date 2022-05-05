namespace UniversalExpressionParser.DemoExpressionLanguageProviders
{
    public class SimpleExpressionsWithoutCodeBlocksExpressionLanguageProvider : DemoExpressionLanguageProviderBase
    {
        /// <inheritdoc />
        public override string LanguageName => "Simple expressions (e.g., no ';', '{', and '}').";
        
        /// <inheritdoc />
        public override string Description => "...";

        public override bool SupportsPrefixes => false;
        public override bool SupportsKeywords => false;

        public override char ExpressionSeparatorCharacter => '\0';
        public override string CodeBlockStartMarker => null;
        public override string CodeBlockEndMarker => null;

    }
}