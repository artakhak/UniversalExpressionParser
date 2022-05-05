# Case Sensitivity and Non Standard Language Features

## Case Sensitivity

- Case sensitivity is controlled by property **bool IsLanguageCaseSensitive { get; }** in interface **UniversalExpressionParser.IExpressionLanguageProvider**.

- If the value of this property **IsLanguageCaseSensitive** is **true**, any two expressions are considered different, if the expressions are the same, except for capitalization of some of the text (say "public class Dog" vs "Public ClaSS DOg"). Otherwise, if the value of property **IsLanguageCaseSensitive** is **false**, the capitalization of any expression items does not matter (i.e., parsing will succeed regardless of capitalization in expression).

- For example C# is considered a case sensitive language, and Visual Basic is considered case insensitive.

- The value of property **IsLanguageCaseSensitive** in abstract implementation **UniversalExpressionParser.ExpressionLanguageProviderBase** of **UniversalExpressionParser.IExpressionLanguageProvider** returns **true**.

- The expression below demonstrates parsing the expression by **UniversalExpressionParser.IExpressionLanguageProvider** with overridden **IsLanguageCaseSensitive** to return **false**.

## Non Standard Comment Markers

- The properties **string LineCommentMarker { get; }**, **string MultilineCommentStartMarker { get; }**, and **string MultilineCommentEndMarker { get; }** in interface **UniversalExpressionParser.IExpressionLanguageProvider** determine the line comment marker as well as code block comment start and end markers.

- The default implementation **UniversalExpressionParser.ExpressionLanguageProviderBase** of **UniversalExpressionParser.IExpressionLanguageProvider** returns "//", "/*", and "*/" for these properties to use C# like comments. However, other values can be used for these properties.

- The expression below demonstrates parsing the expression by an instance of **UniversalExpressionParser.IExpressionLanguageProvider** with overridden **LineCommentMarker**, **MultilineCommentStartMarker**, and **MultilineCommentEndMarker** to return "rem", "rem*", "*rem".

## Non Standard Code Separator Character and Code Block Markers

- The properties **char ExpressionSeparatorCharacter { get; }**, **string CodeBlockStartMarker { get; }**, and **string CodeBlockEndMarker { get; }** in interface **UniversalExpressionParser.IExpressionLanguageProvider** determine the code separator character, as well as the code block start and end markers.

- The default implementation **UniversalExpressionParser.ExpressionLanguageProviderBase** of **UniversalExpressionParser.IExpressionLanguageProvider** returns ";", "{", and "}" for these properties to use C# like code separator and code block markers. However, other values can be used for these properties.

- The expression below demonstrates parsing the expression by an instance of **UniversalExpressionParser.IExpressionLanguageProvider** with overridden **ExpressionSeparatorCharacter**, **CodeBlockStartMarker**, and **CodeBlockEndMarker** to return ";", "BEGIN", and "END".

## Example Demonstrating Case Insensitivity and Non Standard Language Features
 
- The expression below is parsed using the expression language provider **UniversalExpressionParser.DemoExpressionLanguageProviders.VerboseCaseInsensitiveExpressionLanguageProvider**, which overrides **IsLanguageCaseSensitive** to return **false**. As can bee seen in this example, the keywords (e.g., **var**, **public**, **class**, **::pragma**, etc), non standard code comment markers (i.e., "rem", "rem*", "*rem"), code block markers (i.e., "BEGIN", "END") and operators **IS NULL**, **IS NOT NULL** can be used with any capitalization, and the expression is still parsed without errors. 

```csharp
<IncludedFilePlaceHolder>CaseSensitivityAndNonStandardLanguageFeatures.expr</IncludedFilePlaceHolder>
```

<details> <summary>Click to expand the parsed expression</summary>

```XML
<IncludedFilePlaceHolder>CaseSensitivityAndNonStandardLanguageFeatures.parsed</IncludedFilePlaceHolder>
```
</details>