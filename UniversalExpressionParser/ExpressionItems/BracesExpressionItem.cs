// Copyright (c) UniversalExpressionParser Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace UniversalExpressionParser.ExpressionItems
{
    /// <summary>
    /// Expression item for brackets. Example of expression that will be parsed to this type of expression is (y+z) in x*(y+z),
    /// or (#x) in !(#x), [x1, 1, y+3], Func1(x, 2), matrix1[0, i]
    /// </summary>
    public class BracesExpressionItem : ComplexExpressionItemBase, IBracesExpressionItem
    {
        private readonly List<IExpressionItemBase> _parameters = new List<IExpressionItemBase>();

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="prefixExpressionItems">
        /// Prefix expression items. For example in "[Attribute1][Attribute2] public class MyClass",
        /// [Attribute1] and [Attribute2] will be parsed as prefixes, and "public", and "class" might be parsed as keywords for "MyClass"
        /// </param>
        /// <param name="keywordExpressionItems">Applied keyword expression items.
        /// For example in public static _counter: int = 5; public and int are keywords applied
        /// to expression _counter.</param> 
        /// <param name="name">
        /// Expression parsed from the name that appears before the braces. Example of expression that will be parsed to <see cref="ILiteralExpressionItem"/> is "F1" in "F1(x,y)", or "Matrix1" in "Matrix1[i, j]".
        /// The value can be null, which indicates that there is no name before braces. Examples are "var x=[1, y, 5]", or "(x, y) => 5";
        /// </param>
        /// <param name="isRoundBraces">Indicates if the braces  expression item contains round braces (i.e., '(' and ')'), or square braces (i.e., '[' and ']').</param>
        /// <param name="openingBracePositionInText">
        /// Position of the opening brace. If the value of <paramref name="name"/> is not null, the value of
        /// <paramref name="openingBracePositionInText"/> should be greater or equal to (<paramref name="name"/>.<see cref="ITextItem.IndexInText"/> +
        /// <paramref name="name"/>.<see cref="ITextItem.ItemLength"/>)
        /// </param>
        public BracesExpressionItem([NotNull, ItemNotNull] IEnumerable<IExpressionItemBase> prefixExpressionItems, 
                                    [NotNull, ItemNotNull] IEnumerable<IKeywordExpressionItem> keywordExpressionItems, 
                                    [CanBeNull] ILiteralExpressionItem name,
                                    bool isRoundBraces, int openingBracePositionInText) :
            base(prefixExpressionItems, keywordExpressionItems)
        {
            var openingBraceInfo = new OpeningBraceExpressionItem(isRoundBraces, openingBracePositionInText);

            if (name != null && (name.IndexInText + name.ItemLength) > openingBracePositionInText)
                throw new ArgumentException($"The item {name} should be before the opening brace.", nameof(openingBraceInfo));

            if (name != null)
            {
                AddRegularItem(name);
                NameLiteral = name;
            }

            OpeningBrace = openingBraceInfo;
            AddRegularItem(OpeningBrace);
        }

        /// <inheritdoc />
        public ILiteralExpressionItem NameLiteral { get; }

        /// <inheritdoc />
        public IOpeningBraceExpressionItem OpeningBrace { get; }

        /// <inheritdoc />
        public IClosingBraceExpressionItem ClosingBrace { get; private set; }

        /// <inheritdoc />
        public IReadOnlyList<IExpressionItemBase> Parameters => _parameters;

        /// <summary>
        /// Sets the closing brace expression item.
        /// </summary>
        /// <param name="closingBrace">Closing brace.</param>
        public void SetClosingBraceInfo(IClosingBraceExpressionItem closingBrace)
        {
            if (closingBrace == null)
                throw new ArgumentNullException(nameof(closingBrace), $"The value of '{nameof(closingBrace)}' cannot be null.");

            if (this.ClosingBrace != null)
                throw new ArgumentException($"The value of '{nameof(ClosingBrace)}' can be set only if it is null.", nameof(closingBrace));
        
            if (OpeningBrace.IsRoundBrace != closingBrace.IsRoundBrace)
                throw new ArgumentException($"Invalid value. The value of {typeof(IClosingBraceExpressionItem)}.{nameof(IClosingBraceExpressionItem.IsRoundBrace)} is expected to be {OpeningBrace.IsRoundBrace}.", nameof(closingBrace));

            var lastItem = this.GetLastExpressionItem();

            if (lastItem is ICommaExpressionItem)
                _parameters.Add(null);

            ClosingBrace = closingBrace;

            AddRegularItem(ClosingBrace);
        }

        /// <summary>
        /// Adds a comma to <see cref="IComplexExpressionItem.RegularItems"/>.
        /// </summary>
        /// <param name="commaIndexInText">Comma index in text.</param>
        public void AddComma(int commaIndexInText)
        {
            if (IsLastExpressionItemCommaOrOpeningBrace())
                _parameters.Add(null);

            AddRegularItem(new CommaExpressionItem(commaIndexInText));
        }

        private bool IsLastExpressionItemCommaOrOpeningBrace()
        {
            var lastExpressionItem = this.GetLastExpressionItem();

            return lastExpressionItem is ICommaExpressionItem || lastExpressionItem is IOpeningBraceExpressionItem;
        }

        /// <summary>
        /// Adds a new parameter to <see cref="IComplexExpressionItem.Children"/> and <see cref="Parameters"/>.
        /// </summary>
        /// <param name="bracesParameter"></param>
        public void AddParameter([NotNull] IExpressionItemBase bracesParameter)
        {
            //if (!IsLastExpressionItemCommaOrOpeningBrace())
            //    throw new ArgumentException("Expects either a comma or an opening square or round brace before a parameter.", nameof(bracesParameter));

            if (IsLastExpressionItemCommaOrOpeningBrace())
            {
                _parameters.Add(bracesParameter);
                AddChild(bracesParameter);
            }
        }

        /// <inheritdoc />
        void ICanAddChildExpressionItem.AddChildExpressionItem(IExpressionItemBase childExpressionItem)
        {
            AddParameter(childExpressionItem);
        }
    }
}