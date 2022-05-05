// Copyright (c) UniversalExpressionParser Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using JetBrains.Annotations;

namespace UniversalExpressionParser
{
    // Documented
    /// <summary>
    /// A validator for <see cref="IExpressionLanguageProvider"/>.
    /// The default implementation <see cref="DefaultExpressionLanguageProviderValidator"/> can be used in most cases.
    /// </summary>
    public interface IExpressionLanguageProviderValidator
    {
        /// <summary>
        /// Validates the <see cref="IExpressionLanguageProvider"/> in parameter <paramref name="expressionLanguageProvider"/>.
        /// Throws an exception <see cref="ExpressionLanguageProviderException"/> if validation fails.
        /// This method might be executed by <see cref="IExpressionLanguageProviderCache.RegisterExpressionLanguageProvider(IExpressionLanguageProvider)"/> when registering
        /// expression language providers that can be used for parsing expressions using <see cref="IExpressionParser"/>.
        /// </summary>
        /// <param name="expressionLanguageProvider">Expression language provider to validate.</param>
        /// <exception cref="ExpressionLanguageProviderException">Throws this exception if validation fails.</exception>
        void Validate([NotNull] IExpressionLanguageProvider expressionLanguageProvider);

        ///// <summary>
        ///// If the value is true, operator name part can be contained in keyword.
        ///// For example consider operator ":" and keyword "::types" in validated <see cref="IExpressionLanguageProvider"/>.
        ///// If the value of the setting <see cref="OperatorCanBeContainedInKeywordText"/> is true,
        ///// the validation will not fail because of overlap of operator and keyword texts. Otherwise, the validation will fail.
        ///// The opposite case when keyword is part of operator (say keyword where and operator keyword++) will always fail the validation.
        ///// </summary>
        //bool OperatorCanBeContainedInKeywordText { get; }
    }
}