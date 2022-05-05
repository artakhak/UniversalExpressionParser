namespace UniversalExpressionParser.Tests.OperatorTemplates
{
    public abstract class OperatorTemplateBase
    {
        private static int _counter;
        protected OperatorTemplateBase(OperatorType operatorType, OperatorPriority operatorPriority)
        {
            OperatorType = operatorType;
            OperatorPriority = operatorPriority;

            TemplateId = _counter++;
        }

        public int TemplateId { get; }
        public OperatorType OperatorType { get; }
        public OperatorPriority OperatorPriority { get; }

        public abstract OperatorTemplateBase LeftOperatorTemplate { get; }
        public abstract OperatorTemplateBase RightOperatorTemplate { get; }

    }
}
