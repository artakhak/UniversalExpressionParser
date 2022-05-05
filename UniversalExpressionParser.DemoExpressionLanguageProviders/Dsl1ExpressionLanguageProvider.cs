using System.Collections.Generic;
using TextParser;
using UniversalExpressionParser.ExpressionItems.Custom;

namespace UniversalExpressionParser.DemoExpressionLanguageProviders
{
    // TODO: Delete this if there is no use of it for demos.
    public class Dsl1ExpressionLanguageProvider : ExpressionLanguageProviderBase
    {
        private readonly List<ILanguageKeywordInfo> _keywords = new List<ILanguageKeywordInfo>();
        public Dsl1ExpressionLanguageProvider() 
        {
            _keywords.Add(new LanguageKeywordInfo(1, "private"));
            _keywords.Add(new LanguageKeywordInfo(2, "protected"));
            _keywords.Add(new LanguageKeywordInfo(3, "public"));
            _keywords.Add(new LanguageKeywordInfo(4, "var"));


            _keywords.Add(new LanguageKeywordInfo(100, "out"));
            _keywords.Add(new LanguageKeywordInfo(101, "ref"));
            _keywords.Add(new LanguageKeywordInfo(102, "_"));

            _keywords.Add(new LanguageKeywordInfo(100, "namespace"));
            _keywords.Add(new LanguageKeywordInfo(101, "class"));
        }
        

