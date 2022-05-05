namespace UniversalExpressionParser.DemoExpressionLanguageProviders
{
    public class VerboseCaseInsensitiveExpressionLanguageProvider : DemoExpressionLanguageProviderBase
    {
        /// <inheritdoc />
        public override string LanguageName => "Verbose and case insensitive";

        /// <inheritdoc />
        public override string Description => "Expression language provider for languages that are ase insensitive and verbose. For example the code block markers are wordy \"begin\" and \"end\", instead of C# format \"{\" a \"}\".";

        public override string LineCommentMarker => "rem";
        public override string MultilineCommentStartMarker => "rem*";
        public override string MultilineCommentEndMarker => "*rem";

        public override bool IsLanguageCaseSensitive => false;
        
        public override string CodeBlockStartMarker => "BEGIN";
        public override string CodeBlockEndMarker => "END";

        
    }
}