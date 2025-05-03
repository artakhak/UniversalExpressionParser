using JetBrains.Annotations;
using NUnit.Framework;
using OROptimizer.Diagnostics.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TestsSharedLibrary;
using TestsSharedLibrary.Diagnostics.Log;
using TestsSharedLibrary.TestSimulation;
using TestsSharedLibraryForCodeParsers.CodeGeneration;
using TextParser;
using UniversalExpressionParser.ExpressionItems;
using UniversalExpressionParser.ExpressionItems.Custom;
using UniversalExpressionParser.Parser;
using UniversalExpressionParser.Tests.TestLanguage;
using TestLanguageProvider = UniversalExpressionParser.Tests.TestLanguage.TestLanguageProviderForFailureValidations;

namespace UniversalExpressionParser.Tests.ExpressionParseErrorTests
{

    [TestFixture]
    public class ExpressionParseErrorTests : TestsBase
    {
        private static bool LogToLog4NetFile = true;

        private const string SpacesTemplate = "#spaces";
        private const string InjectedInvalidCodeTemplate = "#replace";

        [NotNull]
        private readonly DefaultExpressionLanguageProviderValidator _defaultExpressionLanguageProviderValidator = new DefaultExpressionLanguageProviderValidator();

        /// <summary>
        /// Set this to true only temporarily to not parse the texts that will succeed to make debugging easier.
        /// The default should be false.
        /// </summary>
        private static readonly bool _doNotRunSuccessTests =
#if DEBUG
            false;
#else
            false;
#endif

        /// <summary>
        /// Set this value to re-run the same test for diagnostics. The default should be null.
        /// </summary>
        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        private static int? _presetRandomNumberSeed =
#if DEBUG
            null;
#else
            null;
#endif

        /// <summary>
        /// Normally this can be set this to true, to minimize the number of logged errors for diagnostics.
        /// Tje default should be false.
        /// </summary>
        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        private static bool _doNotAddSpacesAndComments =
#if DEBUG
            false;
#else
            false;
#endif

        [NotNull]
        private IRandomNumberGenerator _randomNumberGenerator = RandomNumberGenerator.CreateWithNullSeed();

        delegate ValidatedParseErrorItem ConvertValidatedParseErrorItemOnSpaceOrCommentAddedDelegate([NotNull] ValidatedParseErrorItem validatedParseErrorItem,
            int addedSpaceCommentPosition, int addedSpaceCommentLength);

        static ExpressionParseErrorTests()
        {
            LogHelper.RemoveContext();

            if (LogToLog4NetFile)
                TestHelpers.RegisterLogger();
            else
                LogHelper.RegisterContext(new LogHelper4TestsContext());
        }

        [SetUp]
        public void SetUp()
        {
            if (_presetRandomNumberSeed != null)
                _randomNumberGenerator = RandomNumberGenerator.CreateWithSeed(_presetRandomNumberSeed.Value);
            else
                _randomNumberGenerator = RandomNumberGenerator.CreateWithNullSeed();

            LogHelper.Context.Log.InfoFormat("Starting a new test. The random value generator seed is {0}.", (_randomNumberGenerator.RandomNumberSeed?.ToString() ?? "null"));
        }

        #region Custom expression tests
        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public void CustomExpressions_IsValidPostfix_Fails(bool usesCustomError)
        {
            var customErrorMessage = "Invalid postfix";

            void TransformTestLanguageProvider(TestLanguageProviderForExpressionParseErrorTests expressionLanguageProvider, string parsedExpression, bool isTransformedForSuccess)
            {
                var classKeywordInfo = new KeywordInfoForTests("class");
                expressionLanguageProvider.Keywords.Add(classKeywordInfo);

                var classPostfixKeywordInfo = new KeywordInfoForTests("class_post_keyword");
                expressionLanguageProvider.Keywords.Add(classPostfixKeywordInfo);

                var customExpressionItemParserMock = new CustomExpressionItemParserMock
                {
                    TryCreateCustomExpressionItemForKeyword = (_, keywordId,
                        parsedPrefixExpressionItems,
                        parsedKeywordExpressionItemsWithoutLastKeyword, lastKeywordExpressionItem) =>
                    {
                        CustomExpressionItemMock customExpressionItemMock = null;

                        if (keywordId == classKeywordInfo.Id)
                        {
                            customExpressionItemMock = new CustomExpressionItemMock(parsedPrefixExpressionItems,
                                parsedKeywordExpressionItemsWithoutLastKeyword, lastKeywordExpressionItem, CustomExpressionItemCategory.Regular);

                            bool IsValidPostfix(IExpressionItemBase postfixExpressionItem, out string errorMessage)
                            {
                                errorMessage = null;

                                if (!isTransformedForSuccess)
                                {
                                    if (usesCustomError)
                                        errorMessage = customErrorMessage;
                                    return false;
                                }

                                errorMessage = null;
                                return true;
                            }

                            customExpressionItemMock.IsValidPostfixDelegate = IsValidPostfix;
                        }
                        else if (keywordId == classPostfixKeywordInfo.Id)
                        {
                            customExpressionItemMock = new CustomExpressionItemMock(parsedPrefixExpressionItems,
                                parsedKeywordExpressionItemsWithoutLastKeyword, lastKeywordExpressionItem, CustomExpressionItemCategory.Postfix);
                        }
                        else
                        {
                            return null;
                        }

                        return customExpressionItemMock;
                    }
                };

                expressionLanguageProvider.CustomExpressionItemParsers.Add(customExpressionItemParserMock);
            }

            var expression = @$"[Attribute1][Tests('TestName')] class MyTests {SpacesTemplate}class_post_keyword x";

            ValidateParseError(TransformTestLanguageProvider, expression, _ => $"{expression} y",
                expr =>
                    new ValidatedParseErrorItem(expr.OrdinalIndexOf("class_post_keyword"),
                    ParseErrorItemCode.CustomPostfixExpressionItemRejectedByPrecedingCustomExpressionItem,
                    () => usesCustomError ? customErrorMessage : ExpressionParserMessages.CustomPostfixIsInvalidError),
                expr => new ValidatedParseErrorItem(expr.OrdinalLastIndexOf("y"),
                    ParseErrorItemCode.NoSeparationBetweenSymbols));
        }

        [Test]
        [TestCase(-1)]
        [TestCase(1)]
        public void CustomExpressions_CustomExpressionItemHasInvalidIndex(int indexOffsetFromCorrectValue)
        {
            void CustomExpressionItemHasInvalidIndexLocal(string expression, Func<string, int> getExpectedCustomExpressionIndex, Func<string, int> getExpectedErrorIndex)
            {
                var errorMessage = string.Empty;
                void TransformTestLanguageProvider(TestLanguageProviderForExpressionParseErrorTests expressionLanguageProvider,
                    string parsedExpression, bool isTransformedForSuccess)
                {
                    var expectedCustomExpressionIndex = getExpectedCustomExpressionIndex(parsedExpression);

                    expressionLanguageProvider.Keywords.Add(new KeywordInfoForTests("var"));
                    expressionLanguageProvider.Keywords.Add(new KeywordInfoForTests("public"));
                    expressionLanguageProvider.Keywords.Add(new KeywordInfoForTests("sealed"));

                    var classKeywordInfo = new KeywordInfoForTests("class");
                    expressionLanguageProvider.Keywords.Add(classKeywordInfo);

                    var customExpressionItemParserMock = new CustomExpressionItemParserMock
                    {
                        TryCreateCustomExpressionItemForKeyword = (_, keywordId,
                            parsedPrefixExpressionItems,
                            parsedKeywordExpressionItemsWithoutLastKeyword, lastKeywordExpressionItem) =>
                        {
                            if (keywordId != classKeywordInfo.Id)
                                return null;

                            var customExpressionItemMock = new CustomExpressionItemMock(parsedPrefixExpressionItems,
                                                           parsedKeywordExpressionItemsWithoutLastKeyword, lastKeywordExpressionItem, CustomExpressionItemCategory.Regular);

                            if (!isTransformedForSuccess)
                            {
                                customExpressionItemMock.IndexInTextOverride = expectedCustomExpressionIndex + indexOffsetFromCorrectValue;
                                errorMessage = $"The value of '{nameof(ICustomExpressionItem.IndexInText)}' is invalid. The invalid value is {customExpressionItemMock.IndexInText}. The value should be {expectedCustomExpressionIndex}.";
                            }

                            return customExpressionItemMock;

                        }
                    };

                    expressionLanguageProvider.CustomExpressionItemParsers.Add(customExpressionItemParserMock);
                }

                ValidateParseError(TransformTestLanguageProvider, expression,
                    validExpression => validExpression.Replace("F(x, y)", "F(x, )"),
                    expr =>
                        new ValidatedParseErrorItem(getExpectedErrorIndex(expr),
                        ParseErrorItemCode.ParsedCustomExpressionItemHasInvalidIndex,
                        () => errorMessage));
            }

            CustomExpressionItemHasInvalidIndexLocal($"var y=x-1;public sealed {SpacesTemplate}class MyTests {{}}; var z=y+1;F(x, y)",
                expression => expression.OrdinalIndexOf("public "),
                expression => expression.OrdinalIndexOf("class "));

            CustomExpressionItemHasInvalidIndexLocal($"var y=x-1;[NotNull, ItemNotNull] [Performance('test')] public sealed {SpacesTemplate}class MyTests {{}}; var z=y+1;F(x, y)",
                expression => expression.OrdinalIndexOf("[NotNull, ItemNotNull]"),
                expression => expression.OrdinalIndexOf("class "));

            CustomExpressionItemHasInvalidIndexLocal("var y=x-1;{SpacesTemplate}class MyTests {{}}; var z=y+1;F(x, y)",
                expression => expression.OrdinalIndexOf("class "),
                expression => expression.OrdinalIndexOf("class "));
        }

        [Test]
        public void CustomExpressions_CustomExpressionItemHasNegativeLength()
        {
            var expression = $"{SpacesTemplate}class A";
            var invalidExpression = $"{expression} F(x, )";

            void TransformTestLanguageProvider(TestLanguageProviderForExpressionParseErrorTests expressionLanguageProvider, string parsedExpression, bool isTransformedForSuccess)
            {
                var classKeywordInfo = new KeywordInfoForTests("class");
                expressionLanguageProvider.Keywords.Add(classKeywordInfo);

                var customExpressionItemParserMock = new CustomExpressionItemParserMock
                {
                    TryCreateCustomExpressionItemForKeyword = (context, keywordId,
                        parsedPrefixExpressionItems,
                        parsedKeywordExpressionItemsWithoutLastKeyword, lastKeywordExpressionItem) =>
                    {
                        CustomExpressionItemMock customExpressionItemMock = null;

                        if (keywordId == classKeywordInfo.Id)
                        {
                            customExpressionItemMock = new CustomExpressionItemMock(parsedPrefixExpressionItems,
                                parsedKeywordExpressionItemsWithoutLastKeyword, lastKeywordExpressionItem, CustomExpressionItemCategory.Regular);

                            if (!isTransformedForSuccess)
                            {
                                customExpressionItemMock.ItemLengthOverride = -1;
                            }
                        }
                        else
                        {
                            return null;
                        }

                        return customExpressionItemMock;
                    }
                };

                expressionLanguageProvider.CustomExpressionItemParsers.Add(customExpressionItemParserMock);
            }

            ValidateParseError(TransformTestLanguageProvider, expression,
                 _ => invalidExpression,
                 (validatedExpression) => new ValidatedParseErrorItem(validatedExpression.OrdinalIndexOf("class"),
                     ParseErrorItemCode.ParsedCustomExpressionItemHasNonPositiveLength,
                     () => "is invalid. The value should be positive."));
        }

        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public void CustomExpressions_CustomExpressionItemParserGeneratesValidationErrors(bool generatesCriticalErrors)
        {
            var expression = $"public sealed {SpacesTemplate}class MyClass {{}}";
            var invalidExpression = $"{expression.Replace("sealed", "var")} F(x, )";
            var validationErrorMessage = "class keyword can be preceded only by public and/or sealed keywords.";

            var customExpressionValidationErrorCode = 100000;

            void TransformTestLanguageProvider(TestLanguageProviderForExpressionParseErrorTests expressionLanguageProvider, string parsedExpression, bool isTransformedForSuccess)
            {
                var publicKeywordInfo = new KeywordInfoForTests("public");
                expressionLanguageProvider.Keywords.Add(publicKeywordInfo);

                var sealedKeywordInfo = new KeywordInfoForTests("sealed");
                expressionLanguageProvider.Keywords.Add(sealedKeywordInfo);

                var classKeywordInfo = new KeywordInfoForTests("class");
                expressionLanguageProvider.Keywords.Add(classKeywordInfo);

                var varKeywordInfo = new KeywordInfoForTests("var");
                expressionLanguageProvider.Keywords.Add(varKeywordInfo);

                var customExpressionItemParserMock = new CustomExpressionItemParserMock
                {
                    TryCreateCustomExpressionItemForKeyword = (context, keywordId,
                        parsedPrefixExpressionItems,
                        parsedKeywordExpressionItemsWithoutLastKeyword, lastKeywordExpressionItem) =>
                    {
                        CustomExpressionItemMock customExpressionItemMock = null;

                        if (keywordId == classKeywordInfo.Id)
                        {
                            customExpressionItemMock = new CustomExpressionItemMock(parsedPrefixExpressionItems,
                                parsedKeywordExpressionItemsWithoutLastKeyword, lastKeywordExpressionItem, CustomExpressionItemCategory.Regular);

                            if (!isTransformedForSuccess)
                            {
                                if (parsedKeywordExpressionItemsWithoutLastKeyword.Any(x =>
                                        x.Id != sealedKeywordInfo.Id))
                                    context.AddParseErrorItem(new ParseErrorItem(parsedExpression.OrdinalIndexOf("class"), () => validationErrorMessage, customExpressionValidationErrorCode, generatesCriticalErrors));
                            }
                        }
                        else
                        {
                            return null;
                        }

                        return customExpressionItemMock;
                    }
                };

                expressionLanguageProvider.CustomExpressionItemParsers.Add(customExpressionItemParserMock);
            }

            var errors = new List<GetValidatedParseErrorItemDelegate>()
            {
                (expr) => new ValidatedParseErrorItem(expr.OrdinalIndexOf("class"),
                    customExpressionValidationErrorCode,
                    () => validationErrorMessage)
            };

            if (!generatesCriticalErrors)
                errors.Add(expr => new ValidatedParseErrorItem(expr.OrdinalIndexOf(",") + 1,
                    ParseErrorItemCode.ExpressionMissingAfterComma));

            ValidateParseError(TransformTestLanguageProvider, expression,
                 _ => invalidExpression, errors.ToArray());
        }

        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public void CustomExpression_OnCustomExpressionItemParsed_Fails(bool generatesCriticalErrors)
        {
            var expression = $"public sealed {SpacesTemplate}class MyClass {{}}";
            var invalidExpression = $"{expression.Replace("sealed", "var")} F(x, )";
            var validationErrorMessage = "class keyword can be preceded only by public and/or sealed keywords.";

            var customExpressionValidationErrorCode = 100000;

            void TransformTestLanguageProvider(TestLanguageProviderForExpressionParseErrorTests expressionLanguageProvider, string parsedExpression, bool isTransformedForSuccess)
            {
                var publicKeywordInfo = new KeywordInfoForTests("public");
                expressionLanguageProvider.Keywords.Add(publicKeywordInfo);

                var sealedKeywordInfo = new KeywordInfoForTests("sealed");
                expressionLanguageProvider.Keywords.Add(sealedKeywordInfo);

                var classKeywordInfo = new KeywordInfoForTests("class");
                expressionLanguageProvider.Keywords.Add(classKeywordInfo);

                var varKeywordInfo = new KeywordInfoForTests("var");
                expressionLanguageProvider.Keywords.Add(varKeywordInfo);

                var customExpressionItemParserMock = new CustomExpressionItemParserMock
                {
                    TryCreateCustomExpressionItemForKeyword = (context, keywordId,
                        parsedPrefixExpressionItems,
                        parsedKeywordExpressionItemsWithoutLastKeyword, lastKeywordExpressionItem) =>
                    {
                        CustomExpressionItemMock customExpressionItemMock = null;

                        if (keywordId == classKeywordInfo.Id)
                        {
                            customExpressionItemMock = new CustomExpressionItemMock(parsedPrefixExpressionItems,
                                parsedKeywordExpressionItemsWithoutLastKeyword, lastKeywordExpressionItem, CustomExpressionItemCategory.Regular);

                            if (!isTransformedForSuccess)
                            {
                                customExpressionItemMock.OnCustomExpressionItemParsedDelegate = (_) =>
                                {
                                    if (customExpressionItemMock.AppliedKeywords.Any(x =>
                                        !(x.Id == classKeywordInfo.Id || x.Id == sealedKeywordInfo.Id)))
                                    {
                                        context.AddParseErrorItem(
                                            new ParseErrorItem(parsedExpression.OrdinalIndexOf("class"), () => validationErrorMessage, customExpressionValidationErrorCode, generatesCriticalErrors));
                                    }
                                };
                            }
                        }
                        else
                        {
                            return null;
                        }

                        return customExpressionItemMock;
                    }
                };

                expressionLanguageProvider.CustomExpressionItemParsers.Add(customExpressionItemParserMock);
            }

            var errors = new List<GetValidatedParseErrorItemDelegate>
            {
                expr => new ValidatedParseErrorItem(expr.OrdinalIndexOf("class"),
                customExpressionValidationErrorCode,
                () => validationErrorMessage)
            };

            if (!generatesCriticalErrors)
                errors.Add(expr => new ValidatedParseErrorItem(expr.OrdinalIndexOf(",") + 1,
                    ParseErrorItemCode.ExpressionMissingAfterComma));

            ValidateParseError(TransformTestLanguageProvider, expression,
                 _ => invalidExpression, errors.ToArray());
        }

