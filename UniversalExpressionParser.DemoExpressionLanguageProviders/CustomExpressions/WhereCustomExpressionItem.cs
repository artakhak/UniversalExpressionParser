using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using UniversalExpressionParser.ExpressionItems;
using UniversalExpressionParser.ExpressionItems.Custom;

namespace UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions
{
    public class WhereCustomExpressionItem : CustomExpressionItem, IWhereCustomExpressionItem, ITestLanguageCustomExpression
    {
        [CanBeNull]
        private ITextExpressionItem _whereEndMarker;

        [NotNull, ItemNotNull]
        private readonly List<IGenericTypeDataExpressionItem> _genericTypeDataExpressionItems = new List<IGenericTypeDataExpressionItem>();
        /// <inheritdoc />
        public IReadOnlyList<IGenericTypeDataExpressionItem> GenericTypeDataExpressionItems => _genericTypeDataExpressionItems;

        /// <inheritdoc />
        public WhereCustomExpressionItem([NotNull][ItemNotNull] IEnumerable<IExpressionItemBase> prefixExpressionItems,
            [NotNull][ItemNotNull] IEnumerable<IKeywordExpressionItem> keywordExpressionItems) :
            base(prefixExpressionItems, keywordExpressionItems, CustomExpressionItemCategory.Postfix)
        {
        }

        public void AddGenericTypeData([NotNull] IGenericTypeDataExpressionItem genericTypeDataExpressionItem)
        {
            _genericTypeDataExpressionItems.Add(genericTypeDataExpressionItem);
            this.AddChild(genericTypeDataExpressionItem);
        }

        /// <inheritdoc />
        public ITextExpressionItem WhereEndMarker
        {
            get => _whereEndMarker;
            set
            {
                _whereEndMarker = value ?? throw new ArgumentNullException(nameof(value));
                this.AddRegularItem(value);
            }
        }

        public long KeywordId => KeywordIds.Where;

        public override int? ErrorsPositionDisplayValue
        {
            get
            {
                if (this.RegularItems.Count > 0)
                    return this.RegularItems[0].IndexInText;

                return null;
            }
        }
    }
    /*public class WhereCustomExpressionItem: TestLanguageCustomExpressionBase, IWhereCustomExpressionItem
    {
        [CanBeNull]
        private INameExpressionItem _whereEndMarker;

        [NotNull, ItemNotNull]
        private readonly List<IGenericTypeDataExpressionItem> _genericTypeDataExpressionItems = new List<IGenericTypeDataExpressionItem>();
        /// <inheritdoc />
        public IReadOnlyList<IGenericTypeDataExpressionItem> GenericTypeDataExpressionItems => _genericTypeDataExpressionItems;

        /// <inheritdoc />
        public WhereCustomExpressionItem([NotNull] [ItemNotNull] IEnumerable<IExpressionItemBase> prefixExpressionItems,
                                         [NotNull] [ItemNotNull] IEnumerable<IKeywordExpressionItem> keywordExpressionItems) :
            base(prefixExpressionItems, keywordExpressionItems,
                KeywordIds.WhereKeywordId, CustomExpressionItemCategory.Postfix)
        {
        }

        public void AddGenericTypeData([NotNull] IGenericTypeDataExpressionItem genericTypeDataExpressionItem)
        {
            _genericTypeDataExpressionItems.Add(genericTypeDataExpressionItem);
            this.AddChild(genericTypeDataExpressionItem);
        }

        /// <inheritdoc />
        public INameExpressionItem WhereEndMarker
        {
            get => _whereEndMarker;
            set
            {
                _whereEndMarker = value ?? throw new ArgumentNullException(nameof(WhereEndMarker));
                this.AddRegularItem(value);
            }
        }
    }*/
}
