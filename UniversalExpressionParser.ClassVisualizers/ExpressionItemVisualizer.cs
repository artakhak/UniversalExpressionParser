using ClassVisualizer;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using UniversalExpressionParser.ExpressionItems;

namespace UniversalExpressionParser.ClassVisualizers
{
    public class ExpressionItemVisualizer<TExpressionItemType> : InterfaceVisualizer where TExpressionItemType : IExpressionItemBase
    {
        [NotNull] private readonly IReadOnlyList<KeyValuePair<string, string>> _attributeNameValuePairsToShowAsAttributes;

        /// <inheritdoc />
        public ExpressionItemVisualizer([NotNull] IValueVisualizerDependencyObjects valueVisualizerDependencyObjects,
                                        [NotNull] IObjectVisualizationContext objectVisualizationContext,
                                        [NotNull] TExpressionItemType visualizedExpressionItem,
                                        [NotNull] string visualizedElementName, 
                                        [NotNull] Type mainInterfaceType, bool addChildren) :
            this(valueVisualizerDependencyObjects, objectVisualizationContext, visualizedExpressionItem, visualizedElementName,
                mainInterfaceType, addChildren, new List<KeyValuePair<string, string>>(0))
        {
        }

        /// <inheritdoc />
        public ExpressionItemVisualizer([NotNull] IValueVisualizerDependencyObjects valueVisualizerDependencyObjects,
            [NotNull] IObjectVisualizationContext objectVisualizationContext,
            [NotNull] TExpressionItemType visualizedExpressionItem,
            [NotNull] string visualizedElementName,
            [NotNull] Type mainInterfaceType, bool addChildren,
            [NotNull] IReadOnlyList<KeyValuePair<string, string>> attributeNameValuePairsToShowAsAttributes) : base(valueVisualizerDependencyObjects, objectVisualizationContext, visualizedExpressionItem, visualizedElementName, mainInterfaceType, addChildren)
        {
            _attributeNameValuePairsToShowAsAttributes = attributeNameValuePairsToShowAsAttributes;
            VisualizedExpressionItem = visualizedExpressionItem;
        }

        protected override bool PropertyShouldBeIgnoredVirtual(string propertyName)
        {
            if (base.PropertyShouldBeIgnoredVirtual(propertyName))
                return true;

            if (ExpressionItemVisualizerSettingsAmbientContext.Context.MinimizeOutput)
            {
                switch (propertyName)
                {
                    case nameof(IComplexExpressionItem.AllItems):
                    case SpecialVisualizedPropertyNames.ObjectId:
                        return true;
                }
            }

            return false;
        }

        [NotNull]
        protected TExpressionItemType VisualizedExpressionItem { get; }

