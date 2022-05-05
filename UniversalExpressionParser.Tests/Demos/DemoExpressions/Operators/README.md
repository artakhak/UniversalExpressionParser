# Operators

- Operators that the valid expressions can use are defined in property **System.Collections.Generic.IReadOnlyList<UniversalExpressionParser.IOperatorInfo> Operators { get; }** in interface **UniversalExpressionParser.IExpressionLanguageProvider** (an instance of this interface is passed to the parser).
- The interface **UniversalExpressionParser.IOperatorInfo** has properties for operator name (i.e., a collection of texts that operator consists of, such as ["IS", "NOT", "NUL"] or ["+="]), priority, unique Id, operator type (i.e., binary, unary prefix or unary postfix).
- Two different operators can have similar names, as long as they have different operator. For example "++" can be used both as unary prefix as well as unary postfix operator.

<details> <summary>Click to see example of defining operators in implementation of UniversalExpressionParser.IExpressionLanguageProvider.</summary>

```csharp
public class TestExpressionLanguageProviderBase : ExpressionLanguageProviderBase
{
    //...
    // Some other method and property implementations here
    // ...
    public override IReadOnlyList<IOperatorInfo> Operators { get; } = new IOperatorInfo[] 
    {
    	// The third parameter (e.g., 0) is the priority.
    	new OperatorInfo(1, new [] {"!"}, OperatorType.PrefixUnaryOperator, 0),
    	new OperatorInfo(2, new [] {"IS", "NOT", "NULL"}, OperatorType.PostfixUnaryOperator, 0),
    
    	new OperatorInfo(3, new [] {"*"}, OperatorType.BinaryOperator, 10),
    	new OperatorInfo(4, new [] {"/"}, OperatorType.BinaryOperator, 10),
    
    	new OperatorInfo(5, new [] {"+"}, OperatorType.BinaryOperator, 30),
	    new OperatorInfo(6, new [] {"-"}, OperatorType.BinaryOperator, 30),
    }
}
```
</details>

- Operator expression (e.g., "a * b + c * d") is parsed to an expression item of type **UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem** (a subclass of **UniversalExpressionParser.ExpressionItems.IComplexExpressionItem**).
  
<details> <summary>Click to see the definition of UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem.</summary>

```csharp
<IncludedFilePlaceHolder>..\..\..\..\UniversalExpressionParser\ExpressionItems\IOperatorExpressionItem.cs</IncludedFilePlaceHolder>
```
</details>

For example the expression "a * b + c * d", will be parsed to an expression logically similar to "*(+(a, b), +(x,d))". 

This is true since the binary operator "+" has lower priority (the value of **IOperatorInfo.Priority** is larger), than the binary operator "*". 

In other words this expression will be parsed to a binary operator expression item for "+" (i.e., an instance of **IOperatorExpressionItem**) with Operand1 and Operand2 also being binary operator expression items of type **UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem** for expression items "a * b" and "c * d".

- Example of parsing operators:

```csharp
<IncludedFilePlaceHolder>OperatorPriorities.expr</IncludedFilePlaceHolder>
```

<details> <summary>Click to expand the parsed expression</summary>

```XML
<IncludedFilePlaceHolder>OperatorPriorities.parsed</IncludedFilePlaceHolder>
```
</details>


- Example of using braces to change order of application of operators:

```csharp
<IncludedFilePlaceHolder>BracesToChangeOperatorEvaluationOrder.expr</IncludedFilePlaceHolder>
```

<details> <summary>Click to expand the parsed expression</summary>

```XML
<IncludedFilePlaceHolder>BracesToChangeOperatorEvaluationOrder.parsed</IncludedFilePlaceHolder>
```
</details>


- Example of operators with multiple parts in operator names:

```csharp
<IncludedFilePlaceHolder>MultipartOperators.expr</IncludedFilePlaceHolder>
```

<details> <summary>Click to expand the parsed expression</summary>

```XML
<IncludedFilePlaceHolder>MultipartOperators.parsed</IncludedFilePlaceHolder>
```
</details>

- Example of two operators (e.g., postfix operators, then a binary operator) used next to each other without spaces in between:

```csharp
<IncludedFilePlaceHolder>NoSpacesBetweenOperators.expr</IncludedFilePlaceHolder>
```

<details> <summary>Click to expand the parsed expression</summary>

```XML
<IncludedFilePlaceHolder>NoSpacesBetweenOperators.parsed</IncludedFilePlaceHolder>
```
</details>

- Example of unary prefix operator used to implement "return" statement:

```csharp
<IncludedFilePlaceHolder>UnaryPrefixOperatorUsedForReturnStatement.expr</IncludedFilePlaceHolder>
```

<details> <summary>Click to expand the parsed expression</summary>

```XML
<IncludedFilePlaceHolder>UnaryPrefixOperatorUsedForReturnStatement.parsed</IncludedFilePlaceHolder>
```
</details>