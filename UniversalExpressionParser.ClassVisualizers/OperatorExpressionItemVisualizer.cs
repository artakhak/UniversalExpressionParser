using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using ClassVisualizer;
using UniversalExpressionParser.ExpressionItems;

namespace UniversalExpressionParser.ClassVisualizers
{
    public class OperatorExpressionItemVisualizer : ExpressionItemVisualizer<IOperatorExpressionItem>
    {
        [NotNull]
        protected IOperatorExpressionItem VisualizedOperatorExpressionItem { get; }

        /// <inheritdoc />
        public OperatorExpressionItemVisualizer([NotNull] IValueVisualizerDependencyObjects valueVisualizerDependencyObjects,
                                                [NotNull] IObjectVisualizationContext objectVisualizationContext,
                                                [NotNull] IOperatorExpressionItem visualizedOperatorExpressionItem,
                                                [NotNull] string visualizedElementName, 
                                                [NotNull] Type mainInterfaceType, 
                                                bool addChildren) : 
            base(valueVisualizerDependencyObjects, objectVisualizationContext, visualizedOperatorExpressionItem, visualizedElementName, mainInterfaceType, addChildren)
        {
            VisualizedOperatorExpressionItem = visualizedOperatorExpressionItem;
        }

        protected override (IList<IInterfacePropertyInfo> interfacePropertiesWithNoCategory, IList<IPropertyCategory> propertyCategories) GetVisualizedProperties()
        {
            var baseClassVisualizedProperties = base.GetVisualizedProperties();

            IList<IInterfacePropertyInfo> interfacePropertiesWithNoCategory;
            IList<IPropertyCategory> propertyCategories;

            if (!ExpressionItemVisualizerSettingsAmbientContext.Context.MinimizeOutput)
            {
                interfacePropertiesWithNoCategory = baseClassVisualizedProperties.interfacePropertiesWithNoCategory;
                propertyCategories = baseClassVisualizedProperties.propertyCategories;
            }
            else
            {
                var interfacePropertiesWithNoCategory2 = new List<IInterfacePropertyInfo>();
                interfacePropertiesWithNoCategory = interfacePropertiesWithNoCategory2;

                propertyCategories = new List<IPropertyCategory>();

                var regularItemsCategory = baseClassVisualizedProperties.propertyCategories.FirstOrDefault(x => x.Name == nameof(IComplexExpressionItem.RegularItems));

                if (regularItemsCategory != null)
                {
                    var operatorInfoProperty = regularItemsCategory.Properties.FirstOrDefault(x => x.Value == this.VisualizedExpressionItem.OperatorInfoExpressionItem);
                    
                    if (operatorInfoProperty != null)
                        interfacePropertiesWithNoCategory2.Add(operatorInfoProperty);

                    var operand1Property = regularItemsCategory.Properties.FirstOrDefault(x => x.Value == this.VisualizedExpressionItem.Operand1);

                    if (operand1Property != null)
                    {
                        operand1Property.Name = $"{nameof(IOperatorExpressionItem.Operand1)}.{operand1Property.Name}";
                        interfacePropertiesWithNoCategory2.Add(operand1Property);
                    }

                    var operand2Property = this.VisualizedExpressionItem.Operand2 == null ? null : regularItemsCategory.Properties.FirstOrDefault(x => x.Value == this.VisualizedExpressionItem.Operand2);
                    if (operand2Property != null)
                    {
                        operand2Property.Name = $"{nameof(IOperatorExpressionItem.Operand2)}.{operand2Property.Name}";
                        interfacePropertiesWithNoCategory2.Add(operand2Property);
                    }

                    interfacePropertiesWithNoCategory2.Sort((x, y) =>
                    {
                        if (!(x.Value is IExpressionItemBase expressionItem1 && y.Value is IExpressionItemBase expressionItem2))
                            return 0;

                        return expressionItem1.IndexInText - expressionItem2.IndexInText;
                    });
                }

                interfacePropertiesWithNoCategory2.AddRange(
                    baseClassVisualizedProperties.interfacePropertiesWithNoCategory.Where(x =>
                        x.VisualizationType == PropertyVisualizationType.VisualizePropertyOnlyInAttribute ||
                        x.VisualizationType == PropertyVisualizationType.VisualizePropertyOnlyInAttributeInNextLine));
            }

            var operatorInfo = VisualizedOperatorExpressionItem.OperatorInfoExpressionItem.OperatorInfo;

            interfacePropertiesWithNoCategory.Insert(0,
                new InterfacePropertyInfo(nameof(IOperatorInfo.Name), typeof(string),
                    PropertyVisualizationType.VisualizePropertyOnlyInAttribute, operatorInfo.Name));

            interfacePropertiesWithNoCategory.Insert(1,
                new InterfacePropertyInfo(nameof(IOperatorInfo.Priority), typeof(int),
                    PropertyVisualizationType.VisualizePropertyOnlyInAttribute, operatorInfo.Priority));

            return (interfacePropertiesWithNoCategory, propertyCategories);
        }
    }
}