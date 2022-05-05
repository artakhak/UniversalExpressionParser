// Copyright (c) UniversalExpressionParser Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using System;
using JetBrains.Annotations;

namespace UniversalExpressionParser
{
    /// <summary>
    /// A cache for <see cref="IExpressionLanguageProviderWrapper"/> instances.
    /// One instance per language name is cached.
    /// </summary>
    public interface IExpressionLanguageProviderCache
    {
        /// <summary>
        /// Adds the expression language provider <paramref name="expressionLanguageProvider"/>. After the provider is registered,
        /// call to <see cref="IExpressionLanguageProviderCache.GetExpressionLanguageProviderWrapper(string)"/> can be made to
        /// get a cached instance of <paramref name="expressionLanguageProvider"/>.
        /// Note, the call to this method also validates the <paramref name="expressionLanguageProvider"/> using some
        /// validator (say <see cref="IExpressionLanguageProviderValidator"/>), and an exception is thrown if the validation fails.
        /// </summary>
        /// <exception cref="ArgumentException">Throws this <paramref name="expressionLanguageProvider"/> was already registered.</exception>
        /// <exception cref="ExpressionLanguageProviderException">Throws this exception if validation of <paramref name="expressionLanguageProvider"/> fails.</exception>
        /// <exception cref="Exception">Throws this exception.</exception>
        void RegisterExpressionLanguageProvider([NotNull] IExpressionLanguageProvider expressionLanguageProvider);

        /// <summary>
        /// Removes the expression language provider with <see cref="IExpressionLanguageProvider.LanguageName"/> equal to
        /// <paramref name="expressionLanguageProviderName"/> from the cache.
        /// This method can be called if another instance of expression language provider should be registered
        /// which has a value of <see cref="IExpressionLanguageProvider.LanguageName"/> that is already registered. 
        /// </summary>
        /// <param name="expressionLanguageProviderName">Name of the expression language provider to un-register.</param>
        void UnRegisterExpressionLanguageProvider([NotNull] String expressionLanguageProviderName);

        /// <summary>
        /// Removes all expression language providers from cache.
        /// </summary>
        void UnRegisterAllExpressionLanguageProviders();

        /// <summary>
        /// Returns cached instance of <see cref="IExpressionLanguageProviderWrapper"/> if expression language provider of instance <see cref="IExpressionLanguageProvider"/>
        /// with <see cref="IExpressionLanguageProvider.LanguageName"/> equal to <paramref name="expressionLanguageProviderName"/> wa previously registered
        /// using a call to <see cref="RegisterExpressionLanguageProvider(IExpressionLanguageProvider)"/>. Note: expression language provider names are case sensitive.
        /// Returns null otherwise.
        /// </summary>
        /// <param name="expressionLanguageProviderName">Expression language provider name of previously registered expression language provider to retrieve.
        /// </param>
        [CanBeNull]
        IExpressionLanguageProviderWrapper GetExpressionLanguageProviderWrapper([NotNull] String expressionLanguageProviderName);
    }

    /// <summary>
    /// Extension methods for <see cref="IExpressionLanguageProviderCache"/>.
    /// </summary>
    public static class ExpressionLanguageProviderCacheExtensions
    {
        /// <summary>
        /// Returns cached instance of <see cref="IExpressionLanguageProviderWrapper"/> if expression language provider of instance <see cref="IExpressionLanguageProvider"/>
        /// with <see cref="IExpressionLanguageProvider.LanguageName"/> equal to <paramref name="expressionLanguageProviderName"/> wa previously registered
        /// using a call to <see cref="IExpressionLanguageProviderCache.RegisterExpressionLanguageProvider(IExpressionLanguageProvider)"/>. Note: expression language provider names are case sensitive.
        /// Throws an exception otherwise.
        /// </summary>
        /// <param name="expressionLanguageProviderCache">An instance of <see cref="IExpressionLanguageProviderCache"/>.</param>
        /// <param name="expressionLanguageProviderName">Expression language provider name of previously registered expression language provider to retrieve.
        /// </param>
        /// <exception cref="ArgumentException">Throws this exception if <paramref name="expressionLanguageProviderName"/> was not previously registered
        /// by calling <see cref="IExpressionLanguageProviderCache.RegisterExpressionLanguageProvider(IExpressionLanguageProvider)"/> (or if the prior registration was removed).
        /// </exception>
        [NotNull]
        public static IExpressionLanguageProviderWrapper GetExpressionLanguageProviderWrapperOrThrow(
            [NotNull] this IExpressionLanguageProviderCache expressionLanguageProviderCache, [NotNull] String expressionLanguageProviderName)
        {
            var expressionLanguageProviderWrapper = expressionLanguageProviderCache.GetExpressionLanguageProviderWrapper(expressionLanguageProviderName);

            if (expressionLanguageProviderWrapper != null)
                return expressionLanguageProviderWrapper;

            throw new ArgumentException(
                        $"An expression language provider with {nameof(IExpressionLanguageProvider.LanguageName)}' was not registered. Please call {nameof(IExpressionLanguageProviderCache.RegisterExpressionLanguageProvider)}({nameof(IExpressionLanguageProvider)}) first before calling the method.");

        }
    }
}