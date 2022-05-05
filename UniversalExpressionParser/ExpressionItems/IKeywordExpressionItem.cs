// Copyright (c) UniversalExpressionParser Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using JetBrains.Annotations;

namespace UniversalExpressionParser.ExpressionItems
{
    /// <summary>
    /// Expression item parsed from keywords. For example if <see cref="IExpressionLanguageProvider.Keywords"/> contains a keyword for "var", then in "var x=10;"
    /// "var" will be parsed to <see cref="IKeywordExpressionItem"/>.
    /// </summary>
    public interface IKeywordExpressionItem : ITextExpressionItem
    {
        /// <summary>
        /// Keyword info.
        /// </summary>
        [NotNull]
        ILanguageKeywordInfo LanguageKeywordInfo { get; }
    }
}