        [Test]
        public void CustomExpressions_CustomExpressionItemParserThrowsException()
        {
            var expression = $"public sealed class {SpacesTemplate}MyClass {{}}; F(x, y)";
            var invalidExpression = expression.Replace("F(x, y)", "F(x, )");

            void TransformTestLanguageProvider(TestLanguageProviderForExpressionParseErrorTests expressionLanguageProvider, string parsedExpression, bool isTransformedForSuccess)
            {
                var publicKeywordInfo = new KeywordInfoForTests("public");
                expressionLanguageProvider.Keywords.Add(publicKeywordInfo);

                var sealedKeywordInfo = new KeywordInfoForTests("sealed");
                expressionLanguageProvider.Keywords.Add(sealedKeywordInfo);

                var classKeywordInfo = new KeywordInfoForTests("class");
                expressionLanguageProvider.Keywords.Add(classKeywordInfo);

                var varKeywordInfo = new KeywordInfoForTests("var");
                expressionLanguageProvider.Keywords.Add(varKeywordInfo);

                var customExpressionItemParserMock = new CustomExpressionItemParserMock
                {
                    ThrowException = !isTransformedForSuccess,

                    TryCreateCustomExpressionItemForKeyword = (_, keywordId,
                        parsedPrefixExpressionItems,
                        parsedKeywordExpressionItemsWithoutLastKeyword, lastKeywordExpressionItem) =>
                    {
                        CustomExpressionItemMock customExpressionItemMock = null;

                        if (keywordId == classKeywordInfo.Id)
                        {
                            customExpressionItemMock = new CustomExpressionItemMock(parsedPrefixExpressionItems,
                                parsedKeywordExpressionItemsWithoutLastKeyword, lastKeywordExpressionItem, CustomExpressionItemCategory.Regular);
                        }
                        else
                        {
                            return null;
                        }

                        return customExpressionItemMock;
                    }
                };

                expressionLanguageProvider.CustomExpressionItemParsers.Add(customExpressionItemParserMock);
            }

            ValidateParseError(TransformTestLanguageProvider, expression,
                 _ => invalidExpression,
                 expr => new ValidatedParseErrorItem(expr.OrdinalIndexOf("MyClass"),
                     ParseErrorItemCode.CustomExpressionParserThrewAnException,
                     () => $"Custom expression parser '{typeof(CustomExpressionItemParserMock).FullName}' threw an exception."));
        }

        [Test]
        public void CustomExpressions_PostfixCustomExpressionItemAppliedToPrefixCustomExpressionItem()
        {
            var expression = $"::pragma IsDebugMode {SpacesTemplate}where T1: class where T2: IInterface2, new() where T3: IInterface3 whereend";
            var invalidExpression = string.Concat(expression.Replace("::pragma IsDebugMode", "::performance(f1(x))"), " x++");

            ValidateParseError(expression, validExpression =>
                    invalidExpression,
                expr =>
                    new ValidatedParseErrorItem(expr.OrdinalIndexOf("where T1:"), ParseErrorItemCode.CustomPostfixExpressionItemAfterNonRegularExpressionItem,
                    () => ExpressionParserMessages.CustomPostfixIsInvalidError),
                    expr => new ValidatedParseErrorItem(expr.OrdinalLastIndexOf("x++"), ParseErrorItemCode.NoSeparationBetweenSymbols,
                    () => ExpressionParserMessages.NoSeparationBetweenSymbolsError));
        }

