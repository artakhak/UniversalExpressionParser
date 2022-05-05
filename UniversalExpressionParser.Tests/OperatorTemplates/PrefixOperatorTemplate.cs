using JetBrains.Annotations;

namespace UniversalExpressionParser.Tests.OperatorTemplates
{
    public class PrefixOperatorTemplate: UnaryOperatorTemplateBase
    {
        /// <inheritdoc />
        public PrefixOperatorTemplate(OperatorPriority operatorPriority) : base(true, operatorPriority)
        {
        }

        /// <inheritdoc />
        public PrefixOperatorTemplate(OperatorPriority operatorPriority, [NotNull] OperatorTemplateBase childOperatorTemplate) : base(true, operatorPriority, childOperatorTemplate)
        {
        }

        public override OperatorTemplateBase LeftOperatorTemplate => this.ChildOperatorTemplate;

        public override OperatorTemplateBase RightOperatorTemplate => null;
    }
}