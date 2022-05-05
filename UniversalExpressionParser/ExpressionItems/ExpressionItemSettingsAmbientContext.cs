// Copyright (c) UniversalExpressionParser Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using OROptimizer;

namespace UniversalExpressionParser.ExpressionItems
{
    /// <summary>
    /// Ambient context for <see cref="IExpressionItemSettings"/>. See the code docs in <see cref="IExpressionItemSettings"/> for an example of how to use this class.
    /// </summary>
    public class ExpressionItemSettingsAmbientContext : AmbientContext<IExpressionItemSettings, ExpressionItemSettings>
    {
    }
}