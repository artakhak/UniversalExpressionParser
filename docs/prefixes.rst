========
Prefixes
========

.. contents::
   :local:
   :depth: 2
   
Prefixes are one or more expression items that precede some other expression item, and are added to the list in property **Prefixes** in interface **UniversalExpressionParser.ExpressionItems.IComplexExpressionItem** for the expression item that follows the list of prefix expression items.

.. note::
    Prefixes are supported only if the value of property **SupportsPrefixes** in interface **UniversalExpressionParser.IExpressionLanguageProvider** is true.

Currently **Universal Expression Parser** supports two types of prefixes:

1) Nameless brace as prefixes
=============================

Square or round braces expressions items without names (i.e. expression items that are parsed to **UniversalExpressionParser.ExpressionItems.IBracesExpressionItem** with property **NamedExpressionItem** equal to **null**) that precede another expression item (e.g., another braces expression, a literal, a code block, text expression item, numeric value expression item, etc) are parsed as prefixes and are added to expression item they precede.

- In the example below the braces expression items parsed from "[NotNull, ItemNotNull]" and "(Attribute("MarkedFunction"))" will be added as prefixes to expression item parsed from "F1(x, x2)".

.. sourcecode::
     :linenos:
     
     [NotNull, ItemNotNull](Attribute("MarkedFunction")) F1(x, x2)
     {
         // This code block will be added to expression item parsed from F1(x:T1, y:T2, z: T3) as a postfix.
         retuens [x1, x2, x3];
     }

.. raw:: html

    <a href="https://github.com/artakhak/UniversalExpressionParser/blob/main/UniversalExpressionParser.Tests/Demos/DemoExpressions/Prefixes/BracesPrefixesSimpleDemo.parsed"><p class="codeSnippetRefText">Click here to see the visualized instance of UniversalExpressionParser.IParseExpressionResult</p></a>

2) Custom expressions as prefixes
=================================

If custom expression items of type **UniversalExpressionParser.ExpressionItems.Custom.ICustomExpressionItem** with property **CustomExpressionItemCategory** equal to **UniversalExpressionParser.ExpressionItems.Custom.CustomExpressionItemCategory.Prefix** are added as prefixes to expression item they precede.

.. note::
    List of prefixes can include both nameless brace expression items as well as custom expression items, placed in any order.


- In the example below, the expression items "::types[T1,T2]" and "::types[T3]" are parsed to custom expression items of type **UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem**, and are added as prefixes to braces expression item parsed from "F1(x:T1, y:T2, z: T3)".

.. note::
    For more details on custom expression items see section **Custom Expression Item Parsers**.

.. sourcecode::
     :linenos:
     
     ::types[T1,T2] ::types[T3] F1(x:T1, y:T2, z: T3)
     {
         // This code block will be added to expression item parsed from F1(x:T1, y:T2, z: T3) as a postfix.
     }

.. raw:: html

    <a href="https://github.com/artakhak/UniversalExpressionParser/blob/main/UniversalExpressionParser.Tests/Demos/DemoExpressions/Prefixes/CustomExpressionItemsAsPrefixesSimpleDemo.parsed"><p class="codeSnippetRefText">Click here to see the visualized instance of UniversalExpressionParser.IParseExpressionResult</p></a>

.. note::
    The list of prefixes can include both types of prefixes at the same time (i.e., braces and custom expression items).

- Here is an example of prefixes used to model c# like attributes for classes and methods:

.. sourcecode::
     :linenos:
     
     // [TestFixture] and [Attribute("IntegrationTest")] are added as prefixes to literal MyTests.
     [TestFixture]
     [Attribute("IntegrationTest")]
     // public and class are added as keywords to MyTests
     public class MyTests
     {
         // Brace expression items [SetupMyTests], [Attribute("This is a demo of multiple prefixes")]
         // and custom expression item starting with ::metadata and ending with } are added as prefixes to 
         // expression SetupMyTests()
         [TestSetup]
         [Attribute("This is a demo of multiple prefixes")]
         ::metadata {
             Description: "Demo of custom expression item parsed to 
                             UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IMetadataCustomExpressionItem
                             used in prefixes list of expression parsed from 'SetupMyTests()'";
             SomeMetadata: 1
         }
         // public and static are added as keywords to expression SetupMyTests().
         public static SetupMyTests() : void
         {
             // Do some test setup here
         }
     }

.. raw:: html

    <a href="https://github.com/artakhak/UniversalExpressionParser/blob/main/UniversalExpressionParser.Tests/Demos/DemoExpressions/Prefixes/MoreComplexPrefixesDemo.parsed"><p class="codeSnippetRefText">Click here to see the visualized instance of UniversalExpressionParser.IParseExpressionResult</p></a>

.. note::
    The list of prefixes can include both types of prefixes at the same time (i.e., braces and custom expression items).

- Below is an example of using prefixes with different expression item types:

.. sourcecode::
     :linenos:
     
     // Prefixes added to a literal "x".
     [NotNull] [Attribute("Marker")] x;

     // Prefixes added to named round braces. [NotNull] [Attribute("Marker")] will be added 
     // to prefixes in braces expression item parsed from "f1(x1)"
     [NotNull] [Attribute("Marker")] f1(x1);

     // Prefixes added to unnamed round braces. [NotNull] [Attribute("Marker")] will be added 
     // to prefixes in braces expression item parsed from "(x1)"
     [NotNull] [Attribute("Marker")] (x1);

     // Prefixes added to named square braces. [NotNull] [Attribute("Marker")] will be added 
     // to prefixes in named braces expression item parsed from "m1[x1]"
     [NotNull] [Attribute("Marker")] m1[x1];

     // Prefixes added to unnamed square braces. [NotNull] [Attribute("Marker")] will be added 
     // to prefixes in braces expression item parsed from "[x1]".
     [NotNull] [Attribute("Marker")] [x1];

     // Prefixes added to code block. 
     // Custom prefix expression item "::types[T1,T2]" will be added to list of prefixes in code block expression item
     // parsed from "{var i = 12;}".
     // Note, if we replace "::types[T1,T2]" to unnamed braces, then the unnamed braces will be used as a postfix for 
     // code block.
     ::types[T1,T2] {var i = 12;};

     // Prefixes added to custom expression item parsed from "::pragma x". 
     // [Attribute("Marker")] will be added to list of prefixes in custom expression item
     // parsed from "::pragma x".
     [Attribute("Marker")] ::pragma x;

     // Prefixes added text expression item. 
     // [Attribute("Marker")] will be added to list of prefixes in text expression item
     // parsed from "Some text".
     [Attribute("Marker")] "Some text";

     // Prefixes added to numeric value item. 
     // [Attribute("Marker")] will be added to list of prefixes in numeric value expression item
     // parsed from "0.5e-3.4".
     [Attribute("Marker")] 0.5e-3.4;



.. raw:: html

    <a href="https://github.com/artakhak/UniversalExpressionParser/blob/main/UniversalExpressionParser.Tests/Demos/DemoExpressions/Prefixes/PrefixesUsedWithDifferentExpressionItems.parsed"><p class="codeSnippetRefText">Click here to see the visualized instance of UniversalExpressionParser.IParseExpressionResult</p></a>