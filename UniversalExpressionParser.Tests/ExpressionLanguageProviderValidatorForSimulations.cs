using JetBrains.Annotations;

namespace UniversalExpressionParser.Tests
{
    /// <summary>
    /// An implementation of <see cref="IExpressionLanguageProviderValidator"/> used in parsing simulated expressions.
    /// </summary>
    internal class ExpressionLanguageProviderValidatorForSimulations : DefaultExpressionLanguageProviderValidator
    {
        /*protected override void ValidateOperatorConflictsWithKeyword([NotNull] IExpressionLanguageProvider expressionLanguageProvider, [NotNull] IOperatorInfo operatorInfo, [NotNull] ILanguageKeywordInfo languageKeywordInfo)
        {
            foreach (var operatorNamePart in operatorInfo.NameParts)
            {
                if (Helpers.CheckIfIdentifiersConflict(expressionLanguageProvider,
                        operatorNamePart, languageKeywordInfo.Keyword,
                        "Operator part", "keyword", out var errorMessage))
                    throw new ExpressionLanguageProviderException(expressionLanguageProvider, errorMessage);
            }
        }*/
    }
}
