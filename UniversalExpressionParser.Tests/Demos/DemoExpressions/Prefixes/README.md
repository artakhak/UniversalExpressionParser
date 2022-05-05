# Prefixes

Prefixes are one or more expression items that precede some other expression item, and are added to the list in property **Prefixes** in interface **UniversalExpressionParser.ExpressionItems.IComplexExpressionItem** for the expression item that follows the list of prefix expression items.

- **NOTE:** Prefixes are supported only if the value of property **SupportsPrefixes** in interface **UniversalExpressionParser.IExpressionLanguageProvider** is true.

Currently **Universal Expression Parser** supports two types of prefixes:

1) Square or round braces expressions items without names (i.e. expression items that are parsed to **UniversalExpressionParser.ExpressionItems.IBracesExpressionItem** with property **NamedExpressionItem** equal to **null**) that precede another expression item (e.g., another braces expression, a literal, a code block, text expression item, numeric value expression item, etc).

- In the example below the braces expression items parsed from "[NotNull, ItemNotNull]" and "(Attribute("MarkedFunction"))" will be added as prefixes to expression item parsed from "F1(x, x2)".

```csharp
<IncludedFilePlaceHolder>BracesPrefixesSimpleDemo.expr</IncludedFilePlaceHolder>
```

<details> <summary>Click to expand the parsed expression</summary>

```XML
<IncludedFilePlaceHolder>BracesPrefixesSimpleDemo.parsed</IncludedFilePlaceHolder>
```
</details>

2) One or more expressions that are parsed to custom expression items of type **UniversalExpressionParser.ExpressionItems.Custom.ICustomExpressionItem** with property **CustomExpressionItemCategory** equal to **UniversalExpressionParser.ExpressionItems.Custom.CustomExpressionItemCategory.Prefix** that precede another expression item. 

- In the example below, the expression items "::types[T1,T2]" and "::types[T3]" are parsed to custom expression items of type **UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem**, and are added as prefixes to braces expression item parsed from "F1(x:T1, y:T2, z: T3)".
- **NOTE:** For more details on custom expression items see section **Custom Expression Item Parsers**.

```csharp
<IncludedFilePlaceHolder>CustomExpressionItemsAsPrefixesSimpleDemo.expr</IncludedFilePlaceHolder>
```

<details> <summary>Click to expand the parsed expression</summary>

```XML
<IncludedFilePlaceHolder>CustomExpressionItemsAsPrefixesSimpleDemo.parsed</IncludedFilePlaceHolder>
```
</details>

- **NOTE:** The list of prefixes can include both types of prefixes at the same time (i.e., braces and custom expression items).

- Here is an example of prefixes used to model c# like attributes for classes and methods:

```csharp
<IncludedFilePlaceHolder>MoreComplexPrefixesDemo.expr</IncludedFilePlaceHolder>
```

<details> <summary>Click to expand the parsed expression</summary>

```XML
<IncludedFilePlaceHolder>MoreComplexPrefixesDemo.parsed</IncludedFilePlaceHolder>
```
</details>

- **NOTE:** The list of prefixes can include both types of prefixes at the same time (i.e., braces and custom expression items).

- Below is an example of using prefixes with different expression item types:

```csharp
<IncludedFilePlaceHolder>PrefixesUsedWithDifferentExpressionItems.expr</IncludedFilePlaceHolder>
```

<details> <summary>Click to expand the parsed expression</summary>

```XML
<IncludedFilePlaceHolder>PrefixesUsedWithDifferentExpressionItems.parsed</IncludedFilePlaceHolder>
```
</details>