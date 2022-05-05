using UniversalExpressionParser.ExpressionItems;

namespace UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions
{
    /// <summary>
    /// Base interface for expression items that are part of custom expression. Might be used in test projects. Otherwise, the custom
    /// expressions do not need to implement a base class for custom expression items.
    /// </summary>
    public interface ICustomExpressionItemPart : IComplexExpressionItem
    {

    }
}