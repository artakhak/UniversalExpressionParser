using ClassVisualizer;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using UniversalExpressionParser.ExpressionItems;

namespace UniversalExpressionParser.ClassVisualizers
{
    public class ValueVisualizerFactory : ValueVisualizerFactoryBase
    {
        /// <inheritdoc />
        public ValueVisualizerFactory([NotNull] IInterfacePropertyVisualizationHelper interfacePropertyVisualizationHelper, [NotNull] IAttributeValueSanitizer attributeValueSanitizer) : base(interfacePropertyVisualizationHelper, attributeValueSanitizer)
        {
        }

        /// <inheritdoc />
        public override IClassVisualizer CreateInterfaceVisualizer(IObjectVisualizationContext objectVisualizationContext,
                                                                   object visualizedObject,
                                                                   string visualizedElementName,
                                                                   Type mainInterfaceType, bool addChildren)
        {
            if (visualizedObject is IParseExpressionResult)
                return new ParseExpressionResultVisualizer(ValueVisualizerDependencyObjects, objectVisualizationContext, visualizedObject, visualizedElementName, mainInterfaceType, addChildren);

            if (visualizedObject is IExpressionItemBase expressionItemBase)
            {
                mainInterfaceType = ExpressionItemBaseExtensions.GetMainInterface(expressionItemBase);

                if (visualizedObject is IOperatorExpressionItem operatorExpressionItem)
                    return new OperatorExpressionItemVisualizer(ValueVisualizerDependencyObjects, objectVisualizationContext, operatorExpressionItem, visualizedElementName, mainInterfaceType, addChildren);

                if (visualizedObject is IOperatorInfoExpressionItem operatorInfoExpressionItem)
                    return new OperatorInfoExpressionItemVisualizer(ValueVisualizerDependencyObjects, objectVisualizationContext, operatorInfoExpressionItem, visualizedElementName, mainInterfaceType, addChildren);

                if (visualizedObject is IKeywordExpressionItem keywordExpressionItem)
                    return new KeywordExpressionItemVisualizer(ValueVisualizerDependencyObjects, objectVisualizationContext, keywordExpressionItem, visualizedElementName, mainInterfaceType, addChildren);

                #region Literals
                if (visualizedObject is ILiteralNameExpressionItem literalNameExpressionItem)
                    return new ExpressionItemVisualizer<IExpressionItemBase>(ValueVisualizerDependencyObjects, objectVisualizationContext, literalNameExpressionItem, visualizedElementName, mainInterfaceType, addChildren,
                        new[]
                        {
                        new KeyValuePair<string, string>(nameof(ILiteralNameExpressionItem.Text), literalNameExpressionItem.Text)
                        });

                if (visualizedObject is ILiteralExpressionItem literalExpressionItem)
                    return new ExpressionItemVisualizer<IExpressionItemBase>(ValueVisualizerDependencyObjects, objectVisualizationContext, literalExpressionItem, visualizedElementName, mainInterfaceType, addChildren,
                        new[]
                        {
                        new KeyValuePair<string, string>($"{nameof(ILiteralExpressionItem.LiteralName)}.{nameof(ILiteralNameExpressionItem.Text)}",
                            literalExpressionItem.LiteralName.Text)
                        });
                #endregion

                #region Numeric values
                if (visualizedObject is INumericExpressionValueItem numericExpressionValueItem)
                    return new ExpressionItemVisualizer<IExpressionItemBase>(ValueVisualizerDependencyObjects, objectVisualizationContext, numericExpressionValueItem, visualizedElementName, mainInterfaceType, addChildren,
                        new[]
                        {
                        new KeyValuePair<string, string>(nameof(INumericExpressionValueItem.NumericValue), numericExpressionValueItem.NumericValue)
                        });

                if (visualizedObject is INumericExpressionItem numericExpressionItem)
                    return new ExpressionItemVisualizer<IExpressionItemBase>(ValueVisualizerDependencyObjects, objectVisualizationContext, numericExpressionItem, visualizedElementName, mainInterfaceType, addChildren,
                        new[]
                        {
                        new KeyValuePair<string, string>($"{nameof(INumericExpressionItem.Value)}.{nameof(INumericExpressionValueItem.NumericValue)}",
                            numericExpressionItem.Value.NumericValue)
                        });
                #endregion

                #region Constant texts
                if (visualizedObject is IConstantTextValueExpressionItem constantTextValueExpression)
                    return new ExpressionItemVisualizer<IExpressionItemBase>(ValueVisualizerDependencyObjects, objectVisualizationContext, constantTextValueExpression, visualizedElementName, mainInterfaceType, addChildren,
                        new[]
                        {
                        new KeyValuePair<string, string>(nameof(IConstantTextValueExpressionItem.Text), constantTextValueExpression.Text),
                        new KeyValuePair<string, string>(nameof(IConstantTextValueExpressionItem.CSharpText), constantTextValueExpression.CSharpText)
                        });

                if (visualizedObject is IConstantTextExpressionItem constantTextExpressionItem)
                    return new ExpressionItemVisualizer<IExpressionItemBase>(ValueVisualizerDependencyObjects, objectVisualizationContext, constantTextExpressionItem, visualizedElementName, mainInterfaceType, addChildren,
                        new[]
                        {
                        new KeyValuePair<string, string>($"{nameof(IConstantTextExpressionItem.TextValue)}.{nameof(IConstantTextValueExpressionItem.Text)}",
                            constantTextExpressionItem.TextValue.Text),
                        new KeyValuePair<string, string>($"{nameof(IConstantTextExpressionItem.TextValue)}.{nameof(IConstantTextValueExpressionItem.CSharpText)}",
                            constantTextExpressionItem.TextValue.Text)
                        });
                #endregion

                if (visualizedObject is IBracesExpressionItem bracesExpressionItem)
                {
                    var attributeNameValuePairsToShowAsAttributes = new List<KeyValuePair<string, string>>();

                    if (bracesExpressionItem.NameLiteral != null)
                        attributeNameValuePairsToShowAsAttributes.Add(
                            new KeyValuePair<string, string>(
                                $"{nameof(IBracesExpressionItem.NameLiteral)}.{nameof(ILiteralExpressionItem.LiteralName)}.{nameof(ILiteralNameExpressionItem.Text)}",
                                bracesExpressionItem.NameLiteral.LiteralName.Text)
                        );

                    return new ExpressionItemVisualizer<IExpressionItemBase>(ValueVisualizerDependencyObjects, objectVisualizationContext, bracesExpressionItem, visualizedElementName, mainInterfaceType, addChildren,
                        attributeNameValuePairsToShowAsAttributes);
                }

                if (visualizedObject is ITextExpressionItem textExpressionItem)
                    return new ExpressionItemVisualizer<IExpressionItemBase>(ValueVisualizerDependencyObjects, objectVisualizationContext, textExpressionItem, visualizedElementName, mainInterfaceType, addChildren,
                        new[]
                        {
                        new KeyValuePair<string, string>(nameof(ITextExpressionItem.Text), textExpressionItem.Text)
                        });

                return new ExpressionItemVisualizer<IExpressionItemBase>(ValueVisualizerDependencyObjects, objectVisualizationContext, expressionItemBase, visualizedElementName, mainInterfaceType, addChildren);
            }

            return base.CreateInterfaceVisualizer(objectVisualizationContext, visualizedObject, visualizedElementName, mainInterfaceType, addChildren);
        }
    }
}