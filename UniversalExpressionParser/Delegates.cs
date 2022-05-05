// Copyright (c) UniversalExpressionParser Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using JetBrains.Annotations;
using TextParser;

namespace UniversalExpressionParser
{
    /// <summary>
    /// Returns true if the parsed character marks the end of expression.
    /// </summary>
    /// <param name="parseExpressionItemContext">
    /// Context object for the item currently being processed.
    /// The value can be null.
    /// Use the values in <see cref="IParseExpressionItemContext"/> such as <see cref="ITextSymbolsParserState.TextToParse"/> in <see cref="IParseExpressionItemContext.TextSymbolsParser"/> to get parse contextual information.
    /// </param>
    /// <param name="character">Character at current position</param>
    /// <param name="characterIndex">Character index.</param>
    public delegate bool IsParsingCompleteDelegate([CanBeNull] IParseExpressionItemContext parseExpressionItemContext, char character, int characterIndex);
    
    /// <summary>
    /// Executed when event <see cref="ParseErrorAddedEventArgs"/> is added. Used in <see cref="IParseExpressionItemContext.ParseErrorAddedEvent"/> event.
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="e">Event with details on added instance of <see cref="IParseErrorItem"/>.</param>
    public delegate void ParseErrorAddedDelegate(object sender, ParseErrorAddedEventArgs e);
}
