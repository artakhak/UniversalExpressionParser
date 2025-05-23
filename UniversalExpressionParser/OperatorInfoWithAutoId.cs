﻿// Copyright (c) UniversalExpressionParser Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using System.Collections.Generic;
using JetBrains.Annotations;
using OROptimizer;

namespace UniversalExpressionParser
{
    /// <summary>
    /// A subclass of <see cref="OperatorInfo"/> with the value of <see cref="IOperatorInfo.Id"/> autogenerated.
    /// </summary>
    public class OperatorInfoWithAutoId: OperatorInfo
    {
        /// <inheritdoc />
        public OperatorInfoWithAutoId([NotNull] [ItemNotNull] IReadOnlyList<string> nameParts, OperatorType operatorType, int priority) : 
            base(GlobalsCoreAmbientContext.Context.GenerateUniqueId(), nameParts, operatorType, priority)
        {
            
        }

        /// <inheritdoc />
        public OperatorInfoWithAutoId([NotNull] string name, OperatorType operatorType, int priority) : 
            base(GlobalsCoreAmbientContext.Context.GenerateUniqueId(), name, operatorType, priority)
        {
        }
    }
}