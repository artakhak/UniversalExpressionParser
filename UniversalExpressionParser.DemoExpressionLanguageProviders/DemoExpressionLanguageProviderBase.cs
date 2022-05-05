using System.Collections.Generic;
using TextParser;
using UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions;
using UniversalExpressionParser.ExpressionItems.Custom;

namespace UniversalExpressionParser.DemoExpressionLanguageProviders
{
    public abstract class DemoExpressionLanguageProviderBase : ExpressionLanguageProviderBase
    {
        private readonly List<IOperatorInfo> _operators = new List<IOperatorInfo>();

        /// <inheritdoc />
        protected DemoExpressionLanguageProviderBase()
        {
            InitializeOperators();
        }

        private void InitializeOperators()
        {
            _operators.AddRange(
                new IOperatorInfo[]
            {
                // Unary operators
                new OperatorInfoWithAutoId("-", OperatorType.PrefixUnaryOperator, 0),
                
                // Example (!(x == 0 || x IS NULL))
                new OperatorInfoWithAutoId("!", OperatorType.PrefixUnaryOperator, 0),
                new OperatorInfoWithAutoId("@", OperatorType.PrefixUnaryOperator, 0),
                new OperatorInfoWithAutoId("#", OperatorType.PrefixUnaryOperator, 0),
                new OperatorInfoWithAutoId("++", OperatorType.PrefixUnaryOperator, 0),
                new OperatorInfoWithAutoId("--", OperatorType.PrefixUnaryOperator, 0),

                // Example: return x1+x2;
                new OperatorInfoWithAutoId("return", OperatorType.PrefixUnaryOperator, int.MaxValue),

                // Postfix operators
                new OperatorInfoWithAutoId("++", OperatorType.PostfixUnaryOperator, 1),
                new OperatorInfoWithAutoId("--", OperatorType.PostfixUnaryOperator, 1),

                // SQL
                new OperatorInfoWithAutoId(new [] {"IS", "NOT", "NULL"}, OperatorType.PostfixUnaryOperator, 1),
                new OperatorInfoWithAutoId(new [] {"IS", "NULL"}, OperatorType.PostfixUnaryOperator, 1),

                // Binary operators

                // Type operator. Example var x:int = 5;
                new OperatorInfoWithAutoId(new [] {":"}, OperatorType.BinaryOperator, 0),

                // SQL
                new OperatorInfoWithAutoId("LIKE", OperatorType.PostfixUnaryOperator, 10),

                // Type check (e.g., 'x is number' or 'x is IControl') where number is keyword.
                new OperatorInfoWithAutoId("is", OperatorType.BinaryOperator, 10),

                // Power
                new OperatorInfoWithAutoId("^", OperatorType.BinaryOperator, 10),

                // Multiplicative
                new OperatorInfoWithAutoId("*", OperatorType.BinaryOperator, 20),
                new OperatorInfoWithAutoId("/", OperatorType.BinaryOperator, 20),
                new OperatorInfoWithAutoId("%", OperatorType.BinaryOperator, 20),

                // Additive
                new OperatorInfoWithAutoId("+", OperatorType.BinaryOperator, 30),
                new OperatorInfoWithAutoId("-", OperatorType.BinaryOperator, 30),

                //  Binary left/right shift
                new OperatorInfoWithAutoId(">>", OperatorType.BinaryOperator, 40),
                new OperatorInfoWithAutoId("<<", OperatorType.BinaryOperator, 40),

                // Comparison
                new OperatorInfoWithAutoId("<", OperatorType.BinaryOperator, 50),
                new OperatorInfoWithAutoId("<=", OperatorType.BinaryOperator, 50),
                new OperatorInfoWithAutoId(">", OperatorType.BinaryOperator, 50),
                new OperatorInfoWithAutoId(">=", OperatorType.BinaryOperator, 50),
                new OperatorInfoWithAutoId("==", OperatorType.BinaryOperator, 50),
                new OperatorInfoWithAutoId("!=", OperatorType.BinaryOperator, 50),

                // Bitwise 'and'
                new OperatorInfoWithAutoId("&", OperatorType.BinaryOperator, 60),

                // Bitwise 'or'
                new OperatorInfoWithAutoId("|", OperatorType.BinaryOperator, 70),

                // Logical 'and'
                new OperatorInfoWithAutoId("&&", OperatorType.BinaryOperator, 80),
                new OperatorInfoWithAutoId("AND", OperatorType.BinaryOperator, 80),

                // Logical 'or'
                new OperatorInfoWithAutoId("||", OperatorType.BinaryOperator, 90),
                new OperatorInfoWithAutoId("OR", OperatorType.BinaryOperator, 90),

                // Function 
                new OperatorInfoWithAutoId("=>", OperatorType.BinaryOperator, 1000),

                //BinaryAssignment = 11, // =,  *=,  /=, +=,  -=,  <<=,  >>=,  &=,  |=, &&=, ||= (allow this only in <exe> element. Also make sure read only variables are respected)
                new OperatorInfoWithAutoId("=", OperatorType.BinaryOperator, 2000),
                new OperatorInfoWithAutoId("+=", OperatorType.BinaryOperator, 2000),
                new OperatorInfoWithAutoId("-=", OperatorType.BinaryOperator, 2000),
                new OperatorInfoWithAutoId("*=", OperatorType.BinaryOperator, 2000),
                new OperatorInfoWithAutoId("/=", OperatorType.BinaryOperator, 2000),
                new OperatorInfoWithAutoId("<<=", OperatorType.BinaryOperator, 2000),
                new OperatorInfoWithAutoId(">>=", OperatorType.BinaryOperator, 2000),
                new OperatorInfoWithAutoId("&=", OperatorType.BinaryOperator, 2000),
                new OperatorInfoWithAutoId("|=", OperatorType.BinaryOperator, 2000),
                new OperatorInfoWithAutoId("&&=", OperatorType.BinaryOperator, 2000),
                new OperatorInfoWithAutoId("||=", OperatorType.BinaryOperator, 2000),

               
            });
        }

