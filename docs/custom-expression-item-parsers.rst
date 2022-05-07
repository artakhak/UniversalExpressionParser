==============================
Custom Expression Item Parsers
==============================

.. contents::
   :local:
   :depth: 2

Custom expression parsers allow to plugin into parsing process and provide special parsing of some portion of the parsed expression. 

The expression parser (i.e., **UniversalExpressionParser.IExpressionParser**) iteratively parses keywords (see the section above on keywords), before parsing any other symbols.

Then the expression parser loops through all the custom expression parsers of type **UniversalExpressionParser.ExpressionItems.Custom.ICustomExpressionItemParser** in property **CustomExpressionItemParsers** in interface **UniversalExpressionParser.IExpressionLanguageProvider** passed to the parser, and for each custom expression parser executes the method
 **ICustomExpressionItem ICustomExpressionItemParser.TryParseCustomExpressionItem(IParseExpressionItemContext context, IReadOnlyList&lt;IExpressionItemBase&gt; parsedPrefixExpressionItems, IReadOnlyList&lt;IKeywordExpressionItem&gt; keywordExpressionItems)**.

If method call **TryParseCustomExpressionItem(...)** returns non-null value of type **UniversalExpressionParser.ExpressionItems.Custom.ICustomExpressionItem**, the parser uses the parsed custom expression item.

Otherwise, if **TryParseCustomExpressionItem(...)** returns null, the parser tries to parse a non custom expression item at current position (i.e., operators, a literal, function, code block, etc.). 

Interface **ICustomExpressionItem** has a property **UniversalExpressionParser.ExpressionItems.Custom.CustomExpressionItemCategory** which is used by the parser to determine if the parsed custom expression item should be used as
 - a prefix for subsequently parsed regular expression (i.e., literal, function, braces, etc.).
 - should be treated as regular expression (which can be part of operators, function parameter, etc.).
 - or should be used as a postfix for the previously parsed expression item.

In the example below the parser parses "::pragma x" to a regular custom expression item of type **UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.PragmaCustomExpressionItem** (i.e., the value of **CustomExpressionItemCategory property** in **UniversalExpressionParser.ExpressionItems.Custom.ICustomExpressionItem** is equal to **UniversalExpressionParser.ExpressionItems.Custom.CustomExpressionItemCategory.Regular**).
As a result, the expression "::pragma x+y;" below is logically similar to "(::pragma x)+y;"

In a similar manner the expression "::types[T1, T2]" is parsed to a prefix custom expression item of type **UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.GenericTypesCustomExpressionItem** by custom expression item parser **UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.GenericTypesExpressionItemParser**.

The custom expression item parsed from "::types[T1, T2]" is added as a prefix to an expression item parsed from **F1(x:T1, y:T2)**.

Also, the expression "where T1:int where T2:double whereend" is parsed to postfix custom expression item of type **UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.WhereCustomExpressionItem** by custom expression parser **UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.WhereCustomExpressionItemParserForTests**.

The parser adds the parsed custom expression as a postfix to the preceding regular expression item parsed from text "F1(x:T1, y:T2)".

In this example, the code block after "whereend" (the expression "{...}") is parsed as a postfix expression item of type **UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem** and is added as a postfix to regular expression item parsed from "F1(x:T1, y:T2)" as well, since the parser adds all the prefixes/postfixes to regular expression item it finds after/before the prefixes/postfixes. 

.. sourcecode::
     :linenos:
     
     ::pragma x+y;
     ::types[T1,T2] F1(x:T1, y:T2) where T1:int where T2:double whereend
     {
     	// This code block will be added as a postfix to expression item parsed from "F1(x:T1, y:T2)".
     }


.. raw:: html

    <a href="https://github.com/artakhak/UniversalExpressionParser/blob/main/UniversalExpressionParser.Tests/Demos/DemoExpressions/CustomExpressionItemParsers/SimpleCustomExpressionItems.parsed"><p class="codeSnippetRefText">Click here to see the visualized instance of UniversalExpressionParser.IParseExpressionResult</p></a>

- This is another example demonstrating that the parsed expression can have multiple prefix and postfix custom expressions items applied to the same regular expression item parsed from "F1(x:T1, y:T2, z:T3)".

