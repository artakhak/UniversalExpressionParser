// Copyright (c) UniversalExpressionParser Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using JetBrains.Annotations;

namespace UniversalExpressionParser.Parser
{
    internal class ErrorsHelper
    {
        internal void AddNoSeparationBetweenSymbolsError([NotNull] ParseExpressionItemContext context, ParseExpressionItemData parseExpressionItemData, int errorIndexInParsedText)
        {
            parseExpressionItemData.OperatorsCannotBeEvaluated = true;
            context.AddParseErrorItem(new ParseErrorItem(
                errorIndexInParsedText, () => ExpressionParserMessages.NoSeparationBetweenSymbolsError,
                ParseErrorItemCode.NoSeparationBetweenSymbols));
        }
    }
}
