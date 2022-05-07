===============================
Code Separators and Code Blocks
===============================

.. contents::
   :local:
   :depth: 2
   
- If the value of property **char ExpressionSeparatorCharacter { get; }** in **UniversalExpressionParser.IExpressionLanguageProvider** is not equal to character '\0', multiple expressions can be used in a single expression. 

For example if the value of property **ExpressionSeparatorCharacter** is ';' the expression "var x=f1(y);println(x)" will be parsed to a list of two expression items for "x=f1(y)" and "println(x)". Otherwise, the parser will report an error for this expression (see section on **Error Reporting** for more details on errors). 

- If both values of properties **char CodeBlockStartMarker { get; }** and **string CodeBlockEndMarker { get; }** in **UniversalExpressionParser.IExpressionLanguageProvider** are not equal to character '\0', code block expressions can be used to combine multiple expressions into code block expression items of type **UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem**. 

- For example if the values of properties **CodeBlockStartMarker** and **CodeBlockEndMarker** are '{' and '}', the expression below will be parsed to two code block expressions of type **UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem**. Otherwise, the parser will report errors.

.. sourcecode::
     :linenos:
     
     {
         var x = y^2;
         println(x);
     }

     {
         fl(x1, x2);
         println(x) // No need for ';' after the last expression
     }

`Click here to see the visualized instance of UniversalExpressionParser.IParseExpressionResult <https://github.com/artakhak/UniversalExpressionParser/blob/main/UniversalExpressionParser.Tests/Demos/DemoExpressions/CodeSeparatorsAndCodeBlocks/SimpleExample.parsed/>`_

More complex examples of code blocks and code separators
========================================================

.. sourcecode::
     :linenos:
     
     var y = x1 + 2.5 * x2;

     f1()
     {
         // Code block used as function body (see examples in Postfixes folder)
     }

     var z = e ^ 2.3;
     {
         var x = 5 * y;
         println("x=" + x);

         {
             var y1 = 10 * x;
             println(getExp(y1));
         }

         {
             var y2 = 20 * x;
             println(getExp(y2));
         }

         getExp(x) : double
         {
             // another code block used as function body (see examples in Postfixes folder)
             return e^x;
         }
     }

     f2()
     {
         // Another code block used as function body (see examples in Postfixes folder)
     }

     {
         // Another code block
     }

     public class Dog
     {
         public Bark()
         {
             // Note, code separator ';' is not necessary, if the expression is followed by code block end marker '}'.
             println("bark")
         }
     }

`Click here to see the visualized instance of UniversalExpressionParser.IParseExpressionResult <https://github.com/artakhak/UniversalExpressionParser/blob/main/UniversalExpressionParser.Tests/Demos/DemoExpressions/CodeSeparatorsAndCodeBlocks/MoreComplexExample.parsed/>`_