        [TestCase(ParseErrorItemCode.CustomPostfixExpressionItemHasNoTargetExpression)]
        [TestCase(ParseErrorItemCode.CustomPostfixExpressionItemFollowsInvalidExpression)]
        public void CustomExpressions_PostfixCustomExpressionHasNoTargetExpression(int postfixErrorType)
        {
            var expression = $"f+z;A[] {SpacesTemplate}postfixkwd1 B; F(x, y)";
            string invalidExpression;
            if (postfixErrorType == ParseErrorItemCode.CustomPostfixExpressionItemHasNoTargetExpression)
                invalidExpression = expression.Replace("A[]", "");
            else if (postfixErrorType == ParseErrorItemCode.CustomPostfixExpressionItemFollowsInvalidExpression)
                invalidExpression = expression.Replace("A[]", "15");
            else
                throw new ArgumentException($"Invalid value {postfixErrorType}.", nameof(postfixErrorType));

            invalidExpression = invalidExpression.Replace("F(x, y)", "F(x, )");

            void TransformTestLanguageProvider(TestLanguageProviderForExpressionParseErrorTests expressionLanguageProvider, string parsedExpression, bool isTransformedForSuccess)
            {
                var postfixKeyword1Info = new KeywordInfoForTests("postfixkwd1");
                expressionLanguageProvider.Keywords.Add(postfixKeyword1Info);

                var customExpressionItemParserMock = new CustomExpressionItemParserMock
                {
                    TryCreateCustomExpressionItemForKeyword = (_, keywordId,
                        parsedPrefixExpressionItems,
                        parsedKeywordExpressionItemsWithoutLastKeyword, lastKeywordExpressionItem) =>
                    {
                        if (keywordId == postfixKeyword1Info.Id)
                        {
                            return new CustomExpressionItemMock(parsedPrefixExpressionItems,
                                parsedKeywordExpressionItemsWithoutLastKeyword, lastKeywordExpressionItem, CustomExpressionItemCategory.Postfix);
                        }

                        return null;
                    }
                };

                expressionLanguageProvider.CustomExpressionItemParsers.Add(customExpressionItemParserMock);
            }

            ValidateParseError(TransformTestLanguageProvider, expression,
                 _ => invalidExpression,
                 expr => new ValidatedParseErrorItem(
                     expr.OrdinalIndexOf("postfixkwd1"),
                     postfixErrorType,
                     () => ExpressionParserMessages.CustomPostfixIsInvalidError),
                 expr => new ValidatedParseErrorItem(
                     expr.OrdinalIndexOf(",") + 1,
                     ParseErrorItemCode.ExpressionMissingAfterComma));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void CustomExpressions_PrefixOrRegularCustomExpressionFollowsInvalidExpression(bool isPrefixCustomExpression)
        {
            var prefixes = "[NotNull, ItemNotNull] [MustUseReturnValue]";
            var expression = $"{prefixes} {SpacesTemplate}keyword1 B {(isPrefixCustomExpression ? "C[0]" : string.Empty)}; F(x, y)";

            var invalidExpression = expression.Replace(prefixes, "A").Replace("F(x, y)", "F(x, )");

            void TransformTestLanguageProvider(TestLanguageProviderForExpressionParseErrorTests expressionLanguageProvider, string parsedExpression, bool isTransformedForSuccess)
            {
                var keyword1Info = new KeywordInfoForTests("keyword1");
                expressionLanguageProvider.Keywords.Add(keyword1Info);

                var customExpressionItemParserMock = new CustomExpressionItemParserMock
                {
                    TryCreateCustomExpressionItemForKeyword = (_, keywordId,
                        parsedPrefixExpressionItems,
                        parsedKeywordExpressionItemsWithoutLastKeyword, lastKeywordExpressionItem) =>
                    {
                        if (keywordId == keyword1Info.Id)
                        {
                            return new CustomExpressionItemMock(parsedPrefixExpressionItems,
                                parsedKeywordExpressionItemsWithoutLastKeyword,
                                lastKeywordExpressionItem,
                                isPrefixCustomExpression ? CustomExpressionItemCategory.Prefix : CustomExpressionItemCategory.Regular);
                        }

                        return null;
                    }
                };

                expressionLanguageProvider.CustomExpressionItemParsers.Add(customExpressionItemParserMock);
            }

            ValidateParseError(TransformTestLanguageProvider, expression,
                 _ => invalidExpression,
                 expr => new ValidatedParseErrorItem(
                     expr.OrdinalIndexOf("keyword1"),
                     ParseErrorItemCode.NoSeparationBetweenSymbols,
                     () => ExpressionParserMessages.NoSeparationBetweenSymbolsError),
                 expr => new ValidatedParseErrorItem(
                     expr.OrdinalLastIndexOf(",") + 1,
                     ParseErrorItemCode.ExpressionMissingAfterComma));
        }

        public enum CustomExpressionIndexOverrideType
        {
            /// <summary>
            /// The parser will determine the error position
            /// </summary>
            Null,

            /// <summary>
            /// The implementation of <see cref="ICustomExpressionItem"/> specifies the error position.
            /// </summary>
            Overridden
        }

        [TestCase(CustomExpressionIndexOverrideType.Null)]
        [TestCase(CustomExpressionIndexOverrideType.Overridden)]
        public void CustomExpression_TestErrorPositionOverrides(CustomExpressionIndexOverrideType customExpressionIndexOverrideType)
        {
            const string invalidPostfixErrorMessage = "Invalid postfix test message";

            var expression = $"[NotNull, ItemNotNull] [MustUseReturnValue] keyword1 A {SpacesTemplate}postfix1 B + F(x, y)";
            var invalidExpression = expression.Replace("F(x, y)", "F(x, )");

            void TransformTestLanguageProvider(TestLanguageProviderForExpressionParseErrorTests expressionLanguageProvider, string parsedExpression, bool isTransformedForSuccess)
            {
                var keyword1Info = new KeywordInfoForTests("keyword1");
                expressionLanguageProvider.Keywords.Add(keyword1Info);

                var keyword2Info = new KeywordInfoForTests("postfix1");
                expressionLanguageProvider.Keywords.Add(keyword2Info);

                var customExpressionItemParserMock = new CustomExpressionItemParserMock
                {
                    TryCreateCustomExpressionItemForKeyword = (_, keywordId,
                        parsedPrefixExpressionItems,
                        parsedKeywordExpressionItemsWithoutLastKeyword, lastKeywordExpressionItem) =>
                    {
                        if (keywordId == keyword1Info.Id)
                        {
                            bool IsValidPostfix([NotNull] IExpressionItemBase postfixExpressionItem,
                                out string customErrorMessage)
                            {
                                customErrorMessage = null;

                                if (isTransformedForSuccess)
                                    return true;

                                customErrorMessage = invalidPostfixErrorMessage;
                                return false;
                            }
                            return new CustomExpressionItemMock(parsedPrefixExpressionItems,
                                parsedKeywordExpressionItemsWithoutLastKeyword,
                                lastKeywordExpressionItem, CustomExpressionItemCategory.Regular)
                            {
                                IsValidPostfixDelegate = IsValidPostfix
                            };
                        }

                        if (keywordId == keyword2Info.Id)
                        {
                            var customExpressionItem = new CustomExpressionItemMock(parsedPrefixExpressionItems,
                                parsedKeywordExpressionItemsWithoutLastKeyword,
                                lastKeywordExpressionItem, CustomExpressionItemCategory.Postfix);

                            if (!isTransformedForSuccess)
                            {
                                switch (customExpressionIndexOverrideType)
                                {
                                    case CustomExpressionIndexOverrideType.Null:
                                        customExpressionItem.GetErrorsPositionDisplayValueDelegate = () => null;
                                        break;

                                    case CustomExpressionIndexOverrideType.Overridden:
                                        customExpressionItem.GetErrorsPositionDisplayValueDelegate = () => parsedExpression.OrdinalIndexOf(keyword2Info.Keyword) + 1;
                                        break;
                                }
                            }

                            return customExpressionItem;
                        }

                        return null;
                    }
                };

                expressionLanguageProvider.CustomExpressionItemParsers.Add(customExpressionItemParserMock);
            }

            var errors = new List<GetValidatedParseErrorItemDelegate>
            {
                expr => new ValidatedParseErrorItem(

                    expr.OrdinalIndexOf("postfix1") + (customExpressionIndexOverrideType == CustomExpressionIndexOverrideType.Overridden ? 1 : 0),
                    ParseErrorItemCode.CustomPostfixExpressionItemRejectedByPrecedingCustomExpressionItem,
                    () => invalidPostfixErrorMessage),

                expr => new ValidatedParseErrorItem(
                    expr.OrdinalLastIndexOf(",") + 1, ParseErrorItemCode.ExpressionMissingAfterComma,
                    () => null)
            };

            ValidateParseError(TransformTestLanguageProvider, expression,
                _ => invalidExpression, errors.ToArray());
        }

        [TestCase(false)]
        [TestCase(true)]
        public void CustomExpressions_PrefixExpressionHasNoTargetExpression(bool usePrefixes)
        {
            var prefixes = "[NotNull, ItemNotNull] [MustUseReturnValue]";
            var expression = $"{(usePrefixes ? $"{prefixes} " : string.Empty)}{SpacesTemplate}prefix1 C[0] PrefixTarget; F(x, y)";

            var invalidExpression = expression.Replace(" C[0]", "").Replace("F(x, y)", "F(x, )");

            void TransformTestLanguageProvider(TestLanguageProviderForExpressionParseErrorTests expressionLanguageProvider, string parsedExpression, bool isTransformedForSuccess)
            {
                var keywordInfo = new KeywordInfoForTests("prefix1");
                expressionLanguageProvider.Keywords.Add(keywordInfo);

                var customExpressionItemParserMock = new CustomExpressionItemParserMock
                {
                    TryCreateCustomExpressionItemForKeyword = (_, keywordId,
                        parsedPrefixExpressionItems,
                        parsedKeywordExpressionItemsWithoutLastKeyword, lastKeywordExpressionItem) =>
                    {
                        if (keywordId == keywordInfo.Id)
                        {
                            return new CustomExpressionItemMock(parsedPrefixExpressionItems,
                                parsedKeywordExpressionItemsWithoutLastKeyword,
                                lastKeywordExpressionItem, CustomExpressionItemCategory.Prefix);
                        }

                        return null;
                    }
                };

                expressionLanguageProvider.CustomExpressionItemParsers.Add(customExpressionItemParserMock);
            }

            ValidateParseError(TransformTestLanguageProvider, expression,
                 _ => invalidExpression,
                 expr => new ValidatedParseErrorItem(
                     usePrefixes ? expr.OrdinalIndexOf("[NotNull") : expr.OrdinalIndexOf("prefix1"),
                     ParseErrorItemCode.InvalidUseOfPrefixes,
                     () => ExpressionParserMessages.CustomPrefixExpressionHasNoTargetError),
                 expr => new ValidatedParseErrorItem(expr.OrdinalLastIndexOf(",") + 1,
                     ParseErrorItemCode.ExpressionMissingAfterComma));
        }

        [TestCase(false)]
        [TestCase(true)]
        public void CustomExpressions_CustomExpressionIndexIsIncorrect(bool usePrefixes)
        {
            var prefixes = "[NotNull, ItemNotNull] [MustUseReturnValue]";
            var expression = $"{(usePrefixes ? $"{prefixes} " : string.Empty)}{SpacesTemplate}prefix1 C x+F(y, z)";

            void TransformTestLanguageProvider(TestLanguageProviderForExpressionParseErrorTests expressionLanguageProvider, string parsedExpression, bool isTransformedForSuccess)
            {
                var keywordInfo = new KeywordInfoForTests("prefix1");
                expressionLanguageProvider.Keywords.Add(keywordInfo);

                var customExpressionItemParserMock = new CustomExpressionItemParserMock
                {
                    TryCreateCustomExpressionItemForKeyword = (_, keywordId,
                        parsedPrefixExpressionItems,
                        parsedKeywordExpressionItemsWithoutLastKeyword, lastKeywordExpressionItem) =>
                    {
                        if (keywordId == keywordInfo.Id)
                        {
                            return new CustomExpressionItemMock(parsedPrefixExpressionItems,
                                parsedKeywordExpressionItemsWithoutLastKeyword,
                                lastKeywordExpressionItem, CustomExpressionItemCategory.Prefix)
                            {
                                IndexInTextOverride = isTransformedForSuccess ? null : parsedExpression.OrdinalIndexOf("prefix1") + 1,
                                GetErrorsPositionDisplayValueDelegate = () => parsedExpression.OrdinalIndexOf("prefix1")
                            };
                        }

                        return null;
                    }
                };

                expressionLanguageProvider.CustomExpressionItemParsers.Add(customExpressionItemParserMock);
            }

            var invalidExpression = expression.Replace("F(y, z)", "F(y, )");
            ValidateParseError(TransformTestLanguageProvider, expression,
                 _ => invalidExpression,
                 expr => new ValidatedParseErrorItem(
                     expr.OrdinalIndexOf("prefix1"),
                     ParseErrorItemCode.ParsedCustomExpressionItemHasInvalidIndex,
                     () => $"The value of '{nameof(ICustomExpressionItem.IndexInText)}' is invalid."));
        }

        [TestCase(true, true)]
        [TestCase(true, false)]
        [TestCase(false, true)]
        [TestCase(false, false)]
        public void CustomExpressions_CustomExpressionParserAddsError(bool returnsNullCustomExpressionItem, bool isCriticalError)
        {
            var expression = $"keyword1 {SpacesTemplate}A+F(y, z)";
            var invalidExpression = expression.Replace("F(y, z)", "F(y, )");

            void TransformTestLanguageProvider(TestLanguageProviderForExpressionParseErrorTests expressionLanguageProvider, string parsedExpression, bool isTransformedForSuccess)
            {
                var keywordInfo = new KeywordInfoForTests("keyword1");
                expressionLanguageProvider.Keywords.Add(keywordInfo);

                var customExpressionItemParserMock = new CustomExpressionItemParserMock
                {
                    AddErrorOnParse = isTransformedForSuccess ? null : () => (parsedExpression.OrdinalIndexOf("A"), returnsNullCustomExpressionItem, isCriticalError),
                    TryCreateCustomExpressionItemForKeyword = (_, keywordId,
                        parsedPrefixExpressionItems,
                        parsedKeywordExpressionItemsWithoutLastKeyword, lastKeywordExpressionItem) =>
                    {
                        if (keywordId == keywordInfo.Id)
                        {
                            return new CustomExpressionItemMock(parsedPrefixExpressionItems,
                                parsedKeywordExpressionItemsWithoutLastKeyword,
                                lastKeywordExpressionItem, CustomExpressionItemCategory.Regular);
                        }

                        return null;
                    }
                };

                expressionLanguageProvider.CustomExpressionItemParsers.Add(customExpressionItemParserMock);
            }

            var errors = new List<GetValidatedParseErrorItemDelegate>
            {
                expr => new ValidatedParseErrorItem(
                    expr.OrdinalIndexOf("A+F("),
                    CustomExpressionItemParserMock.CustomErrorCode)
            };

            if (!returnsNullCustomExpressionItem && !isCriticalError)
                errors.Add(expr => new ValidatedParseErrorItem(
                    expr.OrdinalLastIndexOf(",") + 1,
                    ParseErrorItemCode.ExpressionMissingAfterComma));

            ValidateParseError(TransformTestLanguageProvider, expression,
                _ => invalidExpression, errors.ToArray());
        }
        #endregion

        #region Operator tests

        #region Binary operator expected tests
        private void ValidateParseErrorForBinaryOperatorExpected([NotNull] string expression, [NotNull] Func<string, string> getInvalidExpression,
            [NotNull] Func<string, int> getBinaryOperatorMissingIndex)
        {
            ValidateParseError(expression,
                getInvalidExpression,
                expr =>
                    new ValidatedParseErrorItem(getBinaryOperatorMissingIndex(expr), ParseErrorItemCode.BinaryOperatorMissing,
                        ExpressionParserMessages.BinaryOperatorMissingError),

                expr =>
                    new ValidatedParseErrorItem(expr.OrdinalIndexOf(",") + 1, ParseErrorItemCode.ExpressionMissingAfterComma));
        }

        [Test]
        public void Operators_BinaryOperatorExpected_PostfixesAndPrefixes1()
        {

            ValidateParseErrorForBinaryOperatorExpected($"x post_0_0 post_1_0 bin_0_0 pref_post_0_0 pref_post_1_0 {SpacesTemplate}pref_0_0  pref_post_2_0 pref_1_0 pref_2_0 y; F(x, y)",
                expr => expr.Replace("bin_0_0", "")
                    .Replace("F(x, y)", "F(x, )"), expr => expr.OrdinalIndexOf("pref_0_0"));
        }

        [Test]
        public void Operators_BinaryOperatorExpected_PostfixesAndPrefixes2()
        {

            ValidateParseErrorForBinaryOperatorExpected($"x post_0_0 post_1_0 pref_post_0_0 pref_bin_post_1_0 pref_post_1_0 pref_post_2_0 pref_post_3_0 {SpacesTemplate}pref_2_0 y; F(x, y)",
                expr => expr.Replace("pref_bin_post_1_0", "")
                    .Replace("F(x, y)", "F(x, )"), expr => expr.OrdinalIndexOf("pref_2_0"));
        }

        [Test]
        public void Operators_BinaryOperatorExpected_PostfixesAndPrefixes3()
        {

            ValidateParseErrorForBinaryOperatorExpected($"x post_0_0 pref_bin_post_1_0 {SpacesTemplate}pref_0_0 pref_post_1_0 pref_post_2_0 pref_1_0 y; F(x, y)",
                expr => expr.Replace("pref_bin_post_1_0", "")
                    .Replace("F(x, y)", "F(x, )"), expr => expr.OrdinalIndexOf("pref_0_0"));
        }

        [Test]
        public void Operators_BinaryOperatorExpected_PostfixesAndPrefixes4()
        {

            ValidateParseErrorForBinaryOperatorExpected($"x pref_post_0_0 pref_bin_post_1_0 {SpacesTemplate}pref_1_0 y; F(x, y)",
                expr => expr.Replace("pref_bin_post_1_0", "")
                    .Replace("F(x, y)", "F(x, )"),
                expr => expr.OrdinalIndexOf("pref_1_0"));
        }

        [Test]
        public void Operators_BinaryOperatorExpected_PostfixesOnly1()
        {
            ValidateParseErrorForBinaryOperatorExpected($"x1 pref_post_0_0 pref_bin_post_0_0 pref_post_1_0 {SpacesTemplate}x2; F(x, y)",
                expr => expr.Replace("pref_bin_post_0_0", "")
                    .Replace("F(x, y)", "F(x, )"), expr => expr.OrdinalIndexOf("x2"));
        }

        [Test]
        public void Operators_BinaryOperatorExpected_PostfixesOnly2()
        {
            ValidateParseErrorForBinaryOperatorExpected($"x1 pref_post_0_0 pref_bin_post_0_0 {SpacesTemplate}x2; F(x, y)",
                expr => expr.Replace("pref_bin_post_0_0", "")
                    .Replace("F(x, y)", "F(x, )"), expr => expr.OrdinalIndexOf("x2"));
        }

        [Test]
        public void Operators_BinaryOperatorExpected_PrefixesOnly1()
        {
            ValidateParseErrorForBinaryOperatorExpected($"x1 pref_bin_post_0_0 {SpacesTemplate}pref_0_0 pref_1_0 x2; F(x, y)",
                expr => expr.Replace("pref_bin_post_0_0", "")
                    .Replace("F(x, y)", "F(x, )"), expr => expr.OrdinalIndexOf("pref_0_0"));
        }

        [Test]
        public void Operators_BinaryOperatorExpected_PrefixesOnly2()
        {
            ValidateParseErrorForBinaryOperatorExpected($"x1 pref_bin_post_0_0 {SpacesTemplate}pref_0_0 x2; F(x, y)",
                expr => expr.Replace("pref_bin_post_0_0", "")
                    .Replace("F(x, y)", "F(x, )"), expr => expr.OrdinalIndexOf("pref_0_0"));
        }
        #endregion

        #region Postfix operator expected tests
        private void ValidateParseErrorForPostfixOperatorExpected([NotNull] string expression, [NotNull] Func<string, string> getInvalidExpression,
            [NotNull] Func<string, int> getBinaryOperatorMissingIndex)
        {
            ValidateParseError(expression,
                getInvalidExpression,
                expr =>
                    new ValidatedParseErrorItem(getBinaryOperatorMissingIndex(expr), ParseErrorItemCode.ExpectedPostfixOperator,
                        ExpressionParserMessages.ExpectedPostfixOperatorError),

                expr =>
                    new ValidatedParseErrorItem(expr.OrdinalIndexOf(",") + 1, ParseErrorItemCode.ExpressionMissingAfterComma));
        }

        [Test]
        public void Operators_PostfixOperatorExpected_PrefixesAndBinaryOnly1()
        {
            ValidateParseErrorForPostfixOperatorExpected("x1 pref_bin_post_0_0 pref_bin_post_1_0 pref_bin_post_2_0; F(x, y)",
                expr => "x1 pref_bin_0_0 pref_bin_1_0 pref_bin_2_0; F(x, )",
                expr => expr.OrdinalIndexOf("pref_bin_0_0"));
        }

        [Test]
        public void Operators_PostfixOperatorExpected_PrefixesAndBinaryOnly2()
        {
            ValidateParseErrorForPostfixOperatorExpected("x1 pref_bin_post_0_0 pref_bin_post_1_0 pref_bin_post_2_0; F(x, y)",
                expr => "x1 bin_0_0 pref_1_0 pref_bin_2_0; F(x, )",
                expr => expr.OrdinalIndexOf("bin_0_0"));
        }

        [Test]
        public void Operators_PostfixOperatorExpected_PrefixesAndBinaryOnly3()
        {
            ValidateParseErrorForPostfixOperatorExpected("x1 pref_bin_post_0_0 pref_bin_post_1_0 pref_bin_post_2_0; F(x, y)",
                expr => "x1 pref_0_0 bin_1_0 pref_bin_2_0; F(x, )",
                expr => expr.OrdinalIndexOf("pref_0_0"));
        }

        [Test]
        public void Operators_PostfixOperatorExpected_PrefixesAndBinaryOnly4()
        {
            ValidateParseErrorForPostfixOperatorExpected("x1 pref_bin_post_0_0 pref_bin_post_1_0 pref_bin_post_2_0; F(x, y)",
                expr => "x1 pref_0_0 bin_0_0; F(x, )",
                expr => expr.OrdinalIndexOf("pref_0_0"));
        }


        [Test]
        public void Operators_PostfixOperatorExpected_PrefixesAndBinaryOnly5()
        {
            ValidateParseErrorForPostfixOperatorExpected("x1 pref_bin_post_0_0 pref_bin_post_1_0 pref_bin_post_2_0; F(x, y)",
                expr => "x1 bin_0_0 pref_0_0; F(x, )",
                expr => expr.OrdinalIndexOf("bin_0_0"));
        }

        [Test]
        public void Operators_PostfixOperatorExpected_PostfixesThenPrefixesAndBinaryOnly1()
        {
            ValidateParseErrorForPostfixOperatorExpected("x1 pref_bin_post_0_0 pref_bin_post_1_0 pref_bin_post_2_0; F(x, y)",
                expr => "x1 pref_bin_post_0_0 pref_bin_post_1_0 pref_0_0 bin_0_0; F(x, )",
                expr => expr.OrdinalIndexOf("pref_0_0"));
        }

        [Test]
        public void Operators_PostfixOperatorExpected_PostfixesThenPrefixesAndBinaryOnly2()
        {
            ValidateParseErrorForPostfixOperatorExpected("x1 pref_bin_post_0_0 pref_bin_post_1_0 pref_bin_post_2_0; F(x, y)",
                expr => "x1 pref_bin_post_0_0 pref_bin_post_1_0 bin_0_0 pref_0_0; F(x, )",
                expr => expr.OrdinalIndexOf("bin_0_0"));
        }

        #endregion

        #region Prefix operator expected tests
        //[Test]
        //public void Operators_PrefixOperatorExpected()
        //{
        //    var expression = $"{SpacesTemplate}++{SpacesTemplate}5;F(x, y)";
        //    var invalidExpression = $"{SpacesTemplate}>>{SpacesTemplate}5;F(x, )";

        //    ValidateParseError(expression,
        //        _ => invalidExpression,
        //        expr =>
        //            new ValidatedParseErrorItem(expr.OrdinalLastIndexOf(">>"), CodeParseErrorCode.ExpectedPrefixOperator,
        //                ExpressionParserMessages.ExpectedPrefixOperatorError),
        //        expr =>
        //            new ValidatedParseErrorItem(expr.OrdinalIndexOf(",") + 1, CodeParseErrorCode.ExpressionMissingAfterComma));
        //}

        private void ValidateParseErrorForPrefixOperatorExpected([NotNull] string expression, [NotNull] Func<string, string> getInvalidExpression,
            [NotNull] Func<string, int> getBinaryOperatorMissingIndex)
        {
            ValidateParseError(expression,
                getInvalidExpression,
                expr =>
                    new ValidatedParseErrorItem(getBinaryOperatorMissingIndex(expr), ParseErrorItemCode.ExpectedPrefixOperator,
                        ExpressionParserMessages.ExpectedPrefixOperatorError),

                expr =>
                    new ValidatedParseErrorItem(expr.OrdinalIndexOf(",") + 1, ParseErrorItemCode.ExpressionMissingAfterComma));
        }

        [Test]
        public void Operators_PrefixOperatorExpected_PostfixesAndBinaryOnly1()
        {
            ValidateParseErrorForPrefixOperatorExpected("pref_bin_post_0_0 pref_bin_post_1_0 pref_bin_post_2_0 x1; F(x, y)",
                expr => "bin_post_0_0 bin_post_1_0 bin_post_2_0 x1; F(x, )",
                expr => expr.OrdinalIndexOf("bin_post_0_0"));
        }

        [Test]
        public void Operators_PrefixOperatorExpected_PostfixesAndBinaryOnly2()
        {
            ValidateParseErrorForPrefixOperatorExpected("pref_bin_post_0_0 pref_bin_post_1_0 pref_bin_post_2_0 x1; F(x, y)",
                expr => "bin_0_0 post_1_0 bin_post_1_0 x1; F(x, )",
                expr => expr.OrdinalIndexOf("bin_0_0"));
        }

        [Test]
        public void Operators_PrefixOperatorExpected_PostfixesAndBinaryOnly3()
        {
            ValidateParseErrorForPrefixOperatorExpected("pref_bin_post_0_0 pref_bin_post_1_0 pref_bin_post_2_0 x1; F(x, y)",
                expr => "post_1_0 bin_0_0 bin_post_1_0 x1; F(x, )",
                expr => expr.OrdinalIndexOf("post_1_0"));
        }

        [Test]
        public void Operators_PrefixOperatorExpected_PostfixesAndBinaryOnly4()
        {
            ValidateParseErrorForPrefixOperatorExpected("pref_bin_post_0_0 pref_bin_post_1_0 pref_bin_post_2_0 x1; F(x, y)",
                expr => "post_0_0 bin_0_0 x1; F(x, )",
                expr => expr.OrdinalIndexOf("post_0_0"));
        }

        [Test]
        public void Operators_PrefixOperatorExpected_PostfixesAndBinaryOnly5()
        {
            ValidateParseErrorForPrefixOperatorExpected("pref_bin_post_0_0 pref_bin_post_1_0 pref_bin_post_2_0 x1; F(x, y)",
                expr => "bin_0_0 post_0_0 x1; F(x, )",
                expr => expr.OrdinalIndexOf("bin_0_0"));
        }

        [Test]
        public void Operators_PrefixOperatorExpected_PrefixesThenPostfixesAndBinaryOnly1()
        {
            ValidateParseErrorForPrefixOperatorExpected("pref_bin_post_0_0 pref_bin_post_1_0 pref_bin_post_2_0 x1; F(x, y)",
                expr => "pref_bin_post_0_0 pref_bin_post_1_0 post_2_0 bin_2_0 x1; F(x, )",
                expr => expr.OrdinalIndexOf("post_2_0"));
        }

        [Test]
        public void Operators_PrefixOperatorExpected_PrefixesThenPostfixesAndBinaryOnly2()
        {
            ValidateParseErrorForPrefixOperatorExpected("pref_bin_post_0_0 pref_bin_post_1_0 pref_bin_post_2_0 x1; F(x, y)",
                expr => "pref_bin_post_0_0 pref_bin_post_1_0 bin_2_0 post_2_0 x1; F(x, )",
                expr => expr.OrdinalIndexOf("bin_2_0"));
        }
        #endregion


        #endregion

        #region No separation between symbols tests
        [TestCase("Symbol1")]
        [TestCase("Z(x,y)")]
        [TestCase("158.3")]
        [TestCase("'Text 1'")]
        public void NoSeparationBetweenSymbols_BeforeLiterals([NotNull] string precedingSymbol)
        {
            var expression = string.Concat(
                $"x+{InjectedInvalidCodeTemplate}{SpacesTemplate} y; {{x={InjectedInvalidCodeTemplate}{SpacesTemplate} F1(1, 2)}}",
                Environment.NewLine,
                "F2(x2, y2)");

            ValidateParseError(expression,
                expr =>
                    expr.Replace(InjectedInvalidCodeTemplate, precedingSymbol)
                        .Replace("F2(x2, y2)", "F2(x2, )"),

                expr =>
                    new ValidatedParseErrorItem(expr.OrdinalLastIndexOf("y;"), ParseErrorItemCode.NoSeparationBetweenSymbols,
                        ExpressionParserMessages.NoSeparationBetweenSymbolsError),

                expr =>
                    new ValidatedParseErrorItem(expr.OrdinalLastIndexOf("F1("), ParseErrorItemCode.NoSeparationBetweenSymbols,
                        ExpressionParserMessages.NoSeparationBetweenSymbolsError),
                expr =>
                    new ValidatedParseErrorItem(expr.OrdinalLastIndexOf(",") + 1, ParseErrorItemCode.ExpressionMissingAfterComma));
        }

        [TestCase("Symbol1")]
        [TestCase("Z(x,y)")]
        [TestCase("158.3")]
        [TestCase("'Text 1'")]
        public void NoSeparationBetweenSymbols_BeforeTexts([NotNull] string precedingSymbol)
        {
            var expression = string.Concat(
                $"x+{InjectedInvalidCodeTemplate}{SpacesTemplate} 'MyText 10' + {InjectedInvalidCodeTemplate}{SpacesTemplate} \"MyText 11\" + {InjectedInvalidCodeTemplate}{SpacesTemplate} `MyText 12`;",
                Environment.NewLine,
                "F2(x2, y2)");

            ValidateParseError(expression,
                expr =>
                    expr.Replace(InjectedInvalidCodeTemplate, precedingSymbol)
                        .Replace("F2(x2, y2)", "F2(x2, )"),

                expr =>
                    new ValidatedParseErrorItem(expr.OrdinalIndexOf("'MyText 10'"), ParseErrorItemCode.NoSeparationBetweenSymbols,
                        ExpressionParserMessages.NoSeparationBetweenSymbolsError),

                expr =>
                    new ValidatedParseErrorItem(expr.OrdinalIndexOf("\"MyText 11\""), ParseErrorItemCode.NoSeparationBetweenSymbols,
                        ExpressionParserMessages.NoSeparationBetweenSymbolsError),

                expr =>
                    new ValidatedParseErrorItem(expr.OrdinalIndexOf("`MyText 12`"), ParseErrorItemCode.NoSeparationBetweenSymbols,
                        ExpressionParserMessages.NoSeparationBetweenSymbolsError),

                expr =>
                    new ValidatedParseErrorItem(expr.OrdinalLastIndexOf(",") + 1, ParseErrorItemCode.ExpressionMissingAfterComma));
        }

        [TestCase("Z(x,y)")]
        [TestCase("Z[x,y]")]
        [TestCase("158.3")]
        [TestCase("'Text 1'")]
        public void NoSeparationBetweenSymbols_BeforeBraces([NotNull] string precedingSymbol)
        {
            void ValidateParseErrorLocal(bool isRoundBraces)
            {
                var braces = GetBraces(isRoundBraces);

                var expression = string.Concat(
                    $"{InjectedInvalidCodeTemplate}{SpacesTemplate} {InjectedInvalidCodeTemplate}{SpacesTemplate} {braces.openingBrace}x1, x2{braces.closingBrace};",
                    Environment.NewLine,
                    "F2(x2, y2)");

                ValidateParseError(expression,
                    expr =>
                        expr.Replace(InjectedInvalidCodeTemplate, precedingSymbol)
                            .Replace("F2(x2, y2)", "F2(x2, )"),

                    expr =>
                        new ValidatedParseErrorItem(expr.OrdinalIndexOf(precedingSymbol, 1), ParseErrorItemCode.NoSeparationBetweenSymbols,
                            ExpressionParserMessages.NoSeparationBetweenSymbolsError),

                    expr =>
                        new ValidatedParseErrorItem(expr.OrdinalIndexOf($"{braces.openingBrace}x1"), ParseErrorItemCode.NoSeparationBetweenSymbols,
                            ExpressionParserMessages.NoSeparationBetweenSymbolsError),

                    expr =>
                        new ValidatedParseErrorItem(expr.OrdinalLastIndexOf(",") + 1, ParseErrorItemCode.ExpressionMissingAfterComma));
            }

            ValidateParseErrorLocal(true);
            ValidateParseErrorLocal(false);
        }

        [TestCase("Symbol1")]
        [TestCase("Z(x,y)")]
        [TestCase("158.3")]
        [TestCase("'Text 1'")]
        public void NoSeparationBetweenSymbols_BeforeNumericValues([NotNull] string precedingSymbol)
        {
            var expression = string.Concat(
                $"x+{InjectedInvalidCodeTemplate}{SpacesTemplate} 163.4 + {InjectedInvalidCodeTemplate}{SpacesTemplate} {InjectedInvalidCodeTemplate}{SpacesTemplate} 169;",
                Environment.NewLine,
                "F2(x2, y2)");

            ValidateParseError(expression,
                expr =>
                    expr.Replace(InjectedInvalidCodeTemplate, precedingSymbol)
                        .Replace("F2(x2, y2)", "F2(x2, )"),

                expr =>
                    new ValidatedParseErrorItem(expr.OrdinalIndexOf("163.4"), ParseErrorItemCode.NoSeparationBetweenSymbols,
                        ExpressionParserMessages.NoSeparationBetweenSymbolsError),

                expr =>
                    new ValidatedParseErrorItem(expr.OrdinalIndexOf(precedingSymbol, 2), ParseErrorItemCode.NoSeparationBetweenSymbols,
                        ExpressionParserMessages.NoSeparationBetweenSymbolsError),

                expr =>
                    new ValidatedParseErrorItem(expr.OrdinalIndexOf("169"), ParseErrorItemCode.NoSeparationBetweenSymbols,
                        ExpressionParserMessages.NoSeparationBetweenSymbolsError),

                expr =>
                    new ValidatedParseErrorItem(expr.OrdinalLastIndexOf(",") + 1, ParseErrorItemCode.ExpressionMissingAfterComma));
        }

        [TestCase("Symbol1")]
        [TestCase("Z(x,y)")]
        [TestCase("158.3")]
        [TestCase("'Text 1'")]
        public void NoSeparationBetweenSymbols_BeforeCustomExpressions([NotNull] string precedingSymbol)
        {
            var expression = string.Concat(
                $"x+{InjectedInvalidCodeTemplate}{SpacesTemplate} keyword1 A + {InjectedInvalidCodeTemplate}{SpacesTemplate} {InjectedInvalidCodeTemplate}{SpacesTemplate} keyword1 B;",
                Environment.NewLine,
                "F2(x2, y2)");

            void TransformTestLanguageProvider(TestLanguageProviderForExpressionParseErrorTests expressionLanguageProvider, string parsedExpression, bool isTransformedForSuccess)
            {
                var keywordInfo = new KeywordInfoForTests("keyword1");
                expressionLanguageProvider.Keywords.Add(keywordInfo);

                var customExpressionItemParserMock = new CustomExpressionItemParserMock
                {
                    TryCreateCustomExpressionItemForKeyword = (_, keywordId,
                        parsedPrefixExpressionItems,
                        parsedKeywordExpressionItemsWithoutLastKeyword, lastKeywordExpressionItem) =>
                    {
                        if (keywordId == keywordInfo.Id)
                        {
                            return new CustomExpressionItemMock(parsedPrefixExpressionItems,
                                parsedKeywordExpressionItemsWithoutLastKeyword,
                                lastKeywordExpressionItem, CustomExpressionItemCategory.Regular);
                        }

                        return null;
                    }
                };

                expressionLanguageProvider.CustomExpressionItemParsers.Add(customExpressionItemParserMock);
            }

            ValidateParseError(TransformTestLanguageProvider, expression,
                expr =>
                    expr.Replace(InjectedInvalidCodeTemplate, precedingSymbol)
                        .Replace("F2(x2, y2)", "F2(x2, )"),

                expr =>
                    new ValidatedParseErrorItem(expr.OrdinalIndexOf("keyword1 A"), ParseErrorItemCode.NoSeparationBetweenSymbols,
                        ExpressionParserMessages.NoSeparationBetweenSymbolsError),

                expr =>
                    new ValidatedParseErrorItem(expr.OrdinalIndexOf(precedingSymbol, 2), ParseErrorItemCode.NoSeparationBetweenSymbols,
                        ExpressionParserMessages.NoSeparationBetweenSymbolsError),

                expr =>
                    new ValidatedParseErrorItem(expr.OrdinalIndexOf("keyword1 B"), ParseErrorItemCode.NoSeparationBetweenSymbols,
                        ExpressionParserMessages.NoSeparationBetweenSymbolsError),

                expr =>
                    new ValidatedParseErrorItem(expr.OrdinalLastIndexOf(",") + 1, ParseErrorItemCode.ExpressionMissingAfterComma));
        }
        #endregion

        #region Invalid symbol tests

        private void ReGenerateNonLatinCharactersForInvalidSymbolsTests([NotNull] string invalidSymbol)
        {
            CodeGenerationHelper.ReGenerateNonLatinCharacters((17000, 100));
            foreach (var nonLatinChar in invalidSymbol)
                Assert.IsFalse(CodeGenerationHelper.NonLatinCharacters.Contains(nonLatinChar));
        }

        [TestCase("ф")]
        [TestCase("фϨ")]
        [TestCase("фϨϭ")]
        public void InvalidSymbols_BeforeLiterals([NotNull] string invalidSymbol)
        {
            ReGenerateNonLatinCharactersForInvalidSymbolsTests(invalidSymbol);

            var expression = string.Concat(
                $"{InjectedInvalidCodeTemplate}{SpacesTemplate}x+{InjectedInvalidCodeTemplate}{SpacesTemplate}y;",
                Environment.NewLine,
                $" {{x={InjectedInvalidCodeTemplate}{SpacesTemplate} {InjectedInvalidCodeTemplate}{SpacesTemplate}F1(1, 2)}}",
                Environment.NewLine,
                $"{InjectedInvalidCodeTemplate}{SpacesTemplate}F2(x2, y2);");

            ValidateParseError(expression,
                expr =>
                    expr.Replace(InjectedInvalidCodeTemplate, invalidSymbol)
                        .Replace("F2(x2, y2)", "F2(x2, )"),

                expr => new ValidatedParseErrorItem(expr.OrdinalIndexOf(invalidSymbol), ParseErrorItemCode.InvalidSymbol,
                    ExpressionParserMessages.InvalidSymbolError),
                expr => new ValidatedParseErrorItem(expr.OrdinalIndexOf(invalidSymbol, 1), ParseErrorItemCode.InvalidSymbol,
                    ExpressionParserMessages.InvalidSymbolError),
                expr => new ValidatedParseErrorItem(expr.OrdinalIndexOf(invalidSymbol, 2), ParseErrorItemCode.InvalidSymbol,
                    ExpressionParserMessages.InvalidSymbolError),
                expr => new ValidatedParseErrorItem(expr.OrdinalIndexOf(invalidSymbol, 3), ParseErrorItemCode.InvalidSymbol,
                    ExpressionParserMessages.InvalidSymbolError),
                expr => new ValidatedParseErrorItem(expr.OrdinalIndexOf(invalidSymbol, 4), ParseErrorItemCode.InvalidSymbol,
                    ExpressionParserMessages.InvalidSymbolError),

                expr =>
                    new ValidatedParseErrorItem(expr.OrdinalLastIndexOf(",") + 1, ParseErrorItemCode.ExpressionMissingAfterComma));
        }

        [TestCase("ф")]
        [TestCase("фϨ")]
        [TestCase("фϨϭ")]
        public void InvalidSymbols_BeforeTextExpressions([NotNull] string invalidSymbol)
        {
            ReGenerateNonLatinCharactersForInvalidSymbolsTests(invalidSymbol);

            var expression = string.Concat(
                $"text = {InjectedInvalidCodeTemplate}{SpacesTemplate} {InjectedInvalidCodeTemplate}{SpacesTemplate}'Text1' + {InjectedInvalidCodeTemplate}{SpacesTemplate}\"Text 2\" + {InjectedInvalidCodeTemplate}{SpacesTemplate}`Text 3`;",
                Environment.NewLine,
                "F(x2, y2);");

            ValidateParseError(expression,
                expr =>
                    expr.Replace(InjectedInvalidCodeTemplate, invalidSymbol)
                        .Replace("F(x2, y2)", "F(x2, )"),

                expr => new ValidatedParseErrorItem(expr.OrdinalIndexOf(invalidSymbol), ParseErrorItemCode.InvalidSymbol,
                    ExpressionParserMessages.InvalidSymbolError),
                expr => new ValidatedParseErrorItem(expr.OrdinalIndexOf(invalidSymbol, 1), ParseErrorItemCode.InvalidSymbol,
                    ExpressionParserMessages.InvalidSymbolError),
                expr => new ValidatedParseErrorItem(expr.OrdinalIndexOf(invalidSymbol, 2), ParseErrorItemCode.InvalidSymbol,
                    ExpressionParserMessages.InvalidSymbolError),
                expr => new ValidatedParseErrorItem(expr.OrdinalIndexOf(invalidSymbol, 3), ParseErrorItemCode.InvalidSymbol,
                    ExpressionParserMessages.InvalidSymbolError),

                expr =>
                    new ValidatedParseErrorItem(expr.OrdinalLastIndexOf(",") + 1, ParseErrorItemCode.ExpressionMissingAfterComma));
        }

        [TestCase("ф")]
        [TestCase("фϨ")]
        [TestCase("фϨϭ")]
        public void InvalidSymbols_BeforeNumericValues([NotNull] string invalidSymbol)
        {
            ReGenerateNonLatinCharactersForInvalidSymbolsTests(invalidSymbol);

            var expression = string.Concat(
                $"value = {InjectedInvalidCodeTemplate}{SpacesTemplate} {InjectedInvalidCodeTemplate}{SpacesTemplate}158.3 + {InjectedInvalidCodeTemplate}{SpacesTemplate}165;",
                Environment.NewLine,
                "F(x2, y2);");

            ValidateParseError(expression,
                expr =>
                    expr.Replace(InjectedInvalidCodeTemplate, invalidSymbol)
                        .Replace("F(x2, y2)", "F(x2, )"),

                expr => new ValidatedParseErrorItem(expr.OrdinalIndexOf(invalidSymbol), ParseErrorItemCode.InvalidSymbol,
                    ExpressionParserMessages.InvalidSymbolError),
                expr => new ValidatedParseErrorItem(expr.OrdinalIndexOf(invalidSymbol, 1), ParseErrorItemCode.InvalidSymbol,
                    ExpressionParserMessages.InvalidSymbolError),
                expr => new ValidatedParseErrorItem(expr.OrdinalIndexOf(invalidSymbol, 2), ParseErrorItemCode.InvalidSymbol,
                    ExpressionParserMessages.InvalidSymbolError),

                expr =>
                    new ValidatedParseErrorItem(expr.OrdinalLastIndexOf(",") + 1, ParseErrorItemCode.ExpressionMissingAfterComma));
        }

        [TestCase("ф")]
        [TestCase("фϨ")]
        [TestCase("фϨϭ")]
        public void InvalidSymbols_BeforeOperators([NotNull] string invalidSymbol)
        {
            ReGenerateNonLatinCharactersForInvalidSymbolsTests(invalidSymbol);

            var expression = string.Concat(
                $"x = {InjectedInvalidCodeTemplate}{SpacesTemplate}--x1 {InjectedInvalidCodeTemplate}{SpacesTemplate}+x2{InjectedInvalidCodeTemplate}{SpacesTemplate}++;",
                Environment.NewLine,
                $"x = {InjectedInvalidCodeTemplate}{SpacesTemplate} {InjectedInvalidCodeTemplate}{SpacesTemplate}--x1 {InjectedInvalidCodeTemplate}{SpacesTemplate} {InjectedInvalidCodeTemplate}{SpacesTemplate}+x2{InjectedInvalidCodeTemplate}{SpacesTemplate} {InjectedInvalidCodeTemplate}{SpacesTemplate}++;",
                Environment.NewLine,
                "F(x2, y2);");

            ValidateParseError(expression,
                expr =>
                    expr.Replace(InjectedInvalidCodeTemplate, invalidSymbol)
                        .Replace("F(x2, y2)", "F(x2, )"),

                expr => new ValidatedParseErrorItem(expr.OrdinalIndexOf(invalidSymbol), ParseErrorItemCode.InvalidSymbol,
                    ExpressionParserMessages.InvalidSymbolError),
                expr => new ValidatedParseErrorItem(expr.OrdinalIndexOf(invalidSymbol, 1), ParseErrorItemCode.InvalidSymbol,
                    ExpressionParserMessages.InvalidSymbolError),
                expr => new ValidatedParseErrorItem(expr.OrdinalIndexOf(invalidSymbol, 2), ParseErrorItemCode.InvalidSymbol,
                    ExpressionParserMessages.InvalidSymbolError),
                expr => new ValidatedParseErrorItem(expr.OrdinalIndexOf(invalidSymbol, 3), ParseErrorItemCode.InvalidSymbol,
                    ExpressionParserMessages.InvalidSymbolError),
                expr => new ValidatedParseErrorItem(expr.OrdinalIndexOf(invalidSymbol, 4), ParseErrorItemCode.InvalidSymbol,
                    ExpressionParserMessages.InvalidSymbolError),
                expr => new ValidatedParseErrorItem(expr.OrdinalIndexOf(invalidSymbol, 5), ParseErrorItemCode.InvalidSymbol,
                    ExpressionParserMessages.InvalidSymbolError),
                expr => new ValidatedParseErrorItem(expr.OrdinalIndexOf(invalidSymbol, 6), ParseErrorItemCode.InvalidSymbol,
                    ExpressionParserMessages.InvalidSymbolError),
                expr => new ValidatedParseErrorItem(expr.OrdinalIndexOf(invalidSymbol, 7), ParseErrorItemCode.InvalidSymbol,
                    ExpressionParserMessages.InvalidSymbolError),
                expr => new ValidatedParseErrorItem(expr.OrdinalIndexOf(invalidSymbol, 8), ParseErrorItemCode.InvalidSymbol,
                    ExpressionParserMessages.InvalidSymbolError),

                expr =>
                    new ValidatedParseErrorItem(expr.OrdinalLastIndexOf(",") + 1, ParseErrorItemCode.ExpressionMissingAfterComma));
        }

        [TestCase("ф")]
        [TestCase("фϨ")]
        [TestCase("фϨϭ")]
        public void InvalidSymbols_BeforeCustomExpressions([NotNull] string invalidSymbol)
        {
            ReGenerateNonLatinCharactersForInvalidSymbolsTests(invalidSymbol);

            var expression = $"{InjectedInvalidCodeTemplate}{SpacesTemplate}kwd1 A+x;{InjectedInvalidCodeTemplate}{SpacesTemplate}F2(x2, y2);";

            void TransformTestLanguageProvider(TestLanguageProviderForExpressionParseErrorTests expressionLanguageProvider, string parsedExpression, bool isTransformedForSuccess)
            {
                var keywordInfo = new KeywordInfoForTests("kwd1");
                expressionLanguageProvider.Keywords.Add(keywordInfo);

                var customExpressionItemParserMock = new CustomExpressionItemParserMock
                {
                    TryCreateCustomExpressionItemForKeyword = (_, keywordId,
                        parsedPrefixExpressionItems,
                        parsedKeywordExpressionItemsWithoutLastKeyword, lastKeywordExpressionItem) =>
                    {
                        if (keywordId == keywordInfo.Id)
                        {
                            return new CustomExpressionItemMock(parsedPrefixExpressionItems,
                                parsedKeywordExpressionItemsWithoutLastKeyword,
                                lastKeywordExpressionItem, CustomExpressionItemCategory.Regular);
                        }

                        return null;
                    }
                };

                expressionLanguageProvider.CustomExpressionItemParsers.Add(customExpressionItemParserMock);
            }

            ValidateParseError(TransformTestLanguageProvider,
                expression,
                expr =>
                    expr.Replace(InjectedInvalidCodeTemplate, invalidSymbol)
                        .Replace("F2(x2, y2)", "F2(x2, )"),

                expr => new ValidatedParseErrorItem(expr.OrdinalIndexOf(invalidSymbol), ParseErrorItemCode.InvalidSymbol,
                    ExpressionParserMessages.InvalidSymbolError),
                expr => new ValidatedParseErrorItem(expr.OrdinalIndexOf(invalidSymbol, 1), ParseErrorItemCode.InvalidSymbol),
                expr =>
                    new ValidatedParseErrorItem(expr.OrdinalLastIndexOf(",") + 1, ParseErrorItemCode.ExpressionMissingAfterComma));
        }

        [TestCase("ф")]
        [TestCase("фϨ")]
        [TestCase("фϨϭ")]
        public void InvalidSymbols_BeforeCommas([NotNull] string invalidSymbol)
        {
            ReGenerateNonLatinCharactersForInvalidSymbolsTests(invalidSymbol);

            var expression =
                $"F1(x1{InjectedInvalidCodeTemplate}{SpacesTemplate}, {InjectedInvalidCodeTemplate}{SpacesTemplate} {InjectedInvalidCodeTemplate}{SpacesTemplate}x2, x3);{SpacesTemplate}F2(x2, y2);";

            ValidateParseError(expression,
                expr =>
                    expr.Replace(InjectedInvalidCodeTemplate, invalidSymbol)
                        .Replace("F2(x2, y2)", "F2(x2, )"),

                expr => new ValidatedParseErrorItem(expr.OrdinalIndexOf(invalidSymbol), ParseErrorItemCode.InvalidSymbol,
                    ExpressionParserMessages.InvalidSymbolError),
                expr => new ValidatedParseErrorItem(expr.OrdinalIndexOf(invalidSymbol, 1), ParseErrorItemCode.InvalidSymbol,
                    ExpressionParserMessages.InvalidSymbolError),
                expr => new ValidatedParseErrorItem(expr.OrdinalIndexOf(invalidSymbol, 2), ParseErrorItemCode.InvalidSymbol,
                    ExpressionParserMessages.InvalidSymbolError),

                expr =>
                    new ValidatedParseErrorItem(expr.OrdinalLastIndexOf(",") + 1, ParseErrorItemCode.ExpressionMissingAfterComma));
        }

        [TestCase("ф")]
        [TestCase("фϨ")]
        [TestCase("фϨϭ")]
        public void InvalidSymbols_BeforeBraces([NotNull] string invalidSymbol)
        {
            ReGenerateNonLatinCharactersForInvalidSymbolsTests(invalidSymbol);

            void ValidateParseErrorLocal(bool isRoundBraces)
            {
                var braces = GetBraces(isRoundBraces);
                var expression =
                    string.Concat(
                        $"F1{InjectedInvalidCodeTemplate}{SpacesTemplate}{braces.openingBrace}x1, x2{InjectedInvalidCodeTemplate}{SpacesTemplate}{braces.closingBrace};",
                        Environment.NewLine,
                        $"F2{InjectedInvalidCodeTemplate}{SpacesTemplate} {InjectedInvalidCodeTemplate}{SpacesTemplate}{braces.openingBrace}x1, x2{InjectedInvalidCodeTemplate}{SpacesTemplate} {InjectedInvalidCodeTemplate}{SpacesTemplate}{braces.closingBrace};",
                        Environment.NewLine,
                        "F3(x2, y2)");

                ValidateParseError(expression,
                    expr =>
                        expr.Replace(InjectedInvalidCodeTemplate, invalidSymbol)
                            .Replace("F3(x2, y2)", "F3(x2, )"),

                    expr => new ValidatedParseErrorItem(expr.OrdinalIndexOf(invalidSymbol), ParseErrorItemCode.InvalidSymbol,
                        ExpressionParserMessages.InvalidSymbolError),
                    expr => new ValidatedParseErrorItem(expr.OrdinalIndexOf(invalidSymbol, 1), ParseErrorItemCode.InvalidSymbol,
                        ExpressionParserMessages.InvalidSymbolError),
                    expr => new ValidatedParseErrorItem(expr.OrdinalIndexOf(invalidSymbol, 2), ParseErrorItemCode.InvalidSymbol,
                        ExpressionParserMessages.InvalidSymbolError),
                    expr => new ValidatedParseErrorItem(expr.OrdinalIndexOf(invalidSymbol, 3), ParseErrorItemCode.InvalidSymbol,
                        ExpressionParserMessages.InvalidSymbolError),
                    expr => new ValidatedParseErrorItem(expr.OrdinalIndexOf(invalidSymbol, 4), ParseErrorItemCode.InvalidSymbol,
                        ExpressionParserMessages.InvalidSymbolError),
                    expr => new ValidatedParseErrorItem(expr.OrdinalIndexOf(invalidSymbol, 5), ParseErrorItemCode.InvalidSymbol,
                        ExpressionParserMessages.InvalidSymbolError),
                    expr =>
                        new ValidatedParseErrorItem(expr.OrdinalLastIndexOf(",") + 1, ParseErrorItemCode.ExpressionMissingAfterComma));
            }

            ValidateParseErrorLocal(true);
            ValidateParseErrorLocal(false);
        }

        [TestCase("ф")]
        [TestCase("фϨ")]
        [TestCase("фϨϭ")]
        public void InvalidSymbols_BeforeCodeBlockSymbolsSymbols([NotNull] string invalidSymbol)
        {
            ReGenerateNonLatinCharactersForInvalidSymbolsTests(invalidSymbol);

            var expression =
                string.Concat(
                    $"{InjectedInvalidCodeTemplate}{SpacesTemplate}{{x=y+1{InjectedInvalidCodeTemplate}{SpacesTemplate};{InjectedInvalidCodeTemplate}{SpacesTemplate}}}",
                    Environment.NewLine,
                    $"{InjectedInvalidCodeTemplate}{SpacesTemplate} {InjectedInvalidCodeTemplate}{SpacesTemplate}{{x=y+1{InjectedInvalidCodeTemplate}{SpacesTemplate} {InjectedInvalidCodeTemplate}{SpacesTemplate};{InjectedInvalidCodeTemplate}{SpacesTemplate} {InjectedInvalidCodeTemplate}{SpacesTemplate}}}",
                    Environment.NewLine,
                    "F(x2, y2)");

            ValidateParseError(expression,
                expr =>
                    expr.Replace(InjectedInvalidCodeTemplate, invalidSymbol)
                        .Replace("F(x2, y2)", "F(x2, )"),

                expr => new ValidatedParseErrorItem(expr.OrdinalIndexOf(invalidSymbol), ParseErrorItemCode.InvalidSymbol,
                    ExpressionParserMessages.InvalidSymbolError),
                expr => new ValidatedParseErrorItem(expr.OrdinalIndexOf(invalidSymbol, 1), ParseErrorItemCode.InvalidSymbol,
                    ExpressionParserMessages.InvalidSymbolError),
                expr => new ValidatedParseErrorItem(expr.OrdinalIndexOf(invalidSymbol, 2), ParseErrorItemCode.InvalidSymbol,
                    ExpressionParserMessages.InvalidSymbolError),
                expr => new ValidatedParseErrorItem(expr.OrdinalIndexOf(invalidSymbol, 3), ParseErrorItemCode.InvalidSymbol,
                    ExpressionParserMessages.InvalidSymbolError),
                expr => new ValidatedParseErrorItem(expr.OrdinalIndexOf(invalidSymbol, 4), ParseErrorItemCode.InvalidSymbol,
                    ExpressionParserMessages.InvalidSymbolError),
                expr => new ValidatedParseErrorItem(expr.OrdinalIndexOf(invalidSymbol, 5), ParseErrorItemCode.InvalidSymbol,
                    ExpressionParserMessages.InvalidSymbolError),

                expr => new ValidatedParseErrorItem(expr.OrdinalIndexOf(invalidSymbol, 6), ParseErrorItemCode.InvalidSymbol,
                    ExpressionParserMessages.InvalidSymbolError),
                expr => new ValidatedParseErrorItem(expr.OrdinalIndexOf(invalidSymbol, 7), ParseErrorItemCode.InvalidSymbol,
                    ExpressionParserMessages.InvalidSymbolError),
                expr => new ValidatedParseErrorItem(expr.OrdinalIndexOf(invalidSymbol, 8), ParseErrorItemCode.InvalidSymbol,
                    ExpressionParserMessages.InvalidSymbolError),

                expr =>
                    new ValidatedParseErrorItem(expr.OrdinalLastIndexOf(",") + 1, ParseErrorItemCode.ExpressionMissingAfterComma));
        }
        #endregion

        #region Invalid use of keywords tests

        [Test]
        public void InvalidUseOfKeywords_MultipleOccurrencesOfKeyword()
        {
            var expression = $"f(); {SpacesTemplate}{TestLanguageProvider.NoCustomExprKeyword1}{SpacesTemplate} y; F(x, y)";
            var invalidExpression = expression.Replace("y;", $"{SpacesTemplate}{TestLanguageProvider.NoCustomExprKeyword1}{SpacesTemplate} {TestLanguageProvider.NoCustomExprKeyword1}{SpacesTemplate} y;")
                .Replace("F(x, y)", "F(x, )");

            ValidateParseError(expression,
                _ => invalidExpression,
                expr =>
                    new ValidatedParseErrorItem(expr.OrdinalIndexOf(TestLanguageProvider.NoCustomExprKeyword1, 1),
                        ParseErrorItemCode.MultipleOccurrencesOfKeyword, "Multiple occurrences of keyword"),
                expr =>
                    new ValidatedParseErrorItem(expr.OrdinalIndexOf(TestLanguageProvider.NoCustomExprKeyword1, 2),
                        ParseErrorItemCode.MultipleOccurrencesOfKeyword, "Multiple occurrences of keyword"),
                expr =>
                    new ValidatedParseErrorItem(expr.OrdinalIndexOf(",") + 1, ParseErrorItemCode.ExpressionMissingAfterComma));
        }

        [Test]
        public void InvalidUseOfKeywords_KeywordsBeforePrefixOperators()
        {
            var expression = $"f(); {SpacesTemplate}++{SpacesTemplate}x+y; F(x, y)";
            var invalidExpression = $"f(); {SpacesTemplate}{TestLanguageProvider.NoCustomExprKeyword1}++{SpacesTemplate}x+y; F(x, )";

            ValidateParseError(expression,
                _ => invalidExpression,
                expr =>
                    new ValidatedParseErrorItem(expr.OrdinalLastIndexOf(TestLanguageProvider.NoCustomExprKeyword1),
                        ParseErrorItemCode.InvalidUseOfKeywordsBeforeOperators,
                        ExpressionParserMessages.InvalidUseOfKeywordsBeforeOperatorsError),
                expr =>
                    new ValidatedParseErrorItem(expr.OrdinalIndexOf(",") + 1, ParseErrorItemCode.ExpressionMissingAfterComma));
        }

        [Test]
        public void InvalidUseOfKeywords_KeywordsBeforeBinaryOperators()
        {
            var expression = $"f(); x{SpacesTemplate}+{SpacesTemplate}y+z; F(x, y)";
            var invalidExpression = $"f(); x {TestLanguageProvider.NoCustomExprKeyword1}{SpacesTemplate}+y+z; F(x, )";

            ValidateParseError(expression,
                _ => invalidExpression,
                expr =>
                    new ValidatedParseErrorItem(expr.OrdinalLastIndexOf(TestLanguageProvider.NoCustomExprKeyword1),
                        ParseErrorItemCode.InvalidUseOfKeywordsBeforeOperators,
                        ExpressionParserMessages.InvalidUseOfKeywordsBeforeOperatorsError),
                expr =>
                    new ValidatedParseErrorItem(expr.OrdinalIndexOf(",") + 1, ParseErrorItemCode.ExpressionMissingAfterComma));
        }

        [Test]
        public void Operators_KeywordsBeforePostfixOperators()
        {
            var expression = $"f(); x{SpacesTemplate}++; F(x, y)";
            var invalidExpression = $"f(); x {SpacesTemplate}{TestLanguageProvider.NoCustomExprKeyword1}++; F(x, )";

            ValidateParseError(expression,
                _ => invalidExpression,
                expr =>
                    new ValidatedParseErrorItem(expr.OrdinalLastIndexOf(TestLanguageProvider.NoCustomExprKeyword1),
                        ParseErrorItemCode.InvalidUseOfKeywordsBeforeOperators,
                        ExpressionParserMessages.InvalidUseOfKeywordsBeforeOperatorsError),
                expr =>
                    new ValidatedParseErrorItem(expr.OrdinalIndexOf(",") + 1, ParseErrorItemCode.ExpressionMissingAfterComma));
        }

        [Test]
        public void InvalidUseOfKeywords_KeywordsNotSupported()
        {
            void TransformTestLanguageProvider(TestLanguageProviderForExpressionParseErrorTests expressionLanguageProvider, string parsedExpression, bool isTransformedForSuccess)
            {
                expressionLanguageProvider.SupportsKeywords = isTransformedForSuccess;
            }

            void ValidateParseErrorLocal(string validExpression)
            {
                var invalidExpression = validExpression.Replace("F(x, y)", "F(x, )");


                ValidateParseError(TransformTestLanguageProvider, validExpression,
                    _ => invalidExpression,
                    expr =>
                        new ValidatedParseErrorItem(expr.OrdinalLastIndexOf(TestLanguageProvider.NoCustomExprKeyword1),
                            ParseErrorItemCode.KeywordsNotSupported,
                            ExpressionParserMessages.KeywordsNotSupportedError),
                    expr =>
                        new ValidatedParseErrorItem(expr.OrdinalLastIndexOf(",") + 1, ParseErrorItemCode.ExpressionMissingAfterComma));
            }

            ValidateParseErrorLocal($"f(); {SpacesTemplate}{TestLanguageProvider.NoCustomExprKeyword1} {SpacesTemplate}F1(); F(x, y)");
            ValidateParseErrorLocal($"f(); {SpacesTemplate}{TestLanguageProvider.NoCustomExprKeyword1} {SpacesTemplate}F1[]; F(x, y)");
            ValidateParseErrorLocal($"f(); {SpacesTemplate}{TestLanguageProvider.NoCustomExprKeyword1} {SpacesTemplate}(); F(x, y)");
            ValidateParseErrorLocal($"f(); {SpacesTemplate}{TestLanguageProvider.NoCustomExprKeyword1} {SpacesTemplate}[]; F(x, y)");
            ValidateParseErrorLocal($"f(); {SpacesTemplate}{TestLanguageProvider.NoCustomExprKeyword1} {SpacesTemplate}x; F(x, y)");
            ValidateParseErrorLocal($"f(); {SpacesTemplate}{TestLanguageProvider.NoCustomExprKeyword1} {SpacesTemplate}15; F(x, y)");
            ValidateParseErrorLocal($"f(); {SpacesTemplate}{TestLanguageProvider.NoCustomExprKeyword1} {SpacesTemplate}'some text'; F(x, y)");
        }

        [Test]
        public void InvalidUseOfKeywords_InvalidUseOfKeywordsBeforeUnparsedSymbol()
        {
            var expression = $"f(); {SpacesTemplate}{TestLanguageProvider.NoCustomExprKeyword1} x; F(x, y)";
            var invalidExpression = expression.Replace("x;", "щК;").Replace("F(x, y)", "F(x, )");

            ValidateParseError(expression,
                _ => invalidExpression,
                expr =>
                    new ValidatedParseErrorItem(expr.OrdinalLastIndexOf(TestLanguageProvider.NoCustomExprKeyword1),
                        ParseErrorItemCode.InvalidUseOfKeywords,
                        ExpressionParserMessages.InvalidUseOfKeywordsError),
                expr => new ValidatedParseErrorItem(expr.OrdinalLastIndexOf("щК;"),
                    ParseErrorItemCode.InvalidSymbol, ExpressionParserMessages.InvalidSymbolError),
                expr => new ValidatedParseErrorItem(expr.OrdinalLastIndexOf(",") + 1,
                    ParseErrorItemCode.ExpressionMissingAfterComma, ExpressionParserMessages.ExpressionMissingAfterCommaError));
        }

        public enum InvalidUseOfKeywordType
        {
            KeywordBeforeComma,
            KeywordBeforeClosingRoundBraces,
            KeywordBeforeClosingSquareBraces,
            KeywordBeforeCodeSeparator,
            KeywordBeforeCodeBlockEndMarker,
            KeywordsBeforeUnparsedSymbol
        }


        [TestCase(InvalidUseOfKeywordType.KeywordBeforeComma)]
        [TestCase(InvalidUseOfKeywordType.KeywordBeforeClosingRoundBraces)]
        [TestCase(InvalidUseOfKeywordType.KeywordBeforeClosingSquareBraces)]
        [TestCase(InvalidUseOfKeywordType.KeywordBeforeCodeSeparator)]
        [TestCase(InvalidUseOfKeywordType.KeywordBeforeCodeBlockEndMarker)]
        [TestCase(InvalidUseOfKeywordType.KeywordsBeforeUnparsedSymbol)]

        public void InvalidUseOfKeywords(InvalidUseOfKeywordType invalidUseOfKeywordType)
        {
            void TransformTestLanguageProvider(TestLanguageProviderForExpressionParseErrorTests expressionLanguageProvider, string parsedExpression, bool isTransformedForSuccess)
            {
                expressionLanguageProvider.Keywords.Add(new KeywordInfoForTests("::kwd1"));
                expressionLanguageProvider.Keywords.Add(new KeywordInfoForTests("kwd2"));
            }

            void InvalidUseOfKeywords(string expression, Func<string, string> transformValidExpressionToInvalidExpression,
                int errorCode, string errorMessage,
                params GetValidatedParseErrorItemDelegate[] additionalExpectedErrors)
            {
                var invalidExpression = transformValidExpressionToInvalidExpression(expression);

                var errors = new List<GetValidatedParseErrorItemDelegate>
                {
                    expr => new ValidatedParseErrorItem(expr.OrdinalLastIndexOf("kwd2"),
                        errorCode, () => errorMessage)
                };

                errors.Add(expr => new ValidatedParseErrorItem(expr.OrdinalLastIndexOf("x++"),
                    ParseErrorItemCode.NoSeparationBetweenSymbols,
                    ExpressionParserMessages.NoSeparationBetweenSymbolsError));

                if (additionalExpectedErrors != null && additionalExpectedErrors.Length > 0)
                    errors.AddRange(additionalExpectedErrors);

                ValidateParseError(TransformTestLanguageProvider, expression,
                    _ => invalidExpression,
                    ParseExpression,
                    errors.ToArray());
            }

            switch (invalidUseOfKeywordType)
            {
                case InvalidUseOfKeywordType.KeywordBeforeComma:
                    InvalidUseOfKeywords($"F(::kwd1 {SpacesTemplate}kwd2 x, y)", _ => $"F(::kwd1 {SpacesTemplate}kwd2 , y x++)",
                        ParseErrorItemCode.InvalidUseOfKeywordsBeforeComma, ExpressionParserMessages.InvalidUseOfKeywordsBeforeCommaError);
                    break;

                case InvalidUseOfKeywordType.KeywordBeforeClosingRoundBraces:
                    InvalidUseOfKeywords($"F(::kwd1 {SpacesTemplate}kwd2 x)", _ => $"F(::kwd1 {SpacesTemplate}kwd2) x++",
                        ParseErrorItemCode.InvalidUseOfKeywordsBeforeClosingBraces, ExpressionParserMessages.InvalidUseOfKeywordsBeforeClosingBracesError);
                    break;

                case InvalidUseOfKeywordType.KeywordBeforeClosingSquareBraces:
                    InvalidUseOfKeywords($"F[::kwd1 {SpacesTemplate}kwd2 x]", _ => $"F[::kwd1 {SpacesTemplate}kwd2] x++",
                        ParseErrorItemCode.InvalidUseOfKeywordsBeforeClosingBraces, ExpressionParserMessages.InvalidUseOfKeywordsBeforeClosingBracesError);
                    break;

                case InvalidUseOfKeywordType.KeywordBeforeCodeSeparator:
                    InvalidUseOfKeywords($"::kwd1 {SpacesTemplate}kwd2 y; z+x++", _ => $"::kwd1 {SpacesTemplate}kwd2; z x++",
                        ParseErrorItemCode.InvalidUseOfKeywordsBeforeCodeSeparator, ExpressionParserMessages.InvalidUseOfKeywordsBeforeCodeSeparatorError);
                    break;

                case InvalidUseOfKeywordType.KeywordBeforeCodeBlockEndMarker:
                    InvalidUseOfKeywords($"{{::kwd1 {SpacesTemplate}kwd2 y}} z+x++", _ => $"{{::kwd1 {SpacesTemplate}kwd2}} z x++",
                        ParseErrorItemCode.InvalidUseOfKeywordsBeforeCodeBlockEndMarker, ExpressionParserMessages.InvalidUseOfKeywordsBeforeCodeBlockEndMarkerError);
                    break;
            }
        }


        #endregion

        #region Prefixes not supported tests

        [Test]
        public void Prefixes_CustomPrefixesAndKeywordsAlwaysSupported()
        {
            var validExpression = "customPref x PrefixTarget;";
            void TransformTestLanguageProvider(TestLanguageProviderForExpressionParseErrorTests expressionLanguageProvider, string parsedExpression, bool isTransformedForSuccess)
            {
                expressionLanguageProvider.SupportsPrefixes = isTransformedForSuccess;
                expressionLanguageProvider.SupportsKeywords = isTransformedForSuccess;

                var customPrefInfo = new KeywordInfoForTests("customPref");
                expressionLanguageProvider.Keywords.Add(customPrefInfo);

                var customExpressionItemParserMock = new CustomExpressionItemParserMock
                {
                    TryCreateCustomExpressionItemForKeyword = (_, keywordId,
                        parsedPrefixExpressionItems,
                        parsedKeywordExpressionItemsWithoutLastKeyword, lastKeywordExpressionItem) =>
                    {
                        CustomExpressionItemMock customExpressionItemMock = null;

                        if (keywordId == customPrefInfo.Id)
                        {
                            customExpressionItemMock = new CustomExpressionItemMock(parsedPrefixExpressionItems,
                                parsedKeywordExpressionItemsWithoutLastKeyword, lastKeywordExpressionItem, CustomExpressionItemCategory.Prefix);
                        }
                        else
                        {
                            return null;
                        }

                        return customExpressionItemMock;
                    }
                };

                expressionLanguageProvider.CustomExpressionItemParsers.Add(customExpressionItemParserMock);
            }

            ValidateParseError(TransformTestLanguageProvider, validExpression, _ => null);
        }
        [Test]
        public void Prefixes_PrefixesNotSupported()
        {
            string prefixesText = $"{SpacesTemplate}[NotNull, ItemNotNull]{SpacesTemplate}(x){SpacesTemplate}";

            void TransformTestLanguageProvider(TestLanguageProviderForExpressionParseErrorTests expressionLanguageProvider, string parsedExpression, bool isTransformedForSuccess)
            {
                expressionLanguageProvider.SupportsPrefixes = isTransformedForSuccess;

                var pragmaKeywordInfo = new KeywordInfoForTests("pragma");
                expressionLanguageProvider.Keywords.Add(pragmaKeywordInfo);

                var customExpressionItemParserMock = new CustomExpressionItemParserMock
                {
                    TryCreateCustomExpressionItemForKeyword = (_, keywordId,
                        parsedPrefixExpressionItems,
                        parsedKeywordExpressionItemsWithoutLastKeyword, lastKeywordExpressionItem) =>
                    {
                        CustomExpressionItemMock customExpressionItemMock = null;

                        if (keywordId == pragmaKeywordInfo.Id)
                        {
                            customExpressionItemMock = new CustomExpressionItemMock(parsedPrefixExpressionItems,
                                parsedKeywordExpressionItemsWithoutLastKeyword, lastKeywordExpressionItem, CustomExpressionItemCategory.Regular);
                        }
                        else
                        {
                            return null;
                        }

                        return customExpressionItemMock;
                    }
                };

                expressionLanguageProvider.CustomExpressionItemParsers.Add(customExpressionItemParserMock);
            }

            void ValidateParseErrorLocal(string validExpression, params GetValidatedParseErrorItemDelegate[] additionalErrors)
            {
                var invalidExpression = validExpression.Replace("F(x, y)", "F(x, )");

                var errors = new List<GetValidatedParseErrorItemDelegate>
                {
                    expr => new ValidatedParseErrorItem(expr.OrdinalIndexOf("(x)"),
                        ParseErrorItemCode.NoSeparationBetweenSymbols,
                        ExpressionParserMessages.NoSeparationBetweenSymbolsError)
                };

                foreach (var additionalError in additionalErrors)
                    errors.Add(additionalError);

                errors.Add(expr =>
                    new ValidatedParseErrorItem(expr.OrdinalLastIndexOf(",") + 1, ParseErrorItemCode.ExpressionMissingAfterComma));

                ValidateParseError(TransformTestLanguageProvider, validExpression, _ => invalidExpression, errors.ToArray());
            }

            ValidateParseErrorLocal($"f(); {prefixesText} pragma A; F(x, y)",
                expr => new ValidatedParseErrorItem(expr.OrdinalIndexOf("pragma"),
                    ParseErrorItemCode.NoSeparationBetweenSymbols,
                    ExpressionParserMessages.NoSeparationBetweenSymbolsError));

            ValidateParseErrorLocal($"f(); {prefixesText} F1(); F(x, y)",
                expr => new ValidatedParseErrorItem(expr.OrdinalIndexOf("F1()"),
                    ParseErrorItemCode.NoSeparationBetweenSymbols,
                    ExpressionParserMessages.NoSeparationBetweenSymbolsError));

            ValidateParseErrorLocal($"f(); {prefixesText} F1[]; F(x, y)",
                expr => new ValidatedParseErrorItem(expr.OrdinalIndexOf("F1[]"),
                    ParseErrorItemCode.NoSeparationBetweenSymbols,
                    ExpressionParserMessages.NoSeparationBetweenSymbolsError));

            ValidateParseErrorLocal($"f(); {prefixesText} (x, y); F(x, y)",
                expr => new ValidatedParseErrorItem(expr.OrdinalIndexOf("(x, y)"),
                    ParseErrorItemCode.NoSeparationBetweenSymbols,
                    ExpressionParserMessages.NoSeparationBetweenSymbolsError));

            ValidateParseErrorLocal($"f(); {prefixesText} [x, y]; F(x, y)",
                expr => new ValidatedParseErrorItem(expr.OrdinalIndexOf("[x, y]"),
                    ParseErrorItemCode.NoSeparationBetweenSymbols,
                    ExpressionParserMessages.NoSeparationBetweenSymbolsError));

            ValidateParseErrorLocal($"f(); {prefixesText} MyClass1; F(x, y)",
                expr => new ValidatedParseErrorItem(expr.OrdinalIndexOf("MyClass1"),
                    ParseErrorItemCode.NoSeparationBetweenSymbols,
                    ExpressionParserMessages.NoSeparationBetweenSymbolsError));

            ValidateParseErrorLocal($"f(); {prefixesText} 158.3; F(x, y)",
                expr => new ValidatedParseErrorItem(expr.OrdinalIndexOf("158.3"),
                    ParseErrorItemCode.NoSeparationBetweenSymbols,
                    ExpressionParserMessages.NoSeparationBetweenSymbolsError));

            ValidateParseErrorLocal($"f(); {prefixesText} 'some text'; F(x, y)",
                expr => new ValidatedParseErrorItem(expr.OrdinalIndexOf("'some text'"),
                    ParseErrorItemCode.NoSeparationBetweenSymbols,
                    ExpressionParserMessages.NoSeparationBetweenSymbolsError));
        }
        #endregion

        #region Other tests
        [Test]
        public void InvalidCommaWithoutParentBraces()
        {
            var expression = $"{{x += y; z=x+1{SpacesTemplate}; F(z, y)}}";

            ValidateParseError(expression, validExpression =>
                    validExpression.Replace("; F(z", ", F(z").Replace("F(z, y)", "F(z, )"),
                expr =>
                    new ValidatedParseErrorItem(expr.OrdinalIndexOf(","), ParseErrorItemCode.CommaWithoutParentBraces,
                        () => "Commas can only be used to separate items within braces"),
                expr =>
                    new ValidatedParseErrorItem(expr.OrdinalLastIndexOf(",") + 1, ParseErrorItemCode.ExpressionMissingAfterComma));
        }

        public (string openingBrace, string closingBrace) GetBraces(bool getRoundBracesTest)
        {
            if (getRoundBracesTest)
                return ("(", ")");

            return ("[", "]");
        }

        [TestCase(true, true)]
        [TestCase(true, false)]
        [TestCase(false, true)]
        [TestCase(false, false)]
        public void ExpressionMissingBeforeOrAfterCommaWithinParentBraces(bool isRoundBracesTest, bool isNamelessBracesTest)
        {
            string functionName = isNamelessBracesTest ? string.Empty : "F1";

            var braces = GetBraces(isRoundBracesTest);

            var expression = $"{{x += y; {functionName}{braces.openingBrace}x1{SpacesTemplate},x2{SpacesTemplate},x3{SpacesTemplate}{braces.closingBrace}; F(z, y)}} F2(z1,y);";

            ValidateParseError(expression, validExpression =>
                validExpression.Replace("x1", "")
                        .Replace("x2", "")
                        .Replace("x3", "")
                        .Replace("F(z, y)", "F(z, )")
                        .Replace("F2(z1,y);", "F2(z1,"),
                expr => new ValidatedParseErrorItem(expr.OrdinalIndexOf(","),
                    ParseErrorItemCode.ExpressionMissingBeforeComma,
                    () => ExpressionParserMessages.ExpressionMissingBeforeCommaError),
                expr => new ValidatedParseErrorItem(expr.OrdinalIndexOf(",", 1),
                    ParseErrorItemCode.ExpressionMissingBeforeComma,
                    () => ExpressionParserMessages.ExpressionMissingBeforeCommaError),
                expr => new ValidatedParseErrorItem(expr.OrdinalIndexOf(",", 1) + 1,
                    ParseErrorItemCode.ExpressionMissingAfterComma,
                    () => ExpressionParserMessages.ExpressionMissingAfterCommaError),

                expr =>
                    new ValidatedParseErrorItem(expr.OrdinalIndexOf(",", 2) + 1, ParseErrorItemCode.ExpressionMissingAfterComma),

                expr => new ValidatedParseErrorItem(expr.OrdinalIndexOf("(z1"),
                    ParseErrorItemCode.ClosingBraceMissing),

                expr => new ValidatedParseErrorItem(expr.OrdinalIndexOf(",", 3) + 1,
                    ParseErrorItemCode.ExpressionMissingAfterComma,
                    () => ExpressionParserMessages.ExpressionMissingAfterCommaError));
        }

        [TestCase(true, true)]
        [TestCase(true, false)]
        [TestCase(false, true)]
        [TestCase(false, false)]
        public void InvalidSymbolsInFunctionParameterNames(bool isRoundBracesTest, bool isNamelessBracesTest)
        {
            string functionName = isNamelessBracesTest ? string.Empty : "F1";

            var braces = GetBraces(isRoundBracesTest);


            var expression = $"{functionName}{braces.openingBrace}Dog_Color{SpacesTemplate},{SpacesTemplate}Cat_Color{SpacesTemplate},Cat{SpacesTemplate}{braces.closingBrace}";

            ValidateParseError(expression, validExpression =>
                    validExpression.Replace("Dog_Color", "Dog.Color")
                        .Replace("Cat_Color", "Cat.Color"),
                expr => new ValidatedParseErrorItem(expr.OrdinalIndexOf(".Color"),
                    ParseErrorItemCode.InvalidSymbol,
                    () => ExpressionParserMessages.InvalidSymbolError),
                expr => new ValidatedParseErrorItem(expr.OrdinalIndexOf(".Color", 1),
                    ParseErrorItemCode.InvalidSymbol,
                    () => ExpressionParserMessages.InvalidSymbolError));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void ClosingBraceWithoutOpeningBrace(bool isRoundBracesTest)
        {
            var errorMessage = "No corresponding opening brace for this closing brace";

            var braces = GetBraces(isRoundBracesTest);

            ValidateParseError($"F{braces.openingBrace}x, y{braces.closingBrace}+F2{braces.openingBrace}z{braces.closingBrace}{InjectedInvalidCodeTemplate};{InjectedInvalidCodeTemplate}x1+{InjectedInvalidCodeTemplate}y1; F(x1, y1)",
                expr =>
                    expr.Replace($"{InjectedInvalidCodeTemplate}", $"{SpacesTemplate}{braces.closingBrace}{SpacesTemplate}{braces.closingBrace}")
                        .Replace("F(x1, y1)", "F(x1, )"),
                expr =>
                    new ValidatedParseErrorItem(expr.OrdinalIndexOf(braces.closingBrace, 2), ParseErrorItemCode.ClosingBraceWithoutOpeningBrace,
                        () => errorMessage),

                expr =>
                    new ValidatedParseErrorItem(expr.OrdinalIndexOf(braces.closingBrace, 3), ParseErrorItemCode.ClosingBraceWithoutOpeningBrace,
                        () => errorMessage),
                expr =>
                    new ValidatedParseErrorItem(expr.OrdinalIndexOf(braces.closingBrace, 4), ParseErrorItemCode.ClosingBraceWithoutOpeningBrace,
                        () => errorMessage),
                expr =>
                    new ValidatedParseErrorItem(expr.OrdinalIndexOf(braces.closingBrace, 5), ParseErrorItemCode.ClosingBraceWithoutOpeningBrace,
                        () => errorMessage),

                expr =>
                    new ValidatedParseErrorItem(expr.OrdinalIndexOf(braces.closingBrace, 6), ParseErrorItemCode.ClosingBraceWithoutOpeningBrace,
                        () => errorMessage),
                expr =>
                    new ValidatedParseErrorItem(expr.OrdinalIndexOf(braces.closingBrace, 7), ParseErrorItemCode.ClosingBraceWithoutOpeningBrace,
                        () => errorMessage),

                expr =>
                    new ValidatedParseErrorItem(expr.OrdinalLastIndexOf(",") + 1, ParseErrorItemCode.ExpressionMissingAfterComma,
                        () => ExpressionParserMessages.ExpressionMissingAfterCommaError));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void ClosingBraceDoesNotMatchOpeningBrace(bool isRoundBracesTest)
        {
            var braces1 = GetBraces(isRoundBracesTest);
            var braces2 = GetBraces(!isRoundBracesTest);

            ValidateParseError($"F{braces1.openingBrace}x, y{InjectedInvalidCodeTemplate}, z{braces1.closingBrace}; x1+y1",
                expr =>
                        expr.Replace(InjectedInvalidCodeTemplate, braces2.closingBrace)
                        .Replace("x1+y1", "x1 y1"),

                expr =>
                    new ValidatedParseErrorItem(expr.OrdinalLastIndexOf(braces2.closingBrace), ParseErrorItemCode.ClosingBraceDoesNotMatchOpeningBrace,
                        () => "Invalid closing brace. Expected "),

                expr =>
                    new ValidatedParseErrorItem(expr.OrdinalLastIndexOf("y1"), ParseErrorItemCode.NoSeparationBetweenSymbols));

            ValidateParseError($"F1{braces1.openingBrace}F2{braces1.openingBrace}x, y{SpacesTemplate}{braces1.closingBrace}, z{braces1.closingBrace}; x1+y1",
                expr =>
                    expr.Replace($"{braces1.closingBrace},", $"{braces2.closingBrace},")
                        .Replace("x1+y1", "x1 y1"),

                expr =>
                    new ValidatedParseErrorItem(expr.OrdinalIndexOf(braces1.openingBrace), ParseErrorItemCode.ClosingBraceMissing,
                        () => "Closing brace"),

                expr =>
                    new ValidatedParseErrorItem(expr.OrdinalLastIndexOf(braces2.closingBrace), ParseErrorItemCode.ClosingBraceDoesNotMatchOpeningBrace,
                        () => "Invalid closing brace. Expected"),

                expr =>
                    new ValidatedParseErrorItem(expr.OrdinalLastIndexOf(";"), ParseErrorItemCode.ExpressionSeparatorWithoutParentCodeBlock,
                        () => "Invalid usage of code separator character"),

                expr =>
                    new ValidatedParseErrorItem(expr.OrdinalLastIndexOf("y1"), ParseErrorItemCode.NoSeparationBetweenSymbols));
        }

        [Test]
        public void ExpressionSeparatorWithoutParentCodeBlock()
        {
            ValidateParseError($"F(x,{SpacesTemplate}y);x1+y1;",
                expr =>
                    expr.Replace("x,", "x;")
                        .Replace("x1+y1", "x1 y1"),
                expr =>
                    new ValidatedParseErrorItem(expr.OrdinalIndexOf(";"), ParseErrorItemCode.ExpressionSeparatorWithoutParentCodeBlock,
                        () => "Invalid usage of code separator character"),
                expr =>
                    new ValidatedParseErrorItem(expr.OrdinalLastIndexOf("y1"), ParseErrorItemCode.NoSeparationBetweenSymbols));
        }

        [Test]
        public void CodeBlockClosingMarkerWithoutOpeningMarker()
        {
            var errorMessage = "No corresponding code block start marker";

            ValidateParseError($"{{F1(x, y);z=x + y;}}{SpacesTemplate}x1+y1;",
                expr =>
                    expr.Replace("}", $"}}{SpacesTemplate}}}{SpacesTemplate}}}")
                        .Replace("x1+y1", "x1 y1")
                        .Replace("y1;", $"y1;F2(){SpacesTemplate}}}; x2 y2"),
                expr =>
                    new ValidatedParseErrorItem(expr.OrdinalIndexOf("}", 1), ParseErrorItemCode.CodeBlockClosingMarkerWithoutOpeningMarker,
                        () => errorMessage),
                expr =>
                    new ValidatedParseErrorItem(expr.OrdinalIndexOf("}", 2), ParseErrorItemCode.CodeBlockClosingMarkerWithoutOpeningMarker,
                        () => errorMessage),
                expr =>
                    new ValidatedParseErrorItem(expr.OrdinalLastIndexOf("y1"), ParseErrorItemCode.NoSeparationBetweenSymbols),
                expr =>
                    new ValidatedParseErrorItem(expr.OrdinalIndexOf("}", 3), ParseErrorItemCode.CodeBlockClosingMarkerWithoutOpeningMarker,
                        () => errorMessage),
                expr =>
                    new ValidatedParseErrorItem(expr.OrdinalLastIndexOf("y2"), ParseErrorItemCode.NoSeparationBetweenSymbols));
        }

        [Test]
        public void CodeBlockUsedAfterPostfixCustomExpression()
        {
            void TransformTestLanguageProvider(TestLanguageProviderForExpressionParseErrorTests expressionLanguageProvider, string parsedExpression, bool isTransformedForSuccess)
            {
                var keyword1Info = new KeywordInfoForTests("keyword1");
                expressionLanguageProvider.Keywords.Add(keyword1Info);

                var customExpressionItemParserMock = new CustomExpressionItemParserMock
                {
                    TryCreateCustomExpressionItemForKeyword = (_, keywordId,
                        parsedPrefixExpressionItems,
                        parsedKeywordExpressionItemsWithoutLastKeyword, lastKeywordExpressionItem) =>
                    {
                        if (keywordId == keyword1Info.Id)
                        {
                            return new CustomExpressionItemMock(parsedPrefixExpressionItems,
                                parsedKeywordExpressionItemsWithoutLastKeyword,
                                lastKeywordExpressionItem,
                                isTransformedForSuccess ? CustomExpressionItemCategory.Regular : CustomExpressionItemCategory.Postfix);
                        }

                        return null;
                    }
                };

                expressionLanguageProvider.CustomExpressionItemParsers.Add(customExpressionItemParserMock);
            }

            ValidateParseError(
                TransformTestLanguageProvider,
                $"keyword1 A{SpacesTemplate}{{x+y;}} x2+y2;",
                expr =>
                    expr.Replace("+y2", " y2"),
                expr =>
                    new ValidatedParseErrorItem(expr.OrdinalIndexOf("keyword1"), ParseErrorItemCode.CustomPostfixExpressionItemHasNoTargetExpression,
                        () => ExpressionParserMessages.CustomPostfixIsInvalidError),
                expr =>
                    new ValidatedParseErrorItem(expr.OrdinalIndexOf("{"), ParseErrorItemCode.CodeBlockUsedAfterPostfixCustomExpression,
                        () => ExpressionParserMessages.CodeBlockCannotFollowThePrecedingExpressionError),
                expr =>
                    new ValidatedParseErrorItem(expr.OrdinalLastIndexOf("y2"), ParseErrorItemCode.NoSeparationBetweenSymbols,
                        ExpressionParserMessages.NoSeparationBetweenSymbolsError));
        }

        [TestCase("'Some text'")]
        [TestCase("158")]
        public void CodeBlockCannotFollowThePrecedingExpression([NotNull] string symbolPrecedingCodeBlock)
        {
            ValidateParseError(
                $"A{SpacesTemplate}{{x+y;}} x2+y2;",
                expr =>
                    expr.Replace("A", symbolPrecedingCodeBlock)
                        .Replace("+y2", " y2"),

                expr =>
                    new ValidatedParseErrorItem(expr.OrdinalIndexOf("{"), ParseErrorItemCode.InvalidCodeBlock,
                        () => ExpressionParserMessages.CodeBlockCannotFollowThePrecedingExpressionError),
                expr =>
                    new ValidatedParseErrorItem(expr.OrdinalLastIndexOf("y2"), ParseErrorItemCode.NoSeparationBetweenSymbols,
                        ExpressionParserMessages.NoSeparationBetweenSymbolsError));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void ExpressionMissingBeforeCodeItemSeparator(bool useParentCodeBlock)
        {
            var errorMessage = "Valid expression is missing before code separator";

            ValidateParseError($"{(useParentCodeBlock ? "{" : "")}x+y;{SpacesTemplate}x1+y1;{(useParentCodeBlock ? "}" : "")}",
                expr =>
                    expr.Replace("x1", ";;x1")
                        .Replace("x1+y1", "x1 y1"),
                expr =>
                    new ValidatedParseErrorItem(expr.OrdinalIndexOf(";", 1), ParseErrorItemCode.ExpressionMissingBeforeCodeItemSeparator,
                        () => errorMessage),
                expr => new ValidatedParseErrorItem(expr.OrdinalIndexOf(";", 2), ParseErrorItemCode.ExpressionMissingBeforeCodeItemSeparator,
                    () => errorMessage),
                expr =>
                    new ValidatedParseErrorItem(expr.OrdinalLastIndexOf("y1"), ParseErrorItemCode.NoSeparationBetweenSymbols));
        }

        [Test]
        public void InvalidUseOfKeywordsBetweenFunctionNameAndBraces()
        {
            /*// TODO: Either remove or make this a test for if (lastParsedComplexExpressionItem != null) {if (potentialPrefixExpressionItem != null) {} else if (keywordExpressionItems.Count > 0)}
            var errorMessage = "Valid expression is missing before code separator";

            ValidateParseError($"[NotNull] {SpacesTemplate}(x, y);{SpacesTemplate}x1+y1;",
                expr =>
                    expr.Replace("[NotNull]", $"[NotNull] {SpacesTemplate}{TestLanguageProvider.NoCustomExprKeyword1}")
                        .Replace("x1+y1", "x1 y1"),
                expr =>
                    new ValidatedParseErrorItem(expr.OrdinalIndexOf(";", 1), CodeParseErrorCode.ExpressionMissingBeforeCodeItemSeparator,
                        () => errorMessage),
                expr => new ValidatedParseErrorItem(expr.OrdinalIndexOf(";", 2), CodeParseErrorCode.ExpressionMissingBeforeCodeItemSeparator,
                    () => errorMessage),
                expr =>
                    new ValidatedParseErrorItema(expr.OrdinalLastIndexOf("y1"), CodeParseErrorCode.NoSeparationBetweenSymbols));*/
        }

        [TestCase(true)]
        [TestCase(false)]
        public void InvalidSymbolBeforeBraces(bool isRoundBrace)
        {
            var braces = GetBraces(isRoundBrace);

            void ValidateParseErrorLocal(string invalidSymbolBeforeBraces)
            {
                ValidateParseError($"{braces.openingBrace}NotNull{braces.closingBrace} F1{SpacesTemplate}{braces.openingBrace}x, y{braces.closingBrace};{SpacesTemplate}x1+y1;",
                    expr =>
                        expr.Replace("F1", invalidSymbolBeforeBraces)
                            .Replace("x1+y1", "x1 y1"),
                    expr =>
                        new ValidatedParseErrorItem(expr.OrdinalIndexOf(braces.openingBrace, 1), ParseErrorItemCode.NoSeparationBetweenSymbols,
                            () => ExpressionParserMessages.NoSeparationBetweenSymbolsError),
                    expr =>
                        new ValidatedParseErrorItem(expr.OrdinalLastIndexOf("y1"), ParseErrorItemCode.NoSeparationBetweenSymbols));
            }

            ValidateParseErrorLocal("'Some text'");
            ValidateParseErrorLocal("158");
        }

        #endregion

        #region Code block and braces not closed tests

        private TransformTestLanguageProviderDelegate GetTransformTestLanguageProviderDelegateForCriticalError(bool addCriticalError)
        {
            void TransformTestLanguageProviderForCriticalError(TestLanguageProviderForExpressionParseErrorTests expressionLanguageProvider, string parsedExpression, bool isTransformedForSuccess)
            {
                var keyword1Info = new KeywordInfoForTests("keyword1");
                expressionLanguageProvider.Keywords.Add(keyword1Info);

                var customExpressionItemParserMock = new CustomExpressionItemParserMock
                {
                    TryCreateCustomExpressionItemForKeyword = (context, keywordId,
                        parsedPrefixExpressionItems,
                        parsedKeywordExpressionItemsWithoutLastKeyword, lastKeywordExpressionItem) =>
                    {
                        if (keywordId == keyword1Info.Id)
                        {
                            if (!isTransformedForSuccess && addCriticalError)
                                context.AddParseErrorItem(new ParseErrorItem(lastKeywordExpressionItem.IndexInText,
                                    () => "Critical error", 100000, true));

                            return new CustomExpressionItemMock(parsedPrefixExpressionItems,
                                parsedKeywordExpressionItemsWithoutLastKeyword,
                                lastKeywordExpressionItem,
                                CustomExpressionItemCategory.Regular);
                        }

                        return null;
                    }
                };

                expressionLanguageProvider.CustomExpressionItemParsers.Add(customExpressionItemParserMock);
            }

            return TransformTestLanguageProviderForCriticalError;
        }


        [TestCase(true)]
        [TestCase(false)]
        public void CodeBlockNotClosed(bool testCriticalError)
        {
            var codeBlockEndMarkerMissingError = "Code block end marker ";
            var errors = new List<GetValidatedParseErrorItemDelegate>();

            if (testCriticalError)
            {
                errors.Add(expr =>
                    new ValidatedParseErrorItem(expr.OrdinalIndexOf("keyword1"), 100000));
            }
            else
            {
                errors.Add(expr =>
                    new ValidatedParseErrorItem(expr.OrdinalIndexOf("{"), ParseErrorItemCode.CodeBlockEndMarkerMissing,
                        () => codeBlockEndMarkerMissingError));

                errors.Add(expr =>
                    new ValidatedParseErrorItem(expr.OrdinalIndexOf("{", 2), ParseErrorItemCode.CodeBlockEndMarkerMissing,
                        () => codeBlockEndMarkerMissingError));
            };

            errors.Add(expr =>
                new ValidatedParseErrorItem(expr.OrdinalLastIndexOf(",") + 1, ParseErrorItemCode.ExpressionMissingAfterComma,
                    () => ExpressionParserMessages.ExpressionMissingAfterCommaError));

            ValidateParseError(GetTransformTestLanguageProviderDelegateForCriticalError(testCriticalError),
                $"{{a+b; f1(x, y){{ return x+y;{SpacesTemplate}}} {{x=y; F(x, y);{SpacesTemplate}}} x3+y3;}} F2(x, y)+keyword1 A", validExpression =>
                    validExpression.Replace("} x3", " x3")
                        .Replace("y3;}", "y3;")
                        .Replace("F2(x, y", "F2(x, "),
                errors.ToArray());
        }

        /// <summary>
        /// Use this test to run specific expressions if a test in this class fails.
        /// </summary>
        [Test]
        public void DiagnosticsTest()
        {
            var expression = @"keyword1 A";

            var expressionLanguageProvider = CreateExpressionLanguageProvider("rem", "rem*", "*rem");

            var transformLanguageProvider = GetTransformTestLanguageProviderDelegateForCriticalError(false);

            transformLanguageProvider?.Invoke(expressionLanguageProvider, expression, true);

            var expressionParserWrapper = CreateExpressionParserWrapper(expressionLanguageProvider);

            var parseExpressionResult = ParseExpression(expressionParserWrapper, expression);

            if (parseExpressionResult.ParseErrorData.AllParseErrorItems.Count > 0)
            {
                LogHelper.Context.Log.Error(parseExpressionResult.GetErrorTextWithContextualInformation(0, expression.Length, 100));
            }
            else
            {
                LogHelper.Context.Log.Error("No parse errors reported.");
            }
        }

        //[Test]
        //public void TemTetForDebug()
        //{
        //    ValidateParseError("F(x, y]", _ => null);
        //}

        [TestCase(true)]
        [TestCase(false)]
        public void BracesNotClosed(bool testCriticalError)
        {
            var closingBraceMissingError = "Closing brace ";
            var errors = new List<GetValidatedParseErrorItemDelegate>();

            if (testCriticalError)
            {
                errors.Add(expr =>
                    new ValidatedParseErrorItem(expr.OrdinalIndexOf("keyword1"), 100000));
            }
            else
            {
                errors.Add(expr =>
                    new ValidatedParseErrorItem(expr.OrdinalIndexOf("("), ParseErrorItemCode.ClosingBraceMissing,
                        () => closingBraceMissingError));

                errors.Add(expr =>
                    new ValidatedParseErrorItem(expr.OrdinalIndexOf("[", 1), ParseErrorItemCode.ClosingBraceMissing,
                        () => closingBraceMissingError));
            };

            errors.Add(expr =>
                new ValidatedParseErrorItem(expr.OrdinalLastIndexOf(",") + 1, ParseErrorItemCode.ExpressionMissingAfterComma,
                    () => ExpressionParserMessages.ExpressionMissingAfterCommaError));

            ValidateParseError(GetTransformTestLanguageProviderDelegateForCriticalError(testCriticalError),
                $"F1(a+b, f1(x, y)+({{return x+y;}}), f2[0], [x=y, F(x, y){SpacesTemplate}], x3+y3{SpacesTemplate})+ F2(x, y)+keyword1 A", validExpression =>
                    validExpression
                        .Replace($"y3{SpacesTemplate})", $"y3{SpacesTemplate}")
                        .Replace($"y){SpacesTemplate}]", $"y){SpacesTemplate}")
                        .Replace("F2(x, y", "F2(x, "),
                errors.ToArray());

            //ValidateParseError(GetTransformTestLanguageProviderDelegateForCriticalError(testCriticalError),
            //    "keyword1 A", validExpression =>
            //        validExpression
            //            .Replace($"y3{SpacesTemplate})", $"y3{SpacesTemplate}")
            //            .Replace($"y){SpacesTemplate}]", $"y){SpacesTemplate}")
            //            .Replace("F2(x, y", "F2(x, "),
            //    errors.ToArray());
        }

        #endregion

        private IParseExpressionResult ParseExpression([NotNull] ExpressionParserWrapper expressionParserWrapper, [NotNull] string parsedExpression)
        {
            return expressionParserWrapper.ParseExpression(parsedExpression);
        }

        private delegate void TransformTestLanguageProviderDelegate([NotNull] TestLanguageProviderForExpressionParseErrorTests expressionLanguageProvider, [NotNull] string parsedExpression, bool isTransformedForSuccess);

        /// <summary>
        /// Validates the expression parse error.
        /// </summary>
        /// <param name="validExpression">Valid expression.</param>
        /// <param name="transformValidExpressionToInvalidExpression">Transforms valid expression to an invalid expression.</param>
        /// <param name="validatedCodeItemParseErrors">An array of <see cref="ValidatedParseErrorItem"/> that parse function <see cref="ParseExpression"/> should generate,
        /// where <see cref="ValidatedParseErrorItem.ErrorIndexInParsedText"/> is an index in invalid expression generated by <paramref name="transformValidExpressionToInvalidExpression"/>.
        /// In other words, the error index is in invalid expression, before the spaces are added.
        /// </param>
        private void ValidateParseError(
            [NotNull] string validExpression, [NotNull] Func<string, string> transformValidExpressionToInvalidExpression,
           params GetValidatedParseErrorItemDelegate[] validatedCodeItemParseErrors)
        {
            ValidateParseError(null, validExpression,
                transformValidExpressionToInvalidExpression,
                ParseExpression, validatedCodeItemParseErrors);
        }

        [NotNull]
        private delegate ValidatedParseErrorItem GetValidatedParseErrorItemDelegate([NotNull] string validExpression);

        /// <summary>
        /// Validates the expression parse error.
        /// </summary>
        /// <param name="transformTestLanguageProvider">Transforms the instance of <see cref="TestLanguageProviderForExpressionParseErrorTests"/> to another language provider.</param>
        /// <param name="validExpression">Valid expression.</param>
        /// <param name="transformValidExpressionToInvalidExpression">Transforms valid expression to an invalid expression.</param>

        /// <param name="validatedCodeItemParseErrors">An array of <see cref="ValidatedParseErrorItem"/> that parser should generate,
        /// where <see cref="ValidatedParseErrorItem.ErrorIndexInParsedText"/> is an index in invalid expression generated by <paramref name="transformValidExpressionToInvalidExpression"/>.
        /// In other words, the error index is in invalid expression, before the spaces are added.
        /// </param>
        private void ValidateParseError([CanBeNull] TransformTestLanguageProviderDelegate transformTestLanguageProvider,
            [NotNull] string validExpression,
            [NotNull] Func<string, string> transformValidExpressionToInvalidExpression,
            params GetValidatedParseErrorItemDelegate[] validatedCodeItemParseErrors)
        {
            ValidateParseError(transformTestLanguageProvider, validExpression,
                transformValidExpressionToInvalidExpression,
                ParseExpression, validatedCodeItemParseErrors);
        }

        [Flags]
        private enum CommentType
        {
            None = 0,
            CSharp = 1,
            BatFile = 2
        }

        /// <summary>
        /// Validates the expression parse error.
        /// </summary>
        /// <param name="transformTestLanguageProvider">Transforms the instance of <see cref="TestLanguageProviderForExpressionParseErrorTests"/> to another language provider.</param>
        /// <param name="validExpression">Valid expression.</param>
        /// <param name="transformValidExpressionToInvalidExpression">Transforms valid expression to an invalid expression.</param>
        /// <param name="parseExpression">A parse expression function. Most common value that can be passed as this parameter is <see cref="ParseExpression"/>.</param>

        /// <param name="validatedCodeItemParseErrors">An array of <see cref="ValidatedParseErrorItem"/> that parse function <paramref name="parseExpression"/> should generate,
        /// where <see cref="ValidatedParseErrorItem.ErrorIndexInParsedText"/> is an index in invalid expression generated by <paramref name="transformValidExpressionToInvalidExpression"/>.
        /// In other words, the error index is in invalid expression, before the spaces are added.
        /// </param>
        private void ValidateParseError([CanBeNull] TransformTestLanguageProviderDelegate transformTestLanguageProvider,
            [NotNull] string validExpression, [NotNull] Func<string, string> transformValidExpressionToInvalidExpression,
            [NotNull] Func<ExpressionParserWrapper, string, IParseExpressionResult> parseExpression,

            params GetValidatedParseErrorItemDelegate[] validatedCodeItemParseErrors)
        {
            ValidateParseError(transformTestLanguageProvider, validExpression, transformValidExpressionToInvalidExpression, parseExpression,
                CommentType.CSharp | CommentType.BatFile, validatedCodeItemParseErrors);
        }

        [NotNull]
        private TestLanguageProviderForExpressionParseErrorTests CreateExpressionLanguageProvider(
            [NotNull] String lineCommentMarker, [NotNull] String multilineCommentStartMarker, [NotNull] String multilineCommentEndMarker)
        {
            return new TestLanguageProviderForExpressionParseErrorTests
            {
                LineCommentMarker = lineCommentMarker,
                MultilineCommentStartMarker = multilineCommentStartMarker,
                MultilineCommentEndMarker = multilineCommentEndMarker
            };
        }

        [NotNull]
        private ExpressionParserWrapper CreateExpressionParserWrapper([NotNull] TestLanguageProviderForExpressionParseErrorTests testLanguageProviderForExpressionParseErrorTests)
        {
            var expressionLanguageProviderCache = new ExpressionLanguageProviderCache(_defaultExpressionLanguageProviderValidator);

            expressionLanguageProviderCache.RegisterExpressionLanguageProvider(
                testLanguageProviderForExpressionParseErrorTests);

            return new ExpressionParserWrapper(testLanguageProviderForExpressionParseErrorTests.LanguageName, 
                new ExpressionParser(new TextSymbolsParserFactory(), expressionLanguageProviderCache, LogHelper.Context.Log));
        }

        /// <summary>
        /// Validates the expression parse error.
        /// </summary>
        /// <param name="transformTestLanguageProvider">Transforms the instance of <see cref="TestLanguageProviderForExpressionParseErrorTests"/> to another language provider.</param>
        /// <param name="validExpression">Valid expression.</param>
        /// <param name="transformValidExpressionToInvalidExpression">Transforms valid expression to an invalid expression.</param>
        /// <param name="parseExpression">A parse expression function. Most common value that can be passed as this parameter is <see cref="ParseExpression"/>.</param>
        /// <param name="commentTypeFlags">Comment type flags.</param>
        /// <param name="validatedCodeItemParseErrors">An array of <see cref="ValidatedParseErrorItem"/> that parse function <paramref name="parseExpression"/> should generate,
        /// where <see cref="ValidatedParseErrorItem.ErrorIndexInParsedText"/> is an index in invalid expression generated by <paramref name="transformValidExpressionToInvalidExpression"/>.
        /// In other words, the error index is in invalid expression, before the spaces are added.
        /// </param>
        private void ValidateParseError([CanBeNull] TransformTestLanguageProviderDelegate transformTestLanguageProvider,
            [NotNull] string validExpression, [NotNull] Func<string, string> transformValidExpressionToInvalidExpression,
            [NotNull] Func<ExpressionParserWrapper, string, IParseExpressionResult> parseExpression,
            CommentType commentTypeFlags,
            params GetValidatedParseErrorItemDelegate[] validatedCodeItemParseErrors)
        {
            var invalidExpression = transformValidExpressionToInvalidExpression(validExpression);

            List<(string lineCommentMarker, string multilineCommentStartMarker, string multilineCommentEndMarker)> commentsFormatData =
                new List<(string lineCommentMarker, string multilineCommentStartMarker, string multilineCommentEndMarker)>();

            if ((commentTypeFlags & CommentType.CSharp) == CommentType.CSharp)
                commentsFormatData.Add(("//", "/*", "*/"));

            if ((commentTypeFlags & CommentType.BatFile) == CommentType.BatFile)
                commentsFormatData.Add(("rem", "rem*", "*rem"));

            for (var testSuccess = _doNotRunSuccessTests ? 0 : 1; testSuccess >= 0; --testSuccess)
            {
                foreach (var commentFormatData in commentsFormatData)
                {
                    if (testSuccess == 1)
                    {
                        var expressionLanguageProvider = CreateExpressionLanguageProvider(commentFormatData.lineCommentMarker,
                                    commentFormatData.multilineCommentStartMarker, commentFormatData.multilineCommentEndMarker);

                        validExpression = validExpression.Replace(InjectedInvalidCodeTemplate, string.Empty);

                        transformTestLanguageProvider?.Invoke(expressionLanguageProvider, validExpression, true);

                        // Parse valid expression
                        foreach (var validExpressionWithSpaces in GetParsedTextsWithSpacesAndComments(expressionLanguageProvider, validExpression))
                        {
                            var expressionParserWrapper = CreateExpressionParserWrapper(expressionLanguageProvider);

                            var parseExpressionResult =
                                parseExpression(expressionParserWrapper, validExpressionWithSpaces);

                            Assert.IsNotNull(parseExpressionResult);

                            if (parseExpressionResult.ParseErrorData.AllParseErrorItems.Count > 0)
                            {
                                LogHelper.Context.Log.Error(
                                    parseExpressionResult.GetErrorTextWithContextualInformation(0, validExpressionWithSpaces.Length));
                                Assert.Fail("Expression had parse errors.");
                            }
                        }
                    }
                    else
                    {
                        if (validatedCodeItemParseErrors.Length == 0)
                            continue;

                        var invalidExpressionsWithSpaces = GetParsedTextsWithSpacesAndComments(CreateExpressionLanguageProvider(commentFormatData.lineCommentMarker,
                            commentFormatData.multilineCommentStartMarker, commentFormatData.multilineCommentEndMarker), invalidExpression);

                        foreach (var invalidExpressionWithSpaces in invalidExpressionsWithSpaces)
                        {
                            var expressionLanguageProvider = CreateExpressionLanguageProvider(commentFormatData.lineCommentMarker,
                                commentFormatData.multilineCommentStartMarker, commentFormatData.multilineCommentEndMarker);
                            transformTestLanguageProvider?.Invoke(expressionLanguageProvider, invalidExpressionWithSpaces, false);

                            var expressionParserWrapper = CreateExpressionParserWrapper(expressionLanguageProvider);

                            Assert.IsTrue(validatedCodeItemParseErrors != null && validatedCodeItemParseErrors.Length > 0, "No validation errors provided.");

                            var sortedValidatedCodeItemParseErrors =
                                new List<ValidatedParseErrorItem>(validatedCodeItemParseErrors.Length);

                            foreach (var validatedCodeItemParseError in validatedCodeItemParseErrors)
                                sortedValidatedCodeItemParseErrors.Add(validatedCodeItemParseError(invalidExpressionWithSpaces));

                            sortedValidatedCodeItemParseErrors.Sort((x, y) => x.ErrorIndexInParsedText - y.ErrorIndexInParsedText);

                            var parseExpressionResult = parseExpression(expressionParserWrapper, invalidExpressionWithSpaces);

                            var errorsAreLogged = false;

                            try
                            {
                                Assert.IsNotNull(parseExpressionResult);

                                var expectedAndActualErrorsDoNotMatch = false;
                                if (sortedValidatedCodeItemParseErrors.Count !=
                                    parseExpressionResult.ParseErrorData.AllParseErrorItems.Count)
                                {
                                    expectedAndActualErrorsDoNotMatch = true;
                                    LogHelper.Context.Log.ErrorFormat(
                                        "Number of expected errors is {0}. Number of actual errors is {1}.",
                                        sortedValidatedCodeItemParseErrors.Count,
                                        parseExpressionResult.ParseErrorData.AllParseErrorItems.Count);
                                }

                                for (var validatedCodeItemParseErrorIndex = 0; validatedCodeItemParseErrorIndex < sortedValidatedCodeItemParseErrors.Count; ++validatedCodeItemParseErrorIndex)
                                {
                                    var expectedErrorData = sortedValidatedCodeItemParseErrors[validatedCodeItemParseErrorIndex];

                                    ValidatedParseErrorItem transformedExpectedErrorData = expectedErrorData;

                                    var actualErrors = parseExpressionResult.ParseErrorData
                                        .AllParseErrorItems
                                        .Where(
                                            x => x.ErrorIndexInParsedText ==
                                                 transformedExpectedErrorData.ErrorIndexInParsedText &&
                                                 x.ParseErrorItemCode ==
                                                 transformedExpectedErrorData.ParseErrorItemCode &&
                                                 (string.IsNullOrWhiteSpace(transformedExpectedErrorData
                                                      .ErrorMessage) ||
                                                  x.ErrorMessage.Contains(transformedExpectedErrorData.ErrorMessage)
                                                 ))
                                        .ToList();

                                    if (actualErrors.Count != 1)
                                    {
                                        expectedAndActualErrorsDoNotMatch = true;
                                        LogHelper.Context.Log.ErrorFormat(
                                            "No parse error with {0}={1}, {2}={3} and text containing '{4}'. Failing error index is {5}",
                                            nameof(IParseErrorItem.ParseErrorItemCode),
                                            transformedExpectedErrorData.ParseErrorItemCode,
                                            nameof(IParseErrorItem.ErrorIndexInParsedText),
                                            transformedExpectedErrorData.ErrorIndexInParsedText,
                                            transformedExpectedErrorData.ErrorMessage ?? "No error text",
                                            validatedCodeItemParseErrorIndex);
                                    }
                                }

                                if (expectedAndActualErrorsDoNotMatch)
                                {
                                    errorsAreLogged = true;

                                    var loggedErrors = new StringBuilder();

                                    loggedErrors.AppendLine();
                                    loggedErrors.AppendLine();
                                    loggedErrors.AppendLine("----------------------------------------------------------------------");
                                    loggedErrors.AppendLine();
                                    loggedErrors.AppendLine("-------------------------Parsed text:---------------------------------");
                                    loggedErrors.AppendLine($"Parsed text: {invalidExpressionWithSpaces}");
                                    loggedErrors.AppendLine();

                                    loggedErrors.AppendLine("-------------------------Actual errors:-------------------------------");
                                    loggedErrors.Append(parseExpressionResult.GetErrorTextWithContextualInformation(0,
                                        invalidExpressionWithSpaces.Length, 100));
                                    loggedErrors.AppendLine();

                                    loggedErrors.AppendLine("-------------------------Expected errors:-----------------------------");
                                    loggedErrors.Append(parseExpressionResult.GetErrorTextWithContextualInformation(
                                        sortedValidatedCodeItemParseErrors, 0, invalidExpressionWithSpaces.Length, 100));

                                    LogHelper.Context.Log.Error(loggedErrors.ToString());
                                    Assert.Fail("Expected and actual errors are different.");
                                }
                            }
                            finally
                            {
                                if (!errorsAreLogged)
                                {
                                    var loggedErrors = new StringBuilder();

                                    loggedErrors.AppendLine();
                                    loggedErrors.AppendLine();
                                    loggedErrors.AppendLine("----------------------------------------------------------------------");
                                    loggedErrors.AppendLine("-------------------------Parsed text:---------------------------------");
                                    loggedErrors.AppendLine($"Parsed text: {invalidExpressionWithSpaces}");
                                    loggedErrors.AppendLine();


                                    loggedErrors.AppendLine("-------------------------Actual errors:-------------------------------");
                                    loggedErrors.Append(parseExpressionResult.GetErrorTextWithContextualInformation(0,
                                        invalidExpressionWithSpaces.Length, 100));
                                    loggedErrors.AppendLine();

                                    LogHelper.Context.Log.Info(loggedErrors.ToString());
                                }
                            }
                        }
                    }
                }
            }
        }

        [NotNull, ItemNotNull]
        private List<string> GetParsedTextsWithSpacesAndComments([NotNull] IExpressionLanguageProvider expressionLanguageProvider, [NotNull] string expressionToAddSpacesTo)
        {
            var parsedExpressionsWithCommentsAndSpaces = new List<string>();

            if (expressionToAddSpacesTo.Contains(SpacesTemplate))
            {
                var commentsAndSpacesList = GetSpacesAndComments(expressionLanguageProvider);

                var splitExpression = expressionToAddSpacesTo.Split(SpacesTemplate);
                foreach (var commentAndSpaces in commentsAndSpacesList)
                {
                    var expressionWithSpaces = new StringBuilder();

                    for (var splitExpressionPartInd = 0; splitExpressionPartInd < splitExpression.Length; ++splitExpressionPartInd)
                    {
                        var splitExpressionPart = splitExpression[splitExpressionPartInd];

                        if (splitExpressionPart.Length == 0)
                        {
                            expressionWithSpaces.Append(commentAndSpaces);
                        }
                        else
                        {
                            expressionWithSpaces.Append(splitExpressionPart);

                            var stringComparison = StringComparison.OrdinalIgnoreCase;

                            if (splitExpression[splitExpressionPartInd].Length > 0)
                            {
                                var previousSplitExpressionPart = splitExpression[splitExpressionPartInd];

                                if (!(Char.IsWhiteSpace(previousSplitExpressionPart[^1]) || SpecialCharactersCacheThreadStaticContext.Context.IsSpecialCharacter(previousSplitExpressionPart[^1])))
                                {
                                    if ((expressionLanguageProvider.LineCommentMarker?.Equals("rem", stringComparison) ?? false) &&
                                        commentAndSpaces.StartsWith(expressionLanguageProvider.LineCommentMarker, stringComparison) ||
                                        (expressionLanguageProvider.MultilineCommentEndMarker?.Equals("rem*", stringComparison) ?? false) &&
                                        commentAndSpaces.StartsWith(expressionLanguageProvider.MultilineCommentEndMarker, stringComparison))
                                        expressionWithSpaces.Append(' ');

                                    expressionWithSpaces.Append(commentAndSpaces);
                                }
                            }

                            if (splitExpressionPartInd < splitExpression.Length - 1 && splitExpression[splitExpressionPartInd + 1].Length > 0)
                            {
                                var nextSplitExpressionPart = splitExpression[splitExpressionPartInd + 1];

                                expressionWithSpaces.Append(commentAndSpaces);

                                if (!(Char.IsWhiteSpace(nextSplitExpressionPart[0]) || SpecialCharactersCacheThreadStaticContext.Context.IsSpecialCharacter(nextSplitExpressionPart[0])))
                                {
                                    // If line comment rem wa added to commentAndSpaces, it would have been followed by a new line
                                    if ((expressionLanguageProvider.MultilineCommentEndMarker?.Equals("*rem", stringComparison) ?? false) &&
                                        commentAndSpaces.EndsWith(expressionLanguageProvider.MultilineCommentEndMarker, stringComparison))
                                        expressionWithSpaces.Append(' ');
                                    
                                }
                            }


                            /*if (splitExpressionPartInd < splitExpression.Length - 1 && splitExpression[splitExpressionPartInd + 1].Length > 0)
                            {
                                if (expressionLanguageProvider.LineCommentMarker == "rem" &&
                                    commentAndSpaces.StartsWith(expressionLanguageProvider.LineCommentMarker) ||
                                    expressionLanguageProvider.MultilineCommentStartMarker == "rem*" &&
                                    commentAndSpaces.StartsWith(expressionLanguageProvider.MultilineCommentStartMarker))
                                    expressionWithSpaces.Append(" ");

                                expressionWithSpaces.Append(commentAndSpaces);
                            }*/
                        }
                    }

                    parsedExpressionsWithCommentsAndSpaces.Add(expressionWithSpaces.ToString());
                }
            }
            else
            {
                parsedExpressionsWithCommentsAndSpaces.Add(expressionToAddSpacesTo);
            }

            return parsedExpressionsWithCommentsAndSpaces;
        }

        private List<string> GetSpacesAndComments([NotNull] IExpressionLanguageProvider expressionLanguageProvider)
        {

            var commentsAreSupported = !(string.IsNullOrEmpty(expressionLanguageProvider.LineCommentMarker) ||
                                         string.IsNullOrEmpty(expressionLanguageProvider.MultilineCommentStartMarker) ||
                                         string.IsNullOrEmpty(expressionLanguageProvider.MultilineCommentEndMarker));

            var setOfSpacesAndComments = new HashSet<string>();

            setOfSpacesAndComments.Add(string.Empty);

            if (!_doNotAddSpacesAndComments)
            {
                var codeGenerationHelper = new CodeGenerationHelper(new CodeGeneratorParametersProvider(this, expressionLanguageProvider));

                for (var i = 0; i < 30; ++i)
                {
                    while (true)
                    {
                        var spacesAndCommentsStrBldr = new StringBuilder();

                        codeGenerationHelper.GenerateWhitespacesAndComments(spacesAndCommentsStrBldr, true, null,
                            commentsAreSupported ? WhitespaceCommentFlags.WhiteSpacesAndComments : WhitespaceCommentFlags.WhiteSpace,
                            new List<CharacterTypeProbabilityData>
                            {
                                // Lets only allow non-latin characters in comments, so that the generated comment text does not conflict
                                // with text we expect the validated code have.
                                new CharacterTypeProbabilityData(GeneratedCharacterType.NonLatinCharacter, 60),
                                new CharacterTypeProbabilityData(GeneratedCharacterType.Number, 10),
                                new CharacterTypeProbabilityData(GeneratedCharacterType.Dot, 10),
                                new CharacterTypeProbabilityData(GeneratedCharacterType.Underscore, 10),
                                new CharacterTypeProbabilityData(GeneratedCharacterType.Apostrophe, 10)
                            });

                        var spacesAndComments = spacesAndCommentsStrBldr.ToString();
                        if (setOfSpacesAndComments.Contains(spacesAndComments))
                            continue;

                        setOfSpacesAndComments.Add(spacesAndComments);

                        break;
                    }
                }
            }

            return setOfSpacesAndComments.ToList();
        }
        private class ExpressionParserWrapper
        {
            [NotNull] private readonly string _expressionLanguageProviderLanguageName;
            [NotNull] private readonly IExpressionParser _expressionParser;

            public ExpressionParserWrapper([NotNull] String expressionLanguageProviderLanguageName, [NotNull] IExpressionParser expressionParser)
            {
                _expressionLanguageProviderLanguageName = expressionLanguageProviderLanguageName;
                _expressionParser = expressionParser;
            }

            [NotNull]
            public IParseExpressionResult ParseExpression(
                [NotNull] string expressionText,
                [CanBeNull] ParseExpressionOptions parseExpressionOptions = null)
            {
                return _expressionParser.ParseExpression(_expressionLanguageProviderLanguageName,
                    expressionText, parseExpressionOptions ?? new ParseExpressionOptions());
            }

            [NotNull]
            public IParseExpressionResult ParseBracesExpression([NotNull] string expressionText, [CanBeNull] ParseExpressionOptions parseExpressionOptions)
            {
                return _expressionParser.ParseBracesExpression(_expressionLanguageProviderLanguageName,
                    expressionText, parseExpressionOptions ?? new ParseExpressionOptions());
            }

            [NotNull]
            public IParseExpressionResult ParseCodeBlockExpression([NotNull] string expressionText, ParseExpressionOptions parseExpressionOptions)
            {
                return _expressionParser.ParseCodeBlockExpression(_expressionLanguageProviderLanguageName,
                    expressionText, parseExpressionOptions ?? new ParseExpressionOptions());
            }
        }

        private class ValidatedParseErrorItem : IParseErrorItem
        {
            [CanBeNull]
            private readonly string _errorMessage = "";
            [CanBeNull]
            private readonly Func<string> _getErrorMessage;

            public ValidatedParseErrorItem(int errorIndexInParsedText, int parseErrorItemCode, string errorMessage = "", bool isCriticalError = false)
            {
                ErrorIndexInParsedText = errorIndexInParsedText;
                _errorMessage = errorMessage;
                ParseErrorItemCode = parseErrorItemCode;
                IsCriticalError = isCriticalError;
            }

            public ValidatedParseErrorItem(int errorIndexInParsedText, int parseErrorItemCode, [NotNull] Func<string> getErrorMessage, bool isCriticalError = false)
            {
                ErrorIndexInParsedText = errorIndexInParsedText;
                ParseErrorItemCode = parseErrorItemCode;
                _getErrorMessage = getErrorMessage;
                IsCriticalError = isCriticalError;
            }

            public int ErrorIndexInParsedText { get; set; }

            // ReSharper disable once AssignNullToNotNullAttribute
            public string ErrorMessage => _getErrorMessage != null ? _getErrorMessage() : _errorMessage;

            public int ParseErrorItemCode { get; }
            public bool IsCriticalError { get; private set; }

            public ValidatedParseErrorItem CopyWithChangedPosition(int errorIndexInParsedText)
            {
                if (_getErrorMessage != null)
                    return new ValidatedParseErrorItem(errorIndexInParsedText, this.ParseErrorItemCode, _getErrorMessage);

                return new ValidatedParseErrorItem(errorIndexInParsedText, this.ParseErrorItemCode, _errorMessage);
            }
        }

        private class CodeGeneratorParametersProvider : ICodeGeneratorParametersProvider
        {
            [NotNull] private readonly ExpressionParseErrorTests _expressionParseErrorTests;
            [NotNull] private readonly IExpressionLanguageProvider _expressionLanguageProvider;

            public CodeGeneratorParametersProvider([NotNull] ExpressionParseErrorTests expressionParseErrorTests, [NotNull] IExpressionLanguageProvider expressionLanguageProvider)
            {
                _expressionParseErrorTests = expressionParseErrorTests;
                _expressionLanguageProvider = expressionLanguageProvider;


                if (!(string.IsNullOrEmpty(expressionLanguageProvider.LineCommentMarker) ||
                      string.IsNullOrEmpty(expressionLanguageProvider.MultilineCommentStartMarker) ||
                      string.IsNullOrEmpty(expressionLanguageProvider.MultilineCommentEndMarker)))
                    CommentMarkersData = new CommentMarkersData(expressionLanguageProvider.LineCommentMarker,
                        expressionLanguageProvider.MultilineCommentStartMarker, expressionLanguageProvider.MultilineCommentEndMarker,
                        expressionLanguageProvider.MultilineCommentStartMarker == "/*" ? CommentMarkerType.CSharpStyle : CommentMarkerType.RemarkText);
            }
            public IRandomNumberGenerator RandomNumberGenerator => _expressionParseErrorTests._randomNumberGenerator;
            public int MaxNumberOfAdditionalWhitespaces { get; } = 5;
            public int MaxNumberOfAdditionalComments { get; } = 3;
            public int MaxLengthOfComment { get; } = 10;
            public CommentMarkersData CommentMarkersData { get; }
            public bool SimulateNiceCode { get; } = false;
            public bool IsLanguageCaseSensitive => _expressionLanguageProvider.IsLanguageCaseSensitive;
        }
    }
}
