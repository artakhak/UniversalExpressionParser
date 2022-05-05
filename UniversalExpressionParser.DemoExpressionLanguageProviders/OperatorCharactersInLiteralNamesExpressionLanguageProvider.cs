namespace UniversalExpressionParser.DemoExpressionLanguageProviders
{
    public class OperatorCharactersInLiteralNamesExpressionLanguageProvider : DemoExpressionLanguageProviderBase
    {
        /// <inheritdoc />
        public OperatorCharactersInLiteralNamesExpressionLanguageProvider()
        {
            // TODO: Remove operators that might cause validation fail.
        }

        /// <inheritdoc />
        public override string LanguageName => "Operator characters in literals";

        /// <inheritdoc />
        public override string Description => "...";
    }
}