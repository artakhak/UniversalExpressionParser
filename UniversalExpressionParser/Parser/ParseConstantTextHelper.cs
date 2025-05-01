// Copyright (c) UniversalExpressionParser Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;
using UniversalExpressionParser.ExpressionItems;

namespace UniversalExpressionParser.Parser
{
    internal class ParseConstantTextHelper
    {
        [NotNull] private readonly ParserHelper _parserHelper;

        internal ParseConstantTextHelper([NotNull] ParserHelper parserHelper)
        {
            _parserHelper = parserHelper;
        }

        [CanBeNull]
        internal IConstantTextExpressionItem TryParseConstantTextValueExpressionItem([NotNull] ParseExpressionItemContext context,
                                                                                [CanBeNull] IComplexExpressionItem potentialPrefixExpressionItem,
                                                                                [NotNull][ItemNotNull] IEnumerable<IKeywordExpressionItem> keywordExpressionItems)
        {
            var textSymbolsParser = context.TextSymbolsParser;

            var expressionLanguageProvider = context.ExpressionLanguageProviderWrapper.ExpressionLanguageProvider;

            foreach (var textEnclosingChar in expressionLanguageProvider.ConstantTextStartEndMarkerCharacters)
            {
                if (textSymbolsParser.CurrentChar == textEnclosingChar)
                {
                    var cSharpText = new StringBuilder();

                    var positionInText = textSymbolsParser.PositionInText + 1;
                    var indexOfTextClosingCharacter = -1;

                    while (positionInText < textSymbolsParser.ParsedTextEnd)
                    {
                        var currentChar = textSymbolsParser.TextToParse[positionInText];

                        if (currentChar == textEnclosingChar)
                        {
                            if (positionInText < textSymbolsParser.ParsedTextEnd - 1)
                            {
                                var nextCharacter = textSymbolsParser.TextToParse[positionInText + 1];
                                if (nextCharacter == textEnclosingChar)
                                {
                                    cSharpText.Append(currentChar);
                                    positionInText += 2;
                                    continue;
                                }
                            }

                            indexOfTextClosingCharacter = positionInText;
                            break;
                        }

                        cSharpText.Append(currentChar);
                        ++positionInText;
                    }
                    

                    int constantLength;

                    if (indexOfTextClosingCharacter < 0)
                    {
                        constantLength = textSymbolsParser.ParsedTextEnd - textSymbolsParser.PositionInText;
                        context.AddParseErrorItem(new ParseErrorItem(
                            textSymbolsParser.PositionInText, () => $"Constant text closing character '{textEnclosingChar}' is missing.",
                            ParseErrorItemCode.ConstantTextNotClosed));
                    }
                    else
                    {
                        constantLength = indexOfTextClosingCharacter + 1 - textSymbolsParser.PositionInText;
                    }

                    var constantTextExpressionItem = new ConstantTextExpressionItem(_parserHelper.ConvertLastExpressionItemToPrefixes(potentialPrefixExpressionItem),
                        keywordExpressionItems, new ConstantTextValueExpressionItem(
                            textSymbolsParser.TextToParse.Substring(textSymbolsParser.PositionInText, constantLength), cSharpText.ToString(),
                            textSymbolsParser.PositionInText));

                    textSymbolsParser.SkipCharacters(constantLength);

                    return constantTextExpressionItem;
                }
            }

            return null;
        }
    }
}
