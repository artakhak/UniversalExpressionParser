// Copyright (c) UniversalExpressionParser Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using System;
using JetBrains.Annotations;

namespace UniversalExpressionParser.ExpressionItems
{
    /// <summary>
    /// Default implementation of <see cref="IOperatorExpressionItem"/>
    /// </summary>
    public class OperatorExpressionItem: ComplexExpressionItemBase, IOperatorExpressionItem
    {
        private IExpressionItemBase _operand1;
        private IExpressionItemBase _operand2;

        /// <inheritdoc />
        // ReSharper disable once NotNullMemberIsNotInitialized
        internal OperatorExpressionItem([NotNull] IOperatorInfoExpressionItem operatorInfoExpressionItem) :
            base(Array.Empty<IExpressionItemBase>(), Array.Empty<IKeywordExpressionItem>())
        {
            OperatorInfoExpressionItem = operatorInfoExpressionItem;
        }

        /// <inheritdoc />
        public OperatorExpressionItem([NotNull] IOperatorInfoExpressionItem operatorInfoExpressionItem,
                                         [NotNull] IExpressionItemBase operand1,
                                         [CanBeNull] IExpressionItemBase operand2) : this(operatorInfoExpressionItem)
        {
            this.Operand1 = operand1;

            if (operand2 != null)
                Operand2 = operand2;
        }
        
        internal IExpressionItemBase ExpressionItemToLeft { get; set; }
        internal IExpressionItemBase ExpressionItemToRight { get; set; }

        /// <summary>
        /// For example in "------x" the third decrement operator -- will be <see cref="BottomMostUnaryOperator"/> of the first -- operator expression item
        /// </summary>
        internal OperatorExpressionItem BottomMostUnaryOperator { get; set; }

        /// <inheritdoc />
        public IOperatorInfoExpressionItem OperatorInfoExpressionItem { get; }

        /// <inheritdoc />
        public IExpressionItemBase Operand1
        {
            get => _operand1;
            set
            {
                // ReSharper disable once ConditionIsAlwaysTrueOrFalse
                if (_operand1 != null || value == null)
                    return;
               
                _operand1 = value;

                switch (this.OperatorInfoExpressionItem.OperatorInfo.OperatorType)
                {
                    case OperatorType.PrefixUnaryOperator:
                        this.AddRegularItem(OperatorInfoExpressionItem);
                        this.AddChild(_operand1);
                        break;
                    case OperatorType.PostfixUnaryOperator:
                        this.AddChild(_operand1);
                        this.AddRegularItem(OperatorInfoExpressionItem);
                        break;
                    case OperatorType.BinaryOperator:
                        AddChild(_operand1);
                        this.AddRegularItem(OperatorInfoExpressionItem);
                        break;
                }

            }
        }

        /// <inheritdoc />
        public IExpressionItemBase Operand2
        {
            get => _operand2;
            set
            {
                // ReSharper disable once ConditionIsAlwaysTrueOrFalse
                if (_operand2 != null || value == null)
                    return;

                if (this.OperatorInfoExpressionItem.OperatorInfo.OperatorType != OperatorType.BinaryOperator)
                    throw new ArgumentException($"Unary operators cannot have non-null value for '{typeof(IOperatorExpressionItem).FullName}.{nameof(IOperatorExpressionItem.Operand2)}'.", nameof(Operand2));

                _operand2 = value;
                this.AddChild(_operand2);
            }
        }
    }
}
