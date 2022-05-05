namespace UniversalExpressionParser.DemoExpressionLanguageProviders
{
    public class TestOperatorPrioritiesExpressionLanguageProvider : DemoExpressionLanguageProviderBase
    {
        /// <inheritdoc />
        public TestOperatorPrioritiesExpressionLanguageProvider() 
        {
            // Add pref_bin_post like operators we have in error tests.
        }

        /// <inheritdoc />
        public override string LanguageName => "Operator priorities demo";

        /// <inheritdoc />
        public override string Description => "...";
    }
}