namespace UniversalExpressionParser.ClassVisualizers
{
    public class ExpressionItemVisualizerSettings : IExpressionItemVisualizerSettings
    {
        /// <summary>
        /// Default constructor necessary in AmbientContext.
        /// </summary>
        public ExpressionItemVisualizerSettings()
        {
            
        }

        public ExpressionItemVisualizerSettings(bool renderOtherPropertiesSection, bool minimizeOutput)
        {
            RenderOtherPropertiesSection = renderOtherPropertiesSection;
            MinimizeOutput = minimizeOutput;
        }

        public bool RenderOtherPropertiesSection { get; } = true;
        public bool MinimizeOutput { get; } = true;
    }
}