using ClassVisualizer;
using JetBrains.Annotations;
using System;
using UniversalExpressionParser.ExpressionItems;

namespace UniversalExpressionParser.ClassVisualizers
{
    public class ParseExpressionResultVisualizer : InterfaceVisualizer
    {
        /// <inheritdoc />
        public ParseExpressionResultVisualizer([NotNull] IValueVisualizerDependencyObjects valueVisualizerDependencyObjects, [NotNull] IObjectVisualizationContext objectVisualizationContext, [NotNull] object visualizedObject, [NotNull] string visualizedElementName, [NotNull] Type mainInterfaceType, bool addChildren) : base(valueVisualizerDependencyObjects, objectVisualizationContext, visualizedObject, visualizedElementName, mainInterfaceType, addChildren)
        {
        }

        protected override bool PropertyShouldBeIgnoredVirtual(string propertyName)
        {
            if (base.PropertyShouldBeIgnoredVirtual(propertyName))
                return true;

            if (propertyName == nameof(IParseExpressionResult.ParsedExpression))
                return true;

            return false;
        }
    }
}