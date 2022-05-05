// Copyright (c) UniversalExpressionParser Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

namespace UniversalExpressionParser.ExpressionItems
{
    /// <summary>
    /// Settings used by <see cref="IExpressionParser"/>. Setting values can be accessed through using the ambient context ExpressionItemSettingsAmbientContext.Context.
    /// The default values should not be changed in most cases. If changes are necessary, use a code like follows:<br/><br/>
    /// 
    /// var expressionItemSettingCurrent = ExpressionItemSettingsAmbientContext.Context;<br/>
    ///<br/>
    /// try<br/>
    /// {<br/>
    ///     ExpressionItemSettingsAmbientContext.Context = new ExpressionItemSettings(false);<br/><br/>
    ///
    ///     if (ExpressionItemSettingsAmbientContext.Context.<see cref="IExpressionItemSettings.IsAddExpressionItemValidationOn"/>)<br/>
    ///         some code here...<br/>
    ///     else<br/>
    ///         some code here...<br/>
    ///     ...<br/>
    /// }<br/>
    /// finally<br/>
    /// {<br/>
    ///     ExpressionItemSettingsAmbientContext.Context = expressionItemSettingCurrent;<br/>
    /// }<br/>
    /// </summary>
    public interface IExpressionItemSettings
    {
        /// <summary>
        /// By default method calls that add new items to expressions, such us <see cref="BracesExpressionItem.AddParameter(IExpressionItemBase)"/>, validate of newly added item.
        /// For example, the validation includes checking that the value <see cref="ITextItem.IndexInText"/> of added item is not smaller than the value of previously added items.
        /// In some cases however we want to avoid the validation. A situation that might require skipping the validation is when we add an item that might not be fully initialized,
        /// and will fail the validation. If we are sure that the added expression item is valid, we can temporarily set the value of this property to false, and turn it on later. 
        /// </summary>
        bool IsAddExpressionItemValidationOn { get; }
    }
}