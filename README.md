# UniversalExpressionParser

# NOTE: CLICK ON https://github.com/artakhak/UniversalExpressionParser/blob/main/README.md to view the non-trimmed version of documentation

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

public class Dog : (Animal)
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

- Each instance of **UniversalExpressionParser.ExpressionItems.IExpressionItemBase** is currently an instance of one of the following interfaces in namespace **UniversalExpressionParser.ExpressionItems**, the state of which can be analyzed when evaluating the parsed expression: **IComplexExpressionItem**, **INameExpressionItem**, **INamedComplexExpressionItem**, **INumericValueExpressionItem**, **IBracesExpressionItem**, **ICodeBlockExpressionItem**, **ICustomExpressionItem**, **IKeywordExpressionItem**, **IOperatorInfoExpressionItem**, **IOperatorExpressionItem**.
 
- Below is the visualized instance of **UniversalExpressionParser.ExpressionItems.IRootExpressionItemSeries**:

<details> <summary>Click to expand the parsed expression</summary>

```XML
<?xml version="1.0" encoding="utf-8"?>
<UniversalExpressionParser.ExpressionItems.IRootExpressionItemSeries ObjectId='0' PositionInTextOnCompletion='740' IndexInText='0' ItemLength='736'>
	<ExpressionItemSeries Id='637856640183763621' IndexInText='0' ItemLength='736' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'>
		<RegularItems>
			<BinaryOperator Name='=' Priority='2000' Id='637856640183765059' IndexInText='0' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
				<Operand1.Literal Id='637856640183763986' IndexInText='0' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
					<NameExpressionItem Name='z' Id='637856640183763981' IndexInText='4' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<AppliedKeywords>
						<Keyword Name='var' Id='637856640183763835' IndexInText='0' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
							<LanguageKeywordInfo ObjectId='6' Id='637781064051574641' Keyword='var' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
						</Keyword>
					</AppliedKeywords>
				</Operand1.Literal>
				<OperatorInfo OperatorType='BinaryOperator' Name='=' Priority='2000' Id='637856640183764093' IndexInText='6' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
					<OperatorNameParts>
						<Name Name='=' Id='637856640183764083' IndexInText='6' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</OperatorNameParts>
				</OperatorInfo>
				<Operand2.BinaryOperator Name='+' Priority='30' Id='637856640183765091' IndexInText='8' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
					<Operand1.BinaryOperator Name='*' Priority='20' Id='637856640183765082' IndexInText='8' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.Literal Id='637856640183764235' IndexInText='8' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='x1' Id='637856640183764230' IndexInText='8' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand1.Literal>
						<OperatorInfo OperatorType='BinaryOperator' Name='*' Priority='20' Id='637856640183764313' IndexInText='10' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<Name Name='*' Id='637856640183764307' IndexInText='10' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OperatorNameParts>
						</OperatorInfo>
						<Operand2.Literal Id='637856640183764443' IndexInText='11' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='y1' Id='637856640183764439' IndexInText='11' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand2.Literal>
					</Operand1.BinaryOperator>
					<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637856640183764506' IndexInText='13' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
						<OperatorNameParts>
							<Name Name='+' Id='637856640183764500' IndexInText='13' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OperatorNameParts>
					</OperatorInfo>
					<Operand2.BinaryOperator Name='*' Priority='20' Id='637856640183765097' IndexInText='14' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.Literal Id='637856640183764633' IndexInText='14' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='x2' Id='637856640183764629' IndexInText='14' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand1.Literal>
						<OperatorInfo OperatorType='BinaryOperator' Name='*' Priority='20' Id='637856640183764684' IndexInText='16' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<Name Name='*' Id='637856640183764679' IndexInText='16' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OperatorNameParts>
						</OperatorInfo>
						<Operand2.Literal Id='637856640183764983' IndexInText='17' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='y2' Id='637856640183764964' IndexInText='17' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand2.Literal>
					</Operand2.BinaryOperator>
				</Operand2.BinaryOperator>
			</BinaryOperator>
			<ExpressionSeparator Name=';' Id='637856640183765160' IndexInText='19' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<BinaryOperator Name='=' Priority='2000' Id='637856640183768162' IndexInText='24' ItemLength='116' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
				<Operand1.Literal Id='637856640183765292' IndexInText='24' ItemLength='30' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
					<NameExpressionItem Name='matrixMultiplicationResult' Id='637856640183765288' IndexInText='28' ItemLength='26' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<AppliedKeywords>
						<Keyword Name='var' Id='637856640183765183' IndexInText='24' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
							<LanguageKeywordInfo ObjectId='6' Id='637781064051574641' Keyword='var' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
						</Keyword>
					</AppliedKeywords>
				</Operand1.Literal>
				<OperatorInfo OperatorType='BinaryOperator' Name='=' Priority='2000' Id='637856640183765373' IndexInText='55' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
					<OperatorNameParts>
						<Name Name='=' Id='637856640183765365' IndexInText='55' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</OperatorNameParts>
				</OperatorInfo>
				<Operand2.BinaryOperator Name='*' Priority='20' Id='637856640183768184' IndexInText='57' ItemLength='83' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
					<Operand1.Braces Id='637856640183765564' IndexInText='57' ItemLength='40' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<OpeningSquareBrace Name='[' Id='637856640183765569' IndexInText='57' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Braces Id='637856640183765609' IndexInText='58' ItemLength='18' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
								<RegularItems>
									<OpeningSquareBrace Name='[' Id='637856640183765613' IndexInText='58' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637856640183765697' IndexInText='59' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='x1_1' Id='637856640183765693' IndexInText='59' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<Comma Name=',' Id='637856640183765735' IndexInText='63' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637856640183765817' IndexInText='65' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='x1_2' Id='637856640183765813' IndexInText='65' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<Comma Name=',' Id='637856640183765844' IndexInText='69' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637856640183766267' IndexInText='71' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='x1_3' Id='637856640183766250' IndexInText='71' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<ClosingSquareBrace Name=']' Id='637856640183766347' IndexInText='75' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Children>
									<Literal Id='637856640183765697' IndexInText='59' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<Literal Id='637856640183765817' IndexInText='65' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<Literal Id='637856640183766267' IndexInText='71' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								</Children>
								<OtherProperties>
									<OpeningBraceInfo Name='[' Id='637856640183765613' IndexInText='58' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ClosingBraceInfo Name=']' Id='637856640183766347' IndexInText='75' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
									<Parameters ObjectId='47' Count='3' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640183765697' IndexInText='59' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640183765817' IndexInText='65' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640183766267' IndexInText='71' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</Parameters>
								</OtherProperties>
							</Braces>
							<Comma Name=',' Id='637856640183766391' IndexInText='76' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Braces Id='637856640183766418' IndexInText='78' ItemLength='18' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
								<RegularItems>
									<OpeningSquareBrace Name='[' Id='637856640183766422' IndexInText='78' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637856640183766579' IndexInText='79' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='x2_1' Id='637856640183766561' IndexInText='79' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<Comma Name=',' Id='637856640183766656' IndexInText='83' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637856640183766790' IndexInText='85' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='x2_2' Id='637856640183766781' IndexInText='85' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<Comma Name=',' Id='637856640183766828' IndexInText='89' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637856640183766940' IndexInText='91' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='x2_3' Id='637856640183766936' IndexInText='91' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<ClosingSquareBrace Name=']' Id='637856640183766970' IndexInText='95' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Children>
									<Literal Id='637856640183766579' IndexInText='79' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<Literal Id='637856640183766790' IndexInText='85' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<Literal Id='637856640183766940' IndexInText='91' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								</Children>
								<OtherProperties>
									<OpeningBraceInfo Name='[' Id='637856640183766422' IndexInText='78' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ClosingBraceInfo Name=']' Id='637856640183766970' IndexInText='95' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
									<Parameters ObjectId='60' Count='3' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640183766579' IndexInText='79' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640183766790' IndexInText='85' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640183766940' IndexInText='91' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</Parameters>
								</OtherProperties>
							</Braces>
							<ClosingSquareBrace Name=']' Id='637856640183767029' IndexInText='96' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<Braces Id='637856640183765609' IndexInText='58' ItemLength='18' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
							<Braces Id='637856640183766418' IndexInText='78' ItemLength='18' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
						</Children>
						<OtherProperties>
							<OpeningBraceInfo Name='[' Id='637856640183765569' IndexInText='57' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=']' Id='637856640183767029' IndexInText='96' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
							<Parameters ObjectId='62' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640183765609' IndexInText='58' ItemLength='18' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640183766418' IndexInText='78' ItemLength='18' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Operand1.Braces>
					<OperatorInfo OperatorType='BinaryOperator' Name='*' Priority='20' Id='637856640183767127' IndexInText='97' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
						<OperatorNameParts>
							<Name Name='*' Id='637856640183767119' IndexInText='97' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OperatorNameParts>
					</OperatorInfo>
					<Operand2.Braces Id='637856640183767292' IndexInText='98' ItemLength='42' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<OpeningSquareBrace Name='[' Id='637856640183767297' IndexInText='98' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Braces Id='637856640183767335' IndexInText='99' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
								<RegularItems>
									<OpeningSquareBrace Name='[' Id='637856640183767338' IndexInText='99' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637856640183767424' IndexInText='100' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='y1_1' Id='637856640183767420' IndexInText='100' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<Comma Name=',' Id='637856640183767457' IndexInText='104' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637856640183767539' IndexInText='106' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='x1_2' Id='637856640183767535' IndexInText='106' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<ClosingSquareBrace Name=']' Id='637856640183767568' IndexInText='110' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Children>
									<Literal Id='637856640183767424' IndexInText='100' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<Literal Id='637856640183767539' IndexInText='106' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								</Children>
								<OtherProperties>
									<OpeningBraceInfo Name='[' Id='637856640183767338' IndexInText='99' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ClosingBraceInfo Name=']' Id='637856640183767568' IndexInText='110' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
									<Parameters ObjectId='75' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640183767424' IndexInText='100' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640183767539' IndexInText='106' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</Parameters>
								</OtherProperties>
							</Braces>
							<Comma Name=',' Id='637856640183767596' IndexInText='111' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Braces Id='637856640183767623' IndexInText='113' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
								<RegularItems>
									<OpeningSquareBrace Name='[' Id='637856640183767626' IndexInText='113' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637856640183767700' IndexInText='114' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='y2_1' Id='637856640183767696' IndexInText='114' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<Comma Name=',' Id='637856640183767731' IndexInText='118' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637856640183767805' IndexInText='120' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='x2_2' Id='637856640183767801' IndexInText='120' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<ClosingSquareBrace Name=']' Id='637856640183767831' IndexInText='124' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Children>
									<Literal Id='637856640183767700' IndexInText='114' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<Literal Id='637856640183767805' IndexInText='120' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								</Children>
								<OtherProperties>
									<OpeningBraceInfo Name='[' Id='637856640183767626' IndexInText='113' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ClosingBraceInfo Name=']' Id='637856640183767831' IndexInText='124' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
									<Parameters ObjectId='85' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640183767700' IndexInText='114' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640183767805' IndexInText='120' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</Parameters>
								</OtherProperties>
							</Braces>
							<Comma Name=',' Id='637856640183767861' IndexInText='125' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Braces Id='637856640183767885' IndexInText='127' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
								<RegularItems>
									<OpeningSquareBrace Name='[' Id='637856640183767888' IndexInText='127' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637856640183767966' IndexInText='128' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='y3_1' Id='637856640183767962' IndexInText='128' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<Comma Name=',' Id='637856640183767994' IndexInText='132' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637856640183768072' IndexInText='134' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='x3_2' Id='637856640183768069' IndexInText='134' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<ClosingSquareBrace Name=']' Id='637856640183768100' IndexInText='138' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Children>
									<Literal Id='637856640183767966' IndexInText='128' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<Literal Id='637856640183768072' IndexInText='134' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								</Children>
								<OtherProperties>
									<OpeningBraceInfo Name='[' Id='637856640183767888' IndexInText='127' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ClosingBraceInfo Name=']' Id='637856640183768100' IndexInText='138' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
									<Parameters ObjectId='95' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640183767966' IndexInText='128' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640183768072' IndexInText='134' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</Parameters>
								</OtherProperties>
							</Braces>
							<ClosingSquareBrace Name=']' Id='637856640183768128' IndexInText='139' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<Braces Id='637856640183767335' IndexInText='99' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
							<Braces Id='637856640183767623' IndexInText='113' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
							<Braces Id='637856640183767885' IndexInText='127' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
						</Children>
						<OtherProperties>
							<OpeningBraceInfo Name='[' Id='637856640183767297' IndexInText='98' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=']' Id='637856640183768128' IndexInText='139' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
							<Parameters ObjectId='97' Count='3' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640183767335' IndexInText='99' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640183767623' IndexInText='113' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640183767885' IndexInText='127' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Operand2.Braces>
				</Operand2.BinaryOperator>
			</BinaryOperator>
			<ExpressionSeparator Name=';' Id='637856640183768221' IndexInText='140' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<Braces Id='637856640183770288' IndexInText='145' ItemLength='35' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
				<RegularItems>
					<Literal Id='637856640183768308' IndexInText='145' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='println' Id='637856640183768303' IndexInText='145' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<OpeningRoundBrace Name='(' Id='637856640183770307' IndexInText='152' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Literal Id='637856640183770647' IndexInText='153' ItemLength='26' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='matrixMultiplicationResult' Id='637856640183770475' IndexInText='153' ItemLength='26' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<ClosingRoundBrace Name=')' Id='637856640183770690' IndexInText='179' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Children>
					<Literal Id='637856640183770647' IndexInText='153' ItemLength='26' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
				</Children>
				<OtherProperties>
					<NamedExpressionItem Id='637856640183768308' IndexInText='145' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					<OpeningBraceInfo Name='(' Id='637856640183770307' IndexInText='152' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ClosingBraceInfo Name=')' Id='637856640183770690' IndexInText='179' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Parameters ObjectId='106' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640183770647' IndexInText='153' ItemLength='26' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					</Parameters>
				</OtherProperties>
			</Braces>
			<ExpressionSeparator Name=';' Id='637856640183770731' IndexInText='180' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<BinaryOperator Name='=>' Priority='1000' Id='637856640183840451' IndexInText='185' ItemLength='237' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
				<Operand1.BinaryOperator Name=':' Priority='0' Id='637856640183840430' IndexInText='185' ItemLength='58' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
					<Operand1.Braces Id='637856640183771381' IndexInText='185' ItemLength='52' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<Prefixes>
							<Braces Id='637856640183770761' IndexInText='185' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
								<RegularItems>
									<OpeningSquareBrace Name='[' Id='637856640183770764' IndexInText='185' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637856640183770850' IndexInText='186' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='NotNull' Id='637856640183770846' IndexInText='186' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<ClosingSquareBrace Name=']' Id='637856640183770879' IndexInText='193' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Children>
									<Literal Id='637856640183770850' IndexInText='186' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								</Children>
								<OtherProperties>
									<OpeningBraceInfo Name='[' Id='637856640183770764' IndexInText='185' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ClosingBraceInfo Name=']' Id='637856640183770879' IndexInText='193' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
									<Parameters ObjectId='116' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640183770850' IndexInText='186' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</Parameters>
								</OtherProperties>
							</Braces>
							<Braces Id='637856640183770907' IndexInText='195' ItemLength='25' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
								<RegularItems>
									<OpeningSquareBrace Name='[' Id='637856640183770926' IndexInText='195' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Braces Id='637856640183771036' IndexInText='196' ItemLength='23' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
										<RegularItems>
											<Literal Id='637856640183771009' IndexInText='196' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
												<NameExpressionItem Name='PublicName' Id='637856640183771005' IndexInText='196' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											</Literal>
											<OpeningRoundBrace Name='(' Id='637856640183771039' IndexInText='206' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<ConstantText Id='637856640183771147' IndexInText='207' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
												<NameExpressionItem Name='"Calculate"' Id='637856640183771141' IndexInText='207' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											</ConstantText>
											<ClosingRoundBrace Name=')' Id='637856640183771178' IndexInText='218' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</RegularItems>
										<Children>
											<ConstantText Id='637856640183771147' IndexInText='207' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
										</Children>
										<OtherProperties>
											<NamedExpressionItem Id='637856640183771009' IndexInText='196' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
											<OpeningBraceInfo Name='(' Id='637856640183771039' IndexInText='206' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<ClosingBraceInfo Name=')' Id='637856640183771178' IndexInText='218' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<Parameters ObjectId='126' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
												<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640183771147' IndexInText='207' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
											</Parameters>
										</OtherProperties>
									</Braces>
									<ClosingSquareBrace Name=']' Id='637856640183771206' IndexInText='219' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Children>
									<Braces Id='637856640183771036' IndexInText='196' ItemLength='23' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
								</Children>
								<OtherProperties>
									<OpeningBraceInfo Name='[' Id='637856640183770926' IndexInText='195' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ClosingBraceInfo Name=']' Id='637856640183771206' IndexInText='219' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
									<Parameters ObjectId='128' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640183771036' IndexInText='196' ItemLength='23' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
									</Parameters>
								</OtherProperties>
							</Braces>
						</Prefixes>
						<AppliedKeywords>
							<Keyword Name='public' Id='637856640183771229' IndexInText='222' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
								<LanguageKeywordInfo ObjectId='130' Id='637783669980406419' Keyword='public' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
							</Keyword>
						</AppliedKeywords>
						<RegularItems>
							<Literal Id='637856640183771338' IndexInText='229' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='F1' Id='637856640183771334' IndexInText='229' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<OpeningRoundBrace Name='(' Id='637856640183771386' IndexInText='231' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Literal Id='637856640183771467' IndexInText='232' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='x' Id='637856640183771462' IndexInText='232' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<Comma Name=',' Id='637856640183771496' IndexInText='233' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Literal Id='637856640183771576' IndexInText='235' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='y' Id='637856640183771573' IndexInText='235' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<ClosingRoundBrace Name=')' Id='637856640183771603' IndexInText='236' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<Literal Id='637856640183771467' IndexInText='232' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							<Literal Id='637856640183771576' IndexInText='235' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
						</Children>
						<OtherProperties>
							<NamedExpressionItem Id='637856640183771338' IndexInText='229' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							<OpeningBraceInfo Name='(' Id='637856640183771386' IndexInText='231' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=')' Id='637856640183771603' IndexInText='236' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Parameters ObjectId='140' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640183771467' IndexInText='232' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640183771576' IndexInText='235' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Operand1.Braces>
					<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637856640183771661' IndexInText='238' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
						<OperatorNameParts>
							<Name Name=':' Id='637856640183771652' IndexInText='238' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OperatorNameParts>
					</OperatorInfo>
					<Operand2.Literal Id='637856640183771815' IndexInText='240' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='int' Id='637856640183771811' IndexInText='240' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Operand2.Literal>
				</Operand1.BinaryOperator>
				<OperatorInfo OperatorType='BinaryOperator' Name='=>' Priority='1000' Id='637856640183771902' IndexInText='244' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
					<OperatorNameParts>
						<Name Name='=>' Id='637856640183771894' IndexInText='244' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</OperatorNameParts>
				</OperatorInfo>
				<Operand2.CodeBlock Id='637856640183772020' IndexInText='249' ItemLength='173' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
					<RegularItems>
						<CodeBlockStartMarker Name='{' Id='637856640183772015' IndexInText='249' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<PrefixUnaryOperator Name='++' Priority='0' Id='637856640183772264' IndexInText='334' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
							<OperatorInfo OperatorType='PrefixUnaryOperator' Name='++' Priority='0' Id='637856640183772102' IndexInText='334' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
								<OperatorNameParts>
									<Name Name='++' Id='637856640183772095' IndexInText='334' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</OperatorNameParts>
							</OperatorInfo>
							<Operand1.Literal Id='637856640183772209' IndexInText='336' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='y' Id='637856640183772205' IndexInText='336' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Operand1.Literal>
						</PrefixUnaryOperator>
						<ExpressionSeparator Name=';' Id='637856640183772299' IndexInText='367' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<PrefixUnaryOperator Name='return' Priority='2147483647' Id='637856640183834892' IndexInText='374' ItemLength='26' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
							<OperatorInfo OperatorType='PrefixUnaryOperator' Name='return' Priority='2147483647' Id='637856640183772352' IndexInText='374' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
								<OperatorNameParts>
									<Name Name='return' Id='637856640183772347' IndexInText='374' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</OperatorNameParts>
							</OperatorInfo>
							<Operand1.BinaryOperator Name='+' Priority='30' Id='637856640183834939' IndexInText='381' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
								<Operand1.BinaryOperator Name='+' Priority='30' Id='637856640183834924' IndexInText='381' ItemLength='15' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
									<Operand1.ConstantNumericValue Id='637856640183833934' IndexInText='381' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
										<NameExpressionItem Name='1.3EXP-2.7' Id='637856640183833875' IndexInText='381' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										<OtherProperties>
											<SucceededNumericTypeDescriptor ObjectId='162' NumberTypeId='1581134225786' Interface='UniversalExpressionParser.NumericTypeDescriptor'>
												<RegularExpressions ObjectId='163' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[System.String]'>
													<System.String value='^(\d+\.\d+|\d+\.|\.\d+|\d+)(EXP|exp|E|e)[+|-]?(\d+\.\d+|\d+\.|\.\d+|\d+)' />
												</RegularExpressions>
											</SucceededNumericTypeDescriptor>
										</OtherProperties>
									</Operand1.ConstantNumericValue>
									<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637856640183834201' IndexInText='391' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
										<OperatorNameParts>
											<Name Name='+' Id='637856640183834190' IndexInText='391' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</OperatorNameParts>
									</OperatorInfo>
									<Operand2.PrefixUnaryOperator Name='++' Priority='0' Id='637856640183834932' IndexInText='393' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
										<OperatorInfo OperatorType='PrefixUnaryOperator' Name='++' Priority='0' Id='637856640183834305' IndexInText='393' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
											<OperatorNameParts>
												<Name Name='++' Id='637856640183834299' IndexInText='393' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											</OperatorNameParts>
										</OperatorInfo>
										<Operand1.Literal Id='637856640183834437' IndexInText='395' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
											<NameExpressionItem Name='x' Id='637856640183834432' IndexInText='395' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</Operand1.Literal>
									</Operand2.PrefixUnaryOperator>
								</Operand1.BinaryOperator>
								<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637856640183834532' IndexInText='396' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
									<OperatorNameParts>
										<Name Name='+' Id='637856640183834526' IndexInText='396' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</OperatorNameParts>
								</OperatorInfo>
								<Operand2.BinaryOperator Name='*' Priority='20' Id='637856640183834947' IndexInText='397' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
									<Operand1.Literal Id='637856640183834663' IndexInText='397' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='y' Id='637856640183834659' IndexInText='397' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Operand1.Literal>
									<OperatorInfo OperatorType='BinaryOperator' Name='*' Priority='20' Id='637856640183834716' IndexInText='398' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
										<OperatorNameParts>
											<Name Name='*' Id='637856640183834710' IndexInText='398' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</OperatorNameParts>
									</OperatorInfo>
									<Operand2.Literal Id='637856640183834844' IndexInText='399' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='z' Id='637856640183834840' IndexInText='399' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Operand2.Literal>
								</Operand2.BinaryOperator>
							</Operand1.BinaryOperator>
						</PrefixUnaryOperator>
						<ExpressionSeparator Name=';' Id='637856640183840294' IndexInText='400' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<CodeBlockEndMarker Name='}' Id='637856640183840389' IndexInText='421' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</RegularItems>
					<Children>
						<PrefixUnaryOperator Name='++' Priority='0' Id='637856640183772264' IndexInText='334' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
						<PrefixUnaryOperator Name='return' Priority='2147483647' Id='637856640183834892' IndexInText='374' ItemLength='26' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
					</Children>
					<OtherProperties>
						<CodeBlockStartMarker Name='{' Id='637856640183772015' IndexInText='249' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<CodeBlockEndMarker Name='}' Id='637856640183840389' IndexInText='421' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</OtherProperties>
				</Operand2.CodeBlock>
			</BinaryOperator>
			<Literal Id='637856640183841768' IndexInText='426' ItemLength='218' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
				<NameExpressionItem Name='Animal' Id='637856640183841759' IndexInText='507' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				<Prefixes>
					<Custom Id='637856640183841147' IndexInText='426' ItemLength='60' Interface='UniversalExpressionParser.ExpressionItems.Custom.IKeywordBasedCustomExpressionItem'>
						<AppliedKeywords>
							<Keyword Name='public' Id='637856640183840501' IndexInText='426' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
								<LanguageKeywordInfo ObjectId='130' Id='637783669980406419' Keyword='public' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
							</Keyword>
							<Keyword Name='abstract' Id='637856640183840513' IndexInText='433' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
								<LanguageKeywordInfo ObjectId='187' Id='637793546145647499' Keyword='abstract' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
							</Keyword>
							<Keyword Name='class' Id='637856640183840519' IndexInText='442' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
								<LanguageKeywordInfo ObjectId='189' Id='637783670388745402' Keyword='class' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
							</Keyword>
						</AppliedKeywords>
						<RegularItems>
							<Keyword Name='::metadata' Id='637856640183840532' IndexInText='448' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
								<LanguageKeywordInfo ObjectId='191' Id='637781063212876967' Keyword='::metadata' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
							</Keyword>
							<CodeBlock Id='637856640183840620' IndexInText='458' ItemLength='28' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
								<RegularItems>
									<CodeBlockStartMarker Name='{' Id='637856640183840614' IndexInText='458' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<BinaryOperator Name=':' Priority='0' Id='637856640183841118' IndexInText='459' ItemLength='26' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
										<Operand1.Literal Id='637856640183840846' IndexInText='459' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
											<NameExpressionItem Name='description' Id='637856640183840840' IndexInText='459' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</Operand1.Literal>
										<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637856640183840906' IndexInText='470' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
											<OperatorNameParts>
												<Name Name=':' Id='637856640183840897' IndexInText='470' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											</OperatorNameParts>
										</OperatorInfo>
										<Operand2.ConstantText Id='637856640183841074' IndexInText='472' ItemLength='13' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
											<NameExpressionItem Name='"Demo prefix"' Id='637856640183841068' IndexInText='472' ItemLength='13' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</Operand2.ConstantText>
									</BinaryOperator>
									<CodeBlockEndMarker Name='}' Id='637856640183841137' IndexInText='485' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Children>
									<BinaryOperator Name=':' Priority='0' Id='637856640183841118' IndexInText='459' ItemLength='26' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
								</Children>
								<OtherProperties>
									<CodeBlockStartMarker Name='{' Id='637856640183840614' IndexInText='458' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<CodeBlockEndMarker Name='}' Id='637856640183841137' IndexInText='485' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</OtherProperties>
							</CodeBlock>
						</RegularItems>
						<OtherProperties>
							<LastKeywordExpressionItem Name='::metadata' Id='637856640183840532' IndexInText='448' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'/>
							<ErrorsPositionDisplayValue value='448' type='System.Int32' />
						</OtherProperties>
					</Custom>
					<Custom Id='637856640183841647' IndexInText='487' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.Custom.IKeywordBasedCustomExpressionItem'>
						<RegularItems>
							<Keyword Name='::types' Id='637856640183841195' IndexInText='487' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
								<LanguageKeywordInfo ObjectId='204' Id='637781062811583137' Keyword='::types' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
							</Keyword>
							<Braces Id='637856640183841248' IndexInText='494' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
								<RegularItems>
									<OpeningSquareBrace Name='[' Id='637856640183841252' IndexInText='494' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637856640183841386' IndexInText='495' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='T1' Id='637856640183841381' IndexInText='495' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<Comma Name=',' Id='637856640183841421' IndexInText='497' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637856640183841501' IndexInText='499' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='T2' Id='637856640183841497' IndexInText='499' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<Comma Name=',' Id='637856640183841527' IndexInText='501' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637856640183841602' IndexInText='503' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='T3' Id='637856640183841598' IndexInText='503' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<ClosingSquareBrace Name=']' Id='637856640183841636' IndexInText='505' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Children>
									<Literal Id='637856640183841386' IndexInText='495' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<Literal Id='637856640183841501' IndexInText='499' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<Literal Id='637856640183841602' IndexInText='503' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								</Children>
								<OtherProperties>
									<OpeningBraceInfo Name='[' Id='637856640183841252' IndexInText='494' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ClosingBraceInfo Name=']' Id='637856640183841636' IndexInText='505' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
									<Parameters ObjectId='216' Count='3' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640183841386' IndexInText='495' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640183841501' IndexInText='499' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640183841602' IndexInText='503' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</Parameters>
								</OtherProperties>
							</Braces>
						</RegularItems>
						<OtherProperties>
							<LastKeywordExpressionItem Name='::types' Id='637856640183841195' IndexInText='487' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'/>
							<ErrorsPositionDisplayValue value='487' type='System.Int32' />
						</OtherProperties>
					</Custom>
				</Prefixes>
				<Postfixes>
					<Custom Id='637856640183841826' IndexInText='518' ItemLength='46' Interface='UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IWhereCustomExpressionItem'>
						<RegularItems>
							<CustomExpressionItemPart Id='637856640183841832' IndexInText='518' ItemLength='16' Interface='UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem'>
								<RegularItems>
									<Keyword Name='where' Id='637856640183841786' IndexInText='518' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
										<LanguageKeywordInfo ObjectId='220' Id='637781062905654923' Keyword='where' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
									</Keyword>
									<Name Name='T1' Id='637856640183841885' IndexInText='524' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Name Name=':' Id='637856640183841893' IndexInText='526' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637856640183841935' IndexInText='528' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='IType1' Id='637856640183841902' IndexInText='528' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
								</RegularItems>
								<OtherProperties>
									<WhereKeyword Name='where' Id='637856640183841786' IndexInText='518' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'/>
									<TypeName Name='T1' Id='637856640183841885' IndexInText='524' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<TypeConstraints ObjectId='225' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640183841935' IndexInText='528' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</TypeConstraints>
								</OtherProperties>
							</CustomExpressionItemPart>
							<CustomExpressionItemPart Id='637856640183841951' IndexInText='535' ItemLength='20' Interface='UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem'>
								<RegularItems>
									<Keyword Name='where' Id='637856640183841947' IndexInText='535' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
										<LanguageKeywordInfo ObjectId='220' Id='637781062905654923' Keyword='where' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
									</Keyword>
									<Name Name='T2' Id='637856640183841962' IndexInText='541' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Name Name=':' Id='637856640183841967' IndexInText='543' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637856640183841984' IndexInText='545' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='T1' Id='637856640183841974' IndexInText='545' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<Comma Name=',' Id='637856640183841991' IndexInText='547' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637856640183842006' IndexInText='549' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='IType2' Id='637856640183842000' IndexInText='549' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
								</RegularItems>
								<OtherProperties>
									<WhereKeyword Name='where' Id='637856640183841947' IndexInText='535' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'/>
									<TypeName Name='T2' Id='637856640183841962' IndexInText='541' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<TypeConstraints ObjectId='235' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640183841984' IndexInText='545' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640183842006' IndexInText='549' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</TypeConstraints>
								</OtherProperties>
							</CustomExpressionItemPart>
							<Name Name='whereend' Id='637856640183842026' IndexInText='556' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<CustomExpressionItemPart Id='637856640183841832' IndexInText='518' ItemLength='16' Interface='UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem'/>
							<CustomExpressionItemPart Id='637856640183841951' IndexInText='535' ItemLength='20' Interface='UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem'/>
						</Children>
						<OtherProperties>
							<WhereEndMarker Name='whereend' Id='637856640183842026' IndexInText='556' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<GenericTypeDataExpressionItems ObjectId='237' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem]'>
								<UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem Id='637856640183841832' IndexInText='518' ItemLength='16'/>
								<UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem Id='637856640183841951' IndexInText='535' ItemLength='20'/>
							</GenericTypeDataExpressionItems>
							<ErrorsPositionDisplayValue value='518' type='System.Int32' />
						</OtherProperties>
					</Custom>
					<Custom Id='637856640183842089' IndexInText='569' ItemLength='25' Interface='UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IWhereCustomExpressionItem'>
						<RegularItems>
							<CustomExpressionItemPart Id='637856640183842093' IndexInText='569' ItemLength='16' Interface='UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem'>
								<RegularItems>
									<Keyword Name='where' Id='637856640183842055' IndexInText='569' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
										<LanguageKeywordInfo ObjectId='220' Id='637781062905654923' Keyword='where' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
									</Keyword>
									<Name Name='T3' Id='637856640183842106' IndexInText='575' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Name Name=':' Id='637856640183842112' IndexInText='577' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637856640183842125' IndexInText='579' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='IType3' Id='637856640183842118' IndexInText='579' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
								</RegularItems>
								<OtherProperties>
									<WhereKeyword Name='where' Id='637856640183842055' IndexInText='569' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'/>
									<TypeName Name='T3' Id='637856640183842106' IndexInText='575' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<TypeConstraints ObjectId='245' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640183842125' IndexInText='579' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</TypeConstraints>
								</OtherProperties>
							</CustomExpressionItemPart>
							<Name Name='whereend' Id='637856640183842140' IndexInText='586' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<CustomExpressionItemPart Id='637856640183842093' IndexInText='569' ItemLength='16' Interface='UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem'/>
						</Children>
						<OtherProperties>
							<WhereEndMarker Name='whereend' Id='637856640183842140' IndexInText='586' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<GenericTypeDataExpressionItems ObjectId='247' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem]'>
								<UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem Id='637856640183842093' IndexInText='569' ItemLength='16'/>
							</GenericTypeDataExpressionItems>
							<ErrorsPositionDisplayValue value='569' type='System.Int32' />
						</OtherProperties>
					</Custom>
					<CodeBlock Id='637856640183842178' IndexInText='596' ItemLength='48' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
						<RegularItems>
							<CodeBlockStartMarker Name='{' Id='637856640183842172' IndexInText='596' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<BinaryOperator Name=':' Priority='0' Id='637856640183842597' IndexInText='607' ItemLength='29' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
								<Operand1.Braces Id='637856640183842324' IndexInText='607' ItemLength='22' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
									<AppliedKeywords>
										<Keyword Name='public' Id='637856640183842201' IndexInText='607' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
											<LanguageKeywordInfo ObjectId='130' Id='637783669980406419' Keyword='public' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
										</Keyword>
										<Keyword Name='abstract' Id='637856640183842207' IndexInText='614' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
											<LanguageKeywordInfo ObjectId='187' Id='637793546145647499' Keyword='abstract' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
										</Keyword>
									</AppliedKeywords>
									<RegularItems>
										<Literal Id='637856640183842289' IndexInText='623' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
											<NameExpressionItem Name='Move' Id='637856640183842285' IndexInText='623' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</Literal>
										<OpeningRoundBrace Name='(' Id='637856640183842329' IndexInText='627' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										<ClosingRoundBrace Name=')' Id='637856640183842359' IndexInText='628' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</RegularItems>
									<OtherProperties>
										<NamedExpressionItem Id='637856640183842289' IndexInText='623' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
										<OpeningBraceInfo Name='(' Id='637856640183842329' IndexInText='627' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										<ClosingBraceInfo Name=')' Id='637856640183842359' IndexInText='628' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										<Parameters ObjectId='258' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'/>
									</OtherProperties>
								</Operand1.Braces>
								<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637856640183842416' IndexInText='630' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
									<OperatorNameParts>
										<Name Name=':' Id='637856640183842409' IndexInText='630' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</OperatorNameParts>
								</OperatorInfo>
								<Operand2.Literal Id='637856640183842560' IndexInText='632' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
									<NameExpressionItem Name='void' Id='637856640183842555' IndexInText='632' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</Operand2.Literal>
							</BinaryOperator>
							<ExpressionSeparator Name=';' Id='637856640183842619' IndexInText='636' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637856640183842646' IndexInText='643' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<BinaryOperator Name=':' Priority='0' Id='637856640183842597' IndexInText='607' ItemLength='29' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
						</Children>
						<OtherProperties>
							<CodeBlockStartMarker Name='{' Id='637856640183842172' IndexInText='596' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637856640183842646' IndexInText='643' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OtherProperties>
					</CodeBlock>
				</Postfixes>
			</Literal>
			<BinaryOperator Name=':' Priority='0' Id='637856640183843872' IndexInText='648' ItemLength='88' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
				<Operand1.Literal Id='637856640183842754' IndexInText='648' ItemLength='16' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
					<NameExpressionItem Name='Dog' Id='637856640183842750' IndexInText='661' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<AppliedKeywords>
						<Keyword Name='public' Id='637856640183842671' IndexInText='648' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
							<LanguageKeywordInfo ObjectId='130' Id='637783669980406419' Keyword='public' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
						</Keyword>
						<Keyword Name='class' Id='637856640183842680' IndexInText='655' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
							<LanguageKeywordInfo ObjectId='189' Id='637783670388745402' Keyword='class' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
						</Keyword>
					</AppliedKeywords>
				</Operand1.Literal>
				<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637856640183842805' IndexInText='665' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
					<OperatorNameParts>
						<Name Name=':' Id='637856640183842800' IndexInText='665' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</OperatorNameParts>
				</OperatorInfo>
				<Operand2.Braces Id='637856640183842935' IndexInText='667' ItemLength='69' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
					<RegularItems>
						<OpeningRoundBrace Name='(' Id='637856640183842939' IndexInText='667' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<Literal Id='637856640183843025' IndexInText='668' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='Animal' Id='637856640183843020' IndexInText='668' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Literal>
						<ClosingRoundBrace Name=')' Id='637856640183843055' IndexInText='674' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</RegularItems>
					<Postfixes>
						<CodeBlock Id='637856640183843086' IndexInText='677' ItemLength='59' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
							<RegularItems>
								<CodeBlockStartMarker Name='{' Id='637856640183843082' IndexInText='677' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<BinaryOperator Name='=>' Priority='1000' Id='637856640183843814' IndexInText='684' ItemLength='48' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
									<Operand1.BinaryOperator Name=':' Priority='0' Id='637856640183843800' IndexInText='684' ItemLength='29' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
										<Operand1.Braces Id='637856640183843219' IndexInText='684' ItemLength='22' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
											<AppliedKeywords>
												<Keyword Name='public' Id='637856640183843104' IndexInText='684' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
													<LanguageKeywordInfo ObjectId='130' Id='637783669980406419' Keyword='public' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
												</Keyword>
												<Keyword Name='override' Id='637856640183843111' IndexInText='691' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
													<LanguageKeywordInfo ObjectId='284' Id='637793548069818537' Keyword='override' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
												</Keyword>
											</AppliedKeywords>
											<RegularItems>
												<Literal Id='637856640183843191' IndexInText='700' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
													<NameExpressionItem Name='Move' Id='637856640183843187' IndexInText='700' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												</Literal>
												<OpeningRoundBrace Name='(' Id='637856640183843223' IndexInText='704' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												<ClosingRoundBrace Name=')' Id='637856640183843252' IndexInText='705' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											</RegularItems>
											<OtherProperties>
												<NamedExpressionItem Id='637856640183843191' IndexInText='700' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
												<OpeningBraceInfo Name='(' Id='637856640183843223' IndexInText='704' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												<ClosingBraceInfo Name=')' Id='637856640183843252' IndexInText='705' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												<Parameters ObjectId='289' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'/>
											</OtherProperties>
										</Operand1.Braces>
										<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637856640183843299' IndexInText='707' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
											<OperatorNameParts>
												<Name Name=':' Id='637856640183843294' IndexInText='707' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											</OperatorNameParts>
										</OperatorInfo>
										<Operand2.Literal Id='637856640183843436' IndexInText='709' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
											<NameExpressionItem Name='void' Id='637856640183843431' IndexInText='709' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</Operand2.Literal>
									</Operand1.BinaryOperator>
									<OperatorInfo OperatorType='BinaryOperator' Name='=>' Priority='1000' Id='637856640183843510' IndexInText='714' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
										<OperatorNameParts>
											<Name Name='=>' Id='637856640183843504' IndexInText='714' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</OperatorNameParts>
									</OperatorInfo>
									<Operand2.Braces Id='637856640183843649' IndexInText='717' ItemLength='15' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
										<RegularItems>
											<Literal Id='637856640183843619' IndexInText='717' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
												<NameExpressionItem Name='println' Id='637856640183843614' IndexInText='717' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											</Literal>
											<OpeningRoundBrace Name='(' Id='637856640183843652' IndexInText='724' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<ConstantText Id='637856640183843742' IndexInText='725' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
												<NameExpressionItem Name='"Jump"' Id='637856640183843736' IndexInText='725' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											</ConstantText>
											<ClosingRoundBrace Name=')' Id='637856640183843772' IndexInText='731' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</RegularItems>
										<Children>
											<ConstantText Id='637856640183843742' IndexInText='725' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
										</Children>
										<OtherProperties>
											<NamedExpressionItem Id='637856640183843619' IndexInText='717' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
											<OpeningBraceInfo Name='(' Id='637856640183843652' IndexInText='724' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<ClosingBraceInfo Name=')' Id='637856640183843772' IndexInText='731' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<Parameters ObjectId='303' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
												<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640183843742' IndexInText='725' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
											</Parameters>
										</OtherProperties>
									</Operand2.Braces>
								</BinaryOperator>
								<ExpressionSeparator Name=';' Id='637856640183843838' IndexInText='732' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<CodeBlockEndMarker Name='}' Id='637856640183843864' IndexInText='735' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</RegularItems>
							<Children>
								<BinaryOperator Name='=>' Priority='1000' Id='637856640183843814' IndexInText='684' ItemLength='48' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
							</Children>
							<OtherProperties>
								<CodeBlockStartMarker Name='{' Id='637856640183843082' IndexInText='677' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<CodeBlockEndMarker Name='}' Id='637856640183843864' IndexInText='735' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OtherProperties>
						</CodeBlock>
					</Postfixes>
					<Children>
						<Literal Id='637856640183843025' IndexInText='668' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					</Children>
					<OtherProperties>
						<OpeningBraceInfo Name='(' Id='637856640183842939' IndexInText='667' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<ClosingBraceInfo Name=')' Id='637856640183843055' IndexInText='674' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
						<Parameters ObjectId='306' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
							<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640183843025' IndexInText='668' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
						</Parameters>
					</OtherProperties>
				</Operand2.Braces>
			</BinaryOperator>
		</RegularItems>
		<Children>
			<BinaryOperator Name='=' Priority='2000' Id='637856640183765059' IndexInText='0' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
			<BinaryOperator Name='=' Priority='2000' Id='637856640183768162' IndexInText='24' ItemLength='116' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
			<Braces Id='637856640183770288' IndexInText='145' ItemLength='35' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
			<BinaryOperator Name='=>' Priority='1000' Id='637856640183840451' IndexInText='185' ItemLength='237' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
			<Literal Id='637856640183841768' IndexInText='426' ItemLength='218' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
			<BinaryOperator Name=':' Priority='0' Id='637856640183843872' IndexInText='648' ItemLength='88' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
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
	<MainExpressionItem Id='637856640183763621' IndexInText='0' ItemLength='736' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'/>
</UniversalExpressionParser.ExpressionItems.IRootExpressionItemSeries>
```
</details>

- The format of valid expressions is defined by properties and methods in interface **UniversalExpressionParser.IExpressionLanguageProvider**. An instance of this interface is passed as a parameter to method **ParseExpression(...)** in ****UniversalExpressionParser.IExpressionParser****, as demonstrated in example above. Most of the properties and methods of this interface are demonstrated in examples in sections below.

- The default abstract implementation of this interface in this package is **UniversalExpressionParser.ExpressionLanguageProviderBase**. In most cases, this abstract method can be extended and abstract methods and properties can be implemented, rather than providing a brand new implementation of **UniversalExpressionParser.IExpressionLanguageProvider**.

- The test project **UniversalExpressionParser.Tests** in git repository has a number of tests for testing successful parsing, as well as tests for testing expressions that result in errors (see section **Error Reporting** below). These tests generate random expressions as well as generate randomly configured instances of **UniversalExpressionParser.IExpressionLanguageProvider** to validate parsing of thousands of all possible languages and expressions (see the test classes **UniversalExpressionParser.Tests.SuccessfulParseTests.ExpressionParserSuccessfulTests** and **UniversalExpressionParser.Tests.ExpressionParseErrorTests.ExpressionParseErrorTests**).

- The demo expressions and tests used to parse the demo expressions in this documentation are in folder **Demos** in test project **UniversalExpressionParser.Tests**. This documentation uses implementations of **UniversalExpressionParser.IExpressionLanguageProvider** in project **UniversalExpressionParser.DemoExpressionLanguageProviders** in **git** repository.

- The parsed expressions in this documentation (i.e., instances of **UniversalExpressionParser.ExpressionItems.IRootExpressionItem**) are visualized into xml texts, that contain values of most properties of the parsed expression. However, to make the files shorter, the visualized xml files do not include all the property values.

# Literals

- Literals are names that can be used either alone (say in operators) or can be  part of more complex expressions. For example literals can precede opening square or round braces (e.g., **f1** in **f1(x)**, or **m1** in **m1[1, 2]**),
or code blocks (e.g., **Dog** in expression **public class Dog {}**).

- Literals are parsed into expression items of type **UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem** objects.

- If literal precedes round or square braces, it will be stored in value of property **INamedComplexExpressionItem NamedExpressionItem { get; }** of **UniversalExpressionParser.ExpressionItems.IBracesExpressionItem**.

- If literal precedes a code block staring marker (e.g., **Dog** in expression **public class Dog {}**), then the code block is added to the list **UniversalExpressionParser.ExpressionItems.IComplexExpressionItem.Postfixes** in expression item for the literal (see section **Postfixes** for more details on postfixes).

- Literals are texts that cannot have space and can only contain characters validated by method **UniversalExpressionParser.IExpressionLanguageProvider.IsValidLiteralCharacter(char character, int positionInLiteral, ITextSymbolsParserState textSymbolsParserState)** in interface **UniversalExpressionParser.IExpressionLanguageProvider**. 
  In other words, a literal can contain any character (including numeric or operator characters, a dot '.', '_', etc.), that is considered a valid literal character by method **IExpressionLanguageProvider.IsValidLiteralCharacter(...)**.

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
	<ExpressionItemSeries Id='637856640181588678' IndexInText='106' ItemLength='470' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'>
		<RegularItems>
			<BinaryOperator Name='=' Priority='2000' Id='637856640181673940' IndexInText='106' ItemLength='31' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
				<Operand1.Literal Id='637856640181589136' IndexInText='106' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
					<NameExpressionItem Name='_x' Id='637856640181589131' IndexInText='110' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<AppliedKeywords>
						<Keyword Name='var' Id='637856640181588973' IndexInText='106' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
							<LanguageKeywordInfo ObjectId='6' Id='637781064051574641' Keyword='var' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
						</Keyword>
					</AppliedKeywords>
				</Operand1.Literal>
				<OperatorInfo OperatorType='BinaryOperator' Name='=' Priority='2000' Id='637856640181589247' IndexInText='113' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
					<OperatorNameParts>
						<Name Name='=' Id='637856640181589232' IndexInText='113' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</OperatorNameParts>
				</OperatorInfo>
				<Operand2.BinaryOperator Name='+' Priority='30' Id='637856640181673967' IndexInText='115' ItemLength='22' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
					<Operand1.Braces Id='637856640181589472' IndexInText='115' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<Literal Id='637856640181589394' IndexInText='115' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='f$' Id='637856640181589389' IndexInText='115' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<OpeningRoundBrace Name='(' Id='637856640181589477' IndexInText='117' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Literal Id='637856640181589575' IndexInText='118' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='x1' Id='637856640181589570' IndexInText='118' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<Comma Name=',' Id='637856640181589617' IndexInText='120' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Literal Id='637856640181589699' IndexInText='122' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='x2' Id='637856640181589691' IndexInText='122' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<ClosingRoundBrace Name=')' Id='637856640181589730' IndexInText='124' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<Literal Id='637856640181589575' IndexInText='118' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							<Literal Id='637856640181589699' IndexInText='122' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
						</Children>
						<OtherProperties>
							<NamedExpressionItem Id='637856640181589394' IndexInText='115' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							<OpeningBraceInfo Name='(' Id='637856640181589477' IndexInText='117' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=')' Id='637856640181589730' IndexInText='124' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Parameters ObjectId='20' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640181589575' IndexInText='118' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640181589699' IndexInText='122' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Operand1.Braces>
					<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637856640181589791' IndexInText='126' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
						<OperatorNameParts>
							<Name Name='+' Id='637856640181589785' IndexInText='126' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OperatorNameParts>
					</OperatorInfo>
					<Operand2.Braces Id='637856640181589973' IndexInText='128' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<Literal Id='637856640181589930' IndexInText='128' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='m1' Id='637856640181589925' IndexInText='128' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<OpeningSquareBrace Name='[' Id='637856640181589977' IndexInText='130' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ConstantNumericValue Id='637856640181673524' IndexInText='131' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
								<NameExpressionItem Name='1' Id='637856640181673475' IndexInText='131' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<OtherProperties>
									<SucceededNumericTypeDescriptor ObjectId='29' NumberTypeId='1581134183960' Interface='UniversalExpressionParser.NumericTypeDescriptor'>
										<RegularExpressions ObjectId='30' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[System.String]'>
											<System.String value='^\d+' />
										</RegularExpressions>
									</SucceededNumericTypeDescriptor>
								</OtherProperties>
							</ConstantNumericValue>
							<Comma Name=',' Id='637856640181673692' IndexInText='132' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Literal Id='637856640181673836' IndexInText='134' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='x3' Id='637856640181673831' IndexInText='134' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<ClosingSquareBrace Name=']' Id='637856640181673883' IndexInText='136' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<ConstantNumericValue Id='637856640181673524' IndexInText='131' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'/>
							<Literal Id='637856640181673836' IndexInText='134' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
						</Children>
						<OtherProperties>
							<NamedExpressionItem Id='637856640181589930' IndexInText='128' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							<OpeningBraceInfo Name='[' Id='637856640181589977' IndexInText='130' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=']' Id='637856640181673883' IndexInText='136' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Parameters ObjectId='35' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640181673524' IndexInText='131' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'/>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640181673836' IndexInText='134' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Operand2.Braces>
				</Operand2.BinaryOperator>
			</BinaryOperator>
			<ExpressionSeparator Name=';' Id='637856640181674020' IndexInText='137' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<Literal Id='637856640181674152' IndexInText='142' ItemLength='61' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
				<NameExpressionItem Name='Dog' Id='637856640181674147' IndexInText='155' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				<AppliedKeywords>
					<Keyword Name='public' Id='637856640181674051' IndexInText='142' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
						<LanguageKeywordInfo ObjectId='40' Id='637783669980406419' Keyword='public' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
					</Keyword>
					<Keyword Name='class' Id='637856640181674064' IndexInText='149' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
						<LanguageKeywordInfo ObjectId='42' Id='637783670388745402' Keyword='class' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
					</Keyword>
				</AppliedKeywords>
				<Postfixes>
					<CodeBlock Id='637856640181674190' IndexInText='160' ItemLength='43' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
						<RegularItems>
							<CodeBlockStartMarker Name='{' Id='637856640181674185' IndexInText='160' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<BinaryOperator Name='=>' Priority='1000' Id='637856640181674787' IndexInText='167' ItemLength='32' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
								<Operand1.BinaryOperator Name=':' Priority='0' Id='637856640181674777' IndexInText='167' ItemLength='21' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
									<Operand1.Literal Id='637856640181674301' IndexInText='167' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='Color' Id='637856640181674297' IndexInText='174' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										<AppliedKeywords>
											<Keyword Name='public' Id='637856640181674219' IndexInText='167' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
												<LanguageKeywordInfo ObjectId='40' Id='637783669980406419' Keyword='public' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
											</Keyword>
										</AppliedKeywords>
									</Operand1.Literal>
									<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637856640181674359' IndexInText='180' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
										<OperatorNameParts>
											<Name Name=':' Id='637856640181674346' IndexInText='180' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</OperatorNameParts>
									</OperatorInfo>
									<Operand2.Literal Id='637856640181674521' IndexInText='182' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='string' Id='637856640181674517' IndexInText='182' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Operand2.Literal>
								</Operand1.BinaryOperator>
								<OperatorInfo OperatorType='BinaryOperator' Name='=>' Priority='1000' Id='637856640181674607' IndexInText='189' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
									<OperatorNameParts>
										<Name Name='=>' Id='637856640181674601' IndexInText='189' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</OperatorNameParts>
								</OperatorInfo>
								<Operand2.ConstantText Id='637856640181674735' IndexInText='192' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
									<NameExpressionItem Name='"brown"' Id='637856640181674730' IndexInText='192' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</Operand2.ConstantText>
							</BinaryOperator>
							<ExpressionSeparator Name=';' Id='637856640181674809' IndexInText='199' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637856640181674837' IndexInText='202' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<BinaryOperator Name='=>' Priority='1000' Id='637856640181674787' IndexInText='167' ItemLength='32' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
						</Children>
						<OtherProperties>
							<CodeBlockStartMarker Name='{' Id='637856640181674185' IndexInText='160' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637856640181674837' IndexInText='202' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OtherProperties>
					</CodeBlock>
				</Postfixes>
			</Literal>
			<Braces Id='637856640181674981' IndexInText='557' ItemLength='18' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
				<RegularItems>
					<Literal Id='637856640181674953' IndexInText='557' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='println' Id='637856640181674948' IndexInText='557' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<OpeningRoundBrace Name='(' Id='637856640181674985' IndexInText='564' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Literal Id='637856640181675095' IndexInText='565' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='Dog.Color' Id='637856640181675090' IndexInText='565' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<ClosingRoundBrace Name=')' Id='637856640181675125' IndexInText='574' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Children>
					<Literal Id='637856640181675095' IndexInText='565' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
				</Children>
				<OtherProperties>
					<NamedExpressionItem Id='637856640181674953' IndexInText='557' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					<OpeningBraceInfo Name='(' Id='637856640181674985' IndexInText='564' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ClosingBraceInfo Name=')' Id='637856640181675125' IndexInText='574' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Parameters ObjectId='67' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640181675095' IndexInText='565' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					</Parameters>
				</OtherProperties>
			</Braces>
			<ExpressionSeparator Name=';' Id='637856640181675152' IndexInText='575' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
		</RegularItems>
		<Children>
			<BinaryOperator Name='=' Priority='2000' Id='637856640181673940' IndexInText='106' ItemLength='31' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
			<Literal Id='637856640181674152' IndexInText='142' ItemLength='61' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
			<Braces Id='637856640181674981' IndexInText='557' ItemLength='18' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
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
	<MainExpressionItem Id='637856640181588678' IndexInText='106' ItemLength='470' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'/>
</UniversalExpressionParser.ExpressionItems.IRootExpressionItemSeries>
```
</details>

# Functions and Braces

- Braces are a pair of round or square braces ((e.g., **(x)**, **(x) {}**, **[i,j]**, **[i,j]{}**)). Braces are parsed to expression items of type **UniversalExpressionParser.ExpressionItems.IBracesExpressionItem** with the value of property **INamedComplexExpressionItem NamedExpressionItem { get; }** equal to null.
- Functions are round or square braces preceded with a literal (e.g., **F1(x)**, **F1(x) {}**, **m1[i,j]**, **m1[i,j]{}**). Functions are  parsed to expression items of type **UniversalExpressionParser.ExpressionItems.IBracesExpressionItem** with the value of property **INamedComplexExpressionItem NamedExpressionItem { get; }** equal to a literal that precedes the braces.

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
	<ExpressionItemSeries Id='637856640181097285' IndexInText='150' ItemLength='71' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'>
		<RegularItems>
			<BinaryOperator Name='=' Priority='2000' Id='637856640181182117' IndexInText='150' ItemLength='41' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
				<Operand1.Literal Id='637856640181097658' IndexInText='150' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
					<NameExpressionItem Name='x' Id='637856640181097653' IndexInText='154' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<AppliedKeywords>
						<Keyword Name='var' Id='637856640181097514' IndexInText='150' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
							<LanguageKeywordInfo ObjectId='6' Id='637781064051574641' Keyword='var' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
						</Keyword>
					</AppliedKeywords>
				</Operand1.Literal>
				<OperatorInfo OperatorType='BinaryOperator' Name='=' Priority='2000' Id='637856640181097760' IndexInText='156' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
					<OperatorNameParts>
						<Name Name='=' Id='637856640181097744' IndexInText='156' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</OperatorNameParts>
				</OperatorInfo>
				<Operand2.Braces Id='637856640181097931' IndexInText='158' ItemLength='33' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
					<RegularItems>
						<OpeningRoundBrace Name='(' Id='637856640181097950' IndexInText='158' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<Braces Id='637856640181098043' IndexInText='159' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
							<RegularItems>
								<OpeningRoundBrace Name='(' Id='637856640181098047' IndexInText='159' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<Literal Id='637856640181098153' IndexInText='160' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
									<NameExpressionItem Name='x1' Id='637856640181098148' IndexInText='160' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</Literal>
								<Comma Name=',' Id='637856640181098195' IndexInText='162' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<Literal Id='637856640181098310' IndexInText='164' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
									<NameExpressionItem Name='x2' Id='637856640181098270' IndexInText='164' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</Literal>
								<Comma Name=',' Id='637856640181098369' IndexInText='166' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<Literal Id='637856640181098457' IndexInText='168' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
									<NameExpressionItem Name='x3' Id='637856640181098453' IndexInText='168' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</Literal>
								<ClosingRoundBrace Name=')' Id='637856640181098499' IndexInText='170' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</RegularItems>
							<Children>
								<Literal Id='637856640181098153' IndexInText='160' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								<Literal Id='637856640181098310' IndexInText='164' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								<Literal Id='637856640181098457' IndexInText='168' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							</Children>
							<OtherProperties>
								<OpeningBraceInfo Name='(' Id='637856640181098047' IndexInText='159' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<ClosingBraceInfo Name=')' Id='637856640181098499' IndexInText='170' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
								<Parameters ObjectId='22' Count='3' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
									<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640181098153' IndexInText='160' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640181098310' IndexInText='164' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640181098457' IndexInText='168' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								</Parameters>
							</OtherProperties>
						</Braces>
						<Comma Name=',' Id='637856640181098535' IndexInText='171' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<Braces Id='637856640181098560' IndexInText='173' ItemLength='14' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
							<RegularItems>
								<OpeningSquareBrace Name='[' Id='637856640181098563' IndexInText='173' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<Literal Id='637856640181098647' IndexInText='174' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
									<NameExpressionItem Name='x4' Id='637856640181098643' IndexInText='174' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</Literal>
								<Comma Name=',' Id='637856640181098675' IndexInText='176' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<BinaryOperator Name='+' Priority='30' Id='637856640181181645' IndexInText='178' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
									<Operand1.Literal Id='637856640181098755' IndexInText='178' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='x5' Id='637856640181098752' IndexInText='178' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Operand1.Literal>
									<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637856640181098811' IndexInText='180' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
										<OperatorNameParts>
											<Name Name='+' Id='637856640181098803' IndexInText='180' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</OperatorNameParts>
									</OperatorInfo>
									<Operand2.ConstantNumericValue Id='637856640181181442' IndexInText='181' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
										<NameExpressionItem Name='1' Id='637856640181181379' IndexInText='181' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										<OtherProperties>
											<SucceededNumericTypeDescriptor ObjectId='36' NumberTypeId='1581134183960' Interface='UniversalExpressionParser.NumericTypeDescriptor'>
												<RegularExpressions ObjectId='37' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[System.String]'>
													<System.String value='^\d+' />
												</RegularExpressions>
											</SucceededNumericTypeDescriptor>
										</OtherProperties>
									</Operand2.ConstantNumericValue>
								</BinaryOperator>
								<Comma Name=',' Id='637856640181181731' IndexInText='182' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<Literal Id='637856640181181887' IndexInText='184' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
									<NameExpressionItem Name='x6' Id='637856640181181882' IndexInText='184' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</Literal>
								<ClosingSquareBrace Name=']' Id='637856640181181926' IndexInText='186' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</RegularItems>
							<Children>
								<Literal Id='637856640181098647' IndexInText='174' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								<BinaryOperator Name='+' Priority='30' Id='637856640181181645' IndexInText='178' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
								<Literal Id='637856640181181887' IndexInText='184' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							</Children>
							<OtherProperties>
								<OpeningBraceInfo Name='[' Id='637856640181098563' IndexInText='173' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<ClosingBraceInfo Name=']' Id='637856640181181926' IndexInText='186' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
								<Parameters ObjectId='42' Count='3' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
									<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640181098647' IndexInText='174' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name='+' Priority='30' Id='637856640181181645' IndexInText='178' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
									<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640181181887' IndexInText='184' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								</Parameters>
							</OtherProperties>
						</Braces>
						<Comma Name=',' Id='637856640181181974' IndexInText='187' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<Literal Id='637856640181182057' IndexInText='189' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='y' Id='637856640181182053' IndexInText='189' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Literal>
						<ClosingRoundBrace Name=')' Id='637856640181182084' IndexInText='190' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</RegularItems>
					<Children>
						<Braces Id='637856640181098043' IndexInText='159' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
						<Braces Id='637856640181098560' IndexInText='173' ItemLength='14' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
						<Literal Id='637856640181182057' IndexInText='189' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					</Children>
					<OtherProperties>
						<OpeningBraceInfo Name='(' Id='637856640181097950' IndexInText='158' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<ClosingBraceInfo Name=')' Id='637856640181182084' IndexInText='190' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
						<Parameters ObjectId='47' Count='3' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
							<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640181098043' IndexInText='159' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
							<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640181098560' IndexInText='173' ItemLength='14' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
							<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640181182057' IndexInText='189' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
						</Parameters>
					</OtherProperties>
				</Operand2.Braces>
			</BinaryOperator>
			<ExpressionSeparator Name=';' Id='637856640181182141' IndexInText='191' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<BinaryOperator Name='+=' Priority='2000' Id='637856640181268420' IndexInText='194' ItemLength='26' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
				<Operand1.Literal Id='637856640181182225' IndexInText='194' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
					<NameExpressionItem Name='x' Id='637856640181182221' IndexInText='194' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</Operand1.Literal>
				<OperatorInfo OperatorType='BinaryOperator' Name='+=' Priority='2000' Id='637856640181182345' IndexInText='196' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
					<OperatorNameParts>
						<Name Name='+=' Id='637856640181182339' IndexInText='196' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</OperatorNameParts>
				</OperatorInfo>
				<Operand2.BinaryOperator Name='+' Priority='30' Id='637856640181268448' IndexInText='199' ItemLength='21' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
					<Operand1.Braces Id='637856640181182455' IndexInText='199' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<OpeningRoundBrace Name='(' Id='637856640181182459' IndexInText='199' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Literal Id='637856640181182575' IndexInText='200' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='x2' Id='637856640181182570' IndexInText='200' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<Comma Name=',' Id='637856640181182613' IndexInText='202' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Literal Id='637856640181182690' IndexInText='204' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='x4' Id='637856640181182686' IndexInText='204' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<ClosingRoundBrace Name=')' Id='637856640181182752' IndexInText='206' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<Literal Id='637856640181182575' IndexInText='200' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							<Literal Id='637856640181182690' IndexInText='204' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
						</Children>
						<OtherProperties>
							<OpeningBraceInfo Name='(' Id='637856640181182459' IndexInText='199' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=')' Id='637856640181182752' IndexInText='206' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
							<Parameters ObjectId='63' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640181182575' IndexInText='200' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640181182690' IndexInText='204' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Operand1.Braces>
					<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637856640181182808' IndexInText='208' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
						<OperatorNameParts>
							<Name Name='+' Id='637856640181182802' IndexInText='208' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OperatorNameParts>
					</OperatorInfo>
					<Operand2.BinaryOperator Name='*' Priority='20' Id='637856640181268515' IndexInText='210' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.ConstantNumericValue Id='637856640181267625' IndexInText='210' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
							<NameExpressionItem Name='2' Id='637856640181267568' IndexInText='210' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<OtherProperties>
								<SucceededNumericTypeDescriptor ObjectId='36' NumberTypeId='1581134183960' Interface='UniversalExpressionParser.NumericTypeDescriptor'/>
							</OtherProperties>
						</Operand1.ConstantNumericValue>
						<OperatorInfo OperatorType='BinaryOperator' Name='*' Priority='20' Id='637856640181267932' IndexInText='211' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<Name Name='*' Id='637856640181267917' IndexInText='211' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OperatorNameParts>
						</OperatorInfo>
						<Operand2.Braces Id='637856640181268089' IndexInText='212' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
							<RegularItems>
								<OpeningSquareBrace Name='[' Id='637856640181268094' IndexInText='212' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<Literal Id='637856640181268206' IndexInText='213' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
									<NameExpressionItem Name='x3' Id='637856640181268201' IndexInText='213' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</Literal>
								<Comma Name=',' Id='637856640181268254' IndexInText='215' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<Literal Id='637856640181268336' IndexInText='217' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
									<NameExpressionItem Name='x4' Id='637856640181268329' IndexInText='217' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</Literal>
								<ClosingSquareBrace Name=']' Id='637856640181268368' IndexInText='219' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</RegularItems>
							<Children>
								<Literal Id='637856640181268206' IndexInText='213' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								<Literal Id='637856640181268336' IndexInText='217' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							</Children>
							<OtherProperties>
								<OpeningBraceInfo Name='[' Id='637856640181268094' IndexInText='212' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<ClosingBraceInfo Name=']' Id='637856640181268368' IndexInText='219' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
								<Parameters ObjectId='79' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
									<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640181268206' IndexInText='213' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640181268336' IndexInText='217' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								</Parameters>
							</OtherProperties>
						</Operand2.Braces>
					</Operand2.BinaryOperator>
				</Operand2.BinaryOperator>
			</BinaryOperator>
			<ExpressionSeparator Name=';' Id='637856640181268583' IndexInText='220' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
		</RegularItems>
		<Children>
			<BinaryOperator Name='=' Priority='2000' Id='637856640181182117' IndexInText='150' ItemLength='41' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
			<BinaryOperator Name='+=' Priority='2000' Id='637856640181268420' IndexInText='194' ItemLength='26' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
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
	<MainExpressionItem Id='637856640181097285' IndexInText='150' ItemLength='71' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'/>
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
	<ExpressionItemSeries Id='637856640180681372' IndexInText='150' ItemLength='249' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'>
		<RegularItems>
			<BinaryOperator Name='=' Priority='2000' Id='637856640181043536' IndexInText='150' ItemLength='87' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
				<Operand1.Literal Id='637856640180681707' IndexInText='150' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
					<NameExpressionItem Name='x' Id='637856640180681702' IndexInText='154' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<AppliedKeywords>
						<Keyword Name='var' Id='637856640180681574' IndexInText='150' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
							<LanguageKeywordInfo ObjectId='6' Id='637781064051574641' Keyword='var' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
						</Keyword>
					</AppliedKeywords>
				</Operand1.Literal>
				<OperatorInfo OperatorType='BinaryOperator' Name='=' Priority='2000' Id='637856640180681827' IndexInText='156' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
					<OperatorNameParts>
						<Name Name='=' Id='637856640180681816' IndexInText='156' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</OperatorNameParts>
				</OperatorInfo>
				<Operand2.BinaryOperator Name='+' Priority='30' Id='637856640181043555' IndexInText='158' ItemLength='79' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
					<Operand1.BinaryOperator Name='+' Priority='30' Id='637856640181043545' IndexInText='158' ItemLength='23' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.Literal Id='637856640180681980' IndexInText='158' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='x1' Id='637856640180681974' IndexInText='158' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand1.Literal>
						<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637856640180682057' IndexInText='160' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<Name Name='+' Id='637856640180682050' IndexInText='160' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OperatorNameParts>
						</OperatorInfo>
						<Operand2.Braces Id='637856640180682225' IndexInText='161' ItemLength='20' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
							<RegularItems>
								<Literal Id='637856640180682188' IndexInText='161' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
									<NameExpressionItem Name='f1' Id='637856640180682183' IndexInText='161' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</Literal>
								<OpeningRoundBrace Name='(' Id='637856640180682231' IndexInText='163' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<Literal Id='637856640180682321' IndexInText='164' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
									<NameExpressionItem Name='x2' Id='637856640180682315' IndexInText='164' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</Literal>
								<Comma Name=',' Id='637856640180682362' IndexInText='166' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<BinaryOperator Name='+' Priority='30' Id='637856640180853245' IndexInText='168' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
									<Operand1.BinaryOperator Name='+' Priority='30' Id='637856640180853210' IndexInText='168' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
										<Operand1.Literal Id='637856640180682445' IndexInText='168' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
											<NameExpressionItem Name='x3' Id='637856640180682441' IndexInText='168' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</Operand1.Literal>
										<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637856640180682500' IndexInText='170' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
											<OperatorNameParts>
												<Name Name='+' Id='637856640180682495' IndexInText='170' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											</OperatorNameParts>
										</OperatorInfo>
										<Operand2.BinaryOperator Name='*' Priority='20' Id='637856640180853235' IndexInText='171' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
											<Operand1.Literal Id='637856640180682651' IndexInText='171' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
												<NameExpressionItem Name='x4' Id='637856640180682647' IndexInText='171' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											</Operand1.Literal>
											<OperatorInfo OperatorType='BinaryOperator' Name='*' Priority='20' Id='637856640180682704' IndexInText='173' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
												<OperatorNameParts>
													<Name Name='*' Id='637856640180682699' IndexInText='173' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												</OperatorNameParts>
											</OperatorInfo>
											<Operand2.ConstantNumericValue Id='637856640180769433' IndexInText='174' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
												<NameExpressionItem Name='5' Id='637856640180769376' IndexInText='174' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
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
									<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637856640180769732' IndexInText='175' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
										<OperatorNameParts>
											<Name Name='+' Id='637856640180769718' IndexInText='175' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</OperatorNameParts>
									</OperatorInfo>
									<Operand2.Braces Id='637856640180769965' IndexInText='176' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
										<RegularItems>
											<Literal Id='637856640180769922' IndexInText='176' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
												<NameExpressionItem Name='x' Id='637856640180769917' IndexInText='176' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											</Literal>
											<OpeningSquareBrace Name='[' Id='637856640180769969' IndexInText='177' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<ConstantNumericValue Id='637856640180852989' IndexInText='178' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
												<NameExpressionItem Name='1' Id='637856640180852941' IndexInText='178' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												<OtherProperties>
													<SucceededNumericTypeDescriptor ObjectId='35' NumberTypeId='1581134183960' Interface='UniversalExpressionParser.NumericTypeDescriptor'/>
												</OtherProperties>
											</ConstantNumericValue>
											<ClosingSquareBrace Name=']' Id='637856640180853147' IndexInText='179' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</RegularItems>
										<Children>
											<ConstantNumericValue Id='637856640180852989' IndexInText='178' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'/>
										</Children>
										<OtherProperties>
											<NamedExpressionItem Id='637856640180769922' IndexInText='176' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
											<OpeningBraceInfo Name='[' Id='637856640180769969' IndexInText='177' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<ClosingBraceInfo Name=']' Id='637856640180853147' IndexInText='179' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<Parameters ObjectId='46' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
												<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640180852989' IndexInText='178' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'/>
											</Parameters>
										</OtherProperties>
									</Operand2.Braces>
								</BinaryOperator>
								<ClosingRoundBrace Name=')' Id='637856640180853308' IndexInText='180' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</RegularItems>
							<Children>
								<Literal Id='637856640180682321' IndexInText='164' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								<BinaryOperator Name='+' Priority='30' Id='637856640180853245' IndexInText='168' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
							</Children>
							<OtherProperties>
								<NamedExpressionItem Id='637856640180682188' IndexInText='161' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								<OpeningBraceInfo Name='(' Id='637856640180682231' IndexInText='163' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<ClosingBraceInfo Name=')' Id='637856640180853308' IndexInText='180' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<Parameters ObjectId='48' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
									<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640180682321' IndexInText='164' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name='+' Priority='30' Id='637856640180853245' IndexInText='168' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
								</Parameters>
							</OtherProperties>
						</Operand2.Braces>
					</Operand1.BinaryOperator>
					<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637856640180853419' IndexInText='181' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
						<OperatorNameParts>
							<Name Name='+' Id='637856640180853408' IndexInText='181' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OperatorNameParts>
					</OperatorInfo>
					<Operand2.Braces Id='637856640180853636' IndexInText='193' ItemLength='44' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<Literal Id='637856640180853589' IndexInText='193' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='matrix1' Id='637856640180853584' IndexInText='193' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<OpeningSquareBrace Name='[' Id='637856640180853640' IndexInText='200' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Braces Id='637856640180853678' IndexInText='201' ItemLength='14' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
								<RegularItems>
									<OpeningSquareBrace Name='[' Id='637856640180853682' IndexInText='201' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<BinaryOperator Name='+' Priority='30' Id='637856640180948234' IndexInText='202' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
										<Operand1.Literal Id='637856640180853765' IndexInText='202' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
											<NameExpressionItem Name='y1' Id='637856640180853761' IndexInText='202' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</Operand1.Literal>
										<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637856640180853823' IndexInText='204' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
											<OperatorNameParts>
												<Name Name='+' Id='637856640180853818' IndexInText='204' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											</OperatorNameParts>
										</OperatorInfo>
										<Operand2.ConstantNumericValue Id='637856640180947990' IndexInText='205' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
											<NameExpressionItem Name='3' Id='637856640180947923' IndexInText='205' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<OtherProperties>
												<SucceededNumericTypeDescriptor ObjectId='35' NumberTypeId='1581134183960' Interface='UniversalExpressionParser.NumericTypeDescriptor'/>
											</OtherProperties>
										</Operand2.ConstantNumericValue>
									</BinaryOperator>
									<Comma Name=',' Id='637856640180948335' IndexInText='206' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Braces Id='637856640180948544' IndexInText='208' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
										<RegularItems>
											<Literal Id='637856640180948506' IndexInText='208' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
												<NameExpressionItem Name='f1' Id='637856640180948500' IndexInText='208' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											</Literal>
											<OpeningRoundBrace Name='(' Id='637856640180948549' IndexInText='210' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<Literal Id='637856640180948651' IndexInText='211' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
												<NameExpressionItem Name='x4' Id='637856640180948646' IndexInText='211' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											</Literal>
											<ClosingRoundBrace Name=')' Id='637856640180948683' IndexInText='213' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</RegularItems>
										<Children>
											<Literal Id='637856640180948651' IndexInText='211' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
										</Children>
										<OtherProperties>
											<NamedExpressionItem Id='637856640180948506' IndexInText='208' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
											<OpeningBraceInfo Name='(' Id='637856640180948549' IndexInText='210' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<ClosingBraceInfo Name=')' Id='637856640180948683' IndexInText='213' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<Parameters ObjectId='72' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
												<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640180948651' IndexInText='211' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
											</Parameters>
										</OtherProperties>
									</Braces>
									<ClosingSquareBrace Name=']' Id='637856640180948727' IndexInText='214' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Children>
									<BinaryOperator Name='+' Priority='30' Id='637856640180948234' IndexInText='202' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
									<Braces Id='637856640180948544' IndexInText='208' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
								</Children>
								<OtherProperties>
									<OpeningBraceInfo Name='[' Id='637856640180853682' IndexInText='201' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ClosingBraceInfo Name=']' Id='637856640180948727' IndexInText='214' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
									<Parameters ObjectId='74' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name='+' Priority='30' Id='637856640180948234' IndexInText='202' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640180948544' IndexInText='208' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
									</Parameters>
								</OtherProperties>
							</Braces>
							<Comma Name=',' Id='637856640180948757' IndexInText='215' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Literal Id='637856640180948835' IndexInText='217' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='x3' Id='637856640180948831' IndexInText='217' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<Comma Name=',' Id='637856640180948867' IndexInText='219' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Braces Id='637856640180948977' IndexInText='221' ItemLength='15' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
								<RegularItems>
									<Literal Id='637856640180948948' IndexInText='221' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='f2' Id='637856640180948944' IndexInText='221' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<OpeningRoundBrace Name='(' Id='637856640180948980' IndexInText='223' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637856640180949060' IndexInText='224' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='x3' Id='637856640180949055' IndexInText='224' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<Comma Name=',' Id='637856640180949093' IndexInText='226' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Braces Id='637856640180949200' IndexInText='228' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
										<RegularItems>
											<Literal Id='637856640180949171' IndexInText='228' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
												<NameExpressionItem Name='m2' Id='637856640180949168' IndexInText='228' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											</Literal>
											<OpeningSquareBrace Name='[' Id='637856640180949203' IndexInText='230' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<BinaryOperator Name='+' Priority='30' Id='637856640181043337' IndexInText='231' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
												<Operand1.Literal Id='637856640180949289' IndexInText='231' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
													<NameExpressionItem Name='x' Id='637856640180949284' IndexInText='231' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												</Operand1.Literal>
												<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637856640180949362' IndexInText='232' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
													<OperatorNameParts>
														<Name Name='+' Id='637856640180949352' IndexInText='232' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
													</OperatorNameParts>
												</OperatorInfo>
												<Operand2.ConstantNumericValue Id='637856640181043144' IndexInText='233' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
													<NameExpressionItem Name='5' Id='637856640181043102' IndexInText='233' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
													<OtherProperties>
														<SucceededNumericTypeDescriptor ObjectId='35' NumberTypeId='1581134183960' Interface='UniversalExpressionParser.NumericTypeDescriptor'/>
													</OtherProperties>
												</Operand2.ConstantNumericValue>
											</BinaryOperator>
											<ClosingSquareBrace Name=']' Id='637856640181043413' IndexInText='234' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</RegularItems>
										<Children>
											<BinaryOperator Name='+' Priority='30' Id='637856640181043337' IndexInText='231' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
										</Children>
										<OtherProperties>
											<NamedExpressionItem Id='637856640180949171' IndexInText='228' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
											<OpeningBraceInfo Name='[' Id='637856640180949203' IndexInText='230' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<ClosingBraceInfo Name=']' Id='637856640181043413' IndexInText='234' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<Parameters ObjectId='98' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
												<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name='+' Priority='30' Id='637856640181043337' IndexInText='231' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
											</Parameters>
										</OtherProperties>
									</Braces>
									<ClosingRoundBrace Name=')' Id='637856640181043470' IndexInText='235' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Children>
									<Literal Id='637856640180949060' IndexInText='224' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<Braces Id='637856640180949200' IndexInText='228' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
								</Children>
								<OtherProperties>
									<NamedExpressionItem Id='637856640180948948' IndexInText='221' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<OpeningBraceInfo Name='(' Id='637856640180948980' IndexInText='223' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ClosingBraceInfo Name=')' Id='637856640181043470' IndexInText='235' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Parameters ObjectId='100' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640180949060' IndexInText='224' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640180949200' IndexInText='228' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
									</Parameters>
								</OtherProperties>
							</Braces>
							<ClosingSquareBrace Name=']' Id='637856640181043502' IndexInText='236' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<Braces Id='637856640180853678' IndexInText='201' ItemLength='14' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
							<Literal Id='637856640180948835' IndexInText='217' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							<Braces Id='637856640180948977' IndexInText='221' ItemLength='15' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
						</Children>
						<OtherProperties>
							<NamedExpressionItem Id='637856640180853589' IndexInText='193' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							<OpeningBraceInfo Name='[' Id='637856640180853640' IndexInText='200' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=']' Id='637856640181043502' IndexInText='236' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Parameters ObjectId='102' Count='3' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640180853678' IndexInText='201' ItemLength='14' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640180948835' IndexInText='217' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640180948977' IndexInText='221' ItemLength='15' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Operand2.Braces>
				</Operand2.BinaryOperator>
			</BinaryOperator>
			<ExpressionSeparator Name=';' Id='637856640181043594' IndexInText='237' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<BinaryOperator Name='=>' Priority='1000' Id='637856640181044445' IndexInText='242' ItemLength='15' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
				<Operand1.Braces Id='637856640181043762' IndexInText='242' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
					<RegularItems>
						<Literal Id='637856640181043731' IndexInText='242' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='f1' Id='637856640181043726' IndexInText='242' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Literal>
						<OpeningRoundBrace Name='(' Id='637856640181043765' IndexInText='244' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<Literal Id='637856640181043857' IndexInText='245' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='x' Id='637856640181043852' IndexInText='245' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Literal>
						<Comma Name=',' Id='637856640181043889' IndexInText='246' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<Literal Id='637856640181043971' IndexInText='248' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='y' Id='637856640181043967' IndexInText='248' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Literal>
						<ClosingRoundBrace Name=')' Id='637856640181043997' IndexInText='249' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</RegularItems>
					<Children>
						<Literal Id='637856640181043857' IndexInText='245' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
						<Literal Id='637856640181043971' IndexInText='248' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					</Children>
					<OtherProperties>
						<NamedExpressionItem Id='637856640181043731' IndexInText='242' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
						<OpeningBraceInfo Name='(' Id='637856640181043765' IndexInText='244' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<ClosingBraceInfo Name=')' Id='637856640181043997' IndexInText='249' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<Parameters ObjectId='115' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
							<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640181043857' IndexInText='245' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640181043971' IndexInText='248' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
						</Parameters>
					</OtherProperties>
				</Operand1.Braces>
				<OperatorInfo OperatorType='BinaryOperator' Name='=>' Priority='1000' Id='637856640181044083' IndexInText='251' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
					<OperatorNameParts>
						<Name Name='=>' Id='637856640181044073' IndexInText='251' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</OperatorNameParts>
				</OperatorInfo>
				<Operand2.BinaryOperator Name='+' Priority='30' Id='637856640181044455' IndexInText='254' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
					<Operand1.Literal Id='637856640181044213' IndexInText='254' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='x' Id='637856640181044209' IndexInText='254' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Operand1.Literal>
					<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637856640181044278' IndexInText='255' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
						<OperatorNameParts>
							<Name Name='+' Id='637856640181044273' IndexInText='255' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OperatorNameParts>
					</OperatorInfo>
					<Operand2.Literal Id='637856640181044409' IndexInText='256' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='y' Id='637856640181044404' IndexInText='256' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Operand2.Literal>
				</Operand2.BinaryOperator>
			</BinaryOperator>
			<ExpressionSeparator Name=';' Id='637856640181044474' IndexInText='257' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<Braces Id='637856640181044586' IndexInText='262' ItemLength='137' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
				<RegularItems>
					<Literal Id='637856640181044559' IndexInText='262' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='f2' Id='637856640181044554' IndexInText='262' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<OpeningSquareBrace Name='[' Id='637856640181044589' IndexInText='264' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Literal Id='637856640181044672' IndexInText='265' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='x' Id='637856640181044668' IndexInText='265' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<Comma Name=',' Id='637856640181044701' IndexInText='266' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Literal Id='637856640181044781' IndexInText='268' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='y' Id='637856640181044777' IndexInText='268' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<ClosingSquareBrace Name=']' Id='637856640181044807' IndexInText='269' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Postfixes>
					<CodeBlock Id='637856640181044841' IndexInText='273' ItemLength='126' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
						<RegularItems>
							<CodeBlockStartMarker Name='{' Id='637856640181044837' IndexInText='273' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637856640181044904' IndexInText='398' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<OtherProperties>
							<CodeBlockStartMarker Name='{' Id='637856640181044837' IndexInText='273' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637856640181044904' IndexInText='398' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OtherProperties>
					</CodeBlock>
				</Postfixes>
				<Children>
					<Literal Id='637856640181044672' IndexInText='265' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					<Literal Id='637856640181044781' IndexInText='268' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
				</Children>
				<OtherProperties>
					<NamedExpressionItem Id='637856640181044559' IndexInText='262' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					<OpeningBraceInfo Name='[' Id='637856640181044589' IndexInText='264' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ClosingBraceInfo Name=']' Id='637856640181044807' IndexInText='269' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Parameters ObjectId='139' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640181044672' IndexInText='265' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640181044781' IndexInText='268' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					</Parameters>
				</OtherProperties>
			</Braces>
		</RegularItems>
		<Children>
			<BinaryOperator Name='=' Priority='2000' Id='637856640181043536' IndexInText='150' ItemLength='87' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
			<BinaryOperator Name='=>' Priority='1000' Id='637856640181044445' IndexInText='242' ItemLength='15' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
			<Braces Id='637856640181044586' IndexInText='262' ItemLength='137' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
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
	<MainExpressionItem Id='637856640180681372' IndexInText='150' ItemLength='249' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'/>
</UniversalExpressionParser.ExpressionItems.IRootExpressionItemSeries>
```
</details>

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

For example the expression "a * b + c * d", will be parsed to an expression logically similar to "*(+(a, b), +(x,d))". 

This is true since the binary operator "+" has lower priority (the value of **IOperatorInfo.Priority** is larger), than the binary operator "*". 

In other words this expression will be parsed to a binary operator expression item for "+" (i.e., an instance of **IOperatorExpressionItem**) with Operand1 and Operand2 also being binary operator expression items of type **UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem** for expression items "a * b" and "c * d".

- Example of parsing operators:

```csharp
// The binary operator + has priority 30 and * has priority 20. Therefore, 
// in expression below,  * is applied first and + is applied next.
// The following expression is parsed to an expression equivalent to 
// "=(var y, +(x1, *(f1(x2, +(x3, 1)), x4)))"
var y = x1 + f1(x2,x3+1)*x4;
```

<details> <summary>Click to expand the parsed expression</summary>

```XML
<?xml version="1.0" encoding="utf-8"?>
<UniversalExpressionParser.ExpressionItems.IRootExpressionItemSeries ObjectId='0' PositionInTextOnCompletion='291' IndexInText='263' ItemLength='28'>
	<ExpressionItemSeries Id='637856640182136259' IndexInText='263' ItemLength='28' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'>
		<RegularItems>
			<BinaryOperator Name='=' Priority='2000' Id='637856640182223910' IndexInText='263' ItemLength='27' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
				<Operand1.Literal Id='637856640182136615' IndexInText='263' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
					<NameExpressionItem Name='y' Id='637856640182136611' IndexInText='267' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<AppliedKeywords>
						<Keyword Name='var' Id='637856640182136478' IndexInText='263' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
							<LanguageKeywordInfo ObjectId='6' Id='637781064051574641' Keyword='var' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
						</Keyword>
					</AppliedKeywords>
				</Operand1.Literal>
				<OperatorInfo OperatorType='BinaryOperator' Name='=' Priority='2000' Id='637856640182136720' IndexInText='269' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
					<OperatorNameParts>
						<Name Name='=' Id='637856640182136709' IndexInText='269' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</OperatorNameParts>
				</OperatorInfo>
				<Operand2.BinaryOperator Name='+' Priority='30' Id='637856640182223922' IndexInText='271' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
					<Operand1.Literal Id='637856640182136862' IndexInText='271' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='x1' Id='637856640182136857' IndexInText='271' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Operand1.Literal>
					<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637856640182136940' IndexInText='274' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
						<OperatorNameParts>
							<Name Name='+' Id='637856640182136934' IndexInText='274' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OperatorNameParts>
					</OperatorInfo>
					<Operand2.BinaryOperator Name='*' Priority='20' Id='637856640182223933' IndexInText='276' ItemLength='14' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.Braces Id='637856640182137112' IndexInText='276' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
							<RegularItems>
								<Literal Id='637856640182137070' IndexInText='276' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
									<NameExpressionItem Name='f1' Id='637856640182137066' IndexInText='276' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</Literal>
								<OpeningRoundBrace Name='(' Id='637856640182137116' IndexInText='278' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<Literal Id='637856640182137208' IndexInText='279' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
									<NameExpressionItem Name='x2' Id='637856640182137203' IndexInText='279' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</Literal>
								<Comma Name=',' Id='637856640182137248' IndexInText='281' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<BinaryOperator Name='+' Priority='30' Id='637856640182223459' IndexInText='282' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
									<Operand1.Literal Id='637856640182137328' IndexInText='282' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='x3' Id='637856640182137324' IndexInText='282' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Operand1.Literal>
									<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637856640182137384' IndexInText='284' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
										<OperatorNameParts>
											<Name Name='+' Id='637856640182137379' IndexInText='284' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</OperatorNameParts>
									</OperatorInfo>
									<Operand2.ConstantNumericValue Id='637856640182223262' IndexInText='285' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
										<NameExpressionItem Name='1' Id='637856640182223220' IndexInText='285' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										<OtherProperties>
											<SucceededNumericTypeDescriptor ObjectId='29' NumberTypeId='1581134183960' Interface='UniversalExpressionParser.NumericTypeDescriptor'>
												<RegularExpressions ObjectId='30' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[System.String]'>
													<System.String value='^\d+' />
												</RegularExpressions>
											</SucceededNumericTypeDescriptor>
										</OtherProperties>
									</Operand2.ConstantNumericValue>
								</BinaryOperator>
								<ClosingRoundBrace Name=')' Id='637856640182223539' IndexInText='286' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</RegularItems>
							<Children>
								<Literal Id='637856640182137208' IndexInText='279' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								<BinaryOperator Name='+' Priority='30' Id='637856640182223459' IndexInText='282' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
							</Children>
							<OtherProperties>
								<NamedExpressionItem Id='637856640182137070' IndexInText='276' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								<OpeningBraceInfo Name='(' Id='637856640182137116' IndexInText='278' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<ClosingBraceInfo Name=')' Id='637856640182223539' IndexInText='286' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<Parameters ObjectId='32' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
									<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640182137208' IndexInText='279' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name='+' Priority='30' Id='637856640182223459' IndexInText='282' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
								</Parameters>
							</OtherProperties>
						</Operand1.Braces>
						<OperatorInfo OperatorType='BinaryOperator' Name='*' Priority='20' Id='637856640182223700' IndexInText='287' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<Name Name='*' Id='637856640182223689' IndexInText='287' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OperatorNameParts>
						</OperatorInfo>
						<Operand2.Literal Id='637856640182223866' IndexInText='288' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='x4' Id='637856640182223861' IndexInText='288' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand2.Literal>
					</Operand2.BinaryOperator>
				</Operand2.BinaryOperator>
			</BinaryOperator>
			<ExpressionSeparator Name=';' Id='637856640182223977' IndexInText='290' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
		</RegularItems>
		<Children>
			<BinaryOperator Name='=' Priority='2000' Id='637856640182223910' IndexInText='263' ItemLength='27' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
		</Children>
	</ExpressionItemSeries>
	<ParseErrorData ObjectId='38' HasCriticalErrors='False' Interface='UniversalExpressionParser.IParseErrorData'>
		<AllCodeItemErrors ObjectId='39' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICodeItemParseErrorData]'/>
	</ParseErrorData>
	<SortedCommentedOutCodeInfos ObjectId='40' Count='4' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICommentedOutCodeInfo]'>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='41' IsLineComment='True' IndexInText='0' ItemLength='75'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='42' IsLineComment='True' IndexInText='77' ItemLength='66'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='43' IsLineComment='True' IndexInText='145' ItemLength='69'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='44' IsLineComment='True' IndexInText='216' ItemLength='45'/>
	</SortedCommentedOutCodeInfos>
	<MainExpressionItem Id='637856640182136259' IndexInText='263' ItemLength='28' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'/>
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
	<ExpressionItemSeries Id='637856640182078828' IndexInText='83' ItemLength='25' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'>
		<RegularItems>
			<BinaryOperator Name='=' Priority='2000' Id='637856640182080403' IndexInText='83' ItemLength='24' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
				<Operand1.Literal Id='637856640182079189' IndexInText='83' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
					<NameExpressionItem Name='y1' Id='637856640182079184' IndexInText='87' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<AppliedKeywords>
						<Keyword Name='var' Id='637856640182079046' IndexInText='83' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
							<LanguageKeywordInfo ObjectId='6' Id='637781064051574641' Keyword='var' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
						</Keyword>
					</AppliedKeywords>
				</Operand1.Literal>
				<OperatorInfo OperatorType='BinaryOperator' Name='=' Priority='2000' Id='637856640182079284' IndexInText='90' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
					<OperatorNameParts>
						<Name Name='=' Id='637856640182079275' IndexInText='90' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</OperatorNameParts>
				</OperatorInfo>
				<Operand2.BinaryOperator Name='*' Priority='20' Id='637856640182080411' IndexInText='92' ItemLength='15' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
					<Operand1.Braces Id='637856640182079421' IndexInText='92' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<OpeningSquareBrace Name='[' Id='637856640182079425' IndexInText='92' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<BinaryOperator Name='+' Priority='30' Id='637856640182079768' IndexInText='93' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
								<Operand1.Literal Id='637856640182079533' IndexInText='93' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
									<NameExpressionItem Name='x1' Id='637856640182079528' IndexInText='93' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</Operand1.Literal>
								<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637856640182079592' IndexInText='95' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
									<OperatorNameParts>
										<Name Name='+' Id='637856640182079586' IndexInText='95' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</OperatorNameParts>
								</OperatorInfo>
								<Operand2.Literal Id='637856640182079722' IndexInText='96' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
									<NameExpressionItem Name='x2' Id='637856640182079718' IndexInText='96' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</Operand2.Literal>
							</BinaryOperator>
							<ClosingSquareBrace Name=']' Id='637856640182079822' IndexInText='98' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<BinaryOperator Name='+' Priority='30' Id='637856640182079768' IndexInText='93' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
						</Children>
						<OtherProperties>
							<OpeningBraceInfo Name='[' Id='637856640182079425' IndexInText='92' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=']' Id='637856640182079822' IndexInText='98' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
							<Parameters ObjectId='20' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name='+' Priority='30' Id='637856640182079768' IndexInText='93' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Operand1.Braces>
					<OperatorInfo OperatorType='BinaryOperator' Name='*' Priority='20' Id='637856640182079887' IndexInText='99' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
						<OperatorNameParts>
							<Name Name='*' Id='637856640182079882' IndexInText='99' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OperatorNameParts>
					</OperatorInfo>
					<Operand2.Braces Id='637856640182080020' IndexInText='100' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<OpeningRoundBrace Name='(' Id='637856640182080025' IndexInText='100' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<BinaryOperator Name='-' Priority='30' Id='637856640182080349' IndexInText='101' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
								<Operand1.Literal Id='637856640182080112' IndexInText='101' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
									<NameExpressionItem Name='x3' Id='637856640182080108' IndexInText='101' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</Operand1.Literal>
								<OperatorInfo OperatorType='BinaryOperator' Name='-' Priority='30' Id='637856640182080185' IndexInText='103' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
									<OperatorNameParts>
										<Name Name='-' Id='637856640182080180' IndexInText='103' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</OperatorNameParts>
								</OperatorInfo>
								<Operand2.Literal Id='637856640182080313' IndexInText='104' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
									<NameExpressionItem Name='x4' Id='637856640182080308' IndexInText='104' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</Operand2.Literal>
							</BinaryOperator>
							<ClosingRoundBrace Name=')' Id='637856640182080370' IndexInText='106' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<BinaryOperator Name='-' Priority='30' Id='637856640182080349' IndexInText='101' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
						</Children>
						<OtherProperties>
							<OpeningBraceInfo Name='(' Id='637856640182080025' IndexInText='100' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=')' Id='637856640182080370' IndexInText='106' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
							<Parameters ObjectId='33' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name='-' Priority='30' Id='637856640182080349' IndexInText='101' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Operand2.Braces>
				</Operand2.BinaryOperator>
			</BinaryOperator>
			<ExpressionSeparator Name=';' Id='637856640182080445' IndexInText='107' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
		</RegularItems>
		<Children>
			<BinaryOperator Name='=' Priority='2000' Id='637856640182080403' IndexInText='83' ItemLength='24' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
		</Children>
	</ExpressionItemSeries>
	<ParseErrorData ObjectId='35' HasCriticalErrors='False' Interface='UniversalExpressionParser.IParseErrorData'>
		<AllCodeItemErrors ObjectId='36' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICodeItemParseErrorData]'/>
	</ParseErrorData>
	<SortedCommentedOutCodeInfos ObjectId='37' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICommentedOutCodeInfo]'>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='38' IsLineComment='True' IndexInText='0' ItemLength='81'/>
	</SortedCommentedOutCodeInfos>
	<MainExpressionItem Id='637856640182078828' IndexInText='83' ItemLength='25' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'/>
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
	<ExpressionItemSeries Id='637856640182099741' IndexInText='84' ItemLength='36' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'>
		<RegularItems>
			<BinaryOperator Name='=' Priority='2000' Id='637856640182102413' IndexInText='84' ItemLength='35' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
				<Operand1.Literal Id='637856640182100422' IndexInText='84' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
					<NameExpressionItem Name='z' Id='637856640182100415' IndexInText='84' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</Operand1.Literal>
				<OperatorInfo OperatorType='BinaryOperator' Name='=' Priority='2000' Id='637856640182100522' IndexInText='86' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
					<OperatorNameParts>
						<Name Name='=' Id='637856640182100508' IndexInText='86' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</OperatorNameParts>
				</OperatorInfo>
				<Operand2.PrefixUnaryOperator Name='!' Priority='0' Id='637856640182102424' IndexInText='88' ItemLength='31' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
					<OperatorInfo OperatorType='PrefixUnaryOperator' Name='!' Priority='0' Id='637856640182100595' IndexInText='88' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
						<OperatorNameParts>
							<Name Name='!' Id='637856640182100589' IndexInText='88' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OperatorNameParts>
					</OperatorInfo>
					<Operand1.Braces Id='637856640182100719' IndexInText='89' ItemLength='30' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<OpeningRoundBrace Name='(' Id='637856640182100723' IndexInText='89' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<BinaryOperator Name='&&' Priority='80' Id='637856640182102132' IndexInText='90' ItemLength='28' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
								<Operand1.PostfixUnaryOperator Name='IS NOT NULL' Priority='1' Id='637856640182102110' IndexInText='90' ItemLength='14' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
									<Operand1.Literal Id='637856640182100837' IndexInText='90' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='x1' Id='637856640182100832' IndexInText='90' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Operand1.Literal>
									<OperatorInfo OperatorType='PostfixUnaryOperator' Name='IS NOT NULL' Priority='1' Id='637856640182100898' IndexInText='93' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
										<OperatorNameParts>
											<Name Name='IS' Id='637856640182100874' IndexInText='93' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<Name Name='NOT' Id='637856640182100886' IndexInText='96' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<Name Name='NULL' Id='637856640182100893' IndexInText='100' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</OperatorNameParts>
									</OperatorInfo>
								</Operand1.PostfixUnaryOperator>
								<OperatorInfo OperatorType='BinaryOperator' Name='&&' Priority='80' Id='637856640182101628' IndexInText='105' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
									<OperatorNameParts>
										<Name Name='&&' Id='637856640182101623' IndexInText='105' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</OperatorNameParts>
								</OperatorInfo>
								<Operand2.PostfixUnaryOperator Name='IS NULL' Priority='1' Id='637856640182102140' IndexInText='108' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
									<Operand1.Literal Id='637856640182101894' IndexInText='108' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='x2' Id='637856640182101883' IndexInText='108' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Operand1.Literal>
									<OperatorInfo OperatorType='PostfixUnaryOperator' Name='IS NULL' Priority='1' Id='637856640182101988' IndexInText='111' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
										<OperatorNameParts>
											<Name Name='IS' Id='637856640182101976' IndexInText='111' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<Name Name='NULL' Id='637856640182101983' IndexInText='114' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</OperatorNameParts>
									</OperatorInfo>
								</Operand2.PostfixUnaryOperator>
							</BinaryOperator>
							<ClosingRoundBrace Name=')' Id='637856640182102209' IndexInText='118' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<BinaryOperator Name='&&' Priority='80' Id='637856640182102132' IndexInText='90' ItemLength='28' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
						</Children>
						<OtherProperties>
							<OpeningBraceInfo Name='(' Id='637856640182100723' IndexInText='89' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=')' Id='637856640182102209' IndexInText='118' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
							<Parameters ObjectId='29' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name='&&' Priority='80' Id='637856640182102132' IndexInText='90' ItemLength='28' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Operand1.Braces>
				</Operand2.PrefixUnaryOperator>
			</BinaryOperator>
			<ExpressionSeparator Name=';' Id='637856640182102448' IndexInText='119' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
		</RegularItems>
		<Children>
			<BinaryOperator Name='=' Priority='2000' Id='637856640182102413' IndexInText='84' ItemLength='35' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
		</Children>
	</ExpressionItemSeries>
	<ParseErrorData ObjectId='31' HasCriticalErrors='False' Interface='UniversalExpressionParser.IParseErrorData'>
		<AllCodeItemErrors ObjectId='32' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICodeItemParseErrorData]'/>
	</ParseErrorData>
	<SortedCommentedOutCodeInfos ObjectId='33' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICommentedOutCodeInfo]'>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='34' IsLineComment='True' IndexInText='0' ItemLength='38'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='35' IsLineComment='True' IndexInText='40' ItemLength='42'/>
	</SortedCommentedOutCodeInfos>
	<MainExpressionItem Id='637856640182099741' IndexInText='84' ItemLength='36' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'/>
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
	<ExpressionItemSeries Id='637856640182120357' IndexInText='288' ItemLength='18' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'>
		<RegularItems>
			<Braces Id='637856640182120964' IndexInText='288' ItemLength='18' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
				<RegularItems>
					<Literal Id='637856640182120910' IndexInText='288' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='println' Id='637856640182120903' IndexInText='288' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<OpeningRoundBrace Name='(' Id='637856640182120967' IndexInText='295' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<BinaryOperator Name='+' Priority='30' Id='637856640182121947' IndexInText='296' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.PostfixUnaryOperator Name='++' Priority='1' Id='637856640182121923' IndexInText='296' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
							<Operand1.PostfixUnaryOperator Name='++' Priority='1' Id='637856640182121909' IndexInText='296' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
								<Operand1.Literal Id='637856640182121078' IndexInText='296' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
									<NameExpressionItem Name='x1' Id='637856640182121073' IndexInText='296' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</Operand1.Literal>
								<OperatorInfo OperatorType='PostfixUnaryOperator' Name='++' Priority='1' Id='637856640182121158' IndexInText='298' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
									<OperatorNameParts>
										<Name Name='++' Id='637856640182121153' IndexInText='298' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</OperatorNameParts>
								</OperatorInfo>
							</Operand1.PostfixUnaryOperator>
							<OperatorInfo OperatorType='PostfixUnaryOperator' Name='++' Priority='1' Id='637856640182121429' IndexInText='300' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
								<OperatorNameParts>
									<Name Name='++' Id='637856640182121424' IndexInText='300' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</OperatorNameParts>
							</OperatorInfo>
						</Operand1.PostfixUnaryOperator>
						<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637856640182121701' IndexInText='302' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<Name Name='+' Id='637856640182121508' IndexInText='302' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OperatorNameParts>
						</OperatorInfo>
						<Operand2.Literal Id='637856640182121834' IndexInText='303' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='x2' Id='637856640182121830' IndexInText='303' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand2.Literal>
					</BinaryOperator>
					<ClosingRoundBrace Name=')' Id='637856640182122232' IndexInText='305' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Children>
					<BinaryOperator Name='+' Priority='30' Id='637856640182121947' IndexInText='296' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
				</Children>
				<OtherProperties>
					<NamedExpressionItem Id='637856640182120910' IndexInText='288' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					<OpeningBraceInfo Name='(' Id='637856640182120967' IndexInText='295' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ClosingBraceInfo Name=')' Id='637856640182122232' IndexInText='305' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Parameters ObjectId='20' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name='+' Priority='30' Id='637856640182121947' IndexInText='296' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
					</Parameters>
				</OtherProperties>
			</Braces>
		</RegularItems>
		<Children>
			<Braces Id='637856640182120964' IndexInText='288' ItemLength='18' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
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
	<MainExpressionItem Id='637856640182120357' IndexInText='288' ItemLength='18' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'/>
</UniversalExpressionParser.ExpressionItems.IRootExpressionItemSeries>
```
</details>

- Example of unary prefix operator used to implement "return" statement:

```csharp
// return has priority int.MaxValue which is greater then any other operator priority, therefore
// the expression below is equivalent to "return (x+(2.5*x))";
return x+2.5*y;

// another example within function body
f1(x:int, y:int) : bool
{
	// return has priority int.MaxValue which is greater then any other operator priority, therefore
	// the expression below is equivalent to "return (x+(2.5*x))";
	return f(x)+y > 10;
}
```

<details> <summary>Click to expand the parsed expression</summary>

```XML
<?xml version="1.0" encoding="utf-8"?>
<UniversalExpressionParser.ExpressionItems.IRootExpressionItemSeries ObjectId='0' PositionInTextOnCompletion='437' IndexInText='162' ItemLength='250'>
	<ExpressionItemSeries Id='637856640182243344' IndexInText='162' ItemLength='275' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'>
		<RegularItems>
			<PrefixUnaryOperator Name='return' Priority='2147483647' Id='637856640182333717' IndexInText='162' ItemLength='14' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
				<OperatorInfo OperatorType='PrefixUnaryOperator' Name='return' Priority='2147483647' Id='637856640182243708' IndexInText='162' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
					<OperatorNameParts>
						<Name Name='return' Id='637856640182243686' IndexInText='162' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</OperatorNameParts>
				</OperatorInfo>
				<Operand1.BinaryOperator Name='+' Priority='30' Id='637856640182333753' IndexInText='169' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
					<Operand1.Literal Id='637856640182243850' IndexInText='169' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='x' Id='637856640182243844' IndexInText='169' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Operand1.Literal>
					<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637856640182243936' IndexInText='170' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
						<OperatorNameParts>
							<Name Name='+' Id='637856640182243930' IndexInText='170' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OperatorNameParts>
					</OperatorInfo>
					<Operand2.BinaryOperator Name='*' Priority='20' Id='637856640182333762' IndexInText='171' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.ConstantNumericValue Id='637856640182333102' IndexInText='171' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
							<NameExpressionItem Name='2.5' Id='637856640182333001' IndexInText='171' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<OtherProperties>
								<SucceededNumericTypeDescriptor ObjectId='13' NumberTypeId='1581134136626' Interface='UniversalExpressionParser.NumericTypeDescriptor'>
									<RegularExpressions ObjectId='14' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[System.String]'>
										<System.String value='^(\d+\.\d+|\d+\.|\.\d+)' />
									</RegularExpressions>
								</SucceededNumericTypeDescriptor>
							</OtherProperties>
						</Operand1.ConstantNumericValue>
						<OperatorInfo OperatorType='BinaryOperator' Name='*' Priority='20' Id='637856640182333438' IndexInText='174' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<Name Name='*' Id='637856640182333418' IndexInText='174' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OperatorNameParts>
						</OperatorInfo>
						<Operand2.Literal Id='637856640182333651' IndexInText='175' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='y' Id='637856640182333645' IndexInText='175' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand2.Literal>
					</Operand2.BinaryOperator>
				</Operand1.BinaryOperator>
			</PrefixUnaryOperator>
			<ExpressionSeparator Name=';' Id='637856640182333843' IndexInText='176' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<BinaryOperator Name=':' Priority='0' Id='637856640182438153' IndexInText='222' ItemLength='215' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
				<Operand1.Braces Id='637856640182334001' IndexInText='222' ItemLength='16' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
					<RegularItems>
						<Literal Id='637856640182333962' IndexInText='222' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='f1' Id='637856640182333957' IndexInText='222' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Literal>
						<OpeningRoundBrace Name='(' Id='637856640182334004' IndexInText='224' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<BinaryOperator Name=':' Priority='0' Id='637856640182334349' IndexInText='225' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
							<Operand1.Literal Id='637856640182334107' IndexInText='225' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='x' Id='637856640182334102' IndexInText='225' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Operand1.Literal>
							<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637856640182334166' IndexInText='226' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
								<OperatorNameParts>
									<Name Name=':' Id='637856640182334158' IndexInText='226' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</OperatorNameParts>
							</OperatorInfo>
							<Operand2.Literal Id='637856640182334313' IndexInText='227' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='int' Id='637856640182334307' IndexInText='227' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Operand2.Literal>
						</BinaryOperator>
						<Comma Name=',' Id='637856640182334376' IndexInText='230' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<BinaryOperator Name=':' Priority='0' Id='637856640182334705' IndexInText='232' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
							<Operand1.Literal Id='637856640182334468' IndexInText='232' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='y' Id='637856640182334463' IndexInText='232' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Operand1.Literal>
							<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637856640182334523' IndexInText='233' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
								<OperatorNameParts>
									<Name Name=':' Id='637856640182334512' IndexInText='233' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</OperatorNameParts>
							</OperatorInfo>
							<Operand2.Literal Id='637856640182334670' IndexInText='234' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='int' Id='637856640182334664' IndexInText='234' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Operand2.Literal>
						</BinaryOperator>
						<ClosingRoundBrace Name=')' Id='637856640182334724' IndexInText='237' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</RegularItems>
					<Children>
						<BinaryOperator Name=':' Priority='0' Id='637856640182334349' IndexInText='225' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
						<BinaryOperator Name=':' Priority='0' Id='637856640182334705' IndexInText='232' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
					</Children>
					<OtherProperties>
						<NamedExpressionItem Id='637856640182333962' IndexInText='222' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
						<OpeningBraceInfo Name='(' Id='637856640182334004' IndexInText='224' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<ClosingBraceInfo Name=')' Id='637856640182334724' IndexInText='237' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<Parameters ObjectId='41' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
							<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name=':' Priority='0' Id='637856640182334349' IndexInText='225' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
							<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name=':' Priority='0' Id='637856640182334705' IndexInText='232' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
						</Parameters>
					</OtherProperties>
				</Operand1.Braces>
				<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637856640182334798' IndexInText='239' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
					<OperatorNameParts>
						<Name Name=':' Id='637856640182334786' IndexInText='239' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</OperatorNameParts>
				</OperatorInfo>
				<Operand2.Literal Id='637856640182334960' IndexInText='241' ItemLength='196' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
					<NameExpressionItem Name='bool' Id='637856640182334953' IndexInText='241' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Postfixes>
						<CodeBlock Id='637856640182335001' IndexInText='247' ItemLength='190' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
							<RegularItems>
								<CodeBlockStartMarker Name='{' Id='637856640182334996' IndexInText='247' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<PrefixUnaryOperator Name='return' Priority='2147483647' Id='637856640182437774' IndexInText='415' ItemLength='18' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
									<OperatorInfo OperatorType='PrefixUnaryOperator' Name='return' Priority='2147483647' Id='637856640182335080' IndexInText='415' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
										<OperatorNameParts>
											<Name Name='return' Id='637856640182335074' IndexInText='415' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</OperatorNameParts>
									</OperatorInfo>
									<Operand1.BinaryOperator Name='>' Priority='50' Id='637856640182438004' IndexInText='422' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
										<Operand1.BinaryOperator Name='+' Priority='30' Id='637856640182437994' IndexInText='422' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
											<Operand1.Braces Id='637856640182335259' IndexInText='422' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
												<RegularItems>
													<Literal Id='637856640182335198' IndexInText='422' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
														<NameExpressionItem Name='f' Id='637856640182335192' IndexInText='422' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
													</Literal>
													<OpeningRoundBrace Name='(' Id='637856640182335263' IndexInText='423' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
													<Literal Id='637856640182335351' IndexInText='424' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
														<NameExpressionItem Name='x' Id='637856640182335344' IndexInText='424' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
													</Literal>
													<ClosingRoundBrace Name=')' Id='637856640182335386' IndexInText='425' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												</RegularItems>
												<Children>
													<Literal Id='637856640182335351' IndexInText='424' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
												</Children>
												<OtherProperties>
													<NamedExpressionItem Id='637856640182335198' IndexInText='422' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
													<OpeningBraceInfo Name='(' Id='637856640182335263' IndexInText='423' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
													<ClosingBraceInfo Name=')' Id='637856640182335386' IndexInText='425' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
													<Parameters ObjectId='60' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
														<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640182335351' IndexInText='424' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
													</Parameters>
												</OtherProperties>
											</Operand1.Braces>
											<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637856640182335445' IndexInText='426' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
												<OperatorNameParts>
													<Name Name='+' Id='637856640182335439' IndexInText='426' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												</OperatorNameParts>
											</OperatorInfo>
											<Operand2.Literal Id='637856640182335580' IndexInText='427' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
												<NameExpressionItem Name='y' Id='637856640182335576' IndexInText='427' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											</Operand2.Literal>
										</Operand1.BinaryOperator>
										<OperatorInfo OperatorType='BinaryOperator' Name='>' Priority='50' Id='637856640182335651' IndexInText='429' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
											<OperatorNameParts>
												<Name Name='>' Id='637856640182335645' IndexInText='429' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											</OperatorNameParts>
										</OperatorInfo>
										<Operand2.ConstantNumericValue Id='637856640182437561' IndexInText='431' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
											<NameExpressionItem Name='10' Id='637856640182437506' IndexInText='431' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
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
								<ExpressionSeparator Name=';' Id='637856640182438083' IndexInText='433' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<CodeBlockEndMarker Name='}' Id='637856640182438129' IndexInText='436' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</RegularItems>
							<Children>
								<PrefixUnaryOperator Name='return' Priority='2147483647' Id='637856640182437774' IndexInText='415' ItemLength='18' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
							</Children>
							<OtherProperties>
								<CodeBlockStartMarker Name='{' Id='637856640182334996' IndexInText='247' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<CodeBlockEndMarker Name='}' Id='637856640182438129' IndexInText='436' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OtherProperties>
						</CodeBlock>
					</Postfixes>
				</Operand2.Literal>
			</BinaryOperator>
		</RegularItems>
		<Children>
			<PrefixUnaryOperator Name='return' Priority='2147483647' Id='637856640182333717' IndexInText='162' ItemLength='14' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
			<BinaryOperator Name=':' Priority='0' Id='637856640182438153' IndexInText='222' ItemLength='215' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
		</Children>
	</ExpressionItemSeries>
	<ParseErrorData ObjectId='73' HasCriticalErrors='False' Interface='UniversalExpressionParser.IParseErrorData'>
		<AllCodeItemErrors ObjectId='74' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICodeItemParseErrorData]'/>
	</ParseErrorData>
	<SortedCommentedOutCodeInfos ObjectId='75' Count='5' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICommentedOutCodeInfo]'>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='76' IsLineComment='True' IndexInText='0' ItemLength='96'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='77' IsLineComment='True' IndexInText='98' ItemLength='62'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='78' IsLineComment='True' IndexInText='181' ItemLength='39'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='79' IsLineComment='True' IndexInText='251' ItemLength='96'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='80' IsLineComment='True' IndexInText='350' ItemLength='62'/>
	</SortedCommentedOutCodeInfos>
	<MainExpressionItem Id='637856640182243344' IndexInText='162' ItemLength='275' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'/>
</UniversalExpressionParser.ExpressionItems.IRootExpressionItemSeries>
```
</details>

# Numeric Values

- **Universal Expression Parser** parses expression items that have specific format to expression items of type **UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem**. The format of expression items that will be parsed to **UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem** is determined by list property **IReadOnlyList<NumericTypeDescriptor> NumericTypeDescriptors { get; }** in interface **UniversalExpressionParser.IExpressionLanguageProvider**, an instance of which is passed to the parser.
- Below is the definition of **UniversalExpressionParser.NumericTypeDescriptor**. 

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

- The parser scans the regular expressions in list in property **IReadOnlyList<string> RegularExpressions { get; }** in type **NumericTypeDescriptor** for each provided instance of **UniversalExpressionParser.NumericTypeDescriptor** to try to parse the expression to numeric expression item of type **UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem**. 
   
- The abstract class **UniversalExpressionParser.ExpressionLanguageProviderBase** that can be used as a base class for implementations of **UniversalExpressionParser.IExpressionLanguageProvider** in most cases, implements the property **NumericTypeDescriptors** as a virtual property. The implementation of property **NumericTypeDescriptors** in **UniversalExpressionParser.ExpressionLanguageProviderBase** is demonstrated below, and it can be overridden to provide different format for numeric values:
 
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

- The first regular expression that matches the expression, is stored in properties **SucceededNumericTypeDescriptor** of type **UniversalExpressionParser.NumericTypeDescriptor** and **IndexOfSucceededRegularExpression** in parsed expression item of type **UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem**.
- The numeric value is stored as text in property **INumericValueExpressionItem.NameExpressionItem.Name** of text type. Therefore, there is no limit on numeric value digits.
- The expression evaluator that uses the **Universal Expression Parser** can use the data in these properties to convert the text in property **INumericValueExpressionItem.NameExpressionItem** to a value of numeric type (int, long, double, etc.).
- Examples of numeric value expression items are demonstrated below:

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
	<ExpressionItemSeries Id='637856640181700799' IndexInText='46' ItemLength='414' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'>
		<RegularItems>
			<Braces Id='637856640181701218' IndexInText='46' ItemLength='41' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
				<RegularItems>
					<Literal Id='637856640181701169' IndexInText='46' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='println' Id='637856640181701159' IndexInText='46' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<OpeningRoundBrace Name='(' Id='637856640181701222' IndexInText='53' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<BinaryOperator Name='+' Priority='30' Id='637856640181880189' IndexInText='54' ItemLength='32' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.BinaryOperator Name='+' Priority='30' Id='637856640181880180' IndexInText='54' ItemLength='23' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
							<Operand1.BinaryOperator Name='+' Priority='30' Id='637856640181880169' IndexInText='54' ItemLength='16' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
								<Operand1.PrefixUnaryOperator Name='-' Priority='0' Id='637856640181880135' IndexInText='54' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
									<OperatorInfo OperatorType='PrefixUnaryOperator' Name='-' Priority='0' Id='637856640181701298' IndexInText='54' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
										<OperatorNameParts>
											<Name Name='-' Id='637856640181701285' IndexInText='54' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</OperatorNameParts>
									</OperatorInfo>
									<Operand1.ConstantNumericValue Id='637856640181761161' IndexInText='55' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
										<NameExpressionItem Name='0.5e-3' Id='637856640181761097' IndexInText='55' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										<OtherProperties>
											<SucceededNumericTypeDescriptor ObjectId='14' NumberTypeId='1581134225786' Interface='UniversalExpressionParser.NumericTypeDescriptor'>
												<RegularExpressions ObjectId='15' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[System.String]'>
													<System.String value='^(\d+\.\d+|\d+\.|\.\d+|\d+)(EXP|exp|E|e)[+|-]?(\d+\.\d+|\d+\.|\.\d+|\d+)' />
												</RegularExpressions>
											</SucceededNumericTypeDescriptor>
										</OtherProperties>
									</Operand1.ConstantNumericValue>
								</Operand1.PrefixUnaryOperator>
								<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637856640181761464' IndexInText='61' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
									<OperatorNameParts>
										<Name Name='+' Id='637856640181761450' IndexInText='61' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</OperatorNameParts>
								</OperatorInfo>
								<Operand2.Literal Id='637856640181761654' IndexInText='62' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
									<NameExpressionItem Name='.2exp3.4' Id='637856640181761649' IndexInText='62' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</Operand2.Literal>
							</Operand1.BinaryOperator>
							<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637856640181761852' IndexInText='70' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
								<OperatorNameParts>
									<Name Name='+' Id='637856640181761845' IndexInText='70' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</OperatorNameParts>
							</OperatorInfo>
							<Operand2.ConstantNumericValue Id='637856640181822258' IndexInText='71' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
								<NameExpressionItem Name='3.E2.7' Id='637856640181822186' IndexInText='71' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<OtherProperties>
									<SucceededNumericTypeDescriptor ObjectId='14' NumberTypeId='1581134225786' Interface='UniversalExpressionParser.NumericTypeDescriptor'/>
								</OtherProperties>
							</Operand2.ConstantNumericValue>
						</Operand1.BinaryOperator>
						<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637856640181822535' IndexInText='77' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<Name Name='+' Id='637856640181822519' IndexInText='77' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OperatorNameParts>
						</OperatorInfo>
						<Operand2.ConstantNumericValue Id='637856640181879908' IndexInText='78' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
							<NameExpressionItem Name='2.1EXP.3' Id='637856640181879854' IndexInText='78' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<OtherProperties>
								<SucceededNumericTypeDescriptor ObjectId='14' NumberTypeId='1581134225786' Interface='UniversalExpressionParser.NumericTypeDescriptor'/>
							</OtherProperties>
						</Operand2.ConstantNumericValue>
					</BinaryOperator>
					<ClosingRoundBrace Name=')' Id='637856640181880270' IndexInText='86' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Children>
					<BinaryOperator Name='+' Priority='30' Id='637856640181880189' IndexInText='54' ItemLength='32' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
				</Children>
				<OtherProperties>
					<NamedExpressionItem Id='637856640181701169' IndexInText='46' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					<OpeningBraceInfo Name='(' Id='637856640181701222' IndexInText='53' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ClosingBraceInfo Name=')' Id='637856640181880270' IndexInText='86' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Parameters ObjectId='29' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name='+' Priority='30' Id='637856640181880189' IndexInText='54' ItemLength='32' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
					</Parameters>
				</OtherProperties>
			</Braces>
			<ExpressionSeparator Name=';' Id='637856640181880327' IndexInText='87' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<Braces Id='637856640181880497' IndexInText='90' ItemLength='16' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
				<RegularItems>
					<Literal Id='637856640181880465' IndexInText='90' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='println' Id='637856640181880461' IndexInText='90' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<OpeningRoundBrace Name='(' Id='637856640181880500' IndexInText='97' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<BinaryOperator Name='*' Priority='20' Id='637856640181880836' IndexInText='98' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.Literal Id='637856640181880594' IndexInText='98' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='.5e15' Id='637856640181880589' IndexInText='98' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand1.Literal>
						<OperatorInfo OperatorType='BinaryOperator' Name='*' Priority='20' Id='637856640181880655' IndexInText='103' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<Name Name='*' Id='637856640181880645' IndexInText='103' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OperatorNameParts>
						</OperatorInfo>
						<Operand2.Literal Id='637856640181880801' IndexInText='104' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='x' Id='637856640181880796' IndexInText='104' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand2.Literal>
					</BinaryOperator>
					<ClosingRoundBrace Name=')' Id='637856640181880857' IndexInText='105' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Children>
					<BinaryOperator Name='*' Priority='20' Id='637856640181880836' IndexInText='98' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
				</Children>
				<OtherProperties>
					<NamedExpressionItem Id='637856640181880465' IndexInText='90' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					<OpeningBraceInfo Name='(' Id='637856640181880500' IndexInText='97' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ClosingBraceInfo Name=')' Id='637856640181880857' IndexInText='105' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Parameters ObjectId='43' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name='*' Priority='20' Id='637856640181880836' IndexInText='98' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
					</Parameters>
				</OtherProperties>
			</Braces>
			<ExpressionSeparator Name=';' Id='637856640181880886' IndexInText='106' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<BinaryOperator Name='=' Priority='2000' Id='637856640182042998' IndexInText='409' ItemLength='50' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
				<Operand1.Literal Id='637856640181881034' IndexInText='409' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
					<NameExpressionItem Name='x' Id='637856640181881030' IndexInText='413' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<AppliedKeywords>
						<Keyword Name='var' Id='637856640181880944' IndexInText='409' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
							<LanguageKeywordInfo ObjectId='49' Id='637781064051574641' Keyword='var' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
						</Keyword>
					</AppliedKeywords>
				</Operand1.Literal>
				<OperatorInfo OperatorType='BinaryOperator' Name='=' Priority='2000' Id='637856640181881110' IndexInText='415' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
					<OperatorNameParts>
						<Name Name='=' Id='637856640181881105' IndexInText='415' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</OperatorNameParts>
				</OperatorInfo>
				<Operand2.BinaryOperator Name='+' Priority='30' Id='637856640182043034' IndexInText='417' ItemLength='42' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
					<Operand1.BinaryOperator Name='*' Priority='20' Id='637856640182043025' IndexInText='417' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.ConstantNumericValue Id='637856640181955094' IndexInText='417' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
							<NameExpressionItem Name='2.3' Id='637856640181955041' IndexInText='417' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<OtherProperties>
								<SucceededNumericTypeDescriptor ObjectId='56' NumberTypeId='1581134136626' Interface='UniversalExpressionParser.NumericTypeDescriptor'>
									<RegularExpressions ObjectId='57' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[System.String]'>
										<System.String value='^(\d+\.\d+|\d+\.|\.\d+)' />
									</RegularExpressions>
								</SucceededNumericTypeDescriptor>
							</OtherProperties>
						</Operand1.ConstantNumericValue>
						<OperatorInfo OperatorType='BinaryOperator' Name='*' Priority='20' Id='637856640181955385' IndexInText='420' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<Name Name='*' Id='637856640181955369' IndexInText='420' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OperatorNameParts>
						</OperatorInfo>
						<Operand2.Literal Id='637856640181955594' IndexInText='421' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='x' Id='637856640181955589' IndexInText='421' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand2.Literal>
					</Operand1.BinaryOperator>
					<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637856640181955677' IndexInText='422' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
						<OperatorNameParts>
							<Name Name='+' Id='637856640181955671' IndexInText='422' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OperatorNameParts>
					</OperatorInfo>
					<Operand2.ConstantNumericValue Id='637856640182042815' IndexInText='423' ItemLength='36' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
						<NameExpressionItem Name='123456789123456789123456789123456789' Id='637856640182042768' IndexInText='423' ItemLength='36' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
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
			<ExpressionSeparator Name=';' Id='637856640182043102' IndexInText='459' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
		</RegularItems>
		<Children>
			<Braces Id='637856640181701218' IndexInText='46' ItemLength='41' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
			<Braces Id='637856640181880497' IndexInText='90' ItemLength='16' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
			<BinaryOperator Name='=' Priority='2000' Id='637856640182042998' IndexInText='409' ItemLength='50' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
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
	<MainExpressionItem Id='637856640181700799' IndexInText='46' ItemLength='414' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'/>
</UniversalExpressionParser.ExpressionItems.IRootExpressionItemSeries>
```
</details>

# Texts

The interface **UniversalExpressionParser.IExpressionLanguageProvider** has a property **IReadOnlyList&lt;char&gt; ConstantTextStartEndMarkerCharacters { get; }** that are used by **Universal Expression Parser** to parse expression items that start or end with some character in **ConstantTextStartEndMarkerCharacters** to parse the expression item to  **INamedComplexExpressionItem** with tne value of property **ItemType** equal to **UniversalExpressionParser.ExpressionItems.ConstantText**.

- The characters in property **ConstantTextStartEndMarkerCharacters** can be used within the text itself, if the character is typed twice (see the examples below).

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
	<ExpressionItemSeries Id='637856640183925614' IndexInText='287' ItemLength='172' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'>
		<RegularItems>
			<BinaryOperator Name='=' Priority='2000' Id='637856640183926892' IndexInText='287' ItemLength='78' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
				<Operand1.Literal Id='637856640183926006' IndexInText='287' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
					<NameExpressionItem Name='x' Id='637856640183925998' IndexInText='287' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</Operand1.Literal>
				<OperatorInfo OperatorType='BinaryOperator' Name='=' Priority='2000' Id='637856640183926108' IndexInText='289' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
					<OperatorNameParts>
						<Name Name='=' Id='637856640183926098' IndexInText='289' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</OperatorNameParts>
				</OperatorInfo>
				<Operand2.BinaryOperator Name='+' Priority='30' Id='637856640183926924' IndexInText='291' ItemLength='74' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
					<Operand1.BinaryOperator Name='+' Priority='30' Id='637856640183926916' IndexInText='291' ItemLength='70' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.BinaryOperator Name='+' Priority='30' Id='637856640183926908' IndexInText='291' ItemLength='44' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
							<Operand1.ConstantText Id='637856640183926261' IndexInText='291' ItemLength='17' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='"Text 1 "" ''' ``"' Id='637856640183926255' IndexInText='291' ItemLength='17' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Operand1.ConstantText>
							<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637856640183926334' IndexInText='309' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
								<OperatorNameParts>
									<Name Name='+' Id='637856640183926328' IndexInText='309' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</OperatorNameParts>
							</OperatorInfo>
							<Operand2.ConstantText Id='637856640183926472' IndexInText='317' ItemLength='18' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name=''' Text2  "" ''' ``''' Id='637856640183926464' IndexInText='317' ItemLength='18' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Operand2.ConstantText>
						</Operand1.BinaryOperator>
						<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637856640183926532' IndexInText='336' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<Name Name='+' Id='637856640183926526' IndexInText='336' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OperatorNameParts>
						</OperatorInfo>
						<Operand2.ConstantText Id='637856640183926665' IndexInText='343' ItemLength='18' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='` Text3  "" ''' ```' Id='637856640183926660' IndexInText='343' ItemLength='18' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand2.ConstantText>
					</Operand1.BinaryOperator>
					<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637856640183926724' IndexInText='362' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
						<OperatorNameParts>
							<Name Name='+' Id='637856640183926719' IndexInText='362' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OperatorNameParts>
					</OperatorInfo>
					<Operand2.Literal Id='637856640183926850' IndexInText='364' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='x' Id='637856640183926845' IndexInText='364' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Operand2.Literal>
				</Operand2.BinaryOperator>
			</BinaryOperator>
			<ExpressionSeparator Name=';' Id='637856640183926982' IndexInText='365' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<Braces Id='637856640183927096' IndexInText='370' ItemLength='88' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
				<RegularItems>
					<Literal Id='637856640183927065' IndexInText='370' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='println' Id='637856640183927061' IndexInText='370' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<OpeningRoundBrace Name='(' Id='637856640183927105' IndexInText='377' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<BinaryOperator Name='+' Priority='30' Id='637856640183927617' IndexInText='378' ItemLength='79' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.BinaryOperator Name='+' Priority='30' Id='637856640183927607' IndexInText='378' ItemLength='57' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
							<Operand1.ConstantText Id='637856640183927199' IndexInText='378' ItemLength='53' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='"This is a text that spans$line_break$ multiple $line_break$ lines.   $line_break$"' Id='637856640183927193' IndexInText='378' ItemLength='53' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Operand1.ConstantText>
							<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637856640183927256' IndexInText='432' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
								<OperatorNameParts>
									<Name Name='+' Id='637856640183927250' IndexInText='432' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</OperatorNameParts>
							</OperatorInfo>
							<Operand2.Literal Id='637856640183927382' IndexInText='434' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='x' Id='637856640183927378' IndexInText='434' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Operand2.Literal>
						</Operand1.BinaryOperator>
						<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637856640183927437' IndexInText='436' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<Name Name='+' Id='637856640183927431' IndexInText='436' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OperatorNameParts>
						</OperatorInfo>
						<Operand2.ConstantText Id='637856640183927567' IndexInText='438' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name=''' Some other text.''' Id='637856640183927562' IndexInText='438' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand2.ConstantText>
					</BinaryOperator>
					<ClosingRoundBrace Name=')' Id='637856640183927641' IndexInText='457' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Children>
					<BinaryOperator Name='+' Priority='30' Id='637856640183927617' IndexInText='378' ItemLength='79' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
				</Children>
				<OtherProperties>
					<NamedExpressionItem Id='637856640183927065' IndexInText='370' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					<OpeningBraceInfo Name='(' Id='637856640183927105' IndexInText='377' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ClosingBraceInfo Name=')' Id='637856640183927641' IndexInText='457' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Parameters ObjectId='42' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name='+' Priority='30' Id='637856640183927617' IndexInText='378' ItemLength='79' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
					</Parameters>
				</OtherProperties>
			</Braces>
			<ExpressionSeparator Name=';' Id='637856640183927680' IndexInText='458' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
		</RegularItems>
		<Children>
			<BinaryOperator Name='=' Priority='2000' Id='637856640183926892' IndexInText='287' ItemLength='78' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
			<Braces Id='637856640183927096' IndexInText='370' ItemLength='88' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
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
	<MainExpressionItem Id='637856640183925614' IndexInText='287' ItemLength='172' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'/>
</UniversalExpressionParser.ExpressionItems.IRootExpressionItemSeries>
```
</details>

# Code Separators and Code Blocks

- If the value of property **char ExpressionSeparatorCharacter { get; }** in **UniversalExpressionParser.IExpressionLanguageProvider** is not equal to character '\0', multiple expressions can be used in a single expression. 

For example if the value of property **ExpressionSeparatorCharacter** is ';' the expression "var x=f1(y);println(x)" will be parsed to a list of two expression items for "x=f1(y)" and "println(x)". Otherwise, the parser will report an error for this expression (see section on **Error Reporting** for more details on errors). 

- If both values of properties **char CodeBlockStartMarker { get; }** and **string CodeBlockEndMarker { get; }** in **UniversalExpressionParser.IExpressionLanguageProvider** are not equal to character '\0', code block expressions can be used to combine multiple expressions into code block expression items of type **UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem**. 

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
	<ExpressionItemSeries Id='637856640179935816' IndexInText='0' ItemLength='125' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'>
		<RegularItems>
			<CodeBlock Id='637856640179936045' IndexInText='0' ItemLength='39' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
				<RegularItems>
					<CodeBlockStartMarker Name='{' Id='637856640179936038' IndexInText='0' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<BinaryOperator Name='=' Priority='2000' Id='637856640180022405' IndexInText='7' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.Literal Id='637856640179936216' IndexInText='7' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='x' Id='637856640179936211' IndexInText='11' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<AppliedKeywords>
								<Keyword Name='var' Id='637856640179936102' IndexInText='7' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
									<LanguageKeywordInfo ObjectId='8' Id='637781064051574641' Keyword='var' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
								</Keyword>
							</AppliedKeywords>
						</Operand1.Literal>
						<OperatorInfo OperatorType='BinaryOperator' Name='=' Priority='2000' Id='637856640179936358' IndexInText='13' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<Name Name='=' Id='637856640179936347' IndexInText='13' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OperatorNameParts>
						</OperatorInfo>
						<Operand2.BinaryOperator Name='^' Priority='10' Id='637856640180022440' IndexInText='15' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
							<Operand1.Literal Id='637856640179936524' IndexInText='15' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='y' Id='637856640179936519' IndexInText='15' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Operand1.Literal>
							<OperatorInfo OperatorType='BinaryOperator' Name='^' Priority='10' Id='637856640179936594' IndexInText='16' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
								<OperatorNameParts>
									<Name Name='^' Id='637856640179936588' IndexInText='16' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</OperatorNameParts>
							</OperatorInfo>
							<Operand2.ConstantNumericValue Id='637856640180022167' IndexInText='17' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
								<NameExpressionItem Name='2' Id='637856640180022110' IndexInText='17' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
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
					<ExpressionSeparator Name=';' Id='637856640180022514' IndexInText='18' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Braces Id='637856640180022737' IndexInText='25' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<Literal Id='637856640180022704' IndexInText='25' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='println' Id='637856640180022699' IndexInText='25' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<OpeningRoundBrace Name='(' Id='637856640180022741' IndexInText='32' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Literal Id='637856640180023092' IndexInText='33' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='x' Id='637856640180023087' IndexInText='33' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<ClosingRoundBrace Name=')' Id='637856640180023132' IndexInText='34' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<Literal Id='637856640180023092' IndexInText='33' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
						</Children>
						<OtherProperties>
							<NamedExpressionItem Id='637856640180022704' IndexInText='25' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							<OpeningBraceInfo Name='(' Id='637856640180022741' IndexInText='32' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=')' Id='637856640180023132' IndexInText='34' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Parameters ObjectId='28' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640180023092' IndexInText='33' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Braces>
					<ExpressionSeparator Name=';' Id='637856640180023173' IndexInText='35' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<CodeBlockEndMarker Name='}' Id='637856640180023211' IndexInText='38' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Children>
					<BinaryOperator Name='=' Priority='2000' Id='637856640180022405' IndexInText='7' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
					<Braces Id='637856640180022737' IndexInText='25' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
				</Children>
				<OtherProperties>
					<CodeBlockStartMarker Name='{' Id='637856640179936038' IndexInText='0' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<CodeBlockEndMarker Name='}' Id='637856640180023211' IndexInText='38' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</OtherProperties>
			</CodeBlock>
			<CodeBlock Id='637856640180023448' IndexInText='43' ItemLength='82' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
				<RegularItems>
					<CodeBlockStartMarker Name='{' Id='637856640180023443' IndexInText='43' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Braces Id='637856640180023801' IndexInText='50' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<Literal Id='637856640180023709' IndexInText='50' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='fl' Id='637856640180023689' IndexInText='50' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<OpeningRoundBrace Name='(' Id='637856640180023805' IndexInText='52' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Literal Id='637856640180024061' IndexInText='53' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='x1' Id='637856640180024056' IndexInText='53' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<Comma Name=',' Id='637856640180024123' IndexInText='55' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Literal Id='637856640180024200' IndexInText='57' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='x2' Id='637856640180024196' IndexInText='57' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<ClosingRoundBrace Name=')' Id='637856640180024254' IndexInText='59' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<Literal Id='637856640180024061' IndexInText='53' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							<Literal Id='637856640180024200' IndexInText='57' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
						</Children>
						<OtherProperties>
							<NamedExpressionItem Id='637856640180023709' IndexInText='50' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							<OpeningBraceInfo Name='(' Id='637856640180023805' IndexInText='52' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=')' Id='637856640180024254' IndexInText='59' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Parameters ObjectId='43' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640180024061' IndexInText='53' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640180024200' IndexInText='57' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Braces>
					<ExpressionSeparator Name=';' Id='637856640180024288' IndexInText='60' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Braces Id='637856640180024450' IndexInText='67' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<Literal Id='637856640180024421' IndexInText='67' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='println' Id='637856640180024363' IndexInText='67' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<OpeningRoundBrace Name='(' Id='637856640180024453' IndexInText='74' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Literal Id='637856640180024581' IndexInText='75' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='x' Id='637856640180024528' IndexInText='75' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<ClosingRoundBrace Name=')' Id='637856640180024611' IndexInText='76' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<Literal Id='637856640180024581' IndexInText='75' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
						</Children>
						<OtherProperties>
							<NamedExpressionItem Id='637856640180024421' IndexInText='67' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							<OpeningBraceInfo Name='(' Id='637856640180024453' IndexInText='74' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=')' Id='637856640180024611' IndexInText='76' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Parameters ObjectId='52' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640180024581' IndexInText='75' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Braces>
					<CodeBlockEndMarker Name='}' Id='637856640180024669' IndexInText='124' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Children>
					<Braces Id='637856640180023801' IndexInText='50' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
					<Braces Id='637856640180024450' IndexInText='67' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
				</Children>
				<OtherProperties>
					<CodeBlockStartMarker Name='{' Id='637856640180023443' IndexInText='43' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<CodeBlockEndMarker Name='}' Id='637856640180024669' IndexInText='124' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</OtherProperties>
			</CodeBlock>
		</RegularItems>
		<Children>
			<CodeBlock Id='637856640179936045' IndexInText='0' ItemLength='39' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'/>
			<CodeBlock Id='637856640180023448' IndexInText='43' ItemLength='82' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'/>
		</Children>
	</ExpressionItemSeries>
	<ParseErrorData ObjectId='54' HasCriticalErrors='False' Interface='UniversalExpressionParser.IParseErrorData'>
		<AllCodeItemErrors ObjectId='55' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICodeItemParseErrorData]'/>
	</ParseErrorData>
	<SortedCommentedOutCodeInfos ObjectId='56' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICommentedOutCodeInfo]'>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='57' IsLineComment='True' IndexInText='78' ItemLength='44'/>
	</SortedCommentedOutCodeInfos>
	<MainExpressionItem Id='637856640179935816' IndexInText='0' ItemLength='125' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'/>
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
	<ExpressionItemSeries Id='637856640179450663' IndexInText='0' ItemLength='807' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'>
		<RegularItems>
			<BinaryOperator Name='=' Priority='2000' Id='637856640179562628' IndexInText='0' ItemLength='21' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
				<Operand1.Literal Id='637856640179451054' IndexInText='0' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
					<NameExpressionItem Name='y' Id='637856640179451049' IndexInText='4' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<AppliedKeywords>
						<Keyword Name='var' Id='637856640179450886' IndexInText='0' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
							<LanguageKeywordInfo ObjectId='6' Id='637781064051574641' Keyword='var' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
						</Keyword>
					</AppliedKeywords>
				</Operand1.Literal>
				<OperatorInfo OperatorType='BinaryOperator' Name='=' Priority='2000' Id='637856640179451208' IndexInText='6' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
					<OperatorNameParts>
						<Name Name='=' Id='637856640179451196' IndexInText='6' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</OperatorNameParts>
				</OperatorInfo>
				<Operand2.BinaryOperator Name='+' Priority='30' Id='637856640179562651' IndexInText='8' ItemLength='13' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
					<Operand1.Literal Id='637856640179451359' IndexInText='8' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='x1' Id='637856640179451354' IndexInText='8' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Operand1.Literal>
					<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637856640179451435' IndexInText='11' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
						<OperatorNameParts>
							<Name Name='+' Id='637856640179451428' IndexInText='11' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OperatorNameParts>
					</OperatorInfo>
					<Operand2.BinaryOperator Name='*' Priority='20' Id='637856640179562659' IndexInText='13' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.ConstantNumericValue Id='637856640179561849' IndexInText='13' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
							<NameExpressionItem Name='2.5' Id='637856640179561149' IndexInText='13' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<OtherProperties>
								<SucceededNumericTypeDescriptor ObjectId='17' NumberTypeId='1581134136626' Interface='UniversalExpressionParser.NumericTypeDescriptor'>
									<RegularExpressions ObjectId='18' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[System.String]'>
										<System.String value='^(\d+\.\d+|\d+\.|\.\d+)' />
									</RegularExpressions>
								</SucceededNumericTypeDescriptor>
							</OtherProperties>
						</Operand1.ConstantNumericValue>
						<OperatorInfo OperatorType='BinaryOperator' Name='*' Priority='20' Id='637856640179562389' IndexInText='17' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<Name Name='*' Id='637856640179562376' IndexInText='17' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OperatorNameParts>
						</OperatorInfo>
						<Operand2.Literal Id='637856640179562577' IndexInText='19' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='x2' Id='637856640179562567' IndexInText='19' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand2.Literal>
					</Operand2.BinaryOperator>
				</Operand2.BinaryOperator>
			</BinaryOperator>
			<ExpressionSeparator Name=';' Id='637856640179562757' IndexInText='21' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<Braces Id='637856640179562897' IndexInText='26' ItemLength='86' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
				<RegularItems>
					<Literal Id='637856640179562864' IndexInText='26' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='f1' Id='637856640179562860' IndexInText='26' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<OpeningRoundBrace Name='(' Id='637856640179562901' IndexInText='28' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ClosingRoundBrace Name=')' Id='637856640179562944' IndexInText='29' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Postfixes>
					<CodeBlock Id='637856640179562988' IndexInText='32' ItemLength='80' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
						<RegularItems>
							<CodeBlockStartMarker Name='{' Id='637856640179562984' IndexInText='32' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637856640179563081' IndexInText='111' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<OtherProperties>
							<CodeBlockStartMarker Name='{' Id='637856640179562984' IndexInText='32' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637856640179563081' IndexInText='111' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OtherProperties>
					</CodeBlock>
				</Postfixes>
				<OtherProperties>
					<NamedExpressionItem Id='637856640179562864' IndexInText='26' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					<OpeningBraceInfo Name='(' Id='637856640179562901' IndexInText='28' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ClosingBraceInfo Name=')' Id='637856640179562944' IndexInText='29' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Parameters ObjectId='32' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'/>
				</OtherProperties>
			</Braces>
			<BinaryOperator Name='=' Priority='2000' Id='637856640179636728' IndexInText='116' ItemLength='15' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
				<Operand1.Literal Id='637856640179563203' IndexInText='116' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
					<NameExpressionItem Name='z' Id='637856640179563199' IndexInText='120' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<AppliedKeywords>
						<Keyword Name='var' Id='637856640179563109' IndexInText='116' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
							<LanguageKeywordInfo ObjectId='6' Id='637781064051574641' Keyword='var' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
						</Keyword>
					</AppliedKeywords>
				</Operand1.Literal>
				<OperatorInfo OperatorType='BinaryOperator' Name='=' Priority='2000' Id='637856640179563275' IndexInText='122' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
					<OperatorNameParts>
						<Name Name='=' Id='637856640179563269' IndexInText='122' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</OperatorNameParts>
				</OperatorInfo>
				<Operand2.BinaryOperator Name='^' Priority='10' Id='637856640179636758' IndexInText='124' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
					<Operand1.Literal Id='637856640179563386' IndexInText='124' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='e' Id='637856640179563381' IndexInText='124' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Operand1.Literal>
					<OperatorInfo OperatorType='BinaryOperator' Name='^' Priority='10' Id='637856640179563444' IndexInText='126' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
						<OperatorNameParts>
							<Name Name='^' Id='637856640179563438' IndexInText='126' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OperatorNameParts>
					</OperatorInfo>
					<Operand2.ConstantNumericValue Id='637856640179636443' IndexInText='128' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
						<NameExpressionItem Name='2.3' Id='637856640179636364' IndexInText='128' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<OtherProperties>
							<SucceededNumericTypeDescriptor ObjectId='17' NumberTypeId='1581134136626' Interface='UniversalExpressionParser.NumericTypeDescriptor'/>
						</OtherProperties>
					</Operand2.ConstantNumericValue>
				</Operand2.BinaryOperator>
			</BinaryOperator>
			<ExpressionSeparator Name=';' Id='637856640179636853' IndexInText='131' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<CodeBlock Id='637856640179637083' IndexInText='134' ItemLength='341' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
				<RegularItems>
					<CodeBlockStartMarker Name='{' Id='637856640179637078' IndexInText='134' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<BinaryOperator Name='=' Priority='2000' Id='637856640179718693' IndexInText='141' ItemLength='13' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.Literal Id='637856640179637254' IndexInText='141' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='x' Id='637856640179637250' IndexInText='145' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<AppliedKeywords>
								<Keyword Name='var' Id='637856640179637114' IndexInText='141' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
									<LanguageKeywordInfo ObjectId='6' Id='637781064051574641' Keyword='var' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
								</Keyword>
							</AppliedKeywords>
						</Operand1.Literal>
						<OperatorInfo OperatorType='BinaryOperator' Name='=' Priority='2000' Id='637856640179637343' IndexInText='147' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<Name Name='=' Id='637856640179637327' IndexInText='147' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OperatorNameParts>
						</OperatorInfo>
						<Operand2.BinaryOperator Name='*' Priority='20' Id='637856640179718714' IndexInText='149' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
							<Operand1.ConstantNumericValue Id='637856640179718224' IndexInText='149' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
								<NameExpressionItem Name='5' Id='637856640179718178' IndexInText='149' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<OtherProperties>
									<SucceededNumericTypeDescriptor ObjectId='58' NumberTypeId='1581134183960' Interface='UniversalExpressionParser.NumericTypeDescriptor'>
										<RegularExpressions ObjectId='59' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[System.String]'>
											<System.String value='^\d+' />
										</RegularExpressions>
									</SucceededNumericTypeDescriptor>
								</OtherProperties>
							</Operand1.ConstantNumericValue>
							<OperatorInfo OperatorType='BinaryOperator' Name='*' Priority='20' Id='637856640179718459' IndexInText='151' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
								<OperatorNameParts>
									<Name Name='*' Id='637856640179718448' IndexInText='151' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</OperatorNameParts>
							</OperatorInfo>
							<Operand2.Literal Id='637856640179718641' IndexInText='153' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='y' Id='637856640179718633' IndexInText='153' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Operand2.Literal>
						</Operand2.BinaryOperator>
					</BinaryOperator>
					<ExpressionSeparator Name=';' Id='637856640179718770' IndexInText='154' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Braces Id='637856640179718886' IndexInText='161' ItemLength='17' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<Literal Id='637856640179718858' IndexInText='161' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='println' Id='637856640179718854' IndexInText='161' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<OpeningRoundBrace Name='(' Id='637856640179718889' IndexInText='168' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<BinaryOperator Name='+' Priority='30' Id='637856640179719245' IndexInText='169' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
								<Operand1.ConstantText Id='637856640179719023' IndexInText='169' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
									<NameExpressionItem Name='"x="' Id='637856640179719017' IndexInText='169' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</Operand1.ConstantText>
								<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637856640179719078' IndexInText='174' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
									<OperatorNameParts>
										<Name Name='+' Id='637856640179719073' IndexInText='174' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</OperatorNameParts>
								</OperatorInfo>
								<Operand2.Literal Id='637856640179719205' IndexInText='176' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
									<NameExpressionItem Name='x' Id='637856640179719201' IndexInText='176' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</Operand2.Literal>
							</BinaryOperator>
							<ClosingRoundBrace Name=')' Id='637856640179719267' IndexInText='177' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<BinaryOperator Name='+' Priority='30' Id='637856640179719245' IndexInText='169' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
						</Children>
						<OtherProperties>
							<NamedExpressionItem Id='637856640179718858' IndexInText='161' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							<OpeningBraceInfo Name='(' Id='637856640179718889' IndexInText='168' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=')' Id='637856640179719267' IndexInText='177' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Parameters ObjectId='77' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name='+' Priority='30' Id='637856640179719245' IndexInText='169' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Braces>
					<ExpressionSeparator Name=';' Id='637856640179719306' IndexInText='178' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<CodeBlock Id='637856640179719334' IndexInText='187' ItemLength='64' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
						<RegularItems>
							<CodeBlockStartMarker Name='{' Id='637856640179719330' IndexInText='187' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<BinaryOperator Name='=' Priority='2000' Id='637856640179798588' IndexInText='198' ItemLength='15' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
								<Operand1.Literal Id='637856640179719447' IndexInText='198' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
									<NameExpressionItem Name='y1' Id='637856640179719443' IndexInText='202' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<AppliedKeywords>
										<Keyword Name='var' Id='637856640179719360' IndexInText='198' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
											<LanguageKeywordInfo ObjectId='6' Id='637781064051574641' Keyword='var' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
										</Keyword>
									</AppliedKeywords>
								</Operand1.Literal>
								<OperatorInfo OperatorType='BinaryOperator' Name='=' Priority='2000' Id='637856640179719523' IndexInText='205' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
									<OperatorNameParts>
										<Name Name='=' Id='637856640179719517' IndexInText='205' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</OperatorNameParts>
								</OperatorInfo>
								<Operand2.BinaryOperator Name='*' Priority='20' Id='637856640179798608' IndexInText='207' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
									<Operand1.ConstantNumericValue Id='637856640179798099' IndexInText='207' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
										<NameExpressionItem Name='10' Id='637856640179798055' IndexInText='207' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										<OtherProperties>
											<SucceededNumericTypeDescriptor ObjectId='58' NumberTypeId='1581134183960' Interface='UniversalExpressionParser.NumericTypeDescriptor'/>
										</OtherProperties>
									</Operand1.ConstantNumericValue>
									<OperatorInfo OperatorType='BinaryOperator' Name='*' Priority='20' Id='637856640179798348' IndexInText='210' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
										<OperatorNameParts>
											<Name Name='*' Id='637856640179798334' IndexInText='210' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</OperatorNameParts>
									</OperatorInfo>
									<Operand2.Literal Id='637856640179798531' IndexInText='212' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='x' Id='637856640179798526' IndexInText='212' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Operand2.Literal>
								</Operand2.BinaryOperator>
							</BinaryOperator>
							<ExpressionSeparator Name=';' Id='637856640179798668' IndexInText='213' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Braces Id='637856640179798784' IndexInText='224' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
								<RegularItems>
									<Literal Id='637856640179798756' IndexInText='224' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='println' Id='637856640179798752' IndexInText='224' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<OpeningRoundBrace Name='(' Id='637856640179798787' IndexInText='231' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Braces Id='637856640179798910' IndexInText='232' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
										<RegularItems>
											<Literal Id='637856640179798885' IndexInText='232' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
												<NameExpressionItem Name='getExp' Id='637856640179798880' IndexInText='232' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											</Literal>
											<OpeningRoundBrace Name='(' Id='637856640179798913' IndexInText='238' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<Literal Id='637856640179799000' IndexInText='239' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
												<NameExpressionItem Name='y1' Id='637856640179798995' IndexInText='239' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											</Literal>
											<ClosingRoundBrace Name=')' Id='637856640179799031' IndexInText='241' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</RegularItems>
										<Children>
											<Literal Id='637856640179799000' IndexInText='239' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
										</Children>
										<OtherProperties>
											<NamedExpressionItem Id='637856640179798885' IndexInText='232' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
											<OpeningBraceInfo Name='(' Id='637856640179798913' IndexInText='238' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<ClosingBraceInfo Name=')' Id='637856640179799031' IndexInText='241' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<Parameters ObjectId='106' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
												<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640179799000' IndexInText='239' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
											</Parameters>
										</OtherProperties>
									</Braces>
									<ClosingRoundBrace Name=')' Id='637856640179799068' IndexInText='242' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Children>
									<Braces Id='637856640179798910' IndexInText='232' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
								</Children>
								<OtherProperties>
									<NamedExpressionItem Id='637856640179798756' IndexInText='224' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<OpeningBraceInfo Name='(' Id='637856640179798787' IndexInText='231' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ClosingBraceInfo Name=')' Id='637856640179799068' IndexInText='242' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Parameters ObjectId='108' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640179798910' IndexInText='232' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
									</Parameters>
								</OtherProperties>
							</Braces>
							<ExpressionSeparator Name=';' Id='637856640179799093' IndexInText='243' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637856640179799162' IndexInText='250' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<BinaryOperator Name='=' Priority='2000' Id='637856640179798588' IndexInText='198' ItemLength='15' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
							<Braces Id='637856640179798784' IndexInText='224' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
						</Children>
						<OtherProperties>
							<CodeBlockStartMarker Name='{' Id='637856640179719330' IndexInText='187' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637856640179799162' IndexInText='250' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OtherProperties>
					</CodeBlock>
					<CodeBlock Id='637856640179799250' IndexInText='259' ItemLength='64' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
						<RegularItems>
							<CodeBlockStartMarker Name='{' Id='637856640179799245' IndexInText='259' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<BinaryOperator Name='=' Priority='2000' Id='637856640179878958' IndexInText='270' ItemLength='15' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
								<Operand1.Literal Id='637856640179799474' IndexInText='270' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
									<NameExpressionItem Name='y2' Id='637856640179799469' IndexInText='274' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<AppliedKeywords>
										<Keyword Name='var' Id='637856640179799344' IndexInText='270' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
											<LanguageKeywordInfo ObjectId='6' Id='637781064051574641' Keyword='var' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
										</Keyword>
									</AppliedKeywords>
								</Operand1.Literal>
								<OperatorInfo OperatorType='BinaryOperator' Name='=' Priority='2000' Id='637856640179799551' IndexInText='277' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
									<OperatorNameParts>
										<Name Name='=' Id='637856640179799541' IndexInText='277' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</OperatorNameParts>
								</OperatorInfo>
								<Operand2.BinaryOperator Name='*' Priority='20' Id='637856640179878979' IndexInText='279' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
									<Operand1.ConstantNumericValue Id='637856640179878491' IndexInText='279' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
										<NameExpressionItem Name='20' Id='637856640179878444' IndexInText='279' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										<OtherProperties>
											<SucceededNumericTypeDescriptor ObjectId='58' NumberTypeId='1581134183960' Interface='UniversalExpressionParser.NumericTypeDescriptor'/>
										</OtherProperties>
									</Operand1.ConstantNumericValue>
									<OperatorInfo OperatorType='BinaryOperator' Name='*' Priority='20' Id='637856640179878721' IndexInText='282' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
										<OperatorNameParts>
											<Name Name='*' Id='637856640179878707' IndexInText='282' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</OperatorNameParts>
									</OperatorInfo>
									<Operand2.Literal Id='637856640179878898' IndexInText='284' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='x' Id='637856640179878893' IndexInText='284' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Operand2.Literal>
								</Operand2.BinaryOperator>
							</BinaryOperator>
							<ExpressionSeparator Name=';' Id='637856640179879033' IndexInText='285' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Braces Id='637856640179879203' IndexInText='296' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
								<RegularItems>
									<Literal Id='637856640179879139' IndexInText='296' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='println' Id='637856640179879135' IndexInText='296' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<OpeningRoundBrace Name='(' Id='637856640179879207' IndexInText='303' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Braces Id='637856640179879329' IndexInText='304' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
										<RegularItems>
											<Literal Id='637856640179879304' IndexInText='304' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
												<NameExpressionItem Name='getExp' Id='637856640179879299' IndexInText='304' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											</Literal>
											<OpeningRoundBrace Name='(' Id='637856640179879332' IndexInText='310' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<Literal Id='637856640179879412' IndexInText='311' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
												<NameExpressionItem Name='y2' Id='637856640179879408' IndexInText='311' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											</Literal>
											<ClosingRoundBrace Name=')' Id='637856640179879445' IndexInText='313' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</RegularItems>
										<Children>
											<Literal Id='637856640179879412' IndexInText='311' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
										</Children>
										<OtherProperties>
											<NamedExpressionItem Id='637856640179879304' IndexInText='304' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
											<OpeningBraceInfo Name='(' Id='637856640179879332' IndexInText='310' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<ClosingBraceInfo Name=')' Id='637856640179879445' IndexInText='313' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<Parameters ObjectId='138' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
												<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640179879412' IndexInText='311' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
											</Parameters>
										</OtherProperties>
									</Braces>
									<ClosingRoundBrace Name=')' Id='637856640179879479' IndexInText='314' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Children>
									<Braces Id='637856640179879329' IndexInText='304' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
								</Children>
								<OtherProperties>
									<NamedExpressionItem Id='637856640179879139' IndexInText='296' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<OpeningBraceInfo Name='(' Id='637856640179879207' IndexInText='303' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ClosingBraceInfo Name=')' Id='637856640179879479' IndexInText='314' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Parameters ObjectId='140' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640179879329' IndexInText='304' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
									</Parameters>
								</OtherProperties>
							</Braces>
							<ExpressionSeparator Name=';' Id='637856640179879503' IndexInText='315' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637856640179879532' IndexInText='322' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<BinaryOperator Name='=' Priority='2000' Id='637856640179878958' IndexInText='270' ItemLength='15' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
							<Braces Id='637856640179879203' IndexInText='296' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
						</Children>
						<OtherProperties>
							<CodeBlockStartMarker Name='{' Id='637856640179799245' IndexInText='259' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637856640179879532' IndexInText='322' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OtherProperties>
					</CodeBlock>
					<BinaryOperator Name=':' Priority='0' Id='637856640179880568' IndexInText='331' ItemLength='141' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.Braces Id='637856640179879647' IndexInText='331' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
							<RegularItems>
								<Literal Id='637856640179879618' IndexInText='331' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
									<NameExpressionItem Name='getExp' Id='637856640179879615' IndexInText='331' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</Literal>
								<OpeningRoundBrace Name='(' Id='637856640179879650' IndexInText='337' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<Literal Id='637856640179879725' IndexInText='338' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
									<NameExpressionItem Name='x' Id='637856640179879721' IndexInText='338' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</Literal>
								<ClosingRoundBrace Name=')' Id='637856640179879752' IndexInText='339' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</RegularItems>
							<Children>
								<Literal Id='637856640179879725' IndexInText='338' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							</Children>
							<OtherProperties>
								<NamedExpressionItem Id='637856640179879618' IndexInText='331' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								<OpeningBraceInfo Name='(' Id='637856640179879650' IndexInText='337' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<ClosingBraceInfo Name=')' Id='637856640179879752' IndexInText='339' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<Parameters ObjectId='151' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
									<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640179879725' IndexInText='338' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								</Parameters>
							</OtherProperties>
						</Operand1.Braces>
						<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637856640179879804' IndexInText='341' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<Name Name=':' Id='637856640179879796' IndexInText='341' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OperatorNameParts>
						</OperatorInfo>
						<Operand2.Literal Id='637856640179879944' IndexInText='343' ItemLength='129' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='double' Id='637856640179879939' IndexInText='343' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Postfixes>
								<CodeBlock Id='637856640179879981' IndexInText='355' ItemLength='117' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
									<RegularItems>
										<CodeBlockStartMarker Name='{' Id='637856640179879976' IndexInText='355' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										<PrefixUnaryOperator Name='return' Priority='2147483647' Id='637856640179880494' IndexInText='454' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
											<OperatorInfo OperatorType='PrefixUnaryOperator' Name='return' Priority='2147483647' Id='637856640179880149' IndexInText='454' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
												<OperatorNameParts>
													<Name Name='return' Id='637856640179880142' IndexInText='454' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												</OperatorNameParts>
											</OperatorInfo>
											<Operand1.BinaryOperator Name='^' Priority='10' Id='637856640179880507' IndexInText='461' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
												<Operand1.Literal Id='637856640179880252' IndexInText='461' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
													<NameExpressionItem Name='e' Id='637856640179880248' IndexInText='461' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												</Operand1.Literal>
												<OperatorInfo OperatorType='BinaryOperator' Name='^' Priority='10' Id='637856640179880316' IndexInText='462' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
													<OperatorNameParts>
														<Name Name='^' Id='637856640179880311' IndexInText='462' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
													</OperatorNameParts>
												</OperatorInfo>
												<Operand2.Literal Id='637856640179880456' IndexInText='463' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
													<NameExpressionItem Name='x' Id='637856640179880452' IndexInText='463' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												</Operand2.Literal>
											</Operand1.BinaryOperator>
										</PrefixUnaryOperator>
										<ExpressionSeparator Name=';' Id='637856640179880531' IndexInText='464' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										<CodeBlockEndMarker Name='}' Id='637856640179880557' IndexInText='471' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</RegularItems>
									<Children>
										<PrefixUnaryOperator Name='return' Priority='2147483647' Id='637856640179880494' IndexInText='454' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
									</Children>
									<OtherProperties>
										<CodeBlockStartMarker Name='{' Id='637856640179879976' IndexInText='355' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										<CodeBlockEndMarker Name='}' Id='637856640179880557' IndexInText='471' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</OtherProperties>
								</CodeBlock>
							</Postfixes>
						</Operand2.Literal>
					</BinaryOperator>
					<CodeBlockEndMarker Name='}' Id='637856640179880600' IndexInText='474' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Children>
					<BinaryOperator Name='=' Priority='2000' Id='637856640179718693' IndexInText='141' ItemLength='13' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
					<Braces Id='637856640179718886' IndexInText='161' ItemLength='17' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
					<CodeBlock Id='637856640179719334' IndexInText='187' ItemLength='64' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'/>
					<CodeBlock Id='637856640179799250' IndexInText='259' ItemLength='64' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'/>
					<BinaryOperator Name=':' Priority='0' Id='637856640179880568' IndexInText='331' ItemLength='141' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
				</Children>
				<OtherProperties>
					<CodeBlockStartMarker Name='{' Id='637856640179637078' IndexInText='134' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<CodeBlockEndMarker Name='}' Id='637856640179880600' IndexInText='474' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</OtherProperties>
			</CodeBlock>
			<Braces Id='637856640179880713' IndexInText='479' ItemLength='94' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
				<RegularItems>
					<Literal Id='637856640179880688' IndexInText='479' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='f2' Id='637856640179880684' IndexInText='479' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<OpeningRoundBrace Name='(' Id='637856640179880716' IndexInText='481' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ClosingRoundBrace Name=')' Id='637856640179880748' IndexInText='482' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Postfixes>
					<CodeBlock Id='637856640179880777' IndexInText='485' ItemLength='88' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
						<RegularItems>
							<CodeBlockStartMarker Name='{' Id='637856640179880773' IndexInText='485' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637856640179880808' IndexInText='572' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<OtherProperties>
							<CodeBlockStartMarker Name='{' Id='637856640179880773' IndexInText='485' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637856640179880808' IndexInText='572' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OtherProperties>
					</CodeBlock>
				</Postfixes>
				<OtherProperties>
					<NamedExpressionItem Id='637856640179880688' IndexInText='479' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					<OpeningBraceInfo Name='(' Id='637856640179880716' IndexInText='481' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ClosingBraceInfo Name=')' Id='637856640179880748' IndexInText='482' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Parameters ObjectId='179' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'/>
				</OtherProperties>
			</Braces>
			<CodeBlock Id='637856640179880836' IndexInText='577' ItemLength='31' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
				<RegularItems>
					<CodeBlockStartMarker Name='{' Id='637856640179880832' IndexInText='577' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<CodeBlockEndMarker Name='}' Id='637856640179880867' IndexInText='607' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<OtherProperties>
					<CodeBlockStartMarker Name='{' Id='637856640179880832' IndexInText='577' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<CodeBlockEndMarker Name='}' Id='637856640179880867' IndexInText='607' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</OtherProperties>
			</CodeBlock>
			<Literal Id='637856640179880979' IndexInText='612' ItemLength='195' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
				<NameExpressionItem Name='Dog' Id='637856640179880975' IndexInText='625' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				<AppliedKeywords>
					<Keyword Name='public' Id='637856640179880888' IndexInText='612' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
						<LanguageKeywordInfo ObjectId='186' Id='637783669980406419' Keyword='public' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
					</Keyword>
					<Keyword Name='class' Id='637856640179880901' IndexInText='619' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
						<LanguageKeywordInfo ObjectId='188' Id='637783670388745402' Keyword='class' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
					</Keyword>
				</AppliedKeywords>
				<Postfixes>
					<CodeBlock Id='637856640179881009' IndexInText='630' ItemLength='177' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
						<RegularItems>
							<CodeBlockStartMarker Name='{' Id='637856640179881006' IndexInText='630' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Braces Id='637856640179881188' IndexInText='637' ItemLength='167' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
								<AppliedKeywords>
									<Keyword Name='public' Id='637856640179881025' IndexInText='637' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
										<LanguageKeywordInfo ObjectId='186' Id='637783669980406419' Keyword='public' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
									</Keyword>
								</AppliedKeywords>
								<RegularItems>
									<Literal Id='637856640179881104' IndexInText='644' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='Bark' Id='637856640179881100' IndexInText='644' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<OpeningRoundBrace Name='(' Id='637856640179881192' IndexInText='648' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ClosingRoundBrace Name=')' Id='637856640179881219' IndexInText='649' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Postfixes>
									<CodeBlock Id='637856640179881254' IndexInText='656' ItemLength='148' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
										<RegularItems>
											<CodeBlockStartMarker Name='{' Id='637856640179881250' IndexInText='656' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<Braces Id='637856640179881368' IndexInText='782' ItemLength='15' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
												<RegularItems>
													<Literal Id='637856640179881340' IndexInText='782' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
														<NameExpressionItem Name='println' Id='637856640179881336' IndexInText='782' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
													</Literal>
													<OpeningRoundBrace Name='(' Id='637856640179881371' IndexInText='789' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
													<ConstantText Id='637856640179881466' IndexInText='790' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
														<NameExpressionItem Name='"bark"' Id='637856640179881461' IndexInText='790' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
													</ConstantText>
													<ClosingRoundBrace Name=')' Id='637856640179881501' IndexInText='796' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												</RegularItems>
												<Children>
													<ConstantText Id='637856640179881466' IndexInText='790' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
												</Children>
												<OtherProperties>
													<NamedExpressionItem Id='637856640179881340' IndexInText='782' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
													<OpeningBraceInfo Name='(' Id='637856640179881371' IndexInText='789' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
													<ClosingBraceInfo Name=')' Id='637856640179881501' IndexInText='796' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
													<Parameters ObjectId='206' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
														<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640179881466' IndexInText='790' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
													</Parameters>
												</OtherProperties>
											</Braces>
											<CodeBlockEndMarker Name='}' Id='637856640179881528' IndexInText='803' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</RegularItems>
										<Children>
											<Braces Id='637856640179881368' IndexInText='782' ItemLength='15' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
										</Children>
										<OtherProperties>
											<CodeBlockStartMarker Name='{' Id='637856640179881250' IndexInText='656' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<CodeBlockEndMarker Name='}' Id='637856640179881528' IndexInText='803' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</OtherProperties>
									</CodeBlock>
								</Postfixes>
								<OtherProperties>
									<NamedExpressionItem Id='637856640179881104' IndexInText='644' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<OpeningBraceInfo Name='(' Id='637856640179881192' IndexInText='648' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ClosingBraceInfo Name=')' Id='637856640179881219' IndexInText='649' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Parameters ObjectId='208' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'/>
								</OtherProperties>
							</Braces>
							<CodeBlockEndMarker Name='}' Id='637856640179881552' IndexInText='806' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<Braces Id='637856640179881188' IndexInText='637' ItemLength='167' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
						</Children>
						<OtherProperties>
							<CodeBlockStartMarker Name='{' Id='637856640179881006' IndexInText='630' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637856640179881552' IndexInText='806' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OtherProperties>
					</CodeBlock>
				</Postfixes>
			</Literal>
		</RegularItems>
		<Children>
			<BinaryOperator Name='=' Priority='2000' Id='637856640179562628' IndexInText='0' ItemLength='21' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
			<Braces Id='637856640179562897' IndexInText='26' ItemLength='86' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
			<BinaryOperator Name='=' Priority='2000' Id='637856640179636728' IndexInText='116' ItemLength='15' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
			<CodeBlock Id='637856640179637083' IndexInText='134' ItemLength='341' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'/>
			<Braces Id='637856640179880713' IndexInText='479' ItemLength='94' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
			<CodeBlock Id='637856640179880836' IndexInText='577' ItemLength='31' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'/>
			<Literal Id='637856640179880979' IndexInText='612' ItemLength='195' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
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
	<MainExpressionItem Id='637856640179450663' IndexInText='0' ItemLength='807' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'/>
</UniversalExpressionParser.ExpressionItems.IRootExpressionItemSeries>
```
</details>

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
	<ExpressionItemSeries Id='637856640181297684' IndexInText='207' ItemLength='1843' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'>
		<RegularItems>
			<Literal Id='637856640181298049' IndexInText='207' ItemLength='16' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
				<NameExpressionItem Name='Dog' Id='637856640181298044' IndexInText='220' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				<AppliedKeywords>
					<Keyword Name='public' Id='637856640181297913' IndexInText='207' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
						<LanguageKeywordInfo ObjectId='5' Id='637783669980406419' Keyword='public' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
					</Keyword>
					<Keyword Name='class' Id='637856640181297929' IndexInText='214' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
						<LanguageKeywordInfo ObjectId='7' Id='637783670388745402' Keyword='class' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
					</Keyword>
				</AppliedKeywords>
			</Literal>
			<ExpressionSeparator Name=';' Id='637856640181298114' IndexInText='223' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<Braces Id='637856640181298267' IndexInText='436' ItemLength='18' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
				<AppliedKeywords>
					<Keyword Name='public' Id='637856640181298143' IndexInText='436' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
						<LanguageKeywordInfo ObjectId='5' Id='637783669980406419' Keyword='public' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
					</Keyword>
					<Keyword Name='static' Id='637856640181298151' IndexInText='443' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
						<LanguageKeywordInfo ObjectId='12' Id='637789181872233115' Keyword='static' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
					</Keyword>
				</AppliedKeywords>
				<RegularItems>
					<Literal Id='637856640181298229' IndexInText='450' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='F1' Id='637856640181298221' IndexInText='450' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<OpeningRoundBrace Name='(' Id='637856640181298273' IndexInText='452' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ClosingRoundBrace Name=')' Id='637856640181298344' IndexInText='453' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<OtherProperties>
					<NamedExpressionItem Id='637856640181298229' IndexInText='450' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					<OpeningBraceInfo Name='(' Id='637856640181298273' IndexInText='452' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ClosingBraceInfo Name=')' Id='637856640181298344' IndexInText='453' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Parameters ObjectId='17' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'/>
				</OtherProperties>
			</Braces>
			<ExpressionSeparator Name=';' Id='637856640181298377' IndexInText='454' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<Braces Id='637856640181298524' IndexInText='668' ItemLength='31' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
				<AppliedKeywords>
					<Keyword Name='public' Id='637856640181298405' IndexInText='668' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
						<LanguageKeywordInfo ObjectId='5' Id='637783669980406419' Keyword='public' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
					</Keyword>
					<Keyword Name='static' Id='637856640181298413' IndexInText='675' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
						<LanguageKeywordInfo ObjectId='12' Id='637789181872233115' Keyword='static' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
					</Keyword>
				</AppliedKeywords>
				<RegularItems>
					<Literal Id='637856640181298495' IndexInText='682' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='F1' Id='637856640181298492' IndexInText='682' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<OpeningRoundBrace Name='(' Id='637856640181298528' IndexInText='684' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ClosingRoundBrace Name=')' Id='637856640181298560' IndexInText='685' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Postfixes>
					<CodeBlock Id='637856640181298593' IndexInText='687' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
						<RegularItems>
							<CodeBlockStartMarker Name='{' Id='637856640181298589' IndexInText='687' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<PrefixUnaryOperator Name='return' Priority='2147483647' Id='637856640181383215' IndexInText='688' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
								<OperatorInfo OperatorType='PrefixUnaryOperator' Name='return' Priority='2147483647' Id='637856640181298646' IndexInText='688' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
									<OperatorNameParts>
										<Name Name='return' Id='637856640181298636' IndexInText='688' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</OperatorNameParts>
								</OperatorInfo>
								<Operand1.ConstantNumericValue Id='637856640181383011' IndexInText='695' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
									<NameExpressionItem Name='1' Id='637856640181382965' IndexInText='695' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<OtherProperties>
										<SucceededNumericTypeDescriptor ObjectId='33' NumberTypeId='1581134183960' Interface='UniversalExpressionParser.NumericTypeDescriptor'>
											<RegularExpressions ObjectId='34' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[System.String]'>
												<System.String value='^\d+' />
											</RegularExpressions>
										</SucceededNumericTypeDescriptor>
									</OtherProperties>
								</Operand1.ConstantNumericValue>
							</PrefixUnaryOperator>
							<ExpressionSeparator Name=';' Id='637856640181383320' IndexInText='696' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637856640181383364' IndexInText='698' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<PrefixUnaryOperator Name='return' Priority='2147483647' Id='637856640181383215' IndexInText='688' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
						</Children>
						<OtherProperties>
							<CodeBlockStartMarker Name='{' Id='637856640181298589' IndexInText='687' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637856640181383364' IndexInText='698' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OtherProperties>
					</CodeBlock>
				</Postfixes>
				<OtherProperties>
					<NamedExpressionItem Id='637856640181298495' IndexInText='682' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					<OpeningBraceInfo Name='(' Id='637856640181298528' IndexInText='684' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ClosingBraceInfo Name=')' Id='637856640181298560' IndexInText='685' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Parameters ObjectId='37' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'/>
				</OtherProperties>
			</Braces>
			<Braces Id='637856640181383466' IndexInText='908' ItemLength='21' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
				<AppliedKeywords>
					<Keyword Name='::codeMarker' Id='637856640181383433' IndexInText='908' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
						<LanguageKeywordInfo ObjectId='40' Id='637783880924005707' Keyword='::codeMarker' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
					</Keyword>
				</AppliedKeywords>
				<RegularItems>
					<OpeningRoundBrace Name='(' Id='637856640181383474' IndexInText='921' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Literal Id='637856640181383605' IndexInText='922' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='x1' Id='637856640181383601' IndexInText='922' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<Comma Name=',' Id='637856640181383647' IndexInText='924' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Literal Id='637856640181383730' IndexInText='926' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='x2' Id='637856640181383726' IndexInText='926' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<ClosingRoundBrace Name=')' Id='637856640181383758' IndexInText='928' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Children>
					<Literal Id='637856640181383605' IndexInText='922' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					<Literal Id='637856640181383730' IndexInText='926' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
				</Children>
				<OtherProperties>
					<OpeningBraceInfo Name='(' Id='637856640181383474' IndexInText='921' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ClosingBraceInfo Name=')' Id='637856640181383758' IndexInText='928' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
					<Parameters ObjectId='48' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640181383605' IndexInText='922' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640181383730' IndexInText='926' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					</Parameters>
				</OtherProperties>
			</Braces>
			<ExpressionSeparator Name=';' Id='637856640181383790' IndexInText='929' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<Braces Id='637856640181383943' IndexInText='1140' ItemLength='22' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
				<AppliedKeywords>
					<Keyword Name='::codeMarker' Id='637856640181383828' IndexInText='1140' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
						<LanguageKeywordInfo ObjectId='40' Id='637783880924005707' Keyword='::codeMarker' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
					</Keyword>
				</AppliedKeywords>
				<RegularItems>
					<Literal Id='637856640181383903' IndexInText='1153' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='m1' Id='637856640181383898' IndexInText='1153' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<OpeningSquareBrace Name='[' Id='637856640181383949' IndexInText='1155' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ConstantNumericValue Id='637856640181465288' IndexInText='1156' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
						<NameExpressionItem Name='2' Id='637856640181465236' IndexInText='1156' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<OtherProperties>
							<SucceededNumericTypeDescriptor ObjectId='33' NumberTypeId='1581134183960' Interface='UniversalExpressionParser.NumericTypeDescriptor'/>
						</OtherProperties>
					</ConstantNumericValue>
					<Comma Name=',' Id='637856640181465458' IndexInText='1157' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Literal Id='637856640181465598' IndexInText='1159' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='x1' Id='637856640181465593' IndexInText='1159' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<ClosingSquareBrace Name=']' Id='637856640181465641' IndexInText='1161' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Children>
					<ConstantNumericValue Id='637856640181465288' IndexInText='1156' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'/>
					<Literal Id='637856640181465598' IndexInText='1159' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
				</Children>
				<OtherProperties>
					<NamedExpressionItem Id='637856640181383903' IndexInText='1153' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					<OpeningBraceInfo Name='[' Id='637856640181383949' IndexInText='1155' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ClosingBraceInfo Name=']' Id='637856640181465641' IndexInText='1161' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Parameters ObjectId='61' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640181465288' IndexInText='1156' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'/>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640181465598' IndexInText='1159' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					</Parameters>
				</OtherProperties>
			</Braces>
			<ExpressionSeparator Name=';' Id='637856640181465685' IndexInText='1162' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<Braces Id='637856640181465783' IndexInText='1372' ItemLength='20' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
				<AppliedKeywords>
					<Keyword Name='::codeMarker' Id='637856640181465751' IndexInText='1372' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
						<LanguageKeywordInfo ObjectId='40' Id='637783880924005707' Keyword='::codeMarker' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
					</Keyword>
				</AppliedKeywords>
				<RegularItems>
					<OpeningSquareBrace Name='[' Id='637856640181465790' IndexInText='1384' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Literal Id='637856640181465946' IndexInText='1385' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='x1' Id='637856640181465937' IndexInText='1385' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<Comma Name=',' Id='637856640181465982' IndexInText='1387' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Literal Id='637856640181466064' IndexInText='1389' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='x2' Id='637856640181466060' IndexInText='1389' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<ClosingSquareBrace Name=']' Id='637856640181466092' IndexInText='1391' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Children>
					<Literal Id='637856640181465946' IndexInText='1385' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					<Literal Id='637856640181466064' IndexInText='1389' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
				</Children>
				<OtherProperties>
					<OpeningBraceInfo Name='[' Id='637856640181465790' IndexInText='1384' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ClosingBraceInfo Name=']' Id='637856640181466092' IndexInText='1391' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
					<Parameters ObjectId='72' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640181465946' IndexInText='1385' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640181466064' IndexInText='1389' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					</Parameters>
				</OtherProperties>
			</Braces>
			<ExpressionSeparator Name=';' Id='637856640181466124' IndexInText='1392' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<CodeBlock Id='637856640181466188' IndexInText='1601' ItemLength='25' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
				<AppliedKeywords>
					<Keyword Name='static' Id='637856640181466160' IndexInText='1601' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
						<LanguageKeywordInfo ObjectId='12' Id='637789181872233115' Keyword='static' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
					</Keyword>
				</AppliedKeywords>
				<RegularItems>
					<CodeBlockStartMarker Name='{' Id='637856640181466184' IndexInText='1610' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Literal Id='637856640181466294' IndexInText='1617' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='x' Id='637856640181466290' IndexInText='1621' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<AppliedKeywords>
							<Keyword Name='var' Id='637856640181466213' IndexInText='1617' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
								<LanguageKeywordInfo ObjectId='80' Id='637781064051574641' Keyword='var' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
							</Keyword>
						</AppliedKeywords>
					</Literal>
					<ExpressionSeparator Name=';' Id='637856640181466322' IndexInText='1622' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<CodeBlockEndMarker Name='}' Id='637856640181466345' IndexInText='1625' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Children>
					<Literal Id='637856640181466294' IndexInText='1617' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
				</Children>
				<OtherProperties>
					<CodeBlockStartMarker Name='{' Id='637856640181466184' IndexInText='1610' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<CodeBlockEndMarker Name='}' Id='637856640181466345' IndexInText='1625' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</OtherProperties>
			</CodeBlock>
			<BinaryOperator Name='=' Priority='2000' Id='637856640181553546' IndexInText='2009' ItemLength='40' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
				<Operand1.Literal Id='637856640181466464' IndexInText='2009' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
					<NameExpressionItem Name='y' Id='637856640181466460' IndexInText='2013' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<AppliedKeywords>
						<Keyword Name='var' Id='637856640181466383' IndexInText='2009' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
							<LanguageKeywordInfo ObjectId='80' Id='637781064051574641' Keyword='var' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
						</Keyword>
					</AppliedKeywords>
				</Operand1.Literal>
				<OperatorInfo OperatorType='BinaryOperator' Name='=' Priority='2000' Id='637856640181466542' IndexInText='2015' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
					<OperatorNameParts>
						<Name Name='=' Id='637856640181466532' IndexInText='2015' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</OperatorNameParts>
				</OperatorInfo>
				<Operand2.BinaryOperator Name='+' Priority='30' Id='637856640181553596' IndexInText='2017' ItemLength='32' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
					<Operand1.BinaryOperator Name='+' Priority='30' Id='637856640181553576' IndexInText='2017' ItemLength='29' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.BinaryOperator Name='+' Priority='30' Id='637856640181553567' IndexInText='2017' ItemLength='15' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
							<Operand1.Literal Id='637856640181466672' IndexInText='2017' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='x1' Id='637856640181466667' IndexInText='2017' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Operand1.Literal>
							<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637856640181466740' IndexInText='2020' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
								<OperatorNameParts>
									<Name Name='+' Id='637856640181466734' IndexInText='2020' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</OperatorNameParts>
							</OperatorInfo>
							<Operand2.Custom Id='637856640181466876' IndexInText='2021' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.Custom.IKeywordBasedCustomExpressionItem'>
								<RegularItems>
									<Keyword Name='::pragma' Id='637856640181466795' IndexInText='2021' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
										<LanguageKeywordInfo ObjectId='98' Id='637781063127646487' Keyword='::pragma' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
									</Keyword>
									<Name Name='x2' Id='637856640181466869' IndexInText='2030' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<OtherProperties>
									<LastKeywordExpressionItem Name='::pragma' Id='637856640181466795' IndexInText='2021' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'/>
									<ErrorsPositionDisplayValue value='2021' type='System.Int32' />
								</OtherProperties>
							</Operand2.Custom>
						</Operand1.BinaryOperator>
						<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637856640181466956' IndexInText='2032' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<Name Name='+' Id='637856640181466945' IndexInText='2032' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OperatorNameParts>
						</OperatorInfo>
						<Operand2.BinaryOperator Name='*' Priority='20' Id='637856640181553589' IndexInText='2033' ItemLength='13' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
							<Operand1.ConstantNumericValue Id='637856640181552475' IndexInText='2033' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
								<NameExpressionItem Name='3' Id='637856640181552415' IndexInText='2033' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<OtherProperties>
									<SucceededNumericTypeDescriptor ObjectId='33' NumberTypeId='1581134183960' Interface='UniversalExpressionParser.NumericTypeDescriptor'/>
								</OtherProperties>
							</Operand1.ConstantNumericValue>
							<OperatorInfo OperatorType='BinaryOperator' Name='*' Priority='20' Id='637856640181552994' IndexInText='2034' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
								<OperatorNameParts>
									<Name Name='*' Id='637856640181552956' IndexInText='2034' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</OperatorNameParts>
							</OperatorInfo>
							<Operand2.Custom Id='637856640181553243' IndexInText='2035' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.Custom.IKeywordBasedCustomExpressionItem'>
								<RegularItems>
									<Keyword Name='::pragma' Id='637856640181553113' IndexInText='2035' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
										<LanguageKeywordInfo ObjectId='98' Id='637781063127646487' Keyword='::pragma' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
									</Keyword>
									<Name Name='x3' Id='637856640181553236' IndexInText='2044' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<OtherProperties>
									<LastKeywordExpressionItem Name='::pragma' Id='637856640181553113' IndexInText='2035' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'/>
									<ErrorsPositionDisplayValue value='2035' type='System.Int32' />
								</OtherProperties>
							</Operand2.Custom>
						</Operand2.BinaryOperator>
					</Operand1.BinaryOperator>
					<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637856640181553357' IndexInText='2047' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
						<OperatorNameParts>
							<Name Name='+' Id='637856640181553347' IndexInText='2047' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OperatorNameParts>
					</OperatorInfo>
					<Operand2.Literal Id='637856640181553492' IndexInText='2048' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='y' Id='637856640181553487' IndexInText='2048' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Operand2.Literal>
				</Operand2.BinaryOperator>
			</BinaryOperator>
			<ExpressionSeparator Name=';' Id='637856640181553670' IndexInText='2049' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
		</RegularItems>
		<Children>
			<Literal Id='637856640181298049' IndexInText='207' ItemLength='16' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
			<Braces Id='637856640181298267' IndexInText='436' ItemLength='18' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
			<Braces Id='637856640181298524' IndexInText='668' ItemLength='31' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
			<Braces Id='637856640181383466' IndexInText='908' ItemLength='21' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
			<Braces Id='637856640181383943' IndexInText='1140' ItemLength='22' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
			<Braces Id='637856640181465783' IndexInText='1372' ItemLength='20' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
			<CodeBlock Id='637856640181466188' IndexInText='1601' ItemLength='25' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'/>
			<BinaryOperator Name='=' Priority='2000' Id='637856640181553546' IndexInText='2009' ItemLength='40' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
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
	<MainExpressionItem Id='637856640181297684' IndexInText='207' ItemLength='1843' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'/>
</UniversalExpressionParser.ExpressionItems.IRootExpressionItemSeries>
```
</details>

# Prefixes

Prefixes are one or more expression items that precede some other expression item, and are added to the list in property **Prefixes** in interface **UniversalExpressionParser.ExpressionItems.IComplexExpressionItem** for the expression item that follows the list of prefix expression items.

- **NOTE:** Prefixes are supported only if the value of property **SupportsPrefixes** in interface **UniversalExpressionParser.IExpressionLanguageProvider** is true.

Currently **Universal Expression Parser** supports two types of prefixes:

1) Square or round braces expressions items without names (i.e. expression items that are parsed to **UniversalExpressionParser.ExpressionItems.IBracesExpressionItem** with property **NamedExpressionItem** equal to **null**) that precede another expression item (e.g., another braces expression, a literal, a code block, text expression item, numeric value expression item, etc).

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
	<ExpressionItemSeries Id='637856640183362921' IndexInText='0' ItemLength='199' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'>
		<RegularItems>
			<Braces Id='637856640183363964' IndexInText='0' ItemLength='199' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
				<Prefixes>
					<Braces Id='637856640183363133' IndexInText='0' ItemLength='22' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<OpeningSquareBrace Name='[' Id='637856640183363138' IndexInText='0' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Literal Id='637856640183363271' IndexInText='1' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='NotNull' Id='637856640183363266' IndexInText='1' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<Comma Name=',' Id='637856640183363317' IndexInText='8' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Literal Id='637856640183363401' IndexInText='10' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='ItemNotNull' Id='637856640183363397' IndexInText='10' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<ClosingSquareBrace Name=']' Id='637856640183363430' IndexInText='21' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<Literal Id='637856640183363271' IndexInText='1' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							<Literal Id='637856640183363401' IndexInText='10' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
						</Children>
						<OtherProperties>
							<OpeningBraceInfo Name='[' Id='637856640183363138' IndexInText='0' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=']' Id='637856640183363430' IndexInText='21' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
							<Parameters ObjectId='11' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640183363271' IndexInText='1' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640183363401' IndexInText='10' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Braces>
					<Braces Id='637856640183363467' IndexInText='22' ItemLength='29' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<OpeningRoundBrace Name='(' Id='637856640183363482' IndexInText='22' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Braces Id='637856640183363595' IndexInText='23' ItemLength='27' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
								<RegularItems>
									<Literal Id='637856640183363564' IndexInText='23' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='Attribute' Id='637856640183363560' IndexInText='23' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<OpeningRoundBrace Name='(' Id='637856640183363598' IndexInText='32' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ConstantText Id='637856640183363699' IndexInText='33' ItemLength='16' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='"MarkedFunction"' Id='637856640183363693' IndexInText='33' ItemLength='16' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</ConstantText>
									<ClosingRoundBrace Name=')' Id='637856640183363729' IndexInText='49' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Children>
									<ConstantText Id='637856640183363699' IndexInText='33' ItemLength='16' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								</Children>
								<OtherProperties>
									<NamedExpressionItem Id='637856640183363564' IndexInText='23' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<OpeningBraceInfo Name='(' Id='637856640183363598' IndexInText='32' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ClosingBraceInfo Name=')' Id='637856640183363729' IndexInText='49' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Parameters ObjectId='21' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640183363699' IndexInText='33' ItemLength='16' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</Parameters>
								</OtherProperties>
							</Braces>
							<ClosingRoundBrace Name=')' Id='637856640183363760' IndexInText='50' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<Braces Id='637856640183363595' IndexInText='23' ItemLength='27' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
						</Children>
						<OtherProperties>
							<OpeningBraceInfo Name='(' Id='637856640183363482' IndexInText='22' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=')' Id='637856640183363760' IndexInText='50' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
							<Parameters ObjectId='23' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640183363595' IndexInText='23' ItemLength='27' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Braces>
				</Prefixes>
				<RegularItems>
					<Literal Id='637856640183363923' IndexInText='52' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='F1' Id='637856640183363918' IndexInText='52' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<OpeningRoundBrace Name='(' Id='637856640183363968' IndexInText='54' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Literal Id='637856640183364049' IndexInText='55' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='x' Id='637856640183364044' IndexInText='55' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<Comma Name=',' Id='637856640183364081' IndexInText='56' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Literal Id='637856640183364159' IndexInText='58' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='x2' Id='637856640183364155' IndexInText='58' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<ClosingRoundBrace Name=')' Id='637856640183364195' IndexInText='60' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Postfixes>
					<CodeBlock Id='637856640183364229' IndexInText='63' ItemLength='136' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
						<RegularItems>
							<CodeBlockStartMarker Name='{' Id='637856640183364225' IndexInText='63' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Braces Id='637856640183364360' IndexInText='175' ItemLength='20' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
								<RegularItems>
									<Literal Id='637856640183364333' IndexInText='175' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='retuens' Id='637856640183364329' IndexInText='175' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<OpeningSquareBrace Name='[' Id='637856640183364363' IndexInText='183' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637856640183364445' IndexInText='184' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='x1' Id='637856640183364440' IndexInText='184' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<Comma Name=',' Id='637856640183364473' IndexInText='186' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637856640183364551' IndexInText='188' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='x2' Id='637856640183364543' IndexInText='188' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<Comma Name=',' Id='637856640183364578' IndexInText='190' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637856640183364653' IndexInText='192' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='x3' Id='637856640183364650' IndexInText='192' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<ClosingSquareBrace Name=']' Id='637856640183364684' IndexInText='194' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Children>
									<Literal Id='637856640183364445' IndexInText='184' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<Literal Id='637856640183364551' IndexInText='188' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<Literal Id='637856640183364653' IndexInText='192' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								</Children>
								<OtherProperties>
									<NamedExpressionItem Id='637856640183364333' IndexInText='175' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<OpeningBraceInfo Name='[' Id='637856640183364363' IndexInText='183' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ClosingBraceInfo Name=']' Id='637856640183364684' IndexInText='194' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Parameters ObjectId='48' Count='3' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640183364445' IndexInText='184' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640183364551' IndexInText='188' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640183364653' IndexInText='192' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</Parameters>
								</OtherProperties>
							</Braces>
							<ExpressionSeparator Name=';' Id='637856640183364713' IndexInText='195' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637856640183364740' IndexInText='198' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<Braces Id='637856640183364360' IndexInText='175' ItemLength='20' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
						</Children>
						<OtherProperties>
							<CodeBlockStartMarker Name='{' Id='637856640183364225' IndexInText='63' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637856640183364740' IndexInText='198' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OtherProperties>
					</CodeBlock>
				</Postfixes>
				<Children>
					<Literal Id='637856640183364049' IndexInText='55' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					<Literal Id='637856640183364159' IndexInText='58' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
				</Children>
				<OtherProperties>
					<NamedExpressionItem Id='637856640183363923' IndexInText='52' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					<OpeningBraceInfo Name='(' Id='637856640183363968' IndexInText='54' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ClosingBraceInfo Name=')' Id='637856640183364195' IndexInText='60' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Parameters ObjectId='51' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640183364049' IndexInText='55' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640183364159' IndexInText='58' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					</Parameters>
				</OtherProperties>
			</Braces>
		</RegularItems>
		<Children>
			<Braces Id='637856640183363964' IndexInText='0' ItemLength='199' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
		</Children>
	</ExpressionItemSeries>
	<ParseErrorData ObjectId='52' HasCriticalErrors='False' Interface='UniversalExpressionParser.IParseErrorData'>
		<AllCodeItemErrors ObjectId='53' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICodeItemParseErrorData]'/>
	</ParseErrorData>
	<SortedCommentedOutCodeInfos ObjectId='54' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICommentedOutCodeInfo]'>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='55' IsLineComment='True' IndexInText='70' ItemLength='99'/>
	</SortedCommentedOutCodeInfos>
	<MainExpressionItem Id='637856640183362921' IndexInText='0' ItemLength='199' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'/>
</UniversalExpressionParser.ExpressionItems.IRootExpressionItemSeries>
```
</details>

2) One or more expressions that are parsed to custom expression items of type **UniversalExpressionParser.ExpressionItems.Custom.ICustomExpressionItem** with property **CustomExpressionItemCategory** equal to **UniversalExpressionParser.ExpressionItems.Custom.CustomExpressionItemCategory.Prefix** that precede another expression item. 

- In the example below, the expression items "::types[T1,T2]" and "::types[T3]" are parsed to custom expression items of type **UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem**, and are added as prefixes to braces expression item parsed from "F1(x:T1, y:T2, z: T3)".
- **NOTE:** For more details on custom expression items see section **Custom Expression Item Parsers**.

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
	<ExpressionItemSeries Id='637856640183385510' IndexInText='0' ItemLength='159' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'>
		<RegularItems>
			<Braces Id='637856640183386851' IndexInText='0' ItemLength='159' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
				<Prefixes>
					<Custom Id='637856640183386264' IndexInText='0' ItemLength='14' Interface='UniversalExpressionParser.ExpressionItems.Custom.IKeywordBasedCustomExpressionItem'>
						<RegularItems>
							<Keyword Name='::types' Id='637856640183385712' IndexInText='0' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
								<LanguageKeywordInfo ObjectId='5' Id='637781062811583137' Keyword='::types' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
							</Keyword>
							<Braces Id='637856640183385809' IndexInText='7' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
								<RegularItems>
									<OpeningSquareBrace Name='[' Id='637856640183385813' IndexInText='7' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637856640183385987' IndexInText='8' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='T1' Id='637856640183385982' IndexInText='8' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<Comma Name=',' Id='637856640183386120' IndexInText='10' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637856640183386206' IndexInText='11' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='T2' Id='637856640183386202' IndexInText='11' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<ClosingSquareBrace Name=']' Id='637856640183386249' IndexInText='13' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Children>
									<Literal Id='637856640183385987' IndexInText='8' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<Literal Id='637856640183386206' IndexInText='11' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								</Children>
								<OtherProperties>
									<OpeningBraceInfo Name='[' Id='637856640183385813' IndexInText='7' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ClosingBraceInfo Name=']' Id='637856640183386249' IndexInText='13' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
									<Parameters ObjectId='14' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640183385987' IndexInText='8' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640183386206' IndexInText='11' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</Parameters>
								</OtherProperties>
							</Braces>
						</RegularItems>
						<OtherProperties>
							<LastKeywordExpressionItem Name='::types' Id='637856640183385712' IndexInText='0' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'/>
							<ErrorsPositionDisplayValue value='0' type='System.Int32' />
						</OtherProperties>
					</Custom>
					<Custom Id='637856640183386545' IndexInText='15' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.Custom.IKeywordBasedCustomExpressionItem'>
						<RegularItems>
							<Keyword Name='::types' Id='637856640183386313' IndexInText='15' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
								<LanguageKeywordInfo ObjectId='5' Id='637781062811583137' Keyword='::types' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
							</Keyword>
							<Braces Id='637856640183386360' IndexInText='22' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
								<RegularItems>
									<OpeningSquareBrace Name='[' Id='637856640183386363' IndexInText='22' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637856640183386505' IndexInText='23' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='T3' Id='637856640183386499' IndexInText='23' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<ClosingSquareBrace Name=']' Id='637856640183386538' IndexInText='25' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Children>
									<Literal Id='637856640183386505' IndexInText='23' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								</Children>
								<OtherProperties>
									<OpeningBraceInfo Name='[' Id='637856640183386363' IndexInText='22' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ClosingBraceInfo Name=']' Id='637856640183386538' IndexInText='25' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
									<Parameters ObjectId='22' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640183386505' IndexInText='23' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</Parameters>
								</OtherProperties>
							</Braces>
						</RegularItems>
						<OtherProperties>
							<LastKeywordExpressionItem Name='::types' Id='637856640183386313' IndexInText='15' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'/>
							<ErrorsPositionDisplayValue value='15' type='System.Int32' />
						</OtherProperties>
					</Custom>
				</Prefixes>
				<RegularItems>
					<Literal Id='637856640183386816' IndexInText='27' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='F1' Id='637856640183386808' IndexInText='27' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<OpeningRoundBrace Name='(' Id='637856640183386856' IndexInText='29' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<BinaryOperator Name=':' Priority='0' Id='637856640183387221' IndexInText='30' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.Literal Id='637856640183386952' IndexInText='30' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='x' Id='637856640183386947' IndexInText='30' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand1.Literal>
						<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637856640183387013' IndexInText='31' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<Name Name=':' Id='637856640183386996' IndexInText='31' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OperatorNameParts>
						</OperatorInfo>
						<Operand2.Literal Id='637856640183387171' IndexInText='32' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='T1' Id='637856640183387166' IndexInText='32' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand2.Literal>
					</BinaryOperator>
					<Comma Name=',' Id='637856640183387280' IndexInText='34' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<BinaryOperator Name=':' Priority='0' Id='637856640183387604' IndexInText='36' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.Literal Id='637856640183387369' IndexInText='36' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='y' Id='637856640183387365' IndexInText='36' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand1.Literal>
						<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637856640183387419' IndexInText='37' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<Name Name=':' Id='637856640183387413' IndexInText='37' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OperatorNameParts>
						</OperatorInfo>
						<Operand2.Literal Id='637856640183387566' IndexInText='38' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='T2' Id='637856640183387561' IndexInText='38' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand2.Literal>
					</BinaryOperator>
					<Comma Name=',' Id='637856640183387622' IndexInText='40' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<BinaryOperator Name=':' Priority='0' Id='637856640183387956' IndexInText='42' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.Literal Id='637856640183387707' IndexInText='42' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='z' Id='637856640183387702' IndexInText='42' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand1.Literal>
						<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637856640183387763' IndexInText='43' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<Name Name=':' Id='637856640183387755' IndexInText='43' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OperatorNameParts>
						</OperatorInfo>
						<Operand2.Literal Id='637856640183387924' IndexInText='45' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='T3' Id='637856640183387919' IndexInText='45' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand2.Literal>
					</BinaryOperator>
					<ClosingRoundBrace Name=')' Id='637856640183388001' IndexInText='47' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Postfixes>
					<CodeBlock Id='637856640183388049' IndexInText='50' ItemLength='109' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
						<RegularItems>
							<CodeBlockStartMarker Name='{' Id='637856640183388044' IndexInText='50' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637856640183388104' IndexInText='158' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<OtherProperties>
							<CodeBlockStartMarker Name='{' Id='637856640183388044' IndexInText='50' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637856640183388104' IndexInText='158' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OtherProperties>
					</CodeBlock>
				</Postfixes>
				<Children>
					<BinaryOperator Name=':' Priority='0' Id='637856640183387221' IndexInText='30' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
					<BinaryOperator Name=':' Priority='0' Id='637856640183387604' IndexInText='36' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
					<BinaryOperator Name=':' Priority='0' Id='637856640183387956' IndexInText='42' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
				</Children>
				<OtherProperties>
					<NamedExpressionItem Id='637856640183386816' IndexInText='27' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					<OpeningBraceInfo Name='(' Id='637856640183386856' IndexInText='29' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ClosingBraceInfo Name=')' Id='637856640183388001' IndexInText='47' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Parameters ObjectId='53' Count='3' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name=':' Priority='0' Id='637856640183387221' IndexInText='30' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name=':' Priority='0' Id='637856640183387604' IndexInText='36' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name=':' Priority='0' Id='637856640183387956' IndexInText='42' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
					</Parameters>
				</OtherProperties>
			</Braces>
		</RegularItems>
		<Children>
			<Braces Id='637856640183386851' IndexInText='0' ItemLength='159' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
		</Children>
	</ExpressionItemSeries>
	<ParseErrorData ObjectId='54' HasCriticalErrors='False' Interface='UniversalExpressionParser.IParseErrorData'>
		<AllCodeItemErrors ObjectId='55' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICodeItemParseErrorData]'/>
	</ParseErrorData>
	<SortedCommentedOutCodeInfos ObjectId='56' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICommentedOutCodeInfo]'>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='57' IsLineComment='True' IndexInText='57' ItemLength='99'/>
	</SortedCommentedOutCodeInfos>
	<MainExpressionItem Id='637856640183385510' IndexInText='0' ItemLength='159' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'/>
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
	<ExpressionItemSeries Id='637856640183412494' IndexInText='95' ItemLength='943' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'>
		<RegularItems>
			<Literal Id='637856640183413565' IndexInText='95' ItemLength='943' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
				<NameExpressionItem Name='MyTests' Id='637856640183413560' IndexInText='209' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				<Prefixes>
					<Braces Id='637856640183412785' IndexInText='95' ItemLength='13' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<OpeningSquareBrace Name='[' Id='637856640183412792' IndexInText='95' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Literal Id='637856640183412923' IndexInText='96' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='TestFixture' Id='637856640183412918' IndexInText='96' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<ClosingSquareBrace Name=']' Id='637856640183412992' IndexInText='107' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<Literal Id='637856640183412923' IndexInText='96' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
						</Children>
						<OtherProperties>
							<OpeningBraceInfo Name='[' Id='637856640183412792' IndexInText='95' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=']' Id='637856640183412992' IndexInText='107' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
							<Parameters ObjectId='9' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640183412923' IndexInText='96' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Braces>
					<Braces Id='637856640183413029' IndexInText='110' ItemLength='30' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<OpeningSquareBrace Name='[' Id='637856640183413053' IndexInText='110' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Braces Id='637856640183413161' IndexInText='111' ItemLength='28' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
								<RegularItems>
									<Literal Id='637856640183413136' IndexInText='111' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='Attribute' Id='637856640183413132' IndexInText='111' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<OpeningRoundBrace Name='(' Id='637856640183413165' IndexInText='120' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ConstantText Id='637856640183413299' IndexInText='121' ItemLength='17' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='"IntegrationTest"' Id='637856640183413293' IndexInText='121' ItemLength='17' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</ConstantText>
									<ClosingRoundBrace Name=')' Id='637856640183413331' IndexInText='138' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Children>
									<ConstantText Id='637856640183413299' IndexInText='121' ItemLength='17' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								</Children>
								<OtherProperties>
									<NamedExpressionItem Id='637856640183413136' IndexInText='111' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<OpeningBraceInfo Name='(' Id='637856640183413165' IndexInText='120' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ClosingBraceInfo Name=')' Id='637856640183413331' IndexInText='138' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Parameters ObjectId='19' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640183413299' IndexInText='121' ItemLength='17' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</Parameters>
								</OtherProperties>
							</Braces>
							<ClosingSquareBrace Name=']' Id='637856640183413364' IndexInText='139' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<Braces Id='637856640183413161' IndexInText='111' ItemLength='28' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
						</Children>
						<OtherProperties>
							<OpeningBraceInfo Name='[' Id='637856640183413053' IndexInText='110' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=']' Id='637856640183413364' IndexInText='139' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
							<Parameters ObjectId='21' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640183413161' IndexInText='111' ItemLength='28' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Braces>
				</Prefixes>
				<AppliedKeywords>
					<Keyword Name='public' Id='637856640183413392' IndexInText='196' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
						<LanguageKeywordInfo ObjectId='23' Id='637783669980406419' Keyword='public' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
					</Keyword>
					<Keyword Name='class' Id='637856640183413405' IndexInText='203' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
						<LanguageKeywordInfo ObjectId='25' Id='637783670388745402' Keyword='class' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
					</Keyword>
				</AppliedKeywords>
				<Postfixes>
					<CodeBlock Id='637856640183413613' IndexInText='218' ItemLength='820' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
						<RegularItems>
							<CodeBlockStartMarker Name='{' Id='637856640183413609' IndexInText='218' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<BinaryOperator Name=':' Priority='0' Id='637856640183499313' IndexInText='461' ItemLength='574' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
								<Operand1.Braces Id='637856640183498907' IndexInText='461' ItemLength='517' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
									<Prefixes>
										<Braces Id='637856640183413661' IndexInText='461' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
											<RegularItems>
												<OpeningSquareBrace Name='[' Id='637856640183413664' IndexInText='461' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												<Literal Id='637856640183413748' IndexInText='462' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
													<NameExpressionItem Name='TestSetup' Id='637856640183413745' IndexInText='462' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												</Literal>
												<ClosingSquareBrace Name=']' Id='637856640183413777' IndexInText='471' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											</RegularItems>
											<Children>
												<Literal Id='637856640183413748' IndexInText='462' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
											</Children>
											<OtherProperties>
												<OpeningBraceInfo Name='[' Id='637856640183413664' IndexInText='461' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												<ClosingBraceInfo Name=']' Id='637856640183413777' IndexInText='471' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
												<Parameters ObjectId='35' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
													<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640183413748' IndexInText='462' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
												</Parameters>
											</OtherProperties>
										</Braces>
										<Braces Id='637856640183413803' IndexInText='478' ItemLength='50' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
											<RegularItems>
												<OpeningSquareBrace Name='[' Id='637856640183413810' IndexInText='478' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												<Braces Id='637856640183413913' IndexInText='479' ItemLength='48' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
													<RegularItems>
														<Literal Id='637856640183413889' IndexInText='479' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
															<NameExpressionItem Name='Attribute' Id='637856640183413885' IndexInText='479' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
														</Literal>
														<OpeningRoundBrace Name='(' Id='637856640183413916' IndexInText='488' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
														<ConstantText Id='637856640183414004' IndexInText='489' ItemLength='37' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
															<NameExpressionItem Name='"This is a demo of multiple prefixes"' Id='637856640183413999' IndexInText='489' ItemLength='37' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
														</ConstantText>
														<ClosingRoundBrace Name=')' Id='637856640183414033' IndexInText='526' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
													</RegularItems>
													<Children>
														<ConstantText Id='637856640183414004' IndexInText='489' ItemLength='37' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
													</Children>
													<OtherProperties>
														<NamedExpressionItem Id='637856640183413889' IndexInText='479' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
														<OpeningBraceInfo Name='(' Id='637856640183413916' IndexInText='488' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
														<ClosingBraceInfo Name=')' Id='637856640183414033' IndexInText='526' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
														<Parameters ObjectId='45' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
															<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640183414004' IndexInText='489' ItemLength='37' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
														</Parameters>
													</OtherProperties>
												</Braces>
												<ClosingSquareBrace Name=']' Id='637856640183414060' IndexInText='527' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											</RegularItems>
											<Children>
												<Braces Id='637856640183413913' IndexInText='479' ItemLength='48' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
											</Children>
											<OtherProperties>
												<OpeningBraceInfo Name='[' Id='637856640183413810' IndexInText='478' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												<ClosingBraceInfo Name=']' Id='637856640183414060' IndexInText='527' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
												<Parameters ObjectId='47' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
													<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640183413913' IndexInText='479' ItemLength='48' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
												</Parameters>
											</OtherProperties>
										</Braces>
										<Custom Id='637856640183498380' IndexInText='534' ItemLength='332' Interface='UniversalExpressionParser.ExpressionItems.Custom.IKeywordBasedCustomExpressionItem'>
											<RegularItems>
												<Keyword Name='::metadata' Id='637856640183414080' IndexInText='534' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
													<LanguageKeywordInfo ObjectId='50' Id='637781063212876967' Keyword='::metadata' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
												</Keyword>
												<CodeBlock Id='637856640183414261' IndexInText='545' ItemLength='321' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
													<RegularItems>
														<CodeBlockStartMarker Name='{' Id='637856640183414256' IndexInText='545' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
														<BinaryOperator Name=':' Priority='0' Id='637856640183414720' IndexInText='556' ItemLength='277' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
															<Operand1.Literal Id='637856640183414427' IndexInText='556' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
																<NameExpressionItem Name='Description' Id='637856640183414421' IndexInText='556' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
															</Operand1.Literal>
															<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637856640183414480' IndexInText='567' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
																<OperatorNameParts>
																	<Name Name=':' Id='637856640183414470' IndexInText='567' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
																</OperatorNameParts>
															</OperatorInfo>
															<Operand2.ConstantText Id='637856640183414667' IndexInText='569' ItemLength='264' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
																<NameExpressionItem Name='"Demo of custom expression item parsed to $line_break$                        UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IMetadataCustomExpressionItem$line_break$                        used in prefixes list of expression parsed from ''SetupMyTests()''"'
																  Id='637856640183414662' IndexInText='569' ItemLength='264' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
															</Operand2.ConstantText>
														</BinaryOperator>
														<ExpressionSeparator Name=';' Id='637856640183414764' IndexInText='833' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
														<BinaryOperator Name=':' Priority='0' Id='637856640183498297' IndexInText='844' ItemLength='15' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
															<Operand1.Literal Id='637856640183414853' IndexInText='844' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
																<NameExpressionItem Name='SomeMetadata' Id='637856640183414849' IndexInText='844' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
															</Operand1.Literal>
															<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637856640183414898' IndexInText='856' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
																<OperatorNameParts>
																	<Name Name=':' Id='637856640183414893' IndexInText='856' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
																</OperatorNameParts>
															</OperatorInfo>
															<Operand2.ConstantNumericValue Id='637856640183498099' IndexInText='858' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
																<NameExpressionItem Name='1' Id='637856640183498055' IndexInText='858' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
																<OtherProperties>
																	<SucceededNumericTypeDescriptor ObjectId='68' NumberTypeId='1581134183960' Interface='UniversalExpressionParser.NumericTypeDescriptor'>
																		<RegularExpressions ObjectId='69' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[System.String]'>
																			<System.String value='^\d+' />
																		</RegularExpressions>
																	</SucceededNumericTypeDescriptor>
																</OtherProperties>
															</Operand2.ConstantNumericValue>
														</BinaryOperator>
														<CodeBlockEndMarker Name='}' Id='637856640183498355' IndexInText='865' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
													</RegularItems>
													<Children>
														<BinaryOperator Name=':' Priority='0' Id='637856640183414720' IndexInText='556' ItemLength='277' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
														<BinaryOperator Name=':' Priority='0' Id='637856640183498297' IndexInText='844' ItemLength='15' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
													</Children>
													<OtherProperties>
														<CodeBlockStartMarker Name='{' Id='637856640183414256' IndexInText='545' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
														<CodeBlockEndMarker Name='}' Id='637856640183498355' IndexInText='865' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
													</OtherProperties>
												</CodeBlock>
											</RegularItems>
											<OtherProperties>
												<LastKeywordExpressionItem Name='::metadata' Id='637856640183414080' IndexInText='534' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'/>
												<ErrorsPositionDisplayValue value='534' type='System.Int32' />
											</OtherProperties>
										</Custom>
									</Prefixes>
									<AppliedKeywords>
										<Keyword Name='public' Id='637856640183498668' IndexInText='950' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
											<LanguageKeywordInfo ObjectId='23' Id='637783669980406419' Keyword='public' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
										</Keyword>
										<Keyword Name='static' Id='637856640183498684' IndexInText='957' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
											<LanguageKeywordInfo ObjectId='73' Id='637789181872233115' Keyword='static' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
										</Keyword>
									</AppliedKeywords>
									<RegularItems>
										<Literal Id='637856640183498859' IndexInText='964' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
											<NameExpressionItem Name='SetupMyTests' Id='637856640183498853' IndexInText='964' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</Literal>
										<OpeningRoundBrace Name='(' Id='637856640183498916' IndexInText='976' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										<ClosingRoundBrace Name=')' Id='637856640183498955' IndexInText='977' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</RegularItems>
									<OtherProperties>
										<NamedExpressionItem Id='637856640183498859' IndexInText='964' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
										<OpeningBraceInfo Name='(' Id='637856640183498916' IndexInText='976' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										<ClosingBraceInfo Name=')' Id='637856640183498955' IndexInText='977' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										<Parameters ObjectId='78' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'/>
									</OtherProperties>
								</Operand1.Braces>
								<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637856640183499022' IndexInText='979' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
									<OperatorNameParts>
										<Name Name=':' Id='637856640183499011' IndexInText='979' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</OperatorNameParts>
								</OperatorInfo>
								<Operand2.Literal Id='637856640183499179' IndexInText='981' ItemLength='54' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
									<NameExpressionItem Name='void' Id='637856640183499174' IndexInText='981' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Postfixes>
										<CodeBlock Id='637856640183499223' IndexInText='991' ItemLength='44' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
											<RegularItems>
												<CodeBlockStartMarker Name='{' Id='637856640183499217' IndexInText='991' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												<CodeBlockEndMarker Name='}' Id='637856640183499262' IndexInText='1034' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											</RegularItems>
											<OtherProperties>
												<CodeBlockStartMarker Name='{' Id='637856640183499217' IndexInText='991' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												<CodeBlockEndMarker Name='}' Id='637856640183499262' IndexInText='1034' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											</OtherProperties>
										</CodeBlock>
									</Postfixes>
								</Operand2.Literal>
							</BinaryOperator>
							<CodeBlockEndMarker Name='}' Id='637856640183499364' IndexInText='1037' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<BinaryOperator Name=':' Priority='0' Id='637856640183499313' IndexInText='461' ItemLength='574' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
						</Children>
						<OtherProperties>
							<CodeBlockStartMarker Name='{' Id='637856640183413609' IndexInText='218' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637856640183499364' IndexInText='1037' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OtherProperties>
					</CodeBlock>
				</Postfixes>
			</Literal>
		</RegularItems>
		<Children>
			<Literal Id='637856640183413565' IndexInText='95' ItemLength='943' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
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
	<MainExpressionItem Id='637856640183412494' IndexInText='95' ItemLength='943' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'/>
</UniversalExpressionParser.ExpressionItems.IRootExpressionItemSeries>
```
</details>

- **NOTE:** The list of prefixes can include both types of prefixes at the same time (i.e., braces and custom expression items).

- Below is an example of using prefixes with different expression item types:

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
	<ExpressionItemSeries Id='637856640183528811' IndexInText='37' ItemLength='1783' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'>
		<RegularItems>
			<Literal Id='637856640183529987' IndexInText='37' ItemLength='33' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
				<NameExpressionItem Name='x' Id='637856640183529982' IndexInText='69' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				<Prefixes>
					<Braces Id='637856640183529212' IndexInText='37' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<OpeningSquareBrace Name='[' Id='637856640183529218' IndexInText='37' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Literal Id='637856640183529348' IndexInText='38' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='NotNull' Id='637856640183529342' IndexInText='38' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<ClosingSquareBrace Name=']' Id='637856640183529401' IndexInText='45' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<Literal Id='637856640183529348' IndexInText='38' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
						</Children>
						<OtherProperties>
							<OpeningBraceInfo Name='[' Id='637856640183529218' IndexInText='37' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=']' Id='637856640183529401' IndexInText='45' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
							<Parameters ObjectId='9' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640183529348' IndexInText='38' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Braces>
					<Braces Id='637856640183529441' IndexInText='47' ItemLength='21' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<OpeningSquareBrace Name='[' Id='637856640183529458' IndexInText='47' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Braces Id='637856640183529573' IndexInText='48' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
								<RegularItems>
									<Literal Id='637856640183529542' IndexInText='48' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='Attribute' Id='637856640183529537' IndexInText='48' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<OpeningRoundBrace Name='(' Id='637856640183529576' IndexInText='57' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ConstantText Id='637856640183529683' IndexInText='58' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='"Marker"' Id='637856640183529677' IndexInText='58' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</ConstantText>
									<ClosingRoundBrace Name=')' Id='637856640183529714' IndexInText='66' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Children>
									<ConstantText Id='637856640183529683' IndexInText='58' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								</Children>
								<OtherProperties>
									<NamedExpressionItem Id='637856640183529542' IndexInText='48' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<OpeningBraceInfo Name='(' Id='637856640183529576' IndexInText='57' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ClosingBraceInfo Name=')' Id='637856640183529714' IndexInText='66' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Parameters ObjectId='19' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640183529683' IndexInText='58' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</Parameters>
								</OtherProperties>
							</Braces>
							<ClosingSquareBrace Name=']' Id='637856640183529902' IndexInText='67' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<Braces Id='637856640183529573' IndexInText='48' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
						</Children>
						<OtherProperties>
							<OpeningBraceInfo Name='[' Id='637856640183529458' IndexInText='47' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=']' Id='637856640183529902' IndexInText='67' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
							<Parameters ObjectId='21' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640183529573' IndexInText='48' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Braces>
				</Prefixes>
			</Literal>
			<ExpressionSeparator Name=';' Id='637856640183530207' IndexInText='70' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<Braces Id='637856640183531281' IndexInText='227' ItemLength='38' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
				<Prefixes>
					<Braces Id='637856640183530244' IndexInText='227' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<OpeningSquareBrace Name='[' Id='637856640183530247' IndexInText='227' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Literal Id='637856640183530336' IndexInText='228' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='NotNull' Id='637856640183530332' IndexInText='228' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<ClosingSquareBrace Name=']' Id='637856640183530365' IndexInText='235' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<Literal Id='637856640183530336' IndexInText='228' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
						</Children>
						<OtherProperties>
							<OpeningBraceInfo Name='[' Id='637856640183530247' IndexInText='227' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=']' Id='637856640183530365' IndexInText='235' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
							<Parameters ObjectId='29' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640183530336' IndexInText='228' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Braces>
					<Braces Id='637856640183530392' IndexInText='237' ItemLength='21' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<OpeningSquareBrace Name='[' Id='637856640183530397' IndexInText='237' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Braces Id='637856640183530894' IndexInText='238' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
								<RegularItems>
									<Literal Id='637856640183530577' IndexInText='238' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='Attribute' Id='637856640183530477' IndexInText='238' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<OpeningRoundBrace Name='(' Id='637856640183530907' IndexInText='247' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ConstantText Id='637856640183531045' IndexInText='248' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='"Marker"' Id='637856640183531038' IndexInText='248' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</ConstantText>
									<ClosingRoundBrace Name=')' Id='637856640183531085' IndexInText='256' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Children>
									<ConstantText Id='637856640183531045' IndexInText='248' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								</Children>
								<OtherProperties>
									<NamedExpressionItem Id='637856640183530577' IndexInText='238' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<OpeningBraceInfo Name='(' Id='637856640183530907' IndexInText='247' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ClosingBraceInfo Name=')' Id='637856640183531085' IndexInText='256' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Parameters ObjectId='39' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640183531045' IndexInText='248' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</Parameters>
								</OtherProperties>
							</Braces>
							<ClosingSquareBrace Name=']' Id='637856640183531118' IndexInText='257' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<Braces Id='637856640183530894' IndexInText='238' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
						</Children>
						<OtherProperties>
							<OpeningBraceInfo Name='[' Id='637856640183530397' IndexInText='237' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=']' Id='637856640183531118' IndexInText='257' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
							<Parameters ObjectId='41' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640183530894' IndexInText='238' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Braces>
				</Prefixes>
				<RegularItems>
					<Literal Id='637856640183531222' IndexInText='259' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='f1' Id='637856640183531218' IndexInText='259' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<OpeningRoundBrace Name='(' Id='637856640183531285' IndexInText='261' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Literal Id='637856640183531369' IndexInText='262' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='x1' Id='637856640183531364' IndexInText='262' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<ClosingRoundBrace Name=')' Id='637856640183531398' IndexInText='264' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Children>
					<Literal Id='637856640183531369' IndexInText='262' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
				</Children>
				<OtherProperties>
					<NamedExpressionItem Id='637856640183531222' IndexInText='259' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					<OpeningBraceInfo Name='(' Id='637856640183531285' IndexInText='261' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ClosingBraceInfo Name=')' Id='637856640183531398' IndexInText='264' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Parameters ObjectId='48' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640183531369' IndexInText='262' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					</Parameters>
				</OtherProperties>
			</Braces>
			<ExpressionSeparator Name=';' Id='637856640183531426' IndexInText='265' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<Braces Id='637856640183531905' IndexInText='422' ItemLength='36' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
				<Prefixes>
					<Braces Id='637856640183531477' IndexInText='422' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<OpeningSquareBrace Name='[' Id='637856640183531480' IndexInText='422' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Literal Id='637856640183531561' IndexInText='423' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='NotNull' Id='637856640183531552' IndexInText='423' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<ClosingSquareBrace Name=']' Id='637856640183531588' IndexInText='430' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<Literal Id='637856640183531561' IndexInText='423' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
						</Children>
						<OtherProperties>
							<OpeningBraceInfo Name='[' Id='637856640183531480' IndexInText='422' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=']' Id='637856640183531588' IndexInText='430' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
							<Parameters ObjectId='56' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640183531561' IndexInText='423' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Braces>
					<Braces Id='637856640183531614' IndexInText='432' ItemLength='21' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<OpeningSquareBrace Name='[' Id='637856640183531622' IndexInText='432' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Braces Id='637856640183531727' IndexInText='433' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
								<RegularItems>
									<Literal Id='637856640183531702' IndexInText='433' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='Attribute' Id='637856640183531698' IndexInText='433' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<OpeningRoundBrace Name='(' Id='637856640183531730' IndexInText='442' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ConstantText Id='637856640183531820' IndexInText='443' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='"Marker"' Id='637856640183531814' IndexInText='443' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</ConstantText>
									<ClosingRoundBrace Name=')' Id='637856640183531848' IndexInText='451' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Children>
									<ConstantText Id='637856640183531820' IndexInText='443' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								</Children>
								<OtherProperties>
									<NamedExpressionItem Id='637856640183531702' IndexInText='433' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<OpeningBraceInfo Name='(' Id='637856640183531730' IndexInText='442' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ClosingBraceInfo Name=')' Id='637856640183531848' IndexInText='451' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Parameters ObjectId='66' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640183531820' IndexInText='443' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</Parameters>
								</OtherProperties>
							</Braces>
							<ClosingSquareBrace Name=']' Id='637856640183531874' IndexInText='452' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<Braces Id='637856640183531727' IndexInText='433' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
						</Children>
						<OtherProperties>
							<OpeningBraceInfo Name='[' Id='637856640183531622' IndexInText='432' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=']' Id='637856640183531874' IndexInText='452' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
							<Parameters ObjectId='68' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640183531727' IndexInText='433' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Braces>
				</Prefixes>
				<RegularItems>
					<OpeningRoundBrace Name='(' Id='637856640183531910' IndexInText='454' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Literal Id='637856640183531985' IndexInText='455' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='x1' Id='637856640183531981' IndexInText='455' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<ClosingRoundBrace Name=')' Id='637856640183532011' IndexInText='457' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Children>
					<Literal Id='637856640183531985' IndexInText='455' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
				</Children>
				<OtherProperties>
					<OpeningBraceInfo Name='(' Id='637856640183531910' IndexInText='454' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ClosingBraceInfo Name=')' Id='637856640183532011' IndexInText='457' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
					<Parameters ObjectId='73' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640183531985' IndexInText='455' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					</Parameters>
				</OtherProperties>
			</Braces>
			<ExpressionSeparator Name=';' Id='637856640183532041' IndexInText='458' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<Braces Id='637856640183532571' IndexInText='622' ItemLength='38' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
				<Prefixes>
					<Braces Id='637856640183532077' IndexInText='622' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<OpeningSquareBrace Name='[' Id='637856640183532080' IndexInText='622' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Literal Id='637856640183532160' IndexInText='623' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='NotNull' Id='637856640183532156' IndexInText='623' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<ClosingSquareBrace Name=']' Id='637856640183532188' IndexInText='630' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<Literal Id='637856640183532160' IndexInText='623' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
						</Children>
						<OtherProperties>
							<OpeningBraceInfo Name='[' Id='637856640183532080' IndexInText='622' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=']' Id='637856640183532188' IndexInText='630' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
							<Parameters ObjectId='81' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640183532160' IndexInText='623' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Braces>
					<Braces Id='637856640183532212' IndexInText='632' ItemLength='21' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<OpeningSquareBrace Name='[' Id='637856640183532217' IndexInText='632' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Braces Id='637856640183532322' IndexInText='633' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
								<RegularItems>
									<Literal Id='637856640183532296' IndexInText='633' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='Attribute' Id='637856640183532292' IndexInText='633' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<OpeningRoundBrace Name='(' Id='637856640183532324' IndexInText='642' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ConstantText Id='637856640183532410' IndexInText='643' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='"Marker"' Id='637856640183532405' IndexInText='643' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</ConstantText>
									<ClosingRoundBrace Name=')' Id='637856640183532437' IndexInText='651' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Children>
									<ConstantText Id='637856640183532410' IndexInText='643' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								</Children>
								<OtherProperties>
									<NamedExpressionItem Id='637856640183532296' IndexInText='633' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<OpeningBraceInfo Name='(' Id='637856640183532324' IndexInText='642' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ClosingBraceInfo Name=')' Id='637856640183532437' IndexInText='651' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Parameters ObjectId='91' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640183532410' IndexInText='643' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</Parameters>
								</OtherProperties>
							</Braces>
							<ClosingSquareBrace Name=']' Id='637856640183532463' IndexInText='652' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<Braces Id='637856640183532322' IndexInText='633' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
						</Children>
						<OtherProperties>
							<OpeningBraceInfo Name='[' Id='637856640183532217' IndexInText='632' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=']' Id='637856640183532463' IndexInText='652' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
							<Parameters ObjectId='93' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640183532322' IndexInText='633' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Braces>
				</Prefixes>
				<RegularItems>
					<Literal Id='637856640183532544' IndexInText='654' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='m1' Id='637856640183532540' IndexInText='654' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<OpeningSquareBrace Name='[' Id='637856640183532576' IndexInText='656' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Literal Id='637856640183532656' IndexInText='657' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='x1' Id='637856640183532652' IndexInText='657' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<ClosingSquareBrace Name=']' Id='637856640183532682' IndexInText='659' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Children>
					<Literal Id='637856640183532656' IndexInText='657' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
				</Children>
				<OtherProperties>
					<NamedExpressionItem Id='637856640183532544' IndexInText='654' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					<OpeningBraceInfo Name='[' Id='637856640183532576' IndexInText='656' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ClosingBraceInfo Name=']' Id='637856640183532682' IndexInText='659' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Parameters ObjectId='100' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640183532656' IndexInText='657' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					</Parameters>
				</OtherProperties>
			</Braces>
			<ExpressionSeparator Name=';' Id='637856640183532712' IndexInText='660' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<Braces Id='637856640183533193' IndexInText='819' ItemLength='36' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
				<Prefixes>
					<Braces Id='637856640183532746' IndexInText='819' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<OpeningSquareBrace Name='[' Id='637856640183532749' IndexInText='819' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Literal Id='637856640183532833' IndexInText='820' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='NotNull' Id='637856640183532829' IndexInText='820' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<ClosingSquareBrace Name=']' Id='637856640183532864' IndexInText='827' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<Literal Id='637856640183532833' IndexInText='820' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
						</Children>
						<OtherProperties>
							<OpeningBraceInfo Name='[' Id='637856640183532749' IndexInText='819' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=']' Id='637856640183532864' IndexInText='827' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
							<Parameters ObjectId='108' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640183532833' IndexInText='820' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Braces>
					<Braces Id='637856640183532887' IndexInText='829' ItemLength='21' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<OpeningSquareBrace Name='[' Id='637856640183532891' IndexInText='829' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Braces Id='637856640183532994' IndexInText='830' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
								<RegularItems>
									<Literal Id='637856640183532971' IndexInText='830' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='Attribute' Id='637856640183532967' IndexInText='830' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<OpeningRoundBrace Name='(' Id='637856640183532997' IndexInText='839' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ConstantText Id='637856640183533081' IndexInText='840' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='"Marker"' Id='637856640183533076' IndexInText='840' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</ConstantText>
									<ClosingRoundBrace Name=')' Id='637856640183533109' IndexInText='848' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Children>
									<ConstantText Id='637856640183533081' IndexInText='840' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								</Children>
								<OtherProperties>
									<NamedExpressionItem Id='637856640183532971' IndexInText='830' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<OpeningBraceInfo Name='(' Id='637856640183532997' IndexInText='839' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ClosingBraceInfo Name=')' Id='637856640183533109' IndexInText='848' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Parameters ObjectId='118' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640183533081' IndexInText='840' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</Parameters>
								</OtherProperties>
							</Braces>
							<ClosingSquareBrace Name=']' Id='637856640183533161' IndexInText='849' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<Braces Id='637856640183532994' IndexInText='830' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
						</Children>
						<OtherProperties>
							<OpeningBraceInfo Name='[' Id='637856640183532891' IndexInText='829' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=']' Id='637856640183533161' IndexInText='849' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
							<Parameters ObjectId='120' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640183532994' IndexInText='830' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Braces>
				</Prefixes>
				<RegularItems>
					<OpeningSquareBrace Name='[' Id='637856640183533198' IndexInText='851' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Literal Id='637856640183533272' IndexInText='852' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='x1' Id='637856640183533268' IndexInText='852' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<ClosingSquareBrace Name=']' Id='637856640183533304' IndexInText='854' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Children>
					<Literal Id='637856640183533272' IndexInText='852' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
				</Children>
				<OtherProperties>
					<OpeningBraceInfo Name='[' Id='637856640183533198' IndexInText='851' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ClosingBraceInfo Name=']' Id='637856640183533304' IndexInText='854' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
					<Parameters ObjectId='125' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640183533272' IndexInText='852' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					</Parameters>
				</OtherProperties>
			</Braces>
			<ExpressionSeparator Name=';' Id='637856640183533328' IndexInText='855' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<CodeBlock Id='637856640183533895' IndexInText='1174' ItemLength='28' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
				<Prefixes>
					<Custom Id='637856640183533750' IndexInText='1174' ItemLength='14' Interface='UniversalExpressionParser.ExpressionItems.Custom.IKeywordBasedCustomExpressionItem'>
						<RegularItems>
							<Keyword Name='::types' Id='637856640183533367' IndexInText='1174' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
								<LanguageKeywordInfo ObjectId='130' Id='637781062811583137' Keyword='::types' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
							</Keyword>
							<Braces Id='637856640183533434' IndexInText='1181' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
								<RegularItems>
									<OpeningSquareBrace Name='[' Id='637856640183533438' IndexInText='1181' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637856640183533603' IndexInText='1182' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='T1' Id='637856640183533598' IndexInText='1182' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<Comma Name=',' Id='637856640183533635' IndexInText='1184' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637856640183533714' IndexInText='1185' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='T2' Id='637856640183533710' IndexInText='1185' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<ClosingSquareBrace Name=']' Id='637856640183533741' IndexInText='1187' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Children>
									<Literal Id='637856640183533603' IndexInText='1182' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<Literal Id='637856640183533714' IndexInText='1185' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								</Children>
								<OtherProperties>
									<OpeningBraceInfo Name='[' Id='637856640183533438' IndexInText='1181' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ClosingBraceInfo Name=']' Id='637856640183533741' IndexInText='1187' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
									<Parameters ObjectId='139' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640183533603' IndexInText='1182' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640183533714' IndexInText='1185' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</Parameters>
								</OtherProperties>
							</Braces>
						</RegularItems>
						<OtherProperties>
							<LastKeywordExpressionItem Name='::types' Id='637856640183533367' IndexInText='1174' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'/>
							<ErrorsPositionDisplayValue value='1174' type='System.Int32' />
						</OtherProperties>
					</Custom>
				</Prefixes>
				<RegularItems>
					<CodeBlockStartMarker Name='{' Id='637856640183533889' IndexInText='1189' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<BinaryOperator Name='=' Priority='2000' Id='637856640183624909' IndexInText='1190' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.Literal Id='637856640183534005' IndexInText='1190' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='i' Id='637856640183534001' IndexInText='1194' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<AppliedKeywords>
								<Keyword Name='var' Id='637856640183533920' IndexInText='1190' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
									<LanguageKeywordInfo ObjectId='145' Id='637781064051574641' Keyword='var' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
								</Keyword>
							</AppliedKeywords>
						</Operand1.Literal>
						<OperatorInfo OperatorType='BinaryOperator' Name='=' Priority='2000' Id='637856640183534109' IndexInText='1196' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<Name Name='=' Id='637856640183534100' IndexInText='1196' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OperatorNameParts>
						</OperatorInfo>
						<Operand2.ConstantNumericValue Id='637856640183624688' IndexInText='1198' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
							<NameExpressionItem Name='12' Id='637856640183624630' IndexInText='1198' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<OtherProperties>
								<SucceededNumericTypeDescriptor ObjectId='150' NumberTypeId='1581134183960' Interface='UniversalExpressionParser.NumericTypeDescriptor'>
									<RegularExpressions ObjectId='151' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[System.String]'>
										<System.String value='^\d+' />
									</RegularExpressions>
								</SucceededNumericTypeDescriptor>
							</OtherProperties>
						</Operand2.ConstantNumericValue>
					</BinaryOperator>
					<ExpressionSeparator Name=';' Id='637856640183624987' IndexInText='1200' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<CodeBlockEndMarker Name='}' Id='637856640183625038' IndexInText='1201' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Children>
					<BinaryOperator Name='=' Priority='2000' Id='637856640183624909' IndexInText='1190' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
				</Children>
				<OtherProperties>
					<CodeBlockStartMarker Name='{' Id='637856640183533889' IndexInText='1189' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<CodeBlockEndMarker Name='}' Id='637856640183625038' IndexInText='1201' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</OtherProperties>
			</CodeBlock>
			<ExpressionSeparator Name=';' Id='637856640183625087' IndexInText='1202' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<Custom Id='637856640183625617' IndexInText='1395' ItemLength='32' Interface='UniversalExpressionParser.ExpressionItems.Custom.IKeywordBasedCustomExpressionItem'>
				<Prefixes>
					<Braces Id='637856640183625154' IndexInText='1395' ItemLength='21' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<OpeningSquareBrace Name='[' Id='637856640183625157' IndexInText='1395' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Braces Id='637856640183625327' IndexInText='1396' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
								<RegularItems>
									<Literal Id='637856640183625292' IndexInText='1396' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='Attribute' Id='637856640183625288' IndexInText='1396' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<OpeningRoundBrace Name='(' Id='637856640183625330' IndexInText='1405' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ConstantText Id='637856640183625438' IndexInText='1406' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='"Marker"' Id='637856640183625431' IndexInText='1406' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</ConstantText>
									<ClosingRoundBrace Name=')' Id='637856640183625478' IndexInText='1414' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Children>
									<ConstantText Id='637856640183625438' IndexInText='1406' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								</Children>
								<OtherProperties>
									<NamedExpressionItem Id='637856640183625292' IndexInText='1396' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<OpeningBraceInfo Name='(' Id='637856640183625330' IndexInText='1405' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ClosingBraceInfo Name=')' Id='637856640183625478' IndexInText='1414' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Parameters ObjectId='165' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640183625438' IndexInText='1406' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</Parameters>
								</OtherProperties>
							</Braces>
							<ClosingSquareBrace Name=']' Id='637856640183625509' IndexInText='1415' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<Braces Id='637856640183625327' IndexInText='1396' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
						</Children>
						<OtherProperties>
							<OpeningBraceInfo Name='[' Id='637856640183625157' IndexInText='1395' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=']' Id='637856640183625509' IndexInText='1415' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
							<Parameters ObjectId='167' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640183625327' IndexInText='1396' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Braces>
				</Prefixes>
				<RegularItems>
					<Keyword Name='::pragma' Id='637856640183625531' IndexInText='1417' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
						<LanguageKeywordInfo ObjectId='169' Id='637781063127646487' Keyword='::pragma' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
					</Keyword>
					<Name Name='x' Id='637856640183625611' IndexInText='1426' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<OtherProperties>
					<LastKeywordExpressionItem Name='::pragma' Id='637856640183625531' IndexInText='1417' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'/>
					<ErrorsPositionDisplayValue value='1417' type='System.Int32' />
				</OtherProperties>
			</Custom>
			<ExpressionSeparator Name=';' Id='637856640183625801' IndexInText='1427' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<ConstantText Id='637856640183626195' IndexInText='1587' ItemLength='33' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
				<NameExpressionItem Name='"Some text"' Id='637856640183626190' IndexInText='1609' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				<Prefixes>
					<Braces Id='637856640183625843' IndexInText='1587' ItemLength='21' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<OpeningSquareBrace Name='[' Id='637856640183625846' IndexInText='1587' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Braces Id='637856640183625959' IndexInText='1588' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
								<RegularItems>
									<Literal Id='637856640183625932' IndexInText='1588' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='Attribute' Id='637856640183625924' IndexInText='1588' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<OpeningRoundBrace Name='(' Id='637856640183625962' IndexInText='1597' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ConstantText Id='637856640183626052' IndexInText='1598' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='"Marker"' Id='637856640183626046' IndexInText='1598' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</ConstantText>
									<ClosingRoundBrace Name=')' Id='637856640183626083' IndexInText='1606' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Children>
									<ConstantText Id='637856640183626052' IndexInText='1598' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								</Children>
								<OtherProperties>
									<NamedExpressionItem Id='637856640183625932' IndexInText='1588' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<OpeningBraceInfo Name='(' Id='637856640183625962' IndexInText='1597' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ClosingBraceInfo Name=')' Id='637856640183626083' IndexInText='1606' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Parameters ObjectId='183' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640183626052' IndexInText='1598' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</Parameters>
								</OtherProperties>
							</Braces>
							<ClosingSquareBrace Name=']' Id='637856640183626111' IndexInText='1607' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<Braces Id='637856640183625959' IndexInText='1588' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
						</Children>
						<OtherProperties>
							<OpeningBraceInfo Name='[' Id='637856640183625846' IndexInText='1587' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=']' Id='637856640183626111' IndexInText='1607' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
							<Parameters ObjectId='185' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640183625959' IndexInText='1588' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Braces>
				</Prefixes>
			</ConstantText>
			<ExpressionSeparator Name=';' Id='637856640183626233' IndexInText='1620' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<ConstantNumericValue Id='637856640183682059' IndexInText='1789' ItemLength='30' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
				<NameExpressionItem Name='0.5e-3.4' Id='637856640183681989' IndexInText='1811' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				<Prefixes>
					<Braces Id='637856640183626271' IndexInText='1789' ItemLength='21' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<OpeningSquareBrace Name='[' Id='637856640183626275' IndexInText='1789' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Braces Id='637856640183626385' IndexInText='1790' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
								<RegularItems>
									<Literal Id='637856640183626356' IndexInText='1790' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='Attribute' Id='637856640183626352' IndexInText='1790' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<OpeningRoundBrace Name='(' Id='637856640183626388' IndexInText='1799' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ConstantText Id='637856640183626475' IndexInText='1800' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='"Marker"' Id='637856640183626469' IndexInText='1800' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</ConstantText>
									<ClosingRoundBrace Name=')' Id='637856640183626503' IndexInText='1808' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Children>
									<ConstantText Id='637856640183626475' IndexInText='1800' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								</Children>
								<OtherProperties>
									<NamedExpressionItem Id='637856640183626356' IndexInText='1790' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<OpeningBraceInfo Name='(' Id='637856640183626388' IndexInText='1799' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ClosingBraceInfo Name=')' Id='637856640183626503' IndexInText='1808' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Parameters ObjectId='198' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640183626475' IndexInText='1800' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</Parameters>
								</OtherProperties>
							</Braces>
							<ClosingSquareBrace Name=']' Id='637856640183626529' IndexInText='1809' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<Braces Id='637856640183626385' IndexInText='1790' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
						</Children>
						<OtherProperties>
							<OpeningBraceInfo Name='[' Id='637856640183626275' IndexInText='1789' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=']' Id='637856640183626529' IndexInText='1809' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
							<Parameters ObjectId='200' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640183626385' IndexInText='1790' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
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
			<ExpressionSeparator Name=';' Id='637856640183682252' IndexInText='1819' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
		</RegularItems>
		<Children>
			<Literal Id='637856640183529987' IndexInText='37' ItemLength='33' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
			<Braces Id='637856640183531281' IndexInText='227' ItemLength='38' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
			<Braces Id='637856640183531905' IndexInText='422' ItemLength='36' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
			<Braces Id='637856640183532571' IndexInText='622' ItemLength='38' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
			<Braces Id='637856640183533193' IndexInText='819' ItemLength='36' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
			<CodeBlock Id='637856640183533895' IndexInText='1174' ItemLength='28' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'/>
			<Custom Id='637856640183625617' IndexInText='1395' ItemLength='32' Interface='UniversalExpressionParser.ExpressionItems.Custom.IKeywordBasedCustomExpressionItem'/>
			<ConstantText Id='637856640183626195' IndexInText='1587' ItemLength='33' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
			<ConstantNumericValue Id='637856640183682059' IndexInText='1789' ItemLength='30' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'/>
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
	<MainExpressionItem Id='637856640183528811' IndexInText='37' ItemLength='1783' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'/>
</UniversalExpressionParser.ExpressionItems.IRootExpressionItemSeries>
```
</details>

# Postfixes

Postfixes are one or more expression items that are placed after some other expression item, and are added to the list in property **Postfixes** in interface **UniversalExpressionParser.ExpressionItems.IComplexExpressionItem** for the expression item that the postfixes are placed after.

Currently **Universal Expression Parser** supports two types of postfixes:

1) Code block expression items that are parsed to **UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem** that succeed another expression item.

- **NOTE:** The following are expression types that can have postfixes: Literals, such a x1 or Dog, braces expression items, such as f(x1), (y), m1[x1], [x2], or custom expression items for which property **CustomExpressionItemCategory** in interface **UniversalExpressionParser.ExpressionItems.Custom.ICustomExpressionItem** is equal to **UniversalExpressionParser.ExpressionItems.Custom.CustomExpressionItemCategory.Regular**. 
  
- In the example below the code block expression item of type **UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem** parsed from expression that starts with '**{**' and ends with '**}**'" will be added to the list **Postfixes** in **UniversalExpressionParser.ExpressionItems.IComplexExpressionItem** for the literal expression item parsed from expression **Dog**.

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
	<ExpressionItemSeries Id='637856640183310054' IndexInText='0' ItemLength='117' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'>
		<RegularItems>
			<Literal Id='637856640183310408' IndexInText='0' ItemLength='117' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
				<NameExpressionItem Name='Dog' Id='637856640183310403' IndexInText='13' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				<AppliedKeywords>
					<Keyword Name='public' Id='637856640183310262' IndexInText='0' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
						<LanguageKeywordInfo ObjectId='5' Id='637783669980406419' Keyword='public' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
					</Keyword>
					<Keyword Name='class' Id='637856640183310280' IndexInText='7' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
						<LanguageKeywordInfo ObjectId='7' Id='637783670388745402' Keyword='class' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
					</Keyword>
				</AppliedKeywords>
				<Postfixes>
					<CodeBlock Id='637856640183310468' IndexInText='18' ItemLength='99' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
						<RegularItems>
							<CodeBlockStartMarker Name='{' Id='637856640183310463' IndexInText='18' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637856640183310526' IndexInText='116' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<OtherProperties>
							<CodeBlockStartMarker Name='{' Id='637856640183310463' IndexInText='18' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637856640183310526' IndexInText='116' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OtherProperties>
					</CodeBlock>
				</Postfixes>
			</Literal>
		</RegularItems>
		<Children>
			<Literal Id='637856640183310408' IndexInText='0' ItemLength='117' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
		</Children>
	</ExpressionItemSeries>
	<ParseErrorData ObjectId='11' HasCriticalErrors='False' Interface='UniversalExpressionParser.IParseErrorData'>
		<AllCodeItemErrors ObjectId='12' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICodeItemParseErrorData]'/>
	</ParseErrorData>
	<SortedCommentedOutCodeInfos ObjectId='13' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICommentedOutCodeInfo]'>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='14' IsLineComment='True' IndexInText='24' ItemLength='90'/>
	</SortedCommentedOutCodeInfos>
	<MainExpressionItem Id='637856640183310054' IndexInText='0' ItemLength='117' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'/>
</UniversalExpressionParser.ExpressionItems.IRootExpressionItemSeries>
```
</details>

2) One or more expressions that are parsed to custom expression items of type **UniversalExpressionParser.ExpressionItems.Custom.ICustomExpressionItem** with property **CustomExpressionItemCategory** equal to **UniversalExpressionParser.ExpressionItems.Custom.CustomExpressionItemCategory.Postfix** that succeed another expression item.

- In the example below the two custom expression items of type **UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IWhereCustomExpressionItem** parsed from expressions that start with "where" and end with "whereend"" as well as the code block will be added as postfixes to literal expression item parsed from "Dog".

- **NOTE:** For more details on custom expression items see section **Custom Expression Item Parsers**.

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
	<ExpressionItemSeries Id='637856640183331419' IndexInText='0' ItemLength='416' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'>
		<RegularItems>
			<Braces Id='637856640183332379' IndexInText='0' ItemLength='416' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
				<Prefixes>
					<Custom Id='637856640183332215' IndexInText='0' ItemLength='18' Interface='UniversalExpressionParser.ExpressionItems.Custom.IKeywordBasedCustomExpressionItem'>
						<RegularItems>
							<Keyword Name='::types' Id='637856640183331633' IndexInText='0' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
								<LanguageKeywordInfo ObjectId='5' Id='637781062811583137' Keyword='::types' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
							</Keyword>
							<Braces Id='637856640183331736' IndexInText='7' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
								<RegularItems>
									<OpeningSquareBrace Name='[' Id='637856640183331740' IndexInText='7' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637856640183331918' IndexInText='8' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='T1' Id='637856640183331912' IndexInText='8' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<Comma Name=',' Id='637856640183331976' IndexInText='10' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637856640183332060' IndexInText='11' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='T2' Id='637856640183332049' IndexInText='11' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<Comma Name=',' Id='637856640183332087' IndexInText='13' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637856640183332164' IndexInText='15' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='T3' Id='637856640183332160' IndexInText='15' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<ClosingSquareBrace Name=']' Id='637856640183332200' IndexInText='17' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Children>
									<Literal Id='637856640183331918' IndexInText='8' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<Literal Id='637856640183332060' IndexInText='11' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<Literal Id='637856640183332164' IndexInText='15' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								</Children>
								<OtherProperties>
									<OpeningBraceInfo Name='[' Id='637856640183331740' IndexInText='7' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ClosingBraceInfo Name=']' Id='637856640183332200' IndexInText='17' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
									<Parameters ObjectId='17' Count='3' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640183331918' IndexInText='8' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640183332060' IndexInText='11' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640183332164' IndexInText='15' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</Parameters>
								</OtherProperties>
							</Braces>
						</RegularItems>
						<OtherProperties>
							<LastKeywordExpressionItem Name='::types' Id='637856640183331633' IndexInText='0' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'/>
							<ErrorsPositionDisplayValue value='0' type='System.Int32' />
						</OtherProperties>
					</Custom>
				</Prefixes>
				<RegularItems>
					<Literal Id='637856640183332331' IndexInText='19' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='F1' Id='637856640183332325' IndexInText='19' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<OpeningRoundBrace Name='(' Id='637856640183332383' IndexInText='21' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<BinaryOperator Name=':' Priority='0' Id='637856640183332735' IndexInText='22' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.Literal Id='637856640183332468' IndexInText='22' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='x' Id='637856640183332463' IndexInText='22' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand1.Literal>
						<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637856640183332532' IndexInText='23' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<Name Name=':' Id='637856640183332515' IndexInText='23' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OperatorNameParts>
						</OperatorInfo>
						<Operand2.Literal Id='637856640183332685' IndexInText='24' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='T1' Id='637856640183332680' IndexInText='24' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand2.Literal>
					</BinaryOperator>
					<Comma Name=',' Id='637856640183332789' IndexInText='26' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<BinaryOperator Name=':' Priority='0' Id='637856640183333103' IndexInText='28' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.Literal Id='637856640183332878' IndexInText='28' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='y' Id='637856640183332874' IndexInText='28' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand1.Literal>
						<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637856640183332932' IndexInText='29' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<Name Name=':' Id='637856640183332926' IndexInText='29' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OperatorNameParts>
						</OperatorInfo>
						<Operand2.Literal Id='637856640183333070' IndexInText='30' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='T2' Id='637856640183333065' IndexInText='30' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand2.Literal>
					</BinaryOperator>
					<Comma Name=',' Id='637856640183333119' IndexInText='32' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<BinaryOperator Name=':' Priority='0' Id='637856640183333441' IndexInText='34' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.Literal Id='637856640183333205' IndexInText='34' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='z' Id='637856640183333202' IndexInText='34' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand1.Literal>
						<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637856640183333250' IndexInText='35' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<Name Name=':' Id='637856640183333245' IndexInText='35' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OperatorNameParts>
						</OperatorInfo>
						<Operand2.Literal Id='637856640183333402' IndexInText='37' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='T3' Id='637856640183333396' IndexInText='37' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand2.Literal>
					</BinaryOperator>
					<ClosingRoundBrace Name=')' Id='637856640183333455' IndexInText='39' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Postfixes>
					<Custom Id='637856640183333564' IndexInText='143' ItemLength='37' Interface='UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IWhereCustomExpressionItem'>
						<RegularItems>
							<CustomExpressionItemPart Id='637856640183333570' IndexInText='143' ItemLength='12' Interface='UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem'>
								<RegularItems>
									<Keyword Name='where' Id='637856640183333516' IndexInText='143' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
										<LanguageKeywordInfo ObjectId='48' Id='637781062905654923' Keyword='where' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
									</Keyword>
									<Name Name='T1' Id='637856640183333612' IndexInText='149' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Name Name=':' Id='637856640183333620' IndexInText='151' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637856640183333636' IndexInText='152' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='int' Id='637856640183333628' IndexInText='152' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
								</RegularItems>
								<OtherProperties>
									<WhereKeyword Name='where' Id='637856640183333516' IndexInText='143' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'/>
									<TypeName Name='T1' Id='637856640183333612' IndexInText='149' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<TypeConstraints ObjectId='53' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640183333636' IndexInText='152' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</TypeConstraints>
								</OtherProperties>
							</CustomExpressionItemPart>
							<CustomExpressionItemPart Id='637856640183333652' IndexInText='156' ItemLength='15' Interface='UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem'>
								<RegularItems>
									<Keyword Name='where' Id='637856640183333648' IndexInText='156' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
										<LanguageKeywordInfo ObjectId='48' Id='637781062905654923' Keyword='where' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
									</Keyword>
									<Name Name='T2' Id='637856640183333662' IndexInText='162' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Name Name=':' Id='637856640183333667' IndexInText='164' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637856640183333680' IndexInText='165' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='double' Id='637856640183333674' IndexInText='165' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
								</RegularItems>
								<OtherProperties>
									<WhereKeyword Name='where' Id='637856640183333648' IndexInText='156' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'/>
									<TypeName Name='T2' Id='637856640183333662' IndexInText='162' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<TypeConstraints ObjectId='60' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640183333680' IndexInText='165' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</TypeConstraints>
								</OtherProperties>
							</CustomExpressionItemPart>
							<Name Name='whereend' Id='637856640183333704' IndexInText='172' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<CustomExpressionItemPart Id='637856640183333570' IndexInText='143' ItemLength='12' Interface='UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem'/>
							<CustomExpressionItemPart Id='637856640183333652' IndexInText='156' ItemLength='15' Interface='UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem'/>
						</Children>
						<OtherProperties>
							<WhereEndMarker Name='whereend' Id='637856640183333704' IndexInText='172' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<GenericTypeDataExpressionItems ObjectId='62' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem]'>
								<UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem Id='637856640183333570' IndexInText='143' ItemLength='12'/>
								<UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem Id='637856640183333652' IndexInText='156' ItemLength='15'/>
							</GenericTypeDataExpressionItems>
							<ErrorsPositionDisplayValue value='143' type='System.Int32' />
						</OtherProperties>
					</Custom>
					<Custom Id='637856640183333776' IndexInText='283' ItemLength='21' Interface='UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IWhereCustomExpressionItem'>
						<RegularItems>
							<CustomExpressionItemPart Id='637856640183333785' IndexInText='283' ItemLength='11' Interface='UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem'>
								<RegularItems>
									<Keyword Name='where' Id='637856640183333741' IndexInText='283' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
										<LanguageKeywordInfo ObjectId='48' Id='637781062905654923' Keyword='where' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
									</Keyword>
									<Name Name='T3' Id='637856640183333798' IndexInText='289' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Name Name=':' Id='637856640183333803' IndexInText='291' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637856640183333815' IndexInText='292' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='T1' Id='637856640183333809' IndexInText='292' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
								</RegularItems>
								<OtherProperties>
									<WhereKeyword Name='where' Id='637856640183333741' IndexInText='283' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'/>
									<TypeName Name='T3' Id='637856640183333798' IndexInText='289' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<TypeConstraints ObjectId='70' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640183333815' IndexInText='292' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</TypeConstraints>
								</OtherProperties>
							</CustomExpressionItemPart>
							<Name Name='whereend' Id='637856640183333828' IndexInText='296' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<CustomExpressionItemPart Id='637856640183333785' IndexInText='283' ItemLength='11' Interface='UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem'/>
						</Children>
						<OtherProperties>
							<WhereEndMarker Name='whereend' Id='637856640183333828' IndexInText='296' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<GenericTypeDataExpressionItems ObjectId='72' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem]'>
								<UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem Id='637856640183333785' IndexInText='283' ItemLength='11'/>
							</GenericTypeDataExpressionItems>
							<ErrorsPositionDisplayValue value='283' type='System.Int32' />
						</OtherProperties>
					</Custom>
					<CodeBlock Id='637856640183333870' IndexInText='306' ItemLength='110' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
						<RegularItems>
							<CodeBlockStartMarker Name='{' Id='637856640183333863' IndexInText='306' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637856640183333907' IndexInText='415' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<OtherProperties>
							<CodeBlockStartMarker Name='{' Id='637856640183333863' IndexInText='306' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637856640183333907' IndexInText='415' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OtherProperties>
					</CodeBlock>
				</Postfixes>
				<Children>
					<BinaryOperator Name=':' Priority='0' Id='637856640183332735' IndexInText='22' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
					<BinaryOperator Name=':' Priority='0' Id='637856640183333103' IndexInText='28' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
					<BinaryOperator Name=':' Priority='0' Id='637856640183333441' IndexInText='34' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
				</Children>
				<OtherProperties>
					<NamedExpressionItem Id='637856640183332331' IndexInText='19' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					<OpeningBraceInfo Name='(' Id='637856640183332383' IndexInText='21' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ClosingBraceInfo Name=')' Id='637856640183333455' IndexInText='39' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Parameters ObjectId='76' Count='3' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name=':' Priority='0' Id='637856640183332735' IndexInText='22' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name=':' Priority='0' Id='637856640183333103' IndexInText='28' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name=':' Priority='0' Id='637856640183333441' IndexInText='34' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
					</Parameters>
				</OtherProperties>
			</Braces>
		</RegularItems>
		<Children>
			<Braces Id='637856640183332379' IndexInText='0' ItemLength='416' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
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
	<MainExpressionItem Id='637856640183331419' IndexInText='0' ItemLength='416' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'/>
</UniversalExpressionParser.ExpressionItems.IRootExpressionItemSeries>
```
</details>

- **NOTE:** The list of postfixes can include both types of postfixes at the same time (i.e., custom expression items as well as a code block postfix).

- Example of a code block postfix used to model function body:
 
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
	<ExpressionItemSeries Id='637856640183243072' IndexInText='500' ItemLength='224' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'>
		<RegularItems>
			<BinaryOperator Name=':' Priority='0' Id='637856640183247502' IndexInText='500' ItemLength='149' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
				<Operand1.Braces Id='637856640183243483' IndexInText='500' ItemLength='18' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
					<RegularItems>
						<Literal Id='637856640183243435' IndexInText='500' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='f2' Id='637856640183243428' IndexInText='500' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Literal>
						<OpeningRoundBrace Name='(' Id='637856640183243486' IndexInText='502' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<BinaryOperator Name=':' Priority='0' Id='637856640183243856' IndexInText='503' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
							<Operand1.Literal Id='637856640183243592' IndexInText='503' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='x1' Id='637856640183243586' IndexInText='503' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Operand1.Literal>
							<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637856640183243645' IndexInText='505' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
								<OperatorNameParts>
									<Name Name=':' Id='637856640183243634' IndexInText='505' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</OperatorNameParts>
							</OperatorInfo>
							<Operand2.Literal Id='637856640183243804' IndexInText='506' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='int' Id='637856640183243795' IndexInText='506' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Operand2.Literal>
						</BinaryOperator>
						<Comma Name=',' Id='637856640183243914' IndexInText='509' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<BinaryOperator Name=':' Priority='0' Id='637856640183244222' IndexInText='511' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
							<Operand1.Literal Id='637856640183244001' IndexInText='511' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='x2' Id='637856640183243997' IndexInText='511' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Operand1.Literal>
							<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637856640183244051' IndexInText='513' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
								<OperatorNameParts>
									<Name Name=':' Id='637856640183244045' IndexInText='513' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</OperatorNameParts>
							</OperatorInfo>
							<Operand2.Literal Id='637856640183244186' IndexInText='514' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='int' Id='637856640183244182' IndexInText='514' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Operand2.Literal>
						</BinaryOperator>
						<ClosingRoundBrace Name=')' Id='637856640183244245' IndexInText='517' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</RegularItems>
					<Children>
						<BinaryOperator Name=':' Priority='0' Id='637856640183243856' IndexInText='503' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
						<BinaryOperator Name=':' Priority='0' Id='637856640183244222' IndexInText='511' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
					</Children>
					<OtherProperties>
						<NamedExpressionItem Id='637856640183243435' IndexInText='500' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
						<OpeningBraceInfo Name='(' Id='637856640183243486' IndexInText='502' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<ClosingBraceInfo Name=')' Id='637856640183244245' IndexInText='517' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<Parameters ObjectId='23' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
							<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name=':' Priority='0' Id='637856640183243856' IndexInText='503' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
							<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name=':' Priority='0' Id='637856640183244222' IndexInText='511' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
						</Parameters>
					</OtherProperties>
				</Operand1.Braces>
				<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637856640183244301' IndexInText='519' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
					<OperatorNameParts>
						<Name Name=':' Id='637856640183244295' IndexInText='519' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</OperatorNameParts>
				</OperatorInfo>
				<Operand2.Literal Id='637856640183244446' IndexInText='521' ItemLength='128' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
					<NameExpressionItem Name='int' Id='637856640183244441' IndexInText='521' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Postfixes>
						<CodeBlock Id='637856640183244489' IndexInText='527' ItemLength='122' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
							<RegularItems>
								<CodeBlockStartMarker Name='{' Id='637856640183244481' IndexInText='527' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<BinaryOperator Name=':' Priority='0' Id='637856640183247195' IndexInText='531' ItemLength='97' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
									<Operand1.Braces Id='637856640183244606' IndexInText='531' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
										<RegularItems>
											<Literal Id='637856640183244576' IndexInText='531' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
												<NameExpressionItem Name='f3' Id='637856640183244572' IndexInText='531' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											</Literal>
											<OpeningRoundBrace Name='(' Id='637856640183244609' IndexInText='533' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<ClosingRoundBrace Name=')' Id='637856640183244637' IndexInText='534' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</RegularItems>
										<OtherProperties>
											<NamedExpressionItem Id='637856640183244576' IndexInText='531' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
											<OpeningBraceInfo Name='(' Id='637856640183244609' IndexInText='533' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<ClosingBraceInfo Name=')' Id='637856640183244637' IndexInText='534' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<Parameters ObjectId='36' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'/>
										</OtherProperties>
									</Operand1.Braces>
									<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637856640183244685' IndexInText='536' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
										<OperatorNameParts>
											<Name Name=':' Id='637856640183244680' IndexInText='536' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</OperatorNameParts>
									</OperatorInfo>
									<Operand2.Literal Id='637856640183244846' IndexInText='538' ItemLength='90' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='int' Id='637856640183244841' IndexInText='538' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										<Postfixes>
											<CodeBlock Id='637856640183244884' IndexInText='544' ItemLength='84' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
												<RegularItems>
													<CodeBlockStartMarker Name='{' Id='637856640183244879' IndexInText='544' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
													<BinaryOperator Name='=' Priority='2000' Id='637856640183246068' IndexInText='552' ItemLength='18' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
														<Operand1.Literal Id='637856640183244998' IndexInText='552' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
															<NameExpressionItem Name='result' Id='637856640183244994' IndexInText='556' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
															<AppliedKeywords>
																<Keyword Name='var' Id='637856640183244905' IndexInText='552' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
																	<LanguageKeywordInfo ObjectId='47' Id='637781064051574641' Keyword='var' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
																</Keyword>
															</AppliedKeywords>
														</Operand1.Literal>
														<OperatorInfo OperatorType='BinaryOperator' Name='=' Priority='2000' Id='637856640183245070' IndexInText='563' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
															<OperatorNameParts>
																<Name Name='=' Id='637856640183245064' IndexInText='563' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
															</OperatorNameParts>
														</OperatorInfo>
														<Operand2.BinaryOperator Name='+' Priority='30' Id='637856640183246085' IndexInText='565' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
															<Operand1.Literal Id='637856640183245189' IndexInText='565' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
																<NameExpressionItem Name='x1' Id='637856640183245184' IndexInText='565' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
															</Operand1.Literal>
															<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637856640183245250' IndexInText='567' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
																<OperatorNameParts>
																	<Name Name='+' Id='637856640183245244' IndexInText='567' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
																</OperatorNameParts>
															</OperatorInfo>
															<Operand2.Literal Id='637856640183245997' IndexInText='568' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
																<NameExpressionItem Name='x2' Id='637856640183245977' IndexInText='568' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
															</Operand2.Literal>
														</Operand2.BinaryOperator>
													</BinaryOperator>
													<ExpressionSeparator Name=';' Id='637856640183246138' IndexInText='570' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
													<Braces Id='637856640183246270' IndexInText='575' ItemLength='30' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
														<RegularItems>
															<Literal Id='637856640183246240' IndexInText='575' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
																<NameExpressionItem Name='println' Id='637856640183246236' IndexInText='575' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
															</Literal>
															<OpeningRoundBrace Name='(' Id='637856640183246273' IndexInText='582' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
															<BinaryOperator Name='+' Priority='30' Id='637856640183246842' IndexInText='583' ItemLength='21' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
																<Operand1.BinaryOperator Name='+' Priority='30' Id='637856640183246831' IndexInText='583' ItemLength='17' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
																	<Operand1.ConstantText Id='637856640183246394' IndexInText='583' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
																		<NameExpressionItem Name='"result=''"' Id='637856640183246389' IndexInText='583' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
																	</Operand1.ConstantText>
																	<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637856640183246461' IndexInText='593' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
																		<OperatorNameParts>
																			<Name Name='+' Id='637856640183246450' IndexInText='593' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
																		</OperatorNameParts>
																	</OperatorInfo>
																	<Operand2.Literal Id='637856640183246602' IndexInText='594' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
																		<NameExpressionItem Name='result' Id='637856640183246597' IndexInText='594' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
																	</Operand2.Literal>
																</Operand1.BinaryOperator>
																<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637856640183246661' IndexInText='600' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
																	<OperatorNameParts>
																		<Name Name='+' Id='637856640183246655' IndexInText='600' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
																	</OperatorNameParts>
																</OperatorInfo>
																<Operand2.ConstantText Id='637856640183246791' IndexInText='601' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
																	<NameExpressionItem Name='"''"' Id='637856640183246785' IndexInText='601' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
																</Operand2.ConstantText>
															</BinaryOperator>
															<ClosingRoundBrace Name=')' Id='637856640183246866' IndexInText='604' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
														</RegularItems>
														<Children>
															<BinaryOperator Name='+' Priority='30' Id='637856640183246842' IndexInText='583' ItemLength='21' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
														</Children>
														<OtherProperties>
															<NamedExpressionItem Id='637856640183246240' IndexInText='575' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
															<OpeningBraceInfo Name='(' Id='637856640183246273' IndexInText='582' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
															<ClosingBraceInfo Name=')' Id='637856640183246866' IndexInText='604' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
															<Parameters ObjectId='75' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
																<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name='+' Priority='30' Id='637856640183246842' IndexInText='583' ItemLength='21' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
															</Parameters>
														</OtherProperties>
													</Braces>
													<ExpressionSeparator Name=';' Id='637856640183246904' IndexInText='605' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
													<PrefixUnaryOperator Name='return' Priority='2147483647' Id='637856640183247112' IndexInText='610' ItemLength='13' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
														<OperatorInfo OperatorType='PrefixUnaryOperator' Name='return' Priority='2147483647' Id='637856640183246951' IndexInText='610' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
															<OperatorNameParts>
																<Name Name='return' Id='637856640183246945' IndexInText='610' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
															</OperatorNameParts>
														</OperatorInfo>
														<Operand1.Literal Id='637856640183247062' IndexInText='617' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
															<NameExpressionItem Name='result' Id='637856640183247057' IndexInText='617' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
														</Operand1.Literal>
													</PrefixUnaryOperator>
													<ExpressionSeparator Name=';' Id='637856640183247129' IndexInText='623' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
													<CodeBlockEndMarker Name='}' Id='637856640183247157' IndexInText='627' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												</RegularItems>
												<Children>
													<BinaryOperator Name='=' Priority='2000' Id='637856640183246068' IndexInText='552' ItemLength='18' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
													<Braces Id='637856640183246270' IndexInText='575' ItemLength='30' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
													<PrefixUnaryOperator Name='return' Priority='2147483647' Id='637856640183247112' IndexInText='610' ItemLength='13' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
												</Children>
												<OtherProperties>
													<CodeBlockStartMarker Name='{' Id='637856640183244879' IndexInText='544' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
													<CodeBlockEndMarker Name='}' Id='637856640183247157' IndexInText='627' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												</OtherProperties>
											</CodeBlock>
										</Postfixes>
									</Operand2.Literal>
								</BinaryOperator>
								<PrefixUnaryOperator Name='return' Priority='2147483647' Id='637856640183247451' IndexInText='634' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
									<OperatorInfo OperatorType='PrefixUnaryOperator' Name='return' Priority='2147483647' Id='637856640183247255' IndexInText='634' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
										<OperatorNameParts>
											<Name Name='return' Id='637856640183247249' IndexInText='634' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</OperatorNameParts>
									</OperatorInfo>
									<Operand1.Braces Id='637856640183247391' IndexInText='641' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
										<RegularItems>
											<Literal Id='637856640183247358' IndexInText='641' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
												<NameExpressionItem Name='f3' Id='637856640183247353' IndexInText='641' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											</Literal>
											<OpeningRoundBrace Name='(' Id='637856640183247394' IndexInText='643' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<ClosingRoundBrace Name=')' Id='637856640183247423' IndexInText='644' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</RegularItems>
										<OtherProperties>
											<NamedExpressionItem Id='637856640183247358' IndexInText='641' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
											<OpeningBraceInfo Name='(' Id='637856640183247394' IndexInText='643' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<ClosingBraceInfo Name=')' Id='637856640183247423' IndexInText='644' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<Parameters ObjectId='92' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'/>
										</OtherProperties>
									</Operand1.Braces>
								</PrefixUnaryOperator>
								<ExpressionSeparator Name=';' Id='637856640183247464' IndexInText='645' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<CodeBlockEndMarker Name='}' Id='637856640183247488' IndexInText='648' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</RegularItems>
							<Children>
								<BinaryOperator Name=':' Priority='0' Id='637856640183247195' IndexInText='531' ItemLength='97' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
								<PrefixUnaryOperator Name='return' Priority='2147483647' Id='637856640183247451' IndexInText='634' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
							</Children>
							<OtherProperties>
								<CodeBlockStartMarker Name='{' Id='637856640183244481' IndexInText='527' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<CodeBlockEndMarker Name='}' Id='637856640183247488' IndexInText='648' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OtherProperties>
						</CodeBlock>
					</Postfixes>
				</Operand2.Literal>
			</BinaryOperator>
			<BinaryOperator Name='=' Priority='2000' Id='637856640183249475' IndexInText='653' ItemLength='71' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
				<Operand1.Literal Id='637856640183247619' IndexInText='653' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
					<NameExpressionItem Name='myFunc' Id='637856640183247615' IndexInText='657' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<AppliedKeywords>
						<Keyword Name='var' Id='637856640183247531' IndexInText='653' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
							<LanguageKeywordInfo ObjectId='47' Id='637781064051574641' Keyword='var' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
						</Keyword>
					</AppliedKeywords>
				</Operand1.Literal>
				<OperatorInfo OperatorType='BinaryOperator' Name='=' Priority='2000' Id='637856640183247696' IndexInText='664' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
					<OperatorNameParts>
						<Name Name='=' Id='637856640183247690' IndexInText='664' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</OperatorNameParts>
				</OperatorInfo>
				<Operand2.BinaryOperator Name='=>' Priority='1000' Id='637856640183249483' IndexInText='666' ItemLength='58' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
					<Operand1.Braces Id='637856640183247841' IndexInText='666' ItemLength='18' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<Literal Id='637856640183247808' IndexInText='666' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='f2' Id='637856640183247803' IndexInText='666' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<OpeningRoundBrace Name='(' Id='637856640183247845' IndexInText='668' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<BinaryOperator Name=':' Priority='0' Id='637856640183248141' IndexInText='669' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
								<Operand1.Literal Id='637856640183247927' IndexInText='669' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
									<NameExpressionItem Name='x1' Id='637856640183247922' IndexInText='669' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</Operand1.Literal>
								<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637856640183247976' IndexInText='671' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
									<OperatorNameParts>
										<Name Name=':' Id='637856640183247967' IndexInText='671' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</OperatorNameParts>
								</OperatorInfo>
								<Operand2.Literal Id='637856640183248109' IndexInText='672' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
									<NameExpressionItem Name='int' Id='637856640183248105' IndexInText='672' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</Operand2.Literal>
							</BinaryOperator>
							<Comma Name=',' Id='637856640183248162' IndexInText='675' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<BinaryOperator Name=':' Priority='0' Id='637856640183248461' IndexInText='677' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
								<Operand1.Literal Id='637856640183248243' IndexInText='677' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
									<NameExpressionItem Name='x2' Id='637856640183248239' IndexInText='677' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</Operand1.Literal>
								<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637856640183248290' IndexInText='679' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
									<OperatorNameParts>
										<Name Name=':' Id='637856640183248284' IndexInText='679' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</OperatorNameParts>
								</OperatorInfo>
								<Operand2.Literal Id='637856640183248425' IndexInText='680' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
									<NameExpressionItem Name='int' Id='637856640183248420' IndexInText='680' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</Operand2.Literal>
							</BinaryOperator>
							<ClosingRoundBrace Name=')' Id='637856640183248479' IndexInText='683' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<BinaryOperator Name=':' Priority='0' Id='637856640183248141' IndexInText='669' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
							<BinaryOperator Name=':' Priority='0' Id='637856640183248461' IndexInText='677' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
						</Children>
						<OtherProperties>
							<NamedExpressionItem Id='637856640183247808' IndexInText='666' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							<OpeningBraceInfo Name='(' Id='637856640183247845' IndexInText='668' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=')' Id='637856640183248479' IndexInText='683' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Parameters ObjectId='122' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name=':' Priority='0' Id='637856640183248141' IndexInText='669' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name=':' Priority='0' Id='637856640183248461' IndexInText='677' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Operand1.Braces>
					<OperatorInfo OperatorType='BinaryOperator' Name='=>' Priority='1000' Id='637856640183248552' IndexInText='685' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
						<OperatorNameParts>
							<Name Name='=>' Id='637856640183248546' IndexInText='685' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OperatorNameParts>
					</OperatorInfo>
					<Operand2.CodeBlock Id='637856640183248661' IndexInText='689' ItemLength='35' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
						<RegularItems>
							<CodeBlockStartMarker Name='{' Id='637856640183248656' IndexInText='689' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Braces Id='637856640183248782' IndexInText='696' ItemLength='24' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
								<RegularItems>
									<Literal Id='637856640183248756' IndexInText='696' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='println' Id='637856640183248751' IndexInText='696' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<OpeningRoundBrace Name='(' Id='637856640183248785' IndexInText='703' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<BinaryOperator Name='^' Priority='10' Id='637856640183249403' IndexInText='704' ItemLength='15' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
										<Operand1.Literal Id='637856640183248871' IndexInText='704' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
											<NameExpressionItem Name='exp' Id='637856640183248865' IndexInText='704' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</Operand1.Literal>
										<OperatorInfo OperatorType='BinaryOperator' Name='^' Priority='10' Id='637856640183248922' IndexInText='708' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
											<OperatorNameParts>
												<Name Name='^' Id='637856640183248913' IndexInText='708' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											</OperatorNameParts>
										</OperatorInfo>
										<Operand2.Braces Id='637856640183249047' IndexInText='710' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
											<RegularItems>
												<OpeningRoundBrace Name='(' Id='637856640183249051' IndexInText='710' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												<BinaryOperator Name='+' Priority='30' Id='637856640183249348' IndexInText='711' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
													<Operand1.Literal Id='637856640183249133' IndexInText='711' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
														<NameExpressionItem Name='x1' Id='637856640183249127' IndexInText='711' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
													</Operand1.Literal>
													<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637856640183249190' IndexInText='714' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
														<OperatorNameParts>
															<Name Name='+' Id='637856640183249184' IndexInText='714' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
														</OperatorNameParts>
													</OperatorInfo>
													<Operand2.Literal Id='637856640183249317' IndexInText='716' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
														<NameExpressionItem Name='x2' Id='637856640183249312' IndexInText='716' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
													</Operand2.Literal>
												</BinaryOperator>
												<ClosingRoundBrace Name=')' Id='637856640183249367' IndexInText='718' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											</RegularItems>
											<Children>
												<BinaryOperator Name='+' Priority='30' Id='637856640183249348' IndexInText='711' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
											</Children>
											<OtherProperties>
												<OpeningBraceInfo Name='(' Id='637856640183249051' IndexInText='710' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												<ClosingBraceInfo Name=')' Id='637856640183249367' IndexInText='718' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
												<Parameters ObjectId='146' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
													<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name='+' Priority='30' Id='637856640183249348' IndexInText='711' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
												</Parameters>
											</OtherProperties>
										</Operand2.Braces>
									</BinaryOperator>
									<ClosingRoundBrace Name=')' Id='637856640183249416' IndexInText='719' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Children>
									<BinaryOperator Name='^' Priority='10' Id='637856640183249403' IndexInText='704' ItemLength='15' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
								</Children>
								<OtherProperties>
									<NamedExpressionItem Id='637856640183248756' IndexInText='696' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<OpeningBraceInfo Name='(' Id='637856640183248785' IndexInText='703' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ClosingBraceInfo Name=')' Id='637856640183249416' IndexInText='719' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Parameters ObjectId='148' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name='^' Priority='10' Id='637856640183249403' IndexInText='704' ItemLength='15' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
									</Parameters>
								</OtherProperties>
							</Braces>
							<ExpressionSeparator Name=';' Id='637856640183249442' IndexInText='720' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637856640183249467' IndexInText='723' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<Braces Id='637856640183248782' IndexInText='696' ItemLength='24' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
						</Children>
						<OtherProperties>
							<CodeBlockStartMarker Name='{' Id='637856640183248656' IndexInText='689' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637856640183249467' IndexInText='723' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OtherProperties>
					</Operand2.CodeBlock>
				</Operand2.BinaryOperator>
			</BinaryOperator>
		</RegularItems>
		<Children>
			<BinaryOperator Name=':' Priority='0' Id='637856640183247502' IndexInText='500' ItemLength='149' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
			<BinaryOperator Name='=' Priority='2000' Id='637856640183249475' IndexInText='653' ItemLength='71' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
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
	<MainExpressionItem Id='637856640183243072' IndexInText='500' ItemLength='224' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'/>
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
	<ExpressionItemSeries Id='637856640183289591' IndexInText='539' ItemLength='106' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'>
		<RegularItems>
			<BinaryOperator Name=':' Priority='0' Id='637856640183291403' IndexInText='539' ItemLength='106' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
				<Operand1.Literal Id='637856640183289997' IndexInText='539' ItemLength='16' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
					<NameExpressionItem Name='Dog' Id='637856640183289991' IndexInText='552' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<AppliedKeywords>
						<Keyword Name='public' Id='637856640183289845' IndexInText='539' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
							<LanguageKeywordInfo ObjectId='6' Id='637783669980406419' Keyword='public' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
						</Keyword>
						<Keyword Name='class' Id='637856640183289861' IndexInText='546' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
							<LanguageKeywordInfo ObjectId='8' Id='637783670388745402' Keyword='class' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
						</Keyword>
					</AppliedKeywords>
				</Operand1.Literal>
				<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637856640183290071' IndexInText='556' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
					<OperatorNameParts>
						<Name Name=':' Id='637856640183290061' IndexInText='556' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</OperatorNameParts>
				</OperatorInfo>
				<Operand2.Braces Id='637856640183290237' IndexInText='558' ItemLength='87' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
					<RegularItems>
						<OpeningRoundBrace Name='(' Id='637856640183290243' IndexInText='558' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<Literal Id='637856640183290345' IndexInText='559' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='Anymal' Id='637856640183290340' IndexInText='559' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Literal>
						<Comma Name=',' Id='637856640183290398' IndexInText='565' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<Literal Id='637856640183290476' IndexInText='567' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='IDog' Id='637856640183290472' IndexInText='567' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Literal>
						<ClosingRoundBrace Name=')' Id='637856640183290507' IndexInText='571' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</RegularItems>
					<Postfixes>
						<CodeBlock Id='637856640183290547' IndexInText='574' ItemLength='71' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
							<RegularItems>
								<CodeBlockStartMarker Name='{' Id='637856640183290543' IndexInText='574' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<BinaryOperator Name=':' Priority='0' Id='637856640183291320' IndexInText='581' ItemLength='61' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
									<Operand1.Braces Id='637856640183290699' IndexInText='581' ItemLength='13' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
										<AppliedKeywords>
											<Keyword Name='public' Id='637856640183290572' IndexInText='581' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
												<LanguageKeywordInfo ObjectId='6' Id='637783669980406419' Keyword='public' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
											</Keyword>
										</AppliedKeywords>
										<RegularItems>
											<Literal Id='637856640183290660' IndexInText='588' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
												<NameExpressionItem Name='Bark' Id='637856640183290656' IndexInText='588' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											</Literal>
											<OpeningRoundBrace Name='(' Id='637856640183290702' IndexInText='592' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<ClosingRoundBrace Name=')' Id='637856640183290732' IndexInText='593' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</RegularItems>
										<OtherProperties>
											<NamedExpressionItem Id='637856640183290660' IndexInText='588' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
											<OpeningBraceInfo Name='(' Id='637856640183290702' IndexInText='592' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<ClosingBraceInfo Name=')' Id='637856640183290732' IndexInText='593' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<Parameters ObjectId='28' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'/>
										</OtherProperties>
									</Operand1.Braces>
									<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637856640183290787' IndexInText='595' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
										<OperatorNameParts>
											<Name Name=':' Id='637856640183290779' IndexInText='595' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</OperatorNameParts>
									</OperatorInfo>
									<Operand2.Literal Id='637856640183290927' IndexInText='597' ItemLength='45' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='void' Id='637856640183290923' IndexInText='597' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										<Postfixes>
											<CodeBlock Id='637856640183290964' IndexInText='607' ItemLength='35' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
												<RegularItems>
													<CodeBlockStartMarker Name='{' Id='637856640183290960' IndexInText='607' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
													<Braces Id='637856640183291077' IndexInText='618' ItemLength='16' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
														<RegularItems>
															<Literal Id='637856640183291051' IndexInText='618' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
																<NameExpressionItem Name='println' Id='637856640183291047' IndexInText='618' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
															</Literal>
															<OpeningRoundBrace Name='(' Id='637856640183291082' IndexInText='625' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
															<ConstantText Id='637856640183291205' IndexInText='626' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
																<NameExpressionItem Name='"Bark."' Id='637856640183291199' IndexInText='626' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
															</ConstantText>
															<ClosingRoundBrace Name=')' Id='637856640183291238' IndexInText='633' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
														</RegularItems>
														<Children>
															<ConstantText Id='637856640183291205' IndexInText='626' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
														</Children>
														<OtherProperties>
															<NamedExpressionItem Id='637856640183291051' IndexInText='618' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
															<OpeningBraceInfo Name='(' Id='637856640183291082' IndexInText='625' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
															<ClosingBraceInfo Name=')' Id='637856640183291238' IndexInText='633' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
															<Parameters ObjectId='42' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
																<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640183291205' IndexInText='626' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
															</Parameters>
														</OtherProperties>
													</Braces>
													<ExpressionSeparator Name=';' Id='637856640183291270' IndexInText='634' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
													<CodeBlockEndMarker Name='}' Id='637856640183291300' IndexInText='641' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												</RegularItems>
												<Children>
													<Braces Id='637856640183291077' IndexInText='618' ItemLength='16' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
												</Children>
												<OtherProperties>
													<CodeBlockStartMarker Name='{' Id='637856640183290960' IndexInText='607' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
													<CodeBlockEndMarker Name='}' Id='637856640183291300' IndexInText='641' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												</OtherProperties>
											</CodeBlock>
										</Postfixes>
									</Operand2.Literal>
								</BinaryOperator>
								<CodeBlockEndMarker Name='}' Id='637856640183291393' IndexInText='644' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</RegularItems>
							<Children>
								<BinaryOperator Name=':' Priority='0' Id='637856640183291320' IndexInText='581' ItemLength='61' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
							</Children>
							<OtherProperties>
								<CodeBlockStartMarker Name='{' Id='637856640183290543' IndexInText='574' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<CodeBlockEndMarker Name='}' Id='637856640183291393' IndexInText='644' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OtherProperties>
						</CodeBlock>
					</Postfixes>
					<Children>
						<Literal Id='637856640183290345' IndexInText='559' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
						<Literal Id='637856640183290476' IndexInText='567' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					</Children>
					<OtherProperties>
						<OpeningBraceInfo Name='(' Id='637856640183290243' IndexInText='558' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<ClosingBraceInfo Name=')' Id='637856640183290507' IndexInText='571' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
						<Parameters ObjectId='46' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
							<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640183290345' IndexInText='559' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640183290476' IndexInText='567' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
						</Parameters>
					</OtherProperties>
				</Operand2.Braces>
			</BinaryOperator>
		</RegularItems>
		<Children>
			<BinaryOperator Name=':' Priority='0' Id='637856640183291403' IndexInText='539' ItemLength='106' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
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
	<MainExpressionItem Id='637856640183289591' IndexInText='539' ItemLength='106' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'/>
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
	<ExpressionItemSeries Id='637856640182757324' IndexInText='0' ItemLength='624' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'>
		<RegularItems>
			<Braces Id='637856640182757886' IndexInText='0' ItemLength='106' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
				<RegularItems>
					<Literal Id='637856640182757835' IndexInText='0' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='f1' Id='637856640182757828' IndexInText='0' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<OpeningRoundBrace Name='(' Id='637856640182757890' IndexInText='2' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Literal Id='637856640182758004' IndexInText='3' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='x1' Id='637856640182757999' IndexInText='3' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<ClosingRoundBrace Name=')' Id='637856640182758045' IndexInText='5' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Postfixes>
					<CodeBlock Id='637856640182758084' IndexInText='9' ItemLength='97' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
						<RegularItems>
							<CodeBlockStartMarker Name='{' Id='637856640182758080' IndexInText='9' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<PrefixUnaryOperator Name='return' Priority='2147483647' Id='637856640182758547' IndexInText='90' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
								<OperatorInfo OperatorType='PrefixUnaryOperator' Name='return' Priority='2147483647' Id='637856640182758161' IndexInText='90' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
									<OperatorNameParts>
										<Name Name='return' Id='637856640182758150' IndexInText='90' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</OperatorNameParts>
								</OperatorInfo>
								<Operand1.BinaryOperator Name='*' Priority='20' Id='637856640182758568' IndexInText='97' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
									<Operand1.Literal Id='637856640182758289' IndexInText='97' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='x2' Id='637856640182758284' IndexInText='97' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Operand1.Literal>
									<OperatorInfo OperatorType='BinaryOperator' Name='*' Priority='20' Id='637856640182758356' IndexInText='99' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
										<OperatorNameParts>
											<Name Name='*' Id='637856640182758350' IndexInText='99' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</OperatorNameParts>
									</OperatorInfo>
									<Operand2.Literal Id='637856640182758493' IndexInText='100' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='y1' Id='637856640182758489' IndexInText='100' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Operand2.Literal>
								</Operand1.BinaryOperator>
							</PrefixUnaryOperator>
							<ExpressionSeparator Name=';' Id='637856640182758619' IndexInText='102' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637856640182758650' IndexInText='105' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<PrefixUnaryOperator Name='return' Priority='2147483647' Id='637856640182758547' IndexInText='90' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
						</Children>
						<OtherProperties>
							<CodeBlockStartMarker Name='{' Id='637856640182758080' IndexInText='9' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637856640182758650' IndexInText='105' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OtherProperties>
					</CodeBlock>
				</Postfixes>
				<Children>
					<Literal Id='637856640182758004' IndexInText='3' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
				</Children>
				<OtherProperties>
					<NamedExpressionItem Id='637856640182757835' IndexInText='0' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					<OpeningBraceInfo Name='(' Id='637856640182757890' IndexInText='2' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ClosingBraceInfo Name=')' Id='637856640182758045' IndexInText='5' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Parameters ObjectId='23' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640182758004' IndexInText='3' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					</Parameters>
				</OtherProperties>
			</Braces>
			<Braces Id='637856640182758772' IndexInText='110' ItemLength='97' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
				<RegularItems>
					<Literal Id='637856640182758745' IndexInText='110' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='m1' Id='637856640182758740' IndexInText='110' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<OpeningSquareBrace Name='[' Id='637856640182758774' IndexInText='112' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Literal Id='637856640182758860' IndexInText='113' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='x2' Id='637856640182758856' IndexInText='113' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<ClosingSquareBrace Name=']' Id='637856640182758890' IndexInText='115' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Postfixes>
					<CodeBlock Id='637856640182758922' IndexInText='118' ItemLength='89' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
						<RegularItems>
							<CodeBlockStartMarker Name='{' Id='637856640182758918' IndexInText='118' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<BinaryOperator Name='*' Priority='20' Id='637856640182925904' IndexInText='199' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
								<Operand1.BinaryOperator Name=':' Priority='0' Id='637856640182925877' IndexInText='199' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
									<Operand1.Literal Id='637856640182759013' IndexInText='199' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='x' Id='637856640182759009' IndexInText='199' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Operand1.Literal>
									<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637856640182759061' IndexInText='200' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
										<OperatorNameParts>
											<Name Name=':' Id='637856640182759056' IndexInText='200' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</OperatorNameParts>
									</OperatorInfo>
									<Operand2.ConstantNumericValue Id='637856640182845383' IndexInText='201' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
										<NameExpressionItem Name='2' Id='637856640182845328' IndexInText='201' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										<OtherProperties>
											<SucceededNumericTypeDescriptor ObjectId='41' NumberTypeId='1581134183960' Interface='UniversalExpressionParser.NumericTypeDescriptor'>
												<RegularExpressions ObjectId='42' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[System.String]'>
													<System.String value='^\d+' />
												</RegularExpressions>
											</SucceededNumericTypeDescriptor>
										</OtherProperties>
									</Operand2.ConstantNumericValue>
								</Operand1.BinaryOperator>
								<OperatorInfo OperatorType='BinaryOperator' Name='*' Priority='20' Id='637856640182845615' IndexInText='202' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
									<OperatorNameParts>
										<Name Name='*' Id='637856640182845598' IndexInText='202' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</OperatorNameParts>
								</OperatorInfo>
								<Operand2.ConstantNumericValue Id='637856640182925660' IndexInText='203' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
									<NameExpressionItem Name='3' Id='637856640182925608' IndexInText='203' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<OtherProperties>
										<SucceededNumericTypeDescriptor ObjectId='41' NumberTypeId='1581134183960' Interface='UniversalExpressionParser.NumericTypeDescriptor'/>
									</OtherProperties>
								</Operand2.ConstantNumericValue>
							</BinaryOperator>
							<CodeBlockEndMarker Name='}' Id='637856640182925975' IndexInText='206' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<BinaryOperator Name='*' Priority='20' Id='637856640182925904' IndexInText='199' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
						</Children>
						<OtherProperties>
							<CodeBlockStartMarker Name='{' Id='637856640182758918' IndexInText='118' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637856640182925975' IndexInText='206' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OtherProperties>
					</CodeBlock>
				</Postfixes>
				<Children>
					<Literal Id='637856640182758860' IndexInText='113' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
				</Children>
				<OtherProperties>
					<NamedExpressionItem Id='637856640182758745' IndexInText='110' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					<OpeningBraceInfo Name='[' Id='637856640182758774' IndexInText='112' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ClosingBraceInfo Name=']' Id='637856640182758890' IndexInText='115' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Parameters ObjectId='48' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640182758860' IndexInText='113' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					</Parameters>
				</OtherProperties>
			</Braces>
			<Braces Id='637856640182926028' IndexInText='211' ItemLength='100' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
				<RegularItems>
					<OpeningRoundBrace Name='(' Id='637856640182926031' IndexInText='211' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Literal Id='637856640182926166' IndexInText='212' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='x3' Id='637856640182926162' IndexInText='212' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<ClosingRoundBrace Name=')' Id='637856640182926205' IndexInText='214' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Postfixes>
					<CodeBlock Id='637856640182926239' IndexInText='217' ItemLength='94' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
						<RegularItems>
							<CodeBlockStartMarker Name='{' Id='637856640182926235' IndexInText='217' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<PrefixUnaryOperator Name='return' Priority='2147483647' Id='637856640183027625' IndexInText='296' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
								<OperatorInfo OperatorType='PrefixUnaryOperator' Name='return' Priority='2147483647' Id='637856640182926317' IndexInText='296' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
									<OperatorNameParts>
										<Name Name='return' Id='637856640182926307' IndexInText='296' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</OperatorNameParts>
								</OperatorInfo>
								<Operand1.BinaryOperator Name='*' Priority='20' Id='637856640183027670' IndexInText='303' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
									<Operand1.Literal Id='637856640182926460' IndexInText='303' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='x3' Id='637856640182926452' IndexInText='303' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Operand1.Literal>
									<OperatorInfo OperatorType='BinaryOperator' Name='*' Priority='20' Id='637856640182926536' IndexInText='305' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
										<OperatorNameParts>
											<Name Name='*' Id='637856640182926530' IndexInText='305' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</OperatorNameParts>
									</OperatorInfo>
									<Operand2.ConstantNumericValue Id='637856640183027426' IndexInText='306' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
										<NameExpressionItem Name='2' Id='637856640183027364' IndexInText='306' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										<OtherProperties>
											<SucceededNumericTypeDescriptor ObjectId='41' NumberTypeId='1581134183960' Interface='UniversalExpressionParser.NumericTypeDescriptor'/>
										</OtherProperties>
									</Operand2.ConstantNumericValue>
								</Operand1.BinaryOperator>
							</PrefixUnaryOperator>
							<ExpressionSeparator Name=';' Id='637856640183027746' IndexInText='307' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637856640183027794' IndexInText='310' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<PrefixUnaryOperator Name='return' Priority='2147483647' Id='637856640183027625' IndexInText='296' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
						</Children>
						<OtherProperties>
							<CodeBlockStartMarker Name='{' Id='637856640182926235' IndexInText='217' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637856640183027794' IndexInText='310' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OtherProperties>
					</CodeBlock>
				</Postfixes>
				<Children>
					<Literal Id='637856640182926166' IndexInText='212' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
				</Children>
				<OtherProperties>
					<OpeningBraceInfo Name='(' Id='637856640182926031' IndexInText='211' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ClosingBraceInfo Name=')' Id='637856640182926205' IndexInText='214' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
					<Parameters ObjectId='68' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640182926166' IndexInText='212' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					</Parameters>
				</OtherProperties>
			</Braces>
			<Braces Id='637856640183027844' IndexInText='315' ItemLength='94' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
				<RegularItems>
					<OpeningSquareBrace Name='[' Id='637856640183027848' IndexInText='315' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Literal Id='637856640183028000' IndexInText='316' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='x4' Id='637856640183027994' IndexInText='316' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<ClosingSquareBrace Name=']' Id='637856640183028045' IndexInText='318' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Postfixes>
					<CodeBlock Id='637856640183028078' IndexInText='321' ItemLength='88' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
						<RegularItems>
							<CodeBlockStartMarker Name='{' Id='637856640183028074' IndexInText='321' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<BinaryOperator Name='*' Priority='20' Id='637856640183208126' IndexInText='400' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
								<Operand1.BinaryOperator Name=':' Priority='0' Id='637856640183208098' IndexInText='400' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
									<Operand1.Literal Id='637856640183028194' IndexInText='400' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='x4' Id='637856640183028190' IndexInText='400' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Operand1.Literal>
									<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637856640183028249' IndexInText='402' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
										<OperatorNameParts>
											<Name Name=':' Id='637856640183028236' IndexInText='402' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</OperatorNameParts>
									</OperatorInfo>
									<Operand2.ConstantNumericValue Id='637856640183122230' IndexInText='403' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
										<NameExpressionItem Name='2' Id='637856640183122169' IndexInText='403' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										<OtherProperties>
											<SucceededNumericTypeDescriptor ObjectId='41' NumberTypeId='1581134183960' Interface='UniversalExpressionParser.NumericTypeDescriptor'/>
										</OtherProperties>
									</Operand2.ConstantNumericValue>
								</Operand1.BinaryOperator>
								<OperatorInfo OperatorType='BinaryOperator' Name='*' Priority='20' Id='637856640183122515' IndexInText='404' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
									<OperatorNameParts>
										<Name Name='*' Id='637856640183122503' IndexInText='404' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</OperatorNameParts>
								</OperatorInfo>
								<Operand2.ConstantNumericValue Id='637856640183207871' IndexInText='405' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
									<NameExpressionItem Name='3' Id='637856640183207824' IndexInText='405' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<OtherProperties>
										<SucceededNumericTypeDescriptor ObjectId='41' NumberTypeId='1581134183960' Interface='UniversalExpressionParser.NumericTypeDescriptor'/>
									</OtherProperties>
								</Operand2.ConstantNumericValue>
							</BinaryOperator>
							<CodeBlockEndMarker Name='}' Id='637856640183208198' IndexInText='408' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<BinaryOperator Name='*' Priority='20' Id='637856640183208126' IndexInText='400' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
						</Children>
						<OtherProperties>
							<CodeBlockStartMarker Name='{' Id='637856640183028074' IndexInText='321' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637856640183208198' IndexInText='408' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OtherProperties>
					</CodeBlock>
				</Postfixes>
				<Children>
					<Literal Id='637856640183028000' IndexInText='316' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
				</Children>
				<OtherProperties>
					<OpeningBraceInfo Name='[' Id='637856640183027848' IndexInText='315' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ClosingBraceInfo Name=']' Id='637856640183028045' IndexInText='318' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
					<Parameters ObjectId='89' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640183028000' IndexInText='316' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					</Parameters>
				</OtherProperties>
			</Braces>
			<Literal Id='637856640183208391' IndexInText='413' ItemLength='87' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
				<NameExpressionItem Name='Dog' Id='637856640183208386' IndexInText='419' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				<AppliedKeywords>
					<Keyword Name='class' Id='637856640183208244' IndexInText='413' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
						<LanguageKeywordInfo ObjectId='93' Id='637783670388745402' Keyword='class' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
					</Keyword>
				</AppliedKeywords>
				<Postfixes>
					<CodeBlock Id='637856640183208431' IndexInText='424' ItemLength='76' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
						<RegularItems>
							<CodeBlockStartMarker Name='{' Id='637856640183208426' IndexInText='424' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637856640183208488' IndexInText='499' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<OtherProperties>
							<CodeBlockStartMarker Name='{' Id='637856640183208426' IndexInText='424' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637856640183208488' IndexInText='499' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OtherProperties>
					</CodeBlock>
				</Postfixes>
			</Literal>
			<Custom Id='637856640183208582' IndexInText='504' ItemLength='120' Interface='UniversalExpressionParser.ExpressionItems.Custom.IKeywordBasedCustomExpressionItem'>
				<RegularItems>
					<Keyword Name='::pragma' Id='637856640183208508' IndexInText='504' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
						<LanguageKeywordInfo ObjectId='99' Id='637781063127646487' Keyword='::pragma' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
					</Keyword>
					<Name Name='x' Id='637856640183208576' IndexInText='513' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Postfixes>
					<CodeBlock Id='637856640183208632' IndexInText='516' ItemLength='108' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
						<RegularItems>
							<CodeBlockStartMarker Name='{' Id='637856640183208627' IndexInText='516' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637856640183210268' IndexInText='623' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<OtherProperties>
							<CodeBlockStartMarker Name='{' Id='637856640183208627' IndexInText='516' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637856640183210268' IndexInText='623' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OtherProperties>
					</CodeBlock>
				</Postfixes>
				<OtherProperties>
					<LastKeywordExpressionItem Name='::pragma' Id='637856640183208508' IndexInText='504' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'/>
					<ErrorsPositionDisplayValue value='504' type='System.Int32' />
				</OtherProperties>
			</Custom>
		</RegularItems>
		<Children>
			<Braces Id='637856640182757886' IndexInText='0' ItemLength='106' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
			<Braces Id='637856640182758772' IndexInText='110' ItemLength='97' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
			<Braces Id='637856640182926028' IndexInText='211' ItemLength='100' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
			<Braces Id='637856640183027844' IndexInText='315' ItemLength='94' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
			<Literal Id='637856640183208391' IndexInText='413' ItemLength='87' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
			<Custom Id='637856640183208582' IndexInText='504' ItemLength='120' Interface='UniversalExpressionParser.ExpressionItems.Custom.IKeywordBasedCustomExpressionItem'/>
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
	<MainExpressionItem Id='637856640182757324' IndexInText='0' ItemLength='624' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'/>
</UniversalExpressionParser.ExpressionItems.IRootExpressionItemSeries>
```
</details>

# Custom Expression Item Parsers

Custom expression parsers allow to plugin into parsing process and provide special parsing of some portion of the parsed expression. 

The expression parser (i.e., **UniversalExpressionParser.IExpressionParser**) iteratively parses keywords (see the section above on keywords), before parsing any other symbols.

Then the expression parser loops through all the custom expression parsers of type **UniversalExpressionParser.ExpressionItems.Custom.ICustomExpressionItemParser** in property **CustomExpressionItemParsers** in interface **UniversalExpressionParser.IExpressionLanguageProvider** passed to the parser, and for each custom expression parser executes the method
 **ICustomExpressionItem ICustomExpressionItemParser.TryParseCustomExpressionItem(IParseExpressionItemContext context, IReadOnlyList&lt;IExpressionItemBase&gt; parsedPrefixExpressionItems, IReadOnlyList&lt;IKeywordExpressionItem&gt; keywordExpressionItems)**.

If method call **TryParseCustomExpressionItem(...)** returns non-null value of type **UniversalExpressionParser.ExpressionItems.Custom.ICustomExpressionItem**, the parser uses the parsed custom expression item.

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

In this example, the code block after "whereend" (the expression "{...}") is parsed as a postfix expression item of type **UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem** and is added as a postfix to regular expression item parsed from "F1(x:T1, y:T2)" as well, since the parser adds all the prefixes/postfixes to regular expression item it finds after/before the prefixes/postfixes. 

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
	<ExpressionItemSeries Id='637856640180378241' IndexInText='0' ItemLength='185' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'>
		<RegularItems>
			<BinaryOperator Name='+' Priority='30' Id='637856640180378990' IndexInText='0' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
				<Operand1.Custom Id='637856640180378631' IndexInText='0' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.Custom.IKeywordBasedCustomExpressionItem'>
					<RegularItems>
						<Keyword Name='::pragma' Id='637856640180378506' IndexInText='0' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
							<LanguageKeywordInfo ObjectId='5' Id='637781063127646487' Keyword='::pragma' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
						</Keyword>
						<Name Name='x' Id='637856640180378619' IndexInText='9' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</RegularItems>
					<OtherProperties>
						<LastKeywordExpressionItem Name='::pragma' Id='637856640180378506' IndexInText='0' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'/>
						<ErrorsPositionDisplayValue value='0' type='System.Int32' />
					</OtherProperties>
				</Operand1.Custom>
				<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637856640180378779' IndexInText='10' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
					<OperatorNameParts>
						<Name Name='+' Id='637856640180378766' IndexInText='10' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</OperatorNameParts>
				</OperatorInfo>
				<Operand2.Literal Id='637856640180378933' IndexInText='11' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
					<NameExpressionItem Name='y' Id='637856640180378929' IndexInText='11' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</Operand2.Literal>
			</BinaryOperator>
			<ExpressionSeparator Name=';' Id='637856640180379056' IndexInText='12' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<Braces Id='637856640180379941' IndexInText='15' ItemLength='170' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
				<Prefixes>
					<Custom Id='637856640180379572' IndexInText='15' ItemLength='14' Interface='UniversalExpressionParser.ExpressionItems.Custom.IKeywordBasedCustomExpressionItem'>
						<RegularItems>
							<Keyword Name='::types' Id='637856640180379073' IndexInText='15' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
								<LanguageKeywordInfo ObjectId='15' Id='637781062811583137' Keyword='::types' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
							</Keyword>
							<Braces Id='637856640180379250' IndexInText='22' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
								<RegularItems>
									<OpeningSquareBrace Name='[' Id='637856640180379254' IndexInText='22' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637856640180379403' IndexInText='23' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='T1' Id='637856640180379398' IndexInText='23' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<Comma Name=',' Id='637856640180379442' IndexInText='25' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637856640180379527' IndexInText='26' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='T2' Id='637856640180379522' IndexInText='26' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<ClosingSquareBrace Name=']' Id='637856640180379562' IndexInText='28' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Children>
									<Literal Id='637856640180379403' IndexInText='23' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<Literal Id='637856640180379527' IndexInText='26' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								</Children>
								<OtherProperties>
									<OpeningBraceInfo Name='[' Id='637856640180379254' IndexInText='22' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ClosingBraceInfo Name=']' Id='637856640180379562' IndexInText='28' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
									<Parameters ObjectId='24' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640180379403' IndexInText='23' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640180379527' IndexInText='26' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</Parameters>
								</OtherProperties>
							</Braces>
						</RegularItems>
						<OtherProperties>
							<LastKeywordExpressionItem Name='::types' Id='637856640180379073' IndexInText='15' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'/>
							<ErrorsPositionDisplayValue value='15' type='System.Int32' />
						</OtherProperties>
					</Custom>
				</Prefixes>
				<RegularItems>
					<Literal Id='637856640180379686' IndexInText='30' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='F1' Id='637856640180379680' IndexInText='30' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<OpeningRoundBrace Name='(' Id='637856640180379949' IndexInText='32' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<BinaryOperator Name=':' Priority='0' Id='637856640180380319' IndexInText='33' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.Literal Id='637856640180380043' IndexInText='33' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='x' Id='637856640180380038' IndexInText='33' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand1.Literal>
						<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637856640180380098' IndexInText='34' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<Name Name=':' Id='637856640180380089' IndexInText='34' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OperatorNameParts>
						</OperatorInfo>
						<Operand2.Literal Id='637856640180380277' IndexInText='35' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='T1' Id='637856640180380273' IndexInText='35' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand2.Literal>
					</BinaryOperator>
					<Comma Name=',' Id='637856640180380348' IndexInText='37' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<BinaryOperator Name=':' Priority='0' Id='637856640180380648' IndexInText='39' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.Literal Id='637856640180380426' IndexInText='39' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='y' Id='637856640180380422' IndexInText='39' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand1.Literal>
						<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637856640180380479' IndexInText='40' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<Name Name=':' Id='637856640180380474' IndexInText='40' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OperatorNameParts>
						</OperatorInfo>
						<Operand2.Literal Id='637856640180380615' IndexInText='41' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='T2' Id='637856640180380611' IndexInText='41' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand2.Literal>
					</BinaryOperator>
					<ClosingRoundBrace Name=')' Id='637856640180380665' IndexInText='43' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Postfixes>
					<Custom Id='637856640180380747' IndexInText='45' ItemLength='37' Interface='UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IWhereCustomExpressionItem'>
						<RegularItems>
							<CustomExpressionItemPart Id='637856640180380753' IndexInText='45' ItemLength='12' Interface='UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem'>
								<RegularItems>
									<Keyword Name='where' Id='637856640180380690' IndexInText='45' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
										<LanguageKeywordInfo ObjectId='47' Id='637781062905654923' Keyword='where' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
									</Keyword>
									<Name Name='T1' Id='637856640180380792' IndexInText='51' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Name Name=':' Id='637856640180380800' IndexInText='53' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637856640180380815' IndexInText='54' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='int' Id='637856640180380808' IndexInText='54' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
								</RegularItems>
								<OtherProperties>
									<WhereKeyword Name='where' Id='637856640180380690' IndexInText='45' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'/>
									<TypeName Name='T1' Id='637856640180380792' IndexInText='51' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<TypeConstraints ObjectId='52' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640180380815' IndexInText='54' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</TypeConstraints>
								</OtherProperties>
							</CustomExpressionItemPart>
							<CustomExpressionItemPart Id='637856640180380831' IndexInText='58' ItemLength='15' Interface='UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem'>
								<RegularItems>
									<Keyword Name='where' Id='637856640180380827' IndexInText='58' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
										<LanguageKeywordInfo ObjectId='47' Id='637781062905654923' Keyword='where' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
									</Keyword>
									<Name Name='T2' Id='637856640180380841' IndexInText='64' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Name Name=':' Id='637856640180380846' IndexInText='66' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637856640180380859' IndexInText='67' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='double' Id='637856640180380853' IndexInText='67' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
								</RegularItems>
								<OtherProperties>
									<WhereKeyword Name='where' Id='637856640180380827' IndexInText='58' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'/>
									<TypeName Name='T2' Id='637856640180380841' IndexInText='64' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<TypeConstraints ObjectId='59' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640180380859' IndexInText='67' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</TypeConstraints>
								</OtherProperties>
							</CustomExpressionItemPart>
							<Name Name='whereend' Id='637856640180380882' IndexInText='74' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<CustomExpressionItemPart Id='637856640180380753' IndexInText='45' ItemLength='12' Interface='UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem'/>
							<CustomExpressionItemPart Id='637856640180380831' IndexInText='58' ItemLength='15' Interface='UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem'/>
						</Children>
						<OtherProperties>
							<WhereEndMarker Name='whereend' Id='637856640180380882' IndexInText='74' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<GenericTypeDataExpressionItems ObjectId='61' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem]'>
								<UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem Id='637856640180380753' IndexInText='45' ItemLength='12'/>
								<UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem Id='637856640180380831' IndexInText='58' ItemLength='15'/>
							</GenericTypeDataExpressionItems>
							<ErrorsPositionDisplayValue value='45' type='System.Int32' />
						</OtherProperties>
					</Custom>
					<CodeBlock Id='637856640180380944' IndexInText='84' ItemLength='101' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
						<RegularItems>
							<CodeBlockStartMarker Name='{' Id='637856640180380938' IndexInText='84' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637856640180380995' IndexInText='184' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<OtherProperties>
							<CodeBlockStartMarker Name='{' Id='637856640180380938' IndexInText='84' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637856640180380995' IndexInText='184' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OtherProperties>
					</CodeBlock>
				</Postfixes>
				<Children>
					<BinaryOperator Name=':' Priority='0' Id='637856640180380319' IndexInText='33' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
					<BinaryOperator Name=':' Priority='0' Id='637856640180380648' IndexInText='39' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
				</Children>
				<OtherProperties>
					<NamedExpressionItem Id='637856640180379686' IndexInText='30' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					<OpeningBraceInfo Name='(' Id='637856640180379949' IndexInText='32' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ClosingBraceInfo Name=')' Id='637856640180380665' IndexInText='43' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Parameters ObjectId='65' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name=':' Priority='0' Id='637856640180380319' IndexInText='33' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name=':' Priority='0' Id='637856640180380648' IndexInText='39' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
					</Parameters>
				</OtherProperties>
			</Braces>
		</RegularItems>
		<Children>
			<BinaryOperator Name='+' Priority='30' Id='637856640180378990' IndexInText='0' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
			<Braces Id='637856640180379941' IndexInText='15' ItemLength='170' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
		</Children>
	</ExpressionItemSeries>
	<ParseErrorData ObjectId='66' HasCriticalErrors='False' Interface='UniversalExpressionParser.IParseErrorData'>
		<AllCodeItemErrors ObjectId='67' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICodeItemParseErrorData]'/>
	</ParseErrorData>
	<SortedCommentedOutCodeInfos ObjectId='68' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICommentedOutCodeInfo]'>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='69' IsLineComment='True' IndexInText='88' ItemLength='94'/>
	</SortedCommentedOutCodeInfos>
	<MainExpressionItem Id='637856640180378241' IndexInText='0' ItemLength='185' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'/>
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
	<ExpressionItemSeries Id='637856640180300687' IndexInText='186' ItemLength='892' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'>
		<RegularItems>
			<Braces Id='637856640180320497' IndexInText='186' ItemLength='892' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
				<Prefixes>
					<Custom Id='637856640180310261' IndexInText='186' ItemLength='142' Interface='UniversalExpressionParser.ExpressionItems.Custom.IKeywordBasedCustomExpressionItem'>
						<RegularItems>
							<Keyword Name='::metadata' Id='637856640180300953' IndexInText='186' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
								<LanguageKeywordInfo ObjectId='5' Id='637781063212876967' Keyword='::metadata' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
							</Keyword>
							<CodeBlock Id='637856640180308862' IndexInText='197' ItemLength='131' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
								<RegularItems>
									<CodeBlockStartMarker Name='{' Id='637856640180308826' IndexInText='197' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<BinaryOperator Name=':' Priority='0' Id='637856640180309568' IndexInText='198' ItemLength='129' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
										<Operand1.Literal Id='637856640180309222' IndexInText='198' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
											<NameExpressionItem Name='description' Id='637856640180309215' IndexInText='198' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</Operand1.Literal>
										<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637856640180309304' IndexInText='209' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
											<OperatorNameParts>
												<Name Name=':' Id='637856640180309292' IndexInText='209' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											</OperatorNameParts>
										</OperatorInfo>
										<Operand2.ConstantText Id='637856640180309502' IndexInText='211' ItemLength='116' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
											<NameExpressionItem Name='"F1 demoes regular function expression item to which multiple prefix and postfix custom expression items are added."' Id='637856640180309495' IndexInText='211' ItemLength='116' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</Operand2.ConstantText>
									</BinaryOperator>
									<CodeBlockEndMarker Name='}' Id='637856640180309617' IndexInText='327' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Children>
									<BinaryOperator Name=':' Priority='0' Id='637856640180309568' IndexInText='198' ItemLength='129' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
								</Children>
								<OtherProperties>
									<CodeBlockStartMarker Name='{' Id='637856640180308826' IndexInText='197' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<CodeBlockEndMarker Name='}' Id='637856640180309617' IndexInText='327' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</OtherProperties>
							</CodeBlock>
						</RegularItems>
						<OtherProperties>
							<LastKeywordExpressionItem Name='::metadata' Id='637856640180300953' IndexInText='186' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'/>
							<ErrorsPositionDisplayValue value='186' type='System.Int32' />
						</OtherProperties>
					</Custom>
					<Custom Id='637856640180316314' IndexInText='496' ItemLength='14' Interface='UniversalExpressionParser.ExpressionItems.Custom.IKeywordBasedCustomExpressionItem'>
						<RegularItems>
							<Keyword Name='::types' Id='637856640180311087' IndexInText='496' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
								<LanguageKeywordInfo ObjectId='18' Id='637781062811583137' Keyword='::types' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
							</Keyword>
							<Braces Id='637856640180315295' IndexInText='503' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
								<RegularItems>
									<OpeningSquareBrace Name='[' Id='637856640180315312' IndexInText='503' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637856640180315576' IndexInText='504' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='T1' Id='637856640180315569' IndexInText='504' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<Comma Name=',' Id='637856640180315622' IndexInText='506' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637856640180315707' IndexInText='507' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='T2' Id='637856640180315703' IndexInText='507' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<ClosingSquareBrace Name=']' Id='637856640180315739' IndexInText='509' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Children>
									<Literal Id='637856640180315576' IndexInText='504' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<Literal Id='637856640180315707' IndexInText='507' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								</Children>
								<OtherProperties>
									<OpeningBraceInfo Name='[' Id='637856640180315312' IndexInText='503' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ClosingBraceInfo Name=']' Id='637856640180315739' IndexInText='509' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
									<Parameters ObjectId='27' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640180315576' IndexInText='504' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640180315707' IndexInText='507' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</Parameters>
								</OtherProperties>
							</Braces>
						</RegularItems>
						<OtherProperties>
							<LastKeywordExpressionItem Name='::types' Id='637856640180311087' IndexInText='496' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'/>
							<ErrorsPositionDisplayValue value='496' type='System.Int32' />
						</OtherProperties>
					</Custom>
				</Prefixes>
				<RegularItems>
					<Literal Id='637856640180320356' IndexInText='512' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='F1' Id='637856640180320337' IndexInText='512' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<OpeningRoundBrace Name='(' Id='637856640180320505' IndexInText='514' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<BinaryOperator Name=':' Priority='0' Id='637856640180320892' IndexInText='515' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.Literal Id='637856640180320625' IndexInText='515' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='x' Id='637856640180320619' IndexInText='515' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand1.Literal>
						<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637856640180320684' IndexInText='516' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<Name Name=':' Id='637856640180320673' IndexInText='516' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OperatorNameParts>
						</OperatorInfo>
						<Operand2.Literal Id='637856640180320836' IndexInText='517' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='T1' Id='637856640180320832' IndexInText='517' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand2.Literal>
					</BinaryOperator>
					<Comma Name=',' Id='637856640180320938' IndexInText='519' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<BinaryOperator Name=':' Priority='0' Id='637856640180321247' IndexInText='521' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.Literal Id='637856640180321026' IndexInText='521' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='y' Id='637856640180321017' IndexInText='521' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand1.Literal>
						<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637856640180321072' IndexInText='522' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<Name Name=':' Id='637856640180321067' IndexInText='522' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OperatorNameParts>
						</OperatorInfo>
						<Operand2.Literal Id='637856640180321214' IndexInText='523' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='T2' Id='637856640180321209' IndexInText='523' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand2.Literal>
					</BinaryOperator>
					<Comma Name=',' Id='637856640180321270' IndexInText='525' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<BinaryOperator Name=':' Priority='0' Id='637856640180321574' IndexInText='527' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.Literal Id='637856640180321352' IndexInText='527' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='z' Id='637856640180321347' IndexInText='527' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand1.Literal>
						<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637856640180321402' IndexInText='528' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<Name Name=':' Id='637856640180321397' IndexInText='528' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OperatorNameParts>
						</OperatorInfo>
						<Operand2.Literal Id='637856640180321541' IndexInText='529' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='T3' Id='637856640180321536' IndexInText='529' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand2.Literal>
					</BinaryOperator>
					<ClosingRoundBrace Name=')' Id='637856640180321591' IndexInText='531' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Postfixes>
					<Custom Id='637856640180331914' IndexInText='721' ItemLength='43' Interface='UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IWhereCustomExpressionItem'>
						<RegularItems>
							<CustomExpressionItemPart Id='637856640180332744' IndexInText='721' ItemLength='18' Interface='UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem'>
								<RegularItems>
									<Keyword Name='where' Id='637856640180321640' IndexInText='721' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
										<LanguageKeywordInfo ObjectId='58' Id='637781062905654923' Keyword='where' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
									</Keyword>
									<Name Name='T1' Id='637856640180334000' IndexInText='727' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Name Name=':' Id='637856640180335015' IndexInText='729' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637856640180335555' IndexInText='730' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='int' Id='637856640180335063' IndexInText='730' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<Comma Name=',' Id='637856640180336380' IndexInText='733' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637856640180336433' IndexInText='734' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='class' Id='637856640180336423' IndexInText='734' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
								</RegularItems>
								<OtherProperties>
									<WhereKeyword Name='where' Id='637856640180321640' IndexInText='721' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'/>
									<TypeName Name='T1' Id='637856640180334000' IndexInText='727' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<TypeConstraints ObjectId='66' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640180335555' IndexInText='730' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640180336433' IndexInText='734' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</TypeConstraints>
								</OtherProperties>
							</CustomExpressionItemPart>
							<CustomExpressionItemPart Id='637856640180337309' IndexInText='740' ItemLength='15' Interface='UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem'>
								<RegularItems>
									<Keyword Name='where' Id='637856640180337294' IndexInText='740' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
										<LanguageKeywordInfo ObjectId='58' Id='637781062905654923' Keyword='where' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
									</Keyword>
									<Name Name='T2' Id='637856640180337409' IndexInText='746' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Name Name=':' Id='637856640180337418' IndexInText='748' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637856640180337434' IndexInText='749' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='double' Id='637856640180337427' IndexInText='749' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
								</RegularItems>
								<OtherProperties>
									<WhereKeyword Name='where' Id='637856640180337294' IndexInText='740' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'/>
									<TypeName Name='T2' Id='637856640180337409' IndexInText='746' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<TypeConstraints ObjectId='73' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640180337434' IndexInText='749' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</TypeConstraints>
								</OtherProperties>
							</CustomExpressionItemPart>
							<Name Name='whereend' Id='637856640180339198' IndexInText='756' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<CustomExpressionItemPart Id='637856640180332744' IndexInText='721' ItemLength='18' Interface='UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem'/>
							<CustomExpressionItemPart Id='637856640180337309' IndexInText='740' ItemLength='15' Interface='UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem'/>
						</Children>
						<OtherProperties>
							<WhereEndMarker Name='whereend' Id='637856640180339198' IndexInText='756' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<GenericTypeDataExpressionItems ObjectId='75' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem]'>
								<UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem Id='637856640180332744' IndexInText='721' ItemLength='18'/>
								<UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem Id='637856640180337309' IndexInText='740' ItemLength='15'/>
							</GenericTypeDataExpressionItems>
							<ErrorsPositionDisplayValue value='721' type='System.Int32' />
						</OtherProperties>
					</Custom>
					<Custom Id='637856640180340589' IndexInText='944' ItemLength='22' Interface='UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IWhereCustomExpressionItem'>
						<RegularItems>
							<CustomExpressionItemPart Id='637856640180340597' IndexInText='944' ItemLength='13' Interface='UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem'>
								<RegularItems>
									<Keyword Name='where' Id='637856640180340470' IndexInText='944' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
										<LanguageKeywordInfo ObjectId='58' Id='637781062905654923' Keyword='where' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
									</Keyword>
									<Name Name='T3' Id='637856640180340629' IndexInText='950' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Name Name=':' Id='637856640180340637' IndexInText='953' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637856640180340651' IndexInText='955' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='T1' Id='637856640180340644' IndexInText='955' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
								</RegularItems>
								<OtherProperties>
									<WhereKeyword Name='where' Id='637856640180340470' IndexInText='944' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'/>
									<TypeName Name='T3' Id='637856640180340629' IndexInText='950' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<TypeConstraints ObjectId='83' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640180340651' IndexInText='955' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</TypeConstraints>
								</OtherProperties>
							</CustomExpressionItemPart>
							<Name Name='whereend' Id='637856640180340678' IndexInText='958' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<CustomExpressionItemPart Id='637856640180340597' IndexInText='944' ItemLength='13' Interface='UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem'/>
						</Children>
						<OtherProperties>
							<WhereEndMarker Name='whereend' Id='637856640180340678' IndexInText='958' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<GenericTypeDataExpressionItems ObjectId='85' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem]'>
								<UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem Id='637856640180340597' IndexInText='944' ItemLength='13'/>
							</GenericTypeDataExpressionItems>
							<ErrorsPositionDisplayValue value='944' type='System.Int32' />
						</OtherProperties>
					</Custom>
					<CodeBlock Id='637856640180340927' IndexInText='969' ItemLength='109' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
						<RegularItems>
							<CodeBlockStartMarker Name='{' Id='637856640180340920' IndexInText='969' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637856640180340979' IndexInText='1077' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<OtherProperties>
							<CodeBlockStartMarker Name='{' Id='637856640180340920' IndexInText='969' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637856640180340979' IndexInText='1077' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OtherProperties>
					</CodeBlock>
				</Postfixes>
				<Children>
					<BinaryOperator Name=':' Priority='0' Id='637856640180320892' IndexInText='515' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
					<BinaryOperator Name=':' Priority='0' Id='637856640180321247' IndexInText='521' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
					<BinaryOperator Name=':' Priority='0' Id='637856640180321574' IndexInText='527' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
				</Children>
				<OtherProperties>
					<NamedExpressionItem Id='637856640180320356' IndexInText='512' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					<OpeningBraceInfo Name='(' Id='637856640180320505' IndexInText='514' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ClosingBraceInfo Name=')' Id='637856640180321591' IndexInText='531' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Parameters ObjectId='89' Count='3' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name=':' Priority='0' Id='637856640180320892' IndexInText='515' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name=':' Priority='0' Id='637856640180321247' IndexInText='521' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name=':' Priority='0' Id='637856640180321574' IndexInText='527' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
					</Parameters>
				</OtherProperties>
			</Braces>
		</RegularItems>
		<Children>
			<Braces Id='637856640180320497' IndexInText='186' ItemLength='892' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
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
	<MainExpressionItem Id='637856640180300687' IndexInText='186' ItemLength='892' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'/>
</UniversalExpressionParser.ExpressionItems.IRootExpressionItemSeries>
```
</details>

## Implementing Custom Expression Parsers

For examples of custom expression item parsers look at some examples in demo project **UniversalExpressionParser.DemoExpressionLanguageProviders**.

The following demo implementations of **UniversalExpressionParser.ExpressionItems.Custom.ICustomExpressionItemParserByKeywordId** might be useful when implementing custom expression parses: 

- UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.WhereCustomExpressionItemParserBase
- UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.PragmaCustomExpressionItemParser
- UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.MetadataCustomExpressionItemParser

Also, these custom expression parser implementations demonstrate how to use the helper class **UniversalExpressionParser.IParseExpressionItemContext** that is passed as a parameter to 
method **DoParseCustomExpressionItem(IParseExpressionItemContext context,...)** in **UniversalExpressionParser.ExpressionItems.Custom.CustomExpressionItemParserByKeywordId** to parse the text at current position, as well as how to report errors, if any.

- To add a new custom expression parser, one needs to implement an interface **UniversalExpressionParser.ExpressionItems.Custom.ICustomExpressionItemParser** and make sure the property **CustomExpressionItemParsers** in interface **UniversalExpressionParser.IExpressionLanguageProvider** includes an instance of the implemented parser class.

- In most cases the default implementation **UniversalExpressionParser.ExpressionItems.Custom.AggregateCustomExpressionItemParser** of **UniversalExpressionParser.ExpressionItems.Custom.ICustomExpressionItemParser** can be used to initialize the list of all custom expression parers that will be used by **Universal Expression Parser**.
**UniversalExpressionParser.ExpressionItems.Custom.AggregateCustomExpressionItemParser** has a dependency on **IEnumerable&lt;ICustomExpressionItemParserByKeywordId&gt;** (injected into constructor).

- Using a single instance of **AggregateCustomExpressionItemParser** in property **CustomExpressionItemParsers** in interface **UniversalExpressionParser.IExpressionLanguageProvider** instead of multiple custom expression parsers in this property improves the performance.
**AggregateCustomExpressionItemParser** keeps internally a mapping from keyword Id to all the instances of **UniversalExpressionParser.ExpressionItems.Custom.ICustomExpressionItemParserByKeywordId** injected in constructor. When the parser executes the method **TryParseCustomExpressionItem(...,IReadOnlyList<IKeywordExpressionItem> parsedKeywordExpressionItems,...)** in interface **UniversalExpressionParser.ExpressionItems.Custom**, the custom expression item parser of type **AggregateCustomExpressionItemParser** evaluates the last keyword in list in parameter **parsedKeywordExpressionItems** to retrieve all the parsers mapped to this keyword Id, to try to parse a custom expression item using only those custom expression item parsers. 

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

- Here is the code from demo custom expression item parser **UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.PragmaCustomExpressionItemParser** 

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

The interface **UniversalExpressionParser.IExpressionLanguageProvider** has properties **string LineCommentMarker { get; }**, **string MultilineCommentStartMarker { get; }**, and **string MultilineCommentEndMarker { get; }** for specifying comment markers.

If the values of these properties are not null, line and code block comments can be used.

The abstract implementation **UniversalExpressionParser.ExpressionLanguageProviderBase** of **UniversalExpressionParser.IExpressionLanguageProvider** overrides these properties to return "//", "/*", and "*/" (the values of these properties can be overridden in subclasses).

The on commented out code data is stored in property **IReadOnlyList&lt;UniversalExpressionParser.ICommentedOutCodeInfo&gt; SortedCommentedOutCodeInfos { get; }** in **UniversalExpressionParser.ExpressionItems.IRootExpressionItem**, an instance of which is returned by the call to method **UniversalExpressionParser.IExpressionParserOptions.ParseExpression(...)**.

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
	<ExpressionItemSeries Id='637856640180049987' IndexInText='17' ItemLength='140' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'>
		<RegularItems>
			<BinaryOperator Name='=' Priority='2000' Id='637856640180140749' IndexInText='17' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
				<Operand1.Literal Id='637856640180050354' IndexInText='17' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
					<NameExpressionItem Name='x' Id='637856640180050328' IndexInText='21' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<AppliedKeywords>
						<Keyword Name='var' Id='637856640180050208' IndexInText='17' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
							<LanguageKeywordInfo ObjectId='6' Id='637781064051574641' Keyword='var' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
						</Keyword>
					</AppliedKeywords>
				</Operand1.Literal>
				<OperatorInfo OperatorType='BinaryOperator' Name='=' Priority='2000' Id='637856640180050452' IndexInText='23' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
					<OperatorNameParts>
						<Name Name='=' Id='637856640180050441' IndexInText='23' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</OperatorNameParts>
				</OperatorInfo>
				<Operand2.BinaryOperator Name='*' Priority='20' Id='637856640180140776' IndexInText='25' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
					<Operand1.ConstantNumericValue Id='637856640180139895' IndexInText='25' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
						<NameExpressionItem Name='5' Id='637856640180139677' IndexInText='25' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<OtherProperties>
							<SucceededNumericTypeDescriptor ObjectId='12' NumberTypeId='1581134183960' Interface='UniversalExpressionParser.NumericTypeDescriptor'>
								<RegularExpressions ObjectId='13' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[System.String]'>
									<System.String value='^\d+' />
								</RegularExpressions>
							</SucceededNumericTypeDescriptor>
						</OtherProperties>
					</Operand1.ConstantNumericValue>
					<OperatorInfo OperatorType='BinaryOperator' Name='*' Priority='20' Id='637856640180140451' IndexInText='26' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
						<OperatorNameParts>
							<Name Name='*' Id='637856640180140437' IndexInText='26' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OperatorNameParts>
					</OperatorInfo>
					<Operand2.Literal Id='637856640180140684' IndexInText='27' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='y' Id='637856640180140678' IndexInText='27' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Operand2.Literal>
				</Operand2.BinaryOperator>
			</BinaryOperator>
			<ExpressionSeparator Name=';' Id='637856640180140851' IndexInText='28' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<Braces Id='637856640180141237' IndexInText='58' ItemLength='98' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
				<RegularItems>
					<Literal Id='637856640180141036' IndexInText='58' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='println' Id='637856640180141030' IndexInText='58' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<OpeningRoundBrace Name='(' Id='637856640180141240' IndexInText='65' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<BinaryOperator Name='+' Priority='30' Id='637856640180277531' IndexInText='66' ItemLength='89' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.BinaryOperator Name='+' Priority='30' Id='637856640180277498' IndexInText='66' ItemLength='84' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
							<Operand1.Literal Id='637856640180141351' IndexInText='66' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='x' Id='637856640180141346' IndexInText='66' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Operand1.Literal>
							<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637856640180141424' IndexInText='68' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
								<OperatorNameParts>
									<Name Name='+' Id='637856640180141399' IndexInText='68' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</OperatorNameParts>
							</OperatorInfo>
							<Operand2.Literal Id='637856640180141801' IndexInText='149' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='y' Id='637856640180141795' IndexInText='149' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Operand2.Literal>
						</Operand1.BinaryOperator>
						<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637856640180141862' IndexInText='150' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<Name Name='+' Id='637856640180141856' IndexInText='150' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OperatorNameParts>
						</OperatorInfo>
						<Operand2.BinaryOperator Name='*' Priority='20' Id='637856640180277543' IndexInText='151' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
							<Operand1.ConstantNumericValue Id='637856640180276963' IndexInText='151' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
								<NameExpressionItem Name='10' Id='637856640180276904' IndexInText='151' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<OtherProperties>
									<SucceededNumericTypeDescriptor ObjectId='12' NumberTypeId='1581134183960' Interface='UniversalExpressionParser.NumericTypeDescriptor'/>
								</OtherProperties>
							</Operand1.ConstantNumericValue>
							<OperatorInfo OperatorType='BinaryOperator' Name='*' Priority='20' Id='637856640180277263' IndexInText='153' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
								<OperatorNameParts>
									<Name Name='*' Id='637856640180277249' IndexInText='153' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</OperatorNameParts>
							</OperatorInfo>
							<Operand2.Literal Id='637856640180277440' IndexInText='154' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='z' Id='637856640180277435' IndexInText='154' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Operand2.Literal>
						</Operand2.BinaryOperator>
					</BinaryOperator>
					<ClosingRoundBrace Name=')' Id='637856640180277617' IndexInText='155' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Children>
					<BinaryOperator Name='+' Priority='30' Id='637856640180277531' IndexInText='66' ItemLength='89' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
				</Children>
				<OtherProperties>
					<NamedExpressionItem Id='637856640180141036' IndexInText='58' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					<OpeningBraceInfo Name='(' Id='637856640180141240' IndexInText='65' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ClosingBraceInfo Name=')' Id='637856640180277617' IndexInText='155' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Parameters ObjectId='41' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name='+' Priority='30' Id='637856640180277531' IndexInText='66' ItemLength='89' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
					</Parameters>
				</OtherProperties>
			</Braces>
			<ExpressionSeparator Name=';' Id='637856640180277668' IndexInText='156' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
		</RegularItems>
		<Children>
			<BinaryOperator Name='=' Priority='2000' Id='637856640180140749' IndexInText='17' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
			<Braces Id='637856640180141237' IndexInText='58' ItemLength='98' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
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
	<MainExpressionItem Id='637856640180049987' IndexInText='17' ItemLength='140' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'/>
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

Parse error data is stored in property **IReadOnlyList<ICodeItemParseErrorData> AllCodeItemErrors { get; }** in interface **UniversalExpressionParser.ExpressionItems.IRootExpressionItem** an instance of which is returned by the parser.

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

The extensions class **UniversalExpressionParser.ExpressionItems.RootExpressionItemExtensionMethods** has a helper method **string GetErrorContext(this IRootExpressionItem parsedRootExpressionItem, int parsedTextStartPosition, int parsedTextEnd, int maxNumberOfCharactersToShowBeforeOrAfterErrorPosition = 50)** for returning a string with error details and contextual data (i.e., text before and after the position where error happened, along with arrow pointing to the error). 

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
	<ExpressionItemSeries Id='637856640180407835' IndexInText='0' ItemLength='301' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'>
		<RegularItems>
			<Operators Id='637856640180420929' IndexInText='0' ItemLength='39' Interface='UniversalExpressionParser.ExpressionItems.IComplexExpressionItem'>
				<RegularItems>
					<Literal Id='637856640180408206' IndexInText='0' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='x' Id='637856640180408201' IndexInText='4' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<AppliedKeywords>
							<Keyword Name='var' Id='637856640180408064' IndexInText='0' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
								<LanguageKeywordInfo ObjectId='6' Id='637781064051574641' Keyword='var' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
							</Keyword>
						</AppliedKeywords>
					</Literal>
					<OperatorInfo OperatorType='BinaryOperator' Name='=' Priority='2000' Id='637856640180408309' IndexInText='6' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
						<OperatorNameParts>
							<Name Name='=' Id='637856640180408295' IndexInText='6' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OperatorNameParts>
					</OperatorInfo>
					<Literal Id='637856640180408477' IndexInText='8' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='y' Id='637856640180408447' IndexInText='8' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<Literal Id='637856640180419245' IndexInText='38' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='x' Id='637856640180419212' IndexInText='38' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
				</RegularItems>
			</Operators>
			<ExpressionSeparator Name=';' Id='637856640180421347' IndexInText='39' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<CodeBlock Id='637856640180421433' IndexInText='44' ItemLength='257' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
				<RegularItems>
					<CodeBlockStartMarker Name='{' Id='637856640180421427' IndexInText='44' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Braces Id='637856640180421887' IndexInText='84' ItemLength='217' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<Literal Id='637856640180421839' IndexInText='84' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='f1' Id='637856640180421828' IndexInText='84' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<OpeningRoundBrace Name='(' Id='637856640180421891' IndexInText='86' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Literal Id='637856640180422006' IndexInText='87' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='x' Id='637856640180422002' IndexInText='87' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<Comma Name=',' Id='637856640180422042' IndexInText='88' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Literal Id='637856640180422123' IndexInText='90' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='y' Id='637856640180422119' IndexInText='90' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<Comma Name=',' Id='637856640180422149' IndexInText='91' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingRoundBrace Name=')' Id='637856640180422284' IndexInText='131' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Postfixes>
							<CodeBlock Id='637856640180422333' IndexInText='138' ItemLength='163' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
								<RegularItems>
									<CodeBlockStartMarker Name='{' Id='637856640180422328' IndexInText='138' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Operators Id='637856640180424190' IndexInText='151' ItemLength='17' Interface='UniversalExpressionParser.ExpressionItems.IComplexExpressionItem'>
										<RegularItems>
											<Literal Id='637856640180422470' IndexInText='151' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
												<NameExpressionItem Name='z' Id='637856640180422466' IndexInText='155' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												<AppliedKeywords>
													<Keyword Name='var' Id='637856640180422367' IndexInText='151' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
														<LanguageKeywordInfo ObjectId='6' Id='637781064051574641' Keyword='var' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
													</Keyword>
												</AppliedKeywords>
											</Literal>
											<OperatorInfo OperatorType='BinaryOperator' Name='=' Priority='2000' Id='637856640180422561' IndexInText='157' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
												<OperatorNameParts>
													<Name Name='=' Id='637856640180422543' IndexInText='157' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												</OperatorNameParts>
											</OperatorInfo>
											<OperatorInfo OperatorType='PrefixUnaryOperator' Name='++' Priority='0' Id='637856640180422632' IndexInText='159' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
												<OperatorNameParts>
													<Name Name='++' Id='637856640180422624' IndexInText='159' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												</OperatorNameParts>
											</OperatorInfo>
											<Literal Id='637856640180422743' IndexInText='161' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
												<NameExpressionItem Name='x' Id='637856640180422738' IndexInText='161' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											</Literal>
											<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637856640180422858' IndexInText='163' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
												<OperatorNameParts>
													<Name Name='+' Id='637856640180422851' IndexInText='163' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												</OperatorNameParts>
											</OperatorInfo>
											<Literal Id='637856640180422992' IndexInText='165' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
												<NameExpressionItem Name='y' Id='637856640180422987' IndexInText='165' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											</Literal>
											<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637856640180423050' IndexInText='167' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
												<OperatorNameParts>
													<Name Name='+' Id='637856640180423044' IndexInText='167' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												</OperatorNameParts>
											</OperatorInfo>
										</RegularItems>
									</Operators>
									<ExpressionSeparator Name=';' Id='637856640180424228' IndexInText='218' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Operators Id='637856640180424770' IndexInText='229' ItemLength='64' Interface='UniversalExpressionParser.ExpressionItems.IComplexExpressionItem'>
										<RegularItems>
											<OperatorInfo OperatorType='PrefixUnaryOperator' Name='return' Priority='2147483647' Id='637856640180424315' IndexInText='229' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
												<OperatorNameParts>
													<Name Name='return' Id='637856640180424308' IndexInText='229' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												</OperatorNameParts>
											</OperatorInfo>
											<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637856640180424377' IndexInText='236' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
												<OperatorNameParts>
													<Name Name='+' Id='637856640180424372' IndexInText='236' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												</OperatorNameParts>
											</OperatorInfo>
											<Literal Id='637856640180424530' IndexInText='288' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
												<NameExpressionItem Name='z' Id='637856640180424525' IndexInText='288' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											</Literal>
											<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637856640180424606' IndexInText='290' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
												<OperatorNameParts>
													<Name Name='+' Id='637856640180424599' IndexInText='290' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												</OperatorNameParts>
											</OperatorInfo>
											<Literal Id='637856640180424732' IndexInText='292' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
												<NameExpressionItem Name='y' Id='637856640180424727' IndexInText='292' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											</Literal>
										</RegularItems>
									</Operators>
									<ExpressionSeparator Name=';' Id='637856640180424780' IndexInText='293' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<CodeBlockEndMarker Name='}' Id='637856640180424805' IndexInText='300' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Children>
									<Operators Id='637856640180424190' IndexInText='151' ItemLength='17' Interface='UniversalExpressionParser.ExpressionItems.IComplexExpressionItem'/>
									<Operators Id='637856640180424770' IndexInText='229' ItemLength='64' Interface='UniversalExpressionParser.ExpressionItems.IComplexExpressionItem'/>
								</Children>
								<OtherProperties>
									<CodeBlockStartMarker Name='{' Id='637856640180422328' IndexInText='138' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<CodeBlockEndMarker Name='}' Id='637856640180424805' IndexInText='300' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</OtherProperties>
							</CodeBlock>
						</Postfixes>
						<Children>
							<Literal Id='637856640180422006' IndexInText='87' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							<Literal Id='637856640180422123' IndexInText='90' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
						</Children>
						<OtherProperties>
							<NamedExpressionItem Id='637856640180421839' IndexInText='84' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							<OpeningBraceInfo Name='(' Id='637856640180421891' IndexInText='86' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=')' Id='637856640180422284' IndexInText='131' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Parameters ObjectId='59' Count='3' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640180422006' IndexInText='87' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640180422123' IndexInText='90' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase value='null' interface='UniversalExpressionParser.ExpressionItems.IExpressionItemBase' />
							</Parameters>
						</OtherProperties>
					</Braces>
				</RegularItems>
				<Children>
					<Braces Id='637856640180421887' IndexInText='84' ItemLength='217' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
				</Children>
				<OtherProperties>
					<CodeBlockStartMarker Name='{' Id='637856640180421427' IndexInText='44' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<CodeBlockEndMarker value='null' interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem' />
				</OtherProperties>
			</CodeBlock>
		</RegularItems>
		<Children>
			<Operators Id='637856640180420929' IndexInText='0' ItemLength='39' Interface='UniversalExpressionParser.ExpressionItems.IComplexExpressionItem'/>
			<CodeBlock Id='637856640180421433' IndexInText='44' ItemLength='257' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'/>
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
	<MainExpressionItem Id='637856640180407835' IndexInText='0' ItemLength='301' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'/>
</UniversalExpressionParser.ExpressionItems.IRootExpressionItemSeries>
```
</details>

- Below is the error text generated by using the helper extension method **UniversalExpressionParser.ExpressionItems.RootExpressionItemExtensionMethods.GetErrorContext(...)** for the errors reported by the parser for the expression above.

<details> <summary>Click to expand the file with reported error messages</summary>

```
2022-04-13 22:27:07,331 ERROR : Expression parse errors:

Parse error details: Error code: 200, Error index: 38. Error message: [No separation between two symbols. Here are some invalid expressions that would result in this error, and expressions resulted from invalid expressions by small correction.
Invalid expression: "F(x y)". Fixed valid expressions: "F(x + y)", "F(x, y)".
Invalid expression: "{x=F1(x) F2(y)}". Fixed valid expressions: "{x=F1(x) + F2(y)}", "{x=F1(x); F2(y)}".]
Error context:
var x = y /*operator is missing here*/x;
--------------------------------------↑

{ // This code block is not closed
    f1(x

Parse error details: Error code: 406, Error index: 44. Error message: [Code block end marker '}' is missing for '{'.]
Error context:
var x = y /*operator is missing here*/x;
----------------------------------------

{ // This code block is not closed
↑
    f1(x, y, /

Parse error details: Error code: 701, Error index: 92. Error message: [Valid expression is missing after comma.]
Error context:

{ // This code block is not closed
----------------------------------
    f1(x, y, /*function parameter is missing here*/)
------------↑
    {



Parse error details: Error code: 503, Error index: 167. Error message: [Expected a postfix operator.]
Error context:
missing here*/)
---------------
    {
-----

        var z = ++x + y + /*' +' is not a postfix and operand is missing *
------------------------↑

Parse error details: Error code: 502, Error index: 236. Error message: [Expected a prefix operator.]
Error context:
ostfix and operand is missing */;
---------------------------------
        return + /*' +' is not a postfix and operand is missing *
---------------↑
```
</details>

# Parsing Section in Text

- Sometimes we want to parse a single braces expression at specific location in text (i.e., an expression starting with "(" or "[" and ending in ")" or "]" correspondingly) or single code block expression (i.e., an expression starting with **UniversalExpressionParser.IExpressionLanguageProvider.CodeBlockStartMarker** and ending in **UniversalExpressionParser.IExpressionLanguageProvider.CodeBlockEndMarker**). In these scenarios, we want the parser to stop right after fully parsing the braces or code block expression.

- The interface **UniversalExpressionParser.IExpressionParser** has two methods for doing just that. 
 
- The methods for parsing single braces or code block expression are **UniversalExpressionParser.ExpressionItems.IRootBracesExpressionItem ParseBracesExpressionItem(IExpressionLanguageProvider expressionLanguageProvider, string expressionText, IParseBracesExpressionOptions parseBracesExpressionOptions)** and **IRootCodeBlockExpressionItem ParseCodeBlockExpressionItem(IExpressionLanguageProvider expressionLanguageProvider, string expressionText, IParseCodeBlockExpressionOptions parseCodeBlockExpressionOptions)**, and are demonstrated in sub-sections below.

- The parsed expression of type **UniversalExpressionParser.ExpressionItems.IRootExpressionItem** returned by these methods has a property **int PositionInTextOnCompletion { get; }** that stores the position in text, after the parsing is complete (i.e., the position after closing brace or code block end marker).

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

- The method **ParseBracesAtCurrentPosition(string expression, int positionInText)** in class **UniversalExpressionParser.Tests.Demos.ParseSingleBracesExpressionAtPositionDemo** (shown below) demonstrates how to parse the braces expression **(SALARY > 0 AND SALARY > MAX_SALARY/2 AND f1(SALARY) < f2(MAX_SALARY))**, by passing the position of opening brace in parameter **positionInText**.

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
	<BracesExpressionItem Id='637856640182505599' IndexInText='313' ItemLength='86' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
		<RegularItems>
			<OpeningRoundBrace Name='(' Id='637856640182505621' IndexInText='313' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<BinaryOperator Name='AND' Priority='80' Id='637856640182675695' IndexInText='314' ItemLength='84' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
				<Operand1.BinaryOperator Name='AND' Priority='80' Id='637856640182675667' IndexInText='314' ItemLength='44' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
					<Operand1.BinaryOperator Name='>' Priority='50' Id='637856640182675643' IndexInText='314' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.Literal Id='637856640182506969' IndexInText='314' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='SALARY' Id='637856640182506956' IndexInText='314' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand1.Literal>
						<OperatorInfo OperatorType='BinaryOperator' Name='>' Priority='50' Id='637856640182507056' IndexInText='321' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<Name Name='>' Id='637856640182507045' IndexInText='321' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OperatorNameParts>
						</OperatorInfo>
						<Operand2.ConstantNumericValue Id='637856640182588740' IndexInText='323' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
							<NameExpressionItem Name='0' Id='637856640182588696' IndexInText='323' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<OtherProperties>
								<SucceededNumericTypeDescriptor ObjectId='12' NumberTypeId='1581134183960' Interface='UniversalExpressionParser.NumericTypeDescriptor'>
									<RegularExpressions ObjectId='13' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[System.String]'>
										<System.String value='^\d+' />
									</RegularExpressions>
								</SucceededNumericTypeDescriptor>
							</OtherProperties>
						</Operand2.ConstantNumericValue>
					</Operand1.BinaryOperator>
					<OperatorInfo OperatorType='BinaryOperator' Name='AND' Priority='80' Id='637856640182589000' IndexInText='325' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
						<OperatorNameParts>
							<Name Name='AND' Id='637856640182588987' IndexInText='325' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OperatorNameParts>
					</OperatorInfo>
					<Operand2.BinaryOperator Name='>' Priority='50' Id='637856640182675675' IndexInText='337' ItemLength='21' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.Literal Id='637856640182589149' IndexInText='337' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='SALARY' Id='637856640182589145' IndexInText='337' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand1.Literal>
						<OperatorInfo OperatorType='BinaryOperator' Name='>' Priority='50' Id='637856640182589250' IndexInText='344' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<Name Name='>' Id='637856640182589215' IndexInText='344' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OperatorNameParts>
						</OperatorInfo>
						<Operand2.BinaryOperator Name='/' Priority='20' Id='637856640182675687' IndexInText='346' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
							<Operand1.Literal Id='637856640182589376' IndexInText='346' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='MAX_SALARY' Id='637856640182589372' IndexInText='346' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Operand1.Literal>
							<OperatorInfo OperatorType='BinaryOperator' Name='/' Priority='20' Id='637856640182589431' IndexInText='356' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
								<OperatorNameParts>
									<Name Name='/' Id='637856640182589425' IndexInText='356' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</OperatorNameParts>
							</OperatorInfo>
							<Operand2.ConstantNumericValue Id='637856640182674655' IndexInText='357' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
								<NameExpressionItem Name='2' Id='637856640182674611' IndexInText='357' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<OtherProperties>
									<SucceededNumericTypeDescriptor ObjectId='12' NumberTypeId='1581134183960' Interface='UniversalExpressionParser.NumericTypeDescriptor'/>
								</OtherProperties>
							</Operand2.ConstantNumericValue>
						</Operand2.BinaryOperator>
					</Operand2.BinaryOperator>
				</Operand1.BinaryOperator>
				<OperatorInfo OperatorType='BinaryOperator' Name='AND' Priority='80' Id='637856640182674934' IndexInText='359' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
					<OperatorNameParts>
						<Name Name='AND' Id='637856640182674922' IndexInText='359' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</OperatorNameParts>
				</OperatorInfo>
				<Operand2.BinaryOperator Name='<' Priority='50' Id='637856640182675701' IndexInText='371' ItemLength='27' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
					<Operand1.Braces Id='637856640182675119' IndexInText='371' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<Literal Id='637856640182675084' IndexInText='371' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='f1' Id='637856640182675079' IndexInText='371' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<OpeningRoundBrace Name='(' Id='637856640182675124' IndexInText='373' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Literal Id='637856640182675222' IndexInText='374' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='SALARY' Id='637856640182675216' IndexInText='374' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<ClosingRoundBrace Name=')' Id='637856640182675260' IndexInText='380' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<Literal Id='637856640182675222' IndexInText='374' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
						</Children>
						<OtherProperties>
							<NamedExpressionItem Id='637856640182675084' IndexInText='371' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							<OpeningBraceInfo Name='(' Id='637856640182675124' IndexInText='373' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=')' Id='637856640182675260' IndexInText='380' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Parameters ObjectId='38' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640182675222' IndexInText='374' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Operand1.Braces>
					<OperatorInfo OperatorType='BinaryOperator' Name='<' Priority='50' Id='637856640182675333' IndexInText='382' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
						<OperatorNameParts>
							<Name Name='<' Id='637856640182675323' IndexInText='382' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OperatorNameParts>
					</OperatorInfo>
					<Operand2.Braces Id='637856640182675489' IndexInText='384' ItemLength='14' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<Literal Id='637856640182675458' IndexInText='384' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='f2' Id='637856640182675453' IndexInText='384' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<OpeningRoundBrace Name='(' Id='637856640182675492' IndexInText='386' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Literal Id='637856640182675576' IndexInText='387' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='MAX_SALARY' Id='637856640182675571' IndexInText='387' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<ClosingRoundBrace Name=')' Id='637856640182675603' IndexInText='397' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<Literal Id='637856640182675576' IndexInText='387' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
						</Children>
						<OtherProperties>
							<NamedExpressionItem Id='637856640182675458' IndexInText='384' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							<OpeningBraceInfo Name='(' Id='637856640182675492' IndexInText='386' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=')' Id='637856640182675603' IndexInText='397' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Parameters ObjectId='48' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640182675576' IndexInText='387' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Operand2.Braces>
				</Operand2.BinaryOperator>
			</BinaryOperator>
			<ClosingRoundBrace Name=')' Id='637856640182675780' IndexInText='398' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
		</RegularItems>
		<Children>
			<BinaryOperator Name='AND' Priority='80' Id='637856640182675695' IndexInText='314' ItemLength='84' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
		</Children>
		<OtherProperties>
			<OpeningBraceInfo Name='(' Id='637856640182505621' IndexInText='313' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<ClosingBraceInfo Name=')' Id='637856640182675780' IndexInText='398' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
			<Parameters ObjectId='50' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
				<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name='AND' Priority='80' Id='637856640182675695' IndexInText='314' ItemLength='84' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
			</Parameters>
		</OtherProperties>
	</BracesExpressionItem>
	<ParseErrorData ObjectId='51' HasCriticalErrors='False' Interface='UniversalExpressionParser.IParseErrorData'>
		<AllCodeItemErrors ObjectId='52' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICodeItemParseErrorData]'/>
	</ParseErrorData>
	<SortedCommentedOutCodeInfos ObjectId='53' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICommentedOutCodeInfo]'/>
	<MainExpressionItem Id='637856640182505599' IndexInText='313' ItemLength='86' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
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
	<BracesExpressionItem Id='637856640182735342' IndexInText='22' ItemLength='28' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
		<RegularItems>
			<OpeningSquareBrace Name='[' Id='637856640182735372' IndexInText='22' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<BinaryOperator Name='+' Priority='30' Id='637856640182736208' IndexInText='23' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
				<Operand1.Braces Id='637856640182735781' IndexInText='23' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
					<RegularItems>
						<Literal Id='637856640182735736' IndexInText='23' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='f1' Id='637856640182735723' IndexInText='23' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Literal>
						<OpeningRoundBrace Name='(' Id='637856640182735785' IndexInText='25' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<ClosingRoundBrace Name=')' Id='637856640182735851' IndexInText='26' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</RegularItems>
					<OtherProperties>
						<NamedExpressionItem Id='637856640182735736' IndexInText='23' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
						<OpeningBraceInfo Name='(' Id='637856640182735785' IndexInText='25' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<ClosingBraceInfo Name=')' Id='637856640182735851' IndexInText='26' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<Parameters ObjectId='9' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'/>
					</OtherProperties>
				</Operand1.Braces>
				<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637856640182735935' IndexInText='27' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
					<OperatorNameParts>
						<Name Name='+' Id='637856640182735921' IndexInText='27' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</OperatorNameParts>
				</OperatorInfo>
				<Operand2.Braces Id='637856640182736129' IndexInText='28' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
					<RegularItems>
						<Literal Id='637856640182736085' IndexInText='28' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='m1' Id='637856640182736080' IndexInText='28' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Literal>
						<OpeningSquareBrace Name='[' Id='637856640182736132' IndexInText='30' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<ClosingSquareBrace Name=']' Id='637856640182736160' IndexInText='31' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</RegularItems>
					<OtherProperties>
						<NamedExpressionItem Id='637856640182736085' IndexInText='28' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
						<OpeningBraceInfo Name='[' Id='637856640182736132' IndexInText='30' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<ClosingBraceInfo Name=']' Id='637856640182736160' IndexInText='31' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<Parameters ObjectId='17' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'/>
					</OtherProperties>
				</Operand2.Braces>
			</BinaryOperator>
			<Comma Name=',' Id='637856640182736269' IndexInText='32' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<Literal Id='637856640182736351' IndexInText='34' ItemLength='15' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
				<NameExpressionItem Name='f2' Id='637856640182736347' IndexInText='34' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				<Postfixes>
					<CodeBlock Id='637856640182736393' IndexInText='38' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
						<RegularItems>
							<CodeBlockStartMarker Name='{' Id='637856640182736388' IndexInText='38' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<PrefixUnaryOperator Name='++' Priority='0' Id='637856640182736611' IndexInText='42' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
								<OperatorInfo OperatorType='PrefixUnaryOperator' Name='++' Priority='0' Id='637856640182736448' IndexInText='42' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
									<OperatorNameParts>
										<Name Name='++' Id='637856640182736442' IndexInText='42' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</OperatorNameParts>
								</OperatorInfo>
								<Operand1.Literal Id='637856640182736565' IndexInText='44' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
									<NameExpressionItem Name='i' Id='637856640182736561' IndexInText='44' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</Operand1.Literal>
							</PrefixUnaryOperator>
							<ExpressionSeparator Name=';' Id='637856640182736632' IndexInText='45' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637856640182736662' IndexInText='48' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<PrefixUnaryOperator Name='++' Priority='0' Id='637856640182736611' IndexInText='42' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
						</Children>
						<OtherProperties>
							<CodeBlockStartMarker Name='{' Id='637856640182736388' IndexInText='38' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637856640182736662' IndexInText='48' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OtherProperties>
					</CodeBlock>
				</Postfixes>
			</Literal>
			<ClosingSquareBrace Name=']' Id='637856640182736692' IndexInText='49' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
		</RegularItems>
		<Children>
			<BinaryOperator Name='+' Priority='30' Id='637856640182736208' IndexInText='23' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
			<Literal Id='637856640182736351' IndexInText='34' ItemLength='15' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
		</Children>
		<OtherProperties>
			<OpeningBraceInfo Name='[' Id='637856640182735372' IndexInText='22' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<ClosingBraceInfo Name=']' Id='637856640182736692' IndexInText='49' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
			<Parameters ObjectId='31' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
				<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name='+' Priority='30' Id='637856640182736208' IndexInText='23' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
				<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640182736351' IndexInText='34' ItemLength='15' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
			</Parameters>
		</OtherProperties>
	</BracesExpressionItem>
	<ParseErrorData ObjectId='32' HasCriticalErrors='False' Interface='UniversalExpressionParser.IParseErrorData'>
		<AllCodeItemErrors ObjectId='33' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICodeItemParseErrorData]'/>
	</ParseErrorData>
	<SortedCommentedOutCodeInfos ObjectId='34' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICommentedOutCodeInfo]'/>
	<MainExpressionItem Id='637856640182735342' IndexInText='22' ItemLength='28' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
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
	<CodeBlockExpressionItem Id='637856640182477858' IndexInText='28' ItemLength='29' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
		<RegularItems>
			<CodeBlockStartMarker Name='{' Id='637856640182477820' IndexInText='28' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<Braces Id='637856640182479385' IndexInText='31' ItemLength='23' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
				<RegularItems>
					<Literal Id='637856640182479336' IndexInText='31' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='f1' Id='637856640182479316' IndexInText='31' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<OpeningRoundBrace Name='(' Id='637856640182479389' IndexInText='33' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<BinaryOperator Name='+' Priority='30' Id='637856640182479890' IndexInText='34' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.Braces Id='637856640182479520' IndexInText='34' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
							<RegularItems>
								<Literal Id='637856640182479494' IndexInText='34' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
									<NameExpressionItem Name='f2' Id='637856640182479484' IndexInText='34' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</Literal>
								<OpeningRoundBrace Name='(' Id='637856640182479523' IndexInText='36' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<ClosingRoundBrace Name=')' Id='637856640182479559' IndexInText='37' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</RegularItems>
							<OtherProperties>
								<NamedExpressionItem Id='637856640182479494' IndexInText='34' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								<OpeningBraceInfo Name='(' Id='637856640182479523' IndexInText='36' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<ClosingBraceInfo Name=')' Id='637856640182479559' IndexInText='37' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<Parameters ObjectId='13' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'/>
							</OtherProperties>
						</Operand1.Braces>
						<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637856640182479633' IndexInText='38' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<Name Name='+' Id='637856640182479619' IndexInText='38' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OperatorNameParts>
						</OperatorInfo>
						<Operand2.Braces Id='637856640182479816' IndexInText='39' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
							<RegularItems>
								<Literal Id='637856640182479776' IndexInText='39' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
									<NameExpressionItem Name='m1' Id='637856640182479772' IndexInText='39' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</Literal>
								<OpeningSquareBrace Name='[' Id='637856640182479820' IndexInText='41' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<ClosingSquareBrace Name=']' Id='637856640182479848' IndexInText='42' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</RegularItems>
							<OtherProperties>
								<NamedExpressionItem Id='637856640182479776' IndexInText='39' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								<OpeningBraceInfo Name='[' Id='637856640182479820' IndexInText='41' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<ClosingBraceInfo Name=']' Id='637856640182479848' IndexInText='42' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<Parameters ObjectId='21' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'/>
							</OtherProperties>
						</Operand2.Braces>
					</BinaryOperator>
					<Comma Name=',' Id='637856640182479978' IndexInText='43' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Literal Id='637856640182480066' IndexInText='45' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='f2' Id='637856640182480061' IndexInText='45' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<Postfixes>
							<CodeBlock Id='637856640182480102' IndexInText='47' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
								<RegularItems>
									<CodeBlockStartMarker Name='{' Id='637856640182480097' IndexInText='47' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<PrefixUnaryOperator Name='++' Priority='0' Id='637856640182480300' IndexInText='48' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
										<OperatorInfo OperatorType='PrefixUnaryOperator' Name='++' Priority='0' Id='637856640182480150' IndexInText='48' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
											<OperatorNameParts>
												<Name Name='++' Id='637856640182480144' IndexInText='48' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											</OperatorNameParts>
										</OperatorInfo>
										<Operand1.Literal Id='637856640182480256' IndexInText='50' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
											<NameExpressionItem Name='i' Id='637856640182480251' IndexInText='50' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</Operand1.Literal>
									</PrefixUnaryOperator>
									<ExpressionSeparator Name=';' Id='637856640182480320' IndexInText='51' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<CodeBlockEndMarker Name='}' Id='637856640182480351' IndexInText='52' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Children>
									<PrefixUnaryOperator Name='++' Priority='0' Id='637856640182480300' IndexInText='48' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
								</Children>
								<OtherProperties>
									<CodeBlockStartMarker Name='{' Id='637856640182480097' IndexInText='47' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<CodeBlockEndMarker Name='}' Id='637856640182480351' IndexInText='52' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</OtherProperties>
							</CodeBlock>
						</Postfixes>
					</Literal>
					<ClosingRoundBrace Name=')' Id='637856640182480381' IndexInText='53' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Children>
					<BinaryOperator Name='+' Priority='30' Id='637856640182479890' IndexInText='34' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
					<Literal Id='637856640182480066' IndexInText='45' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
				</Children>
				<OtherProperties>
					<NamedExpressionItem Id='637856640182479336' IndexInText='31' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					<OpeningBraceInfo Name='(' Id='637856640182479389' IndexInText='33' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ClosingBraceInfo Name=')' Id='637856640182480381' IndexInText='53' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Parameters ObjectId='35' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name='+' Priority='30' Id='637856640182479890' IndexInText='34' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640182480066' IndexInText='45' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					</Parameters>
				</OtherProperties>
			</Braces>
			<CodeBlockEndMarker Name='}' Id='637856640182480411' IndexInText='56' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
		</RegularItems>
		<Children>
			<Braces Id='637856640182479385' IndexInText='31' ItemLength='23' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
		</Children>
		<OtherProperties>
			<CodeBlockStartMarker Name='{' Id='637856640182477820' IndexInText='28' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<CodeBlockEndMarker Name='}' Id='637856640182480411' IndexInText='56' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
		</OtherProperties>
	</CodeBlockExpressionItem>
	<ParseErrorData ObjectId='37' HasCriticalErrors='False' Interface='UniversalExpressionParser.IParseErrorData'>
		<AllCodeItemErrors ObjectId='38' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICodeItemParseErrorData]'/>
	</ParseErrorData>
	<SortedCommentedOutCodeInfos ObjectId='39' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICommentedOutCodeInfo]'/>
	<MainExpressionItem Id='637856640182477858' IndexInText='28' ItemLength='29' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'/>
</UniversalExpressionParser.ExpressionItems.IRootCodeBlockExpressionItem>
```
</details>

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
	<ExpressionItemSeries Id='637856640178930471' IndexInText='333' ItemLength='1171' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'>
		<RegularItems>
			<CodeBlock Id='637856640179023790' IndexInText='333' ItemLength='163' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
				<RegularItems>
					<CodeBlockStartMarker Name='BEGIN' Id='637856640179023009' IndexInText='333' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Braces Id='637856640179060188' IndexInText='344' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<Literal Id='637856640179055712' IndexInText='344' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='println' Id='637856640179055164' IndexInText='344' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<OpeningRoundBrace Name='(' Id='637856640179060204' IndexInText='351' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Literal Id='637856640179066130' IndexInText='352' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='x' Id='637856640179066116' IndexInText='352' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<ClosingRoundBrace Name=')' Id='637856640179080178' IndexInText='353' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<Literal Id='637856640179066130' IndexInText='352' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
						</Children>
						<OtherProperties>
							<NamedExpressionItem Id='637856640179055712' IndexInText='344' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							<OpeningBraceInfo Name='(' Id='637856640179060204' IndexInText='351' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=')' Id='637856640179080178' IndexInText='353' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Parameters ObjectId='11' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640179066130' IndexInText='352' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Braces>
					<ExpressionSeparator Name=';' Id='637856640179083641' IndexInText='354' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Braces Id='637856640179083864' IndexInText='356' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<Literal Id='637856640179083819' IndexInText='356' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='println' Id='637856640179083812' IndexInText='356' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<OpeningRoundBrace Name='(' Id='637856640179083868' IndexInText='363' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<BinaryOperator Name='+' Priority='30' Id='637856640179089898' IndexInText='364' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
								<Operand1.Literal Id='637856640179084079' IndexInText='364' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
									<NameExpressionItem Name='x' Id='637856640179084073' IndexInText='364' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</Operand1.Literal>
								<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637856640179085611' IndexInText='365' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
									<OperatorNameParts>
										<Name Name='+' Id='637856640179084152' IndexInText='365' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</OperatorNameParts>
								</OperatorInfo>
								<Operand2.Literal Id='637856640179086873' IndexInText='366' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
									<NameExpressionItem Name='y' Id='637856640179086861' IndexInText='366' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</Operand2.Literal>
							</BinaryOperator>
							<ClosingRoundBrace Name=')' Id='637856640179098697' IndexInText='367' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<BinaryOperator Name='+' Priority='30' Id='637856640179089898' IndexInText='364' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
						</Children>
						<OtherProperties>
							<NamedExpressionItem Id='637856640179083819' IndexInText='356' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							<OpeningBraceInfo Name='(' Id='637856640179083868' IndexInText='363' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=')' Id='637856640179098697' IndexInText='367' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Parameters ObjectId='25' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name='+' Priority='30' Id='637856640179089898' IndexInText='364' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Braces>
					<CodeBlockEndMarker Name='END' Id='637856640179098901' IndexInText='493' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Children>
					<Braces Id='637856640179060188' IndexInText='344' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
					<Braces Id='637856640179083864' IndexInText='356' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
				</Children>
				<OtherProperties>
					<CodeBlockStartMarker Name='BEGIN' Id='637856640179023009' IndexInText='333' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<CodeBlockEndMarker Name='END' Id='637856640179098901' IndexInText='493' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</OtherProperties>
			</CodeBlock>
			<Literal Id='637856640179101479' IndexInText='718' ItemLength='192' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
				<NameExpressionItem Name='DOG' Id='637856640179101465' IndexInText='731' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				<AppliedKeywords>
					<Keyword Name='PUBLIC' Id='637856640179100829' IndexInText='718' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
						<LanguageKeywordInfo ObjectId='30' Id='637783669980406419' Keyword='public' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
					</Keyword>
					<Keyword Name='Class' Id='637856640179100995' IndexInText='725' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
						<LanguageKeywordInfo ObjectId='32' Id='637783670388745402' Keyword='class' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
					</Keyword>
				</AppliedKeywords>
				<Postfixes>
					<CodeBlock Id='637856640179103541' IndexInText='737' ItemLength='173' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
						<RegularItems>
							<CodeBlockStartMarker Name='BEGIN' Id='637856640179103528' IndexInText='737' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Braces Id='637856640179106335' IndexInText='820' ItemLength='18' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
								<AppliedKeywords>
									<Keyword Name='PUBLIc' Id='637856640179105502' IndexInText='820' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
										<LanguageKeywordInfo ObjectId='30' Id='637783669980406419' Keyword='public' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
									</Keyword>
									<Keyword Name='static' Id='637856640179105526' IndexInText='827' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
										<LanguageKeywordInfo ObjectId='38' Id='637789181872233115' Keyword='static' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
									</Keyword>
								</AppliedKeywords>
								<RegularItems>
									<Literal Id='637856640179105697' IndexInText='834' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='F1' Id='637856640179105691' IndexInText='834' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<OpeningRoundBrace Name='(' Id='637856640179106353' IndexInText='836' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ClosingRoundBrace Name=')' Id='637856640179106445' IndexInText='837' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<OtherProperties>
									<NamedExpressionItem Id='637856640179105697' IndexInText='834' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<OpeningBraceInfo Name='(' Id='637856640179106353' IndexInText='836' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ClosingBraceInfo Name=')' Id='637856640179106445' IndexInText='837' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Parameters ObjectId='43' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'/>
								</OtherProperties>
							</Braces>
							<ExpressionSeparator Name=';' Id='637856640179106498' IndexInText='838' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='end' Id='637856640179106547' IndexInText='907' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<Braces Id='637856640179106335' IndexInText='820' ItemLength='18' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
						</Children>
						<OtherProperties>
							<CodeBlockStartMarker Name='BEGIN' Id='637856640179103528' IndexInText='737' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='end' Id='637856640179106547' IndexInText='907' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OtherProperties>
					</CodeBlock>
				</Postfixes>
			</Literal>
			<BinaryOperator Name='=' Priority='2000' Id='637856640179116294' IndexInText='970' ItemLength='16' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
				<Operand1.Literal Id='637856640179106697' IndexInText='970' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
					<NameExpressionItem Name='x' Id='637856640179106692' IndexInText='974' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<AppliedKeywords>
						<Keyword Name='VaR' Id='637856640179106582' IndexInText='970' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
							<LanguageKeywordInfo ObjectId='50' Id='637781064051574641' Keyword='var' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
						</Keyword>
					</AppliedKeywords>
				</Operand1.Literal>
				<OperatorInfo OperatorType='BinaryOperator' Name='=' Priority='2000' Id='637856640179106787' IndexInText='975' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
					<OperatorNameParts>
						<Name Name='=' Id='637856640179106779' IndexInText='975' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</OperatorNameParts>
				</OperatorInfo>
				<Operand2.Custom Id='637856640179114821' IndexInText='976' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.Custom.IKeywordBasedCustomExpressionItem'>
					<RegularItems>
						<Keyword Name='::PRagma' Id='637856640179106832' IndexInText='976' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
							<LanguageKeywordInfo ObjectId='55' Id='637781063127646487' Keyword='::pragma' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
						</Keyword>
						<Name Name='y' Id='637856640179112709' IndexInText='985' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</RegularItems>
					<OtherProperties>
						<LastKeywordExpressionItem Name='::PRagma' Id='637856640179106832' IndexInText='976' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'/>
						<ErrorsPositionDisplayValue value='976' type='System.Int32' />
					</OtherProperties>
				</Operand2.Custom>
			</BinaryOperator>
			<ExpressionSeparator Name=';' Id='637856640179116524' IndexInText='986' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<Braces Id='637856640179116696' IndexInText='991' ItemLength='63' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
				<RegularItems>
					<Literal Id='637856640179116660' IndexInText='991' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='PRintLN' Id='637856640179116655' IndexInText='991' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<OpeningRoundBrace Name='(' Id='637856640179116698' IndexInText='998' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<BinaryOperator Name='&&' Priority='80' Id='637856640179127917' IndexInText='999' ItemLength='54' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.BinaryOperator Name='+' Priority='30' Id='637856640179127884' IndexInText='999' ItemLength='32' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
							<Operand1.ConstantText Id='637856640179120531' IndexInText='999' ItemLength='16' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='"X IS NOT NULL="' Id='637856640179120514' IndexInText='999' ItemLength='16' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Operand1.ConstantText>
							<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637856640179120736' IndexInText='1016' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
								<OperatorNameParts>
									<Name Name='+' Id='637856640179120726' IndexInText='1016' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</OperatorNameParts>
							</OperatorInfo>
							<Operand2.PostfixUnaryOperator Name='IS NOT NULL' Priority='1' Id='637856640179127900' IndexInText='1018' ItemLength='13' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
								<Operand1.Literal Id='637856640179121645' IndexInText='1018' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
									<NameExpressionItem Name='X' Id='637856640179121634' IndexInText='1018' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</Operand1.Literal>
								<OperatorInfo OperatorType='PostfixUnaryOperator' Name='IS NOT NULL' Priority='1' Id='637856640179121763' IndexInText='1020' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
									<OperatorNameParts>
										<Name Name='Is' Id='637856640179121704' IndexInText='1020' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										<Name Name='noT' Id='637856640179121745' IndexInText='1023' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										<Name Name='Null' Id='637856640179121756' IndexInText='1027' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</OperatorNameParts>
								</OperatorInfo>
							</Operand2.PostfixUnaryOperator>
						</Operand1.BinaryOperator>
						<OperatorInfo OperatorType='BinaryOperator' Name='&&' Priority='80' Id='637856640179121873' IndexInText='1032' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<Name Name='&&' Id='637856640179121868' IndexInText='1032' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OperatorNameParts>
						</OperatorInfo>
						<Operand2.PostfixUnaryOperator Name='IS NULL' Priority='1' Id='637856640179127925' IndexInText='1035' ItemLength='18' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
							<Operand1.Custom Id='637856640179122112' IndexInText='1035' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.Custom.IKeywordBasedCustomExpressionItem'>
								<RegularItems>
									<Keyword Name='::pRAGMA' Id='637856640179121948' IndexInText='1035' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
										<LanguageKeywordInfo ObjectId='55' Id='637781063127646487' Keyword='::pragma' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
									</Keyword>
									<Name Name='y' Id='637856640179122102' IndexInText='1044' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<OtherProperties>
									<LastKeywordExpressionItem Name='::pRAGMA' Id='637856640179121948' IndexInText='1035' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'/>
									<ErrorsPositionDisplayValue value='1035' type='System.Int32' />
								</OtherProperties>
							</Operand1.Custom>
							<OperatorInfo OperatorType='PostfixUnaryOperator' Name='IS NULL' Priority='1' Id='637856640179127714' IndexInText='1046' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
								<OperatorNameParts>
									<Name Name='is' Id='637856640179126259' IndexInText='1046' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Name Name='NULL' Id='637856640179127692' IndexInText='1049' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</OperatorNameParts>
							</OperatorInfo>
						</Operand2.PostfixUnaryOperator>
					</BinaryOperator>
					<ClosingRoundBrace Name=')' Id='637856640179130032' IndexInText='1053' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Children>
					<BinaryOperator Name='&&' Priority='80' Id='637856640179127917' IndexInText='999' ItemLength='54' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
				</Children>
				<OtherProperties>
					<NamedExpressionItem Id='637856640179116660' IndexInText='991' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					<OpeningBraceInfo Name='(' Id='637856640179116698' IndexInText='998' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ClosingBraceInfo Name=')' Id='637856640179130032' IndexInText='1053' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Parameters ObjectId='85' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name='&&' Priority='80' Id='637856640179127917' IndexInText='999' ItemLength='54' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
					</Parameters>
				</OtherProperties>
			</Braces>
			<ExpressionSeparator Name=';' Id='637856640179130116' IndexInText='1054' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<Braces Id='637856640179130267' IndexInText='1059' ItemLength='445' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
				<RegularItems>
					<Literal Id='637856640179130230' IndexInText='1059' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='f1' Id='637856640179130225' IndexInText='1059' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<OpeningRoundBrace Name='(' Id='637856640179130270' IndexInText='1061' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Literal Id='637856640179130372' IndexInText='1062' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='x1' Id='637856640179130367' IndexInText='1062' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<Comma Name=',' Id='637856640179131118' IndexInText='1064' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Literal Id='637856640179131270' IndexInText='1066' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='y1' Id='637856640179131264' IndexInText='1066' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<ClosingRoundBrace Name=')' Id='637856640179131311' IndexInText='1068' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Postfixes>
					<CodeBlock Id='637856640179131364' IndexInText='1071' ItemLength='433' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
						<RegularItems>
							<CodeBlockStartMarker Name='BEGin' Id='637856640179131360' IndexInText='1071' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<PrefixUnaryOperator Name='return' Priority='2147483647' Id='637856640179131920' IndexInText='1388' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
								<OperatorInfo OperatorType='PrefixUnaryOperator' Name='return' Priority='2147483647' Id='637856640179131511' IndexInText='1388' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
									<OperatorNameParts>
										<Name Name='RETurN' Id='637856640179131503' IndexInText='1388' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</OperatorNameParts>
								</OperatorInfo>
								<Operand1.BinaryOperator Name='+' Priority='30' Id='637856640179131934' IndexInText='1395' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
									<Operand1.Literal Id='637856640179131645' IndexInText='1395' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='X1' Id='637856640179131641' IndexInText='1395' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Operand1.Literal>
									<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637856640179131723' IndexInText='1397' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
										<OperatorNameParts>
											<Name Name='+' Id='637856640179131717' IndexInText='1397' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</OperatorNameParts>
									</OperatorInfo>
									<Operand2.Literal Id='637856640179131877' IndexInText='1398' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='Y1' Id='637856640179131873' IndexInText='1398' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Operand2.Literal>
								</Operand1.BinaryOperator>
							</PrefixUnaryOperator>
							<ExpressionSeparator Name=';' Id='637856640179132038' IndexInText='1400' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='enD' Id='637856640179132093' IndexInText='1501' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<PrefixUnaryOperator Name='return' Priority='2147483647' Id='637856640179131920' IndexInText='1388' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
						</Children>
						<OtherProperties>
							<CodeBlockStartMarker Name='BEGin' Id='637856640179131360' IndexInText='1071' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='enD' Id='637856640179132093' IndexInText='1501' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OtherProperties>
					</CodeBlock>
				</Postfixes>
				<Children>
					<Literal Id='637856640179130372' IndexInText='1062' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					<Literal Id='637856640179131270' IndexInText='1066' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
				</Children>
				<OtherProperties>
					<NamedExpressionItem Id='637856640179130230' IndexInText='1059' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					<OpeningBraceInfo Name='(' Id='637856640179130270' IndexInText='1061' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ClosingBraceInfo Name=')' Id='637856640179131311' IndexInText='1068' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Parameters ObjectId='111' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640179130372' IndexInText='1062' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637856640179131270' IndexInText='1066' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					</Parameters>
				</OtherProperties>
			</Braces>
		</RegularItems>
		<Children>
			<CodeBlock Id='637856640179023790' IndexInText='333' ItemLength='163' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'/>
			<Literal Id='637856640179101479' IndexInText='718' ItemLength='192' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
			<BinaryOperator Name='=' Priority='2000' Id='637856640179116294' IndexInText='970' ItemLength='16' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
			<Braces Id='637856640179116696' IndexInText='991' ItemLength='63' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
			<Braces Id='637856640179130267' IndexInText='1059' ItemLength='445' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
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
	<MainExpressionItem Id='637856640178930471' IndexInText='333' ItemLength='1171' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'/>
</UniversalExpressionParser.ExpressionItems.IRootExpressionItemSeries>
```
</details>