.. sourcecode::
     :linenos:
     
     // The expression below ("::metadata {...}") is parsed to a prefix custom expression item and added to list of prefixes of regular
     // expression item parsed from F1(x:T1, y:T2, z:T3) 
     ::metadata {description: "F1 demoes regular function expression item to which multiple prefix and postfix custom expression items are added."}

     // ::types[T1,T2] is also parsed to a prefix custom expression item and added to list of prefixes of regular
     // expression item parsed from F1(x:T1, y:T2, z:T3) 
     ::types[T1,T2]
     F1(x:T1, y:T2, z:T3) 

     // The postfix custom expression item parsed from "where T1:int where T2:double whereend" is added to list of postfixes of regular expression 
     // parsed from "F1(x:T1, y:T2, z:T3)".
     where T1:int,class where T2:double whereend 

     // The postfix custom expression item parsed from "where T3 : T1 whereend " is also added to list of postfixes of regular expression 
     // parsed from "F1(x:T1, y:T2, z:T3)".
     where T3 : T1 whereend 
     {
        // This code block will be added as a postfix to expression item parsed from "F1(x:T1, y:T2, z:T3)".
     }

.. raw:: html

    <a href="https://github.com/artakhak/UniversalExpressionParser/blob/main/UniversalExpressionParser.Tests/Demos/DemoExpressions/CustomExpressionItemParsers/MultipleAdjacentPrefixPostfixCustomExpressionItems.parsed"><p class="codeSnippetRefText">Click here to see the visualized instance of UniversalExpressionParser.IParseExpressionResult</p></a>

Implementing Custom Expression Parsers
======================================

For examples of custom expression item parsers look at some examples in demo project **UniversalExpressionParser.DemoExpressionLanguageProviders**.

The following demo implementations of **UniversalExpressionParser.ExpressionItems.Custom.ICustomExpressionItemParserByKeywordId** might be useful when implementing custom expression parses: 

- UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.WhereCustomExpressionItemParserBase
- UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.PragmaCustomExpressionItemParser
- UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.MetadataCustomExpressionItemParser

Also, these custom expression parser implementations demonstrate how to use the helper class **UniversalExpressionParser.IParseExpressionItemContext** that is passed as a parameter to 
method **DoParseCustomExpressionItem(IParseExpressionItemContext context,...)** in **UniversalExpressionParser.ExpressionItems.Custom.CustomExpressionItemParserByKeywordId** to parse the text at current position, as well as how to report errors, if any.

- To add a new custom expression parser, one needs to implement an interface **UniversalExpressionParser.ExpressionItems.Custom.ICustomExpressionItemParser** and make sure the property **CustomExpressionItemParsers** in interface **UniversalExpressionParser.IExpressionLanguageProvider** includes an instance of the implemented parser class.

- In most cases the default implementation **UniversalExpressionParser.ExpressionItems.Custom.AggregateCustomExpressionItemParser** of **UniversalExpressionParser.ExpressionItems.Custom.ICustomExpressionItemParser** can be used to initialize the list of all custom expression parers that will be used by **Universal Expression Parser**.
**UniversalExpressionParser.ExpressionItems.Custom.AggregateCustomExpressionItemParser** has a dependency on **IEnumerable&lt;ICustomExpressionItemParserByKeywordId&gt;** (injected into constructor).

- Using a single instance of **AggregateCustomExpressionItemParser** in property **CustomExpressionItemParsers** in interface **UniversalExpressionParser.IExpressionLanguageProvider** instead of multiple custom expression parsers in this property improves the performance.
**AggregateCustomExpressionItemParser** keeps internally a mapping from keyword Id to all the instances of **UniversalExpressionParser.ExpressionItems.Custom.ICustomExpressionItemParserByKeywordId** injected in constructor. When the parser executes the method **TryParseCustomExpressionItem(...,IReadOnlyList<IKeywordExpressionItem> parsedKeywordExpressionItems,...)** in interface **UniversalExpressionParser.ExpressionItems.Custom**, the custom expression item parser of type **AggregateCustomExpressionItemParser** evaluates the last keyword in list in parameter **parsedKeywordExpressionItems** to retrieve all the parsers mapped to this keyword Id, to try to parse a custom expression item using only those custom expression item parsers. 

