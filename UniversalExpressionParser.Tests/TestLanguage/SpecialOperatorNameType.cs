using System;

namespace UniversalExpressionParser.Tests.TestLanguage
{
    [Flags]
    public enum SpecialOperatorNameType: long
    {
        None = 0,
        NameSimilarToTwoOtherOperatorTypes = 1,
        NameSimilarToOtherBinaryOperator = 2,
        NameSimilarToOtherPrefixUnaryOperator = 4,
        NameSimilarToOtherPostfixUnaryOperator = 8,
        StartsWithOtherOperatorName = 16,
        EndsWithOtherOperatorName = 32,
        UsesOtherOperatorParts = 64,
        StartsWithOtherOperatorNameWithConcatenatedText = 128,
    }
}