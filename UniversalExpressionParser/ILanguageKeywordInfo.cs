// Copyright (c) UniversalExpressionParser Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using JetBrains.Annotations;
using UniversalExpressionParser.ExpressionItems;

namespace UniversalExpressionParser
{
    // Documented
    /// <summary>
    /// Language specific keyword info. Examples of keywords are "public", "class", "namespace", "ref", "out", "_".
    /// Keywords are special names (e.g., var, public, class, where) that can be specified in property <see cref="IComplexExpressionItem.AppliedKeywords"/>,
    /// as shown in example below:
    /// public class Dog; // In this example "public" and "class" are keywords that are added to an expression item of type <see cref="ILiteralExpressionItem"/> parsed from expression "Dog".
    /// </summary>
    public interface ILanguageKeywordInfo
    {
        /// <summary>
        /// Unique Id that identifies the keyword.
        /// </summary>
        long Id { get; } 

        /// <summary>
        /// Keyword text.
        /// </summary>
        [NotNull]
        string Keyword { get; }
    }
}