- Below is some of the code from classes **AggregateCustomExpressionItemParser** and **ICustomExpressionItemParserByKeywordId**.

.. sourcecode:: csharp
     :linenos:     

     namespace UniversalExpressionParser.ExpressionItems.Custom;
     
     public class AggregateCustomExpressionItemParser : ICustomExpressionItemParser
     {
         public AggregateCustomExpressionItemParser(
             IEnumerable<ICustomExpressionItemParserByKeywordId> customExpressionItemParsers)
         {
             ...
         }
     
         public ICustomExpressionItem TryParseCustomExpressionItem(IParseExpressionItemContext context,
                 IReadOnlyList<IExpressionItemBase> parsedPrefixExpressionItems,
                 IReadOnlyList<IKeywordExpressionItem> parsedKeywordExpressionItems)
         {
             ...
         }   
     }

     public interface ICustomExpressionItemParserByKeywordId
     {
         long ParsedKeywordId { get; }
     
         ICustomExpressionItem TryParseCustomExpressionItem(IParseExpressionItemContext context,
                 IReadOnlyList<IExpressionItemBase> parsedPrefixExpressionItems,
                 IReadOnlyList<IKeywordExpressionItem> parsedKeywordExpressionItemsWithoutLastKeyword,
                 IKeywordExpressionItem lastKeywordExpressionItem);
     }

- Here is the code from demo custom expression item parser **UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.PragmaCustomExpressionItemParser** 

.. sourcecode:: csharp
     :linenos:
     
     using System.Collections.Generic;
     using UniversalExpressionParser.ExpressionItems;
     using UniversalExpressionParser.ExpressionItems.Custom;

     namespace UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions
     {
         /// <summary>
         ///  Example: ::pragma x
         /// </summary>
         public class PragmaCustomExpressionItemParser : CustomExpressionItemParserByKeywordId
         {
             public PragmaCustomExpressionItemParser() : base(KeywordIds.Pragma)
             {
             }

             /// <inheritdoc />
             protected override ICustomExpressionItem DoParseCustomExpressionItem(IParseExpressionItemContext context, IReadOnlyList<IExpressionItemBase> parsedPrefixExpressionItems, 
                                                                                IReadOnlyList<IKeywordExpressionItem> parsedKeywordExpressionItemsWithoutLastKeyword,
                                                                                IKeywordExpressionItem pragmaKeywordExpressionItem)
             {
                 var pragmaKeywordInfo = pragmaKeywordExpressionItem.LanguageKeywordInfo;

                 var textSymbolsParser = context.TextSymbolsParser;

                 if (!context.SkipSpacesAndComments() || !context.TryParseSymbol(out var literalExpressionItem))
                 {
                     if (!context.ParseErrorData.HasCriticalErrors)
                     {
                         // Example: print("Is in debug mode=" + ::pragma IsDebugMode)
                         context.AddParseErrorItem(new ParseErrorItem(textSymbolsParser.PositionInText,
                             () => $"Pragma keyword '{pragmaKeywordInfo.Keyword}' should be followed with pragma symbol. Example: println(\"Is in debug mode = \" + {pragmaKeywordInfo.Keyword} IsDebug);",
                             CustomExpressionParseErrorCodes.PragmaKeywordShouldBeFollowedByValidSymbol));
                     }

                     return null;
                 }

                 return new PragmaCustomExpressionItem(parsedPrefixExpressionItems, parsedKeywordExpressionItemsWithoutLastKeyword,
                     pragmaKeywordExpressionItem,
                     new NameExpressionItem(literalExpressionItem, textSymbolsParser.PositionInText - literalExpressionItem.Length));
             }
         }
     }

- Below is the interface **IParseExpressionItemContext** that shows the helper methods and properties, as well as extension methods that can be used when parsing custom expressions.

