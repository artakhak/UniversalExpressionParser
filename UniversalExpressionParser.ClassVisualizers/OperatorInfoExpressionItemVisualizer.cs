using ClassVisualizer;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using UniversalExpressionParser.ExpressionItems;

namespace UniversalExpressionParser.ClassVisualizers
{
    public class OperatorInfoExpressionItemVisualizer : ExpressionItemVisualizer<IOperatorInfoExpressionItem>
    {
        /// <inheritdoc />
        public OperatorInfoExpressionItemVisualizer([NotNull] IValueVisualizerDependencyObjects valueVisualizerDependencyObjects,
                                                    [NotNull] IObjectVisualizationContext objectVisualizationContext,
                                                    [NotNull] IOperatorInfoExpressionItem visualizedExpressionItem,
                                                    [NotNull] string visualizedElementName,
                                                    [NotNull] Type mainInterfaceType, bool addChildren) : 
            base(valueVisualizerDependencyObjects, objectVisualizationContext, visualizedExpressionItem, visualizedElementName, mainInterfaceType, addChildren)
        {

        }

        protected override bool PropertyShouldBeIgnoredVirtual(string propertyName)
        {
            if (propertyName == nameof(IOperatorInfoExpressionItem.OperatorInfo) || propertyName == nameof(IOperatorInfoExpressionItem.OperatorNameParts))
                return true;

            return base.PropertyShouldBeIgnoredVirtual(propertyName);
        }

        protected override (IList<IInterfacePropertyInfo> interfacePropertiesWithNoCategory, IList<IPropertyCategory> propertyCategories) GetVisualizedProperties()
        {
            var baseClassVisualizedProperties = base.GetVisualizedProperties();
            var interfacePropertiesWithNoCategory = new List<IInterfacePropertyInfo>();
            //var interfacePropertiesWithNoCategory = baseClassVisualizedProperties.interfacePropertiesWithNoCategory;

            var operatorInfo = this.VisualizedExpressionItem.OperatorInfo;

            interfacePropertiesWithNoCategory.Add(
                new InterfacePropertyInfo(nameof(IOperatorInfo.OperatorType), typeof(OperatorType),
                    PropertyVisualizationType.VisualizePropertyOnlyInAttribute, operatorInfo.OperatorType));

            interfacePropertiesWithNoCategory.Add(
                new InterfacePropertyInfo(nameof(IOperatorInfo.Name), typeof(string),
                    PropertyVisualizationType.VisualizePropertyOnlyInAttribute, operatorInfo.Name));

            interfacePropertiesWithNoCategory.Add(
                new InterfacePropertyInfo(nameof(IOperatorInfo.Priority), typeof(int),
                    PropertyVisualizationType.VisualizePropertyOnlyInAttribute, operatorInfo.Priority));

            interfacePropertiesWithNoCategory.AddRange(baseClassVisualizedProperties.interfacePropertiesWithNoCategory);

            IList<IPropertyCategory> propertyCategories;

            if (!ExpressionItemVisualizerSettingsAmbientContext.Context.MinimizeOutput)
            {
                propertyCategories = baseClassVisualizedProperties.propertyCategories;
            }
            else
            {
                propertyCategories = new List<IPropertyCategory>();

                var propertyCategory = new PropertyCategory(nameof(IOperatorInfoExpressionItem.OperatorNameParts), false);
                propertyCategories.Add(propertyCategory);

                foreach (var operatorNamePartExpressionItem in this.VisualizedExpressionItem.OperatorNameParts)
                {
                    var interfaceType = typeof(IOperatorNamePartExpressionItem);

                    propertyCategory.Properties.Add(new InterfacePropertyInfo(interfaceType.Name,
                        interfaceType, PropertyVisualizationType.VisualizePropertyAndChildren, operatorNamePartExpressionItem));
                }
            }

            return (interfacePropertiesWithNoCategory, propertyCategories);
        }
    }
}