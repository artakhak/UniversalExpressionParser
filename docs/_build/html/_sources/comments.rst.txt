========
Comments
========

.. contents::
   :local:
   :depth: 2
   
The interface **UniversalExpressionParser.IExpressionLanguageProvider** has properties **string LineCommentMarker { get; }**, **string MultilineCommentStartMarker { get; }**, and **string MultilineCommentEndMarker { get; }** for specifying comment markers.

If the values of these properties are not null, line and code block comments can be used.

The abstract implementation **UniversalExpressionParser.ExpressionLanguageProviderBase** of **UniversalExpressionParser.IExpressionLanguageProvider** overrides these properties to return "//", "/*", and "*/" (the values of these properties can be overridden in subclasses).

The on commented out code data is stored in property **IReadOnlyList&lt;UniversalExpressionParser.ICommentedTextData&gt; SortedCommentedTextData { get; }** in **UniversalExpressionParser.IParsedExpressionResult**, an instance of which is returned by the call to method **UniversalExpressionParser.IExpressionParser.ParseExpression(...)**.

- Below are some examples of line and code block comments:

.. sourcecode::
     :linenos:
     
     // Line comment
     var x = 5*y; // another line comments

     println(x +/*Code block 
     comments
     can span multiple lines and can be placed anywhere.
     */y+10*z);

     /*
     Another code block comments
     var y=5*x;
     var z = 3*y;
     */

.. raw:: html

    <a href="https://github.com/artakhak/UniversalExpressionParser/blob/main/UniversalExpressionParser.Tests/Demos/DemoExpressions/Comments/Comments.parsed"><p class="codeSnippetRefText">Click here to see the visualized instance of UniversalExpressionParser.IParseExpressionResult</p></a>

- Below is the definition of interface **UniversalExpressionParser.ICommentedTextData** that stores data on comments.

.. sourcecode::
     :linenos:
     
     // Copyright (c) UniversalExpressionParser Project. All rights reserved.
     // Licensed under the MIT License. See LICENSE in the solution root for license information.

     using UniversalExpressionParser.ExpressionItems;

     namespace UniversalExpressionParser
     {
         /// <summary>
         /// Info on commented out code block.
         /// </summary>
         public interface ICommentedTextData: ITextItem
         {
             /// <summary>
             /// If true, the comment is a line comment. Otherwise, it is a block comment.
             /// </summary>
             bool IsLineComment { get; }
         }
     }