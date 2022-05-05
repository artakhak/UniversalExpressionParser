# Summary

**UniversalExpressionParser** is a library for parsing expressions like the one demonstrated below into expression items for functions, literals, operators, etc.

```csharp
<IncludedFilePlaceHolder>SummaryExpression.expr</IncludedFilePlaceHolder>
```

- Below is the demo code used to parse this expression:

```csharp
<IncludedFilePlaceHolder>..\SummaryDemo.cs</IncludedFilePlaceHolder>
```

- Expression is parsed to an instance of **UniversalExpressionParser.IParseExpressionResult** by calling the method  **IParseExpressionResult ParseExpression(string expressionLanguageProviderName, string expressionText, IParseExpressionOptions parseExpressionOptions)** in **UniversalExpressionParser.IExpressionParser**.

- The interface **UniversalExpressionParser.IParseExpressionResult** (i.e., result of parsing the expression) has a property **UniversalExpressionParser.ExpressionItems.IRooxExpressionItem RootExpressionItem { get; }** that stores the root expression item of a tree structure of parsed expression items.

- The code that evaluates the parsed expression can use the following properties in **UniversalExpressionParser.ExpressionItems.IRootExpressionItem** to iterate through all parsed expression items:

   - **IEnumerable&lt;IExpressionItemBase&gt; AllItems { get; }**
   - **IReadOnlyList&lt;IExpressionItemBase&gt; Prefixes { get; }**
   - **IReadOnlyList&lt;IKeywordExpressionItem&gt; AppliedKeywords { get; }**
   - **IReadOnlyList&lt;IExpressionItemBase&gt; RegularItems { get; }**
   - **IReadOnlyList&lt;IExpressionItemBase&gt; Children { get; }**
   - **IReadOnlyList&lt;IExpressionItemBase&gt; Postfixes { get; }**
   - **IReadOnlyList&lt;IExpressionItemBase&gt; Prefixes { get; }**

- Each instance of **UniversalExpressionParser.ExpressionItems.IExpressionItemBase** is currently an instance of one of the following interfaces in namespace **UniversalExpressionParser.ExpressionItems**, the state of which can be analyzed when evaluating the parsed expression: **IComplexExpressionItem**, **INameExpressionItem**, **INamedComplexExpressionItem**, **INumericExpressionItem**, **IBracesExpressionItem**, **ICodeBlockExpressionItem**, **ICustomExpressionItem**, **IKeywordExpressionItem**, **IOperatorInfoExpressionItem**, **IOperatorExpressionItem**.
 
- Below is the visualized instance of **UniversalExpressionParser.IParseExpressionResult**:

<details> <summary>Click to expand the parsed expression</summary>

```XML
<IncludedFilePlaceHolder>SummaryExpression.parsed</IncludedFilePlaceHolder>
```
</details>

- The format of valid expressions is defined by properties and methods in interface **UniversalExpressionParser.IExpressionLanguageProvider**. The expression language name **UniversalExpressionParser.IExpressionLanguageProvider.LanguageName** of some instance **UniversalExpressionParser.IExpressionLanguageProvider** is passed as a parameter to method **ParseExpression(...)** in **UniversalExpressionParser.IExpressionParser**, as demonstrated in example above. Most of the properties and methods of this interface are demonstrated in examples in sections below.

- The default abstract implementation of this interface in this package is **UniversalExpressionParser.ExpressionLanguageProviderBase**. In most cases, this abstract class can be extended and abstract methods and properties can be implemented, rather than providing a brand new implementation of **UniversalExpressionParser.IExpressionLanguageProvider**.

- The test project **UniversalExpressionParser.Tests** in git repository has a number of tests for testing successful parsing, as well as tests for testing expressions that result in errors (see section **Error Reporting** below). These tests generate random expressions as well as generate randomly configured instances of **UniversalExpressionParser.IExpressionLanguageProvider** to validate parsing of thousands of all possible languages and expressions (see the test classes **UniversalExpressionParser.Tests.SuccessfulParseTests.ExpressionParserSuccessfulTests** and **UniversalExpressionParser.Tests.ExpressionParseErrorTests.ExpressionParseErrorTests**).

- The demo expressions and tests used to parse the demo expressions in this documentation are in folder **Demos** in test project **UniversalExpressionParser.Tests**. This documentation uses implementations of **UniversalExpressionParser.IExpressionLanguageProvider** in project **UniversalExpressionParser.DemoExpressionLanguageProviders** in **git** repository.

- The parsed expressions in this documentation (i.e., instances of **UniversalExpressionParser.ExpressionItems.IParseExpressionResult**) are visualized into xml texts, that contain values of most properties of the parsed expression. However, to make the files shorter, the visualized xml files do not include all the property values.