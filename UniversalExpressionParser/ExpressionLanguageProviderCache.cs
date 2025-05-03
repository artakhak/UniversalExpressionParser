// Copyright (c) UniversalExpressionParser Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;

namespace UniversalExpressionParser
{
    /// <inheritdoc />
    public class ExpressionLanguageProviderCache: IExpressionLanguageProviderCache
    {
        [NotNull]
        private readonly IExpressionLanguageProviderValidator _expressionLanguageProviderValidator;

        [NotNull]
        private readonly Dictionary<string, IExpressionLanguageProviderWrapper> _languageNameToLanguageProviderMap =
            new Dictionary<string, IExpressionLanguageProviderWrapper>(StringComparer.OrdinalIgnoreCase);

        private readonly object _lockObject = new object();

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="expressionLanguageProviderValidator">A validator for <see cref="IExpressionLanguageProvider"/> objects.
        /// An instance of <see cref="DefaultExpressionLanguageProviderValidator"/> can be used in most cases.
        /// </param>
        public ExpressionLanguageProviderCache([NotNull] IExpressionLanguageProviderValidator expressionLanguageProviderValidator)
        {
            _expressionLanguageProviderValidator = expressionLanguageProviderValidator;
        }

        /// <inheritdoc />
        public void RegisterExpressionLanguageProvider(IExpressionLanguageProvider expressionLanguageProvider)
        {
            lock (_lockObject)
            {
                if (_languageNameToLanguageProviderMap.TryGetValue(expressionLanguageProvider.LanguageName, out var expressionLanguageProviderWrapper))
                {
                    var errorMessage = new StringBuilder();
                    errorMessage.AppendLine($"Failed to register expression {expressionLanguageProvider} of type {expressionLanguageProviderWrapper.GetType().FullName} with {nameof(IExpressionLanguageProvider.LanguageName)}={expressionLanguageProviderWrapper.ExpressionLanguageProvider.LanguageName}.");
                    errorMessage.AppendLine($"Expression language provider with similar was already registered. Call {nameof(UnRegisterExpressionLanguageProvider)}({expressionLanguageProvider.LanguageName}) first before trying to register the provider again.");
                    errorMessage.AppendLine($"Previously registered expression language provider: {expressionLanguageProviderWrapper}.");

                    throw new ArgumentException(errorMessage.ToString());
                }

                _expressionLanguageProviderValidator.Validate(expressionLanguageProvider);

                _languageNameToLanguageProviderMap[expressionLanguageProvider.LanguageName] = new ExpressionLanguageProviderWrapper(expressionLanguageProvider);
            }
        }

        /// <inheritdoc />
        public void UnRegisterExpressionLanguageProvider(string expressionLanguageProviderName)
        {
            lock(_lockObject)
                _languageNameToLanguageProviderMap.Remove(expressionLanguageProviderName);
        }

        /// <inheritdoc />
        public IExpressionLanguageProviderWrapper GetExpressionLanguageProviderWrapper(string expressionLanguageProviderName)
        {
            lock (_lockObject)
                return _languageNameToLanguageProviderMap.TryGetValue(expressionLanguageProviderName, out var expressionLanguageProviderWrapper) ? 
                    expressionLanguageProviderWrapper : null;
        }

        /// <inheritdoc />
        public void UnRegisterAllExpressionLanguageProviders()
        {
            lock (_lockObject)
                _languageNameToLanguageProviderMap.Clear();
        }
    }
}
