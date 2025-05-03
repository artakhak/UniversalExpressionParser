using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using TextParser;
using UniversalExpressionParser.ExpressionItems.Custom;
using UniversalExpressionParser.DemoExpressionLanguageProviders;
using UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions;

namespace UniversalExpressionParser.Tests.TestLanguage
{
    public abstract class TestLanguageProviderForFailureValidations: IExpressionLanguageProvider
    {
        public delegate bool IsValidLiteralCharacterDelegate(char character, int positionInLiteral, [CanBeNull] ITextSymbolsParserState textSymbolsParserState);

        public delegate bool IsCriticalErrorDelegate(int parseErrorItemCode);

        public const string NoCustomExprKeyword1 = "nocusomtexprkwd1";
        public const string NoCustomExprKeyword2 = "nocusomtexprkwd2";

        protected TestLanguageProviderForFailureValidations()
        {
            IsValidLiteralCharacter = this.IsValidLiteralCharacterDefault;

            this.Operators = new List<IOperatorInfo>
            {
                // Unary prefix operators
                new OperatorInfoWithAutoId("!", OperatorType.PrefixUnaryOperator, 0),
                new OperatorInfoWithAutoId("@", OperatorType.PrefixUnaryOperator, 0),
                new OperatorInfoWithAutoId("#", OperatorType.PrefixUnaryOperator, 0),
                new OperatorInfoWithAutoId("-", OperatorType.PrefixUnaryOperator, 0),
              

                new OperatorInfoWithAutoId("++", OperatorType.PrefixUnaryOperator, 0),
                new OperatorInfoWithAutoId("--", OperatorType.PrefixUnaryOperator, 0),

                new OperatorInfoWithAutoId("return", OperatorType.PrefixUnaryOperator, int.MaxValue),

                // Unary postfix operators

                new OperatorInfoWithAutoId("--", OperatorType.PostfixUnaryOperator, 1),
                new OperatorInfoWithAutoId("++", OperatorType.PostfixUnaryOperator, 1),

                // Binary operators
                new OperatorInfoWithAutoId("*", OperatorType.BinaryOperator, 2),
                new OperatorInfoWithAutoId("/", OperatorType.BinaryOperator, 2),
                new OperatorInfoWithAutoId("^", OperatorType.BinaryOperator, 2),

                new OperatorInfoWithAutoId("%", OperatorType.BinaryOperator, 2),
                new OperatorInfoWithAutoId("<<", OperatorType.BinaryOperator, 2),
                new OperatorInfoWithAutoId(">>", OperatorType.BinaryOperator, 2),

                new OperatorInfoWithAutoId("+", OperatorType.BinaryOperator, 3),
                new OperatorInfoWithAutoId("-", OperatorType.BinaryOperator, 3),

                new OperatorInfoWithAutoId("=", OperatorType.BinaryOperator, 10),
                new OperatorInfoWithAutoId("+=", OperatorType.BinaryOperator, 10),
                new OperatorInfoWithAutoId("-=", OperatorType.BinaryOperator, 10),
                new OperatorInfoWithAutoId("*=", OperatorType.BinaryOperator, 10),
                new OperatorInfoWithAutoId("/=", OperatorType.BinaryOperator, 10),
            };

            // Lets add some operators we can use for different tests when the same operator name can be used as prefix, binary, or postfix operator
            for (var priority = 0; priority < 10; ++priority)
            {
                for (var i = 0; i < 10; ++i)
                {
                    // Prefix operators
                    this.Operators.Add(new OperatorInfoWithAutoId($"pref_{i}_{priority}", OperatorType.PrefixUnaryOperator,
                        priority));

                    this.Operators.Add(new OperatorInfoWithAutoId($"pref_bin_{i}_{priority}", OperatorType.PrefixUnaryOperator,
                        priority));
                    this.Operators.Add(new OperatorInfoWithAutoId($"pref_bin_{i}_{priority}", OperatorType.BinaryOperator,
                        priority));

                    this.Operators.Add(new OperatorInfoWithAutoId($"pref_post_{i}_{priority}", OperatorType.PrefixUnaryOperator,
                        priority));
                    this.Operators.Add(new OperatorInfoWithAutoId($"pref_post_{i}_{priority}", OperatorType.PostfixUnaryOperator,
                        priority));

                    this.Operators.Add(new OperatorInfoWithAutoId($"pref_bin_post_{i}_{priority}", OperatorType.PrefixUnaryOperator, priority));
                    this.Operators.Add(new OperatorInfoWithAutoId($"pref_bin_post_{i}_{priority}", OperatorType.BinaryOperator, priority));
                    this.Operators.Add(new OperatorInfoWithAutoId($"pref_bin_post_{i}_{priority}", OperatorType.PostfixUnaryOperator, priority));

                    // Binary operators
                    this.Operators.Add(new OperatorInfoWithAutoId($"bin_{i}_{priority}", OperatorType.BinaryOperator,
                        priority));

                    this.Operators.Add(new OperatorInfoWithAutoId($"bin_post_{i}_{priority}", OperatorType.BinaryOperator,
                        priority));
                    this.Operators.Add(new OperatorInfoWithAutoId($"bin_post_{i}_{priority}", OperatorType.PostfixUnaryOperator,
                        priority));

                    // Postfix operators
                    this.Operators.Add(new OperatorInfoWithAutoId($"post_{i}_{priority}", OperatorType.PostfixUnaryOperator,
                        priority));
                }
            }

            this.Keywords = new List<ILanguageKeywordInfo>
            {
                KeywordHelpers.CreateKeyword(KeywordIds.GenericTypes),
                KeywordHelpers.CreateKeyword(KeywordIds.Where),
                KeywordHelpers.CreateKeyword(KeywordIds.Performance),
                KeywordHelpers.CreateKeyword(KeywordIds.Pragma),
                KeywordHelpers.CreateKeyword(KeywordIds.Metadata),
                KeywordHelpers.CreateKeyword(KeywordIds.GlobalIntInlineVarDeclaration),
                new LanguageKeywordInfo(long.MaxValue, NoCustomExprKeyword1),
                new LanguageKeywordInfo(long.MaxValue-1, NoCustomExprKeyword2)
            };

            CustomExpressionItemParsers = new List<ICustomExpressionItemParser>
            {
                new AggregateCustomExpressionItemParser(new ICustomExpressionItemParserByKeywordId[]
                {
                    new WhereCustomExpressionItemParserForTests(),
                    new GenericTypesExpressionItemParser(),
                    new PerformanceCustomExpressionItemParser(),
                    new PragmaCustomExpressionItemParser(),
                    new MetadataCustomExpressionItemParser(),
                    new KeywordOnlyCustomExpressionItemParser(KeywordIds.GlobalIntInlineVarDeclaration, CustomExpressionItemCategory.Postfix)
                })
            };
        }

