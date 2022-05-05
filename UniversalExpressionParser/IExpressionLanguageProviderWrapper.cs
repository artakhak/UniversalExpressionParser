// Copyright (c) UniversalExpressionParser Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using JetBrains.Annotations;
using System;
using System.Collections.Generic;

namespace UniversalExpressionParser
{
    /// <summary>
    /// A wrapper for <see cref="IExpressionLanguageProvider"/> which provides some additional functionality.
    /// </summary>
    public interface IExpressionLanguageProviderWrapper
    {
        /// <summary>
        /// Wrapped expression language provider.
        /// </summary>
        [NotNull]
        IExpressionLanguageProvider ExpressionLanguageProvider { get; }

        /// <summary>
        /// Keywords sorted by priority.
        /// When parsing expression the parser <see cref="IExpressionParser"/> will give higher priority to keywords that appear in this list first.
        /// For example consider an expression "where:end ..." lets say we have keywords "where" and "where:end" (normally keywords are not supposed to use operator symbols).
        /// In this case, if "where:end" appears before "where" in <see cref="SortedKeywordInfos"/>, then the parser will parse "where:end" to a keyword
        /// with <see cref="ILanguageKeywordInfo.Keyword"/> equal to "where:end". Otherwise, the parser will parse "where" in "where:end" to keywords with
        /// <see cref="ILanguageKeywordInfo.Keyword"/> equal to "where" and will try to parse ":end" to some other expression items. 
        /// </summary>
        [NotNull, ItemNotNull]
        IReadOnlyList<ILanguageKeywordInfo> SortedKeywordInfos { get; }

        /// <summary>
        /// Gets keyword for which <see cref="ILanguageKeywordInfo.Id"/> is <paramref name="keywordId"/>.
        /// </summary>
        /// <param name="keywordId">Keyword Id.</param>
        /// <exception cref="ArgumentException">Throws this exception if keyword was not found.</exception>
        [NotNull]
        ILanguageKeywordInfo GetKeywordInfo(long keywordId);

        /// <summary>
        /// Gets operator for which <see cref="IOperatorInfo.Id"/> is <paramref name="operatorId"/>.
        /// </summary>
        /// <param name="operatorId">Operator Id.</param>
        /// <exception cref="ArgumentException">Throws this exception if operator was not found.</exception>
        [NotNull]
        IOperatorInfo GetOperatorInfo(long operatorId);
    }
}