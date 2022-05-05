using JetBrains.Annotations;
using UniversalExpressionParser.ExpressionItems;

namespace UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions
{
    /*Example:
    abstract ::types(T1(T2, T3(T4), T5)) F1(x:T1): void; 
    is similar to C# code like 
    abstract void F1<T1<T2, T3<T4>,T5>> (T1 x);*/
    public interface IGenericTypesCustomExpressionItem: ITestLanguageCustomExpression
    {
        [CanBeNull]
        IBracesExpressionItem TypesBracesExpressionItem { get; }
    }
}