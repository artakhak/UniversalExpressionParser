// Copyright (c) UniversalExpressionParser Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using System;
using JetBrains.Annotations;

namespace UniversalExpressionParser.ExpressionItems
{
    /// <summary>
    /// Expression item to which <see cref="IConstantTextExpressionItem.TextValue"/> is parsed.
    /// For example the expression "[Metadata1] 'Some text'" will be parsed to <see cref="IConstantTextExpressionItem"/> with the list <see cref="IComplexExpressionItem.Prefixes"/> containing
    /// the expression parsed from "[Metadata1]", and
    /// <see cref="IConstantTextExpressionItem.TextValue"/> of type <see cref="IConstantTextValueExpressionItem"/> will parsed from "'Some text'". 
    /// </summary>
    public interface IConstantTextValueExpressionItem : IExpressionItemBase
    {
        /// <summary>
        /// Text exactly as in expression that includes the start and end markers.
        /// For example in expression [var x="This expression text marker "" should be typed twice. However, other text markers do not need to appear: ', `."],
        /// the value of <see cref="Text"/> includes the entire
        /// text ["This expression text marker "" should be typed twice. However, other text markers do not need to appear: ', `."], which also includes the start/end text marker ["],
        /// as well as [""] typed twice in text. 
        /// </summary>
        [NotNull]
        string Text { get; }

        /// <summary>
        /// Parsed text that does not include the start and end markers, and that can be used in C# code..
        /// For example in expression [var x="This expression text marker "" should be typed twice. However, other text markers do not need to appear: ', `."],
        /// the value of <see cref="CSharpText"/> includes the following (without opening/closing square braces):
        /// [This expression text marker " should be typed twice. However, other text markers do not need to appear: ', `.],
        /// as well as [""] typed twice in text. 
        /// </summary>
        [NotNull]
        string CSharpText { get; }
    }

    /// <summary>
    /// Default implementation of <see cref="IConstantTextValueExpressionItem"/>.
    /// </summary>
    internal class ConstantTextValueExpressionItem : ExpressionItemBase, IConstantTextValueExpressionItem, ITextExpressionItem
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="text">
        /// Text exactly as in expression that includes the start and end markers.
        /// For example in expression [var x="This expression text marker "" should be typed twice. However, other text markers do not need to appear: ', `."],
        /// the value of <see cref="Text"/> includes the entire
        /// text ["This expression text marker "" should be typed twice. However, other text markers do not need to appear: ', `."], which also includes the start/end text marker ["],
        /// as well as [""] typed twice in text. 
        /// </param>
        /// <param name="cSharpText">
        /// <summary>
        /// Parsed text that does not include the start and end markers, and that can be used in C# code..
        /// For example in expression [var x="This expression text marker "" should be typed twice. However, other text markers do not need to appear: ', `."],
        /// the value of <see cref="CSharpText"/> includes the following (without opening/closing square braces):
        /// [This expression text marker " should be typed twice. However, other text markers do not need to appear: ', `.],
        /// as well as [""] typed twice in text. 
        /// </summary>
        /// </param>
        /// <param name="indexInText">Index of the expression item in parsed expression text.</param>
        /// <exception cref="System.ArgumentException">Throws this exception if <paramref name="text"/> is empty or tarts with space..</exception>
        public ConstantTextValueExpressionItem([NotNull] string text, [NotNull] string cSharpText, int indexInText)
        {
            if (text.Length == 0) throw new ArgumentException($"The value of '{nameof(text)}' cannot be empty.", nameof(text));

            if (char.IsWhiteSpace(text[0]))
                throw new ArgumentException($"The value of '{nameof(text)}' cannot start with white space character.", nameof(text));

            Text = text;
            IndexInText = indexInText;
            CSharpText = cSharpText;
        }

        /// <inheritdoc />
        public string Text { get; }

        /// <inheritdoc />
        public string CSharpText { get; }

        /// <inheritdoc />
        public override int IndexInText { get; }

        /// <inheritdoc />
        public override int ItemLength => Text.Length;

        /// <inheritdoc />
        string ITextExpressionItem.Text => Text;

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{this.GetType().FullName}, {nameof(IndexInText)}:{IndexInText}, {nameof(Text)}:{Text}";
        }
    }
}
