namespace UniversalExpressionParser.ClassVisualizers
{
    /// <summary>
    /// Extensions for <see cref="OperatorType"/>
    /// </summary>
    public static class OperatorTypeExtensionMethods
    {
        /// <summary>
        /// Returns display value for <see cref="OperatorType"/>
        /// </summary>
        public static string GetDisplayValue(this OperatorType operatorType, bool startWithUpperCase)
        {
            switch (operatorType)
            {
                case OperatorType.BinaryOperator:
                    return $"{(startWithUpperCase ? 'B' : 'b')}inary";
                case OperatorType.PostfixUnaryOperator:
                    return $"{(startWithUpperCase ? 'P' : 'p')}ostfix";
                case OperatorType.PrefixUnaryOperator:
                    return $"{(startWithUpperCase ? 'P' : 'p')}refix";
                default:
                    return "NA";
            }
        }
    }
}