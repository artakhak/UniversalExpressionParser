using ClassVisualizer;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using TestsSharedLibrary.ClassVisualizers;
using UniversalExpressionParser.ExpressionItems;
using UniversalExpressionParser.Tests.TestStatistics;

namespace UniversalExpressionParser.Tests.Utilities.ExpressionItemVisualizers
{
    public class TextItemStatisticsVisualizer : TestStatisticsVisualizer<ITextItem>
    {
        [NotNull]
        private readonly ITextItemStatistics _textItemStatistics;

        /// <inheritdoc />
        public TextItemStatisticsVisualizer([NotNull] IValueVisualizerDependencyObjects valueVisualizerDependencyObjects, 
                                            [NotNull] IObjectVisualizationContext objectVisualizationContext, 
                                            [NotNull] ITextItemStatistics textItemStatistics, 
                                            [NotNull] string visualizedElementName, 
                                            [NotNull] Type mainInterfaceType, bool addChildren) : 
            base(valueVisualizerDependencyObjects, objectVisualizationContext, textItemStatistics, visualizedElementName, mainInterfaceType, addChildren)
        {
            _textItemStatistics = textItemStatistics;
        }

        protected override (IList<IInterfacePropertyInfo> interfacePropertiesWithNoCategory, IList<IPropertyCategory> propertyCategories) GetVisualizedProperties()
        {
            var visualizedProperties = base.GetVisualizedProperties();

            var allExpressionTypesStatistic = _textItemStatistics.TestStatisticsCollection.FirstOrDefault(x => x.StatisticName == StatisticGroupNames.AllExpressionTypes);

            if (allExpressionTypesStatistic != null && allExpressionTypesStatistic is ITextItemStatisticGroup textItemStatisticGroup)
            {
                var parsedCodeCounterStatistics = textItemStatisticGroup.ChildStatisticsCollection.FirstOrDefault(x =>
                    x is ParsedCodeCounterStatistics);

                if (parsedCodeCounterStatistics != null)
                {
                    var averageNumberOfExpressionItems = (_textItemStatistics.Counter / (double)parsedCodeCounterStatistics.Counter);

                    visualizedProperties.interfacePropertiesWithNoCategory.Add(
                        new InterfacePropertyInfo("AverageNumberOfExpressionItemsPerSimulationIteration", typeof(string),
                            PropertyVisualizationType.VisualizePropertyOnlyInAttribute,
                            Math.Round(averageNumberOfExpressionItems, 6).ToString(CultureInfo.InvariantCulture)));
                }
            }
          
            return visualizedProperties;
        }
    }
}