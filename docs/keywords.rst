========
Keywords
========

.. contents::
   :local:
   :depth: 2

- Keywords are special names (e.g., **var**, **public**, **class**, **where**) that can be specified in property **IReadOnlyList&lt;ILanguageKeywordInfo&gt; Keywords { get; }** in interface **UniversalExpressionParser.IExpressionLanguageProvider**, as shown in example below.

.. note::
    Keywords are supported only if the value of property **SupportsKeywords** in **UniversalExpressionParser.IExpressionLanguageProvider** is true.

.. sourcecode:: csharp
     :linenos:

     public class DemoExpressionLanguageProvider : IExpressionLanguageProvider 
     {
         ...
         public override IReadOnlyList<ILanguageKeywordInfo> Keywords { get; } = new []
         {
             new UniversalExpressionParser.UniversalExpressionParser(1, "where"),
             new UniversalExpressionParser.UniversalExpressionParser(2, "var"),
             ...
         }; 
     }

- Keywords are parsed to expression items of type **UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem**.

- Keywords have the following two applications.

1) One or more keywords can be placed in front of any literal (e.g., variable name), round or square braces expression, function or matrix expression, a code block. In this type of usage of keywords the parser parses the keywords and adds the list of parsed keyword expression items (i.e., list of **UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem** objects) to list in property **IReadOnlyList&lt;IKeywordExpressionItem&gt; AppliedKeywords { get; }** in **UniversalExpressionParser.ExpressionItems.IComplexExpressionItem** for the expression item that follows the list of keywords.

2) Custom expression parser evaluates the list of parsed keywords to determine if the expression that follows the keywords should be parsed to a custom expression item.
   See section **Custom Expression Item Parsers** for more details on custom expression parsers.

Examples of keywords
====================

.. sourcecode::
     :linenos:

     // Keywords "public" and "class" will be added to list in property "AppliedKeywords" in class 
     // "UniversalExpressionParser.ExpressionItems.Custom.IComplexExpressionItem" for the parsed expression "Dog".
     public class Dog;

     // Keywords "public" and "static will be added to list in property "AppliedKeywords" in class 
     // "UniversalExpressionParser.ExpressionItems.Custom.IComplexExpressionItem" for the parsed expression "F1()".
     public static F1();

     // Keywords "public" and "static" will be added to list in property "AppliedKeywords" in class 
     // "UniversalExpressionParser.ExpressionItems.Custom.IComplexExpressionItem" for the parsed expression "F1()".
     public static F1() {return 1; }

     // Keyword "::codeMarker" will be added to list in property "AppliedKeywords" in class 
     // "UniversalExpressionParser.ExpressionItems.Custom.IComplexExpressionItem" for the parsed expression "(x1, x2)".
     ::codeMarker (x1, x2);

     // Keyword "::codeMarker" will be added to list in property "AppliedKeywords" in class 
     // "UniversalExpressionParser.ExpressionItems.Custom.IComplexExpressionItem" for the parsed expression "m1[2, x1]".
     ::codeMarker m1[2, x1];

     // Keyword "::codeMarker" will be added to list in property "AppliedKeywords" in class 
     // "UniversalExpressionParser.ExpressionItems.Custom.IComplexExpressionItem" for the parsed expression "[x1, x2]".
     ::codeMarker[x1, x2];

     // Keyword "static" will be added to list in property "AppliedKeywords" in class 
     // "UniversalExpressionParser.ExpressionItems.Custom.IComplexExpressionItem" for the code block parsed expression "{}".
     static 
     {
         var x;
     }

     // Keyword "::pragma" will be used by custom expression parser "UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.PragmaCustomExpressionItemParser" to 
     // parse expressions "::pragma x2" and "::pragma x3" to custom expression items of type 
     // "UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.PragmaCustomExpressionItem".
     var y = x1 +::pragma x2+3*::pragma x3 +y;

.. raw:: html

    <a href="https://github.com/artakhak/UniversalExpressionParser/blob/main/UniversalExpressionParser.Tests/Demos/DemoExpressions/Keywords/Keywords.parsed"><p class="codeSnippetRefText">Click here to see the visualized instance of UniversalExpressionParser.IParseExpressionResult</p></a>