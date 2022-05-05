using ClassVisualizer;
using JetBrains.Annotations;
using System;
using UniversalExpressionParser.ClassVisualizers;
using UniversalExpressionParser.ExpressionItems;

namespace UniversalExpressionParser.Tests.Utilities.ExpressionItemVisualizersForDemos
{
    public class ValueVisualizerFactoryForDemos : ValueVisualizerFactory
    {
        public ValueVisualizerFactoryForDemos([NotNull] IInterfacePropertyVisualizationHelper interfacePropertyVisualizationHelper, [NotNull] IAttributeValueSanitizer attributeValueSanitizer) : base(interfacePropertyVisualizationHelper, attributeValueSanitizer)
        {
        }

        public override IClassVisualizer CreateInterfaceVisualizer(IObjectVisualizationContext objectVisualizationContext, object visualizedObject, string visualizedElementName, Type mainInterfaceType, bool addChildren)
        {
            if (visualizedObject is IOperatorExpressionItem operatorExpressionItem)
                return new OperatorExpressionItemVisualizerForDemos(ValueVisualizerDependencyObjects, objectVisualizationContext, operatorExpressionItem, visualizedElementName, mainInterfaceType, addChildren);

            return base.CreateInterfaceVisualizer(objectVisualizationContext, visualizedObject, visualizedElementName, mainInterfaceType, addChildren);
        }
    }
}
