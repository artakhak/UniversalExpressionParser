// Copyright (c) UniversalExpressionParser Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using System.Collections.Generic;
using JetBrains.Annotations;

namespace UniversalExpressionParser.ExpressionItems
{
    /// <summary>
    /// Default implementation of <see cref="IConstantTextExpressionItem"/>
    /// </summary>
    public class ConstantTextExpressionItem : ComplexExpressionItemBase, IConstantTextExpressionItem
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="prefixExpressionItems">
        /// Prefix expression items. For example in "[Attribute1][Attribute2] 'Some text'",
        /// expressions "[Attribute1]" and "[Attribute2]" will be parsed as prefixes and added to <see cref="IComplexExpressionItem.Prefixes"/> in
        /// <see cref="IConstantTextExpressionItem"/> parsed from "[Attribute1][Attribute2] 'Some text'".
        /// </param>
        /// <param name="keywordExpressionItems">Applied keyword expression items.
        /// For example in "keyword1 'Some text'", "keyword1" will be parsed to a keyword and added to <see cref="IComplexExpressionItem.AppliedKeywords"/> in
        /// <see cref="IConstantTextExpressionItem"/> (provided "keyword1" is in <see cref="IExpressionLanguageProvider"/>), and "'Some text'" will be parsed to
        /// <see cref="IConstantTextValueExpressionItem"/> and stored in <see cref="IConstantTextExpressionItem.TextValue"/> property.
        /// </param>
        /// <param name="constantTextValueExpressionItem">Text that includes also apostrophes. Examples stored in <see cref="TextValue"/> are "This is will be parsed to text" or 'This is will be parsed to text'.
        /// </param>
        public ConstantTextExpressionItem([NotNull][ItemNotNull] IEnumerable<IExpressionItemBase> prefixExpressionItems, 
            [NotNull][ItemNotNull] IEnumerable<IKeywordExpressionItem> keywordExpressionItems,
            [NotNull] IConstantTextValueExpressionItem constantTextValueExpressionItem) : base(prefixExpressionItems, keywordExpressionItems)
        {
            TextValue = constantTextValueExpressionItem;
            AddRegularItem(TextValue);
        }

        /// <inheritdoc />
        public IConstantTextValueExpressionItem TextValue { get; }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{this.GetType().FullName}, {nameof(IndexInText)}:{IndexInText}, {nameof(TextValue)}:'{TextValue.Text}'";
        }
    }
}