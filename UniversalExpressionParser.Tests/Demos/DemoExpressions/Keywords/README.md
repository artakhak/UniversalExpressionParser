# Keywords

- Keywords are special names (e.g., **var**, **public**, **class**, **where**) that can be specified in property **IReadOnlyList&lt;ILanguageKeywordInfo&gt; Keywords { get; }** in interface **UniversalExpressionParser.IExpressionLanguageProvider**, as shown in example below.

- **NOTE:** Keywords are supported only if the value of property **SupportsKeywords** in **UniversalExpressionParser.IExpressionLanguageProvider** is true.

```csharp
public class DemoExpressionLanguageProvider : IExpressionLanguageProvider 
{
    ...
    public override IReadOnlyList<ILanguageKeywordInfo> Keywords { get; } = new []
    {
        new UniversalExpressionParser.UniversalExpressionParser(1, "where"),
        new UniversalExpressionParser.UniversalExpressionParser(2, "var"),
        ...
    }; 
}       
```

- Keywords are parsed to expression items of type **UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem**.
- Keywords have the following two applications.

1) One or more keywords can be placed in front of any literal (e.g., variable name), round or square braces expression, function or matrix expression, a code block. In this type of uage of keywords the parser parses the keywords and adds the list of parsed keyword expression items (i.e., list of **UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem** objects) to list in property **IReadOnlyList&lt;IKeywordExpressionItem&gt; AppliedKeywords { get; }** in **UniversalExpressionParser.ExpressionItems.IComplexExpressionItem** for the expression item that follows the list of keywords.

2) Custom expression parser evaluates the list of parsed keywords to determine if the expression that follows the keywords should be parsed to a custom expression item.
   See section **Custom Expression Item Parsers** for more details on custom expression parsers.

- Examples of keywords: 

```csharp
<IncludedFilePlaceHolder>Keywords.expr</IncludedFilePlaceHolder>
```

<details> <summary>Click to expand the parsed expression</summary>

```XML
<IncludedFilePlaceHolder>Keywords.parsed</IncludedFilePlaceHolder>
```
</details>