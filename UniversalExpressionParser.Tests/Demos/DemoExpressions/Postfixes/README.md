# Postfixes

Postfixes are one or more expression items that are placed after some other expression item, and are added to the list in property **Postfixes** in interface **UniversalExpressionParser.ExpressionItems.IComplexExpressionItem** for the expression item that the postfixes are placed after.

Currently **Universal Expression Parser** supports two types of postfixes:

1) Code block expression items that are parsed to **UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem** that succeed another expression item.

- **NOTE:** The following are expression types that can have postfixes: Literals, such a x1 or Dog, braces expression items, such as f(x1), (y), m1[x1], [x2], or custom expression items for which property **CustomExpressionItemCategory** in interface **UniversalExpressionParser.ExpressionItems.Custom.ICustomExpressionItem** is equal to **UniversalExpressionParser.ExpressionItems.Custom.CustomExpressionItemCategory.Regular**. 
  
- In the example below the code block expression item of type **UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem** parsed from expression that starts with '**{**' and ends with '**}**'" will be added to the list **Postfixes** in **UniversalExpressionParser.ExpressionItems.IComplexExpressionItem** for the literal expression item parsed from expression **Dog**.

```csharp
<IncludedFilePlaceHolder>SimpleCodeBlockPostfixAfterLiteral.expr</IncludedFilePlaceHolder>
```

<details> <summary>Click to expand the parsed expression</summary>

```XML
<IncludedFilePlaceHolder>SimpleCodeBlockPostfixAfterLiteral.parsed</IncludedFilePlaceHolder>
```
</details>

2) One or more expressions that are parsed to custom expression items of type **UniversalExpressionParser.ExpressionItems.Custom.ICustomExpressionItem** with property **CustomExpressionItemCategory** equal to **UniversalExpressionParser.ExpressionItems.Custom.CustomExpressionItemCategory.Postfix** that succeed another expression item.

- In the example below the two custom expression items of type **UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IWhereCustomExpressionItem** parsed from expressions that start with "where" and end with "whereend"" as well as the code block will be added as postfixes to literal expression item parsed from "Dog".

- **NOTE:** For more details on custom expression items see section **Custom Expression Item Parsers**.

```csharp
<IncludedFilePlaceHolder>SimpleCustomExpressionItemAsPostfixAfterLiteral.expr</IncludedFilePlaceHolder>
```

<details> <summary>Click to expand the parsed expression</summary>

```XML
<IncludedFilePlaceHolder>SimpleCustomExpressionItemAsPostfixAfterLiteral.parsed</IncludedFilePlaceHolder>
```
</details>

- **NOTE:** The list of postfixes can include both types of postfixes at the same time (i.e., custom expression items as well as a code block postfix).

- Example of a code block postfix used to model function body:
 
```csharp
<IncludedFilePlaceHolder>CodeBlockPostfixToModelFunctionBody.expr</IncludedFilePlaceHolder>
```

<details> <summary>Click to expand the parsed expression</summary>

```XML
<IncludedFilePlaceHolder>CodeBlockPostfixToModelFunctionBody.parsed</IncludedFilePlaceHolder>
```
</details>

- Example of code block postfix used to model class definition:

```csharp
<IncludedFilePlaceHolder>CodeBlockPostfixUsedToModelAClassDefinition.expr</IncludedFilePlaceHolder>
```

<details> <summary>Click to expand the parsed expression</summary>

```XML
<IncludedFilePlaceHolder>CodeBlockPostfixUsedToModelAClassDefinition.parsed</IncludedFilePlaceHolder>
```
</details>

- Below are some more examples of postfixes with different expression items:

```csharp
<IncludedFilePlaceHolder>CodeBlockPostfixForDifferentExpressionItems.expr</IncludedFilePlaceHolder>
```

<details> <summary>Click to expand the parsed expression</summary>

```XML
<IncludedFilePlaceHolder>CodeBlockPostfixForDifferentExpressionItems.parsed</IncludedFilePlaceHolder>
```
</details>