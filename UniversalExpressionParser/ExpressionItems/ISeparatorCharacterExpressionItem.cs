// Copyright (c) UniversalExpressionParser Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

namespace UniversalExpressionParser.ExpressionItems
{
    /// <summary>
    /// Expression item parsed from expression separator character that is defined in <see cref="IExpressionLanguageProvider.ExpressionSeparatorCharacter"/>.
    /// Example is ';' in expression "var x=1; ++x;"
    /// </summary>
    public interface ISeparatorCharacterExpressionItem : ITextExpressionItem
    {

    }

    /// <summary>
    /// Default implementation for <see cref="ISeparatorCharacterExpressionItem"/>.
    /// </summary>
    public class SeparatorCharacterExpressionItem : NameExpressionItem, ISeparatorCharacterExpressionItem
    {
        /// <inheritdoc />
        public SeparatorCharacterExpressionItem(char separatorCharacter, int indexInText) : base(separatorCharacter.ToString(), indexInText)
        {
        }
    }
}