========
Literals
========

.. contents::
   :local:
   :depth: 2

- Literals are names that can be used either alone (say in operators) or can be  part of more complex expressions. For example literals can precede opening square or round braces (e.g., **f1** in **f1(x)**, or **m1** in **m1[1, 2]**), or code blocks (e.g., **Dog** in expression **public class Dog {}**).

- Literals are parsed into expression items of type **UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem** objects.

- If literal precedes round or square braces, it will be stored in value of property **ILiteralExpressionItem Name { get; }** of **UniversalExpressionParser.ExpressionItems.IBracesExpressionItem**.

- If literal precedes a code block staring marker (e.g., **Dog** in expression **public class Dog {}**), then the code block is added to the list **UniversalExpressionParser.ExpressionItems.IComplexExpressionItem.Postfixes** in expression item for the literal (see section **Postfixes** for more details on postfixes).

- Literals are texts that cannot have space and can only contain characters validated by method **UniversalExpressionParser.IExpressionLanguageProvider.IsValidLiteralCharacter(char character, int positionInLiteral, ITextSymbolsParserState textSymbolsParserState)** in interface **UniversalExpressionParser.IExpressionLanguageProvider**. In other words, a literal can contain any character (including numeric or operator characters, a dot '.', '_', etc.), that is considered a valid literal character by method **IExpressionLanguageProvider.IsValidLiteralCharacter(...)**.


Examples of literals
====================

.. sourcecode::
     :linenos:

     // In example below _x, f$, x1, x2, m1, and x3, Dog, Color, string, println, and Dog.Color are literals.
     var _x = f$(x1, x2) + m1[1, x3];

     public class Dog
     {
         public Color : string => "brown";
     }

     // println is a literal and it is part of "println(Dog.Color)" braces expression item. In this example, 
     // println will be parsed to an expression item UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem
     // and will be the value of property NamedExpressionItem in UniversalExpressionParser.ExpressionItems.IBracesExpressionItem
     println(Dog.Color);


`Click here to see the visualized instance of UniversalExpressionParser.IParseExpressionResult <https://github.com/artakhak/UniversalExpressionParser/blob/main/UniversalExpressionParser.Tests/Demos/DemoExpressions/Literals/Literals.parsed/>`_