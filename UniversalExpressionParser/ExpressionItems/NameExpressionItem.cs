// Copyright (c) UniversalExpressionParser Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using System;
using JetBrains.Annotations;

namespace UniversalExpressionParser.ExpressionItems
{
    /// <summary>
    /// Simple expression item for names. Normally this expression item is used for storing data
    /// for expression items that are parts of ome other complex expression item <see cref="IComplexExpressionItem"/>.
    /// Examples are expressions "func1", "(", "x", ",", "y", and ")" in func1(x, y), etc.
    /// </summary>
    public class NameExpressionItem : ExpressionItemBase, ITextExpressionItem
    {
        /// <inheritdoc />
        public NameExpressionItem([NotNull] string name, int indexInText)
        {
            if (name.Length == 0)
                throw new ArgumentException($"The value of '{nameof(name)}' cannot be empty.", nameof(name));

            if (char.IsWhiteSpace(name[0]))
                throw new ArgumentException($"The value of '{nameof(name)}' cannot start with white space character.", nameof(name));

            if (name.Length > 1 && char.IsWhiteSpace(name[name.Length - 1]))
                throw new ArgumentException($"The value of '{nameof(name)}' cannot end with white space character.", nameof(name));

            Text = name;
            IndexInText = indexInText;
        }

        /// <inheritdoc />
        public override int IndexInText { get; }

        /// <inheritdoc />
        public override int ItemLength => Text.Length;

        /// <inheritdoc />
        public string Text { get; }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{this.GetType().FullName}, {nameof(IndexInText)}:{IndexInText}, {nameof(Text)}:{Text}";
        }
    }
}
