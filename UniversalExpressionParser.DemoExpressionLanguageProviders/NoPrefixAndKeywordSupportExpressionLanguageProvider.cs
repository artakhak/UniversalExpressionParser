namespace UniversalExpressionParser.DemoExpressionLanguageProviders
{
    public class NoPrefixAndKeywordSupportExpressionLanguageProvider : DemoExpressionLanguageProviderBase
    {
        /// <inheritdoc />
        public override string LanguageName => "Non-verbose with no prefix and keyword support";
        /// <inheritdoc />
        public override string Description => "...";

        public override bool SupportsPrefixes => false;
        public override bool SupportsKeywords => false;
    }
}