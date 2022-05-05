using JetBrains.Annotations;

namespace UniversalExpressionParser.Tests.OperatorTemplates
{
    public class BinaryOperatorTemplate : OperatorTemplateBase
    {
        /// <inheritdoc />
        private BinaryOperatorTemplate(OperatorPriority operatorPriority, 
                                      [CanBeNull] OperatorTemplateBase leftOperatorTemplate,
                                      [CanBeNull] OperatorTemplateBase rightOperatorTemplate) :
            base(OperatorType.BinaryOperator, operatorPriority)
        {
            LeftOperatorTemplate = leftOperatorTemplate;
            RightOperatorTemplate = rightOperatorTemplate;
        }

        public static BinaryOperatorTemplate CreateWithNoChildTemplates(OperatorPriority operatorPriority) =>
            new BinaryOperatorTemplate(operatorPriority, null, null);
        public static BinaryOperatorTemplate CreateWithTwoChildTemplates(OperatorPriority operatorPriority,
                                                    [NotNull] OperatorTemplateBase leftOperatorTemplate,
                                                    [NotNull] OperatorTemplateBase rightOperatorTemplate) =>
            new BinaryOperatorTemplate(operatorPriority, leftOperatorTemplate, rightOperatorTemplate);

        public static BinaryOperatorTemplate CreateWithLeftOperatorTemplate(OperatorPriority operatorPriority,
                                                    [NotNull] OperatorTemplateBase leftOperatorTemplate) =>
            new BinaryOperatorTemplate(operatorPriority, leftOperatorTemplate, null);

        public static BinaryOperatorTemplate CreateWithRightOperatorTemplate(OperatorPriority operatorPriority,
                                                                         [NotNull] OperatorTemplateBase rightOperatorTemplate) =>
            new BinaryOperatorTemplate(operatorPriority, null, rightOperatorTemplate);


        /// <summary>
        /// If the value is null, the left operand be non operator expression item. Otherwise
        /// the left operand will be an operator constructed using <see cref="LeftOperatorTemplate"/> template
        /// </summary>
        [CanBeNull]
        public override OperatorTemplateBase LeftOperatorTemplate { get; }

        /// <summary>
        /// If the value is null, the right operand be non operator expression item. Otherwise
        /// the right operand will be an operator constructed using <see cref="RightOperatorTemplate"/> template
        /// </summary>
        [CanBeNull]
        public override OperatorTemplateBase RightOperatorTemplate { get; }
    }
}