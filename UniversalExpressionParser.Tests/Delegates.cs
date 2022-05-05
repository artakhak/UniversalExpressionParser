using JetBrains.Annotations;
using UniversalExpressionParser.Tests.OperatorTemplates;

namespace UniversalExpressionParser.Tests
{
    public static class Delegates
    {
        //public delegate void ObjectsEqualityValidationDelegate(object expectedObject, object actualObject);

        //[NotNull]
        //Action<object, object> onObjectValidationFailed
        public delegate void OperatorTemplateProcessorDelegate([NotNull] OperatorTemplateBase operatorTemplate, ref bool stopProcessing);
    }
}
