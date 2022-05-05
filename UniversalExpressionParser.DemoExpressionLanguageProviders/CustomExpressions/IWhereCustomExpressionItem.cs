using JetBrains.Annotations;
using System.Collections.Generic;
using UniversalExpressionParser.ExpressionItems;
using UniversalExpressionParser.ExpressionItems.Custom;

namespace UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions
{
    public interface IWhereCustomExpressionItem: ICustomExpressionItem
    {
        [NotNull, ItemNotNull]
        IReadOnlyList<IGenericTypeDataExpressionItem> GenericTypeDataExpressionItems { get; }

        [CanBeNull]
        ITextExpressionItem WhereEndMarker { get; set; }
    }
}