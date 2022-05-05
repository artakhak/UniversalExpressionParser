using UniversalExpressionParser.Tests.TestLanguage;

namespace UniversalExpressionParser.Tests.ExpressionParseErrorTests
{
    public class TestLanguageProviderForExpressionParseErrorTests : TestLanguageProviderForFailureValidations
    {
        public TestLanguageProviderForExpressionParseErrorTests()
        {
            
        }

        /// <inheritdoc />
        public override string LanguageName => "Test errors";

        /// <inheritdoc />
        public override string Description => "Test errors...";
    }
}