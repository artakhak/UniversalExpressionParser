// Copyright (c) UniversalExpressionParser Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using JetBrains.Annotations;

namespace UniversalExpressionParser.ExpressionItems
{
    /// <summary>
    /// Code block expression item. Example of expression that is parsed to <see cref="ICodeBlockExpressionItem"/> is "{var x=15; var y=x*3; }" when
    /// <see cref="IExpressionLanguageProvider.CodeBlockStartMarker"/> and <see cref="IExpressionLanguageProvider.CodeBlockEndMarker"/> are equal to "{" and "}" correspondingly.
    /// </summary>
    public interface ICodeBlockExpressionItem: ICanAddChildExpressionItem
    {
        /// <summary>
        /// Parsed code block start marker. Examples "{" or "BEGIN".
        /// </summary>
        [NotNull]
        ICodeBlockStartMarkerExpressionItem CodeBlockStartMarker { get; }

        /// <summary>
        /// Parsed code block start marker. Examples "}" or "END".
        /// </summary>
        [CanBeNull]
        ICodeBlockEndMarkerExpressionItem CodeBlockEndMarker { get; }
    }
}