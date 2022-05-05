// Copyright (c) UniversalExpressionParser Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace UniversalExpressionParser.ExpressionItems
{
    /// <summary>
    /// Default implementation for <see cref="ICodeBlockExpressionItem"/>
    /// </summary>
    public class CodeBlockExpressionItem: ExpressionItemSeriesBase, ICodeBlockExpressionItem, ICanAddSeparatorCharacterExpressionItem
    {
        private ICodeBlockEndMarkerExpressionItem _codeBlockEndMarkerItem;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="prefixExpressionItems">
        /// Prefix expression items. For example in "[Attribute1][Attribute2] public class MyClass",
        /// [Attribute1] and [Attribute2] will be parsed as prefixes, and "public", and "class" might be parsed as keywords for "MyClass"
        /// </param>
        /// <param name="keywordExpressionItems">Applied keyword expression items.
        /// For example in public static _counter: int = 5; public and int are keywords applied
        /// to expression _counter.</param>
        /// <param name="codeBlockStartMarker">Expression item for code block start marker.</param>
        /// <exception cref="ArgumentException">Throws this exception</exception>
        public CodeBlockExpressionItem([NotNull, ItemNotNull] IEnumerable<IExpressionItemBase> prefixExpressionItems,
                                       [NotNull, ItemNotNull] IEnumerable<IKeywordExpressionItem> keywordExpressionItems, 
                                       [NotNull] ICodeBlockStartMarkerExpressionItem codeBlockStartMarker) : 
            base(prefixExpressionItems, keywordExpressionItems)
        {
            CodeBlockStartMarker = codeBlockStartMarker;
            AddRegularItem(CodeBlockStartMarker);
        }

        /// <inheritdoc />
        public ICodeBlockStartMarkerExpressionItem CodeBlockStartMarker { get; }

        /// <inheritdoc />
        public ICodeBlockEndMarkerExpressionItem CodeBlockEndMarker
        {
            get => _codeBlockEndMarkerItem;
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value), $"The value of '{nameof(CodeBlockEndMarker)}' cannot be set to null.");

                if (this.CodeBlockEndMarker != null)
                    throw new ArgumentException($"The value of '{nameof(CodeBlockEndMarker)}' can be set only if it is null.", nameof(value));

                _codeBlockEndMarkerItem = value;

                this.AddRegularItem(_codeBlockEndMarkerItem);
            }
        }
    }
}