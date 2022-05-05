// Copyright (c) UniversalExpressionParser Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

namespace UniversalExpressionParser.ExpressionItems
{
    /// <inheritdoc />
    public class ExpressionItemSettings: IExpressionItemSettings
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public ExpressionItemSettings()
        {
        }
        
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="isAddExpressionItemValidationOn">Value from which the property <see cref="IsAddExpressionItemValidationOn"/> is initialized.</param>
        public ExpressionItemSettings(bool isAddExpressionItemValidationOn)
        {
            IsAddExpressionItemValidationOn = isAddExpressionItemValidationOn;
        }

        /// <inheritdoc />
        public bool IsAddExpressionItemValidationOn { get; } = true;
    }
}