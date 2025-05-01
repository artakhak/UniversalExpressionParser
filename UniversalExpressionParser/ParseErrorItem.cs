// Copyright (c) UniversalExpressionParser Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using System;
using JetBrains.Annotations;
using UniversalExpressionParser.ExpressionItems.Custom;

namespace UniversalExpressionParser
{
    /// <inheritdoc />
    public class ParseErrorItem: IParseErrorItem
    {
        [CanBeNull]
        private string _errorMessage;

        [NotNull] 
        private readonly Func<string> _getErrorMessage;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="errorIndexInParsedText">Error index in evaluated text.</param>
        /// <param name="getErrorMessage">A function that returns non-null and non-empty error message.</param>
        /// <param name="parseErrorItemCode">Parse error code. Look at <see cref="UniversalExpressionParser.ParseErrorItemCode"/> for predefined error codes.
        /// Other custom values can be used as well.
        /// </param>
        public ParseErrorItem(int errorIndexInParsedText, [NotNull] Func<string> getErrorMessage, int parseErrorItemCode) : 
            this(errorIndexInParsedText, getErrorMessage, parseErrorItemCode, false)
        {
            _getErrorMessage = getErrorMessage;
            ErrorIndexInParsedText = errorIndexInParsedText;
            ParseErrorItemCode = parseErrorItemCode;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="errorIndexInParsedText">Error index in evaluated text.</param>
        /// <param name="getErrorMessage">A function that returns non-null and non-empty error message.</param>
        /// <param name="parseErrorItemCode">Parse error code. Look at <see cref="UniversalExpressionParser.ParseErrorItemCode"/> for predefined error codes.
        /// Other custom values can be used as well.
        /// </param>
        /// <param name="isCriticalError">
        /// If the value, parsing will stop after this error is added by the custom expression parser <see cref="ICustomExpressionItemParser"/>,
        /// parsing will stop on error and rest of the expression will not be parsed ny <see cref="IExpressionParser"/>.
        /// Note, the same error code might be considered as critical error in one context, and non-critical in some other context.
        /// </param>
        public ParseErrorItem(int errorIndexInParsedText, [NotNull] Func<string> getErrorMessage, int parseErrorItemCode, bool isCriticalError)
        {
            _getErrorMessage = getErrorMessage;
            ErrorIndexInParsedText = errorIndexInParsedText;
            ParseErrorItemCode = parseErrorItemCode;
            IsCriticalError = isCriticalError;
        }

        /// <inheritdoc />
        public int ErrorIndexInParsedText { get; }

        /// <inheritdoc />
        public string ErrorMessage
        {
            get
            {
                if (_errorMessage == null)
                    _errorMessage = _getErrorMessage();

                return _errorMessage??string.Empty;
            }
        }

        /// <inheritdoc />
        public int ParseErrorItemCode { get; }

        /// <inheritdoc />
        public bool IsCriticalError { get; }
    }
}
