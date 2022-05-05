// Copyright (c) UniversalExpressionParser Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using System;
using System.Collections.Generic;
using TextParser;
using UniversalExpressionParser.ExpressionItems.Custom;

namespace UniversalExpressionParser
{
    // Documented
    /// <inheritdoc />
    public abstract class ExpressionLanguageProviderBase : IExpressionLanguageProvider
    {
        /// <inheritdoc />
        public abstract string LanguageName { get; }

        /// <inheritdoc />
        public abstract string Description { get; }

        /// <inheritdoc />
        public virtual string LineCommentMarker { get; } = "//";

        /// <inheritdoc />
        public virtual string MultilineCommentStartMarker { get; } = "/*";

        /// <inheritdoc />
        public virtual string MultilineCommentEndMarker { get; } = "*/";

        /// <inheritdoc />
        public virtual string CodeBlockStartMarker { get; } = "{";

        /// <inheritdoc />
        public virtual string CodeBlockEndMarker { get; } = "}";

        /// <inheritdoc />
        public virtual char ExpressionSeparatorCharacter { get; } = ';';

        /// <inheritdoc />
        public virtual IReadOnlyList<char> ConstantTextStartEndMarkerCharacters { get; } = new[] { '"', '\'', '`' };

        /// <inheritdoc />
        public abstract IReadOnlyList<IOperatorInfo> Operators { get; }

        /// <inheritdoc />
        public abstract IReadOnlyList<ILanguageKeywordInfo> Keywords { get; }
        /// <inheritdoc />
        public virtual bool IsValidLiteralCharacter(char character, int positionInLiteral, ITextSymbolsParserState textSymbolsParserState)
        {
            if (character == '_')
                return true;

            if (character == '.' || Char.IsNumber(character))
                return positionInLiteral > 0;

            return Helpers.IsLatinLetter(character);
        }

        /// <inheritdoc />
        public virtual bool IsLanguageCaseSensitive => true;

        /// <inheritdoc />
        public abstract IEnumerable<ICustomExpressionItemParser> CustomExpressionItemParsers { get; }

        /// <inheritdoc />
        public virtual IReadOnlyList<NumericTypeDescriptor> NumericTypeDescriptors { get; } = 
            ExpressionLanguageProviderHelpers.GetDefaultNumericTypeDescriptors();

        /// <inheritdoc />
        public virtual bool SupportsPrefixes => false;

        /// <inheritdoc />
        public virtual bool SupportsKeywords => false;

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{nameof(LanguageName)}={this.LanguageName}, {this.GetType()}";
        }
    }
}