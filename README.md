# UniversalExpressionParser

# NOTE: CLICK ON https://github.com/artakhak/UniversalExpressionParser/blob/main/README.md to view the non-trimmed version of documentation.

# Summary

**UniversalExpressionParser** is a library for parsing expressions like the one demonstrated below into expression items for functions, literals, operators, etc.

```csharp
var z = x1*y1+x2*y2;

var matrixMultiplicationResult = [[x1_1, x1_2, x1_3], [x2_1, x2_2, x2_3]]*[[y1_1, x1_2], [y2_1, x2_2], [y3_1, x3_2]];

println(matrixMultiplicationResult);

[NotNull] [PublicName("Calculate")]
public F1(x, y) : int => 
{
    
    /*This demos multiline
    comments that can be placed anywhere*/
    ++y /*another multiline comment*/;
    return 1.3EXP-2.7+ ++x+y*z; // Line comments.
}

public abstract class ::metadata{description: "Demo prefix"} ::types[T1, T2, T3] Animal
   where T1: IType1 where T2: T1, IType2 whereend
   where T3: IType3 whereend
{    
    public abstract Move() : void;    
}

public class Dog : (Anymal)
{
    public override Move() : void => println("Jump");
}


```

- Below is the demo code used to parse this expression:

```csharp
using TextParser;
using UniversalExpressionParser.DemoExpressionLanguageProviders;
using UniversalExpressionParser.ExpressionItems;

namespace UniversalExpressionParser.Tests.Demos
{
    public class SummaryDemo
    {
        private readonly IExpressionParser _expressionParser;
        private readonly IExpressionLanguageProvider _nonVerboseLanguageProvider = new NonVerboseCaseSensitiveExpressionLanguageProvider();
        private readonly IExpressionLanguageProvider _verboseLanguageProvider = new VerboseCaseInsensitiveExpressionLanguageProvider();
        private readonly IParseExpressionOptions _parseExpressionOptions = new ParseExpressionOptions();

        public SummaryDemo()
        {
            IExpressionLanguageProviderCache expressionLanguageProviderCache = 
                new ExpressionLanguageProviderCache(new DefaultExpressionLanguageProviderValidator());
            
            _expressionParser = new ExpressionParser(new TextSymbolsParserFactory(), expressionLanguageProviderCache);

            expressionLanguageProviderCache.RegisterExpressionLanguageProvider(_nonVerboseLanguageProvider);
            expressionLanguageProviderCache.RegisterExpressionLanguageProvider(_verboseLanguageProvider);
        }
     
        public IRootExpressionItemSeries ParseNonVerboseExpression(string expression)
        {
            /*
            The same instance _expressionParser of UniversalExpressionParser.IExpressionParser can be used
            to parse multiple expressions using different instances of UniversalExpressionParser.IExpressionLanguageProvider
            Example:

            var parsedExpression1 = _expressionParser.ParseExpression(_nonVerboseLanguageProvider, "var x=2*y; f1() {++x;} f1();");
            var parsedExpression2 = _expressionParser.ParseExpression(_verboseLanguageProvider, "var x=2*y; f1() BEGIN ++x;END f1();");
            */
            return _expressionParser.ParseExpression(_nonVerboseLanguageProvider, expression, _parseExpressionOptions);
        }
    }
}

```

- Expression is parsed to an instance of **UniversalExpressionParser.ExpressionItems.IRootExpressionItemSeries** by calling the method  **IRootExpressionItemSeries ParseExpression(IExpressionLanguageProvider expressionLanguageProvider, string expressionText, IParseExpressionOptions parseExpressionOptions)** in **UniversalExpressionParser.IExpressionParser**.

- The interface **UniversalExpressionParser.ExpressionItems.IRootExpressionItemSeries** (i.e., type of the parsed expression) extends **UniversalExpressionParser.ExpressionItems.IRootExpressionItem** which has a property **UniversalExpressionParser.ExpressionItems.IComplexExpressionItem MainExpressionItem { get; }** that stores the root expression item of a tree structure of parsed expression items.

- The code that evaluates the parsed expression can use the following properties in **UniversalExpressionParser.ExpressionItems.IComplexExpressionItem** (e.g., the type of property **MainExpressionItem** in interface **UniversalExpressionParser.ExpressionItems.IRootExpressionItem** in parsed expression) to iterate through all parsed expression items:

   - **IEnumerable&lt;IExpressionItemBase&gt; AllItems { get; }**
   - **IReadOnlyList&lt;IExpressionItemBase&gt; Prefixes { get; }**
   - **IReadOnlyList&lt;IKeywordExpressionItem&gt; AppliedKeywords { get; }**
   - **IReadOnlyList&lt;IExpressionItemBase&gt; RegularItems { get; }**
   - **IReadOnlyList&lt;IExpressionItemBase&gt; Children { get; }**
   - **IReadOnlyList&lt;IExpressionItemBase&gt; Postfixes { get; }**
   - **IReadOnlyList&lt;IExpressionItemBase&gt; Prefixes { get; }**

- Each instance of **UniversalExpressionParser.ExpressionItems.IExpressionItemBase** is currently an instance of one of the following interfaces in namespace **UniversalExpressionParser.ExpressionItems**, the state of which can be analyzed when evaluating the parsed expression: **INameExpressionItem**, **INamedComplexExpressionItem**, **INumericValueExpressionItem**, **IBracesExpressionItem**, **ICodeBlockExpressionItem**, **ICustomExpressionItem**, **IKeywordExpressionItem**, **IOperatorExpressionItem**.
 
- Below is the visualized instance of **UniversalExpressionParser.ExpressionItems.IRootExpressionItemSeries**

<details> <summary>Click to expand the parsed expression</summary>

```XML
<?xml version="1.0" encoding="utf-8"?>
<UniversalExpressionParser.ExpressionItems.IRootExpressionItemSeries ObjectId='0' PositionInTextOnCompletion='740' IndexInText='0' ItemLength='736'>
	<ExpressionItemSeries Id='637840162086530587' IndexInText='0' ItemLength='736' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'>
		<RegularItems>
			<BinaryOperator Name='=' Priority='2000' Id='637840162086531889' IndexInText='0' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
				<Operand1.Literal Id='637840162086531042' IndexInText='0' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
					<NameExpressionItem Name='z' Id='637840162086531039' IndexInText='4' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<AppliedKeywords>
						<Keyword Name='var' Id='637840162086530873' IndexInText='0' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
							<LanguageKeywordInfo ObjectId='6' Id='637781064051574641' Keyword='var' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
						</Keyword>
					</AppliedKeywords>
				</Operand1.Literal>
				<OperatorInfo OperatorType='BinaryOperator' Name='=' Priority='2000' Id='637840162086531149' IndexInText='6' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
					<OperatorNameParts>
						<Name Name='=' Id='637840162086531141' IndexInText='6' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</OperatorNameParts>
				</OperatorInfo>
				<Operand2.BinaryOperator Name='+' Priority='30' Id='637840162086531910' IndexInText='8' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
					<Operand1.BinaryOperator Name='*' Priority='20' Id='637840162086531904' IndexInText='8' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.Literal Id='637840162086531288' IndexInText='8' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='x1' Id='637840162086531285' IndexInText='8' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand1.Literal>
						<OperatorInfo OperatorType='BinaryOperator' Name='*' Priority='20' Id='637840162086531359' IndexInText='10' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<Name Name='*' Id='637840162086531355' IndexInText='10' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OperatorNameParts>
						</OperatorInfo>
						<Operand2.Literal Id='637840162086531490' IndexInText='11' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='y1' Id='637840162086531487' IndexInText='11' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand2.Literal>
					</Operand1.BinaryOperator>
					<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637840162086531549' IndexInText='13' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
						<OperatorNameParts>
							<Name Name='+' Id='637840162086531546' IndexInText='13' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OperatorNameParts>
					</OperatorInfo>
					<Operand2.BinaryOperator Name='*' Priority='20' Id='637840162086531914' IndexInText='14' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.Literal Id='637840162086531671' IndexInText='14' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='x2' Id='637840162086531668' IndexInText='14' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand1.Literal>
						<OperatorInfo OperatorType='BinaryOperator' Name='*' Priority='20' Id='637840162086531718' IndexInText='16' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<Name Name='*' Id='637840162086531715' IndexInText='16' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OperatorNameParts>
						</OperatorInfo>
						<Operand2.Literal Id='637840162086531847' IndexInText='17' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='y2' Id='637840162086531845' IndexInText='17' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand2.Literal>
					</Operand2.BinaryOperator>
				</Operand2.BinaryOperator>
			</BinaryOperator>
			<ExpressionSeparator Name=';' Id='637840162086531970' IndexInText='19' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<BinaryOperator Name='=' Priority='2000' Id='637840162086535466' IndexInText='24' ItemLength='116' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
				<Operand1.Literal Id='637840162086532085' IndexInText='24' ItemLength='30' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
					<NameExpressionItem Name='matrixMultiplicationResult' Id='637840162086532083' IndexInText='28' ItemLength='26' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<AppliedKeywords>
						<Keyword Name='var' Id='637840162086531990' IndexInText='24' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
							<LanguageKeywordInfo ObjectId='6' Id='637781064051574641' Keyword='var' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
						</Keyword>
					</AppliedKeywords>
				</Operand1.Literal>
				<OperatorInfo OperatorType='BinaryOperator' Name='=' Priority='2000' Id='637840162086532155' IndexInText='55' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
					<OperatorNameParts>
						<Name Name='=' Id='637840162086532151' IndexInText='55' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</OperatorNameParts>
				</OperatorInfo>
				<Operand2.BinaryOperator Name='*' Priority='20' Id='637840162086535485' IndexInText='57' ItemLength='83' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
					<Operand1.Braces Id='637840162086532255' IndexInText='57' ItemLength='40' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<OpeningSquareBrace Name='[' Id='637840162086532259' IndexInText='57' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Braces Id='637840162086532289' IndexInText='58' ItemLength='18' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
								<RegularItems>
									<OpeningSquareBrace Name='[' Id='637840162086532292' IndexInText='58' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637840162086532370' IndexInText='59' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='x1_1' Id='637840162086532368' IndexInText='59' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<Comma Name=',' Id='637840162086532405' IndexInText='63' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637840162086532483' IndexInText='65' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='x1_2' Id='637840162086532481' IndexInText='65' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<Comma Name=',' Id='637840162086532507' IndexInText='69' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637840162086532586' IndexInText='71' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='x1_3' Id='637840162086532584' IndexInText='71' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<ClosingSquareBrace Name=']' Id='637840162086532614' IndexInText='75' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Children>
									<Literal Id='637840162086532370' IndexInText='59' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<Literal Id='637840162086532483' IndexInText='65' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<Literal Id='637840162086532586' IndexInText='71' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								</Children>
								<OtherProperties>
									<OpeningBraceInfo Name='[' Id='637840162086532292' IndexInText='58' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ClosingBraceInfo Name=']' Id='637840162086532614' IndexInText='75' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
									<Parameters ObjectId='47' Count='3' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162086532370' IndexInText='59' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162086532483' IndexInText='65' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162086532586' IndexInText='71' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</Parameters>
								</OtherProperties>
							</Braces>
							<Comma Name=',' Id='637840162086532647' IndexInText='76' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Braces Id='637840162086532668' IndexInText='78' ItemLength='18' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
								<RegularItems>
									<OpeningSquareBrace Name='[' Id='637840162086532670' IndexInText='78' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637840162086532748' IndexInText='79' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='x2_1' Id='637840162086532746' IndexInText='79' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<Comma Name=',' Id='637840162086532773' IndexInText='83' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637840162086532851' IndexInText='85' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='x2_2' Id='637840162086532848' IndexInText='85' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<Comma Name=',' Id='637840162086532874' IndexInText='89' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637840162086532950' IndexInText='91' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='x2_3' Id='637840162086532948' IndexInText='91' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<ClosingSquareBrace Name=']' Id='637840162086532975' IndexInText='95' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Children>
									<Literal Id='637840162086532748' IndexInText='79' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<Literal Id='637840162086532851' IndexInText='85' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<Literal Id='637840162086532950' IndexInText='91' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								</Children>
								<OtherProperties>
									<OpeningBraceInfo Name='[' Id='637840162086532670' IndexInText='78' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ClosingBraceInfo Name=']' Id='637840162086532975' IndexInText='95' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
									<Parameters ObjectId='60' Count='3' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162086532748' IndexInText='79' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162086532851' IndexInText='85' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162086532950' IndexInText='91' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</Parameters>
								</OtherProperties>
							</Braces>
							<ClosingSquareBrace Name=']' Id='637840162086532999' IndexInText='96' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<Braces Id='637840162086532289' IndexInText='58' ItemLength='18' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
							<Braces Id='637840162086532668' IndexInText='78' ItemLength='18' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
						</Children>
						<OtherProperties>
							<OpeningBraceInfo Name='[' Id='637840162086532259' IndexInText='57' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=']' Id='637840162086532999' IndexInText='96' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
							<Parameters ObjectId='62' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162086532289' IndexInText='58' ItemLength='18' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162086532668' IndexInText='78' ItemLength='18' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Operand1.Braces>
					<OperatorInfo OperatorType='BinaryOperator' Name='*' Priority='20' Id='637840162086533049' IndexInText='97' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
						<OperatorNameParts>
							<Name Name='*' Id='637840162086533046' IndexInText='97' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OperatorNameParts>
					</OperatorInfo>
					<Operand2.Braces Id='637840162086533180' IndexInText='98' ItemLength='42' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<OpeningSquareBrace Name='[' Id='637840162086533182' IndexInText='98' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Braces Id='637840162086533231' IndexInText='99' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
								<RegularItems>
									<OpeningSquareBrace Name='[' Id='637840162086533233' IndexInText='99' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637840162086533314' IndexInText='100' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='y1_1' Id='637840162086533312' IndexInText='100' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<Comma Name=',' Id='637840162086533340' IndexInText='104' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637840162086533612' IndexInText='106' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='x1_2' Id='637840162086533600' IndexInText='106' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<ClosingSquareBrace Name=']' Id='637840162086533763' IndexInText='110' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Children>
									<Literal Id='637840162086533314' IndexInText='100' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<Literal Id='637840162086533612' IndexInText='106' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								</Children>
								<OtherProperties>
									<OpeningBraceInfo Name='[' Id='637840162086533233' IndexInText='99' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ClosingBraceInfo Name=']' Id='637840162086533763' IndexInText='110' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
									<Parameters ObjectId='75' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162086533314' IndexInText='100' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162086533612' IndexInText='106' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</Parameters>
								</OtherProperties>
							</Braces>
							<Comma Name=',' Id='637840162086533814' IndexInText='111' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Braces Id='637840162086533868' IndexInText='113' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
								<RegularItems>
									<OpeningSquareBrace Name='[' Id='637840162086533871' IndexInText='113' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637840162086533958' IndexInText='114' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='y2_1' Id='637840162086533955' IndexInText='114' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<Comma Name=',' Id='637840162086533997' IndexInText='118' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637840162086534073' IndexInText='120' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='x2_2' Id='637840162086534071' IndexInText='120' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<ClosingSquareBrace Name=']' Id='637840162086534098' IndexInText='124' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Children>
									<Literal Id='637840162086533958' IndexInText='114' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<Literal Id='637840162086534073' IndexInText='120' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								</Children>
								<OtherProperties>
									<OpeningBraceInfo Name='[' Id='637840162086533871' IndexInText='113' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ClosingBraceInfo Name=']' Id='637840162086534098' IndexInText='124' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
									<Parameters ObjectId='85' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162086533958' IndexInText='114' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162086534073' IndexInText='120' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</Parameters>
								</OtherProperties>
							</Braces>
							<Comma Name=',' Id='637840162086534128' IndexInText='125' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Braces Id='637840162086534149' IndexInText='127' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
								<RegularItems>
									<OpeningSquareBrace Name='[' Id='637840162086534151' IndexInText='127' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637840162086534230' IndexInText='128' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='y3_1' Id='637840162086534227' IndexInText='128' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<Comma Name=',' Id='637840162086534286' IndexInText='132' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637840162086535346' IndexInText='134' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='x3_2' Id='637840162086535339' IndexInText='134' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<ClosingSquareBrace Name=']' Id='637840162086535399' IndexInText='138' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Children>
									<Literal Id='637840162086534230' IndexInText='128' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<Literal Id='637840162086535346' IndexInText='134' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								</Children>
								<OtherProperties>
									<OpeningBraceInfo Name='[' Id='637840162086534151' IndexInText='127' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ClosingBraceInfo Name=']' Id='637840162086535399' IndexInText='138' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
									<Parameters ObjectId='95' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162086534230' IndexInText='128' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162086535346' IndexInText='134' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</Parameters>
								</OtherProperties>
							</Braces>
							<ClosingSquareBrace Name=']' Id='637840162086535431' IndexInText='139' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<Braces Id='637840162086533231' IndexInText='99' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
							<Braces Id='637840162086533868' IndexInText='113' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
							<Braces Id='637840162086534149' IndexInText='127' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
						</Children>
						<OtherProperties>
							<OpeningBraceInfo Name='[' Id='637840162086533182' IndexInText='98' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=']' Id='637840162086535431' IndexInText='139' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
							<Parameters ObjectId='97' Count='3' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162086533231' IndexInText='99' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162086533868' IndexInText='113' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162086534149' IndexInText='127' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Operand2.Braces>
				</Operand2.BinaryOperator>
			</BinaryOperator>
			<ExpressionSeparator Name=';' Id='637840162086535525' IndexInText='140' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<Braces Id='637840162086535649' IndexInText='145' ItemLength='35' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
				<RegularItems>
					<Literal Id='637840162086535622' IndexInText='145' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='println' Id='637840162086535619' IndexInText='145' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<OpeningRoundBrace Name='(' Id='637840162086535652' IndexInText='152' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Literal Id='637840162086535748' IndexInText='153' ItemLength='26' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='matrixMultiplicationResult' Id='637840162086535746' IndexInText='153' ItemLength='26' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<ClosingRoundBrace Name=')' Id='637840162086535775' IndexInText='179' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Children>
					<Literal Id='637840162086535748' IndexInText='153' ItemLength='26' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
				</Children>
				<OtherProperties>
					<NamedExpressionItem Id='637840162086535622' IndexInText='145' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					<OpeningBraceInfo Name='(' Id='637840162086535652' IndexInText='152' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ClosingBraceInfo Name=')' Id='637840162086535775' IndexInText='179' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Parameters ObjectId='106' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162086535748' IndexInText='153' ItemLength='26' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					</Parameters>
				</OtherProperties>
			</Braces>
			<ExpressionSeparator Name=';' Id='637840162086535803' IndexInText='180' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<BinaryOperator Name='=>' Priority='1000' Id='637840162086599749' IndexInText='185' ItemLength='237' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
				<Operand1.BinaryOperator Name=':' Priority='0' Id='637840162086599725' IndexInText='185' ItemLength='58' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
					<Operand1.Braces Id='637840162086536404' IndexInText='185' ItemLength='52' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<Prefixes>
							<Braces Id='637840162086535827' IndexInText='185' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
								<RegularItems>
									<OpeningSquareBrace Name='[' Id='637840162086535829' IndexInText='185' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637840162086535908' IndexInText='186' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='NotNull' Id='637840162086535906' IndexInText='186' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<ClosingSquareBrace Name=']' Id='637840162086535933' IndexInText='193' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Children>
									<Literal Id='637840162086535908' IndexInText='186' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								</Children>
								<OtherProperties>
									<OpeningBraceInfo Name='[' Id='637840162086535829' IndexInText='185' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ClosingBraceInfo Name=']' Id='637840162086535933' IndexInText='193' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
									<Parameters ObjectId='116' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162086535908' IndexInText='186' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</Parameters>
								</OtherProperties>
							</Braces>
							<Braces Id='637840162086535957' IndexInText='195' ItemLength='25' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
								<RegularItems>
									<OpeningSquareBrace Name='[' Id='637840162086535978' IndexInText='195' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Braces Id='637840162086536083' IndexInText='196' ItemLength='23' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
										<RegularItems>
											<Literal Id='637840162086536060' IndexInText='196' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
												<NameExpressionItem Name='PublicName' Id='637840162086536057' IndexInText='196' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											</Literal>
											<OpeningRoundBrace Name='(' Id='637840162086536085' IndexInText='206' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<ConstantText Id='637840162086536186' IndexInText='207' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
												<NameExpressionItem Name='"Calculate"' Id='637840162086536183' IndexInText='207' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											</ConstantText>
											<ClosingRoundBrace Name=')' Id='637840162086536216' IndexInText='218' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</RegularItems>
										<Children>
											<ConstantText Id='637840162086536186' IndexInText='207' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
										</Children>
										<OtherProperties>
											<NamedExpressionItem Id='637840162086536060' IndexInText='196' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
											<OpeningBraceInfo Name='(' Id='637840162086536085' IndexInText='206' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<ClosingBraceInfo Name=')' Id='637840162086536216' IndexInText='218' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<Parameters ObjectId='126' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
												<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162086536186' IndexInText='207' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
											</Parameters>
										</OtherProperties>
									</Braces>
									<ClosingSquareBrace Name=']' Id='637840162086536241' IndexInText='219' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Children>
									<Braces Id='637840162086536083' IndexInText='196' ItemLength='23' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
								</Children>
								<OtherProperties>
									<OpeningBraceInfo Name='[' Id='637840162086535978' IndexInText='195' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ClosingBraceInfo Name=']' Id='637840162086536241' IndexInText='219' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
									<Parameters ObjectId='128' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162086536083' IndexInText='196' ItemLength='23' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
									</Parameters>
								</OtherProperties>
							</Braces>
						</Prefixes>
						<AppliedKeywords>
							<Keyword Name='public' Id='637840162086536262' IndexInText='222' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
								<LanguageKeywordInfo ObjectId='130' Id='637783669980406419' Keyword='public' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
							</Keyword>
						</AppliedKeywords>
						<RegularItems>
							<Literal Id='637840162086536366' IndexInText='229' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='F1' Id='637840162086536364' IndexInText='229' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<OpeningRoundBrace Name='(' Id='637840162086536409' IndexInText='231' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Literal Id='637840162086536485' IndexInText='232' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='x' Id='637840162086536482' IndexInText='232' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<Comma Name=',' Id='637840162086536510' IndexInText='233' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Literal Id='637840162086536589' IndexInText='235' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='y' Id='637840162086536587' IndexInText='235' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<ClosingRoundBrace Name=')' Id='637840162086536612' IndexInText='236' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<Literal Id='637840162086536485' IndexInText='232' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							<Literal Id='637840162086536589' IndexInText='235' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
						</Children>
						<OtherProperties>
							<NamedExpressionItem Id='637840162086536366' IndexInText='229' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							<OpeningBraceInfo Name='(' Id='637840162086536409' IndexInText='231' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=')' Id='637840162086536612' IndexInText='236' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Parameters ObjectId='140' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162086536485' IndexInText='232' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162086536589' IndexInText='235' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Operand1.Braces>
					<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637840162086536666' IndexInText='238' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
						<OperatorNameParts>
							<Name Name=':' Id='637840162086536660' IndexInText='238' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OperatorNameParts>
					</OperatorInfo>
					<Operand2.Literal Id='637840162086536849' IndexInText='240' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='int' Id='637840162086536846' IndexInText='240' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Operand2.Literal>
				</Operand1.BinaryOperator>
				<OperatorInfo OperatorType='BinaryOperator' Name='=>' Priority='1000' Id='637840162086536930' IndexInText='244' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
					<OperatorNameParts>
						<Name Name='=>' Id='637840162086536926' IndexInText='244' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</OperatorNameParts>
				</OperatorInfo>
				<Operand2.CodeBlock Id='637840162086537037' IndexInText='249' ItemLength='173' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
					<RegularItems>
						<CodeBlockStartMarker Name='{' Id='637840162086537034' IndexInText='249' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<PrefixUnaryOperator Name='++' Priority='0' Id='637840162086537313' IndexInText='334' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
							<OperatorInfo OperatorType='PrefixUnaryOperator' Name='++' Priority='0' Id='637840162086537099' IndexInText='334' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
								<OperatorNameParts>
									<Name Name='++' Id='637840162086537095' IndexInText='334' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</OperatorNameParts>
							</OperatorInfo>
							<Operand1.Literal Id='637840162086537199' IndexInText='336' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='y' Id='637840162086537197' IndexInText='336' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Operand1.Literal>
						</PrefixUnaryOperator>
						<ExpressionSeparator Name=';' Id='637840162086537335' IndexInText='367' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<PrefixUnaryOperator Name='return' Priority='2147483647' Id='637840162086593946' IndexInText='374' ItemLength='26' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
							<OperatorInfo OperatorType='PrefixUnaryOperator' Name='return' Priority='2147483647' Id='637840162086537382' IndexInText='374' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
								<OperatorNameParts>
									<Name Name='return' Id='637840162086537379' IndexInText='374' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</OperatorNameParts>
							</OperatorInfo>
							<Operand1.BinaryOperator Name='+' Priority='30' Id='637840162086593981' IndexInText='381' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
								<Operand1.BinaryOperator Name='+' Priority='30' Id='637840162086593969' IndexInText='381' ItemLength='15' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
									<Operand1.ConstantNumericValue Id='637840162086593002' IndexInText='381' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
										<NameExpressionItem Name='1.3EXP-2.7' Id='637840162086592966' IndexInText='381' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										<OtherProperties>
											<SucceededNumericTypeDescriptor ObjectId='162' NumberTypeId='1581134225786' Interface='UniversalExpressionParser.NumericTypeDescriptor'>
												<RegularExpressions ObjectId='163' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[System.String]'>
													<System.String value='^(\d+\.\d+|\d+\.|\.\d+|\d+)(EXP|exp|E|e)[+|-]?(\d+\.\d+|\d+\.|\.\d+|\d+)' />
												</RegularExpressions>
											</SucceededNumericTypeDescriptor>
										</OtherProperties>
									</Operand1.ConstantNumericValue>
									<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637840162086593272' IndexInText='391' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
										<OperatorNameParts>
											<Name Name='+' Id='637840162086593262' IndexInText='391' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</OperatorNameParts>
									</OperatorInfo>
									<Operand2.PrefixUnaryOperator Name='++' Priority='0' Id='637840162086593975' IndexInText='393' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
										<OperatorInfo OperatorType='PrefixUnaryOperator' Name='++' Priority='0' Id='637840162086593379' IndexInText='393' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
											<OperatorNameParts>
												<Name Name='++' Id='637840162086593376' IndexInText='393' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											</OperatorNameParts>
										</OperatorInfo>
										<Operand1.Literal Id='637840162086593509' IndexInText='395' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
											<NameExpressionItem Name='x' Id='637840162086593506' IndexInText='395' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</Operand1.Literal>
									</Operand2.PrefixUnaryOperator>
								</Operand1.BinaryOperator>
								<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637840162086593597' IndexInText='396' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
									<OperatorNameParts>
										<Name Name='+' Id='637840162086593594' IndexInText='396' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</OperatorNameParts>
								</OperatorInfo>
								<Operand2.BinaryOperator Name='*' Priority='20' Id='637840162086593987' IndexInText='397' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
									<Operand1.Literal Id='637840162086593726' IndexInText='397' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='y' Id='637840162086593723' IndexInText='397' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Operand1.Literal>
									<OperatorInfo OperatorType='BinaryOperator' Name='*' Priority='20' Id='637840162086593777' IndexInText='398' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
										<OperatorNameParts>
											<Name Name='*' Id='637840162086593774' IndexInText='398' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</OperatorNameParts>
									</OperatorInfo>
									<Operand2.Literal Id='637840162086593902' IndexInText='399' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='z' Id='637840162086593899' IndexInText='399' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Operand2.Literal>
								</Operand2.BinaryOperator>
							</Operand1.BinaryOperator>
						</PrefixUnaryOperator>
						<ExpressionSeparator Name=';' Id='637840162086599600' IndexInText='400' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<CodeBlockEndMarker Name='}' Id='637840162086599689' IndexInText='421' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</RegularItems>
					<Children>
						<PrefixUnaryOperator Name='++' Priority='0' Id='637840162086537313' IndexInText='334' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
						<PrefixUnaryOperator Name='return' Priority='2147483647' Id='637840162086593946' IndexInText='374' ItemLength='26' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
					</Children>
					<OtherProperties>
						<CodeBlockStartMarker Name='{' Id='637840162086537034' IndexInText='249' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<CodeBlockEndMarker Name='}' Id='637840162086599689' IndexInText='421' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</OtherProperties>
				</Operand2.CodeBlock>
			</BinaryOperator>
			<Literal Id='637840162086601020' IndexInText='426' ItemLength='218' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
				<NameExpressionItem Name='Animal' Id='637840162086601012' IndexInText='507' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				<Prefixes>
					<Custom Id='637840162086600437' IndexInText='426' ItemLength='60' Interface='UniversalExpressionParser.ExpressionItems.Custom.IKeywordBasedCustomExpressionItem'>
						<AppliedKeywords>
							<Keyword Name='public' Id='637840162086599790' IndexInText='426' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
								<LanguageKeywordInfo ObjectId='130' Id='637783669980406419' Keyword='public' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
							</Keyword>
							<Keyword Name='abstract' Id='637840162086599802' IndexInText='433' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
								<LanguageKeywordInfo ObjectId='187' Id='637793546145647499' Keyword='abstract' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
							</Keyword>
							<Keyword Name='class' Id='637840162086599808' IndexInText='442' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
								<LanguageKeywordInfo ObjectId='189' Id='637783670388745402' Keyword='class' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
							</Keyword>
						</AppliedKeywords>
						<RegularItems>
							<Keyword Name='::metadata' Id='637840162086599825' IndexInText='448' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
								<LanguageKeywordInfo ObjectId='191' Id='637781063212876967' Keyword='::metadata' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
							</Keyword>
							<CodeBlock Id='637840162086599908' IndexInText='458' ItemLength='28' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
								<RegularItems>
									<CodeBlockStartMarker Name='{' Id='637840162086599905' IndexInText='458' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<BinaryOperator Name=':' Priority='0' Id='637840162086600412' IndexInText='459' ItemLength='26' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
										<Operand1.Literal Id='637840162086600152' IndexInText='459' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
											<NameExpressionItem Name='description' Id='637840162086600149' IndexInText='459' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</Operand1.Literal>
										<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637840162086600210' IndexInText='470' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
											<OperatorNameParts>
												<Name Name=':' Id='637840162086600203' IndexInText='470' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											</OperatorNameParts>
										</OperatorInfo>
										<Operand2.ConstantText Id='637840162086600370' IndexInText='472' ItemLength='13' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
											<NameExpressionItem Name='"Demo prefix"' Id='637840162086600367' IndexInText='472' ItemLength='13' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</Operand2.ConstantText>
									</BinaryOperator>
									<CodeBlockEndMarker Name='}' Id='637840162086600430' IndexInText='485' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Children>
									<BinaryOperator Name=':' Priority='0' Id='637840162086600412' IndexInText='459' ItemLength='26' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
								</Children>
								<OtherProperties>
									<CodeBlockStartMarker Name='{' Id='637840162086599905' IndexInText='458' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<CodeBlockEndMarker Name='}' Id='637840162086600430' IndexInText='485' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</OtherProperties>
							</CodeBlock>
						</RegularItems>
						<OtherProperties>
							<LastKeywordExpressionItem Name='::metadata' Id='637840162086599825' IndexInText='448' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'/>
							<ErrorsPositionDisplayValue value='448' type='System.Int32' />
						</OtherProperties>
					</Custom>
					<Custom Id='637840162086600908' IndexInText='487' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.Custom.IKeywordBasedCustomExpressionItem'>
						<RegularItems>
							<Keyword Name='::types' Id='637840162086600478' IndexInText='487' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
								<LanguageKeywordInfo ObjectId='204' Id='637781062811583137' Keyword='::types' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
							</Keyword>
							<Braces Id='637840162086600524' IndexInText='494' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
								<RegularItems>
									<OpeningSquareBrace Name='[' Id='637840162086600528' IndexInText='494' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637840162086600658' IndexInText='495' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='T1' Id='637840162086600656' IndexInText='495' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<Comma Name=',' Id='637840162086600691' IndexInText='497' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637840162086600770' IndexInText='499' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='T2' Id='637840162086600767' IndexInText='499' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<Comma Name=',' Id='637840162086600794' IndexInText='501' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637840162086600868' IndexInText='503' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='T3' Id='637840162086600865' IndexInText='503' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<ClosingSquareBrace Name=']' Id='637840162086600900' IndexInText='505' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Children>
									<Literal Id='637840162086600658' IndexInText='495' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<Literal Id='637840162086600770' IndexInText='499' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<Literal Id='637840162086600868' IndexInText='503' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								</Children>
								<OtherProperties>
									<OpeningBraceInfo Name='[' Id='637840162086600528' IndexInText='494' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ClosingBraceInfo Name=']' Id='637840162086600900' IndexInText='505' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
									<Parameters ObjectId='216' Count='3' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162086600658' IndexInText='495' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162086600770' IndexInText='499' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162086600868' IndexInText='503' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</Parameters>
								</OtherProperties>
							</Braces>
						</RegularItems>
						<OtherProperties>
							<LastKeywordExpressionItem Name='::types' Id='637840162086600478' IndexInText='487' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'/>
							<ErrorsPositionDisplayValue value='487' type='System.Int32' />
						</OtherProperties>
					</Custom>
				</Prefixes>
				<Postfixes>
					<Custom Id='637840162086601095' IndexInText='518' ItemLength='46' Interface='UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IWhereCustomExpressionItem'>
						<RegularItems>
							<CustomExpressionItemPart Id='637840162086601099' IndexInText='518' ItemLength='16' Interface='UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem'>
								<RegularItems>
									<Keyword Name='where' Id='637840162086601036' IndexInText='518' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
										<LanguageKeywordInfo ObjectId='220' Id='637781062905654923' Keyword='where' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
									</Keyword>
									<Name Name='T1' Id='637840162086601145' IndexInText='524' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Name Name=':' Id='637840162086601150' IndexInText='526' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637840162086601166' IndexInText='528' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='IType1' Id='637840162086601161' IndexInText='528' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
								</RegularItems>
								<OtherProperties>
									<WhereKeyword Name='where' Id='637840162086601036' IndexInText='518' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'/>
									<TypeName Name='T1' Id='637840162086601145' IndexInText='524' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<TypeConstraints ObjectId='225' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162086601166' IndexInText='528' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</TypeConstraints>
								</OtherProperties>
							</CustomExpressionItemPart>
							<CustomExpressionItemPart Id='637840162086601179' IndexInText='535' ItemLength='20' Interface='UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem'>
								<RegularItems>
									<Keyword Name='where' Id='637840162086601176' IndexInText='535' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
										<LanguageKeywordInfo ObjectId='220' Id='637781062905654923' Keyword='where' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
									</Keyword>
									<Name Name='T2' Id='637840162086601189' IndexInText='541' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Name Name=':' Id='637840162086601193' IndexInText='543' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637840162086601209' IndexInText='545' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='T1' Id='637840162086601199' IndexInText='545' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<Comma Name=',' Id='637840162086601215' IndexInText='547' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637840162086601230' IndexInText='549' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='IType2' Id='637840162086601225' IndexInText='549' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
								</RegularItems>
								<OtherProperties>
									<WhereKeyword Name='where' Id='637840162086601176' IndexInText='535' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'/>
									<TypeName Name='T2' Id='637840162086601189' IndexInText='541' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<TypeConstraints ObjectId='235' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162086601209' IndexInText='545' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162086601230' IndexInText='549' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</TypeConstraints>
								</OtherProperties>
							</CustomExpressionItemPart>
							<Name Name='whereend' Id='637840162086601252' IndexInText='556' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<CustomExpressionItemPart Id='637840162086601099' IndexInText='518' ItemLength='16' Interface='UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem'/>
							<CustomExpressionItemPart Id='637840162086601179' IndexInText='535' ItemLength='20' Interface='UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem'/>
						</Children>
						<OtherProperties>
							<WhereEndMarker Name='whereend' Id='637840162086601252' IndexInText='556' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<GenericTypeDataExpressionItems ObjectId='237' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem]'>
								<UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem Id='637840162086601099' IndexInText='518' ItemLength='16'/>
								<UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem Id='637840162086601179' IndexInText='535' ItemLength='20'/>
							</GenericTypeDataExpressionItems>
							<ErrorsPositionDisplayValue value='518' type='System.Int32' />
						</OtherProperties>
					</Custom>
					<Custom Id='637840162086601313' IndexInText='569' ItemLength='25' Interface='UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IWhereCustomExpressionItem'>
						<RegularItems>
							<CustomExpressionItemPart Id='637840162086601316' IndexInText='569' ItemLength='16' Interface='UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem'>
								<RegularItems>
									<Keyword Name='where' Id='637840162086601283' IndexInText='569' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
										<LanguageKeywordInfo ObjectId='220' Id='637781062905654923' Keyword='where' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
									</Keyword>
									<Name Name='T3' Id='637840162086601328' IndexInText='575' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Name Name=':' Id='637840162086601332' IndexInText='577' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637840162086601343' IndexInText='579' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='IType3' Id='637840162086601339' IndexInText='579' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
								</RegularItems>
								<OtherProperties>
									<WhereKeyword Name='where' Id='637840162086601283' IndexInText='569' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'/>
									<TypeName Name='T3' Id='637840162086601328' IndexInText='575' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<TypeConstraints ObjectId='245' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162086601343' IndexInText='579' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</TypeConstraints>
								</OtherProperties>
							</CustomExpressionItemPart>
							<Name Name='whereend' Id='637840162086601359' IndexInText='586' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<CustomExpressionItemPart Id='637840162086601316' IndexInText='569' ItemLength='16' Interface='UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem'/>
						</Children>
						<OtherProperties>
							<WhereEndMarker Name='whereend' Id='637840162086601359' IndexInText='586' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<GenericTypeDataExpressionItems ObjectId='247' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem]'>
								<UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem Id='637840162086601316' IndexInText='569' ItemLength='16'/>
							</GenericTypeDataExpressionItems>
							<ErrorsPositionDisplayValue value='569' type='System.Int32' />
						</OtherProperties>
					</Custom>
					<CodeBlock Id='637840162086601390' IndexInText='596' ItemLength='48' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
						<RegularItems>
							<CodeBlockStartMarker Name='{' Id='637840162086601387' IndexInText='596' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<BinaryOperator Name=':' Priority='0' Id='637840162086601813' IndexInText='607' ItemLength='29' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
								<Operand1.Braces Id='637840162086601526' IndexInText='607' ItemLength='22' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
									<AppliedKeywords>
										<Keyword Name='public' Id='637840162086601408' IndexInText='607' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
											<LanguageKeywordInfo ObjectId='130' Id='637783669980406419' Keyword='public' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
										</Keyword>
										<Keyword Name='abstract' Id='637840162086601413' IndexInText='614' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
											<LanguageKeywordInfo ObjectId='187' Id='637793546145647499' Keyword='abstract' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
										</Keyword>
									</AppliedKeywords>
									<RegularItems>
										<Literal Id='637840162086601495' IndexInText='623' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
											<NameExpressionItem Name='Move' Id='637840162086601493' IndexInText='623' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</Literal>
										<OpeningRoundBrace Name='(' Id='637840162086601530' IndexInText='627' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										<ClosingRoundBrace Name=')' Id='637840162086601558' IndexInText='628' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</RegularItems>
									<OtherProperties>
										<NamedExpressionItem Id='637840162086601495' IndexInText='623' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
										<OpeningBraceInfo Name='(' Id='637840162086601530' IndexInText='627' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										<ClosingBraceInfo Name=')' Id='637840162086601558' IndexInText='628' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										<Parameters ObjectId='258' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'/>
									</OtherProperties>
								</Operand1.Braces>
								<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637840162086601611' IndexInText='630' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
									<OperatorNameParts>
										<Name Name=':' Id='637840162086601606' IndexInText='630' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</OperatorNameParts>
								</OperatorInfo>
								<Operand2.Literal Id='637840162086601780' IndexInText='632' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
									<NameExpressionItem Name='void' Id='637840162086601777' IndexInText='632' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</Operand2.Literal>
							</BinaryOperator>
							<ExpressionSeparator Name=';' Id='637840162086601834' IndexInText='636' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637840162086601859' IndexInText='643' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<BinaryOperator Name=':' Priority='0' Id='637840162086601813' IndexInText='607' ItemLength='29' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
						</Children>
						<OtherProperties>
							<CodeBlockStartMarker Name='{' Id='637840162086601387' IndexInText='596' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637840162086601859' IndexInText='643' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OtherProperties>
					</CodeBlock>
				</Postfixes>
			</Literal>
			<BinaryOperator Name=':' Priority='0' Id='637840162086603029' IndexInText='648' ItemLength='88' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
				<Operand1.Literal Id='637840162086601964' IndexInText='648' ItemLength='16' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
					<NameExpressionItem Name='Dog' Id='637840162086601962' IndexInText='661' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<AppliedKeywords>
						<Keyword Name='public' Id='637840162086601883' IndexInText='648' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
							<LanguageKeywordInfo ObjectId='130' Id='637783669980406419' Keyword='public' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
						</Keyword>
						<Keyword Name='class' Id='637840162086601891' IndexInText='655' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
							<LanguageKeywordInfo ObjectId='189' Id='637783670388745402' Keyword='class' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
						</Keyword>
					</AppliedKeywords>
				</Operand1.Literal>
				<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637840162086602012' IndexInText='665' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
					<OperatorNameParts>
						<Name Name=':' Id='637840162086602009' IndexInText='665' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</OperatorNameParts>
				</OperatorInfo>
				<Operand2.Braces Id='637840162086602139' IndexInText='667' ItemLength='69' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
					<RegularItems>
						<OpeningRoundBrace Name='(' Id='637840162086602141' IndexInText='667' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<Literal Id='637840162086602226' IndexInText='668' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='Anymal' Id='637840162086602223' IndexInText='668' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Literal>
						<ClosingRoundBrace Name=')' Id='637840162086602253' IndexInText='674' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</RegularItems>
					<Postfixes>
						<CodeBlock Id='637840162086602280' IndexInText='677' ItemLength='59' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
							<RegularItems>
								<CodeBlockStartMarker Name='{' Id='637840162086602278' IndexInText='677' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<BinaryOperator Name='=>' Priority='1000' Id='637840162086602968' IndexInText='684' ItemLength='48' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
									<Operand1.BinaryOperator Name=':' Priority='0' Id='637840162086602955' IndexInText='684' ItemLength='29' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
										<Operand1.Braces Id='637840162086602408' IndexInText='684' ItemLength='22' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
											<AppliedKeywords>
												<Keyword Name='public' Id='637840162086602296' IndexInText='684' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
													<LanguageKeywordInfo ObjectId='130' Id='637783669980406419' Keyword='public' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
												</Keyword>
												<Keyword Name='override' Id='637840162086602303' IndexInText='691' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
													<LanguageKeywordInfo ObjectId='284' Id='637793548069818537' Keyword='override' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
												</Keyword>
											</AppliedKeywords>
											<RegularItems>
												<Literal Id='637840162086602382' IndexInText='700' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
													<NameExpressionItem Name='Move' Id='637840162086602380' IndexInText='700' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												</Literal>
												<OpeningRoundBrace Name='(' Id='637840162086602411' IndexInText='704' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												<ClosingRoundBrace Name=')' Id='637840162086602440' IndexInText='705' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											</RegularItems>
											<OtherProperties>
												<NamedExpressionItem Id='637840162086602382' IndexInText='700' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
												<OpeningBraceInfo Name='(' Id='637840162086602411' IndexInText='704' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												<ClosingBraceInfo Name=')' Id='637840162086602440' IndexInText='705' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												<Parameters ObjectId='289' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'/>
											</OtherProperties>
										</Operand1.Braces>
										<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637840162086602483' IndexInText='707' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
											<OperatorNameParts>
												<Name Name=':' Id='637840162086602480' IndexInText='707' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											</OperatorNameParts>
										</OperatorInfo>
										<Operand2.Literal Id='637840162086602614' IndexInText='709' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
											<NameExpressionItem Name='void' Id='637840162086602611' IndexInText='709' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</Operand2.Literal>
									</Operand1.BinaryOperator>
									<OperatorInfo OperatorType='BinaryOperator' Name='=>' Priority='1000' Id='637840162086602685' IndexInText='714' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
										<OperatorNameParts>
											<Name Name='=>' Id='637840162086602681' IndexInText='714' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</OperatorNameParts>
									</OperatorInfo>
									<Operand2.Braces Id='637840162086602815' IndexInText='717' ItemLength='15' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
										<RegularItems>
											<Literal Id='637840162086602789' IndexInText='717' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
												<NameExpressionItem Name='println' Id='637840162086602786' IndexInText='717' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											</Literal>
											<OpeningRoundBrace Name='(' Id='637840162086602817' IndexInText='724' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<ConstantText Id='637840162086602900' IndexInText='725' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
												<NameExpressionItem Name='"Jump"' Id='637840162086602898' IndexInText='725' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											</ConstantText>
											<ClosingRoundBrace Name=')' Id='637840162086602928' IndexInText='731' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</RegularItems>
										<Children>
											<ConstantText Id='637840162086602900' IndexInText='725' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
										</Children>
										<OtherProperties>
											<NamedExpressionItem Id='637840162086602789' IndexInText='717' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
											<OpeningBraceInfo Name='(' Id='637840162086602817' IndexInText='724' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<ClosingBraceInfo Name=')' Id='637840162086602928' IndexInText='731' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<Parameters ObjectId='303' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
												<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162086602900' IndexInText='725' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
											</Parameters>
										</OtherProperties>
									</Operand2.Braces>
								</BinaryOperator>
								<ExpressionSeparator Name=';' Id='637840162086602996' IndexInText='732' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<CodeBlockEndMarker Name='}' Id='637840162086603022' IndexInText='735' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</RegularItems>
							<Children>
								<BinaryOperator Name='=>' Priority='1000' Id='637840162086602968' IndexInText='684' ItemLength='48' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
							</Children>
							<OtherProperties>
								<CodeBlockStartMarker Name='{' Id='637840162086602278' IndexInText='677' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<CodeBlockEndMarker Name='}' Id='637840162086603022' IndexInText='735' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OtherProperties>
						</CodeBlock>
					</Postfixes>
					<Children>
						<Literal Id='637840162086602226' IndexInText='668' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					</Children>
					<OtherProperties>
						<OpeningBraceInfo Name='(' Id='637840162086602141' IndexInText='667' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<ClosingBraceInfo Name=')' Id='637840162086602253' IndexInText='674' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
						<Parameters ObjectId='306' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
							<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162086602226' IndexInText='668' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
						</Parameters>
					</OtherProperties>
				</Operand2.Braces>
			</BinaryOperator>
		</RegularItems>
		<Children>
			<BinaryOperator Name='=' Priority='2000' Id='637840162086531889' IndexInText='0' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
			<BinaryOperator Name='=' Priority='2000' Id='637840162086535466' IndexInText='24' ItemLength='116' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
			<Braces Id='637840162086535649' IndexInText='145' ItemLength='35' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
			<BinaryOperator Name='=>' Priority='1000' Id='637840162086599749' IndexInText='185' ItemLength='237' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
			<Literal Id='637840162086601020' IndexInText='426' ItemLength='218' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
			<BinaryOperator Name=':' Priority='0' Id='637840162086603029' IndexInText='648' ItemLength='88' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
		</Children>
	</ExpressionItemSeries>
	<ParseErrorData ObjectId='307' HasCriticalErrors='False' Interface='UniversalExpressionParser.IParseErrorData'>
		<AllCodeItemErrors ObjectId='308' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICodeItemParseErrorData]'/>
	</ParseErrorData>
	<SortedCommentedOutCodeInfos ObjectId='309' Count='3' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICommentedOutCodeInfo]'>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='310' IsLineComment='False' IndexInText='262' ItemLength='66'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='311' IsLineComment='False' IndexInText='338' ItemLength='29'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='312' IsLineComment='True' IndexInText='402' ItemLength='17'/>
	</SortedCommentedOutCodeInfos>
	<MainExpressionItem Id='637840162086530587' IndexInText='0' ItemLength='736' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'/>
</UniversalExpressionParser.ExpressionItems.IRootExpressionItemSeries>
```
</details>

- The format of valid expressions is defined by properties and methods in interface **UniversalExpressionParser.IExpressionLanguageProvider**. An instance of this interface is passed as a parameter to method **ParseExpression(...)** in ****UniversalExpressionParser.IExpressionParser****, as demonstrated in example above. Most of the properties and methods of this interface are examples in sections below.

- The default abstract implementation of this interface in this package is **UniversalExpressionParser.ExpressionLanguageProviderBase**.

- The test project **UniversalExpressionParser.Tests** in git repository has number of tests for testing successful parsing, as well as tests for testing expressions that result in parser reporting errors (see section **Error Reporting** below). These tests generate random expressions as well as generate randomly configured instances of **UniversalExpressionParser.IExpressionLanguageProvider** to validate parsing of thousands of all possible languages and expressions (see the test classes **UniversalExpressionParser.Tests.SuccessfulParseTests.ExpressionParserSuccessfulTests** and **UniversalExpressionParser.Tests.ExpressionParseErrorTests.ExpressionParseErrorTests**).

- The demo expressions, and tests used to parse the demo expressions in this documentation are in folder **Demos** in test projects **UniversalExpressionParser.Tests**. The document uses implementations of **UniversalExpressionParser.IExpressionLanguageProvider** in project **UniversalExpressionParser.DemoExpressionLanguageProviders** in **git** repository.

- The parsed expressions in this document (i.e., instances of **UniversalExpressionParser.ExpressionItems.IRootExpressionItem**) are visualized into xml texts, that have some important properties of the parsed expression, however to make the files shorter, the visualized xml files do not include all the property values.

# Literals

- Literals are names without spaces that can be used alone (say in operators) or can precede opening square or round braces (e.g., **f1** in **f1(x)**, or **m1** in **m1[1, 2]**),
or code blocks (e.g., **Dog** in expression **public class Dog {}**).

- Literals are parsed into expression items of type **UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem** objects.

- If literal precedes round or square braces, it will be parsed into property **INamedComplexExpressionItem NamedExpressionItem { get; }** of **UniversalExpressionParser.ExpressionItems.IBracesExpressionItem**.

- If literal precedes a code block staring marker (e.g., **{** in this example), then the code block is added to the list **UniversalExpressionParser.ExpressionItems.IComplexExpressionItem.Postfixes** in expression item for the literal (see section **Postfixes** for more details on postfixes).

- Literals are texts that cannot have space and can only contain characters validated by method **UniversalExpressionParser.IsValidLiteralCharacter(char character, int positionInLiteral, ITextSymbolsParserState textSymbolsParserState)** in **IExpressionLanguageProvider.UniversalExpressionParser**. In other words, a literal can contain any character (including numeric or operator characters, a dot '.', '_', etc.), that is considered a valid literal character by method **IsValidLiteralCharacter**.

- Examples of literals:

```csharp
// In example below _x, f$, x1, x2, m1, and x3, Dog, Color, string, println, and Dog.Color are literals.
var _x = f$(x1, x2) + m1[1, x3];

public class Dog
{
    public Color : string => "brown";
}

// println is a literal and it is part of "println(Dog.Color)" braces expression item. In this example, 
// println will be parsed to an expression item UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem
// and will be the value of property NamedExpressionItem in UniversalExpressionParser.ExpressionItems.IBracesExpressionItem
println(Dog.Color);

```

<details> <summary>Click to expand the parsed expression</summary>

```XML
<?xml version="1.0" encoding="utf-8"?>
<UniversalExpressionParser.ExpressionItems.IRootExpressionItemSeries ObjectId='0' PositionInTextOnCompletion='578' IndexInText='106' ItemLength='470'>
	<ExpressionItemSeries Id='637840162081408931' IndexInText='106' ItemLength='470' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'>
		<RegularItems>
			<BinaryOperator Name='=' Priority='2000' Id='637840162081492776' IndexInText='106' ItemLength='31' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
				<Operand1.Literal Id='637840162081409332' IndexInText='106' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
					<NameExpressionItem Name='_x' Id='637840162081409327' IndexInText='110' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<AppliedKeywords>
						<Keyword Name='var' Id='637840162081409188' IndexInText='106' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
							<LanguageKeywordInfo ObjectId='6' Id='637781064051574641' Keyword='var' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
						</Keyword>
					</AppliedKeywords>
				</Operand1.Literal>
				<OperatorInfo OperatorType='BinaryOperator' Name='=' Priority='2000' Id='637840162081409426' IndexInText='113' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
					<OperatorNameParts>
						<Name Name='=' Id='637840162081409413' IndexInText='113' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</OperatorNameParts>
				</OperatorInfo>
				<Operand2.BinaryOperator Name='+' Priority='30' Id='637840162081492799' IndexInText='115' ItemLength='22' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
					<Operand1.Braces Id='637840162081409614' IndexInText='115' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<Literal Id='637840162081409566' IndexInText='115' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='f$' Id='637840162081409562' IndexInText='115' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<OpeningRoundBrace Name='(' Id='637840162081409618' IndexInText='117' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Literal Id='637840162081409710' IndexInText='118' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='x1' Id='637840162081409706' IndexInText='118' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<Comma Name=',' Id='637840162081409745' IndexInText='120' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Literal Id='637840162081409825' IndexInText='122' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='x2' Id='637840162081409818' IndexInText='122' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<ClosingRoundBrace Name=')' Id='637840162081409853' IndexInText='124' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<Literal Id='637840162081409710' IndexInText='118' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							<Literal Id='637840162081409825' IndexInText='122' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
						</Children>
						<OtherProperties>
							<NamedExpressionItem Id='637840162081409566' IndexInText='115' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							<OpeningBraceInfo Name='(' Id='637840162081409618' IndexInText='117' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=')' Id='637840162081409853' IndexInText='124' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Parameters ObjectId='20' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162081409710' IndexInText='118' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162081409825' IndexInText='122' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Operand1.Braces>
					<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637840162081409911' IndexInText='126' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
						<OperatorNameParts>
							<Name Name='+' Id='637840162081409906' IndexInText='126' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OperatorNameParts>
					</OperatorInfo>
					<Operand2.Braces Id='637840162081410075' IndexInText='128' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<Literal Id='637840162081410039' IndexInText='128' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='m1' Id='637840162081410035' IndexInText='128' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<OpeningSquareBrace Name='[' Id='637840162081410080' IndexInText='130' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ConstantNumericValue Id='637840162081492383' IndexInText='131' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
								<NameExpressionItem Name='1' Id='637840162081492341' IndexInText='131' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<OtherProperties>
									<SucceededNumericTypeDescriptor ObjectId='29' NumberTypeId='1581134183960' Interface='UniversalExpressionParser.NumericTypeDescriptor'>
										<RegularExpressions ObjectId='30' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[System.String]'>
											<System.String value='^\d+' />
										</RegularExpressions>
									</SucceededNumericTypeDescriptor>
								</OtherProperties>
							</ConstantNumericValue>
							<Comma Name=',' Id='637840162081492546' IndexInText='132' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Literal Id='637840162081492684' IndexInText='134' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='x3' Id='637840162081492679' IndexInText='134' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<ClosingSquareBrace Name=']' Id='637840162081492723' IndexInText='136' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<ConstantNumericValue Id='637840162081492383' IndexInText='131' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'/>
							<Literal Id='637840162081492684' IndexInText='134' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
						</Children>
						<OtherProperties>
							<NamedExpressionItem Id='637840162081410039' IndexInText='128' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							<OpeningBraceInfo Name='[' Id='637840162081410080' IndexInText='130' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=']' Id='637840162081492723' IndexInText='136' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Parameters ObjectId='35' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162081492383' IndexInText='131' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'/>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162081492684' IndexInText='134' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Operand2.Braces>
				</Operand2.BinaryOperator>
			</BinaryOperator>
			<ExpressionSeparator Name=';' Id='637840162081492854' IndexInText='137' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<Literal Id='637840162081492986' IndexInText='142' ItemLength='61' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
				<NameExpressionItem Name='Dog' Id='637840162081492981' IndexInText='155' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				<AppliedKeywords>
					<Keyword Name='public' Id='637840162081492883' IndexInText='142' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
						<LanguageKeywordInfo ObjectId='40' Id='637783669980406419' Keyword='public' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
					</Keyword>
					<Keyword Name='class' Id='637840162081492898' IndexInText='149' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
						<LanguageKeywordInfo ObjectId='42' Id='637783670388745402' Keyword='class' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
					</Keyword>
				</AppliedKeywords>
				<Postfixes>
					<CodeBlock Id='637840162081493021' IndexInText='160' ItemLength='43' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
						<RegularItems>
							<CodeBlockStartMarker Name='{' Id='637840162081493017' IndexInText='160' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<BinaryOperator Name='=>' Priority='1000' Id='637840162081493639' IndexInText='167' ItemLength='32' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
								<Operand1.BinaryOperator Name=':' Priority='0' Id='637840162081493629' IndexInText='167' ItemLength='21' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
									<Operand1.Literal Id='637840162081493133' IndexInText='167' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='Color' Id='637840162081493129' IndexInText='174' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										<AppliedKeywords>
											<Keyword Name='public' Id='637840162081493049' IndexInText='167' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
												<LanguageKeywordInfo ObjectId='40' Id='637783669980406419' Keyword='public' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
											</Keyword>
										</AppliedKeywords>
									</Operand1.Literal>
									<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637840162081493188' IndexInText='180' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
										<OperatorNameParts>
											<Name Name=':' Id='637840162081493176' IndexInText='180' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</OperatorNameParts>
									</OperatorInfo>
									<Operand2.Literal Id='637840162081493379' IndexInText='182' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='string' Id='637840162081493374' IndexInText='182' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Operand2.Literal>
								</Operand1.BinaryOperator>
								<OperatorInfo OperatorType='BinaryOperator' Name='=>' Priority='1000' Id='637840162081493465' IndexInText='189' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
									<OperatorNameParts>
										<Name Name='=>' Id='637840162081493459' IndexInText='189' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</OperatorNameParts>
								</OperatorInfo>
								<Operand2.ConstantText Id='637840162081493589' IndexInText='192' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
									<NameExpressionItem Name='"brown"' Id='637840162081493585' IndexInText='192' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</Operand2.ConstantText>
							</BinaryOperator>
							<ExpressionSeparator Name=';' Id='637840162081493660' IndexInText='199' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637840162081493688' IndexInText='202' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<BinaryOperator Name='=>' Priority='1000' Id='637840162081493639' IndexInText='167' ItemLength='32' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
						</Children>
						<OtherProperties>
							<CodeBlockStartMarker Name='{' Id='637840162081493017' IndexInText='160' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637840162081493688' IndexInText='202' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OtherProperties>
					</CodeBlock>
				</Postfixes>
			</Literal>
			<Braces Id='637840162081493835' IndexInText='557' ItemLength='18' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
				<RegularItems>
					<Literal Id='637840162081493806' IndexInText='557' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='println' Id='637840162081493801' IndexInText='557' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<OpeningRoundBrace Name='(' Id='637840162081493839' IndexInText='564' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Literal Id='637840162081493931' IndexInText='565' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='Dog.Color' Id='637840162081493927' IndexInText='565' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<ClosingRoundBrace Name=')' Id='637840162081493960' IndexInText='574' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Children>
					<Literal Id='637840162081493931' IndexInText='565' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
				</Children>
				<OtherProperties>
					<NamedExpressionItem Id='637840162081493806' IndexInText='557' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					<OpeningBraceInfo Name='(' Id='637840162081493839' IndexInText='564' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ClosingBraceInfo Name=')' Id='637840162081493960' IndexInText='574' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Parameters ObjectId='67' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162081493931' IndexInText='565' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					</Parameters>
				</OtherProperties>
			</Braces>
			<ExpressionSeparator Name=';' Id='637840162081493987' IndexInText='575' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
		</RegularItems>
		<Children>
			<BinaryOperator Name='=' Priority='2000' Id='637840162081492776' IndexInText='106' ItemLength='31' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
			<Literal Id='637840162081492986' IndexInText='142' ItemLength='61' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
			<Braces Id='637840162081493835' IndexInText='557' ItemLength='18' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
		</Children>
	</ExpressionItemSeries>
	<ParseErrorData ObjectId='69' HasCriticalErrors='False' Interface='UniversalExpressionParser.IParseErrorData'>
		<AllCodeItemErrors ObjectId='70' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICodeItemParseErrorData]'/>
	</ParseErrorData>
	<SortedCommentedOutCodeInfos ObjectId='71' Count='4' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICommentedOutCodeInfo]'>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='72' IsLineComment='True' IndexInText='0' ItemLength='104'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='73' IsLineComment='True' IndexInText='207' ItemLength='104'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='74' IsLineComment='True' IndexInText='313' ItemLength='117'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='75' IsLineComment='True' IndexInText='432' ItemLength='123'/>
	</SortedCommentedOutCodeInfos>
	<MainExpressionItem Id='637840162081408931' IndexInText='106' ItemLength='470' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'/>
</UniversalExpressionParser.ExpressionItems.IRootExpressionItemSeries>
```
</details>

# Functions and Braces

- Braces are a pair of round or square braces ((e.g., **(x)**, **(x) {}**, **[i,j]**, **[i,j]{}**)). Braces are parsed to expression items of type **UniversalExpressionParser.ExpressionItems.IBracesExpressionItem** with value of property **INamedComplexExpressionItem NamedExpressionItem { get; }** equal to null.
- Functions are round or square braces preceded with a literal (e.g., **F1(x)**, **F1(x) {}**, **m1[i,j]**, **m1[i,j]{}**). Functions are  parsed to expression items of type **UniversalExpressionParser.ExpressionItems.IBracesExpressionItem** with value of property **INamedComplexExpressionItem NamedExpressionItem { get; }** equal to a literal that precedes the braces.

- Examples of braces:

```csharp
// The statements below do not make much sense.
// They just demonstrate different ways the square and round braces can be used
// in expressions.
var x = ((x1, x2, x3), [x4, x5+1, x6], y);
x += (x2, x4) + 2*[x3, x4];
```

<details> <summary>Click to expand the parsed expression</summary>

```XML
<?xml version="1.0" encoding="utf-8"?>
<UniversalExpressionParser.ExpressionItems.IRootExpressionItemSeries ObjectId='0' PositionInTextOnCompletion='221' IndexInText='150' ItemLength='71'>
	<ExpressionItemSeries Id='637840162080776050' IndexInText='150' ItemLength='71' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'>
		<RegularItems>
			<BinaryOperator Name='=' Priority='2000' Id='637840162080865390' IndexInText='150' ItemLength='41' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
				<Operand1.Literal Id='637840162080776459' IndexInText='150' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
					<NameExpressionItem Name='x' Id='637840162080776455' IndexInText='154' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<AppliedKeywords>
						<Keyword Name='var' Id='637840162080776318' IndexInText='150' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
							<LanguageKeywordInfo ObjectId='6' Id='637781064051574641' Keyword='var' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
						</Keyword>
					</AppliedKeywords>
				</Operand1.Literal>
				<OperatorInfo OperatorType='BinaryOperator' Name='=' Priority='2000' Id='637840162080776565' IndexInText='156' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
					<OperatorNameParts>
						<Name Name='=' Id='637840162080776548' IndexInText='156' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</OperatorNameParts>
				</OperatorInfo>
				<Operand2.Braces Id='637840162080776699' IndexInText='158' ItemLength='33' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
					<RegularItems>
						<OpeningRoundBrace Name='(' Id='637840162080776703' IndexInText='158' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<Braces Id='637840162080776751' IndexInText='159' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
							<RegularItems>
								<OpeningRoundBrace Name='(' Id='637840162080776753' IndexInText='159' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<Literal Id='637840162080776837' IndexInText='160' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
									<NameExpressionItem Name='x1' Id='637840162080776833' IndexInText='160' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</Literal>
								<Comma Name=',' Id='637840162080776875' IndexInText='162' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<Literal Id='637840162080776957' IndexInText='164' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
									<NameExpressionItem Name='x2' Id='637840162080776948' IndexInText='164' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</Literal>
								<Comma Name=',' Id='637840162080776983' IndexInText='166' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<Literal Id='637840162080777060' IndexInText='168' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
									<NameExpressionItem Name='x3' Id='637840162080777056' IndexInText='168' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</Literal>
								<ClosingRoundBrace Name=')' Id='637840162080777094' IndexInText='170' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</RegularItems>
							<Children>
								<Literal Id='637840162080776837' IndexInText='160' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								<Literal Id='637840162080776957' IndexInText='164' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								<Literal Id='637840162080777060' IndexInText='168' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							</Children>
							<OtherProperties>
								<OpeningBraceInfo Name='(' Id='637840162080776753' IndexInText='159' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<ClosingBraceInfo Name=')' Id='637840162080777094' IndexInText='170' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
								<Parameters ObjectId='22' Count='3' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
									<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162080776837' IndexInText='160' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162080776957' IndexInText='164' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162080777060' IndexInText='168' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								</Parameters>
							</OtherProperties>
						</Braces>
						<Comma Name=',' Id='637840162080777126' IndexInText='171' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<Braces Id='637840162080777150' IndexInText='173' ItemLength='14' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
							<RegularItems>
								<OpeningSquareBrace Name='[' Id='637840162080777153' IndexInText='173' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<Literal Id='637840162080777232' IndexInText='174' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
									<NameExpressionItem Name='x4' Id='637840162080777228' IndexInText='174' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</Literal>
								<Comma Name=',' Id='637840162080777258' IndexInText='176' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<BinaryOperator Name='+' Priority='30' Id='637840162080864952' IndexInText='178' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
									<Operand1.Literal Id='637840162080777338' IndexInText='178' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='x5' Id='637840162080777334' IndexInText='178' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Operand1.Literal>
									<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637840162080777389' IndexInText='180' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
										<OperatorNameParts>
											<Name Name='+' Id='637840162080777384' IndexInText='180' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</OperatorNameParts>
									</OperatorInfo>
									<Operand2.ConstantNumericValue Id='637840162080864783' IndexInText='181' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
										<NameExpressionItem Name='1' Id='637840162080864738' IndexInText='181' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										<OtherProperties>
											<SucceededNumericTypeDescriptor ObjectId='36' NumberTypeId='1581134183960' Interface='UniversalExpressionParser.NumericTypeDescriptor'>
												<RegularExpressions ObjectId='37' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[System.String]'>
													<System.String value='^\d+' />
												</RegularExpressions>
											</SucceededNumericTypeDescriptor>
										</OtherProperties>
									</Operand2.ConstantNumericValue>
								</BinaryOperator>
								<Comma Name=',' Id='637840162080865018' IndexInText='182' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<Literal Id='637840162080865169' IndexInText='184' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
									<NameExpressionItem Name='x6' Id='637840162080865164' IndexInText='184' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</Literal>
								<ClosingSquareBrace Name=']' Id='637840162080865206' IndexInText='186' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</RegularItems>
							<Children>
								<Literal Id='637840162080777232' IndexInText='174' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								<BinaryOperator Name='+' Priority='30' Id='637840162080864952' IndexInText='178' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
								<Literal Id='637840162080865169' IndexInText='184' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							</Children>
							<OtherProperties>
								<OpeningBraceInfo Name='[' Id='637840162080777153' IndexInText='173' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<ClosingBraceInfo Name=']' Id='637840162080865206' IndexInText='186' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
								<Parameters ObjectId='42' Count='3' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
									<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162080777232' IndexInText='174' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name='+' Priority='30' Id='637840162080864952' IndexInText='178' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
									<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162080865169' IndexInText='184' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								</Parameters>
							</OtherProperties>
						</Braces>
						<Comma Name=',' Id='637840162080865243' IndexInText='187' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<Literal Id='637840162080865331' IndexInText='189' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='y' Id='637840162080865327' IndexInText='189' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Literal>
						<ClosingRoundBrace Name=')' Id='637840162080865358' IndexInText='190' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</RegularItems>
					<Children>
						<Braces Id='637840162080776751' IndexInText='159' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
						<Braces Id='637840162080777150' IndexInText='173' ItemLength='14' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
						<Literal Id='637840162080865331' IndexInText='189' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					</Children>
					<OtherProperties>
						<OpeningBraceInfo Name='(' Id='637840162080776703' IndexInText='158' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<ClosingBraceInfo Name=')' Id='637840162080865358' IndexInText='190' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
						<Parameters ObjectId='47' Count='3' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
							<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162080776751' IndexInText='159' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
							<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162080777150' IndexInText='173' ItemLength='14' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
							<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162080865331' IndexInText='189' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
						</Parameters>
					</OtherProperties>
				</Operand2.Braces>
			</BinaryOperator>
			<ExpressionSeparator Name=';' Id='637840162080865413' IndexInText='191' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<BinaryOperator Name='+=' Priority='2000' Id='637840162080948988' IndexInText='194' ItemLength='26' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
				<Operand1.Literal Id='637840162080865496' IndexInText='194' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
					<NameExpressionItem Name='x' Id='637840162080865492' IndexInText='194' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</Operand1.Literal>
				<OperatorInfo OperatorType='BinaryOperator' Name='+=' Priority='2000' Id='637840162080865610' IndexInText='196' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
					<OperatorNameParts>
						<Name Name='+=' Id='637840162080865605' IndexInText='196' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</OperatorNameParts>
				</OperatorInfo>
				<Operand2.BinaryOperator Name='+' Priority='30' Id='637840162080949008' IndexInText='199' ItemLength='21' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
					<Operand1.Braces Id='637840162080865722' IndexInText='199' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<OpeningRoundBrace Name='(' Id='637840162080865727' IndexInText='199' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Literal Id='637840162080865820' IndexInText='200' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='x2' Id='637840162080865815' IndexInText='200' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<Comma Name=',' Id='637840162080865850' IndexInText='202' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Literal Id='637840162080865932' IndexInText='204' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='x4' Id='637840162080865923' IndexInText='204' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<ClosingRoundBrace Name=')' Id='637840162080865986' IndexInText='206' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<Literal Id='637840162080865820' IndexInText='200' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							<Literal Id='637840162080865932' IndexInText='204' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
						</Children>
						<OtherProperties>
							<OpeningBraceInfo Name='(' Id='637840162080865727' IndexInText='199' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=')' Id='637840162080865986' IndexInText='206' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
							<Parameters ObjectId='63' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162080865820' IndexInText='200' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162080865932' IndexInText='204' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Operand1.Braces>
					<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637840162080866042' IndexInText='208' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
						<OperatorNameParts>
							<Name Name='+' Id='637840162080866036' IndexInText='208' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OperatorNameParts>
					</OperatorInfo>
					<Operand2.BinaryOperator Name='*' Priority='20' Id='637840162080949071' IndexInText='210' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.ConstantNumericValue Id='637840162080948236' IndexInText='210' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
							<NameExpressionItem Name='2' Id='637840162080948182' IndexInText='210' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<OtherProperties>
								<SucceededNumericTypeDescriptor ObjectId='36' NumberTypeId='1581134183960' Interface='UniversalExpressionParser.NumericTypeDescriptor'/>
							</OtherProperties>
						</Operand1.ConstantNumericValue>
						<OperatorInfo OperatorType='BinaryOperator' Name='*' Priority='20' Id='637840162080948495' IndexInText='211' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<Name Name='*' Id='637840162080948484' IndexInText='211' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OperatorNameParts>
						</OperatorInfo>
						<Operand2.Braces Id='637840162080948649' IndexInText='212' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
							<RegularItems>
								<OpeningSquareBrace Name='[' Id='637840162080948654' IndexInText='212' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<Literal Id='637840162080948771' IndexInText='213' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
									<NameExpressionItem Name='x3' Id='637840162080948766' IndexInText='213' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</Literal>
								<Comma Name=',' Id='637840162080948822' IndexInText='215' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<Literal Id='637840162080948909' IndexInText='217' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
									<NameExpressionItem Name='x4' Id='637840162080948905' IndexInText='217' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</Literal>
								<ClosingSquareBrace Name=']' Id='637840162080948940' IndexInText='219' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</RegularItems>
							<Children>
								<Literal Id='637840162080948771' IndexInText='213' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								<Literal Id='637840162080948909' IndexInText='217' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							</Children>
							<OtherProperties>
								<OpeningBraceInfo Name='[' Id='637840162080948654' IndexInText='212' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<ClosingBraceInfo Name=']' Id='637840162080948940' IndexInText='219' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
								<Parameters ObjectId='79' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
									<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162080948771' IndexInText='213' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162080948909' IndexInText='217' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								</Parameters>
							</OtherProperties>
						</Operand2.Braces>
					</Operand2.BinaryOperator>
				</Operand2.BinaryOperator>
			</BinaryOperator>
			<ExpressionSeparator Name=';' Id='637840162080949125' IndexInText='220' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
		</RegularItems>
		<Children>
			<BinaryOperator Name='=' Priority='2000' Id='637840162080865390' IndexInText='150' ItemLength='41' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
			<BinaryOperator Name='+=' Priority='2000' Id='637840162080948988' IndexInText='194' ItemLength='26' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
		</Children>
	</ExpressionItemSeries>
	<ParseErrorData ObjectId='81' HasCriticalErrors='False' Interface='UniversalExpressionParser.IParseErrorData'>
		<AllCodeItemErrors ObjectId='82' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICodeItemParseErrorData]'/>
	</ParseErrorData>
	<SortedCommentedOutCodeInfos ObjectId='83' Count='3' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICommentedOutCodeInfo]'>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='84' IsLineComment='True' IndexInText='0' ItemLength='47'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='85' IsLineComment='True' IndexInText='49' ItemLength='79'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='86' IsLineComment='True' IndexInText='130' ItemLength='18'/>
	</SortedCommentedOutCodeInfos>
	<MainExpressionItem Id='637840162080776050' IndexInText='150' ItemLength='71' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'/>
</UniversalExpressionParser.ExpressionItems.IRootExpressionItemSeries>
```
</details>

- Examples of functions:

```csharp
// The statements below do not make much sense.
// They just demonstrate different ways the square and round braces can be used
// in expressions.
var x = x1+f1(x2, x3+x4*5+x[1])+
         matrix1[[y1+3, f1(x4)], x3, f2(x3, m2[x+5])];

f1(x, y) => x+y;

f2[x, y] 
{
   // Normally matrixes do not have bodies like functions doo. This is just to demo that 
   // the parser allows this.
}
```

<details> <summary>Click to expand the parsed expression</summary>

```XML
<?xml version="1.0" encoding="utf-8"?>
<UniversalExpressionParser.ExpressionItems.IRootExpressionItemSeries ObjectId='0' PositionInTextOnCompletion='399' IndexInText='150' ItemLength='246'>
	<ExpressionItemSeries Id='637840162080283699' IndexInText='150' ItemLength='249' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'>
		<RegularItems>
			<BinaryOperator Name='=' Priority='2000' Id='637840162080619595' IndexInText='150' ItemLength='87' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
				<Operand1.Literal Id='637840162080284137' IndexInText='150' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
					<NameExpressionItem Name='x' Id='637840162080284131' IndexInText='154' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<AppliedKeywords>
						<Keyword Name='var' Id='637840162080283988' IndexInText='150' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
							<LanguageKeywordInfo ObjectId='6' Id='637781064051574641' Keyword='var' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
						</Keyword>
					</AppliedKeywords>
				</Operand1.Literal>
				<OperatorInfo OperatorType='BinaryOperator' Name='=' Priority='2000' Id='637840162080284238' IndexInText='156' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
					<OperatorNameParts>
						<Name Name='=' Id='637840162080284228' IndexInText='156' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</OperatorNameParts>
				</OperatorInfo>
				<Operand2.BinaryOperator Name='+' Priority='30' Id='637840162080619623' IndexInText='158' ItemLength='79' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
					<Operand1.BinaryOperator Name='+' Priority='30' Id='637840162080619606' IndexInText='158' ItemLength='23' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.Literal Id='637840162080284382' IndexInText='158' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='x1' Id='637840162080284378' IndexInText='158' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand1.Literal>
						<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637840162080284456' IndexInText='160' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<Name Name='+' Id='637840162080284451' IndexInText='160' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OperatorNameParts>
						</OperatorInfo>
						<Operand2.Braces Id='637840162080284624' IndexInText='161' ItemLength='20' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
							<RegularItems>
								<Literal Id='637840162080284586' IndexInText='161' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
									<NameExpressionItem Name='f1' Id='637840162080284582' IndexInText='161' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</Literal>
								<OpeningRoundBrace Name='(' Id='637840162080284627' IndexInText='163' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<Literal Id='637840162080284721' IndexInText='164' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
									<NameExpressionItem Name='x2' Id='637840162080284717' IndexInText='164' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</Literal>
								<Comma Name=',' Id='637840162080284762' IndexInText='166' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<BinaryOperator Name='+' Priority='30' Id='637840162080454184' IndexInText='168' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
									<Operand1.BinaryOperator Name='+' Priority='30' Id='637840162080454148' IndexInText='168' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
										<Operand1.Literal Id='637840162080284841' IndexInText='168' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
											<NameExpressionItem Name='x3' Id='637840162080284837' IndexInText='168' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</Operand1.Literal>
										<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637840162080284899' IndexInText='170' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
											<OperatorNameParts>
												<Name Name='+' Id='637840162080284894' IndexInText='170' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											</OperatorNameParts>
										</OperatorInfo>
										<Operand2.BinaryOperator Name='*' Priority='20' Id='637840162080454174' IndexInText='171' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
											<Operand1.Literal Id='637840162080285028' IndexInText='171' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
												<NameExpressionItem Name='x4' Id='637840162080285024' IndexInText='171' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											</Operand1.Literal>
											<OperatorInfo OperatorType='BinaryOperator' Name='*' Priority='20' Id='637840162080285081' IndexInText='173' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
												<OperatorNameParts>
													<Name Name='*' Id='637840162080285076' IndexInText='173' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												</OperatorNameParts>
											</OperatorInfo>
											<Operand2.ConstantNumericValue Id='637840162080370054' IndexInText='174' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
												<NameExpressionItem Name='5' Id='637840162080370006' IndexInText='174' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												<OtherProperties>
													<SucceededNumericTypeDescriptor ObjectId='35' NumberTypeId='1581134183960' Interface='UniversalExpressionParser.NumericTypeDescriptor'>
														<RegularExpressions ObjectId='36' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[System.String]'>
															<System.String value='^\d+' />
														</RegularExpressions>
													</SucceededNumericTypeDescriptor>
												</OtherProperties>
											</Operand2.ConstantNumericValue>
										</Operand2.BinaryOperator>
									</Operand1.BinaryOperator>
									<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637840162080370302' IndexInText='175' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
										<OperatorNameParts>
											<Name Name='+' Id='637840162080370291' IndexInText='175' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</OperatorNameParts>
									</OperatorInfo>
									<Operand2.Braces Id='637840162080370513' IndexInText='176' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
										<RegularItems>
											<Literal Id='637840162080370472' IndexInText='176' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
												<NameExpressionItem Name='x' Id='637840162080370468' IndexInText='176' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											</Literal>
											<OpeningSquareBrace Name='[' Id='637840162080370516' IndexInText='177' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<ConstantNumericValue Id='637840162080453909' IndexInText='178' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
												<NameExpressionItem Name='1' Id='637840162080453861' IndexInText='178' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												<OtherProperties>
													<SucceededNumericTypeDescriptor ObjectId='35' NumberTypeId='1581134183960' Interface='UniversalExpressionParser.NumericTypeDescriptor'/>
												</OtherProperties>
											</ConstantNumericValue>
											<ClosingSquareBrace Name=']' Id='637840162080454094' IndexInText='179' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</RegularItems>
										<Children>
											<ConstantNumericValue Id='637840162080453909' IndexInText='178' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'/>
										</Children>
										<OtherProperties>
											<NamedExpressionItem Id='637840162080370472' IndexInText='176' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
											<OpeningBraceInfo Name='[' Id='637840162080370516' IndexInText='177' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<ClosingBraceInfo Name=']' Id='637840162080454094' IndexInText='179' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<Parameters ObjectId='46' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
												<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162080453909' IndexInText='178' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'/>
											</Parameters>
										</OtherProperties>
									</Operand2.Braces>
								</BinaryOperator>
								<ClosingRoundBrace Name=')' Id='637840162080454255' IndexInText='180' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</RegularItems>
							<Children>
								<Literal Id='637840162080284721' IndexInText='164' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								<BinaryOperator Name='+' Priority='30' Id='637840162080454184' IndexInText='168' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
							</Children>
							<OtherProperties>
								<NamedExpressionItem Id='637840162080284586' IndexInText='161' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								<OpeningBraceInfo Name='(' Id='637840162080284627' IndexInText='163' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<ClosingBraceInfo Name=')' Id='637840162080454255' IndexInText='180' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<Parameters ObjectId='48' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
									<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162080284721' IndexInText='164' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name='+' Priority='30' Id='637840162080454184' IndexInText='168' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
								</Parameters>
							</OtherProperties>
						</Operand2.Braces>
					</Operand1.BinaryOperator>
					<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637840162080454369' IndexInText='181' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
						<OperatorNameParts>
							<Name Name='+' Id='637840162080454360' IndexInText='181' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OperatorNameParts>
					</OperatorInfo>
					<Operand2.Braces Id='637840162080454603' IndexInText='193' ItemLength='44' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<Literal Id='637840162080454547' IndexInText='193' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='matrix1' Id='637840162080454541' IndexInText='193' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<OpeningSquareBrace Name='[' Id='637840162080454607' IndexInText='200' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Braces Id='637840162080454645' IndexInText='201' ItemLength='14' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
								<RegularItems>
									<OpeningSquareBrace Name='[' Id='637840162080454648' IndexInText='201' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<BinaryOperator Name='+' Priority='30' Id='637840162080535571' IndexInText='202' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
										<Operand1.Literal Id='637840162080454729' IndexInText='202' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
											<NameExpressionItem Name='y1' Id='637840162080454725' IndexInText='202' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</Operand1.Literal>
										<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637840162080454787' IndexInText='204' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
											<OperatorNameParts>
												<Name Name='+' Id='637840162080454782' IndexInText='204' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											</OperatorNameParts>
										</OperatorInfo>
										<Operand2.ConstantNumericValue Id='637840162080535397' IndexInText='205' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
											<NameExpressionItem Name='3' Id='637840162080535363' IndexInText='205' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<OtherProperties>
												<SucceededNumericTypeDescriptor ObjectId='35' NumberTypeId='1581134183960' Interface='UniversalExpressionParser.NumericTypeDescriptor'/>
											</OtherProperties>
										</Operand2.ConstantNumericValue>
									</BinaryOperator>
									<Comma Name=',' Id='637840162080535634' IndexInText='206' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Braces Id='637840162080535815' IndexInText='208' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
										<RegularItems>
											<Literal Id='637840162080535776' IndexInText='208' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
												<NameExpressionItem Name='f1' Id='637840162080535772' IndexInText='208' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											</Literal>
											<OpeningRoundBrace Name='(' Id='637840162080535819' IndexInText='210' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<Literal Id='637840162080535909' IndexInText='211' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
												<NameExpressionItem Name='x4' Id='637840162080535905' IndexInText='211' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											</Literal>
											<ClosingRoundBrace Name=')' Id='637840162080535941' IndexInText='213' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</RegularItems>
										<Children>
											<Literal Id='637840162080535909' IndexInText='211' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
										</Children>
										<OtherProperties>
											<NamedExpressionItem Id='637840162080535776' IndexInText='208' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
											<OpeningBraceInfo Name='(' Id='637840162080535819' IndexInText='210' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<ClosingBraceInfo Name=')' Id='637840162080535941' IndexInText='213' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<Parameters ObjectId='72' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
												<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162080535909' IndexInText='211' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
											</Parameters>
										</OtherProperties>
									</Braces>
									<ClosingSquareBrace Name=']' Id='637840162080535982' IndexInText='214' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Children>
									<BinaryOperator Name='+' Priority='30' Id='637840162080535571' IndexInText='202' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
									<Braces Id='637840162080535815' IndexInText='208' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
								</Children>
								<OtherProperties>
									<OpeningBraceInfo Name='[' Id='637840162080454648' IndexInText='201' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ClosingBraceInfo Name=']' Id='637840162080535982' IndexInText='214' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
									<Parameters ObjectId='74' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name='+' Priority='30' Id='637840162080535571' IndexInText='202' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162080535815' IndexInText='208' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
									</Parameters>
								</OtherProperties>
							</Braces>
							<Comma Name=',' Id='637840162080536011' IndexInText='215' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Literal Id='637840162080536095' IndexInText='217' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='x3' Id='637840162080536090' IndexInText='217' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<Comma Name=',' Id='637840162080536122' IndexInText='219' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Braces Id='637840162080536231' IndexInText='221' ItemLength='15' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
								<RegularItems>
									<Literal Id='637840162080536206' IndexInText='221' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='f2' Id='637840162080536197' IndexInText='221' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<OpeningRoundBrace Name='(' Id='637840162080536233' IndexInText='223' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637840162080536317' IndexInText='224' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='x3' Id='637840162080536309' IndexInText='224' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<Comma Name=',' Id='637840162080536344' IndexInText='226' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Braces Id='637840162080536450' IndexInText='228' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
										<RegularItems>
											<Literal Id='637840162080536421' IndexInText='228' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
												<NameExpressionItem Name='m2' Id='637840162080536417' IndexInText='228' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											</Literal>
											<OpeningSquareBrace Name='[' Id='637840162080536454' IndexInText='230' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<BinaryOperator Name='+' Priority='30' Id='637840162080619414' IndexInText='231' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
												<Operand1.Literal Id='637840162080536535' IndexInText='231' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
													<NameExpressionItem Name='x' Id='637840162080536531' IndexInText='231' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												</Operand1.Literal>
												<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637840162080536599' IndexInText='232' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
													<OperatorNameParts>
														<Name Name='+' Id='637840162080536590' IndexInText='232' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
													</OperatorNameParts>
												</OperatorInfo>
												<Operand2.ConstantNumericValue Id='637840162080619217' IndexInText='233' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
													<NameExpressionItem Name='5' Id='637840162080619170' IndexInText='233' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
													<OtherProperties>
														<SucceededNumericTypeDescriptor ObjectId='35' NumberTypeId='1581134183960' Interface='UniversalExpressionParser.NumericTypeDescriptor'/>
													</OtherProperties>
												</Operand2.ConstantNumericValue>
											</BinaryOperator>
											<ClosingSquareBrace Name=']' Id='637840162080619487' IndexInText='234' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</RegularItems>
										<Children>
											<BinaryOperator Name='+' Priority='30' Id='637840162080619414' IndexInText='231' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
										</Children>
										<OtherProperties>
											<NamedExpressionItem Id='637840162080536421' IndexInText='228' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
											<OpeningBraceInfo Name='[' Id='637840162080536454' IndexInText='230' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<ClosingBraceInfo Name=']' Id='637840162080619487' IndexInText='234' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<Parameters ObjectId='98' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
												<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name='+' Priority='30' Id='637840162080619414' IndexInText='231' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
											</Parameters>
										</OtherProperties>
									</Braces>
									<ClosingRoundBrace Name=')' Id='637840162080619532' IndexInText='235' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Children>
									<Literal Id='637840162080536317' IndexInText='224' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<Braces Id='637840162080536450' IndexInText='228' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
								</Children>
								<OtherProperties>
									<NamedExpressionItem Id='637840162080536206' IndexInText='221' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<OpeningBraceInfo Name='(' Id='637840162080536233' IndexInText='223' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ClosingBraceInfo Name=')' Id='637840162080619532' IndexInText='235' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Parameters ObjectId='100' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162080536317' IndexInText='224' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162080536450' IndexInText='228' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
									</Parameters>
								</OtherProperties>
							</Braces>
							<ClosingSquareBrace Name=']' Id='637840162080619563' IndexInText='236' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<Braces Id='637840162080454645' IndexInText='201' ItemLength='14' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
							<Literal Id='637840162080536095' IndexInText='217' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							<Braces Id='637840162080536231' IndexInText='221' ItemLength='15' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
						</Children>
						<OtherProperties>
							<NamedExpressionItem Id='637840162080454547' IndexInText='193' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							<OpeningBraceInfo Name='[' Id='637840162080454607' IndexInText='200' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=']' Id='637840162080619563' IndexInText='236' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Parameters ObjectId='102' Count='3' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162080454645' IndexInText='201' ItemLength='14' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162080536095' IndexInText='217' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162080536231' IndexInText='221' ItemLength='15' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Operand2.Braces>
				</Operand2.BinaryOperator>
			</BinaryOperator>
			<ExpressionSeparator Name=';' Id='637840162080619654' IndexInText='237' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<BinaryOperator Name='=>' Priority='1000' Id='637840162080620500' IndexInText='242' ItemLength='15' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
				<Operand1.Braces Id='637840162080619829' IndexInText='242' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
					<RegularItems>
						<Literal Id='637840162080619795' IndexInText='242' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='f1' Id='637840162080619791' IndexInText='242' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Literal>
						<OpeningRoundBrace Name='(' Id='637840162080619833' IndexInText='244' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<Literal Id='637840162080619921' IndexInText='245' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='x' Id='637840162080619917' IndexInText='245' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Literal>
						<Comma Name=',' Id='637840162080619953' IndexInText='246' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<Literal Id='637840162080620037' IndexInText='248' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='y' Id='637840162080620033' IndexInText='248' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Literal>
						<ClosingRoundBrace Name=')' Id='637840162080620064' IndexInText='249' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</RegularItems>
					<Children>
						<Literal Id='637840162080619921' IndexInText='245' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
						<Literal Id='637840162080620037' IndexInText='248' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					</Children>
					<OtherProperties>
						<NamedExpressionItem Id='637840162080619795' IndexInText='242' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
						<OpeningBraceInfo Name='(' Id='637840162080619833' IndexInText='244' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<ClosingBraceInfo Name=')' Id='637840162080620064' IndexInText='249' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<Parameters ObjectId='115' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
							<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162080619921' IndexInText='245' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162080620037' IndexInText='248' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
						</Parameters>
					</OtherProperties>
				</Operand1.Braces>
				<OperatorInfo OperatorType='BinaryOperator' Name='=>' Priority='1000' Id='637840162080620151' IndexInText='251' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
					<OperatorNameParts>
						<Name Name='=>' Id='637840162080620140' IndexInText='251' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</OperatorNameParts>
				</OperatorInfo>
				<Operand2.BinaryOperator Name='+' Priority='30' Id='637840162080620509' IndexInText='254' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
					<Operand1.Literal Id='637840162080620280' IndexInText='254' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='x' Id='637840162080620276' IndexInText='254' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Operand1.Literal>
					<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637840162080620340' IndexInText='255' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
						<OperatorNameParts>
							<Name Name='+' Id='637840162080620335' IndexInText='255' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OperatorNameParts>
					</OperatorInfo>
					<Operand2.Literal Id='637840162080620469' IndexInText='256' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='y' Id='637840162080620465' IndexInText='256' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Operand2.Literal>
				</Operand2.BinaryOperator>
			</BinaryOperator>
			<ExpressionSeparator Name=';' Id='637840162080620528' IndexInText='257' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<Braces Id='637840162080620637' IndexInText='262' ItemLength='137' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
				<RegularItems>
					<Literal Id='637840162080620612' IndexInText='262' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='f2' Id='637840162080620608' IndexInText='262' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<OpeningSquareBrace Name='[' Id='637840162080620642' IndexInText='264' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Literal Id='637840162080620729' IndexInText='265' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='x' Id='637840162080620722' IndexInText='265' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<Comma Name=',' Id='637840162080620758' IndexInText='266' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Literal Id='637840162080620837' IndexInText='268' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='y' Id='637840162080620833' IndexInText='268' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<ClosingSquareBrace Name=']' Id='637840162080620863' IndexInText='269' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Postfixes>
					<CodeBlock Id='637840162080620893' IndexInText='273' ItemLength='126' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
						<RegularItems>
							<CodeBlockStartMarker Name='{' Id='637840162080620889' IndexInText='273' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637840162080620951' IndexInText='398' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<OtherProperties>
							<CodeBlockStartMarker Name='{' Id='637840162080620889' IndexInText='273' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637840162080620951' IndexInText='398' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OtherProperties>
					</CodeBlock>
				</Postfixes>
				<Children>
					<Literal Id='637840162080620729' IndexInText='265' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					<Literal Id='637840162080620837' IndexInText='268' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
				</Children>
				<OtherProperties>
					<NamedExpressionItem Id='637840162080620612' IndexInText='262' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					<OpeningBraceInfo Name='[' Id='637840162080620642' IndexInText='264' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ClosingBraceInfo Name=']' Id='637840162080620863' IndexInText='269' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Parameters ObjectId='139' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162080620729' IndexInText='265' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162080620837' IndexInText='268' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					</Parameters>
				</OtherProperties>
			</Braces>
		</RegularItems>
		<Children>
			<BinaryOperator Name='=' Priority='2000' Id='637840162080619595' IndexInText='150' ItemLength='87' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
			<BinaryOperator Name='=>' Priority='1000' Id='637840162080620500' IndexInText='242' ItemLength='15' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
			<Braces Id='637840162080620637' IndexInText='262' ItemLength='137' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
		</Children>
	</ExpressionItemSeries>
	<ParseErrorData ObjectId='140' HasCriticalErrors='False' Interface='UniversalExpressionParser.IParseErrorData'>
		<AllCodeItemErrors ObjectId='141' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICodeItemParseErrorData]'/>
	</ParseErrorData>
	<SortedCommentedOutCodeInfos ObjectId='142' Count='5' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICommentedOutCodeInfo]'>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='143' IsLineComment='True' IndexInText='0' ItemLength='47'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='144' IsLineComment='True' IndexInText='49' ItemLength='79'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='145' IsLineComment='True' IndexInText='130' ItemLength='18'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='146' IsLineComment='True' IndexInText='279' ItemLength='86'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='147' IsLineComment='True' IndexInText='370' ItemLength='26'/>
	</SortedCommentedOutCodeInfos>
	<MainExpressionItem Id='637840162080283699' IndexInText='150' ItemLength='249' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'/>
</UniversalExpressionParser.ExpressionItems.IRootExpressionItemSeries>
```
</details>

# Operators

- Operators are defined by implementing the property **System.Collections.Generic.IReadOnlyList<UniversalExpressionParser.IOperatorInfo> Operators { get; }** in **UniversalExpressionParser.IExpressionLanguageProvider**.
- The interface **UniversalExpressionParser.IOperatorInfo** has properties for operator name (i.e., a collection of texts that operator consists of like ["IS", "NOT", "NUL"] or ["+="]), priority, unique Id, operator type (i.e., binary, unary prefix or postfix).
- Two different operators can have similar names, as long as they are different types of operators. For example "++" can be used both as prefix as well as postfix operator.

<details> <summary>Click to see example of defining operators in implementation of UniversalExpressionParser.IExpressionLanguageProvider.</summary>

```csharp

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
```
</details>


- Operator expression (e.g., "a * b + c * d") is parsed to an expression item of type **UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem** (a subclass of **UniversalExpressionParser.ExpressionItems.IComplexExpressionItem**).
  
<details> <summary>Click to see the definition of UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem.</summary>

```csharp
using JetBrains.Annotations;

namespace UniversalExpressionParser.ExpressionItems
{
    /// <summary>
    /// Interface for operator expression items.
    /// </summary>
    public interface IOperatorExpressionItem : IComplexExpressionItem
    {
        /// <summary>
        /// Expression item for operator. For example in x+y, + will be parsed to <see cref="IOperatorInfoExpressionItem"/>.
        /// On the other hand in expression ++x, ++ will be parsed to <see cref="IOperatorInfoExpressionItem"/>.
        /// </summary>
        [NotNull]
        IOperatorInfoExpressionItem OperatorInfoExpressionItem { get; }

        /// <summary>
        /// Operand 1. The value cannot be null
        /// </summary>
        [NotNull]
        IExpressionItemBase Operand1 { get; }


        /// <summary>
        /// Operand 2. The value is null if either the operator is unary operator, or if the second operator failed to parse.
        /// </summary>
        [CanBeNull]
        IExpressionItemBase Operand2 { get; }
    }
}
```
</details>

For example the expression "a * b + c * d", will be parsed to an expression similar to "*(+(a, b), +(x,d))". 

This is so since the binary operator "+" has lower priority (the value of **IOperatorInfo.Priority** is larger), then the binary operator "*". 

In other words this expression will be parsed to a binary operator expression item for "+" (i.e., an instance of **IOperatorExpressionItem**) with Operand1 and Operand2 also being binary operator expressions of types **UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem** for expressions "a * b" and "c * d".


- Example of parsing operators:

```csharp
// The binary operator + has priority 30 and * has priority 20. Therefore, 
// in expression below,  * is applied first and + is applied next.
// The following expression is parsed to an expression equivalent to 
// "=(y, +(x1, *(f1(x2, +(x3, 1)), x4)))"
var y = x1 + f1(x2,x3+1)*x4;
```

<details> <summary>Click to expand the parsed expression</summary>

```XML
<?xml version="1.0" encoding="utf-8"?>
<UniversalExpressionParser.ExpressionItems.IRootExpressionItemSeries ObjectId='0' PositionInTextOnCompletion='287' IndexInText='259' ItemLength='28'>
	<ExpressionItemSeries Id='637840162082253663' IndexInText='259' ItemLength='28' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'>
		<RegularItems>
			<BinaryOperator Name='=' Priority='2000' Id='637840162082343215' IndexInText='259' ItemLength='27' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
				<Operand1.Literal Id='637840162082254083' IndexInText='259' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
					<NameExpressionItem Name='y' Id='637840162082254077' IndexInText='263' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<AppliedKeywords>
						<Keyword Name='var' Id='637840162082253935' IndexInText='259' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
							<LanguageKeywordInfo ObjectId='6' Id='637781064051574641' Keyword='var' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
						</Keyword>
					</AppliedKeywords>
				</Operand1.Literal>
				<OperatorInfo OperatorType='BinaryOperator' Name='=' Priority='2000' Id='637840162082254190' IndexInText='265' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
					<OperatorNameParts>
						<Name Name='=' Id='637840162082254176' IndexInText='265' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</OperatorNameParts>
				</OperatorInfo>
				<Operand2.BinaryOperator Name='+' Priority='30' Id='637840162082343227' IndexInText='267' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
					<Operand1.Literal Id='637840162082254332' IndexInText='267' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='x1' Id='637840162082254328' IndexInText='267' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Operand1.Literal>
					<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637840162082254406' IndexInText='270' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
						<OperatorNameParts>
							<Name Name='+' Id='637840162082254400' IndexInText='270' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OperatorNameParts>
					</OperatorInfo>
					<Operand2.BinaryOperator Name='*' Priority='20' Id='637840162082343235' IndexInText='272' ItemLength='14' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.Braces Id='637840162082254583' IndexInText='272' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
							<RegularItems>
								<Literal Id='637840162082254544' IndexInText='272' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
									<NameExpressionItem Name='f1' Id='637840162082254539' IndexInText='272' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</Literal>
								<OpeningRoundBrace Name='(' Id='637840162082254617' IndexInText='274' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<Literal Id='637840162082254713' IndexInText='275' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
									<NameExpressionItem Name='x2' Id='637840162082254709' IndexInText='275' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</Literal>
								<Comma Name=',' Id='637840162082254749' IndexInText='277' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<BinaryOperator Name='+' Priority='30' Id='637840162082342739' IndexInText='278' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
									<Operand1.Literal Id='637840162082254833' IndexInText='278' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='x3' Id='637840162082254829' IndexInText='278' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Operand1.Literal>
									<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637840162082254895' IndexInText='280' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
										<OperatorNameParts>
											<Name Name='+' Id='637840162082254889' IndexInText='280' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</OperatorNameParts>
									</OperatorInfo>
									<Operand2.ConstantNumericValue Id='637840162082342508' IndexInText='281' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
										<NameExpressionItem Name='1' Id='637840162082342451' IndexInText='281' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										<OtherProperties>
											<SucceededNumericTypeDescriptor ObjectId='29' NumberTypeId='1581134183960' Interface='UniversalExpressionParser.NumericTypeDescriptor'>
												<RegularExpressions ObjectId='30' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[System.String]'>
													<System.String value='^\d+' />
												</RegularExpressions>
											</SucceededNumericTypeDescriptor>
										</OtherProperties>
									</Operand2.ConstantNumericValue>
								</BinaryOperator>
								<ClosingRoundBrace Name=')' Id='637840162082342843' IndexInText='282' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</RegularItems>
							<Children>
								<Literal Id='637840162082254713' IndexInText='275' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								<BinaryOperator Name='+' Priority='30' Id='637840162082342739' IndexInText='278' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
							</Children>
							<OtherProperties>
								<NamedExpressionItem Id='637840162082254544' IndexInText='272' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								<OpeningBraceInfo Name='(' Id='637840162082254617' IndexInText='274' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<ClosingBraceInfo Name=')' Id='637840162082342843' IndexInText='282' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<Parameters ObjectId='32' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
									<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162082254713' IndexInText='275' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name='+' Priority='30' Id='637840162082342739' IndexInText='278' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
								</Parameters>
							</OtherProperties>
						</Operand1.Braces>
						<OperatorInfo OperatorType='BinaryOperator' Name='*' Priority='20' Id='637840162082342993' IndexInText='283' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<Name Name='*' Id='637840162082342982' IndexInText='283' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OperatorNameParts>
						</OperatorInfo>
						<Operand2.Literal Id='637840162082343172' IndexInText='284' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='x4' Id='637840162082343168' IndexInText='284' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand2.Literal>
					</Operand2.BinaryOperator>
				</Operand2.BinaryOperator>
			</BinaryOperator>
			<ExpressionSeparator Name=';' Id='637840162082343275' IndexInText='286' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
		</RegularItems>
		<Children>
			<BinaryOperator Name='=' Priority='2000' Id='637840162082343215' IndexInText='259' ItemLength='27' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
		</Children>
	</ExpressionItemSeries>
	<ParseErrorData ObjectId='38' HasCriticalErrors='False' Interface='UniversalExpressionParser.IParseErrorData'>
		<AllCodeItemErrors ObjectId='39' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICodeItemParseErrorData]'/>
	</ParseErrorData>
	<SortedCommentedOutCodeInfos ObjectId='40' Count='4' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICommentedOutCodeInfo]'>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='41' IsLineComment='True' IndexInText='0' ItemLength='75'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='42' IsLineComment='True' IndexInText='77' ItemLength='66'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='43' IsLineComment='True' IndexInText='145' ItemLength='69'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='44' IsLineComment='True' IndexInText='216' ItemLength='41'/>
	</SortedCommentedOutCodeInfos>
	<MainExpressionItem Id='637840162082253663' IndexInText='259' ItemLength='28' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'/>
</UniversalExpressionParser.ExpressionItems.IRootExpressionItemSeries>
```
</details>


- Example of using braces to change order of application of operators:

```csharp
// Without the braces, the expression below would be equivalent to x1+(x2*x3)-x4.
var y1 = [x1+x2]*(x3-x4);

```

<details> <summary>Click to expand the parsed expression</summary>

```XML
<?xml version="1.0" encoding="utf-8"?>
<UniversalExpressionParser.ExpressionItems.IRootExpressionItemSeries ObjectId='0' PositionInTextOnCompletion='110' IndexInText='83' ItemLength='25'>
	<ExpressionItemSeries Id='637840162081999047' IndexInText='83' ItemLength='25' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'>
		<RegularItems>
			<BinaryOperator Name='=' Priority='2000' Id='637840162082000654' IndexInText='83' ItemLength='24' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
				<Operand1.Literal Id='637840162081999454' IndexInText='83' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
					<NameExpressionItem Name='y1' Id='637840162081999449' IndexInText='87' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<AppliedKeywords>
						<Keyword Name='var' Id='637840162081999309' IndexInText='83' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
							<LanguageKeywordInfo ObjectId='6' Id='637781064051574641' Keyword='var' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
						</Keyword>
					</AppliedKeywords>
				</Operand1.Literal>
				<OperatorInfo OperatorType='BinaryOperator' Name='=' Priority='2000' Id='637840162081999551' IndexInText='90' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
					<OperatorNameParts>
						<Name Name='=' Id='637840162081999541' IndexInText='90' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</OperatorNameParts>
				</OperatorInfo>
				<Operand2.BinaryOperator Name='*' Priority='20' Id='637840162082000662' IndexInText='92' ItemLength='15' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
					<Operand1.Braces Id='637840162081999685' IndexInText='92' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<OpeningSquareBrace Name='[' Id='637840162081999689' IndexInText='92' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<BinaryOperator Name='+' Priority='30' Id='637840162082000031' IndexInText='93' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
								<Operand1.Literal Id='637840162081999794' IndexInText='93' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
									<NameExpressionItem Name='x1' Id='637840162081999789' IndexInText='93' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</Operand1.Literal>
								<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637840162081999852' IndexInText='95' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
									<OperatorNameParts>
										<Name Name='+' Id='637840162081999846' IndexInText='95' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</OperatorNameParts>
								</OperatorInfo>
								<Operand2.Literal Id='637840162081999981' IndexInText='96' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
									<NameExpressionItem Name='x2' Id='637840162081999977' IndexInText='96' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</Operand2.Literal>
							</BinaryOperator>
							<ClosingSquareBrace Name=']' Id='637840162082000086' IndexInText='98' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<BinaryOperator Name='+' Priority='30' Id='637840162082000031' IndexInText='93' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
						</Children>
						<OtherProperties>
							<OpeningBraceInfo Name='[' Id='637840162081999689' IndexInText='92' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=']' Id='637840162082000086' IndexInText='98' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
							<Parameters ObjectId='20' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name='+' Priority='30' Id='637840162082000031' IndexInText='93' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Operand1.Braces>
					<OperatorInfo OperatorType='BinaryOperator' Name='*' Priority='20' Id='637840162082000148' IndexInText='99' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
						<OperatorNameParts>
							<Name Name='*' Id='637840162082000142' IndexInText='99' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OperatorNameParts>
					</OperatorInfo>
					<Operand2.Braces Id='637840162082000275' IndexInText='100' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<OpeningRoundBrace Name='(' Id='637840162082000279' IndexInText='100' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<BinaryOperator Name='-' Priority='30' Id='637840162082000604' IndexInText='101' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
								<Operand1.Literal Id='637840162082000366' IndexInText='101' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
									<NameExpressionItem Name='x3' Id='637840162082000362' IndexInText='101' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</Operand1.Literal>
								<OperatorInfo OperatorType='BinaryOperator' Name='-' Priority='30' Id='637840162082000437' IndexInText='103' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
									<OperatorNameParts>
										<Name Name='-' Id='637840162082000433' IndexInText='103' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</OperatorNameParts>
								</OperatorInfo>
								<Operand2.Literal Id='637840162082000566' IndexInText='104' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
									<NameExpressionItem Name='x4' Id='637840162082000562' IndexInText='104' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</Operand2.Literal>
							</BinaryOperator>
							<ClosingRoundBrace Name=')' Id='637840162082000624' IndexInText='106' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<BinaryOperator Name='-' Priority='30' Id='637840162082000604' IndexInText='101' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
						</Children>
						<OtherProperties>
							<OpeningBraceInfo Name='(' Id='637840162082000279' IndexInText='100' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=')' Id='637840162082000624' IndexInText='106' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
							<Parameters ObjectId='33' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name='-' Priority='30' Id='637840162082000604' IndexInText='101' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Operand2.Braces>
				</Operand2.BinaryOperator>
			</BinaryOperator>
			<ExpressionSeparator Name=';' Id='637840162082000686' IndexInText='107' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
		</RegularItems>
		<Children>
			<BinaryOperator Name='=' Priority='2000' Id='637840162082000654' IndexInText='83' ItemLength='24' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
		</Children>
	</ExpressionItemSeries>
	<ParseErrorData ObjectId='35' HasCriticalErrors='False' Interface='UniversalExpressionParser.IParseErrorData'>
		<AllCodeItemErrors ObjectId='36' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICodeItemParseErrorData]'/>
	</ParseErrorData>
	<SortedCommentedOutCodeInfos ObjectId='37' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICommentedOutCodeInfo]'>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='38' IsLineComment='True' IndexInText='0' ItemLength='81'/>
	</SortedCommentedOutCodeInfos>
	<MainExpressionItem Id='637840162081999047' IndexInText='83' ItemLength='25' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'/>
</UniversalExpressionParser.ExpressionItems.IRootExpressionItemSeries>
```
</details>


- Example of operators with multiple parts in operator names:

```csharp
// The expression below is similar to 
// z = !((x1 IS NOT NULL) && (x2 IS NULL);
z = !(x1 IS NOT NULL && x2 IS NULL);
```

<details> <summary>Click to expand the parsed expression</summary>

```XML
<?xml version="1.0" encoding="utf-8"?>
<UniversalExpressionParser.ExpressionItems.IRootExpressionItemSeries ObjectId='0' PositionInTextOnCompletion='120' IndexInText='84' ItemLength='36'>
	<ExpressionItemSeries Id='637840162082074498' IndexInText='84' ItemLength='36' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'>
		<RegularItems>
			<BinaryOperator Name='=' Priority='2000' Id='637840162082076189' IndexInText='84' ItemLength='35' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
				<Operand1.Literal Id='637840162082074939' IndexInText='84' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
					<NameExpressionItem Name='z' Id='637840162082074930' IndexInText='84' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</Operand1.Literal>
				<OperatorInfo OperatorType='BinaryOperator' Name='=' Priority='2000' Id='637840162082075046' IndexInText='86' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
					<OperatorNameParts>
						<Name Name='=' Id='637840162082075033' IndexInText='86' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</OperatorNameParts>
				</OperatorInfo>
				<Operand2.PrefixUnaryOperator Name='!' Priority='0' Id='637840162082076234' IndexInText='88' ItemLength='31' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
					<OperatorInfo OperatorType='PrefixUnaryOperator' Name='!' Priority='0' Id='637840162082075190' IndexInText='88' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
						<OperatorNameParts>
							<Name Name='!' Id='637840162082075175' IndexInText='88' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OperatorNameParts>
					</OperatorInfo>
					<Operand1.Braces Id='637840162082075316' IndexInText='89' ItemLength='30' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<OpeningRoundBrace Name='(' Id='637840162082075322' IndexInText='89' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<BinaryOperator Name='&&' Priority='80' Id='637840162082076036' IndexInText='90' ItemLength='28' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
								<Operand1.PostfixUnaryOperator Name='IS NOT NULL' Priority='1' Id='637840162082076010' IndexInText='90' ItemLength='14' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
									<Operand1.Literal Id='637840162082075453' IndexInText='90' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='x1' Id='637840162082075443' IndexInText='90' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Operand1.Literal>
									<OperatorInfo OperatorType='PostfixUnaryOperator' Name='IS NOT NULL' Priority='1' Id='637840162082075520' IndexInText='93' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
										<OperatorNameParts>
											<Name Name='IS' Id='637840162082075495' IndexInText='93' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<Name Name='NOT' Id='637840162082075507' IndexInText='96' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<Name Name='NULL' Id='637840162082075515' IndexInText='100' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</OperatorNameParts>
									</OperatorInfo>
								</Operand1.PostfixUnaryOperator>
								<OperatorInfo OperatorType='BinaryOperator' Name='&&' Priority='80' Id='637840162082075620' IndexInText='105' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
									<OperatorNameParts>
										<Name Name='&&' Id='637840162082075615' IndexInText='105' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</OperatorNameParts>
								</OperatorInfo>
								<Operand2.PostfixUnaryOperator Name='IS NULL' Priority='1' Id='637840162082076070' IndexInText='108' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
									<Operand1.Literal Id='637840162082075808' IndexInText='108' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='x2' Id='637840162082075798' IndexInText='108' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Operand1.Literal>
									<OperatorInfo OperatorType='PostfixUnaryOperator' Name='IS NULL' Priority='1' Id='637840162082075887' IndexInText='111' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
										<OperatorNameParts>
											<Name Name='IS' Id='637840162082075873' IndexInText='111' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<Name Name='NULL' Id='637840162082075881' IndexInText='114' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</OperatorNameParts>
									</OperatorInfo>
								</Operand2.PostfixUnaryOperator>
							</BinaryOperator>
							<ClosingRoundBrace Name=')' Id='637840162082076135' IndexInText='118' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<BinaryOperator Name='&&' Priority='80' Id='637840162082076036' IndexInText='90' ItemLength='28' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
						</Children>
						<OtherProperties>
							<OpeningBraceInfo Name='(' Id='637840162082075322' IndexInText='89' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=')' Id='637840162082076135' IndexInText='118' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
							<Parameters ObjectId='29' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name='&&' Priority='80' Id='637840162082076036' IndexInText='90' ItemLength='28' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Operand1.Braces>
				</Operand2.PrefixUnaryOperator>
			</BinaryOperator>
			<ExpressionSeparator Name=';' Id='637840162082076259' IndexInText='119' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
		</RegularItems>
		<Children>
			<BinaryOperator Name='=' Priority='2000' Id='637840162082076189' IndexInText='84' ItemLength='35' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
		</Children>
	</ExpressionItemSeries>
	<ParseErrorData ObjectId='31' HasCriticalErrors='False' Interface='UniversalExpressionParser.IParseErrorData'>
		<AllCodeItemErrors ObjectId='32' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICodeItemParseErrorData]'/>
	</ParseErrorData>
	<SortedCommentedOutCodeInfos ObjectId='33' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICommentedOutCodeInfo]'>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='34' IsLineComment='True' IndexInText='0' ItemLength='38'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='35' IsLineComment='True' IndexInText='40' ItemLength='42'/>
	</SortedCommentedOutCodeInfos>
	<MainExpressionItem Id='637840162082074498' IndexInText='84' ItemLength='36' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'/>
</UniversalExpressionParser.ExpressionItems.IRootExpressionItemSeries>
```
</details>


- Example of two operators (e.g., postfix operators, then a binary operator) used next to each other without spaces in between:

```csharp
// The spaces between two ++ operators, and + was omitted intentionally to show that the parser will parse the expression 
// correctly even without the space.
// The expression below is similar to  println(((x1++)++)+x2). To avoid confusion, in some cases it is better to use braces.
println(x1+++++x2)
```

<details> <summary>Click to expand the parsed expression</summary>

```XML
<?xml version="1.0" encoding="utf-8"?>
<UniversalExpressionParser.ExpressionItems.IRootExpressionItemSeries ObjectId='0' PositionInTextOnCompletion='306' IndexInText='288' ItemLength='18'>
	<ExpressionItemSeries Id='637840162082171714' IndexInText='288' ItemLength='18' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'>
		<RegularItems>
			<Braces Id='637840162082172119' IndexInText='288' ItemLength='18' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
				<RegularItems>
					<Literal Id='637840162082172070' IndexInText='288' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='println' Id='637840162082172061' IndexInText='288' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<OpeningRoundBrace Name='(' Id='637840162082172123' IndexInText='295' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<BinaryOperator Name='+' Priority='30' Id='637840162082172735' IndexInText='296' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.PostfixUnaryOperator Name='++' Priority='1' Id='637840162082172712' IndexInText='296' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
							<Operand1.PostfixUnaryOperator Name='++' Priority='1' Id='637840162082172693' IndexInText='296' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
								<Operand1.Literal Id='637840162082172223' IndexInText='296' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
									<NameExpressionItem Name='x1' Id='637840162082172219' IndexInText='296' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</Operand1.Literal>
								<OperatorInfo OperatorType='PostfixUnaryOperator' Name='++' Priority='1' Id='637840162082172297' IndexInText='298' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
									<OperatorNameParts>
										<Name Name='++' Id='637840162082172293' IndexInText='298' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</OperatorNameParts>
								</OperatorInfo>
							</Operand1.PostfixUnaryOperator>
							<OperatorInfo OperatorType='PostfixUnaryOperator' Name='++' Priority='1' Id='637840162082172399' IndexInText='300' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
								<OperatorNameParts>
									<Name Name='++' Id='637840162082172394' IndexInText='300' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</OperatorNameParts>
							</OperatorInfo>
						</Operand1.PostfixUnaryOperator>
						<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637840162082172485' IndexInText='302' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<Name Name='+' Id='637840162082172477' IndexInText='302' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OperatorNameParts>
						</OperatorInfo>
						<Operand2.Literal Id='637840162082172624' IndexInText='303' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='x2' Id='637840162082172619' IndexInText='303' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand2.Literal>
					</BinaryOperator>
					<ClosingRoundBrace Name=')' Id='637840162082172789' IndexInText='305' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Children>
					<BinaryOperator Name='+' Priority='30' Id='637840162082172735' IndexInText='296' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
				</Children>
				<OtherProperties>
					<NamedExpressionItem Id='637840162082172070' IndexInText='288' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					<OpeningBraceInfo Name='(' Id='637840162082172123' IndexInText='295' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ClosingBraceInfo Name=')' Id='637840162082172789' IndexInText='305' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Parameters ObjectId='20' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name='+' Priority='30' Id='637840162082172735' IndexInText='296' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
					</Parameters>
				</OtherProperties>
			</Braces>
		</RegularItems>
		<Children>
			<Braces Id='637840162082172119' IndexInText='288' ItemLength='18' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
		</Children>
	</ExpressionItemSeries>
	<ParseErrorData ObjectId='21' HasCriticalErrors='False' Interface='UniversalExpressionParser.IParseErrorData'>
		<AllCodeItemErrors ObjectId='22' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICodeItemParseErrorData]'/>
	</ParseErrorData>
	<SortedCommentedOutCodeInfos ObjectId='23' Count='3' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICommentedOutCodeInfo]'>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='24' IsLineComment='True' IndexInText='0' ItemLength='122'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='25' IsLineComment='True' IndexInText='124' ItemLength='36'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='26' IsLineComment='True' IndexInText='162' ItemLength='124'/>
	</SortedCommentedOutCodeInfos>
	<MainExpressionItem Id='637840162082171714' IndexInText='288' ItemLength='18' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'/>
</UniversalExpressionParser.ExpressionItems.IRootExpressionItemSeries>
```
</details>


- Example of unary prefix operator used to implement "return" statement:

```csharp
// return has priority int.MaxValue which is greater then any other operator priority, therefore
// the expression below is equialent to "return (x+(2.5*x))";
return x+2.5*y;

// another example within function body
f1(x:int, y:int) : bool
{
	// return has priority int.MaxValue which is greater then any other operator priority, therefore
	// the expression below is equialent to "return (x+(2.5*x))";
	return f(x)+y > 10;
}
```

<details> <summary>Click to expand the parsed expression</summary>

```XML
<?xml version="1.0" encoding="utf-8"?>
<UniversalExpressionParser.ExpressionItems.IRootExpressionItemSeries ObjectId='0' PositionInTextOnCompletion='435' IndexInText='161' ItemLength='249'>
	<ExpressionItemSeries Id='637840162082426606' IndexInText='161' ItemLength='274' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'>
		<RegularItems>
			<PrefixUnaryOperator Name='return' Priority='2147483647' Id='637840162082506097' IndexInText='161' ItemLength='14' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
				<OperatorInfo OperatorType='PrefixUnaryOperator' Name='return' Priority='2147483647' Id='637840162082426979' IndexInText='161' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
					<OperatorNameParts>
						<Name Name='return' Id='637840162082426962' IndexInText='161' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</OperatorNameParts>
				</OperatorInfo>
				<Operand1.BinaryOperator Name='+' Priority='30' Id='637840162082506134' IndexInText='168' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
					<Operand1.Literal Id='637840162082427136' IndexInText='168' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='x' Id='637840162082427131' IndexInText='168' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Operand1.Literal>
					<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637840162082427219' IndexInText='169' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
						<OperatorNameParts>
							<Name Name='+' Id='637840162082427214' IndexInText='169' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OperatorNameParts>
					</OperatorInfo>
					<Operand2.BinaryOperator Name='*' Priority='20' Id='637840162082506143' IndexInText='170' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.ConstantNumericValue Id='637840162082505560' IndexInText='170' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
							<NameExpressionItem Name='2.5' Id='637840162082505504' IndexInText='170' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<OtherProperties>
								<SucceededNumericTypeDescriptor ObjectId='13' NumberTypeId='1581134136626' Interface='UniversalExpressionParser.NumericTypeDescriptor'>
									<RegularExpressions ObjectId='14' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[System.String]'>
										<System.String value='^(\d+\.\d+|\d+\.|\.\d+)' />
									</RegularExpressions>
								</SucceededNumericTypeDescriptor>
							</OtherProperties>
						</Operand1.ConstantNumericValue>
						<OperatorInfo OperatorType='BinaryOperator' Name='*' Priority='20' Id='637840162082505859' IndexInText='173' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<Name Name='*' Id='637840162082505847' IndexInText='173' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OperatorNameParts>
						</OperatorInfo>
						<Operand2.Literal Id='637840162082506040' IndexInText='174' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='y' Id='637840162082506035' IndexInText='174' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand2.Literal>
					</Operand2.BinaryOperator>
				</Operand1.BinaryOperator>
			</PrefixUnaryOperator>
			<ExpressionSeparator Name=';' Id='637840162082506234' IndexInText='175' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<BinaryOperator Name=':' Priority='0' Id='637840162082590514' IndexInText='221' ItemLength='214' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
				<Operand1.Braces Id='637840162082506378' IndexInText='221' ItemLength='16' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
					<RegularItems>
						<Literal Id='637840162082506345' IndexInText='221' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='f1' Id='637840162082506341' IndexInText='221' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Literal>
						<OpeningRoundBrace Name='(' Id='637840162082506381' IndexInText='223' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<BinaryOperator Name=':' Priority='0' Id='637840162082506708' IndexInText='224' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
							<Operand1.Literal Id='637840162082506480' IndexInText='224' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='x' Id='637840162082506476' IndexInText='224' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Operand1.Literal>
							<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637840162082506528' IndexInText='225' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
								<OperatorNameParts>
									<Name Name=':' Id='637840162082506523' IndexInText='225' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</OperatorNameParts>
							</OperatorInfo>
							<Operand2.Literal Id='637840162082506673' IndexInText='226' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='int' Id='637840162082506668' IndexInText='226' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Operand2.Literal>
						</BinaryOperator>
						<Comma Name=',' Id='637840162082506735' IndexInText='229' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<BinaryOperator Name=':' Priority='0' Id='637840162082507042' IndexInText='231' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
							<Operand1.Literal Id='637840162082506821' IndexInText='231' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='y' Id='637840162082506817' IndexInText='231' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Operand1.Literal>
							<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637840162082506867' IndexInText='232' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
								<OperatorNameParts>
									<Name Name=':' Id='637840162082506861' IndexInText='232' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</OperatorNameParts>
							</OperatorInfo>
							<Operand2.Literal Id='637840162082507004' IndexInText='233' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='int' Id='637840162082507000' IndexInText='233' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Operand2.Literal>
						</BinaryOperator>
						<ClosingRoundBrace Name=')' Id='637840162082507060' IndexInText='236' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</RegularItems>
					<Children>
						<BinaryOperator Name=':' Priority='0' Id='637840162082506708' IndexInText='224' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
						<BinaryOperator Name=':' Priority='0' Id='637840162082507042' IndexInText='231' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
					</Children>
					<OtherProperties>
						<NamedExpressionItem Id='637840162082506345' IndexInText='221' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
						<OpeningBraceInfo Name='(' Id='637840162082506381' IndexInText='223' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<ClosingBraceInfo Name=')' Id='637840162082507060' IndexInText='236' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<Parameters ObjectId='41' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
							<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name=':' Priority='0' Id='637840162082506708' IndexInText='224' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
							<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name=':' Priority='0' Id='637840162082507042' IndexInText='231' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
						</Parameters>
					</OtherProperties>
				</Operand1.Braces>
				<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637840162082507124' IndexInText='238' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
					<OperatorNameParts>
						<Name Name=':' Id='637840162082507118' IndexInText='238' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</OperatorNameParts>
				</OperatorInfo>
				<Operand2.Literal Id='637840162082507272' IndexInText='240' ItemLength='195' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
					<NameExpressionItem Name='bool' Id='637840162082507268' IndexInText='240' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Postfixes>
						<CodeBlock Id='637840162082507313' IndexInText='246' ItemLength='189' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
							<RegularItems>
								<CodeBlockStartMarker Name='{' Id='637840162082507308' IndexInText='246' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<PrefixUnaryOperator Name='return' Priority='2147483647' Id='637840162082590347' IndexInText='413' ItemLength='18' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
									<OperatorInfo OperatorType='PrefixUnaryOperator' Name='return' Priority='2147483647' Id='637840162082507384' IndexInText='413' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
										<OperatorNameParts>
											<Name Name='return' Id='637840162082507379' IndexInText='413' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</OperatorNameParts>
									</OperatorInfo>
									<Operand1.BinaryOperator Name='>' Priority='50' Id='637840162082590381' IndexInText='420' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
										<Operand1.BinaryOperator Name='+' Priority='30' Id='637840162082590372' IndexInText='420' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
											<Operand1.Braces Id='637840162082507530' IndexInText='420' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
												<RegularItems>
													<Literal Id='637840162082507489' IndexInText='420' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
														<NameExpressionItem Name='f' Id='637840162082507485' IndexInText='420' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
													</Literal>
													<OpeningRoundBrace Name='(' Id='637840162082507533' IndexInText='421' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
													<Literal Id='637840162082507619' IndexInText='422' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
														<NameExpressionItem Name='x' Id='637840162082507615' IndexInText='422' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
													</Literal>
													<ClosingRoundBrace Name=')' Id='637840162082507648' IndexInText='423' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												</RegularItems>
												<Children>
													<Literal Id='637840162082507619' IndexInText='422' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
												</Children>
												<OtherProperties>
													<NamedExpressionItem Id='637840162082507489' IndexInText='420' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
													<OpeningBraceInfo Name='(' Id='637840162082507533' IndexInText='421' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
													<ClosingBraceInfo Name=')' Id='637840162082507648' IndexInText='423' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
													<Parameters ObjectId='60' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
														<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162082507619' IndexInText='422' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
													</Parameters>
												</OtherProperties>
											</Operand1.Braces>
											<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637840162082507708' IndexInText='424' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
												<OperatorNameParts>
													<Name Name='+' Id='637840162082507702' IndexInText='424' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												</OperatorNameParts>
											</OperatorInfo>
											<Operand2.Literal Id='637840162082507837' IndexInText='425' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
												<NameExpressionItem Name='y' Id='637840162082507833' IndexInText='425' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											</Operand2.Literal>
										</Operand1.BinaryOperator>
										<OperatorInfo OperatorType='BinaryOperator' Name='>' Priority='50' Id='637840162082507899' IndexInText='427' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
											<OperatorNameParts>
												<Name Name='>' Id='637840162082507894' IndexInText='427' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											</OperatorNameParts>
										</OperatorInfo>
										<Operand2.ConstantNumericValue Id='637840162082590158' IndexInText='429' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
											<NameExpressionItem Name='10' Id='637840162082590123' IndexInText='429' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<OtherProperties>
												<SucceededNumericTypeDescriptor ObjectId='69' NumberTypeId='1581134183960' Interface='UniversalExpressionParser.NumericTypeDescriptor'>
													<RegularExpressions ObjectId='70' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[System.String]'>
														<System.String value='^\d+' />
													</RegularExpressions>
												</SucceededNumericTypeDescriptor>
											</OtherProperties>
										</Operand2.ConstantNumericValue>
									</Operand1.BinaryOperator>
								</PrefixUnaryOperator>
								<ExpressionSeparator Name=';' Id='637840162082590449' IndexInText='431' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<CodeBlockEndMarker Name='}' Id='637840162082590495' IndexInText='434' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</RegularItems>
							<Children>
								<PrefixUnaryOperator Name='return' Priority='2147483647' Id='637840162082590347' IndexInText='413' ItemLength='18' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
							</Children>
							<OtherProperties>
								<CodeBlockStartMarker Name='{' Id='637840162082507308' IndexInText='246' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<CodeBlockEndMarker Name='}' Id='637840162082590495' IndexInText='434' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OtherProperties>
						</CodeBlock>
					</Postfixes>
				</Operand2.Literal>
			</BinaryOperator>
		</RegularItems>
		<Children>
			<PrefixUnaryOperator Name='return' Priority='2147483647' Id='637840162082506097' IndexInText='161' ItemLength='14' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
			<BinaryOperator Name=':' Priority='0' Id='637840162082590514' IndexInText='221' ItemLength='214' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
		</Children>
	</ExpressionItemSeries>
	<ParseErrorData ObjectId='73' HasCriticalErrors='False' Interface='UniversalExpressionParser.IParseErrorData'>
		<AllCodeItemErrors ObjectId='74' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICodeItemParseErrorData]'/>
	</ParseErrorData>
	<SortedCommentedOutCodeInfos ObjectId='75' Count='5' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICommentedOutCodeInfo]'>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='76' IsLineComment='True' IndexInText='0' ItemLength='96'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='77' IsLineComment='True' IndexInText='98' ItemLength='61'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='78' IsLineComment='True' IndexInText='180' ItemLength='39'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='79' IsLineComment='True' IndexInText='250' ItemLength='96'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='80' IsLineComment='True' IndexInText='349' ItemLength='61'/>
	</SortedCommentedOutCodeInfos>
	<MainExpressionItem Id='637840162082426606' IndexInText='161' ItemLength='274' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'/>
</UniversalExpressionParser.ExpressionItems.IRootExpressionItemSeries>
```
</details>

# Numeric Values

- To make the **Universal Expression Parser** parse an expression item to a numeric expression item of type **UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem** we should make sure that right instances of **UniversalExpressionParser.NumericTypeDescriptor** are included in list in property **IReadOnlyList<NumericTypeDescriptor> NumericTypeDescriptors { get; }** in **UniversalExpressionParser.IExpressionLanguageProvider**. 

<details> <summary>Click to expand the class UniversalExpressionParser.NumericTypeDescriptor.</summary>

```csharp
using System.Collections.Generic;
using JetBrains.Annotations;
using UniversalExpressionParser.ExpressionItems;

namespace UniversalExpressionParser
{
    /// <summary>
    /// Numeric type data.
    /// </summary>
    public class NumericTypeDescriptor
    {
        public NumericTypeDescriptor(long numberTypeId, [NotNull, ItemNotNull] IReadOnlyList<string> regularExpressions)
        {
            NumberTypeId = numberTypeId;
            RegularExpressions = regularExpressions;
        }

        /// <summary>
        /// Numeric value type, such as int, double, exponent format int, etc.
        /// </summary>
        public long NumberTypeId { get; }

        /// <summary>
        /// Regular expressions used to try to parse numbers of this format.
        /// Each regular expression should start with '^' character and cannot end with '$'.
        /// In additional, it is preferred that the regular expressions do not contain '|' character, so that
        /// the format of the parsed numeric value can be identified using <see cref="INumericValueExpressionItem.IndexOfSucceededRegularExpression"/>.
        /// Examples of valid regular expressions are: "^\d*[.]*\d*[E|e]{1,1}([+|-]?\d+)", "^\d*[.]\d+", "^\d+[.]\d*", "^[0-9]+", or "^\d+"
        /// </summary>
        [NotNull, ItemNotNull]
        public IReadOnlyList<string> RegularExpressions { get; }
    }
}
```
</details>

<details> <summary>Click to expand the class UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem.</summary>

```csharp
using JetBrains.Annotations;

namespace UniversalExpressionParser.ExpressionItems
{

    /// <summary>
    /// Expression item for numeric values.
    /// The value of the constant value will be in <see cref="INameExpressionItem.Name"/>. Examples of values parsed to this class
    /// instance are "123456789012345678901234567890123456789", "-2.3", "-2.3e+1003", "-2.3exp+1003", -2.3EXP+1003, "TRUE", "False", etc.
    /// </summary>
    public interface INumericValueExpressionItem : INamedComplexExpressionItem
    {
        /// <summary>
        /// An instance of <see cref="NumericTypeDescriptor"/> that succeeded in parsing the text to numeric value.
        /// </summary>
        [NotNull]
        NumericTypeDescriptor SucceededNumericTypeDescriptor { get; }

        /// <summary>
        /// Index or regular expression in <see cref="NumericTypeDescriptor.RegularExpressions"/> used to match the numeric value.
        /// </summary>
        int IndexOfSucceededRegularExpression { get; }
    }
}
```
</details>

- The parser scans the regular expressions in list in property **IReadOnlyList<string> RegularExpressions { get; }** in type **NumericTypeDescriptor** for each provided instance of **UniversalExpressionParser.NumericTypeDescriptor** to try to parse the expression to numeric expression item of type ****UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem****. 
   
- By default the abstract class **UniversalExpressionParser.ExpressionLanguageProviderBase** uses the following implementation of property **NumericTypeDescriptors**, which can be overridden to provide different format for numeric values:
 
   **NOTE** The regular expressions used in implementation of property **NumericTypeDescriptors** should always tart with '**^**' and should never end with '**$**'.

```csharp
public virtual IReadOnlyList<NumericTypeDescriptor> NumericTypeDescriptors { get; } = new List<NumericTypeDescriptor>
{   
    new NumericTypeDescriptor(KnownNumericTypeDescriptorIds.ExponentFormatValueId, 
    new[] { @"^(\d+\.\d+|\d+\.|\.\d+|\d+)(EXP|exp|E|e)[+|-]?(\d+\.\d+|\d+\.|\.\d+|\d+)"}),
    
    new NumericTypeDescriptor(KnownNumericTypeDescriptorIds.FloatingPointValueId, 
    new[] { @"^(\d+\.\d+|\d+\.|\.\d+)"}),    
    
    new NumericTypeDescriptor(KnownNumericTypeDescriptorIds.IntValuePointValueId, new[] { @"^\d+" })
}
```

- The first regular expression that matches the expression, is stored in properties **SucceededNumericTypeDescriptor** of type **UniversalExpressionParser.NumericTypeDescriptor** and **IndexOfSucceededRegularExpression** in parsed expression item type **UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem**.

- The numeric value is stored as text in property **INumericValueExpressionItem.NameExpressionItem.Name** of text type. Therefore, there is no limit on numeric value digits. 
  
- The expression evaluator that uses the **Universal Expression Parser** can use the data in these properties to convert the text in property **INumericValueExpressionItem.NameExpressionItem.Name** to a numeric type.

- Examples of numeric value expression items:

```csharp
// By default exponent notation can be used.
println(-0.5e-3+.2exp3.4+3.E2.7+2.1EXP.3);
println(.5e15*x);

// Numeric values can have no limitations on the number of digits. The value is stored as text in
// UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem.
// The text can be validated farther and converted to numeric values by the expression evaluator that
// uses the parser.
var x = 2.3*x+123456789123456789123456789123456789; 


```

<details> <summary>Click to expand the parsed expression</summary>

```XML
<?xml version="1.0" encoding="utf-8"?>
<UniversalExpressionParser.ExpressionItems.IRootExpressionItemSeries ObjectId='0' PositionInTextOnCompletion='465' IndexInText='46' ItemLength='414'>
	<ExpressionItemSeries Id='637840162081576208' IndexInText='46' ItemLength='414' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'>
		<RegularItems>
			<Braces Id='637840162081576636' IndexInText='46' ItemLength='41' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
				<RegularItems>
					<Literal Id='637840162081576589' IndexInText='46' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='println' Id='637840162081576581' IndexInText='46' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<OpeningRoundBrace Name='(' Id='637840162081576640' IndexInText='53' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<BinaryOperator Name='+' Priority='30' Id='637840162081745565' IndexInText='54' ItemLength='32' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.BinaryOperator Name='+' Priority='30' Id='637840162081745558' IndexInText='54' ItemLength='23' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
							<Operand1.BinaryOperator Name='+' Priority='30' Id='637840162081745548' IndexInText='54' ItemLength='16' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
								<Operand1.PrefixUnaryOperator Name='-' Priority='0' Id='637840162081745514' IndexInText='54' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
									<OperatorInfo OperatorType='PrefixUnaryOperator' Name='-' Priority='0' Id='637840162081576706' IndexInText='54' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
										<OperatorNameParts>
											<Name Name='-' Id='637840162081576695' IndexInText='54' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</OperatorNameParts>
									</OperatorInfo>
									<Operand1.ConstantNumericValue Id='637840162081632507' IndexInText='55' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
										<NameExpressionItem Name='0.5e-3' Id='637840162081632447' IndexInText='55' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										<OtherProperties>
											<SucceededNumericTypeDescriptor ObjectId='14' NumberTypeId='1581134225786' Interface='UniversalExpressionParser.NumericTypeDescriptor'>
												<RegularExpressions ObjectId='15' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[System.String]'>
													<System.String value='^(\d+\.\d+|\d+\.|\.\d+|\d+)(EXP|exp|E|e)[+|-]?(\d+\.\d+|\d+\.|\.\d+|\d+)' />
												</RegularExpressions>
											</SucceededNumericTypeDescriptor>
										</OtherProperties>
									</Operand1.ConstantNumericValue>
								</Operand1.PrefixUnaryOperator>
								<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637840162081632761' IndexInText='61' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
									<OperatorNameParts>
										<Name Name='+' Id='637840162081632751' IndexInText='61' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</OperatorNameParts>
								</OperatorInfo>
								<Operand2.Literal Id='637840162081632947' IndexInText='62' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
									<NameExpressionItem Name='.2exp3.4' Id='637840162081632942' IndexInText='62' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</Operand2.Literal>
							</Operand1.BinaryOperator>
							<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637840162081633115' IndexInText='70' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
								<OperatorNameParts>
									<Name Name='+' Id='637840162081633108' IndexInText='70' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</OperatorNameParts>
							</OperatorInfo>
							<Operand2.ConstantNumericValue Id='637840162081688112' IndexInText='71' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
								<NameExpressionItem Name='3.E2.7' Id='637840162081688067' IndexInText='71' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<OtherProperties>
									<SucceededNumericTypeDescriptor ObjectId='14' NumberTypeId='1581134225786' Interface='UniversalExpressionParser.NumericTypeDescriptor'/>
								</OtherProperties>
							</Operand2.ConstantNumericValue>
						</Operand1.BinaryOperator>
						<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637840162081688353' IndexInText='77' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<Name Name='+' Id='637840162081688342' IndexInText='77' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OperatorNameParts>
						</OperatorInfo>
						<Operand2.ConstantNumericValue Id='637840162081745315' IndexInText='78' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
							<NameExpressionItem Name='2.1EXP.3' Id='637840162081745259' IndexInText='78' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<OtherProperties>
								<SucceededNumericTypeDescriptor ObjectId='14' NumberTypeId='1581134225786' Interface='UniversalExpressionParser.NumericTypeDescriptor'/>
							</OtherProperties>
						</Operand2.ConstantNumericValue>
					</BinaryOperator>
					<ClosingRoundBrace Name=')' Id='637840162081745642' IndexInText='86' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Children>
					<BinaryOperator Name='+' Priority='30' Id='637840162081745565' IndexInText='54' ItemLength='32' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
				</Children>
				<OtherProperties>
					<NamedExpressionItem Id='637840162081576589' IndexInText='46' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					<OpeningBraceInfo Name='(' Id='637840162081576640' IndexInText='53' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ClosingBraceInfo Name=')' Id='637840162081745642' IndexInText='86' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Parameters ObjectId='29' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name='+' Priority='30' Id='637840162081745565' IndexInText='54' ItemLength='32' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
					</Parameters>
				</OtherProperties>
			</Braces>
			<ExpressionSeparator Name=';' Id='637840162081745693' IndexInText='87' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<Braces Id='637840162081745892' IndexInText='90' ItemLength='16' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
				<RegularItems>
					<Literal Id='637840162081745860' IndexInText='90' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='println' Id='637840162081745855' IndexInText='90' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<OpeningRoundBrace Name='(' Id='637840162081745895' IndexInText='97' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<BinaryOperator Name='*' Priority='20' Id='637840162081746235' IndexInText='98' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.Literal Id='637840162081745995' IndexInText='98' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='.5e15' Id='637840162081745991' IndexInText='98' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand1.Literal>
						<OperatorInfo OperatorType='BinaryOperator' Name='*' Priority='20' Id='637840162081746052' IndexInText='103' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<Name Name='*' Id='637840162081746044' IndexInText='103' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OperatorNameParts>
						</OperatorInfo>
						<Operand2.Literal Id='637840162081746196' IndexInText='104' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='x' Id='637840162081746191' IndexInText='104' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand2.Literal>
					</BinaryOperator>
					<ClosingRoundBrace Name=')' Id='637840162081746256' IndexInText='105' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Children>
					<BinaryOperator Name='*' Priority='20' Id='637840162081746235' IndexInText='98' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
				</Children>
				<OtherProperties>
					<NamedExpressionItem Id='637840162081745860' IndexInText='90' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					<OpeningBraceInfo Name='(' Id='637840162081745895' IndexInText='97' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ClosingBraceInfo Name=')' Id='637840162081746256' IndexInText='105' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Parameters ObjectId='43' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name='*' Priority='20' Id='637840162081746235' IndexInText='98' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
					</Parameters>
				</OtherProperties>
			</Braces>
			<ExpressionSeparator Name=';' Id='637840162081746284' IndexInText='106' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<BinaryOperator Name='=' Priority='2000' Id='637840162081908065' IndexInText='409' ItemLength='50' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
				<Operand1.Literal Id='637840162081746443' IndexInText='409' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
					<NameExpressionItem Name='x' Id='637840162081746439' IndexInText='413' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<AppliedKeywords>
						<Keyword Name='var' Id='637840162081746348' IndexInText='409' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
							<LanguageKeywordInfo ObjectId='49' Id='637781064051574641' Keyword='var' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
						</Keyword>
					</AppliedKeywords>
				</Operand1.Literal>
				<OperatorInfo OperatorType='BinaryOperator' Name='=' Priority='2000' Id='637840162081746517' IndexInText='415' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
					<OperatorNameParts>
						<Name Name='=' Id='637840162081746512' IndexInText='415' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</OperatorNameParts>
				</OperatorInfo>
				<Operand2.BinaryOperator Name='+' Priority='30' Id='637840162081908096' IndexInText='417' ItemLength='42' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
					<Operand1.BinaryOperator Name='*' Priority='20' Id='637840162081908088' IndexInText='417' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.ConstantNumericValue Id='637840162081823625' IndexInText='417' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
							<NameExpressionItem Name='2.3' Id='637840162081823575' IndexInText='417' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<OtherProperties>
								<SucceededNumericTypeDescriptor ObjectId='56' NumberTypeId='1581134136626' Interface='UniversalExpressionParser.NumericTypeDescriptor'>
									<RegularExpressions ObjectId='57' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[System.String]'>
										<System.String value='^(\d+\.\d+|\d+\.|\.\d+)' />
									</RegularExpressions>
								</SucceededNumericTypeDescriptor>
							</OtherProperties>
						</Operand1.ConstantNumericValue>
						<OperatorInfo OperatorType='BinaryOperator' Name='*' Priority='20' Id='637840162081823898' IndexInText='420' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<Name Name='*' Id='637840162081823876' IndexInText='420' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OperatorNameParts>
						</OperatorInfo>
						<Operand2.Literal Id='637840162081824103' IndexInText='421' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='x' Id='637840162081824097' IndexInText='421' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand2.Literal>
					</Operand1.BinaryOperator>
					<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637840162081824178' IndexInText='422' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
						<OperatorNameParts>
							<Name Name='+' Id='637840162081824173' IndexInText='422' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OperatorNameParts>
					</OperatorInfo>
					<Operand2.ConstantNumericValue Id='637840162081907881' IndexInText='423' ItemLength='36' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
						<NameExpressionItem Name='123456789123456789123456789123456789' Id='637840162081907835' IndexInText='423' ItemLength='36' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<OtherProperties>
							<SucceededNumericTypeDescriptor ObjectId='66' NumberTypeId='1581134183960' Interface='UniversalExpressionParser.NumericTypeDescriptor'>
								<RegularExpressions ObjectId='67' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[System.String]'>
									<System.String value='^\d+' />
								</RegularExpressions>
							</SucceededNumericTypeDescriptor>
						</OtherProperties>
					</Operand2.ConstantNumericValue>
				</Operand2.BinaryOperator>
			</BinaryOperator>
			<ExpressionSeparator Name=';' Id='637840162081908159' IndexInText='459' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
		</RegularItems>
		<Children>
			<Braces Id='637840162081576636' IndexInText='46' ItemLength='41' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
			<Braces Id='637840162081745892' IndexInText='90' ItemLength='16' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
			<BinaryOperator Name='=' Priority='2000' Id='637840162081908065' IndexInText='409' ItemLength='50' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
		</Children>
	</ExpressionItemSeries>
	<ParseErrorData ObjectId='69' HasCriticalErrors='False' Interface='UniversalExpressionParser.IParseErrorData'>
		<AllCodeItemErrors ObjectId='70' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICodeItemParseErrorData]'/>
	</ParseErrorData>
	<SortedCommentedOutCodeInfos ObjectId='71' Count='5' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICommentedOutCodeInfo]'>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='72' IsLineComment='True' IndexInText='0' ItemLength='44'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='73' IsLineComment='True' IndexInText='111' ItemLength='97'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='74' IsLineComment='True' IndexInText='210' ItemLength='73'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='75' IsLineComment='True' IndexInText='285' ItemLength='101'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='76' IsLineComment='True' IndexInText='388' ItemLength='19'/>
	</SortedCommentedOutCodeInfos>
	<MainExpressionItem Id='637840162081576208' IndexInText='46' ItemLength='414' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'/>
</UniversalExpressionParser.ExpressionItems.IRootExpressionItemSeries>
```
</details>

# Texts

The interface **UniversalExpressionParser.IExpressionLanguageProvider** has a property **IReadOnlyList&lt;char&gt; ConstantTextStartEndMarkerCharacters { get; }** that are used by **Universal Expression Parser** to parse expression items that start or end with some character in **ConstantTextStartEndMarkerCharacters** to parse the expression item to  **INamedComplexExpressionItem** with tne value of property **ItemType** equal to **UniversalExpressionParser.ExpressionItems.ConstantText**.

- The characters in property **ConstantTextStartEndMarkerCharacters** within the text itself if the character is typed twice (see the examples below).

- Texts can span multiple lines (see the example below).

```csharp
// Example of texts using text markers ',", and ` in IExpressionLanguageProvider.ConstantTextStartEndMarkerCharacters property.
// The text marker can be used in text as well when it is typed twice (e.g., '', "", ``, etc).

// Example of using all text markers together in operators
x = "Text 1 "" '' ``" + 
    ' Text2  "" '' ``' +
    ` Text3  "" '' ``` + x;

println("This is a text that spans
 multiple 
 lines.   
" + x + ' Some other text.');
```

<details> <summary>Click to expand the parsed expression</summary>

```XML
<?xml version="1.0" encoding="utf-8"?>
<UniversalExpressionParser.ExpressionItems.IRootExpressionItemSeries ObjectId='0' PositionInTextOnCompletion='459' IndexInText='287' ItemLength='172'>
	<ExpressionItemSeries Id='637840162086753145' IndexInText='287' ItemLength='172' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'>
		<RegularItems>
			<BinaryOperator Name='=' Priority='2000' Id='637840162086754398' IndexInText='287' ItemLength='78' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
				<Operand1.Literal Id='637840162086753544' IndexInText='287' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
					<NameExpressionItem Name='x' Id='637840162086753539' IndexInText='287' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</Operand1.Literal>
				<OperatorInfo OperatorType='BinaryOperator' Name='=' Priority='2000' Id='637840162086753638' IndexInText='289' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
					<OperatorNameParts>
						<Name Name='=' Id='637840162086753630' IndexInText='289' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</OperatorNameParts>
				</OperatorInfo>
				<Operand2.BinaryOperator Name='+' Priority='30' Id='637840162086754430' IndexInText='291' ItemLength='74' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
					<Operand1.BinaryOperator Name='+' Priority='30' Id='637840162086754424' IndexInText='291' ItemLength='70' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.BinaryOperator Name='+' Priority='30' Id='637840162086754415' IndexInText='291' ItemLength='44' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
							<Operand1.ConstantText Id='637840162086753791' IndexInText='291' ItemLength='17' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='"Text 1 "" ''' ``"' Id='637840162086753788' IndexInText='291' ItemLength='17' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Operand1.ConstantText>
							<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637840162086753866' IndexInText='309' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
								<OperatorNameParts>
									<Name Name='+' Id='637840162086753862' IndexInText='309' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</OperatorNameParts>
							</OperatorInfo>
							<Operand2.ConstantText Id='637840162086753993' IndexInText='317' ItemLength='18' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name=''' Text2  "" ''' ``''' Id='637840162086753991' IndexInText='317' ItemLength='18' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Operand2.ConstantText>
						</Operand1.BinaryOperator>
						<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637840162086754048' IndexInText='336' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<Name Name='+' Id='637840162086754045' IndexInText='336' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OperatorNameParts>
						</OperatorInfo>
						<Operand2.ConstantText Id='637840162086754178' IndexInText='343' ItemLength='18' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='` Text3  "" ''' ```' Id='637840162086754176' IndexInText='343' ItemLength='18' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand2.ConstantText>
					</Operand1.BinaryOperator>
					<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637840162086754235' IndexInText='362' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
						<OperatorNameParts>
							<Name Name='+' Id='637840162086754231' IndexInText='362' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OperatorNameParts>
					</OperatorInfo>
					<Operand2.Literal Id='637840162086754358' IndexInText='364' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='x' Id='637840162086754356' IndexInText='364' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Operand2.Literal>
				</Operand2.BinaryOperator>
			</BinaryOperator>
			<ExpressionSeparator Name=';' Id='637840162086754487' IndexInText='365' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<Braces Id='637840162086754600' IndexInText='370' ItemLength='88' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
				<RegularItems>
					<Literal Id='637840162086754571' IndexInText='370' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='println' Id='637840162086754568' IndexInText='370' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<OpeningRoundBrace Name='(' Id='637840162086754604' IndexInText='377' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<BinaryOperator Name='+' Priority='30' Id='637840162086755089' IndexInText='378' ItemLength='79' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.BinaryOperator Name='+' Priority='30' Id='637840162086755082' IndexInText='378' ItemLength='57' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
							<Operand1.ConstantText Id='637840162086754693' IndexInText='378' ItemLength='53' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='"This is a text that spans$line_break$ multiple $line_break$ lines.   $line_break$"' Id='637840162086754690' IndexInText='378' ItemLength='53' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Operand1.ConstantText>
							<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637840162086754746' IndexInText='432' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
								<OperatorNameParts>
									<Name Name='+' Id='637840162086754743' IndexInText='432' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</OperatorNameParts>
							</OperatorInfo>
							<Operand2.Literal Id='637840162086754870' IndexInText='434' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='x' Id='637840162086754867' IndexInText='434' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Operand2.Literal>
						</Operand1.BinaryOperator>
						<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637840162086754926' IndexInText='436' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<Name Name='+' Id='637840162086754919' IndexInText='436' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OperatorNameParts>
						</OperatorInfo>
						<Operand2.ConstantText Id='637840162086755050' IndexInText='438' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name=''' Some other text.''' Id='637840162086755047' IndexInText='438' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand2.ConstantText>
					</BinaryOperator>
					<ClosingRoundBrace Name=')' Id='637840162086755112' IndexInText='457' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Children>
					<BinaryOperator Name='+' Priority='30' Id='637840162086755089' IndexInText='378' ItemLength='79' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
				</Children>
				<OtherProperties>
					<NamedExpressionItem Id='637840162086754571' IndexInText='370' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					<OpeningBraceInfo Name='(' Id='637840162086754604' IndexInText='377' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ClosingBraceInfo Name=')' Id='637840162086755112' IndexInText='457' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Parameters ObjectId='42' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name='+' Priority='30' Id='637840162086755089' IndexInText='378' ItemLength='79' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
					</Parameters>
				</OtherProperties>
			</Braces>
			<ExpressionSeparator Name=';' Id='637840162086755142' IndexInText='458' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
		</RegularItems>
		<Children>
			<BinaryOperator Name='=' Priority='2000' Id='637840162086754398' IndexInText='287' ItemLength='78' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
			<Braces Id='637840162086754600' IndexInText='370' ItemLength='88' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
		</Children>
	</ExpressionItemSeries>
	<ParseErrorData ObjectId='44' HasCriticalErrors='False' Interface='UniversalExpressionParser.IParseErrorData'>
		<AllCodeItemErrors ObjectId='45' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICodeItemParseErrorData]'/>
	</ParseErrorData>
	<SortedCommentedOutCodeInfos ObjectId='46' Count='3' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICommentedOutCodeInfo]'>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='47' IsLineComment='True' IndexInText='0' ItemLength='127'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='48' IsLineComment='True' IndexInText='129' ItemLength='94'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='49' IsLineComment='True' IndexInText='227' ItemLength='58'/>
	</SortedCommentedOutCodeInfos>
	<MainExpressionItem Id='637840162086753145' IndexInText='287' ItemLength='172' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'/>
</UniversalExpressionParser.ExpressionItems.IRootExpressionItemSeries>
```
</details>

# Code Separators and Code Blocks

- If the value of property [**char ExpressionSeparatorCharacter { get; }**] in **UniversalExpressionParser.IExpressionLanguageProvider** is not equal to character '\0', multiple expressions can be used in a single expression. 

For example if the value of property **ExpressionSeparatorCharacter** is ";" the expression "var x=f1(y);println(x)" will be parsed to a list of two expression items for "x=f1(y)" and "println(x)". Otherwise, the parser will report an error for this expression (see section on **Error Reporting** for more details on errors). 

- If both values of properties [**char CodeBlockStartMarker { get; }**] and **string CodeBlockEndMarker { get; }** in **UniversalExpressionParser.IExpressionLanguageProvider** are not equal to character '\0', code block expressions can be used to combine multiple expressions into code block expression items of type **UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem**. 

- For example if the values of properties **CodeBlockStartMarker** and **CodeBlockEndMarker** are '{' and '}', the expression below will be parsed to two code block expressions of type **UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem**. Otherwise, the parser will report errors.

```csharp
{
    var x = y^2;
    println(x);
}

{
    fl(x1, x2);
    println(x) // No need for ';' after the last expression
}
```

<details> <summary>Click to expand the parsed expression</summary>

```XML
<?xml version="1.0" encoding="utf-8"?>
<UniversalExpressionParser.ExpressionItems.IRootExpressionItemSeries ObjectId='0' PositionInTextOnCompletion='125' IndexInText='0' ItemLength='122'>
	<ExpressionItemSeries Id='637840162079216062' IndexInText='0' ItemLength='125' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'>
		<RegularItems>
			<CodeBlock Id='637840162079216293' IndexInText='0' ItemLength='39' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
				<RegularItems>
					<CodeBlockStartMarker Name='{' Id='637840162079216285' IndexInText='0' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<BinaryOperator Name='=' Priority='2000' Id='637840162079298648' IndexInText='7' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.Literal Id='637840162079216441' IndexInText='7' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='x' Id='637840162079216436' IndexInText='11' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<AppliedKeywords>
								<Keyword Name='var' Id='637840162079216326' IndexInText='7' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
									<LanguageKeywordInfo ObjectId='8' Id='637781064051574641' Keyword='var' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
								</Keyword>
							</AppliedKeywords>
						</Operand1.Literal>
						<OperatorInfo OperatorType='BinaryOperator' Name='=' Priority='2000' Id='637840162079216557' IndexInText='13' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<Name Name='=' Id='637840162079216546' IndexInText='13' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OperatorNameParts>
						</OperatorInfo>
						<Operand2.BinaryOperator Name='^' Priority='10' Id='637840162079298672' IndexInText='15' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
							<Operand1.Literal Id='637840162079216695' IndexInText='15' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='y' Id='637840162079216691' IndexInText='15' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Operand1.Literal>
							<OperatorInfo OperatorType='BinaryOperator' Name='^' Priority='10' Id='637840162079216761' IndexInText='16' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
								<OperatorNameParts>
									<Name Name='^' Id='637840162079216756' IndexInText='16' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</OperatorNameParts>
							</OperatorInfo>
							<Operand2.ConstantNumericValue Id='637840162079298446' IndexInText='17' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
								<NameExpressionItem Name='2' Id='637840162079298401' IndexInText='17' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<OtherProperties>
									<SucceededNumericTypeDescriptor ObjectId='18' NumberTypeId='1581134183960' Interface='UniversalExpressionParser.NumericTypeDescriptor'>
										<RegularExpressions ObjectId='19' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[System.String]'>
											<System.String value='^\d+' />
										</RegularExpressions>
									</SucceededNumericTypeDescriptor>
								</OtherProperties>
							</Operand2.ConstantNumericValue>
						</Operand2.BinaryOperator>
					</BinaryOperator>
					<ExpressionSeparator Name=';' Id='637840162079298735' IndexInText='18' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Braces Id='637840162079298916' IndexInText='25' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<Literal Id='637840162079298883' IndexInText='25' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='println' Id='637840162079298879' IndexInText='25' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<OpeningRoundBrace Name='(' Id='637840162079298922' IndexInText='32' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Literal Id='637840162079299017' IndexInText='33' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='x' Id='637840162079299013' IndexInText='33' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<ClosingRoundBrace Name=')' Id='637840162079299052' IndexInText='34' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<Literal Id='637840162079299017' IndexInText='33' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
						</Children>
						<OtherProperties>
							<NamedExpressionItem Id='637840162079298883' IndexInText='25' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							<OpeningBraceInfo Name='(' Id='637840162079298922' IndexInText='32' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=')' Id='637840162079299052' IndexInText='34' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Parameters ObjectId='28' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162079299017' IndexInText='33' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Braces>
					<ExpressionSeparator Name=';' Id='637840162079299084' IndexInText='35' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<CodeBlockEndMarker Name='}' Id='637840162079299118' IndexInText='38' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Children>
					<BinaryOperator Name='=' Priority='2000' Id='637840162079298648' IndexInText='7' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
					<Braces Id='637840162079298916' IndexInText='25' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
				</Children>
				<OtherProperties>
					<CodeBlockStartMarker Name='{' Id='637840162079216285' IndexInText='0' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<CodeBlockEndMarker Name='}' Id='637840162079299118' IndexInText='38' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</OtherProperties>
			</CodeBlock>
			<CodeBlock Id='637840162079299161' IndexInText='43' ItemLength='82' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
				<RegularItems>
					<CodeBlockStartMarker Name='{' Id='637840162079299157' IndexInText='43' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Braces Id='637840162079299275' IndexInText='50' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<Literal Id='637840162079299245' IndexInText='50' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='fl' Id='637840162079299240' IndexInText='50' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<OpeningRoundBrace Name='(' Id='637840162079299278' IndexInText='52' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Literal Id='637840162079299356' IndexInText='53' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='x1' Id='637840162079299352' IndexInText='53' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<Comma Name=',' Id='637840162079299393' IndexInText='55' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Literal Id='637840162079299473' IndexInText='57' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='x2' Id='637840162079299468' IndexInText='57' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<ClosingRoundBrace Name=')' Id='637840162079299503' IndexInText='59' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<Literal Id='637840162079299356' IndexInText='53' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							<Literal Id='637840162079299473' IndexInText='57' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
						</Children>
						<OtherProperties>
							<NamedExpressionItem Id='637840162079299245' IndexInText='50' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							<OpeningBraceInfo Name='(' Id='637840162079299278' IndexInText='52' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=')' Id='637840162079299503' IndexInText='59' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Parameters ObjectId='43' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162079299356' IndexInText='53' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162079299473' IndexInText='57' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Braces>
					<ExpressionSeparator Name=';' Id='637840162079299530' IndexInText='60' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Braces Id='637840162079299639' IndexInText='67' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<Literal Id='637840162079299614' IndexInText='67' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='println' Id='637840162079299606' IndexInText='67' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<OpeningRoundBrace Name='(' Id='637840162079299642' IndexInText='74' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Literal Id='637840162079299723' IndexInText='75' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='x' Id='637840162079299715' IndexInText='75' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<ClosingRoundBrace Name=')' Id='637840162079299751' IndexInText='76' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<Literal Id='637840162079299723' IndexInText='75' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
						</Children>
						<OtherProperties>
							<NamedExpressionItem Id='637840162079299614' IndexInText='67' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							<OpeningBraceInfo Name='(' Id='637840162079299642' IndexInText='74' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=')' Id='637840162079299751' IndexInText='76' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Parameters ObjectId='52' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162079299723' IndexInText='75' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Braces>
					<CodeBlockEndMarker Name='}' Id='637840162079299797' IndexInText='124' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Children>
					<Braces Id='637840162079299275' IndexInText='50' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
					<Braces Id='637840162079299639' IndexInText='67' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
				</Children>
				<OtherProperties>
					<CodeBlockStartMarker Name='{' Id='637840162079299157' IndexInText='43' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<CodeBlockEndMarker Name='}' Id='637840162079299797' IndexInText='124' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</OtherProperties>
			</CodeBlock>
		</RegularItems>
		<Children>
			<CodeBlock Id='637840162079216293' IndexInText='0' ItemLength='39' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'/>
			<CodeBlock Id='637840162079299161' IndexInText='43' ItemLength='82' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'/>
		</Children>
	</ExpressionItemSeries>
	<ParseErrorData ObjectId='54' HasCriticalErrors='False' Interface='UniversalExpressionParser.IParseErrorData'>
		<AllCodeItemErrors ObjectId='55' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICodeItemParseErrorData]'/>
	</ParseErrorData>
	<SortedCommentedOutCodeInfos ObjectId='56' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICommentedOutCodeInfo]'>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='57' IsLineComment='True' IndexInText='78' ItemLength='44'/>
	</SortedCommentedOutCodeInfos>
	<MainExpressionItem Id='637840162079216062' IndexInText='0' ItemLength='125' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'/>
</UniversalExpressionParser.ExpressionItems.IRootExpressionItemSeries>
```
</details>

- More complex examples of code blocks and code separators:

```csharp
var y = x1 + 2.5 * x2;

f1()
{
    // Code block used as function body (see examples in Postfixes folder)
}

var z = e ^ 2.3;
{
    var x = 5 * y;
    println("x=" + x);

    {
        var y1 = 10 * x;
        println(getExp(y1));
    }

    {
        var y2 = 20 * x;
        println(getExp(y2));
    }

    getExp(x) : double
    {
        // another code block used as function body (see examples in Postfixes folder)
        return e^x;
    }
}

f2()
{
    // Another code block used as function body (see examples in Postfixes folder)
}

{
    // Another code block
}

public class Dog
{
    public Bark()
    {
        // Note, code separator ';' is not necessary, if the expression is followed by code block end marker '}'.
        println("bark")
    }
}
```

<details> <summary>Click to expand the parsed expression</summary>

```XML
<?xml version="1.0" encoding="utf-8"?>
<UniversalExpressionParser.ExpressionItems.IRootExpressionItemSeries ObjectId='0' PositionInTextOnCompletion='807' IndexInText='0' ItemLength='772'>
	<ExpressionItemSeries Id='637840162078639493' IndexInText='0' ItemLength='807' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'>
		<RegularItems>
			<BinaryOperator Name='=' Priority='2000' Id='637840162078764875' IndexInText='0' ItemLength='21' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
				<Operand1.Literal Id='637840162078640079' IndexInText='0' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
					<NameExpressionItem Name='y' Id='637840162078640068' IndexInText='4' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<AppliedKeywords>
						<Keyword Name='var' Id='637840162078639866' IndexInText='0' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
							<LanguageKeywordInfo ObjectId='6' Id='637781064051574641' Keyword='var' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
						</Keyword>
					</AppliedKeywords>
				</Operand1.Literal>
				<OperatorInfo OperatorType='BinaryOperator' Name='=' Priority='2000' Id='637840162078640219' IndexInText='6' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
					<OperatorNameParts>
						<Name Name='=' Id='637840162078640208' IndexInText='6' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</OperatorNameParts>
				</OperatorInfo>
				<Operand2.BinaryOperator Name='+' Priority='30' Id='637840162078764897' IndexInText='8' ItemLength='13' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
					<Operand1.Literal Id='637840162078640405' IndexInText='8' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='x1' Id='637840162078640400' IndexInText='8' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Operand1.Literal>
					<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637840162078640495' IndexInText='11' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
						<OperatorNameParts>
							<Name Name='+' Id='637840162078640489' IndexInText='11' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OperatorNameParts>
					</OperatorInfo>
					<Operand2.BinaryOperator Name='*' Priority='20' Id='637840162078764905' IndexInText='13' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.ConstantNumericValue Id='637840162078764050' IndexInText='13' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
							<NameExpressionItem Name='2.5' Id='637840162078763301' IndexInText='13' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<OtherProperties>
								<SucceededNumericTypeDescriptor ObjectId='17' NumberTypeId='1581134136626' Interface='UniversalExpressionParser.NumericTypeDescriptor'>
									<RegularExpressions ObjectId='18' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[System.String]'>
										<System.String value='^(\d+\.\d+|\d+\.|\.\d+)' />
									</RegularExpressions>
								</SucceededNumericTypeDescriptor>
							</OtherProperties>
						</Operand1.ConstantNumericValue>
						<OperatorInfo OperatorType='BinaryOperator' Name='*' Priority='20' Id='637840162078764620' IndexInText='17' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<Name Name='*' Id='637840162078764606' IndexInText='17' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OperatorNameParts>
						</OperatorInfo>
						<Operand2.Literal Id='637840162078764815' IndexInText='19' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='x2' Id='637840162078764810' IndexInText='19' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand2.Literal>
					</Operand2.BinaryOperator>
				</Operand2.BinaryOperator>
			</BinaryOperator>
			<ExpressionSeparator Name=';' Id='637840162078765007' IndexInText='21' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<Braces Id='637840162078765153' IndexInText='26' ItemLength='86' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
				<RegularItems>
					<Literal Id='637840162078765121' IndexInText='26' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='f1' Id='637840162078765116' IndexInText='26' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<OpeningRoundBrace Name='(' Id='637840162078765157' IndexInText='28' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ClosingRoundBrace Name=')' Id='637840162078765197' IndexInText='29' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Postfixes>
					<CodeBlock Id='637840162078765238' IndexInText='32' ItemLength='80' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
						<RegularItems>
							<CodeBlockStartMarker Name='{' Id='637840162078765230' IndexInText='32' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637840162078765358' IndexInText='111' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<OtherProperties>
							<CodeBlockStartMarker Name='{' Id='637840162078765230' IndexInText='32' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637840162078765358' IndexInText='111' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OtherProperties>
					</CodeBlock>
				</Postfixes>
				<OtherProperties>
					<NamedExpressionItem Id='637840162078765121' IndexInText='26' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					<OpeningBraceInfo Name='(' Id='637840162078765157' IndexInText='28' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ClosingBraceInfo Name=')' Id='637840162078765197' IndexInText='29' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Parameters ObjectId='32' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'/>
				</OtherProperties>
			</Braces>
			<BinaryOperator Name='=' Priority='2000' Id='637840162078842934' IndexInText='116' ItemLength='15' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
				<Operand1.Literal Id='637840162078765482' IndexInText='116' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
					<NameExpressionItem Name='z' Id='637840162078765477' IndexInText='120' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<AppliedKeywords>
						<Keyword Name='var' Id='637840162078765385' IndexInText='116' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
							<LanguageKeywordInfo ObjectId='6' Id='637781064051574641' Keyword='var' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
						</Keyword>
					</AppliedKeywords>
				</Operand1.Literal>
				<OperatorInfo OperatorType='BinaryOperator' Name='=' Priority='2000' Id='637840162078765558' IndexInText='122' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
					<OperatorNameParts>
						<Name Name='=' Id='637840162078765551' IndexInText='122' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</OperatorNameParts>
				</OperatorInfo>
				<Operand2.BinaryOperator Name='^' Priority='10' Id='637840162078842970' IndexInText='124' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
					<Operand1.Literal Id='637840162078765675' IndexInText='124' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='e' Id='637840162078765670' IndexInText='124' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Operand1.Literal>
					<OperatorInfo OperatorType='BinaryOperator' Name='^' Priority='10' Id='637840162078765732' IndexInText='126' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
						<OperatorNameParts>
							<Name Name='^' Id='637840162078765727' IndexInText='126' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OperatorNameParts>
					</OperatorInfo>
					<Operand2.ConstantNumericValue Id='637840162078842736' IndexInText='128' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
						<NameExpressionItem Name='2.3' Id='637840162078842686' IndexInText='128' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<OtherProperties>
							<SucceededNumericTypeDescriptor ObjectId='17' NumberTypeId='1581134136626' Interface='UniversalExpressionParser.NumericTypeDescriptor'/>
						</OtherProperties>
					</Operand2.ConstantNumericValue>
				</Operand2.BinaryOperator>
			</BinaryOperator>
			<ExpressionSeparator Name=';' Id='637840162078843037' IndexInText='131' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<CodeBlock Id='637840162078843090' IndexInText='134' ItemLength='341' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
				<RegularItems>
					<CodeBlockStartMarker Name='{' Id='637840162078843085' IndexInText='134' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<BinaryOperator Name='=' Priority='2000' Id='637840162078926824' IndexInText='141' ItemLength='13' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.Literal Id='637840162078843265' IndexInText='141' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='x' Id='637840162078843260' IndexInText='145' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<AppliedKeywords>
								<Keyword Name='var' Id='637840162078843119' IndexInText='141' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
									<LanguageKeywordInfo ObjectId='6' Id='637781064051574641' Keyword='var' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
								</Keyword>
							</AppliedKeywords>
						</Operand1.Literal>
						<OperatorInfo OperatorType='BinaryOperator' Name='=' Priority='2000' Id='637840162078843378' IndexInText='147' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<Name Name='=' Id='637840162078843368' IndexInText='147' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OperatorNameParts>
						</OperatorInfo>
						<Operand2.BinaryOperator Name='*' Priority='20' Id='637840162078926845' IndexInText='149' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
							<Operand1.ConstantNumericValue Id='637840162078926316' IndexInText='149' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
								<NameExpressionItem Name='5' Id='637840162078926273' IndexInText='149' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<OtherProperties>
									<SucceededNumericTypeDescriptor ObjectId='58' NumberTypeId='1581134183960' Interface='UniversalExpressionParser.NumericTypeDescriptor'>
										<RegularExpressions ObjectId='59' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[System.String]'>
											<System.String value='^\d+' />
										</RegularExpressions>
									</SucceededNumericTypeDescriptor>
								</OtherProperties>
							</Operand1.ConstantNumericValue>
							<OperatorInfo OperatorType='BinaryOperator' Name='*' Priority='20' Id='637840162078926564' IndexInText='151' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
								<OperatorNameParts>
									<Name Name='*' Id='637840162078926551' IndexInText='151' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</OperatorNameParts>
							</OperatorInfo>
							<Operand2.Literal Id='637840162078926762' IndexInText='153' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='y' Id='637840162078926756' IndexInText='153' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Operand2.Literal>
						</Operand2.BinaryOperator>
					</BinaryOperator>
					<ExpressionSeparator Name=';' Id='637840162078926904' IndexInText='154' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Braces Id='637840162078927027' IndexInText='161' ItemLength='17' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<Literal Id='637840162078926998' IndexInText='161' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='println' Id='637840162078926993' IndexInText='161' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<OpeningRoundBrace Name='(' Id='637840162078927031' IndexInText='168' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<BinaryOperator Name='+' Priority='30' Id='637840162078927402' IndexInText='169' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
								<Operand1.ConstantText Id='637840162078927167' IndexInText='169' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
									<NameExpressionItem Name='"x="' Id='637840162078927162' IndexInText='169' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</Operand1.ConstantText>
								<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637840162078927227' IndexInText='174' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
									<OperatorNameParts>
										<Name Name='+' Id='637840162078927220' IndexInText='174' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</OperatorNameParts>
								</OperatorInfo>
								<Operand2.Literal Id='637840162078927363' IndexInText='176' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
									<NameExpressionItem Name='x' Id='637840162078927358' IndexInText='176' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</Operand2.Literal>
							</BinaryOperator>
							<ClosingRoundBrace Name=')' Id='637840162078927428' IndexInText='177' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<BinaryOperator Name='+' Priority='30' Id='637840162078927402' IndexInText='169' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
						</Children>
						<OtherProperties>
							<NamedExpressionItem Id='637840162078926998' IndexInText='161' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							<OpeningBraceInfo Name='(' Id='637840162078927031' IndexInText='168' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=')' Id='637840162078927428' IndexInText='177' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Parameters ObjectId='77' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name='+' Priority='30' Id='637840162078927402' IndexInText='169' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Braces>
					<ExpressionSeparator Name=';' Id='637840162078927464' IndexInText='178' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<CodeBlock Id='637840162078927495' IndexInText='187' ItemLength='64' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
						<RegularItems>
							<CodeBlockStartMarker Name='{' Id='637840162078927491' IndexInText='187' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<BinaryOperator Name='=' Priority='2000' Id='637840162079009276' IndexInText='198' ItemLength='15' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
								<Operand1.Literal Id='637840162078927620' IndexInText='198' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
									<NameExpressionItem Name='y1' Id='637840162078927616' IndexInText='202' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<AppliedKeywords>
										<Keyword Name='var' Id='637840162078927524' IndexInText='198' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
											<LanguageKeywordInfo ObjectId='6' Id='637781064051574641' Keyword='var' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
										</Keyword>
									</AppliedKeywords>
								</Operand1.Literal>
								<OperatorInfo OperatorType='BinaryOperator' Name='=' Priority='2000' Id='637840162078927697' IndexInText='205' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
									<OperatorNameParts>
										<Name Name='=' Id='637840162078927691' IndexInText='205' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</OperatorNameParts>
								</OperatorInfo>
								<Operand2.BinaryOperator Name='*' Priority='20' Id='637840162079009301' IndexInText='207' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
									<Operand1.ConstantNumericValue Id='637840162079008771' IndexInText='207' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
										<NameExpressionItem Name='10' Id='637840162079008724' IndexInText='207' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										<OtherProperties>
											<SucceededNumericTypeDescriptor ObjectId='58' NumberTypeId='1581134183960' Interface='UniversalExpressionParser.NumericTypeDescriptor'/>
										</OtherProperties>
									</Operand1.ConstantNumericValue>
									<OperatorInfo OperatorType='BinaryOperator' Name='*' Priority='20' Id='637840162079009028' IndexInText='210' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
										<OperatorNameParts>
											<Name Name='*' Id='637840162079009015' IndexInText='210' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</OperatorNameParts>
									</OperatorInfo>
									<Operand2.Literal Id='637840162079009217' IndexInText='212' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='x' Id='637840162079009213' IndexInText='212' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Operand2.Literal>
								</Operand2.BinaryOperator>
							</BinaryOperator>
							<ExpressionSeparator Name=';' Id='637840162079009356' IndexInText='213' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Braces Id='637840162079009479' IndexInText='224' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
								<RegularItems>
									<Literal Id='637840162079009449' IndexInText='224' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='println' Id='637840162079009445' IndexInText='224' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<OpeningRoundBrace Name='(' Id='637840162079009484' IndexInText='231' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Braces Id='637840162079009602' IndexInText='232' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
										<RegularItems>
											<Literal Id='637840162079009576' IndexInText='232' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
												<NameExpressionItem Name='getExp' Id='637840162079009571' IndexInText='232' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											</Literal>
											<OpeningRoundBrace Name='(' Id='637840162079009605' IndexInText='238' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<Literal Id='637840162079009696' IndexInText='239' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
												<NameExpressionItem Name='y1' Id='637840162079009691' IndexInText='239' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											</Literal>
											<ClosingRoundBrace Name=')' Id='637840162079009727' IndexInText='241' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</RegularItems>
										<Children>
											<Literal Id='637840162079009696' IndexInText='239' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
										</Children>
										<OtherProperties>
											<NamedExpressionItem Id='637840162079009576' IndexInText='232' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
											<OpeningBraceInfo Name='(' Id='637840162079009605' IndexInText='238' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<ClosingBraceInfo Name=')' Id='637840162079009727' IndexInText='241' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<Parameters ObjectId='106' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
												<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162079009696' IndexInText='239' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
											</Parameters>
										</OtherProperties>
									</Braces>
									<ClosingRoundBrace Name=')' Id='637840162079009762' IndexInText='242' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Children>
									<Braces Id='637840162079009602' IndexInText='232' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
								</Children>
								<OtherProperties>
									<NamedExpressionItem Id='637840162079009449' IndexInText='224' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<OpeningBraceInfo Name='(' Id='637840162079009484' IndexInText='231' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ClosingBraceInfo Name=')' Id='637840162079009762' IndexInText='242' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Parameters ObjectId='108' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162079009602' IndexInText='232' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
									</Parameters>
								</OtherProperties>
							</Braces>
							<ExpressionSeparator Name=';' Id='637840162079009788' IndexInText='243' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637840162079009860' IndexInText='250' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<BinaryOperator Name='=' Priority='2000' Id='637840162079009276' IndexInText='198' ItemLength='15' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
							<Braces Id='637840162079009479' IndexInText='224' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
						</Children>
						<OtherProperties>
							<CodeBlockStartMarker Name='{' Id='637840162078927491' IndexInText='187' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637840162079009860' IndexInText='250' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OtherProperties>
					</CodeBlock>
					<CodeBlock Id='637840162079009939' IndexInText='259' ItemLength='64' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
						<RegularItems>
							<CodeBlockStartMarker Name='{' Id='637840162079009934' IndexInText='259' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<BinaryOperator Name='=' Priority='2000' Id='637840162079097835' IndexInText='270' ItemLength='15' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
								<Operand1.Literal Id='637840162079010112' IndexInText='270' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
									<NameExpressionItem Name='y2' Id='637840162079010107' IndexInText='274' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<AppliedKeywords>
										<Keyword Name='var' Id='637840162079010013' IndexInText='270' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
											<LanguageKeywordInfo ObjectId='6' Id='637781064051574641' Keyword='var' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
										</Keyword>
									</AppliedKeywords>
								</Operand1.Literal>
								<OperatorInfo OperatorType='BinaryOperator' Name='=' Priority='2000' Id='637840162079010186' IndexInText='277' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
									<OperatorNameParts>
										<Name Name='=' Id='637840162079010180' IndexInText='277' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</OperatorNameParts>
								</OperatorInfo>
								<Operand2.BinaryOperator Name='*' Priority='20' Id='637840162079097853' IndexInText='279' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
									<Operand1.ConstantNumericValue Id='637840162079097361' IndexInText='279' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
										<NameExpressionItem Name='20' Id='637840162079097315' IndexInText='279' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										<OtherProperties>
											<SucceededNumericTypeDescriptor ObjectId='58' NumberTypeId='1581134183960' Interface='UniversalExpressionParser.NumericTypeDescriptor'/>
										</OtherProperties>
									</Operand1.ConstantNumericValue>
									<OperatorInfo OperatorType='BinaryOperator' Name='*' Priority='20' Id='637840162079097592' IndexInText='282' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
										<OperatorNameParts>
											<Name Name='*' Id='637840162079097581' IndexInText='282' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</OperatorNameParts>
									</OperatorInfo>
									<Operand2.Literal Id='637840162079097778' IndexInText='284' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='x' Id='637840162079097774' IndexInText='284' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Operand2.Literal>
								</Operand2.BinaryOperator>
							</BinaryOperator>
							<ExpressionSeparator Name=';' Id='637840162079097908' IndexInText='285' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Braces Id='637840162079098066' IndexInText='296' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
								<RegularItems>
									<Literal Id='637840162079097999' IndexInText='296' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='println' Id='637840162079097991' IndexInText='296' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<OpeningRoundBrace Name='(' Id='637840162079098070' IndexInText='303' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Braces Id='637840162079098188' IndexInText='304' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
										<RegularItems>
											<Literal Id='637840162079098162' IndexInText='304' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
												<NameExpressionItem Name='getExp' Id='637840162079098153' IndexInText='304' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											</Literal>
											<OpeningRoundBrace Name='(' Id='637840162079098191' IndexInText='310' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<Literal Id='637840162079098300' IndexInText='311' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
												<NameExpressionItem Name='y2' Id='637840162079098291' IndexInText='311' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											</Literal>
											<ClosingRoundBrace Name=')' Id='637840162079098334' IndexInText='313' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</RegularItems>
										<Children>
											<Literal Id='637840162079098300' IndexInText='311' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
										</Children>
										<OtherProperties>
											<NamedExpressionItem Id='637840162079098162' IndexInText='304' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
											<OpeningBraceInfo Name='(' Id='637840162079098191' IndexInText='310' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<ClosingBraceInfo Name=')' Id='637840162079098334' IndexInText='313' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<Parameters ObjectId='138' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
												<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162079098300' IndexInText='311' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
											</Parameters>
										</OtherProperties>
									</Braces>
									<ClosingRoundBrace Name=')' Id='637840162079098367' IndexInText='314' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Children>
									<Braces Id='637840162079098188' IndexInText='304' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
								</Children>
								<OtherProperties>
									<NamedExpressionItem Id='637840162079097999' IndexInText='296' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<OpeningBraceInfo Name='(' Id='637840162079098070' IndexInText='303' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ClosingBraceInfo Name=')' Id='637840162079098367' IndexInText='314' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Parameters ObjectId='140' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162079098188' IndexInText='304' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
									</Parameters>
								</OtherProperties>
							</Braces>
							<ExpressionSeparator Name=';' Id='637840162079098392' IndexInText='315' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637840162079098419' IndexInText='322' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<BinaryOperator Name='=' Priority='2000' Id='637840162079097835' IndexInText='270' ItemLength='15' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
							<Braces Id='637840162079098066' IndexInText='296' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
						</Children>
						<OtherProperties>
							<CodeBlockStartMarker Name='{' Id='637840162079009934' IndexInText='259' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637840162079098419' IndexInText='322' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OtherProperties>
					</CodeBlock>
					<BinaryOperator Name=':' Priority='0' Id='637840162079099451' IndexInText='331' ItemLength='141' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.Braces Id='637840162079098544' IndexInText='331' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
							<RegularItems>
								<Literal Id='637840162079098519' IndexInText='331' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
									<NameExpressionItem Name='getExp' Id='637840162079098515' IndexInText='331' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</Literal>
								<OpeningRoundBrace Name='(' Id='637840162079098547' IndexInText='337' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<Literal Id='637840162079098627' IndexInText='338' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
									<NameExpressionItem Name='x' Id='637840162079098623' IndexInText='338' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</Literal>
								<ClosingRoundBrace Name=')' Id='637840162079098654' IndexInText='339' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</RegularItems>
							<Children>
								<Literal Id='637840162079098627' IndexInText='338' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							</Children>
							<OtherProperties>
								<NamedExpressionItem Id='637840162079098519' IndexInText='331' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								<OpeningBraceInfo Name='(' Id='637840162079098547' IndexInText='337' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<ClosingBraceInfo Name=')' Id='637840162079098654' IndexInText='339' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<Parameters ObjectId='151' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
									<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162079098627' IndexInText='338' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								</Parameters>
							</OtherProperties>
						</Operand1.Braces>
						<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637840162079098704' IndexInText='341' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<Name Name=':' Id='637840162079098698' IndexInText='341' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OperatorNameParts>
						</OperatorInfo>
						<Operand2.Literal Id='637840162079098846' IndexInText='343' ItemLength='129' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='double' Id='637840162079098842' IndexInText='343' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Postfixes>
								<CodeBlock Id='637840162079098882' IndexInText='355' ItemLength='117' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
									<RegularItems>
										<CodeBlockStartMarker Name='{' Id='637840162079098878' IndexInText='355' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										<PrefixUnaryOperator Name='return' Priority='2147483647' Id='637840162079099380' IndexInText='454' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
											<OperatorInfo OperatorType='PrefixUnaryOperator' Name='return' Priority='2147483647' Id='637840162079099028' IndexInText='454' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
												<OperatorNameParts>
													<Name Name='return' Id='637840162079099022' IndexInText='454' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												</OperatorNameParts>
											</OperatorInfo>
											<Operand1.BinaryOperator Name='^' Priority='10' Id='637840162079099391' IndexInText='461' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
												<Operand1.Literal Id='637840162079099133' IndexInText='461' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
													<NameExpressionItem Name='e' Id='637840162079099129' IndexInText='461' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												</Operand1.Literal>
												<OperatorInfo OperatorType='BinaryOperator' Name='^' Priority='10' Id='637840162079099195' IndexInText='462' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
													<OperatorNameParts>
														<Name Name='^' Id='637840162079099190' IndexInText='462' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
													</OperatorNameParts>
												</OperatorInfo>
												<Operand2.Literal Id='637840162079099343' IndexInText='463' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
													<NameExpressionItem Name='x' Id='637840162079099339' IndexInText='463' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												</Operand2.Literal>
											</Operand1.BinaryOperator>
										</PrefixUnaryOperator>
										<ExpressionSeparator Name=';' Id='637840162079099413' IndexInText='464' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										<CodeBlockEndMarker Name='}' Id='637840162079099440' IndexInText='471' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</RegularItems>
									<Children>
										<PrefixUnaryOperator Name='return' Priority='2147483647' Id='637840162079099380' IndexInText='454' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
									</Children>
									<OtherProperties>
										<CodeBlockStartMarker Name='{' Id='637840162079098878' IndexInText='355' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										<CodeBlockEndMarker Name='}' Id='637840162079099440' IndexInText='471' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</OtherProperties>
								</CodeBlock>
							</Postfixes>
						</Operand2.Literal>
					</BinaryOperator>
					<CodeBlockEndMarker Name='}' Id='637840162079099484' IndexInText='474' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Children>
					<BinaryOperator Name='=' Priority='2000' Id='637840162078926824' IndexInText='141' ItemLength='13' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
					<Braces Id='637840162078927027' IndexInText='161' ItemLength='17' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
					<CodeBlock Id='637840162078927495' IndexInText='187' ItemLength='64' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'/>
					<CodeBlock Id='637840162079009939' IndexInText='259' ItemLength='64' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'/>
					<BinaryOperator Name=':' Priority='0' Id='637840162079099451' IndexInText='331' ItemLength='141' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
				</Children>
				<OtherProperties>
					<CodeBlockStartMarker Name='{' Id='637840162078843085' IndexInText='134' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<CodeBlockEndMarker Name='}' Id='637840162079099484' IndexInText='474' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</OtherProperties>
			</CodeBlock>
			<Braces Id='637840162079099599' IndexInText='479' ItemLength='94' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
				<RegularItems>
					<Literal Id='637840162079099574' IndexInText='479' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='f2' Id='637840162079099569' IndexInText='479' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<OpeningRoundBrace Name='(' Id='637840162079099602' IndexInText='481' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ClosingRoundBrace Name=')' Id='637840162079099634' IndexInText='482' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Postfixes>
					<CodeBlock Id='637840162079099664' IndexInText='485' ItemLength='88' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
						<RegularItems>
							<CodeBlockStartMarker Name='{' Id='637840162079099660' IndexInText='485' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637840162079099695' IndexInText='572' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<OtherProperties>
							<CodeBlockStartMarker Name='{' Id='637840162079099660' IndexInText='485' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637840162079099695' IndexInText='572' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OtherProperties>
					</CodeBlock>
				</Postfixes>
				<OtherProperties>
					<NamedExpressionItem Id='637840162079099574' IndexInText='479' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					<OpeningBraceInfo Name='(' Id='637840162079099602' IndexInText='481' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ClosingBraceInfo Name=')' Id='637840162079099634' IndexInText='482' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Parameters ObjectId='179' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'/>
				</OtherProperties>
			</Braces>
			<CodeBlock Id='637840162079099725' IndexInText='577' ItemLength='31' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
				<RegularItems>
					<CodeBlockStartMarker Name='{' Id='637840162079099721' IndexInText='577' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<CodeBlockEndMarker Name='}' Id='637840162079099758' IndexInText='607' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<OtherProperties>
					<CodeBlockStartMarker Name='{' Id='637840162079099721' IndexInText='577' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<CodeBlockEndMarker Name='}' Id='637840162079099758' IndexInText='607' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</OtherProperties>
			</CodeBlock>
			<Literal Id='637840162079099877' IndexInText='612' ItemLength='195' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
				<NameExpressionItem Name='Dog' Id='637840162079099869' IndexInText='625' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				<AppliedKeywords>
					<Keyword Name='public' Id='637840162079099780' IndexInText='612' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
						<LanguageKeywordInfo ObjectId='186' Id='637783669980406419' Keyword='public' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
					</Keyword>
					<Keyword Name='class' Id='637840162079099794' IndexInText='619' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
						<LanguageKeywordInfo ObjectId='188' Id='637783670388745402' Keyword='class' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
					</Keyword>
				</AppliedKeywords>
				<Postfixes>
					<CodeBlock Id='637840162079099908' IndexInText='630' ItemLength='177' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
						<RegularItems>
							<CodeBlockStartMarker Name='{' Id='637840162079099905' IndexInText='630' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Braces Id='637840162079100075' IndexInText='637' ItemLength='167' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
								<AppliedKeywords>
									<Keyword Name='public' Id='637840162079099925' IndexInText='637' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
										<LanguageKeywordInfo ObjectId='186' Id='637783669980406419' Keyword='public' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
									</Keyword>
								</AppliedKeywords>
								<RegularItems>
									<Literal Id='637840162079100007' IndexInText='644' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='Bark' Id='637840162079100002' IndexInText='644' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<OpeningRoundBrace Name='(' Id='637840162079100079' IndexInText='648' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ClosingRoundBrace Name=')' Id='637840162079100107' IndexInText='649' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Postfixes>
									<CodeBlock Id='637840162079100140' IndexInText='656' ItemLength='148' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
										<RegularItems>
											<CodeBlockStartMarker Name='{' Id='637840162079100136' IndexInText='656' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<Braces Id='637840162079100260' IndexInText='782' ItemLength='15' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
												<RegularItems>
													<Literal Id='637840162079100231' IndexInText='782' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
														<NameExpressionItem Name='println' Id='637840162079100227' IndexInText='782' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
													</Literal>
													<OpeningRoundBrace Name='(' Id='637840162079100263' IndexInText='789' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
													<ConstantText Id='637840162079100352' IndexInText='790' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
														<NameExpressionItem Name='"bark"' Id='637840162079100347' IndexInText='790' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
													</ConstantText>
													<ClosingRoundBrace Name=')' Id='637840162079100385' IndexInText='796' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												</RegularItems>
												<Children>
													<ConstantText Id='637840162079100352' IndexInText='790' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
												</Children>
												<OtherProperties>
													<NamedExpressionItem Id='637840162079100231' IndexInText='782' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
													<OpeningBraceInfo Name='(' Id='637840162079100263' IndexInText='789' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
													<ClosingBraceInfo Name=')' Id='637840162079100385' IndexInText='796' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
													<Parameters ObjectId='206' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
														<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162079100352' IndexInText='790' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
													</Parameters>
												</OtherProperties>
											</Braces>
											<CodeBlockEndMarker Name='}' Id='637840162079100422' IndexInText='803' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</RegularItems>
										<Children>
											<Braces Id='637840162079100260' IndexInText='782' ItemLength='15' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
										</Children>
										<OtherProperties>
											<CodeBlockStartMarker Name='{' Id='637840162079100136' IndexInText='656' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<CodeBlockEndMarker Name='}' Id='637840162079100422' IndexInText='803' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</OtherProperties>
									</CodeBlock>
								</Postfixes>
								<OtherProperties>
									<NamedExpressionItem Id='637840162079100007' IndexInText='644' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<OpeningBraceInfo Name='(' Id='637840162079100079' IndexInText='648' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ClosingBraceInfo Name=')' Id='637840162079100107' IndexInText='649' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Parameters ObjectId='208' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'/>
								</OtherProperties>
							</Braces>
							<CodeBlockEndMarker Name='}' Id='637840162079100448' IndexInText='806' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<Braces Id='637840162079100075' IndexInText='637' ItemLength='167' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
						</Children>
						<OtherProperties>
							<CodeBlockStartMarker Name='{' Id='637840162079099905' IndexInText='630' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637840162079100448' IndexInText='806' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OtherProperties>
					</CodeBlock>
				</Postfixes>
			</Literal>
		</RegularItems>
		<Children>
			<BinaryOperator Name='=' Priority='2000' Id='637840162078764875' IndexInText='0' ItemLength='21' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
			<Braces Id='637840162078765153' IndexInText='26' ItemLength='86' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
			<BinaryOperator Name='=' Priority='2000' Id='637840162078842934' IndexInText='116' ItemLength='15' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
			<CodeBlock Id='637840162078843090' IndexInText='134' ItemLength='341' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'/>
			<Braces Id='637840162079099599' IndexInText='479' ItemLength='94' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
			<CodeBlock Id='637840162079099725' IndexInText='577' ItemLength='31' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'/>
			<Literal Id='637840162079099877' IndexInText='612' ItemLength='195' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
		</Children>
	</ExpressionItemSeries>
	<ParseErrorData ObjectId='210' HasCriticalErrors='False' Interface='UniversalExpressionParser.IParseErrorData'>
		<AllCodeItemErrors ObjectId='211' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICodeItemParseErrorData]'/>
	</ParseErrorData>
	<SortedCommentedOutCodeInfos ObjectId='212' Count='5' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICommentedOutCodeInfo]'>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='213' IsLineComment='True' IndexInText='39' ItemLength='70'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='214' IsLineComment='True' IndexInText='366' ItemLength='78'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='215' IsLineComment='True' IndexInText='492' ItemLength='78'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='216' IsLineComment='True' IndexInText='584' ItemLength='21'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='217' IsLineComment='True' IndexInText='667' ItemLength='105'/>
	</SortedCommentedOutCodeInfos>
	<MainExpressionItem Id='637840162078639493' IndexInText='0' ItemLength='807' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'/>
</UniversalExpressionParser.ExpressionItems.IRootExpressionItemSeries>
```
</details>

# Keywords

- Keywords are special names (e.g., **var**, **public**, **class**, **where**) that can be specified in property **IReadOnlyList&lt;ILanguageKeywordInfo&gt; Keywords { get; }** in **UniversalExpressionParser.IExpressionLanguageProvider** as shown in example below.

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

1) One or more keywords can be placed in front of any literal (e.g., variable name), round or square braces expression, function or matrix expression, a code block. In this case the parser parses the keywords and adds the list of parsed keyword expression items (i.e., list of **UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem** objects) to list in **IReadOnlyList&lt;IKeywordExpressionItem&gt; AppliedKeywords { get; }** in **UniversalExpressionParser.ExpressionItems.IComplexExpressionItem** for the expression item that follows the list of keywords.

2) Keywords are used by custom expression parsers to determine if the expression that follows the keywords should be parsed to a custom expression item.
   See section **Custom Expression Item Parsers** for more details on custom expression parsers.

- Examples of keywords: 

```csharp
// Keywords "public" and "class" will be added to list in property "AppliedKeywords" in class 
// "UniversalExpressionParser.ExpressionItems.Custom.IComplexExpressionItem" for the parsed expression "Dog".
public class Dog;

// Keywords "public" and "static will be added to list in property "AppliedKeywords" in class 
// "UniversalExpressionParser.ExpressionItems.Custom.IComplexExpressionItem" for the parsed expression "F1()".
public static F1();

// Keywords "public" and "static" will be added to list in property "AppliedKeywords" in class 
// "UniversalExpressionParser.ExpressionItems.Custom.IComplexExpressionItem" for the parsed expression "F1()".
public static F1() {return 1; }

// Keyword "::codeMarker" will be added to list in property "AppliedKeywords" in class 
// "UniversalExpressionParser.ExpressionItems.Custom.IComplexExpressionItem" for the parsed expression "(x1, x2)".
::codeMarker (x1, x2);

// Keyword "::codeMarker" will be added to list in property "AppliedKeywords" in class 
// "UniversalExpressionParser.ExpressionItems.Custom.IComplexExpressionItem" for the parsed expression "m1[2, x1]".
::codeMarker m1[2, x1];

// Keyword "::codeMarker" will be added to list in property "AppliedKeywords" in class 
// "UniversalExpressionParser.ExpressionItems.Custom.IComplexExpressionItem" for the parsed expression "[x1, x2]".
::codeMarker[x1, x2];

// Keyword "static" will be added to list in property "AppliedKeywords" in class 
// "UniversalExpressionParser.ExpressionItems.Custom.IComplexExpressionItem" for the code block parsed expression "{}".
static 
{
    var x;
}

// Keyword "::pragma" will be used by custom expression parser "UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.PragmaCustomExpressionItemParser" to 
// parse expressions "::pragma x2" and "::pragma x3" to custom expression items of type 
// "UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.PragmaCustomExpressionItem".
var y = x1 +::pragma x2+3*::pragma x3 +y;
```

<details> <summary>Click to expand the parsed expression</summary>

```XML
<?xml version="1.0" encoding="utf-8"?>
<UniversalExpressionParser.ExpressionItems.IRootExpressionItemSeries ObjectId='0' PositionInTextOnCompletion='2050' IndexInText='207' ItemLength='1843'>
	<ExpressionItemSeries Id='637840162081046145' IndexInText='207' ItemLength='1843' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'>
		<RegularItems>
			<Literal Id='637840162081046549' IndexInText='207' ItemLength='16' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
				<NameExpressionItem Name='Dog' Id='637840162081046545' IndexInText='220' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				<AppliedKeywords>
					<Keyword Name='public' Id='637840162081046399' IndexInText='207' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
						<LanguageKeywordInfo ObjectId='5' Id='637783669980406419' Keyword='public' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
					</Keyword>
					<Keyword Name='class' Id='637840162081046418' IndexInText='214' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
						<LanguageKeywordInfo ObjectId='7' Id='637783670388745402' Keyword='class' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
					</Keyword>
				</AppliedKeywords>
			</Literal>
			<ExpressionSeparator Name=';' Id='637840162081046615' IndexInText='223' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<Braces Id='637840162081046775' IndexInText='436' ItemLength='18' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
				<AppliedKeywords>
					<Keyword Name='public' Id='637840162081046649' IndexInText='436' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
						<LanguageKeywordInfo ObjectId='5' Id='637783669980406419' Keyword='public' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
					</Keyword>
					<Keyword Name='static' Id='637840162081046657' IndexInText='443' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
						<LanguageKeywordInfo ObjectId='12' Id='637789181872233115' Keyword='static' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
					</Keyword>
				</AppliedKeywords>
				<RegularItems>
					<Literal Id='637840162081046735' IndexInText='450' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='F1' Id='637840162081046731' IndexInText='450' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<OpeningRoundBrace Name='(' Id='637840162081046780' IndexInText='452' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ClosingRoundBrace Name=')' Id='637840162081046831' IndexInText='453' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<OtherProperties>
					<NamedExpressionItem Id='637840162081046735' IndexInText='450' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					<OpeningBraceInfo Name='(' Id='637840162081046780' IndexInText='452' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ClosingBraceInfo Name=')' Id='637840162081046831' IndexInText='453' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Parameters ObjectId='17' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'/>
				</OtherProperties>
			</Braces>
			<ExpressionSeparator Name=';' Id='637840162081046862' IndexInText='454' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<Braces Id='637840162081047005' IndexInText='668' ItemLength='31' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
				<AppliedKeywords>
					<Keyword Name='public' Id='637840162081046890' IndexInText='668' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
						<LanguageKeywordInfo ObjectId='5' Id='637783669980406419' Keyword='public' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
					</Keyword>
					<Keyword Name='static' Id='637840162081046897' IndexInText='675' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
						<LanguageKeywordInfo ObjectId='12' Id='637789181872233115' Keyword='static' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
					</Keyword>
				</AppliedKeywords>
				<RegularItems>
					<Literal Id='637840162081046977' IndexInText='682' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='F1' Id='637840162081046973' IndexInText='682' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<OpeningRoundBrace Name='(' Id='637840162081047008' IndexInText='684' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ClosingRoundBrace Name=')' Id='637840162081047039' IndexInText='685' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Postfixes>
					<CodeBlock Id='637840162081047070' IndexInText='687' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
						<RegularItems>
							<CodeBlockStartMarker Name='{' Id='637840162081047065' IndexInText='687' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<PrefixUnaryOperator Name='return' Priority='2147483647' Id='637840162081129643' IndexInText='688' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
								<OperatorInfo OperatorType='PrefixUnaryOperator' Name='return' Priority='2147483647' Id='637840162081047120' IndexInText='688' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
									<OperatorNameParts>
										<Name Name='return' Id='637840162081047111' IndexInText='688' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</OperatorNameParts>
								</OperatorInfo>
								<Operand1.ConstantNumericValue Id='637840162081129452' IndexInText='695' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
									<NameExpressionItem Name='1' Id='637840162081129409' IndexInText='695' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<OtherProperties>
										<SucceededNumericTypeDescriptor ObjectId='33' NumberTypeId='1581134183960' Interface='UniversalExpressionParser.NumericTypeDescriptor'>
											<RegularExpressions ObjectId='34' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[System.String]'>
												<System.String value='^\d+' />
											</RegularExpressions>
										</SucceededNumericTypeDescriptor>
									</OtherProperties>
								</Operand1.ConstantNumericValue>
							</PrefixUnaryOperator>
							<ExpressionSeparator Name=';' Id='637840162081129720' IndexInText='696' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637840162081129763' IndexInText='698' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<PrefixUnaryOperator Name='return' Priority='2147483647' Id='637840162081129643' IndexInText='688' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
						</Children>
						<OtherProperties>
							<CodeBlockStartMarker Name='{' Id='637840162081047065' IndexInText='687' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637840162081129763' IndexInText='698' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OtherProperties>
					</CodeBlock>
				</Postfixes>
				<OtherProperties>
					<NamedExpressionItem Id='637840162081046977' IndexInText='682' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					<OpeningBraceInfo Name='(' Id='637840162081047008' IndexInText='684' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ClosingBraceInfo Name=')' Id='637840162081047039' IndexInText='685' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Parameters ObjectId='37' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'/>
				</OtherProperties>
			</Braces>
			<Braces Id='637840162081129886' IndexInText='908' ItemLength='21' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
				<AppliedKeywords>
					<Keyword Name='::codeMarker' Id='637840162081129853' IndexInText='908' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
						<LanguageKeywordInfo ObjectId='40' Id='637783880924005707' Keyword='::codeMarker' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
					</Keyword>
				</AppliedKeywords>
				<RegularItems>
					<OpeningRoundBrace Name='(' Id='637840162081129892' IndexInText='921' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Literal Id='637840162081130032' IndexInText='922' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='x1' Id='637840162081130026' IndexInText='922' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<Comma Name=',' Id='637840162081130078' IndexInText='924' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Literal Id='637840162081130164' IndexInText='926' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='x2' Id='637840162081130159' IndexInText='926' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<ClosingRoundBrace Name=')' Id='637840162081130192' IndexInText='928' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Children>
					<Literal Id='637840162081130032' IndexInText='922' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					<Literal Id='637840162081130164' IndexInText='926' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
				</Children>
				<OtherProperties>
					<OpeningBraceInfo Name='(' Id='637840162081129892' IndexInText='921' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ClosingBraceInfo Name=')' Id='637840162081130192' IndexInText='928' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
					<Parameters ObjectId='48' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162081130032' IndexInText='922' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162081130164' IndexInText='926' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					</Parameters>
				</OtherProperties>
			</Braces>
			<ExpressionSeparator Name=';' Id='637840162081130224' IndexInText='929' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<Braces Id='637840162081130377' IndexInText='1140' ItemLength='22' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
				<AppliedKeywords>
					<Keyword Name='::codeMarker' Id='637840162081130259' IndexInText='1140' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
						<LanguageKeywordInfo ObjectId='40' Id='637783880924005707' Keyword='::codeMarker' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
					</Keyword>
				</AppliedKeywords>
				<RegularItems>
					<Literal Id='637840162081130339' IndexInText='1153' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='m1' Id='637840162081130335' IndexInText='1153' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<OpeningSquareBrace Name='[' Id='637840162081130381' IndexInText='1155' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ConstantNumericValue Id='637840162081209443' IndexInText='1156' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
						<NameExpressionItem Name='2' Id='637840162081209411' IndexInText='1156' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<OtherProperties>
							<SucceededNumericTypeDescriptor ObjectId='33' NumberTypeId='1581134183960' Interface='UniversalExpressionParser.NumericTypeDescriptor'/>
						</OtherProperties>
					</ConstantNumericValue>
					<Comma Name=',' Id='637840162081209562' IndexInText='1157' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Literal Id='637840162081209690' IndexInText='1159' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='x1' Id='637840162081209684' IndexInText='1159' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<ClosingSquareBrace Name=']' Id='637840162081209724' IndexInText='1161' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Children>
					<ConstantNumericValue Id='637840162081209443' IndexInText='1156' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'/>
					<Literal Id='637840162081209690' IndexInText='1159' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
				</Children>
				<OtherProperties>
					<NamedExpressionItem Id='637840162081130339' IndexInText='1153' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					<OpeningBraceInfo Name='[' Id='637840162081130381' IndexInText='1155' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ClosingBraceInfo Name=']' Id='637840162081209724' IndexInText='1161' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Parameters ObjectId='61' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162081209443' IndexInText='1156' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'/>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162081209690' IndexInText='1159' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					</Parameters>
				</OtherProperties>
			</Braces>
			<ExpressionSeparator Name=';' Id='637840162081209769' IndexInText='1162' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<Braces Id='637840162081209853' IndexInText='1372' ItemLength='20' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
				<AppliedKeywords>
					<Keyword Name='::codeMarker' Id='637840162081209820' IndexInText='1372' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
						<LanguageKeywordInfo ObjectId='40' Id='637783880924005707' Keyword='::codeMarker' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
					</Keyword>
				</AppliedKeywords>
				<RegularItems>
					<OpeningSquareBrace Name='[' Id='637840162081209858' IndexInText='1384' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Literal Id='637840162081209946' IndexInText='1385' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='x1' Id='637840162081209942' IndexInText='1385' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<Comma Name=',' Id='637840162081209974' IndexInText='1387' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Literal Id='637840162081210054' IndexInText='1389' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='x2' Id='637840162081210050' IndexInText='1389' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<ClosingSquareBrace Name=']' Id='637840162081210080' IndexInText='1391' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Children>
					<Literal Id='637840162081209946' IndexInText='1385' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					<Literal Id='637840162081210054' IndexInText='1389' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
				</Children>
				<OtherProperties>
					<OpeningBraceInfo Name='[' Id='637840162081209858' IndexInText='1384' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ClosingBraceInfo Name=']' Id='637840162081210080' IndexInText='1391' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
					<Parameters ObjectId='72' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162081209946' IndexInText='1385' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162081210054' IndexInText='1389' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					</Parameters>
				</OtherProperties>
			</Braces>
			<ExpressionSeparator Name=';' Id='637840162081210107' IndexInText='1392' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<CodeBlock Id='637840162081210171' IndexInText='1601' ItemLength='25' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
				<AppliedKeywords>
					<Keyword Name='static' Id='637840162081210139' IndexInText='1601' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
						<LanguageKeywordInfo ObjectId='12' Id='637789181872233115' Keyword='static' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
					</Keyword>
				</AppliedKeywords>
				<RegularItems>
					<CodeBlockStartMarker Name='{' Id='637840162081210167' IndexInText='1610' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Literal Id='637840162081210274' IndexInText='1617' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='x' Id='637840162081210269' IndexInText='1621' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<AppliedKeywords>
							<Keyword Name='var' Id='637840162081210192' IndexInText='1617' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
								<LanguageKeywordInfo ObjectId='80' Id='637781064051574641' Keyword='var' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
							</Keyword>
						</AppliedKeywords>
					</Literal>
					<ExpressionSeparator Name=';' Id='637840162081210301' IndexInText='1622' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<CodeBlockEndMarker Name='}' Id='637840162081210326' IndexInText='1625' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Children>
					<Literal Id='637840162081210274' IndexInText='1617' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
				</Children>
				<OtherProperties>
					<CodeBlockStartMarker Name='{' Id='637840162081210167' IndexInText='1610' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<CodeBlockEndMarker Name='}' Id='637840162081210326' IndexInText='1625' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</OtherProperties>
			</CodeBlock>
			<BinaryOperator Name='=' Priority='2000' Id='637840162081295621' IndexInText='2009' ItemLength='40' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
				<Operand1.Literal Id='637840162081210443' IndexInText='2009' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
					<NameExpressionItem Name='y' Id='637840162081210439' IndexInText='2013' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<AppliedKeywords>
						<Keyword Name='var' Id='637840162081210360' IndexInText='2009' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
							<LanguageKeywordInfo ObjectId='80' Id='637781064051574641' Keyword='var' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
						</Keyword>
					</AppliedKeywords>
				</Operand1.Literal>
				<OperatorInfo OperatorType='BinaryOperator' Name='=' Priority='2000' Id='637840162081210519' IndexInText='2015' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
					<OperatorNameParts>
						<Name Name='=' Id='637840162081210507' IndexInText='2015' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</OperatorNameParts>
				</OperatorInfo>
				<Operand2.BinaryOperator Name='+' Priority='30' Id='637840162081295672' IndexInText='2017' ItemLength='32' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
					<Operand1.BinaryOperator Name='+' Priority='30' Id='637840162081295658' IndexInText='2017' ItemLength='29' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.BinaryOperator Name='+' Priority='30' Id='637840162081295650' IndexInText='2017' ItemLength='15' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
							<Operand1.Literal Id='637840162081210648' IndexInText='2017' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='x1' Id='637840162081210640' IndexInText='2017' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Operand1.Literal>
							<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637840162081210719' IndexInText='2020' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
								<OperatorNameParts>
									<Name Name='+' Id='637840162081210714' IndexInText='2020' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</OperatorNameParts>
							</OperatorInfo>
							<Operand2.Custom Id='637840162081210847' IndexInText='2021' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.Custom.IKeywordBasedCustomExpressionItem'>
								<RegularItems>
									<Keyword Name='::pragma' Id='637840162081210770' IndexInText='2021' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
										<LanguageKeywordInfo ObjectId='98' Id='637781063127646487' Keyword='::pragma' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
									</Keyword>
									<Name Name='x2' Id='637840162081210840' IndexInText='2030' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<OtherProperties>
									<LastKeywordExpressionItem Name='::pragma' Id='637840162081210770' IndexInText='2021' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'/>
									<ErrorsPositionDisplayValue value='2021' type='System.Int32' />
								</OtherProperties>
							</Operand2.Custom>
						</Operand1.BinaryOperator>
						<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637840162081210920' IndexInText='2032' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<Name Name='+' Id='637840162081210915' IndexInText='2032' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OperatorNameParts>
						</OperatorInfo>
						<Operand2.BinaryOperator Name='*' Priority='20' Id='637840162081295666' IndexInText='2033' ItemLength='13' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
							<Operand1.ConstantNumericValue Id='637840162081294782' IndexInText='2033' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
								<NameExpressionItem Name='3' Id='637840162081294708' IndexInText='2033' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<OtherProperties>
									<SucceededNumericTypeDescriptor ObjectId='33' NumberTypeId='1581134183960' Interface='UniversalExpressionParser.NumericTypeDescriptor'/>
								</OtherProperties>
							</Operand1.ConstantNumericValue>
							<OperatorInfo OperatorType='BinaryOperator' Name='*' Priority='20' Id='637840162081295125' IndexInText='2034' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
								<OperatorNameParts>
									<Name Name='*' Id='637840162081295112' IndexInText='2034' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</OperatorNameParts>
							</OperatorInfo>
							<Operand2.Custom Id='637840162081295337' IndexInText='2035' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.Custom.IKeywordBasedCustomExpressionItem'>
								<RegularItems>
									<Keyword Name='::pragma' Id='637840162081295211' IndexInText='2035' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
										<LanguageKeywordInfo ObjectId='98' Id='637781063127646487' Keyword='::pragma' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
									</Keyword>
									<Name Name='x3' Id='637840162081295330' IndexInText='2044' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<OtherProperties>
									<LastKeywordExpressionItem Name='::pragma' Id='637840162081295211' IndexInText='2035' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'/>
									<ErrorsPositionDisplayValue value='2035' type='System.Int32' />
								</OtherProperties>
							</Operand2.Custom>
						</Operand2.BinaryOperator>
					</Operand1.BinaryOperator>
					<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637840162081295425' IndexInText='2047' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
						<OperatorNameParts>
							<Name Name='+' Id='637840162081295419' IndexInText='2047' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OperatorNameParts>
					</OperatorInfo>
					<Operand2.Literal Id='637840162081295559' IndexInText='2048' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='y' Id='637840162081295554' IndexInText='2048' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Operand2.Literal>
				</Operand2.BinaryOperator>
			</BinaryOperator>
			<ExpressionSeparator Name=';' Id='637840162081295750' IndexInText='2049' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
		</RegularItems>
		<Children>
			<Literal Id='637840162081046549' IndexInText='207' ItemLength='16' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
			<Braces Id='637840162081046775' IndexInText='436' ItemLength='18' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
			<Braces Id='637840162081047005' IndexInText='668' ItemLength='31' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
			<Braces Id='637840162081129886' IndexInText='908' ItemLength='21' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
			<Braces Id='637840162081130377' IndexInText='1140' ItemLength='22' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
			<Braces Id='637840162081209853' IndexInText='1372' ItemLength='20' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
			<CodeBlock Id='637840162081210171' IndexInText='1601' ItemLength='25' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'/>
			<BinaryOperator Name='=' Priority='2000' Id='637840162081295621' IndexInText='2009' ItemLength='40' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
		</Children>
	</ExpressionItemSeries>
	<ParseErrorData ObjectId='115' HasCriticalErrors='False' Interface='UniversalExpressionParser.IParseErrorData'>
		<AllCodeItemErrors ObjectId='116' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICodeItemParseErrorData]'/>
	</ParseErrorData>
	<SortedCommentedOutCodeInfos ObjectId='117' Count='17' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICommentedOutCodeInfo]'>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='118' IsLineComment='True' IndexInText='0' ItemLength='94'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='119' IsLineComment='True' IndexInText='96' ItemLength='109'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='120' IsLineComment='True' IndexInText='228' ItemLength='94'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='121' IsLineComment='True' IndexInText='324' ItemLength='110'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='122' IsLineComment='True' IndexInText='459' ItemLength='95'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='123' IsLineComment='True' IndexInText='556' ItemLength='110'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='124' IsLineComment='True' IndexInText='703' ItemLength='87'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='125' IsLineComment='True' IndexInText='792' ItemLength='114'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='126' IsLineComment='True' IndexInText='934' ItemLength='87'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='127' IsLineComment='True' IndexInText='1023' ItemLength='115'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='128' IsLineComment='True' IndexInText='1167' ItemLength='87'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='129' IsLineComment='True' IndexInText='1256' ItemLength='114'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='130' IsLineComment='True' IndexInText='1397' ItemLength='81'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='131' IsLineComment='True' IndexInText='1480' ItemLength='119'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='132' IsLineComment='True' IndexInText='1630' ItemLength='177'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='133' IsLineComment='True' IndexInText='1809' ItemLength='88'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='134' IsLineComment='True' IndexInText='1899' ItemLength='108'/>
	</SortedCommentedOutCodeInfos>
	<MainExpressionItem Id='637840162081046145' IndexInText='207' ItemLength='1843' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'/>
</UniversalExpressionParser.ExpressionItems.IRootExpressionItemSeries>
```
</details>

# Prefixes

Prefixes are one or more expression items that precede some other expression item, and are added to the list in property **Prefixes** in **UniversalExpressionParser.ExpressionItems.IComplexExpressionItem** for the expression item that follows the list of prefix expression items.

- **NOTE:** Prefixes are supported only if the value of property **SupportsPrefixes** in **UniversalExpressionParser.IExpressionLanguageProvider** is true.

Currently **Universal Expression Parser** supports two types of prefixes:

1) Square or round braces expressions items without names (i.e. expression items that are parsed to **UniversalExpressionParser.ExpressionItems.IBracesExpressionItem** with property **NamedExpressionItem** equal to **null**) that precede another expression item (e.g., braces expression, literal, code block, text expression item, numeric value expression item, etc).

- In the example below the braces expression items parsed from "[NotNull, ItemNotNull]" and "(Attribute("MarkedFunction"))" will be added as prefixes to expression item parsed from "F1(x, x2)".

```csharp
[NotNull, ItemNotNull](Attribute("MarkedFunction")) F1(x, x2)
{
    // This code block will be added to expression item parsed from F1(x:T1, y:T2, z: T3) as a postfix.
    retuens [x1, x2, x3];
}
```

<details> <summary>Click to expand the parsed expression</summary>

```XML
<?xml version="1.0" encoding="utf-8"?>
<UniversalExpressionParser.ExpressionItems.IRootExpressionItemSeries ObjectId='0' PositionInTextOnCompletion='199' IndexInText='0' ItemLength='169'>
	<ExpressionItemSeries Id='637840162084375482' IndexInText='0' ItemLength='199' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'>
		<RegularItems>
			<Braces Id='637840162084376673' IndexInText='0' ItemLength='199' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
				<Prefixes>
					<Braces Id='637840162084375776' IndexInText='0' ItemLength='22' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<OpeningSquareBrace Name='[' Id='637840162084375783' IndexInText='0' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Literal Id='637840162084375924' IndexInText='1' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='NotNull' Id='637840162084375919' IndexInText='1' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<Comma Name=',' Id='637840162084375983' IndexInText='8' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Literal Id='637840162084376076' IndexInText='10' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='ItemNotNull' Id='637840162084376072' IndexInText='10' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<ClosingSquareBrace Name=']' Id='637840162084376107' IndexInText='21' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<Literal Id='637840162084375924' IndexInText='1' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							<Literal Id='637840162084376076' IndexInText='10' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
						</Children>
						<OtherProperties>
							<OpeningBraceInfo Name='[' Id='637840162084375783' IndexInText='0' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=']' Id='637840162084376107' IndexInText='21' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
							<Parameters ObjectId='11' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162084375924' IndexInText='1' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162084376076' IndexInText='10' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Braces>
					<Braces Id='637840162084376148' IndexInText='22' ItemLength='29' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<OpeningRoundBrace Name='(' Id='637840162084376168' IndexInText='22' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Braces Id='637840162084376283' IndexInText='23' ItemLength='27' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
								<RegularItems>
									<Literal Id='637840162084376252' IndexInText='23' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='Attribute' Id='637840162084376248' IndexInText='23' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<OpeningRoundBrace Name='(' Id='637840162084376286' IndexInText='32' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ConstantText Id='637840162084376390' IndexInText='33' ItemLength='16' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='"MarkedFunction"' Id='637840162084376386' IndexInText='33' ItemLength='16' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</ConstantText>
									<ClosingRoundBrace Name=')' Id='637840162084376422' IndexInText='49' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Children>
									<ConstantText Id='637840162084376390' IndexInText='33' ItemLength='16' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								</Children>
								<OtherProperties>
									<NamedExpressionItem Id='637840162084376252' IndexInText='23' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<OpeningBraceInfo Name='(' Id='637840162084376286' IndexInText='32' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ClosingBraceInfo Name=')' Id='637840162084376422' IndexInText='49' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Parameters ObjectId='21' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162084376390' IndexInText='33' ItemLength='16' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</Parameters>
								</OtherProperties>
							</Braces>
							<ClosingRoundBrace Name=')' Id='637840162084376454' IndexInText='50' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<Braces Id='637840162084376283' IndexInText='23' ItemLength='27' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
						</Children>
						<OtherProperties>
							<OpeningBraceInfo Name='(' Id='637840162084376168' IndexInText='22' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=')' Id='637840162084376454' IndexInText='50' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
							<Parameters ObjectId='23' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162084376283' IndexInText='23' ItemLength='27' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Braces>
				</Prefixes>
				<RegularItems>
					<Literal Id='637840162084376632' IndexInText='52' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='F1' Id='637840162084376627' IndexInText='52' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<OpeningRoundBrace Name='(' Id='637840162084376677' IndexInText='54' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Literal Id='637840162084376758' IndexInText='55' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='x' Id='637840162084376754' IndexInText='55' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<Comma Name=',' Id='637840162084376790' IndexInText='56' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Literal Id='637840162084376868' IndexInText='58' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='x2' Id='637840162084376864' IndexInText='58' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<ClosingRoundBrace Name=')' Id='637840162084376898' IndexInText='60' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Postfixes>
					<CodeBlock Id='637840162084376931' IndexInText='63' ItemLength='136' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
						<RegularItems>
							<CodeBlockStartMarker Name='{' Id='637840162084376927' IndexInText='63' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Braces Id='637840162084377060' IndexInText='175' ItemLength='20' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
								<RegularItems>
									<Literal Id='637840162084377034' IndexInText='175' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='retuens' Id='637840162084377029' IndexInText='175' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<OpeningSquareBrace Name='[' Id='637840162084377063' IndexInText='183' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637840162084377145' IndexInText='184' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='x1' Id='637840162084377141' IndexInText='184' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<Comma Name=',' Id='637840162084377172' IndexInText='186' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637840162084377254' IndexInText='188' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='x2' Id='637840162084377246' IndexInText='188' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<Comma Name=',' Id='637840162084377280' IndexInText='190' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637840162084377359' IndexInText='192' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='x3' Id='637840162084377355' IndexInText='192' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<ClosingSquareBrace Name=']' Id='637840162084377389' IndexInText='194' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Children>
									<Literal Id='637840162084377145' IndexInText='184' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<Literal Id='637840162084377254' IndexInText='188' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<Literal Id='637840162084377359' IndexInText='192' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								</Children>
								<OtherProperties>
									<NamedExpressionItem Id='637840162084377034' IndexInText='175' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<OpeningBraceInfo Name='[' Id='637840162084377063' IndexInText='183' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ClosingBraceInfo Name=']' Id='637840162084377389' IndexInText='194' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Parameters ObjectId='48' Count='3' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162084377145' IndexInText='184' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162084377254' IndexInText='188' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162084377359' IndexInText='192' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</Parameters>
								</OtherProperties>
							</Braces>
							<ExpressionSeparator Name=';' Id='637840162084377419' IndexInText='195' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637840162084377445' IndexInText='198' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<Braces Id='637840162084377060' IndexInText='175' ItemLength='20' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
						</Children>
						<OtherProperties>
							<CodeBlockStartMarker Name='{' Id='637840162084376927' IndexInText='63' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637840162084377445' IndexInText='198' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OtherProperties>
					</CodeBlock>
				</Postfixes>
				<Children>
					<Literal Id='637840162084376758' IndexInText='55' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					<Literal Id='637840162084376868' IndexInText='58' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
				</Children>
				<OtherProperties>
					<NamedExpressionItem Id='637840162084376632' IndexInText='52' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					<OpeningBraceInfo Name='(' Id='637840162084376677' IndexInText='54' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ClosingBraceInfo Name=')' Id='637840162084376898' IndexInText='60' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Parameters ObjectId='51' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162084376758' IndexInText='55' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162084376868' IndexInText='58' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					</Parameters>
				</OtherProperties>
			</Braces>
		</RegularItems>
		<Children>
			<Braces Id='637840162084376673' IndexInText='0' ItemLength='199' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
		</Children>
	</ExpressionItemSeries>
	<ParseErrorData ObjectId='52' HasCriticalErrors='False' Interface='UniversalExpressionParser.IParseErrorData'>
		<AllCodeItemErrors ObjectId='53' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICodeItemParseErrorData]'/>
	</ParseErrorData>
	<SortedCommentedOutCodeInfos ObjectId='54' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICommentedOutCodeInfo]'>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='55' IsLineComment='True' IndexInText='70' ItemLength='99'/>
	</SortedCommentedOutCodeInfos>
	<MainExpressionItem Id='637840162084375482' IndexInText='0' ItemLength='199' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'/>
</UniversalExpressionParser.ExpressionItems.IRootExpressionItemSeries>
```
</details>

2) One or more expressions that are parsed to custom expression items of type **UniversalExpressionParser.ExpressionItems.Custom.ICustomExpressionItem** with property **CustomExpressionItemCategory** equal to **UniversalExpressionParser.ExpressionItems.Custom.CustomExpressionItemCategory.Prefix** that precede another expression item (e.g., braces expression, literal, code block, text expression item, numeric value expression item, etc). 

- In the example below the custom expression items of type **UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem** parsed from expressions "::types[T1,T2]" and "::types[T3]" will be added to braces expression item parsed from "F1(x:T1, y:T2, z: T3)".
NOTE: For more details on custom expression items see section **CustomExpressionItemParsers**.

```csharp
::types[T1,T2] ::types[T3] F1(x:T1, y:T2, z: T3)
{
    // This code block will be added to expression item parsed from F1(x:T1, y:T2, z: T3) as a postfix.
}
```

<details> <summary>Click to expand the parsed expression</summary>

```XML
<?xml version="1.0" encoding="utf-8"?>
<UniversalExpressionParser.ExpressionItems.IRootExpressionItemSeries ObjectId='0' PositionInTextOnCompletion='159' IndexInText='0' ItemLength='156'>
	<ExpressionItemSeries Id='637840162085442499' IndexInText='0' ItemLength='159' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'>
		<RegularItems>
			<Braces Id='637840162085443941' IndexInText='0' ItemLength='159' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
				<Prefixes>
					<Custom Id='637840162085443381' IndexInText='0' ItemLength='14' Interface='UniversalExpressionParser.ExpressionItems.Custom.IKeywordBasedCustomExpressionItem'>
						<RegularItems>
							<Keyword Name='::types' Id='637840162085442816' IndexInText='0' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
								<LanguageKeywordInfo ObjectId='5' Id='637781062811583137' Keyword='::types' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
							</Keyword>
							<Braces Id='637840162085442927' IndexInText='7' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
								<RegularItems>
									<OpeningSquareBrace Name='[' Id='637840162085442937' IndexInText='7' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637840162085443134' IndexInText='8' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='T1' Id='637840162085443129' IndexInText='8' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<Comma Name=',' Id='637840162085443250' IndexInText='10' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637840162085443333' IndexInText='11' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='T2' Id='637840162085443330' IndexInText='11' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<ClosingSquareBrace Name=']' Id='637840162085443367' IndexInText='13' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Children>
									<Literal Id='637840162085443134' IndexInText='8' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<Literal Id='637840162085443333' IndexInText='11' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								</Children>
								<OtherProperties>
									<OpeningBraceInfo Name='[' Id='637840162085442937' IndexInText='7' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ClosingBraceInfo Name=']' Id='637840162085443367' IndexInText='13' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
									<Parameters ObjectId='14' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162085443134' IndexInText='8' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162085443333' IndexInText='11' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</Parameters>
								</OtherProperties>
							</Braces>
						</RegularItems>
						<OtherProperties>
							<LastKeywordExpressionItem Name='::types' Id='637840162085442816' IndexInText='0' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'/>
							<ErrorsPositionDisplayValue value='0' type='System.Int32' />
						</OtherProperties>
					</Custom>
					<Custom Id='637840162085443625' IndexInText='15' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.Custom.IKeywordBasedCustomExpressionItem'>
						<RegularItems>
							<Keyword Name='::types' Id='637840162085443423' IndexInText='15' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
								<LanguageKeywordInfo ObjectId='5' Id='637781062811583137' Keyword='::types' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
							</Keyword>
							<Braces Id='637840162085443460' IndexInText='22' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
								<RegularItems>
									<OpeningSquareBrace Name='[' Id='637840162085443462' IndexInText='22' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637840162085443591' IndexInText='23' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='T3' Id='637840162085443589' IndexInText='23' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<ClosingSquareBrace Name=']' Id='637840162085443620' IndexInText='25' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Children>
									<Literal Id='637840162085443591' IndexInText='23' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								</Children>
								<OtherProperties>
									<OpeningBraceInfo Name='[' Id='637840162085443462' IndexInText='22' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ClosingBraceInfo Name=']' Id='637840162085443620' IndexInText='25' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
									<Parameters ObjectId='22' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162085443591' IndexInText='23' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</Parameters>
								</OtherProperties>
							</Braces>
						</RegularItems>
						<OtherProperties>
							<LastKeywordExpressionItem Name='::types' Id='637840162085443423' IndexInText='15' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'/>
							<ErrorsPositionDisplayValue value='15' type='System.Int32' />
						</OtherProperties>
					</Custom>
				</Prefixes>
				<RegularItems>
					<Literal Id='637840162085443905' IndexInText='27' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='F1' Id='637840162085443896' IndexInText='27' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<OpeningRoundBrace Name='(' Id='637840162085443945' IndexInText='29' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<BinaryOperator Name=':' Priority='0' Id='637840162085444344' IndexInText='30' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.Literal Id='637840162085444033' IndexInText='30' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='x' Id='637840162085444030' IndexInText='30' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand1.Literal>
						<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637840162085444099' IndexInText='31' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<Name Name=':' Id='637840162085444074' IndexInText='31' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OperatorNameParts>
						</OperatorInfo>
						<Operand2.Literal Id='637840162085444290' IndexInText='32' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='T1' Id='637840162085444286' IndexInText='32' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand2.Literal>
					</BinaryOperator>
					<Comma Name=',' Id='637840162085444394' IndexInText='34' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<BinaryOperator Name=':' Priority='0' Id='637840162085444687' IndexInText='36' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.Literal Id='637840162085444478' IndexInText='36' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='y' Id='637840162085444475' IndexInText='36' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand1.Literal>
						<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637840162085444521' IndexInText='37' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<Name Name=':' Id='637840162085444517' IndexInText='37' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OperatorNameParts>
						</OperatorInfo>
						<Operand2.Literal Id='637840162085444654' IndexInText='38' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='T2' Id='637840162085444652' IndexInText='38' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand2.Literal>
					</BinaryOperator>
					<Comma Name=',' Id='637840162085444701' IndexInText='40' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<BinaryOperator Name=':' Priority='0' Id='637840162085444997' IndexInText='42' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.Literal Id='637840162085444779' IndexInText='42' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='z' Id='637840162085444776' IndexInText='42' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand1.Literal>
						<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637840162085444824' IndexInText='43' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<Name Name=':' Id='637840162085444820' IndexInText='43' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OperatorNameParts>
						</OperatorInfo>
						<Operand2.Literal Id='637840162085444970' IndexInText='45' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='T3' Id='637840162085444967' IndexInText='45' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand2.Literal>
					</BinaryOperator>
					<ClosingRoundBrace Name=')' Id='637840162085445010' IndexInText='47' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Postfixes>
					<CodeBlock Id='637840162085445046' IndexInText='50' ItemLength='109' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
						<RegularItems>
							<CodeBlockStartMarker Name='{' Id='637840162085445043' IndexInText='50' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637840162085445088' IndexInText='158' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<OtherProperties>
							<CodeBlockStartMarker Name='{' Id='637840162085445043' IndexInText='50' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637840162085445088' IndexInText='158' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OtherProperties>
					</CodeBlock>
				</Postfixes>
				<Children>
					<BinaryOperator Name=':' Priority='0' Id='637840162085444344' IndexInText='30' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
					<BinaryOperator Name=':' Priority='0' Id='637840162085444687' IndexInText='36' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
					<BinaryOperator Name=':' Priority='0' Id='637840162085444997' IndexInText='42' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
				</Children>
				<OtherProperties>
					<NamedExpressionItem Id='637840162085443905' IndexInText='27' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					<OpeningBraceInfo Name='(' Id='637840162085443945' IndexInText='29' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ClosingBraceInfo Name=')' Id='637840162085445010' IndexInText='47' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Parameters ObjectId='53' Count='3' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name=':' Priority='0' Id='637840162085444344' IndexInText='30' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name=':' Priority='0' Id='637840162085444687' IndexInText='36' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name=':' Priority='0' Id='637840162085444997' IndexInText='42' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
					</Parameters>
				</OtherProperties>
			</Braces>
		</RegularItems>
		<Children>
			<Braces Id='637840162085443941' IndexInText='0' ItemLength='159' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
		</Children>
	</ExpressionItemSeries>
	<ParseErrorData ObjectId='54' HasCriticalErrors='False' Interface='UniversalExpressionParser.IParseErrorData'>
		<AllCodeItemErrors ObjectId='55' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICodeItemParseErrorData]'/>
	</ParseErrorData>
	<SortedCommentedOutCodeInfos ObjectId='56' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICommentedOutCodeInfo]'>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='57' IsLineComment='True' IndexInText='57' ItemLength='99'/>
	</SortedCommentedOutCodeInfos>
	<MainExpressionItem Id='637840162085442499' IndexInText='0' ItemLength='159' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'/>
</UniversalExpressionParser.ExpressionItems.IRootExpressionItemSeries>
```
</details>

- **NOTE:** The list of prefixes can include both types of prefixes at the same time (i.e., braces and custom expression items).

- Here is a more complex example of prefixes

```csharp
::types[T1,T2] ::types[T3] F1(x:T1, y:T2, z: T3)
{
    // This code block will be added to expression item parsed from F1(x:T1, y:T2, z: T3) as a postfix.
}
```

<details> <summary>Click to expand the parsed expression</summary>

```XML
<?xml version="1.0" encoding="utf-8"?>
<UniversalExpressionParser.ExpressionItems.IRootExpressionItemSeries ObjectId='0' PositionInTextOnCompletion='159' IndexInText='0' ItemLength='156'>
	<ExpressionItemSeries Id='637840162085442499' IndexInText='0' ItemLength='159' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'>
		<RegularItems>
			<Braces Id='637840162085443941' IndexInText='0' ItemLength='159' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
				<Prefixes>
					<Custom Id='637840162085443381' IndexInText='0' ItemLength='14' Interface='UniversalExpressionParser.ExpressionItems.Custom.IKeywordBasedCustomExpressionItem'>
						<RegularItems>
							<Keyword Name='::types' Id='637840162085442816' IndexInText='0' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
								<LanguageKeywordInfo ObjectId='5' Id='637781062811583137' Keyword='::types' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
							</Keyword>
							<Braces Id='637840162085442927' IndexInText='7' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
								<RegularItems>
									<OpeningSquareBrace Name='[' Id='637840162085442937' IndexInText='7' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637840162085443134' IndexInText='8' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='T1' Id='637840162085443129' IndexInText='8' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<Comma Name=',' Id='637840162085443250' IndexInText='10' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637840162085443333' IndexInText='11' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='T2' Id='637840162085443330' IndexInText='11' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<ClosingSquareBrace Name=']' Id='637840162085443367' IndexInText='13' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Children>
									<Literal Id='637840162085443134' IndexInText='8' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<Literal Id='637840162085443333' IndexInText='11' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								</Children>
								<OtherProperties>
									<OpeningBraceInfo Name='[' Id='637840162085442937' IndexInText='7' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ClosingBraceInfo Name=']' Id='637840162085443367' IndexInText='13' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
									<Parameters ObjectId='14' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162085443134' IndexInText='8' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162085443333' IndexInText='11' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</Parameters>
								</OtherProperties>
							</Braces>
						</RegularItems>
						<OtherProperties>
							<LastKeywordExpressionItem Name='::types' Id='637840162085442816' IndexInText='0' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'/>
							<ErrorsPositionDisplayValue value='0' type='System.Int32' />
						</OtherProperties>
					</Custom>
					<Custom Id='637840162085443625' IndexInText='15' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.Custom.IKeywordBasedCustomExpressionItem'>
						<RegularItems>
							<Keyword Name='::types' Id='637840162085443423' IndexInText='15' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
								<LanguageKeywordInfo ObjectId='5' Id='637781062811583137' Keyword='::types' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
							</Keyword>
							<Braces Id='637840162085443460' IndexInText='22' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
								<RegularItems>
									<OpeningSquareBrace Name='[' Id='637840162085443462' IndexInText='22' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637840162085443591' IndexInText='23' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='T3' Id='637840162085443589' IndexInText='23' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<ClosingSquareBrace Name=']' Id='637840162085443620' IndexInText='25' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Children>
									<Literal Id='637840162085443591' IndexInText='23' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								</Children>
								<OtherProperties>
									<OpeningBraceInfo Name='[' Id='637840162085443462' IndexInText='22' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ClosingBraceInfo Name=']' Id='637840162085443620' IndexInText='25' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
									<Parameters ObjectId='22' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162085443591' IndexInText='23' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</Parameters>
								</OtherProperties>
							</Braces>
						</RegularItems>
						<OtherProperties>
							<LastKeywordExpressionItem Name='::types' Id='637840162085443423' IndexInText='15' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'/>
							<ErrorsPositionDisplayValue value='15' type='System.Int32' />
						</OtherProperties>
					</Custom>
				</Prefixes>
				<RegularItems>
					<Literal Id='637840162085443905' IndexInText='27' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='F1' Id='637840162085443896' IndexInText='27' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<OpeningRoundBrace Name='(' Id='637840162085443945' IndexInText='29' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<BinaryOperator Name=':' Priority='0' Id='637840162085444344' IndexInText='30' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.Literal Id='637840162085444033' IndexInText='30' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='x' Id='637840162085444030' IndexInText='30' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand1.Literal>
						<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637840162085444099' IndexInText='31' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<Name Name=':' Id='637840162085444074' IndexInText='31' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OperatorNameParts>
						</OperatorInfo>
						<Operand2.Literal Id='637840162085444290' IndexInText='32' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='T1' Id='637840162085444286' IndexInText='32' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand2.Literal>
					</BinaryOperator>
					<Comma Name=',' Id='637840162085444394' IndexInText='34' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<BinaryOperator Name=':' Priority='0' Id='637840162085444687' IndexInText='36' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.Literal Id='637840162085444478' IndexInText='36' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='y' Id='637840162085444475' IndexInText='36' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand1.Literal>
						<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637840162085444521' IndexInText='37' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<Name Name=':' Id='637840162085444517' IndexInText='37' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OperatorNameParts>
						</OperatorInfo>
						<Operand2.Literal Id='637840162085444654' IndexInText='38' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='T2' Id='637840162085444652' IndexInText='38' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand2.Literal>
					</BinaryOperator>
					<Comma Name=',' Id='637840162085444701' IndexInText='40' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<BinaryOperator Name=':' Priority='0' Id='637840162085444997' IndexInText='42' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.Literal Id='637840162085444779' IndexInText='42' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='z' Id='637840162085444776' IndexInText='42' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand1.Literal>
						<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637840162085444824' IndexInText='43' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<Name Name=':' Id='637840162085444820' IndexInText='43' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OperatorNameParts>
						</OperatorInfo>
						<Operand2.Literal Id='637840162085444970' IndexInText='45' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='T3' Id='637840162085444967' IndexInText='45' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand2.Literal>
					</BinaryOperator>
					<ClosingRoundBrace Name=')' Id='637840162085445010' IndexInText='47' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Postfixes>
					<CodeBlock Id='637840162085445046' IndexInText='50' ItemLength='109' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
						<RegularItems>
							<CodeBlockStartMarker Name='{' Id='637840162085445043' IndexInText='50' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637840162085445088' IndexInText='158' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<OtherProperties>
							<CodeBlockStartMarker Name='{' Id='637840162085445043' IndexInText='50' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637840162085445088' IndexInText='158' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OtherProperties>
					</CodeBlock>
				</Postfixes>
				<Children>
					<BinaryOperator Name=':' Priority='0' Id='637840162085444344' IndexInText='30' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
					<BinaryOperator Name=':' Priority='0' Id='637840162085444687' IndexInText='36' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
					<BinaryOperator Name=':' Priority='0' Id='637840162085444997' IndexInText='42' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
				</Children>
				<OtherProperties>
					<NamedExpressionItem Id='637840162085443905' IndexInText='27' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					<OpeningBraceInfo Name='(' Id='637840162085443945' IndexInText='29' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ClosingBraceInfo Name=')' Id='637840162085445010' IndexInText='47' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Parameters ObjectId='53' Count='3' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name=':' Priority='0' Id='637840162085444344' IndexInText='30' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name=':' Priority='0' Id='637840162085444687' IndexInText='36' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name=':' Priority='0' Id='637840162085444997' IndexInText='42' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
					</Parameters>
				</OtherProperties>
			</Braces>
		</RegularItems>
		<Children>
			<Braces Id='637840162085443941' IndexInText='0' ItemLength='159' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
		</Children>
	</ExpressionItemSeries>
	<ParseErrorData ObjectId='54' HasCriticalErrors='False' Interface='UniversalExpressionParser.IParseErrorData'>
		<AllCodeItemErrors ObjectId='55' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICodeItemParseErrorData]'/>
	</ParseErrorData>
	<SortedCommentedOutCodeInfos ObjectId='56' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICommentedOutCodeInfo]'>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='57' IsLineComment='True' IndexInText='57' ItemLength='99'/>
	</SortedCommentedOutCodeInfos>
	<MainExpressionItem Id='637840162085442499' IndexInText='0' ItemLength='159' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'/>
</UniversalExpressionParser.ExpressionItems.IRootExpressionItemSeries>
```
</details>

- **NOTE:** The list of prefixes can include both types of prefixes at the same time (i.e., braces and custom expression items).

- Here is an example of prefixes used to model c# like attributes for classes and methods:

```csharp
// [TestFixture] and [Attribute("IntegrationTest")] are added as prefixes to literal MyTests.
[TestFixture]
[Attribute("IntegrationTest")]
// public and class are added as keywords to MyTests
public class MyTests
{
    // Brace expression items [SetupMyTests], [Attribute("This is a demo of multiple prefixes")]
    // and custom expression item starting with ::metadata and ending with } are added as prefixes to 
    // expression SetupMyTests()
    [TestSetup]
    [Attribute("This is a demo of multiple prefixes")]
    ::metadata {
        Description: "Demo of custom expression item parsed to 
                        UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IMetadataCustomExpressionItem
                        used in prefixes list of expression parsed from 'SetupMyTests()'";
        SomeMetadata: 1
    }
    // public and static are added as keywords to expression SetupMyTests().
    public static SetupMyTests() : void
    {
        // Do some test setup here
    }
}
```

<details> <summary>Click to expand the parsed expression</summary>

```XML
<?xml version="1.0" encoding="utf-8"?>
<UniversalExpressionParser.ExpressionItems.IRootExpressionItemSeries ObjectId='0' PositionInTextOnCompletion='1038' IndexInText='95' ItemLength='933'>
	<ExpressionItemSeries Id='637840162085548518' IndexInText='95' ItemLength='943' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'>
		<RegularItems>
			<Literal Id='637840162085549551' IndexInText='95' ItemLength='943' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
				<NameExpressionItem Name='MyTests' Id='637840162085549546' IndexInText='209' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				<Prefixes>
					<Braces Id='637840162085548785' IndexInText='95' ItemLength='13' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<OpeningSquareBrace Name='[' Id='637840162085548790' IndexInText='95' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Literal Id='637840162085548921' IndexInText='96' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='TestFixture' Id='637840162085548917' IndexInText='96' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<ClosingSquareBrace Name=']' Id='637840162085548979' IndexInText='107' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<Literal Id='637840162085548921' IndexInText='96' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
						</Children>
						<OtherProperties>
							<OpeningBraceInfo Name='[' Id='637840162085548790' IndexInText='95' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=']' Id='637840162085548979' IndexInText='107' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
							<Parameters ObjectId='9' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162085548921' IndexInText='96' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Braces>
					<Braces Id='637840162085549011' IndexInText='110' ItemLength='30' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<OpeningSquareBrace Name='[' Id='637840162085549031' IndexInText='110' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Braces Id='637840162085549139' IndexInText='111' ItemLength='28' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
								<RegularItems>
									<Literal Id='637840162085549115' IndexInText='111' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='Attribute' Id='637840162085549112' IndexInText='111' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<OpeningRoundBrace Name='(' Id='637840162085549142' IndexInText='120' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ConstantText Id='637840162085549257' IndexInText='121' ItemLength='17' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='"IntegrationTest"' Id='637840162085549254' IndexInText='121' ItemLength='17' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</ConstantText>
									<ClosingRoundBrace Name=')' Id='637840162085549290' IndexInText='138' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Children>
									<ConstantText Id='637840162085549257' IndexInText='121' ItemLength='17' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								</Children>
								<OtherProperties>
									<NamedExpressionItem Id='637840162085549115' IndexInText='111' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<OpeningBraceInfo Name='(' Id='637840162085549142' IndexInText='120' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ClosingBraceInfo Name=')' Id='637840162085549290' IndexInText='138' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Parameters ObjectId='19' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162085549257' IndexInText='121' ItemLength='17' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</Parameters>
								</OtherProperties>
							</Braces>
							<ClosingSquareBrace Name=']' Id='637840162085549321' IndexInText='139' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<Braces Id='637840162085549139' IndexInText='111' ItemLength='28' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
						</Children>
						<OtherProperties>
							<OpeningBraceInfo Name='[' Id='637840162085549031' IndexInText='110' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=']' Id='637840162085549321' IndexInText='139' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
							<Parameters ObjectId='21' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162085549139' IndexInText='111' ItemLength='28' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Braces>
				</Prefixes>
				<AppliedKeywords>
					<Keyword Name='public' Id='637840162085549352' IndexInText='196' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
						<LanguageKeywordInfo ObjectId='23' Id='637783669980406419' Keyword='public' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
					</Keyword>
					<Keyword Name='class' Id='637840162085549364' IndexInText='203' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
						<LanguageKeywordInfo ObjectId='25' Id='637783670388745402' Keyword='class' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
					</Keyword>
				</AppliedKeywords>
				<Postfixes>
					<CodeBlock Id='637840162085549591' IndexInText='218' ItemLength='820' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
						<RegularItems>
							<CodeBlockStartMarker Name='{' Id='637840162085549589' IndexInText='218' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<BinaryOperator Name=':' Priority='0' Id='637840162085640197' IndexInText='461' ItemLength='574' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
								<Operand1.Braces Id='637840162085639813' IndexInText='461' ItemLength='517' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
									<Prefixes>
										<Braces Id='637840162085549637' IndexInText='461' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
											<RegularItems>
												<OpeningSquareBrace Name='[' Id='637840162085549639' IndexInText='461' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												<Literal Id='637840162085549721' IndexInText='462' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
													<NameExpressionItem Name='TestSetup' Id='637840162085549719' IndexInText='462' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												</Literal>
												<ClosingSquareBrace Name=']' Id='637840162085549748' IndexInText='471' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											</RegularItems>
											<Children>
												<Literal Id='637840162085549721' IndexInText='462' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
											</Children>
											<OtherProperties>
												<OpeningBraceInfo Name='[' Id='637840162085549639' IndexInText='461' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												<ClosingBraceInfo Name=']' Id='637840162085549748' IndexInText='471' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
												<Parameters ObjectId='35' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
													<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162085549721' IndexInText='462' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
												</Parameters>
											</OtherProperties>
										</Braces>
										<Braces Id='637840162085549771' IndexInText='478' ItemLength='50' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
											<RegularItems>
												<OpeningSquareBrace Name='[' Id='637840162085549775' IndexInText='478' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												<Braces Id='637840162085549883' IndexInText='479' ItemLength='48' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
													<RegularItems>
														<Literal Id='637840162085549860' IndexInText='479' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
															<NameExpressionItem Name='Attribute' Id='637840162085549857' IndexInText='479' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
														</Literal>
														<OpeningRoundBrace Name='(' Id='637840162085549885' IndexInText='488' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
														<ConstantText Id='637840162085549970' IndexInText='489' ItemLength='37' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
															<NameExpressionItem Name='"This is a demo of multiple prefixes"' Id='637840162085549968' IndexInText='489' ItemLength='37' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
														</ConstantText>
														<ClosingRoundBrace Name=')' Id='637840162085549996' IndexInText='526' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
													</RegularItems>
													<Children>
														<ConstantText Id='637840162085549970' IndexInText='489' ItemLength='37' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
													</Children>
													<OtherProperties>
														<NamedExpressionItem Id='637840162085549860' IndexInText='479' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
														<OpeningBraceInfo Name='(' Id='637840162085549885' IndexInText='488' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
														<ClosingBraceInfo Name=')' Id='637840162085549996' IndexInText='526' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
														<Parameters ObjectId='45' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
															<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162085549970' IndexInText='489' ItemLength='37' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
														</Parameters>
													</OtherProperties>
												</Braces>
												<ClosingSquareBrace Name=']' Id='637840162085550021' IndexInText='527' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											</RegularItems>
											<Children>
												<Braces Id='637840162085549883' IndexInText='479' ItemLength='48' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
											</Children>
											<OtherProperties>
												<OpeningBraceInfo Name='[' Id='637840162085549775' IndexInText='478' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												<ClosingBraceInfo Name=']' Id='637840162085550021' IndexInText='527' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
												<Parameters ObjectId='47' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
													<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162085549883' IndexInText='479' ItemLength='48' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
												</Parameters>
											</OtherProperties>
										</Braces>
										<Custom Id='637840162085639236' IndexInText='534' ItemLength='332' Interface='UniversalExpressionParser.ExpressionItems.Custom.IKeywordBasedCustomExpressionItem'>
											<RegularItems>
												<Keyword Name='::metadata' Id='637840162085550042' IndexInText='534' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
													<LanguageKeywordInfo ObjectId='50' Id='637781063212876967' Keyword='::metadata' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
												</Keyword>
												<CodeBlock Id='637840162085550208' IndexInText='545' ItemLength='321' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
													<RegularItems>
														<CodeBlockStartMarker Name='{' Id='637840162085550205' IndexInText='545' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
														<BinaryOperator Name=':' Priority='0' Id='637840162085550634' IndexInText='556' ItemLength='277' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
															<Operand1.Literal Id='637840162085550354' IndexInText='556' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
																<NameExpressionItem Name='Description' Id='637840162085550351' IndexInText='556' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
															</Operand1.Literal>
															<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637840162085550406' IndexInText='567' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
																<OperatorNameParts>
																	<Name Name=':' Id='637840162085550397' IndexInText='567' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
																</OperatorNameParts>
															</OperatorInfo>
															<Operand2.ConstantText Id='637840162085550590' IndexInText='569' ItemLength='264' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
																<NameExpressionItem Name='"Demo of custom expression item parsed to $line_break$                        UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IMetadataCustomExpressionItem$line_break$                        used in prefixes list of expression parsed from ''SetupMyTests()''"'
																  Id='637840162085550587' IndexInText='569' ItemLength='264' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
															</Operand2.ConstantText>
														</BinaryOperator>
														<ExpressionSeparator Name=';' Id='637840162085550683' IndexInText='833' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
														<BinaryOperator Name=':' Priority='0' Id='637840162085639154' IndexInText='844' ItemLength='15' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
															<Operand1.Literal Id='637840162085550768' IndexInText='844' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
																<NameExpressionItem Name='SomeMetadata' Id='637840162085550765' IndexInText='844' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
															</Operand1.Literal>
															<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637840162085550812' IndexInText='856' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
																<OperatorNameParts>
																	<Name Name=':' Id='637840162085550808' IndexInText='856' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
																</OperatorNameParts>
															</OperatorInfo>
															<Operand2.ConstantNumericValue Id='637840162085638954' IndexInText='858' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
																<NameExpressionItem Name='1' Id='637840162085638915' IndexInText='858' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
																<OtherProperties>
																	<SucceededNumericTypeDescriptor ObjectId='68' NumberTypeId='1581134183960' Interface='UniversalExpressionParser.NumericTypeDescriptor'>
																		<RegularExpressions ObjectId='69' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[System.String]'>
																			<System.String value='^\d+' />
																		</RegularExpressions>
																	</SucceededNumericTypeDescriptor>
																</OtherProperties>
															</Operand2.ConstantNumericValue>
														</BinaryOperator>
														<CodeBlockEndMarker Name='}' Id='637840162085639220' IndexInText='865' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
													</RegularItems>
													<Children>
														<BinaryOperator Name=':' Priority='0' Id='637840162085550634' IndexInText='556' ItemLength='277' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
														<BinaryOperator Name=':' Priority='0' Id='637840162085639154' IndexInText='844' ItemLength='15' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
													</Children>
													<OtherProperties>
														<CodeBlockStartMarker Name='{' Id='637840162085550205' IndexInText='545' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
														<CodeBlockEndMarker Name='}' Id='637840162085639220' IndexInText='865' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
													</OtherProperties>
												</CodeBlock>
											</RegularItems>
											<OtherProperties>
												<LastKeywordExpressionItem Name='::metadata' Id='637840162085550042' IndexInText='534' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'/>
												<ErrorsPositionDisplayValue value='534' type='System.Int32' />
											</OtherProperties>
										</Custom>
									</Prefixes>
									<AppliedKeywords>
										<Keyword Name='public' Id='637840162085639574' IndexInText='950' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
											<LanguageKeywordInfo ObjectId='23' Id='637783669980406419' Keyword='public' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
										</Keyword>
										<Keyword Name='static' Id='637840162085639590' IndexInText='957' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
											<LanguageKeywordInfo ObjectId='73' Id='637789181872233115' Keyword='static' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
										</Keyword>
									</AppliedKeywords>
									<RegularItems>
										<Literal Id='637840162085639768' IndexInText='964' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
											<NameExpressionItem Name='SetupMyTests' Id='637840162085639764' IndexInText='964' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</Literal>
										<OpeningRoundBrace Name='(' Id='637840162085639820' IndexInText='976' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										<ClosingRoundBrace Name=')' Id='637840162085639855' IndexInText='977' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</RegularItems>
									<OtherProperties>
										<NamedExpressionItem Id='637840162085639768' IndexInText='964' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
										<OpeningBraceInfo Name='(' Id='637840162085639820' IndexInText='976' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										<ClosingBraceInfo Name=')' Id='637840162085639855' IndexInText='977' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										<Parameters ObjectId='78' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'/>
									</OtherProperties>
								</Operand1.Braces>
								<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637840162085639915' IndexInText='979' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
									<OperatorNameParts>
										<Name Name=':' Id='637840162085639908' IndexInText='979' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</OperatorNameParts>
								</OperatorInfo>
								<Operand2.Literal Id='637840162085640070' IndexInText='981' ItemLength='54' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
									<NameExpressionItem Name='void' Id='637840162085640067' IndexInText='981' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Postfixes>
										<CodeBlock Id='637840162085640104' IndexInText='991' ItemLength='44' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
											<RegularItems>
												<CodeBlockStartMarker Name='{' Id='637840162085640101' IndexInText='991' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												<CodeBlockEndMarker Name='}' Id='637840162085640148' IndexInText='1034' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											</RegularItems>
											<OtherProperties>
												<CodeBlockStartMarker Name='{' Id='637840162085640101' IndexInText='991' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												<CodeBlockEndMarker Name='}' Id='637840162085640148' IndexInText='1034' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											</OtherProperties>
										</CodeBlock>
									</Postfixes>
								</Operand2.Literal>
							</BinaryOperator>
							<CodeBlockEndMarker Name='}' Id='637840162085640240' IndexInText='1037' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<BinaryOperator Name=':' Priority='0' Id='637840162085640197' IndexInText='461' ItemLength='574' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
						</Children>
						<OtherProperties>
							<CodeBlockStartMarker Name='{' Id='637840162085549589' IndexInText='218' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637840162085640240' IndexInText='1037' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OtherProperties>
					</CodeBlock>
				</Postfixes>
			</Literal>
		</RegularItems>
		<Children>
			<Literal Id='637840162085549551' IndexInText='95' ItemLength='943' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
		</Children>
	</ExpressionItemSeries>
	<ParseErrorData ObjectId='87' HasCriticalErrors='False' Interface='UniversalExpressionParser.IParseErrorData'>
		<AllCodeItemErrors ObjectId='88' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICodeItemParseErrorData]'/>
	</ParseErrorData>
	<SortedCommentedOutCodeInfos ObjectId='89' Count='7' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICommentedOutCodeInfo]'>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='90' IsLineComment='True' IndexInText='0' ItemLength='93'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='91' IsLineComment='True' IndexInText='142' ItemLength='52'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='92' IsLineComment='True' IndexInText='225' ItemLength='92'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='93' IsLineComment='True' IndexInText='323' ItemLength='98'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='94' IsLineComment='True' IndexInText='427' ItemLength='28'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='95' IsLineComment='True' IndexInText='872' ItemLength='72'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='96' IsLineComment='True' IndexInText='1002' ItemLength='26'/>
	</SortedCommentedOutCodeInfos>
	<MainExpressionItem Id='637840162085548518' IndexInText='95' ItemLength='943' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'/>
</UniversalExpressionParser.ExpressionItems.IRootExpressionItemSeries>
```
</details>

- Below is an example of using prefixes wit different expression item types:

```csharp
// Prefixes added to a literal "x".
[NotNull] [Attribute("Marker")] x;

// Prefixes added to named round braces. [NotNull] [Attribute("Marker")] will be added 
// to prefixes in braces expression item parsed from "f1(x1)"
[NotNull] [Attribute("Marker")] f1(x1);

// Prefixes added to unnamed round braces. [NotNull] [Attribute("Marker")] will be added 
// to prefixes in braces expression item parsed from "(x1)"
[NotNull] [Attribute("Marker")] (x1);

// Prefixes added to named square braces. [NotNull] [Attribute("Marker")] will be added 
// to prefixes in named braces expression item parsed from "m1[x1]"
[NotNull] [Attribute("Marker")] m1[x1];

// Prefixes added to unnamed square braces. [NotNull] [Attribute("Marker")] will be added 
// to prefixes in braces expression item parsed from "[x1]".
[NotNull] [Attribute("Marker")] [x1];

// Prefixes added to code block. 
// Custom prefix expression item "::types[T1,T2]" will be added to list of prefixes in code block expression item
// parsed from "{var i = 12;}".
// Note, if we replace "::types[T1,T2]" to unnamed braces, then the unnamed braces will be used as a postfix for 
// code block.
::types[T1,T2] {var i = 12;};

// Prefixes added to custom expression item parsed from "::pragma x". 
// [Attribute("Marker")] will be added to list of prefixes in custom expression item
// parsed from "::pragma x".
[Attribute("Marker")] ::pragma x;

// Prefixes added text expression item. 
// [Attribute("Marker")] will be added to list of prefixes in text expression item
// parsed from "Some text".
[Attribute("Marker")] "Some text";

// Prefixes added to numeric value item. 
// [Attribute("Marker")] will be added to list of prefixes in numeric value expression item
// parsed from "0.5e-3.4".
[Attribute("Marker")] 0.5e-3.4;


```

<details> <summary>Click to expand the parsed expression</summary>

```XML
<?xml version="1.0" encoding="utf-8"?>
<UniversalExpressionParser.ExpressionItems.IRootExpressionItemSeries ObjectId='0' PositionInTextOnCompletion='1824' IndexInText='37' ItemLength='1783'>
	<ExpressionItemSeries Id='637840162086224910' IndexInText='37' ItemLength='1783' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'>
		<RegularItems>
			<Literal Id='637840162086225994' IndexInText='37' ItemLength='33' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
				<NameExpressionItem Name='x' Id='637840162086225991' IndexInText='69' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				<Prefixes>
					<Braces Id='637840162086225289' IndexInText='37' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<OpeningSquareBrace Name='[' Id='637840162086225294' IndexInText='37' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Literal Id='637840162086225457' IndexInText='38' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='NotNull' Id='637840162086225454' IndexInText='38' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<ClosingSquareBrace Name=']' Id='637840162086225528' IndexInText='45' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<Literal Id='637840162086225457' IndexInText='38' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
						</Children>
						<OtherProperties>
							<OpeningBraceInfo Name='[' Id='637840162086225294' IndexInText='37' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=']' Id='637840162086225528' IndexInText='45' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
							<Parameters ObjectId='9' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162086225457' IndexInText='38' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Braces>
					<Braces Id='637840162086225569' IndexInText='47' ItemLength='21' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<OpeningSquareBrace Name='[' Id='637840162086225588' IndexInText='47' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Braces Id='637840162086225703' IndexInText='48' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
								<RegularItems>
									<Literal Id='637840162086225669' IndexInText='48' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='Attribute' Id='637840162086225666' IndexInText='48' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<OpeningRoundBrace Name='(' Id='637840162086225707' IndexInText='57' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ConstantText Id='637840162086225815' IndexInText='58' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='"Marker"' Id='637840162086225812' IndexInText='58' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</ConstantText>
									<ClosingRoundBrace Name=')' Id='637840162086225869' IndexInText='66' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Children>
									<ConstantText Id='637840162086225815' IndexInText='58' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								</Children>
								<OtherProperties>
									<NamedExpressionItem Id='637840162086225669' IndexInText='48' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<OpeningBraceInfo Name='(' Id='637840162086225707' IndexInText='57' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ClosingBraceInfo Name=')' Id='637840162086225869' IndexInText='66' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Parameters ObjectId='19' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162086225815' IndexInText='58' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</Parameters>
								</OtherProperties>
							</Braces>
							<ClosingSquareBrace Name=']' Id='637840162086225900' IndexInText='67' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<Braces Id='637840162086225703' IndexInText='48' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
						</Children>
						<OtherProperties>
							<OpeningBraceInfo Name='[' Id='637840162086225588' IndexInText='47' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=']' Id='637840162086225900' IndexInText='67' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
							<Parameters ObjectId='21' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162086225703' IndexInText='48' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Braces>
				</Prefixes>
			</Literal>
			<ExpressionSeparator Name=';' Id='637840162086226031' IndexInText='70' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<Braces Id='637840162086226554' IndexInText='227' ItemLength='38' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
				<Prefixes>
					<Braces Id='637840162086226067' IndexInText='227' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<OpeningSquareBrace Name='[' Id='637840162086226069' IndexInText='227' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Literal Id='637840162086226151' IndexInText='228' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='NotNull' Id='637840162086226149' IndexInText='228' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<ClosingSquareBrace Name=']' Id='637840162086226177' IndexInText='235' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<Literal Id='637840162086226151' IndexInText='228' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
						</Children>
						<OtherProperties>
							<OpeningBraceInfo Name='[' Id='637840162086226069' IndexInText='227' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=']' Id='637840162086226177' IndexInText='235' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
							<Parameters ObjectId='29' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162086226151' IndexInText='228' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Braces>
					<Braces Id='637840162086226200' IndexInText='237' ItemLength='21' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<OpeningSquareBrace Name='[' Id='637840162086226204' IndexInText='237' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Braces Id='637840162086226303' IndexInText='238' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
								<RegularItems>
									<Literal Id='637840162086226282' IndexInText='238' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='Attribute' Id='637840162086226279' IndexInText='238' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<OpeningRoundBrace Name='(' Id='637840162086226306' IndexInText='247' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ConstantText Id='637840162086226389' IndexInText='248' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='"Marker"' Id='637840162086226386' IndexInText='248' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</ConstantText>
									<ClosingRoundBrace Name=')' Id='637840162086226415' IndexInText='256' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Children>
									<ConstantText Id='637840162086226389' IndexInText='248' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								</Children>
								<OtherProperties>
									<NamedExpressionItem Id='637840162086226282' IndexInText='238' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<OpeningBraceInfo Name='(' Id='637840162086226306' IndexInText='247' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ClosingBraceInfo Name=')' Id='637840162086226415' IndexInText='256' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Parameters ObjectId='39' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162086226389' IndexInText='248' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</Parameters>
								</OtherProperties>
							</Braces>
							<ClosingSquareBrace Name=']' Id='637840162086226441' IndexInText='257' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<Braces Id='637840162086226303' IndexInText='238' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
						</Children>
						<OtherProperties>
							<OpeningBraceInfo Name='[' Id='637840162086226204' IndexInText='237' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=']' Id='637840162086226441' IndexInText='257' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
							<Parameters ObjectId='41' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162086226303' IndexInText='238' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Braces>
				</Prefixes>
				<RegularItems>
					<Literal Id='637840162086226523' IndexInText='259' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='f1' Id='637840162086226520' IndexInText='259' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<OpeningRoundBrace Name='(' Id='637840162086226557' IndexInText='261' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Literal Id='637840162086226636' IndexInText='262' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='x1' Id='637840162086226634' IndexInText='262' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<ClosingRoundBrace Name=')' Id='637840162086226661' IndexInText='264' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Children>
					<Literal Id='637840162086226636' IndexInText='262' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
				</Children>
				<OtherProperties>
					<NamedExpressionItem Id='637840162086226523' IndexInText='259' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					<OpeningBraceInfo Name='(' Id='637840162086226557' IndexInText='261' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ClosingBraceInfo Name=')' Id='637840162086226661' IndexInText='264' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Parameters ObjectId='48' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162086226636' IndexInText='262' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					</Parameters>
				</OtherProperties>
			</Braces>
			<ExpressionSeparator Name=';' Id='637840162086226690' IndexInText='265' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<Braces Id='637840162086227126' IndexInText='422' ItemLength='36' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
				<Prefixes>
					<Braces Id='637840162086226728' IndexInText='422' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<OpeningSquareBrace Name='[' Id='637840162086226730' IndexInText='422' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Literal Id='637840162086226810' IndexInText='423' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='NotNull' Id='637840162086226803' IndexInText='423' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<ClosingSquareBrace Name=']' Id='637840162086226835' IndexInText='430' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<Literal Id='637840162086226810' IndexInText='423' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
						</Children>
						<OtherProperties>
							<OpeningBraceInfo Name='[' Id='637840162086226730' IndexInText='422' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=']' Id='637840162086226835' IndexInText='430' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
							<Parameters ObjectId='56' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162086226810' IndexInText='423' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Braces>
					<Braces Id='637840162086226859' IndexInText='432' ItemLength='21' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<OpeningSquareBrace Name='[' Id='637840162086226862' IndexInText='432' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Braces Id='637840162086226964' IndexInText='433' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
								<RegularItems>
									<Literal Id='637840162086226942' IndexInText='433' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='Attribute' Id='637840162086226940' IndexInText='433' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<OpeningRoundBrace Name='(' Id='637840162086226966' IndexInText='442' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ConstantText Id='637840162086227047' IndexInText='443' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='"Marker"' Id='637840162086227045' IndexInText='443' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</ConstantText>
									<ClosingRoundBrace Name=')' Id='637840162086227073' IndexInText='451' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Children>
									<ConstantText Id='637840162086227047' IndexInText='443' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								</Children>
								<OtherProperties>
									<NamedExpressionItem Id='637840162086226942' IndexInText='433' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<OpeningBraceInfo Name='(' Id='637840162086226966' IndexInText='442' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ClosingBraceInfo Name=')' Id='637840162086227073' IndexInText='451' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Parameters ObjectId='66' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162086227047' IndexInText='443' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</Parameters>
								</OtherProperties>
							</Braces>
							<ClosingSquareBrace Name=']' Id='637840162086227098' IndexInText='452' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<Braces Id='637840162086226964' IndexInText='433' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
						</Children>
						<OtherProperties>
							<OpeningBraceInfo Name='[' Id='637840162086226862' IndexInText='432' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=']' Id='637840162086227098' IndexInText='452' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
							<Parameters ObjectId='68' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162086226964' IndexInText='433' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Braces>
				</Prefixes>
				<RegularItems>
					<OpeningRoundBrace Name='(' Id='637840162086227129' IndexInText='454' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Literal Id='637840162086227203' IndexInText='455' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='x1' Id='637840162086227201' IndexInText='455' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<ClosingRoundBrace Name=')' Id='637840162086227229' IndexInText='457' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Children>
					<Literal Id='637840162086227203' IndexInText='455' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
				</Children>
				<OtherProperties>
					<OpeningBraceInfo Name='(' Id='637840162086227129' IndexInText='454' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ClosingBraceInfo Name=')' Id='637840162086227229' IndexInText='457' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
					<Parameters ObjectId='73' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162086227203' IndexInText='455' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					</Parameters>
				</OtherProperties>
			</Braces>
			<ExpressionSeparator Name=';' Id='637840162086227257' IndexInText='458' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<Braces Id='637840162086227777' IndexInText='622' ItemLength='38' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
				<Prefixes>
					<Braces Id='637840162086227290' IndexInText='622' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<OpeningSquareBrace Name='[' Id='637840162086227292' IndexInText='622' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Literal Id='637840162086227375' IndexInText='623' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='NotNull' Id='637840162086227372' IndexInText='623' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<ClosingSquareBrace Name=']' Id='637840162086227399' IndexInText='630' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<Literal Id='637840162086227375' IndexInText='623' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
						</Children>
						<OtherProperties>
							<OpeningBraceInfo Name='[' Id='637840162086227292' IndexInText='622' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=']' Id='637840162086227399' IndexInText='630' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
							<Parameters ObjectId='81' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162086227375' IndexInText='623' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Braces>
					<Braces Id='637840162086227422' IndexInText='632' ItemLength='21' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<OpeningSquareBrace Name='[' Id='637840162086227426' IndexInText='632' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Braces Id='637840162086227531' IndexInText='633' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
								<RegularItems>
									<Literal Id='637840162086227508' IndexInText='633' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='Attribute' Id='637840162086227505' IndexInText='633' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<OpeningRoundBrace Name='(' Id='637840162086227533' IndexInText='642' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ConstantText Id='637840162086227618' IndexInText='643' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='"Marker"' Id='637840162086227615' IndexInText='643' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</ConstantText>
									<ClosingRoundBrace Name=')' Id='637840162086227643' IndexInText='651' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Children>
									<ConstantText Id='637840162086227618' IndexInText='643' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								</Children>
								<OtherProperties>
									<NamedExpressionItem Id='637840162086227508' IndexInText='633' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<OpeningBraceInfo Name='(' Id='637840162086227533' IndexInText='642' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ClosingBraceInfo Name=')' Id='637840162086227643' IndexInText='651' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Parameters ObjectId='91' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162086227618' IndexInText='643' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</Parameters>
								</OtherProperties>
							</Braces>
							<ClosingSquareBrace Name=']' Id='637840162086227668' IndexInText='652' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<Braces Id='637840162086227531' IndexInText='633' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
						</Children>
						<OtherProperties>
							<OpeningBraceInfo Name='[' Id='637840162086227426' IndexInText='632' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=']' Id='637840162086227668' IndexInText='652' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
							<Parameters ObjectId='93' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162086227531' IndexInText='633' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Braces>
				</Prefixes>
				<RegularItems>
					<Literal Id='637840162086227752' IndexInText='654' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='m1' Id='637840162086227749' IndexInText='654' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<OpeningSquareBrace Name='[' Id='637840162086227781' IndexInText='656' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Literal Id='637840162086227863' IndexInText='657' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='x1' Id='637840162086227860' IndexInText='657' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<ClosingSquareBrace Name=']' Id='637840162086227888' IndexInText='659' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Children>
					<Literal Id='637840162086227863' IndexInText='657' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
				</Children>
				<OtherProperties>
					<NamedExpressionItem Id='637840162086227752' IndexInText='654' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					<OpeningBraceInfo Name='[' Id='637840162086227781' IndexInText='656' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ClosingBraceInfo Name=']' Id='637840162086227888' IndexInText='659' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Parameters ObjectId='100' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162086227863' IndexInText='657' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					</Parameters>
				</OtherProperties>
			</Braces>
			<ExpressionSeparator Name=';' Id='637840162086227917' IndexInText='660' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<Braces Id='637840162086229548' IndexInText='819' ItemLength='36' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
				<Prefixes>
					<Braces Id='637840162086227949' IndexInText='819' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<OpeningSquareBrace Name='[' Id='637840162086227951' IndexInText='819' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Literal Id='637840162086228031' IndexInText='820' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='NotNull' Id='637840162086228028' IndexInText='820' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<ClosingSquareBrace Name=']' Id='637840162086228057' IndexInText='827' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<Literal Id='637840162086228031' IndexInText='820' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
						</Children>
						<OtherProperties>
							<OpeningBraceInfo Name='[' Id='637840162086227951' IndexInText='819' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=']' Id='637840162086228057' IndexInText='827' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
							<Parameters ObjectId='108' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162086228031' IndexInText='820' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Braces>
					<Braces Id='637840162086228079' IndexInText='829' ItemLength='21' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<OpeningSquareBrace Name='[' Id='637840162086228082' IndexInText='829' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Braces Id='637840162086228191' IndexInText='830' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
								<RegularItems>
									<Literal Id='637840162086228165' IndexInText='830' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='Attribute' Id='637840162086228160' IndexInText='830' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<OpeningRoundBrace Name='(' Id='637840162086228193' IndexInText='839' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ConstantText Id='637840162086229335' IndexInText='840' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='"Marker"' Id='637840162086229319' IndexInText='840' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</ConstantText>
									<ClosingRoundBrace Name=')' Id='637840162086229430' IndexInText='848' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Children>
									<ConstantText Id='637840162086229335' IndexInText='840' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								</Children>
								<OtherProperties>
									<NamedExpressionItem Id='637840162086228165' IndexInText='830' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<OpeningBraceInfo Name='(' Id='637840162086228193' IndexInText='839' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ClosingBraceInfo Name=')' Id='637840162086229430' IndexInText='848' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Parameters ObjectId='118' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162086229335' IndexInText='840' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</Parameters>
								</OtherProperties>
							</Braces>
							<ClosingSquareBrace Name=']' Id='637840162086229501' IndexInText='849' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<Braces Id='637840162086228191' IndexInText='830' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
						</Children>
						<OtherProperties>
							<OpeningBraceInfo Name='[' Id='637840162086228082' IndexInText='829' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=']' Id='637840162086229501' IndexInText='849' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
							<Parameters ObjectId='120' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162086228191' IndexInText='830' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Braces>
				</Prefixes>
				<RegularItems>
					<OpeningSquareBrace Name='[' Id='637840162086229554' IndexInText='851' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Literal Id='637840162086229644' IndexInText='852' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='x1' Id='637840162086229642' IndexInText='852' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<ClosingSquareBrace Name=']' Id='637840162086229674' IndexInText='854' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Children>
					<Literal Id='637840162086229644' IndexInText='852' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
				</Children>
				<OtherProperties>
					<OpeningBraceInfo Name='[' Id='637840162086229554' IndexInText='851' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ClosingBraceInfo Name=']' Id='637840162086229674' IndexInText='854' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
					<Parameters ObjectId='125' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162086229644' IndexInText='852' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					</Parameters>
				</OtherProperties>
			</Braces>
			<ExpressionSeparator Name=';' Id='637840162086229701' IndexInText='855' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<CodeBlock Id='637840162086230349' IndexInText='1174' ItemLength='28' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
				<Prefixes>
					<Custom Id='637840162086230168' IndexInText='1174' ItemLength='14' Interface='UniversalExpressionParser.ExpressionItems.Custom.IKeywordBasedCustomExpressionItem'>
						<RegularItems>
							<Keyword Name='::types' Id='637840162086229748' IndexInText='1174' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
								<LanguageKeywordInfo ObjectId='130' Id='637781062811583137' Keyword='::types' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
							</Keyword>
							<Braces Id='637840162086229830' IndexInText='1181' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
								<RegularItems>
									<OpeningSquareBrace Name='[' Id='637840162086229832' IndexInText='1181' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637840162086230026' IndexInText='1182' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='T1' Id='637840162086230022' IndexInText='1182' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<Comma Name=',' Id='637840162086230055' IndexInText='1184' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637840162086230135' IndexInText='1185' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='T2' Id='637840162086230132' IndexInText='1185' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<ClosingSquareBrace Name=']' Id='637840162086230160' IndexInText='1187' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Children>
									<Literal Id='637840162086230026' IndexInText='1182' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<Literal Id='637840162086230135' IndexInText='1185' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								</Children>
								<OtherProperties>
									<OpeningBraceInfo Name='[' Id='637840162086229832' IndexInText='1181' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ClosingBraceInfo Name=']' Id='637840162086230160' IndexInText='1187' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
									<Parameters ObjectId='139' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162086230026' IndexInText='1182' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162086230135' IndexInText='1185' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</Parameters>
								</OtherProperties>
							</Braces>
						</RegularItems>
						<OtherProperties>
							<LastKeywordExpressionItem Name='::types' Id='637840162086229748' IndexInText='1174' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'/>
							<ErrorsPositionDisplayValue value='1174' type='System.Int32' />
						</OtherProperties>
					</Custom>
				</Prefixes>
				<RegularItems>
					<CodeBlockStartMarker Name='{' Id='637840162086230346' IndexInText='1189' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<BinaryOperator Name='=' Priority='2000' Id='637840162086330804' IndexInText='1190' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.Literal Id='637840162086230460' IndexInText='1190' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='i' Id='637840162086230457' IndexInText='1194' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<AppliedKeywords>
								<Keyword Name='var' Id='637840162086230375' IndexInText='1190' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
									<LanguageKeywordInfo ObjectId='145' Id='637781064051574641' Keyword='var' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
								</Keyword>
							</AppliedKeywords>
						</Operand1.Literal>
						<OperatorInfo OperatorType='BinaryOperator' Name='=' Priority='2000' Id='637840162086230533' IndexInText='1196' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<Name Name='=' Id='637840162086230525' IndexInText='1196' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OperatorNameParts>
						</OperatorInfo>
						<Operand2.ConstantNumericValue Id='637840162086330557' IndexInText='1198' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
							<NameExpressionItem Name='12' Id='637840162086330522' IndexInText='1198' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<OtherProperties>
								<SucceededNumericTypeDescriptor ObjectId='150' NumberTypeId='1581134183960' Interface='UniversalExpressionParser.NumericTypeDescriptor'>
									<RegularExpressions ObjectId='151' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[System.String]'>
										<System.String value='^\d+' />
									</RegularExpressions>
								</SucceededNumericTypeDescriptor>
							</OtherProperties>
						</Operand2.ConstantNumericValue>
					</BinaryOperator>
					<ExpressionSeparator Name=';' Id='637840162086330870' IndexInText='1200' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<CodeBlockEndMarker Name='}' Id='637840162086330909' IndexInText='1201' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Children>
					<BinaryOperator Name='=' Priority='2000' Id='637840162086330804' IndexInText='1190' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
				</Children>
				<OtherProperties>
					<CodeBlockStartMarker Name='{' Id='637840162086230346' IndexInText='1189' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<CodeBlockEndMarker Name='}' Id='637840162086330909' IndexInText='1201' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</OtherProperties>
			</CodeBlock>
			<ExpressionSeparator Name=';' Id='637840162086330953' IndexInText='1202' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<Custom Id='637840162086331450' IndexInText='1395' ItemLength='32' Interface='UniversalExpressionParser.ExpressionItems.Custom.IKeywordBasedCustomExpressionItem'>
				<Prefixes>
					<Braces Id='637840162086331003' IndexInText='1395' ItemLength='21' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<OpeningSquareBrace Name='[' Id='637840162086331006' IndexInText='1395' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Braces Id='637840162086331168' IndexInText='1396' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
								<RegularItems>
									<Literal Id='637840162086331141' IndexInText='1396' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='Attribute' Id='637840162086331138' IndexInText='1396' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<OpeningRoundBrace Name='(' Id='637840162086331170' IndexInText='1405' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ConstantText Id='637840162086331285' IndexInText='1406' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='"Marker"' Id='637840162086331283' IndexInText='1406' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</ConstantText>
									<ClosingRoundBrace Name=')' Id='637840162086331320' IndexInText='1414' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Children>
									<ConstantText Id='637840162086331285' IndexInText='1406' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								</Children>
								<OtherProperties>
									<NamedExpressionItem Id='637840162086331141' IndexInText='1396' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<OpeningBraceInfo Name='(' Id='637840162086331170' IndexInText='1405' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ClosingBraceInfo Name=')' Id='637840162086331320' IndexInText='1414' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Parameters ObjectId='165' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162086331285' IndexInText='1406' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</Parameters>
								</OtherProperties>
							</Braces>
							<ClosingSquareBrace Name=']' Id='637840162086331352' IndexInText='1415' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<Braces Id='637840162086331168' IndexInText='1396' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
						</Children>
						<OtherProperties>
							<OpeningBraceInfo Name='[' Id='637840162086331006' IndexInText='1395' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=']' Id='637840162086331352' IndexInText='1415' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
							<Parameters ObjectId='167' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162086331168' IndexInText='1396' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Braces>
				</Prefixes>
				<RegularItems>
					<Keyword Name='::pragma' Id='637840162086331372' IndexInText='1417' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
						<LanguageKeywordInfo ObjectId='169' Id='637781063127646487' Keyword='::pragma' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
					</Keyword>
					<Name Name='x' Id='637840162086331446' IndexInText='1426' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<OtherProperties>
					<LastKeywordExpressionItem Name='::pragma' Id='637840162086331372' IndexInText='1417' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'/>
					<ErrorsPositionDisplayValue value='1417' type='System.Int32' />
				</OtherProperties>
			</Custom>
			<ExpressionSeparator Name=';' Id='637840162086331620' IndexInText='1427' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<ConstantText Id='637840162086331992' IndexInText='1587' ItemLength='33' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
				<NameExpressionItem Name='"Some text"' Id='637840162086331989' IndexInText='1609' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				<Prefixes>
					<Braces Id='637840162086331662' IndexInText='1587' ItemLength='21' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<OpeningSquareBrace Name='[' Id='637840162086331664' IndexInText='1587' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Braces Id='637840162086331774' IndexInText='1588' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
								<RegularItems>
									<Literal Id='637840162086331746' IndexInText='1588' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='Attribute' Id='637840162086331744' IndexInText='1588' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<OpeningRoundBrace Name='(' Id='637840162086331776' IndexInText='1597' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ConstantText Id='637840162086331858' IndexInText='1598' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='"Marker"' Id='637840162086331855' IndexInText='1598' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</ConstantText>
									<ClosingRoundBrace Name=')' Id='637840162086331888' IndexInText='1606' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Children>
									<ConstantText Id='637840162086331858' IndexInText='1598' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								</Children>
								<OtherProperties>
									<NamedExpressionItem Id='637840162086331746' IndexInText='1588' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<OpeningBraceInfo Name='(' Id='637840162086331776' IndexInText='1597' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ClosingBraceInfo Name=')' Id='637840162086331888' IndexInText='1606' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Parameters ObjectId='183' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162086331858' IndexInText='1598' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</Parameters>
								</OtherProperties>
							</Braces>
							<ClosingSquareBrace Name=']' Id='637840162086331914' IndexInText='1607' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<Braces Id='637840162086331774' IndexInText='1588' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
						</Children>
						<OtherProperties>
							<OpeningBraceInfo Name='[' Id='637840162086331664' IndexInText='1587' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=']' Id='637840162086331914' IndexInText='1607' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
							<Parameters ObjectId='185' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162086331774' IndexInText='1588' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Braces>
				</Prefixes>
			</ConstantText>
			<ExpressionSeparator Name=';' Id='637840162086332031' IndexInText='1620' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<ConstantNumericValue Id='637840162086391617' IndexInText='1789' ItemLength='30' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
				<NameExpressionItem Name='0.5e-3.4' Id='637840162086391578' IndexInText='1811' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				<Prefixes>
					<Braces Id='637840162086332064' IndexInText='1789' ItemLength='21' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<OpeningSquareBrace Name='[' Id='637840162086332066' IndexInText='1789' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Braces Id='637840162086332167' IndexInText='1790' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
								<RegularItems>
									<Literal Id='637840162086332145' IndexInText='1790' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='Attribute' Id='637840162086332143' IndexInText='1790' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<OpeningRoundBrace Name='(' Id='637840162086332169' IndexInText='1799' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ConstantText Id='637840162086332248' IndexInText='1800' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='"Marker"' Id='637840162086332246' IndexInText='1800' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</ConstantText>
									<ClosingRoundBrace Name=')' Id='637840162086332272' IndexInText='1808' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Children>
									<ConstantText Id='637840162086332248' IndexInText='1800' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								</Children>
								<OtherProperties>
									<NamedExpressionItem Id='637840162086332145' IndexInText='1790' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<OpeningBraceInfo Name='(' Id='637840162086332169' IndexInText='1799' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ClosingBraceInfo Name=')' Id='637840162086332272' IndexInText='1808' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Parameters ObjectId='198' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162086332248' IndexInText='1800' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</Parameters>
								</OtherProperties>
							</Braces>
							<ClosingSquareBrace Name=']' Id='637840162086332297' IndexInText='1809' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<Braces Id='637840162086332167' IndexInText='1790' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
						</Children>
						<OtherProperties>
							<OpeningBraceInfo Name='[' Id='637840162086332066' IndexInText='1789' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=']' Id='637840162086332297' IndexInText='1809' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
							<Parameters ObjectId='200' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162086332167' IndexInText='1790' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Braces>
				</Prefixes>
				<OtherProperties>
					<SucceededNumericTypeDescriptor ObjectId='201' NumberTypeId='1581134225786' Interface='UniversalExpressionParser.NumericTypeDescriptor'>
						<RegularExpressions ObjectId='202' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[System.String]'>
							<System.String value='^(\d+\.\d+|\d+\.|\.\d+|\d+)(EXP|exp|E|e)[+|-]?(\d+\.\d+|\d+\.|\.\d+|\d+)' />
						</RegularExpressions>
					</SucceededNumericTypeDescriptor>
				</OtherProperties>
			</ConstantNumericValue>
			<ExpressionSeparator Name=';' Id='637840162086391810' IndexInText='1819' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
		</RegularItems>
		<Children>
			<Literal Id='637840162086225994' IndexInText='37' ItemLength='33' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
			<Braces Id='637840162086226554' IndexInText='227' ItemLength='38' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
			<Braces Id='637840162086227126' IndexInText='422' ItemLength='36' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
			<Braces Id='637840162086227777' IndexInText='622' ItemLength='38' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
			<Braces Id='637840162086229548' IndexInText='819' ItemLength='36' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
			<CodeBlock Id='637840162086230349' IndexInText='1174' ItemLength='28' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'/>
			<Custom Id='637840162086331450' IndexInText='1395' ItemLength='32' Interface='UniversalExpressionParser.ExpressionItems.Custom.IKeywordBasedCustomExpressionItem'/>
			<ConstantText Id='637840162086331992' IndexInText='1587' ItemLength='33' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
			<ConstantNumericValue Id='637840162086391617' IndexInText='1789' ItemLength='30' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'/>
		</Children>
	</ExpressionItemSeries>
	<ParseErrorData ObjectId='204' HasCriticalErrors='False' Interface='UniversalExpressionParser.IParseErrorData'>
		<AllCodeItemErrors ObjectId='205' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICodeItemParseErrorData]'/>
	</ParseErrorData>
	<SortedCommentedOutCodeInfos ObjectId='206' Count='23' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICommentedOutCodeInfo]'>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='207' IsLineComment='True' IndexInText='0' ItemLength='35'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='208' IsLineComment='True' IndexInText='75' ItemLength='87'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='209' IsLineComment='True' IndexInText='164' ItemLength='61'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='210' IsLineComment='True' IndexInText='270' ItemLength='89'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='211' IsLineComment='True' IndexInText='361' ItemLength='59'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='212' IsLineComment='True' IndexInText='463' ItemLength='88'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='213' IsLineComment='True' IndexInText='553' ItemLength='67'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='214' IsLineComment='True' IndexInText='665' ItemLength='90'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='215' IsLineComment='True' IndexInText='757' ItemLength='60'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='216' IsLineComment='True' IndexInText='860' ItemLength='33'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='217' IsLineComment='True' IndexInText='895' ItemLength='113'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='218' IsLineComment='True' IndexInText='1010' ItemLength='31'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='219' IsLineComment='True' IndexInText='1043' ItemLength='113'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='220' IsLineComment='True' IndexInText='1158' ItemLength='14'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='221' IsLineComment='True' IndexInText='1207' ItemLength='70'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='222' IsLineComment='True' IndexInText='1279' ItemLength='84'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='223' IsLineComment='True' IndexInText='1365' ItemLength='28'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='224' IsLineComment='True' IndexInText='1432' ItemLength='40'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='225' IsLineComment='True' IndexInText='1474' ItemLength='82'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='226' IsLineComment='True' IndexInText='1558' ItemLength='27'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='227' IsLineComment='True' IndexInText='1625' ItemLength='41'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='228' IsLineComment='True' IndexInText='1668' ItemLength='91'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='229' IsLineComment='True' IndexInText='1761' ItemLength='26'/>
	</SortedCommentedOutCodeInfos>
	<MainExpressionItem Id='637840162086224910' IndexInText='37' ItemLength='1783' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'/>
</UniversalExpressionParser.ExpressionItems.IRootExpressionItemSeries>
```
</details>





# Postfixes

Postfixes are one or more expression items that are placed after some other expression item, and are added to the list in property **Postfixes** in **UniversalExpressionParser.ExpressionItems.IComplexExpressionItem** for the expression item that the postfixes are placed after.

Currently **Universal Expression Parser** supports two types of prefixes:

1) Code block expressions items that are parsed to **UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem** that succeed another expression item, that can have a postfixes.
 The following are expression types that can have postfixes: Literals, such a x1 or Dog, braces expression items, such as f(x1), (y), m1[x1], [x2], or custom expression items for which property **CustomExpressionItemCategory** in **UniversalExpressionParser.ExpressionItems.Custom.ICustomExpressionItem** is equal to **UniversalExpressionParser.ExpressionItems.Custom.CustomExpressionItemCategory.Regular**. 
  
- In the example below the code expression item parsed from code block expression item that starts with '**{**' and ends with '**}**'" will be added to the list **Postfixes** in **UniversalExpressionParser.ExpressionItems.IComplexExpressionItem** for literal expression item parsed from expression **Dog**.

```csharp
public class Dog
{
   // This code block will be added as a postfix to literal expression item parsed from "Dog"
}
```

<details> <summary>Click to expand the parsed expression</summary>

```XML
<?xml version="1.0" encoding="utf-8"?>
<UniversalExpressionParser.ExpressionItems.IRootExpressionItemSeries ObjectId='0' PositionInTextOnCompletion='117' IndexInText='0' ItemLength='114'>
	<ExpressionItemSeries Id='637840162084212446' IndexInText='0' ItemLength='117' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'>
		<RegularItems>
			<Literal Id='637840162084212890' IndexInText='0' ItemLength='117' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
				<NameExpressionItem Name='Dog' Id='637840162084212885' IndexInText='13' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				<AppliedKeywords>
					<Keyword Name='public' Id='637840162084212733' IndexInText='0' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
						<LanguageKeywordInfo ObjectId='5' Id='637783669980406419' Keyword='public' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
					</Keyword>
					<Keyword Name='class' Id='637840162084212749' IndexInText='7' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
						<LanguageKeywordInfo ObjectId='7' Id='637783670388745402' Keyword='class' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
					</Keyword>
				</AppliedKeywords>
				<Postfixes>
					<CodeBlock Id='637840162084212944' IndexInText='18' ItemLength='99' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
						<RegularItems>
							<CodeBlockStartMarker Name='{' Id='637840162084212939' IndexInText='18' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637840162084212999' IndexInText='116' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<OtherProperties>
							<CodeBlockStartMarker Name='{' Id='637840162084212939' IndexInText='18' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637840162084212999' IndexInText='116' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OtherProperties>
					</CodeBlock>
				</Postfixes>
			</Literal>
		</RegularItems>
		<Children>
			<Literal Id='637840162084212890' IndexInText='0' ItemLength='117' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
		</Children>
	</ExpressionItemSeries>
	<ParseErrorData ObjectId='11' HasCriticalErrors='False' Interface='UniversalExpressionParser.IParseErrorData'>
		<AllCodeItemErrors ObjectId='12' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICodeItemParseErrorData]'/>
	</ParseErrorData>
	<SortedCommentedOutCodeInfos ObjectId='13' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICommentedOutCodeInfo]'>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='14' IsLineComment='True' IndexInText='24' ItemLength='90'/>
	</SortedCommentedOutCodeInfos>
	<MainExpressionItem Id='637840162084212446' IndexInText='0' ItemLength='117' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'/>
</UniversalExpressionParser.ExpressionItems.IRootExpressionItemSeries>
```
</details>

2) One or more expressions that are parsed to custom expression items of type **UniversalExpressionParser.ExpressionItems.Custom.ICustomExpressionItem** with property **CustomExpressionItemCategory** equal to **UniversalExpressionParser.ExpressionItems.Custom.CustomExpressionItemCategory.Postfix** that succeed another expression item, that can have a postfix (e.g., braces expression, literal, text expression item, numeric value expression item, etc).

- In the example below the two custom expression items of type **UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IWhereCustomExpressionItem** parsed from expressions that start with "where" and end with "whereend"" as well as the code block will be added to literal expression item parsed from "Dog".
  NOTE: For more details on custom expression items see section **CustomExpressionItemParsers**.

```csharp
::types[T1,T2, T3] F1(x:T1, y:T2, z: T3)
// The where below will be added as a postfix to expression item parsed from "F1(x:T1, y:T2, z: T3)
where T1:int where T2:double whereend
// The where below will be added as a postfix to expression item parsed from "F1(x:T1, y:T2, z: T3)
where T3:T1  whereend
{
    // This code block will be added as a postfix to expression item parsed from "F1(x:T1, y:T2, z: T3).
}
```

<details> <summary>Click to expand the parsed expression</summary>

```XML
<?xml version="1.0" encoding="utf-8"?>
<UniversalExpressionParser.ExpressionItems.IRootExpressionItemSeries ObjectId='0' PositionInTextOnCompletion='416' IndexInText='0' ItemLength='413'>
	<ExpressionItemSeries Id='637840162084283431' IndexInText='0' ItemLength='416' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'>
		<RegularItems>
			<Braces Id='637840162084284452' IndexInText='0' ItemLength='416' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
				<Prefixes>
					<Custom Id='637840162084284285' IndexInText='0' ItemLength='18' Interface='UniversalExpressionParser.ExpressionItems.Custom.IKeywordBasedCustomExpressionItem'>
						<RegularItems>
							<Keyword Name='::types' Id='637840162084283684' IndexInText='0' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
								<LanguageKeywordInfo ObjectId='5' Id='637781062811583137' Keyword='::types' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
							</Keyword>
							<Braces Id='637840162084283784' IndexInText='7' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
								<RegularItems>
									<OpeningSquareBrace Name='[' Id='637840162084283788' IndexInText='7' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637840162084283967' IndexInText='8' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='T1' Id='637840162084283962' IndexInText='8' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<Comma Name=',' Id='637840162084284043' IndexInText='10' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637840162084284125' IndexInText='11' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='T2' Id='637840162084284121' IndexInText='11' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<Comma Name=',' Id='637840162084284152' IndexInText='13' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637840162084284239' IndexInText='15' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='T3' Id='637840162084284235' IndexInText='15' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<ClosingSquareBrace Name=']' Id='637840162084284270' IndexInText='17' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Children>
									<Literal Id='637840162084283967' IndexInText='8' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<Literal Id='637840162084284125' IndexInText='11' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<Literal Id='637840162084284239' IndexInText='15' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								</Children>
								<OtherProperties>
									<OpeningBraceInfo Name='[' Id='637840162084283788' IndexInText='7' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ClosingBraceInfo Name=']' Id='637840162084284270' IndexInText='17' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
									<Parameters ObjectId='17' Count='3' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162084283967' IndexInText='8' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162084284125' IndexInText='11' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162084284239' IndexInText='15' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</Parameters>
								</OtherProperties>
							</Braces>
						</RegularItems>
						<OtherProperties>
							<LastKeywordExpressionItem Name='::types' Id='637840162084283684' IndexInText='0' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'/>
							<ErrorsPositionDisplayValue value='0' type='System.Int32' />
						</OtherProperties>
					</Custom>
				</Prefixes>
				<RegularItems>
					<Literal Id='637840162084284405' IndexInText='19' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='F1' Id='637840162084284401' IndexInText='19' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<OpeningRoundBrace Name='(' Id='637840162084284457' IndexInText='21' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<BinaryOperator Name=':' Priority='0' Id='637840162084284831' IndexInText='22' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.Literal Id='637840162084284568' IndexInText='22' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='x' Id='637840162084284563' IndexInText='22' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand1.Literal>
						<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637840162084284627' IndexInText='23' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<Name Name=':' Id='637840162084284617' IndexInText='23' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OperatorNameParts>
						</OperatorInfo>
						<Operand2.Literal Id='637840162084284779' IndexInText='24' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='T1' Id='637840162084284775' IndexInText='24' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand2.Literal>
					</BinaryOperator>
					<Comma Name=',' Id='637840162084284888' IndexInText='26' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<BinaryOperator Name=':' Priority='0' Id='637840162084285198' IndexInText='28' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.Literal Id='637840162084284977' IndexInText='28' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='y' Id='637840162084284973' IndexInText='28' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand1.Literal>
						<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637840162084285024' IndexInText='29' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<Name Name=':' Id='637840162084285019' IndexInText='29' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OperatorNameParts>
						</OperatorInfo>
						<Operand2.Literal Id='637840162084285162' IndexInText='30' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='T2' Id='637840162084285157' IndexInText='30' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand2.Literal>
					</BinaryOperator>
					<Comma Name=',' Id='637840162084285216' IndexInText='32' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<BinaryOperator Name=':' Priority='0' Id='637840162084285535' IndexInText='34' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.Literal Id='637840162084285304' IndexInText='34' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='z' Id='637840162084285295' IndexInText='34' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand1.Literal>
						<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637840162084285350' IndexInText='35' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<Name Name=':' Id='637840162084285345' IndexInText='35' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OperatorNameParts>
						</OperatorInfo>
						<Operand2.Literal Id='637840162084285500' IndexInText='37' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='T3' Id='637840162084285495' IndexInText='37' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand2.Literal>
					</BinaryOperator>
					<ClosingRoundBrace Name=')' Id='637840162084285552' IndexInText='39' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Postfixes>
					<Custom Id='637840162084285633' IndexInText='143' ItemLength='37' Interface='UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IWhereCustomExpressionItem'>
						<RegularItems>
							<CustomExpressionItemPart Id='637840162084285639' IndexInText='143' ItemLength='12' Interface='UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem'>
								<RegularItems>
									<Keyword Name='where' Id='637840162084285588' IndexInText='143' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
										<LanguageKeywordInfo ObjectId='48' Id='637781062905654923' Keyword='where' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
									</Keyword>
									<Name Name='T1' Id='637840162084285668' IndexInText='149' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Name Name=':' Id='637840162084285675' IndexInText='151' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637840162084285692' IndexInText='152' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='int' Id='637840162084285685' IndexInText='152' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
								</RegularItems>
								<OtherProperties>
									<WhereKeyword Name='where' Id='637840162084285588' IndexInText='143' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'/>
									<TypeName Name='T1' Id='637840162084285668' IndexInText='149' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<TypeConstraints ObjectId='53' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162084285692' IndexInText='152' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</TypeConstraints>
								</OtherProperties>
							</CustomExpressionItemPart>
							<CustomExpressionItemPart Id='637840162084285707' IndexInText='156' ItemLength='15' Interface='UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem'>
								<RegularItems>
									<Keyword Name='where' Id='637840162084285703' IndexInText='156' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
										<LanguageKeywordInfo ObjectId='48' Id='637781062905654923' Keyword='where' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
									</Keyword>
									<Name Name='T2' Id='637840162084285717' IndexInText='162' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Name Name=':' Id='637840162084285721' IndexInText='164' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637840162084285739' IndexInText='165' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='double' Id='637840162084285733' IndexInText='165' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
								</RegularItems>
								<OtherProperties>
									<WhereKeyword Name='where' Id='637840162084285703' IndexInText='156' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'/>
									<TypeName Name='T2' Id='637840162084285717' IndexInText='162' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<TypeConstraints ObjectId='60' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162084285739' IndexInText='165' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</TypeConstraints>
								</OtherProperties>
							</CustomExpressionItemPart>
							<Name Name='whereend' Id='637840162084285770' IndexInText='172' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<CustomExpressionItemPart Id='637840162084285639' IndexInText='143' ItemLength='12' Interface='UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem'/>
							<CustomExpressionItemPart Id='637840162084285707' IndexInText='156' ItemLength='15' Interface='UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem'/>
						</Children>
						<OtherProperties>
							<WhereEndMarker Name='whereend' Id='637840162084285770' IndexInText='172' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<GenericTypeDataExpressionItems ObjectId='62' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem]'>
								<UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem Id='637840162084285639' IndexInText='143' ItemLength='12'/>
								<UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem Id='637840162084285707' IndexInText='156' ItemLength='15'/>
							</GenericTypeDataExpressionItems>
							<ErrorsPositionDisplayValue value='143' type='System.Int32' />
						</OtherProperties>
					</Custom>
					<Custom Id='637840162084285838' IndexInText='283' ItemLength='21' Interface='UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IWhereCustomExpressionItem'>
						<RegularItems>
							<CustomExpressionItemPart Id='637840162084285843' IndexInText='283' ItemLength='11' Interface='UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem'>
								<RegularItems>
									<Keyword Name='where' Id='637840162084285805' IndexInText='283' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
										<LanguageKeywordInfo ObjectId='48' Id='637781062905654923' Keyword='where' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
									</Keyword>
									<Name Name='T3' Id='637840162084285856' IndexInText='289' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Name Name=':' Id='637840162084285860' IndexInText='291' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637840162084285872' IndexInText='292' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='T1' Id='637840162084285866' IndexInText='292' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
								</RegularItems>
								<OtherProperties>
									<WhereKeyword Name='where' Id='637840162084285805' IndexInText='283' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'/>
									<TypeName Name='T3' Id='637840162084285856' IndexInText='289' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<TypeConstraints ObjectId='70' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162084285872' IndexInText='292' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</TypeConstraints>
								</OtherProperties>
							</CustomExpressionItemPart>
							<Name Name='whereend' Id='637840162084285884' IndexInText='296' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<CustomExpressionItemPart Id='637840162084285843' IndexInText='283' ItemLength='11' Interface='UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem'/>
						</Children>
						<OtherProperties>
							<WhereEndMarker Name='whereend' Id='637840162084285884' IndexInText='296' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<GenericTypeDataExpressionItems ObjectId='72' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem]'>
								<UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem Id='637840162084285843' IndexInText='283' ItemLength='11'/>
							</GenericTypeDataExpressionItems>
							<ErrorsPositionDisplayValue value='283' type='System.Int32' />
						</OtherProperties>
					</Custom>
					<CodeBlock Id='637840162084285926' IndexInText='306' ItemLength='110' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
						<RegularItems>
							<CodeBlockStartMarker Name='{' Id='637840162084285921' IndexInText='306' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637840162084285962' IndexInText='415' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<OtherProperties>
							<CodeBlockStartMarker Name='{' Id='637840162084285921' IndexInText='306' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637840162084285962' IndexInText='415' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OtherProperties>
					</CodeBlock>
				</Postfixes>
				<Children>
					<BinaryOperator Name=':' Priority='0' Id='637840162084284831' IndexInText='22' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
					<BinaryOperator Name=':' Priority='0' Id='637840162084285198' IndexInText='28' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
					<BinaryOperator Name=':' Priority='0' Id='637840162084285535' IndexInText='34' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
				</Children>
				<OtherProperties>
					<NamedExpressionItem Id='637840162084284405' IndexInText='19' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					<OpeningBraceInfo Name='(' Id='637840162084284457' IndexInText='21' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ClosingBraceInfo Name=')' Id='637840162084285552' IndexInText='39' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Parameters ObjectId='76' Count='3' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name=':' Priority='0' Id='637840162084284831' IndexInText='22' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name=':' Priority='0' Id='637840162084285198' IndexInText='28' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name=':' Priority='0' Id='637840162084285535' IndexInText='34' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
					</Parameters>
				</OtherProperties>
			</Braces>
		</RegularItems>
		<Children>
			<Braces Id='637840162084284452' IndexInText='0' ItemLength='416' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
		</Children>
	</ExpressionItemSeries>
	<ParseErrorData ObjectId='77' HasCriticalErrors='False' Interface='UniversalExpressionParser.IParseErrorData'>
		<AllCodeItemErrors ObjectId='78' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICodeItemParseErrorData]'/>
	</ParseErrorData>
	<SortedCommentedOutCodeInfos ObjectId='79' Count='3' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICommentedOutCodeInfo]'>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='80' IsLineComment='True' IndexInText='42' ItemLength='99'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='81' IsLineComment='True' IndexInText='182' ItemLength='99'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='82' IsLineComment='True' IndexInText='313' ItemLength='100'/>
	</SortedCommentedOutCodeInfos>
	<MainExpressionItem Id='637840162084283431' IndexInText='0' ItemLength='416' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'/>
</UniversalExpressionParser.ExpressionItems.IRootExpressionItemSeries>
```
</details>

- **NOTE:** The list of postfixes can include both types of postfixes at the same time (i.e., custom expression items as well as a code block postfix).

- Example of code block postfix used to model function body:
 
```csharp
// More complicated cases
// In the example below the parser will apply operator ':' to 'f2(x1:int, x2:int)' and 'int'
// and will add the code block after 'int' as a postfix to 'int'.
// The evaluator that processes the parsed expression can do farther transformation so that the code block is assigned to
// some new property in some wrapper for an expression for 'f2(x1:int, x2:int)', so that the code block belongs to the function, rather than
// to the returned type 'int' of function f2.
f2(x1:int, x2:int) : int 
{
	f3() : int
	{
	    var result = x1+x2;
		println("result='"+result+"'");
		return result;
	}
	
	return f3();
}

var myFunc = f2(x1:int, x2:int) =>
{
    println(exp ^ (x1 + x2));
}

```

<details> <summary>Click to expand the parsed expression</summary>

```XML
<?xml version="1.0" encoding="utf-8"?>
<UniversalExpressionParser.ExpressionItems.IRootExpressionItemSeries ObjectId='0' PositionInTextOnCompletion='726' IndexInText='500' ItemLength='224'>
	<ExpressionItemSeries Id='637840162084023360' IndexInText='500' ItemLength='224' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'>
		<RegularItems>
			<BinaryOperator Name=':' Priority='0' Id='637840162084027079' IndexInText='500' ItemLength='149' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
				<Operand1.Braces Id='637840162084023834' IndexInText='500' ItemLength='18' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
					<RegularItems>
						<Literal Id='637840162084023787' IndexInText='500' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='f2' Id='637840162084023779' IndexInText='500' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Literal>
						<OpeningRoundBrace Name='(' Id='637840162084023838' IndexInText='502' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<BinaryOperator Name=':' Priority='0' Id='637840162084024212' IndexInText='503' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
							<Operand1.Literal Id='637840162084023944' IndexInText='503' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='x1' Id='637840162084023940' IndexInText='503' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Operand1.Literal>
							<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637840162084023998' IndexInText='505' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
								<OperatorNameParts>
									<Name Name=':' Id='637840162084023987' IndexInText='505' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</OperatorNameParts>
							</OperatorInfo>
							<Operand2.Literal Id='637840162084024158' IndexInText='506' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='int' Id='637840162084024149' IndexInText='506' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Operand2.Literal>
						</BinaryOperator>
						<Comma Name=',' Id='637840162084024290' IndexInText='509' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<BinaryOperator Name=':' Priority='0' Id='637840162084024600' IndexInText='511' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
							<Operand1.Literal Id='637840162084024377' IndexInText='511' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='x2' Id='637840162084024373' IndexInText='511' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Operand1.Literal>
							<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637840162084024425' IndexInText='513' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
								<OperatorNameParts>
									<Name Name=':' Id='637840162084024420' IndexInText='513' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</OperatorNameParts>
							</OperatorInfo>
							<Operand2.Literal Id='637840162084024561' IndexInText='514' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='int' Id='637840162084024558' IndexInText='514' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Operand2.Literal>
						</BinaryOperator>
						<ClosingRoundBrace Name=')' Id='637840162084024620' IndexInText='517' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</RegularItems>
					<Children>
						<BinaryOperator Name=':' Priority='0' Id='637840162084024212' IndexInText='503' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
						<BinaryOperator Name=':' Priority='0' Id='637840162084024600' IndexInText='511' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
					</Children>
					<OtherProperties>
						<NamedExpressionItem Id='637840162084023787' IndexInText='500' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
						<OpeningBraceInfo Name='(' Id='637840162084023838' IndexInText='502' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<ClosingBraceInfo Name=')' Id='637840162084024620' IndexInText='517' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<Parameters ObjectId='23' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
							<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name=':' Priority='0' Id='637840162084024212' IndexInText='503' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
							<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name=':' Priority='0' Id='637840162084024600' IndexInText='511' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
						</Parameters>
					</OtherProperties>
				</Operand1.Braces>
				<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637840162084024677' IndexInText='519' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
					<OperatorNameParts>
						<Name Name=':' Id='637840162084024671' IndexInText='519' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</OperatorNameParts>
				</OperatorInfo>
				<Operand2.Literal Id='637840162084024828' IndexInText='521' ItemLength='128' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
					<NameExpressionItem Name='int' Id='637840162084024824' IndexInText='521' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Postfixes>
						<CodeBlock Id='637840162084024870' IndexInText='527' ItemLength='122' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
							<RegularItems>
								<CodeBlockStartMarker Name='{' Id='637840162084024860' IndexInText='527' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<BinaryOperator Name=':' Priority='0' Id='637840162084026776' IndexInText='531' ItemLength='97' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
									<Operand1.Braces Id='637840162084024989' IndexInText='531' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
										<RegularItems>
											<Literal Id='637840162084024959' IndexInText='531' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
												<NameExpressionItem Name='f3' Id='637840162084024955' IndexInText='531' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											</Literal>
											<OpeningRoundBrace Name='(' Id='637840162084024992' IndexInText='533' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<ClosingRoundBrace Name=')' Id='637840162084025020' IndexInText='534' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</RegularItems>
										<OtherProperties>
											<NamedExpressionItem Id='637840162084024959' IndexInText='531' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
											<OpeningBraceInfo Name='(' Id='637840162084024992' IndexInText='533' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<ClosingBraceInfo Name=')' Id='637840162084025020' IndexInText='534' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<Parameters ObjectId='36' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'/>
										</OtherProperties>
									</Operand1.Braces>
									<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637840162084025070' IndexInText='536' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
										<OperatorNameParts>
											<Name Name=':' Id='637840162084025064' IndexInText='536' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</OperatorNameParts>
									</OperatorInfo>
									<Operand2.Literal Id='637840162084025208' IndexInText='538' ItemLength='90' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='int' Id='637840162084025204' IndexInText='538' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										<Postfixes>
											<CodeBlock Id='637840162084025245' IndexInText='544' ItemLength='84' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
												<RegularItems>
													<CodeBlockStartMarker Name='{' Id='637840162084025241' IndexInText='544' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
													<BinaryOperator Name='=' Priority='2000' Id='637840162084025787' IndexInText='552' ItemLength='18' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
														<Operand1.Literal Id='637840162084025358' IndexInText='552' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
															<NameExpressionItem Name='result' Id='637840162084025353' IndexInText='556' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
															<AppliedKeywords>
																<Keyword Name='var' Id='637840162084025265' IndexInText='552' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
																	<LanguageKeywordInfo ObjectId='47' Id='637781064051574641' Keyword='var' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
																</Keyword>
															</AppliedKeywords>
														</Operand1.Literal>
														<OperatorInfo OperatorType='BinaryOperator' Name='=' Priority='2000' Id='637840162084025430' IndexInText='563' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
															<OperatorNameParts>
																<Name Name='=' Id='637840162084025425' IndexInText='563' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
															</OperatorNameParts>
														</OperatorInfo>
														<Operand2.BinaryOperator Name='+' Priority='30' Id='637840162084025797' IndexInText='565' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
															<Operand1.Literal Id='637840162084025563' IndexInText='565' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
																<NameExpressionItem Name='x1' Id='637840162084025558' IndexInText='565' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
															</Operand1.Literal>
															<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637840162084025624' IndexInText='567' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
																<OperatorNameParts>
																	<Name Name='+' Id='637840162084025619' IndexInText='567' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
																</OperatorNameParts>
															</OperatorInfo>
															<Operand2.Literal Id='637840162084025752' IndexInText='568' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
																<NameExpressionItem Name='x2' Id='637840162084025748' IndexInText='568' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
															</Operand2.Literal>
														</Operand2.BinaryOperator>
													</BinaryOperator>
													<ExpressionSeparator Name=';' Id='637840162084025831' IndexInText='570' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
													<Braces Id='637840162084025947' IndexInText='575' ItemLength='30' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
														<RegularItems>
															<Literal Id='637840162084025920' IndexInText='575' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
																<NameExpressionItem Name='println' Id='637840162084025916' IndexInText='575' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
															</Literal>
															<OpeningRoundBrace Name='(' Id='637840162084025950' IndexInText='582' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
															<BinaryOperator Name='+' Priority='30' Id='637840162084026475' IndexInText='583' ItemLength='21' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
																<Operand1.BinaryOperator Name='+' Priority='30' Id='637840162084026465' IndexInText='583' ItemLength='17' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
																	<Operand1.ConstantText Id='637840162084026054' IndexInText='583' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
																		<NameExpressionItem Name='"result=''"' Id='637840162084026050' IndexInText='583' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
																	</Operand1.ConstantText>
																	<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637840162084026114' IndexInText='593' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
																		<OperatorNameParts>
																			<Name Name='+' Id='637840162084026104' IndexInText='593' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
																		</OperatorNameParts>
																	</OperatorInfo>
																	<Operand2.Literal Id='637840162084026243' IndexInText='594' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
																		<NameExpressionItem Name='result' Id='637840162084026239' IndexInText='594' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
																	</Operand2.Literal>
																</Operand1.BinaryOperator>
																<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637840162084026299' IndexInText='600' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
																	<OperatorNameParts>
																		<Name Name='+' Id='637840162084026294' IndexInText='600' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
																	</OperatorNameParts>
																</OperatorInfo>
																<Operand2.ConstantText Id='637840162084026428' IndexInText='601' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
																	<NameExpressionItem Name='"''"' Id='637840162084026424' IndexInText='601' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
																</Operand2.ConstantText>
															</BinaryOperator>
															<ClosingRoundBrace Name=')' Id='637840162084026497' IndexInText='604' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
														</RegularItems>
														<Children>
															<BinaryOperator Name='+' Priority='30' Id='637840162084026475' IndexInText='583' ItemLength='21' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
														</Children>
														<OtherProperties>
															<NamedExpressionItem Id='637840162084025920' IndexInText='575' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
															<OpeningBraceInfo Name='(' Id='637840162084025950' IndexInText='582' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
															<ClosingBraceInfo Name=')' Id='637840162084026497' IndexInText='604' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
															<Parameters ObjectId='75' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
																<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name='+' Priority='30' Id='637840162084026475' IndexInText='583' ItemLength='21' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
															</Parameters>
														</OtherProperties>
													</Braces>
													<ExpressionSeparator Name=';' Id='637840162084026527' IndexInText='605' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
													<PrefixUnaryOperator Name='return' Priority='2147483647' Id='637840162084026721' IndexInText='610' ItemLength='13' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
														<OperatorInfo OperatorType='PrefixUnaryOperator' Name='return' Priority='2147483647' Id='637840162084026572' IndexInText='610' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
															<OperatorNameParts>
																<Name Name='return' Id='637840162084026567' IndexInText='610' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
															</OperatorNameParts>
														</OperatorInfo>
														<Operand1.Literal Id='637840162084026682' IndexInText='617' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
															<NameExpressionItem Name='result' Id='637840162084026678' IndexInText='617' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
														</Operand1.Literal>
													</PrefixUnaryOperator>
													<ExpressionSeparator Name=';' Id='637840162084026739' IndexInText='623' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
													<CodeBlockEndMarker Name='}' Id='637840162084026765' IndexInText='627' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												</RegularItems>
												<Children>
													<BinaryOperator Name='=' Priority='2000' Id='637840162084025787' IndexInText='552' ItemLength='18' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
													<Braces Id='637840162084025947' IndexInText='575' ItemLength='30' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
													<PrefixUnaryOperator Name='return' Priority='2147483647' Id='637840162084026721' IndexInText='610' ItemLength='13' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
												</Children>
												<OtherProperties>
													<CodeBlockStartMarker Name='{' Id='637840162084025241' IndexInText='544' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
													<CodeBlockEndMarker Name='}' Id='637840162084026765' IndexInText='627' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												</OtherProperties>
											</CodeBlock>
										</Postfixes>
									</Operand2.Literal>
								</BinaryOperator>
								<PrefixUnaryOperator Name='return' Priority='2147483647' Id='637840162084027026' IndexInText='634' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
									<OperatorInfo OperatorType='PrefixUnaryOperator' Name='return' Priority='2147483647' Id='637840162084026830' IndexInText='634' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
										<OperatorNameParts>
											<Name Name='return' Id='637840162084026825' IndexInText='634' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</OperatorNameParts>
									</OperatorInfo>
									<Operand1.Braces Id='637840162084026968' IndexInText='641' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
										<RegularItems>
											<Literal Id='637840162084026936' IndexInText='641' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
												<NameExpressionItem Name='f3' Id='637840162084026931' IndexInText='641' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											</Literal>
											<OpeningRoundBrace Name='(' Id='637840162084026971' IndexInText='643' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<ClosingRoundBrace Name=')' Id='637840162084026998' IndexInText='644' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</RegularItems>
										<OtherProperties>
											<NamedExpressionItem Id='637840162084026936' IndexInText='641' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
											<OpeningBraceInfo Name='(' Id='637840162084026971' IndexInText='643' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<ClosingBraceInfo Name=')' Id='637840162084026998' IndexInText='644' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<Parameters ObjectId='92' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'/>
										</OtherProperties>
									</Operand1.Braces>
								</PrefixUnaryOperator>
								<ExpressionSeparator Name=';' Id='637840162084027040' IndexInText='645' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<CodeBlockEndMarker Name='}' Id='637840162084027066' IndexInText='648' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</RegularItems>
							<Children>
								<BinaryOperator Name=':' Priority='0' Id='637840162084026776' IndexInText='531' ItemLength='97' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
								<PrefixUnaryOperator Name='return' Priority='2147483647' Id='637840162084027026' IndexInText='634' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
							</Children>
							<OtherProperties>
								<CodeBlockStartMarker Name='{' Id='637840162084024860' IndexInText='527' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<CodeBlockEndMarker Name='}' Id='637840162084027066' IndexInText='648' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OtherProperties>
						</CodeBlock>
					</Postfixes>
				</Operand2.Literal>
			</BinaryOperator>
			<BinaryOperator Name='=' Priority='2000' Id='637840162084029109' IndexInText='653' ItemLength='71' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
				<Operand1.Literal Id='637840162084027191' IndexInText='653' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
					<NameExpressionItem Name='myFunc' Id='637840162084027186' IndexInText='657' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<AppliedKeywords>
						<Keyword Name='var' Id='637840162084027105' IndexInText='653' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
							<LanguageKeywordInfo ObjectId='47' Id='637781064051574641' Keyword='var' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
						</Keyword>
					</AppliedKeywords>
				</Operand1.Literal>
				<OperatorInfo OperatorType='BinaryOperator' Name='=' Priority='2000' Id='637840162084027265' IndexInText='664' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
					<OperatorNameParts>
						<Name Name='=' Id='637840162084027259' IndexInText='664' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</OperatorNameParts>
				</OperatorInfo>
				<Operand2.BinaryOperator Name='=>' Priority='1000' Id='637840162084029117' IndexInText='666' ItemLength='58' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
					<Operand1.Braces Id='637840162084027405' IndexInText='666' ItemLength='18' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<Literal Id='637840162084027375' IndexInText='666' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='f2' Id='637840162084027371' IndexInText='666' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<OpeningRoundBrace Name='(' Id='637840162084027408' IndexInText='668' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<BinaryOperator Name=':' Priority='0' Id='637840162084027710' IndexInText='669' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
								<Operand1.Literal Id='637840162084027493' IndexInText='669' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
									<NameExpressionItem Name='x1' Id='637840162084027489' IndexInText='669' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</Operand1.Literal>
								<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637840162084027543' IndexInText='671' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
									<OperatorNameParts>
										<Name Name=':' Id='637840162084027533' IndexInText='671' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</OperatorNameParts>
								</OperatorInfo>
								<Operand2.Literal Id='637840162084027679' IndexInText='672' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
									<NameExpressionItem Name='int' Id='637840162084027675' IndexInText='672' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</Operand2.Literal>
							</BinaryOperator>
							<Comma Name=',' Id='637840162084027728' IndexInText='675' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<BinaryOperator Name=':' Priority='0' Id='637840162084028036' IndexInText='677' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
								<Operand1.Literal Id='637840162084027812' IndexInText='677' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
									<NameExpressionItem Name='x2' Id='637840162084027808' IndexInText='677' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</Operand1.Literal>
								<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637840162084027858' IndexInText='679' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
									<OperatorNameParts>
										<Name Name=':' Id='637840162084027852' IndexInText='679' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</OperatorNameParts>
								</OperatorInfo>
								<Operand2.Literal Id='637840162084027995' IndexInText='680' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
									<NameExpressionItem Name='int' Id='637840162084027990' IndexInText='680' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</Operand2.Literal>
							</BinaryOperator>
							<ClosingRoundBrace Name=')' Id='637840162084028053' IndexInText='683' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<BinaryOperator Name=':' Priority='0' Id='637840162084027710' IndexInText='669' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
							<BinaryOperator Name=':' Priority='0' Id='637840162084028036' IndexInText='677' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
						</Children>
						<OtherProperties>
							<NamedExpressionItem Id='637840162084027375' IndexInText='666' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							<OpeningBraceInfo Name='(' Id='637840162084027408' IndexInText='668' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=')' Id='637840162084028053' IndexInText='683' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Parameters ObjectId='122' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name=':' Priority='0' Id='637840162084027710' IndexInText='669' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name=':' Priority='0' Id='637840162084028036' IndexInText='677' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Operand1.Braces>
					<OperatorInfo OperatorType='BinaryOperator' Name='=>' Priority='1000' Id='637840162084028128' IndexInText='685' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
						<OperatorNameParts>
							<Name Name='=>' Id='637840162084028122' IndexInText='685' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OperatorNameParts>
					</OperatorInfo>
					<Operand2.CodeBlock Id='637840162084028237' IndexInText='689' ItemLength='35' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
						<RegularItems>
							<CodeBlockStartMarker Name='{' Id='637840162084028233' IndexInText='689' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Braces Id='637840162084028381' IndexInText='696' ItemLength='24' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
								<RegularItems>
									<Literal Id='637840162084028354' IndexInText='696' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='println' Id='637840162084028349' IndexInText='696' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<OpeningRoundBrace Name='(' Id='637840162084028384' IndexInText='703' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<BinaryOperator Name='^' Priority='10' Id='637840162084029031' IndexInText='704' ItemLength='15' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
										<Operand1.Literal Id='637840162084028469' IndexInText='704' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
											<NameExpressionItem Name='exp' Id='637840162084028465' IndexInText='704' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</Operand1.Literal>
										<OperatorInfo OperatorType='BinaryOperator' Name='^' Priority='10' Id='637840162084028520' IndexInText='708' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
											<OperatorNameParts>
												<Name Name='^' Id='637840162084028512' IndexInText='708' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											</OperatorNameParts>
										</OperatorInfo>
										<Operand2.Braces Id='637840162084028647' IndexInText='710' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
											<RegularItems>
												<OpeningRoundBrace Name='(' Id='637840162084028650' IndexInText='710' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												<BinaryOperator Name='+' Priority='30' Id='637840162084028953' IndexInText='711' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
													<Operand1.Literal Id='637840162084028731' IndexInText='711' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
														<NameExpressionItem Name='x1' Id='637840162084028727' IndexInText='711' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
													</Operand1.Literal>
													<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637840162084028787' IndexInText='714' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
														<OperatorNameParts>
															<Name Name='+' Id='637840162084028782' IndexInText='714' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
														</OperatorNameParts>
													</OperatorInfo>
													<Operand2.Literal Id='637840162084028915' IndexInText='716' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
														<NameExpressionItem Name='x2' Id='637840162084028911' IndexInText='716' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
													</Operand2.Literal>
												</BinaryOperator>
												<ClosingRoundBrace Name=')' Id='637840162084028993' IndexInText='718' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											</RegularItems>
											<Children>
												<BinaryOperator Name='+' Priority='30' Id='637840162084028953' IndexInText='711' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
											</Children>
											<OtherProperties>
												<OpeningBraceInfo Name='(' Id='637840162084028650' IndexInText='710' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												<ClosingBraceInfo Name=')' Id='637840162084028993' IndexInText='718' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
												<Parameters ObjectId='146' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
													<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name='+' Priority='30' Id='637840162084028953' IndexInText='711' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
												</Parameters>
											</OtherProperties>
										</Operand2.Braces>
									</BinaryOperator>
									<ClosingRoundBrace Name=')' Id='637840162084029046' IndexInText='719' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Children>
									<BinaryOperator Name='^' Priority='10' Id='637840162084029031' IndexInText='704' ItemLength='15' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
								</Children>
								<OtherProperties>
									<NamedExpressionItem Id='637840162084028354' IndexInText='696' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<OpeningBraceInfo Name='(' Id='637840162084028384' IndexInText='703' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ClosingBraceInfo Name=')' Id='637840162084029046' IndexInText='719' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Parameters ObjectId='148' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name='^' Priority='10' Id='637840162084029031' IndexInText='704' ItemLength='15' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
									</Parameters>
								</OtherProperties>
							</Braces>
							<ExpressionSeparator Name=';' Id='637840162084029074' IndexInText='720' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637840162084029100' IndexInText='723' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<Braces Id='637840162084028381' IndexInText='696' ItemLength='24' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
						</Children>
						<OtherProperties>
							<CodeBlockStartMarker Name='{' Id='637840162084028233' IndexInText='689' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637840162084029100' IndexInText='723' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OtherProperties>
					</Operand2.CodeBlock>
				</Operand2.BinaryOperator>
			</BinaryOperator>
		</RegularItems>
		<Children>
			<BinaryOperator Name=':' Priority='0' Id='637840162084027079' IndexInText='500' ItemLength='149' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
			<BinaryOperator Name='=' Priority='2000' Id='637840162084029109' IndexInText='653' ItemLength='71' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
		</Children>
	</ExpressionItemSeries>
	<ParseErrorData ObjectId='151' HasCriticalErrors='False' Interface='UniversalExpressionParser.IParseErrorData'>
		<AllCodeItemErrors ObjectId='152' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICodeItemParseErrorData]'/>
	</ParseErrorData>
	<SortedCommentedOutCodeInfos ObjectId='153' Count='6' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICommentedOutCodeInfo]'>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='154' IsLineComment='True' IndexInText='0' ItemLength='25'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='155' IsLineComment='True' IndexInText='27' ItemLength='92'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='156' IsLineComment='True' IndexInText='121' ItemLength='65'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='157' IsLineComment='True' IndexInText='188' ItemLength='121'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='158' IsLineComment='True' IndexInText='311' ItemLength='140'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='159' IsLineComment='True' IndexInText='453' ItemLength='45'/>
	</SortedCommentedOutCodeInfos>
	<MainExpressionItem Id='637840162084023360' IndexInText='500' ItemLength='224' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'/>
</UniversalExpressionParser.ExpressionItems.IRootExpressionItemSeries>
```
</details>

- Example of code block postfix used to model class definition:

```csharp
// In the example below the parser will apply operator ':' to literal 'Dog' (with keywords public and class) and 
// braces '(Anymal, IDog)' and will add the code block after '(Anymal, IDog)' as a postfix to '(Anymal, IDog)'.
// The evaluator that processes the parsed expression can do farther transformation so that the code block is assigned to
// some new property in some wrapper for an expression for 'Dog', so that the code block belongs to the 'Dog' class, rather than
// to the braces for public classes in '(Anymal, IDog)'.
public class Dog : (Anymal, IDog)
{
    public Bark() : void
    {
        println("Bark.");
    }
}
```

<details> <summary>Click to expand the parsed expression</summary>

```XML
<?xml version="1.0" encoding="utf-8"?>
<UniversalExpressionParser.ExpressionItems.IRootExpressionItemSeries ObjectId='0' PositionInTextOnCompletion='645' IndexInText='539' ItemLength='106'>
	<ExpressionItemSeries Id='637840162084136044' IndexInText='539' ItemLength='106' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'>
		<RegularItems>
			<BinaryOperator Name=':' Priority='0' Id='637840162084137954' IndexInText='539' ItemLength='106' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
				<Operand1.Literal Id='637840162084136514' IndexInText='539' ItemLength='16' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
					<NameExpressionItem Name='Dog' Id='637840162084136509' IndexInText='552' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<AppliedKeywords>
						<Keyword Name='public' Id='637840162084136347' IndexInText='539' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
							<LanguageKeywordInfo ObjectId='6' Id='637783669980406419' Keyword='public' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
						</Keyword>
						<Keyword Name='class' Id='637840162084136364' IndexInText='546' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
							<LanguageKeywordInfo ObjectId='8' Id='637783670388745402' Keyword='class' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
						</Keyword>
					</AppliedKeywords>
				</Operand1.Literal>
				<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637840162084136593' IndexInText='556' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
					<OperatorNameParts>
						<Name Name=':' Id='637840162084136583' IndexInText='556' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</OperatorNameParts>
				</OperatorInfo>
				<Operand2.Braces Id='637840162084136780' IndexInText='558' ItemLength='87' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
					<RegularItems>
						<OpeningRoundBrace Name='(' Id='637840162084136784' IndexInText='558' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<Literal Id='637840162084136891' IndexInText='559' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='Anymal' Id='637840162084136887' IndexInText='559' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Literal>
						<Comma Name=',' Id='637840162084136935' IndexInText='565' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<Literal Id='637840162084137017' IndexInText='567' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='IDog' Id='637840162084137012' IndexInText='567' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Literal>
						<ClosingRoundBrace Name=')' Id='637840162084137047' IndexInText='571' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</RegularItems>
					<Postfixes>
						<CodeBlock Id='637840162084137089' IndexInText='574' ItemLength='71' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
							<RegularItems>
								<CodeBlockStartMarker Name='{' Id='637840162084137082' IndexInText='574' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<BinaryOperator Name=':' Priority='0' Id='637840162084137862' IndexInText='581' ItemLength='61' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
									<Operand1.Braces Id='637840162084137236' IndexInText='581' ItemLength='13' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
										<AppliedKeywords>
											<Keyword Name='public' Id='637840162084137112' IndexInText='581' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
												<LanguageKeywordInfo ObjectId='6' Id='637783669980406419' Keyword='public' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
											</Keyword>
										</AppliedKeywords>
										<RegularItems>
											<Literal Id='637840162084137199' IndexInText='588' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
												<NameExpressionItem Name='Bark' Id='637840162084137195' IndexInText='588' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											</Literal>
											<OpeningRoundBrace Name='(' Id='637840162084137241' IndexInText='592' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<ClosingRoundBrace Name=')' Id='637840162084137274' IndexInText='593' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</RegularItems>
										<OtherProperties>
											<NamedExpressionItem Id='637840162084137199' IndexInText='588' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
											<OpeningBraceInfo Name='(' Id='637840162084137241' IndexInText='592' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<ClosingBraceInfo Name=')' Id='637840162084137274' IndexInText='593' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<Parameters ObjectId='28' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'/>
										</OtherProperties>
									</Operand1.Braces>
									<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637840162084137329' IndexInText='595' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
										<OperatorNameParts>
											<Name Name=':' Id='637840162084137323' IndexInText='595' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</OperatorNameParts>
									</OperatorInfo>
									<Operand2.Literal Id='637840162084137486' IndexInText='597' ItemLength='45' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='void' Id='637840162084137481' IndexInText='597' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										<Postfixes>
											<CodeBlock Id='637840162084137520' IndexInText='607' ItemLength='35' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
												<RegularItems>
													<CodeBlockStartMarker Name='{' Id='637840162084137517' IndexInText='607' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
													<Braces Id='637840162084137639' IndexInText='618' ItemLength='16' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
														<RegularItems>
															<Literal Id='637840162084137613' IndexInText='618' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
																<NameExpressionItem Name='println' Id='637840162084137609' IndexInText='618' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
															</Literal>
															<OpeningRoundBrace Name='(' Id='637840162084137641' IndexInText='625' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
															<ConstantText Id='637840162084137743' IndexInText='626' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
																<NameExpressionItem Name='"Bark."' Id='637840162084137739' IndexInText='626' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
															</ConstantText>
															<ClosingRoundBrace Name=')' Id='637840162084137774' IndexInText='633' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
														</RegularItems>
														<Children>
															<ConstantText Id='637840162084137743' IndexInText='626' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
														</Children>
														<OtherProperties>
															<NamedExpressionItem Id='637840162084137613' IndexInText='618' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
															<OpeningBraceInfo Name='(' Id='637840162084137641' IndexInText='625' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
															<ClosingBraceInfo Name=')' Id='637840162084137774' IndexInText='633' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
															<Parameters ObjectId='42' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
																<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162084137743' IndexInText='626' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
															</Parameters>
														</OtherProperties>
													</Braces>
													<ExpressionSeparator Name=';' Id='637840162084137805' IndexInText='634' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
													<CodeBlockEndMarker Name='}' Id='637840162084137837' IndexInText='641' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												</RegularItems>
												<Children>
													<Braces Id='637840162084137639' IndexInText='618' ItemLength='16' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
												</Children>
												<OtherProperties>
													<CodeBlockStartMarker Name='{' Id='637840162084137517' IndexInText='607' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
													<CodeBlockEndMarker Name='}' Id='637840162084137837' IndexInText='641' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												</OtherProperties>
											</CodeBlock>
										</Postfixes>
									</Operand2.Literal>
								</BinaryOperator>
								<CodeBlockEndMarker Name='}' Id='637840162084137942' IndexInText='644' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</RegularItems>
							<Children>
								<BinaryOperator Name=':' Priority='0' Id='637840162084137862' IndexInText='581' ItemLength='61' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
							</Children>
							<OtherProperties>
								<CodeBlockStartMarker Name='{' Id='637840162084137082' IndexInText='574' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<CodeBlockEndMarker Name='}' Id='637840162084137942' IndexInText='644' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OtherProperties>
						</CodeBlock>
					</Postfixes>
					<Children>
						<Literal Id='637840162084136891' IndexInText='559' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
						<Literal Id='637840162084137017' IndexInText='567' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					</Children>
					<OtherProperties>
						<OpeningBraceInfo Name='(' Id='637840162084136784' IndexInText='558' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<ClosingBraceInfo Name=')' Id='637840162084137047' IndexInText='571' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
						<Parameters ObjectId='46' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
							<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162084136891' IndexInText='559' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162084137017' IndexInText='567' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
						</Parameters>
					</OtherProperties>
				</Operand2.Braces>
			</BinaryOperator>
		</RegularItems>
		<Children>
			<BinaryOperator Name=':' Priority='0' Id='637840162084137954' IndexInText='539' ItemLength='106' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
		</Children>
	</ExpressionItemSeries>
	<ParseErrorData ObjectId='47' HasCriticalErrors='False' Interface='UniversalExpressionParser.IParseErrorData'>
		<AllCodeItemErrors ObjectId='48' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICodeItemParseErrorData]'/>
	</ParseErrorData>
	<SortedCommentedOutCodeInfos ObjectId='49' Count='5' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICommentedOutCodeInfo]'>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='50' IsLineComment='True' IndexInText='0' ItemLength='113'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='51' IsLineComment='True' IndexInText='115' ItemLength='111'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='52' IsLineComment='True' IndexInText='228' ItemLength='121'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='53' IsLineComment='True' IndexInText='351' ItemLength='128'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='54' IsLineComment='True' IndexInText='481' ItemLength='56'/>
	</SortedCommentedOutCodeInfos>
	<MainExpressionItem Id='637840162084136044' IndexInText='539' ItemLength='106' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'/>
</UniversalExpressionParser.ExpressionItems.IRootExpressionItemSeries>
```
</details>

- Below are some more examples of postfixes with different expression items:

```csharp
f1(x1) 
{
    // Code block added to postfixes list for braces expression "f1(x1)"
    return x2*y1;
}

m1[x2]
{
    // Code block added to postfixes list for braces expression "m1[x2]"
    x:2*3
}

(x3)
{
    // Code block added to postfixes list for braces expression "(x3)"
    return x3*2;
}

[x4]
{
    // Code block added to postfixes list for braces expression "[x4]"
    x4:2*3
}

class Dog
{
    // Code block added to postfixes list for literal expression "Dog"
}

::pragma x
{
    // Code block added to custom expression item IPragmaCustomExpressionItem parsed from "::pragma x"
}

```

<details> <summary>Click to expand the parsed expression</summary>

```XML
<?xml version="1.0" encoding="utf-8"?>
<UniversalExpressionParser.ExpressionItems.IRootExpressionItemSeries ObjectId='0' PositionInTextOnCompletion='626' IndexInText='0' ItemLength='621'>
	<ExpressionItemSeries Id='637840162083239949' IndexInText='0' ItemLength='624' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'>
		<RegularItems>
			<Braces Id='637840162083240395' IndexInText='0' ItemLength='106' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
				<RegularItems>
					<Literal Id='637840162083240340' IndexInText='0' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='f1' Id='637840162083240331' IndexInText='0' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<OpeningRoundBrace Name='(' Id='637840162083240400' IndexInText='2' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Literal Id='637840162083240534' IndexInText='3' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='x1' Id='637840162083240529' IndexInText='3' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<ClosingRoundBrace Name=')' Id='637840162083240586' IndexInText='5' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Postfixes>
					<CodeBlock Id='637840162083240632' IndexInText='9' ItemLength='97' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
						<RegularItems>
							<CodeBlockStartMarker Name='{' Id='637840162083240623' IndexInText='9' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<PrefixUnaryOperator Name='return' Priority='2147483647' Id='637840162083241108' IndexInText='90' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
								<OperatorInfo OperatorType='PrefixUnaryOperator' Name='return' Priority='2147483647' Id='637840162083240706' IndexInText='90' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
									<OperatorNameParts>
										<Name Name='return' Id='637840162083240695' IndexInText='90' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</OperatorNameParts>
								</OperatorInfo>
								<Operand1.BinaryOperator Name='*' Priority='20' Id='637840162083241130' IndexInText='97' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
									<Operand1.Literal Id='637840162083240841' IndexInText='97' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='x2' Id='637840162083240836' IndexInText='97' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Operand1.Literal>
									<OperatorInfo OperatorType='BinaryOperator' Name='*' Priority='20' Id='637840162083240909' IndexInText='99' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
										<OperatorNameParts>
											<Name Name='*' Id='637840162083240903' IndexInText='99' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</OperatorNameParts>
									</OperatorInfo>
									<Operand2.Literal Id='637840162083241053' IndexInText='100' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='y1' Id='637840162083241045' IndexInText='100' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Operand2.Literal>
								</Operand1.BinaryOperator>
							</PrefixUnaryOperator>
							<ExpressionSeparator Name=';' Id='637840162083241186' IndexInText='102' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637840162083241217' IndexInText='105' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<PrefixUnaryOperator Name='return' Priority='2147483647' Id='637840162083241108' IndexInText='90' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
						</Children>
						<OtherProperties>
							<CodeBlockStartMarker Name='{' Id='637840162083240623' IndexInText='9' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637840162083241217' IndexInText='105' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OtherProperties>
					</CodeBlock>
				</Postfixes>
				<Children>
					<Literal Id='637840162083240534' IndexInText='3' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
				</Children>
				<OtherProperties>
					<NamedExpressionItem Id='637840162083240340' IndexInText='0' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					<OpeningBraceInfo Name='(' Id='637840162083240400' IndexInText='2' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ClosingBraceInfo Name=')' Id='637840162083240586' IndexInText='5' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Parameters ObjectId='23' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162083240534' IndexInText='3' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					</Parameters>
				</OtherProperties>
			</Braces>
			<Braces Id='637840162083241338' IndexInText='110' ItemLength='97' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
				<RegularItems>
					<Literal Id='637840162083241310' IndexInText='110' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='m1' Id='637840162083241306' IndexInText='110' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<OpeningSquareBrace Name='[' Id='637840162083241342' IndexInText='112' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Literal Id='637840162083241426' IndexInText='113' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='x2' Id='637840162083241422' IndexInText='113' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<ClosingSquareBrace Name=']' Id='637840162083241457' IndexInText='115' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Postfixes>
					<CodeBlock Id='637840162083241489' IndexInText='118' ItemLength='89' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
						<RegularItems>
							<CodeBlockStartMarker Name='{' Id='637840162083241485' IndexInText='118' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<BinaryOperator Name='*' Priority='20' Id='637840162083580046' IndexInText='199' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
								<Operand1.BinaryOperator Name=':' Priority='0' Id='637840162083580015' IndexInText='199' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
									<Operand1.Literal Id='637840162083241581' IndexInText='199' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='x' Id='637840162083241577' IndexInText='199' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Operand1.Literal>
									<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637840162083241631' IndexInText='200' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
										<OperatorNameParts>
											<Name Name=':' Id='637840162083241622' IndexInText='200' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</OperatorNameParts>
									</OperatorInfo>
									<Operand2.ConstantNumericValue Id='637840162083492695' IndexInText='201' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
										<NameExpressionItem Name='2' Id='637840162083492646' IndexInText='201' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										<OtherProperties>
											<SucceededNumericTypeDescriptor ObjectId='41' NumberTypeId='1581134183960' Interface='UniversalExpressionParser.NumericTypeDescriptor'>
												<RegularExpressions ObjectId='42' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[System.String]'>
													<System.String value='^\d+' />
												</RegularExpressions>
											</SucceededNumericTypeDescriptor>
										</OtherProperties>
									</Operand2.ConstantNumericValue>
								</Operand1.BinaryOperator>
								<OperatorInfo OperatorType='BinaryOperator' Name='*' Priority='20' Id='637840162083492957' IndexInText='202' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
									<OperatorNameParts>
										<Name Name='*' Id='637840162083492946' IndexInText='202' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</OperatorNameParts>
								</OperatorInfo>
								<Operand2.ConstantNumericValue Id='637840162083579808' IndexInText='203' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
									<NameExpressionItem Name='3' Id='637840162083579764' IndexInText='203' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<OtherProperties>
										<SucceededNumericTypeDescriptor ObjectId='41' NumberTypeId='1581134183960' Interface='UniversalExpressionParser.NumericTypeDescriptor'/>
									</OtherProperties>
								</Operand2.ConstantNumericValue>
							</BinaryOperator>
							<CodeBlockEndMarker Name='}' Id='637840162083580129' IndexInText='206' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<BinaryOperator Name='*' Priority='20' Id='637840162083580046' IndexInText='199' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
						</Children>
						<OtherProperties>
							<CodeBlockStartMarker Name='{' Id='637840162083241485' IndexInText='118' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637840162083580129' IndexInText='206' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OtherProperties>
					</CodeBlock>
				</Postfixes>
				<Children>
					<Literal Id='637840162083241426' IndexInText='113' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
				</Children>
				<OtherProperties>
					<NamedExpressionItem Id='637840162083241310' IndexInText='110' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					<OpeningBraceInfo Name='[' Id='637840162083241342' IndexInText='112' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ClosingBraceInfo Name=']' Id='637840162083241457' IndexInText='115' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Parameters ObjectId='48' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162083241426' IndexInText='113' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					</Parameters>
				</OtherProperties>
			</Braces>
			<Braces Id='637840162083580185' IndexInText='211' ItemLength='100' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
				<RegularItems>
					<OpeningRoundBrace Name='(' Id='637840162083580189' IndexInText='211' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Literal Id='637840162083580319' IndexInText='212' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='x3' Id='637840162083580314' IndexInText='212' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<ClosingRoundBrace Name=')' Id='637840162083580364' IndexInText='214' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Postfixes>
					<CodeBlock Id='637840162083580400' IndexInText='217' ItemLength='94' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
						<RegularItems>
							<CodeBlockStartMarker Name='{' Id='637840162083580396' IndexInText='217' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<PrefixUnaryOperator Name='return' Priority='2147483647' Id='637840162083664924' IndexInText='296' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
								<OperatorInfo OperatorType='PrefixUnaryOperator' Name='return' Priority='2147483647' Id='637840162083580474' IndexInText='296' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
									<OperatorNameParts>
										<Name Name='return' Id='637840162083580465' IndexInText='296' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</OperatorNameParts>
								</OperatorInfo>
								<Operand1.BinaryOperator Name='*' Priority='20' Id='637840162083664957' IndexInText='303' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
									<Operand1.Literal Id='637840162083580600' IndexInText='303' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='x3' Id='637840162083580595' IndexInText='303' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Operand1.Literal>
									<OperatorInfo OperatorType='BinaryOperator' Name='*' Priority='20' Id='637840162083580671' IndexInText='305' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
										<OperatorNameParts>
											<Name Name='*' Id='637840162083580666' IndexInText='305' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</OperatorNameParts>
									</OperatorInfo>
									<Operand2.ConstantNumericValue Id='637840162083664688' IndexInText='306' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
										<NameExpressionItem Name='2' Id='637840162083664636' IndexInText='306' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										<OtherProperties>
											<SucceededNumericTypeDescriptor ObjectId='41' NumberTypeId='1581134183960' Interface='UniversalExpressionParser.NumericTypeDescriptor'/>
										</OtherProperties>
									</Operand2.ConstantNumericValue>
								</Operand1.BinaryOperator>
							</PrefixUnaryOperator>
							<ExpressionSeparator Name=';' Id='637840162083665033' IndexInText='307' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637840162083665077' IndexInText='310' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<PrefixUnaryOperator Name='return' Priority='2147483647' Id='637840162083664924' IndexInText='296' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
						</Children>
						<OtherProperties>
							<CodeBlockStartMarker Name='{' Id='637840162083580396' IndexInText='217' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637840162083665077' IndexInText='310' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OtherProperties>
					</CodeBlock>
				</Postfixes>
				<Children>
					<Literal Id='637840162083580319' IndexInText='212' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
				</Children>
				<OtherProperties>
					<OpeningBraceInfo Name='(' Id='637840162083580189' IndexInText='211' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ClosingBraceInfo Name=')' Id='637840162083580364' IndexInText='214' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
					<Parameters ObjectId='68' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162083580319' IndexInText='212' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					</Parameters>
				</OtherProperties>
			</Braces>
			<Braces Id='637840162083665136' IndexInText='315' ItemLength='94' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
				<RegularItems>
					<OpeningSquareBrace Name='[' Id='637840162083665139' IndexInText='315' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Literal Id='637840162083665284' IndexInText='316' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='x4' Id='637840162083665279' IndexInText='316' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<ClosingSquareBrace Name=']' Id='637840162083665330' IndexInText='318' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Postfixes>
					<CodeBlock Id='637840162083665368' IndexInText='321' ItemLength='88' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
						<RegularItems>
							<CodeBlockStartMarker Name='{' Id='637840162083665363' IndexInText='321' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<BinaryOperator Name='*' Priority='20' Id='637840162083922391' IndexInText='400' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
								<Operand1.BinaryOperator Name=':' Priority='0' Id='637840162083922357' IndexInText='400' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
									<Operand1.Literal Id='637840162083665477' IndexInText='400' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='x4' Id='637840162083665473' IndexInText='400' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Operand1.Literal>
									<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637840162083665528' IndexInText='402' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
										<OperatorNameParts>
											<Name Name=':' Id='637840162083665519' IndexInText='402' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</OperatorNameParts>
									</OperatorInfo>
									<Operand2.ConstantNumericValue Id='637840162083749185' IndexInText='403' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
										<NameExpressionItem Name='2' Id='637840162083749121' IndexInText='403' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										<OtherProperties>
											<SucceededNumericTypeDescriptor ObjectId='41' NumberTypeId='1581134183960' Interface='UniversalExpressionParser.NumericTypeDescriptor'/>
										</OtherProperties>
									</Operand2.ConstantNumericValue>
								</Operand1.BinaryOperator>
								<OperatorInfo OperatorType='BinaryOperator' Name='*' Priority='20' Id='637840162083749496' IndexInText='404' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
									<OperatorNameParts>
										<Name Name='*' Id='637840162083749484' IndexInText='404' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</OperatorNameParts>
								</OperatorInfo>
								<Operand2.ConstantNumericValue Id='637840162083922123' IndexInText='405' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
									<NameExpressionItem Name='3' Id='637840162083922064' IndexInText='405' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<OtherProperties>
										<SucceededNumericTypeDescriptor ObjectId='41' NumberTypeId='1581134183960' Interface='UniversalExpressionParser.NumericTypeDescriptor'/>
									</OtherProperties>
								</Operand2.ConstantNumericValue>
							</BinaryOperator>
							<CodeBlockEndMarker Name='}' Id='637840162083922464' IndexInText='408' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<BinaryOperator Name='*' Priority='20' Id='637840162083922391' IndexInText='400' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
						</Children>
						<OtherProperties>
							<CodeBlockStartMarker Name='{' Id='637840162083665363' IndexInText='321' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637840162083922464' IndexInText='408' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OtherProperties>
					</CodeBlock>
				</Postfixes>
				<Children>
					<Literal Id='637840162083665284' IndexInText='316' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
				</Children>
				<OtherProperties>
					<OpeningBraceInfo Name='[' Id='637840162083665139' IndexInText='315' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ClosingBraceInfo Name=']' Id='637840162083665330' IndexInText='318' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
					<Parameters ObjectId='89' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162083665284' IndexInText='316' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					</Parameters>
				</OtherProperties>
			</Braces>
			<Literal Id='637840162083922670' IndexInText='413' ItemLength='87' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
				<NameExpressionItem Name='Dog' Id='637840162083922665' IndexInText='419' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				<AppliedKeywords>
					<Keyword Name='class' Id='637840162083922522' IndexInText='413' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
						<LanguageKeywordInfo ObjectId='93' Id='637783670388745402' Keyword='class' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
					</Keyword>
				</AppliedKeywords>
				<Postfixes>
					<CodeBlock Id='637840162083922711' IndexInText='424' ItemLength='76' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
						<RegularItems>
							<CodeBlockStartMarker Name='{' Id='637840162083922702' IndexInText='424' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637840162083922766' IndexInText='499' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<OtherProperties>
							<CodeBlockStartMarker Name='{' Id='637840162083922702' IndexInText='424' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637840162083922766' IndexInText='499' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OtherProperties>
					</CodeBlock>
				</Postfixes>
			</Literal>
			<Custom Id='637840162083922860' IndexInText='504' ItemLength='120' Interface='UniversalExpressionParser.ExpressionItems.Custom.IKeywordBasedCustomExpressionItem'>
				<RegularItems>
					<Keyword Name='::pragma' Id='637840162083922787' IndexInText='504' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
						<LanguageKeywordInfo ObjectId='99' Id='637781063127646487' Keyword='::pragma' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
					</Keyword>
					<Name Name='x' Id='637840162083922854' IndexInText='513' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Postfixes>
					<CodeBlock Id='637840162083922912' IndexInText='516' ItemLength='108' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
						<RegularItems>
							<CodeBlockStartMarker Name='{' Id='637840162083922908' IndexInText='516' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637840162083924587' IndexInText='623' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<OtherProperties>
							<CodeBlockStartMarker Name='{' Id='637840162083922908' IndexInText='516' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637840162083924587' IndexInText='623' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OtherProperties>
					</CodeBlock>
				</Postfixes>
				<OtherProperties>
					<LastKeywordExpressionItem Name='::pragma' Id='637840162083922787' IndexInText='504' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'/>
					<ErrorsPositionDisplayValue value='504' type='System.Int32' />
				</OtherProperties>
			</Custom>
		</RegularItems>
		<Children>
			<Braces Id='637840162083240395' IndexInText='0' ItemLength='106' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
			<Braces Id='637840162083241338' IndexInText='110' ItemLength='97' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
			<Braces Id='637840162083580185' IndexInText='211' ItemLength='100' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
			<Braces Id='637840162083665136' IndexInText='315' ItemLength='94' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
			<Literal Id='637840162083922670' IndexInText='413' ItemLength='87' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
			<Custom Id='637840162083922860' IndexInText='504' ItemLength='120' Interface='UniversalExpressionParser.ExpressionItems.Custom.IKeywordBasedCustomExpressionItem'/>
		</Children>
	</ExpressionItemSeries>
	<ParseErrorData ObjectId='104' HasCriticalErrors='False' Interface='UniversalExpressionParser.IParseErrorData'>
		<AllCodeItemErrors ObjectId='105' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICodeItemParseErrorData]'/>
	</ParseErrorData>
	<SortedCommentedOutCodeInfos ObjectId='106' Count='6' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICommentedOutCodeInfo]'>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='107' IsLineComment='True' IndexInText='16' ItemLength='68'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='108' IsLineComment='True' IndexInText='125' ItemLength='68'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='109' IsLineComment='True' IndexInText='224' ItemLength='66'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='110' IsLineComment='True' IndexInText='328' ItemLength='66'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='111' IsLineComment='True' IndexInText='431' ItemLength='66'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='112' IsLineComment='True' IndexInText='523' ItemLength='98'/>
	</SortedCommentedOutCodeInfos>
	<MainExpressionItem Id='637840162083239949' IndexInText='0' ItemLength='624' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'/>
</UniversalExpressionParser.ExpressionItems.IRootExpressionItemSeries>
```
</details>

SimpleCodeBlockPostfixUsedAsFunctionOrMatrixBody
SimpleCodeBlockPostfixUsedAsFunctionOrMatrixBody

# Custom Expression Item Parsers

Custom expression parsers allow to plugin into parsing process and provide special parsing of some portion of expression. 

The expression parser (i.e., **UniversalExpressionParser.IExpressionParser**) iteratively parses keywords (see the section above on keywords), before parsing any other symbols.

Then the expression parser loops through all the custom expression parsers of type **UniversalExpressionParser.ExpressionItems.Custom.ICustomExpressionItemParser** in **UniversalExpressionParser.IExpressionLanguageProvider.CustomExpressionItemParsers** and for each custom expression parser executes the method
 **ICustomExpressionItemParser.TryParseCustomExpressionItem(IParseExpressionItemContext context,...,
                                                            IReadOnlyList&lt;IExpressionItemBase&gt; parsedPrefixExpressionItems;
 IReadOnly                                                  IReadOnlyList&lt;IKeywordExpressionItem&gt; keywordExpressionItems)** and passes the parsed keyword expression items (i.e., list of **UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem** objects).

If method call **TryParseCustomExpressionItem(...)** returns non-null value of type **UniversalExpressionParser.ExpressionItems.Custom.ICustomExpressionItem**, the parser uses the parsed expression item.

Otherwise, if **TryParseCustomExpressionItem(...)** returns null, the parser tries to parse a non custom expression item at current position (i.e., operators, a literal, function, code block, etc.). 

Interface **ICustomExpressionItem** has a property **UniversalExpressionParser.ExpressionItems.Custom.CustomExpressionItemCategory** which is used by the parser to determine if the parsed custom expression item should be used as
 - a prefix for subsequently parsed regular expression (i.e., literal, function, braces, etc.).
 - should be treated as regular expression (which can be part of operators, function parameter, etc.).
 - or should be used as a postfix for the previously parsed expression item.

In the example below the parser parses "::pragma x" to a regular custom expression item of type **UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.PragmaCustomExpressionItem** (i.e., the value of **CustomExpressionItemCategory property** in **UniversalExpressionParser.ExpressionItems.Custom.ICustomExpressionItem** is equal to **UniversalExpressionParser.ExpressionItems.Custom.CustomExpressionItemCategory.Regular**).
As a result, the expression "::pragma x+y;" below is logically similar to "(::pragma x)+y;"

In a similar manner the expression "::types[T1, T2]" is parsed to a prefix custom expression item of type **UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.GenericTypesCustomExpressionItem** by custom expression item parser **UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.GenericTypesExpressionItemParser**.

The custom expression item parsed from "::types[T1, T2]" is added as a prefix to an expression item parsed from **F1(x:T1, y:T2)**.

Also, the expression "where T1:int where T2:double whereend" is parsed to postfix custom expression item of type **UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.WhereCustomExpressionItem** by custom expression parser **UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.WhereCustomExpressionItemParserForTests**.

The parser adds the parsed custom expression as a postfix to the preceding regular expression item parsed from text "F1(x:T1, y:T2)".

In this example, the code block after "whereend" (the expression "{...}") is parsed as a postfix expression item **UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem** and is added as a postfix to regular expression item parsed from "F1(x:T1, y:T2)" as well, since the parser adds all the prefixes/postfixes to regular expression item it finds after/before the prefixes/postfixes. 

```csharp
::pragma x+y;
::types[T1,T2] F1(x:T1, y:T2) where T1:int where T2:double whereend
{
	// This code block will be added as a postfix to expression item parsed from "F1(x:T1, y:T2)".
}

```

<details> <summary>Click to expand the parsed expression</summary>

```XML
<?xml version="1.0" encoding="utf-8"?>
<UniversalExpressionParser.ExpressionItems.IRootExpressionItemSeries ObjectId='0' PositionInTextOnCompletion='187' IndexInText='0' ItemLength='182'>
	<ExpressionItemSeries Id='637840162079793412' IndexInText='0' ItemLength='185' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'>
		<RegularItems>
			<BinaryOperator Name='+' Priority='30' Id='637840162079794148' IndexInText='0' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
				<Operand1.Custom Id='637840162079793804' IndexInText='0' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.Custom.IKeywordBasedCustomExpressionItem'>
					<RegularItems>
						<Keyword Name='::pragma' Id='637840162079793705' IndexInText='0' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
							<LanguageKeywordInfo ObjectId='5' Id='637781063127646487' Keyword='::pragma' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
						</Keyword>
						<Name Name='x' Id='637840162079793798' IndexInText='9' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</RegularItems>
					<OtherProperties>
						<LastKeywordExpressionItem Name='::pragma' Id='637840162079793705' IndexInText='0' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'/>
						<ErrorsPositionDisplayValue value='0' type='System.Int32' />
					</OtherProperties>
				</Operand1.Custom>
				<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637840162079793943' IndexInText='10' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
					<OperatorNameParts>
						<Name Name='+' Id='637840162079793931' IndexInText='10' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</OperatorNameParts>
				</OperatorInfo>
				<Operand2.Literal Id='637840162079794093' IndexInText='11' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
					<NameExpressionItem Name='y' Id='637840162079794089' IndexInText='11' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</Operand2.Literal>
			</BinaryOperator>
			<ExpressionSeparator Name=';' Id='637840162079794204' IndexInText='12' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<Braces Id='637840162079795103' IndexInText='15' ItemLength='170' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
				<Prefixes>
					<Custom Id='637840162079794727' IndexInText='15' ItemLength='14' Interface='UniversalExpressionParser.ExpressionItems.Custom.IKeywordBasedCustomExpressionItem'>
						<RegularItems>
							<Keyword Name='::types' Id='637840162079794221' IndexInText='15' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
								<LanguageKeywordInfo ObjectId='15' Id='637781062811583137' Keyword='::types' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
							</Keyword>
							<Braces Id='637840162079794401' IndexInText='22' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
								<RegularItems>
									<OpeningSquareBrace Name='[' Id='637840162079794406' IndexInText='22' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637840162079794563' IndexInText='23' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='T1' Id='637840162079794558' IndexInText='23' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<Comma Name=',' Id='637840162079794603' IndexInText='25' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637840162079794688' IndexInText='26' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='T2' Id='637840162079794684' IndexInText='26' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<ClosingSquareBrace Name=']' Id='637840162079794717' IndexInText='28' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Children>
									<Literal Id='637840162079794563' IndexInText='23' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<Literal Id='637840162079794688' IndexInText='26' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								</Children>
								<OtherProperties>
									<OpeningBraceInfo Name='[' Id='637840162079794406' IndexInText='22' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ClosingBraceInfo Name=']' Id='637840162079794717' IndexInText='28' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
									<Parameters ObjectId='24' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162079794563' IndexInText='23' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162079794688' IndexInText='26' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</Parameters>
								</OtherProperties>
							</Braces>
						</RegularItems>
						<OtherProperties>
							<LastKeywordExpressionItem Name='::types' Id='637840162079794221' IndexInText='15' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'/>
							<ErrorsPositionDisplayValue value='15' type='System.Int32' />
						</OtherProperties>
					</Custom>
				</Prefixes>
				<RegularItems>
					<Literal Id='637840162079794838' IndexInText='30' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='F1' Id='637840162079794833' IndexInText='30' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<OpeningRoundBrace Name='(' Id='637840162079795110' IndexInText='32' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<BinaryOperator Name=':' Priority='0' Id='637840162079795445' IndexInText='33' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.Literal Id='637840162079795207' IndexInText='33' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='x' Id='637840162079795202' IndexInText='33' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand1.Literal>
						<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637840162079795258' IndexInText='34' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<Name Name=':' Id='637840162079795252' IndexInText='34' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OperatorNameParts>
						</OperatorInfo>
						<Operand2.Literal Id='637840162079795403' IndexInText='35' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='T1' Id='637840162079795399' IndexInText='35' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand2.Literal>
					</BinaryOperator>
					<Comma Name=',' Id='637840162079795471' IndexInText='37' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<BinaryOperator Name=':' Priority='0' Id='637840162079795774' IndexInText='39' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.Literal Id='637840162079795550' IndexInText='39' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='y' Id='637840162079795547' IndexInText='39' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand1.Literal>
						<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637840162079795602' IndexInText='40' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<Name Name=':' Id='637840162079795596' IndexInText='40' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OperatorNameParts>
						</OperatorInfo>
						<Operand2.Literal Id='637840162079795742' IndexInText='41' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='T2' Id='637840162079795738' IndexInText='41' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand2.Literal>
					</BinaryOperator>
					<ClosingRoundBrace Name=')' Id='637840162079795790' IndexInText='43' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Postfixes>
					<Custom Id='637840162079795858' IndexInText='45' ItemLength='37' Interface='UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IWhereCustomExpressionItem'>
						<RegularItems>
							<CustomExpressionItemPart Id='637840162079795864' IndexInText='45' ItemLength='12' Interface='UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem'>
								<RegularItems>
									<Keyword Name='where' Id='637840162079795809' IndexInText='45' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
										<LanguageKeywordInfo ObjectId='47' Id='637781062905654923' Keyword='where' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
									</Keyword>
									<Name Name='T1' Id='637840162079795892' IndexInText='51' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Name Name=':' Id='637840162079795899' IndexInText='53' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637840162079795915' IndexInText='54' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='int' Id='637840162079795908' IndexInText='54' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
								</RegularItems>
								<OtherProperties>
									<WhereKeyword Name='where' Id='637840162079795809' IndexInText='45' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'/>
									<TypeName Name='T1' Id='637840162079795892' IndexInText='51' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<TypeConstraints ObjectId='52' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162079795915' IndexInText='54' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</TypeConstraints>
								</OtherProperties>
							</CustomExpressionItemPart>
							<CustomExpressionItemPart Id='637840162079795931' IndexInText='58' ItemLength='15' Interface='UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem'>
								<RegularItems>
									<Keyword Name='where' Id='637840162079795926' IndexInText='58' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
										<LanguageKeywordInfo ObjectId='47' Id='637781062905654923' Keyword='where' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
									</Keyword>
									<Name Name='T2' Id='637840162079795941' IndexInText='64' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Name Name=':' Id='637840162079795946' IndexInText='66' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637840162079795960' IndexInText='67' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='double' Id='637840162079795954' IndexInText='67' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
								</RegularItems>
								<OtherProperties>
									<WhereKeyword Name='where' Id='637840162079795926' IndexInText='58' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'/>
									<TypeName Name='T2' Id='637840162079795941' IndexInText='64' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<TypeConstraints ObjectId='59' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162079795960' IndexInText='67' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</TypeConstraints>
								</OtherProperties>
							</CustomExpressionItemPart>
							<Name Name='whereend' Id='637840162079795982' IndexInText='74' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<CustomExpressionItemPart Id='637840162079795864' IndexInText='45' ItemLength='12' Interface='UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem'/>
							<CustomExpressionItemPart Id='637840162079795931' IndexInText='58' ItemLength='15' Interface='UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem'/>
						</Children>
						<OtherProperties>
							<WhereEndMarker Name='whereend' Id='637840162079795982' IndexInText='74' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<GenericTypeDataExpressionItems ObjectId='61' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem]'>
								<UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem Id='637840162079795864' IndexInText='45' ItemLength='12'/>
								<UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem Id='637840162079795931' IndexInText='58' ItemLength='15'/>
							</GenericTypeDataExpressionItems>
							<ErrorsPositionDisplayValue value='45' type='System.Int32' />
						</OtherProperties>
					</Custom>
					<CodeBlock Id='637840162079796036' IndexInText='84' ItemLength='101' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
						<RegularItems>
							<CodeBlockStartMarker Name='{' Id='637840162079796032' IndexInText='84' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637840162079796079' IndexInText='184' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<OtherProperties>
							<CodeBlockStartMarker Name='{' Id='637840162079796032' IndexInText='84' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637840162079796079' IndexInText='184' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OtherProperties>
					</CodeBlock>
				</Postfixes>
				<Children>
					<BinaryOperator Name=':' Priority='0' Id='637840162079795445' IndexInText='33' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
					<BinaryOperator Name=':' Priority='0' Id='637840162079795774' IndexInText='39' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
				</Children>
				<OtherProperties>
					<NamedExpressionItem Id='637840162079794838' IndexInText='30' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					<OpeningBraceInfo Name='(' Id='637840162079795110' IndexInText='32' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ClosingBraceInfo Name=')' Id='637840162079795790' IndexInText='43' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Parameters ObjectId='65' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name=':' Priority='0' Id='637840162079795445' IndexInText='33' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name=':' Priority='0' Id='637840162079795774' IndexInText='39' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
					</Parameters>
				</OtherProperties>
			</Braces>
		</RegularItems>
		<Children>
			<BinaryOperator Name='+' Priority='30' Id='637840162079794148' IndexInText='0' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
			<Braces Id='637840162079795103' IndexInText='15' ItemLength='170' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
		</Children>
	</ExpressionItemSeries>
	<ParseErrorData ObjectId='66' HasCriticalErrors='False' Interface='UniversalExpressionParser.IParseErrorData'>
		<AllCodeItemErrors ObjectId='67' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICodeItemParseErrorData]'/>
	</ParseErrorData>
	<SortedCommentedOutCodeInfos ObjectId='68' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICommentedOutCodeInfo]'>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='69' IsLineComment='True' IndexInText='88' ItemLength='94'/>
	</SortedCommentedOutCodeInfos>
	<MainExpressionItem Id='637840162079793412' IndexInText='0' ItemLength='185' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'/>
</UniversalExpressionParser.ExpressionItems.IRootExpressionItemSeries>
```
</details>

- This is another example demonstrating that the parsed expression can have multiple prefix and postfix custom expressions items applied to the same regular expression item parsed from "F1(x:T1, y:T2, z:T3)".

```csharp
// The expression below ("::metadata {...}") is parsed to a prefix custom expression item and added to list of prefixes of regular
// expression item parsed from F1(x:T1, y:T2, z:T3) 
::metadata {description: "F1 demoes regular function expression item to which multiple prefix and postfix custom expression items are added."}

// ::types[T1,T2] is also parsed to a prefix custom expression item and added to list of prefixes of regular
// expression item parsed from F1(x:T1, y:T2, z:T3) 
::types[T1,T2]
F1(x:T1, y:T2, z:T3) 

// The postfix custom expression item parsed from "where T1:int where T2:double whereend" is added to list of postfixes of regular expression 
// parsed from "F1(x:T1, y:T2, z:T3)".
where T1:int,class where T2:double whereend 

// The postfix custom expression item parsed from "where T3 : T1 whereend " is also added to list of postfixes of regular expression 
// parsed from "F1(x:T1, y:T2, z:T3)".
where T3 : T1 whereend 
{
   // This code block will be added as a postfix to expression item parsed from "F1(x:T1, y:T2, z:T3)".
}
```

<details> <summary>Click to expand the parsed expression</summary>

```XML
<?xml version="1.0" encoding="utf-8"?>
<UniversalExpressionParser.ExpressionItems.IRootExpressionItemSeries ObjectId='0' PositionInTextOnCompletion='1078' IndexInText='186' ItemLength='889'>
	<ExpressionItemSeries Id='637840162079666472' IndexInText='186' ItemLength='892' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'>
		<RegularItems>
			<Braces Id='637840162079685365' IndexInText='186' ItemLength='892' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
				<Prefixes>
					<Custom Id='637840162079675791' IndexInText='186' ItemLength='142' Interface='UniversalExpressionParser.ExpressionItems.Custom.IKeywordBasedCustomExpressionItem'>
						<RegularItems>
							<Keyword Name='::metadata' Id='637840162079666735' IndexInText='186' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
								<LanguageKeywordInfo ObjectId='5' Id='637781063212876967' Keyword='::metadata' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
							</Keyword>
							<CodeBlock Id='637840162079674546' IndexInText='197' ItemLength='131' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
								<RegularItems>
									<CodeBlockStartMarker Name='{' Id='637840162079674527' IndexInText='197' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<BinaryOperator Name=':' Priority='0' Id='637840162079675122' IndexInText='198' ItemLength='129' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
										<Operand1.Literal Id='637840162079674796' IndexInText='198' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
											<NameExpressionItem Name='description' Id='637840162079674790' IndexInText='198' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</Operand1.Literal>
										<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637840162079674864' IndexInText='209' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
											<OperatorNameParts>
												<Name Name=':' Id='637840162079674854' IndexInText='209' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											</OperatorNameParts>
										</OperatorInfo>
										<Operand2.ConstantText Id='637840162079675062' IndexInText='211' ItemLength='116' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
											<NameExpressionItem Name='"F1 demoes regular function expression item to which multiple prefix and postfix custom expression items are added."' Id='637840162079675058' IndexInText='211' ItemLength='116' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</Operand2.ConstantText>
									</BinaryOperator>
									<CodeBlockEndMarker Name='}' Id='637840162079675171' IndexInText='327' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Children>
									<BinaryOperator Name=':' Priority='0' Id='637840162079675122' IndexInText='198' ItemLength='129' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
								</Children>
								<OtherProperties>
									<CodeBlockStartMarker Name='{' Id='637840162079674527' IndexInText='197' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<CodeBlockEndMarker Name='}' Id='637840162079675171' IndexInText='327' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</OtherProperties>
							</CodeBlock>
						</RegularItems>
						<OtherProperties>
							<LastKeywordExpressionItem Name='::metadata' Id='637840162079666735' IndexInText='186' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'/>
							<ErrorsPositionDisplayValue value='186' type='System.Int32' />
						</OtherProperties>
					</Custom>
					<Custom Id='637840162079681450' IndexInText='496' ItemLength='14' Interface='UniversalExpressionParser.ExpressionItems.Custom.IKeywordBasedCustomExpressionItem'>
						<RegularItems>
							<Keyword Name='::types' Id='637840162079676520' IndexInText='496' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
								<LanguageKeywordInfo ObjectId='18' Id='637781062811583137' Keyword='::types' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
							</Keyword>
							<Braces Id='637840162079680489' IndexInText='503' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
								<RegularItems>
									<OpeningSquareBrace Name='[' Id='637840162079680508' IndexInText='503' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637840162079680709' IndexInText='504' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='T1' Id='637840162079680703' IndexInText='504' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<Comma Name=',' Id='637840162079680757' IndexInText='506' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637840162079680844' IndexInText='507' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='T2' Id='637840162079680835' IndexInText='507' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<ClosingSquareBrace Name=']' Id='637840162079680874' IndexInText='509' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Children>
									<Literal Id='637840162079680709' IndexInText='504' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<Literal Id='637840162079680844' IndexInText='507' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								</Children>
								<OtherProperties>
									<OpeningBraceInfo Name='[' Id='637840162079680508' IndexInText='503' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ClosingBraceInfo Name=']' Id='637840162079680874' IndexInText='509' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
									<Parameters ObjectId='27' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162079680709' IndexInText='504' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162079680844' IndexInText='507' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</Parameters>
								</OtherProperties>
							</Braces>
						</RegularItems>
						<OtherProperties>
							<LastKeywordExpressionItem Name='::types' Id='637840162079676520' IndexInText='496' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'/>
							<ErrorsPositionDisplayValue value='496' type='System.Int32' />
						</OtherProperties>
					</Custom>
				</Prefixes>
				<RegularItems>
					<Literal Id='637840162079685228' IndexInText='512' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='F1' Id='637840162079685211' IndexInText='512' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<OpeningRoundBrace Name='(' Id='637840162079685372' IndexInText='514' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<BinaryOperator Name=':' Priority='0' Id='637840162079685762' IndexInText='515' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.Literal Id='637840162079685510' IndexInText='515' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='x' Id='637840162079685506' IndexInText='515' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand1.Literal>
						<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637840162079685568' IndexInText='516' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<Name Name=':' Id='637840162079685560' IndexInText='516' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OperatorNameParts>
						</OperatorInfo>
						<Operand2.Literal Id='637840162079685716' IndexInText='517' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='T1' Id='637840162079685711' IndexInText='517' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand2.Literal>
					</BinaryOperator>
					<Comma Name=',' Id='637840162079685793' IndexInText='519' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<BinaryOperator Name=':' Priority='0' Id='637840162079686099' IndexInText='521' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.Literal Id='637840162079685878' IndexInText='521' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='y' Id='637840162079685874' IndexInText='521' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand1.Literal>
						<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637840162079685929' IndexInText='522' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<Name Name=':' Id='637840162079685924' IndexInText='522' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OperatorNameParts>
						</OperatorInfo>
						<Operand2.Literal Id='637840162079686067' IndexInText='523' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='T2' Id='637840162079686063' IndexInText='523' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand2.Literal>
					</BinaryOperator>
					<Comma Name=',' Id='637840162079686115' IndexInText='525' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<BinaryOperator Name=':' Priority='0' Id='637840162079686422' IndexInText='527' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.Literal Id='637840162079686201' IndexInText='527' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='z' Id='637840162079686197' IndexInText='527' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand1.Literal>
						<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637840162079686248' IndexInText='528' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<Name Name=':' Id='637840162079686243' IndexInText='528' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OperatorNameParts>
						</OperatorInfo>
						<Operand2.Literal Id='637840162079686390' IndexInText='529' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='T3' Id='637840162079686382' IndexInText='529' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand2.Literal>
					</BinaryOperator>
					<ClosingRoundBrace Name=')' Id='637840162079686438' IndexInText='531' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Postfixes>
					<Custom Id='637840162079695632' IndexInText='721' ItemLength='43' Interface='UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IWhereCustomExpressionItem'>
						<RegularItems>
							<CustomExpressionItemPart Id='637840162079696410' IndexInText='721' ItemLength='18' Interface='UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem'>
								<RegularItems>
									<Keyword Name='where' Id='637840162079686476' IndexInText='721' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
										<LanguageKeywordInfo ObjectId='58' Id='637781062905654923' Keyword='where' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
									</Keyword>
									<Name Name='T1' Id='637840162079697486' IndexInText='727' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Name Name=':' Id='637840162079698572' IndexInText='729' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637840162079699084' IndexInText='730' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='int' Id='637840162079698618' IndexInText='730' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<Comma Name=',' Id='637840162079699871' IndexInText='733' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637840162079699926' IndexInText='734' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='class' Id='637840162079699916' IndexInText='734' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
								</RegularItems>
								<OtherProperties>
									<WhereKeyword Name='where' Id='637840162079686476' IndexInText='721' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'/>
									<TypeName Name='T1' Id='637840162079697486' IndexInText='727' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<TypeConstraints ObjectId='66' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162079699084' IndexInText='730' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162079699926' IndexInText='734' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</TypeConstraints>
								</OtherProperties>
							</CustomExpressionItemPart>
							<CustomExpressionItemPart Id='637840162079700685' IndexInText='740' ItemLength='15' Interface='UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem'>
								<RegularItems>
									<Keyword Name='where' Id='637840162079700670' IndexInText='740' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
										<LanguageKeywordInfo ObjectId='58' Id='637781062905654923' Keyword='where' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
									</Keyword>
									<Name Name='T2' Id='637840162079700757' IndexInText='746' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Name Name=':' Id='637840162079700765' IndexInText='748' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637840162079700781' IndexInText='749' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='double' Id='637840162079700774' IndexInText='749' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
								</RegularItems>
								<OtherProperties>
									<WhereKeyword Name='where' Id='637840162079700670' IndexInText='740' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'/>
									<TypeName Name='T2' Id='637840162079700757' IndexInText='746' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<TypeConstraints ObjectId='73' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162079700781' IndexInText='749' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</TypeConstraints>
								</OtherProperties>
							</CustomExpressionItemPart>
							<Name Name='whereend' Id='637840162079702537' IndexInText='756' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<CustomExpressionItemPart Id='637840162079696410' IndexInText='721' ItemLength='18' Interface='UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem'/>
							<CustomExpressionItemPart Id='637840162079700685' IndexInText='740' ItemLength='15' Interface='UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem'/>
						</Children>
						<OtherProperties>
							<WhereEndMarker Name='whereend' Id='637840162079702537' IndexInText='756' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<GenericTypeDataExpressionItems ObjectId='75' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem]'>
								<UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem Id='637840162079696410' IndexInText='721' ItemLength='18'/>
								<UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem Id='637840162079700685' IndexInText='740' ItemLength='15'/>
							</GenericTypeDataExpressionItems>
							<ErrorsPositionDisplayValue value='721' type='System.Int32' />
						</OtherProperties>
					</Custom>
					<Custom Id='637840162079703675' IndexInText='944' ItemLength='22' Interface='UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IWhereCustomExpressionItem'>
						<RegularItems>
							<CustomExpressionItemPart Id='637840162079703683' IndexInText='944' ItemLength='13' Interface='UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem'>
								<RegularItems>
									<Keyword Name='where' Id='637840162079703590' IndexInText='944' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
										<LanguageKeywordInfo ObjectId='58' Id='637781062905654923' Keyword='where' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
									</Keyword>
									<Name Name='T3' Id='637840162079703714' IndexInText='950' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Name Name=':' Id='637840162079703722' IndexInText='953' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637840162079703738' IndexInText='955' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='T1' Id='637840162079703731' IndexInText='955' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
								</RegularItems>
								<OtherProperties>
									<WhereKeyword Name='where' Id='637840162079703590' IndexInText='944' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'/>
									<TypeName Name='T3' Id='637840162079703714' IndexInText='950' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<TypeConstraints ObjectId='83' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162079703738' IndexInText='955' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</TypeConstraints>
								</OtherProperties>
							</CustomExpressionItemPart>
							<Name Name='whereend' Id='637840162079703759' IndexInText='958' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<CustomExpressionItemPart Id='637840162079703683' IndexInText='944' ItemLength='13' Interface='UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem'/>
						</Children>
						<OtherProperties>
							<WhereEndMarker Name='whereend' Id='637840162079703759' IndexInText='958' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<GenericTypeDataExpressionItems ObjectId='85' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem]'>
								<UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem Id='637840162079703683' IndexInText='944' ItemLength='13'/>
							</GenericTypeDataExpressionItems>
							<ErrorsPositionDisplayValue value='944' type='System.Int32' />
						</OtherProperties>
					</Custom>
					<CodeBlock Id='637840162079703962' IndexInText='969' ItemLength='109' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
						<RegularItems>
							<CodeBlockStartMarker Name='{' Id='637840162079703956' IndexInText='969' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637840162079704004' IndexInText='1077' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<OtherProperties>
							<CodeBlockStartMarker Name='{' Id='637840162079703956' IndexInText='969' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637840162079704004' IndexInText='1077' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OtherProperties>
					</CodeBlock>
				</Postfixes>
				<Children>
					<BinaryOperator Name=':' Priority='0' Id='637840162079685762' IndexInText='515' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
					<BinaryOperator Name=':' Priority='0' Id='637840162079686099' IndexInText='521' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
					<BinaryOperator Name=':' Priority='0' Id='637840162079686422' IndexInText='527' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
				</Children>
				<OtherProperties>
					<NamedExpressionItem Id='637840162079685228' IndexInText='512' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					<OpeningBraceInfo Name='(' Id='637840162079685372' IndexInText='514' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ClosingBraceInfo Name=')' Id='637840162079686438' IndexInText='531' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Parameters ObjectId='89' Count='3' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name=':' Priority='0' Id='637840162079685762' IndexInText='515' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name=':' Priority='0' Id='637840162079686099' IndexInText='521' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name=':' Priority='0' Id='637840162079686422' IndexInText='527' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
					</Parameters>
				</OtherProperties>
			</Braces>
		</RegularItems>
		<Children>
			<Braces Id='637840162079685365' IndexInText='186' ItemLength='892' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
		</Children>
	</ExpressionItemSeries>
	<ParseErrorData ObjectId='90' HasCriticalErrors='False' Interface='UniversalExpressionParser.IParseErrorData'>
		<AllCodeItemErrors ObjectId='91' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICodeItemParseErrorData]'/>
	</ParseErrorData>
	<SortedCommentedOutCodeInfos ObjectId='92' Count='9' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICommentedOutCodeInfo]'>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='93' IsLineComment='True' IndexInText='0' ItemLength='130'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='94' IsLineComment='True' IndexInText='132' ItemLength='52'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='95' IsLineComment='True' IndexInText='332' ItemLength='108'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='96' IsLineComment='True' IndexInText='442' ItemLength='52'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='97' IsLineComment='True' IndexInText='537' ItemLength='142'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='98' IsLineComment='True' IndexInText='681' ItemLength='38'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='99' IsLineComment='True' IndexInText='769' ItemLength='133'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='100' IsLineComment='True' IndexInText='904' ItemLength='38'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='101' IsLineComment='True' IndexInText='975' ItemLength='100'/>
	</SortedCommentedOutCodeInfos>
	<MainExpressionItem Id='637840162079666472' IndexInText='186' ItemLength='892' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'/>
</UniversalExpressionParser.ExpressionItems.IRootExpressionItemSeries>
```
</details>

## Implementing Custom Expression Parsers

For examples of custom expression item parsers look at some examples in demo project **UniversalExpressionParser.DemoExpressionLanguageProviders**.

The following classes might be useful when implementing custom expression parses: 

- UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.WhereCustomExpressionItemParserBase
- UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.PragmaCustomExpressionItemParser
- UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.MetadataCustomExpressionItemParser

Also, these example custom expression parser classes demonstrate how to use the helper class **UniversalExpressionParser.IParseExpressionItemContext** that is passed as a parameter to 
method **DoParseCustomExpressionItem(IParseExpressionItemContext context,...)** in **UniversalExpressionParser.ExpressionItems.Custom.CustomExpressionItemParserByKeywordId** to parse the text at current position, as well as how to report errors, if any.

To add a new custom expression parser, one needs to implement an interface **UniversalExpressionParser.ExpressionItems.Custom.ICustomExpressionItemParser** and make sure the property **CustomExpressionItemParsers** of type **System.Collections.Generic.IEnumerable&lt;ICustomExpressionItemParser&gt;** in **UniversalExpressionParser.IExpressionLanguageProvider** includes an instance of the implemented parser class.

In most cases the default implementation **UniversalExpressionParser.ExpressionItems.Custom.AggregateCustomExpressionItemParser.** of **UniversalExpressionParser.ExpressionItems.Custom.ICustomExpressionItemParser** can be used.

**UniversalExpressionParser.ExpressionItems.Custom.AggregateCustomExpressionItemParser** has a dependency on **IEnumerable&lt;ICustomExpressionItemParserByKeywordId&gt;** (injected into constructor).

Using single **AggregateCustomExpressionItemParser** parser in **UniversalExpressionParser.IExpressionLanguageProvider.CustomExpressionItemParsers** instead of multiple parsers that are implementations of **ICustomExpressionItemParser** improves performance,
since **AggregateCustomExpressionItemParser** internally keeps a mapping from keyword if to all the instances of **UniversalExpressionParser.ExpressionItems.Custom.ICustomExpressionItemParserByKeywordId** injected in constructor, to quickly determine which instances of 
**ICustomExpressionItemParserByKeywordId** to try, using the keyword expression items parsed to method **TryParseCustomExpressionItem(..., IReadOnlyList&lt;IKeywordExpressionItem&gt; parsedKeywordExpressionItems)**.

- Below is some of the code from classes **AggregateCustomExpressionItemParser** and **ICustomExpressionItemParserByKeywordId**.

```csharp

namespace UniversalExpressionParser.ExpressionItems.Custom;

public class AggregateCustomExpressionItemParser : ICustomExpressionItemParser
{
    public AggregateCustomExpressionItemParser(
        IEnumerable<ICustomExpressionItemParserByKeywordId> customExpressionItemParsers)
    {
        ...
    }

    public ICustomExpressionItem TryParseCustomExpressionItem(IParseExpressionItemContext context,
            IReadOnlyList<IExpressionItemBase> parsedPrefixExpressionItems,
            IReadOnlyList<IKeywordExpressionItem> parsedKeywordExpressionItems)
    {
        ...
    }   
}

public interface ICustomExpressionItemParserByKeywordId
{
    long ParsedKeywordId { get; }

    ICustomExpressionItem TryParseCustomExpressionItem(IParseExpressionItemContext context,
            IReadOnlyList<IExpressionItemBase> parsedPrefixExpressionItems,
            IReadOnlyList<IKeywordExpressionItem> parsedKeywordExpressionItemsWithoutLastKeyword,
            IKeywordExpressionItem lastKeywordExpressionItem);
}

```

- Here is the code from demo parser **UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.PragmaCustomExpressionItemParser** 

<details> <summary>Click to expand the UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.PragmaCustomExpressionItemParser code</summary>

```csharp
using System.Collections.Generic;
using UniversalExpressionParser.ExpressionItems;
using UniversalExpressionParser.ExpressionItems.Custom;

namespace UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions
{
    /// <summary>
    ///  Example: ::pragma x
    /// </summary>
    public class PragmaCustomExpressionItemParser : CustomExpressionItemParserByKeywordId
    {
        public PragmaCustomExpressionItemParser() : base(KeywordIds.Pragma)
        {
        }

        /// <inheritdoc />
        protected override ICustomExpressionItem DoParseCustomExpressionItem(IParseExpressionItemContext context, IReadOnlyList<IExpressionItemBase> parsedPrefixExpressionItems, 
                                                                           IReadOnlyList<IKeywordExpressionItem> parsedKeywordExpressionItemsWithoutLastKeyword,
                                                                           IKeywordExpressionItem pragmaKeywordExpressionItem)
        {
            var pragmaKeywordInfo = pragmaKeywordExpressionItem.LanguageKeywordInfo;

            var textSymbolsParser = context.TextSymbolsParser;

            if (!context.SkipSpacesAndComments() || !context.TryParseLiteral(out var literalExpressionItem))
            {
                if (!context.ParseErrorData.HasCriticalErrors)
                {
                    // Example: print("Is in debug mode=" + ::pragma IsDebugMode)
                    context.AddCodeItemParseErrorData(new CodeItemParseErrorData(textSymbolsParser.PositionInText,
                        () => $"Pragma keyword '{pragmaKeywordInfo.Keyword}' should be followed with pragma symbol. Example: println(\"Is in debug mode = \" + {pragmaKeywordInfo.Keyword} IsDebug);",
                        CustomExpressionParseErrorCodes.PragmaKeywordShouldBeFollowedByValidSymbol));
                }

                return null;
            }

            return new PragmaCustomExpressionItem(parsedPrefixExpressionItems, parsedKeywordExpressionItemsWithoutLastKeyword,
                pragmaKeywordExpressionItem,
                new SimpleExpressionItem(literalExpressionItem, textSymbolsParser.PositionInText - literalExpressionItem.Length, ExpressionItemType.Name));
        }
    }
}
```
</details>

- Below is the interface **IParseExpressionItemContext** that shows the helper methods and properties, as well as extension methods that can be used when parsing custom expressions.

<details> <summary>Click to expand the UniversalExpressionParser.IParseExpressionItemContext.</summary>

```csharp

using System;
using JetBrains.Annotations;
using TextParser;
using UniversalExpressionParser.ExpressionItems;

namespace UniversalExpressionParser
{
    public delegate void ParseErrorAddedDelegate(object sender, ParseErrorAddedEventArgs e);

    /// <summary>
    /// Parser context data.
    /// </summary>
    public interface IParseExpressionItemContext
    {
        event ParseErrorAddedDelegate ParseErrorAddedEvent;

        [NotNull]
        IExpressionLanguageProviderWrapper ExpressionLanguageProviderWrapper { get; }

        /// <summary>
        /// Root expression item that contains currently parsed expressions.
        /// </summary>
        [NotNull]
        IRootExpressionItem RootExpressionItem { get; }

        /// <summary>
        /// Error data. Use <see cref="ParseExpressionItemContext.AddCodeItemParseErrorData(ICodeItemParseErrorData)"/> to add errors.
        /// Default implementation of <see cref="ICodeItemParseErrorData"/> is <see cref="CodeItemParseErrorData"/>.
        /// </summary>
        [NotNull]
        IParseErrorData ParseErrorData { get; }

        /// <summary>
        /// Text symbols parser that should be used to navigate the position in parsed text as the expressions a re parsed.
        /// </summary>
        [NotNull]
        ITextSymbolsParser TextSymbolsParser { get; }

        /// <summary>
        /// Returns true, if parsing was completed before reaching end of text due to some condition meat, and not dues to critical error encountered.
        /// </summary>
        bool IsEarlyParseStopEncountered { get; }

        /// <summary>
        /// Tries to parse braces if the text at current position starts with either '(' or '['.
        /// If parsing is successful, the position after parsing will be after ')' or ']'
        /// </summary>
        /// <param name="nameExpressionItem">If the parameter is not null, the braces expression item will have a
        /// value <see cref="IBracesExpressionItem.NamedExpressionItem"/> initialized using <paramref name="nameExpressionItem"/>.
        /// </param>
        /// <exception cref="ParseTextException">Throws this exception if the text at current position (i.e., at position <see cref="TextSymbolsParser"/>.PositionInText
        /// is not '(' or '['</exception>
        [NotNull]
        IBracesExpressionItem EvaluateBracesExpressionItem([CanBeNull] INameExpressionItem nameExpressionItem);

        /// <summary>
        /// Tries to parse braces if the text at current position starts with either '(' or '['.
        /// If parsing is successful, the position after parsing will be after ')' or ']'
        /// </summary>
        /// <exception cref="ParseTextException">Throws this exception if the text at current position (i.e., at position <see cref="TextSymbolsParser"/>.PositionInText
        /// is not is not code block start marker <see cref="IExpressionLanguageProvider.CodeBlockStartMarker"/>.</exception>
        [NotNull]
        ICodeBlockExpressionItem EvaluateCodeBlockExpressionItem();

        /// <summary>
        /// Skips comments and spaces. Comments will be added to <see cref="IRootExpressionItem.SortedCommentedOutCodeInfos"/>.
        /// </summary>
        /// <returns>Returns true, if text end is not reached (i.e. the value of <see cref="TextSymbolsParser"/>.IsEndOfTextReached is false).
        /// Returns false otherwise.</returns>
        bool SkipSpacesAndComments();

        /// <summary>
        /// Tries to pare literal at current position. Literal is a symbol that contains only characters that pass the test
        /// <see cref="IExpressionLanguageProvider.IsValidLiteralCharacter(char, int, ITextSymbolsParserState)"/>
        /// </summary>
        /// <param name="parsedLiteral">Parsed literal.</param>
        /// <returns>Returns true if valid literal was parsed. Returns false otherwise.</returns>
        bool TryParseLiteral(out string parsedLiteral);

        /// <summary>
        /// Adds an error data.
        /// </summary>
        /// <param name="codeItemParseErrorData">Code item error data.</param>
        void AddCodeItemParseErrorData([NotNull] ICodeItemParseErrorData codeItemParseErrorData);
    }

    public static class ParseExpressionItemContextExtensionMethods
    {
        public static bool StartsWith([NotNull] this IParseExpressionItemContext parseExpressionItemContext, string textToMatch)
        {
            var textSymbolsParser = parseExpressionItemContext.TextSymbolsParser;
            return Helpers.StartsWith(textSymbolsParser.TextToParse, textToMatch, textSymbolsParser.PositionInText, textSymbolsParser.ParsedTextEnd,
                parseExpressionItemContext.ExpressionLanguageProviderWrapper);
        }

        public static bool StartsWith([NotNull] this IParseExpressionItemContext parseExpressionItemContext, string textToMatch, out string matchedText)
        {
            var textSymbolsParser = parseExpressionItemContext.TextSymbolsParser;
            return Helpers.StartsWith(textSymbolsParser.TextToParse, textToMatch, textSymbolsParser.PositionInText, textSymbolsParser.ParsedTextEnd,
                parseExpressionItemContext.ExpressionLanguageProviderWrapper, null, out matchedText);
        }

        public static bool StartsWith([NotNull] this IParseExpressionItemContext parseExpressionItemContext, string textToMatch,
                                      IsValidTextAfterMatchedTextDelegate isValidTextAfterTextToMatch)
        {
            var textSymbolsParser = parseExpressionItemContext.TextSymbolsParser;
            return Helpers.StartsWith(textSymbolsParser.TextToParse, textToMatch, textSymbolsParser.PositionInText, textSymbolsParser.ParsedTextEnd,
                                        parseExpressionItemContext.ExpressionLanguageProviderWrapper, isValidTextAfterTextToMatch);
        }

        public static bool StartsWith([NotNull] this IParseExpressionItemContext parseExpressionItemContext, string textToMatch,
                                      IsValidTextAfterMatchedTextDelegate isValidTextAfterTextToMatch, out string matchedText)
        {
            var textSymbolsParser = parseExpressionItemContext.TextSymbolsParser;
            return Helpers.StartsWith(textSymbolsParser.TextToParse, textToMatch, textSymbolsParser.PositionInText, textSymbolsParser.ParsedTextEnd,
                parseExpressionItemContext.ExpressionLanguageProviderWrapper, isValidTextAfterTextToMatch, out matchedText);
        }
    }
}

```
</details>

# Comments

Tne interface **UniversalExpressionParser.IExpressionLanguageProvider** has properties **string LineCommentMarker { get; }**, **string MultilineCommentStartMarker { get; }**, and **string MultilineCommentEndMarker { get; }** for specifying comment markers.

If the values of these properties are not null, line anc code block comments can be used.

The default implementation **UniversalExpressionParser.ExpressionLanguageProviderBase** of **UniversalExpressionParser.IExpressionLanguageProvider** overrides these properties to return "//", "/*"m and "*/" (the values of these properties can be overridden in subclasses).

The information on commented out code is stored in property **IReadOnlyList<UniversalExpressionParser.ICommentedOutCodeInfo> SortedCommentedOutCodeInfos { get; }** in **UniversalExpressionParser.ExpressionItems.IRootExpressionItem** an instance of which is returned by the call to method **UniversalExpressionParser.IExpressionParserOptions.ParseExpression(...)**.

- Below are some examples of line and code block comments:

```csharp
// Line comment
var x = 5*y; // another line comments

println(x +/*Code block 
comments
can span multiple lines and can be placed anywhere.
*/y+10*z);

/*
Another code block comments
var y=5*x;
var z = 3*y;
*/
```

<details> <summary>Click to expand the parsed expression</summary>

```XML
<?xml version="1.0" encoding="utf-8"?>
<UniversalExpressionParser.ExpressionItems.IRootExpressionItemSeries ObjectId='0' PositionInTextOnCompletion='222' IndexInText='17' ItemLength='205'>
	<ExpressionItemSeries Id='637840162079395412' IndexInText='17' ItemLength='140' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'>
		<RegularItems>
			<BinaryOperator Name='=' Priority='2000' Id='637840162079477999' IndexInText='17' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
				<Operand1.Literal Id='637840162079395818' IndexInText='17' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
					<NameExpressionItem Name='x' Id='637840162079395813' IndexInText='21' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<AppliedKeywords>
						<Keyword Name='var' Id='637840162079395648' IndexInText='17' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
							<LanguageKeywordInfo ObjectId='6' Id='637781064051574641' Keyword='var' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
						</Keyword>
					</AppliedKeywords>
				</Operand1.Literal>
				<OperatorInfo OperatorType='BinaryOperator' Name='=' Priority='2000' Id='637840162079395943' IndexInText='23' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
					<OperatorNameParts>
						<Name Name='=' Id='637840162079395932' IndexInText='23' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</OperatorNameParts>
				</OperatorInfo>
				<Operand2.BinaryOperator Name='*' Priority='20' Id='637840162079478018' IndexInText='25' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
					<Operand1.ConstantNumericValue Id='637840162079477482' IndexInText='25' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
						<NameExpressionItem Name='5' Id='637840162079477441' IndexInText='25' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<OtherProperties>
							<SucceededNumericTypeDescriptor ObjectId='12' NumberTypeId='1581134183960' Interface='UniversalExpressionParser.NumericTypeDescriptor'>
								<RegularExpressions ObjectId='13' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[System.String]'>
									<System.String value='^\d+' />
								</RegularExpressions>
							</SucceededNumericTypeDescriptor>
						</OtherProperties>
					</Operand1.ConstantNumericValue>
					<OperatorInfo OperatorType='BinaryOperator' Name='*' Priority='20' Id='637840162079477705' IndexInText='26' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
						<OperatorNameParts>
							<Name Name='*' Id='637840162079477694' IndexInText='26' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OperatorNameParts>
					</OperatorInfo>
					<Operand2.Literal Id='637840162079477914' IndexInText='27' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='y' Id='637840162079477909' IndexInText='27' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Operand2.Literal>
				</Operand2.BinaryOperator>
			</BinaryOperator>
			<ExpressionSeparator Name=';' Id='637840162079478073' IndexInText='28' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<Braces Id='637840162079478234' IndexInText='58' ItemLength='98' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
				<RegularItems>
					<Literal Id='637840162079478205' IndexInText='58' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='println' Id='637840162079478201' IndexInText='58' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<OpeningRoundBrace Name='(' Id='637840162079478238' IndexInText='65' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<BinaryOperator Name='+' Priority='30' Id='637840162079571589' IndexInText='66' ItemLength='89' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.BinaryOperator Name='+' Priority='30' Id='637840162079571566' IndexInText='66' ItemLength='84' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
							<Operand1.Literal Id='637840162079478352' IndexInText='66' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='x' Id='637840162079478347' IndexInText='66' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Operand1.Literal>
							<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637840162079478409' IndexInText='68' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
								<OperatorNameParts>
									<Name Name='+' Id='637840162079478402' IndexInText='68' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</OperatorNameParts>
							</OperatorInfo>
							<Operand2.Literal Id='637840162079478604' IndexInText='149' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='y' Id='637840162079478599' IndexInText='149' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Operand2.Literal>
						</Operand1.BinaryOperator>
						<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637840162079478688' IndexInText='150' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<Name Name='+' Id='637840162079478682' IndexInText='150' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OperatorNameParts>
						</OperatorInfo>
						<Operand2.BinaryOperator Name='*' Priority='20' Id='637840162079571596' IndexInText='151' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
							<Operand1.ConstantNumericValue Id='637840162079571109' IndexInText='151' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
								<NameExpressionItem Name='10' Id='637840162079571069' IndexInText='151' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<OtherProperties>
									<SucceededNumericTypeDescriptor ObjectId='12' NumberTypeId='1581134183960' Interface='UniversalExpressionParser.NumericTypeDescriptor'/>
								</OtherProperties>
							</Operand1.ConstantNumericValue>
							<OperatorInfo OperatorType='BinaryOperator' Name='*' Priority='20' Id='637840162079571339' IndexInText='153' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
								<OperatorNameParts>
									<Name Name='*' Id='637840162079571328' IndexInText='153' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</OperatorNameParts>
							</OperatorInfo>
							<Operand2.Literal Id='637840162079571516' IndexInText='154' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='z' Id='637840162079571507' IndexInText='154' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Operand2.Literal>
						</Operand2.BinaryOperator>
					</BinaryOperator>
					<ClosingRoundBrace Name=')' Id='637840162079571656' IndexInText='155' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Children>
					<BinaryOperator Name='+' Priority='30' Id='637840162079571589' IndexInText='66' ItemLength='89' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
				</Children>
				<OtherProperties>
					<NamedExpressionItem Id='637840162079478205' IndexInText='58' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					<OpeningBraceInfo Name='(' Id='637840162079478238' IndexInText='65' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ClosingBraceInfo Name=')' Id='637840162079571656' IndexInText='155' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Parameters ObjectId='41' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name='+' Priority='30' Id='637840162079571589' IndexInText='66' ItemLength='89' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
					</Parameters>
				</OtherProperties>
			</Braces>
			<ExpressionSeparator Name=';' Id='637840162079571698' IndexInText='156' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
		</RegularItems>
		<Children>
			<BinaryOperator Name='=' Priority='2000' Id='637840162079477999' IndexInText='17' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
			<Braces Id='637840162079478234' IndexInText='58' ItemLength='98' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
		</Children>
	</ExpressionItemSeries>
	<ParseErrorData ObjectId='43' HasCriticalErrors='False' Interface='UniversalExpressionParser.IParseErrorData'>
		<AllCodeItemErrors ObjectId='44' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICodeItemParseErrorData]'/>
	</ParseErrorData>
	<SortedCommentedOutCodeInfos ObjectId='45' Count='4' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICommentedOutCodeInfo]'>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='46' IsLineComment='True' IndexInText='0' ItemLength='15'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='47' IsLineComment='True' IndexInText='30' ItemLength='24'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='48' IsLineComment='False' IndexInText='69' ItemLength='80'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='49' IsLineComment='False' IndexInText='161' ItemLength='61'/>
	</SortedCommentedOutCodeInfos>
	<MainExpressionItem Id='637840162079395412' IndexInText='17' ItemLength='140' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'/>
</UniversalExpressionParser.ExpressionItems.IRootExpressionItemSeries>
```
</details>

<details> <summary>Click to expand the interface UniversalExpressionParser.ICommentedOutCodeInfo</summary>

```XML
using UniversalExpressionParser.ExpressionItems;

namespace UniversalExpressionParser
{
    /// <summary>
    /// Info on commented out code block.
    /// </summary>
    public interface ICommentedOutCodeInfo: ITextItem
    {
        /// <summary>
        /// If true, the comment is a line comment. Otherwise, it is a block comment.
        /// </summary>
        bool IsLineComment { get; }
    }
}
```
</details>


# Error Reporting

Information on errors is stored in property **IReadOnlyList<ICodeItemParseErrorData> AllCodeItemErrors { get; }** in **UniversalExpressionParser.ExpressionItems.IRootExpressionItem** an instance of which is returned by the parser.

<details> <summary>Click to expand UniversalExpressionParser.ICodeItemParseErrorData</summary>

```XML
using JetBrains.Annotations;
using UniversalExpressionParser.ExpressionItems.Custom;

namespace UniversalExpressionParser
{
    /// <summary>
    /// Code item error data.
    /// </summary>
    public interface ICodeItemParseErrorData
    {
        /// <summary>
        /// Error index in evaluated text.
        /// </summary>
        int ErrorIndexInParsedText { get; }

        /// <summary>
        /// Error message.
        /// </summary>
        [NotNull]
        string ErrorMessage { get; }

        /// <summary>
        /// Parse error type. Look at <see cref="CodeParseErrorType"/> for predefined error codes.
        /// </summary>
        int CodeParseErrorType { get; }

        /// <summary>
        /// If the value, parsing will stop after this error is added by the custom expression parser <see cref="ICustomExpressionItemParser"/>,
        /// parsing will stop on error and rest of the expression will not be parsed ny <see cref="IExpressionParser"/>.
        /// Note, the same error code might be considered as critical error in one context, and non-critical in some other context.
        /// </summary>
        bool IsCriticalError { get; }
    }
}
```
</details>

The extensions class **UniversalExpressionParser.ExpressionItems.RootExpressionItemExtensionMethods** has a helper method **string GetErrorContext(this IRootExpressionItem parsedRootExpressionItem, int parsedTextStartPosition, int parsedTextEnd, int maxNumberOfCharactersToShowBeforeOrAfterErrorPosition = 50)** for returning a string with error details and contextual data (i.e., text before and after the position where error happened along with arrow pointing to the error). 


- Below is an expression which has several errors:

```csharp
var x = y /*operator is missing here*/x;

{ // This code block is not closed
    f1(x, y, /*function parameter is missing here*/)
    {

        var z = ++x + y + /*' +' is not a postfix and operand is missing */;
        return + /*' +' is not a postfix and operand is missing */ z + y;
    }
// Closing curly brace is missing here

```

<details> <summary>Click to expand the parsed expression</summary>

```XML
<?xml version="1.0" encoding="utf-8"?>
<UniversalExpressionParser.ExpressionItems.IRootExpressionItemSeries ObjectId='0' PositionInTextOnCompletion='343' IndexInText='0' ItemLength='341'>
	<ExpressionItemSeries Id='637840162079889208' IndexInText='0' ItemLength='301' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'>
		<RegularItems>
			<Operators Id='637840162079907233' IndexInText='0' ItemLength='39' Interface='UniversalExpressionParser.ExpressionItems.IComplexExpressionItem'>
				<RegularItems>
					<Literal Id='637840162079889577' IndexInText='0' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='x' Id='637840162079889572' IndexInText='4' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<AppliedKeywords>
							<Keyword Name='var' Id='637840162079889434' IndexInText='0' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
								<LanguageKeywordInfo ObjectId='6' Id='637781064051574641' Keyword='var' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
							</Keyword>
						</AppliedKeywords>
					</Literal>
					<OperatorInfo OperatorType='BinaryOperator' Name='=' Priority='2000' Id='637840162079889682' IndexInText='6' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
						<OperatorNameParts>
							<Name Name='=' Id='637840162079889666' IndexInText='6' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OperatorNameParts>
					</OperatorInfo>
					<Literal Id='637840162079889819' IndexInText='8' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='y' Id='637840162079889815' IndexInText='8' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<Literal Id='637840162079905442' IndexInText='38' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='x' Id='637840162079905390' IndexInText='38' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
				</RegularItems>
			</Operators>
			<ExpressionSeparator Name=';' Id='637840162079907642' IndexInText='39' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<CodeBlock Id='637840162079907731' IndexInText='44' ItemLength='257' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
				<RegularItems>
					<CodeBlockStartMarker Name='{' Id='637840162079907725' IndexInText='44' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Braces Id='637840162079907948' IndexInText='84' ItemLength='217' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<Literal Id='637840162079907913' IndexInText='84' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='f1' Id='637840162079907907' IndexInText='84' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<OpeningRoundBrace Name='(' Id='637840162079907953' IndexInText='86' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Literal Id='637840162079908050' IndexInText='87' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='x' Id='637840162079908046' IndexInText='87' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<Comma Name=',' Id='637840162079908083' IndexInText='88' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Literal Id='637840162079908199' IndexInText='90' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='y' Id='637840162079908195' IndexInText='90' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<Comma Name=',' Id='637840162079908230' IndexInText='91' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingRoundBrace Name=')' Id='637840162079908333' IndexInText='131' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Postfixes>
							<CodeBlock Id='637840162079908379' IndexInText='138' ItemLength='163' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
								<RegularItems>
									<CodeBlockStartMarker Name='{' Id='637840162079908375' IndexInText='138' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Operators Id='637840162079910162' IndexInText='151' ItemLength='17' Interface='UniversalExpressionParser.ExpressionItems.IComplexExpressionItem'>
										<RegularItems>
											<Literal Id='637840162079908506' IndexInText='151' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
												<NameExpressionItem Name='z' Id='637840162079908502' IndexInText='155' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												<AppliedKeywords>
													<Keyword Name='var' Id='637840162079908410' IndexInText='151' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
														<LanguageKeywordInfo ObjectId='6' Id='637781064051574641' Keyword='var' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
													</Keyword>
												</AppliedKeywords>
											</Literal>
											<OperatorInfo OperatorType='BinaryOperator' Name='=' Priority='2000' Id='637840162079908587' IndexInText='157' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
												<OperatorNameParts>
													<Name Name='=' Id='637840162079908579' IndexInText='157' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												</OperatorNameParts>
											</OperatorInfo>
											<OperatorInfo OperatorType='PrefixUnaryOperator' Name='++' Priority='0' Id='637840162079908684' IndexInText='159' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
												<OperatorNameParts>
													<Name Name='++' Id='637840162079908678' IndexInText='159' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												</OperatorNameParts>
											</OperatorInfo>
											<Literal Id='637840162079908794' IndexInText='161' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
												<NameExpressionItem Name='x' Id='637840162079908790' IndexInText='161' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											</Literal>
											<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637840162079908903' IndexInText='163' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
												<OperatorNameParts>
													<Name Name='+' Id='637840162079908897' IndexInText='163' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												</OperatorNameParts>
											</OperatorInfo>
											<Literal Id='637840162079909037' IndexInText='165' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
												<NameExpressionItem Name='y' Id='637840162079909033' IndexInText='165' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											</Literal>
											<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637840162079909095' IndexInText='167' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
												<OperatorNameParts>
													<Name Name='+' Id='637840162079909090' IndexInText='167' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												</OperatorNameParts>
											</OperatorInfo>
										</RegularItems>
									</Operators>
									<ExpressionSeparator Name=';' Id='637840162079910194' IndexInText='218' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Operators Id='637840162079910728' IndexInText='229' ItemLength='64' Interface='UniversalExpressionParser.ExpressionItems.IComplexExpressionItem'>
										<RegularItems>
											<OperatorInfo OperatorType='PrefixUnaryOperator' Name='return' Priority='2147483647' Id='637840162079910269' IndexInText='229' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
												<OperatorNameParts>
													<Name Name='return' Id='637840162079910263' IndexInText='229' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												</OperatorNameParts>
											</OperatorInfo>
											<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637840162079910331' IndexInText='236' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
												<OperatorNameParts>
													<Name Name='+' Id='637840162079910325' IndexInText='236' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												</OperatorNameParts>
											</OperatorInfo>
											<Literal Id='637840162079910488' IndexInText='288' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
												<NameExpressionItem Name='z' Id='637840162079910483' IndexInText='288' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											</Literal>
											<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637840162079910559' IndexInText='290' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
												<OperatorNameParts>
													<Name Name='+' Id='637840162079910554' IndexInText='290' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												</OperatorNameParts>
											</OperatorInfo>
											<Literal Id='637840162079910690' IndexInText='292' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
												<NameExpressionItem Name='y' Id='637840162079910686' IndexInText='292' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											</Literal>
										</RegularItems>
									</Operators>
									<ExpressionSeparator Name=';' Id='637840162079910738' IndexInText='293' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<CodeBlockEndMarker Name='}' Id='637840162079910764' IndexInText='300' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Children>
									<Operators Id='637840162079910162' IndexInText='151' ItemLength='17' Interface='UniversalExpressionParser.ExpressionItems.IComplexExpressionItem'/>
									<Operators Id='637840162079910728' IndexInText='229' ItemLength='64' Interface='UniversalExpressionParser.ExpressionItems.IComplexExpressionItem'/>
								</Children>
								<OtherProperties>
									<CodeBlockStartMarker Name='{' Id='637840162079908375' IndexInText='138' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<CodeBlockEndMarker Name='}' Id='637840162079910764' IndexInText='300' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</OtherProperties>
							</CodeBlock>
						</Postfixes>
						<Children>
							<Literal Id='637840162079908050' IndexInText='87' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							<Literal Id='637840162079908199' IndexInText='90' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
						</Children>
						<OtherProperties>
							<NamedExpressionItem Id='637840162079907913' IndexInText='84' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							<OpeningBraceInfo Name='(' Id='637840162079907953' IndexInText='86' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=')' Id='637840162079908333' IndexInText='131' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Parameters ObjectId='59' Count='3' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162079908050' IndexInText='87' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162079908199' IndexInText='90' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase value='null' interface='UniversalExpressionParser.ExpressionItems.IExpressionItemBase' />
							</Parameters>
						</OtherProperties>
					</Braces>
				</RegularItems>
				<Children>
					<Braces Id='637840162079907948' IndexInText='84' ItemLength='217' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
				</Children>
				<OtherProperties>
					<CodeBlockStartMarker Name='{' Id='637840162079907725' IndexInText='44' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<CodeBlockEndMarker value='null' interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem' />
				</OtherProperties>
			</CodeBlock>
		</RegularItems>
		<Children>
			<Operators Id='637840162079907233' IndexInText='0' ItemLength='39' Interface='UniversalExpressionParser.ExpressionItems.IComplexExpressionItem'/>
			<CodeBlock Id='637840162079907731' IndexInText='44' ItemLength='257' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'/>
		</Children>
	</ExpressionItemSeries>
	<ParseErrorData ObjectId='60' HasCriticalErrors='False' Interface='UniversalExpressionParser.IParseErrorData'>
		<AllCodeItemErrors ObjectId='61' Count='5' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICodeItemParseErrorData]'>
			<UniversalExpressionParser.ICodeItemParseErrorData ObjectId='62' ErrorIndexInParsedText='38'
			  ErrorMessage='No separation between two symbols. Here are some invalid expressions that would result in this error, and expressions resulted from invalid expressions by small correction.$line_break$Invalid expression: "F(x y)". Fixed valid expressions: "F(x + y)", "F(x, y)".$line_break$Invalid expression: "{x=F1(x) F2(y)}". Fixed valid expressions: "{x=F1(x) + F2(y)}", "{x=F1(x); F2(y)}".'
			  CodeParseErrorType='200' IsCriticalError='False'/>
			<UniversalExpressionParser.ICodeItemParseErrorData ObjectId='63' ErrorIndexInParsedText='92' ErrorMessage='Valid expression is missing after comma.' CodeParseErrorType='701' IsCriticalError='False'/>
			<UniversalExpressionParser.ICodeItemParseErrorData ObjectId='64' ErrorIndexInParsedText='167' ErrorMessage='Expected a postfix operator.' CodeParseErrorType='503' IsCriticalError='False'/>
			<UniversalExpressionParser.ICodeItemParseErrorData ObjectId='65' ErrorIndexInParsedText='236' ErrorMessage='Expected a prefix operator.' CodeParseErrorType='502' IsCriticalError='False'/>
			<UniversalExpressionParser.ICodeItemParseErrorData ObjectId='66' ErrorIndexInParsedText='44' ErrorMessage='Code block end marker ''}'' is missing for ''{''.' CodeParseErrorType='406' IsCriticalError='False'/>
		</AllCodeItemErrors>
	</ParseErrorData>
	<SortedCommentedOutCodeInfos ObjectId='67' Count='6' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICommentedOutCodeInfo]'>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='68' IsLineComment='False' IndexInText='10' ItemLength='28'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='69' IsLineComment='True' IndexInText='46' ItemLength='32'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='70' IsLineComment='False' IndexInText='93' ItemLength='38'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='71' IsLineComment='False' IndexInText='169' ItemLength='49'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='72' IsLineComment='False' IndexInText='238' ItemLength='49'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='73' IsLineComment='True' IndexInText='303' ItemLength='38'/>
	</SortedCommentedOutCodeInfos>
	<MainExpressionItem Id='637840162079889208' IndexInText='0' ItemLength='301' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'/>
</UniversalExpressionParser.ExpressionItems.IRootExpressionItemSeries>
```
</details>

- Errors reported by the parser for the expression above using the helper extension method **UniversalExpressionParser.ExpressionItems.RootExpressionItemExtensionMethods.GetErrorContext(...)**.

<details> <summary>Click to expand the file with reported error messages</summary>

```
2022-01-31 23:07:59,555 ERROR : Expression parse errors:

Parse error details: Error code: 200, Error index: 38. Error message: [No separation between two symbols. Here are some invalid expressions that would result in this error, and expressions resulted from invalid expressions by small correction.
Invalid expression: "F(x y)". Fixed valid expressions: "F(x + y)", "F(x, y)".
Invalid expression: "{x=F1(x) F2(y)}". Fixed valid expressions: "{x=F1(x) + F2(y)}", "{x=F1(x); F2(y)}".]
Error context:
var x = y /*operator is missing here*/x;
--------------------------------------^

{ // This code block is not closed
    f1(x

Parse error details: Error code: 406, Error index: 44. Error message: [Code block end marker '}' is missing for '{'.]
Error context:
var x = y /*operator is missing here*/x;
----------------------------------------

{ // This code block is not closed
^
    f1(x, y, /

Parse error details: Error code: 701, Error index: 92. Error message: [Valid expression is missing after comma.]
Error context:

{ // This code block is not closed
----------------------------------
    f1(x, y, /*function parameter is missing here*/)
------------^
    {



Parse error details: Error code: 503, Error index: 167. Error message: [Expected a postfix operator.]
Error context:
missing here*/)
---------------
    {
-----

        var z = ++x + y + /*' +' is not a postfix and operand is missing *
------------------------^

Parse error details: Error code: 502, Error index: 236. Error message: [Expected a prefix operator.]
Error context:
ostfix and operand is missing */;
---------------------------------
        return + /*' +' is not a postfix and operand is missing *
---------------^
```
</details>

# Parsing Section in Text

- Sometimes we want to parse a single braces expression at specific location in text (i.e., an expression starting with "(" or "[" and ending in ")" or "]" correspondingly) or single code block expression (i.e., an expression starting with **UniversalExpressionParser.IExpressionLanguageProvider.CodeBlockStartMarker** and ending in **UniversalExpressionParser.IExpressionLanguageProvider.CodeBlockEndMarker**). In these scenarios, we want the parser to stop right after fully parsing the braces or code block expression.

- The interface **UniversalExpressionParser.IExpressionParser** has two methods for doing just that. 
 
- The methods for parsing single braces or code block expression are **UniversalExpressionParser.ExpressionItems.IRootBracesExpressionItem ParseBracesExpressionItem(IExpressionLanguageProvider expressionLanguageProvider, string expressionText, IParseBracesExpressionOptions parseBracesExpressionOptions)** and **IRootCodeBlockExpressionItem ParseCodeBlockExpressionItem(IExpressionLanguageProvider expressionLanguageProvider, string expressionText, IParseCodeBlockExpressionOptions parseCodeBlockExpressionOptions)**, and are demonstrated in sub-sections below.

- The parsed expression of type **UniversalExpressionParser.ExpressionItems.IRootExpressionItem** returned by these methods has a property **int PositionInTextOnCompletion { get; }** that stores the position of text, after the parsing is complete (i.e., the position after closing brace or code block end marker).

## Parsing Single Round Braces Expression at Specific Location

- Below is an an SQLite table definition in which we want to parse only the braces expression **(SALARY > 0 AND SALARY > MAX_SALARY/2 AND f1(SALARY) < f2(MAX_SALARY))**, and stop parsing right after the closing brace ')'.

```csharp
CREATE TABLE COMPANY(
	ID INT PRIMARY KEY     NOT NULL,
	MAX_SALARY     REAL,
	/* The parser will only parse expression 
	(SALARY > 0 AND SALARY > MAX_SALARY/2 AND f1(SALARY)<f2(MAX_SALARY)) and will stop right after the 
	closing round brace ')' of in this expression. */
	AVG_SALARY     REAL    
			CHECK(SALARY > 0 AND 
				  SALARY > MAX_SALARY/2 AND 
				  f1(SALARY) < f2(MAX_SALARY)),	
	ADDRESS        CHAR(50)
);
```

- The method **ParseBracesAtCurrentPosition(string expression, int positionInText)** in class **UniversalExpressionParser.Tests.Demos.ParseSingleBracesExpressionAtPositionDemo** demonstrates how to parse the braces expression **(SALARY > 0 AND SALARY > MAX_SALARY/2 AND f1(SALARY) < f2(MAX_SALARY))**, by passing the position of opening brace in parameter **positionInText**.

<details> <summary>Click to expand the class UniversalExpressionParser.Tests.Demos.ParseSingleBracesExpressionAtPositionDemo</summary>

```XML
using TextParser;
using UniversalExpressionParser.DemoExpressionLanguageProviders;
using UniversalExpressionParser.ExpressionItems;

namespace UniversalExpressionParser.Tests.Demos
{
    public class ParseSingleBracesExpressionAtPositionDemo
    {
        private readonly IExpressionParser _expressionParser;
        private readonly IExpressionLanguageProvider _expressionLanguageProvider = new NonVerboseCaseSensitiveExpressionLanguageProvider();
       
        public ParseSingleBracesExpressionAtPositionDemo()
        {
            IExpressionLanguageProviderCache expressionLanguageProviderCache = 
                new ExpressionLanguageProviderCache(new DefaultExpressionLanguageProviderValidator());
            
            _expressionParser = new ExpressionParser(new TextSymbolsParserFactory(), expressionLanguageProviderCache);
            expressionLanguageProviderCache.RegisterExpressionLanguageProvider(_expressionLanguageProvider);
        }

        public IRootBracesExpressionItem ParseBracesAtCurrentPosition(string expression, int positionInText)
        {
            // _expressionParser.ParseBracesExpressionItem tries to parse single square or round braces expression like "[f1()+m1[], f2{++i;}]" within
            // which there can be any number of other valid expressions, such as operators, other braces expressions, code block expressions, etc.
            // For example the text expression can be
            // "any text before braces[f1()+m1[], f2{++i;}]any text after braces including more braces expressions that will not be parsed".
            // Parsing starts at position IParseBracesExpressionOptions.StartIndex, and stops at position after the closing square or round braces
            // corresponding to opening square or round brace at position IParseBracesExpressionOptions.StartIndex.
            // Note, if the text has more braces expressions after the closing square or round brace, they will not be parsed (i.e., the method
            // parses only the first braces expression at position IParseBracesExpressionOptions.StartIndex).
            // When parsing is complete, IRootExpressionItem.PositionInTextOnCompletion has the position after the closing brace
            // for the parsed braces expression at position IParseBracesExpressionOptions.StartIndex.
            
            return _expressionParser.ParseBracesExpressionItem(_expressionLanguageProvider, expression, new ParseBracesExpressionOptions
            {
                StartIndex = positionInText
            });
        }
    }
}

```
</details>

<details> <summary>Click to expand the single braces expression parsed from text above is shown here</summary>

```XML
<?xml version="1.0" encoding="utf-8"?>
<UniversalExpressionParser.ExpressionItems.IRootBracesExpressionItem ObjectId='0' PositionInTextOnCompletion='399' IndexInText='313' ItemLength='86'>
	<BracesExpressionItem Id='637840162082771811' IndexInText='313' ItemLength='86' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
		<RegularItems>
			<OpeningRoundBrace Name='(' Id='637840162082771855' IndexInText='313' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<BinaryOperator Name='AND' Priority='80' Id='637840162082966327' IndexInText='314' ItemLength='84' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
				<Operand1.BinaryOperator Name='AND' Priority='80' Id='637840162082966303' IndexInText='314' ItemLength='44' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
					<Operand1.BinaryOperator Name='>' Priority='50' Id='637840162082966270' IndexInText='314' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.Literal Id='637840162082773329' IndexInText='314' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='SALARY' Id='637840162082773315' IndexInText='314' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand1.Literal>
						<OperatorInfo OperatorType='BinaryOperator' Name='>' Priority='50' Id='637840162082773424' IndexInText='321' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<Name Name='>' Id='637840162082773412' IndexInText='321' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OperatorNameParts>
						</OperatorInfo>
						<Operand2.ConstantNumericValue Id='637840162082857860' IndexInText='323' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
							<NameExpressionItem Name='0' Id='637840162082857814' IndexInText='323' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<OtherProperties>
								<SucceededNumericTypeDescriptor ObjectId='12' NumberTypeId='1581134183960' Interface='UniversalExpressionParser.NumericTypeDescriptor'>
									<RegularExpressions ObjectId='13' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[System.String]'>
										<System.String value='^\d+' />
									</RegularExpressions>
								</SucceededNumericTypeDescriptor>
							</OtherProperties>
						</Operand2.ConstantNumericValue>
					</Operand1.BinaryOperator>
					<OperatorInfo OperatorType='BinaryOperator' Name='AND' Priority='80' Id='637840162082858127' IndexInText='325' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
						<OperatorNameParts>
							<Name Name='AND' Id='637840162082858116' IndexInText='325' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OperatorNameParts>
					</OperatorInfo>
					<Operand2.BinaryOperator Name='>' Priority='50' Id='637840162082966311' IndexInText='337' ItemLength='21' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.Literal Id='637840162082858290' IndexInText='337' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='SALARY' Id='637840162082858285' IndexInText='337' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand1.Literal>
						<OperatorInfo OperatorType='BinaryOperator' Name='>' Priority='50' Id='637840162082858366' IndexInText='344' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<Name Name='>' Id='637840162082858357' IndexInText='344' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OperatorNameParts>
						</OperatorInfo>
						<Operand2.BinaryOperator Name='/' Priority='20' Id='637840162082966321' IndexInText='346' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
							<Operand1.Literal Id='637840162082858492' IndexInText='346' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='MAX_SALARY' Id='637840162082858489' IndexInText='346' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Operand1.Literal>
							<OperatorInfo OperatorType='BinaryOperator' Name='/' Priority='20' Id='637840162082858548' IndexInText='356' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
								<OperatorNameParts>
									<Name Name='/' Id='637840162082858543' IndexInText='356' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</OperatorNameParts>
							</OperatorInfo>
							<Operand2.ConstantNumericValue Id='637840162082965175' IndexInText='357' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
								<NameExpressionItem Name='2' Id='637840162082965112' IndexInText='357' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<OtherProperties>
									<SucceededNumericTypeDescriptor ObjectId='12' NumberTypeId='1581134183960' Interface='UniversalExpressionParser.NumericTypeDescriptor'/>
								</OtherProperties>
							</Operand2.ConstantNumericValue>
						</Operand2.BinaryOperator>
					</Operand2.BinaryOperator>
				</Operand1.BinaryOperator>
				<OperatorInfo OperatorType='BinaryOperator' Name='AND' Priority='80' Id='637840162082965482' IndexInText='359' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
					<OperatorNameParts>
						<Name Name='AND' Id='637840162082965470' IndexInText='359' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</OperatorNameParts>
				</OperatorInfo>
				<Operand2.BinaryOperator Name='<' Priority='50' Id='637840162082966333' IndexInText='371' ItemLength='27' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
					<Operand1.Braces Id='637840162082965711' IndexInText='371' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<Literal Id='637840162082965666' IndexInText='371' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='f1' Id='637840162082965661' IndexInText='371' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<OpeningRoundBrace Name='(' Id='637840162082965715' IndexInText='373' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Literal Id='637840162082965814' IndexInText='374' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='SALARY' Id='637840162082965810' IndexInText='374' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<ClosingRoundBrace Name=')' Id='637840162082965865' IndexInText='380' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<Literal Id='637840162082965814' IndexInText='374' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
						</Children>
						<OtherProperties>
							<NamedExpressionItem Id='637840162082965666' IndexInText='371' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							<OpeningBraceInfo Name='(' Id='637840162082965715' IndexInText='373' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=')' Id='637840162082965865' IndexInText='380' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Parameters ObjectId='38' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162082965814' IndexInText='374' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Operand1.Braces>
					<OperatorInfo OperatorType='BinaryOperator' Name='<' Priority='50' Id='637840162082965937' IndexInText='382' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
						<OperatorNameParts>
							<Name Name='<' Id='637840162082965932' IndexInText='382' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OperatorNameParts>
					</OperatorInfo>
					<Operand2.Braces Id='637840162082966098' IndexInText='384' ItemLength='14' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<Literal Id='637840162082966067' IndexInText='384' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='f2' Id='637840162082966063' IndexInText='384' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<OpeningRoundBrace Name='(' Id='637840162082966101' IndexInText='386' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Literal Id='637840162082966190' IndexInText='387' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='MAX_SALARY' Id='637840162082966186' IndexInText='387' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<ClosingRoundBrace Name=')' Id='637840162082966219' IndexInText='397' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<Literal Id='637840162082966190' IndexInText='387' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
						</Children>
						<OtherProperties>
							<NamedExpressionItem Id='637840162082966067' IndexInText='384' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							<OpeningBraceInfo Name='(' Id='637840162082966101' IndexInText='386' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=')' Id='637840162082966219' IndexInText='397' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Parameters ObjectId='48' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162082966190' IndexInText='387' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Operand2.Braces>
				</Operand2.BinaryOperator>
			</BinaryOperator>
			<ClosingRoundBrace Name=')' Id='637840162082966416' IndexInText='398' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
		</RegularItems>
		<Children>
			<BinaryOperator Name='AND' Priority='80' Id='637840162082966327' IndexInText='314' ItemLength='84' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
		</Children>
		<OtherProperties>
			<OpeningBraceInfo Name='(' Id='637840162082771855' IndexInText='313' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<ClosingBraceInfo Name=')' Id='637840162082966416' IndexInText='398' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
			<Parameters ObjectId='50' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
				<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name='AND' Priority='80' Id='637840162082966327' IndexInText='314' ItemLength='84' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
			</Parameters>
		</OtherProperties>
	</BracesExpressionItem>
	<ParseErrorData ObjectId='51' HasCriticalErrors='False' Interface='UniversalExpressionParser.IParseErrorData'>
		<AllCodeItemErrors ObjectId='52' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICodeItemParseErrorData]'/>
	</ParseErrorData>
	<SortedCommentedOutCodeInfos ObjectId='53' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICommentedOutCodeInfo]'/>
	<MainExpressionItem Id='637840162082771811' IndexInText='313' ItemLength='86' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
</UniversalExpressionParser.ExpressionItems.IRootBracesExpressionItem>
```
</details>

- Here is square braces expression **[f1()+m1[], f2{++i;}]** between texts 'any text before braces' and 'any text after braces...', which can also be parsed using the code in class **UniversalExpressionParser.Tests.Demos.ParseSingleBracesExpressionAtPositionDemo**.

```csharp
any text before braces[f1()+m1[], f2
{
	++i;
}]any text after braces including more braces expressions that will not be parsed
```

<details> <summary>Click to expand the parsed expression</summary>

```XML
<?xml version="1.0" encoding="utf-8"?>
<UniversalExpressionParser.ExpressionItems.IRootBracesExpressionItem ObjectId='0' PositionInTextOnCompletion='50' IndexInText='22' ItemLength='28'>
	<BracesExpressionItem Id='637840162083053804' IndexInText='22' ItemLength='28' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
		<RegularItems>
			<OpeningSquareBrace Name='[' Id='637840162083053846' IndexInText='22' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<BinaryOperator Name='+' Priority='30' Id='637840162083054750' IndexInText='23' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
				<Operand1.Braces Id='637840162083054343' IndexInText='23' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
					<RegularItems>
						<Literal Id='637840162083054272' IndexInText='23' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='f1' Id='637840162083054264' IndexInText='23' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Literal>
						<OpeningRoundBrace Name='(' Id='637840162083054347' IndexInText='25' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<ClosingRoundBrace Name=')' Id='637840162083054399' IndexInText='26' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</RegularItems>
					<OtherProperties>
						<NamedExpressionItem Id='637840162083054272' IndexInText='23' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
						<OpeningBraceInfo Name='(' Id='637840162083054347' IndexInText='25' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<ClosingBraceInfo Name=')' Id='637840162083054399' IndexInText='26' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<Parameters ObjectId='9' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'/>
					</OtherProperties>
				</Operand1.Braces>
				<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637840162083054484' IndexInText='27' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
					<OperatorNameParts>
						<Name Name='+' Id='637840162083054474' IndexInText='27' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</OperatorNameParts>
				</OperatorInfo>
				<Operand2.Braces Id='637840162083054676' IndexInText='28' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
					<RegularItems>
						<Literal Id='637840162083054634' IndexInText='28' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='m1' Id='637840162083054630' IndexInText='28' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Literal>
						<OpeningSquareBrace Name='[' Id='637840162083054679' IndexInText='30' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<ClosingSquareBrace Name=']' Id='637840162083054705' IndexInText='31' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</RegularItems>
					<OtherProperties>
						<NamedExpressionItem Id='637840162083054634' IndexInText='28' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
						<OpeningBraceInfo Name='[' Id='637840162083054679' IndexInText='30' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<ClosingBraceInfo Name=']' Id='637840162083054705' IndexInText='31' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<Parameters ObjectId='17' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'/>
					</OtherProperties>
				</Operand2.Braces>
			</BinaryOperator>
			<Comma Name=',' Id='637840162083054833' IndexInText='32' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<Literal Id='637840162083054918' IndexInText='34' ItemLength='15' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
				<NameExpressionItem Name='f2' Id='637840162083054914' IndexInText='34' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				<Postfixes>
					<CodeBlock Id='637840162083054954' IndexInText='38' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
						<RegularItems>
							<CodeBlockStartMarker Name='{' Id='637840162083054949' IndexInText='38' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<PrefixUnaryOperator Name='++' Priority='0' Id='637840162083055155' IndexInText='42' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
								<OperatorInfo OperatorType='PrefixUnaryOperator' Name='++' Priority='0' Id='637840162083055002' IndexInText='42' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
									<OperatorNameParts>
										<Name Name='++' Id='637840162083054996' IndexInText='42' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</OperatorNameParts>
								</OperatorInfo>
								<Operand1.Literal Id='637840162083055111' IndexInText='44' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
									<NameExpressionItem Name='i' Id='637840162083055107' IndexInText='44' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</Operand1.Literal>
							</PrefixUnaryOperator>
							<ExpressionSeparator Name=';' Id='637840162083055194' IndexInText='45' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637840162083055227' IndexInText='48' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<PrefixUnaryOperator Name='++' Priority='0' Id='637840162083055155' IndexInText='42' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
						</Children>
						<OtherProperties>
							<CodeBlockStartMarker Name='{' Id='637840162083054949' IndexInText='38' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637840162083055227' IndexInText='48' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OtherProperties>
					</CodeBlock>
				</Postfixes>
			</Literal>
			<ClosingSquareBrace Name=']' Id='637840162083055258' IndexInText='49' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
		</RegularItems>
		<Children>
			<BinaryOperator Name='+' Priority='30' Id='637840162083054750' IndexInText='23' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
			<Literal Id='637840162083054918' IndexInText='34' ItemLength='15' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
		</Children>
		<OtherProperties>
			<OpeningBraceInfo Name='[' Id='637840162083053846' IndexInText='22' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<ClosingBraceInfo Name=']' Id='637840162083055258' IndexInText='49' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
			<Parameters ObjectId='31' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
				<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name='+' Priority='30' Id='637840162083054750' IndexInText='23' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
				<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162083054918' IndexInText='34' ItemLength='15' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
			</Parameters>
		</OtherProperties>
	</BracesExpressionItem>
	<ParseErrorData ObjectId='32' HasCriticalErrors='False' Interface='UniversalExpressionParser.IParseErrorData'>
		<AllCodeItemErrors ObjectId='33' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICodeItemParseErrorData]'/>
	</ParseErrorData>
	<SortedCommentedOutCodeInfos ObjectId='34' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICommentedOutCodeInfo]'/>
	<MainExpressionItem Id='637840162083053804' IndexInText='22' ItemLength='28' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
</UniversalExpressionParser.ExpressionItems.IRootBracesExpressionItem>
```
</details>

## Parsing Single Code Block Expression at Specific Location

Below is a text with code block expression **{f1(f2()+m1[], f2{++i;})}** between texts 'any text before code block' and 'any text after code block...' that we want to parse.

```csharp
any text before braces[f1()+m1[], f2
{
	++i;
}]any text after braces including more braces expressions that will not be parsed
```


- The method **IRootCodeBlockExpressionItem ParseCodeBlockExpressionItemAtCurrentPosition(string expression, int positionInText)** in class **UniversalExpressionParser.Tests.Demos.ParseSingleCodeBlockExpressionAtPositionDemo** demonstrates how to parse the single code block expression **{f1(f2()+m1[], f2{++i;})}**, by passing the position of code block start marker '{' in parameter **positionInText**.

<details> <summary>Click to expand the class UniversalExpressionParser.Tests.Demos.ParseSingleCodeBlockExpressionAtPositionDemo</summary>

```XML
using TextParser;
using UniversalExpressionParser.DemoExpressionLanguageProviders;
using UniversalExpressionParser.ExpressionItems;

namespace UniversalExpressionParser.Tests.Demos
{
    public class ParseSingleCodeBlockExpressionAtPositionDemo
    {
        private readonly IExpressionParser _expressionParser;
        private readonly IExpressionLanguageProvider _expressionLanguageProvider = new NonVerboseCaseSensitiveExpressionLanguageProvider();
       
        public ParseSingleCodeBlockExpressionAtPositionDemo()
        {
            IExpressionLanguageProviderCache expressionLanguageProviderCache = 
                new ExpressionLanguageProviderCache(new DefaultExpressionLanguageProviderValidator());
            
            _expressionParser = new ExpressionParser(new TextSymbolsParserFactory(), expressionLanguageProviderCache);
            expressionLanguageProviderCache.RegisterExpressionLanguageProvider(_expressionLanguageProvider);
        }

        public IRootCodeBlockExpressionItem ParseCodeBlockExpressionItemAtCurrentPosition(string expression, int positionInText)
        {
            // _expressionParser.ParseBracesExpressionItem tries to parse single code block expression like "{f1(f2()+m1[], f2{++i;})}",
            // within which there can be any number of other valid expressions, such as operators, another braces expression, code block
            // expressions, etc.
            // For example the text expression can be
            // "any text before code block{f1(f2()+m1[], f2{++i;})}any text after code block including more code blocks that will not be parsed".
            // Parsing starts at position IParseCodeBlockExpressionOptions.StartIndex, and stops at position after the
            // code block end marker (i.e., '}' in this example) corresponding to code block start marker at
            // position IParseCodeBlockExpressionOptions.StartIndex.
            // Note, if the text has more code block expressions after the code block end marker, they will not be
            // parsed (i.e., the method parses only the first code block expression at position IParseCodeBlockExpressionOptions.StartIndex).
            // When parsing is complete, IRootExpressionItem.PositionInTextOnCompletion has the position after the code block end marker
            // for the parsed code block expression at position IParseCodeBlockExpressionOptions.StartIndex.
            
            return _expressionParser.ParseCodeBlockExpressionItem(_expressionLanguageProvider, expression, new ParseCodeBlockExpressionOptions
            {
                StartIndex = positionInText
            });
        }
    }
}

```
</details>

<details> <summary>Click to expand the parsed expression</summary>

```XML
<?xml version="1.0" encoding="utf-8"?>
<UniversalExpressionParser.ExpressionItems.IRootCodeBlockExpressionItem ObjectId='0' PositionInTextOnCompletion='57' IndexInText='28' ItemLength='29'>
	<CodeBlockExpressionItem Id='637840162082685373' IndexInText='28' ItemLength='29' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
		<RegularItems>
			<CodeBlockStartMarker Name='{' Id='637840162082685326' IndexInText='28' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<Braces Id='637840162082686944' IndexInText='31' ItemLength='23' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
				<RegularItems>
					<Literal Id='637840162082686886' IndexInText='31' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='f1' Id='637840162082686863' IndexInText='31' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<OpeningRoundBrace Name='(' Id='637840162082686949' IndexInText='33' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<BinaryOperator Name='+' Priority='30' Id='637840162082687509' IndexInText='34' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.Braces Id='637840162082687093' IndexInText='34' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
							<RegularItems>
								<Literal Id='637840162082687066' IndexInText='34' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
									<NameExpressionItem Name='f2' Id='637840162082687058' IndexInText='34' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</Literal>
								<OpeningRoundBrace Name='(' Id='637840162082687096' IndexInText='36' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<ClosingRoundBrace Name=')' Id='637840162082687135' IndexInText='37' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</RegularItems>
							<OtherProperties>
								<NamedExpressionItem Id='637840162082687066' IndexInText='34' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								<OpeningBraceInfo Name='(' Id='637840162082687096' IndexInText='36' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<ClosingBraceInfo Name=')' Id='637840162082687135' IndexInText='37' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<Parameters ObjectId='13' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'/>
							</OtherProperties>
						</Operand1.Braces>
						<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637840162082687214' IndexInText='38' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<Name Name='+' Id='637840162082687200' IndexInText='38' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OperatorNameParts>
						</OperatorInfo>
						<Operand2.Braces Id='637840162082687431' IndexInText='39' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
							<RegularItems>
								<Literal Id='637840162082687359' IndexInText='39' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
									<NameExpressionItem Name='m1' Id='637840162082687355' IndexInText='39' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</Literal>
								<OpeningSquareBrace Name='[' Id='637840162082687435' IndexInText='41' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<ClosingSquareBrace Name=']' Id='637840162082687465' IndexInText='42' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</RegularItems>
							<OtherProperties>
								<NamedExpressionItem Id='637840162082687359' IndexInText='39' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								<OpeningBraceInfo Name='[' Id='637840162082687435' IndexInText='41' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<ClosingBraceInfo Name=']' Id='637840162082687465' IndexInText='42' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<Parameters ObjectId='21' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'/>
							</OtherProperties>
						</Operand2.Braces>
					</BinaryOperator>
					<Comma Name=',' Id='637840162082687626' IndexInText='43' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Literal Id='637840162082687717' IndexInText='45' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='f2' Id='637840162082687713' IndexInText='45' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<Postfixes>
							<CodeBlock Id='637840162082687752' IndexInText='47' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
								<RegularItems>
									<CodeBlockStartMarker Name='{' Id='637840162082687748' IndexInText='47' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<PrefixUnaryOperator Name='++' Priority='0' Id='637840162082687959' IndexInText='48' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
										<OperatorInfo OperatorType='PrefixUnaryOperator' Name='++' Priority='0' Id='637840162082687806' IndexInText='48' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
											<OperatorNameParts>
												<Name Name='++' Id='637840162082687799' IndexInText='48' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											</OperatorNameParts>
										</OperatorInfo>
										<Operand1.Literal Id='637840162082687914' IndexInText='50' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
											<NameExpressionItem Name='i' Id='637840162082687910' IndexInText='50' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</Operand1.Literal>
									</PrefixUnaryOperator>
									<ExpressionSeparator Name=';' Id='637840162082687983' IndexInText='51' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<CodeBlockEndMarker Name='}' Id='637840162082688016' IndexInText='52' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Children>
									<PrefixUnaryOperator Name='++' Priority='0' Id='637840162082687959' IndexInText='48' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
								</Children>
								<OtherProperties>
									<CodeBlockStartMarker Name='{' Id='637840162082687748' IndexInText='47' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<CodeBlockEndMarker Name='}' Id='637840162082688016' IndexInText='52' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</OtherProperties>
							</CodeBlock>
						</Postfixes>
					</Literal>
					<ClosingRoundBrace Name=')' Id='637840162082688049' IndexInText='53' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Children>
					<BinaryOperator Name='+' Priority='30' Id='637840162082687509' IndexInText='34' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
					<Literal Id='637840162082687717' IndexInText='45' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
				</Children>
				<OtherProperties>
					<NamedExpressionItem Id='637840162082686886' IndexInText='31' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					<OpeningBraceInfo Name='(' Id='637840162082686949' IndexInText='33' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ClosingBraceInfo Name=')' Id='637840162082688049' IndexInText='53' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Parameters ObjectId='35' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name='+' Priority='30' Id='637840162082687509' IndexInText='34' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162082687717' IndexInText='45' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					</Parameters>
				</OtherProperties>
			</Braces>
			<CodeBlockEndMarker Name='}' Id='637840162082688081' IndexInText='56' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
		</RegularItems>
		<Children>
			<Braces Id='637840162082686944' IndexInText='31' ItemLength='23' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
		</Children>
		<OtherProperties>
			<CodeBlockStartMarker Name='{' Id='637840162082685326' IndexInText='28' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<CodeBlockEndMarker Name='}' Id='637840162082688081' IndexInText='56' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
		</OtherProperties>
	</CodeBlockExpressionItem>
	<ParseErrorData ObjectId='37' HasCriticalErrors='False' Interface='UniversalExpressionParser.IParseErrorData'>
		<AllCodeItemErrors ObjectId='38' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICodeItemParseErrorData]'/>
	</ParseErrorData>
	<SortedCommentedOutCodeInfos ObjectId='39' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICommentedOutCodeInfo]'/>
	<MainExpressionItem Id='637840162082685373' IndexInText='28' ItemLength='29' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'/>
</UniversalExpressionParser.ExpressionItems.IRootCodeBlockExpressionItem>
```
</details>

# Case Sensitivity and Non Standard Language Features

## Case Sensitivity

- Case sensitivity is controlled by property **bool IsLanguageCaseSensitive { get; }** in **UniversalExpressionParser.IExpressionLanguageProvider**.

- If the value of this property **IsLanguageCaseSensitive** is **true**, any two expressions are considered as different, if the expressions are the same, except for capitalization of some of the text (say "public class Dog" vs "Public ClaSS DOg"). Otherwise, if the value of property **IsLanguageCaseSensitive** is **false**, the capitalization of any expression items does not matter (i.e., parsing will succeed regardless of capitalization in expression).

- For example C# is considered a case sensitive language, and Visual Basic is considered case insensitive.

- The value of property **IsLanguageCaseSensitive** in default implementation **UniversalExpressionParser.ExpressionLanguageProviderBase** of **UniversalExpressionParser.IExpressionLanguageProvider** returns **true**.

- The expression below demonstrates parsing the expression by **UniversalExpressionParser.IExpressionLanguageProvider** with overridden **IsLanguageCaseSensitive** to return **false**.

## Non Standard Comment Markers

- The properties **string LineCommentMarker { get; }**, **string MultilineCommentStartMarker { get; }**, and **string MultilineCommentEndMarker { get; }** in **UniversalExpressionParser.IExpressionLanguageProvider** determine the line comment marker as well as code block comment start and end markers.

- The default implementation **UniversalExpressionParser.ExpressionLanguageProviderBase** of **UniversalExpressionParser.IExpressionLanguageProvider** returns "//", "/*", and "*/" for these properties to use C# like comments. However, other values can be used for these properties.

- The expression below demonstrates parsing the expression by **UniversalExpressionParser.IExpressionLanguageProvider** with overridden **LineCommentMarker**, **MultilineCommentStartMarker**, and **MultilineCommentEndMarker** to return "rem", "rem*", "*rem".

## Non Standard Code Separator Character and Code Bloc Markers

- The properties **char ExpressionSeparatorCharacter { get; }**, **string CodeBlockStartMarker { get; }**, and **string CodeBlockEndMarker { get; }** in **UniversalExpressionParser.IExpressionLanguageProvider** determine the code separator character, as well as the code block start and end markers.

- The default implementation **UniversalExpressionParser.ExpressionLanguageProviderBase** of **UniversalExpressionParser.IExpressionLanguageProvider** returns ";", "{", and "}" for these properties to use C# like code separator and code block markers. However, other values can be used for these properties.

- The expression below demonstrates parsing the expression by **UniversalExpressionParser.IExpressionLanguageProvider** with overridden **ExpressionSeparatorCharacter**, **CodeBlockStartMarker**, and **CodeBlockEndMarker** to return ";", "BEGIN", and "END".

## Example Demonstrating Case Insensitivity and Non Standard Language Features
 
- The expression below is parsed using the expression language provider **UniversalExpressionParser.DemoExpressionLanguageProviders.VerboseCaseInsensitiveExpressionLanguageProvider** which overrides **IsLanguageCaseSensitive** to return **false**. As can bee seen in this example, the keywords (e.g., **var**, **public**, **class**, **::pragma**, etc), code comment markers (i.e., "rem", "rem*", "*rem"), code block markers (i.e., "BEGIN", "END") and operators **IS NULL**, **IS NOT NULL** can be used with any capitalization, and the expression is still parsed without errors. 

```csharp
rem This line commented out code with verbose line comment marker 'rem'
rem*this is a demo of verbose code block comment markers*rem

rem#No space is required between line comment marker and the comment, if the first 
rem character is a special character (such as operator, opening, closing round or squer braces, comma etc.)

BEGIN
    println(x); println(x+y)
    rem* this is an example of code block
    with verbose code block start and end markers 'BEGIN' and 'END'.
    *rem
END

Rem Line comment marker can be used with any capitalization

REm* Multi-line comment start/end markers can be used with
any capitalization *ReM

rem keywords public and class can be used with any capitalization.
PUBLIC Class DOG 
BEGIN Rem Code block start marker 'BEGIN' can be used with any capitalization
    PUBLIc static F1(); rem keywords (e.g., 'PUBLIC') can be used with any capitalization
end

REm keyword 'var' can be used with any capitalization.
VaR x=::PRagma y;

PRintLN("X IS NOT NULL=" + X Is noT Null && ::pRAGMA y is NULL);

f1(x1, y1)
BEGin Rem Code block start marker 'BEGIN'can be used with any capitalization.
   Rem Line comment marker 'rem' can be used with any capitalization
   rem Line comment marker 'rem' can be used with any capitalization

   REm* Multi line comment start/end markers can be used with
   any capitalization *rEM

   RETurN X1+Y1; rem unary prefix operator 'return' (and any other) operator  can be used with any capitalization.
enD rem Code block end marker 'END' can be used  with any capitalization.
```

<details> <summary>Click to expand the parsed expression</summary>

```XML
<?xml version="1.0" encoding="utf-8"?>
<UniversalExpressionParser.ExpressionItems.IRootExpressionItemSeries ObjectId='0' PositionInTextOnCompletion='1574' IndexInText='333' ItemLength='1241'>
	<ExpressionItemSeries Id='637840162078067457' IndexInText='333' ItemLength='1171' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'>
		<RegularItems>
			<CodeBlock Id='637840162078161602' IndexInText='333' ItemLength='163' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
				<RegularItems>
					<CodeBlockStartMarker Name='BEGIN' Id='637840162078160818' IndexInText='333' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Braces Id='637840162078196775' IndexInText='344' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<Literal Id='637840162078192177' IndexInText='344' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='println' Id='637840162078191667' IndexInText='344' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<OpeningRoundBrace Name='(' Id='637840162078196790' IndexInText='351' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Literal Id='637840162078202913' IndexInText='352' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='x' Id='637840162078202898' IndexInText='352' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<ClosingRoundBrace Name=')' Id='637840162078217331' IndexInText='353' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<Literal Id='637840162078202913' IndexInText='352' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
						</Children>
						<OtherProperties>
							<NamedExpressionItem Id='637840162078192177' IndexInText='344' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							<OpeningBraceInfo Name='(' Id='637840162078196790' IndexInText='351' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=')' Id='637840162078217331' IndexInText='353' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Parameters ObjectId='11' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162078202913' IndexInText='352' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Braces>
					<ExpressionSeparator Name=';' Id='637840162078220895' IndexInText='354' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Braces Id='637840162078221141' IndexInText='356' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<Literal Id='637840162078221095' IndexInText='356' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='println' Id='637840162078221089' IndexInText='356' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<OpeningRoundBrace Name='(' Id='637840162078221145' IndexInText='363' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<BinaryOperator Name='+' Priority='30' Id='637840162078227343' IndexInText='364' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
								<Operand1.Literal Id='637840162078221403' IndexInText='364' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
									<NameExpressionItem Name='x' Id='637840162078221398' IndexInText='364' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</Operand1.Literal>
								<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637840162078223005' IndexInText='365' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
									<OperatorNameParts>
										<Name Name='+' Id='637840162078221476' IndexInText='365' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</OperatorNameParts>
								</OperatorInfo>
								<Operand2.Literal Id='637840162078224243' IndexInText='366' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
									<NameExpressionItem Name='y' Id='637840162078224232' IndexInText='366' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</Operand2.Literal>
							</BinaryOperator>
							<ClosingRoundBrace Name=')' Id='637840162078238422' IndexInText='367' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<BinaryOperator Name='+' Priority='30' Id='637840162078227343' IndexInText='364' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
						</Children>
						<OtherProperties>
							<NamedExpressionItem Id='637840162078221095' IndexInText='356' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							<OpeningBraceInfo Name='(' Id='637840162078221145' IndexInText='363' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=')' Id='637840162078238422' IndexInText='367' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Parameters ObjectId='25' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name='+' Priority='30' Id='637840162078227343' IndexInText='364' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Braces>
					<CodeBlockEndMarker Name='END' Id='637840162078238662' IndexInText='493' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Children>
					<Braces Id='637840162078196775' IndexInText='344' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
					<Braces Id='637840162078221141' IndexInText='356' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
				</Children>
				<OtherProperties>
					<CodeBlockStartMarker Name='BEGIN' Id='637840162078160818' IndexInText='333' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<CodeBlockEndMarker Name='END' Id='637840162078238662' IndexInText='493' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</OtherProperties>
			</CodeBlock>
			<Literal Id='637840162078241433' IndexInText='718' ItemLength='192' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
				<NameExpressionItem Name='DOG' Id='637840162078241420' IndexInText='731' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				<AppliedKeywords>
					<Keyword Name='PUBLIC' Id='637840162078240709' IndexInText='718' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
						<LanguageKeywordInfo ObjectId='30' Id='637783669980406419' Keyword='public' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
					</Keyword>
					<Keyword Name='Class' Id='637840162078240914' IndexInText='725' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
						<LanguageKeywordInfo ObjectId='32' Id='637783670388745402' Keyword='class' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
					</Keyword>
				</AppliedKeywords>
				<Postfixes>
					<CodeBlock Id='637840162078243613' IndexInText='737' ItemLength='173' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
						<RegularItems>
							<CodeBlockStartMarker Name='BEGIN' Id='637840162078243600' IndexInText='737' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Braces Id='637840162078246565' IndexInText='820' ItemLength='18' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
								<AppliedKeywords>
									<Keyword Name='PUBLIc' Id='637840162078245646' IndexInText='820' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
										<LanguageKeywordInfo ObjectId='30' Id='637783669980406419' Keyword='public' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
									</Keyword>
									<Keyword Name='static' Id='637840162078245670' IndexInText='827' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
										<LanguageKeywordInfo ObjectId='38' Id='637789181872233115' Keyword='static' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
									</Keyword>
								</AppliedKeywords>
								<RegularItems>
									<Literal Id='637840162078245848' IndexInText='834' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='F1' Id='637840162078245837' IndexInText='834' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<OpeningRoundBrace Name='(' Id='637840162078246586' IndexInText='836' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ClosingRoundBrace Name=')' Id='637840162078246675' IndexInText='837' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<OtherProperties>
									<NamedExpressionItem Id='637840162078245848' IndexInText='834' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<OpeningBraceInfo Name='(' Id='637840162078246586' IndexInText='836' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ClosingBraceInfo Name=')' Id='637840162078246675' IndexInText='837' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Parameters ObjectId='43' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'/>
								</OtherProperties>
							</Braces>
							<ExpressionSeparator Name=';' Id='637840162078246721' IndexInText='838' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='end' Id='637840162078246781' IndexInText='907' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<Braces Id='637840162078246565' IndexInText='820' ItemLength='18' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
						</Children>
						<OtherProperties>
							<CodeBlockStartMarker Name='BEGIN' Id='637840162078243600' IndexInText='737' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='end' Id='637840162078246781' IndexInText='907' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OtherProperties>
					</CodeBlock>
				</Postfixes>
			</Literal>
			<BinaryOperator Name='=' Priority='2000' Id='637840162078256849' IndexInText='970' ItemLength='16' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
				<Operand1.Literal Id='637840162078246933' IndexInText='970' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
					<NameExpressionItem Name='x' Id='637840162078246928' IndexInText='974' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<AppliedKeywords>
						<Keyword Name='VaR' Id='637840162078246817' IndexInText='970' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
							<LanguageKeywordInfo ObjectId='50' Id='637781064051574641' Keyword='var' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
						</Keyword>
					</AppliedKeywords>
				</Operand1.Literal>
				<OperatorInfo OperatorType='BinaryOperator' Name='=' Priority='2000' Id='637840162078247029' IndexInText='975' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
					<OperatorNameParts>
						<Name Name='=' Id='637840162078247021' IndexInText='975' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</OperatorNameParts>
				</OperatorInfo>
				<Operand2.Custom Id='637840162078255329' IndexInText='976' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.Custom.IKeywordBasedCustomExpressionItem'>
					<RegularItems>
						<Keyword Name='::PRagma' Id='637840162078247076' IndexInText='976' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
							<LanguageKeywordInfo ObjectId='55' Id='637781063127646487' Keyword='::pragma' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
						</Keyword>
						<Name Name='y' Id='637840162078253150' IndexInText='985' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</RegularItems>
					<OtherProperties>
						<LastKeywordExpressionItem Name='::PRagma' Id='637840162078247076' IndexInText='976' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'/>
						<ErrorsPositionDisplayValue value='976' type='System.Int32' />
					</OtherProperties>
				</Operand2.Custom>
			</BinaryOperator>
			<ExpressionSeparator Name=';' Id='637840162078257191' IndexInText='986' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<Braces Id='637840162078257378' IndexInText='991' ItemLength='63' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
				<RegularItems>
					<Literal Id='637840162078257340' IndexInText='991' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='PRintLN' Id='637840162078257335' IndexInText='991' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<OpeningRoundBrace Name='(' Id='637840162078257381' IndexInText='998' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<BinaryOperator Name='&&' Priority='80' Id='637840162078268810' IndexInText='999' ItemLength='54' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.BinaryOperator Name='+' Priority='30' Id='637840162078268780' IndexInText='999' ItemLength='32' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
							<Operand1.ConstantText Id='637840162078261392' IndexInText='999' ItemLength='16' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='"X IS NOT NULL="' Id='637840162078261375' IndexInText='999' ItemLength='16' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Operand1.ConstantText>
							<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637840162078261553' IndexInText='1016' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
								<OperatorNameParts>
									<Name Name='+' Id='637840162078261543' IndexInText='1016' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</OperatorNameParts>
							</OperatorInfo>
							<Operand2.PostfixUnaryOperator Name='IS NOT NULL' Priority='1' Id='637840162078268794' IndexInText='1018' ItemLength='13' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
								<Operand1.Literal Id='637840162078262456' IndexInText='1018' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
									<NameExpressionItem Name='X' Id='637840162078262444' IndexInText='1018' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</Operand1.Literal>
								<OperatorInfo OperatorType='PostfixUnaryOperator' Name='IS NOT NULL' Priority='1' Id='637840162078262573' IndexInText='1020' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
									<OperatorNameParts>
										<Name Name='Is' Id='637840162078262518' IndexInText='1020' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										<Name Name='noT' Id='637840162078262557' IndexInText='1023' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										<Name Name='Null' Id='637840162078262567' IndexInText='1027' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</OperatorNameParts>
								</OperatorInfo>
							</Operand2.PostfixUnaryOperator>
						</Operand1.BinaryOperator>
						<OperatorInfo OperatorType='BinaryOperator' Name='&&' Priority='80' Id='637840162078262696' IndexInText='1032' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<Name Name='&&' Id='637840162078262691' IndexInText='1032' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OperatorNameParts>
						</OperatorInfo>
						<Operand2.PostfixUnaryOperator Name='IS NULL' Priority='1' Id='637840162078268818' IndexInText='1035' ItemLength='18' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
							<Operand1.Custom Id='637840162078262941' IndexInText='1035' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.Custom.IKeywordBasedCustomExpressionItem'>
								<RegularItems>
									<Keyword Name='::pRAGMA' Id='637840162078262779' IndexInText='1035' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
										<LanguageKeywordInfo ObjectId='55' Id='637781063127646487' Keyword='::pragma' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
									</Keyword>
									<Name Name='y' Id='637840162078262934' IndexInText='1044' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<OtherProperties>
									<LastKeywordExpressionItem Name='::pRAGMA' Id='637840162078262779' IndexInText='1035' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'/>
									<ErrorsPositionDisplayValue value='1035' type='System.Int32' />
								</OtherProperties>
							</Operand1.Custom>
							<OperatorInfo OperatorType='PostfixUnaryOperator' Name='IS NULL' Priority='1' Id='637840162078268607' IndexInText='1046' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
								<OperatorNameParts>
									<Name Name='is' Id='637840162078267109' IndexInText='1046' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Name Name='NULL' Id='637840162078268586' IndexInText='1049' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</OperatorNameParts>
							</OperatorInfo>
						</Operand2.PostfixUnaryOperator>
					</BinaryOperator>
					<ClosingRoundBrace Name=')' Id='637840162078271020' IndexInText='1053' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Children>
					<BinaryOperator Name='&&' Priority='80' Id='637840162078268810' IndexInText='999' ItemLength='54' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
				</Children>
				<OtherProperties>
					<NamedExpressionItem Id='637840162078257340' IndexInText='991' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					<OpeningBraceInfo Name='(' Id='637840162078257381' IndexInText='998' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ClosingBraceInfo Name=')' Id='637840162078271020' IndexInText='1053' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Parameters ObjectId='85' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name='&&' Priority='80' Id='637840162078268810' IndexInText='999' ItemLength='54' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
					</Parameters>
				</OtherProperties>
			</Braces>
			<ExpressionSeparator Name=';' Id='637840162078271101' IndexInText='1054' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<Braces Id='637840162078271263' IndexInText='1059' ItemLength='445' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
				<RegularItems>
					<Literal Id='637840162078271227' IndexInText='1059' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='f1' Id='637840162078271222' IndexInText='1059' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<OpeningRoundBrace Name='(' Id='637840162078271265' IndexInText='1061' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Literal Id='637840162078271370' IndexInText='1062' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='x1' Id='637840162078271366' IndexInText='1062' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<Comma Name=',' Id='637840162078272128' IndexInText='1064' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Literal Id='637840162078272297' IndexInText='1066' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='y1' Id='637840162078272292' IndexInText='1066' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<ClosingRoundBrace Name=')' Id='637840162078272338' IndexInText='1068' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Postfixes>
					<CodeBlock Id='637840162078272388' IndexInText='1071' ItemLength='433' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
						<RegularItems>
							<CodeBlockStartMarker Name='BEGin' Id='637840162078272379' IndexInText='1071' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<PrefixUnaryOperator Name='return' Priority='2147483647' Id='637840162078272984' IndexInText='1388' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
								<OperatorInfo OperatorType='PrefixUnaryOperator' Name='return' Priority='2147483647' Id='637840162078272553' IndexInText='1388' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
									<OperatorNameParts>
										<Name Name='RETurN' Id='637840162078272545' IndexInText='1388' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</OperatorNameParts>
								</OperatorInfo>
								<Operand1.BinaryOperator Name='+' Priority='30' Id='637840162078272998' IndexInText='1395' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
									<Operand1.Literal Id='637840162078272701' IndexInText='1395' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='X1' Id='637840162078272696' IndexInText='1395' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Operand1.Literal>
									<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637840162078272777' IndexInText='1397' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
										<OperatorNameParts>
											<Name Name='+' Id='637840162078272771' IndexInText='1397' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</OperatorNameParts>
									</OperatorInfo>
									<Operand2.Literal Id='637840162078272939' IndexInText='1398' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='Y1' Id='637840162078272931' IndexInText='1398' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Operand2.Literal>
								</Operand1.BinaryOperator>
							</PrefixUnaryOperator>
							<ExpressionSeparator Name=';' Id='637840162078273111' IndexInText='1400' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='enD' Id='637840162078273167' IndexInText='1501' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<PrefixUnaryOperator Name='return' Priority='2147483647' Id='637840162078272984' IndexInText='1388' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
						</Children>
						<OtherProperties>
							<CodeBlockStartMarker Name='BEGin' Id='637840162078272379' IndexInText='1071' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='enD' Id='637840162078273167' IndexInText='1501' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OtherProperties>
					</CodeBlock>
				</Postfixes>
				<Children>
					<Literal Id='637840162078271370' IndexInText='1062' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					<Literal Id='637840162078272297' IndexInText='1066' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
				</Children>
				<OtherProperties>
					<NamedExpressionItem Id='637840162078271227' IndexInText='1059' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					<OpeningBraceInfo Name='(' Id='637840162078271265' IndexInText='1061' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ClosingBraceInfo Name=')' Id='637840162078272338' IndexInText='1068' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Parameters ObjectId='111' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162078271370' IndexInText='1062' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637840162078272297' IndexInText='1066' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					</Parameters>
				</OtherProperties>
			</Braces>
		</RegularItems>
		<Children>
			<CodeBlock Id='637840162078161602' IndexInText='333' ItemLength='163' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'/>
			<Literal Id='637840162078241433' IndexInText='718' ItemLength='192' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
			<BinaryOperator Name='=' Priority='2000' Id='637840162078256849' IndexInText='970' ItemLength='16' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
			<Braces Id='637840162078257378' IndexInText='991' ItemLength='63' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
			<Braces Id='637840162078271263' IndexInText='1059' ItemLength='445' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
		</Children>
	</ExpressionItemSeries>
	<ParseErrorData ObjectId='112' HasCriticalErrors='False' Interface='UniversalExpressionParser.IParseErrorData'>
		<AllCodeItemErrors ObjectId='113' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICodeItemParseErrorData]'/>
	</ParseErrorData>
	<SortedCommentedOutCodeInfos ObjectId='114' Count='17' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICommentedOutCodeInfo]'>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='115' IsLineComment='True' IndexInText='0' ItemLength='71'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='116' IsLineComment='False' IndexInText='73' ItemLength='60'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='117' IsLineComment='True' IndexInText='137' ItemLength='83'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='118' IsLineComment='True' IndexInText='222' ItemLength='107'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='119' IsLineComment='False' IndexInText='374' ItemLength='117'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='120' IsLineComment='True' IndexInText='500' ItemLength='59'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='121' IsLineComment='False' IndexInText='563' ItemLength='83'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='122' IsLineComment='True' IndexInText='650' ItemLength='66'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='123' IsLineComment='True' IndexInText='743' ItemLength='71'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='124' IsLineComment='True' IndexInText='840' ItemLength='65'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='125' IsLineComment='True' IndexInText='914' ItemLength='54'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='126' IsLineComment='True' IndexInText='1077' ItemLength='71'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='127' IsLineComment='True' IndexInText='1153' ItemLength='65'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='128' IsLineComment='True' IndexInText='1223' ItemLength='65'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='129' IsLineComment='False' IndexInText='1295' ItemLength='86'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='130' IsLineComment='True' IndexInText='1402' ItemLength='97'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='131' IsLineComment='True' IndexInText='1505' ItemLength='69'/>
	</SortedCommentedOutCodeInfos>
	<MainExpressionItem Id='637840162078067457' IndexInText='333' ItemLength='1171' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'/>
</UniversalExpressionParser.ExpressionItems.IRootExpressionItemSeries>
```
</details>
