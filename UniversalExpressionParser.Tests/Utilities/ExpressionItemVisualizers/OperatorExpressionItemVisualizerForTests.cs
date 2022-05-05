using ClassVisualizer;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using UniversalExpressionParser.ClassVisualizers;
using UniversalExpressionParser.ExpressionItems;

namespace UniversalExpressionParser.Tests.Utilities.ExpressionItemVisualizers
{
    public class OperatorExpressionItemVisualizerForTests: OperatorExpressionItemVisualizer
    {
        /// <inheritdoc />
        public OperatorExpressionItemVisualizerForTests([NotNull] IValueVisualizerDependencyObjects valueVisualizerDependencyObjects, [NotNull] IObjectVisualizationContext objectVisualizationContext, [NotNull] IOperatorExpressionItem visualizedOperatorExpressionItem, [NotNull] string visualizedElementName, [CanBeNull] Type mainInterfaceType, bool addChildren) : base(valueVisualizerDependencyObjects, objectVisualizationContext, visualizedOperatorExpressionItem, visualizedElementName, mainInterfaceType, addChildren)
        {
        }

        protected override (IList<IInterfacePropertyInfo> interfacePropertiesWithNoCategory, IList<IPropertyCategory> propertyCategories) GetVisualizedProperties()
        {
            var visualizedProperties = base.GetVisualizedProperties();

            visualizedProperties.interfacePropertiesWithNoCategory.Add(new InterfacePropertyInfo(
                "PriorityOrder", typeof(OperatorPriority), PropertyVisualizationType.VisualizePropertyOnlyInAttribute,
                OperatorPriorities.GetPriority(this.VisualizedExpressionItem.OperatorInfoExpressionItem.OperatorInfo.Priority)));

            return visualizedProperties;
        }
    }
}