.. sourcecode:: csharp
     :linenos:
     
     // Copyright (c) UniversalExpressionParser Project. All rights reserved.
     // Licensed under the MIT License. See LICENSE in the solution root for license information.

     using JetBrains.Annotations;
     using TextParser;
     using UniversalExpressionParser.ExpressionItems;

     namespace UniversalExpressionParser
     {
         /// <summary>
         /// Parser context data.
         /// </summary>
         public interface IParseExpressionItemContext
         {
             /// <summary>
             /// Event that is raised when new error is added by parser.
             /// </summary>
             event ParseErrorAddedDelegate ParseErrorAddedEvent;

             /// <summary>
             /// An instance of <see cref="IExpressionLanguageProviderWrapper"/> currently used by parser.
             /// </summary>
             [NotNull]
             IExpressionLanguageProviderWrapper ExpressionLanguageProviderWrapper { get; }

             /// <summary>
             /// Pare expression result.
             /// </summary>
             [NotNull]
             IParseExpressionResult ParseExpressionResult { get; }

             /// <summary>
             /// Error data. Use <see cref="ParseExpressionItemContext.AddParseErrorItem"/> to add errors.
             /// Default implementation of <see cref="IParseErrorItem"/> is <see cref="ParseErrorItem"/>.
             /// </summary>
             [NotNull]
             IParseErrorData ParseErrorData { get; }

             /// <summary>
             /// Text symbols parser that can be used to parse the symbols at current position.
             /// </summary>
             [NotNull]
             ITextSymbolsParser TextSymbolsParser { get; }

             /// <summary>
             /// Returns true, if parsing was completed before reaching end of text due to some condition met, and not due to critical error encountered.
             /// </summary>
             bool IsEarlyParseStopEncountered { get; }

             /// <summary>
             /// Tries to parse braces if the text at current position starts with either '(' or '['.
             /// If parsing is successful, the position after parsing will be after ')' or ']'
             /// </summary>
             /// <param name="literalExpressionItem">If the parameter is not null, the braces expression item will have a
             /// value <see cref="IBracesExpressionItem.NameLiteral"/> equal to <paramref name="literalExpressionItem"/>.
             /// </param>
             /// <exception cref="ParseTextException">Throws this exception if the text at current position <see cref="ITextSymbolsParserState.PositionInText"/> in
             /// of property <see cref="TextSymbolsParser"/> is not '(' or '['
             /// </exception>
             [NotNull]
             IBracesExpressionItem ParseBracesExpression([CanBeNull] ILiteralExpressionItem literalExpressionItem);

             /// <summary>
             /// Tries to parse an expression at current position to <see cref="ICodeBlockExpressionItem"/>, if text at current position starts with
             /// <see cref="IExpressionLanguageProvider.CodeBlockStartMarker"/>.
             /// If parsing is successful, the position after parsing will be after the code block end marker <see cref="IExpressionLanguageProvider.CodeBlockEndMarker"/>
             /// </summary>
             /// <exception cref="ParseTextException">Throws this exception if the text at current position at current position <see cref="ITextSymbolsParserState.PositionInText"/> in
             /// of property <see cref="TextSymbolsParser"/> is not block start marker <see cref="IExpressionLanguageProvider.CodeBlockStartMarker"/>.
             /// </exception>
             [NotNull]
             ICodeBlockExpressionItem ParseCodeBlockExpression();

             /// <summary>
             /// Skips comments and spaces. Comments will be added to <see cref="IParseExpressionResult.SortedCommentedTextData"/>.
             /// </summary>
             /// <returns>Returns true, if text end is not reached (i.e. the value of <see cref="ITextSymbolsParserState.PositionInText"/> of <see cref="TextSymbolsParser"/> is not past the
             /// parsed text end). Returns false otherwise.</returns>
             bool SkipSpacesAndComments();

             /// <summary>
             /// Tries to parse a symbol at current position. For example if parsed text is "var var1=8;", and the value
             /// of <see cref="ITextSymbolsParserState.PositionInText"/> of <see cref="TextSymbolsParser"/> is 4 (index of "var1"), then
             /// this method will return true, and the value of <paramref name="parsedLiteral"/> will be set to "var1".
             /// </summary>
             /// <param name="parsedLiteral">Parsed symbol. The value is null, if the returned value is false. Otherwise the value is not null.</param>
             /// <returns>Returns true if valid literal was parsed. Returns false otherwise.</returns>
             bool TryParseSymbol(out string parsedLiteral);

             /// <summary>
             /// Adds an error data.
             /// </summary>
             /// <param name="parseErrorItem">Code item error data.</param>
             void AddParseErrorItem([NotNull] IParseErrorItem parseErrorItem);
         }

         /// <summary>
         /// Extension methods for <see cref="IParseExpressionItemContext"/>
         /// </summary>
         public static class ParseExpressionItemContextExtensionMethods
         {
             /// <summary>
             /// Returns true if parsed text in property <see cref="ITextSymbolsParserState.TextToParse"/> in <see cref="IParseExpressionItemContext.TextSymbolsParser"/>
             /// starts with symbol <paramref name="symbolToMatch"/>.
             /// For example if parsed text is "f(x) begin++x;end;" and <paramref name="symbolToMatch"/> is "begin" and property
             /// <see cref="ITextSymbolsParserState.PositionInText"/> in <see cref="IParseExpressionItemContext.TextSymbolsParser"/>
             /// is 5 (index of "begin"), then this method will return true. On the other hand if we change the parsed text is "f(x) beginy+x;end;", with all the other
             /// values being the same, the returned value will be false.
             /// </summary>
             /// <param name="parseExpressionItemContext">Pare expression context data.</param>
             /// <param name="symbolToMatch">Symbol to match.</param>
             public static bool StartsWithSymbol([NotNull] this IParseExpressionItemContext parseExpressionItemContext, string symbolToMatch)
             {
                 var textSymbolsParser = parseExpressionItemContext.TextSymbolsParser;
                 return Helpers.StartsWithSymbol(textSymbolsParser.TextToParse, symbolToMatch, textSymbolsParser.PositionInText, textSymbolsParser.ParsedTextEnd,
                     parseExpressionItemContext.ExpressionLanguageProviderWrapper.ExpressionLanguageProvider);
             }

             /// <summary>
             /// Returns true if parsed text in property <see cref="ITextSymbolsParserState.TextToParse"/> in <see cref="IParseExpressionItemContext.TextSymbolsParser"/>
             /// starts with symbol <paramref name="symbolToMatch"/>.
             /// For example if parsed text is "f(x) begin++x;end;" and <paramref name="symbolToMatch"/> is "begin" and property
             /// <see cref="ITextSymbolsParserState.PositionInText"/> in <see cref="IParseExpressionItemContext.TextSymbolsParser"/>
             /// is 5 (index of "begin"), then this method will return true. On the other hand if we change the parsed text is "f(x) beginy+x;end;", with all the other
             /// values being the same, the returned value will be false.
             /// </summary>
             /// <param name="parseExpressionItemContext">Pare expression context data.</param>
             /// <param name="symbolToMatch">Symbol to match.</param>
             /// <param name="matchedSymbol">
             /// An output parameter for the matched symbol. If the returned value is true, the value is not null.
             /// Otherwise the value is null.<br/>
             /// If the value of <see cref="IExpressionLanguageProvider.IsLanguageCaseSensitive"/> is true, the value is the same as
             /// <paramref name="symbolToMatch"/>, otherwise, the value is equal to <paramref name="matchedSymbol"/> when case is ignored. 
             /// </param>
             public static bool StartsWithSymbol([NotNull] this IParseExpressionItemContext parseExpressionItemContext, string symbolToMatch, 
                 out string matchedSymbol)
             {
                 var textSymbolsParser = parseExpressionItemContext.TextSymbolsParser;
                 return Helpers.StartsWithSymbol(textSymbolsParser.TextToParse, symbolToMatch, textSymbolsParser.PositionInText, textSymbolsParser.ParsedTextEnd,
                     parseExpressionItemContext.ExpressionLanguageProviderWrapper.ExpressionLanguageProvider, null, out matchedSymbol);
             }

             /// <summary>
             /// Returns true if parsed text in property <see cref="ITextSymbolsParserState.TextToParse"/> in <see cref="IParseExpressionItemContext.TextSymbolsParser"/>
             /// starts with symbol <paramref name="symbolToMatch"/>.
             /// For example if parsed text is "f(x) begin++x;end;" and <paramref name="symbolToMatch"/> is "begin" and property
             /// <see cref="ITextSymbolsParserState.PositionInText"/> in <see cref="IParseExpressionItemContext.TextSymbolsParser"/>
             /// is 5 (index of "begin"), then this method will return true. On the other hand if we change the parsed text is "f(x) beginy+x;end;", with all the other
             /// values being the same, the returned value will be false.
             /// </summary>
             /// <param name="parseExpressionItemContext">Pare expression context data.</param>
             /// <param name="symbolToMatch">Symbol to match.</param>
             /// <param name="isValidTextAfterTextToMatch">A delegate that specifies if the text after the matched symbol is not part of the same symbol.
             /// For example if parsed text is "var var1+", <paramref name="symbolToMatch"/> is "var1", and property
             /// <see cref="ITextSymbolsParserState.PositionInText"/> in <see cref="IParseExpressionItemContext.TextSymbolsParser"/> is 4 (i.e., index of "var1"),
             /// then if this delegate returns true for character '+', then the method <see cref="StartsWithSymbol(IParseExpressionItemContext, string, IsValidTextAfterMatchedTextDelegate)"/>
             /// will return true, otherwise, the method call will return false.
             /// </param>
             public static bool StartsWithSymbol([NotNull] this IParseExpressionItemContext parseExpressionItemContext, string symbolToMatch,
                                           IsValidTextAfterMatchedTextDelegate isValidTextAfterTextToMatch)
             {
                 var textSymbolsParser = parseExpressionItemContext.TextSymbolsParser;
                 return Helpers.StartsWithSymbol(textSymbolsParser.TextToParse, symbolToMatch, textSymbolsParser.PositionInText, textSymbolsParser.ParsedTextEnd,
                                             parseExpressionItemContext.ExpressionLanguageProviderWrapper.ExpressionLanguageProvider, isValidTextAfterTextToMatch);
             }

             /// <summary>
             /// Returns true if parsed text in property <see cref="ITextSymbolsParserState.TextToParse"/> in <see cref="IParseExpressionItemContext.TextSymbolsParser"/>
             /// starts with symbol <paramref name="symbolToMatch"/>.
             /// For example if parsed text is "f(x) begin++x;end;" and <paramref name="symbolToMatch"/> is "begin" and property
             /// <see cref="ITextSymbolsParserState.PositionInText"/> in <see cref="IParseExpressionItemContext.TextSymbolsParser"/>
             /// is 5 (index of "begin"), then this method will return true. On the other hand if we change the parsed text is "f(x) beginy+x;end;", with all the other
             /// values being the same, the returned value will be false.
             /// </summary>
             /// <param name="parseExpressionItemContext">Pare expression context data.</param>
             /// <param name="symbolToMatch">Symbol to match.</param>
             /// <param name="isValidTextAfterTextToMatch">A delegate that specifies if the text after the matched symbol is not part of the same symbol.
             /// For example if parsed text is "var var1+", <paramref name="symbolToMatch"/> is "var1", and property
             /// <see cref="ITextSymbolsParserState.PositionInText"/> in <see cref="IParseExpressionItemContext.TextSymbolsParser"/> is 4 (i.e., index of "var1"),
             /// then if this delegate returns true for character '+', then the method <see cref="StartsWithSymbol(IParseExpressionItemContext, string, IsValidTextAfterMatchedTextDelegate)"/>
             /// will return true, otherwise, the method call will return false.
             /// </param>
             /// <param name="matchedSymbol">
             /// An output parameter for the matched symbol. If the returned value is true, the value is not null.
             /// Otherwise the value is null.<br/>
             /// If the value of <see cref="IExpressionLanguageProvider.IsLanguageCaseSensitive"/> is true, the value is the same as
             /// <paramref name="symbolToMatch"/>, otherwise, the value is equal to <paramref name="matchedSymbol"/> when case is ignored. 
             /// </param>
             public static bool StartsWithSymbol([NotNull] this IParseExpressionItemContext parseExpressionItemContext, string symbolToMatch,
                                           IsValidTextAfterMatchedTextDelegate isValidTextAfterTextToMatch, out string matchedSymbol)
             {
                 var textSymbolsParser = parseExpressionItemContext.TextSymbolsParser;
                 return Helpers.StartsWithSymbol(textSymbolsParser.TextToParse, symbolToMatch, textSymbolsParser.PositionInText, textSymbolsParser.ParsedTextEnd,
                     parseExpressionItemContext.ExpressionLanguageProviderWrapper.ExpressionLanguageProvider, isValidTextAfterTextToMatch, out matchedSymbol);
             }
         }
     }