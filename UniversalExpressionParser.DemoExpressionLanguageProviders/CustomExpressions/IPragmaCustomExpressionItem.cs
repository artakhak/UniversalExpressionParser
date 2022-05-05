using JetBrains.Annotations;
using UniversalExpressionParser.ExpressionItems;

namespace UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions
{
    /// <summary>
    /// Example: print("Is in debug mode=" + ::pragma IsDebugMode)
    /// </summary>
    public interface IPragmaCustomExpressionItem: ITestLanguageCustomExpression
    {
        [NotNull]
        ITextExpressionItem PragmaSymbolExpressionItem { get; }
    }
}