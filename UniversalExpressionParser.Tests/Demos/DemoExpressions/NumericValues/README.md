# Numeric Values

- **Universal Expression Parser** parses expression items that have specific format to expression items of type **UniversalExpressionParser.ExpressionItems.INumericExpressionItem**. The format of expression items that will be parsed to **UniversalExpressionParser.ExpressionItems.INumericExpressionItem** is determined by list property **IReadOnlyList<NumericTypeDescriptor> NumericTypeDescriptors { get; }** in interface **UniversalExpressionParser.IExpressionLanguageProvider**, an instance of which is passed to the parser.
- Below is the definition of **UniversalExpressionParser.NumericTypeDescriptor**. 

<details> <summary>Click to expand the class UniversalExpressionParser.NumericTypeDescriptor.</summary>

```csharp
<IncludedFilePlaceHolder>..\..\..\..\UniversalExpressionParser\NumericTypeDescriptor.cs</IncludedFilePlaceHolder>
```
</details>

<details> <summary>Click to expand the class UniversalExpressionParser.ExpressionItems.INumericExpressionItem.</summary>

```csharp
<IncludedFilePlaceHolder>..\..\..\..\UniversalExpressionParser\ExpressionItems\INumericExpressionItem.cs</IncludedFilePlaceHolder>
```
</details>

- The parser scans the regular expressions in list in property **IReadOnlyList<string> RegularExpressions { get; }** in type **NumericTypeDescriptor** for each provided instance of **UniversalExpressionParser.NumericTypeDescriptor** to try to parse the expression to numeric expression item of type **UniversalExpressionParser.ExpressionItems.INumericExpressionItem**. 
   
- The abstract class **UniversalExpressionParser.ExpressionLanguageProviderBase** that can be used as a base class for implementations of **UniversalExpressionParser.IExpressionLanguageProvider** in most cases, implements the property **NumericTypeDescriptors** as a virtual property. The implementation of property **NumericTypeDescriptors** in **UniversalExpressionParser.ExpressionLanguageProviderBase** is demonstrated below, and it can be overridden to provide different format for numeric values:
 
   **NOTE** The regular expressions used in implementation of property **NumericTypeDescriptors** should always tart with '**^**' and should never end with '**$**'.

```csharp
public virtual IReadOnlyList<NumericTypeDescriptor> NumericTypeDescriptors { get; } = new List<NumericTypeDescriptor>
{   
    new NumericTypeDescriptor(KnownNumericTypeDescriptorIds.ExponentFormatValueId, 
    new[] { @"^(\d+\.\d+|\d+\.|\.\d+|\d+)(EXP|exp|E|e)[+|-]?(\d+\.\d+|\d+\.|\.\d+|\d+)"}),
    
    new NumericTypeDescriptor(KnownNumericTypeDescriptorIds.FloatingPointValueId, 
    new[] { @"^(\d+\.\d+|\d+\.|\.\d+)"}),    
    
    new NumericTypeDescriptor(KnownNumericTypeDescriptorIds.IntegerValueId, new[] { @"^\d+" })
}
```

- The first regular expression that matches the expression, is stored in properties **SucceededNumericTypeDescriptor** of type **UniversalExpressionParser.NumericTypeDescriptor** and **IndexOfSucceededRegularExpression** in parsed expression item of type **UniversalExpressionParser.ExpressionItems.INumericExpressionItem**.
- The numeric value is stored as text in property **INameExpressionItem Value** in text format. Therefore, there is no limit on numeric value digits.
- The expression evaluator that uses the **Universal Expression Parser** can convert the textual value in property **Value** of type **INameExpressionItem** in **UniversalExpressionParser.ExpressionItems.INumericExpressionItem** to a value of numeric type (int, long, double, etc.).
- Examples of numeric value expression items are demonstrated below:

```csharp
<IncludedFilePlaceHolder>NumericValues.expr</IncludedFilePlaceHolder>
```

<details> <summary>Click to expand the parsed expression</summary>

```XML
<IncludedFilePlaceHolder>NumericValues.parsed</IncludedFilePlaceHolder>
```
</details>