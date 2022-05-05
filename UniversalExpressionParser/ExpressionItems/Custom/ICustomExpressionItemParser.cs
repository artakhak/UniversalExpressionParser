// Copyright (c) UniversalExpressionParser Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using TextParser;

namespace UniversalExpressionParser.ExpressionItems.Custom
{
    /// <summary>
    /// Custom expression parser.
    /// Custom expression parsers allow to plugin into parsing process and provide special parsing of some portion of the parsed expression.
    /// To add custom expression parser make sure it is included in list <see cref="IExpressionLanguageProvider.CustomExpressionItemParsers"/>.
    /// The default implementation of <see cref="ICustomExpressionItem"/> that can be used in most cases is <see cref="AggregateCustomExpressionItemParser"/>.
    /// </summary>
    public interface ICustomExpressionItemParser
    {
        /// <summary>
        /// Tries to parse a custom expression item <see cref="ICustomExpressionItem"/> at current position.
        /// Current position in text can be retrieved from parameter <paramref name="context"/> in property <see cref="IParseExpressionItemContext.TextSymbolsParser"/>.<see cref="ITextSymbolsParserState.PositionInText"/>.
        /// If null is returned, the value of <see cref="IParseExpressionItemContext.TextSymbolsParser"/>.PositionInText should be unchanged.
        /// Otherwise, the position should be an index in text after the parsed custom expression item.
        /// The recommended implementation of <see cref="ICustomExpressionItem"/> that the parser can return is either <see cref="CustomExpressionItem"/>, or a subclass of this class.
        /// The parser <see cref="IExpressionParser"/> executes <see cref="ICustomExpressionItemParser.TryParseCustomExpressionItem(IParseExpressionItemContext, IReadOnlyList{IExpressionItemBase}, IReadOnlyList{IKeywordExpressionItem})"/> for each parse rregistered
        /// in <see cref="IExpressionLanguageProvider.CustomExpressionItemParsers"/>, and uses the first non-null value <see cref="ICustomExpressionItem"/> returned by some parser <see cref="ICustomExpressionItemParser"/>.
        /// The custom expression item parser might chose to add errors when evaluating the expression and parameters <paramref name="parsedPrefixExpressionItems"/> and <paramref name="parsedKeywordExpressionItems"/>.
        /// The implementation of this interface can use the method <see cref="IParseExpressionItemContext.AddParseErrorItem"/> in <paramref name="context"/> to add parse errors.
        /// The parser <see cref="IExpressionParser"/> will evaluate the added errors and the result of this method to determine if it should try other custom expression item parsers, or if it should stop parsing the rest of the expression.
        /// If the parser adds an error of type <see cref="IParseErrorItem"/> (regardless if the the returned value is null or not), <see cref="IExpressionParser"/> will not try other custom expression parsers in <see cref="IExpressionLanguageProvider.CustomExpressionItemParsers"/>.
        /// If the value of <see cref="IParseErrorItem.IsCriticalError"/> is true for any added errors, the parser <see cref="IExpressionParser"/> will stop parsing the rest of expression as well.
        /// See at examples in projects "UniversalExpressionParser.Tests" and "UniversalExpressionParser.DemoExpressionLanguageProviders" in repository https://github.com/artakhak/UniversalExpressionParser for examples.
        /// </summary>
        /// <param name="context">Stores context data at current position.
        /// use the helpers in <see cref="IParseExpressionItemContext"/> (in parameter <paramref name="context"/>), such as <see cref="IParseExpressionItemContext.ParseCodeBlockExpression"/>, <see cref="IParseExpressionItemContext.ParseBracesExpression"/>,
        /// <see cref="IParseExpressionItemContext.TryParseSymbol"/>, <see cref="IParseExpressionItemContext.SkipSpacesAndComments()"/> and others to parse text current position.
        /// See at examples in projects "UniversalExpressionParser.Tests" and "UniversalExpressionParser.DemoExpressionLanguageProviders" in repository https://github.com/artakhak/UniversalExpressionParser for examples
        /// </param>
        /// <param name="parsedPrefixExpressionItems">
        /// Parsed prefixes not yet added to any expression item yet. These prefixes can be added to <see cref="ICustomExpressionItem"/>.<see cref="IComplexExpressionItem.Prefixes"/> in
        /// returned parsed instance of <see cref="ICustomExpressionItem"/>, or the parser might chose no add the prefixes to <see cref="ICustomExpressionItem"/>.
        /// The implementation might chose to add an error <see cref="IParseErrorItem"/> with <see cref="IParseErrorItem.ParseErrorItemCode"/> equal to <see cref="ParseErrorItemCode.InvalidUseOfPrefixes"/>, if the evaluated prefixes are not valid for the custom expression item.
        /// Also, the implementation can report an error with any other value for <see cref="IParseErrorItem.ParseErrorItemCode"/>, including error codes not in <see cref="ParseErrorItemCode"/>.
        /// </param>
        /// <param name="parsedKeywordExpressionItems">
        /// Parsed keywords not yet added to any expression item yet. These keywords can be added to <see cref="ICustomExpressionItem"/>.<see cref="IComplexExpressionItem.AppliedKeywords"/> in
        /// returned parsed instance of <see cref="ICustomExpressionItem"/>, or the parser might chose not to add the keywords to <see cref="ICustomExpressionItem"/>.
        /// The implementation might chose to add an error <see cref="IParseErrorItem"/> with <see cref="IParseErrorItem.ParseErrorItemCode"/> equal to <see cref="ParseErrorItemCode.InvalidUseOfKeywords"/>, if the evaluated keywords are not valid for the custom expression item. 
        /// Also, the implementation can report an error with any other value for <see cref="IParseErrorItem.ParseErrorItemCode"/>, including error codes not in <see cref="ParseErrorItemCode"/>.
        /// <see cref="IParseExpressionItemContext.ParseErrorData"/>.
        /// </param>
        /// <returns>Returns either a null, or an instance of <see cref="ICustomExpressionItem"/> that stores the
        /// parsed custom expression data.
        /// </returns>
        /// <exception cref="Exception">The implementation might chose to throw an exception, in which case the parser <see cref="IExpressionParser"/> will log an error and will add an error <see cref="IParseErrorItem"/> with <see cref="IParseErrorItem.ParseErrorItemCode"/> equal to <see cref="ParseErrorItemCode.CustomExpressionParserThrewAnException"/></exception>
        [CanBeNull]
        ICustomExpressionItem TryParseCustomExpressionItem([NotNull] IParseExpressionItemContext context,
                                                           [NotNull, ItemNotNull] IReadOnlyList<IExpressionItemBase> parsedPrefixExpressionItems,
                                                           [NotNull, ItemNotNull] IReadOnlyList<IKeywordExpressionItem> parsedKeywordExpressionItems);
    }
}