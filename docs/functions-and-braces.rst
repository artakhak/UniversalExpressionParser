====================
Functions and Braces
====================

.. contents::
   :local:
   :depth: 2

- Functions are round or square braces preceded with a literal (e.g., **F1(x)**, **F1(x) {}**, **m1[i,j]**, **m1[i,j]{}**). Functions are  parsed to expression items of type **UniversalExpressionParser.ExpressionItems.IBracesExpressionItem** with the value of property **ILiteralExpressionItem NameLiteral { get; }** equal to a literal that precedes the braces.
- Braces are a pair of round or square braces ((e.g., **(x)**, **(x) {}**, **[i,j]**, **[i,j]{}**)). Braces are parsed to expression items of type **UniversalExpressionParser.ExpressionItems.IBracesExpressionItem** with the value of property **ILiteralExpressionItem NameLiteral { get; }** equal to null.

Examples of functions
=====================

.. sourcecode::
     :linenos:

     // The statements below do not make much sense.
     // They just demonstrate different ways the square and round braces can be used
     // in expressions.
     var x = x1+f1(x2, x3+x4*5+x[1])+
              matrix1[[y1+3, f1(x4)], x3, f2(x3, m2[x+5])];

     f1(x, y) => x+y;

     f2[x, y] 
     {
        // Normally matrixes do not have bodies like functions doo. This is just to demo that 
        // the parser allows this.
     }

`Click here to see the visualized instance of UniversalExpressionParser.IParseExpressionResult <https://github.com/artakhak/UniversalExpressionParser/blob/main/UniversalExpressionParser.Tests/Demos/DemoExpressions/FunctionsAndBraces/FunctionsWithRoundAndSquareBraces.parsed/>`_


Examples of braces
==================

.. sourcecode::
     :linenos:

     // The statements below do not make much sense.
     // They just demonstrate different ways the square and round braces can be used
     // in expressions.
     var x = ((x1, x2, x3), [x4, x5+1, x6], y);
     x += (x2, x4) + 2*[x3, x4];

`Click here to see the visualized instance of UniversalExpressionParser.IParseExpressionResult <https://github.com/artakhak/UniversalExpressionParser/blob/main/UniversalExpressionParser.Tests/Demos/DemoExpressions/FunctionsAndBraces/RoundAndSquareBraces.parsed/>`_