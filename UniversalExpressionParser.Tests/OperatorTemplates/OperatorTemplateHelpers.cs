using JetBrains.Annotations;
using System.Collections.Generic;

namespace UniversalExpressionParser.Tests.OperatorTemplates
{
    public static class OperatorTemplateHelpers
    {
        private static readonly Dictionary<OperatorType, List<OperatorPriority>> _operatorTypeToPrioritiesList = new Dictionary<OperatorType, List<OperatorPriority>>();
        private static readonly Dictionary<OperatorType, HashSet<OperatorPriority>> _operatorTypeToPrioritiesSet = new Dictionary<OperatorType, HashSet<OperatorPriority>>();
        static OperatorTemplateHelpers()
        {
            _operatorTypeToPrioritiesList[OperatorType.PrefixUnaryOperator] = new List<OperatorPriority>();
            _operatorTypeToPrioritiesSet[OperatorType.PrefixUnaryOperator] = new HashSet<OperatorPriority>();

            _operatorTypeToPrioritiesList[OperatorType.PostfixUnaryOperator] = new List<OperatorPriority>();
            _operatorTypeToPrioritiesSet[OperatorType.PostfixUnaryOperator] = new HashSet<OperatorPriority>();

            _operatorTypeToPrioritiesList[OperatorType.BinaryOperator] = new List<OperatorPriority>();
            _operatorTypeToPrioritiesSet[OperatorType.BinaryOperator] = new HashSet<OperatorPriority>();

            void OperatorTemplateProcessor(OperatorTemplateBase operatorTemplate, ref bool stopProcessing)
            {
                var operatorPrioritiesSet = _operatorTypeToPrioritiesSet[operatorTemplate.OperatorType];

                if (!operatorPrioritiesSet.Contains(operatorTemplate.OperatorPriority))
                {
                    operatorPrioritiesSet.Add(operatorTemplate.OperatorPriority);
                    _operatorTypeToPrioritiesList[operatorTemplate.OperatorType].Add(operatorTemplate.OperatorPriority);
                }
            }

            foreach (var operatorTemplate in OperatorTemplatesCollection.OperatorTemplates)
                ProcessOperatorTemplate(operatorTemplate, OperatorTemplateProcessor);
        }

        public static IReadOnlyList<OperatorPriority> GetOperatorPriorities(OperatorType operatorType)
        {
            return _operatorTypeToPrioritiesList[operatorType];
        }

        public static bool ContainsPriority(OperatorType operatorType, OperatorPriority operatorPriority) => _operatorTypeToPrioritiesSet[operatorType].Contains(operatorPriority);

        public static void ProcessOperatorTemplate([NotNull] OperatorTemplateBase operatorTemplate, [NotNull] Delegates.OperatorTemplateProcessorDelegate operatorTemplateProcessor)
        {
            var stopProcessing = false;
            ProcessOperatorTemplate(operatorTemplate, operatorTemplateProcessor, ref stopProcessing);
        }

        private static void ProcessOperatorTemplate([NotNull] OperatorTemplateBase operatorTemplate, [NotNull] Delegates.OperatorTemplateProcessorDelegate operatorTemplateProcessor, ref bool stopProcessing)
        {
            operatorTemplateProcessor(operatorTemplate, ref stopProcessing);

            if (stopProcessing)
                return;

            if (operatorTemplate.LeftOperatorTemplate != null)
            {
                ProcessOperatorTemplate(operatorTemplate.LeftOperatorTemplate, operatorTemplateProcessor, ref stopProcessing);

                if (stopProcessing)
                    return;
            }

            if (operatorTemplate.RightOperatorTemplate != null)
                ProcessOperatorTemplate(operatorTemplate.RightOperatorTemplate, operatorTemplateProcessor, ref stopProcessing);
        }
    }
}
