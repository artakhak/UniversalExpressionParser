using System.Collections.Generic;
using JetBrains.Annotations;

namespace UniversalExpressionParser.Tests.TestLanguage
{
    public class OperatorInfoForTesting : OperatorInfoWithAutoId, IOperatorInfoForTesting
    {
        /// <inheritdoc />
        public OperatorInfoForTesting([NotNull] [ItemNotNull] IReadOnlyList<string> nameParts, OperatorType operatorType, 
                                      OperatorPriority operatorPriority, SpecialOperatorNameType specialOperatorNameType) : 
            base(nameParts, operatorType, OperatorPriorities.GetPriority(operatorPriority))
        {
            SpecialOperatorNameType = specialOperatorNameType;
            OperatorPriority = operatorPriority;
        }

        /// <inheritdoc />
        public SpecialOperatorNameType SpecialOperatorNameType { get; set; }

        /// <inheritdoc />
        public OperatorPriority OperatorPriority { get; }
    }
}