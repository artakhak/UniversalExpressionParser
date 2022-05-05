namespace UniversalExpressionParser.DemoExpressionLanguageProviders
{
    public class NonVerboseCaseSensitiveExpressionLanguageProvider : DemoExpressionLanguageProviderBase
    {
        /// <inheritdoc />
        public override string LanguageName => "Non-verbose with prefix and keyword support";

        /// <inheritdoc />
        public override string Description => "Expression language provider for C# like languages (vs Visual Basic which use verbose statements for code block tart/end, etc) with prefix and keyword support.";
    }
}