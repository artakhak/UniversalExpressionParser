using JetBrains.Annotations;
using UniversalExpressionParser.ExpressionItems;

namespace UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions
{
    /// <summary>
    /// public ::metadata({attributes: [Attribute1, Attribute2]; DisplayName: "Factorial"}) Fact(x: int): int
    /// </summary>
    public interface IMetadataCustomExpressionItem : ITestLanguageCustomExpression
    {
        [CanBeNull]
        ICodeBlockExpressionItem MetadataExpressionItem { get; }
    }
}