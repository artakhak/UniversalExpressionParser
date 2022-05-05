using System.Collections.Generic;
using System.Linq;

namespace UniversalExpressionParser.Tests.TestLanguage
{
    public class OperatorInfoForTest : IOperatorInfo
    {
        private static long _currentId;
        public OperatorInfoForTest(IReadOnlyList<string> nameParts, OperatorType operatorType) : this(++_currentId, nameParts, operatorType)
        {
        }

        public OperatorInfoForTest(long id, IReadOnlyList<string> nameParts, OperatorType operatorType)
        {
            Id = id;
            OperatorType = operatorType;
            Priority = 0;
            NameParts = nameParts;

            Name = NameParts != null ? string.Join(" ", NameParts.Select(x => x ?? string.Empty)) : string.Empty;
        }

        public long Id { get; }
        public int Priority { get; }
        public IReadOnlyList<string> NameParts { get; }
        public OperatorType OperatorType { get; }
        public string Name { get; }
    }
}