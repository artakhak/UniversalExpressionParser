using System.Collections.Generic;
using JetBrains.Annotations;
using UniversalExpressionParser.ExpressionItems;

namespace UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions
{
    public interface IGenericTypeDataExpressionItem: ICustomExpressionItemPart
    {
        [NotNull]
        IKeywordExpressionItem WhereKeyword { get; }

        [CanBeNull]
        ITextExpressionItem TypeName { get; }

        [NotNull, ItemNotNull]
        IReadOnlyList<IExpressionItemBase> TypeConstraints { get; }
    }
}