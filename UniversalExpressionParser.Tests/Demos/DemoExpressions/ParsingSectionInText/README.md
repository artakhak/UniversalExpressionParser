# Parsing Section in Text

- Sometimes we want to parse a single braces expression at specific location in text (i.e., an expression starting with "(" or "[" and ending in ")" or "]" correspondingly) or single code block expression (i.e., an expression starting with **UniversalExpressionParser.IExpressionLanguageProvider.CodeBlockStartMarker** and ending in **UniversalExpressionParser.IExpressionLanguageProvider.CodeBlockEndMarker**). In these scenarios, we want the parser to stop right after fully parsing the braces or code block expression.

- The interface **UniversalExpressionParser.IExpressionParser** has two methods for doing just that. 
 
- The methods for parsing single braces or code block expression are **UniversalExpressionParser.IParseExpressionResult ParseBracesExpression(string expressionLanguageProviderName, string expressionText, IParseExpressionOptions parseExpressionOptions)** and **UniversalExpressionParser.IParseExpressionResult ParseCodeBlockExpression(string expressionLanguageProviderName, string expressionText, IParseExpressionOptions parseExpressionOptions)**, and are demonstrated in sub-sections below.

- The parsed expression of type **UniversalExpressionParser.IParseExpressionResult** returned by these methods has a property **int PositionInTextOnCompletion { get; }** that stores the position in text, after the parsing is complete (i.e., the position after closing brace or code block end marker).

## Parsing Single Round Braces Expression at Specific Location

- Below is an an SQLite table definition in which we want to parse only the braces expression **(SALARY > 0 AND SALARY > MAX_SALARY/2 AND f1(SALARY) < f2(MAX_SALARY))**, and stop parsing right after the closing brace ')'.

```csharp
<IncludedFilePlaceHolder>ParseSingleRoundBracesExpressionDemo.expr</IncludedFilePlaceHolder>
```

- The method **ParseBracesAtCurrentPosition(string expression, int positionInText)** in class **UniversalExpressionParser.Tests.Demos.ParseSingleBracesExpressionAtPositionDemo** (shown below) demonstrates how to parse the braces expression **(SALARY > 0 AND SALARY > MAX_SALARY/2 AND f1(SALARY) < f2(MAX_SALARY))**, by passing the position of opening brace in parameter **positionInText**.

<details> <summary>Click to expand the class UniversalExpressionParser.Tests.Demos.ParseSingleBracesExpressionAtPositionDemo</summary>

```XML
<IncludedFilePlaceHolder>../../ParseSingleBracesExpressionAtPositionDemo.cs</IncludedFilePlaceHolder>
```
</details>

<details> <summary>Click to expand the single braces expression parsed from text above is shown here</summary>

```XML
<IncludedFilePlaceHolder>ParseSingleRoundBracesExpressionDemo.parsed</IncludedFilePlaceHolder>
```
</details>

- Here is square braces expression **[f1()+m1[], f2{++i;}]** between texts 'any text before braces' and 'any text after braces...', which can also be parsed using the code in class **UniversalExpressionParser.Tests.Demos.ParseSingleBracesExpressionAtPositionDemo**.

```csharp
<IncludedFilePlaceHolder>ParseSingleSquareBracesExpressionDemo.expr</IncludedFilePlaceHolder>
```

<details> <summary>Click to expand the parsed expression</summary>

```XML
<IncludedFilePlaceHolder>ParseSingleSquareBracesExpressionDemo.parsed</IncludedFilePlaceHolder>
```
</details>

## Parsing Single Code Block Expression at Specific Location

Below is a text with code block expression **{f1(f2()+m1[], f2{++i;})}** between texts 'any text before code block' and 'any text after code block...' that we want to parse.

```csharp
<IncludedFilePlaceHolder>ParseSingleSquareBracesExpressionDemo.expr</IncludedFilePlaceHolder>
```

- The method **IParseExpressionResult ParseCodeBlockExpressionAtCurrentPosition(string expression, int positionInText)** in class **UniversalExpressionParser.Tests.Demos.ParseSingleCodeBlockExpressionAtPositionDemo** demonstrates how to parse the single code block expression **{f1(f2()+m1[], f2{++i;})}**, by passing the position of code block start marker '{' in parameter **positionInText**.

<details> <summary>Click to expand the class UniversalExpressionParser.Tests.Demos.ParseSingleCodeBlockExpressionAtPositionDemo</summary>

```XML
<IncludedFilePlaceHolder>../../ParseSingleCodeBlockExpressionAtPositionDemo.cs</IncludedFilePlaceHolder>
```
</details>

<details> <summary>Click to expand the parsed expression</summary>

```XML
<IncludedFilePlaceHolder>ParseSingleCodeBlockExpressionDemo.parsed</IncludedFilePlaceHolder>
```
</details>