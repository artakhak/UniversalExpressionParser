// Copyright (c) UniversalExpressionParser Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using System;

namespace UniversalExpressionParser.ExpressionItems
{
    /// <summary>
    /// Base class for classes that implement <see cref="IExpressionItemBase"/>
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{ToString()}")]
    public abstract class ExpressionItemBase: IExpressionItemBase
    {
        private IComplexExpressionItem _parent;

        /// <summary>
        /// Constructor.
        /// </summary>
        protected ExpressionItemBase()
        {
            Id = OROptimizer.GlobalsCoreAmbientContext.Context.GenerateUniqueId();
        }

        /// <inheritdoc />
        public abstract int IndexInText { get; }

        /// <inheritdoc />
        public abstract int ItemLength { get; }

        /// <inheritdoc />
        public long Id { get; }

        /// <summary>
        /// Parent code item.
        /// </summary>
        public IComplexExpressionItem Parent
        {
            get => _parent;
            set
            {
                if (value != null)
                {
                    var currentParent = value.Parent;
                    while (currentParent != null)
                    {
                        if (currentParent == this)
                            throw new Exception($"Circular reference via '{nameof(Parent)}' property");

                        currentParent = currentParent.Parent;
                    }
                }

                _parent = value;
            }
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{this.GetType().FullName}, {nameof(IndexInText)}:{IndexInText}, {nameof(ItemLength)}:{ItemLength}";
        }
    }
}