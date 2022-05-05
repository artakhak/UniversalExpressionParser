using System.Collections.Generic;
using System.Linq;
using UniversalExpressionParser.ExpressionItems.Custom;
using UniversalExpressionParser.Tests.TestLanguage;

namespace UniversalExpressionParser.Tests.LanguageProviderValidatorTests
{
    public class TestLanguageProviderForLanguageProviderValidationTests : TestLanguageProviderForFailureValidations
    {
        private static bool _keywordsAndOperatorsAreTurnedOff = false;
        public TestLanguageProviderForLanguageProviderValidationTests()
        {
            if (_keywordsAndOperatorsAreTurnedOff)
            {
                Operators.Clear();
                Keywords.Clear();
            }
            else
            {
                this.Keywords = new List<ILanguageKeywordInfo>
                {
                    new KeywordInfoForTests("where"),
                    new KeywordInfoForTests("pragma"),
                    new KeywordInfoForTests("metadata"),
                    new KeywordInfoForTests("namespace"),
                    new KeywordInfoForTests("class")
                };

                CustomExpressionItemParsers.Clear();

                foreach (var _ in this.Keywords)
                {
                    //  parsedPrefixExpressionItems, IReadOnlyList<IKeywordExpressionItem> parsedKeywordExpressionItemsWithoutLastKeyword
                    var customExpressionItemParserMock = new CustomExpressionItemParserMock();
                    customExpressionItemParserMock.TryCreateCustomExpressionItemForKeyword = 
                        (_, keywordId, parsedPrefixExpressionItems, 
                            parsedKeywordExpressionItemsWithoutLastKeyword, lastKeywordExpressionItem) =>
                    {
                        var keywordInfo = this.Keywords.FirstOrDefault(x => x.Id == keywordId);

                        if (keywordInfo == null)
                            return null;

                        return new CustomExpressionItemMock(parsedPrefixExpressionItems, parsedKeywordExpressionItemsWithoutLastKeyword, lastKeywordExpressionItem, CustomExpressionItemCategory.Regular);
                    };

                    CustomExpressionItemParsers.Add(customExpressionItemParserMock);
                }
            }
        }

        /// <summary>
        /// When an instance of <see cref="TestLanguageProviderForLanguageProviderValidationTests"/> is created, the lists
        /// <see cref="IExpressionLanguageProvider.Operators"/> and <see cref="IExpressionLanguageProvider.Keywords"/> will be empty.
        /// Items can be added to <see cref="TestLanguageProviderForFailureValidations.Operators"/> and <see cref="TestLanguageProviderForFailureValidations.Keywords"/> lists
        /// after an instance of <see cref="TestLanguageProviderForLanguageProviderValidationTests"/> is created.
        /// </summary>
        public static void TurnOffOperatorsAndKeywords()
        {
            _keywordsAndOperatorsAreTurnedOff = true;
        }

        /// <summary>
        /// When an instance of <see cref="TestLanguageProviderForLanguageProviderValidationTests"/> is created, the lists
        /// <see cref="IExpressionLanguageProvider.Operators"/> and <see cref="IExpressionLanguageProvider.Keywords"/> are populated.
        /// </summary>
        public static void TurnOnOperatorsAndKeywords()
        {
            _keywordsAndOperatorsAreTurnedOff = false;
        }

        /// <inheritdoc />
        public override string LanguageName => nameof(TestLanguageProviderForLanguageProviderValidationTests);

        /// <inheritdoc />
        public override string Description => "Test language provider for language provider validation tests";
    }
}