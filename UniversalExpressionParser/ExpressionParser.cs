// Copyright (c) UniversalExpressionParser Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using OROptimizer.Diagnostics.Log;
using TextParser;
using UniversalExpressionParser.ExpressionItems;
using UniversalExpressionParser.ExpressionItems.Custom;
using UniversalExpressionParser.Parser;

namespace UniversalExpressionParser
{
    /// <inheritdoc />
    public class ExpressionParser : IExpressionParser
    {

#if DEBUG
#pragma warning disable 414
        /// <summary>
        /// Counter used for diagnostics purposes. Not visible in release mode.
        /// </summary>
        internal static int Counter = 0;

        private static void LogContextData([NotNull] ParseExpressionItemContext context)
        {
            LogHelper.Context.Log.InfoFormat("Counter={0}. PositionInText={1}.", Counter, context.TextSymbolsParser.PositionInText);
        }
#pragma warning restore 414
#endif

        [NotNull]
        private readonly ITextSymbolsParserFactory _textSymbolsParserFactory;

        [NotNull]
        private readonly IExpressionLanguageProviderCache _expressionLanguageProviderCache;

        [NotNull] 
        private readonly ParserHelper _parserHelper = new ParserHelper();

        [NotNull]
        private readonly ErrorsHelper _errorsHelper = new ErrorsHelper();

        [NotNull]
        private readonly ParseKeywordsHelper _parseKeywordsHelper = new ParseKeywordsHelper();

        [NotNull]
        private readonly ParseConstantNumericValueHelper _parseConstantNumericValueHelper;

        [NotNull] 
        private readonly ParseConstantTextHelper _parseConstantTextHelper;

        [NotNull] 
        private readonly ParseOperatorsHelper _parseOperatorsHelper;

        [NotNull]
        private readonly ParseCustomExpressionHelper _parseCustomExpressionHelper;

        [NotNull] private readonly GenerateExpressionItemHelper _generateExpressionItemHelper;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="textSymbolsParserFactory">An instance of <see cref="ITextSymbolsParserFactory"/></param>
        /// <param name="expressionLanguageProviderCache">
        /// A cache that maps the language name to an instance of <see cref="IExpressionLanguageProviderWrapper"/>.
        /// The recommended default implementation to use is <see cref="ExpressionLanguageProviderCache"/>.
        /// </param>
        public ExpressionParser([NotNull] ITextSymbolsParserFactory textSymbolsParserFactory,
                                [NotNull] IExpressionLanguageProviderCache expressionLanguageProviderCache)
        {
            _textSymbolsParserFactory = textSymbolsParserFactory;
            _expressionLanguageProviderCache = expressionLanguageProviderCache;

            _parseOperatorsHelper = new ParseOperatorsHelper(_parseKeywordsHelper);
            _generateExpressionItemHelper = new GenerateExpressionItemHelper(_parseOperatorsHelper);
            _parseConstantNumericValueHelper = new ParseConstantNumericValueHelper(_parserHelper);
            _parseConstantTextHelper = new ParseConstantTextHelper(_parserHelper);
            _parseCustomExpressionHelper = new ParseCustomExpressionHelper(_parserHelper, _errorsHelper);

            if (!LogHelper.IsContextInitialized)
                LogHelper.RegisterContext(new NullLogHelperContext());
        }


