// Copyright (c) UniversalExpressionParser Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using JetBrains.Annotations;

namespace UniversalExpressionParser.ExpressionItems
{
    /// <summary>
    /// Default implementation of <see cref="IKeywordExpressionItem"/>.
    /// </summary>
    public class KeywordExpressionItem: NameExpressionItem, IKeywordExpressionItem
    {
        /// <inheritdoc />
        public KeywordExpressionItem([NotNull] ILanguageKeywordInfo languageKeywordInfo, int indexInText) : 
            this(languageKeywordInfo, languageKeywordInfo.Keyword, indexInText)
        {
            LanguageKeywordInfo = languageKeywordInfo;
        }

        /// <inheritdoc />
        public KeywordExpressionItem([NotNull] ILanguageKeywordInfo languageKeywordInfo, string keywordText, int indexInText) : base(keywordText, indexInText)
        {
            LanguageKeywordInfo = languageKeywordInfo;
        }

        /// <inheritdoc />
        public ILanguageKeywordInfo LanguageKeywordInfo { get; }
    }
}