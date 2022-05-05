// Copyright (c) UniversalExpressionParser Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using JetBrains.Annotations;

namespace UniversalExpressionParser.ExpressionItems
{
    /// <summary>
    /// Expression item to which <see cref="ILiteralExpressionItem.LiteralName"/> is parsed.
    /// For example the expression "var x" will be parsed to <see cref="ILiteralExpressionItem"/> with the list <see cref="IComplexExpressionItem.AppliedKeywords"/> containing
    /// the keyword expression item of type <see cref="IKeywordExpressionItem"/> parsed from "var", and
    /// <see cref="ILiteralExpressionItem.LiteralName"/> of type <see cref="ILiteralNameExpressionItem"/> will parsed from "x". 
    /// </summary>
    public interface ILiteralNameExpressionItem : ITextExpressionItem
    {

    }

    /// <summary>
    /// Default implementation of <see cref="ILiteralNameExpressionItem"/>
    /// </summary>
    public class LiteralNameExpressionItem : NameExpressionItem, ILiteralNameExpressionItem
    {
        /// <inheritdoc />
        public LiteralNameExpressionItem([NotNull] string name, int indexInText) : base(name, indexInText)
        {
        }
    }
}