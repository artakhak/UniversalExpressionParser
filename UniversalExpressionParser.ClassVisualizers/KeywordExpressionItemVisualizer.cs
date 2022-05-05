using ClassVisualizer;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using UniversalExpressionParser.ExpressionItems;

namespace UniversalExpressionParser.ClassVisualizers
{
    public class KeywordExpressionItemVisualizer : ExpressionItemVisualizer<IKeywordExpressionItem>
    {
        public KeywordExpressionItemVisualizer([NotNull] IValueVisualizerDependencyObjects valueVisualizerDependencyObjects, [NotNull] IObjectVisualizationContext objectVisualizationContext, 
            [NotNull] IKeywordExpressionItem visualizedExpressionItem, [NotNull] string visualizedElementName, [NotNull] Type mainInterfaceType, bool addChildren) : 
            base(valueVisualizerDependencyObjects, objectVisualizationContext, visualizedExpressionItem, visualizedElementName, mainInterfaceType, 
                addChildren)

        {
        }

        protected override (IList<IInterfacePropertyInfo> interfacePropertiesWithNoCategory, IList<IPropertyCategory> propertyCategories) GetVisualizedProperties()
        {
            var visualizedProperties = base.GetVisualizedProperties();

            if (!ExpressionItemVisualizerSettingsAmbientContext.Context.MinimizeOutput)
                return visualizedProperties;

            var interfacePropertiesWithNoCategory = new List<IInterfacePropertyInfo>(visualizedProperties.interfacePropertiesWithNoCategory);
            var propertyCategories = new List<IPropertyCategory>();

            foreach (var propertyCategory in visualizedProperties.propertyCategories)
            {
                var modifiedPropertyCategory = new PropertyCategory(propertyCategory.Name, propertyCategory.DoNotRenderIfEmpty);

                foreach(var childCategory in propertyCategory.ChildCategories)
                    modifiedPropertyCategory.ChildCategories.Add(childCategory);

                var propertyCategoryProperties = propertyCategory.Properties.ToList();
                foreach (var propertyInfo in propertyCategoryProperties)
                {
                    if (propertyCategory.Name == VisualizerConstants.OtherPropertiesCategoryName && 
                        propertyInfo.Name == nameof(IKeywordExpressionItem.LanguageKeywordInfo))
                    {
                        interfacePropertiesWithNoCategory.Add(propertyInfo);
                        propertyCategory.Properties.Remove(propertyInfo);
                    }
                    else if (propertyCategory.Name != nameof(IComplexExpressionItem.RegularItems))
                    {
                        modifiedPropertyCategory.Properties.Add(propertyInfo);
                    }
                }

                propertyCategories.Add(modifiedPropertyCategory);
            }

            return (interfacePropertiesWithNoCategory, propertyCategories);
        }
    }
}