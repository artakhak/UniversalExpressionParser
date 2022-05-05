namespace UniversalExpressionParser.Tests.TestLanguage
{
    public interface IOperatorInfoForTesting : IOperatorInfo
    {
        SpecialOperatorNameType SpecialOperatorNameType { get; }
        OperatorPriority OperatorPriority { get; }
    }
}
