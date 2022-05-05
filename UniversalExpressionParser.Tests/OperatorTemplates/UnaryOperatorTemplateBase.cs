using JetBrains.Annotations;

namespace UniversalExpressionParser.Tests.OperatorTemplates
{
    public abstract class UnaryOperatorTemplateBase: OperatorTemplateBase
    {
        /// <inheritdoc />
        protected UnaryOperatorTemplateBase(bool isPrefixOperator, OperatorPriority operatorPriority) :
            // ReSharper disable once AssignNullToNotNullAttribute
            this(isPrefixOperator, operatorPriority, null)
        {
            
        }

        /// <inheritdoc />
        protected UnaryOperatorTemplateBase(bool isPrefixOperator, OperatorPriority operatorPriority, [NotNull] OperatorTemplateBase childOperatorTemplate) : 
            base(isPrefixOperator ? OperatorType.PrefixUnaryOperator : OperatorType.PostfixUnaryOperator, operatorPriority)
        {
            ChildOperatorTemplate = childOperatorTemplate;
        }

        /// <summary>
        /// If the value is null, the child will be non operator expression item. Otherwise
        /// the child will be constructed using <see cref="ChildOperatorTemplate"/> template
        /// </summary>
        [CanBeNull]
        public OperatorTemplateBase ChildOperatorTemplate { get; }
    }
}