using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace UniversalExpressionParser.Tests
{
    public static class OperatorPriorities
    {
        private static readonly Dictionary<OperatorPriority, int> OperatorPriorityToNumericValueMap = new Dictionary<OperatorPriority, int>();
        private static readonly Dictionary<int, OperatorPriority> NumericOperatorPriorityToEnumMap = new Dictionary<int, OperatorPriority>();
        
        public static void Initialize()
        {
            OperatorPriorityToNumericValueMap.Clear();

            OperatorPriority previousPriority = OperatorPriority.Priority0;

            foreach (var priorityObj in Enum.GetValues(typeof(OperatorPriority)))
            {
                var priorityEnum = (OperatorPriority)priorityObj;

                int operatorPriority;

                if (priorityEnum == OperatorPriority.Priority0)
                {
                    Assert.AreEqual(0, OperatorPriorityToNumericValueMap.Count);
                    operatorPriority = TestSetup.SimulationRandomNumberGenerator.Next(0, 100);
                }
                else
                {
                    Assert.IsTrue(OperatorPriorityToNumericValueMap.ContainsKey(previousPriority));
                    Assert.IsTrue(previousPriority < priorityEnum);

                    operatorPriority = GeneratePriority(OperatorPriorityToNumericValueMap[previousPriority]);
                }
                    

                OperatorPriorityToNumericValueMap[priorityEnum] = operatorPriority;
                NumericOperatorPriorityToEnumMap[operatorPriority] = priorityEnum;

                previousPriority = priorityEnum;
            }
        }

        private static int GeneratePriority(int previousPriority)
        {
            var randomNumberGenerator = TestSetup.SimulationRandomNumberGenerator;

            if (randomNumberGenerator.Next(100) <= 50)
                return previousPriority + 1;

            return previousPriority + randomNumberGenerator.Next(2, 20);
        }

        public static int GetPriority(OperatorPriority operatorPriority) => OperatorPriorityToNumericValueMap[operatorPriority];
        public static OperatorPriority GetPriority(int operatorPriority) => NumericOperatorPriorityToEnumMap[operatorPriority];

    }
}
