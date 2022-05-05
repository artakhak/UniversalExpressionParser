# Comments

The interface **UniversalExpressionParser.IExpressionLanguageProvider** has properties **string LineCommentMarker { get; }**, **string MultilineCommentStartMarker { get; }**, and **string MultilineCommentEndMarker { get; }** for specifying comment markers.

If the values of these properties are not null, line and code block comments can be used.

The abstract implementation **UniversalExpressionParser.ExpressionLanguageProviderBase** of **UniversalExpressionParser.IExpressionLanguageProvider** overrides these properties to return "//", "/*", and "*/" (the values of these properties can be overridden in subclasses).

The on commented out code data is stored in property **IReadOnlyList&lt;UniversalExpressionParser.ICommentedTextData&gt; SortedCommentedTextData { get; }** in **UniversalExpressionParser.IParsedExpressionResult**, an instance of which is returned by the call to method **UniversalExpressionParser.IExpressionParser.ParseExpression(...)**.

- Below are some examples of line and code block comments:

```csharp
<IncludedFilePlaceHolder>Comments.expr</IncludedFilePlaceHolder>
```

<details> <summary>Click to expand the parsed expression</summary>

```XML
<IncludedFilePlaceHolder>Comments.parsed</IncludedFilePlaceHolder>
```
</details>

<details> <summary>Click to expand the interface UniversalExpressionParser.ICommentedTextData</summary>

```XML
<IncludedFilePlaceHolder>..\..\..\..\UniversalExpressionParser\ICommentedTextData.cs</IncludedFilePlaceHolder>
```
</details>