using JetBrains.Annotations;
using System.Collections.Generic;
using UniversalExpressionParser.ExpressionItems;

namespace UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions
{
    public class GenericTypeDataExpressionItem: ComplexExpressionItemBase, IGenericTypeDataExpressionItem
    {
        [NotNull, ItemNotNull]
        private readonly List<IExpressionItemBase> _typeConstraints = new List<IExpressionItemBase>();

        /// <inheritdoc />
        public GenericTypeDataExpressionItem([NotNull] IKeywordExpressionItem whereKeywordExpressionItem) : 
            base(new List<IExpressionItemBase>(0) , new List<IKeywordExpressionItem>(0))
        {
            WhereKeyword = whereKeywordExpressionItem;
            AddRegularItem(WhereKeyword);
        }

        /// <inheritdoc />
        // ReSharper disable once NotNullMemberIsNotInitialized
        public IKeywordExpressionItem WhereKeyword { get; private set; }

        /// <inheritdoc />
        // ReSharper disable once NotNullMemberIsNotInitialized
        public ITextExpressionItem TypeName { get; private set; }

        /// <inheritdoc />
        // ReSharper disable once UnassignedGetOnlyAutoProperty
        public IReadOnlyList<IExpressionItemBase> TypeConstraints => _typeConstraints;

        public void AddTypeName([NotNull] ITextExpressionItem typeName)
        {
            this.TypeName = typeName;
            AddRegularItem(typeName);
        }

        public void AddComma(int positionInText)
        {
            this.AddRegularItem(new CommaExpressionItem(positionInText));
        }

        public void AddColon(int positionInText)
        {
            this.AddRegularItem(new NameExpressionItem(":", positionInText));
        }

        public void AddTypeConstraint([NotNull] IExpressionItemBase typeConstraint)
        {
            this._typeConstraints.Add(typeConstraint);
            this.AddRegularItem(typeConstraint);
        }

    }
}