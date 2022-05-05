# Functions and Braces

- Braces are a pair of round or square braces ((e.g., **(x)**, **(x) {}**, **[i,j]**, **[i,j]{}**)). Braces are parsed to expression items of type **UniversalExpressionParser.ExpressionItems.IBracesExpressionItem** with the value of property **ILiteralExpressionItem Name { get; }** equal to null.
- Functions are round or square braces preceded with a literal (e.g., **F1(x)**, **F1(x) {}**, **m1[i,j]**, **m1[i,j]{}**). Functions are  parsed to expression items of type **UniversalExpressionParser.ExpressionItems.IBracesExpressionItem** with the value of property **ILiteralExpressionItem Name { get; }** equal to a literal that precedes the braces.

- Examples of braces:

```csharp
<IncludedFilePlaceHolder>RoundAndSquareBraces.expr</IncludedFilePlaceHolder>
```

<details> <summary>Click to expand the parsed expression</summary>

```XML
<IncludedFilePlaceHolder>RoundAndSquareBraces.parsed</IncludedFilePlaceHolder>
```
</details>

- Examples of functions:

```csharp
<IncludedFilePlaceHolder>FunctionsWithRoundAndSquareBraces.expr</IncludedFilePlaceHolder>
```

<details> <summary>Click to expand the parsed expression</summary>

```XML
<IncludedFilePlaceHolder>FunctionsWithRoundAndSquareBraces.parsed</IncludedFilePlaceHolder>
```
</details>