namespace UniversalExpressionParser.DemoExpressionLanguageProviders
{
    public class SimpleKeywordsDemoExpressionLanguageProvider : DemoExpressionLanguageProviderBase
    {
        /// <inheritdoc />
        public SimpleKeywordsDemoExpressionLanguageProvider()
        {
            // Add 'marker', keyword without custom expression.
        }

        /// <inheritdoc />
        public override string LanguageName => "Simple keywords demo";

        /// <inheritdoc />
        public override string Description => "...";
    }
}