        protected override (IList<IInterfacePropertyInfo> interfacePropertiesWithNoCategory, IList<IPropertyCategory> propertyCategories) GetVisualizedProperties()
        {
            var propertiesWithoutCategory = new List<IInterfacePropertyInfo>();
            var propertyCategories = new List<IPropertyCategory>();

            var addedPropertyNames = new HashSet<string>(StringComparer.Ordinal);
            var baseVisualizedProperties = base.GetVisualizedProperties();
            var propertyNameToPropertyInfo = new Dictionary<string, IInterfacePropertyInfo>(StringComparer.Ordinal);

            foreach (var interfacePropertyInfo in baseVisualizedProperties.interfacePropertiesWithNoCategory)
            {
                if (!propertyNameToPropertyInfo.ContainsKey(interfacePropertyInfo.Name))
                    propertyNameToPropertyInfo[interfacePropertyInfo.Name] = interfacePropertyInfo;
            }

            void TryAddProperty(string propertyName)
            {
                if (propertyNameToPropertyInfo.TryGetValue(propertyName, out var interfacePropertyInfo))
                {
                    addedPropertyNames.Add(propertyName);
                    propertiesWithoutCategory.Add(interfacePropertyInfo);
                }
            }

            void AddAttributePropertyWithValue(string propertyName, string propertyValue)
            {
                addedPropertyNames.Add(propertyName);
                propertiesWithoutCategory.Add(new InterfacePropertyInfo(propertyName, typeof(string),
                    PropertyVisualizationType.VisualizePropertyOnlyInAttribute, propertyValue));
            }

            void TryAddCollectionCategory<TCollectionItemType>(string categoryName,
                                                               IEnumerable<TCollectionItemType> expressionItems,
                                                               PropertyVisualizationType collectionItemVisualizationType,
                                                               bool doNotRenderIfEmpty = true) where TCollectionItemType : IExpressionItemBase
            {
                if (propertyNameToPropertyInfo.TryGetValue(categoryName, out var interfacePropertyInfo))
                {
                    addedPropertyNames.Add(interfacePropertyInfo.Name);

                    var propertyCategory = new PropertyCategory(categoryName, 
                        ExpressionItemVisualizerSettingsAmbientContext.Context.MinimizeOutput || doNotRenderIfEmpty);
                    propertyCategories.Add(propertyCategory);

                    foreach (var expressionItem in expressionItems)
                    {
                        var propertyInterfaceType = typeof(TCollectionItemType);

                        if (!propertyInterfaceType.IsInterface)
                            throw new Exception($"Type '{typeof(TCollectionItemType)}' is not an interface.");

                        propertyCategory.Properties.Add(new InterfacePropertyInfo(
                            expressionItem.GetVisualizedExpressionItemElementName(),
                            propertyInterfaceType, collectionItemVisualizationType, expressionItem));
                    }
                }
            }

            foreach (var attributeNameValuePair in _attributeNameValuePairsToShowAsAttributes)
            {
                AddAttributePropertyWithValue(attributeNameValuePair.Key, attributeNameValuePair.Value);
            }

            TryAddProperty(nameof(IExpressionItemBase.Id));
            TryAddProperty(nameof(IExpressionItemBase.IndexInText));
            TryAddProperty(nameof(IExpressionItemBase.ItemLength));

            if (this.VisualizedExpressionItem is IComplexExpressionItem complexExpressionItem)
            {
                if (complexExpressionItem.Prefixes.Count > 0)
                    TryAddCollectionCategory(nameof(IComplexExpressionItem.Prefixes), complexExpressionItem.Prefixes, PropertyVisualizationType.VisualizePropertyAndChildren);
                else
                    addedPropertyNames.Add(nameof(IComplexExpressionItem.Prefixes));

                if (complexExpressionItem.AppliedKeywords.Count > 0)
                    TryAddCollectionCategory(nameof(IComplexExpressionItem.AppliedKeywords), complexExpressionItem.AppliedKeywords, PropertyVisualizationType.VisualizePropertyAndChildren);
                else
                    addedPropertyNames.Add(nameof(IComplexExpressionItem.AppliedKeywords));

                if (complexExpressionItem.RegularItems.Count > 0)
                    TryAddCollectionCategory(nameof(IComplexExpressionItem.RegularItems), complexExpressionItem.RegularItems, PropertyVisualizationType.VisualizePropertyAndChildren,
                        ExpressionItemVisualizerSettingsAmbientContext.Context.MinimizeOutput);
                else
                    addedPropertyNames.Add(nameof(IComplexExpressionItem.RegularItems));

                if (complexExpressionItem.Postfixes.Count > 0)
                    TryAddCollectionCategory(nameof(IComplexExpressionItem.Postfixes), complexExpressionItem.Postfixes, PropertyVisualizationType.VisualizePropertyAndChildren);
                else
                    addedPropertyNames.Add(nameof(IComplexExpressionItem.Postfixes));

                if (complexExpressionItem.Children.Count > 0)
                    TryAddCollectionCategory(nameof(IComplexExpressionItem.Children), complexExpressionItem.Children, PropertyVisualizationType.VisualizePropertyOnlyInSeparateElement);
                else
                    addedPropertyNames.Add(nameof(IComplexExpressionItem.Children));
            }
            
            if (ExpressionItemVisualizerSettingsAmbientContext.Context.RenderOtherPropertiesSection)
            {
                var otherProperties = baseVisualizedProperties.interfacePropertiesWithNoCategory.Where(x =>
                    x.VisualizationType != PropertyVisualizationType.VisualizePropertyOnlyInAttribute &&
                    x.VisualizationType != PropertyVisualizationType.VisualizePropertyOnlyInAttributeInNextLine &&
                    !(addedPropertyNames.Contains(x.Name) ||
                      string.Equals(x.Name, nameof(IExpressionItemBase.Parent)))).ToList();

                if (otherProperties.Count > 0)
                {
                    var otherPropertiesCategory = new PropertyCategory(VisualizerConstants.OtherPropertiesCategoryName);
                    propertyCategories.Add(otherPropertiesCategory);

                    otherProperties.Sort((x, y) =>
                    {

                        var textItem1 = x.Value as ITextItem;
                        var textItem2 = y.Value as ITextItem;

                        if (textItem1 != null || textItem2 != null)
                        {
                            if (textItem2 == null)
                                return -1;

                            if (textItem1 == null)
                                return 1;

                            if (textItem1.IndexInText < textItem2.IndexInText)
                                return -1;

                            return textItem1.IndexInText == textItem2.IndexInText ? 0 : 1;
                        }

                        return 0;
                    });

                    foreach (var otherPropertyInfo in otherProperties)
                    {
                        otherPropertiesCategory.Properties.Add(otherPropertyInfo);
                    }
                }
            }

            return (propertiesWithoutCategory, propertyCategories);
        }
    }
}