        /// <inheritdoc />
        public override IReadOnlyList<IOperatorInfo> Operators => _operators;

        /// <inheritdoc />
        public override IReadOnlyList<ILanguageKeywordInfo> Keywords { get; } = new []
        {
            KeywordHelpers.CreateKeyword(KeywordIds.GenericTypes),
            KeywordHelpers.CreateKeyword(KeywordIds.Where),
            KeywordHelpers.CreateKeyword(KeywordIds.Performance),
            KeywordHelpers.CreateKeyword(KeywordIds.Pragma),
            KeywordHelpers.CreateKeyword(KeywordIds.Metadata),
            KeywordHelpers.CreateKeyword(KeywordIds.GlobalIntInlineVarDeclaration),
            KeywordHelpers.CreateKeyword(KeywordIds.Var),
            KeywordHelpers.CreateKeyword(KeywordIds.Public),
            KeywordHelpers.CreateKeyword(KeywordIds.Abstract),
            KeywordHelpers.CreateKeyword(KeywordIds.Virtual),
            KeywordHelpers.CreateKeyword(KeywordIds.Override),
            KeywordHelpers.CreateKeyword(KeywordIds.Static),
            KeywordHelpers.CreateKeyword(KeywordIds.Class),
            KeywordHelpers.CreateKeyword(KeywordIds.CodeMarker)
        };

        public override IEnumerable<ICustomExpressionItemParser> CustomExpressionItemParsers { get; } = new List<ICustomExpressionItemParser>
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
        };

        public override bool SupportsKeywords => true;

        public override bool SupportsPrefixes => true;

        public override bool IsValidLiteralCharacter(char character, int positionInLiteral, ITextSymbolsParserState textSymbolsParserState)
        {
            if (character == '$')
                return true;

            return base.IsValidLiteralCharacter(character, positionInLiteral, textSymbolsParserState);
        }

    }
}
