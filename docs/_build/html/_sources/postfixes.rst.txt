=========
Postfixes
=========

.. contents::
   :local:
   :depth: 2
   
Postfixes are one or more expression items that are placed after some other expression item, and are added to the list in property **Postfixes** in interface **UniversalExpressionParser.ExpressionItems.IComplexExpressionItem** for the expression item that the postfixes are placed after.

Currently **Universal Expression Parser** supports two types of postfixes:

1) Code block expression items
==============================

Code block expression items that are parsed to **UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem** that succeed another expression item are added as postfixes to the expression item they succeed.

.. note::
    The following are expression types that can have postfixes: Literals, such a **x1** or **Dog**, braces expression items, such as **f(x1)**, **(y)**, **m1[x1]**, **[x2]**, or custom expression items for which property **CustomExpressionItemCategory** in interface **UniversalExpressionParser.ExpressionItems.Custom.ICustomExpressionItem** is equal to **UniversalExpressionParser.ExpressionItems.Custom.CustomExpressionItemCategory.Regular**. 
  
- In the example below the code block expression item of type **UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem** parsed from expression that starts with '**{**' and ends with '**}**'" will be added to the list **Postfixes** in **UniversalExpressionParser.ExpressionItems.IComplexExpressionItem** for the literal expression item parsed from expression **Dog**.

.. sourcecode::
     :linenos:
     
     public class Dog
     {
        // This code block will be added as a postfix to literal expression item parsed from "Dog"
     }

.. raw:: html

    <a href="https://github.com/artakhak/UniversalExpressionParser/blob/main/UniversalExpressionParser.Tests/Demos/DemoExpressions/Postfixes/SimpleCodeBlockPostfixAfterLiteral.parsed"><p class="codeSnippetRefText">Click here to see the visualized instance of UniversalExpressionParser.IParseExpressionResult</p></a>

2) Custom postfix expression items
==================================

Custom expression items of type **UniversalExpressionParser.ExpressionItems.Custom.ICustomExpressionItem** with property **CustomExpressionItemCategory** equal to **UniversalExpressionParser.ExpressionItems.Custom.CustomExpressionItemCategory.Postfix** that succeed another expression item are added as postfixes to the expression item they succeed.

- In the example below the two custom expression items of type **UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IWhereCustomExpressionItem** parsed from expressions that start with "where" and end with "whereend"" as well as the code block will be added as postfixes to literal expression item parsed from "Dog".

.. note::
    For more details on custom expression items see section **Custom Expression Item Parsers**.

.. sourcecode::
     :linenos:
     
     ::types[T1,T2, T3] F1(x:T1, y:T2, z: T3)
     // The where below will be added as a postfix to expression item parsed from "F1(x:T1, y:T2, z: T3)
     where T1:int where T2:double whereend
     // The where below will be added as a postfix to expression item parsed from "F1(x:T1, y:T2, z: T3)
     where T3:T1  whereend
     {
         // This code block will be added as a postfix to expression item parsed from "F1(x:T1, y:T2, z: T3).
     }

.. raw:: html

    <a href="https://github.com/artakhak/UniversalExpressionParser/blob/main/UniversalExpressionParser.Tests/Demos/DemoExpressions/Postfixes/SimpleCustomExpressionItemAsPostfixAfterLiteral.parsed"><p class="codeSnippetRefText">Click here to see the visualized instance of UniversalExpressionParser.IParseExpressionResult</p></a>

.. note::
    The list of postfixes can include both types of postfixes at the same time (i.e., custom expression items as well as a code block postfix).

- Example of a code block postfix used to model function body:
 
.. sourcecode::
     :linenos:
     
     // More complicated cases
     // In the example below the parser will apply operator ':' to 'f2(x1:int, x2:int)' and 'int'
     // and will add the code block after 'int' as a postfix to 'int'.
     // The evaluator that processes the parsed expression can do farther transformation so that the code block is assigned to
     // some new property in some wrapper for an expression for 'f2(x1:int, x2:int)', so that the code block belongs to the function, rather than
     // to the returned type 'int' of function f2.
     f2(x1:int, x2:int) : int 
     {
     	f3() : int
     	{
     	    var result = x1+x2;
     		println("result='"+result+"'");
     		return result;
     	}
     	
     	return f3();
     }

     var myFunc = f2(x1:int, x2:int) =>
     {
         println(exp ^ (x1 + x2));
     }


.. raw:: html

    <a href="https://github.com/artakhak/UniversalExpressionParser/blob/main/UniversalExpressionParser.Tests/Demos/DemoExpressions/Postfixes/CodeBlockPostfixToModelFunctionBody.parsed"><p class="codeSnippetRefText">Click here to see the visualized instance of UniversalExpressionParser.IParseExpressionResult</p></a>
    
- Example of code block postfix used to model class definition:

.. sourcecode::
     :linenos:
     
     // In the example below the parser will apply operator ':' to literal 'Dog' (with keywords public and class) and 
     // braces '(Anymal, IDog)' and will add the code block after '(Anymal, IDog)' as a postfix to '(Anymal, IDog)'.
     // The evaluator that processes the parsed expression can do farther transformation so that the code block is assigned to
     // some new property in some wrapper for an expression for 'Dog', so that the code block belongs to the 'Dog' class, rather than
     // to the braces for public classes in '(Anymal, IDog)'.
     public class Dog : (Anymal, IDog)
     {
         public Bark() : void
         {
             println("Bark.");
         }
     }

.. raw:: html

    <a href="https://github.com/artakhak/UniversalExpressionParser/blob/main/UniversalExpressionParser.Tests/Demos/DemoExpressions/Postfixes/CodeBlockPostfixUsedToModelAClassDefinition.parsed"><p class="codeSnippetRefText">Click here to see the visualized instance of UniversalExpressionParser.IParseExpressionResult</p></a>   

- Below are some more examples of postfixes with different expression items:

.. sourcecode::
     :linenos:
     
     f1(x1) 
     {
         // Code block added to postfixes list for braces expression "f1(x1)"
         return x2*y1;
     }

     m1[x2]
     {
         // Code block added to postfixes list for braces expression "m1[x2]"
         x:2*3
     }

     (x3)
     {
         // Code block added to postfixes list for braces expression "(x3)"
         return x3*2;
     }

     [x4]
     {
         // Code block added to postfixes list for braces expression "[x4]"
         x4:2*3
     }

     class Dog
     {
         // Code block added to postfixes list for literal expression "Dog"
     }

     ::pragma x
     {
         // Code block added to custom expression item IPragmaCustomExpressionItem parsed from "::pragma x"
     }


.. raw:: html

    <a href="https://github.com/artakhak/UniversalExpressionParser/blob/main/UniversalExpressionParser.Tests/Demos/DemoExpressions/Postfixes/CodeBlockPostfixForDifferentExpressionItems.parsed"><p class="codeSnippetRefText">Click here to see the visualized instance of UniversalExpressionParser.IParseExpressionResult</p></a> 