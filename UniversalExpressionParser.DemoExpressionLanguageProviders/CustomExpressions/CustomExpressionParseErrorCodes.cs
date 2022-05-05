using System;
using System.Collections.Generic;
using System.Text;

namespace UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions
{
    public static class CustomExpressionParseErrorCodes
    {
        public const int TypeNameMissingInWhereExpression = 10000;
        public const int ColonMissingInWhereExpression = 10001;
        public const int TypeConstraintMissingInWhereExpression = 10002;
        public const int ColonMissingAfterTypeConstraintInWhereExpression = 10003;
        public const int WhereExpressionFollowedByInvalidSymbol = 10004;
        public const int TypeNameOccursMultipleTimesInWhereExpression = 10005;
        public const int GenericTypesKeywordShouldBeFollowedByRoundBraces = 10006;
        public const int GenericTypesKeywordMissesTypeNames = 10007;
        public const int PerformanceKeywordShouldBeFollowedByRoundBraces = 10008;
        public const int PragmaKeywordShouldBeFollowedByValidSymbol = 10009;
        public const int MetadataKeywordShouldBeFollowedByRoundBraces = 100010;
    }
}
