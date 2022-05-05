using JetBrains.Annotations;

namespace UniversalExpressionParser.Tests.OperatorTemplates
{
    public class PostfixOperatorTemplate : UnaryOperatorTemplateBase
    {
        /// <inheritdoc />
        public PostfixOperatorTemplate(OperatorPriority operatorPriority, [CanBeNull] OperatorTemplateBase childOperatorTemplate) : base(false, operatorPriority, childOperatorTemplate)
        {
        }

        /// <inheritdoc />
        public PostfixOperatorTemplate(OperatorPriority operatorPriority) : base(false, operatorPriority)
        {
        }

        public override OperatorTemplateBase RightOperatorTemplate => this.ChildOperatorTemplate;

        public override OperatorTemplateBase LeftOperatorTemplate => null;
    }
}