        /// <inheritdoc />
        public abstract string LanguageName { get; }

        /// <inheritdoc />
        public abstract string Description { get; }

        /// <inheritdoc />
        public string LineCommentMarker { get; set; } = "//";

        /// <inheritdoc />
        public string MultilineCommentStartMarker { get; set; } = "/*";

        /// <inheritdoc />
        public string MultilineCommentEndMarker { get; set; } = "*/";

        /// <inheritdoc />
        public char ExpressionSeparatorCharacter { get; set; } = ';';

        /// <inheritdoc />
        public string CodeBlockStartMarker { get; set; } = "{";

        /// <inheritdoc />
        public string CodeBlockEndMarker { get; set; } = "}";

        public List<char> ConstantTextStartEndMarkerCharacters { get; set; } = new List<char> { '\'', '\"', '`' };
        IReadOnlyList<char> IExpressionLanguageProvider.ConstantTextStartEndMarkerCharacters => ConstantTextStartEndMarkerCharacters;
       
        public List<IOperatorInfo> Operators { get; set; }
        IReadOnlyList<IOperatorInfo> IExpressionLanguageProvider.Operators => Operators;

        public List<ILanguageKeywordInfo> Keywords { get; set; }

        IReadOnlyList<ILanguageKeywordInfo> IExpressionLanguageProvider.Keywords => Keywords;
        public IsValidLiteralCharacterDelegate IsValidLiteralCharacter { get; set; }

        bool IExpressionLanguageProvider.IsValidLiteralCharacter(char character, int positionInLiteral, ITextSymbolsParserState textSymbolsParserState) =>
        this.IsValidLiteralCharacter(character, positionInLiteral, textSymbolsParserState);

        private bool IsValidLiteralCharacterDefault(char character, int positionInLiteral, ITextSymbolsParserState textSymbolsParserState)
        {
            if (character == '_')
                return true;

            if (Char.IsNumber(character))
                return positionInLiteral > 0;

            return Helpers.IsLatinLetter(character);
        }

        /// <inheritdoc />
        public bool IsLanguageCaseSensitive { get; set; } = true;

        public List<ICustomExpressionItemParser> CustomExpressionItemParsers { get; set; }

        IEnumerable<ICustomExpressionItemParser> IExpressionLanguageProvider.CustomExpressionItemParsers => CustomExpressionItemParsers;

        public List<NumericTypeDescriptor> NumericTypeDescriptors { get; set; } = new(ExpressionLanguageProviderHelpers.GetDefaultNumericTypeDescriptors());
        public bool SupportsPrefixes { get; set; } = true;
        public bool SupportsKeywords { get; set; } = true;
        public IEnumerable<char> SpecialOperatorCharacters => DefaultSpecialCharacters.SpecialOperatorCharacters;
        public IEnumerable<char> SpecialNonOperatorCharacters => DefaultSpecialCharacters.SpecialNonOperatorCharacters;

        bool IExpressionLanguageProvider.SupportsPrefixes => SupportsPrefixes;
        bool IExpressionLanguageProvider.SupportsKeywords => SupportsKeywords;
        
        IReadOnlyList<NumericTypeDescriptor> IExpressionLanguageProvider.NumericTypeDescriptors => NumericTypeDescriptors;
    }
}
