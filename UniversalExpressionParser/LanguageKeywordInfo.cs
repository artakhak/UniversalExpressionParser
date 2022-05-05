// Copyright (c) UniversalExpressionParser Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using JetBrains.Annotations;

namespace UniversalExpressionParser
{
    // Documented
    /// <inheritdoc />
    public class LanguageKeywordInfo: ILanguageKeywordInfo
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="id">Unique Id that identifies the keyword.</param>
        /// <param name="keyword">Keyword text.</param>
        public LanguageKeywordInfo(long id, [NotNull] string keyword)
        {
            Id = id;
            Keyword = keyword;
        }

        /// <inheritdoc />
        public long Id { get; }

        /// <inheritdoc />
        public string Keyword { get; }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{nameof(Keyword)}={Keyword}, {nameof(Id)}={Id}, {this.GetType().FullName}";
        }
    }
}