        private readonly List<IOperatorInfo> _operators = new List<IOperatorInfo>
        {
            // Unary prefix operators with highest priorities
            new OperatorInfoWithAutoId("!", OperatorType.PrefixUnaryOperator, 0),
            new OperatorInfoWithAutoId("-", OperatorType.PrefixUnaryOperator, 0),

            new OperatorInfoWithAutoId("++", OperatorType.PrefixUnaryOperator, 0),
            new OperatorInfoWithAutoId("--", OperatorType.PrefixUnaryOperator, 0),
            

            // Demo of non-standard operator which is a text.
            new OperatorInfoWithAutoId("TO_BIG_NUM", OperatorType.PrefixUnaryOperator, 0),

            // Demo of multi-part operator.
            new OperatorInfoWithAutoId(new [] {"TO", "INT"}, OperatorType.PrefixUnaryOperator, 0),
            // Demo of multi-part operator.
            new OperatorInfoWithAutoId(new [] {"CONV", "TO", "INT"}, OperatorType.PrefixUnaryOperator, 0),

            // Unary postfix operators
            new OperatorInfoWithAutoId("--", OperatorType.PostfixUnaryOperator, 1),
            new OperatorInfoWithAutoId("++", OperatorType.PostfixUnaryOperator, 1),

            // Demo of non-standard operator which uses text.
            new OperatorInfoWithAutoId("IS_BOOL", OperatorType.PrefixUnaryOperator, 1),

            // Demo of multi-part operator.
            new OperatorInfoWithAutoId(new [] {"IS", "PRIME"}, OperatorType.PrefixUnaryOperator, 1),

            // Demo of multi-part operator.
            new OperatorInfoWithAutoId(new [] {"IS", "NOT", "PRIME"}, OperatorType.PrefixUnaryOperator, 1),

            // Binary operators
            // Power operator
            new OperatorInfoWithAutoId("^", OperatorType.BinaryOperator, 2),

            // Demo of multi-part operator
            new OperatorInfoWithAutoId( new [] {"RAISE", "TO", "PWR"}, OperatorType.PrefixUnaryOperator, 2),

            // Multiplicative  operators
            new OperatorInfoWithAutoId("*", OperatorType.BinaryOperator, 3),
            new OperatorInfoWithAutoId("/", OperatorType.BinaryOperator, 3),
            new OperatorInfoWithAutoId("%", OperatorType.BinaryOperator, 3),

            // Demo of non-standard operator which is a text.
            new OperatorInfoWithAutoId("MULTIPLY", OperatorType.BinaryOperator, 3),
           
            // Additive operators
            new OperatorInfoWithAutoId("+", OperatorType.BinaryOperator, 4),
            new OperatorInfoWithAutoId("-", OperatorType.BinaryOperator, 4),

            // Binary shift operators
            new OperatorInfoWithAutoId("<<", OperatorType.BinaryOperator, 5),
            new OperatorInfoWithAutoId(">>", OperatorType.BinaryOperator, 5),
           
            // Demo of multi-part operator.
            new OperatorInfoWithAutoId(new [] {"LEFT", "SHIFT"}, OperatorType.BinaryOperator, 5),
            

            // Comparison operators
            new OperatorInfoWithAutoId("<", OperatorType.BinaryOperator, 6),
            new OperatorInfoWithAutoId("<=", OperatorType.BinaryOperator, 6),
            new OperatorInfoWithAutoId(">", OperatorType.BinaryOperator, 6),
            new OperatorInfoWithAutoId(">=", OperatorType.BinaryOperator, 6),

            // Demo of multi-part operator.
            new OperatorInfoWithAutoId(new [] {"IS", "LESS", "THAN"}, OperatorType.BinaryOperator, 6),
            new OperatorInfoWithAutoId(new [] {"IS", "LESS"}, OperatorType.BinaryOperator, 6),

            // Equality operators
            new OperatorInfoWithAutoId("==", OperatorType.BinaryOperator, 7),
            new OperatorInfoWithAutoId("!=", OperatorType.BinaryOperator, 7),

            // Bitwise and
            new OperatorInfoWithAutoId("&", OperatorType.BinaryOperator, 8),

            // Bitwise or
            new OperatorInfoWithAutoId("|", OperatorType.BinaryOperator, 9),

            // Conditional and
            new OperatorInfoWithAutoId("&&", OperatorType.BinaryOperator, 10),

            // Conditional or
            new OperatorInfoWithAutoId("||", OperatorType.BinaryOperator, 11),

            // Binary assignment operators. = 11, // =,  *=,  /=, +=,  -=,  <<=,  >>=,  &=,  |=, &&=, ||= 
            new OperatorInfoWithAutoId("=", OperatorType.BinaryOperator, 12),
            new OperatorInfoWithAutoId("*=", OperatorType.BinaryOperator, 12),
            new OperatorInfoWithAutoId("/=", OperatorType.BinaryOperator, 12),
            new OperatorInfoWithAutoId("+=", OperatorType.BinaryOperator, 12),
            new OperatorInfoWithAutoId("-=", OperatorType.BinaryOperator, 12),
            new OperatorInfoWithAutoId("<<=", OperatorType.BinaryOperator, 12),
            new OperatorInfoWithAutoId(">>=", OperatorType.BinaryOperator, 12),
            new OperatorInfoWithAutoId("&=", OperatorType.BinaryOperator, 12),
            new OperatorInfoWithAutoId("|=", OperatorType.BinaryOperator, 12),
            new OperatorInfoWithAutoId("&&=", OperatorType.BinaryOperator, 12),
            new OperatorInfoWithAutoId("||=", OperatorType.BinaryOperator, 12)
        };

        /// <inheritdoc />
        public override string LanguageName => "Dsl1";

        /// <inheritdoc />
        public override string Description => "Dsl1";

        /// <inheritdoc />
        public override IReadOnlyList<IOperatorInfo> Operators => _operators;

        /// <inheritdoc />
        public override IReadOnlyList<ILanguageKeywordInfo> Keywords => _keywords;
       
        /// <inheritdoc />
        public override bool IsValidLiteralCharacter(char character, int positionInLiteral, ITextSymbolsParserState textSymbolsParserState)
        {
            switch (character)
            {

                // Examples of usage of '.' and ':' are:
                // var1._Member1
                // assembly1:MyClass.StaticMethod()
                case '.':
                case ':':
                    return positionInLiteral > 0;
            }

            return base.IsValidLiteralCharacter(character, positionInLiteral, textSymbolsParserState);
        }

        /// <inheritdoc />
        public override IEnumerable<ICustomExpressionItemParser> CustomExpressionItemParsers { get; } =
            new List<ICustomExpressionItemParser>(0);
    }
}