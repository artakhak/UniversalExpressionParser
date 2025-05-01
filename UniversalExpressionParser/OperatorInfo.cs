// Copyright (c) UniversalExpressionParser Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UniversalExpressionParser.ExpressionItems;

namespace UniversalExpressionParser
{
    /// <inheritdoc />
    public class OperatorInfo: IOperatorInfo
    {
        /// <summary>
        /// Constructor for cases when operator can have one or many parts specified in <paramref name="nameParts"/>, such as {"IS", "NOT", "NULL"} or {"+"}.
        /// For simple cases when the operator has only part, the other constructor with parameter "name" of <see cref="string"/> type can e used.
        /// </summary>
        /// <param name="id">Unique value identifying the operator.</param>
        /// <param name="nameParts">
        /// In most cases this list will contain just one item (say "+" or "-").
        /// However, in some cases the list might contain multiple items.
        /// For example the SQL operator "IS NOT" will have two items: "IS" and "NOT".
        /// </param>
        /// <param name="operatorType">Operator type, such as binary, prefix unary, or postfix unary.</param>
        /// <param name="priority">
        /// Priority. Lower value means higher priority.
        /// For example if the priority of multiplication and division operators "*" and "/" are set to 2, and the
        /// priority of addition and subtraction operators '+', '-' are set to 3, then the expression
        /// x+y*z will be evaluated in such a way, that multiplication of y and z will be applied before addition of x and y*z.
        /// In other words the parsed expression will parse this expression to <see cref="IOperatorExpressionItem"/> for binary operator "+", which will have value
        /// of <see cref="IOperatorExpressionItem.Operand1"/> equal to an instance of <see cref="ILiteralExpressionItem"/> parsed from "x", and
        /// the value of <see cref="IOperatorExpressionItem.Operand2"/> will be another instance of <see cref="IOperatorExpressionItem"/> parsed from an expression
        /// "y*z".
        /// </param>
        /// <exception cref="ArgumentException">Throws this exception if <paramref name="nameParts"/> is null, or is an empty list.</exception>
        /// <exception cref="ArgumentException">Throws this exception if <paramref name="nameParts"/> contains an item which is either null, or is an empty string, or contains spaces.</exception>
        public OperatorInfo(long id, [NotNull, ItemNotNull] IReadOnlyList<string> nameParts, OperatorType operatorType, 
                            int priority)
        {
            if (nameParts == null || nameParts.Count == 0)
                throw new ArgumentException($"The value of parameter '{nameof(nameParts)}' cannot be null or an empty collection.", nameof(nameParts));

            foreach (var namePart in nameParts)
            {
                // ReSharper disable once ConditionIsAlwaysTrueOrFalse
                if (namePart == null || namePart.Trim().Length != namePart.Length)
                    throw new ArgumentException($"The collection '{nameof(namePart)}' cannot contain an item which is null or is an empty string.", nameof(nameParts));
            }

            NameParts = nameParts;
            Name = Helpers.GetOperatorName(nameParts);
            Initialize(id, operatorType, priority);
        }

        /// <summary>
        /// Constructor for simple cases when operator has only one part such as "+", "-".
        /// </summary>
        /// <param name="id">Unique value identifying the operator.</param>
        /// <param name="name">Operator name. Cannot include spaces. Valid examples are "&lt;&lt;", "|".</param>
        /// <param name="operatorType">Operator type, such as binary, prefix unary, or postfix unary.</param>
        /// <param name="priority">
        /// Priority. Lower value means higher priority.
        /// For example if the priority of multiplication and division operators "*" and "/" are set to 2, and the
        /// priority of addition and subtraction operators '+', '-' are set to 3, then the expression
        /// x+y*z will be evaluated in such a way, that multiplication of y and z will be applied before addition of x and y*z.
        /// In other words the parsed expression will parse this expression to <see cref="IOperatorExpressionItem"/> for binary operator "+", which will have value
        /// of <see cref="IOperatorExpressionItem.Operand1"/> equal to an instance of <see cref="ILiteralExpressionItem"/> parsed from "x", and
        /// the value of <see cref="IOperatorExpressionItem.Operand2"/> will be another instance of <see cref="IOperatorExpressionItem"/> parsed from an expression
        /// "y*z".
        /// </param>
        /// <exception cref="ArgumentException">Throws this exception if the value of <paramref name="name"/> is null or an empty string.</exception>
        public OperatorInfo(long id, [NotNull] string name, OperatorType operatorType, int priority)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));
           
            if (name.Trim().Length != name.Length)
                throw new ArgumentException($"The value of parameter '{nameof(name)}' cannot be null or an empty string.", nameof(name));

            NameParts = new [] { name };
            Name = name;
            Initialize(id, operatorType, priority);
        }

        private void Initialize(long id, OperatorType operatorType, int priority)
        {
            this.Id = id;
            OperatorType = operatorType;
            Priority = priority;
        }
        
        /// <inheritdoc />
        public long Id { get; private set; }

        /// <inheritdoc />
        public int Priority { get; private set; }

        /// <inheritdoc />
        public IReadOnlyList<string> NameParts { get; private set; }

        /// <inheritdoc />
        public OperatorType OperatorType { get; private set; }

        /// <inheritdoc />
        public string Name { get; }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{this.GetType().FullName}, {nameof(OperatorType)}:{OperatorType}, {nameof(Priority)}:{Priority}, {nameof(Name)}:{Name}";
        }
    }
}
