=========
Operators
=========

.. contents::
   :local:
   :depth: 2

- Operators that the valid expressions can use are defined in property **System.Collections.Generic.IReadOnlyList&lt;UniversalExpressionParser.IOperatorInfo&gt; Operators { get; }** in interface **UniversalExpressionParser.IExpressionLanguageProvider** (an instance of this interface is passed to the parser).
- The interface **UniversalExpressionParser.IOperatorInfo** has properties for operator name (i.e., a collection of texts that operator consists of, such as ["IS", "NOT", "NUL"] or ["+="]), priority, unique Id, operator type (i.e., binary, unary prefix or unary postfix).
- Two different operators can have similar names, as long as they have different operator. For example "++" can be used both as unary prefix as well as unary postfix operator.

Example of defining operators in an implementation of **UniversalExpressionParser.IExpressionLanguageProvider**
===============================================================================================================

.. sourcecode:: csharp
     :linenos:     

     public class TestExpressionLanguageProviderBase : ExpressionLanguageProviderBase
     {
         //...
         // Some other method and property implementations here
         // ...
         public override IReadOnlyList<IOperatorInfo> Operators { get; } = new IOperatorInfo[] 
         {
         	// The third parameter (e.g., 0) is the priority.
         	new OperatorInfo(1, new [] {"!"}, OperatorType.PrefixUnaryOperator, 0),
         	new OperatorInfo(2, new [] {"IS", "NOT", "NULL"}, OperatorType.PostfixUnaryOperator, 0),
         
         	new OperatorInfo(3, new [] {"*"}, OperatorType.BinaryOperator, 10),
         	new OperatorInfo(4, new [] {"/"}, OperatorType.BinaryOperator, 10),
         
         	new OperatorInfo(5, new [] {"+"}, OperatorType.BinaryOperator, 30),
     	    new OperatorInfo(6, new [] {"-"}, OperatorType.BinaryOperator, 30),
         }
     }

- Operator expression (e.g., "a * b + c * d") is parsed to an expression item of type **UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem** (a subclass of **UniversalExpressionParser.ExpressionItems.IComplexExpressionItem**).

.. raw:: html

    <a href="https://github.com/artakhak/UniversalExpressionParser/blob/main/UniversalExpressionParser/ExpressionItems/IOperatorExpressionItem.cs"><p class="codeSnippetRefText">Click here to see the definition of UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem</p></a>

For example the expression "a * b + c * d", will be parsed to an expression logically similar to "*(+(a, b), +(x,d))". This is so since the binary operator "+" has lower priority (the value of **IOperatorInfo.Priority** is larger), than the binary operator "*". 

In other words this expression will be parsed to a binary operator expression item for "+" (i.e., an instance of **IOperatorExpressionItem**) with Operand1 and Operand2 also being binary operator expression items of type **UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem** for expression items "a * b" and "c * d".

Example of considering priorities when parsing operators
========================================================

.. sourcecode::
     :linenos:
     
     // The binary operator + has priority 30 and * has priority 20. Therefore, 
     // in expression below,  * is applied first and + is applied next.
     // The following expression is parsed to an expression equivalent to 
     // "=(var y, +(x1, *(f1(x2, +(x3, 1)), x4)))"
     var y = x1 + f1(x2,x3+1)*x4;

.. raw:: html

    <a href="https://github.com/artakhak/UniversalExpressionParser/blob/main/UniversalExpressionParser.Tests/Demos/DemoExpressions/Operators/OperatorPriorities.parsed"><p class="codeSnippetRefText">Click here to see the visualized instance of UniversalExpressionParser.IParseExpressionResult</p></a>

Example of using braces to change order of application of operators
===================================================================

.. sourcecode::
     :linenos:
     
     // Without the braces, the expression below would be equivalent to x1+(x2*x3)-x4.
     var y1 = [x1+x2]*(x3-x4);


.. raw:: html

    <a href="https://github.com/artakhak/UniversalExpressionParser/blob/main/UniversalExpressionParser.Tests/Demos/DemoExpressions/Operators/BracesToChangeOperatorEvaluationOrder.parsed"><p class="codeSnippetRefText">Click here to see the visualized instance of UniversalExpressionParser.IParseExpressionResult</p></a>

Example of operators with multiple parts in operator names
==========================================================

.. sourcecode::
     :linenos:
     
     // The expression below is similar to 
     // z = !((x1 IS NOT NULL) && (x2 IS NULL);
     z = !(x1 IS NOT NULL && x2 IS NULL);

.. raw:: html

    <a href="https://github.com/artakhak/UniversalExpressionParser/blob/main/UniversalExpressionParser.Tests/Demos/DemoExpressions/Operators/MultipartOperators.parsed"><p class="codeSnippetRefText">Click here to see the visualized instance of UniversalExpressionParser.IParseExpressionResult</p></a>

Example of two operators (e.g., postfix operators, then a binary operator) used next to each other without spaces in between
============================================================================================================================

.. sourcecode::
     :linenos:
     
     // The spaces between two ++ operators, and + was omitted intentionally to show that the parser will parse the expression 
     // correctly even without the space.
     // The expression below is similar to  println(((x1++)++)+x2). To avoid confusion, in some cases it is better to use braces.
     println(x1+++++x2)

.. raw:: html

    <a href="https://github.com/artakhak/UniversalExpressionParser/blob/main/UniversalExpressionParser.Tests/Demos/DemoExpressions/Operators/NoSpacesBetweenOperators.parsed"><p class="codeSnippetRefText">Click here to see the visualized instance of UniversalExpressionParser.IParseExpressionResult</p></a>

Example of unary prefix operator used to implement "return" statement
=====================================================================

.. sourcecode::
     :linenos:
     
     // return has priority int.MaxValue which is greater then any other operator priority, therefore
     // the expression below is equivalent to "return (x+(2.5*x))";
     return x+2.5*y;

     // another example within function body
     f1(x:int, y:int) : bool
     {
     	// return has priority int.MaxValue which is greater then any other operator priority, therefore
     	// the expression below is equivalent to "return (x+(2.5*x))";
     	return f(x)+y > 10;
     }

.. raw:: html

    <a href="https://github.com/artakhak/UniversalExpressionParser/blob/main/UniversalExpressionParser.Tests/Demos/DemoExpressions/Operators/UnaryPrefixOperatorUsedForReturnStatement.parsed"><p class="codeSnippetRefText">Click here to see the visualized instance of UniversalExpressionParser.IParseExpressionResult</p></a>