==============
Numeric Values
==============

.. contents::
   :local:
   :depth: 2
   
- **Universal Expression Parser** parses expression items that have numeric format to expression items of type **UniversalExpressionParser.ExpressionItems.INumericExpressionItem**. The format of expression items that will be parsed to **UniversalExpressionParser.ExpressionItems.INumericExpressionItem** is determined by property **IReadOnlyList<NumericTypeDescriptor> NumericTypeDescriptors { get; }** in interface **UniversalExpressionParser.IExpressionLanguageProvider**, an instance of which is passed to the parser.

.. raw:: html

    <a href="https://github.com/artakhak/UniversalExpressionParser/blob/main/UniversalExpressionParser/NumericTypeDescriptor.cs"><p class="codeSnippetRefText">Click here to see the definition of UniversalExpressionParser.NumericTypeDescriptor</p></a>

.. raw:: html

    <a href="https://github.com/artakhak/UniversalExpressionParser/blob/main/UniversalExpressionParser/ExpressionItems/INumericExpressionItem.cs"><p class="codeSnippetRefText">Click here to see the definition of UniversalExpressionParser.ExpressionItems.INumericExpressionItem</p></a>

- The parser scans the regular expressions in list in property **IReadOnlyList<string> RegularExpressions { get; }** in type **NumericTypeDescriptor** for each provided instance of **UniversalExpressionParser.NumericTypeDescriptor** to try to parse the expression to numeric expression item of type **UniversalExpressionParser.ExpressionItems.INumericExpressionItem**. 
   
- The abstract class **UniversalExpressionParser.ExpressionLanguageProviderBase** that can be used as a base class for implementations of **UniversalExpressionParser.IExpressionLanguageProvider** in most cases, implements the property **NumericTypeDescriptors** as a virtual property. The implementation of property **NumericTypeDescriptors** in **UniversalExpressionParser.ExpressionLanguageProviderBase** is demonstrated below, and it can be overridden to provide different format for numeric values:
 
.. note::
   The regular expressions used in implementation of property **NumericTypeDescriptors** should always start with '**^**' and should never end with '**$**'.

.. sourcecode:: csharp
     :linenos:
     
     public virtual IReadOnlyList<NumericTypeDescriptor> NumericTypeDescriptors { get; } = new List<NumericTypeDescriptor>
     {   
         new NumericTypeDescriptor(KnownNumericTypeDescriptorIds.ExponentFormatValueId, 
         new[] { @"^(\d+\.\d+|\d+\.|\.\d+|\d+)(EXP|exp|E|e)[+|-]?(\d+\.\d+|\d+\.|\.\d+|\d+)"}),
         
         new NumericTypeDescriptor(KnownNumericTypeDescriptorIds.FloatingPointValueId, 
         new[] { @"^(\d+\.\d+|\d+\.|\.\d+)"}),    
         
         new NumericTypeDescriptor(KnownNumericTypeDescriptorIds.IntegerValueId, new[] { @"^\d+" })
     }

- The first regular expression that matches the expression, is stored in properties **SucceededNumericTypeDescriptor** of type **UniversalExpressionParser.NumericTypeDescriptor** and **IndexOfSucceededRegularExpression** in parsed expression item of type **UniversalExpressionParser.ExpressionItems.INumericExpressionItem**.
- The numeric value is stored as text in property **INameExpressionItem Value** in text format. Therefore, there is no limit on numeric value digits.
- The expression evaluator that uses the **Universal Expression Parser** can convert the textual value in property **Value** of type **INameExpressionItem** in **UniversalExpressionParser.ExpressionItems.INumericExpressionItem** to a value of numeric type (int, long, double, etc.).
- Examples of numeric value expression items are demonstrated below:

.. sourcecode:: csharp
     :linenos:
     
     // By default exponent notation can be used.
     println(-0.5e-3+.2exp3.4+3.E2.7+2.1EXP.3);
     println(.5e15*x);

     // Numeric values can have no limitations on the number of digits. The value is stored as text in
     // UniversalExpressionParser.ExpressionItems.INumericExpressionItem.
     // The text can be validated farther and converted to numeric values by the expression evaluator that
     // uses the parser.
     var x = 2.3*x+123456789123456789123456789123456789; 



.. raw:: html

    <a href="https://github.com/artakhak/UniversalExpressionParser/blob/main/UniversalExpressionParser.Tests/Demos/DemoExpressions/NumericValues/NumericValues.parsed"><p class="codeSnippetRefText">Click here to see the visualized instance of UniversalExpressionParser.IParseExpressionResult</p></a>