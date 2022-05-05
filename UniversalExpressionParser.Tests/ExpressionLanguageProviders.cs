using JetBrains.Annotations;
using System.Collections.Generic;
using UniversalExpressionParser.DemoExpressionLanguageProviders;
using UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions;
using UniversalExpressionParser.ExpressionItems.Custom;
using UniversalExpressionParser.Tests.LanguageProviderValidatorTests;
using UniversalExpressionParser.Tests.SuccessfulParseTests;

namespace UniversalExpressionParser.Tests
{
    public static class ExpressionLanguageProviders
    {
        [NotNull]
        public static TestLanguageProviderForSimulatedSuccessfulParseTests
            TestLanguageProviderForSimulatedSuccessfulParseTests { get; private set; } =
            CreateTestLanguageProviderForSimulatedSuccessfulParseTests();

        [NotNull]
        public static TestLanguageProviderForLanguageProviderValidationTests
            TestLanguageProviderForLanguageProviderValidationTests { get; private set; } =
            new TestLanguageProviderForLanguageProviderValidationTests();

        public static void Regenerate()
        {
            TestLanguageProviderForSimulatedSuccessfulParseTests =
                new TestLanguageProviderForSimulatedSuccessfulParseTests(new List<ICustomExpressionItemParser>
                {
                    new AggregateCustomExpressionItemParser(new ICustomExpressionItemParserByKeywordId[]
                    {
                        new WhereCustomExpressionItemParserForTests(),
                        new GenericTypesExpressionItemParser(),
                        new PerformanceCustomExpressionItemParser(),
                        new PragmaCustomExpressionItemParser(),
                        new MetadataCustomExpressionItemParser(),
                        new KeywordOnlyCustomExpressionItemParser(KeywordIds.GlobalIntInlineVarDeclaration,
                            CustomExpressionItemCategory.Postfix)
                    })
                });

            TestLanguageProviderForLanguageProviderValidationTests = new TestLanguageProviderForLanguageProviderValidationTests();
        }

        private static TestLanguageProviderForSimulatedSuccessfulParseTests CreateTestLanguageProviderForSimulatedSuccessfulParseTests()
        {
            return new TestLanguageProviderForSimulatedSuccessfulParseTests(new List<ICustomExpressionItemParser>
            {
                new AggregateCustomExpressionItemParser(new ICustomExpressionItemParserByKeywordId[]
                {
                    new WhereCustomExpressionItemParserForTests(),
                    new GenericTypesExpressionItemParser(),
                    new PerformanceCustomExpressionItemParser(),
                    new PragmaCustomExpressionItemParser(),
                    new MetadataCustomExpressionItemParser(),
                    new KeywordOnlyCustomExpressionItemParser(KeywordIds.GlobalIntInlineVarDeclaration,
                        CustomExpressionItemCategory.Postfix)
                })
            });
        }
    }
}