        private delegate TParseExpressionResult CreateParseExpressionResult<out TParseExpressionResult>([NotNull] ITextSymbolsParser textSymbolsParser,
                                                                                       [NotNull] IExpressionLanguageProviderWrapper expressionLanguageProviderWrapper,
                                                                                       [NotNull] IParseErrorData parseErrorData) where TParseExpressionResult : IParseExpressionResult;
        private TParseExpressionResult ParseExpressionLocal<TParseExpressionResult>([NotNull] string expressionLanguageProviderName, string expressionText,
                                                                    [NotNull] IParseExpressionOptions parseExpressionOptions,
                                                                    [NotNull] CreateParseExpressionResult<TParseExpressionResult> createParseExpressionResult,
                                                                    [NotNull] Action<TParseExpressionResult, ParseExpressionItemContext> doParseExpression) where TParseExpressionResult : IParseExpressionResult
        {
            var expressionItemSettingCurrent = ExpressionItemSettingsAmbientContext.Context;

            try
            {
                ExpressionItemSettingsAmbientContext.Context = new ExpressionItemSettings(false);

                var expressionLanguageProviderWrapper = _expressionLanguageProviderCache.GetExpressionLanguageProviderWrapperOrThrow(expressionLanguageProviderName);

                var parseErrorData = new ParseErrorData();

                var textSymbolsParser = _textSymbolsParserFactory.CreateTextSymbolsParser(expressionText,
                    parseExpressionOptions.StartIndex, expressionText.Length - parseExpressionOptions.StartIndex,
                    expressionLanguageProviderWrapper.ExpressionLanguageProvider.IsValidLiteralCharacter);

                var parseExpressionResult = createParseExpressionResult(textSymbolsParser, expressionLanguageProviderWrapper, parseErrorData);

                var context =
                    new ParseExpressionItemContext(parseExpressionResult, parseErrorData, textSymbolsParser, expressionLanguageProviderWrapper, this,
                        parseExpressionOptions.IsExpressionParsingComplete);

                doParseExpression(parseExpressionResult, context);

                parseExpressionResult.OnParsingComplete(expressionText, context.TextSymbolsParser.PositionInText);
                return parseExpressionResult;
            }
            finally
            {
                ExpressionItemSettingsAmbientContext.Context = expressionItemSettingCurrent;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool GetIsRoundBraces([NotNull] ITextSymbolsParser textSymbolsParser)
        {
            if (textSymbolsParser.CurrentChar == '(')
                return true;

            if (textSymbolsParser.CurrentChar == '[')
                return false;

            throw new ParseTextException(new ParseTextErrorDetails(textSymbolsParser.PositionInText,
                ExpressionParserMessages.InvalidStartTextWhenParsingBracesError));
        }

        /// <inheritdoc />
        public IParseExpressionResult ParseExpression(string expressionLanguageProviderName, string expressionText,
            IParseExpressionOptions parseExpressionOptions)
        {
            return ParseExpressionLocal(expressionLanguageProviderName, expressionText,
                parseExpressionOptions,
                (textSymbolsParser, expressionLanguageProviderWrapper, parseErrorData) =>
                    new ParseExpressionResult(new RootExpressionItem(), parseErrorData),
                (parseExpressionResult, context) =>
                {
                    ParseExpressionItem(parseExpressionResult.RootExpressionItem, context, EvaluateExpressionType.ExpressionItemSeries);
                }
            );
        }

        internal IBracesExpressionItem ParseBracesExpression([NotNull] ParseExpressionItemContext context,
                                                            [CanBeNull] ILiteralExpressionItem literalExpressionItem)
        {
            var textSymbolsParser = context.TextSymbolsParser;

            var bracesExpressionItem = new BracesExpressionItem(new List<IExpressionItemBase>(0), new List<IKeywordExpressionItem>(0),
                literalExpressionItem, 
                GetIsRoundBraces(context.TextSymbolsParser), textSymbolsParser.PositionInText);

            ParseExpressionItem(bracesExpressionItem, context, EvaluateExpressionType.Braces);
            return bracesExpressionItem;
        }

        /// <inheritdoc />
        public IParseExpressionResult ParseBracesExpression(string expressionLanguageProviderName, string expressionText, IParseExpressionOptions parseExpressionOptions)
        {
            return ParseExpressionLocal(expressionLanguageProviderName, expressionText,
                parseExpressionOptions,
                (textSymbolsParser, expressionLanguageProviderWrapper, parseErrorData) =>
                {
                    if (textSymbolsParser.CurrentChar != '(' && textSymbolsParser.CurrentChar != '[')
                        throw new ParseTextException(new ParseTextErrorDetails(textSymbolsParser.PositionInText,
                            ExpressionParserMessages.InvalidStartTextWhenParsingBracesError));

                    var rootExpressionItem = new RootExpressionItem();

                    var bracesExpressionItem = new BracesExpressionItem(new List<IExpressionItemBase>(0),
                        new List<IKeywordExpressionItem>(0), null, 
                        textSymbolsParser.CurrentChar == '(', textSymbolsParser.PositionInText);

                    rootExpressionItem.AddChildExpressionItem(bracesExpressionItem);
                    return new ParseExpressionResult(rootExpressionItem, parseErrorData);
                },
                (parseExpressionResult, context) =>
                {
                    if (parseExpressionResult.RootExpressionItem.Children.Count != 1 ||
                        !(parseExpressionResult.RootExpressionItem.Children[0] is IBracesExpressionItem bracesExpressionItem))
                        throw new ArgumentException($"Expected an instance of {typeof(IBracesExpressionItem)}.");

                    this.ParseExpressionItem(bracesExpressionItem, context, EvaluateExpressionType.Braces);
                });
        }

        internal ICodeBlockExpressionItem ParseCodeBlockExpression([NotNull] ParseExpressionItemContext context)
        {
            var textSymbolsParser = context.TextSymbolsParser;
            var expressionLanguageProvider = context.ExpressionLanguageProviderWrapper.ExpressionLanguageProvider;

            if (expressionLanguageProvider.CodeBlockStartMarker == null)
                throw new ParseTextException(new ParseTextErrorDetails(textSymbolsParser.PositionInText,
                        $"Code blocks are not supported by expression language provider '{context.ExpressionLanguageProviderWrapper.ExpressionLanguageProvider.GetType().FullName}'."));

            if (!Helpers.StartsWithSymbol(textSymbolsParser.TextToParse, expressionLanguageProvider.CodeBlockStartMarker, textSymbolsParser.PositionInText,
                textSymbolsParser.TextToParse.Length, expressionLanguageProvider, null, out var matchedText))
                throw new ParseTextException(new ParseTextErrorDetails(textSymbolsParser.PositionInText,
                        string.Format(ExpressionParserMessages.InvalidStartTextWhenParsingCodeBlockError, expressionLanguageProvider.CodeBlockStartMarker)));

            var codeBlockExpressionItem =
                new CodeBlockExpressionItem(new List<IExpressionItemBase>(0), new List<IKeywordExpressionItem>(0),
                    new CodeBlockStartMarkerExpressionItem(matchedText, textSymbolsParser.PositionInText));

            ParseExpressionItem(codeBlockExpressionItem, context, EvaluateExpressionType.CodeBlock);
            return codeBlockExpressionItem;
        }

        /// <inheritdoc />
        public IParseExpressionResult ParseCodeBlockExpression(string expressionLanguageProviderName, string expressionText, IParseExpressionOptions parseExpressionOptions)
        {

            return ParseExpressionLocal(expressionLanguageProviderName, expressionText,
                parseExpressionOptions,
            (textSymbolsParser, expressionLanguageProviderWrapper, parseErrorData) =>
            {
                var expressionLanguageProvider = expressionLanguageProviderWrapper.ExpressionLanguageProvider;
                if (expressionLanguageProvider.CodeBlockStartMarker == null)
                    throw new ParseTextException(
                        new ParseTextErrorDetails(parseExpressionOptions.StartIndex, $"Code blocks are not supported by expression language provider '{expressionLanguageProvider}'."));

                if (!Helpers.StartsWithSymbol(textSymbolsParser.TextToParse, expressionLanguageProvider.CodeBlockStartMarker, textSymbolsParser.PositionInText,
                    textSymbolsParser.TextToParse.Length, expressionLanguageProvider, null, out var matchedText))
                    throw new ParseTextException(new ParseTextErrorDetails(textSymbolsParser.PositionInText,
                            string.Format(ExpressionParserMessages.InvalidStartTextWhenParsingCodeBlockError, expressionLanguageProvider.CodeBlockStartMarker)));
                
                var rootExpressionItem = new RootExpressionItem();

                var codeBlockExpressionItem = new CodeBlockExpressionItem(new List<IExpressionItemBase>(0),
                    new List<IKeywordExpressionItem>(0),
                    new CodeBlockStartMarkerExpressionItem(matchedText, textSymbolsParser.PositionInText));

                rootExpressionItem.AddChildExpressionItem(codeBlockExpressionItem);

                return new ParseExpressionResult(rootExpressionItem, parseErrorData);
            },
            (parseExpressionResult, context) =>
            {
                if (parseExpressionResult.RootExpressionItem.Children.Count != 1 ||
                    !(parseExpressionResult.RootExpressionItem.Children[0] is ICodeBlockExpressionItem codeBlockExpressionItem))
                    throw new ArgumentException($"Expected an instance of {typeof(ICodeBlockExpressionItem)}.");

                this.ParseExpressionItem(codeBlockExpressionItem, context, EvaluateExpressionType.CodeBlock);
            });
        }

        private void AddExpressionItem([NotNull] ParseExpressionItemContext context,
                               [NotNull] ParseExpressionItemData parseExpressionItemData,
                               [NotNull] IExpressionItemBase expressionItem,
                               ref int numberOfOperators)
        {
            if (!(expressionItem is IOperatorInfoExpressionItem))
                this._parseOperatorsHelper.ProcessUnprocessedOperators(context, parseExpressionItemData, expressionItem,
                    this.IgnoreBinaryOperatorWhenBreakingPostfixPrefixTie,
                    ref numberOfOperators);

            parseExpressionItemData.AllExpressionItems.Add(expressionItem);
        }

        private void AddInvalidKeywordUsageError([NotNull] IParseExpressionItemContext context, [NotNull] ParseExpressionItemData parseExpressionItemData,
            [NotNull] IList<IKeywordExpressionItem> keywordExpressionItems,
            int parseErrorItemCode = ParseErrorItemCode.InvalidUseOfKeywords, string errorMessage = ExpressionParserMessages.InvalidUseOfKeywordsError)
        {
            if (keywordExpressionItems.Count == 0)
                return;

            parseExpressionItemData.OperatorsCannotBeEvaluated = true;

            var lastKeyword = keywordExpressionItems[keywordExpressionItems.Count - 1];
            context.AddParseErrorItem(new ParseErrorItem(lastKeyword.IndexInText,
                () => errorMessage, parseErrorItemCode));

            foreach (var keywordExpressionItem in keywordExpressionItems)
                parseExpressionItemData.AllExpressionItems.Add(keywordExpressionItem);

            keywordExpressionItems.Clear();
        }

        private bool LastExpressionItemCanBeUsedAsPrefix([NotNull] ParseExpressionItemContext context, [NotNull] IComplexExpressionItem lastExpressionItem)
        {
            if (lastExpressionItem is ICustomExpressionItem customExpressionItem)
                return customExpressionItem.CustomExpressionItemCategory == CustomExpressionItemCategory.Prefix;

            if (context.ExpressionLanguageProviderWrapper.ExpressionLanguageProvider.SupportsPrefixes && lastExpressionItem is IBracesExpressionItem bracesExpressionItem)
                return bracesExpressionItem.NameLiteral == null;

            return false;
        }

        /// <summary>
        /// Added for tests only.
        /// </summary>
        [CanBeNull]
        internal IgnoreOperatorInfoDelegate IgnoreOperatorInfoDelegate { get; set; }

        [CanBeNull]
        internal IgnoreBinaryOperatorWhenBreakingPostfixPrefixTieDelegate IgnoreBinaryOperatorWhenBreakingPostfixPrefixTie { get; set; }

        /// <summary>
        /// Parses expressions of types x=y*z, or var x = [x, y, 5]
        /// </summary>
        private void ParseExpressionItem([NotNull] ICanAddChildExpressionItem parentExpressionItem, [NotNull] ParseExpressionItemContext context, EvaluateExpressionType evaluateExpressionType)
        {
            var parseErrorData = context.ParseErrorData;
            var textSymbolsParser = context.TextSymbolsParser;

            var expressionLanguageProvider = context.ExpressionLanguageProviderWrapper.ExpressionLanguageProvider;

            var previousSymbolIsUnparsed = false;
            var lastInvalidSymbolIndex = -1;

            var parsedExpressionItemsStack = new Stack<ParseExpressionItemData>();

            var parseExpressionItemData = new ParseExpressionItemData(parentExpressionItem);
            var lastParsedOperatorsAreInvalid = false;

            var sortedCandidateOperators = expressionLanguageProvider.Operators.OrderByDescending(x => x.NameParts.Count).ToList();

            parsedExpressionItemsStack.Push(parseExpressionItemData);

            if (evaluateExpressionType == EvaluateExpressionType.CodeBlock || evaluateExpressionType == EvaluateExpressionType.Braces)
            {
                // Skip the opening brace or code block start symbol, since it is already parsed in parentExpressionItem.
                // Otherwise, we would add the expression to the stack again
                if (evaluateExpressionType == EvaluateExpressionType.Braces)
                {
                    // Before this method was called, it was already checked that current character is either '(' or '['
                    textSymbolsParser.SkipCharacters(1);
                }
                else
                {
                    if (expressionLanguageProvider.CodeBlockStartMarker == null)
                        throw new ParseTextException(new ParseTextErrorDetails(textSymbolsParser.PositionInText,
                                    "Code block start marker cannot be null in this context."));

                    textSymbolsParser.SkipCharacters(expressionLanguageProvider.CodeBlockStartMarker.Length);
                }
            }

            void GenerateExpressionItemLocal()
            {
                var numberOfOperators = 0;

                var expressionItem = _generateExpressionItemHelper.GenerateExpressionItem(context, parseExpressionItemData,
                    IgnoreBinaryOperatorWhenBreakingPostfixPrefixTie,
                    ref numberOfOperators);

                parseExpressionItemData.AllExpressionItems.Clear();
                parseExpressionItemData.ParsedUnprocessedOperatorInfoItems = null;
                parseExpressionItemData.OperatorsCannotBeEvaluated = false;

                if (expressionItem != null)
                    parseExpressionItemData.ParentExpressionItem.AddChildExpressionItem(expressionItem);
            }

            void SkipInvalidSymbol(int numberOfCharactersToSkip)
            {
                parseExpressionItemData.OperatorsCannotBeEvaluated = true;
                previousSymbolIsUnparsed = true;
                textSymbolsParser.SkipCharacters(numberOfCharactersToSkip);
            }

            void AddExpressionItemLocal(IComplexExpressionItem expressionItem)
            {
                var numberOfOperators = 0;

                this.AddExpressionItem(context, parseExpressionItemData, expressionItem, ref numberOfOperators);
            }

            try
            {
                List<IKeywordExpressionItem> keywordsParsedAfterOperators = null;

                while (Helpers.TryParseComments(context))
                {
#if DEBUG
                    //++Counter;
                    //LogContextData(context);
                    //TraceParsing?.Invoke(context, evaluateExpressionType);
#endif
                    if (parseErrorData.HasCriticalErrors)
                    {
                        GenerateExpressionItemLocal();
                        return;
                    }

                    if (context.CheckIsParsingComplete())
                        break;

                    var lastParsedExpressionItemIndex = -1;

                    if (!lastParsedOperatorsAreInvalid && !previousSymbolIsUnparsed &&
                        (parseExpressionItemData.ParsedUnprocessedOperatorInfoItems?.Count ?? 0) == 0 &&
                        parseExpressionItemData.AllExpressionItems.Count > 0)
                    {
                        lastParsedExpressionItemIndex = parseExpressionItemData.AllExpressionItems.Count - 1;

                        while (lastParsedExpressionItemIndex >= 0 && parseExpressionItemData.AllExpressionItems[lastParsedExpressionItemIndex] is IKeywordExpressionItem)
                            --lastParsedExpressionItemIndex;
                    }

                    lastParsedOperatorsAreInvalid = false;

                    IComplexExpressionItem lastParsedComplexExpressionItem = null;
                    ICustomExpressionItem lastParsedCustomExpressionItem = null;
                    IComplexExpressionItem potentialPrefixExpressionItem = null;

                    if (lastParsedExpressionItemIndex >= 0)
                    {
                        var lastParsedExpressionItem = parseExpressionItemData.AllExpressionItems[lastParsedExpressionItemIndex];

                        if (!(lastParsedExpressionItem is IOperatorInfoExpressionItem))
                        {
                            lastParsedComplexExpressionItem = lastParsedExpressionItem as IComplexExpressionItem;

                            if (lastParsedComplexExpressionItem == null)
                            {
                                context.AddParseErrorItem(new ParseErrorItem(
                                    textSymbolsParser.PositionInText, () => $"Parser implementation error. Expected an instance of '{typeof(IComplexExpressionItem)}'.",
                                    ParseErrorItemCode.ParserImplementationError, true));

                                GenerateExpressionItemLocal();
                                return;
                            }

                            if ((parseExpressionItemData.ParsedUnprocessedOperatorInfoItems?.Count ?? 0) > 0)
                            {
                                context.AddParseErrorItem(new ParseErrorItem(
                                    textSymbolsParser.PositionInText, () => $"Parser implementation error. The list '{nameof(ParseExpressionItemData.ParsedUnprocessedOperatorInfoItems)}' is expected to be empty if '{nameof(lastParsedComplexExpressionItem)}' is not null.",
                                    ParseErrorItemCode.ParserImplementationError, true));

                                GenerateExpressionItemLocal();
                                return;
                            }

                            lastParsedCustomExpressionItem = lastParsedExpressionItem as ICustomExpressionItem;

                            if (lastParsedComplexExpressionItem != null && this.LastExpressionItemCanBeUsedAsPrefix(context, lastParsedComplexExpressionItem))
                                potentialPrefixExpressionItem = lastParsedComplexExpressionItem;
                        }
                    }

                    previousSymbolIsUnparsed = false;

                    List<IKeywordExpressionItem> keywordExpressionItems;

                    if ((keywordsParsedAfterOperators?.Count ?? 0) == 0)
                    {
                        keywordExpressionItems = _parseKeywordsHelper.TryParseKeywords(context);
                    }
                    else
                    {
                        keywordExpressionItems = keywordsParsedAfterOperators;
                        keywordsParsedAfterOperators = null;
                    }

                    #region Try parse custom expression item

                    context.ClearNewlyAddedErrors();
                    var currentPositionInText = textSymbolsParser.PositionInText;

                    var parsedCustomExpressionItem = _parseCustomExpressionHelper.TryParseCustomExpression(
                        context, AddExpressionItemLocal, parseExpressionItemData, keywordExpressionItems,
                        potentialPrefixExpressionItem, lastParsedComplexExpressionItem, lastParsedCustomExpressionItem);

                    if (context.ParseErrorData.HasCriticalErrors)
                    {
                        GenerateExpressionItemLocal();
                        return;
                    }

                    if (parsedCustomExpressionItem != null)
                    {
                        continue;
                    }

                    if (context.NewlyAddedErrors.Count > 0)
                    {
                        // If customExpressionItem is null but errors were reported, lets not continue.
                        context.SetHasCriticalErrors();
                        GenerateExpressionItemLocal();
                        return;
                    }

                    if (currentPositionInText != textSymbolsParser.PositionInText)
                        textSymbolsParser.MoveToPosition(currentPositionInText);
                    #endregion

                    if (!expressionLanguageProvider.SupportsKeywords && keywordExpressionItems.Count > 0)
                    {
                        // The keywords list will be empty after call to AddInvalidKeywordUsageError().
                        AddInvalidKeywordUsageError(context, parseExpressionItemData, keywordExpressionItems, ParseErrorItemCode.KeywordsNotSupported, 
                            ExpressionParserMessages.KeywordsNotSupportedError);
                    }

                    switch (textSymbolsParser.CurrentChar)
                    {
                        case ',':
                            {
                                if (keywordExpressionItems.Count > 0)
                                    AddInvalidKeywordUsageError(context, parseExpressionItemData, keywordExpressionItems,
                                        ParseErrorItemCode.InvalidUseOfKeywordsBeforeComma, ExpressionParserMessages.InvalidUseOfKeywordsBeforeCommaError);

                                if (!(parseExpressionItemData.ParentExpressionItem is BracesExpressionItem bracesExpressionItem))
                                {
                                    parseExpressionItemData.OperatorsCannotBeEvaluated = true;

                                    var currentChar = textSymbolsParser.CurrentChar;

                                    context.AddParseErrorItem(new ParseErrorItem(
                                        textSymbolsParser.PositionInText, () => $"Invalid comma '{currentChar}'. Commas can only be used to separate items within braces. Valid examples are \"F1(x,y+x)\" or \"[1, y+z*i]\". Invalid example is \"x+y,i\".",
                                        ParseErrorItemCode.CommaWithoutParentBraces));

                                    SkipInvalidSymbol(1);
                                }
                                else
                                {
                                    GenerateExpressionItemLocal();
                                    const string validExpressionMissingBeforeCommaError = "Valid expression is missing before comma.";
                                    var bracesLastExpressionItem = bracesExpressionItem.GetLastExpressionItem();

                                    if (!(bracesLastExpressionItem is IComplexExpressionItem))
                                    {
                                        context.AddParseErrorItem(new ParseErrorItem(
                                            textSymbolsParser.PositionInText, () => validExpressionMissingBeforeCommaError,
                                            ParseErrorItemCode.ExpressionMissingBeforeComma));
                                    }

                                    bracesExpressionItem.AddComma(textSymbolsParser.PositionInText);
                                    textSymbolsParser.SkipCharacters(1);

                                    // Lets skip all the commas after this comma.
                                    while (Helpers.TryParseComments(context) && textSymbolsParser.CurrentChar == ',')
                                    {
                                        context.AddParseErrorItem(new ParseErrorItem(
                                            textSymbolsParser.PositionInText, () => validExpressionMissingBeforeCommaError, ParseErrorItemCode.ExpressionMissingBeforeComma));

                                        bracesExpressionItem.AddComma(textSymbolsParser.PositionInText);
                                        textSymbolsParser.SkipCharacters(1);
                                    }
                                }

                                continue;
                            }

                        case ')':
                        case ']':

                            {
                                if (keywordExpressionItems.Count > 0)
                                    AddInvalidKeywordUsageError(context, parseExpressionItemData, keywordExpressionItems,
                                        ParseErrorItemCode.InvalidUseOfKeywordsBeforeClosingBraces, ExpressionParserMessages.InvalidUseOfKeywordsBeforeClosingBracesError);

                                if (!(parseExpressionItemData.ParentExpressionItem is BracesExpressionItem bracesExpressionItem))
                                {
                                    var currentChar = textSymbolsParser.CurrentChar;
                                    context.AddParseErrorItem(new ParseErrorItem(
                                        textSymbolsParser.PositionInText, () => $"Invalid closing brace '{currentChar}'. No corresponding opening brace for this closing brace.",
                                        ParseErrorItemCode.ClosingBraceWithoutOpeningBrace));

                                    SkipInvalidSymbol(1);
                                    continue;
                                }

                                if (parsedExpressionItemsStack.Count == 0 || 
                                    parsedExpressionItemsStack.Count == 1 &&
                                    evaluateExpressionType == EvaluateExpressionType.ExpressionItemSeries)
                                {
                                    var currentChar = textSymbolsParser.CurrentChar;
                                    context.AddParseErrorItem(new ParseErrorItem(
                                        textSymbolsParser.PositionInText, () => $"Invalid closing brace '{currentChar}'.",
                                        ParseErrorItemCode.ParserImplementationError));
                                    return;
                                }

                                var expectedClosingBraceChar = bracesExpressionItem.OpeningBrace.IsRoundBrace ? ')' : ']';

                                if (textSymbolsParser.CurrentChar != expectedClosingBraceChar)
                                {
                                    context.AddParseErrorItem(new ParseErrorItem(
                                        textSymbolsParser.PositionInText, () => $"Invalid closing brace. Expected '{expectedClosingBraceChar}' to match with an opening brace.",
                                        ParseErrorItemCode.ClosingBraceDoesNotMatchOpeningBrace));

                                    SkipInvalidSymbol(1);
                                    continue;
                                }

                                GenerateExpressionItemLocal();
                                var lastExpressionItemInBraces = parseExpressionItemData.ParentExpressionItem.GetLastExpressionItem();
                                
                                if (lastExpressionItemInBraces != null && lastExpressionItemInBraces is ICommaExpressionItem)
                                {
                                    context.AddParseErrorItem(new ParseErrorItem(
                                        lastExpressionItemInBraces.IndexInText + 1,
                                        () => ExpressionParserMessages.ExpressionMissingAfterCommaError, ParseErrorItemCode.ExpressionMissingAfterComma));
                                }

                                bracesExpressionItem.SetClosingBraceInfo(
                                    new ClosingBraceExpressionItem(textSymbolsParser.CurrentChar == ')', textSymbolsParser.PositionInText));

                                textSymbolsParser.SkipCharacters(1);

                                parsedExpressionItemsStack.Pop();

                                if (parsedExpressionItemsStack.Count == 0)
                                    return;

                                parseExpressionItemData = parsedExpressionItemsStack.Peek();
                                continue;
                            }
                    }

                    if (textSymbolsParser.CurrentChar == expressionLanguageProvider.ExpressionSeparatorCharacter)
                    {
                        if (keywordExpressionItems.Count > 0)
                            AddInvalidKeywordUsageError(context, parseExpressionItemData, keywordExpressionItems,
                                ParseErrorItemCode.InvalidUseOfKeywordsBeforeCodeSeparator, ExpressionParserMessages.InvalidUseOfKeywordsBeforeCodeSeparatorError);

                        if (!(parseExpressionItemData.ParentExpressionItem is ICanAddSeparatorCharacterExpressionItem canAddSeparatorCharacterExpressionItem))
                        {
                            var currentChar = textSymbolsParser.CurrentChar;
                            context.AddParseErrorItem(new ParseErrorItem(
                                textSymbolsParser.PositionInText, () => $"Invalid usage of code separator character '{currentChar}'.",
                                ParseErrorItemCode.ExpressionSeparatorWithoutParentCodeBlock));

                            SkipInvalidSymbol(1);
                            continue;
                        }

                        GenerateExpressionItemLocal();

                        canAddSeparatorCharacterExpressionItem.AddSeparatorCharacterExpressionItem(
                            new SeparatorCharacterExpressionItem(expressionLanguageProvider.ExpressionSeparatorCharacter, textSymbolsParser.PositionInText));

                        textSymbolsParser.SkipCharacters(1);

                        // Lets skip all the commas after this comma.
                        while (Helpers.TryParseComments(context) &&
                               textSymbolsParser.CurrentChar == expressionLanguageProvider.ExpressionSeparatorCharacter)
                        {
                            context.AddParseErrorItem(new ParseErrorItem(
                                textSymbolsParser.PositionInText, () => $"Valid expression is missing before code separator '{expressionLanguageProvider.ExpressionSeparatorCharacter}'.",
                                ParseErrorItemCode.ExpressionMissingBeforeCodeItemSeparator));

                            canAddSeparatorCharacterExpressionItem.AddSeparatorCharacterExpressionItem(
                                new SeparatorCharacterExpressionItem(expressionLanguageProvider.ExpressionSeparatorCharacter, textSymbolsParser.PositionInText));
                            textSymbolsParser.SkipCharacters(1);
                        }

                        continue;
                    }

                    switch (textSymbolsParser.CurrentChar)
                    {
                        case '(':
                        case '[':
                            ILiteralExpressionItem literalExpressionItem = null;
                            IReadOnlyList<IExpressionItemBase> prefixExpressionItems = null;

                            if (lastParsedComplexExpressionItem != null)
                            {
                                if (potentialPrefixExpressionItem != null)
                                {
                                    prefixExpressionItems = _parserHelper.ConvertLastExpressionItemToPrefixes(potentialPrefixExpressionItem);
                                    parseExpressionItemData.AllExpressionItems.RemoveAt(lastParsedExpressionItemIndex);
                                }
                                else if (keywordExpressionItems.Count > 0)
                                {
                                    // TODO: Find out how this can happen.
                                    // Report an error that there is no separation between the first keyword and the last non-prefix we parsed.
                                    // The keywords will be added to current braces later.
                                    _errorsHelper.AddNoSeparationBetweenSymbolsError(context, parseExpressionItemData, keywordExpressionItems[0].IndexInText);
                                }
                                else
                                {
                                    literalExpressionItem = lastParsedComplexExpressionItem as ILiteralExpressionItem;

                                    if (literalExpressionItem != null)
                                    {
                                        if (literalExpressionItem.Prefixes.Count > 0)
                                        {
                                            prefixExpressionItems = new List<IExpressionItemBase>(literalExpressionItem.Prefixes);
                                            literalExpressionItem.RemovePrefixes();
                                        }

                                        if (literalExpressionItem.AppliedKeywords.Count > 0)
                                        {
                                            keywordExpressionItems = new List<IKeywordExpressionItem>(literalExpressionItem.AppliedKeywords);
                                            literalExpressionItem.RemoveKeywords();
                                        }

                                        // When we parsed the function/matrix name before parsing the braces, we didn't know that we will encounter braces.
                                        // Therefore, we added a literal. Now, that we know that this is a function or matrix, we replace
                                        // the last item with this braces item.
                                        parseExpressionItemData.AllExpressionItems.RemoveAt(lastParsedExpressionItemIndex);
                                    }
                                    else
                                    {
                                        _errorsHelper.AddNoSeparationBetweenSymbolsError(context, parseExpressionItemData, textSymbolsParser.PositionInText);
                                    }
                                }
                            }

                            var bracesExpressionItem = new BracesExpressionItem(prefixExpressionItems ?? new List<IExpressionItemBase>(0), keywordExpressionItems,
                                literalExpressionItem,
                                GetIsRoundBraces(textSymbolsParser), textSymbolsParser.PositionInText);

                            AddExpressionItemLocal(bracesExpressionItem);

                            parseExpressionItemData = new ParseExpressionItemData(bracesExpressionItem);
                            parsedExpressionItemsStack.Push(parseExpressionItemData);

                            textSymbolsParser.SkipCharacters(1);
                            continue;
                    }

                    if (expressionLanguageProvider.CodeBlockStartMarker != null && expressionLanguageProvider.CodeBlockEndMarker != null)
                    {
                        if (Helpers.StartsWithSymbol(textSymbolsParser.TextToParse, expressionLanguageProvider.CodeBlockEndMarker, textSymbolsParser.PositionInText,
                            textSymbolsParser.TextToParse.Length, expressionLanguageProvider, null, out var matchedText))
                        {
                            if (keywordExpressionItems.Count > 0)
                                AddInvalidKeywordUsageError(context, parseExpressionItemData, keywordExpressionItems,
                                    ParseErrorItemCode.InvalidUseOfKeywordsBeforeCodeBlockEndMarker, ExpressionParserMessages.InvalidUseOfKeywordsBeforeCodeBlockEndMarkerError);

                            // If parent item is braces, never check for early termination
                            if (!(parseExpressionItemData.ParentExpressionItem is CodeBlockExpressionItem codeBlockExpressionItem))
                            {
                                context.AddParseErrorItem(new ParseErrorItem(
                                    textSymbolsParser.PositionInText, () => $"Invalid code block closing marker '{matchedText}'. No corresponding code block start marker '{expressionLanguageProvider.CodeBlockStartMarker}'.",
                                    ParseErrorItemCode.CodeBlockClosingMarkerWithoutOpeningMarker));

                                SkipInvalidSymbol(expressionLanguageProvider.CodeBlockEndMarker.Length);
                                continue;
                            }

                            // Lets call GenerateExpressionItemLocal() now to process all items in code block.
                            GenerateExpressionItemLocal();

                            codeBlockExpressionItem.CodeBlockEndMarker = new CodeBlockEndMarkerExpressionItem(matchedText, textSymbolsParser.PositionInText);

                            //if (parsedExpressionItemsStack.Count <= 1)
                            if (parsedExpressionItemsStack.Count == 0 || parsedExpressionItemsStack.Count == 1 &&
                                evaluateExpressionType == EvaluateExpressionType.ExpressionItemSeries)
                            {
                                context.AddParseErrorItem(new ParseErrorItem(
                                    textSymbolsParser.PositionInText, () => $"Invalid code block termination symbol '{matchedText}'.",
                                    ParseErrorItemCode.ParserImplementationError, true));

                                return;
                            }

                            textSymbolsParser.SkipCharacters(matchedText.Length);
                            parsedExpressionItemsStack.Pop();

                            if (parsedExpressionItemsStack.Count == 0)
                                return;

                            parseExpressionItemData = parsedExpressionItemsStack.Peek();

                            // Code block is the last item in operators. For example if we have expression like x+y+{x:10} we evaluate expression items in
                            // operators series, as soon as we encounter '{' character, and create the code block expression. Doe not matter that at this point we didn't yet add,
                            // children to code block expression item yet.
                            GenerateExpressionItemLocal();
                            continue;
                        }

                        if (Helpers.StartsWithSymbol(textSymbolsParser.TextToParse, expressionLanguageProvider.CodeBlockStartMarker, textSymbolsParser.PositionInText,
                        textSymbolsParser.TextToParse.Length, expressionLanguageProvider, null, out matchedText))
                        {
                            if (potentialPrefixExpressionItem != null)
                            {
                                // We add prefixes to code block only if the prefix is a custom expression item and it is of type Prefix.
                                // Otherwise we will make code block a postfix to the last item

                                if (lastParsedCustomExpressionItem != null && lastParsedCustomExpressionItem.CustomExpressionItemCategory == CustomExpressionItemCategory.Prefix)
                                {
                                    parseExpressionItemData.AllExpressionItems.RemoveAt(lastParsedExpressionItemIndex);
                                }
                                else
                                {
                                    potentialPrefixExpressionItem = null;
                                }
                            }

                            var codeBlockExpressionItem =
                                new CodeBlockExpressionItem(potentialPrefixExpressionItem == null ?
                                        new List<IExpressionItemBase>(0) : _parserHelper.ConvertLastExpressionItemToPrefixes(potentialPrefixExpressionItem),
                                    keywordExpressionItems,
                                    new CodeBlockStartMarkerExpressionItem(matchedText, textSymbolsParser.PositionInText));

                            if (potentialPrefixExpressionItem != null)
                            {
                                // We add prefixes to code block only if the prefix is a custom expression item and it is of type Prefix.
                                // Otherwise we will make code block a postfix to the last item
                                AddExpressionItemLocal(codeBlockExpressionItem);
                            }
                            else if (lastParsedComplexExpressionItem != null)
                            {
                                // If we got here, we should be able to use this code block as a postfix, since there is no prefix.
                                IComplexExpressionItem codeBlockOwnerExpressionItem = null;

                                if (lastParsedCustomExpressionItem != null)
                                {
                                    if (lastParsedCustomExpressionItem.CustomExpressionItemCategory == CustomExpressionItemCategory.Regular)
                                    {
                                        if (lastParsedCustomExpressionItem.IsValidPostfix(codeBlockExpressionItem,
                                            out var invalidPostfixErrorMessage))
                                        {
                                            codeBlockOwnerExpressionItem = lastParsedCustomExpressionItem;
                                        }
                                        else
                                        {
                                            context.AddParseErrorItem(new ParseErrorItem(
                                                codeBlockExpressionItem.IndexInText, () => invalidPostfixErrorMessage ??
                                                                                           ExpressionParserMessages.CodeBlockCannotFollowThePrecedingExpressionError,
                                                ParseErrorItemCode.CodeBlockRejectedByOwnerCustomExpression));
                                        }
                                    }
                                    else
                                    {
                                        parseExpressionItemData.OperatorsCannotBeEvaluated = true;
                                        context.AddParseErrorItem(new ParseErrorItem(
                                            codeBlockExpressionItem.IndexInText, () =>
                                                ExpressionParserMessages.CodeBlockCannotFollowThePrecedingExpressionError,
                                            ParseErrorItemCode.CodeBlockUsedAfterPostfixCustomExpression));
                                    }
                                }
                                else
                                {
                                    if (lastParsedComplexExpressionItem is ILiteralExpressionItem ||
                                        lastParsedComplexExpressionItem is IBracesExpressionItem)
                                    {
                                        codeBlockOwnerExpressionItem = lastParsedComplexExpressionItem;
                                    }
                                    else
                                    {
                                        parseExpressionItemData.OperatorsCannotBeEvaluated = true;
                                        context.AddParseErrorItem(new ParseErrorItem(
                                            codeBlockExpressionItem.IndexInText, () =>
                                                ExpressionParserMessages.CodeBlockCannotFollowThePrecedingExpressionError,
                                            ParseErrorItemCode.InvalidCodeBlock));
                                    }
                                }

                                if (codeBlockOwnerExpressionItem != null)
                                {
                                    codeBlockOwnerExpressionItem.AddPostfix(codeBlockExpressionItem);
                                }
                                else
                                {
                                    AddExpressionItemLocal(codeBlockExpressionItem);
                                }
                            }
                            else
                            {
                                AddExpressionItemLocal(codeBlockExpressionItem);
                            }

                            parseExpressionItemData = new ParseExpressionItemData(codeBlockExpressionItem);
                            parsedExpressionItemsStack.Push(parseExpressionItemData);
                            textSymbolsParser.SkipCharacters(matchedText.Length);

                            continue;
                        }
                    }


                    if ((parseExpressionItemData.ParsedUnprocessedOperatorInfoItems?.Count ?? 0) == 0)
                    {
                        var hasOperand1 = lastParsedComplexExpressionItem != null &&
                                           (lastParsedCustomExpressionItem == null ||
                                            lastParsedCustomExpressionItem.CustomExpressionItemCategory == CustomExpressionItemCategory.Regular);

                        var parsedOperators = _parseOperatorsHelper.TryParseOperatorsAndKeywords(context, sortedCandidateOperators, hasOperand1, 
                            IgnoreOperatorInfoDelegate, out keywordsParsedAfterOperators);

                        var hasParsedOperators = parsedOperators != null && parsedOperators.Count > 0;

                        if (hasParsedOperators || (keywordsParsedAfterOperators?.Count ?? 0) > 0)
                        {
                            if (hasParsedOperators)
                            {
                                parseExpressionItemData.ParsedUnprocessedOperatorInfoItems = parsedOperators;

                                if (keywordExpressionItems.Count > 0)
                                    AddInvalidKeywordUsageError(context, parseExpressionItemData, keywordExpressionItems, ParseErrorItemCode.InvalidUseOfKeywordsBeforeOperators,
                                        ExpressionParserMessages.InvalidUseOfKeywordsBeforeOperatorsError);
                            }

                            continue;
                        }
                    }

                    // Everything after this point is non-operator 

                    // After this point we should be able to parse either a literal, constant, or a custom expression item.
                    // If there is a non-operator and non-keyword expression preceding this symbol, we expect this expression to be 
                    // a prefix. Report an error and return, if the previous expression is not a prefix.
                    var hasNoSeparationBetweenSymbolsError = lastParsedComplexExpressionItem != null && potentialPrefixExpressionItem == null;

                    // Try to read literal, such as variable name or function name.
                    var indexInText = textSymbolsParser.PositionInText;
                    if (textSymbolsParser.ReadSymbol(context.ExpressionLanguageProviderWrapper.ExpressionLanguageProvider.IsValidLiteralCharacter))
                    {
                        if (hasNoSeparationBetweenSymbolsError)
                            _errorsHelper.AddNoSeparationBetweenSymbolsError(context, parseExpressionItemData, indexInText);

                        if (potentialPrefixExpressionItem != null)
                            parseExpressionItemData.AllExpressionItems.RemoveAt(parseExpressionItemData.AllExpressionItems.Count - 1);


                        AddExpressionItemLocal(new LiteralExpressionItem(_parserHelper.ConvertLastExpressionItemToPrefixes(potentialPrefixExpressionItem),
                            keywordExpressionItems,
                            new LiteralNameExpressionItem(textSymbolsParser.LastReadSymbol, indexInText)));
                    }
                    else
                    {
                        // If the parser didn't parse the first character, lets see if the symbols we are trying to parse is a constant text or a number.
                        if (textSymbolsParser.PositionInText == indexInText)
                        {
                            IComplexExpressionItem constantValueExpressionItem = _parseConstantTextHelper.TryParseConstantTextValueExpressionItem(context, potentialPrefixExpressionItem,
                                keywordExpressionItems);

                            if (constantValueExpressionItem == null)
                                constantValueExpressionItem = _parseConstantNumericValueHelper.TryParseConstantNumericValueExpressionItem(context, potentialPrefixExpressionItem,
                                    keywordExpressionItems);

                            if (constantValueExpressionItem != null)
                            {
                                if (hasNoSeparationBetweenSymbolsError)
                                    _errorsHelper.AddNoSeparationBetweenSymbolsError(context, parseExpressionItemData, indexInText);

                                if (potentialPrefixExpressionItem != null)
                                    parseExpressionItemData.AllExpressionItems.RemoveAt(lastParsedExpressionItemIndex);

                                AddExpressionItemLocal(constantValueExpressionItem);
                                continue;
                            }
                        }

                        if (keywordExpressionItems.Count > 0)
                            AddInvalidKeywordUsageError(context, parseExpressionItemData, keywordExpressionItems);

                        if (lastInvalidSymbolIndex < 0 || lastInvalidSymbolIndex + 1 != textSymbolsParser.PositionInText)
                        {
                            lastInvalidSymbolIndex = textSymbolsParser.PositionInText;
                            parseExpressionItemData.OperatorsCannotBeEvaluated = true;

                            // If we could not parse a literal, number or text, lets add an invalid symbol error.
                            context.AddParseErrorItem(new ParseErrorItem(
                                textSymbolsParser.PositionInText, () => ExpressionParserMessages.InvalidSymbolError, ParseErrorItemCode.InvalidSymbol));

                            GenerateExpressionItemLocal();
                        }
                        else
                        {
                            lastInvalidSymbolIndex = textSymbolsParser.PositionInText;
                        }

                        SkipInvalidSymbol(1);
                    }
                }
            }
            finally
            {
                // Lets process any unprocessed items before returning.

                while (parsedExpressionItemsStack.Count > 0)
                {
                    parseExpressionItemData = parsedExpressionItemsStack.Pop();

                    GenerateExpressionItemLocal();

                    //If there are no critical errors, add errors for all unclosed braces / code blocks.
                    if (!context.ParseErrorData.HasCriticalErrors)
                    {
                        if (parseExpressionItemData.ParentExpressionItem is IBracesExpressionItem bracesExpressionItem)
                        {
                            if (bracesExpressionItem.ClosingBrace == null)
                            {
                                var lastExpressionItem = bracesExpressionItem.GetLastExpressionItem();

                                if (lastExpressionItem is ICommaExpressionItem)
                                    context.AddParseErrorItem(new ParseErrorItem(
                                        lastExpressionItem.IndexInText + 1, 
                                        () => ExpressionParserMessages.ExpressionMissingAfterCommaError, ParseErrorItemCode.ExpressionMissingAfterComma));

                                var expectedClosingBraceChar = bracesExpressionItem.OpeningBrace.IsRoundBrace ? ')' : ']';
                                context.AddParseErrorItem(new ParseErrorItem(
                                    bracesExpressionItem.OpeningBrace.IndexInText, () => $"Closing brace '{expectedClosingBraceChar}' is missing.",
                                    ParseErrorItemCode.ClosingBraceMissing));
                            }
                        }
                        else if (parseExpressionItemData.ParentExpressionItem is ICodeBlockExpressionItem codeBlockExpressionItem)
                        {
                            context.AddParseErrorItem(new ParseErrorItem(
                                codeBlockExpressionItem.CodeBlockStartMarker.IndexInText,
                                () => $"Code block end marker '{expressionLanguageProvider.CodeBlockEndMarker}' is missing for '{expressionLanguageProvider.CodeBlockStartMarker}'.",
                                ParseErrorItemCode.CodeBlockEndMarkerMissing));
                        }
                    }
                }
            }
        }
    }
}