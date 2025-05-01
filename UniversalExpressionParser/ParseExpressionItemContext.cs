// Copyright (c) UniversalExpressionParser Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using System.Collections.Generic;
using JetBrains.Annotations;
using TextParser;
using UniversalExpressionParser.ExpressionItems;

namespace UniversalExpressionParser
{
    internal delegate bool IgnoreOperatorInfoDelegate([NotNull] IOperatorInfoExpressionItem operatorInfoExpressionItem);
    internal delegate bool IgnoreBinaryOperatorWhenBreakingPostfixPrefixTieDelegate([NotNull] IOperatorInfoExpressionItem operatorInfoExpressionItem);

    internal class ParseExpressionItemContext : IParseExpressionItemContext
    {
        [NotNull]
        private readonly ExpressionParser _expressionParser;

        [CanBeNull]
        private readonly IsParsingCompleteDelegate _checkIsExpressionParsingComplete;

        [NotNull]
        private readonly ParseErrorData _parseErrorData;

        [NotNull, ItemNotNull]
        private readonly List<IParseErrorItem> _newlyAddedErrors = new List<IParseErrorItem>();

        public ParseExpressionItemContext([NotNull] IParseExpressionResult parseExpressionResult, [NotNull] ParseErrorData parseErrorData,
                                          [NotNull] ITextSymbolsParser textSymbolsParser,
                                          [NotNull] IExpressionLanguageProviderWrapper expressionLanguageProviderWrapper,
                                          [NotNull] ExpressionParser expressionParser,
                                          [CanBeNull] IsParsingCompleteDelegate checkIsExpressionParsingComplete)
        {
            ParseExpressionResult = parseExpressionResult;
            _parseErrorData = parseErrorData;
            TextSymbolsParser = textSymbolsParser;
            ExpressionLanguageProviderWrapper = expressionLanguageProviderWrapper;
            _expressionParser = expressionParser;
            _checkIsExpressionParsingComplete = checkIsExpressionParsingComplete;
        }

        public IReadOnlyList<IParseErrorItem> NewlyAddedErrors => _newlyAddedErrors;

        public void ClearNewlyAddedErrors() => _newlyAddedErrors.Clear();

        /// <inheritdoc />
        public event ParseErrorAddedDelegate ParseErrorAddedEvent;

        /// <inheritdoc />
        public IExpressionLanguageProviderWrapper ExpressionLanguageProviderWrapper { get; }

        /// <inheritdoc />
        public IParseExpressionResult ParseExpressionResult { get; }

        /// <inheritdoc />
        public IParseErrorData ParseErrorData => _parseErrorData;

        /// <inheritdoc />
        public ITextSymbolsParser TextSymbolsParser { get; }

        /// <inheritdoc />
        public bool IsEarlyParseStopEncountered { get; private set; }

        /// <inheritdoc />
        public IBracesExpressionItem ParseBracesExpression(ILiteralExpressionItem literalExpressionItem)
        {
            return _expressionParser.ParseBracesExpression(this, literalExpressionItem);
        }

        /// <inheritdoc />
        public ICodeBlockExpressionItem ParseCodeBlockExpression()
        {
            return _expressionParser.ParseCodeBlockExpression(this);
        }

        /// <inheritdoc />
        public bool SkipSpacesAndComments()
        {
            return Helpers.TryParseComments(this);
        }

        /// <inheritdoc />
        public bool TryParseSymbol(out string parsedLiteral)
        {
            parsedLiteral = null;
            if (!TextSymbolsParser.ReadSymbol(ExpressionLanguageProviderWrapper.ExpressionLanguageProvider.IsValidLiteralCharacter))
                return false;

            parsedLiteral = this.TextSymbolsParser.LastReadSymbol;
            return true;
        }

        /// <inheritdoc />
        public void AddParseErrorItem(IParseErrorItem parseErrorItem)
        {
            if (parseErrorItem.IsCriticalError)
                _parseErrorData.HasCriticalErrors = true;

            _parseErrorData.AddParseErrorItem(parseErrorItem);

            _newlyAddedErrors.Add(parseErrorItem);
            ParseErrorAddedEvent?.Invoke(this, new ParseErrorAddedEventArgs(parseErrorItem));
        }

        /// <summary>
        /// TODO: Improve the doc.
        /// Checks if parsing is complete. The value can be null.
        /// Null value is similar to setting this value to a function that always returns false.
        /// The check is done only when one of characters ')', ']', '}', ';', or ',' is encountered, and
        /// the value of ParentExpressionItem (no ParentExpressionItem anymore) is the same as <see cref="ParseExpressionResult"/> (in other words no function, matrix or
        /// code block is currently being parsed).
        /// </summary>
        internal bool CheckIsParsingComplete()
        {
            if (_checkIsExpressionParsingComplete != null)
            {
                var textSymbolsParser = TextSymbolsParser;

                if (!TextSymbolsParser.IsEndOfTextReached &&
                    _checkIsExpressionParsingComplete(this, textSymbolsParser.CurrentChar, textSymbolsParser.PositionInText))
                {
                    IsEarlyParseStopEncountered = true;
                    return true;
                }
            }

            return false;
        }

        internal void SetHasCriticalErrors()
        {
            _parseErrorData.HasCriticalErrors = true;
        }
    }
}
