===================================================
Case Sensitivity and Non Standard Language Features
===================================================

.. contents::
   :local:
   :depth: 2
   
Case sensitivity
================

- Case sensitivity is controlled by property **bool IsLanguageCaseSensitive { get; }** in interface **UniversalExpressionParser.IExpressionLanguageProvider**.

- If the value of this property **IsLanguageCaseSensitive** is **true**, any two expressions are considered different, if the expressions are the same, except for capitalization of some of the text (say "public class Dog" vs "Public ClaSS DOg"). Otherwise, if the value of property **IsLanguageCaseSensitive** is **false**, the capitalization of any expression items does not matter (i.e., parsing will succeed regardless of capitalization in expression).

- For example C# is considered a case sensitive language, and Visual Basic is considered case insensitive.

- The value of property **IsLanguageCaseSensitive** in abstract implementation **UniversalExpressionParser.ExpressionLanguageProviderBase** of **UniversalExpressionParser.IExpressionLanguageProvider** returns **true**.

- The expression below demonstrates parsing the expression by **UniversalExpressionParser.IExpressionLanguageProvider** with overridden **IsLanguageCaseSensitive** to return **false**.

Non standard comment markers
============================

- The properties **string LineCommentMarker { get; }**, **string MultilineCommentStartMarker { get; }**, and **string MultilineCommentEndMarker { get; }** in interface **UniversalExpressionParser.IExpressionLanguageProvider** determine the line comment marker as well as code block comment start and end markers.

- The default implementation **UniversalExpressionParser.ExpressionLanguageProviderBase** of **UniversalExpressionParser.IExpressionLanguageProvider** returns "//", "/*", and "*/" for these properties to use C# like comments. However, other values can be used for these properties.

- The expression below demonstrates parsing the expression by an instance of **UniversalExpressionParser.IExpressionLanguageProvider** with overridden **LineCommentMarker**, **MultilineCommentStartMarker**, and **MultilineCommentEndMarker** to return "rem", "rem*", "*rem".

Non standard code separator character and code block markers
============================================================

- The properties **char ExpressionSeparatorCharacter { get; }**, **string CodeBlockStartMarker { get; }**, and **string CodeBlockEndMarker { get; }** in interface **UniversalExpressionParser.IExpressionLanguageProvider** determine the code separator character, as well as the code block start and end markers.

- The default implementation **UniversalExpressionParser.ExpressionLanguageProviderBase** of **UniversalExpressionParser.IExpressionLanguageProvider** returns ";", "{", and "}" for these properties to use C# like code separator and code block markers. However, other values can be used for these properties.

- The expression below demonstrates parsing the expression by an instance of **UniversalExpressionParser.IExpressionLanguageProvider** with overridden **ExpressionSeparatorCharacter**, **CodeBlockStartMarker**, and **CodeBlockEndMarker** to return ";", "BEGIN", and "END".

Example demonstrating case insensitivity and non standard language features
===========================================================================
 
- The expression below is parsed using the expression language provider **UniversalExpressionParser.DemoExpressionLanguageProviders.VerboseCaseInsensitiveExpressionLanguageProvider**, which overrides **IsLanguageCaseSensitive** to return **false**. As can bee seen in this example, the keywords (e.g., **var**, **public**, **class**, **::pragma**, etc), non standard code comment markers (i.e., "rem", "rem*", "*rem"), code block markers (i.e., **BEGIN**, **END**) and operators **IS NULL**, **IS NOT NULL** can be used with any capitalization, and the expression is still parsed without errors. 

.. sourcecode:: csharp
     :linenos:
     
     rem This line commented out code with verbose line comment marker 'rem'
     rem*this is a demo of verbose code block comment markers*rem

     rem#No space is required between line comment marker and the comment, if the first 
     rem character is a special character (such as operator, opening, closing round or squer braces, comma etc.)

     BEGIN
         println(x); println(x+y)
         rem* this is an example of code block
         with verbose code block start and end markers 'BEGIN' and 'END'.
         *rem
     END

     Rem Line comment marker can be used with any capitalization

     REm* Multi-line comment start/end markers can be used with
     any capitalization *ReM

     rem keywords public and class can be used with any capitalization.
     PUBLIC Class DOG 
     BEGIN Rem Code block start marker 'BEGIN' can be used with any capitalization
         PUBLIc static F1(); rem keywords (e.g., 'PUBLIC') can be used with any capitalization
     end

     REm keyword 'var' can be used with any capitalization.
     VaR x=::PRagma y;

     PRintLN("X IS NOT NULL=" + X Is noT Null && ::pRAGMA y is NULL);

     f1(x1, y1)
     BEGin Rem Code block start marker 'BEGIN'can be used with any capitalization.
        Rem Line comment marker 'rem' can be used with any capitalization
        rem Line comment marker 'rem' can be used with any capitalization

        REm* Multi line comment start/end markers can be used with
        any capitalization *rEM

        RETurN X1+Y1; rem unary prefix operator 'return' (and any other) operator  can be used with any capitalization.
     enD rem Code block end marker 'END' can be used  with any capitalization.

.. raw:: html

    <a href="https://github.com/artakhak/UniversalExpressionParser/blob/main/UniversalExpressionParser.Tests/Demos/DemoExpressions/CaseSensitivityAndNonStandardLanguageFeatures/CaseSensitivityAndNonStandardLanguageFeatures.parsed"><p class="codeSnippetRefText">Click here to see the visualized instance of UniversalExpressionParser.IParseExpressionResult</p></a>