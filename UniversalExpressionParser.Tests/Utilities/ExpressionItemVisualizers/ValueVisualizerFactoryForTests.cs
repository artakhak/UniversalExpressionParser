using ClassVisualizer;
using JetBrains.Annotations;
using System;
using TestsSharedLibrary.ClassVisualizers;
using UniversalExpressionParser.ClassVisualizers;
using UniversalExpressionParser.ExpressionItems;
using UniversalExpressionParser.Tests.TestStatistics;

namespace UniversalExpressionParser.Tests.Utilities.ExpressionItemVisualizers
{
    public class ValueVisualizerFactoryForTests : ValueVisualizerFactory
    {
        /// <inheritdoc />
        public ValueVisualizerFactoryForTests([NotNull] IInterfacePropertyVisualizationHelper interfacePropertyVisualizationHelper, [NotNull] IAttributeValueSanitizer attributeValueSanitizer) : base(interfacePropertyVisualizationHelper, attributeValueSanitizer)
        {
        }

        public override IClassVisualizer CreateInterfaceVisualizer(IObjectVisualizationContext objectVisualizationContext, object visualizedObject, string visualizedElementName, Type mainInterfaceType, bool addChildren)
        {
            if (visualizedObject is ITextItemStatistics textItemStatistics)
                return new TextItemStatisticsVisualizer(ValueVisualizerDependencyObjects, objectVisualizationContext, textItemStatistics, visualizedElementName, mainInterfaceType, addChildren);

            if (visualizedObject is ITextItemStatisticGroup textItemStatisticGroup)
                return new TestStatisticGroupVisualizer<ITextItem>(ValueVisualizerDependencyObjects, objectVisualizationContext, textItemStatisticGroup, visualizedElementName);

            if (visualizedObject is ITextItemStatistic textItemStatistic)
                return new TestStatisticVisualizer<ITextItem>(ValueVisualizerDependencyObjects, objectVisualizationContext, textItemStatistic, visualizedElementName);

            if (visualizedObject is IOperatorExpressionItem operatorExpressionItem)
                return new OperatorExpressionItemVisualizerForTests(ValueVisualizerDependencyObjects, objectVisualizationContext, operatorExpressionItem, visualizedElementName, mainInterfaceType, addChildren);
            
            return base.CreateInterfaceVisualizer(objectVisualizationContext, visualizedObject, visualizedElementName, mainInterfaceType, addChildren);
        }
    }
}