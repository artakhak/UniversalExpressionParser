using ClassVisualizer;
using JetBrains.Annotations;
using System;
using UniversalExpressionParser.ClassVisualizers;
using UniversalExpressionParser.ExpressionItems;

namespace UniversalExpressionParser.Tests.Utilities.ExpressionItemVisualizersForDemos
{
    internal class OperatorExpressionItemVisualizerForDemos : OperatorExpressionItemVisualizer
    {
        public OperatorExpressionItemVisualizerForDemos([NotNull] IValueVisualizerDependencyObjects valueVisualizerDependencyObjects, [NotNull] IObjectVisualizationContext objectVisualizationContext, [NotNull] IOperatorExpressionItem visualizedOperatorExpressionItem, [NotNull] string visualizedElementName, [NotNull] Type mainInterfaceType, bool addChildren) : base(valueVisualizerDependencyObjects, objectVisualizationContext, visualizedOperatorExpressionItem, visualizedElementName, mainInterfaceType, addChildren)
        {
        }

    }
}
