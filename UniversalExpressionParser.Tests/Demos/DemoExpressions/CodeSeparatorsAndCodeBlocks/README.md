# Code Separators and Code Blocks

- If the value of property **char ExpressionSeparatorCharacter { get; }** in **UniversalExpressionParser.IExpressionLanguageProvider** is not equal to character '\0', multiple expressions can be used in a single expression. 

For example if the value of property **ExpressionSeparatorCharacter** is ';' the expression "var x=f1(y);println(x)" will be parsed to a list of two expression items for "x=f1(y)" and "println(x)". Otherwise, the parser will report an error for this expression (see section on **Error Reporting** for more details on errors). 

- If both values of properties **char CodeBlockStartMarker { get; }** and **string CodeBlockEndMarker { get; }** in **UniversalExpressionParser.IExpressionLanguageProvider** are not equal to character '\0', code block expressions can be used to combine multiple expressions into code block expression items of type **UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem**. 

- For example if the values of properties **CodeBlockStartMarker** and **CodeBlockEndMarker** are '{' and '}', the expression below will be parsed to two code block expressions of type **UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem**. Otherwise, the parser will report errors.

```csharp
<IncludedFilePlaceHolder>SimpleExample.expr</IncludedFilePlaceHolder>
```

<details> <summary>Click to expand the parsed expression</summary>

```XML
<IncludedFilePlaceHolder>SimpleExample.parsed</IncludedFilePlaceHolder>
```
</details>

- More complex examples of code blocks and code separators:

```csharp
<IncludedFilePlaceHolder>MoreComplexExample.expr</IncludedFilePlaceHolder>
```

<details> <summary>Click to expand the parsed expression</summary>

```XML
<IncludedFilePlaceHolder>MoreComplexExample.parsed</IncludedFilePlaceHolder>
```
</details>