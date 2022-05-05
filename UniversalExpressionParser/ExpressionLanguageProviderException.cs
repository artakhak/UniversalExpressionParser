// Copyright (c) UniversalExpressionParser Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using System;
using JetBrains.Annotations;

namespace UniversalExpressionParser
{
    // Documented.
    /// <summary>
    /// Expression language provider validation exception.
    /// </summary>
    public class ExpressionLanguageProviderException: Exception
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public ExpressionLanguageProviderException([NotNull] IExpressionLanguageProvider expressionLanguageProvider, [NotNull] string message) : 
            base($"Validation of \"{typeof(IExpressionLanguageProvider).FullName}\" failed.{Environment.NewLine}Implementation class is \"{expressionLanguageProvider.GetType().FullName}\".{Environment.NewLine}Validation error:  {message}" )
        {
            ExpressionLanguageProvider = expressionLanguageProvider;
        }

        /// <summary>
        /// The expression language provider that failed the validation.
        /// </summary>
        [NotNull]
        public IExpressionLanguageProvider ExpressionLanguageProvider { get; }
    }
}