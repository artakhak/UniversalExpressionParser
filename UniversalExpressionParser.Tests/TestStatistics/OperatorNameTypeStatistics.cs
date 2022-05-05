using System;
using UniversalExpressionParser.ExpressionItems;
using UniversalExpressionParser.Tests.TestLanguage;

namespace UniversalExpressionParser.Tests.TestStatistics
{
    public class OperatorNameTypeStatistics: TextItemStatistic
    {
        private readonly OperatorType _operatorType;
        private readonly SpecialOperatorNameType _specialOperatorNameType;

        public OperatorNameTypeStatistics(OperatorType operatorType, SpecialOperatorNameType specialOperatorNameType)
        {
            _operatorType = operatorType;
            _specialOperatorNameType = specialOperatorNameType;

            StatisticName = $"OperatorType={_operatorType}, OperatorNameType={_specialOperatorNameType}";
        }
        /// <inheritdoc />
        public override string StatisticName { get; }

        /// <inheritdoc />
        public override bool IsStatisticSourceAMatch(ITextItem statisticSource)
        {
            if (!(statisticSource is IOperatorInfoExpressionItem operatorInfoExpressionItem))
                return false;

            if (!(operatorInfoExpressionItem.OperatorInfo is IOperatorInfoForTesting operatorInfoForTesting))
                throw new Exception($"Invalid type. Expected '{typeof(IOperatorInfoForTesting).FullName}'.");

            return operatorInfoForTesting.OperatorType == _operatorType && 
                   (operatorInfoForTesting.SpecialOperatorNameType & _specialOperatorNameType) == _specialOperatorNameType;
        }
    }
}