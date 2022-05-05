// Copyright (c) UniversalExpressionParser Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using JetBrains.Annotations;

namespace UniversalExpressionParser.ExpressionItems
{
    /// <summary>
    /// Expression item parsed from code block start marker that is defined in <see cref="IExpressionLanguageProvider.CodeBlockEndMarker"/>.
    /// For example if <see cref="IExpressionLanguageProvider.CodeBlockEndMarker"/>. is '{', than the text "}" in expression "f1() => { var x=1; ++x;}" will be parsed to <see cref="ICodeBlockEndMarkerExpressionItem"/>.
    /// </summary>
    public interface ICodeBlockEndMarkerExpressionItem : ITextExpressionItem
    {

    }

    /// <summary>
    /// Default implementation for <see cref="ISeparatorCharacterExpressionItem"/>.
    /// </summary>
    public class CodeBlockEndMarkerExpressionItem : NameExpressionItem, ICodeBlockEndMarkerExpressionItem
    {
        /// <inheritdoc />
        public CodeBlockEndMarkerExpressionItem([NotNull] string name, int indexInText) : base(name, indexInText)
        {
        }
    }
}