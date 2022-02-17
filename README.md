# UniversalExpressionParser

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
	<ExpressionItemSeries Id='637795220126429853' IndexInText='0' ItemLength='736' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'>
		<RegularItems>
			<BinaryOperator Name='=' Priority='2000' Id='637795220126431184' IndexInText='0' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
				<Operand1.Literal Id='637795220126430244' IndexInText='0' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
					<NameExpressionItem Name='z' Id='637795220126430238' IndexInText='4' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<AppliedKeywords>
						<Keyword Name='var' Id='637795220126430080' IndexInText='0' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
							<LanguageKeywordInfo ObjectId='6' Id='637781064051574641' Keyword='var' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
						</Keyword>
					</AppliedKeywords>
				</Operand1.Literal>
				<OperatorInfo OperatorType='BinaryOperator' Name='=' Priority='2000' Id='637795220126430368' IndexInText='6' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
					<OperatorNameParts>
						<Name Name='=' Id='637795220126430357' IndexInText='6' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</OperatorNameParts>
				</OperatorInfo>
				<Operand2.BinaryOperator Name='+' Priority='30' Id='637795220126431212' IndexInText='8' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
					<Operand1.BinaryOperator Name='*' Priority='20' Id='637795220126431204' IndexInText='8' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.Literal Id='637795220126430517' IndexInText='8' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='x1' Id='637795220126430511' IndexInText='8' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand1.Literal>
						<OperatorInfo OperatorType='BinaryOperator' Name='*' Priority='20' Id='637795220126430593' IndexInText='10' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<Name Name='*' Id='637795220126430587' IndexInText='10' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OperatorNameParts>
						</OperatorInfo>
						<Operand2.Literal Id='637795220126430735' IndexInText='11' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='y1' Id='637795220126430731' IndexInText='11' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand2.Literal>
					</Operand1.BinaryOperator>
					<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637795220126430798' IndexInText='13' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
						<OperatorNameParts>
							<Name Name='+' Id='637795220126430792' IndexInText='13' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OperatorNameParts>
					</OperatorInfo>
					<Operand2.BinaryOperator Name='*' Priority='20' Id='637795220126431219' IndexInText='14' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.Literal Id='637795220126430936' IndexInText='14' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='x2' Id='637795220126430927' IndexInText='14' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand1.Literal>
						<OperatorInfo OperatorType='BinaryOperator' Name='*' Priority='20' Id='637795220126430998' IndexInText='16' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<Name Name='*' Id='637795220126430992' IndexInText='16' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OperatorNameParts>
						</OperatorInfo>
						<Operand2.Literal Id='637795220126431137' IndexInText='17' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='y2' Id='637795220126431133' IndexInText='17' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand2.Literal>
					</Operand2.BinaryOperator>
				</Operand2.BinaryOperator>
			</BinaryOperator>
			<ExpressionSeparator Name=';' Id='637795220126431279' IndexInText='19' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<BinaryOperator Name='=' Priority='2000' Id='637795220126433575' IndexInText='24' ItemLength='116' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
				<Operand1.Literal Id='637795220126431409' IndexInText='24' ItemLength='30' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
					<NameExpressionItem Name='matrixMultiplicationResult' Id='637795220126431404' IndexInText='28' ItemLength='26' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<AppliedKeywords>
						<Keyword Name='var' Id='637795220126431300' IndexInText='24' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
							<LanguageKeywordInfo ObjectId='6' Id='637781064051574641' Keyword='var' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
						</Keyword>
					</AppliedKeywords>
				</Operand1.Literal>
				<OperatorInfo OperatorType='BinaryOperator' Name='=' Priority='2000' Id='637795220126431484' IndexInText='55' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
					<OperatorNameParts>
						<Name Name='=' Id='637795220126431478' IndexInText='55' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</OperatorNameParts>
				</OperatorInfo>
				<Operand2.BinaryOperator Name='*' Priority='20' Id='637795220126433586' IndexInText='57' ItemLength='83' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
					<Operand1.Braces Id='637795220126431597' IndexInText='57' ItemLength='40' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<OpeningSquareBrace Name='[' Id='637795220126431602' IndexInText='57' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Braces Id='637795220126431641' IndexInText='58' ItemLength='18' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
								<RegularItems>
									<OpeningSquareBrace Name='[' Id='637795220126431644' IndexInText='58' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637795220126431739' IndexInText='59' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='x1_1' Id='637795220126431735' IndexInText='59' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<Comma Name=',' Id='637795220126431782' IndexInText='63' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637795220126431867' IndexInText='65' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='x1_2' Id='637795220126431863' IndexInText='65' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<Comma Name=',' Id='637795220126431904' IndexInText='69' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637795220126431988' IndexInText='71' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='x1_3' Id='637795220126431984' IndexInText='71' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<ClosingSquareBrace Name=']' Id='637795220126432019' IndexInText='75' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Children>
									<Literal Id='637795220126431739' IndexInText='59' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<Literal Id='637795220126431867' IndexInText='65' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<Literal Id='637795220126431988' IndexInText='71' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								</Children>
								<OtherProperties>
									<OpeningBraceInfo Name='[' Id='637795220126431644' IndexInText='58' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ClosingBraceInfo Name=']' Id='637795220126432019' IndexInText='75' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
									<Parameters ObjectId='47' Count='3' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220126431739' IndexInText='59' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220126431867' IndexInText='65' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220126431988' IndexInText='71' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</Parameters>
								</OtherProperties>
							</Braces>
							<Comma Name=',' Id='637795220126432059' IndexInText='76' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Braces Id='637795220126432085' IndexInText='78' ItemLength='18' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
								<RegularItems>
									<OpeningSquareBrace Name='[' Id='637795220126432088' IndexInText='78' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637795220126432176' IndexInText='79' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='x2_1' Id='637795220126432171' IndexInText='79' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<Comma Name=',' Id='637795220126432210' IndexInText='83' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637795220126432299' IndexInText='85' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='x2_2' Id='637795220126432290' IndexInText='85' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<Comma Name=',' Id='637795220126432326' IndexInText='89' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637795220126432410' IndexInText='91' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='x2_3' Id='637795220126432406' IndexInText='91' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<ClosingSquareBrace Name=']' Id='637795220126432443' IndexInText='95' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Children>
									<Literal Id='637795220126432176' IndexInText='79' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<Literal Id='637795220126432299' IndexInText='85' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<Literal Id='637795220126432410' IndexInText='91' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								</Children>
								<OtherProperties>
									<OpeningBraceInfo Name='[' Id='637795220126432088' IndexInText='78' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ClosingBraceInfo Name=']' Id='637795220126432443' IndexInText='95' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
									<Parameters ObjectId='60' Count='3' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220126432176' IndexInText='79' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220126432299' IndexInText='85' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220126432410' IndexInText='91' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</Parameters>
								</OtherProperties>
							</Braces>
							<ClosingSquareBrace Name=']' Id='637795220126432470' IndexInText='96' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<Braces Id='637795220126431641' IndexInText='58' ItemLength='18' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
							<Braces Id='637795220126432085' IndexInText='78' ItemLength='18' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
						</Children>
						<OtherProperties>
							<OpeningBraceInfo Name='[' Id='637795220126431602' IndexInText='57' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=']' Id='637795220126432470' IndexInText='96' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
							<Parameters ObjectId='62' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220126431641' IndexInText='58' ItemLength='18' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220126432085' IndexInText='78' ItemLength='18' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Operand1.Braces>
					<OperatorInfo OperatorType='BinaryOperator' Name='*' Priority='20' Id='637795220126432523' IndexInText='97' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
						<OperatorNameParts>
							<Name Name='*' Id='637795220126432516' IndexInText='97' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OperatorNameParts>
					</OperatorInfo>
					<Operand2.Braces Id='637795220126432658' IndexInText='98' ItemLength='42' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<OpeningSquareBrace Name='[' Id='637795220126432661' IndexInText='98' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Braces Id='637795220126432694' IndexInText='99' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
								<RegularItems>
									<OpeningSquareBrace Name='[' Id='637795220126432697' IndexInText='99' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637795220126432785' IndexInText='100' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='y1_1' Id='637795220126432776' IndexInText='100' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<Comma Name=',' Id='637795220126432815' IndexInText='104' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637795220126432898' IndexInText='106' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='x1_2' Id='637795220126432893' IndexInText='106' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<ClosingSquareBrace Name=']' Id='637795220126432931' IndexInText='110' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Children>
									<Literal Id='637795220126432785' IndexInText='100' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<Literal Id='637795220126432898' IndexInText='106' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								</Children>
								<OtherProperties>
									<OpeningBraceInfo Name='[' Id='637795220126432697' IndexInText='99' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ClosingBraceInfo Name=']' Id='637795220126432931' IndexInText='110' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
									<Parameters ObjectId='75' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220126432785' IndexInText='100' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220126432898' IndexInText='106' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</Parameters>
								</OtherProperties>
							</Braces>
							<Comma Name=',' Id='637795220126432959' IndexInText='111' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Braces Id='637795220126432984' IndexInText='113' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
								<RegularItems>
									<OpeningSquareBrace Name='[' Id='637795220126432987' IndexInText='113' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637795220126433075' IndexInText='114' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='y2_1' Id='637795220126433071' IndexInText='114' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<Comma Name=',' Id='637795220126433103' IndexInText='118' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637795220126433192' IndexInText='120' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='x2_2' Id='637795220126433187' IndexInText='120' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<ClosingSquareBrace Name=']' Id='637795220126433219' IndexInText='124' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Children>
									<Literal Id='637795220126433075' IndexInText='114' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<Literal Id='637795220126433192' IndexInText='120' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								</Children>
								<OtherProperties>
									<OpeningBraceInfo Name='[' Id='637795220126432987' IndexInText='113' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ClosingBraceInfo Name=']' Id='637795220126433219' IndexInText='124' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
									<Parameters ObjectId='85' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220126433075' IndexInText='114' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220126433192' IndexInText='120' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</Parameters>
								</OtherProperties>
							</Braces>
							<Comma Name=',' Id='637795220126433247' IndexInText='125' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Braces Id='637795220126433272' IndexInText='127' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
								<RegularItems>
									<OpeningSquareBrace Name='[' Id='637795220126433279' IndexInText='127' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637795220126433369' IndexInText='128' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='y3_1' Id='637795220126433364' IndexInText='128' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<Comma Name=',' Id='637795220126433399' IndexInText='132' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637795220126433486' IndexInText='134' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='x3_2' Id='637795220126433482' IndexInText='134' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<ClosingSquareBrace Name=']' Id='637795220126433514' IndexInText='138' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Children>
									<Literal Id='637795220126433369' IndexInText='128' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<Literal Id='637795220126433486' IndexInText='134' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								</Children>
								<OtherProperties>
									<OpeningBraceInfo Name='[' Id='637795220126433279' IndexInText='127' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ClosingBraceInfo Name=']' Id='637795220126433514' IndexInText='138' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
									<Parameters ObjectId='95' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220126433369' IndexInText='128' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220126433486' IndexInText='134' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</Parameters>
								</OtherProperties>
							</Braces>
							<ClosingSquareBrace Name=']' Id='637795220126433545' IndexInText='139' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<Braces Id='637795220126432694' IndexInText='99' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
							<Braces Id='637795220126432984' IndexInText='113' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
							<Braces Id='637795220126433272' IndexInText='127' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
						</Children>
						<OtherProperties>
							<OpeningBraceInfo Name='[' Id='637795220126432661' IndexInText='98' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=']' Id='637795220126433545' IndexInText='139' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
							<Parameters ObjectId='97' Count='3' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220126432694' IndexInText='99' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220126432984' IndexInText='113' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220126433272' IndexInText='127' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Operand2.Braces>
				</Operand2.BinaryOperator>
			</BinaryOperator>
			<ExpressionSeparator Name=';' Id='637795220126433607' IndexInText='140' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<Braces Id='637795220126433732' IndexInText='145' ItemLength='35' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
				<RegularItems>
					<Literal Id='637795220126433701' IndexInText='145' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='println' Id='637795220126433696' IndexInText='145' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<OpeningRoundBrace Name='(' Id='637795220126433736' IndexInText='152' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Literal Id='637795220126433839' IndexInText='153' ItemLength='26' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='matrixMultiplicationResult' Id='637795220126433834' IndexInText='153' ItemLength='26' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<ClosingRoundBrace Name=')' Id='637795220126433867' IndexInText='179' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Children>
					<Literal Id='637795220126433839' IndexInText='153' ItemLength='26' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
				</Children>
				<OtherProperties>
					<NamedExpressionItem Id='637795220126433701' IndexInText='145' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					<OpeningBraceInfo Name='(' Id='637795220126433736' IndexInText='152' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ClosingBraceInfo Name=')' Id='637795220126433867' IndexInText='179' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Parameters ObjectId='106' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220126433839' IndexInText='153' ItemLength='26' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					</Parameters>
				</OtherProperties>
			</Braces>
			<ExpressionSeparator Name=';' Id='637795220126433894' IndexInText='180' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<BinaryOperator Name='=>' Priority='1000' Id='637795220126498996' IndexInText='185' ItemLength='237' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
				<Operand1.BinaryOperator Name=':' Priority='0' Id='637795220126498980' IndexInText='185' ItemLength='58' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
					<Operand1.Braces Id='637795220126434551' IndexInText='185' ItemLength='52' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<Prefixes>
							<Braces Id='637795220126433920' IndexInText='185' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
								<RegularItems>
									<OpeningSquareBrace Name='[' Id='637795220126433928' IndexInText='185' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637795220126434012' IndexInText='186' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='NotNull' Id='637795220126434008' IndexInText='186' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<ClosingSquareBrace Name=']' Id='637795220126434040' IndexInText='193' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Children>
									<Literal Id='637795220126434012' IndexInText='186' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								</Children>
								<OtherProperties>
									<OpeningBraceInfo Name='[' Id='637795220126433928' IndexInText='185' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ClosingBraceInfo Name=']' Id='637795220126434040' IndexInText='193' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
									<Parameters ObjectId='116' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220126434012' IndexInText='186' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</Parameters>
								</OtherProperties>
							</Braces>
							<Braces Id='637795220126434071' IndexInText='195' ItemLength='25' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
								<RegularItems>
									<OpeningSquareBrace Name='[' Id='637795220126434086' IndexInText='195' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Braces Id='637795220126434216' IndexInText='196' ItemLength='23' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
										<RegularItems>
											<Literal Id='637795220126434186' IndexInText='196' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
												<NameExpressionItem Name='PublicName' Id='637795220126434179' IndexInText='196' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											</Literal>
											<OpeningRoundBrace Name='(' Id='637795220126434219' IndexInText='206' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<ConstantText Id='637795220126434318' IndexInText='207' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
												<NameExpressionItem Name='"Calculate"' Id='637795220126434313' IndexInText='207' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											</ConstantText>
											<ClosingRoundBrace Name=')' Id='637795220126434352' IndexInText='218' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</RegularItems>
										<Children>
											<ConstantText Id='637795220126434318' IndexInText='207' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
										</Children>
										<OtherProperties>
											<NamedExpressionItem Id='637795220126434186' IndexInText='196' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
											<OpeningBraceInfo Name='(' Id='637795220126434219' IndexInText='206' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<ClosingBraceInfo Name=')' Id='637795220126434352' IndexInText='218' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<Parameters ObjectId='126' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
												<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220126434318' IndexInText='207' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
											</Parameters>
										</OtherProperties>
									</Braces>
									<ClosingSquareBrace Name=']' Id='637795220126434379' IndexInText='219' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Children>
									<Braces Id='637795220126434216' IndexInText='196' ItemLength='23' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
								</Children>
								<OtherProperties>
									<OpeningBraceInfo Name='[' Id='637795220126434086' IndexInText='195' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ClosingBraceInfo Name=']' Id='637795220126434379' IndexInText='219' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
									<Parameters ObjectId='128' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220126434216' IndexInText='196' ItemLength='23' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
									</Parameters>
								</OtherProperties>
							</Braces>
						</Prefixes>
						<AppliedKeywords>
							<Keyword Name='public' Id='637795220126434399' IndexInText='222' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
								<LanguageKeywordInfo ObjectId='130' Id='637783669980406419' Keyword='public' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
							</Keyword>
						</AppliedKeywords>
						<RegularItems>
							<Literal Id='637795220126434510' IndexInText='229' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='F1' Id='637795220126434505' IndexInText='229' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<OpeningRoundBrace Name='(' Id='637795220126434556' IndexInText='231' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Literal Id='637795220126434646' IndexInText='232' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='x' Id='637795220126434641' IndexInText='232' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<Comma Name=',' Id='637795220126434673' IndexInText='233' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Literal Id='637795220126434761' IndexInText='235' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='y' Id='637795220126434756' IndexInText='235' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<ClosingRoundBrace Name=')' Id='637795220126434787' IndexInText='236' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<Literal Id='637795220126434646' IndexInText='232' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							<Literal Id='637795220126434761' IndexInText='235' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
						</Children>
						<OtherProperties>
							<NamedExpressionItem Id='637795220126434510' IndexInText='229' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							<OpeningBraceInfo Name='(' Id='637795220126434556' IndexInText='231' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=')' Id='637795220126434787' IndexInText='236' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Parameters ObjectId='140' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220126434646' IndexInText='232' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220126434761' IndexInText='235' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Operand1.Braces>
					<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637795220126434837' IndexInText='238' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
						<OperatorNameParts>
							<Name Name=':' Id='637795220126434832' IndexInText='238' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OperatorNameParts>
					</OperatorInfo>
					<Operand2.Literal Id='637795220126434991' IndexInText='240' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='int' Id='637795220126434982' IndexInText='240' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Operand2.Literal>
				</Operand1.BinaryOperator>
				<OperatorInfo OperatorType='BinaryOperator' Name='=>' Priority='1000' Id='637795220126435068' IndexInText='244' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
					<OperatorNameParts>
						<Name Name='=>' Id='637795220126435063' IndexInText='244' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</OperatorNameParts>
				</OperatorInfo>
				<Operand2.CodeBlock Id='637795220126435190' IndexInText='249' ItemLength='173' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
					<RegularItems>
						<CodeBlockStartMarker Name='{' Id='637795220126435184' IndexInText='249' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<PrefixUnaryOperator Name='++' Priority='0' Id='637795220126435445' IndexInText='334' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
							<OperatorInfo OperatorType='PrefixUnaryOperator' Name='++' Priority='0' Id='637795220126435283' IndexInText='334' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
								<OperatorNameParts>
									<Name Name='++' Id='637795220126435272' IndexInText='334' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</OperatorNameParts>
							</OperatorInfo>
							<Operand1.Literal Id='637795220126435395' IndexInText='336' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='y' Id='637795220126435387' IndexInText='336' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Operand1.Literal>
						</PrefixUnaryOperator>
						<ExpressionSeparator Name=';' Id='637795220126435471' IndexInText='367' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<PrefixUnaryOperator Name='return' Priority='2147483647' Id='637795220126494758' IndexInText='374' ItemLength='26' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
							<OperatorInfo OperatorType='PrefixUnaryOperator' Name='return' Priority='2147483647' Id='637795220126435520' IndexInText='374' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
								<OperatorNameParts>
									<Name Name='return' Id='637795220126435514' IndexInText='374' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</OperatorNameParts>
							</OperatorInfo>
							<Operand1.BinaryOperator Name='+' Priority='30' Id='637795220126494802' IndexInText='381' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
								<Operand1.BinaryOperator Name='+' Priority='30' Id='637795220126494787' IndexInText='381' ItemLength='15' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
									<Operand1.ConstantNumericValue Id='637795220126493718' IndexInText='381' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
										<NameExpressionItem Name='1.3EXP-2.7' Id='637795220126493658' IndexInText='381' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										<OtherProperties>
											<SucceededNumericTypeDescriptor ObjectId='162' NumberTypeId='1581134225786' Interface='UniversalExpressionParser.NumericTypeDescriptor'>
												<RegularExpressions ObjectId='163' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[System.String]'>
													<System.String value='^(\d+\.\d+|\d+\.|\.\d+|\d+)(EXP|exp|E|e)[+|-]?(\d+\.\d+|\d+\.|\.\d+|\d+)' />
												</RegularExpressions>
											</SucceededNumericTypeDescriptor>
										</OtherProperties>
									</Operand1.ConstantNumericValue>
									<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637795220126494033' IndexInText='391' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
										<OperatorNameParts>
											<Name Name='+' Id='637795220126494021' IndexInText='391' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</OperatorNameParts>
									</OperatorInfo>
									<Operand2.PrefixUnaryOperator Name='++' Priority='0' Id='637795220126494795' IndexInText='393' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
										<OperatorInfo OperatorType='PrefixUnaryOperator' Name='++' Priority='0' Id='637795220126494150' IndexInText='393' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
											<OperatorNameParts>
												<Name Name='++' Id='637795220126494143' IndexInText='393' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											</OperatorNameParts>
										</OperatorInfo>
										<Operand1.Literal Id='637795220126494286' IndexInText='395' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
											<NameExpressionItem Name='x' Id='637795220126494281' IndexInText='395' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</Operand1.Literal>
									</Operand2.PrefixUnaryOperator>
								</Operand1.BinaryOperator>
								<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637795220126494381' IndexInText='396' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
									<OperatorNameParts>
										<Name Name='+' Id='637795220126494371' IndexInText='396' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</OperatorNameParts>
								</OperatorInfo>
								<Operand2.BinaryOperator Name='*' Priority='20' Id='637795220126494809' IndexInText='397' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
									<Operand1.Literal Id='637795220126494515' IndexInText='397' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='y' Id='637795220126494510' IndexInText='397' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Operand1.Literal>
									<OperatorInfo OperatorType='BinaryOperator' Name='*' Priority='20' Id='637795220126494571' IndexInText='398' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
										<OperatorNameParts>
											<Name Name='*' Id='637795220126494566' IndexInText='398' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</OperatorNameParts>
									</OperatorInfo>
									<Operand2.Literal Id='637795220126494708' IndexInText='399' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='z' Id='637795220126494703' IndexInText='399' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Operand2.Literal>
								</Operand2.BinaryOperator>
							</Operand1.BinaryOperator>
						</PrefixUnaryOperator>
						<ExpressionSeparator Name=';' Id='637795220126498836' IndexInText='400' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<CodeBlockEndMarker Name='}' Id='637795220126498934' IndexInText='421' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</RegularItems>
					<Children>
						<PrefixUnaryOperator Name='++' Priority='0' Id='637795220126435445' IndexInText='334' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
						<PrefixUnaryOperator Name='return' Priority='2147483647' Id='637795220126494758' IndexInText='374' ItemLength='26' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
					</Children>
					<OtherProperties>
						<CodeBlockStartMarker Name='{' Id='637795220126435184' IndexInText='249' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<CodeBlockEndMarker Name='}' Id='637795220126498934' IndexInText='421' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</OtherProperties>
				</Operand2.CodeBlock>
			</BinaryOperator>
			<Literal Id='637795220126500405' IndexInText='426' ItemLength='218' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
				<NameExpressionItem Name='Animal' Id='637795220126500400' IndexInText='507' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				<Prefixes>
					<Custom Id='637795220126499754' IndexInText='426' ItemLength='60' Interface='UniversalExpressionParser.ExpressionItems.Custom.IKeywordBasedCustomExpressionItem'>
						<AppliedKeywords>
							<Keyword Name='public' Id='637795220126499044' IndexInText='426' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
								<LanguageKeywordInfo ObjectId='130' Id='637783669980406419' Keyword='public' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
							</Keyword>
							<Keyword Name='abstract' Id='637795220126499059' IndexInText='433' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
								<LanguageKeywordInfo ObjectId='187' Id='637793546145647499' Keyword='abstract' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
							</Keyword>
							<Keyword Name='class' Id='637795220126499067' IndexInText='442' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
								<LanguageKeywordInfo ObjectId='189' Id='637783670388745402' Keyword='class' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
							</Keyword>
						</AppliedKeywords>
						<RegularItems>
							<Keyword Name='::metadata' Id='637795220126499081' IndexInText='448' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
								<LanguageKeywordInfo ObjectId='191' Id='637781063212876967' Keyword='::metadata' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
							</Keyword>
							<CodeBlock Id='637795220126499205' IndexInText='458' ItemLength='28' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
								<RegularItems>
									<CodeBlockStartMarker Name='{' Id='637795220126499198' IndexInText='458' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<BinaryOperator Name=':' Priority='0' Id='637795220126499715' IndexInText='459' ItemLength='26' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
										<Operand1.Literal Id='637795220126499435' IndexInText='459' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
											<NameExpressionItem Name='description' Id='637795220126499429' IndexInText='459' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</Operand1.Literal>
										<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637795220126499496' IndexInText='470' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
											<OperatorNameParts>
												<Name Name=':' Id='637795220126499489' IndexInText='470' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											</OperatorNameParts>
										</OperatorInfo>
										<Operand2.ConstantText Id='637795220126499669' IndexInText='472' ItemLength='13' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
											<NameExpressionItem Name='"Demo prefix"' Id='637795220126499664' IndexInText='472' ItemLength='13' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</Operand2.ConstantText>
									</BinaryOperator>
									<CodeBlockEndMarker Name='}' Id='637795220126499744' IndexInText='485' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Children>
									<BinaryOperator Name=':' Priority='0' Id='637795220126499715' IndexInText='459' ItemLength='26' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
								</Children>
								<OtherProperties>
									<CodeBlockStartMarker Name='{' Id='637795220126499198' IndexInText='458' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<CodeBlockEndMarker Name='}' Id='637795220126499744' IndexInText='485' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</OtherProperties>
							</CodeBlock>
						</RegularItems>
						<OtherProperties>
							<LastKeywordExpressionItem Name='::metadata' Id='637795220126499081' IndexInText='448' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'/>
							<ErrorsPositionDisplayValue value='448' type='System.Int32' />
						</OtherProperties>
					</Custom>
					<Custom Id='637795220126500280' IndexInText='487' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.Custom.IKeywordBasedCustomExpressionItem'>
						<RegularItems>
							<Keyword Name='::types' Id='637795220126499806' IndexInText='487' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
								<LanguageKeywordInfo ObjectId='204' Id='637781062811583137' Keyword='::types' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
							</Keyword>
							<Braces Id='637795220126499860' IndexInText='494' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
								<RegularItems>
									<OpeningSquareBrace Name='[' Id='637795220126499865' IndexInText='494' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637795220126500004' IndexInText='495' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='T1' Id='637795220126499999' IndexInText='495' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<Comma Name=',' Id='637795220126500039' IndexInText='497' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637795220126500125' IndexInText='499' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='T2' Id='637795220126500120' IndexInText='499' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<Comma Name=',' Id='637795220126500152' IndexInText='501' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637795220126500238' IndexInText='503' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='T3' Id='637795220126500234' IndexInText='503' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<ClosingSquareBrace Name=']' Id='637795220126500268' IndexInText='505' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Children>
									<Literal Id='637795220126500004' IndexInText='495' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<Literal Id='637795220126500125' IndexInText='499' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<Literal Id='637795220126500238' IndexInText='503' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								</Children>
								<OtherProperties>
									<OpeningBraceInfo Name='[' Id='637795220126499865' IndexInText='494' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ClosingBraceInfo Name=']' Id='637795220126500268' IndexInText='505' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
									<Parameters ObjectId='216' Count='3' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220126500004' IndexInText='495' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220126500125' IndexInText='499' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220126500238' IndexInText='503' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</Parameters>
								</OtherProperties>
							</Braces>
						</RegularItems>
						<OtherProperties>
							<LastKeywordExpressionItem Name='::types' Id='637795220126499806' IndexInText='487' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'/>
							<ErrorsPositionDisplayValue value='487' type='System.Int32' />
						</OtherProperties>
					</Custom>
				</Prefixes>
				<Postfixes>
					<Custom Id='637795220126500480' IndexInText='518' ItemLength='46' Interface='UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IWhereCustomExpressionItem'>
						<RegularItems>
							<CustomExpressionItemPart Id='637795220126500486' IndexInText='518' ItemLength='16' Interface='UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem'>
								<RegularItems>
									<Keyword Name='where' Id='637795220126500425' IndexInText='518' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
										<LanguageKeywordInfo ObjectId='220' Id='637781062905654923' Keyword='where' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
									</Keyword>
									<Name Name='T1' Id='637795220126500528' IndexInText='524' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Name Name=':' Id='637795220126500536' IndexInText='526' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637795220126500554' IndexInText='528' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='IType1' Id='637795220126500546' IndexInText='528' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
								</RegularItems>
								<OtherProperties>
									<WhereKeyword Name='where' Id='637795220126500425' IndexInText='518' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'/>
									<TypeName Name='T1' Id='637795220126500528' IndexInText='524' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<TypeConstraints ObjectId='225' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220126500554' IndexInText='528' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</TypeConstraints>
								</OtherProperties>
							</CustomExpressionItemPart>
							<CustomExpressionItemPart Id='637795220126500575' IndexInText='535' ItemLength='20' Interface='UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem'>
								<RegularItems>
									<Keyword Name='where' Id='637795220126500565' IndexInText='535' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
										<LanguageKeywordInfo ObjectId='220' Id='637781062905654923' Keyword='where' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
									</Keyword>
									<Name Name='T2' Id='637795220126500585' IndexInText='541' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Name Name=':' Id='637795220126500591' IndexInText='543' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637795220126500604' IndexInText='545' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='T1' Id='637795220126500597' IndexInText='545' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<Comma Name=',' Id='637795220126500611' IndexInText='547' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637795220126500628' IndexInText='549' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='IType2' Id='637795220126500622' IndexInText='549' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
								</RegularItems>
								<OtherProperties>
									<WhereKeyword Name='where' Id='637795220126500565' IndexInText='535' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'/>
									<TypeName Name='T2' Id='637795220126500585' IndexInText='541' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<TypeConstraints ObjectId='235' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220126500604' IndexInText='545' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220126500628' IndexInText='549' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</TypeConstraints>
								</OtherProperties>
							</CustomExpressionItemPart>
							<Name Name='whereend' Id='637795220126500662' IndexInText='556' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<CustomExpressionItemPart Id='637795220126500486' IndexInText='518' ItemLength='16' Interface='UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem'/>
							<CustomExpressionItemPart Id='637795220126500575' IndexInText='535' ItemLength='20' Interface='UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem'/>
						</Children>
						<OtherProperties>
							<WhereEndMarker Name='whereend' Id='637795220126500662' IndexInText='556' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<GenericTypeDataExpressionItems ObjectId='237' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem]'>
								<UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem Id='637795220126500486' IndexInText='518' ItemLength='16'/>
								<UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem Id='637795220126500575' IndexInText='535' ItemLength='20'/>
							</GenericTypeDataExpressionItems>
							<ErrorsPositionDisplayValue value='518' type='System.Int32' />
						</OtherProperties>
					</Custom>
					<Custom Id='637795220126500732' IndexInText='569' ItemLength='25' Interface='UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IWhereCustomExpressionItem'>
						<RegularItems>
							<CustomExpressionItemPart Id='637795220126500737' IndexInText='569' ItemLength='16' Interface='UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem'>
								<RegularItems>
									<Keyword Name='where' Id='637795220126500697' IndexInText='569' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
										<LanguageKeywordInfo ObjectId='220' Id='637781062905654923' Keyword='where' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
									</Keyword>
									<Name Name='T3' Id='637795220126500754' IndexInText='575' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Name Name=':' Id='637795220126500759' IndexInText='577' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637795220126500773' IndexInText='579' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='IType3' Id='637795220126500766' IndexInText='579' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
								</RegularItems>
								<OtherProperties>
									<WhereKeyword Name='where' Id='637795220126500697' IndexInText='569' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'/>
									<TypeName Name='T3' Id='637795220126500754' IndexInText='575' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<TypeConstraints ObjectId='245' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220126500773' IndexInText='579' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</TypeConstraints>
								</OtherProperties>
							</CustomExpressionItemPart>
							<Name Name='whereend' Id='637795220126500787' IndexInText='586' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<CustomExpressionItemPart Id='637795220126500737' IndexInText='569' ItemLength='16' Interface='UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem'/>
						</Children>
						<OtherProperties>
							<WhereEndMarker Name='whereend' Id='637795220126500787' IndexInText='586' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<GenericTypeDataExpressionItems ObjectId='247' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem]'>
								<UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem Id='637795220126500737' IndexInText='569' ItemLength='16'/>
							</GenericTypeDataExpressionItems>
							<ErrorsPositionDisplayValue value='569' type='System.Int32' />
						</OtherProperties>
					</Custom>
					<CodeBlock Id='637795220126500825' IndexInText='596' ItemLength='48' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
						<RegularItems>
							<CodeBlockStartMarker Name='{' Id='637795220126500819' IndexInText='596' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<BinaryOperator Name=':' Priority='0' Id='637795220126501263' IndexInText='607' ItemLength='29' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
								<Operand1.Braces Id='637795220126500979' IndexInText='607' ItemLength='22' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
									<AppliedKeywords>
										<Keyword Name='public' Id='637795220126500847' IndexInText='607' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
											<LanguageKeywordInfo ObjectId='130' Id='637783669980406419' Keyword='public' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
										</Keyword>
										<Keyword Name='abstract' Id='637795220126500853' IndexInText='614' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
											<LanguageKeywordInfo ObjectId='187' Id='637793546145647499' Keyword='abstract' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
										</Keyword>
									</AppliedKeywords>
									<RegularItems>
										<Literal Id='637795220126500944' IndexInText='623' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
											<NameExpressionItem Name='Move' Id='637795220126500939' IndexInText='623' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</Literal>
										<OpeningRoundBrace Name='(' Id='637795220126500984' IndexInText='627' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										<ClosingRoundBrace Name=')' Id='637795220126501015' IndexInText='628' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</RegularItems>
									<OtherProperties>
										<NamedExpressionItem Id='637795220126500944' IndexInText='623' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
										<OpeningBraceInfo Name='(' Id='637795220126500984' IndexInText='627' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										<ClosingBraceInfo Name=')' Id='637795220126501015' IndexInText='628' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										<Parameters ObjectId='258' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'/>
									</OtherProperties>
								</Operand1.Braces>
								<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637795220126501075' IndexInText='630' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
									<OperatorNameParts>
										<Name Name=':' Id='637795220126501069' IndexInText='630' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</OperatorNameParts>
								</OperatorInfo>
								<Operand2.Literal Id='637795220126501226' IndexInText='632' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
									<NameExpressionItem Name='void' Id='637795220126501221' IndexInText='632' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</Operand2.Literal>
							</BinaryOperator>
							<ExpressionSeparator Name=';' Id='637795220126501286' IndexInText='636' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637795220126501319' IndexInText='643' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<BinaryOperator Name=':' Priority='0' Id='637795220126501263' IndexInText='607' ItemLength='29' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
						</Children>
						<OtherProperties>
							<CodeBlockStartMarker Name='{' Id='637795220126500819' IndexInText='596' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637795220126501319' IndexInText='643' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OtherProperties>
					</CodeBlock>
				</Postfixes>
			</Literal>
			<BinaryOperator Name=':' Priority='0' Id='637795220126502600' IndexInText='648' ItemLength='88' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
				<Operand1.Literal Id='637795220126501431' IndexInText='648' ItemLength='16' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
					<NameExpressionItem Name='Dog' Id='637795220126501427' IndexInText='661' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<AppliedKeywords>
						<Keyword Name='public' Id='637795220126501341' IndexInText='648' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
							<LanguageKeywordInfo ObjectId='130' Id='637783669980406419' Keyword='public' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
						</Keyword>
						<Keyword Name='class' Id='637795220126501351' IndexInText='655' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
							<LanguageKeywordInfo ObjectId='189' Id='637783670388745402' Keyword='class' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
						</Keyword>
					</AppliedKeywords>
				</Operand1.Literal>
				<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637795220126501486' IndexInText='665' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
					<OperatorNameParts>
						<Name Name=':' Id='637795220126501480' IndexInText='665' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</OperatorNameParts>
				</OperatorInfo>
				<Operand2.Braces Id='637795220126501625' IndexInText='667' ItemLength='69' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
					<RegularItems>
						<OpeningRoundBrace Name='(' Id='637795220126501628' IndexInText='667' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<Literal Id='637795220126501722' IndexInText='668' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='Anymal' Id='637795220126501717' IndexInText='668' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Literal>
						<ClosingRoundBrace Name=')' Id='637795220126501750' IndexInText='674' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</RegularItems>
					<Postfixes>
						<CodeBlock Id='637795220126501782' IndexInText='677' ItemLength='59' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
							<RegularItems>
								<CodeBlockStartMarker Name='{' Id='637795220126501777' IndexInText='677' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<BinaryOperator Name='=>' Priority='1000' Id='637795220126502542' IndexInText='684' ItemLength='48' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
									<Operand1.BinaryOperator Name=':' Priority='0' Id='637795220126502534' IndexInText='684' ItemLength='29' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
										<Operand1.Braces Id='637795220126501925' IndexInText='684' ItemLength='22' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
											<AppliedKeywords>
												<Keyword Name='public' Id='637795220126501800' IndexInText='684' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
													<LanguageKeywordInfo ObjectId='130' Id='637783669980406419' Keyword='public' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
												</Keyword>
												<Keyword Name='override' Id='637795220126501813' IndexInText='691' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
													<LanguageKeywordInfo ObjectId='284' Id='637793548069818537' Keyword='override' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
												</Keyword>
											</AppliedKeywords>
											<RegularItems>
												<Literal Id='637795220126501893' IndexInText='700' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
													<NameExpressionItem Name='Move' Id='637795220126501888' IndexInText='700' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												</Literal>
												<OpeningRoundBrace Name='(' Id='637795220126501930' IndexInText='704' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												<ClosingRoundBrace Name=')' Id='637795220126501956' IndexInText='705' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											</RegularItems>
											<OtherProperties>
												<NamedExpressionItem Id='637795220126501893' IndexInText='700' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
												<OpeningBraceInfo Name='(' Id='637795220126501930' IndexInText='704' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												<ClosingBraceInfo Name=')' Id='637795220126501956' IndexInText='705' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												<Parameters ObjectId='289' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'/>
											</OtherProperties>
										</Operand1.Braces>
										<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637795220126502004' IndexInText='707' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
											<OperatorNameParts>
												<Name Name=':' Id='637795220126501999' IndexInText='707' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											</OperatorNameParts>
										</OperatorInfo>
										<Operand2.Literal Id='637795220126502149' IndexInText='709' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
											<NameExpressionItem Name='void' Id='637795220126502145' IndexInText='709' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</Operand2.Literal>
									</Operand1.BinaryOperator>
									<OperatorInfo OperatorType='BinaryOperator' Name='=>' Priority='1000' Id='637795220126502227' IndexInText='714' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
										<OperatorNameParts>
											<Name Name='=>' Id='637795220126502221' IndexInText='714' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</OperatorNameParts>
									</OperatorInfo>
									<Operand2.Braces Id='637795220126502369' IndexInText='717' ItemLength='15' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
										<RegularItems>
											<Literal Id='637795220126502340' IndexInText='717' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
												<NameExpressionItem Name='println' Id='637795220126502335' IndexInText='717' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											</Literal>
											<OpeningRoundBrace Name='(' Id='637795220126502372' IndexInText='724' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<ConstantText Id='637795220126502469' IndexInText='725' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
												<NameExpressionItem Name='"Jump"' Id='637795220126502464' IndexInText='725' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											</ConstantText>
											<ClosingRoundBrace Name=')' Id='637795220126502499' IndexInText='731' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</RegularItems>
										<Children>
											<ConstantText Id='637795220126502469' IndexInText='725' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
										</Children>
										<OtherProperties>
											<NamedExpressionItem Id='637795220126502340' IndexInText='717' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
											<OpeningBraceInfo Name='(' Id='637795220126502372' IndexInText='724' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<ClosingBraceInfo Name=')' Id='637795220126502499' IndexInText='731' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<Parameters ObjectId='303' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
												<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220126502469' IndexInText='725' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
											</Parameters>
										</OtherProperties>
									</Operand2.Braces>
								</BinaryOperator>
								<ExpressionSeparator Name=';' Id='637795220126502562' IndexInText='732' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<CodeBlockEndMarker Name='}' Id='637795220126502591' IndexInText='735' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</RegularItems>
							<Children>
								<BinaryOperator Name='=>' Priority='1000' Id='637795220126502542' IndexInText='684' ItemLength='48' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
							</Children>
							<OtherProperties>
								<CodeBlockStartMarker Name='{' Id='637795220126501777' IndexInText='677' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<CodeBlockEndMarker Name='}' Id='637795220126502591' IndexInText='735' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OtherProperties>
						</CodeBlock>
					</Postfixes>
					<Children>
						<Literal Id='637795220126501722' IndexInText='668' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					</Children>
					<OtherProperties>
						<OpeningBraceInfo Name='(' Id='637795220126501628' IndexInText='667' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<ClosingBraceInfo Name=')' Id='637795220126501750' IndexInText='674' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
						<Parameters ObjectId='306' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
							<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220126501722' IndexInText='668' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
						</Parameters>
					</OtherProperties>
				</Operand2.Braces>
			</BinaryOperator>
		</RegularItems>
		<Children>
			<BinaryOperator Name='=' Priority='2000' Id='637795220126431184' IndexInText='0' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
			<BinaryOperator Name='=' Priority='2000' Id='637795220126433575' IndexInText='24' ItemLength='116' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
			<Braces Id='637795220126433732' IndexInText='145' ItemLength='35' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
			<BinaryOperator Name='=>' Priority='1000' Id='637795220126498996' IndexInText='185' ItemLength='237' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
			<Literal Id='637795220126500405' IndexInText='426' ItemLength='218' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
			<BinaryOperator Name=':' Priority='0' Id='637795220126502600' IndexInText='648' ItemLength='88' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
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
	<MainExpressionItem Id='637795220126429853' IndexInText='0' ItemLength='736' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'/>
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
	<ExpressionItemSeries Id='637795220124264033' IndexInText='106' ItemLength='470' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'>
		<RegularItems>
			<BinaryOperator Name='=' Priority='2000' Id='637795220124347563' IndexInText='106' ItemLength='31' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
				<Operand1.Literal Id='637795220124264417' IndexInText='106' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
					<NameExpressionItem Name='_x' Id='637795220124264411' IndexInText='110' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<AppliedKeywords>
						<Keyword Name='var' Id='637795220124264266' IndexInText='106' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
							<LanguageKeywordInfo ObjectId='6' Id='637781064051574641' Keyword='var' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
						</Keyword>
					</AppliedKeywords>
				</Operand1.Literal>
				<OperatorInfo OperatorType='BinaryOperator' Name='=' Priority='2000' Id='637795220124264525' IndexInText='113' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
					<OperatorNameParts>
						<Name Name='=' Id='637795220124264513' IndexInText='113' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</OperatorNameParts>
				</OperatorInfo>
				<Operand2.BinaryOperator Name='+' Priority='30' Id='637795220124347585' IndexInText='115' ItemLength='22' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
					<Operand1.Braces Id='637795220124264728' IndexInText='115' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<Literal Id='637795220124264675' IndexInText='115' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='f$' Id='637795220124264669' IndexInText='115' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<OpeningRoundBrace Name='(' Id='637795220124264733' IndexInText='117' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Literal Id='637795220124264834' IndexInText='118' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='x1' Id='637795220124264829' IndexInText='118' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<Comma Name=',' Id='637795220124264871' IndexInText='120' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Literal Id='637795220124264968' IndexInText='122' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='x2' Id='637795220124264960' IndexInText='122' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<ClosingRoundBrace Name=')' Id='637795220124265000' IndexInText='124' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<Literal Id='637795220124264834' IndexInText='118' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							<Literal Id='637795220124264968' IndexInText='122' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
						</Children>
						<OtherProperties>
							<NamedExpressionItem Id='637795220124264675' IndexInText='115' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							<OpeningBraceInfo Name='(' Id='637795220124264733' IndexInText='117' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=')' Id='637795220124265000' IndexInText='124' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Parameters ObjectId='20' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220124264834' IndexInText='118' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220124264968' IndexInText='122' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Operand1.Braces>
					<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637795220124265066' IndexInText='126' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
						<OperatorNameParts>
							<Name Name='+' Id='637795220124265059' IndexInText='126' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OperatorNameParts>
					</OperatorInfo>
					<Operand2.Braces Id='637795220124265248' IndexInText='128' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<Literal Id='637795220124265206' IndexInText='128' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='m1' Id='637795220124265202' IndexInText='128' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<OpeningSquareBrace Name='[' Id='637795220124265253' IndexInText='130' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ConstantNumericValue Id='637795220124347154' IndexInText='131' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
								<NameExpressionItem Name='1' Id='637795220124347103' IndexInText='131' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<OtherProperties>
									<SucceededNumericTypeDescriptor ObjectId='29' NumberTypeId='1581134183960' Interface='UniversalExpressionParser.NumericTypeDescriptor'>
										<RegularExpressions ObjectId='30' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[System.String]'>
											<System.String value='^\d+' />
										</RegularExpressions>
									</SucceededNumericTypeDescriptor>
								</OtherProperties>
							</ConstantNumericValue>
							<Comma Name=',' Id='637795220124347318' IndexInText='132' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Literal Id='637795220124347462' IndexInText='134' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='x3' Id='637795220124347457' IndexInText='134' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<ClosingSquareBrace Name=']' Id='637795220124347504' IndexInText='136' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<ConstantNumericValue Id='637795220124347154' IndexInText='131' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'/>
							<Literal Id='637795220124347462' IndexInText='134' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
						</Children>
						<OtherProperties>
							<NamedExpressionItem Id='637795220124265206' IndexInText='128' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							<OpeningBraceInfo Name='[' Id='637795220124265253' IndexInText='130' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=']' Id='637795220124347504' IndexInText='136' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Parameters ObjectId='35' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220124347154' IndexInText='131' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'/>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220124347462' IndexInText='134' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Operand2.Braces>
				</Operand2.BinaryOperator>
			</BinaryOperator>
			<ExpressionSeparator Name=';' Id='637795220124347639' IndexInText='137' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<Literal Id='637795220124347782' IndexInText='142' ItemLength='61' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
				<NameExpressionItem Name='Dog' Id='637795220124347777' IndexInText='155' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				<AppliedKeywords>
					<Keyword Name='public' Id='637795220124347671' IndexInText='142' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
						<LanguageKeywordInfo ObjectId='40' Id='637783669980406419' Keyword='public' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
					</Keyword>
					<Keyword Name='class' Id='637795220124347688' IndexInText='149' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
						<LanguageKeywordInfo ObjectId='42' Id='637783670388745402' Keyword='class' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
					</Keyword>
				</AppliedKeywords>
				<Postfixes>
					<CodeBlock Id='637795220124347820' IndexInText='160' ItemLength='43' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
						<RegularItems>
							<CodeBlockStartMarker Name='{' Id='637795220124347816' IndexInText='160' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<BinaryOperator Name='=>' Priority='1000' Id='637795220124348432' IndexInText='167' ItemLength='32' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
								<Operand1.BinaryOperator Name=':' Priority='0' Id='637795220124348422' IndexInText='167' ItemLength='21' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
									<Operand1.Literal Id='637795220124347938' IndexInText='167' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='Color' Id='637795220124347934' IndexInText='174' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										<AppliedKeywords>
											<Keyword Name='public' Id='637795220124347848' IndexInText='167' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
												<LanguageKeywordInfo ObjectId='40' Id='637783669980406419' Keyword='public' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
											</Keyword>
										</AppliedKeywords>
									</Operand1.Literal>
									<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637795220124347998' IndexInText='180' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
										<OperatorNameParts>
											<Name Name=':' Id='637795220124347989' IndexInText='180' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</OperatorNameParts>
									</OperatorInfo>
									<Operand2.Literal Id='637795220124348164' IndexInText='182' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='string' Id='637795220124348160' IndexInText='182' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Operand2.Literal>
								</Operand1.BinaryOperator>
								<OperatorInfo OperatorType='BinaryOperator' Name='=>' Priority='1000' Id='637795220124348252' IndexInText='189' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
									<OperatorNameParts>
										<Name Name='=>' Id='637795220124348246' IndexInText='189' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</OperatorNameParts>
								</OperatorInfo>
								<Operand2.ConstantText Id='637795220124348380' IndexInText='192' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
									<NameExpressionItem Name='"brown"' Id='637795220124348375' IndexInText='192' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</Operand2.ConstantText>
							</BinaryOperator>
							<ExpressionSeparator Name=';' Id='637795220124348453' IndexInText='199' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637795220124348482' IndexInText='202' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<BinaryOperator Name='=>' Priority='1000' Id='637795220124348432' IndexInText='167' ItemLength='32' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
						</Children>
						<OtherProperties>
							<CodeBlockStartMarker Name='{' Id='637795220124347816' IndexInText='160' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637795220124348482' IndexInText='202' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OtherProperties>
					</CodeBlock>
				</Postfixes>
			</Literal>
			<Braces Id='637795220124348634' IndexInText='557' ItemLength='18' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
				<RegularItems>
					<Literal Id='637795220124348605' IndexInText='557' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='println' Id='637795220124348600' IndexInText='557' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<OpeningRoundBrace Name='(' Id='637795220124348638' IndexInText='564' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Literal Id='637795220124348734' IndexInText='565' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='Dog.Color' Id='637795220124348729' IndexInText='565' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<ClosingRoundBrace Name=')' Id='637795220124348764' IndexInText='574' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Children>
					<Literal Id='637795220124348734' IndexInText='565' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
				</Children>
				<OtherProperties>
					<NamedExpressionItem Id='637795220124348605' IndexInText='557' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					<OpeningBraceInfo Name='(' Id='637795220124348638' IndexInText='564' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ClosingBraceInfo Name=')' Id='637795220124348764' IndexInText='574' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Parameters ObjectId='67' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220124348734' IndexInText='565' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					</Parameters>
				</OtherProperties>
			</Braces>
			<ExpressionSeparator Name=';' Id='637795220124348793' IndexInText='575' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
		</RegularItems>
		<Children>
			<BinaryOperator Name='=' Priority='2000' Id='637795220124347563' IndexInText='106' ItemLength='31' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
			<Literal Id='637795220124347782' IndexInText='142' ItemLength='61' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
			<Braces Id='637795220124348634' IndexInText='557' ItemLength='18' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
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
	<MainExpressionItem Id='637795220124264033' IndexInText='106' ItemLength='470' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'/>
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
	<ExpressionItemSeries Id='637795220123756000' IndexInText='150' ItemLength='71' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'>
		<RegularItems>
			<BinaryOperator Name='=' Priority='2000' Id='637795220123842093' IndexInText='150' ItemLength='41' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
				<Operand1.Literal Id='637795220123756327' IndexInText='150' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
					<NameExpressionItem Name='x' Id='637795220123756321' IndexInText='154' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<AppliedKeywords>
						<Keyword Name='var' Id='637795220123756173' IndexInText='150' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
							<LanguageKeywordInfo ObjectId='6' Id='637781064051574641' Keyword='var' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
						</Keyword>
					</AppliedKeywords>
				</Operand1.Literal>
				<OperatorInfo OperatorType='BinaryOperator' Name='=' Priority='2000' Id='637795220123756433' IndexInText='156' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
					<OperatorNameParts>
						<Name Name='=' Id='637795220123756421' IndexInText='156' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</OperatorNameParts>
				</OperatorInfo>
				<Operand2.Braces Id='637795220123756572' IndexInText='158' ItemLength='33' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
					<RegularItems>
						<OpeningRoundBrace Name='(' Id='637795220123756577' IndexInText='158' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<Braces Id='637795220123756622' IndexInText='159' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
							<RegularItems>
								<OpeningRoundBrace Name='(' Id='637795220123756625' IndexInText='159' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<Literal Id='637795220123756716' IndexInText='160' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
									<NameExpressionItem Name='x1' Id='637795220123756711' IndexInText='160' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</Literal>
								<Comma Name=',' Id='637795220123756752' IndexInText='162' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<Literal Id='637795220123756842' IndexInText='164' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
									<NameExpressionItem Name='x2' Id='637795220123756836' IndexInText='164' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</Literal>
								<Comma Name=',' Id='637795220123756870' IndexInText='166' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<Literal Id='637795220123756957' IndexInText='168' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
									<NameExpressionItem Name='x3' Id='637795220123756948' IndexInText='168' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</Literal>
								<ClosingRoundBrace Name=')' Id='637795220123756989' IndexInText='170' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</RegularItems>
							<Children>
								<Literal Id='637795220123756716' IndexInText='160' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								<Literal Id='637795220123756842' IndexInText='164' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								<Literal Id='637795220123756957' IndexInText='168' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							</Children>
							<OtherProperties>
								<OpeningBraceInfo Name='(' Id='637795220123756625' IndexInText='159' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<ClosingBraceInfo Name=')' Id='637795220123756989' IndexInText='170' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
								<Parameters ObjectId='22' Count='3' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
									<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220123756716' IndexInText='160' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220123756842' IndexInText='164' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220123756957' IndexInText='168' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								</Parameters>
							</OtherProperties>
						</Braces>
						<Comma Name=',' Id='637795220123757024' IndexInText='171' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<Braces Id='637795220123757050' IndexInText='173' ItemLength='14' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
							<RegularItems>
								<OpeningSquareBrace Name='[' Id='637795220123757055' IndexInText='173' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<Literal Id='637795220123757141' IndexInText='174' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
									<NameExpressionItem Name='x4' Id='637795220123757136' IndexInText='174' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</Literal>
								<Comma Name=',' Id='637795220123757170' IndexInText='176' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<BinaryOperator Name='+' Priority='30' Id='637795220123841630' IndexInText='178' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
									<Operand1.Literal Id='637795220123757260' IndexInText='178' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='x5' Id='637795220123757254' IndexInText='178' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Operand1.Literal>
									<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637795220123757316' IndexInText='180' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
										<OperatorNameParts>
											<Name Name='+' Id='637795220123757310' IndexInText='180' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</OperatorNameParts>
									</OperatorInfo>
									<Operand2.ConstantNumericValue Id='637795220123841449' IndexInText='181' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
										<NameExpressionItem Name='1' Id='637795220123841389' IndexInText='181' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										<OtherProperties>
											<SucceededNumericTypeDescriptor ObjectId='36' NumberTypeId='1581134183960' Interface='UniversalExpressionParser.NumericTypeDescriptor'>
												<RegularExpressions ObjectId='37' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[System.String]'>
													<System.String value='^\d+' />
												</RegularExpressions>
											</SucceededNumericTypeDescriptor>
										</OtherProperties>
									</Operand2.ConstantNumericValue>
								</BinaryOperator>
								<Comma Name=',' Id='637795220123841698' IndexInText='182' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<Literal Id='637795220123841856' IndexInText='184' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
									<NameExpressionItem Name='x6' Id='637795220123841850' IndexInText='184' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</Literal>
								<ClosingSquareBrace Name=']' Id='637795220123841898' IndexInText='186' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</RegularItems>
							<Children>
								<Literal Id='637795220123757141' IndexInText='174' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								<BinaryOperator Name='+' Priority='30' Id='637795220123841630' IndexInText='178' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
								<Literal Id='637795220123841856' IndexInText='184' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							</Children>
							<OtherProperties>
								<OpeningBraceInfo Name='[' Id='637795220123757055' IndexInText='173' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<ClosingBraceInfo Name=']' Id='637795220123841898' IndexInText='186' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
								<Parameters ObjectId='42' Count='3' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
									<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220123757141' IndexInText='174' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name='+' Priority='30' Id='637795220123841630' IndexInText='178' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
									<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220123841856' IndexInText='184' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								</Parameters>
							</OtherProperties>
						</Braces>
						<Comma Name=',' Id='637795220123841938' IndexInText='187' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<Literal Id='637795220123842029' IndexInText='189' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='y' Id='637795220123842024' IndexInText='189' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Literal>
						<ClosingRoundBrace Name=')' Id='637795220123842058' IndexInText='190' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</RegularItems>
					<Children>
						<Braces Id='637795220123756622' IndexInText='159' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
						<Braces Id='637795220123757050' IndexInText='173' ItemLength='14' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
						<Literal Id='637795220123842029' IndexInText='189' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					</Children>
					<OtherProperties>
						<OpeningBraceInfo Name='(' Id='637795220123756577' IndexInText='158' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<ClosingBraceInfo Name=')' Id='637795220123842058' IndexInText='190' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
						<Parameters ObjectId='47' Count='3' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
							<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220123756622' IndexInText='159' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
							<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220123757050' IndexInText='173' ItemLength='14' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
							<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220123842029' IndexInText='189' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
						</Parameters>
					</OtherProperties>
				</Operand2.Braces>
			</BinaryOperator>
			<ExpressionSeparator Name=';' Id='637795220123842114' IndexInText='191' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<BinaryOperator Name='+=' Priority='2000' Id='637795220123926290' IndexInText='194' ItemLength='26' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
				<Operand1.Literal Id='637795220123842204' IndexInText='194' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
					<NameExpressionItem Name='x' Id='637795220123842200' IndexInText='194' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</Operand1.Literal>
				<OperatorInfo OperatorType='BinaryOperator' Name='+=' Priority='2000' Id='637795220123842319' IndexInText='196' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
					<OperatorNameParts>
						<Name Name='+=' Id='637795220123842314' IndexInText='196' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</OperatorNameParts>
				</OperatorInfo>
				<Operand2.BinaryOperator Name='+' Priority='30' Id='637795220123926310' IndexInText='199' ItemLength='21' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
					<Operand1.Braces Id='637795220123842437' IndexInText='199' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<OpeningRoundBrace Name='(' Id='637795220123842442' IndexInText='199' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Literal Id='637795220123842536' IndexInText='200' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='x2' Id='637795220123842532' IndexInText='200' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<Comma Name=',' Id='637795220123842572' IndexInText='202' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Literal Id='637795220123842654' IndexInText='204' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='x4' Id='637795220123842650' IndexInText='204' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<ClosingRoundBrace Name=')' Id='637795220123842720' IndexInText='206' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<Literal Id='637795220123842536' IndexInText='200' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							<Literal Id='637795220123842654' IndexInText='204' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
						</Children>
						<OtherProperties>
							<OpeningBraceInfo Name='(' Id='637795220123842442' IndexInText='199' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=')' Id='637795220123842720' IndexInText='206' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
							<Parameters ObjectId='63' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220123842536' IndexInText='200' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220123842654' IndexInText='204' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Operand1.Braces>
					<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637795220123842781' IndexInText='208' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
						<OperatorNameParts>
							<Name Name='+' Id='637795220123842775' IndexInText='208' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OperatorNameParts>
					</OperatorInfo>
					<Operand2.BinaryOperator Name='*' Priority='20' Id='637795220123926373' IndexInText='210' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.ConstantNumericValue Id='637795220123925536' IndexInText='210' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
							<NameExpressionItem Name='2' Id='637795220123925476' IndexInText='210' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<OtherProperties>
								<SucceededNumericTypeDescriptor ObjectId='36' NumberTypeId='1581134183960' Interface='UniversalExpressionParser.NumericTypeDescriptor'/>
							</OtherProperties>
						</Operand1.ConstantNumericValue>
						<OperatorInfo OperatorType='BinaryOperator' Name='*' Priority='20' Id='637795220123925783' IndexInText='211' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<Name Name='*' Id='637795220123925771' IndexInText='211' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OperatorNameParts>
						</OperatorInfo>
						<Operand2.Braces Id='637795220123925944' IndexInText='212' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
							<RegularItems>
								<OpeningSquareBrace Name='[' Id='637795220123925949' IndexInText='212' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<Literal Id='637795220123926068' IndexInText='213' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
									<NameExpressionItem Name='x3' Id='637795220123926063' IndexInText='213' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</Literal>
								<Comma Name=',' Id='637795220123926117' IndexInText='215' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<Literal Id='637795220123926209' IndexInText='217' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
									<NameExpressionItem Name='x4' Id='637795220123926204' IndexInText='217' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</Literal>
								<ClosingSquareBrace Name=']' Id='637795220123926240' IndexInText='219' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</RegularItems>
							<Children>
								<Literal Id='637795220123926068' IndexInText='213' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								<Literal Id='637795220123926209' IndexInText='217' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							</Children>
							<OtherProperties>
								<OpeningBraceInfo Name='[' Id='637795220123925949' IndexInText='212' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<ClosingBraceInfo Name=']' Id='637795220123926240' IndexInText='219' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
								<Parameters ObjectId='79' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
									<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220123926068' IndexInText='213' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220123926209' IndexInText='217' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								</Parameters>
							</OtherProperties>
						</Operand2.Braces>
					</Operand2.BinaryOperator>
				</Operand2.BinaryOperator>
			</BinaryOperator>
			<ExpressionSeparator Name=';' Id='637795220123926434' IndexInText='220' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
		</RegularItems>
		<Children>
			<BinaryOperator Name='=' Priority='2000' Id='637795220123842093' IndexInText='150' ItemLength='41' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
			<BinaryOperator Name='+=' Priority='2000' Id='637795220123926290' IndexInText='194' ItemLength='26' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
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
	<MainExpressionItem Id='637795220123756000' IndexInText='150' ItemLength='71' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'/>
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
	<ExpressionItemSeries Id='637795220123355285' IndexInText='150' ItemLength='249' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'>
		<RegularItems>
			<BinaryOperator Name='=' Priority='2000' Id='637795220123695299' IndexInText='150' ItemLength='87' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
				<Operand1.Literal Id='637795220123355653' IndexInText='150' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
					<NameExpressionItem Name='x' Id='637795220123355647' IndexInText='154' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<AppliedKeywords>
						<Keyword Name='var' Id='637795220123355487' IndexInText='150' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
							<LanguageKeywordInfo ObjectId='6' Id='637781064051574641' Keyword='var' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
						</Keyword>
					</AppliedKeywords>
				</Operand1.Literal>
				<OperatorInfo OperatorType='BinaryOperator' Name='=' Priority='2000' Id='637795220123355907' IndexInText='156' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
					<OperatorNameParts>
						<Name Name='=' Id='637795220123355874' IndexInText='156' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</OperatorNameParts>
				</OperatorInfo>
				<Operand2.BinaryOperator Name='+' Priority='30' Id='637795220123695319' IndexInText='158' ItemLength='79' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
					<Operand1.BinaryOperator Name='+' Priority='30' Id='637795220123695309' IndexInText='158' ItemLength='23' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.Literal Id='637795220123356149' IndexInText='158' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='x1' Id='637795220123356142' IndexInText='158' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand1.Literal>
						<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637795220123356266' IndexInText='160' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<Name Name='+' Id='637795220123356259' IndexInText='160' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OperatorNameParts>
						</OperatorInfo>
						<Operand2.Braces Id='637795220123356447' IndexInText='161' ItemLength='20' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
							<RegularItems>
								<Literal Id='637795220123356408' IndexInText='161' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
									<NameExpressionItem Name='f1' Id='637795220123356403' IndexInText='161' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</Literal>
								<OpeningRoundBrace Name='(' Id='637795220123356452' IndexInText='163' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<Literal Id='637795220123356579' IndexInText='164' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
									<NameExpressionItem Name='x2' Id='637795220123356574' IndexInText='164' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</Literal>
								<Comma Name=',' Id='637795220123356618' IndexInText='166' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<BinaryOperator Name='+' Priority='30' Id='637795220123534001' IndexInText='168' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
									<Operand1.BinaryOperator Name='+' Priority='30' Id='637795220123533965' IndexInText='168' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
										<Operand1.Literal Id='637795220123356816' IndexInText='168' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
											<NameExpressionItem Name='x3' Id='637795220123356795' IndexInText='168' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</Operand1.Literal>
										<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637795220123356937' IndexInText='170' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
											<OperatorNameParts>
												<Name Name='+' Id='637795220123356920' IndexInText='170' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											</OperatorNameParts>
										</OperatorInfo>
										<Operand2.BinaryOperator Name='*' Priority='20' Id='637795220123533990' IndexInText='171' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
											<Operand1.Literal Id='637795220123357097' IndexInText='171' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
												<NameExpressionItem Name='x4' Id='637795220123357092' IndexInText='171' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											</Operand1.Literal>
											<OperatorInfo OperatorType='BinaryOperator' Name='*' Priority='20' Id='637795220123357165' IndexInText='173' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
												<OperatorNameParts>
													<Name Name='*' Id='637795220123357159' IndexInText='173' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												</OperatorNameParts>
											</OperatorInfo>
											<Operand2.ConstantNumericValue Id='637795220123452747' IndexInText='174' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
												<NameExpressionItem Name='5' Id='637795220123452693' IndexInText='174' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
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
									<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637795220123453024' IndexInText='175' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
										<OperatorNameParts>
											<Name Name='+' Id='637795220123453011' IndexInText='175' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</OperatorNameParts>
									</OperatorInfo>
									<Operand2.Braces Id='637795220123453254' IndexInText='176' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
										<RegularItems>
											<Literal Id='637795220123453208' IndexInText='176' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
												<NameExpressionItem Name='x' Id='637795220123453203' IndexInText='176' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											</Literal>
											<OpeningSquareBrace Name='[' Id='637795220123453258' IndexInText='177' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<ConstantNumericValue Id='637795220123533716' IndexInText='178' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
												<NameExpressionItem Name='1' Id='637795220123533653' IndexInText='178' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												<OtherProperties>
													<SucceededNumericTypeDescriptor ObjectId='35' NumberTypeId='1581134183960' Interface='UniversalExpressionParser.NumericTypeDescriptor'/>
												</OtherProperties>
											</ConstantNumericValue>
											<ClosingSquareBrace Name=']' Id='637795220123533901' IndexInText='179' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</RegularItems>
										<Children>
											<ConstantNumericValue Id='637795220123533716' IndexInText='178' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'/>
										</Children>
										<OtherProperties>
											<NamedExpressionItem Id='637795220123453208' IndexInText='176' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
											<OpeningBraceInfo Name='[' Id='637795220123453258' IndexInText='177' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<ClosingBraceInfo Name=']' Id='637795220123533901' IndexInText='179' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<Parameters ObjectId='46' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
												<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220123533716' IndexInText='178' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'/>
											</Parameters>
										</OtherProperties>
									</Operand2.Braces>
								</BinaryOperator>
								<ClosingRoundBrace Name=')' Id='637795220123534063' IndexInText='180' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</RegularItems>
							<Children>
								<Literal Id='637795220123356579' IndexInText='164' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								<BinaryOperator Name='+' Priority='30' Id='637795220123534001' IndexInText='168' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
							</Children>
							<OtherProperties>
								<NamedExpressionItem Id='637795220123356408' IndexInText='161' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								<OpeningBraceInfo Name='(' Id='637795220123356452' IndexInText='163' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<ClosingBraceInfo Name=')' Id='637795220123534063' IndexInText='180' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<Parameters ObjectId='48' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
									<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220123356579' IndexInText='164' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name='+' Priority='30' Id='637795220123534001' IndexInText='168' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
								</Parameters>
							</OtherProperties>
						</Operand2.Braces>
					</Operand1.BinaryOperator>
					<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637795220123534187' IndexInText='181' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
						<OperatorNameParts>
							<Name Name='+' Id='637795220123534176' IndexInText='181' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OperatorNameParts>
					</OperatorInfo>
					<Operand2.Braces Id='637795220123534430' IndexInText='193' ItemLength='44' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<Literal Id='637795220123534372' IndexInText='193' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='matrix1' Id='637795220123534367' IndexInText='193' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<OpeningSquareBrace Name='[' Id='637795220123534435' IndexInText='200' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Braces Id='637795220123534471' IndexInText='201' ItemLength='14' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
								<RegularItems>
									<OpeningSquareBrace Name='[' Id='637795220123534474' IndexInText='201' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<BinaryOperator Name='+' Priority='30' Id='637795220123614021' IndexInText='202' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
										<Operand1.Literal Id='637795220123534561' IndexInText='202' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
											<NameExpressionItem Name='y1' Id='637795220123534556' IndexInText='202' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</Operand1.Literal>
										<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637795220123534623' IndexInText='204' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
											<OperatorNameParts>
												<Name Name='+' Id='637795220123534617' IndexInText='204' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											</OperatorNameParts>
										</OperatorInfo>
										<Operand2.ConstantNumericValue Id='637795220123613856' IndexInText='205' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
											<NameExpressionItem Name='3' Id='637795220123613806' IndexInText='205' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<OtherProperties>
												<SucceededNumericTypeDescriptor ObjectId='35' NumberTypeId='1581134183960' Interface='UniversalExpressionParser.NumericTypeDescriptor'/>
											</OtherProperties>
										</Operand2.ConstantNumericValue>
									</BinaryOperator>
									<Comma Name=',' Id='637795220123614097' IndexInText='206' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Braces Id='637795220123614280' IndexInText='208' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
										<RegularItems>
											<Literal Id='637795220123614241' IndexInText='208' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
												<NameExpressionItem Name='f1' Id='637795220123614236' IndexInText='208' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											</Literal>
											<OpeningRoundBrace Name='(' Id='637795220123614284' IndexInText='210' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<Literal Id='637795220123614383' IndexInText='211' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
												<NameExpressionItem Name='x4' Id='637795220123614378' IndexInText='211' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											</Literal>
											<ClosingRoundBrace Name=')' Id='637795220123614416' IndexInText='213' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</RegularItems>
										<Children>
											<Literal Id='637795220123614383' IndexInText='211' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
										</Children>
										<OtherProperties>
											<NamedExpressionItem Id='637795220123614241' IndexInText='208' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
											<OpeningBraceInfo Name='(' Id='637795220123614284' IndexInText='210' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<ClosingBraceInfo Name=')' Id='637795220123614416' IndexInText='213' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<Parameters ObjectId='72' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
												<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220123614383' IndexInText='211' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
											</Parameters>
										</OtherProperties>
									</Braces>
									<ClosingSquareBrace Name=']' Id='637795220123614455' IndexInText='214' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Children>
									<BinaryOperator Name='+' Priority='30' Id='637795220123614021' IndexInText='202' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
									<Braces Id='637795220123614280' IndexInText='208' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
								</Children>
								<OtherProperties>
									<OpeningBraceInfo Name='[' Id='637795220123534474' IndexInText='201' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ClosingBraceInfo Name=']' Id='637795220123614455' IndexInText='214' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
									<Parameters ObjectId='74' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name='+' Priority='30' Id='637795220123614021' IndexInText='202' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220123614280' IndexInText='208' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
									</Parameters>
								</OtherProperties>
							</Braces>
							<Comma Name=',' Id='637795220123614486' IndexInText='215' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Literal Id='637795220123614574' IndexInText='217' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='x3' Id='637795220123614569' IndexInText='217' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<Comma Name=',' Id='637795220123614602' IndexInText='219' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Braces Id='637795220123614718' IndexInText='221' ItemLength='15' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
								<RegularItems>
									<Literal Id='637795220123614692' IndexInText='221' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='f2' Id='637795220123614683' IndexInText='221' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<OpeningRoundBrace Name='(' Id='637795220123614721' IndexInText='223' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637795220123614809' IndexInText='224' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='x3' Id='637795220123614800' IndexInText='224' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<Comma Name=',' Id='637795220123614837' IndexInText='226' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Braces Id='637795220123614950' IndexInText='228' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
										<RegularItems>
											<Literal Id='637795220123614920' IndexInText='228' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
												<NameExpressionItem Name='m2' Id='637795220123614915' IndexInText='228' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											</Literal>
											<OpeningSquareBrace Name='[' Id='637795220123614954' IndexInText='230' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<BinaryOperator Name='+' Priority='30' Id='637795220123695082' IndexInText='231' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
												<Operand1.Literal Id='637795220123615037' IndexInText='231' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
													<NameExpressionItem Name='x' Id='637795220123615033' IndexInText='231' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												</Operand1.Literal>
												<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637795220123615107' IndexInText='232' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
													<OperatorNameParts>
														<Name Name='+' Id='637795220123615096' IndexInText='232' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
													</OperatorNameParts>
												</OperatorInfo>
												<Operand2.ConstantNumericValue Id='637795220123694872' IndexInText='233' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
													<NameExpressionItem Name='5' Id='637795220123694813' IndexInText='233' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
													<OtherProperties>
														<SucceededNumericTypeDescriptor ObjectId='35' NumberTypeId='1581134183960' Interface='UniversalExpressionParser.NumericTypeDescriptor'/>
													</OtherProperties>
												</Operand2.ConstantNumericValue>
											</BinaryOperator>
											<ClosingSquareBrace Name=']' Id='637795220123695161' IndexInText='234' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</RegularItems>
										<Children>
											<BinaryOperator Name='+' Priority='30' Id='637795220123695082' IndexInText='231' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
										</Children>
										<OtherProperties>
											<NamedExpressionItem Id='637795220123614920' IndexInText='228' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
											<OpeningBraceInfo Name='[' Id='637795220123614954' IndexInText='230' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<ClosingBraceInfo Name=']' Id='637795220123695161' IndexInText='234' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<Parameters ObjectId='98' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
												<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name='+' Priority='30' Id='637795220123695082' IndexInText='231' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
											</Parameters>
										</OtherProperties>
									</Braces>
									<ClosingRoundBrace Name=')' Id='637795220123695228' IndexInText='235' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Children>
									<Literal Id='637795220123614809' IndexInText='224' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<Braces Id='637795220123614950' IndexInText='228' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
								</Children>
								<OtherProperties>
									<NamedExpressionItem Id='637795220123614692' IndexInText='221' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<OpeningBraceInfo Name='(' Id='637795220123614721' IndexInText='223' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ClosingBraceInfo Name=')' Id='637795220123695228' IndexInText='235' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Parameters ObjectId='100' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220123614809' IndexInText='224' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220123614950' IndexInText='228' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
									</Parameters>
								</OtherProperties>
							</Braces>
							<ClosingSquareBrace Name=']' Id='637795220123695263' IndexInText='236' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<Braces Id='637795220123534471' IndexInText='201' ItemLength='14' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
							<Literal Id='637795220123614574' IndexInText='217' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							<Braces Id='637795220123614718' IndexInText='221' ItemLength='15' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
						</Children>
						<OtherProperties>
							<NamedExpressionItem Id='637795220123534372' IndexInText='193' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							<OpeningBraceInfo Name='[' Id='637795220123534435' IndexInText='200' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=']' Id='637795220123695263' IndexInText='236' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Parameters ObjectId='102' Count='3' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220123534471' IndexInText='201' ItemLength='14' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220123614574' IndexInText='217' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220123614718' IndexInText='221' ItemLength='15' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Operand2.Braces>
				</Operand2.BinaryOperator>
			</BinaryOperator>
			<ExpressionSeparator Name=';' Id='637795220123695355' IndexInText='237' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<BinaryOperator Name='=>' Priority='1000' Id='637795220123696443' IndexInText='242' ItemLength='15' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
				<Operand1.Braces Id='637795220123695543' IndexInText='242' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
					<RegularItems>
						<Literal Id='637795220123695511' IndexInText='242' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='f1' Id='637795220123695505' IndexInText='242' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Literal>
						<OpeningRoundBrace Name='(' Id='637795220123695548' IndexInText='244' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<Literal Id='637795220123695811' IndexInText='245' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='x' Id='637795220123695804' IndexInText='245' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Literal>
						<Comma Name=',' Id='637795220123695851' IndexInText='246' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<Literal Id='637795220123695941' IndexInText='248' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='y' Id='637795220123695936' IndexInText='248' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Literal>
						<ClosingRoundBrace Name=')' Id='637795220123695969' IndexInText='249' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</RegularItems>
					<Children>
						<Literal Id='637795220123695811' IndexInText='245' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
						<Literal Id='637795220123695941' IndexInText='248' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					</Children>
					<OtherProperties>
						<NamedExpressionItem Id='637795220123695511' IndexInText='242' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
						<OpeningBraceInfo Name='(' Id='637795220123695548' IndexInText='244' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<ClosingBraceInfo Name=')' Id='637795220123695969' IndexInText='249' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<Parameters ObjectId='115' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
							<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220123695811' IndexInText='245' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220123695941' IndexInText='248' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
						</Parameters>
					</OtherProperties>
				</Operand1.Braces>
				<OperatorInfo OperatorType='BinaryOperator' Name='=>' Priority='1000' Id='637795220123696063' IndexInText='251' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
					<OperatorNameParts>
						<Name Name='=>' Id='637795220123696052' IndexInText='251' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</OperatorNameParts>
				</OperatorInfo>
				<Operand2.BinaryOperator Name='+' Priority='30' Id='637795220123696453' IndexInText='254' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
					<Operand1.Literal Id='637795220123696201' IndexInText='254' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='x' Id='637795220123696197' IndexInText='254' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Operand1.Literal>
					<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637795220123696269' IndexInText='255' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
						<OperatorNameParts>
							<Name Name='+' Id='637795220123696263' IndexInText='255' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OperatorNameParts>
					</OperatorInfo>
					<Operand2.Literal Id='637795220123696408' IndexInText='256' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='y' Id='637795220123696399' IndexInText='256' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Operand2.Literal>
				</Operand2.BinaryOperator>
			</BinaryOperator>
			<ExpressionSeparator Name=';' Id='637795220123696475' IndexInText='257' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<Braces Id='637795220123696596' IndexInText='262' ItemLength='137' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
				<RegularItems>
					<Literal Id='637795220123696568' IndexInText='262' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='f2' Id='637795220123696563' IndexInText='262' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<OpeningSquareBrace Name='[' Id='637795220123696600' IndexInText='264' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Literal Id='637795220123696693' IndexInText='265' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='x' Id='637795220123696689' IndexInText='265' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<Comma Name=',' Id='637795220123696724' IndexInText='266' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Literal Id='637795220123696812' IndexInText='268' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='y' Id='637795220123696808' IndexInText='268' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<ClosingSquareBrace Name=']' Id='637795220123696840' IndexInText='269' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Postfixes>
					<CodeBlock Id='637795220123696874' IndexInText='273' ItemLength='126' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
						<RegularItems>
							<CodeBlockStartMarker Name='{' Id='637795220123696869' IndexInText='273' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637795220123696938' IndexInText='398' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<OtherProperties>
							<CodeBlockStartMarker Name='{' Id='637795220123696869' IndexInText='273' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637795220123696938' IndexInText='398' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OtherProperties>
					</CodeBlock>
				</Postfixes>
				<Children>
					<Literal Id='637795220123696693' IndexInText='265' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					<Literal Id='637795220123696812' IndexInText='268' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
				</Children>
				<OtherProperties>
					<NamedExpressionItem Id='637795220123696568' IndexInText='262' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					<OpeningBraceInfo Name='[' Id='637795220123696600' IndexInText='264' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ClosingBraceInfo Name=']' Id='637795220123696840' IndexInText='269' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Parameters ObjectId='139' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220123696693' IndexInText='265' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220123696812' IndexInText='268' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					</Parameters>
				</OtherProperties>
			</Braces>
		</RegularItems>
		<Children>
			<BinaryOperator Name='=' Priority='2000' Id='637795220123695299' IndexInText='150' ItemLength='87' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
			<BinaryOperator Name='=>' Priority='1000' Id='637795220123696443' IndexInText='242' ItemLength='15' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
			<Braces Id='637795220123696596' IndexInText='262' ItemLength='137' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
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
	<MainExpressionItem Id='637795220123355285' IndexInText='150' ItemLength='249' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'/>
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
	<ExpressionItemSeries Id='637795220124822730' IndexInText='259' ItemLength='28' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'>
		<RegularItems>
			<BinaryOperator Name='=' Priority='2000' Id='637795220124908713' IndexInText='259' ItemLength='27' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
				<Operand1.Literal Id='637795220124823107' IndexInText='259' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
					<NameExpressionItem Name='y' Id='637795220124823102' IndexInText='263' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<AppliedKeywords>
						<Keyword Name='var' Id='637795220124822953' IndexInText='259' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
							<LanguageKeywordInfo ObjectId='6' Id='637781064051574641' Keyword='var' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
						</Keyword>
					</AppliedKeywords>
				</Operand1.Literal>
				<OperatorInfo OperatorType='BinaryOperator' Name='=' Priority='2000' Id='637795220124823211' IndexInText='265' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
					<OperatorNameParts>
						<Name Name='=' Id='637795220124823199' IndexInText='265' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</OperatorNameParts>
				</OperatorInfo>
				<Operand2.BinaryOperator Name='+' Priority='30' Id='637795220124908724' IndexInText='267' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
					<Operand1.Literal Id='637795220124823360' IndexInText='267' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='x1' Id='637795220124823355' IndexInText='267' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Operand1.Literal>
					<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637795220124823446' IndexInText='270' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
						<OperatorNameParts>
							<Name Name='+' Id='637795220124823440' IndexInText='270' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OperatorNameParts>
					</OperatorInfo>
					<Operand2.BinaryOperator Name='*' Priority='20' Id='637795220124908732' IndexInText='272' ItemLength='14' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.Braces Id='637795220124823620' IndexInText='272' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
							<RegularItems>
								<Literal Id='637795220124823585' IndexInText='272' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
									<NameExpressionItem Name='f1' Id='637795220124823579' IndexInText='272' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</Literal>
								<OpeningRoundBrace Name='(' Id='637795220124823625' IndexInText='274' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<Literal Id='637795220124823733' IndexInText='275' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
									<NameExpressionItem Name='x2' Id='637795220124823728' IndexInText='275' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</Literal>
								<Comma Name=',' Id='637795220124823771' IndexInText='277' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<BinaryOperator Name='+' Priority='30' Id='637795220124908312' IndexInText='278' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
									<Operand1.Literal Id='637795220124823865' IndexInText='278' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='x3' Id='637795220124823859' IndexInText='278' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Operand1.Literal>
									<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637795220124823922' IndexInText='280' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
										<OperatorNameParts>
											<Name Name='+' Id='637795220124823916' IndexInText='280' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</OperatorNameParts>
									</OperatorInfo>
									<Operand2.ConstantNumericValue Id='637795220124908134' IndexInText='281' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
										<NameExpressionItem Name='1' Id='637795220124908087' IndexInText='281' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										<OtherProperties>
											<SucceededNumericTypeDescriptor ObjectId='29' NumberTypeId='1581134183960' Interface='UniversalExpressionParser.NumericTypeDescriptor'>
												<RegularExpressions ObjectId='30' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[System.String]'>
													<System.String value='^\d+' />
												</RegularExpressions>
											</SucceededNumericTypeDescriptor>
										</OtherProperties>
									</Operand2.ConstantNumericValue>
								</BinaryOperator>
								<ClosingRoundBrace Name=')' Id='637795220124908375' IndexInText='282' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</RegularItems>
							<Children>
								<Literal Id='637795220124823733' IndexInText='275' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								<BinaryOperator Name='+' Priority='30' Id='637795220124908312' IndexInText='278' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
							</Children>
							<OtherProperties>
								<NamedExpressionItem Id='637795220124823585' IndexInText='272' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								<OpeningBraceInfo Name='(' Id='637795220124823625' IndexInText='274' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<ClosingBraceInfo Name=')' Id='637795220124908375' IndexInText='282' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<Parameters ObjectId='32' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
									<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220124823733' IndexInText='275' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name='+' Priority='30' Id='637795220124908312' IndexInText='278' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
								</Parameters>
							</OtherProperties>
						</Operand1.Braces>
						<OperatorInfo OperatorType='BinaryOperator' Name='*' Priority='20' Id='637795220124908487' IndexInText='283' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<Name Name='*' Id='637795220124908477' IndexInText='283' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OperatorNameParts>
						</OperatorInfo>
						<Operand2.Literal Id='637795220124908670' IndexInText='284' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='x4' Id='637795220124908665' IndexInText='284' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand2.Literal>
					</Operand2.BinaryOperator>
				</Operand2.BinaryOperator>
			</BinaryOperator>
			<ExpressionSeparator Name=';' Id='637795220124908768' IndexInText='286' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
		</RegularItems>
		<Children>
			<BinaryOperator Name='=' Priority='2000' Id='637795220124908713' IndexInText='259' ItemLength='27' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
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
	<MainExpressionItem Id='637795220124822730' IndexInText='259' ItemLength='28' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'/>
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
	<ExpressionItemSeries Id='637795220124762090' IndexInText='83' ItemLength='25' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'>
		<RegularItems>
			<BinaryOperator Name='=' Priority='2000' Id='637795220124763748' IndexInText='83' ItemLength='24' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
				<Operand1.Literal Id='637795220124762418' IndexInText='83' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
					<NameExpressionItem Name='y1' Id='637795220124762412' IndexInText='87' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<AppliedKeywords>
						<Keyword Name='var' Id='637795220124762269' IndexInText='83' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
							<LanguageKeywordInfo ObjectId='6' Id='637781064051574641' Keyword='var' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
						</Keyword>
					</AppliedKeywords>
				</Operand1.Literal>
				<OperatorInfo OperatorType='BinaryOperator' Name='=' Priority='2000' Id='637795220124762522' IndexInText='90' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
					<OperatorNameParts>
						<Name Name='=' Id='637795220124762510' IndexInText='90' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</OperatorNameParts>
				</OperatorInfo>
				<Operand2.BinaryOperator Name='*' Priority='20' Id='637795220124763756' IndexInText='92' ItemLength='15' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
					<Operand1.Braces Id='637795220124762665' IndexInText='92' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<OpeningSquareBrace Name='[' Id='637795220124762670' IndexInText='92' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<BinaryOperator Name='+' Priority='30' Id='637795220124763075' IndexInText='93' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
								<Operand1.Literal Id='637795220124762826' IndexInText='93' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
									<NameExpressionItem Name='x1' Id='637795220124762820' IndexInText='93' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</Operand1.Literal>
								<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637795220124762892' IndexInText='95' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
									<OperatorNameParts>
										<Name Name='+' Id='637795220124762886' IndexInText='95' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</OperatorNameParts>
								</OperatorInfo>
								<Operand2.Literal Id='637795220124763029' IndexInText='96' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
									<NameExpressionItem Name='x2' Id='637795220124763025' IndexInText='96' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</Operand2.Literal>
							</BinaryOperator>
							<ClosingSquareBrace Name=']' Id='637795220124763134' IndexInText='98' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<BinaryOperator Name='+' Priority='30' Id='637795220124763075' IndexInText='93' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
						</Children>
						<OtherProperties>
							<OpeningBraceInfo Name='[' Id='637795220124762670' IndexInText='92' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=']' Id='637795220124763134' IndexInText='98' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
							<Parameters ObjectId='20' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name='+' Priority='30' Id='637795220124763075' IndexInText='93' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Operand1.Braces>
					<OperatorInfo OperatorType='BinaryOperator' Name='*' Priority='20' Id='637795220124763202' IndexInText='99' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
						<OperatorNameParts>
							<Name Name='*' Id='637795220124763196' IndexInText='99' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OperatorNameParts>
					</OperatorInfo>
					<Operand2.Braces Id='637795220124763336' IndexInText='100' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<OpeningRoundBrace Name='(' Id='637795220124763340' IndexInText='100' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<BinaryOperator Name='-' Priority='30' Id='637795220124763697' IndexInText='101' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
								<Operand1.Literal Id='637795220124763433' IndexInText='101' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
									<NameExpressionItem Name='x3' Id='637795220124763428' IndexInText='101' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</Operand1.Literal>
								<OperatorInfo OperatorType='BinaryOperator' Name='-' Priority='30' Id='637795220124763511' IndexInText='103' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
									<OperatorNameParts>
										<Name Name='-' Id='637795220124763506' IndexInText='103' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</OperatorNameParts>
								</OperatorInfo>
								<Operand2.Literal Id='637795220124763652' IndexInText='104' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
									<NameExpressionItem Name='x4' Id='637795220124763647' IndexInText='104' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</Operand2.Literal>
							</BinaryOperator>
							<ClosingRoundBrace Name=')' Id='637795220124763716' IndexInText='106' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<BinaryOperator Name='-' Priority='30' Id='637795220124763697' IndexInText='101' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
						</Children>
						<OtherProperties>
							<OpeningBraceInfo Name='(' Id='637795220124763340' IndexInText='100' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=')' Id='637795220124763716' IndexInText='106' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
							<Parameters ObjectId='33' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name='-' Priority='30' Id='637795220124763697' IndexInText='101' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Operand2.Braces>
				</Operand2.BinaryOperator>
			</BinaryOperator>
			<ExpressionSeparator Name=';' Id='637795220124763784' IndexInText='107' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
		</RegularItems>
		<Children>
			<BinaryOperator Name='=' Priority='2000' Id='637795220124763748' IndexInText='83' ItemLength='24' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
		</Children>
	</ExpressionItemSeries>
	<ParseErrorData ObjectId='35' HasCriticalErrors='False' Interface='UniversalExpressionParser.IParseErrorData'>
		<AllCodeItemErrors ObjectId='36' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICodeItemParseErrorData]'/>
	</ParseErrorData>
	<SortedCommentedOutCodeInfos ObjectId='37' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICommentedOutCodeInfo]'>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='38' IsLineComment='True' IndexInText='0' ItemLength='81'/>
	</SortedCommentedOutCodeInfos>
	<MainExpressionItem Id='637795220124762090' IndexInText='83' ItemLength='25' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'/>
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
	<ExpressionItemSeries Id='637795220124782936' IndexInText='84' ItemLength='36' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'>
		<RegularItems>
			<BinaryOperator Name='=' Priority='2000' Id='637795220124784449' IndexInText='84' ItemLength='35' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
				<Operand1.Literal Id='637795220124783298' IndexInText='84' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
					<NameExpressionItem Name='z' Id='637795220124783289' IndexInText='84' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</Operand1.Literal>
				<OperatorInfo OperatorType='BinaryOperator' Name='=' Priority='2000' Id='637795220124783398' IndexInText='86' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
					<OperatorNameParts>
						<Name Name='=' Id='637795220124783387' IndexInText='86' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</OperatorNameParts>
				</OperatorInfo>
				<Operand2.PrefixUnaryOperator Name='!' Priority='0' Id='637795220124784459' IndexInText='88' ItemLength='31' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
					<OperatorInfo OperatorType='PrefixUnaryOperator' Name='!' Priority='0' Id='637795220124783476' IndexInText='88' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
						<OperatorNameParts>
							<Name Name='!' Id='637795220124783471' IndexInText='88' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OperatorNameParts>
					</OperatorInfo>
					<Operand1.Braces Id='637795220124783595' IndexInText='89' ItemLength='30' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<OpeningRoundBrace Name='(' Id='637795220124783600' IndexInText='89' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<BinaryOperator Name='&&' Priority='80' Id='637795220124784339' IndexInText='90' ItemLength='28' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
								<Operand1.PostfixUnaryOperator Name='IS NOT NULL' Priority='1' Id='637795220124784322' IndexInText='90' ItemLength='14' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
									<Operand1.Literal Id='637795220124783730' IndexInText='90' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='x1' Id='637795220124783725' IndexInText='90' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Operand1.Literal>
									<OperatorInfo OperatorType='PostfixUnaryOperator' Name='IS NOT NULL' Priority='1' Id='637795220124783786' IndexInText='93' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
										<OperatorNameParts>
											<Name Name='IS' Id='637795220124783763' IndexInText='93' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<Name Name='NOT' Id='637795220124783773' IndexInText='96' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<Name Name='NULL' Id='637795220124783781' IndexInText='100' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</OperatorNameParts>
									</OperatorInfo>
								</Operand1.PostfixUnaryOperator>
								<OperatorInfo OperatorType='BinaryOperator' Name='&&' Priority='80' Id='637795220124783869' IndexInText='105' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
									<OperatorNameParts>
										<Name Name='&&' Id='637795220124783865' IndexInText='105' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</OperatorNameParts>
								</OperatorInfo>
								<Operand2.PostfixUnaryOperator Name='IS NULL' Priority='1' Id='637795220124784348' IndexInText='108' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
									<Operand1.Literal Id='637795220124784134' IndexInText='108' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='x2' Id='637795220124784126' IndexInText='108' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Operand1.Literal>
									<OperatorInfo OperatorType='PostfixUnaryOperator' Name='IS NULL' Priority='1' Id='637795220124784201' IndexInText='111' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
										<OperatorNameParts>
											<Name Name='IS' Id='637795220124784188' IndexInText='111' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<Name Name='NULL' Id='637795220124784196' IndexInText='114' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</OperatorNameParts>
									</OperatorInfo>
								</Operand2.PostfixUnaryOperator>
							</BinaryOperator>
							<ClosingRoundBrace Name=')' Id='637795220124784405' IndexInText='118' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<BinaryOperator Name='&&' Priority='80' Id='637795220124784339' IndexInText='90' ItemLength='28' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
						</Children>
						<OtherProperties>
							<OpeningBraceInfo Name='(' Id='637795220124783600' IndexInText='89' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=')' Id='637795220124784405' IndexInText='118' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
							<Parameters ObjectId='29' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name='&&' Priority='80' Id='637795220124784339' IndexInText='90' ItemLength='28' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Operand1.Braces>
				</Operand2.PrefixUnaryOperator>
			</BinaryOperator>
			<ExpressionSeparator Name=';' Id='637795220124784482' IndexInText='119' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
		</RegularItems>
		<Children>
			<BinaryOperator Name='=' Priority='2000' Id='637795220124784449' IndexInText='84' ItemLength='35' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
		</Children>
	</ExpressionItemSeries>
	<ParseErrorData ObjectId='31' HasCriticalErrors='False' Interface='UniversalExpressionParser.IParseErrorData'>
		<AllCodeItemErrors ObjectId='32' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICodeItemParseErrorData]'/>
	</ParseErrorData>
	<SortedCommentedOutCodeInfos ObjectId='33' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICommentedOutCodeInfo]'>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='34' IsLineComment='True' IndexInText='0' ItemLength='38'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='35' IsLineComment='True' IndexInText='40' ItemLength='42'/>
	</SortedCommentedOutCodeInfos>
	<MainExpressionItem Id='637795220124782936' IndexInText='84' ItemLength='36' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'/>
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
	<ExpressionItemSeries Id='637795220124802258' IndexInText='288' ItemLength='18' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'>
		<RegularItems>
			<Braces Id='637795220124803012' IndexInText='288' ItemLength='18' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
				<RegularItems>
					<Literal Id='637795220124802604' IndexInText='288' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='println' Id='637795220124802596' IndexInText='288' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<OpeningRoundBrace Name='(' Id='637795220124803032' IndexInText='295' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<BinaryOperator Name='+' Priority='30' Id='637795220124803734' IndexInText='296' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.PostfixUnaryOperator Name='++' Priority='1' Id='637795220124803711' IndexInText='296' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
							<Operand1.PostfixUnaryOperator Name='++' Priority='1' Id='637795220124803698' IndexInText='296' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
								<Operand1.Literal Id='637795220124803208' IndexInText='296' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
									<NameExpressionItem Name='x1' Id='637795220124803201' IndexInText='296' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</Operand1.Literal>
								<OperatorInfo OperatorType='PostfixUnaryOperator' Name='++' Priority='1' Id='637795220124803298' IndexInText='298' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
									<OperatorNameParts>
										<Name Name='++' Id='637795220124803293' IndexInText='298' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</OperatorNameParts>
								</OperatorInfo>
							</Operand1.PostfixUnaryOperator>
							<OperatorInfo OperatorType='PostfixUnaryOperator' Name='++' Priority='1' Id='637795220124803397' IndexInText='300' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
								<OperatorNameParts>
									<Name Name='++' Id='637795220124803392' IndexInText='300' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</OperatorNameParts>
							</OperatorInfo>
						</Operand1.PostfixUnaryOperator>
						<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637795220124803489' IndexInText='302' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<Name Name='+' Id='637795220124803480' IndexInText='302' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OperatorNameParts>
						</OperatorInfo>
						<Operand2.Literal Id='637795220124803627' IndexInText='303' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='x2' Id='637795220124803622' IndexInText='303' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand2.Literal>
					</BinaryOperator>
					<ClosingRoundBrace Name=')' Id='637795220124803786' IndexInText='305' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Children>
					<BinaryOperator Name='+' Priority='30' Id='637795220124803734' IndexInText='296' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
				</Children>
				<OtherProperties>
					<NamedExpressionItem Id='637795220124802604' IndexInText='288' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					<OpeningBraceInfo Name='(' Id='637795220124803032' IndexInText='295' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ClosingBraceInfo Name=')' Id='637795220124803786' IndexInText='305' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Parameters ObjectId='20' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name='+' Priority='30' Id='637795220124803734' IndexInText='296' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
					</Parameters>
				</OtherProperties>
			</Braces>
		</RegularItems>
		<Children>
			<Braces Id='637795220124803012' IndexInText='288' ItemLength='18' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
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
	<MainExpressionItem Id='637795220124802258' IndexInText='288' ItemLength='18' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'/>
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
	<ExpressionItemSeries Id='637795220124926762' IndexInText='161' ItemLength='274' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'>
		<RegularItems>
			<PrefixUnaryOperator Name='return' Priority='2147483647' Id='637795220125003203' IndexInText='161' ItemLength='14' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
				<OperatorInfo OperatorType='PrefixUnaryOperator' Name='return' Priority='2147483647' Id='637795220124927005' IndexInText='161' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
					<OperatorNameParts>
						<Name Name='return' Id='637795220124926992' IndexInText='161' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</OperatorNameParts>
				</OperatorInfo>
				<Operand1.BinaryOperator Name='+' Priority='30' Id='637795220125003227' IndexInText='168' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
					<Operand1.Literal Id='637795220124927150' IndexInText='168' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='x' Id='637795220124927145' IndexInText='168' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Operand1.Literal>
					<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637795220124927237' IndexInText='169' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
						<OperatorNameParts>
							<Name Name='+' Id='637795220124927231' IndexInText='169' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OperatorNameParts>
					</OperatorInfo>
					<Operand2.BinaryOperator Name='*' Priority='20' Id='637795220125003237' IndexInText='170' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.ConstantNumericValue Id='637795220125002712' IndexInText='170' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
							<NameExpressionItem Name='2.5' Id='637795220125002661' IndexInText='170' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<OtherProperties>
								<SucceededNumericTypeDescriptor ObjectId='13' NumberTypeId='1581134136626' Interface='UniversalExpressionParser.NumericTypeDescriptor'>
									<RegularExpressions ObjectId='14' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[System.String]'>
										<System.String value='^(\d+\.\d+|\d+\.|\.\d+)' />
									</RegularExpressions>
								</SucceededNumericTypeDescriptor>
							</OtherProperties>
						</Operand1.ConstantNumericValue>
						<OperatorInfo OperatorType='BinaryOperator' Name='*' Priority='20' Id='637795220125002965' IndexInText='173' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<Name Name='*' Id='637795220125002951' IndexInText='173' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OperatorNameParts>
						</OperatorInfo>
						<Operand2.Literal Id='637795220125003144' IndexInText='174' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='y' Id='637795220125003139' IndexInText='174' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand2.Literal>
					</Operand2.BinaryOperator>
				</Operand1.BinaryOperator>
			</PrefixUnaryOperator>
			<ExpressionSeparator Name=';' Id='637795220125003300' IndexInText='175' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<BinaryOperator Name=':' Priority='0' Id='637795220125085772' IndexInText='221' ItemLength='214' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
				<Operand1.Braces Id='637795220125003449' IndexInText='221' ItemLength='16' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
					<RegularItems>
						<Literal Id='637795220125003418' IndexInText='221' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='f1' Id='637795220125003413' IndexInText='221' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Literal>
						<OpeningRoundBrace Name='(' Id='637795220125003458' IndexInText='223' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<BinaryOperator Name=':' Priority='0' Id='637795220125003790' IndexInText='224' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
							<Operand1.Literal Id='637795220125003554' IndexInText='224' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='x' Id='637795220125003549' IndexInText='224' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Operand1.Literal>
							<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637795220125003610' IndexInText='225' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
								<OperatorNameParts>
									<Name Name=':' Id='637795220125003603' IndexInText='225' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</OperatorNameParts>
							</OperatorInfo>
							<Operand2.Literal Id='637795220125003754' IndexInText='226' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='int' Id='637795220125003749' IndexInText='226' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Operand2.Literal>
						</BinaryOperator>
						<Comma Name=',' Id='637795220125003821' IndexInText='229' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<BinaryOperator Name=':' Priority='0' Id='637795220125004141' IndexInText='231' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
							<Operand1.Literal Id='637795220125003914' IndexInText='231' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='y' Id='637795220125003909' IndexInText='231' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Operand1.Literal>
							<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637795220125003963' IndexInText='232' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
								<OperatorNameParts>
									<Name Name=':' Id='637795220125003957' IndexInText='232' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</OperatorNameParts>
							</OperatorInfo>
							<Operand2.Literal Id='637795220125004104' IndexInText='233' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='int' Id='637795220125004099' IndexInText='233' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Operand2.Literal>
						</BinaryOperator>
						<ClosingRoundBrace Name=')' Id='637795220125004158' IndexInText='236' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</RegularItems>
					<Children>
						<BinaryOperator Name=':' Priority='0' Id='637795220125003790' IndexInText='224' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
						<BinaryOperator Name=':' Priority='0' Id='637795220125004141' IndexInText='231' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
					</Children>
					<OtherProperties>
						<NamedExpressionItem Id='637795220125003418' IndexInText='221' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
						<OpeningBraceInfo Name='(' Id='637795220125003458' IndexInText='223' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<ClosingBraceInfo Name=')' Id='637795220125004158' IndexInText='236' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<Parameters ObjectId='41' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
							<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name=':' Priority='0' Id='637795220125003790' IndexInText='224' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
							<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name=':' Priority='0' Id='637795220125004141' IndexInText='231' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
						</Parameters>
					</OtherProperties>
				</Operand1.Braces>
				<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637795220125004218' IndexInText='238' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
					<OperatorNameParts>
						<Name Name=':' Id='637795220125004212' IndexInText='238' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</OperatorNameParts>
				</OperatorInfo>
				<Operand2.Literal Id='637795220125004367' IndexInText='240' ItemLength='195' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
					<NameExpressionItem Name='bool' Id='637795220125004362' IndexInText='240' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Postfixes>
						<CodeBlock Id='637795220125004408' IndexInText='246' ItemLength='189' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
							<RegularItems>
								<CodeBlockStartMarker Name='{' Id='637795220125004404' IndexInText='246' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<PrefixUnaryOperator Name='return' Priority='2147483647' Id='637795220125085605' IndexInText='413' ItemLength='18' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
									<OperatorInfo OperatorType='PrefixUnaryOperator' Name='return' Priority='2147483647' Id='637795220125004481' IndexInText='413' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
										<OperatorNameParts>
											<Name Name='return' Id='637795220125004475' IndexInText='413' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</OperatorNameParts>
									</OperatorInfo>
									<Operand1.BinaryOperator Name='>' Priority='50' Id='637795220125085640' IndexInText='420' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
										<Operand1.BinaryOperator Name='+' Priority='30' Id='637795220125085631' IndexInText='420' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
											<Operand1.Braces Id='637795220125004634' IndexInText='420' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
												<RegularItems>
													<Literal Id='637795220125004590' IndexInText='420' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
														<NameExpressionItem Name='f' Id='637795220125004585' IndexInText='420' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
													</Literal>
													<OpeningRoundBrace Name='(' Id='637795220125004637' IndexInText='421' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
													<Literal Id='637795220125004727' IndexInText='422' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
														<NameExpressionItem Name='x' Id='637795220125004723' IndexInText='422' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
													</Literal>
													<ClosingRoundBrace Name=')' Id='637795220125004758' IndexInText='423' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												</RegularItems>
												<Children>
													<Literal Id='637795220125004727' IndexInText='422' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
												</Children>
												<OtherProperties>
													<NamedExpressionItem Id='637795220125004590' IndexInText='420' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
													<OpeningBraceInfo Name='(' Id='637795220125004637' IndexInText='421' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
													<ClosingBraceInfo Name=')' Id='637795220125004758' IndexInText='423' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
													<Parameters ObjectId='60' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
														<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220125004727' IndexInText='422' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
													</Parameters>
												</OtherProperties>
											</Operand1.Braces>
											<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637795220125004818' IndexInText='424' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
												<OperatorNameParts>
													<Name Name='+' Id='637795220125004813' IndexInText='424' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												</OperatorNameParts>
											</OperatorInfo>
											<Operand2.Literal Id='637795220125004951' IndexInText='425' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
												<NameExpressionItem Name='y' Id='637795220125004947' IndexInText='425' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											</Operand2.Literal>
										</Operand1.BinaryOperator>
										<OperatorInfo OperatorType='BinaryOperator' Name='>' Priority='50' Id='637795220125005019' IndexInText='427' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
											<OperatorNameParts>
												<Name Name='>' Id='637795220125005010' IndexInText='427' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											</OperatorNameParts>
										</OperatorInfo>
										<Operand2.ConstantNumericValue Id='637795220125085437' IndexInText='429' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
											<NameExpressionItem Name='10' Id='637795220125085392' IndexInText='429' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
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
								<ExpressionSeparator Name=';' Id='637795220125085706' IndexInText='431' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<CodeBlockEndMarker Name='}' Id='637795220125085747' IndexInText='434' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</RegularItems>
							<Children>
								<PrefixUnaryOperator Name='return' Priority='2147483647' Id='637795220125085605' IndexInText='413' ItemLength='18' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
							</Children>
							<OtherProperties>
								<CodeBlockStartMarker Name='{' Id='637795220125004404' IndexInText='246' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<CodeBlockEndMarker Name='}' Id='637795220125085747' IndexInText='434' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OtherProperties>
						</CodeBlock>
					</Postfixes>
				</Operand2.Literal>
			</BinaryOperator>
		</RegularItems>
		<Children>
			<PrefixUnaryOperator Name='return' Priority='2147483647' Id='637795220125003203' IndexInText='161' ItemLength='14' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
			<BinaryOperator Name=':' Priority='0' Id='637795220125085772' IndexInText='221' ItemLength='214' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
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
	<MainExpressionItem Id='637795220124926762' IndexInText='161' ItemLength='274' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'/>
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
	<ExpressionItemSeries Id='637795220124376469' IndexInText='46' ItemLength='414' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'>
		<RegularItems>
			<Braces Id='637795220124376846' IndexInText='46' ItemLength='41' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
				<RegularItems>
					<Literal Id='637795220124376797' IndexInText='46' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='println' Id='637795220124376789' IndexInText='46' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<OpeningRoundBrace Name='(' Id='637795220124376850' IndexInText='53' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<BinaryOperator Name='+' Priority='30' Id='637795220124551264' IndexInText='54' ItemLength='32' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.BinaryOperator Name='+' Priority='30' Id='637795220124551256' IndexInText='54' ItemLength='23' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
							<Operand1.BinaryOperator Name='+' Priority='30' Id='637795220124551245' IndexInText='54' ItemLength='16' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
								<Operand1.PrefixUnaryOperator Name='-' Priority='0' Id='637795220124551218' IndexInText='54' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
									<OperatorInfo OperatorType='PrefixUnaryOperator' Name='-' Priority='0' Id='637795220124376923' IndexInText='54' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
										<OperatorNameParts>
											<Name Name='-' Id='637795220124376908' IndexInText='54' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</OperatorNameParts>
									</OperatorInfo>
									<Operand1.ConstantNumericValue Id='637795220124436640' IndexInText='55' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
										<NameExpressionItem Name='0.5e-3' Id='637795220124436581' IndexInText='55' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										<OtherProperties>
											<SucceededNumericTypeDescriptor ObjectId='14' NumberTypeId='1581134225786' Interface='UniversalExpressionParser.NumericTypeDescriptor'>
												<RegularExpressions ObjectId='15' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[System.String]'>
													<System.String value='^(\d+\.\d+|\d+\.|\.\d+|\d+)(EXP|exp|E|e)[+|-]?(\d+\.\d+|\d+\.|\.\d+|\d+)' />
												</RegularExpressions>
											</SucceededNumericTypeDescriptor>
										</OtherProperties>
									</Operand1.ConstantNumericValue>
								</Operand1.PrefixUnaryOperator>
								<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637795220124436917' IndexInText='61' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
									<OperatorNameParts>
										<Name Name='+' Id='637795220124436904' IndexInText='61' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</OperatorNameParts>
								</OperatorInfo>
								<Operand2.Literal Id='637795220124437109' IndexInText='62' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
									<NameExpressionItem Name='.2exp3.4' Id='637795220124437104' IndexInText='62' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</Operand2.Literal>
							</Operand1.BinaryOperator>
							<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637795220124437259' IndexInText='70' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
								<OperatorNameParts>
									<Name Name='+' Id='637795220124437251' IndexInText='70' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</OperatorNameParts>
							</OperatorInfo>
							<Operand2.ConstantNumericValue Id='637795220124494404' IndexInText='71' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
								<NameExpressionItem Name='3.E2.7' Id='637795220124494369' IndexInText='71' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<OtherProperties>
									<SucceededNumericTypeDescriptor ObjectId='14' NumberTypeId='1581134225786' Interface='UniversalExpressionParser.NumericTypeDescriptor'/>
								</OtherProperties>
							</Operand2.ConstantNumericValue>
						</Operand1.BinaryOperator>
						<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637795220124494607' IndexInText='77' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<Name Name='+' Id='637795220124494596' IndexInText='77' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OperatorNameParts>
						</OperatorInfo>
						<Operand2.ConstantNumericValue Id='637795220124551017' IndexInText='78' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
							<NameExpressionItem Name='2.1EXP.3' Id='637795220124550948' IndexInText='78' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<OtherProperties>
								<SucceededNumericTypeDescriptor ObjectId='14' NumberTypeId='1581134225786' Interface='UniversalExpressionParser.NumericTypeDescriptor'/>
							</OtherProperties>
						</Operand2.ConstantNumericValue>
					</BinaryOperator>
					<ClosingRoundBrace Name=')' Id='637795220124551345' IndexInText='86' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Children>
					<BinaryOperator Name='+' Priority='30' Id='637795220124551264' IndexInText='54' ItemLength='32' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
				</Children>
				<OtherProperties>
					<NamedExpressionItem Id='637795220124376797' IndexInText='46' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					<OpeningBraceInfo Name='(' Id='637795220124376850' IndexInText='53' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ClosingBraceInfo Name=')' Id='637795220124551345' IndexInText='86' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Parameters ObjectId='29' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name='+' Priority='30' Id='637795220124551264' IndexInText='54' ItemLength='32' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
					</Parameters>
				</OtherProperties>
			</Braces>
			<ExpressionSeparator Name=';' Id='637795220124551396' IndexInText='87' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<Braces Id='637795220124551578' IndexInText='90' ItemLength='16' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
				<RegularItems>
					<Literal Id='637795220124551545' IndexInText='90' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='println' Id='637795220124551540' IndexInText='90' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<OpeningRoundBrace Name='(' Id='637795220124551582' IndexInText='97' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<BinaryOperator Name='*' Priority='20' Id='637795220124551945' IndexInText='98' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.Literal Id='637795220124551688' IndexInText='98' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='.5e15' Id='637795220124551683' IndexInText='98' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand1.Literal>
						<OperatorInfo OperatorType='BinaryOperator' Name='*' Priority='20' Id='637795220124551759' IndexInText='103' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<Name Name='*' Id='637795220124551745' IndexInText='103' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OperatorNameParts>
						</OperatorInfo>
						<Operand2.Literal Id='637795220124551909' IndexInText='104' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='x' Id='637795220124551904' IndexInText='104' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand2.Literal>
					</BinaryOperator>
					<ClosingRoundBrace Name=')' Id='637795220124551966' IndexInText='105' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Children>
					<BinaryOperator Name='*' Priority='20' Id='637795220124551945' IndexInText='98' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
				</Children>
				<OtherProperties>
					<NamedExpressionItem Id='637795220124551545' IndexInText='90' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					<OpeningBraceInfo Name='(' Id='637795220124551582' IndexInText='97' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ClosingBraceInfo Name=')' Id='637795220124551966' IndexInText='105' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Parameters ObjectId='43' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name='*' Priority='20' Id='637795220124551945' IndexInText='98' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
					</Parameters>
				</OtherProperties>
			</Braces>
			<ExpressionSeparator Name=';' Id='637795220124551995' IndexInText='106' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<BinaryOperator Name='=' Priority='2000' Id='637795220124722732' IndexInText='409' ItemLength='50' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
				<Operand1.Literal Id='637795220124552160' IndexInText='409' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
					<NameExpressionItem Name='x' Id='637795220124552155' IndexInText='413' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<AppliedKeywords>
						<Keyword Name='var' Id='637795220124552062' IndexInText='409' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
							<LanguageKeywordInfo ObjectId='49' Id='637781064051574641' Keyword='var' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
						</Keyword>
					</AppliedKeywords>
				</Operand1.Literal>
				<OperatorInfo OperatorType='BinaryOperator' Name='=' Priority='2000' Id='637795220124552243' IndexInText='415' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
					<OperatorNameParts>
						<Name Name='=' Id='637795220124552237' IndexInText='415' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</OperatorNameParts>
				</OperatorInfo>
				<Operand2.BinaryOperator Name='+' Priority='30' Id='637795220124722835' IndexInText='417' ItemLength='42' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
					<Operand1.BinaryOperator Name='*' Priority='20' Id='637795220124722825' IndexInText='417' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.ConstantNumericValue Id='637795220124636565' IndexInText='417' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
							<NameExpressionItem Name='2.3' Id='637795220124636502' IndexInText='417' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<OtherProperties>
								<SucceededNumericTypeDescriptor ObjectId='56' NumberTypeId='1581134136626' Interface='UniversalExpressionParser.NumericTypeDescriptor'>
									<RegularExpressions ObjectId='57' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[System.String]'>
										<System.String value='^(\d+\.\d+|\d+\.|\.\d+)' />
									</RegularExpressions>
								</SucceededNumericTypeDescriptor>
							</OtherProperties>
						</Operand1.ConstantNumericValue>
						<OperatorInfo OperatorType='BinaryOperator' Name='*' Priority='20' Id='637795220124636858' IndexInText='420' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<Name Name='*' Id='637795220124636844' IndexInText='420' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OperatorNameParts>
						</OperatorInfo>
						<Operand2.Literal Id='637795220124637049' IndexInText='421' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='x' Id='637795220124637044' IndexInText='421' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand2.Literal>
					</Operand1.BinaryOperator>
					<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637795220124637131' IndexInText='422' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
						<OperatorNameParts>
							<Name Name='+' Id='637795220124637125' IndexInText='422' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OperatorNameParts>
					</OperatorInfo>
					<Operand2.ConstantNumericValue Id='637795220124722506' IndexInText='423' ItemLength='36' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
						<NameExpressionItem Name='123456789123456789123456789123456789' Id='637795220124722444' IndexInText='423' ItemLength='36' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
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
			<ExpressionSeparator Name=';' Id='637795220124722936' IndexInText='459' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
		</RegularItems>
		<Children>
			<Braces Id='637795220124376846' IndexInText='46' ItemLength='41' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
			<Braces Id='637795220124551578' IndexInText='90' ItemLength='16' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
			<BinaryOperator Name='=' Priority='2000' Id='637795220124722732' IndexInText='409' ItemLength='50' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
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
	<MainExpressionItem Id='637795220124376469' IndexInText='46' ItemLength='414' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'/>
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
	<ExpressionItemSeries Id='637795220126584189' IndexInText='287' ItemLength='172' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'>
		<RegularItems>
			<BinaryOperator Name='=' Priority='2000' Id='637795220126585500' IndexInText='287' ItemLength='78' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
				<Operand1.Literal Id='637795220126584559' IndexInText='287' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
					<NameExpressionItem Name='x' Id='637795220126584550' IndexInText='287' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</Operand1.Literal>
				<OperatorInfo OperatorType='BinaryOperator' Name='=' Priority='2000' Id='637795220126584662' IndexInText='289' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
					<OperatorNameParts>
						<Name Name='=' Id='637795220126584645' IndexInText='289' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</OperatorNameParts>
				</OperatorInfo>
				<Operand2.BinaryOperator Name='+' Priority='30' Id='637795220126585536' IndexInText='291' ItemLength='74' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
					<Operand1.BinaryOperator Name='+' Priority='30' Id='637795220126585530' IndexInText='291' ItemLength='70' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.BinaryOperator Name='+' Priority='30' Id='637795220126585523' IndexInText='291' ItemLength='44' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
							<Operand1.ConstantText Id='637795220126584829' IndexInText='291' ItemLength='17' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='"Text 1 "" ''' ``"' Id='637795220126584823' IndexInText='291' ItemLength='17' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Operand1.ConstantText>
							<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637795220126584907' IndexInText='309' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
								<OperatorNameParts>
									<Name Name='+' Id='637795220126584900' IndexInText='309' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</OperatorNameParts>
							</OperatorInfo>
							<Operand2.ConstantText Id='637795220126585051' IndexInText='317' ItemLength='18' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name=''' Text2  "" ''' ``''' Id='637795220126585046' IndexInText='317' ItemLength='18' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Operand2.ConstantText>
						</Operand1.BinaryOperator>
						<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637795220126585117' IndexInText='336' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<Name Name='+' Id='637795220126585111' IndexInText='336' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OperatorNameParts>
						</OperatorInfo>
						<Operand2.ConstantText Id='637795220126585258' IndexInText='343' ItemLength='18' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='` Text3  "" ''' ```' Id='637795220126585253' IndexInText='343' ItemLength='18' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand2.ConstantText>
					</Operand1.BinaryOperator>
					<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637795220126585317' IndexInText='362' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
						<OperatorNameParts>
							<Name Name='+' Id='637795220126585311' IndexInText='362' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OperatorNameParts>
					</OperatorInfo>
					<Operand2.Literal Id='637795220126585452' IndexInText='364' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='x' Id='637795220126585447' IndexInText='364' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Operand2.Literal>
				</Operand2.BinaryOperator>
			</BinaryOperator>
			<ExpressionSeparator Name=';' Id='637795220126585596' IndexInText='365' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<Braces Id='637795220126585727' IndexInText='370' ItemLength='88' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
				<RegularItems>
					<Literal Id='637795220126585696' IndexInText='370' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='println' Id='637795220126585691' IndexInText='370' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<OpeningRoundBrace Name='(' Id='637795220126585731' IndexInText='377' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<BinaryOperator Name='+' Priority='30' Id='637795220126586290' IndexInText='378' ItemLength='79' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.BinaryOperator Name='+' Priority='30' Id='637795220126586280' IndexInText='378' ItemLength='57' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
							<Operand1.ConstantText Id='637795220126585840' IndexInText='378' ItemLength='53' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='"This is a text that spans$line_break$ multiple $line_break$ lines.   $line_break$"' Id='637795220126585835' IndexInText='378' ItemLength='53' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Operand1.ConstantText>
							<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637795220126585896' IndexInText='432' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
								<OperatorNameParts>
									<Name Name='+' Id='637795220126585891' IndexInText='432' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</OperatorNameParts>
							</OperatorInfo>
							<Operand2.Literal Id='637795220126586038' IndexInText='434' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='x' Id='637795220126586029' IndexInText='434' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Operand2.Literal>
						</Operand1.BinaryOperator>
						<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637795220126586097' IndexInText='436' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<Name Name='+' Id='637795220126586092' IndexInText='436' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OperatorNameParts>
						</OperatorInfo>
						<Operand2.ConstantText Id='637795220126586235' IndexInText='438' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name=''' Some other text.''' Id='637795220126586230' IndexInText='438' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand2.ConstantText>
					</BinaryOperator>
					<ClosingRoundBrace Name=')' Id='637795220126586320' IndexInText='457' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Children>
					<BinaryOperator Name='+' Priority='30' Id='637795220126586290' IndexInText='378' ItemLength='79' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
				</Children>
				<OtherProperties>
					<NamedExpressionItem Id='637795220126585696' IndexInText='370' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					<OpeningBraceInfo Name='(' Id='637795220126585731' IndexInText='377' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ClosingBraceInfo Name=')' Id='637795220126586320' IndexInText='457' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Parameters ObjectId='42' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name='+' Priority='30' Id='637795220126586290' IndexInText='378' ItemLength='79' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
					</Parameters>
				</OtherProperties>
			</Braces>
			<ExpressionSeparator Name=';' Id='637795220126586363' IndexInText='458' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
		</RegularItems>
		<Children>
			<BinaryOperator Name='=' Priority='2000' Id='637795220126585500' IndexInText='287' ItemLength='78' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
			<Braces Id='637795220126585727' IndexInText='370' ItemLength='88' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
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
	<MainExpressionItem Id='637795220126584189' IndexInText='287' ItemLength='172' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'/>
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
	<ExpressionItemSeries Id='637795220122652956' IndexInText='0' ItemLength='125' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'>
		<RegularItems>
			<CodeBlock Id='637795220122653153' IndexInText='0' ItemLength='39' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
				<RegularItems>
					<CodeBlockStartMarker Name='{' Id='637795220122653142' IndexInText='0' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<BinaryOperator Name='=' Priority='2000' Id='637795220122745323' IndexInText='7' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.Literal Id='637795220122653341' IndexInText='7' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='x' Id='637795220122653335' IndexInText='11' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<AppliedKeywords>
								<Keyword Name='var' Id='637795220122653187' IndexInText='7' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
									<LanguageKeywordInfo ObjectId='8' Id='637781064051574641' Keyword='var' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
								</Keyword>
							</AppliedKeywords>
						</Operand1.Literal>
						<OperatorInfo OperatorType='BinaryOperator' Name='=' Priority='2000' Id='637795220122653622' IndexInText='13' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<Name Name='=' Id='637795220122653603' IndexInText='13' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OperatorNameParts>
						</OperatorInfo>
						<Operand2.BinaryOperator Name='^' Priority='10' Id='637795220122745354' IndexInText='15' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
							<Operand1.Literal Id='637795220122653800' IndexInText='15' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='y' Id='637795220122653795' IndexInText='15' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Operand1.Literal>
							<OperatorInfo OperatorType='BinaryOperator' Name='^' Priority='10' Id='637795220122653875' IndexInText='16' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
								<OperatorNameParts>
									<Name Name='^' Id='637795220122653870' IndexInText='16' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</OperatorNameParts>
							</OperatorInfo>
							<Operand2.ConstantNumericValue Id='637795220122745061' IndexInText='17' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
								<NameExpressionItem Name='2' Id='637795220122745000' IndexInText='17' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
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
					<ExpressionSeparator Name=';' Id='637795220122745432' IndexInText='18' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Braces Id='637795220122745656' IndexInText='25' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<Literal Id='637795220122745622' IndexInText='25' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='println' Id='637795220122745616' IndexInText='25' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<OpeningRoundBrace Name='(' Id='637795220122745660' IndexInText='32' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Literal Id='637795220122745780' IndexInText='33' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='x' Id='637795220122745775' IndexInText='33' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<ClosingRoundBrace Name=')' Id='637795220122745818' IndexInText='34' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<Literal Id='637795220122745780' IndexInText='33' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
						</Children>
						<OtherProperties>
							<NamedExpressionItem Id='637795220122745622' IndexInText='25' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							<OpeningBraceInfo Name='(' Id='637795220122745660' IndexInText='32' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=')' Id='637795220122745818' IndexInText='34' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Parameters ObjectId='28' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220122745780' IndexInText='33' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Braces>
					<ExpressionSeparator Name=';' Id='637795220122745852' IndexInText='35' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<CodeBlockEndMarker Name='}' Id='637795220122745885' IndexInText='38' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Children>
					<BinaryOperator Name='=' Priority='2000' Id='637795220122745323' IndexInText='7' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
					<Braces Id='637795220122745656' IndexInText='25' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
				</Children>
				<OtherProperties>
					<CodeBlockStartMarker Name='{' Id='637795220122653142' IndexInText='0' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<CodeBlockEndMarker Name='}' Id='637795220122745885' IndexInText='38' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</OtherProperties>
			</CodeBlock>
			<CodeBlock Id='637795220122745947' IndexInText='43' ItemLength='82' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
				<RegularItems>
					<CodeBlockStartMarker Name='{' Id='637795220122745942' IndexInText='43' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Braces Id='637795220122746085' IndexInText='50' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<Literal Id='637795220122746036' IndexInText='50' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='fl' Id='637795220122746031' IndexInText='50' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<OpeningRoundBrace Name='(' Id='637795220122746088' IndexInText='52' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Literal Id='637795220122746172' IndexInText='53' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='x1' Id='637795220122746168' IndexInText='53' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<Comma Name=',' Id='637795220122746227' IndexInText='55' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Literal Id='637795220122746314' IndexInText='57' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='x2' Id='637795220122746309' IndexInText='57' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<ClosingRoundBrace Name=')' Id='637795220122746365' IndexInText='59' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<Literal Id='637795220122746172' IndexInText='53' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							<Literal Id='637795220122746314' IndexInText='57' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
						</Children>
						<OtherProperties>
							<NamedExpressionItem Id='637795220122746036' IndexInText='50' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							<OpeningBraceInfo Name='(' Id='637795220122746088' IndexInText='52' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=')' Id='637795220122746365' IndexInText='59' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Parameters ObjectId='43' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220122746172' IndexInText='53' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220122746314' IndexInText='57' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Braces>
					<ExpressionSeparator Name=';' Id='637795220122746393' IndexInText='60' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Braces Id='637795220122746529' IndexInText='67' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<Literal Id='637795220122746480' IndexInText='67' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='println' Id='637795220122746476' IndexInText='67' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<OpeningRoundBrace Name='(' Id='637795220122746533' IndexInText='74' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Literal Id='637795220122746616' IndexInText='75' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='x' Id='637795220122746612' IndexInText='75' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<ClosingRoundBrace Name=')' Id='637795220122746679' IndexInText='76' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<Literal Id='637795220122746616' IndexInText='75' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
						</Children>
						<OtherProperties>
							<NamedExpressionItem Id='637795220122746480' IndexInText='67' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							<OpeningBraceInfo Name='(' Id='637795220122746533' IndexInText='74' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=')' Id='637795220122746679' IndexInText='76' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Parameters ObjectId='52' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220122746616' IndexInText='75' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Braces>
					<CodeBlockEndMarker Name='}' Id='637795220122746729' IndexInText='124' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Children>
					<Braces Id='637795220122746085' IndexInText='50' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
					<Braces Id='637795220122746529' IndexInText='67' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
				</Children>
				<OtherProperties>
					<CodeBlockStartMarker Name='{' Id='637795220122745942' IndexInText='43' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<CodeBlockEndMarker Name='}' Id='637795220122746729' IndexInText='124' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</OtherProperties>
			</CodeBlock>
		</RegularItems>
		<Children>
			<CodeBlock Id='637795220122653153' IndexInText='0' ItemLength='39' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'/>
			<CodeBlock Id='637795220122745947' IndexInText='43' ItemLength='82' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'/>
		</Children>
	</ExpressionItemSeries>
	<ParseErrorData ObjectId='54' HasCriticalErrors='False' Interface='UniversalExpressionParser.IParseErrorData'>
		<AllCodeItemErrors ObjectId='55' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICodeItemParseErrorData]'/>
	</ParseErrorData>
	<SortedCommentedOutCodeInfos ObjectId='56' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICommentedOutCodeInfo]'>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='57' IsLineComment='True' IndexInText='78' ItemLength='44'/>
	</SortedCommentedOutCodeInfos>
	<MainExpressionItem Id='637795220122652956' IndexInText='0' ItemLength='125' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'/>
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
	<ExpressionItemSeries Id='637795220122149381' IndexInText='0' ItemLength='807' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'>
		<RegularItems>
			<BinaryOperator Name='=' Priority='2000' Id='637795220122263757' IndexInText='0' ItemLength='21' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
				<Operand1.Literal Id='637795220122149858' IndexInText='0' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
					<NameExpressionItem Name='y' Id='637795220122149851' IndexInText='4' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<AppliedKeywords>
						<Keyword Name='var' Id='637795220122149647' IndexInText='0' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
							<LanguageKeywordInfo ObjectId='6' Id='637781064051574641' Keyword='var' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
						</Keyword>
					</AppliedKeywords>
				</Operand1.Literal>
				<OperatorInfo OperatorType='BinaryOperator' Name='=' Priority='2000' Id='637795220122149996' IndexInText='6' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
					<OperatorNameParts>
						<Name Name='=' Id='637795220122149983' IndexInText='6' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</OperatorNameParts>
				</OperatorInfo>
				<Operand2.BinaryOperator Name='+' Priority='30' Id='637795220122263785' IndexInText='8' ItemLength='13' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
					<Operand1.Literal Id='637795220122150175' IndexInText='8' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='x1' Id='637795220122150169' IndexInText='8' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Operand1.Literal>
					<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637795220122150260' IndexInText='11' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
						<OperatorNameParts>
							<Name Name='+' Id='637795220122150253' IndexInText='11' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OperatorNameParts>
					</OperatorInfo>
					<Operand2.BinaryOperator Name='*' Priority='20' Id='637795220122263793' IndexInText='13' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.ConstantNumericValue Id='637795220122262974' IndexInText='13' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
							<NameExpressionItem Name='2.5' Id='637795220122262299' IndexInText='13' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<OtherProperties>
								<SucceededNumericTypeDescriptor ObjectId='17' NumberTypeId='1581134136626' Interface='UniversalExpressionParser.NumericTypeDescriptor'>
									<RegularExpressions ObjectId='18' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[System.String]'>
										<System.String value='^(\d+\.\d+|\d+\.|\.\d+)' />
									</RegularExpressions>
								</SucceededNumericTypeDescriptor>
							</OtherProperties>
						</Operand1.ConstantNumericValue>
						<OperatorInfo OperatorType='BinaryOperator' Name='*' Priority='20' Id='637795220122263511' IndexInText='17' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<Name Name='*' Id='637795220122263495' IndexInText='17' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OperatorNameParts>
						</OperatorInfo>
						<Operand2.Literal Id='637795220122263705' IndexInText='19' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='x2' Id='637795220122263699' IndexInText='19' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand2.Literal>
					</Operand2.BinaryOperator>
				</Operand2.BinaryOperator>
			</BinaryOperator>
			<ExpressionSeparator Name=';' Id='637795220122263887' IndexInText='21' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<Braces Id='637795220122264035' IndexInText='26' ItemLength='86' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
				<RegularItems>
					<Literal Id='637795220122263998' IndexInText='26' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='f1' Id='637795220122263993' IndexInText='26' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<OpeningRoundBrace Name='(' Id='637795220122264039' IndexInText='28' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ClosingRoundBrace Name=')' Id='637795220122264082' IndexInText='29' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Postfixes>
					<CodeBlock Id='637795220122264121' IndexInText='32' ItemLength='80' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
						<RegularItems>
							<CodeBlockStartMarker Name='{' Id='637795220122264117' IndexInText='32' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637795220122264209' IndexInText='111' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<OtherProperties>
							<CodeBlockStartMarker Name='{' Id='637795220122264117' IndexInText='32' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637795220122264209' IndexInText='111' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OtherProperties>
					</CodeBlock>
				</Postfixes>
				<OtherProperties>
					<NamedExpressionItem Id='637795220122263998' IndexInText='26' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					<OpeningBraceInfo Name='(' Id='637795220122264039' IndexInText='28' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ClosingBraceInfo Name=')' Id='637795220122264082' IndexInText='29' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Parameters ObjectId='32' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'/>
				</OtherProperties>
			</Braces>
			<BinaryOperator Name='=' Priority='2000' Id='637795220122336330' IndexInText='116' ItemLength='15' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
				<Operand1.Literal Id='637795220122264341' IndexInText='116' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
					<NameExpressionItem Name='z' Id='637795220122264336' IndexInText='120' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<AppliedKeywords>
						<Keyword Name='var' Id='637795220122264242' IndexInText='116' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
							<LanguageKeywordInfo ObjectId='6' Id='637781064051574641' Keyword='var' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
						</Keyword>
					</AppliedKeywords>
				</Operand1.Literal>
				<OperatorInfo OperatorType='BinaryOperator' Name='=' Priority='2000' Id='637795220122264423' IndexInText='122' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
					<OperatorNameParts>
						<Name Name='=' Id='637795220122264416' IndexInText='122' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</OperatorNameParts>
				</OperatorInfo>
				<Operand2.BinaryOperator Name='^' Priority='10' Id='637795220122336344' IndexInText='124' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
					<Operand1.Literal Id='637795220122264541' IndexInText='124' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='e' Id='637795220122264536' IndexInText='124' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Operand1.Literal>
					<OperatorInfo OperatorType='BinaryOperator' Name='^' Priority='10' Id='637795220122264598' IndexInText='126' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
						<OperatorNameParts>
							<Name Name='^' Id='637795220122264593' IndexInText='126' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OperatorNameParts>
					</OperatorInfo>
					<Operand2.ConstantNumericValue Id='637795220122336228' IndexInText='128' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
						<NameExpressionItem Name='2.3' Id='637795220122336206' IndexInText='128' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<OtherProperties>
							<SucceededNumericTypeDescriptor ObjectId='17' NumberTypeId='1581134136626' Interface='UniversalExpressionParser.NumericTypeDescriptor'/>
						</OtherProperties>
					</Operand2.ConstantNumericValue>
				</Operand2.BinaryOperator>
			</BinaryOperator>
			<ExpressionSeparator Name=';' Id='637795220122336382' IndexInText='131' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<CodeBlock Id='637795220122336431' IndexInText='134' ItemLength='341' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
				<RegularItems>
					<CodeBlockStartMarker Name='{' Id='637795220122336426' IndexInText='134' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<BinaryOperator Name='=' Priority='2000' Id='637795220122419407' IndexInText='141' ItemLength='13' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.Literal Id='637795220122336567' IndexInText='141' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='x' Id='637795220122336563' IndexInText='145' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<AppliedKeywords>
								<Keyword Name='var' Id='637795220122336455' IndexInText='141' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
									<LanguageKeywordInfo ObjectId='6' Id='637781064051574641' Keyword='var' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
								</Keyword>
							</AppliedKeywords>
						</Operand1.Literal>
						<OperatorInfo OperatorType='BinaryOperator' Name='=' Priority='2000' Id='637795220122336651' IndexInText='147' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<Name Name='=' Id='637795220122336643' IndexInText='147' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OperatorNameParts>
						</OperatorInfo>
						<Operand2.BinaryOperator Name='*' Priority='20' Id='637795220122419437' IndexInText='149' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
							<Operand1.ConstantNumericValue Id='637795220122418876' IndexInText='149' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
								<NameExpressionItem Name='5' Id='637795220122418813' IndexInText='149' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<OtherProperties>
									<SucceededNumericTypeDescriptor ObjectId='58' NumberTypeId='1581134183960' Interface='UniversalExpressionParser.NumericTypeDescriptor'>
										<RegularExpressions ObjectId='59' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[System.String]'>
											<System.String value='^\d+' />
										</RegularExpressions>
									</SucceededNumericTypeDescriptor>
								</OtherProperties>
							</Operand1.ConstantNumericValue>
							<OperatorInfo OperatorType='BinaryOperator' Name='*' Priority='20' Id='637795220122419136' IndexInText='151' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
								<OperatorNameParts>
									<Name Name='*' Id='637795220122419124' IndexInText='151' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</OperatorNameParts>
							</OperatorInfo>
							<Operand2.Literal Id='637795220122419325' IndexInText='153' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='y' Id='637795220122419319' IndexInText='153' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Operand2.Literal>
						</Operand2.BinaryOperator>
					</BinaryOperator>
					<ExpressionSeparator Name=';' Id='637795220122419499' IndexInText='154' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Braces Id='637795220122419632' IndexInText='161' ItemLength='17' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<Literal Id='637795220122419596' IndexInText='161' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='println' Id='637795220122419591' IndexInText='161' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<OpeningRoundBrace Name='(' Id='637795220122419636' IndexInText='168' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<BinaryOperator Name='+' Priority='30' Id='637795220122420026' IndexInText='169' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
								<Operand1.ConstantText Id='637795220122419776' IndexInText='169' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
									<NameExpressionItem Name='"x="' Id='637795220122419770' IndexInText='169' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</Operand1.ConstantText>
								<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637795220122419848' IndexInText='174' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
									<OperatorNameParts>
										<Name Name='+' Id='637795220122419842' IndexInText='174' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</OperatorNameParts>
								</OperatorInfo>
								<Operand2.Literal Id='637795220122419988' IndexInText='176' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
									<NameExpressionItem Name='x' Id='637795220122419983' IndexInText='176' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</Operand2.Literal>
							</BinaryOperator>
							<ClosingRoundBrace Name=')' Id='637795220122420048' IndexInText='177' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<BinaryOperator Name='+' Priority='30' Id='637795220122420026' IndexInText='169' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
						</Children>
						<OtherProperties>
							<NamedExpressionItem Id='637795220122419596' IndexInText='161' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							<OpeningBraceInfo Name='(' Id='637795220122419636' IndexInText='168' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=')' Id='637795220122420048' IndexInText='177' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Parameters ObjectId='77' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name='+' Priority='30' Id='637795220122420026' IndexInText='169' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Braces>
					<ExpressionSeparator Name=';' Id='637795220122420088' IndexInText='178' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<CodeBlock Id='637795220122420121' IndexInText='187' ItemLength='64' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
						<RegularItems>
							<CodeBlockStartMarker Name='{' Id='637795220122420116' IndexInText='187' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<BinaryOperator Name='=' Priority='2000' Id='637795220122498167' IndexInText='198' ItemLength='15' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
								<Operand1.Literal Id='637795220122420249' IndexInText='198' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
									<NameExpressionItem Name='y1' Id='637795220122420244' IndexInText='202' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<AppliedKeywords>
										<Keyword Name='var' Id='637795220122420147' IndexInText='198' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
											<LanguageKeywordInfo ObjectId='6' Id='637781064051574641' Keyword='var' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
										</Keyword>
									</AppliedKeywords>
								</Operand1.Literal>
								<OperatorInfo OperatorType='BinaryOperator' Name='=' Priority='2000' Id='637795220122420328' IndexInText='205' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
									<OperatorNameParts>
										<Name Name='=' Id='637795220122420321' IndexInText='205' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</OperatorNameParts>
								</OperatorInfo>
								<Operand2.BinaryOperator Name='*' Priority='20' Id='637795220122498183' IndexInText='207' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
									<Operand1.ConstantNumericValue Id='637795220122497762' IndexInText='207' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
										<NameExpressionItem Name='10' Id='637795220122497724' IndexInText='207' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										<OtherProperties>
											<SucceededNumericTypeDescriptor ObjectId='58' NumberTypeId='1581134183960' Interface='UniversalExpressionParser.NumericTypeDescriptor'/>
										</OtherProperties>
									</Operand1.ConstantNumericValue>
									<OperatorInfo OperatorType='BinaryOperator' Name='*' Priority='20' Id='637795220122497939' IndexInText='210' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
										<OperatorNameParts>
											<Name Name='*' Id='637795220122497930' IndexInText='210' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</OperatorNameParts>
									</OperatorInfo>
									<Operand2.Literal Id='637795220122498113' IndexInText='212' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='x' Id='637795220122498108' IndexInText='212' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Operand2.Literal>
								</Operand2.BinaryOperator>
							</BinaryOperator>
							<ExpressionSeparator Name=';' Id='637795220122498227' IndexInText='213' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Braces Id='637795220122498356' IndexInText='224' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
								<RegularItems>
									<Literal Id='637795220122498322' IndexInText='224' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='println' Id='637795220122498317' IndexInText='224' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<OpeningRoundBrace Name='(' Id='637795220122498360' IndexInText='231' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Braces Id='637795220122498485' IndexInText='232' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
										<RegularItems>
											<Literal Id='637795220122498453' IndexInText='232' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
												<NameExpressionItem Name='getExp' Id='637795220122498449' IndexInText='232' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											</Literal>
											<OpeningRoundBrace Name='(' Id='637795220122498489' IndexInText='238' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<Literal Id='637795220122498577' IndexInText='239' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
												<NameExpressionItem Name='y1' Id='637795220122498573' IndexInText='239' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											</Literal>
											<ClosingRoundBrace Name=')' Id='637795220122498616' IndexInText='241' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</RegularItems>
										<Children>
											<Literal Id='637795220122498577' IndexInText='239' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
										</Children>
										<OtherProperties>
											<NamedExpressionItem Id='637795220122498453' IndexInText='232' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
											<OpeningBraceInfo Name='(' Id='637795220122498489' IndexInText='238' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<ClosingBraceInfo Name=')' Id='637795220122498616' IndexInText='241' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<Parameters ObjectId='106' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
												<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220122498577' IndexInText='239' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
											</Parameters>
										</OtherProperties>
									</Braces>
									<ClosingRoundBrace Name=')' Id='637795220122498648' IndexInText='242' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Children>
									<Braces Id='637795220122498485' IndexInText='232' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
								</Children>
								<OtherProperties>
									<NamedExpressionItem Id='637795220122498322' IndexInText='224' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<OpeningBraceInfo Name='(' Id='637795220122498360' IndexInText='231' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ClosingBraceInfo Name=')' Id='637795220122498648' IndexInText='242' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Parameters ObjectId='108' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220122498485' IndexInText='232' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
									</Parameters>
								</OtherProperties>
							</Braces>
							<ExpressionSeparator Name=';' Id='637795220122498673' IndexInText='243' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637795220122498735' IndexInText='250' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<BinaryOperator Name='=' Priority='2000' Id='637795220122498167' IndexInText='198' ItemLength='15' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
							<Braces Id='637795220122498356' IndexInText='224' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
						</Children>
						<OtherProperties>
							<CodeBlockStartMarker Name='{' Id='637795220122420116' IndexInText='187' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637795220122498735' IndexInText='250' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OtherProperties>
					</CodeBlock>
					<CodeBlock Id='637795220122498973' IndexInText='259' ItemLength='64' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
						<RegularItems>
							<CodeBlockStartMarker Name='{' Id='637795220122498967' IndexInText='259' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<BinaryOperator Name='=' Priority='2000' Id='637795220122583321' IndexInText='270' ItemLength='15' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
								<Operand1.Literal Id='637795220122499174' IndexInText='270' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
									<NameExpressionItem Name='y2' Id='637795220122499164' IndexInText='274' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<AppliedKeywords>
										<Keyword Name='var' Id='637795220122499062' IndexInText='270' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
											<LanguageKeywordInfo ObjectId='6' Id='637781064051574641' Keyword='var' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
										</Keyword>
									</AppliedKeywords>
								</Operand1.Literal>
								<OperatorInfo OperatorType='BinaryOperator' Name='=' Priority='2000' Id='637795220122499404' IndexInText='277' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
									<OperatorNameParts>
										<Name Name='=' Id='637795220122499395' IndexInText='277' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</OperatorNameParts>
								</OperatorInfo>
								<Operand2.BinaryOperator Name='*' Priority='20' Id='637795220122583350' IndexInText='279' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
									<Operand1.ConstantNumericValue Id='637795220122582796' IndexInText='279' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
										<NameExpressionItem Name='20' Id='637795220122582745' IndexInText='279' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										<OtherProperties>
											<SucceededNumericTypeDescriptor ObjectId='58' NumberTypeId='1581134183960' Interface='UniversalExpressionParser.NumericTypeDescriptor'/>
										</OtherProperties>
									</Operand1.ConstantNumericValue>
									<OperatorInfo OperatorType='BinaryOperator' Name='*' Priority='20' Id='637795220122583070' IndexInText='282' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
										<OperatorNameParts>
											<Name Name='*' Id='637795220122583057' IndexInText='282' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</OperatorNameParts>
									</OperatorInfo>
									<Operand2.Literal Id='637795220122583262' IndexInText='284' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='x' Id='637795220122583256' IndexInText='284' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Operand2.Literal>
								</Operand2.BinaryOperator>
							</BinaryOperator>
							<ExpressionSeparator Name=';' Id='637795220122583408' IndexInText='285' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Braces Id='637795220122583578' IndexInText='296' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
								<RegularItems>
									<Literal Id='637795220122583503' IndexInText='296' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='println' Id='637795220122583497' IndexInText='296' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<OpeningRoundBrace Name='(' Id='637795220122583583' IndexInText='303' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Braces Id='637795220122583713' IndexInText='304' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
										<RegularItems>
											<Literal Id='637795220122583681' IndexInText='304' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
												<NameExpressionItem Name='getExp' Id='637795220122583676' IndexInText='304' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											</Literal>
											<OpeningRoundBrace Name='(' Id='637795220122583716' IndexInText='310' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<Literal Id='637795220122583801' IndexInText='311' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
												<NameExpressionItem Name='y2' Id='637795220122583796' IndexInText='311' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											</Literal>
											<ClosingRoundBrace Name=')' Id='637795220122583838' IndexInText='313' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</RegularItems>
										<Children>
											<Literal Id='637795220122583801' IndexInText='311' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
										</Children>
										<OtherProperties>
											<NamedExpressionItem Id='637795220122583681' IndexInText='304' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
											<OpeningBraceInfo Name='(' Id='637795220122583716' IndexInText='310' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<ClosingBraceInfo Name=')' Id='637795220122583838' IndexInText='313' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<Parameters ObjectId='138' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
												<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220122583801' IndexInText='311' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
											</Parameters>
										</OtherProperties>
									</Braces>
									<ClosingRoundBrace Name=')' Id='637795220122583874' IndexInText='314' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Children>
									<Braces Id='637795220122583713' IndexInText='304' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
								</Children>
								<OtherProperties>
									<NamedExpressionItem Id='637795220122583503' IndexInText='296' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<OpeningBraceInfo Name='(' Id='637795220122583583' IndexInText='303' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ClosingBraceInfo Name=')' Id='637795220122583874' IndexInText='314' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Parameters ObjectId='140' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220122583713' IndexInText='304' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
									</Parameters>
								</OtherProperties>
							</Braces>
							<ExpressionSeparator Name=';' Id='637795220122583900' IndexInText='315' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637795220122583930' IndexInText='322' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<BinaryOperator Name='=' Priority='2000' Id='637795220122583321' IndexInText='270' ItemLength='15' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
							<Braces Id='637795220122583578' IndexInText='296' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
						</Children>
						<OtherProperties>
							<CodeBlockStartMarker Name='{' Id='637795220122498967' IndexInText='259' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637795220122583930' IndexInText='322' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OtherProperties>
					</CodeBlock>
					<BinaryOperator Name=':' Priority='0' Id='637795220122585060' IndexInText='331' ItemLength='141' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.Braces Id='637795220122584056' IndexInText='331' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
							<RegularItems>
								<Literal Id='637795220122584030' IndexInText='331' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
									<NameExpressionItem Name='getExp' Id='637795220122584025' IndexInText='331' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</Literal>
								<OpeningRoundBrace Name='(' Id='637795220122584060' IndexInText='337' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<Literal Id='637795220122584147' IndexInText='338' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
									<NameExpressionItem Name='x' Id='637795220122584142' IndexInText='338' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</Literal>
								<ClosingRoundBrace Name=')' Id='637795220122584175' IndexInText='339' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</RegularItems>
							<Children>
								<Literal Id='637795220122584147' IndexInText='338' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							</Children>
							<OtherProperties>
								<NamedExpressionItem Id='637795220122584030' IndexInText='331' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								<OpeningBraceInfo Name='(' Id='637795220122584060' IndexInText='337' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<ClosingBraceInfo Name=')' Id='637795220122584175' IndexInText='339' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<Parameters ObjectId='151' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
									<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220122584147' IndexInText='338' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								</Parameters>
							</OtherProperties>
						</Operand1.Braces>
						<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637795220122584231' IndexInText='341' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<Name Name=':' Id='637795220122584221' IndexInText='341' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OperatorNameParts>
						</OperatorInfo>
						<Operand2.Literal Id='637795220122584379' IndexInText='343' ItemLength='129' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='double' Id='637795220122584374' IndexInText='343' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Postfixes>
								<CodeBlock Id='637795220122584419' IndexInText='355' ItemLength='117' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
									<RegularItems>
										<CodeBlockStartMarker Name='{' Id='637795220122584414' IndexInText='355' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										<PrefixUnaryOperator Name='return' Priority='2147483647' Id='637795220122584981' IndexInText='454' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
											<OperatorInfo OperatorType='PrefixUnaryOperator' Name='return' Priority='2147483647' Id='637795220122584588' IndexInText='454' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
												<OperatorNameParts>
													<Name Name='return' Id='637795220122584580' IndexInText='454' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												</OperatorNameParts>
											</OperatorInfo>
											<Operand1.BinaryOperator Name='^' Priority='10' Id='637795220122584993' IndexInText='461' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
												<Operand1.Literal Id='637795220122584703' IndexInText='461' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
													<NameExpressionItem Name='e' Id='637795220122584698' IndexInText='461' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												</Operand1.Literal>
												<OperatorInfo OperatorType='BinaryOperator' Name='^' Priority='10' Id='637795220122584781' IndexInText='462' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
													<OperatorNameParts>
														<Name Name='^' Id='637795220122584775' IndexInText='462' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
													</OperatorNameParts>
												</OperatorInfo>
												<Operand2.Literal Id='637795220122584945' IndexInText='463' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
													<NameExpressionItem Name='x' Id='637795220122584940' IndexInText='463' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												</Operand2.Literal>
											</Operand1.BinaryOperator>
										</PrefixUnaryOperator>
										<ExpressionSeparator Name=';' Id='637795220122585017' IndexInText='464' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										<CodeBlockEndMarker Name='}' Id='637795220122585049' IndexInText='471' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</RegularItems>
									<Children>
										<PrefixUnaryOperator Name='return' Priority='2147483647' Id='637795220122584981' IndexInText='454' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
									</Children>
									<OtherProperties>
										<CodeBlockStartMarker Name='{' Id='637795220122584414' IndexInText='355' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										<CodeBlockEndMarker Name='}' Id='637795220122585049' IndexInText='471' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</OtherProperties>
								</CodeBlock>
							</Postfixes>
						</Operand2.Literal>
					</BinaryOperator>
					<CodeBlockEndMarker Name='}' Id='637795220122585095' IndexInText='474' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Children>
					<BinaryOperator Name='=' Priority='2000' Id='637795220122419407' IndexInText='141' ItemLength='13' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
					<Braces Id='637795220122419632' IndexInText='161' ItemLength='17' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
					<CodeBlock Id='637795220122420121' IndexInText='187' ItemLength='64' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'/>
					<CodeBlock Id='637795220122498973' IndexInText='259' ItemLength='64' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'/>
					<BinaryOperator Name=':' Priority='0' Id='637795220122585060' IndexInText='331' ItemLength='141' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
				</Children>
				<OtherProperties>
					<CodeBlockStartMarker Name='{' Id='637795220122336426' IndexInText='134' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<CodeBlockEndMarker Name='}' Id='637795220122585095' IndexInText='474' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</OtherProperties>
			</CodeBlock>
			<Braces Id='637795220122585220' IndexInText='479' ItemLength='94' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
				<RegularItems>
					<Literal Id='637795220122585192' IndexInText='479' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='f2' Id='637795220122585187' IndexInText='479' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<OpeningRoundBrace Name='(' Id='637795220122585224' IndexInText='481' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ClosingRoundBrace Name=')' Id='637795220122585253' IndexInText='482' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Postfixes>
					<CodeBlock Id='637795220122585288' IndexInText='485' ItemLength='88' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
						<RegularItems>
							<CodeBlockStartMarker Name='{' Id='637795220122585284' IndexInText='485' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637795220122585324' IndexInText='572' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<OtherProperties>
							<CodeBlockStartMarker Name='{' Id='637795220122585284' IndexInText='485' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637795220122585324' IndexInText='572' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OtherProperties>
					</CodeBlock>
				</Postfixes>
				<OtherProperties>
					<NamedExpressionItem Id='637795220122585192' IndexInText='479' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					<OpeningBraceInfo Name='(' Id='637795220122585224' IndexInText='481' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ClosingBraceInfo Name=')' Id='637795220122585253' IndexInText='482' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Parameters ObjectId='179' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'/>
				</OtherProperties>
			</Braces>
			<CodeBlock Id='637795220122585355' IndexInText='577' ItemLength='31' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
				<RegularItems>
					<CodeBlockStartMarker Name='{' Id='637795220122585351' IndexInText='577' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<CodeBlockEndMarker Name='}' Id='637795220122585384' IndexInText='607' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<OtherProperties>
					<CodeBlockStartMarker Name='{' Id='637795220122585351' IndexInText='577' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<CodeBlockEndMarker Name='}' Id='637795220122585384' IndexInText='607' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</OtherProperties>
			</CodeBlock>
			<Literal Id='637795220122585519' IndexInText='612' ItemLength='195' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
				<NameExpressionItem Name='Dog' Id='637795220122585515' IndexInText='625' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				<AppliedKeywords>
					<Keyword Name='public' Id='637795220122585412' IndexInText='612' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
						<LanguageKeywordInfo ObjectId='186' Id='637783669980406419' Keyword='public' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
					</Keyword>
					<Keyword Name='class' Id='637795220122585433' IndexInText='619' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
						<LanguageKeywordInfo ObjectId='188' Id='637783670388745402' Keyword='class' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
					</Keyword>
				</AppliedKeywords>
				<Postfixes>
					<CodeBlock Id='637795220122585558' IndexInText='630' ItemLength='177' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
						<RegularItems>
							<CodeBlockStartMarker Name='{' Id='637795220122585554' IndexInText='630' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Braces Id='637795220122585745' IndexInText='637' ItemLength='167' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
								<AppliedKeywords>
									<Keyword Name='public' Id='637795220122585577' IndexInText='637' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
										<LanguageKeywordInfo ObjectId='186' Id='637783669980406419' Keyword='public' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
									</Keyword>
								</AppliedKeywords>
								<RegularItems>
									<Literal Id='637795220122585660' IndexInText='644' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='Bark' Id='637795220122585656' IndexInText='644' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<OpeningRoundBrace Name='(' Id='637795220122585750' IndexInText='648' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ClosingRoundBrace Name=')' Id='637795220122585779' IndexInText='649' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Postfixes>
									<CodeBlock Id='637795220122585811' IndexInText='656' ItemLength='148' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
										<RegularItems>
											<CodeBlockStartMarker Name='{' Id='637795220122585806' IndexInText='656' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<Braces Id='637795220122585941' IndexInText='782' ItemLength='15' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
												<RegularItems>
													<Literal Id='637795220122585913' IndexInText='782' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
														<NameExpressionItem Name='println' Id='637795220122585908' IndexInText='782' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
													</Literal>
													<OpeningRoundBrace Name='(' Id='637795220122585944' IndexInText='789' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
													<ConstantText Id='637795220122586065' IndexInText='790' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
														<NameExpressionItem Name='"bark"' Id='637795220122586060' IndexInText='790' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
													</ConstantText>
													<ClosingRoundBrace Name=')' Id='637795220122586096' IndexInText='796' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												</RegularItems>
												<Children>
													<ConstantText Id='637795220122586065' IndexInText='790' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
												</Children>
												<OtherProperties>
													<NamedExpressionItem Id='637795220122585913' IndexInText='782' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
													<OpeningBraceInfo Name='(' Id='637795220122585944' IndexInText='789' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
													<ClosingBraceInfo Name=')' Id='637795220122586096' IndexInText='796' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
													<Parameters ObjectId='206' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
														<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220122586065' IndexInText='790' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
													</Parameters>
												</OtherProperties>
											</Braces>
											<CodeBlockEndMarker Name='}' Id='637795220122586132' IndexInText='803' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</RegularItems>
										<Children>
											<Braces Id='637795220122585941' IndexInText='782' ItemLength='15' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
										</Children>
										<OtherProperties>
											<CodeBlockStartMarker Name='{' Id='637795220122585806' IndexInText='656' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<CodeBlockEndMarker Name='}' Id='637795220122586132' IndexInText='803' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</OtherProperties>
									</CodeBlock>
								</Postfixes>
								<OtherProperties>
									<NamedExpressionItem Id='637795220122585660' IndexInText='644' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<OpeningBraceInfo Name='(' Id='637795220122585750' IndexInText='648' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ClosingBraceInfo Name=')' Id='637795220122585779' IndexInText='649' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Parameters ObjectId='208' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'/>
								</OtherProperties>
							</Braces>
							<CodeBlockEndMarker Name='}' Id='637795220122586158' IndexInText='806' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<Braces Id='637795220122585745' IndexInText='637' ItemLength='167' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
						</Children>
						<OtherProperties>
							<CodeBlockStartMarker Name='{' Id='637795220122585554' IndexInText='630' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637795220122586158' IndexInText='806' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OtherProperties>
					</CodeBlock>
				</Postfixes>
			</Literal>
		</RegularItems>
		<Children>
			<BinaryOperator Name='=' Priority='2000' Id='637795220122263757' IndexInText='0' ItemLength='21' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
			<Braces Id='637795220122264035' IndexInText='26' ItemLength='86' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
			<BinaryOperator Name='=' Priority='2000' Id='637795220122336330' IndexInText='116' ItemLength='15' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
			<CodeBlock Id='637795220122336431' IndexInText='134' ItemLength='341' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'/>
			<Braces Id='637795220122585220' IndexInText='479' ItemLength='94' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
			<CodeBlock Id='637795220122585355' IndexInText='577' ItemLength='31' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'/>
			<Literal Id='637795220122585519' IndexInText='612' ItemLength='195' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
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
	<MainExpressionItem Id='637795220122149381' IndexInText='0' ItemLength='807' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'/>
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
	<ExpressionItemSeries Id='637795220123962124' IndexInText='207' ItemLength='1843' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'>
		<RegularItems>
			<Literal Id='637795220123962922' IndexInText='207' ItemLength='16' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
				<NameExpressionItem Name='Dog' Id='637795220123962916' IndexInText='220' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				<AppliedKeywords>
					<Keyword Name='public' Id='637795220123962725' IndexInText='207' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
						<LanguageKeywordInfo ObjectId='5' Id='637783669980406419' Keyword='public' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
					</Keyword>
					<Keyword Name='class' Id='637795220123962764' IndexInText='214' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
						<LanguageKeywordInfo ObjectId='7' Id='637783670388745402' Keyword='class' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
					</Keyword>
				</AppliedKeywords>
			</Literal>
			<ExpressionSeparator Name=';' Id='637795220123962990' IndexInText='223' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<Braces Id='637795220123963165' IndexInText='436' ItemLength='18' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
				<AppliedKeywords>
					<Keyword Name='public' Id='637795220123963024' IndexInText='436' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
						<LanguageKeywordInfo ObjectId='5' Id='637783669980406419' Keyword='public' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
					</Keyword>
					<Keyword Name='static' Id='637795220123963032' IndexInText='443' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
						<LanguageKeywordInfo ObjectId='12' Id='637789181872233115' Keyword='static' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
					</Keyword>
				</AppliedKeywords>
				<RegularItems>
					<Literal Id='637795220123963120' IndexInText='450' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='F1' Id='637795220123963115' IndexInText='450' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<OpeningRoundBrace Name='(' Id='637795220123963172' IndexInText='452' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ClosingRoundBrace Name=')' Id='637795220123963224' IndexInText='453' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<OtherProperties>
					<NamedExpressionItem Id='637795220123963120' IndexInText='450' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					<OpeningBraceInfo Name='(' Id='637795220123963172' IndexInText='452' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ClosingBraceInfo Name=')' Id='637795220123963224' IndexInText='453' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Parameters ObjectId='17' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'/>
				</OtherProperties>
			</Braces>
			<ExpressionSeparator Name=';' Id='637795220123963258' IndexInText='454' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<Braces Id='637795220123963411' IndexInText='668' ItemLength='31' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
				<AppliedKeywords>
					<Keyword Name='public' Id='637795220123963288' IndexInText='668' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
						<LanguageKeywordInfo ObjectId='5' Id='637783669980406419' Keyword='public' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
					</Keyword>
					<Keyword Name='static' Id='637795220123963296' IndexInText='675' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
						<LanguageKeywordInfo ObjectId='12' Id='637789181872233115' Keyword='static' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
					</Keyword>
				</AppliedKeywords>
				<RegularItems>
					<Literal Id='637795220123963381' IndexInText='682' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='F1' Id='637795220123963376' IndexInText='682' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<OpeningRoundBrace Name='(' Id='637795220123963415' IndexInText='684' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ClosingRoundBrace Name=')' Id='637795220123963445' IndexInText='685' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Postfixes>
					<CodeBlock Id='637795220123963477' IndexInText='687' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
						<RegularItems>
							<CodeBlockStartMarker Name='{' Id='637795220123963472' IndexInText='687' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<PrefixUnaryOperator Name='return' Priority='2147483647' Id='637795220124054282' IndexInText='688' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
								<OperatorInfo OperatorType='PrefixUnaryOperator' Name='return' Priority='2147483647' Id='637795220123963541' IndexInText='688' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
									<OperatorNameParts>
										<Name Name='return' Id='637795220123963530' IndexInText='688' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</OperatorNameParts>
								</OperatorInfo>
								<Operand1.ConstantNumericValue Id='637795220124054069' IndexInText='695' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
									<NameExpressionItem Name='1' Id='637795220124054014' IndexInText='695' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<OtherProperties>
										<SucceededNumericTypeDescriptor ObjectId='33' NumberTypeId='1581134183960' Interface='UniversalExpressionParser.NumericTypeDescriptor'>
											<RegularExpressions ObjectId='34' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[System.String]'>
												<System.String value='^\d+' />
											</RegularExpressions>
										</SucceededNumericTypeDescriptor>
									</OtherProperties>
								</Operand1.ConstantNumericValue>
							</PrefixUnaryOperator>
							<ExpressionSeparator Name=';' Id='637795220124054368' IndexInText='696' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637795220124054411' IndexInText='698' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<PrefixUnaryOperator Name='return' Priority='2147483647' Id='637795220124054282' IndexInText='688' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
						</Children>
						<OtherProperties>
							<CodeBlockStartMarker Name='{' Id='637795220123963472' IndexInText='687' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637795220124054411' IndexInText='698' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OtherProperties>
					</CodeBlock>
				</Postfixes>
				<OtherProperties>
					<NamedExpressionItem Id='637795220123963381' IndexInText='682' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					<OpeningBraceInfo Name='(' Id='637795220123963415' IndexInText='684' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ClosingBraceInfo Name=')' Id='637795220123963445' IndexInText='685' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Parameters ObjectId='37' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'/>
				</OtherProperties>
			</Braces>
			<Braces Id='637795220124054516' IndexInText='908' ItemLength='21' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
				<AppliedKeywords>
					<Keyword Name='::codeMarker' Id='637795220124054481' IndexInText='908' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
						<LanguageKeywordInfo ObjectId='40' Id='637783880924005707' Keyword='::codeMarker' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
					</Keyword>
				</AppliedKeywords>
				<RegularItems>
					<OpeningRoundBrace Name='(' Id='637795220124054524' IndexInText='921' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Literal Id='637795220124054717' IndexInText='922' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='x1' Id='637795220124054711' IndexInText='922' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<Comma Name=',' Id='637795220124054768' IndexInText='924' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Literal Id='637795220124054859' IndexInText='926' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='x2' Id='637795220124054854' IndexInText='926' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<ClosingRoundBrace Name=')' Id='637795220124054890' IndexInText='928' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Children>
					<Literal Id='637795220124054717' IndexInText='922' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					<Literal Id='637795220124054859' IndexInText='926' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
				</Children>
				<OtherProperties>
					<OpeningBraceInfo Name='(' Id='637795220124054524' IndexInText='921' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ClosingBraceInfo Name=')' Id='637795220124054890' IndexInText='928' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
					<Parameters ObjectId='48' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220124054717' IndexInText='922' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220124054859' IndexInText='926' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					</Parameters>
				</OtherProperties>
			</Braces>
			<ExpressionSeparator Name=';' Id='637795220124054923' IndexInText='929' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<Braces Id='637795220124055088' IndexInText='1140' ItemLength='22' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
				<AppliedKeywords>
					<Keyword Name='::codeMarker' Id='637795220124054965' IndexInText='1140' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
						<LanguageKeywordInfo ObjectId='40' Id='637783880924005707' Keyword='::codeMarker' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
					</Keyword>
				</AppliedKeywords>
				<RegularItems>
					<Literal Id='637795220124055047' IndexInText='1153' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='m1' Id='637795220124055043' IndexInText='1153' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<OpeningSquareBrace Name='[' Id='637795220124055094' IndexInText='1155' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ConstantNumericValue Id='637795220124139967' IndexInText='1156' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
						<NameExpressionItem Name='2' Id='637795220124139917' IndexInText='1156' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<OtherProperties>
							<SucceededNumericTypeDescriptor ObjectId='33' NumberTypeId='1581134183960' Interface='UniversalExpressionParser.NumericTypeDescriptor'/>
						</OtherProperties>
					</ConstantNumericValue>
					<Comma Name=',' Id='637795220124140135' IndexInText='1157' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Literal Id='637795220124140301' IndexInText='1159' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='x1' Id='637795220124140293' IndexInText='1159' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<ClosingSquareBrace Name=']' Id='637795220124140338' IndexInText='1161' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Children>
					<ConstantNumericValue Id='637795220124139967' IndexInText='1156' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'/>
					<Literal Id='637795220124140301' IndexInText='1159' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
				</Children>
				<OtherProperties>
					<NamedExpressionItem Id='637795220124055047' IndexInText='1153' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					<OpeningBraceInfo Name='[' Id='637795220124055094' IndexInText='1155' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ClosingBraceInfo Name=']' Id='637795220124140338' IndexInText='1161' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Parameters ObjectId='61' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220124139967' IndexInText='1156' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'/>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220124140301' IndexInText='1159' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					</Parameters>
				</OtherProperties>
			</Braces>
			<ExpressionSeparator Name=';' Id='637795220124140388' IndexInText='1162' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<Braces Id='637795220124140486' IndexInText='1372' ItemLength='20' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
				<AppliedKeywords>
					<Keyword Name='::codeMarker' Id='637795220124140448' IndexInText='1372' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
						<LanguageKeywordInfo ObjectId='40' Id='637783880924005707' Keyword='::codeMarker' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
					</Keyword>
				</AppliedKeywords>
				<RegularItems>
					<OpeningSquareBrace Name='[' Id='637795220124140494' IndexInText='1384' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Literal Id='637795220124140591' IndexInText='1385' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='x1' Id='637795220124140586' IndexInText='1385' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<Comma Name=',' Id='637795220124140622' IndexInText='1387' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Literal Id='637795220124140711' IndexInText='1389' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='x2' Id='637795220124140706' IndexInText='1389' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<ClosingSquareBrace Name=']' Id='637795220124140738' IndexInText='1391' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Children>
					<Literal Id='637795220124140591' IndexInText='1385' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					<Literal Id='637795220124140711' IndexInText='1389' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
				</Children>
				<OtherProperties>
					<OpeningBraceInfo Name='[' Id='637795220124140494' IndexInText='1384' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ClosingBraceInfo Name=']' Id='637795220124140738' IndexInText='1391' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
					<Parameters ObjectId='72' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220124140591' IndexInText='1385' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220124140711' IndexInText='1389' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					</Parameters>
				</OtherProperties>
			</Braces>
			<ExpressionSeparator Name=';' Id='637795220124140767' IndexInText='1392' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<CodeBlock Id='637795220124140834' IndexInText='1601' ItemLength='25' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
				<AppliedKeywords>
					<Keyword Name='static' Id='637795220124140798' IndexInText='1601' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
						<LanguageKeywordInfo ObjectId='12' Id='637789181872233115' Keyword='static' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
					</Keyword>
				</AppliedKeywords>
				<RegularItems>
					<CodeBlockStartMarker Name='{' Id='637795220124140829' IndexInText='1610' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Literal Id='637795220124140948' IndexInText='1617' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='x' Id='637795220124140940' IndexInText='1621' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<AppliedKeywords>
							<Keyword Name='var' Id='637795220124140860' IndexInText='1617' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
								<LanguageKeywordInfo ObjectId='80' Id='637781064051574641' Keyword='var' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
							</Keyword>
						</AppliedKeywords>
					</Literal>
					<ExpressionSeparator Name=';' Id='637795220124140978' IndexInText='1622' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<CodeBlockEndMarker Name='}' Id='637795220124141005' IndexInText='1625' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Children>
					<Literal Id='637795220124140948' IndexInText='1617' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
				</Children>
				<OtherProperties>
					<CodeBlockStartMarker Name='{' Id='637795220124140829' IndexInText='1610' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<CodeBlockEndMarker Name='}' Id='637795220124141005' IndexInText='1625' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</OtherProperties>
			</CodeBlock>
			<BinaryOperator Name='=' Priority='2000' Id='637795220124222165' IndexInText='2009' ItemLength='40' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
				<Operand1.Literal Id='637795220124141128' IndexInText='2009' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
					<NameExpressionItem Name='y' Id='637795220124141123' IndexInText='2013' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<AppliedKeywords>
						<Keyword Name='var' Id='637795220124141040' IndexInText='2009' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
							<LanguageKeywordInfo ObjectId='80' Id='637781064051574641' Keyword='var' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
						</Keyword>
					</AppliedKeywords>
				</Operand1.Literal>
				<OperatorInfo OperatorType='BinaryOperator' Name='=' Priority='2000' Id='637795220124141209' IndexInText='2015' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
					<OperatorNameParts>
						<Name Name='=' Id='637795220124141197' IndexInText='2015' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</OperatorNameParts>
				</OperatorInfo>
				<Operand2.BinaryOperator Name='+' Priority='30' Id='637795220124222210' IndexInText='2017' ItemLength='32' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
					<Operand1.BinaryOperator Name='+' Priority='30' Id='637795220124222196' IndexInText='2017' ItemLength='29' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.BinaryOperator Name='+' Priority='30' Id='637795220124222187' IndexInText='2017' ItemLength='15' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
							<Operand1.Literal Id='637795220124141341' IndexInText='2017' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='x1' Id='637795220124141333' IndexInText='2017' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Operand1.Literal>
							<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637795220124141415' IndexInText='2020' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
								<OperatorNameParts>
									<Name Name='+' Id='637795220124141409' IndexInText='2020' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</OperatorNameParts>
							</OperatorInfo>
							<Operand2.Custom Id='637795220124141551' IndexInText='2021' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.Custom.IKeywordBasedCustomExpressionItem'>
								<RegularItems>
									<Keyword Name='::pragma' Id='637795220124141466' IndexInText='2021' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
										<LanguageKeywordInfo ObjectId='98' Id='637781063127646487' Keyword='::pragma' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
									</Keyword>
									<Name Name='x2' Id='637795220124141545' IndexInText='2030' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<OtherProperties>
									<LastKeywordExpressionItem Name='::pragma' Id='637795220124141466' IndexInText='2021' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'/>
									<ErrorsPositionDisplayValue value='2021' type='System.Int32' />
								</OtherProperties>
							</Operand2.Custom>
						</Operand1.BinaryOperator>
						<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637795220124141629' IndexInText='2032' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<Name Name='+' Id='637795220124141624' IndexInText='2032' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OperatorNameParts>
						</OperatorInfo>
						<Operand2.BinaryOperator Name='*' Priority='20' Id='637795220124222203' IndexInText='2033' ItemLength='13' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
							<Operand1.ConstantNumericValue Id='637795220124221417' IndexInText='2033' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
								<NameExpressionItem Name='3' Id='637795220124221361' IndexInText='2033' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<OtherProperties>
									<SucceededNumericTypeDescriptor ObjectId='33' NumberTypeId='1581134183960' Interface='UniversalExpressionParser.NumericTypeDescriptor'/>
								</OtherProperties>
							</Operand1.ConstantNumericValue>
							<OperatorInfo OperatorType='BinaryOperator' Name='*' Priority='20' Id='637795220124221675' IndexInText='2034' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
								<OperatorNameParts>
									<Name Name='*' Id='637795220124221664' IndexInText='2034' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</OperatorNameParts>
							</OperatorInfo>
							<Operand2.Custom Id='637795220124221880' IndexInText='2035' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.Custom.IKeywordBasedCustomExpressionItem'>
								<RegularItems>
									<Keyword Name='::pragma' Id='637795220124221761' IndexInText='2035' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
										<LanguageKeywordInfo ObjectId='98' Id='637781063127646487' Keyword='::pragma' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
									</Keyword>
									<Name Name='x3' Id='637795220124221874' IndexInText='2044' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<OtherProperties>
									<LastKeywordExpressionItem Name='::pragma' Id='637795220124221761' IndexInText='2035' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'/>
									<ErrorsPositionDisplayValue value='2035' type='System.Int32' />
								</OtherProperties>
							</Operand2.Custom>
						</Operand2.BinaryOperator>
					</Operand1.BinaryOperator>
					<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637795220124221976' IndexInText='2047' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
						<OperatorNameParts>
							<Name Name='+' Id='637795220124221964' IndexInText='2047' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OperatorNameParts>
					</OperatorInfo>
					<Operand2.Literal Id='637795220124222116' IndexInText='2048' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='y' Id='637795220124222111' IndexInText='2048' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Operand2.Literal>
				</Operand2.BinaryOperator>
			</BinaryOperator>
			<ExpressionSeparator Name=';' Id='637795220124222283' IndexInText='2049' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
		</RegularItems>
		<Children>
			<Literal Id='637795220123962922' IndexInText='207' ItemLength='16' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
			<Braces Id='637795220123963165' IndexInText='436' ItemLength='18' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
			<Braces Id='637795220123963411' IndexInText='668' ItemLength='31' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
			<Braces Id='637795220124054516' IndexInText='908' ItemLength='21' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
			<Braces Id='637795220124055088' IndexInText='1140' ItemLength='22' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
			<Braces Id='637795220124140486' IndexInText='1372' ItemLength='20' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
			<CodeBlock Id='637795220124140834' IndexInText='1601' ItemLength='25' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'/>
			<BinaryOperator Name='=' Priority='2000' Id='637795220124222165' IndexInText='2009' ItemLength='40' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
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
	<MainExpressionItem Id='637795220123962124' IndexInText='207' ItemLength='1843' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'/>
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
	<ExpressionItemSeries Id='637795220126013258' IndexInText='0' ItemLength='199' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'>
		<RegularItems>
			<Braces Id='637795220126014438' IndexInText='0' ItemLength='199' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
				<Prefixes>
					<Braces Id='637795220126013492' IndexInText='0' ItemLength='22' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<OpeningSquareBrace Name='[' Id='637795220126013499' IndexInText='0' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Literal Id='637795220126013644' IndexInText='1' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='NotNull' Id='637795220126013638' IndexInText='1' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<Comma Name=',' Id='637795220126013701' IndexInText='8' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Literal Id='637795220126013795' IndexInText='10' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='ItemNotNull' Id='637795220126013786' IndexInText='10' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<ClosingSquareBrace Name=']' Id='637795220126013829' IndexInText='21' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<Literal Id='637795220126013644' IndexInText='1' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							<Literal Id='637795220126013795' IndexInText='10' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
						</Children>
						<OtherProperties>
							<OpeningBraceInfo Name='[' Id='637795220126013499' IndexInText='0' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=']' Id='637795220126013829' IndexInText='21' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
							<Parameters ObjectId='11' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220126013644' IndexInText='1' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220126013795' IndexInText='10' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Braces>
					<Braces Id='637795220126013866' IndexInText='22' ItemLength='29' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<OpeningRoundBrace Name='(' Id='637795220126013889' IndexInText='22' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Braces Id='637795220126014015' IndexInText='23' ItemLength='27' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
								<RegularItems>
									<Literal Id='637795220126013986' IndexInText='23' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='Attribute' Id='637795220126013981' IndexInText='23' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<OpeningRoundBrace Name='(' Id='637795220126014019' IndexInText='32' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ConstantText Id='637795220126014136' IndexInText='33' ItemLength='16' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='"MarkedFunction"' Id='637795220126014131' IndexInText='33' ItemLength='16' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</ConstantText>
									<ClosingRoundBrace Name=')' Id='637795220126014169' IndexInText='49' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Children>
									<ConstantText Id='637795220126014136' IndexInText='33' ItemLength='16' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								</Children>
								<OtherProperties>
									<NamedExpressionItem Id='637795220126013986' IndexInText='23' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<OpeningBraceInfo Name='(' Id='637795220126014019' IndexInText='32' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ClosingBraceInfo Name=')' Id='637795220126014169' IndexInText='49' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Parameters ObjectId='21' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220126014136' IndexInText='33' ItemLength='16' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</Parameters>
								</OtherProperties>
							</Braces>
							<ClosingRoundBrace Name=')' Id='637795220126014196' IndexInText='50' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<Braces Id='637795220126014015' IndexInText='23' ItemLength='27' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
						</Children>
						<OtherProperties>
							<OpeningBraceInfo Name='(' Id='637795220126013889' IndexInText='22' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=')' Id='637795220126014196' IndexInText='50' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
							<Parameters ObjectId='23' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220126014015' IndexInText='23' ItemLength='27' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Braces>
				</Prefixes>
				<RegularItems>
					<Literal Id='637795220126014397' IndexInText='52' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='F1' Id='637795220126014390' IndexInText='52' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<OpeningRoundBrace Name='(' Id='637795220126014443' IndexInText='54' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Literal Id='637795220126014540' IndexInText='55' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='x' Id='637795220126014535' IndexInText='55' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<Comma Name=',' Id='637795220126014572' IndexInText='56' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Literal Id='637795220126014659' IndexInText='58' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='x2' Id='637795220126014655' IndexInText='58' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<ClosingRoundBrace Name=')' Id='637795220126014687' IndexInText='60' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Postfixes>
					<CodeBlock Id='637795220126014724' IndexInText='63' ItemLength='136' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
						<RegularItems>
							<CodeBlockStartMarker Name='{' Id='637795220126014719' IndexInText='63' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Braces Id='637795220126014891' IndexInText='175' ItemLength='20' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
								<RegularItems>
									<Literal Id='637795220126014863' IndexInText='175' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='retuens' Id='637795220126014857' IndexInText='175' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<OpeningSquareBrace Name='[' Id='637795220126014894' IndexInText='183' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637795220126014983' IndexInText='184' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='x1' Id='637795220126014979' IndexInText='184' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<Comma Name=',' Id='637795220126015014' IndexInText='186' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637795220126015103' IndexInText='188' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='x2' Id='637795220126015099' IndexInText='188' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<Comma Name=',' Id='637795220126015132' IndexInText='190' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637795220126015220' IndexInText='192' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='x3' Id='637795220126015215' IndexInText='192' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<ClosingSquareBrace Name=']' Id='637795220126015248' IndexInText='194' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Children>
									<Literal Id='637795220126014983' IndexInText='184' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<Literal Id='637795220126015103' IndexInText='188' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<Literal Id='637795220126015220' IndexInText='192' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								</Children>
								<OtherProperties>
									<NamedExpressionItem Id='637795220126014863' IndexInText='175' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<OpeningBraceInfo Name='[' Id='637795220126014894' IndexInText='183' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ClosingBraceInfo Name=']' Id='637795220126015248' IndexInText='194' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Parameters ObjectId='48' Count='3' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220126014983' IndexInText='184' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220126015103' IndexInText='188' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220126015220' IndexInText='192' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</Parameters>
								</OtherProperties>
							</Braces>
							<ExpressionSeparator Name=';' Id='637795220126015279' IndexInText='195' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637795220126015311' IndexInText='198' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<Braces Id='637795220126014891' IndexInText='175' ItemLength='20' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
						</Children>
						<OtherProperties>
							<CodeBlockStartMarker Name='{' Id='637795220126014719' IndexInText='63' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637795220126015311' IndexInText='198' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OtherProperties>
					</CodeBlock>
				</Postfixes>
				<Children>
					<Literal Id='637795220126014540' IndexInText='55' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					<Literal Id='637795220126014659' IndexInText='58' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
				</Children>
				<OtherProperties>
					<NamedExpressionItem Id='637795220126014397' IndexInText='52' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					<OpeningBraceInfo Name='(' Id='637795220126014443' IndexInText='54' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ClosingBraceInfo Name=')' Id='637795220126014687' IndexInText='60' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Parameters ObjectId='51' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220126014540' IndexInText='55' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220126014659' IndexInText='58' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					</Parameters>
				</OtherProperties>
			</Braces>
		</RegularItems>
		<Children>
			<Braces Id='637795220126014438' IndexInText='0' ItemLength='199' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
		</Children>
	</ExpressionItemSeries>
	<ParseErrorData ObjectId='52' HasCriticalErrors='False' Interface='UniversalExpressionParser.IParseErrorData'>
		<AllCodeItemErrors ObjectId='53' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICodeItemParseErrorData]'/>
	</ParseErrorData>
	<SortedCommentedOutCodeInfos ObjectId='54' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICommentedOutCodeInfo]'>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='55' IsLineComment='True' IndexInText='70' ItemLength='99'/>
	</SortedCommentedOutCodeInfos>
	<MainExpressionItem Id='637795220126013258' IndexInText='0' ItemLength='199' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'/>
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
	<ExpressionItemSeries Id='637795220126046219' IndexInText='0' ItemLength='159' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'>
		<RegularItems>
			<Braces Id='637795220126047623' IndexInText='0' ItemLength='159' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
				<Prefixes>
					<Custom Id='637795220126047081' IndexInText='0' ItemLength='14' Interface='UniversalExpressionParser.ExpressionItems.Custom.IKeywordBasedCustomExpressionItem'>
						<RegularItems>
							<Keyword Name='::types' Id='637795220126046456' IndexInText='0' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
								<LanguageKeywordInfo ObjectId='5' Id='637781062811583137' Keyword='::types' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
							</Keyword>
							<Braces Id='637795220126046569' IndexInText='7' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
								<RegularItems>
									<OpeningSquareBrace Name='[' Id='637795220126046574' IndexInText='7' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637795220126046761' IndexInText='8' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='T1' Id='637795220126046755' IndexInText='8' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<Comma Name=',' Id='637795220126046931' IndexInText='10' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637795220126047031' IndexInText='11' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='T2' Id='637795220126047026' IndexInText='11' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<ClosingSquareBrace Name=']' Id='637795220126047066' IndexInText='13' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Children>
									<Literal Id='637795220126046761' IndexInText='8' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<Literal Id='637795220126047031' IndexInText='11' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								</Children>
								<OtherProperties>
									<OpeningBraceInfo Name='[' Id='637795220126046574' IndexInText='7' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ClosingBraceInfo Name=']' Id='637795220126047066' IndexInText='13' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
									<Parameters ObjectId='14' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220126046761' IndexInText='8' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220126047031' IndexInText='11' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</Parameters>
								</OtherProperties>
							</Braces>
						</RegularItems>
						<OtherProperties>
							<LastKeywordExpressionItem Name='::types' Id='637795220126046456' IndexInText='0' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'/>
							<ErrorsPositionDisplayValue value='0' type='System.Int32' />
						</OtherProperties>
					</Custom>
					<Custom Id='637795220126047342' IndexInText='15' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.Custom.IKeywordBasedCustomExpressionItem'>
						<RegularItems>
							<Keyword Name='::types' Id='637795220126047126' IndexInText='15' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
								<LanguageKeywordInfo ObjectId='5' Id='637781062811583137' Keyword='::types' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
							</Keyword>
							<Braces Id='637795220126047170' IndexInText='22' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
								<RegularItems>
									<OpeningSquareBrace Name='[' Id='637795220126047178' IndexInText='22' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637795220126047298' IndexInText='23' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='T3' Id='637795220126047293' IndexInText='23' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<ClosingSquareBrace Name=']' Id='637795220126047334' IndexInText='25' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Children>
									<Literal Id='637795220126047298' IndexInText='23' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								</Children>
								<OtherProperties>
									<OpeningBraceInfo Name='[' Id='637795220126047178' IndexInText='22' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ClosingBraceInfo Name=']' Id='637795220126047334' IndexInText='25' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
									<Parameters ObjectId='22' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220126047298' IndexInText='23' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</Parameters>
								</OtherProperties>
							</Braces>
						</RegularItems>
						<OtherProperties>
							<LastKeywordExpressionItem Name='::types' Id='637795220126047126' IndexInText='15' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'/>
							<ErrorsPositionDisplayValue value='15' type='System.Int32' />
						</OtherProperties>
					</Custom>
				</Prefixes>
				<RegularItems>
					<Literal Id='637795220126047580' IndexInText='27' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='F1' Id='637795220126047573' IndexInText='27' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<OpeningRoundBrace Name='(' Id='637795220126047629' IndexInText='29' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<BinaryOperator Name=':' Priority='0' Id='637795220126048008' IndexInText='30' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.Literal Id='637795220126047724' IndexInText='30' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='x' Id='637795220126047719' IndexInText='30' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand1.Literal>
						<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637795220126047796' IndexInText='31' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<Name Name=':' Id='637795220126047777' IndexInText='31' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OperatorNameParts>
						</OperatorInfo>
						<Operand2.Literal Id='637795220126047955' IndexInText='32' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='T1' Id='637795220126047950' IndexInText='32' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand2.Literal>
					</BinaryOperator>
					<Comma Name=',' Id='637795220126048061' IndexInText='34' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<BinaryOperator Name=':' Priority='0' Id='637795220126048383' IndexInText='36' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.Literal Id='637795220126048153' IndexInText='36' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='y' Id='637795220126048148' IndexInText='36' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand1.Literal>
						<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637795220126048206' IndexInText='37' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<Name Name=':' Id='637795220126048197' IndexInText='37' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OperatorNameParts>
						</OperatorInfo>
						<Operand2.Literal Id='637795220126048349' IndexInText='38' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='T2' Id='637795220126048344' IndexInText='38' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand2.Literal>
					</BinaryOperator>
					<Comma Name=',' Id='637795220126048401' IndexInText='40' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<BinaryOperator Name=':' Priority='0' Id='637795220126048743' IndexInText='42' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.Literal Id='637795220126048493' IndexInText='42' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='z' Id='637795220126048489' IndexInText='42' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand1.Literal>
						<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637795220126048543' IndexInText='43' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<Name Name=':' Id='637795220126048537' IndexInText='43' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OperatorNameParts>
						</OperatorInfo>
						<Operand2.Literal Id='637795220126048705' IndexInText='45' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='T3' Id='637795220126048701' IndexInText='45' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand2.Literal>
					</BinaryOperator>
					<ClosingRoundBrace Name=')' Id='637795220126048761' IndexInText='47' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Postfixes>
					<CodeBlock Id='637795220126048797' IndexInText='50' ItemLength='109' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
						<RegularItems>
							<CodeBlockStartMarker Name='{' Id='637795220126048792' IndexInText='50' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637795220126048850' IndexInText='158' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<OtherProperties>
							<CodeBlockStartMarker Name='{' Id='637795220126048792' IndexInText='50' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637795220126048850' IndexInText='158' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OtherProperties>
					</CodeBlock>
				</Postfixes>
				<Children>
					<BinaryOperator Name=':' Priority='0' Id='637795220126048008' IndexInText='30' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
					<BinaryOperator Name=':' Priority='0' Id='637795220126048383' IndexInText='36' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
					<BinaryOperator Name=':' Priority='0' Id='637795220126048743' IndexInText='42' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
				</Children>
				<OtherProperties>
					<NamedExpressionItem Id='637795220126047580' IndexInText='27' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					<OpeningBraceInfo Name='(' Id='637795220126047629' IndexInText='29' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ClosingBraceInfo Name=')' Id='637795220126048761' IndexInText='47' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Parameters ObjectId='53' Count='3' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name=':' Priority='0' Id='637795220126048008' IndexInText='30' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name=':' Priority='0' Id='637795220126048383' IndexInText='36' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name=':' Priority='0' Id='637795220126048743' IndexInText='42' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
					</Parameters>
				</OtherProperties>
			</Braces>
		</RegularItems>
		<Children>
			<Braces Id='637795220126047623' IndexInText='0' ItemLength='159' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
		</Children>
	</ExpressionItemSeries>
	<ParseErrorData ObjectId='54' HasCriticalErrors='False' Interface='UniversalExpressionParser.IParseErrorData'>
		<AllCodeItemErrors ObjectId='55' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICodeItemParseErrorData]'/>
	</ParseErrorData>
	<SortedCommentedOutCodeInfos ObjectId='56' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICommentedOutCodeInfo]'>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='57' IsLineComment='True' IndexInText='57' ItemLength='99'/>
	</SortedCommentedOutCodeInfos>
	<MainExpressionItem Id='637795220126046219' IndexInText='0' ItemLength='159' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'/>
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
	<ExpressionItemSeries Id='637795220126046219' IndexInText='0' ItemLength='159' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'>
		<RegularItems>
			<Braces Id='637795220126047623' IndexInText='0' ItemLength='159' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
				<Prefixes>
					<Custom Id='637795220126047081' IndexInText='0' ItemLength='14' Interface='UniversalExpressionParser.ExpressionItems.Custom.IKeywordBasedCustomExpressionItem'>
						<RegularItems>
							<Keyword Name='::types' Id='637795220126046456' IndexInText='0' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
								<LanguageKeywordInfo ObjectId='5' Id='637781062811583137' Keyword='::types' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
							</Keyword>
							<Braces Id='637795220126046569' IndexInText='7' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
								<RegularItems>
									<OpeningSquareBrace Name='[' Id='637795220126046574' IndexInText='7' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637795220126046761' IndexInText='8' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='T1' Id='637795220126046755' IndexInText='8' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<Comma Name=',' Id='637795220126046931' IndexInText='10' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637795220126047031' IndexInText='11' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='T2' Id='637795220126047026' IndexInText='11' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<ClosingSquareBrace Name=']' Id='637795220126047066' IndexInText='13' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Children>
									<Literal Id='637795220126046761' IndexInText='8' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<Literal Id='637795220126047031' IndexInText='11' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								</Children>
								<OtherProperties>
									<OpeningBraceInfo Name='[' Id='637795220126046574' IndexInText='7' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ClosingBraceInfo Name=']' Id='637795220126047066' IndexInText='13' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
									<Parameters ObjectId='14' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220126046761' IndexInText='8' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220126047031' IndexInText='11' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</Parameters>
								</OtherProperties>
							</Braces>
						</RegularItems>
						<OtherProperties>
							<LastKeywordExpressionItem Name='::types' Id='637795220126046456' IndexInText='0' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'/>
							<ErrorsPositionDisplayValue value='0' type='System.Int32' />
						</OtherProperties>
					</Custom>
					<Custom Id='637795220126047342' IndexInText='15' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.Custom.IKeywordBasedCustomExpressionItem'>
						<RegularItems>
							<Keyword Name='::types' Id='637795220126047126' IndexInText='15' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
								<LanguageKeywordInfo ObjectId='5' Id='637781062811583137' Keyword='::types' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
							</Keyword>
							<Braces Id='637795220126047170' IndexInText='22' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
								<RegularItems>
									<OpeningSquareBrace Name='[' Id='637795220126047178' IndexInText='22' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637795220126047298' IndexInText='23' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='T3' Id='637795220126047293' IndexInText='23' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<ClosingSquareBrace Name=']' Id='637795220126047334' IndexInText='25' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Children>
									<Literal Id='637795220126047298' IndexInText='23' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								</Children>
								<OtherProperties>
									<OpeningBraceInfo Name='[' Id='637795220126047178' IndexInText='22' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ClosingBraceInfo Name=']' Id='637795220126047334' IndexInText='25' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
									<Parameters ObjectId='22' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220126047298' IndexInText='23' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</Parameters>
								</OtherProperties>
							</Braces>
						</RegularItems>
						<OtherProperties>
							<LastKeywordExpressionItem Name='::types' Id='637795220126047126' IndexInText='15' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'/>
							<ErrorsPositionDisplayValue value='15' type='System.Int32' />
						</OtherProperties>
					</Custom>
				</Prefixes>
				<RegularItems>
					<Literal Id='637795220126047580' IndexInText='27' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='F1' Id='637795220126047573' IndexInText='27' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<OpeningRoundBrace Name='(' Id='637795220126047629' IndexInText='29' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<BinaryOperator Name=':' Priority='0' Id='637795220126048008' IndexInText='30' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.Literal Id='637795220126047724' IndexInText='30' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='x' Id='637795220126047719' IndexInText='30' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand1.Literal>
						<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637795220126047796' IndexInText='31' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<Name Name=':' Id='637795220126047777' IndexInText='31' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OperatorNameParts>
						</OperatorInfo>
						<Operand2.Literal Id='637795220126047955' IndexInText='32' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='T1' Id='637795220126047950' IndexInText='32' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand2.Literal>
					</BinaryOperator>
					<Comma Name=',' Id='637795220126048061' IndexInText='34' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<BinaryOperator Name=':' Priority='0' Id='637795220126048383' IndexInText='36' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.Literal Id='637795220126048153' IndexInText='36' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='y' Id='637795220126048148' IndexInText='36' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand1.Literal>
						<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637795220126048206' IndexInText='37' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<Name Name=':' Id='637795220126048197' IndexInText='37' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OperatorNameParts>
						</OperatorInfo>
						<Operand2.Literal Id='637795220126048349' IndexInText='38' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='T2' Id='637795220126048344' IndexInText='38' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand2.Literal>
					</BinaryOperator>
					<Comma Name=',' Id='637795220126048401' IndexInText='40' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<BinaryOperator Name=':' Priority='0' Id='637795220126048743' IndexInText='42' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.Literal Id='637795220126048493' IndexInText='42' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='z' Id='637795220126048489' IndexInText='42' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand1.Literal>
						<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637795220126048543' IndexInText='43' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<Name Name=':' Id='637795220126048537' IndexInText='43' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OperatorNameParts>
						</OperatorInfo>
						<Operand2.Literal Id='637795220126048705' IndexInText='45' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='T3' Id='637795220126048701' IndexInText='45' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand2.Literal>
					</BinaryOperator>
					<ClosingRoundBrace Name=')' Id='637795220126048761' IndexInText='47' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Postfixes>
					<CodeBlock Id='637795220126048797' IndexInText='50' ItemLength='109' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
						<RegularItems>
							<CodeBlockStartMarker Name='{' Id='637795220126048792' IndexInText='50' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637795220126048850' IndexInText='158' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<OtherProperties>
							<CodeBlockStartMarker Name='{' Id='637795220126048792' IndexInText='50' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637795220126048850' IndexInText='158' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OtherProperties>
					</CodeBlock>
				</Postfixes>
				<Children>
					<BinaryOperator Name=':' Priority='0' Id='637795220126048008' IndexInText='30' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
					<BinaryOperator Name=':' Priority='0' Id='637795220126048383' IndexInText='36' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
					<BinaryOperator Name=':' Priority='0' Id='637795220126048743' IndexInText='42' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
				</Children>
				<OtherProperties>
					<NamedExpressionItem Id='637795220126047580' IndexInText='27' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					<OpeningBraceInfo Name='(' Id='637795220126047629' IndexInText='29' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ClosingBraceInfo Name=')' Id='637795220126048761' IndexInText='47' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Parameters ObjectId='53' Count='3' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name=':' Priority='0' Id='637795220126048008' IndexInText='30' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name=':' Priority='0' Id='637795220126048383' IndexInText='36' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name=':' Priority='0' Id='637795220126048743' IndexInText='42' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
					</Parameters>
				</OtherProperties>
			</Braces>
		</RegularItems>
		<Children>
			<Braces Id='637795220126047623' IndexInText='0' ItemLength='159' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
		</Children>
	</ExpressionItemSeries>
	<ParseErrorData ObjectId='54' HasCriticalErrors='False' Interface='UniversalExpressionParser.IParseErrorData'>
		<AllCodeItemErrors ObjectId='55' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICodeItemParseErrorData]'/>
	</ParseErrorData>
	<SortedCommentedOutCodeInfos ObjectId='56' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICommentedOutCodeInfo]'>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='57' IsLineComment='True' IndexInText='57' ItemLength='99'/>
	</SortedCommentedOutCodeInfos>
	<MainExpressionItem Id='637795220126046219' IndexInText='0' ItemLength='159' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'/>
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
	<ExpressionItemSeries Id='637795220126075195' IndexInText='95' ItemLength='943' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'>
		<RegularItems>
			<Literal Id='637795220126076295' IndexInText='95' ItemLength='943' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
				<NameExpressionItem Name='MyTests' Id='637795220126076289' IndexInText='209' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				<Prefixes>
					<Braces Id='637795220126075435' IndexInText='95' ItemLength='13' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<OpeningSquareBrace Name='[' Id='637795220126075443' IndexInText='95' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Literal Id='637795220126075574' IndexInText='96' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='TestFixture' Id='637795220126075569' IndexInText='96' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<ClosingSquareBrace Name=']' Id='637795220126075643' IndexInText='107' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<Literal Id='637795220126075574' IndexInText='96' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
						</Children>
						<OtherProperties>
							<OpeningBraceInfo Name='[' Id='637795220126075443' IndexInText='95' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=']' Id='637795220126075643' IndexInText='107' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
							<Parameters ObjectId='9' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220126075574' IndexInText='96' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Braces>
					<Braces Id='637795220126075690' IndexInText='110' ItemLength='30' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<OpeningSquareBrace Name='[' Id='637795220126075711' IndexInText='110' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Braces Id='637795220126075833' IndexInText='111' ItemLength='28' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
								<RegularItems>
									<Literal Id='637795220126075801' IndexInText='111' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='Attribute' Id='637795220126075796' IndexInText='111' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<OpeningRoundBrace Name='(' Id='637795220126075838' IndexInText='120' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ConstantText Id='637795220126075942' IndexInText='121' ItemLength='17' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='"IntegrationTest"' Id='637795220126075937' IndexInText='121' ItemLength='17' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</ConstantText>
									<ClosingRoundBrace Name=')' Id='637795220126075978' IndexInText='138' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Children>
									<ConstantText Id='637795220126075942' IndexInText='121' ItemLength='17' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								</Children>
								<OtherProperties>
									<NamedExpressionItem Id='637795220126075801' IndexInText='111' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<OpeningBraceInfo Name='(' Id='637795220126075838' IndexInText='120' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ClosingBraceInfo Name=')' Id='637795220126075978' IndexInText='138' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Parameters ObjectId='19' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220126075942' IndexInText='121' ItemLength='17' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</Parameters>
								</OtherProperties>
							</Braces>
							<ClosingSquareBrace Name=']' Id='637795220126076007' IndexInText='139' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<Braces Id='637795220126075833' IndexInText='111' ItemLength='28' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
						</Children>
						<OtherProperties>
							<OpeningBraceInfo Name='[' Id='637795220126075711' IndexInText='110' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=']' Id='637795220126076007' IndexInText='139' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
							<Parameters ObjectId='21' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220126075833' IndexInText='111' ItemLength='28' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Braces>
				</Prefixes>
				<AppliedKeywords>
					<Keyword Name='public' Id='637795220126076037' IndexInText='196' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
						<LanguageKeywordInfo ObjectId='23' Id='637783669980406419' Keyword='public' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
					</Keyword>
					<Keyword Name='class' Id='637795220126076051' IndexInText='203' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
						<LanguageKeywordInfo ObjectId='25' Id='637783670388745402' Keyword='class' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
					</Keyword>
				</AppliedKeywords>
				<Postfixes>
					<CodeBlock Id='637795220126076345' IndexInText='218' ItemLength='820' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
						<RegularItems>
							<CodeBlockStartMarker Name='{' Id='637795220126076339' IndexInText='218' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<BinaryOperator Name=':' Priority='0' Id='637795220126164228' IndexInText='461' ItemLength='574' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
								<Operand1.Braces Id='637795220126163811' IndexInText='461' ItemLength='517' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
									<Prefixes>
										<Braces Id='637795220126076401' IndexInText='461' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
											<RegularItems>
												<OpeningSquareBrace Name='[' Id='637795220126076405' IndexInText='461' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												<Literal Id='637795220126076498' IndexInText='462' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
													<NameExpressionItem Name='TestSetup' Id='637795220126076493' IndexInText='462' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												</Literal>
												<ClosingSquareBrace Name=']' Id='637795220126076528' IndexInText='471' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											</RegularItems>
											<Children>
												<Literal Id='637795220126076498' IndexInText='462' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
											</Children>
											<OtherProperties>
												<OpeningBraceInfo Name='[' Id='637795220126076405' IndexInText='461' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												<ClosingBraceInfo Name=']' Id='637795220126076528' IndexInText='471' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
												<Parameters ObjectId='35' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
													<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220126076498' IndexInText='462' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
												</Parameters>
											</OtherProperties>
										</Braces>
										<Braces Id='637795220126076570' IndexInText='478' ItemLength='50' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
											<RegularItems>
												<OpeningSquareBrace Name='[' Id='637795220126076576' IndexInText='478' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												<Braces Id='637795220126076697' IndexInText='479' ItemLength='48' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
													<RegularItems>
														<Literal Id='637795220126076665' IndexInText='479' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
															<NameExpressionItem Name='Attribute' Id='637795220126076660' IndexInText='479' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
														</Literal>
														<OpeningRoundBrace Name='(' Id='637795220126076700' IndexInText='488' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
														<ConstantText Id='637795220126076798' IndexInText='489' ItemLength='37' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
															<NameExpressionItem Name='"This is a demo of multiple prefixes"' Id='637795220126076794' IndexInText='489' ItemLength='37' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
														</ConstantText>
														<ClosingRoundBrace Name=')' Id='637795220126076842' IndexInText='526' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
													</RegularItems>
													<Children>
														<ConstantText Id='637795220126076798' IndexInText='489' ItemLength='37' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
													</Children>
													<OtherProperties>
														<NamedExpressionItem Id='637795220126076665' IndexInText='479' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
														<OpeningBraceInfo Name='(' Id='637795220126076700' IndexInText='488' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
														<ClosingBraceInfo Name=')' Id='637795220126076842' IndexInText='526' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
														<Parameters ObjectId='45' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
															<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220126076798' IndexInText='489' ItemLength='37' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
														</Parameters>
													</OtherProperties>
												</Braces>
												<ClosingSquareBrace Name=']' Id='637795220126076870' IndexInText='527' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											</RegularItems>
											<Children>
												<Braces Id='637795220126076697' IndexInText='479' ItemLength='48' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
											</Children>
											<OtherProperties>
												<OpeningBraceInfo Name='[' Id='637795220126076576' IndexInText='478' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												<ClosingBraceInfo Name=']' Id='637795220126076870' IndexInText='527' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
												<Parameters ObjectId='47' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
													<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220126076697' IndexInText='479' ItemLength='48' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
												</Parameters>
											</OtherProperties>
										</Braces>
										<Custom Id='637795220126163223' IndexInText='534' ItemLength='332' Interface='UniversalExpressionParser.ExpressionItems.Custom.IKeywordBasedCustomExpressionItem'>
											<RegularItems>
												<Keyword Name='::metadata' Id='637795220126076888' IndexInText='534' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
													<LanguageKeywordInfo ObjectId='50' Id='637781063212876967' Keyword='::metadata' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
												</Keyword>
												<CodeBlock Id='637795220126077177' IndexInText='545' ItemLength='321' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
													<RegularItems>
														<CodeBlockStartMarker Name='{' Id='637795220126077170' IndexInText='545' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
														<BinaryOperator Name=':' Priority='0' Id='637795220126077656' IndexInText='556' ItemLength='277' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
															<Operand1.Literal Id='637795220126077344' IndexInText='556' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
																<NameExpressionItem Name='Description' Id='637795220126077338' IndexInText='556' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
															</Operand1.Literal>
															<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637795220126077407' IndexInText='567' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
																<OperatorNameParts>
																	<Name Name=':' Id='637795220126077396' IndexInText='567' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
																</OperatorNameParts>
															</OperatorInfo>
															<Operand2.ConstantText Id='637795220126077600' IndexInText='569' ItemLength='264' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
																<NameExpressionItem Name='"Demo of custom expression item parsed to $line_break$                        UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IMetadataCustomExpressionItem$line_break$                        used in prefixes list of expression parsed from ''SetupMyTests()''"'
																  Id='637795220126077596' IndexInText='569' ItemLength='264' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
															</Operand2.ConstantText>
														</BinaryOperator>
														<ExpressionSeparator Name=';' Id='637795220126077701' IndexInText='833' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
														<BinaryOperator Name=':' Priority='0' Id='637795220126163144' IndexInText='844' ItemLength='15' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
															<Operand1.Literal Id='637795220126077801' IndexInText='844' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
																<NameExpressionItem Name='SomeMetadata' Id='637795220126077795' IndexInText='844' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
															</Operand1.Literal>
															<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637795220126077854' IndexInText='856' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
																<OperatorNameParts>
																	<Name Name=':' Id='637795220126077844' IndexInText='856' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
																</OperatorNameParts>
															</OperatorInfo>
															<Operand2.ConstantNumericValue Id='637795220126162951' IndexInText='858' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
																<NameExpressionItem Name='1' Id='637795220126162899' IndexInText='858' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
																<OtherProperties>
																	<SucceededNumericTypeDescriptor ObjectId='68' NumberTypeId='1581134183960' Interface='UniversalExpressionParser.NumericTypeDescriptor'>
																		<RegularExpressions ObjectId='69' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[System.String]'>
																			<System.String value='^\d+' />
																		</RegularExpressions>
																	</SucceededNumericTypeDescriptor>
																</OtherProperties>
															</Operand2.ConstantNumericValue>
														</BinaryOperator>
														<CodeBlockEndMarker Name='}' Id='637795220126163203' IndexInText='865' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
													</RegularItems>
													<Children>
														<BinaryOperator Name=':' Priority='0' Id='637795220126077656' IndexInText='556' ItemLength='277' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
														<BinaryOperator Name=':' Priority='0' Id='637795220126163144' IndexInText='844' ItemLength='15' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
													</Children>
													<OtherProperties>
														<CodeBlockStartMarker Name='{' Id='637795220126077170' IndexInText='545' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
														<CodeBlockEndMarker Name='}' Id='637795220126163203' IndexInText='865' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
													</OtherProperties>
												</CodeBlock>
											</RegularItems>
											<OtherProperties>
												<LastKeywordExpressionItem Name='::metadata' Id='637795220126076888' IndexInText='534' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'/>
												<ErrorsPositionDisplayValue value='534' type='System.Int32' />
											</OtherProperties>
										</Custom>
									</Prefixes>
									<AppliedKeywords>
										<Keyword Name='public' Id='637795220126163538' IndexInText='950' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
											<LanguageKeywordInfo ObjectId='23' Id='637783669980406419' Keyword='public' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
										</Keyword>
										<Keyword Name='static' Id='637795220126163563' IndexInText='957' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
											<LanguageKeywordInfo ObjectId='73' Id='637789181872233115' Keyword='static' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
										</Keyword>
									</AppliedKeywords>
									<RegularItems>
										<Literal Id='637795220126163756' IndexInText='964' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
											<NameExpressionItem Name='SetupMyTests' Id='637795220126163749' IndexInText='964' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</Literal>
										<OpeningRoundBrace Name='(' Id='637795220126163821' IndexInText='976' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										<ClosingRoundBrace Name=')' Id='637795220126163859' IndexInText='977' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</RegularItems>
									<OtherProperties>
										<NamedExpressionItem Id='637795220126163756' IndexInText='964' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
										<OpeningBraceInfo Name='(' Id='637795220126163821' IndexInText='976' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										<ClosingBraceInfo Name=')' Id='637795220126163859' IndexInText='977' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										<Parameters ObjectId='78' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'/>
									</OtherProperties>
								</Operand1.Braces>
								<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637795220126163921' IndexInText='979' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
									<OperatorNameParts>
										<Name Name=':' Id='637795220126163911' IndexInText='979' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</OperatorNameParts>
								</OperatorInfo>
								<Operand2.Literal Id='637795220126164087' IndexInText='981' ItemLength='54' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
									<NameExpressionItem Name='void' Id='637795220126164082' IndexInText='981' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Postfixes>
										<CodeBlock Id='637795220126164131' IndexInText='991' ItemLength='44' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
											<RegularItems>
												<CodeBlockStartMarker Name='{' Id='637795220126164126' IndexInText='991' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												<CodeBlockEndMarker Name='}' Id='637795220126164172' IndexInText='1034' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											</RegularItems>
											<OtherProperties>
												<CodeBlockStartMarker Name='{' Id='637795220126164126' IndexInText='991' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												<CodeBlockEndMarker Name='}' Id='637795220126164172' IndexInText='1034' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											</OtherProperties>
										</CodeBlock>
									</Postfixes>
								</Operand2.Literal>
							</BinaryOperator>
							<CodeBlockEndMarker Name='}' Id='637795220126164275' IndexInText='1037' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<BinaryOperator Name=':' Priority='0' Id='637795220126164228' IndexInText='461' ItemLength='574' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
						</Children>
						<OtherProperties>
							<CodeBlockStartMarker Name='{' Id='637795220126076339' IndexInText='218' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637795220126164275' IndexInText='1037' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OtherProperties>
					</CodeBlock>
				</Postfixes>
			</Literal>
		</RegularItems>
		<Children>
			<Literal Id='637795220126076295' IndexInText='95' ItemLength='943' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
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
	<MainExpressionItem Id='637795220126075195' IndexInText='95' ItemLength='943' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'/>
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
	<ExpressionItemSeries Id='637795220126197422' IndexInText='37' ItemLength='1783' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'>
		<RegularItems>
			<Literal Id='637795220126198846' IndexInText='37' ItemLength='33' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
				<NameExpressionItem Name='x' Id='637795220126198841' IndexInText='69' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				<Prefixes>
					<Braces Id='637795220126197718' IndexInText='37' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<OpeningSquareBrace Name='[' Id='637795220126197728' IndexInText='37' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Literal Id='637795220126197954' IndexInText='38' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='NotNull' Id='637795220126197944' IndexInText='38' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<ClosingSquareBrace Name=']' Id='637795220126198031' IndexInText='45' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<Literal Id='637795220126197954' IndexInText='38' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
						</Children>
						<OtherProperties>
							<OpeningBraceInfo Name='[' Id='637795220126197728' IndexInText='37' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=']' Id='637795220126198031' IndexInText='45' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
							<Parameters ObjectId='9' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220126197954' IndexInText='38' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Braces>
					<Braces Id='637795220126198345' IndexInText='47' ItemLength='21' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<OpeningSquareBrace Name='[' Id='637795220126198377' IndexInText='47' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Braces Id='637795220126198548' IndexInText='48' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
								<RegularItems>
									<Literal Id='637795220126198512' IndexInText='48' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='Attribute' Id='637795220126198496' IndexInText='48' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<OpeningRoundBrace Name='(' Id='637795220126198553' IndexInText='57' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ConstantText Id='637795220126198669' IndexInText='58' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='"Marker"' Id='637795220126198657' IndexInText='58' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</ConstantText>
									<ClosingRoundBrace Name=')' Id='637795220126198709' IndexInText='66' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Children>
									<ConstantText Id='637795220126198669' IndexInText='58' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								</Children>
								<OtherProperties>
									<NamedExpressionItem Id='637795220126198512' IndexInText='48' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<OpeningBraceInfo Name='(' Id='637795220126198553' IndexInText='57' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ClosingBraceInfo Name=')' Id='637795220126198709' IndexInText='66' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Parameters ObjectId='19' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220126198669' IndexInText='58' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</Parameters>
								</OtherProperties>
							</Braces>
							<ClosingSquareBrace Name=']' Id='637795220126198739' IndexInText='67' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<Braces Id='637795220126198548' IndexInText='48' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
						</Children>
						<OtherProperties>
							<OpeningBraceInfo Name='[' Id='637795220126198377' IndexInText='47' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=']' Id='637795220126198739' IndexInText='67' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
							<Parameters ObjectId='21' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220126198548' IndexInText='48' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Braces>
				</Prefixes>
			</Literal>
			<ExpressionSeparator Name=';' Id='637795220126198881' IndexInText='70' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<Braces Id='637795220126199508' IndexInText='227' ItemLength='38' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
				<Prefixes>
					<Braces Id='637795220126198929' IndexInText='227' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<OpeningSquareBrace Name='[' Id='637795220126198933' IndexInText='227' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Literal Id='637795220126199044' IndexInText='228' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='NotNull' Id='637795220126199038' IndexInText='228' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<ClosingSquareBrace Name=']' Id='637795220126199074' IndexInText='235' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<Literal Id='637795220126199044' IndexInText='228' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
						</Children>
						<OtherProperties>
							<OpeningBraceInfo Name='[' Id='637795220126198933' IndexInText='227' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=']' Id='637795220126199074' IndexInText='235' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
							<Parameters ObjectId='29' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220126199044' IndexInText='228' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Braces>
					<Braces Id='637795220126199101' IndexInText='237' ItemLength='21' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<OpeningSquareBrace Name='[' Id='637795220126199110' IndexInText='237' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Braces Id='637795220126199227' IndexInText='238' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
								<RegularItems>
									<Literal Id='637795220126199199' IndexInText='238' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='Attribute' Id='637795220126199195' IndexInText='238' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<OpeningRoundBrace Name='(' Id='637795220126199230' IndexInText='247' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ConstantText Id='637795220126199322' IndexInText='248' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='"Marker"' Id='637795220126199317' IndexInText='248' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</ConstantText>
									<ClosingRoundBrace Name=')' Id='637795220126199351' IndexInText='256' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Children>
									<ConstantText Id='637795220126199322' IndexInText='248' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								</Children>
								<OtherProperties>
									<NamedExpressionItem Id='637795220126199199' IndexInText='238' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<OpeningBraceInfo Name='(' Id='637795220126199230' IndexInText='247' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ClosingBraceInfo Name=')' Id='637795220126199351' IndexInText='256' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Parameters ObjectId='39' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220126199322' IndexInText='248' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</Parameters>
								</OtherProperties>
							</Braces>
							<ClosingSquareBrace Name=']' Id='637795220126199383' IndexInText='257' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<Braces Id='637795220126199227' IndexInText='238' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
						</Children>
						<OtherProperties>
							<OpeningBraceInfo Name='[' Id='637795220126199110' IndexInText='237' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=']' Id='637795220126199383' IndexInText='257' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
							<Parameters ObjectId='41' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220126199227' IndexInText='238' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Braces>
				</Prefixes>
				<RegularItems>
					<Literal Id='637795220126199469' IndexInText='259' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='f1' Id='637795220126199465' IndexInText='259' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<OpeningRoundBrace Name='(' Id='637795220126199513' IndexInText='261' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Literal Id='637795220126199598' IndexInText='262' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='x1' Id='637795220126199593' IndexInText='262' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<ClosingRoundBrace Name=')' Id='637795220126199631' IndexInText='264' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Children>
					<Literal Id='637795220126199598' IndexInText='262' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
				</Children>
				<OtherProperties>
					<NamedExpressionItem Id='637795220126199469' IndexInText='259' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					<OpeningBraceInfo Name='(' Id='637795220126199513' IndexInText='261' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ClosingBraceInfo Name=')' Id='637795220126199631' IndexInText='264' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Parameters ObjectId='48' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220126199598' IndexInText='262' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					</Parameters>
				</OtherProperties>
			</Braces>
			<ExpressionSeparator Name=';' Id='637795220126199659' IndexInText='265' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<Braces Id='637795220126200159' IndexInText='422' ItemLength='36' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
				<Prefixes>
					<Braces Id='637795220126199698' IndexInText='422' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<OpeningSquareBrace Name='[' Id='637795220126199701' IndexInText='422' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Literal Id='637795220126199790' IndexInText='423' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='NotNull' Id='637795220126199786' IndexInText='423' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<ClosingSquareBrace Name=']' Id='637795220126199820' IndexInText='430' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<Literal Id='637795220126199790' IndexInText='423' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
						</Children>
						<OtherProperties>
							<OpeningBraceInfo Name='[' Id='637795220126199701' IndexInText='422' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=']' Id='637795220126199820' IndexInText='430' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
							<Parameters ObjectId='56' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220126199790' IndexInText='423' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Braces>
					<Braces Id='637795220126199849' IndexInText='432' ItemLength='21' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<OpeningSquareBrace Name='[' Id='637795220126199854' IndexInText='432' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Braces Id='637795220126199970' IndexInText='433' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
								<RegularItems>
									<Literal Id='637795220126199944' IndexInText='433' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='Attribute' Id='637795220126199939' IndexInText='433' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<OpeningRoundBrace Name='(' Id='637795220126199974' IndexInText='442' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ConstantText Id='637795220126200065' IndexInText='443' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='"Marker"' Id='637795220126200061' IndexInText='443' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</ConstantText>
									<ClosingRoundBrace Name=')' Id='637795220126200097' IndexInText='451' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Children>
									<ConstantText Id='637795220126200065' IndexInText='443' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								</Children>
								<OtherProperties>
									<NamedExpressionItem Id='637795220126199944' IndexInText='433' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<OpeningBraceInfo Name='(' Id='637795220126199974' IndexInText='442' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ClosingBraceInfo Name=')' Id='637795220126200097' IndexInText='451' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Parameters ObjectId='66' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220126200065' IndexInText='443' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</Parameters>
								</OtherProperties>
							</Braces>
							<ClosingSquareBrace Name=']' Id='637795220126200130' IndexInText='452' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<Braces Id='637795220126199970' IndexInText='433' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
						</Children>
						<OtherProperties>
							<OpeningBraceInfo Name='[' Id='637795220126199854' IndexInText='432' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=']' Id='637795220126200130' IndexInText='452' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
							<Parameters ObjectId='68' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220126199970' IndexInText='433' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Braces>
				</Prefixes>
				<RegularItems>
					<OpeningRoundBrace Name='(' Id='637795220126200163' IndexInText='454' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Literal Id='637795220126200251' IndexInText='455' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='x1' Id='637795220126200242' IndexInText='455' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<ClosingRoundBrace Name=')' Id='637795220126200279' IndexInText='457' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Children>
					<Literal Id='637795220126200251' IndexInText='455' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
				</Children>
				<OtherProperties>
					<OpeningBraceInfo Name='(' Id='637795220126200163' IndexInText='454' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ClosingBraceInfo Name=')' Id='637795220126200279' IndexInText='457' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
					<Parameters ObjectId='73' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220126200251' IndexInText='455' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					</Parameters>
				</OtherProperties>
			</Braces>
			<ExpressionSeparator Name=';' Id='637795220126200305' IndexInText='458' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<Braces Id='637795220126200880' IndexInText='622' ItemLength='38' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
				<Prefixes>
					<Braces Id='637795220126200342' IndexInText='622' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<OpeningSquareBrace Name='[' Id='637795220126200346' IndexInText='622' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Literal Id='637795220126200432' IndexInText='623' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='NotNull' Id='637795220126200427' IndexInText='623' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<ClosingSquareBrace Name=']' Id='637795220126200459' IndexInText='630' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<Literal Id='637795220126200432' IndexInText='623' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
						</Children>
						<OtherProperties>
							<OpeningBraceInfo Name='[' Id='637795220126200346' IndexInText='622' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=']' Id='637795220126200459' IndexInText='630' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
							<Parameters ObjectId='81' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220126200432' IndexInText='623' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Braces>
					<Braces Id='637795220126200489' IndexInText='632' ItemLength='21' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<OpeningSquareBrace Name='[' Id='637795220126200494' IndexInText='632' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Braces Id='637795220126200610' IndexInText='633' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
								<RegularItems>
									<Literal Id='637795220126200578' IndexInText='633' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='Attribute' Id='637795220126200573' IndexInText='633' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<OpeningRoundBrace Name='(' Id='637795220126200614' IndexInText='642' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ConstantText Id='637795220126200701' IndexInText='643' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='"Marker"' Id='637795220126200696' IndexInText='643' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</ConstantText>
									<ClosingRoundBrace Name=')' Id='637795220126200729' IndexInText='651' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Children>
									<ConstantText Id='637795220126200701' IndexInText='643' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								</Children>
								<OtherProperties>
									<NamedExpressionItem Id='637795220126200578' IndexInText='633' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<OpeningBraceInfo Name='(' Id='637795220126200614' IndexInText='642' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ClosingBraceInfo Name=')' Id='637795220126200729' IndexInText='651' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Parameters ObjectId='91' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220126200701' IndexInText='643' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</Parameters>
								</OtherProperties>
							</Braces>
							<ClosingSquareBrace Name=']' Id='637795220126200760' IndexInText='652' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<Braces Id='637795220126200610' IndexInText='633' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
						</Children>
						<OtherProperties>
							<OpeningBraceInfo Name='[' Id='637795220126200494' IndexInText='632' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=']' Id='637795220126200760' IndexInText='652' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
							<Parameters ObjectId='93' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220126200610' IndexInText='633' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Braces>
				</Prefixes>
				<RegularItems>
					<Literal Id='637795220126200847' IndexInText='654' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='m1' Id='637795220126200842' IndexInText='654' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<OpeningSquareBrace Name='[' Id='637795220126200885' IndexInText='656' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Literal Id='637795220126200970' IndexInText='657' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='x1' Id='637795220126200964' IndexInText='657' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<ClosingSquareBrace Name=']' Id='637795220126201002' IndexInText='659' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Children>
					<Literal Id='637795220126200970' IndexInText='657' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
				</Children>
				<OtherProperties>
					<NamedExpressionItem Id='637795220126200847' IndexInText='654' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					<OpeningBraceInfo Name='[' Id='637795220126200885' IndexInText='656' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ClosingBraceInfo Name=']' Id='637795220126201002' IndexInText='659' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Parameters ObjectId='100' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220126200970' IndexInText='657' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					</Parameters>
				</OtherProperties>
			</Braces>
			<ExpressionSeparator Name=';' Id='637795220126201031' IndexInText='660' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<Braces Id='637795220126201548' IndexInText='819' ItemLength='36' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
				<Prefixes>
					<Braces Id='637795220126201068' IndexInText='819' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<OpeningSquareBrace Name='[' Id='637795220126201072' IndexInText='819' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Literal Id='637795220126201161' IndexInText='820' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='NotNull' Id='637795220126201157' IndexInText='820' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<ClosingSquareBrace Name=']' Id='637795220126201190' IndexInText='827' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<Literal Id='637795220126201161' IndexInText='820' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
						</Children>
						<OtherProperties>
							<OpeningBraceInfo Name='[' Id='637795220126201072' IndexInText='819' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=']' Id='637795220126201190' IndexInText='827' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
							<Parameters ObjectId='108' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220126201161' IndexInText='820' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Braces>
					<Braces Id='637795220126201216' IndexInText='829' ItemLength='21' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<OpeningSquareBrace Name='[' Id='637795220126201222' IndexInText='829' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Braces Id='637795220126201338' IndexInText='830' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
								<RegularItems>
									<Literal Id='637795220126201312' IndexInText='830' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='Attribute' Id='637795220126201307' IndexInText='830' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<OpeningRoundBrace Name='(' Id='637795220126201341' IndexInText='839' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ConstantText Id='637795220126201431' IndexInText='840' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='"Marker"' Id='637795220126201426' IndexInText='840' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</ConstantText>
									<ClosingRoundBrace Name=')' Id='637795220126201459' IndexInText='848' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Children>
									<ConstantText Id='637795220126201431' IndexInText='840' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								</Children>
								<OtherProperties>
									<NamedExpressionItem Id='637795220126201312' IndexInText='830' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<OpeningBraceInfo Name='(' Id='637795220126201341' IndexInText='839' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ClosingBraceInfo Name=')' Id='637795220126201459' IndexInText='848' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Parameters ObjectId='118' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220126201431' IndexInText='840' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</Parameters>
								</OtherProperties>
							</Braces>
							<ClosingSquareBrace Name=']' Id='637795220126201519' IndexInText='849' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<Braces Id='637795220126201338' IndexInText='830' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
						</Children>
						<OtherProperties>
							<OpeningBraceInfo Name='[' Id='637795220126201222' IndexInText='829' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=']' Id='637795220126201519' IndexInText='849' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
							<Parameters ObjectId='120' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220126201338' IndexInText='830' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Braces>
				</Prefixes>
				<RegularItems>
					<OpeningSquareBrace Name='[' Id='637795220126201553' IndexInText='851' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Literal Id='637795220126201645' IndexInText='852' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='x1' Id='637795220126201640' IndexInText='852' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<ClosingSquareBrace Name=']' Id='637795220126201673' IndexInText='854' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Children>
					<Literal Id='637795220126201645' IndexInText='852' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
				</Children>
				<OtherProperties>
					<OpeningBraceInfo Name='[' Id='637795220126201553' IndexInText='851' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ClosingBraceInfo Name=']' Id='637795220126201673' IndexInText='854' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
					<Parameters ObjectId='125' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220126201645' IndexInText='852' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					</Parameters>
				</OtherProperties>
			</Braces>
			<ExpressionSeparator Name=';' Id='637795220126201698' IndexInText='855' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<CodeBlock Id='637795220126202287' IndexInText='1174' ItemLength='28' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
				<Prefixes>
					<Custom Id='637795220126202145' IndexInText='1174' ItemLength='14' Interface='UniversalExpressionParser.ExpressionItems.Custom.IKeywordBasedCustomExpressionItem'>
						<RegularItems>
							<Keyword Name='::types' Id='637795220126201736' IndexInText='1174' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
								<LanguageKeywordInfo ObjectId='130' Id='637781062811583137' Keyword='::types' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
							</Keyword>
							<Braces Id='637795220126201817' IndexInText='1181' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
								<RegularItems>
									<OpeningSquareBrace Name='[' Id='637795220126201821' IndexInText='1181' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637795220126201979' IndexInText='1182' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='T1' Id='637795220126201973' IndexInText='1182' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<Comma Name=',' Id='637795220126202018' IndexInText='1184' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637795220126202101' IndexInText='1185' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='T2' Id='637795220126202096' IndexInText='1185' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<ClosingSquareBrace Name=']' Id='637795220126202136' IndexInText='1187' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Children>
									<Literal Id='637795220126201979' IndexInText='1182' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<Literal Id='637795220126202101' IndexInText='1185' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								</Children>
								<OtherProperties>
									<OpeningBraceInfo Name='[' Id='637795220126201821' IndexInText='1181' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ClosingBraceInfo Name=']' Id='637795220126202136' IndexInText='1187' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
									<Parameters ObjectId='139' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220126201979' IndexInText='1182' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220126202101' IndexInText='1185' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</Parameters>
								</OtherProperties>
							</Braces>
						</RegularItems>
						<OtherProperties>
							<LastKeywordExpressionItem Name='::types' Id='637795220126201736' IndexInText='1174' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'/>
							<ErrorsPositionDisplayValue value='1174' type='System.Int32' />
						</OtherProperties>
					</Custom>
				</Prefixes>
				<RegularItems>
					<CodeBlockStartMarker Name='{' Id='637795220126202281' IndexInText='1189' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<BinaryOperator Name='=' Priority='2000' Id='637795220126283528' IndexInText='1190' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.Literal Id='637795220126202410' IndexInText='1190' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='i' Id='637795220126202405' IndexInText='1194' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<AppliedKeywords>
								<Keyword Name='var' Id='637795220126202315' IndexInText='1190' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
									<LanguageKeywordInfo ObjectId='145' Id='637781064051574641' Keyword='var' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
								</Keyword>
							</AppliedKeywords>
						</Operand1.Literal>
						<OperatorInfo OperatorType='BinaryOperator' Name='=' Priority='2000' Id='637795220126202492' IndexInText='1196' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<Name Name='=' Id='637795220126202481' IndexInText='1196' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OperatorNameParts>
						</OperatorInfo>
						<Operand2.ConstantNumericValue Id='637795220126283350' IndexInText='1198' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
							<NameExpressionItem Name='12' Id='637795220126283299' IndexInText='1198' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<OtherProperties>
								<SucceededNumericTypeDescriptor ObjectId='150' NumberTypeId='1581134183960' Interface='UniversalExpressionParser.NumericTypeDescriptor'>
									<RegularExpressions ObjectId='151' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[System.String]'>
										<System.String value='^\d+' />
									</RegularExpressions>
								</SucceededNumericTypeDescriptor>
							</OtherProperties>
						</Operand2.ConstantNumericValue>
					</BinaryOperator>
					<ExpressionSeparator Name=';' Id='637795220126283590' IndexInText='1200' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<CodeBlockEndMarker Name='}' Id='637795220126283630' IndexInText='1201' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Children>
					<BinaryOperator Name='=' Priority='2000' Id='637795220126283528' IndexInText='1190' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
				</Children>
				<OtherProperties>
					<CodeBlockStartMarker Name='{' Id='637795220126202281' IndexInText='1189' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<CodeBlockEndMarker Name='}' Id='637795220126283630' IndexInText='1201' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</OtherProperties>
			</CodeBlock>
			<ExpressionSeparator Name=';' Id='637795220126283675' IndexInText='1202' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<Custom Id='637795220126284228' IndexInText='1395' ItemLength='32' Interface='UniversalExpressionParser.ExpressionItems.Custom.IKeywordBasedCustomExpressionItem'>
				<Prefixes>
					<Braces Id='637795220126283742' IndexInText='1395' ItemLength='21' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<OpeningSquareBrace Name='[' Id='637795220126283747' IndexInText='1395' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Braces Id='637795220126283927' IndexInText='1396' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
								<RegularItems>
									<Literal Id='637795220126283896' IndexInText='1396' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='Attribute' Id='637795220126283885' IndexInText='1396' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<OpeningRoundBrace Name='(' Id='637795220126283932' IndexInText='1405' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ConstantText Id='637795220126284042' IndexInText='1406' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='"Marker"' Id='637795220126284032' IndexInText='1406' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</ConstantText>
									<ClosingRoundBrace Name=')' Id='637795220126284079' IndexInText='1414' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Children>
									<ConstantText Id='637795220126284042' IndexInText='1406' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								</Children>
								<OtherProperties>
									<NamedExpressionItem Id='637795220126283896' IndexInText='1396' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<OpeningBraceInfo Name='(' Id='637795220126283932' IndexInText='1405' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ClosingBraceInfo Name=')' Id='637795220126284079' IndexInText='1414' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Parameters ObjectId='165' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220126284042' IndexInText='1406' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</Parameters>
								</OtherProperties>
							</Braces>
							<ClosingSquareBrace Name=']' Id='637795220126284111' IndexInText='1415' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<Braces Id='637795220126283927' IndexInText='1396' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
						</Children>
						<OtherProperties>
							<OpeningBraceInfo Name='[' Id='637795220126283747' IndexInText='1395' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=']' Id='637795220126284111' IndexInText='1415' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
							<Parameters ObjectId='167' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220126283927' IndexInText='1396' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Braces>
				</Prefixes>
				<RegularItems>
					<Keyword Name='::pragma' Id='637795220126284133' IndexInText='1417' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
						<LanguageKeywordInfo ObjectId='169' Id='637781063127646487' Keyword='::pragma' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
					</Keyword>
					<Name Name='x' Id='637795220126284217' IndexInText='1426' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<OtherProperties>
					<LastKeywordExpressionItem Name='::pragma' Id='637795220126284133' IndexInText='1417' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'/>
					<ErrorsPositionDisplayValue value='1417' type='System.Int32' />
				</OtherProperties>
			</Custom>
			<ExpressionSeparator Name=';' Id='637795220126284411' IndexInText='1427' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<ConstantText Id='637795220126284829' IndexInText='1587' ItemLength='33' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
				<NameExpressionItem Name='"Some text"' Id='637795220126284824' IndexInText='1609' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				<Prefixes>
					<Braces Id='637795220126284456' IndexInText='1587' ItemLength='21' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<OpeningSquareBrace Name='[' Id='637795220126284460' IndexInText='1587' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Braces Id='637795220126284585' IndexInText='1588' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
								<RegularItems>
									<Literal Id='637795220126284557' IndexInText='1588' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='Attribute' Id='637795220126284552' IndexInText='1588' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<OpeningRoundBrace Name='(' Id='637795220126284588' IndexInText='1597' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ConstantText Id='637795220126284681' IndexInText='1598' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='"Marker"' Id='637795220126284676' IndexInText='1598' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</ConstantText>
									<ClosingRoundBrace Name=')' Id='637795220126284712' IndexInText='1606' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Children>
									<ConstantText Id='637795220126284681' IndexInText='1598' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								</Children>
								<OtherProperties>
									<NamedExpressionItem Id='637795220126284557' IndexInText='1588' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<OpeningBraceInfo Name='(' Id='637795220126284588' IndexInText='1597' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ClosingBraceInfo Name=')' Id='637795220126284712' IndexInText='1606' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Parameters ObjectId='183' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220126284681' IndexInText='1598' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</Parameters>
								</OtherProperties>
							</Braces>
							<ClosingSquareBrace Name=']' Id='637795220126284740' IndexInText='1607' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<Braces Id='637795220126284585' IndexInText='1588' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
						</Children>
						<OtherProperties>
							<OpeningBraceInfo Name='[' Id='637795220126284460' IndexInText='1587' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=']' Id='637795220126284740' IndexInText='1607' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
							<Parameters ObjectId='185' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220126284585' IndexInText='1588' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Braces>
				</Prefixes>
			</ConstantText>
			<ExpressionSeparator Name=';' Id='637795220126284868' IndexInText='1620' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<ConstantNumericValue Id='637795220126343114' IndexInText='1789' ItemLength='30' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
				<NameExpressionItem Name='0.5e-3.4' Id='637795220126343049' IndexInText='1811' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				<Prefixes>
					<Braces Id='637795220126284907' IndexInText='1789' ItemLength='21' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<OpeningSquareBrace Name='[' Id='637795220126284911' IndexInText='1789' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Braces Id='637795220126285026' IndexInText='1790' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
								<RegularItems>
									<Literal Id='637795220126284999' IndexInText='1790' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='Attribute' Id='637795220126284995' IndexInText='1790' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<OpeningRoundBrace Name='(' Id='637795220126285029' IndexInText='1799' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ConstantText Id='637795220126285118' IndexInText='1800' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='"Marker"' Id='637795220126285113' IndexInText='1800' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</ConstantText>
									<ClosingRoundBrace Name=')' Id='637795220126285148' IndexInText='1808' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Children>
									<ConstantText Id='637795220126285118' IndexInText='1800' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								</Children>
								<OtherProperties>
									<NamedExpressionItem Id='637795220126284999' IndexInText='1790' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<OpeningBraceInfo Name='(' Id='637795220126285029' IndexInText='1799' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ClosingBraceInfo Name=')' Id='637795220126285148' IndexInText='1808' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Parameters ObjectId='198' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220126285118' IndexInText='1800' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</Parameters>
								</OtherProperties>
							</Braces>
							<ClosingSquareBrace Name=']' Id='637795220126285180' IndexInText='1809' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<Braces Id='637795220126285026' IndexInText='1790' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
						</Children>
						<OtherProperties>
							<OpeningBraceInfo Name='[' Id='637795220126284911' IndexInText='1789' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=']' Id='637795220126285180' IndexInText='1809' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
							<Parameters ObjectId='200' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220126285026' IndexInText='1790' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
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
			<ExpressionSeparator Name=';' Id='637795220126343306' IndexInText='1819' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
		</RegularItems>
		<Children>
			<Literal Id='637795220126198846' IndexInText='37' ItemLength='33' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
			<Braces Id='637795220126199508' IndexInText='227' ItemLength='38' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
			<Braces Id='637795220126200159' IndexInText='422' ItemLength='36' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
			<Braces Id='637795220126200880' IndexInText='622' ItemLength='38' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
			<Braces Id='637795220126201548' IndexInText='819' ItemLength='36' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
			<CodeBlock Id='637795220126202287' IndexInText='1174' ItemLength='28' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'/>
			<Custom Id='637795220126284228' IndexInText='1395' ItemLength='32' Interface='UniversalExpressionParser.ExpressionItems.Custom.IKeywordBasedCustomExpressionItem'/>
			<ConstantText Id='637795220126284829' IndexInText='1587' ItemLength='33' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
			<ConstantNumericValue Id='637795220126343114' IndexInText='1789' ItemLength='30' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'/>
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
	<MainExpressionItem Id='637795220126197422' IndexInText='37' ItemLength='1783' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'/>
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
	<ExpressionItemSeries Id='637795220125939276' IndexInText='0' ItemLength='117' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'>
		<RegularItems>
			<Literal Id='637795220125939613' IndexInText='0' ItemLength='117' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
				<NameExpressionItem Name='Dog' Id='637795220125939608' IndexInText='13' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				<AppliedKeywords>
					<Keyword Name='public' Id='637795220125939445' IndexInText='0' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
						<LanguageKeywordInfo ObjectId='5' Id='637783669980406419' Keyword='public' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
					</Keyword>
					<Keyword Name='class' Id='637795220125939461' IndexInText='7' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
						<LanguageKeywordInfo ObjectId='7' Id='637783670388745402' Keyword='class' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
					</Keyword>
				</AppliedKeywords>
				<Postfixes>
					<CodeBlock Id='637795220125939706' IndexInText='18' ItemLength='99' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
						<RegularItems>
							<CodeBlockStartMarker Name='{' Id='637795220125939701' IndexInText='18' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637795220125939758' IndexInText='116' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<OtherProperties>
							<CodeBlockStartMarker Name='{' Id='637795220125939701' IndexInText='18' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637795220125939758' IndexInText='116' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OtherProperties>
					</CodeBlock>
				</Postfixes>
			</Literal>
		</RegularItems>
		<Children>
			<Literal Id='637795220125939613' IndexInText='0' ItemLength='117' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
		</Children>
	</ExpressionItemSeries>
	<ParseErrorData ObjectId='11' HasCriticalErrors='False' Interface='UniversalExpressionParser.IParseErrorData'>
		<AllCodeItemErrors ObjectId='12' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICodeItemParseErrorData]'/>
	</ParseErrorData>
	<SortedCommentedOutCodeInfos ObjectId='13' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICommentedOutCodeInfo]'>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='14' IsLineComment='True' IndexInText='24' ItemLength='90'/>
	</SortedCommentedOutCodeInfos>
	<MainExpressionItem Id='637795220125939276' IndexInText='0' ItemLength='117' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'/>
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
	<ExpressionItemSeries Id='637795220125968791' IndexInText='0' ItemLength='416' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'>
		<RegularItems>
			<Braces Id='637795220125970320' IndexInText='0' ItemLength='416' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
				<Prefixes>
					<Custom Id='637795220125969629' IndexInText='0' ItemLength='18' Interface='UniversalExpressionParser.ExpressionItems.Custom.IKeywordBasedCustomExpressionItem'>
						<RegularItems>
							<Keyword Name='::types' Id='637795220125968990' IndexInText='0' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
								<LanguageKeywordInfo ObjectId='5' Id='637781062811583137' Keyword='::types' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
							</Keyword>
							<Braces Id='637795220125969104' IndexInText='7' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
								<RegularItems>
									<OpeningSquareBrace Name='[' Id='637795220125969111' IndexInText='7' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637795220125969281' IndexInText='8' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='T1' Id='637795220125969275' IndexInText='8' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<Comma Name=',' Id='637795220125969349' IndexInText='10' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637795220125969439' IndexInText='11' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='T2' Id='637795220125969434' IndexInText='11' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<Comma Name=',' Id='637795220125969469' IndexInText='13' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637795220125969581' IndexInText='15' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='T3' Id='637795220125969576' IndexInText='15' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<ClosingSquareBrace Name=']' Id='637795220125969616' IndexInText='17' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Children>
									<Literal Id='637795220125969281' IndexInText='8' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<Literal Id='637795220125969439' IndexInText='11' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<Literal Id='637795220125969581' IndexInText='15' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								</Children>
								<OtherProperties>
									<OpeningBraceInfo Name='[' Id='637795220125969111' IndexInText='7' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ClosingBraceInfo Name=']' Id='637795220125969616' IndexInText='17' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
									<Parameters ObjectId='17' Count='3' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220125969281' IndexInText='8' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220125969439' IndexInText='11' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220125969581' IndexInText='15' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</Parameters>
								</OtherProperties>
							</Braces>
						</RegularItems>
						<OtherProperties>
							<LastKeywordExpressionItem Name='::types' Id='637795220125968990' IndexInText='0' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'/>
							<ErrorsPositionDisplayValue value='0' type='System.Int32' />
						</OtherProperties>
					</Custom>
				</Prefixes>
				<RegularItems>
					<Literal Id='637795220125969762' IndexInText='19' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='F1' Id='637795220125969757' IndexInText='19' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<OpeningRoundBrace Name='(' Id='637795220125970351' IndexInText='21' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<BinaryOperator Name=':' Priority='0' Id='637795220125973216' IndexInText='22' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.Literal Id='637795220125972763' IndexInText='22' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='x' Id='637795220125972599' IndexInText='22' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand1.Literal>
						<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637795220125972953' IndexInText='23' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<Name Name=':' Id='637795220125972936' IndexInText='23' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OperatorNameParts>
						</OperatorInfo>
						<Operand2.Literal Id='637795220125973138' IndexInText='24' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='T1' Id='637795220125973132' IndexInText='24' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand2.Literal>
					</BinaryOperator>
					<Comma Name=',' Id='637795220125973348' IndexInText='26' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<BinaryOperator Name=':' Priority='0' Id='637795220125973705' IndexInText='28' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.Literal Id='637795220125973456' IndexInText='28' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='y' Id='637795220125973451' IndexInText='28' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand1.Literal>
						<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637795220125973509' IndexInText='29' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<Name Name=':' Id='637795220125973503' IndexInText='29' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OperatorNameParts>
						</OperatorInfo>
						<Operand2.Literal Id='637795220125973660' IndexInText='30' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='T2' Id='637795220125973655' IndexInText='30' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand2.Literal>
					</BinaryOperator>
					<Comma Name=',' Id='637795220125973724' IndexInText='32' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<BinaryOperator Name=':' Priority='0' Id='637795220125974063' IndexInText='34' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.Literal Id='637795220125973814' IndexInText='34' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='z' Id='637795220125973809' IndexInText='34' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand1.Literal>
						<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637795220125973867' IndexInText='35' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<Name Name=':' Id='637795220125973861' IndexInText='35' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OperatorNameParts>
						</OperatorInfo>
						<Operand2.Literal Id='637795220125974024' IndexInText='37' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='T3' Id='637795220125974019' IndexInText='37' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand2.Literal>
					</BinaryOperator>
					<ClosingRoundBrace Name=')' Id='637795220125974080' IndexInText='39' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Postfixes>
					<Custom Id='637795220125974228' IndexInText='143' ItemLength='37' Interface='UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IWhereCustomExpressionItem'>
						<RegularItems>
							<CustomExpressionItemPart Id='637795220125974235' IndexInText='143' ItemLength='12' Interface='UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem'>
								<RegularItems>
									<Keyword Name='where' Id='637795220125974138' IndexInText='143' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
										<LanguageKeywordInfo ObjectId='48' Id='637781062905654923' Keyword='where' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
									</Keyword>
									<Name Name='T1' Id='637795220125974293' IndexInText='149' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Name Name=':' Id='637795220125974303' IndexInText='151' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637795220125974320' IndexInText='152' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='int' Id='637795220125974312' IndexInText='152' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
								</RegularItems>
								<OtherProperties>
									<WhereKeyword Name='where' Id='637795220125974138' IndexInText='143' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'/>
									<TypeName Name='T1' Id='637795220125974293' IndexInText='149' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<TypeConstraints ObjectId='53' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220125974320' IndexInText='152' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</TypeConstraints>
								</OtherProperties>
							</CustomExpressionItemPart>
							<CustomExpressionItemPart Id='637795220125974449' IndexInText='156' ItemLength='15' Interface='UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem'>
								<RegularItems>
									<Keyword Name='where' Id='637795220125974415' IndexInText='156' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
										<LanguageKeywordInfo ObjectId='48' Id='637781062905654923' Keyword='where' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
									</Keyword>
									<Name Name='T2' Id='637795220125974505' IndexInText='162' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Name Name=':' Id='637795220125974514' IndexInText='164' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637795220125974532' IndexInText='165' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='double' Id='637795220125974524' IndexInText='165' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
								</RegularItems>
								<OtherProperties>
									<WhereKeyword Name='where' Id='637795220125974415' IndexInText='156' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'/>
									<TypeName Name='T2' Id='637795220125974505' IndexInText='162' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<TypeConstraints ObjectId='60' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220125974532' IndexInText='165' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</TypeConstraints>
								</OtherProperties>
							</CustomExpressionItemPart>
							<Name Name='whereend' Id='637795220125974577' IndexInText='172' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<CustomExpressionItemPart Id='637795220125974235' IndexInText='143' ItemLength='12' Interface='UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem'/>
							<CustomExpressionItemPart Id='637795220125974449' IndexInText='156' ItemLength='15' Interface='UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem'/>
						</Children>
						<OtherProperties>
							<WhereEndMarker Name='whereend' Id='637795220125974577' IndexInText='172' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<GenericTypeDataExpressionItems ObjectId='62' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem]'>
								<UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem Id='637795220125974235' IndexInText='143' ItemLength='12'/>
								<UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem Id='637795220125974449' IndexInText='156' ItemLength='15'/>
							</GenericTypeDataExpressionItems>
							<ErrorsPositionDisplayValue value='143' type='System.Int32' />
						</OtherProperties>
					</Custom>
					<Custom Id='637795220125974728' IndexInText='283' ItemLength='21' Interface='UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IWhereCustomExpressionItem'>
						<RegularItems>
							<CustomExpressionItemPart Id='637795220125974735' IndexInText='283' ItemLength='11' Interface='UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem'>
								<RegularItems>
									<Keyword Name='where' Id='637795220125974661' IndexInText='283' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
										<LanguageKeywordInfo ObjectId='48' Id='637781062905654923' Keyword='where' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
									</Keyword>
									<Name Name='T3' Id='637795220125974756' IndexInText='289' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Name Name=':' Id='637795220125974763' IndexInText='291' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637795220125974777' IndexInText='292' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='T1' Id='637795220125974770' IndexInText='292' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
								</RegularItems>
								<OtherProperties>
									<WhereKeyword Name='where' Id='637795220125974661' IndexInText='283' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'/>
									<TypeName Name='T3' Id='637795220125974756' IndexInText='289' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<TypeConstraints ObjectId='70' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220125974777' IndexInText='292' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</TypeConstraints>
								</OtherProperties>
							</CustomExpressionItemPart>
							<Name Name='whereend' Id='637795220125974793' IndexInText='296' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<CustomExpressionItemPart Id='637795220125974735' IndexInText='283' ItemLength='11' Interface='UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem'/>
						</Children>
						<OtherProperties>
							<WhereEndMarker Name='whereend' Id='637795220125974793' IndexInText='296' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<GenericTypeDataExpressionItems ObjectId='72' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem]'>
								<UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem Id='637795220125974735' IndexInText='283' ItemLength='11'/>
							</GenericTypeDataExpressionItems>
							<ErrorsPositionDisplayValue value='283' type='System.Int32' />
						</OtherProperties>
					</Custom>
					<CodeBlock Id='637795220125974855' IndexInText='306' ItemLength='110' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
						<RegularItems>
							<CodeBlockStartMarker Name='{' Id='637795220125974849' IndexInText='306' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637795220125974899' IndexInText='415' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<OtherProperties>
							<CodeBlockStartMarker Name='{' Id='637795220125974849' IndexInText='306' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637795220125974899' IndexInText='415' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OtherProperties>
					</CodeBlock>
				</Postfixes>
				<Children>
					<BinaryOperator Name=':' Priority='0' Id='637795220125973216' IndexInText='22' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
					<BinaryOperator Name=':' Priority='0' Id='637795220125973705' IndexInText='28' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
					<BinaryOperator Name=':' Priority='0' Id='637795220125974063' IndexInText='34' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
				</Children>
				<OtherProperties>
					<NamedExpressionItem Id='637795220125969762' IndexInText='19' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					<OpeningBraceInfo Name='(' Id='637795220125970351' IndexInText='21' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ClosingBraceInfo Name=')' Id='637795220125974080' IndexInText='39' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Parameters ObjectId='76' Count='3' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name=':' Priority='0' Id='637795220125973216' IndexInText='22' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name=':' Priority='0' Id='637795220125973705' IndexInText='28' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name=':' Priority='0' Id='637795220125974063' IndexInText='34' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
					</Parameters>
				</OtherProperties>
			</Braces>
		</RegularItems>
		<Children>
			<Braces Id='637795220125970320' IndexInText='0' ItemLength='416' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
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
	<MainExpressionItem Id='637795220125968791' IndexInText='0' ItemLength='416' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'/>
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
	<ExpressionItemSeries Id='637795220125861485' IndexInText='500' ItemLength='224' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'>
		<RegularItems>
			<BinaryOperator Name=':' Priority='0' Id='637795220125865507' IndexInText='500' ItemLength='149' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
				<Operand1.Braces Id='637795220125861980' IndexInText='500' ItemLength='18' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
					<RegularItems>
						<Literal Id='637795220125861932' IndexInText='500' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='f2' Id='637795220125861923' IndexInText='500' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Literal>
						<OpeningRoundBrace Name='(' Id='637795220125861984' IndexInText='502' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<BinaryOperator Name=':' Priority='0' Id='637795220125862373' IndexInText='503' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
							<Operand1.Literal Id='637795220125862096' IndexInText='503' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='x1' Id='637795220125862090' IndexInText='503' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Operand1.Literal>
							<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637795220125862153' IndexInText='505' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
								<OperatorNameParts>
									<Name Name=':' Id='637795220125862141' IndexInText='505' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</OperatorNameParts>
							</OperatorInfo>
							<Operand2.Literal Id='637795220125862320' IndexInText='506' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='int' Id='637795220125862314' IndexInText='506' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Operand2.Literal>
						</BinaryOperator>
						<Comma Name=',' Id='637795220125862445' IndexInText='509' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<BinaryOperator Name=':' Priority='0' Id='637795220125862783' IndexInText='511' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
							<Operand1.Literal Id='637795220125862541' IndexInText='511' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='x2' Id='637795220125862536' IndexInText='511' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Operand1.Literal>
							<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637795220125862591' IndexInText='513' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
								<OperatorNameParts>
									<Name Name=':' Id='637795220125862585' IndexInText='513' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</OperatorNameParts>
							</OperatorInfo>
							<Operand2.Literal Id='637795220125862738' IndexInText='514' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='int' Id='637795220125862733' IndexInText='514' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Operand2.Literal>
						</BinaryOperator>
						<ClosingRoundBrace Name=')' Id='637795220125862802' IndexInText='517' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</RegularItems>
					<Children>
						<BinaryOperator Name=':' Priority='0' Id='637795220125862373' IndexInText='503' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
						<BinaryOperator Name=':' Priority='0' Id='637795220125862783' IndexInText='511' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
					</Children>
					<OtherProperties>
						<NamedExpressionItem Id='637795220125861932' IndexInText='500' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
						<OpeningBraceInfo Name='(' Id='637795220125861984' IndexInText='502' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<ClosingBraceInfo Name=')' Id='637795220125862802' IndexInText='517' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<Parameters ObjectId='23' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
							<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name=':' Priority='0' Id='637795220125862373' IndexInText='503' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
							<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name=':' Priority='0' Id='637795220125862783' IndexInText='511' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
						</Parameters>
					</OtherProperties>
				</Operand1.Braces>
				<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637795220125862860' IndexInText='519' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
					<OperatorNameParts>
						<Name Name=':' Id='637795220125862854' IndexInText='519' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</OperatorNameParts>
				</OperatorInfo>
				<Operand2.Literal Id='637795220125863019' IndexInText='521' ItemLength='128' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
					<NameExpressionItem Name='int' Id='637795220125863013' IndexInText='521' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Postfixes>
						<CodeBlock Id='637795220125863066' IndexInText='527' ItemLength='122' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
							<RegularItems>
								<CodeBlockStartMarker Name='{' Id='637795220125863057' IndexInText='527' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<BinaryOperator Name=':' Priority='0' Id='637795220125865153' IndexInText='531' ItemLength='97' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
									<Operand1.Braces Id='637795220125863194' IndexInText='531' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
										<RegularItems>
											<Literal Id='637795220125863162' IndexInText='531' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
												<NameExpressionItem Name='f3' Id='637795220125863158' IndexInText='531' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											</Literal>
											<OpeningRoundBrace Name='(' Id='637795220125863197' IndexInText='533' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<ClosingRoundBrace Name=')' Id='637795220125863227' IndexInText='534' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</RegularItems>
										<OtherProperties>
											<NamedExpressionItem Id='637795220125863162' IndexInText='531' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
											<OpeningBraceInfo Name='(' Id='637795220125863197' IndexInText='533' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<ClosingBraceInfo Name=')' Id='637795220125863227' IndexInText='534' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<Parameters ObjectId='36' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'/>
										</OtherProperties>
									</Operand1.Braces>
									<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637795220125863278' IndexInText='536' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
										<OperatorNameParts>
											<Name Name=':' Id='637795220125863272' IndexInText='536' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</OperatorNameParts>
									</OperatorInfo>
									<Operand2.Literal Id='637795220125863429' IndexInText='538' ItemLength='90' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='int' Id='637795220125863420' IndexInText='538' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										<Postfixes>
											<CodeBlock Id='637795220125863465' IndexInText='544' ItemLength='84' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
												<RegularItems>
													<CodeBlockStartMarker Name='{' Id='637795220125863461' IndexInText='544' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
													<BinaryOperator Name='=' Priority='2000' Id='637795220125864045' IndexInText='552' ItemLength='18' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
														<Operand1.Literal Id='637795220125863589' IndexInText='552' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
															<NameExpressionItem Name='result' Id='637795220125863584' IndexInText='556' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
															<AppliedKeywords>
																<Keyword Name='var' Id='637795220125863489' IndexInText='552' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
																	<LanguageKeywordInfo ObjectId='47' Id='637781064051574641' Keyword='var' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
																</Keyword>
															</AppliedKeywords>
														</Operand1.Literal>
														<OperatorInfo OperatorType='BinaryOperator' Name='=' Priority='2000' Id='637795220125863668' IndexInText='563' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
															<OperatorNameParts>
																<Name Name='=' Id='637795220125863662' IndexInText='563' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
															</OperatorNameParts>
														</OperatorInfo>
														<Operand2.BinaryOperator Name='+' Priority='30' Id='637795220125864060' IndexInText='565' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
															<Operand1.Literal Id='637795220125863791' IndexInText='565' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
																<NameExpressionItem Name='x1' Id='637795220125863787' IndexInText='565' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
															</Operand1.Literal>
															<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637795220125863858' IndexInText='567' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
																<OperatorNameParts>
																	<Name Name='+' Id='637795220125863852' IndexInText='567' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
																</OperatorNameParts>
															</OperatorInfo>
															<Operand2.Literal Id='637795220125864001' IndexInText='568' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
																<NameExpressionItem Name='x2' Id='637795220125863996' IndexInText='568' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
															</Operand2.Literal>
														</Operand2.BinaryOperator>
													</BinaryOperator>
													<ExpressionSeparator Name=';' Id='637795220125864093' IndexInText='570' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
													<Braces Id='637795220125864221' IndexInText='575' ItemLength='30' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
														<RegularItems>
															<Literal Id='637795220125864190' IndexInText='575' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
																<NameExpressionItem Name='println' Id='637795220125864184' IndexInText='575' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
															</Literal>
															<OpeningRoundBrace Name='(' Id='637795220125864224' IndexInText='582' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
															<BinaryOperator Name='+' Priority='30' Id='637795220125864796' IndexInText='583' ItemLength='21' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
																<Operand1.BinaryOperator Name='+' Priority='30' Id='637795220125864786' IndexInText='583' ItemLength='17' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
																	<Operand1.ConstantText Id='637795220125864331' IndexInText='583' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
																		<NameExpressionItem Name='"result=''"' Id='637795220125864326' IndexInText='583' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
																	</Operand1.ConstantText>
																	<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637795220125864402' IndexInText='593' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
																		<OperatorNameParts>
																			<Name Name='+' Id='637795220125864389' IndexInText='593' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
																		</OperatorNameParts>
																	</OperatorInfo>
																	<Operand2.Literal Id='637795220125864539' IndexInText='594' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
																		<NameExpressionItem Name='result' Id='637795220125864534' IndexInText='594' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
																	</Operand2.Literal>
																</Operand1.BinaryOperator>
																<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637795220125864599' IndexInText='600' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
																	<OperatorNameParts>
																		<Name Name='+' Id='637795220125864594' IndexInText='600' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
																	</OperatorNameParts>
																</OperatorInfo>
																<Operand2.ConstantText Id='637795220125864743' IndexInText='601' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
																	<NameExpressionItem Name='"''"' Id='637795220125864738' IndexInText='601' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
																</Operand2.ConstantText>
															</BinaryOperator>
															<ClosingRoundBrace Name=')' Id='637795220125864817' IndexInText='604' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
														</RegularItems>
														<Children>
															<BinaryOperator Name='+' Priority='30' Id='637795220125864796' IndexInText='583' ItemLength='21' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
														</Children>
														<OtherProperties>
															<NamedExpressionItem Id='637795220125864190' IndexInText='575' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
															<OpeningBraceInfo Name='(' Id='637795220125864224' IndexInText='582' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
															<ClosingBraceInfo Name=')' Id='637795220125864817' IndexInText='604' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
															<Parameters ObjectId='75' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
																<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name='+' Priority='30' Id='637795220125864796' IndexInText='583' ItemLength='21' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
															</Parameters>
														</OtherProperties>
													</Braces>
													<ExpressionSeparator Name=';' Id='637795220125864852' IndexInText='605' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
													<PrefixUnaryOperator Name='return' Priority='2147483647' Id='637795220125865091' IndexInText='610' ItemLength='13' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
														<OperatorInfo OperatorType='PrefixUnaryOperator' Name='return' Priority='2147483647' Id='637795220125864911' IndexInText='610' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
															<OperatorNameParts>
																<Name Name='return' Id='637795220125864899' IndexInText='610' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
															</OperatorNameParts>
														</OperatorInfo>
														<Operand1.Literal Id='637795220125865034' IndexInText='617' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
															<NameExpressionItem Name='result' Id='637795220125865030' IndexInText='617' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
														</Operand1.Literal>
													</PrefixUnaryOperator>
													<ExpressionSeparator Name=';' Id='637795220125865110' IndexInText='623' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
													<CodeBlockEndMarker Name='}' Id='637795220125865139' IndexInText='627' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												</RegularItems>
												<Children>
													<BinaryOperator Name='=' Priority='2000' Id='637795220125864045' IndexInText='552' ItemLength='18' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
													<Braces Id='637795220125864221' IndexInText='575' ItemLength='30' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
													<PrefixUnaryOperator Name='return' Priority='2147483647' Id='637795220125865091' IndexInText='610' ItemLength='13' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
												</Children>
												<OtherProperties>
													<CodeBlockStartMarker Name='{' Id='637795220125863461' IndexInText='544' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
													<CodeBlockEndMarker Name='}' Id='637795220125865139' IndexInText='627' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												</OtherProperties>
											</CodeBlock>
										</Postfixes>
									</Operand2.Literal>
								</BinaryOperator>
								<PrefixUnaryOperator Name='return' Priority='2147483647' Id='637795220125865448' IndexInText='634' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
									<OperatorInfo OperatorType='PrefixUnaryOperator' Name='return' Priority='2147483647' Id='637795220125865222' IndexInText='634' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
										<OperatorNameParts>
											<Name Name='return' Id='637795220125865211' IndexInText='634' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</OperatorNameParts>
									</OperatorInfo>
									<Operand1.Braces Id='637795220125865382' IndexInText='641' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
										<RegularItems>
											<Literal Id='637795220125865337' IndexInText='641' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
												<NameExpressionItem Name='f3' Id='637795220125865332' IndexInText='641' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											</Literal>
											<OpeningRoundBrace Name='(' Id='637795220125865387' IndexInText='643' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<ClosingRoundBrace Name=')' Id='637795220125865420' IndexInText='644' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</RegularItems>
										<OtherProperties>
											<NamedExpressionItem Id='637795220125865337' IndexInText='641' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
											<OpeningBraceInfo Name='(' Id='637795220125865387' IndexInText='643' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<ClosingBraceInfo Name=')' Id='637795220125865420' IndexInText='644' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<Parameters ObjectId='92' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'/>
										</OtherProperties>
									</Operand1.Braces>
								</PrefixUnaryOperator>
								<ExpressionSeparator Name=';' Id='637795220125865464' IndexInText='645' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<CodeBlockEndMarker Name='}' Id='637795220125865497' IndexInText='648' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</RegularItems>
							<Children>
								<BinaryOperator Name=':' Priority='0' Id='637795220125865153' IndexInText='531' ItemLength='97' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
								<PrefixUnaryOperator Name='return' Priority='2147483647' Id='637795220125865448' IndexInText='634' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
							</Children>
							<OtherProperties>
								<CodeBlockStartMarker Name='{' Id='637795220125863057' IndexInText='527' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<CodeBlockEndMarker Name='}' Id='637795220125865497' IndexInText='648' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OtherProperties>
						</CodeBlock>
					</Postfixes>
				</Operand2.Literal>
			</BinaryOperator>
			<BinaryOperator Name='=' Priority='2000' Id='637795220125867825' IndexInText='653' ItemLength='71' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
				<Operand1.Literal Id='637795220125865646' IndexInText='653' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
					<NameExpressionItem Name='myFunc' Id='637795220125865640' IndexInText='657' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<AppliedKeywords>
						<Keyword Name='var' Id='637795220125865536' IndexInText='653' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
							<LanguageKeywordInfo ObjectId='47' Id='637781064051574641' Keyword='var' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
						</Keyword>
					</AppliedKeywords>
				</Operand1.Literal>
				<OperatorInfo OperatorType='BinaryOperator' Name='=' Priority='2000' Id='637795220125865733' IndexInText='664' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
					<OperatorNameParts>
						<Name Name='=' Id='637795220125865725' IndexInText='664' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</OperatorNameParts>
				</OperatorInfo>
				<Operand2.BinaryOperator Name='=>' Priority='1000' Id='637795220125867839' IndexInText='666' ItemLength='58' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
					<Operand1.Braces Id='637795220125865898' IndexInText='666' ItemLength='18' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<Literal Id='637795220125865855' IndexInText='666' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='f2' Id='637795220125865851' IndexInText='666' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<OpeningRoundBrace Name='(' Id='637795220125865903' IndexInText='668' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<BinaryOperator Name=':' Priority='0' Id='637795220125866264' IndexInText='669' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
								<Operand1.Literal Id='637795220125865993' IndexInText='669' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
									<NameExpressionItem Name='x1' Id='637795220125865988' IndexInText='669' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</Operand1.Literal>
								<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637795220125866054' IndexInText='671' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
									<OperatorNameParts>
										<Name Name=':' Id='637795220125866047' IndexInText='671' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</OperatorNameParts>
								</OperatorInfo>
								<Operand2.Literal Id='637795220125866222' IndexInText='672' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
									<NameExpressionItem Name='int' Id='637795220125866217' IndexInText='672' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</Operand2.Literal>
							</BinaryOperator>
							<Comma Name=',' Id='637795220125866288' IndexInText='675' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<BinaryOperator Name=':' Priority='0' Id='637795220125866659' IndexInText='677' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
								<Operand1.Literal Id='637795220125866388' IndexInText='677' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
									<NameExpressionItem Name='x2' Id='637795220125866382' IndexInText='677' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</Operand1.Literal>
								<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637795220125866445' IndexInText='679' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
									<OperatorNameParts>
										<Name Name=':' Id='637795220125866437' IndexInText='679' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</OperatorNameParts>
								</OperatorInfo>
								<Operand2.Literal Id='637795220125866615' IndexInText='680' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
									<NameExpressionItem Name='int' Id='637795220125866602' IndexInText='680' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</Operand2.Literal>
							</BinaryOperator>
							<ClosingRoundBrace Name=')' Id='637795220125866678' IndexInText='683' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<BinaryOperator Name=':' Priority='0' Id='637795220125866264' IndexInText='669' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
							<BinaryOperator Name=':' Priority='0' Id='637795220125866659' IndexInText='677' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
						</Children>
						<OtherProperties>
							<NamedExpressionItem Id='637795220125865855' IndexInText='666' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							<OpeningBraceInfo Name='(' Id='637795220125865903' IndexInText='668' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=')' Id='637795220125866678' IndexInText='683' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Parameters ObjectId='122' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name=':' Priority='0' Id='637795220125866264' IndexInText='669' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name=':' Priority='0' Id='637795220125866659' IndexInText='677' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Operand1.Braces>
					<OperatorInfo OperatorType='BinaryOperator' Name='=>' Priority='1000' Id='637795220125866768' IndexInText='685' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
						<OperatorNameParts>
							<Name Name='=>' Id='637795220125866755' IndexInText='685' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OperatorNameParts>
					</OperatorInfo>
					<Operand2.CodeBlock Id='637795220125866895' IndexInText='689' ItemLength='35' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
						<RegularItems>
							<CodeBlockStartMarker Name='{' Id='637795220125866885' IndexInText='689' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Braces Id='637795220125867038' IndexInText='696' ItemLength='24' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
								<RegularItems>
									<Literal Id='637795220125866999' IndexInText='696' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='println' Id='637795220125866992' IndexInText='696' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<OpeningRoundBrace Name='(' Id='637795220125867042' IndexInText='703' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<BinaryOperator Name='^' Priority='10' Id='637795220125867745' IndexInText='704' ItemLength='15' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
										<Operand1.Literal Id='637795220125867133' IndexInText='704' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
											<NameExpressionItem Name='exp' Id='637795220125867128' IndexInText='704' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</Operand1.Literal>
										<OperatorInfo OperatorType='BinaryOperator' Name='^' Priority='10' Id='637795220125867192' IndexInText='708' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
											<OperatorNameParts>
												<Name Name='^' Id='637795220125867184' IndexInText='708' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											</OperatorNameParts>
										</OperatorInfo>
										<Operand2.Braces Id='637795220125867335' IndexInText='710' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
											<RegularItems>
												<OpeningRoundBrace Name='(' Id='637795220125867339' IndexInText='710' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												<BinaryOperator Name='+' Priority='30' Id='637795220125867685' IndexInText='711' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
													<Operand1.Literal Id='637795220125867437' IndexInText='711' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
														<NameExpressionItem Name='x1' Id='637795220125867427' IndexInText='711' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
													</Operand1.Literal>
													<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637795220125867498' IndexInText='714' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
														<OperatorNameParts>
															<Name Name='+' Id='637795220125867492' IndexInText='714' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
														</OperatorNameParts>
													</OperatorInfo>
													<Operand2.Literal Id='637795220125867638' IndexInText='716' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
														<NameExpressionItem Name='x2' Id='637795220125867633' IndexInText='716' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
													</Operand2.Literal>
												</BinaryOperator>
												<ClosingRoundBrace Name=')' Id='637795220125867712' IndexInText='718' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											</RegularItems>
											<Children>
												<BinaryOperator Name='+' Priority='30' Id='637795220125867685' IndexInText='711' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
											</Children>
											<OtherProperties>
												<OpeningBraceInfo Name='(' Id='637795220125867339' IndexInText='710' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												<ClosingBraceInfo Name=')' Id='637795220125867712' IndexInText='718' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
												<Parameters ObjectId='146' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
													<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name='+' Priority='30' Id='637795220125867685' IndexInText='711' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
												</Parameters>
											</OtherProperties>
										</Operand2.Braces>
									</BinaryOperator>
									<ClosingRoundBrace Name=')' Id='637795220125867759' IndexInText='719' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Children>
									<BinaryOperator Name='^' Priority='10' Id='637795220125867745' IndexInText='704' ItemLength='15' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
								</Children>
								<OtherProperties>
									<NamedExpressionItem Id='637795220125866999' IndexInText='696' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<OpeningBraceInfo Name='(' Id='637795220125867042' IndexInText='703' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ClosingBraceInfo Name=')' Id='637795220125867759' IndexInText='719' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Parameters ObjectId='148' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name='^' Priority='10' Id='637795220125867745' IndexInText='704' ItemLength='15' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
									</Parameters>
								</OtherProperties>
							</Braces>
							<ExpressionSeparator Name=';' Id='637795220125867788' IndexInText='720' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637795220125867815' IndexInText='723' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<Braces Id='637795220125867038' IndexInText='696' ItemLength='24' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
						</Children>
						<OtherProperties>
							<CodeBlockStartMarker Name='{' Id='637795220125866885' IndexInText='689' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637795220125867815' IndexInText='723' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OtherProperties>
					</Operand2.CodeBlock>
				</Operand2.BinaryOperator>
			</BinaryOperator>
		</RegularItems>
		<Children>
			<BinaryOperator Name=':' Priority='0' Id='637795220125865507' IndexInText='500' ItemLength='149' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
			<BinaryOperator Name='=' Priority='2000' Id='637795220125867825' IndexInText='653' ItemLength='71' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
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
	<MainExpressionItem Id='637795220125861485' IndexInText='500' ItemLength='224' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'/>
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
	<ExpressionItemSeries Id='637795220125915203' IndexInText='539' ItemLength='106' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'>
		<RegularItems>
			<BinaryOperator Name=':' Priority='0' Id='637795220125917084' IndexInText='539' ItemLength='106' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
				<Operand1.Literal Id='637795220125915597' IndexInText='539' ItemLength='16' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
					<NameExpressionItem Name='Dog' Id='637795220125915591' IndexInText='552' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<AppliedKeywords>
						<Keyword Name='public' Id='637795220125915426' IndexInText='539' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
							<LanguageKeywordInfo ObjectId='6' Id='637783669980406419' Keyword='public' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
						</Keyword>
						<Keyword Name='class' Id='637795220125915442' IndexInText='546' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
							<LanguageKeywordInfo ObjectId='8' Id='637783670388745402' Keyword='class' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
						</Keyword>
					</AppliedKeywords>
				</Operand1.Literal>
				<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637795220125915681' IndexInText='556' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
					<OperatorNameParts>
						<Name Name=':' Id='637795220125915668' IndexInText='556' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</OperatorNameParts>
				</OperatorInfo>
				<Operand2.Braces Id='637795220125915867' IndexInText='558' ItemLength='87' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
					<RegularItems>
						<OpeningRoundBrace Name='(' Id='637795220125915873' IndexInText='558' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<Literal Id='637795220125915988' IndexInText='559' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='Anymal' Id='637795220125915983' IndexInText='559' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Literal>
						<Comma Name=',' Id='637795220125916033' IndexInText='565' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<Literal Id='637795220125916119' IndexInText='567' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='IDog' Id='637795220125916115' IndexInText='567' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Literal>
						<ClosingRoundBrace Name=')' Id='637795220125916153' IndexInText='571' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</RegularItems>
					<Postfixes>
						<CodeBlock Id='637795220125916199' IndexInText='574' ItemLength='71' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
							<RegularItems>
								<CodeBlockStartMarker Name='{' Id='637795220125916194' IndexInText='574' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<BinaryOperator Name=':' Priority='0' Id='637795220125916998' IndexInText='581' ItemLength='61' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
									<Operand1.Braces Id='637795220125916361' IndexInText='581' ItemLength='13' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
										<AppliedKeywords>
											<Keyword Name='public' Id='637795220125916225' IndexInText='581' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
												<LanguageKeywordInfo ObjectId='6' Id='637783669980406419' Keyword='public' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
											</Keyword>
										</AppliedKeywords>
										<RegularItems>
											<Literal Id='637795220125916320' IndexInText='588' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
												<NameExpressionItem Name='Bark' Id='637795220125916316' IndexInText='588' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											</Literal>
											<OpeningRoundBrace Name='(' Id='637795220125916365' IndexInText='592' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<ClosingRoundBrace Name=')' Id='637795220125916397' IndexInText='593' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</RegularItems>
										<OtherProperties>
											<NamedExpressionItem Id='637795220125916320' IndexInText='588' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
											<OpeningBraceInfo Name='(' Id='637795220125916365' IndexInText='592' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<ClosingBraceInfo Name=')' Id='637795220125916397' IndexInText='593' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											<Parameters ObjectId='28' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'/>
										</OtherProperties>
									</Operand1.Braces>
									<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637795220125916453' IndexInText='595' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
										<OperatorNameParts>
											<Name Name=':' Id='637795220125916447' IndexInText='595' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</OperatorNameParts>
									</OperatorInfo>
									<Operand2.Literal Id='637795220125916609' IndexInText='597' ItemLength='45' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='void' Id='637795220125916603' IndexInText='597' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										<Postfixes>
											<CodeBlock Id='637795220125916648' IndexInText='607' ItemLength='35' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
												<RegularItems>
													<CodeBlockStartMarker Name='{' Id='637795220125916643' IndexInText='607' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
													<Braces Id='637795220125916773' IndexInText='618' ItemLength='16' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
														<RegularItems>
															<Literal Id='637795220125916744' IndexInText='618' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
																<NameExpressionItem Name='println' Id='637795220125916739' IndexInText='618' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
															</Literal>
															<OpeningRoundBrace Name='(' Id='637795220125916777' IndexInText='625' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
															<ConstantText Id='637795220125916880' IndexInText='626' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
																<NameExpressionItem Name='"Bark."' Id='637795220125916875' IndexInText='626' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
															</ConstantText>
															<ClosingRoundBrace Name=')' Id='637795220125916912' IndexInText='633' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
														</RegularItems>
														<Children>
															<ConstantText Id='637795220125916880' IndexInText='626' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
														</Children>
														<OtherProperties>
															<NamedExpressionItem Id='637795220125916744' IndexInText='618' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
															<OpeningBraceInfo Name='(' Id='637795220125916777' IndexInText='625' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
															<ClosingBraceInfo Name=')' Id='637795220125916912' IndexInText='633' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
															<Parameters ObjectId='42' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
																<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220125916880' IndexInText='626' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
															</Parameters>
														</OtherProperties>
													</Braces>
													<ExpressionSeparator Name=';' Id='637795220125916944' IndexInText='634' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
													<CodeBlockEndMarker Name='}' Id='637795220125916978' IndexInText='641' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												</RegularItems>
												<Children>
													<Braces Id='637795220125916773' IndexInText='618' ItemLength='16' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
												</Children>
												<OtherProperties>
													<CodeBlockStartMarker Name='{' Id='637795220125916643' IndexInText='607' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
													<CodeBlockEndMarker Name='}' Id='637795220125916978' IndexInText='641' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												</OtherProperties>
											</CodeBlock>
										</Postfixes>
									</Operand2.Literal>
								</BinaryOperator>
								<CodeBlockEndMarker Name='}' Id='637795220125917072' IndexInText='644' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</RegularItems>
							<Children>
								<BinaryOperator Name=':' Priority='0' Id='637795220125916998' IndexInText='581' ItemLength='61' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
							</Children>
							<OtherProperties>
								<CodeBlockStartMarker Name='{' Id='637795220125916194' IndexInText='574' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<CodeBlockEndMarker Name='}' Id='637795220125917072' IndexInText='644' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OtherProperties>
						</CodeBlock>
					</Postfixes>
					<Children>
						<Literal Id='637795220125915988' IndexInText='559' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
						<Literal Id='637795220125916119' IndexInText='567' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					</Children>
					<OtherProperties>
						<OpeningBraceInfo Name='(' Id='637795220125915873' IndexInText='558' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<ClosingBraceInfo Name=')' Id='637795220125916153' IndexInText='571' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
						<Parameters ObjectId='46' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
							<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220125915988' IndexInText='559' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220125916119' IndexInText='567' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
						</Parameters>
					</OtherProperties>
				</Operand2.Braces>
			</BinaryOperator>
		</RegularItems>
		<Children>
			<BinaryOperator Name=':' Priority='0' Id='637795220125917084' IndexInText='539' ItemLength='106' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
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
	<MainExpressionItem Id='637795220125915203' IndexInText='539' ItemLength='106' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'/>
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
	<ExpressionItemSeries Id='637795220125415562' IndexInText='0' ItemLength='624' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'>
		<RegularItems>
			<Braces Id='637795220125415891' IndexInText='0' ItemLength='106' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
				<RegularItems>
					<Literal Id='637795220125415844' IndexInText='0' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='f1' Id='637795220125415837' IndexInText='0' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<OpeningRoundBrace Name='(' Id='637795220125415895' IndexInText='2' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Literal Id='637795220125416000' IndexInText='3' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='x1' Id='637795220125415995' IndexInText='3' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<ClosingRoundBrace Name=')' Id='637795220125416042' IndexInText='5' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Postfixes>
					<CodeBlock Id='637795220125416079' IndexInText='9' ItemLength='97' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
						<RegularItems>
							<CodeBlockStartMarker Name='{' Id='637795220125416075' IndexInText='9' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<PrefixUnaryOperator Name='return' Priority='2147483647' Id='637795220125416559' IndexInText='90' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
								<OperatorInfo OperatorType='PrefixUnaryOperator' Name='return' Priority='2147483647' Id='637795220125416150' IndexInText='90' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
									<OperatorNameParts>
										<Name Name='return' Id='637795220125416140' IndexInText='90' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</OperatorNameParts>
								</OperatorInfo>
								<Operand1.BinaryOperator Name='*' Priority='20' Id='637795220125416578' IndexInText='97' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
									<Operand1.Literal Id='637795220125416283' IndexInText='97' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='x2' Id='637795220125416278' IndexInText='97' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Operand1.Literal>
									<OperatorInfo OperatorType='BinaryOperator' Name='*' Priority='20' Id='637795220125416362' IndexInText='99' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
										<OperatorNameParts>
											<Name Name='*' Id='637795220125416356' IndexInText='99' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</OperatorNameParts>
									</OperatorInfo>
									<Operand2.Literal Id='637795220125416510' IndexInText='100' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='y1' Id='637795220125416505' IndexInText='100' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Operand2.Literal>
								</Operand1.BinaryOperator>
							</PrefixUnaryOperator>
							<ExpressionSeparator Name=';' Id='637795220125416634' IndexInText='102' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637795220125416670' IndexInText='105' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<PrefixUnaryOperator Name='return' Priority='2147483647' Id='637795220125416559' IndexInText='90' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
						</Children>
						<OtherProperties>
							<CodeBlockStartMarker Name='{' Id='637795220125416075' IndexInText='9' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637795220125416670' IndexInText='105' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OtherProperties>
					</CodeBlock>
				</Postfixes>
				<Children>
					<Literal Id='637795220125416000' IndexInText='3' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
				</Children>
				<OtherProperties>
					<NamedExpressionItem Id='637795220125415844' IndexInText='0' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					<OpeningBraceInfo Name='(' Id='637795220125415895' IndexInText='2' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ClosingBraceInfo Name=')' Id='637795220125416042' IndexInText='5' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Parameters ObjectId='23' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220125416000' IndexInText='3' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					</Parameters>
				</OtherProperties>
			</Braces>
			<Braces Id='637795220125416796' IndexInText='110' ItemLength='97' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
				<RegularItems>
					<Literal Id='637795220125416763' IndexInText='110' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='m1' Id='637795220125416758' IndexInText='110' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<OpeningSquareBrace Name='[' Id='637795220125416800' IndexInText='112' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Literal Id='637795220125416887' IndexInText='113' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='x2' Id='637795220125416882' IndexInText='113' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<ClosingSquareBrace Name=']' Id='637795220125416917' IndexInText='115' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Postfixes>
					<CodeBlock Id='637795220125416954' IndexInText='118' ItemLength='89' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
						<RegularItems>
							<CodeBlockStartMarker Name='{' Id='637795220125416950' IndexInText='118' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<BinaryOperator Name='*' Priority='20' Id='637795220125578626' IndexInText='199' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
								<Operand1.BinaryOperator Name=':' Priority='0' Id='637795220125578602' IndexInText='199' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
									<Operand1.Literal Id='637795220125417057' IndexInText='199' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='x' Id='637795220125417052' IndexInText='199' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Operand1.Literal>
									<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637795220125417111' IndexInText='200' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
										<OperatorNameParts>
											<Name Name=':' Id='637795220125417105' IndexInText='200' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</OperatorNameParts>
									</OperatorInfo>
									<Operand2.ConstantNumericValue Id='637795220125497282' IndexInText='201' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
										<NameExpressionItem Name='2' Id='637795220125497220' IndexInText='201' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										<OtherProperties>
											<SucceededNumericTypeDescriptor ObjectId='41' NumberTypeId='1581134183960' Interface='UniversalExpressionParser.NumericTypeDescriptor'>
												<RegularExpressions ObjectId='42' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[System.String]'>
													<System.String value='^\d+' />
												</RegularExpressions>
											</SucceededNumericTypeDescriptor>
										</OtherProperties>
									</Operand2.ConstantNumericValue>
								</Operand1.BinaryOperator>
								<OperatorInfo OperatorType='BinaryOperator' Name='*' Priority='20' Id='637795220125497553' IndexInText='202' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
									<OperatorNameParts>
										<Name Name='*' Id='637795220125497540' IndexInText='202' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</OperatorNameParts>
								</OperatorInfo>
								<Operand2.ConstantNumericValue Id='637795220125578401' IndexInText='203' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
									<NameExpressionItem Name='3' Id='637795220125578350' IndexInText='203' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<OtherProperties>
										<SucceededNumericTypeDescriptor ObjectId='41' NumberTypeId='1581134183960' Interface='UniversalExpressionParser.NumericTypeDescriptor'/>
									</OtherProperties>
								</Operand2.ConstantNumericValue>
							</BinaryOperator>
							<CodeBlockEndMarker Name='}' Id='637795220125578688' IndexInText='206' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<BinaryOperator Name='*' Priority='20' Id='637795220125578626' IndexInText='199' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
						</Children>
						<OtherProperties>
							<CodeBlockStartMarker Name='{' Id='637795220125416950' IndexInText='118' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637795220125578688' IndexInText='206' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OtherProperties>
					</CodeBlock>
				</Postfixes>
				<Children>
					<Literal Id='637795220125416887' IndexInText='113' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
				</Children>
				<OtherProperties>
					<NamedExpressionItem Id='637795220125416763' IndexInText='110' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					<OpeningBraceInfo Name='[' Id='637795220125416800' IndexInText='112' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ClosingBraceInfo Name=']' Id='637795220125416917' IndexInText='115' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Parameters ObjectId='48' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220125416887' IndexInText='113' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					</Parameters>
				</OtherProperties>
			</Braces>
			<Braces Id='637795220125578752' IndexInText='211' ItemLength='100' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
				<RegularItems>
					<OpeningRoundBrace Name='(' Id='637795220125578757' IndexInText='211' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Literal Id='637795220125578899' IndexInText='212' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='x3' Id='637795220125578894' IndexInText='212' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<ClosingRoundBrace Name=')' Id='637795220125578946' IndexInText='214' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Postfixes>
					<CodeBlock Id='637795220125578983' IndexInText='217' ItemLength='94' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
						<RegularItems>
							<CodeBlockStartMarker Name='{' Id='637795220125578978' IndexInText='217' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<PrefixUnaryOperator Name='return' Priority='2147483647' Id='637795220125658079' IndexInText='296' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
								<OperatorInfo OperatorType='PrefixUnaryOperator' Name='return' Priority='2147483647' Id='637795220125579062' IndexInText='296' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
									<OperatorNameParts>
										<Name Name='return' Id='637795220125579049' IndexInText='296' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</OperatorNameParts>
								</OperatorInfo>
								<Operand1.BinaryOperator Name='*' Priority='20' Id='637795220125658103' IndexInText='303' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
									<Operand1.Literal Id='637795220125579189' IndexInText='303' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='x3' Id='637795220125579184' IndexInText='303' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Operand1.Literal>
									<OperatorInfo OperatorType='BinaryOperator' Name='*' Priority='20' Id='637795220125579264' IndexInText='305' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
										<OperatorNameParts>
											<Name Name='*' Id='637795220125579258' IndexInText='305' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</OperatorNameParts>
									</OperatorInfo>
									<Operand2.ConstantNumericValue Id='637795220125657910' IndexInText='306' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
										<NameExpressionItem Name='2' Id='637795220125657863' IndexInText='306' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										<OtherProperties>
											<SucceededNumericTypeDescriptor ObjectId='41' NumberTypeId='1581134183960' Interface='UniversalExpressionParser.NumericTypeDescriptor'/>
										</OtherProperties>
									</Operand2.ConstantNumericValue>
								</Operand1.BinaryOperator>
							</PrefixUnaryOperator>
							<ExpressionSeparator Name=';' Id='637795220125658159' IndexInText='307' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637795220125658209' IndexInText='310' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<PrefixUnaryOperator Name='return' Priority='2147483647' Id='637795220125658079' IndexInText='296' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
						</Children>
						<OtherProperties>
							<CodeBlockStartMarker Name='{' Id='637795220125578978' IndexInText='217' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637795220125658209' IndexInText='310' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OtherProperties>
					</CodeBlock>
				</Postfixes>
				<Children>
					<Literal Id='637795220125578899' IndexInText='212' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
				</Children>
				<OtherProperties>
					<OpeningBraceInfo Name='(' Id='637795220125578757' IndexInText='211' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ClosingBraceInfo Name=')' Id='637795220125578946' IndexInText='214' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
					<Parameters ObjectId='68' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220125578899' IndexInText='212' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					</Parameters>
				</OtherProperties>
			</Braces>
			<Braces Id='637795220125658253' IndexInText='315' ItemLength='94' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
				<RegularItems>
					<OpeningSquareBrace Name='[' Id='637795220125658257' IndexInText='315' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Literal Id='637795220125658395' IndexInText='316' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='x4' Id='637795220125658390' IndexInText='316' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<ClosingSquareBrace Name=']' Id='637795220125658438' IndexInText='318' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Postfixes>
					<CodeBlock Id='637795220125658475' IndexInText='321' ItemLength='88' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
						<RegularItems>
							<CodeBlockStartMarker Name='{' Id='637795220125658469' IndexInText='321' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<BinaryOperator Name='*' Priority='20' Id='637795220125817941' IndexInText='400' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
								<Operand1.BinaryOperator Name=':' Priority='0' Id='637795220125817914' IndexInText='400' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
									<Operand1.Literal Id='637795220125658593' IndexInText='400' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='x4' Id='637795220125658588' IndexInText='400' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Operand1.Literal>
									<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637795220125658647' IndexInText='402' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
										<OperatorNameParts>
											<Name Name=':' Id='637795220125658637' IndexInText='402' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</OperatorNameParts>
									</OperatorInfo>
									<Operand2.ConstantNumericValue Id='637795220125737127' IndexInText='403' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
										<NameExpressionItem Name='2' Id='637795220125737072' IndexInText='403' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										<OtherProperties>
											<SucceededNumericTypeDescriptor ObjectId='41' NumberTypeId='1581134183960' Interface='UniversalExpressionParser.NumericTypeDescriptor'/>
										</OtherProperties>
									</Operand2.ConstantNumericValue>
								</Operand1.BinaryOperator>
								<OperatorInfo OperatorType='BinaryOperator' Name='*' Priority='20' Id='637795220125737361' IndexInText='404' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
									<OperatorNameParts>
										<Name Name='*' Id='637795220125737349' IndexInText='404' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</OperatorNameParts>
								</OperatorInfo>
								<Operand2.ConstantNumericValue Id='637795220125817656' IndexInText='405' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
									<NameExpressionItem Name='3' Id='637795220125817594' IndexInText='405' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<OtherProperties>
										<SucceededNumericTypeDescriptor ObjectId='41' NumberTypeId='1581134183960' Interface='UniversalExpressionParser.NumericTypeDescriptor'/>
									</OtherProperties>
								</Operand2.ConstantNumericValue>
							</BinaryOperator>
							<CodeBlockEndMarker Name='}' Id='637795220125818011' IndexInText='408' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<BinaryOperator Name='*' Priority='20' Id='637795220125817941' IndexInText='400' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
						</Children>
						<OtherProperties>
							<CodeBlockStartMarker Name='{' Id='637795220125658469' IndexInText='321' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637795220125818011' IndexInText='408' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OtherProperties>
					</CodeBlock>
				</Postfixes>
				<Children>
					<Literal Id='637795220125658395' IndexInText='316' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
				</Children>
				<OtherProperties>
					<OpeningBraceInfo Name='[' Id='637795220125658257' IndexInText='315' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ClosingBraceInfo Name=']' Id='637795220125658438' IndexInText='318' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
					<Parameters ObjectId='89' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220125658395' IndexInText='316' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					</Parameters>
				</OtherProperties>
			</Braces>
			<Literal Id='637795220125818241' IndexInText='413' ItemLength='87' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
				<NameExpressionItem Name='Dog' Id='637795220125818231' IndexInText='419' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				<AppliedKeywords>
					<Keyword Name='class' Id='637795220125818056' IndexInText='413' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
						<LanguageKeywordInfo ObjectId='93' Id='637783670388745402' Keyword='class' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
					</Keyword>
				</AppliedKeywords>
				<Postfixes>
					<CodeBlock Id='637795220125818283' IndexInText='424' ItemLength='76' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
						<RegularItems>
							<CodeBlockStartMarker Name='{' Id='637795220125818278' IndexInText='424' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637795220125818348' IndexInText='499' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<OtherProperties>
							<CodeBlockStartMarker Name='{' Id='637795220125818278' IndexInText='424' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637795220125818348' IndexInText='499' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OtherProperties>
					</CodeBlock>
				</Postfixes>
			</Literal>
			<Custom Id='637795220125818460' IndexInText='504' ItemLength='120' Interface='UniversalExpressionParser.ExpressionItems.Custom.IKeywordBasedCustomExpressionItem'>
				<RegularItems>
					<Keyword Name='::pragma' Id='637795220125818368' IndexInText='504' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
						<LanguageKeywordInfo ObjectId='99' Id='637781063127646487' Keyword='::pragma' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
					</Keyword>
					<Name Name='x' Id='637795220125818454' IndexInText='513' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Postfixes>
					<CodeBlock Id='637795220125818514' IndexInText='516' ItemLength='108' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
						<RegularItems>
							<CodeBlockStartMarker Name='{' Id='637795220125818509' IndexInText='516' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637795220125820392' IndexInText='623' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<OtherProperties>
							<CodeBlockStartMarker Name='{' Id='637795220125818509' IndexInText='516' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637795220125820392' IndexInText='623' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OtherProperties>
					</CodeBlock>
				</Postfixes>
				<OtherProperties>
					<LastKeywordExpressionItem Name='::pragma' Id='637795220125818368' IndexInText='504' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'/>
					<ErrorsPositionDisplayValue value='504' type='System.Int32' />
				</OtherProperties>
			</Custom>
		</RegularItems>
		<Children>
			<Braces Id='637795220125415891' IndexInText='0' ItemLength='106' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
			<Braces Id='637795220125416796' IndexInText='110' ItemLength='97' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
			<Braces Id='637795220125578752' IndexInText='211' ItemLength='100' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
			<Braces Id='637795220125658253' IndexInText='315' ItemLength='94' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
			<Literal Id='637795220125818241' IndexInText='413' ItemLength='87' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
			<Custom Id='637795220125818460' IndexInText='504' ItemLength='120' Interface='UniversalExpressionParser.ExpressionItems.Custom.IKeywordBasedCustomExpressionItem'/>
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
	<MainExpressionItem Id='637795220125415562' IndexInText='0' ItemLength='624' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'/>
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
	<ExpressionItemSeries Id='637795220123085743' IndexInText='0' ItemLength='185' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'>
		<RegularItems>
			<BinaryOperator Name='+' Priority='30' Id='637795220123086331' IndexInText='0' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
				<Operand1.Custom Id='637795220123085991' IndexInText='0' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.Custom.IKeywordBasedCustomExpressionItem'>
					<RegularItems>
						<Keyword Name='::pragma' Id='637795220123085898' IndexInText='0' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
							<LanguageKeywordInfo ObjectId='5' Id='637781063127646487' Keyword='::pragma' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
						</Keyword>
						<Name Name='x' Id='637795220123085985' IndexInText='9' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</RegularItems>
					<OtherProperties>
						<LastKeywordExpressionItem Name='::pragma' Id='637795220123085898' IndexInText='0' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'/>
						<ErrorsPositionDisplayValue value='0' type='System.Int32' />
					</OtherProperties>
				</Operand1.Custom>
				<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637795220123086116' IndexInText='10' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
					<OperatorNameParts>
						<Name Name='+' Id='637795220123086107' IndexInText='10' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</OperatorNameParts>
				</OperatorInfo>
				<Operand2.Literal Id='637795220123086274' IndexInText='11' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
					<NameExpressionItem Name='y' Id='637795220123086268' IndexInText='11' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</Operand2.Literal>
			</BinaryOperator>
			<ExpressionSeparator Name=';' Id='637795220123086383' IndexInText='12' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<Braces Id='637795220123087483' IndexInText='15' ItemLength='170' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
				<Prefixes>
					<Custom Id='637795220123087074' IndexInText='15' ItemLength='14' Interface='UniversalExpressionParser.ExpressionItems.Custom.IKeywordBasedCustomExpressionItem'>
						<RegularItems>
							<Keyword Name='::types' Id='637795220123086401' IndexInText='15' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
								<LanguageKeywordInfo ObjectId='15' Id='637781062811583137' Keyword='::types' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
							</Keyword>
							<Braces Id='637795220123086578' IndexInText='22' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
								<RegularItems>
									<OpeningSquareBrace Name='[' Id='637795220123086585' IndexInText='22' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637795220123086879' IndexInText='23' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='T1' Id='637795220123086856' IndexInText='23' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<Comma Name=',' Id='637795220123086933' IndexInText='25' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637795220123087028' IndexInText='26' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='T2' Id='637795220123087023' IndexInText='26' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<ClosingSquareBrace Name=']' Id='637795220123087061' IndexInText='28' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Children>
									<Literal Id='637795220123086879' IndexInText='23' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<Literal Id='637795220123087028' IndexInText='26' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								</Children>
								<OtherProperties>
									<OpeningBraceInfo Name='[' Id='637795220123086585' IndexInText='22' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ClosingBraceInfo Name=']' Id='637795220123087061' IndexInText='28' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
									<Parameters ObjectId='24' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220123086879' IndexInText='23' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220123087028' IndexInText='26' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</Parameters>
								</OtherProperties>
							</Braces>
						</RegularItems>
						<OtherProperties>
							<LastKeywordExpressionItem Name='::types' Id='637795220123086401' IndexInText='15' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'/>
							<ErrorsPositionDisplayValue value='15' type='System.Int32' />
						</OtherProperties>
					</Custom>
				</Prefixes>
				<RegularItems>
					<Literal Id='637795220123087198' IndexInText='30' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='F1' Id='637795220123087193' IndexInText='30' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<OpeningRoundBrace Name='(' Id='637795220123087491' IndexInText='32' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<BinaryOperator Name=':' Priority='0' Id='637795220123087852' IndexInText='33' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.Literal Id='637795220123087593' IndexInText='33' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='x' Id='637795220123087588' IndexInText='33' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand1.Literal>
						<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637795220123087649' IndexInText='34' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<Name Name=':' Id='637795220123087640' IndexInText='34' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OperatorNameParts>
						</OperatorInfo>
						<Operand2.Literal Id='637795220123087803' IndexInText='35' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='T1' Id='637795220123087799' IndexInText='35' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand2.Literal>
					</BinaryOperator>
					<Comma Name=',' Id='637795220123087886' IndexInText='37' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<BinaryOperator Name=':' Priority='0' Id='637795220123088215' IndexInText='39' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.Literal Id='637795220123087975' IndexInText='39' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='y' Id='637795220123087970' IndexInText='39' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand1.Literal>
						<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637795220123088030' IndexInText='40' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<Name Name=':' Id='637795220123088024' IndexInText='40' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OperatorNameParts>
						</OperatorInfo>
						<Operand2.Literal Id='637795220123088176' IndexInText='41' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='T2' Id='637795220123088171' IndexInText='41' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand2.Literal>
					</BinaryOperator>
					<ClosingRoundBrace Name=')' Id='637795220123088232' IndexInText='43' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Postfixes>
					<Custom Id='637795220123088317' IndexInText='45' ItemLength='37' Interface='UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IWhereCustomExpressionItem'>
						<RegularItems>
							<CustomExpressionItemPart Id='637795220123088324' IndexInText='45' ItemLength='12' Interface='UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem'>
								<RegularItems>
									<Keyword Name='where' Id='637795220123088257' IndexInText='45' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
										<LanguageKeywordInfo ObjectId='47' Id='637781062905654923' Keyword='where' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
									</Keyword>
									<Name Name='T1' Id='637795220123088358' IndexInText='51' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Name Name=':' Id='637795220123088365' IndexInText='53' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637795220123088381' IndexInText='54' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='int' Id='637795220123088374' IndexInText='54' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
								</RegularItems>
								<OtherProperties>
									<WhereKeyword Name='where' Id='637795220123088257' IndexInText='45' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'/>
									<TypeName Name='T1' Id='637795220123088358' IndexInText='51' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<TypeConstraints ObjectId='52' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220123088381' IndexInText='54' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</TypeConstraints>
								</OtherProperties>
							</CustomExpressionItemPart>
							<CustomExpressionItemPart Id='637795220123088398' IndexInText='58' ItemLength='15' Interface='UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem'>
								<RegularItems>
									<Keyword Name='where' Id='637795220123088393' IndexInText='58' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
										<LanguageKeywordInfo ObjectId='47' Id='637781062905654923' Keyword='where' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
									</Keyword>
									<Name Name='T2' Id='637795220123088409' IndexInText='64' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Name Name=':' Id='637795220123088415' IndexInText='66' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637795220123088430' IndexInText='67' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='double' Id='637795220123088423' IndexInText='67' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
								</RegularItems>
								<OtherProperties>
									<WhereKeyword Name='where' Id='637795220123088393' IndexInText='58' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'/>
									<TypeName Name='T2' Id='637795220123088409' IndexInText='64' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<TypeConstraints ObjectId='59' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220123088430' IndexInText='67' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</TypeConstraints>
								</OtherProperties>
							</CustomExpressionItemPart>
							<Name Name='whereend' Id='637795220123088463' IndexInText='74' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<CustomExpressionItemPart Id='637795220123088324' IndexInText='45' ItemLength='12' Interface='UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem'/>
							<CustomExpressionItemPart Id='637795220123088398' IndexInText='58' ItemLength='15' Interface='UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem'/>
						</Children>
						<OtherProperties>
							<WhereEndMarker Name='whereend' Id='637795220123088463' IndexInText='74' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<GenericTypeDataExpressionItems ObjectId='61' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem]'>
								<UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem Id='637795220123088324' IndexInText='45' ItemLength='12'/>
								<UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem Id='637795220123088398' IndexInText='58' ItemLength='15'/>
							</GenericTypeDataExpressionItems>
							<ErrorsPositionDisplayValue value='45' type='System.Int32' />
						</OtherProperties>
					</Custom>
					<CodeBlock Id='637795220123088521' IndexInText='84' ItemLength='101' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
						<RegularItems>
							<CodeBlockStartMarker Name='{' Id='637795220123088515' IndexInText='84' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637795220123088569' IndexInText='184' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<OtherProperties>
							<CodeBlockStartMarker Name='{' Id='637795220123088515' IndexInText='84' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637795220123088569' IndexInText='184' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OtherProperties>
					</CodeBlock>
				</Postfixes>
				<Children>
					<BinaryOperator Name=':' Priority='0' Id='637795220123087852' IndexInText='33' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
					<BinaryOperator Name=':' Priority='0' Id='637795220123088215' IndexInText='39' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
				</Children>
				<OtherProperties>
					<NamedExpressionItem Id='637795220123087198' IndexInText='30' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					<OpeningBraceInfo Name='(' Id='637795220123087491' IndexInText='32' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ClosingBraceInfo Name=')' Id='637795220123088232' IndexInText='43' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Parameters ObjectId='65' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name=':' Priority='0' Id='637795220123087852' IndexInText='33' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name=':' Priority='0' Id='637795220123088215' IndexInText='39' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
					</Parameters>
				</OtherProperties>
			</Braces>
		</RegularItems>
		<Children>
			<BinaryOperator Name='+' Priority='30' Id='637795220123086331' IndexInText='0' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
			<Braces Id='637795220123087483' IndexInText='15' ItemLength='170' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
		</Children>
	</ExpressionItemSeries>
	<ParseErrorData ObjectId='66' HasCriticalErrors='False' Interface='UniversalExpressionParser.IParseErrorData'>
		<AllCodeItemErrors ObjectId='67' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICodeItemParseErrorData]'/>
	</ParseErrorData>
	<SortedCommentedOutCodeInfos ObjectId='68' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICommentedOutCodeInfo]'>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='69' IsLineComment='True' IndexInText='88' ItemLength='94'/>
	</SortedCommentedOutCodeInfos>
	<MainExpressionItem Id='637795220123085743' IndexInText='0' ItemLength='185' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'/>
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
	<ExpressionItemSeries Id='637795220122998748' IndexInText='186' ItemLength='892' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'>
		<RegularItems>
			<Braces Id='637795220123020654' IndexInText='186' ItemLength='892' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
				<Prefixes>
					<Custom Id='637795220123008452' IndexInText='186' ItemLength='142' Interface='UniversalExpressionParser.ExpressionItems.Custom.IKeywordBasedCustomExpressionItem'>
						<RegularItems>
							<Keyword Name='::metadata' Id='637795220122998913' IndexInText='186' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
								<LanguageKeywordInfo ObjectId='5' Id='637781063212876967' Keyword='::metadata' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
							</Keyword>
							<CodeBlock Id='637795220123007077' IndexInText='197' ItemLength='131' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
								<RegularItems>
									<CodeBlockStartMarker Name='{' Id='637795220123007044' IndexInText='197' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<BinaryOperator Name=':' Priority='0' Id='637795220123007729' IndexInText='198' ItemLength='129' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
										<Operand1.Literal Id='637795220123007380' IndexInText='198' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
											<NameExpressionItem Name='description' Id='637795220123007373' IndexInText='198' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</Operand1.Literal>
										<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637795220123007452' IndexInText='209' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
											<OperatorNameParts>
												<Name Name=':' Id='637795220123007441' IndexInText='209' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											</OperatorNameParts>
										</OperatorInfo>
										<Operand2.ConstantText Id='637795220123007668' IndexInText='211' ItemLength='116' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
											<NameExpressionItem Name='"F1 demoes regular function expression item to which multiple prefix and postfix custom expression items are added."' Id='637795220123007663' IndexInText='211' ItemLength='116' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</Operand2.ConstantText>
									</BinaryOperator>
									<CodeBlockEndMarker Name='}' Id='637795220123007783' IndexInText='327' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Children>
									<BinaryOperator Name=':' Priority='0' Id='637795220123007729' IndexInText='198' ItemLength='129' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
								</Children>
								<OtherProperties>
									<CodeBlockStartMarker Name='{' Id='637795220123007044' IndexInText='197' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<CodeBlockEndMarker Name='}' Id='637795220123007783' IndexInText='327' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</OtherProperties>
							</CodeBlock>
						</RegularItems>
						<OtherProperties>
							<LastKeywordExpressionItem Name='::metadata' Id='637795220122998913' IndexInText='186' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'/>
							<ErrorsPositionDisplayValue value='186' type='System.Int32' />
						</OtherProperties>
					</Custom>
					<Custom Id='637795220123014623' IndexInText='496' ItemLength='14' Interface='UniversalExpressionParser.ExpressionItems.Custom.IKeywordBasedCustomExpressionItem'>
						<RegularItems>
							<Keyword Name='::types' Id='637795220123009231' IndexInText='496' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
								<LanguageKeywordInfo ObjectId='18' Id='637781062811583137' Keyword='::types' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
							</Keyword>
							<Braces Id='637795220123013616' IndexInText='503' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
								<RegularItems>
									<OpeningSquareBrace Name='[' Id='637795220123013635' IndexInText='503' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637795220123013849' IndexInText='504' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='T1' Id='637795220123013842' IndexInText='504' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<Comma Name=',' Id='637795220123013897' IndexInText='506' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637795220123013987' IndexInText='507' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='T2' Id='637795220123013978' IndexInText='507' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<ClosingSquareBrace Name=']' Id='637795220123014019' IndexInText='509' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Children>
									<Literal Id='637795220123013849' IndexInText='504' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<Literal Id='637795220123013987' IndexInText='507' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								</Children>
								<OtherProperties>
									<OpeningBraceInfo Name='[' Id='637795220123013635' IndexInText='503' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ClosingBraceInfo Name=']' Id='637795220123014019' IndexInText='509' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
									<Parameters ObjectId='27' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220123013849' IndexInText='504' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220123013987' IndexInText='507' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</Parameters>
								</OtherProperties>
							</Braces>
						</RegularItems>
						<OtherProperties>
							<LastKeywordExpressionItem Name='::types' Id='637795220123009231' IndexInText='496' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'/>
							<ErrorsPositionDisplayValue value='496' type='System.Int32' />
						</OtherProperties>
					</Custom>
				</Prefixes>
				<RegularItems>
					<Literal Id='637795220123020463' IndexInText='512' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='F1' Id='637795220123020432' IndexInText='512' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<OpeningRoundBrace Name='(' Id='637795220123020664' IndexInText='514' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<BinaryOperator Name=':' Priority='0' Id='637795220123021096' IndexInText='515' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.Literal Id='637795220123020812' IndexInText='515' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='x' Id='637795220123020806' IndexInText='515' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand1.Literal>
						<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637795220123020879' IndexInText='516' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<Name Name=':' Id='637795220123020870' IndexInText='516' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OperatorNameParts>
						</OperatorInfo>
						<Operand2.Literal Id='637795220123021043' IndexInText='517' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='T1' Id='637795220123021039' IndexInText='517' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand2.Literal>
					</BinaryOperator>
					<Comma Name=',' Id='637795220123021144' IndexInText='519' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<BinaryOperator Name=':' Priority='0' Id='637795220123021753' IndexInText='521' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.Literal Id='637795220123021483' IndexInText='521' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='y' Id='637795220123021468' IndexInText='521' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand1.Literal>
						<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637795220123021554' IndexInText='522' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<Name Name=':' Id='637795220123021546' IndexInText='522' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OperatorNameParts>
						</OperatorInfo>
						<Operand2.Literal Id='637795220123021707' IndexInText='523' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='T2' Id='637795220123021702' IndexInText='523' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand2.Literal>
					</BinaryOperator>
					<Comma Name=',' Id='637795220123021789' IndexInText='525' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<BinaryOperator Name=':' Priority='0' Id='637795220123022775' IndexInText='527' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.Literal Id='637795220123021885' IndexInText='527' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='z' Id='637795220123021880' IndexInText='527' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand1.Literal>
						<OperatorInfo OperatorType='BinaryOperator' Name=':' Priority='0' Id='637795220123021933' IndexInText='528' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<Name Name=':' Id='637795220123021927' IndexInText='528' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OperatorNameParts>
						</OperatorInfo>
						<Operand2.Literal Id='637795220123022701' IndexInText='529' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='T3' Id='637795220123022680' IndexInText='529' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand2.Literal>
					</BinaryOperator>
					<ClosingRoundBrace Name=')' Id='637795220123022812' IndexInText='531' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Postfixes>
					<Custom Id='637795220123034283' IndexInText='721' ItemLength='43' Interface='UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IWhereCustomExpressionItem'>
						<RegularItems>
							<CustomExpressionItemPart Id='637795220123035211' IndexInText='721' ItemLength='18' Interface='UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem'>
								<RegularItems>
									<Keyword Name='where' Id='637795220123022874' IndexInText='721' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
										<LanguageKeywordInfo ObjectId='58' Id='637781062905654923' Keyword='where' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
									</Keyword>
									<Name Name='T1' Id='637795220123036605' IndexInText='727' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Name Name=':' Id='637795220123037737' IndexInText='729' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637795220123038326' IndexInText='730' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='int' Id='637795220123037802' IndexInText='730' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<Comma Name=',' Id='637795220123039246' IndexInText='733' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637795220123039308' IndexInText='734' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='class' Id='637795220123039296' IndexInText='734' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
								</RegularItems>
								<OtherProperties>
									<WhereKeyword Name='where' Id='637795220123022874' IndexInText='721' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'/>
									<TypeName Name='T1' Id='637795220123036605' IndexInText='727' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<TypeConstraints ObjectId='66' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220123038326' IndexInText='730' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220123039308' IndexInText='734' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</TypeConstraints>
								</OtherProperties>
							</CustomExpressionItemPart>
							<CustomExpressionItemPart Id='637795220123040232' IndexInText='740' ItemLength='15' Interface='UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem'>
								<RegularItems>
									<Keyword Name='where' Id='637795220123040213' IndexInText='740' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
										<LanguageKeywordInfo ObjectId='58' Id='637781062905654923' Keyword='where' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
									</Keyword>
									<Name Name='T2' Id='637795220123040328' IndexInText='746' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Name Name=':' Id='637795220123040337' IndexInText='748' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637795220123040356' IndexInText='749' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='double' Id='637795220123040347' IndexInText='749' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
								</RegularItems>
								<OtherProperties>
									<WhereKeyword Name='where' Id='637795220123040213' IndexInText='740' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'/>
									<TypeName Name='T2' Id='637795220123040328' IndexInText='746' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<TypeConstraints ObjectId='73' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220123040356' IndexInText='749' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</TypeConstraints>
								</OtherProperties>
							</CustomExpressionItemPart>
							<Name Name='whereend' Id='637795220123042333' IndexInText='756' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<CustomExpressionItemPart Id='637795220123035211' IndexInText='721' ItemLength='18' Interface='UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem'/>
							<CustomExpressionItemPart Id='637795220123040232' IndexInText='740' ItemLength='15' Interface='UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem'/>
						</Children>
						<OtherProperties>
							<WhereEndMarker Name='whereend' Id='637795220123042333' IndexInText='756' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<GenericTypeDataExpressionItems ObjectId='75' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem]'>
								<UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem Id='637795220123035211' IndexInText='721' ItemLength='18'/>
								<UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem Id='637795220123040232' IndexInText='740' ItemLength='15'/>
							</GenericTypeDataExpressionItems>
							<ErrorsPositionDisplayValue value='721' type='System.Int32' />
						</OtherProperties>
					</Custom>
					<Custom Id='637795220123043871' IndexInText='944' ItemLength='22' Interface='UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IWhereCustomExpressionItem'>
						<RegularItems>
							<CustomExpressionItemPart Id='637795220123043881' IndexInText='944' ItemLength='13' Interface='UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem'>
								<RegularItems>
									<Keyword Name='where' Id='637795220123043727' IndexInText='944' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
										<LanguageKeywordInfo ObjectId='58' Id='637781062905654923' Keyword='where' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
									</Keyword>
									<Name Name='T3' Id='637795220123043924' IndexInText='950' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Name Name=':' Id='637795220123043932' IndexInText='953' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Literal Id='637795220123043948' IndexInText='955' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='T1' Id='637795220123043941' IndexInText='955' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
								</RegularItems>
								<OtherProperties>
									<WhereKeyword Name='where' Id='637795220123043727' IndexInText='944' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'/>
									<TypeName Name='T3' Id='637795220123043924' IndexInText='950' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<TypeConstraints ObjectId='83' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220123043948' IndexInText='955' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									</TypeConstraints>
								</OtherProperties>
							</CustomExpressionItemPart>
							<Name Name='whereend' Id='637795220123043972' IndexInText='958' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<CustomExpressionItemPart Id='637795220123043881' IndexInText='944' ItemLength='13' Interface='UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem'/>
						</Children>
						<OtherProperties>
							<WhereEndMarker Name='whereend' Id='637795220123043972' IndexInText='958' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<GenericTypeDataExpressionItems ObjectId='85' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem]'>
								<UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem Id='637795220123043881' IndexInText='944' ItemLength='13'/>
							</GenericTypeDataExpressionItems>
							<ErrorsPositionDisplayValue value='944' type='System.Int32' />
						</OtherProperties>
					</Custom>
					<CodeBlock Id='637795220123044266' IndexInText='969' ItemLength='109' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
						<RegularItems>
							<CodeBlockStartMarker Name='{' Id='637795220123044257' IndexInText='969' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637795220123044319' IndexInText='1077' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<OtherProperties>
							<CodeBlockStartMarker Name='{' Id='637795220123044257' IndexInText='969' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637795220123044319' IndexInText='1077' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OtherProperties>
					</CodeBlock>
				</Postfixes>
				<Children>
					<BinaryOperator Name=':' Priority='0' Id='637795220123021096' IndexInText='515' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
					<BinaryOperator Name=':' Priority='0' Id='637795220123021753' IndexInText='521' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
					<BinaryOperator Name=':' Priority='0' Id='637795220123022775' IndexInText='527' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
				</Children>
				<OtherProperties>
					<NamedExpressionItem Id='637795220123020463' IndexInText='512' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					<OpeningBraceInfo Name='(' Id='637795220123020664' IndexInText='514' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ClosingBraceInfo Name=')' Id='637795220123022812' IndexInText='531' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Parameters ObjectId='89' Count='3' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name=':' Priority='0' Id='637795220123021096' IndexInText='515' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name=':' Priority='0' Id='637795220123021753' IndexInText='521' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name=':' Priority='0' Id='637795220123022775' IndexInText='527' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
					</Parameters>
				</OtherProperties>
			</Braces>
		</RegularItems>
		<Children>
			<Braces Id='637795220123020654' IndexInText='186' ItemLength='892' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
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
	<MainExpressionItem Id='637795220122998748' IndexInText='186' ItemLength='892' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'/>
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
	<ExpressionItemSeries Id='637795220122777974' IndexInText='17' ItemLength='140' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'>
		<RegularItems>
			<BinaryOperator Name='=' Priority='2000' Id='637795220122865451' IndexInText='17' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
				<Operand1.Literal Id='637795220122778408' IndexInText='17' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
					<NameExpressionItem Name='x' Id='637795220122778401' IndexInText='21' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<AppliedKeywords>
						<Keyword Name='var' Id='637795220122778213' IndexInText='17' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
							<LanguageKeywordInfo ObjectId='6' Id='637781064051574641' Keyword='var' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
						</Keyword>
					</AppliedKeywords>
				</Operand1.Literal>
				<OperatorInfo OperatorType='BinaryOperator' Name='=' Priority='2000' Id='637795220122778541' IndexInText='23' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
					<OperatorNameParts>
						<Name Name='=' Id='637795220122778526' IndexInText='23' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</OperatorNameParts>
				</OperatorInfo>
				<Operand2.BinaryOperator Name='*' Priority='20' Id='637795220122865479' IndexInText='25' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
					<Operand1.ConstantNumericValue Id='637795220122864788' IndexInText='25' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
						<NameExpressionItem Name='5' Id='637795220122864721' IndexInText='25' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<OtherProperties>
							<SucceededNumericTypeDescriptor ObjectId='12' NumberTypeId='1581134183960' Interface='UniversalExpressionParser.NumericTypeDescriptor'>
								<RegularExpressions ObjectId='13' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[System.String]'>
									<System.String value='^\d+' />
								</RegularExpressions>
							</SucceededNumericTypeDescriptor>
						</OtherProperties>
					</Operand1.ConstantNumericValue>
					<OperatorInfo OperatorType='BinaryOperator' Name='*' Priority='20' Id='637795220122865096' IndexInText='26' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
						<OperatorNameParts>
							<Name Name='*' Id='637795220122865083' IndexInText='26' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OperatorNameParts>
					</OperatorInfo>
					<Operand2.Literal Id='637795220122865378' IndexInText='27' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='y' Id='637795220122865370' IndexInText='27' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Operand2.Literal>
				</Operand2.BinaryOperator>
			</BinaryOperator>
			<ExpressionSeparator Name=';' Id='637795220122865549' IndexInText='28' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<Braces Id='637795220122896940' IndexInText='58' ItemLength='98' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
				<RegularItems>
					<Literal Id='637795220122896731' IndexInText='58' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='println' Id='637795220122865727' IndexInText='58' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<OpeningRoundBrace Name='(' Id='637795220122896948' IndexInText='65' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<BinaryOperator Name='+' Priority='30' Id='637795220122976852' IndexInText='66' ItemLength='89' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.BinaryOperator Name='+' Priority='30' Id='637795220122976831' IndexInText='66' ItemLength='84' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
							<Operand1.Literal Id='637795220122897127' IndexInText='66' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='x' Id='637795220122897115' IndexInText='66' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Operand1.Literal>
							<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637795220122897203' IndexInText='68' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
								<OperatorNameParts>
									<Name Name='+' Id='637795220122897192' IndexInText='68' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</OperatorNameParts>
							</OperatorInfo>
							<Operand2.Literal Id='637795220122897393' IndexInText='149' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='y' Id='637795220122897388' IndexInText='149' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Operand2.Literal>
						</Operand1.BinaryOperator>
						<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637795220122897476' IndexInText='150' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<Name Name='+' Id='637795220122897470' IndexInText='150' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OperatorNameParts>
						</OperatorInfo>
						<Operand2.BinaryOperator Name='*' Priority='20' Id='637795220122976860' IndexInText='151' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
							<Operand1.ConstantNumericValue Id='637795220122976378' IndexInText='151' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
								<NameExpressionItem Name='10' Id='637795220122976337' IndexInText='151' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<OtherProperties>
									<SucceededNumericTypeDescriptor ObjectId='12' NumberTypeId='1581134183960' Interface='UniversalExpressionParser.NumericTypeDescriptor'/>
								</OtherProperties>
							</Operand1.ConstantNumericValue>
							<OperatorInfo OperatorType='BinaryOperator' Name='*' Priority='20' Id='637795220122976590' IndexInText='153' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
								<OperatorNameParts>
									<Name Name='*' Id='637795220122976577' IndexInText='153' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</OperatorNameParts>
							</OperatorInfo>
							<Operand2.Literal Id='637795220122976772' IndexInText='154' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='z' Id='637795220122976763' IndexInText='154' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Operand2.Literal>
						</Operand2.BinaryOperator>
					</BinaryOperator>
					<ClosingRoundBrace Name=')' Id='637795220122976921' IndexInText='155' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Children>
					<BinaryOperator Name='+' Priority='30' Id='637795220122976852' IndexInText='66' ItemLength='89' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
				</Children>
				<OtherProperties>
					<NamedExpressionItem Id='637795220122896731' IndexInText='58' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					<OpeningBraceInfo Name='(' Id='637795220122896948' IndexInText='65' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ClosingBraceInfo Name=')' Id='637795220122976921' IndexInText='155' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Parameters ObjectId='41' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name='+' Priority='30' Id='637795220122976852' IndexInText='66' ItemLength='89' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
					</Parameters>
				</OtherProperties>
			</Braces>
			<ExpressionSeparator Name=';' Id='637795220122976964' IndexInText='156' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
		</RegularItems>
		<Children>
			<BinaryOperator Name='=' Priority='2000' Id='637795220122865451' IndexInText='17' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
			<Braces Id='637795220122896940' IndexInText='58' ItemLength='98' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
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
	<MainExpressionItem Id='637795220122777974' IndexInText='17' ItemLength='140' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'/>
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
	<ExpressionItemSeries Id='637795220123115270' IndexInText='0' ItemLength='301' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'>
		<RegularItems>
			<Operators Id='637795220123129611' IndexInText='0' ItemLength='39' Interface='UniversalExpressionParser.ExpressionItems.IComplexExpressionItem'>
				<RegularItems>
					<Literal Id='637795220123115607' IndexInText='0' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='x' Id='637795220123115601' IndexInText='4' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<AppliedKeywords>
							<Keyword Name='var' Id='637795220123115440' IndexInText='0' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
								<LanguageKeywordInfo ObjectId='6' Id='637781064051574641' Keyword='var' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
							</Keyword>
						</AppliedKeywords>
					</Literal>
					<OperatorInfo OperatorType='BinaryOperator' Name='=' Priority='2000' Id='637795220123115720' IndexInText='6' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
						<OperatorNameParts>
							<Name Name='=' Id='637795220123115709' IndexInText='6' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OperatorNameParts>
					</OperatorInfo>
					<Literal Id='637795220123115961' IndexInText='8' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='y' Id='637795220123115938' IndexInText='8' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<Literal Id='637795220123127882' IndexInText='38' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='x' Id='637795220123127845' IndexInText='38' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
				</RegularItems>
			</Operators>
			<ExpressionSeparator Name=';' Id='637795220123130062' IndexInText='39' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<CodeBlock Id='637795220123130148' IndexInText='44' ItemLength='257' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
				<RegularItems>
					<CodeBlockStartMarker Name='{' Id='637795220123130142' IndexInText='44' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Braces Id='637795220123130359' IndexInText='84' ItemLength='217' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<Literal Id='637795220123130322' IndexInText='84' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='f1' Id='637795220123130317' IndexInText='84' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<OpeningRoundBrace Name='(' Id='637795220123130364' IndexInText='86' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Literal Id='637795220123130472' IndexInText='87' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='x' Id='637795220123130467' IndexInText='87' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<Comma Name=',' Id='637795220123130507' IndexInText='88' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Literal Id='637795220123130596' IndexInText='90' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='y' Id='637795220123130591' IndexInText='90' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<Comma Name=',' Id='637795220123130624' IndexInText='91' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingRoundBrace Name=')' Id='637795220123130710' IndexInText='131' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Postfixes>
							<CodeBlock Id='637795220123130760' IndexInText='138' ItemLength='163' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
								<RegularItems>
									<CodeBlockStartMarker Name='{' Id='637795220123130750' IndexInText='138' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Operators Id='637795220123132583' IndexInText='151' ItemLength='17' Interface='UniversalExpressionParser.ExpressionItems.IComplexExpressionItem'>
										<RegularItems>
											<Literal Id='637795220123130892' IndexInText='151' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
												<NameExpressionItem Name='z' Id='637795220123130887' IndexInText='155' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												<AppliedKeywords>
													<Keyword Name='var' Id='637795220123130788' IndexInText='151' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
														<LanguageKeywordInfo ObjectId='6' Id='637781064051574641' Keyword='var' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
													</Keyword>
												</AppliedKeywords>
											</Literal>
											<OperatorInfo OperatorType='BinaryOperator' Name='=' Priority='2000' Id='637795220123130980' IndexInText='157' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
												<OperatorNameParts>
													<Name Name='=' Id='637795220123130967' IndexInText='157' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												</OperatorNameParts>
											</OperatorInfo>
											<OperatorInfo OperatorType='PrefixUnaryOperator' Name='++' Priority='0' Id='637795220123131045' IndexInText='159' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
												<OperatorNameParts>
													<Name Name='++' Id='637795220123131039' IndexInText='159' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												</OperatorNameParts>
											</OperatorInfo>
											<Literal Id='637795220123131165' IndexInText='161' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
												<NameExpressionItem Name='x' Id='637795220123131161' IndexInText='161' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											</Literal>
											<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637795220123131275' IndexInText='163' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
												<OperatorNameParts>
													<Name Name='+' Id='637795220123131269' IndexInText='163' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												</OperatorNameParts>
											</OperatorInfo>
											<Literal Id='637795220123131413' IndexInText='165' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
												<NameExpressionItem Name='y' Id='637795220123131408' IndexInText='165' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											</Literal>
											<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637795220123131480' IndexInText='167' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
												<OperatorNameParts>
													<Name Name='+' Id='637795220123131470' IndexInText='167' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												</OperatorNameParts>
											</OperatorInfo>
										</RegularItems>
									</Operators>
									<ExpressionSeparator Name=';' Id='637795220123132622' IndexInText='218' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Operators Id='637795220123133182' IndexInText='229' ItemLength='64' Interface='UniversalExpressionParser.ExpressionItems.IComplexExpressionItem'>
										<RegularItems>
											<OperatorInfo OperatorType='PrefixUnaryOperator' Name='return' Priority='2147483647' Id='637795220123132700' IndexInText='229' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
												<OperatorNameParts>
													<Name Name='return' Id='637795220123132693' IndexInText='229' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												</OperatorNameParts>
											</OperatorInfo>
											<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637795220123132770' IndexInText='236' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
												<OperatorNameParts>
													<Name Name='+' Id='637795220123132758' IndexInText='236' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												</OperatorNameParts>
											</OperatorInfo>
											<Literal Id='637795220123132927' IndexInText='288' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
												<NameExpressionItem Name='z' Id='637795220123132922' IndexInText='288' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											</Literal>
											<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637795220123133006' IndexInText='290' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
												<OperatorNameParts>
													<Name Name='+' Id='637795220123133000' IndexInText='290' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
												</OperatorNameParts>
											</OperatorInfo>
											<Literal Id='637795220123133142' IndexInText='292' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
												<NameExpressionItem Name='y' Id='637795220123133137' IndexInText='292' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											</Literal>
										</RegularItems>
									</Operators>
									<ExpressionSeparator Name=';' Id='637795220123133193' IndexInText='293' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<CodeBlockEndMarker Name='}' Id='637795220123133221' IndexInText='300' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Children>
									<Operators Id='637795220123132583' IndexInText='151' ItemLength='17' Interface='UniversalExpressionParser.ExpressionItems.IComplexExpressionItem'/>
									<Operators Id='637795220123133182' IndexInText='229' ItemLength='64' Interface='UniversalExpressionParser.ExpressionItems.IComplexExpressionItem'/>
								</Children>
								<OtherProperties>
									<CodeBlockStartMarker Name='{' Id='637795220123130750' IndexInText='138' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<CodeBlockEndMarker Name='}' Id='637795220123133221' IndexInText='300' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</OtherProperties>
							</CodeBlock>
						</Postfixes>
						<Children>
							<Literal Id='637795220123130472' IndexInText='87' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							<Literal Id='637795220123130596' IndexInText='90' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
						</Children>
						<OtherProperties>
							<NamedExpressionItem Id='637795220123130322' IndexInText='84' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							<OpeningBraceInfo Name='(' Id='637795220123130364' IndexInText='86' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=')' Id='637795220123130710' IndexInText='131' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Parameters ObjectId='59' Count='3' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220123130472' IndexInText='87' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220123130596' IndexInText='90' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase value='null' interface='UniversalExpressionParser.ExpressionItems.IExpressionItemBase' />
							</Parameters>
						</OtherProperties>
					</Braces>
				</RegularItems>
				<Children>
					<Braces Id='637795220123130359' IndexInText='84' ItemLength='217' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
				</Children>
				<OtherProperties>
					<CodeBlockStartMarker Name='{' Id='637795220123130142' IndexInText='44' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<CodeBlockEndMarker value='null' interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem' />
				</OtherProperties>
			</CodeBlock>
		</RegularItems>
		<Children>
			<Operators Id='637795220123129611' IndexInText='0' ItemLength='39' Interface='UniversalExpressionParser.ExpressionItems.IComplexExpressionItem'/>
			<CodeBlock Id='637795220123130148' IndexInText='44' ItemLength='257' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'/>
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
	<MainExpressionItem Id='637795220123115270' IndexInText='0' ItemLength='301' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'/>
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
	<BracesExpressionItem Id='637795220125155421' IndexInText='313' ItemLength='86' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
		<RegularItems>
			<OpeningRoundBrace Name='(' Id='637795220125155459' IndexInText='313' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<BinaryOperator Name='AND' Priority='80' Id='637795220125330179' IndexInText='314' ItemLength='84' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
				<Operand1.BinaryOperator Name='AND' Priority='80' Id='637795220125330152' IndexInText='314' ItemLength='44' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
					<Operand1.BinaryOperator Name='>' Priority='50' Id='637795220125330129' IndexInText='314' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.Literal Id='637795220125156871' IndexInText='314' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='SALARY' Id='637795220125156851' IndexInText='314' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand1.Literal>
						<OperatorInfo OperatorType='BinaryOperator' Name='>' Priority='50' Id='637795220125156967' IndexInText='321' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<Name Name='>' Id='637795220125156956' IndexInText='321' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OperatorNameParts>
						</OperatorInfo>
						<Operand2.ConstantNumericValue Id='637795220125247662' IndexInText='323' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
							<NameExpressionItem Name='0' Id='637795220125247601' IndexInText='323' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<OtherProperties>
								<SucceededNumericTypeDescriptor ObjectId='12' NumberTypeId='1581134183960' Interface='UniversalExpressionParser.NumericTypeDescriptor'>
									<RegularExpressions ObjectId='13' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[System.String]'>
										<System.String value='^\d+' />
									</RegularExpressions>
								</SucceededNumericTypeDescriptor>
							</OtherProperties>
						</Operand2.ConstantNumericValue>
					</Operand1.BinaryOperator>
					<OperatorInfo OperatorType='BinaryOperator' Name='AND' Priority='80' Id='637795220125247989' IndexInText='325' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
						<OperatorNameParts>
							<Name Name='AND' Id='637795220125247975' IndexInText='325' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OperatorNameParts>
					</OperatorInfo>
					<Operand2.BinaryOperator Name='>' Priority='50' Id='637795220125330161' IndexInText='337' ItemLength='21' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.Literal Id='637795220125248165' IndexInText='337' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='SALARY' Id='637795220125248160' IndexInText='337' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Operand1.Literal>
						<OperatorInfo OperatorType='BinaryOperator' Name='>' Priority='50' Id='637795220125248244' IndexInText='344' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<Name Name='>' Id='637795220125248237' IndexInText='344' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OperatorNameParts>
						</OperatorInfo>
						<Operand2.BinaryOperator Name='/' Priority='20' Id='637795220125330172' IndexInText='346' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
							<Operand1.Literal Id='637795220125248378' IndexInText='346' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='MAX_SALARY' Id='637795220125248373' IndexInText='346' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Operand1.Literal>
							<OperatorInfo OperatorType='BinaryOperator' Name='/' Priority='20' Id='637795220125248439' IndexInText='356' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
								<OperatorNameParts>
									<Name Name='/' Id='637795220125248433' IndexInText='356' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</OperatorNameParts>
							</OperatorInfo>
							<Operand2.ConstantNumericValue Id='637795220125329127' IndexInText='357' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INumericValueExpressionItem'>
								<NameExpressionItem Name='2' Id='637795220125329080' IndexInText='357' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<OtherProperties>
									<SucceededNumericTypeDescriptor ObjectId='12' NumberTypeId='1581134183960' Interface='UniversalExpressionParser.NumericTypeDescriptor'/>
								</OtherProperties>
							</Operand2.ConstantNumericValue>
						</Operand2.BinaryOperator>
					</Operand2.BinaryOperator>
				</Operand1.BinaryOperator>
				<OperatorInfo OperatorType='BinaryOperator' Name='AND' Priority='80' Id='637795220125329390' IndexInText='359' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
					<OperatorNameParts>
						<Name Name='AND' Id='637795220125329373' IndexInText='359' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</OperatorNameParts>
				</OperatorInfo>
				<Operand2.BinaryOperator Name='<' Priority='50' Id='637795220125330190' IndexInText='371' ItemLength='27' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
					<Operand1.Braces Id='637795220125329584' IndexInText='371' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<Literal Id='637795220125329545' IndexInText='371' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='f1' Id='637795220125329536' IndexInText='371' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<OpeningRoundBrace Name='(' Id='637795220125329588' IndexInText='373' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Literal Id='637795220125329688' IndexInText='374' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='SALARY' Id='637795220125329679' IndexInText='374' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<ClosingRoundBrace Name=')' Id='637795220125329727' IndexInText='380' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<Literal Id='637795220125329688' IndexInText='374' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
						</Children>
						<OtherProperties>
							<NamedExpressionItem Id='637795220125329545' IndexInText='371' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							<OpeningBraceInfo Name='(' Id='637795220125329588' IndexInText='373' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=')' Id='637795220125329727' IndexInText='380' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Parameters ObjectId='38' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220125329688' IndexInText='374' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Operand1.Braces>
					<OperatorInfo OperatorType='BinaryOperator' Name='<' Priority='50' Id='637795220125329797' IndexInText='382' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
						<OperatorNameParts>
							<Name Name='<' Id='637795220125329791' IndexInText='382' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OperatorNameParts>
					</OperatorInfo>
					<Operand2.Braces Id='637795220125329967' IndexInText='384' ItemLength='14' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<Literal Id='637795220125329931' IndexInText='384' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='f2' Id='637795220125329926' IndexInText='384' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<OpeningRoundBrace Name='(' Id='637795220125329970' IndexInText='386' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Literal Id='637795220125330057' IndexInText='387' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='MAX_SALARY' Id='637795220125330053' IndexInText='387' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<ClosingRoundBrace Name=')' Id='637795220125330090' IndexInText='397' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<Literal Id='637795220125330057' IndexInText='387' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
						</Children>
						<OtherProperties>
							<NamedExpressionItem Id='637795220125329931' IndexInText='384' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							<OpeningBraceInfo Name='(' Id='637795220125329970' IndexInText='386' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=')' Id='637795220125330090' IndexInText='397' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Parameters ObjectId='48' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220125330057' IndexInText='387' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Operand2.Braces>
				</Operand2.BinaryOperator>
			</BinaryOperator>
			<ClosingRoundBrace Name=')' Id='637795220125330257' IndexInText='398' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
		</RegularItems>
		<Children>
			<BinaryOperator Name='AND' Priority='80' Id='637795220125330179' IndexInText='314' ItemLength='84' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
		</Children>
		<OtherProperties>
			<OpeningBraceInfo Name='(' Id='637795220125155459' IndexInText='313' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<ClosingBraceInfo Name=')' Id='637795220125330257' IndexInText='398' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
			<Parameters ObjectId='50' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
				<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name='AND' Priority='80' Id='637795220125330179' IndexInText='314' ItemLength='84' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
			</Parameters>
		</OtherProperties>
	</BracesExpressionItem>
	<ParseErrorData ObjectId='51' HasCriticalErrors='False' Interface='UniversalExpressionParser.IParseErrorData'>
		<AllCodeItemErrors ObjectId='52' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICodeItemParseErrorData]'/>
	</ParseErrorData>
	<SortedCommentedOutCodeInfos ObjectId='53' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICommentedOutCodeInfo]'/>
	<MainExpressionItem Id='637795220125155421' IndexInText='313' ItemLength='86' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
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
	<BracesExpressionItem Id='637795220125394518' IndexInText='22' ItemLength='28' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
		<RegularItems>
			<OpeningSquareBrace Name='[' Id='637795220125394537' IndexInText='22' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<BinaryOperator Name='+' Priority='30' Id='637795220125395301' IndexInText='23' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
				<Operand1.Braces Id='637795220125394899' IndexInText='23' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
					<RegularItems>
						<Literal Id='637795220125394849' IndexInText='23' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='f1' Id='637795220125394842' IndexInText='23' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Literal>
						<OpeningRoundBrace Name='(' Id='637795220125394903' IndexInText='25' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<ClosingRoundBrace Name=')' Id='637795220125394957' IndexInText='26' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</RegularItems>
					<OtherProperties>
						<NamedExpressionItem Id='637795220125394849' IndexInText='23' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
						<OpeningBraceInfo Name='(' Id='637795220125394903' IndexInText='25' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<ClosingBraceInfo Name=')' Id='637795220125394957' IndexInText='26' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<Parameters ObjectId='9' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'/>
					</OtherProperties>
				</Operand1.Braces>
				<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637795220125395032' IndexInText='27' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
					<OperatorNameParts>
						<Name Name='+' Id='637795220125395022' IndexInText='27' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</OperatorNameParts>
				</OperatorInfo>
				<Operand2.Braces Id='637795220125395228' IndexInText='28' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
					<RegularItems>
						<Literal Id='637795220125395183' IndexInText='28' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
							<NameExpressionItem Name='m1' Id='637795220125395179' IndexInText='28' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</Literal>
						<OpeningSquareBrace Name='[' Id='637795220125395232' IndexInText='30' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<ClosingSquareBrace Name=']' Id='637795220125395259' IndexInText='31' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</RegularItems>
					<OtherProperties>
						<NamedExpressionItem Id='637795220125395183' IndexInText='28' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
						<OpeningBraceInfo Name='[' Id='637795220125395232' IndexInText='30' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<ClosingBraceInfo Name=']' Id='637795220125395259' IndexInText='31' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<Parameters ObjectId='17' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'/>
					</OtherProperties>
				</Operand2.Braces>
			</BinaryOperator>
			<Comma Name=',' Id='637795220125395358' IndexInText='32' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<Literal Id='637795220125395452' IndexInText='34' ItemLength='15' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
				<NameExpressionItem Name='f2' Id='637795220125395448' IndexInText='34' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				<Postfixes>
					<CodeBlock Id='637795220125395485' IndexInText='38' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
						<RegularItems>
							<CodeBlockStartMarker Name='{' Id='637795220125395481' IndexInText='38' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<PrefixUnaryOperator Name='++' Priority='0' Id='637795220125395701' IndexInText='42' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
								<OperatorInfo OperatorType='PrefixUnaryOperator' Name='++' Priority='0' Id='637795220125395541' IndexInText='42' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
									<OperatorNameParts>
										<Name Name='++' Id='637795220125395535' IndexInText='42' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</OperatorNameParts>
								</OperatorInfo>
								<Operand1.Literal Id='637795220125395657' IndexInText='44' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
									<NameExpressionItem Name='i' Id='637795220125395651' IndexInText='44' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</Operand1.Literal>
							</PrefixUnaryOperator>
							<ExpressionSeparator Name=';' Id='637795220125395721' IndexInText='45' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637795220125395749' IndexInText='48' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<PrefixUnaryOperator Name='++' Priority='0' Id='637795220125395701' IndexInText='42' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
						</Children>
						<OtherProperties>
							<CodeBlockStartMarker Name='{' Id='637795220125395481' IndexInText='38' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='}' Id='637795220125395749' IndexInText='48' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OtherProperties>
					</CodeBlock>
				</Postfixes>
			</Literal>
			<ClosingSquareBrace Name=']' Id='637795220125395779' IndexInText='49' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
		</RegularItems>
		<Children>
			<BinaryOperator Name='+' Priority='30' Id='637795220125395301' IndexInText='23' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
			<Literal Id='637795220125395452' IndexInText='34' ItemLength='15' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
		</Children>
		<OtherProperties>
			<OpeningBraceInfo Name='[' Id='637795220125394537' IndexInText='22' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<ClosingBraceInfo Name=']' Id='637795220125395779' IndexInText='49' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<NamedExpressionItem value='null' interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem' />
			<Parameters ObjectId='31' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
				<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name='+' Priority='30' Id='637795220125395301' IndexInText='23' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
				<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220125395452' IndexInText='34' ItemLength='15' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
			</Parameters>
		</OtherProperties>
	</BracesExpressionItem>
	<ParseErrorData ObjectId='32' HasCriticalErrors='False' Interface='UniversalExpressionParser.IParseErrorData'>
		<AllCodeItemErrors ObjectId='33' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICodeItemParseErrorData]'/>
	</ParseErrorData>
	<SortedCommentedOutCodeInfos ObjectId='34' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICommentedOutCodeInfo]'/>
	<MainExpressionItem Id='637795220125394518' IndexInText='22' ItemLength='28' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
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
	<CodeBlockExpressionItem Id='637795220125121335' IndexInText='28' ItemLength='29' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
		<RegularItems>
			<CodeBlockStartMarker Name='{' Id='637795220125121299' IndexInText='28' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<Braces Id='637795220125122959' IndexInText='31' ItemLength='23' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
				<RegularItems>
					<Literal Id='637795220125122907' IndexInText='31' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='f1' Id='637795220125122891' IndexInText='31' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<OpeningRoundBrace Name='(' Id='637795220125122964' IndexInText='33' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<BinaryOperator Name='+' Priority='30' Id='637795220125123486' IndexInText='34' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.Braces Id='637795220125123103' IndexInText='34' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
							<RegularItems>
								<Literal Id='637795220125123076' IndexInText='34' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
									<NameExpressionItem Name='f2' Id='637795220125123071' IndexInText='34' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</Literal>
								<OpeningRoundBrace Name='(' Id='637795220125123107' IndexInText='36' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<ClosingRoundBrace Name=')' Id='637795220125123142' IndexInText='37' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</RegularItems>
							<OtherProperties>
								<NamedExpressionItem Id='637795220125123076' IndexInText='34' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								<OpeningBraceInfo Name='(' Id='637795220125123107' IndexInText='36' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<ClosingBraceInfo Name=')' Id='637795220125123142' IndexInText='37' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<Parameters ObjectId='13' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'/>
							</OtherProperties>
						</Operand1.Braces>
						<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637795220125123220' IndexInText='38' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<Name Name='+' Id='637795220125123210' IndexInText='38' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OperatorNameParts>
						</OperatorInfo>
						<Operand2.Braces Id='637795220125123412' IndexInText='39' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
							<RegularItems>
								<Literal Id='637795220125123368' IndexInText='39' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
									<NameExpressionItem Name='m1' Id='637795220125123363' IndexInText='39' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</Literal>
								<OpeningSquareBrace Name='[' Id='637795220125123416' IndexInText='41' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<ClosingSquareBrace Name=']' Id='637795220125123449' IndexInText='42' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</RegularItems>
							<OtherProperties>
								<NamedExpressionItem Id='637795220125123368' IndexInText='39' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
								<OpeningBraceInfo Name='[' Id='637795220125123416' IndexInText='41' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<ClosingBraceInfo Name=']' Id='637795220125123449' IndexInText='42' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								<Parameters ObjectId='21' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'/>
							</OtherProperties>
						</Operand2.Braces>
					</BinaryOperator>
					<Comma Name=',' Id='637795220125123576' IndexInText='43' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Literal Id='637795220125123674' IndexInText='45' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='f2' Id='637795220125123670' IndexInText='45' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						<Postfixes>
							<CodeBlock Id='637795220125123706' IndexInText='47' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
								<RegularItems>
									<CodeBlockStartMarker Name='{' Id='637795220125123701' IndexInText='47' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<PrefixUnaryOperator Name='++' Priority='0' Id='637795220125123923' IndexInText='48' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
										<OperatorInfo OperatorType='PrefixUnaryOperator' Name='++' Priority='0' Id='637795220125123757' IndexInText='48' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
											<OperatorNameParts>
												<Name Name='++' Id='637795220125123751' IndexInText='48' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
											</OperatorNameParts>
										</OperatorInfo>
										<Operand1.Literal Id='637795220125123873' IndexInText='50' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
											<NameExpressionItem Name='i' Id='637795220125123869' IndexInText='50' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</Operand1.Literal>
									</PrefixUnaryOperator>
									<ExpressionSeparator Name=';' Id='637795220125123942' IndexInText='51' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<CodeBlockEndMarker Name='}' Id='637795220125123971' IndexInText='52' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<Children>
									<PrefixUnaryOperator Name='++' Priority='0' Id='637795220125123923' IndexInText='48' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
								</Children>
								<OtherProperties>
									<CodeBlockStartMarker Name='{' Id='637795220125123701' IndexInText='47' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<CodeBlockEndMarker Name='}' Id='637795220125123971' IndexInText='52' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</OtherProperties>
							</CodeBlock>
						</Postfixes>
					</Literal>
					<ClosingRoundBrace Name=')' Id='637795220125124001' IndexInText='53' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Children>
					<BinaryOperator Name='+' Priority='30' Id='637795220125123486' IndexInText='34' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
					<Literal Id='637795220125123674' IndexInText='45' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
				</Children>
				<OtherProperties>
					<NamedExpressionItem Id='637795220125122907' IndexInText='31' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					<OpeningBraceInfo Name='(' Id='637795220125122964' IndexInText='33' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ClosingBraceInfo Name=')' Id='637795220125124001' IndexInText='53' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Parameters ObjectId='35' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name='+' Priority='30' Id='637795220125123486' IndexInText='34' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220125123674' IndexInText='45' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					</Parameters>
				</OtherProperties>
			</Braces>
			<CodeBlockEndMarker Name='}' Id='637795220125124032' IndexInText='56' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
		</RegularItems>
		<Children>
			<Braces Id='637795220125122959' IndexInText='31' ItemLength='23' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
		</Children>
		<OtherProperties>
			<CodeBlockStartMarker Name='{' Id='637795220125121299' IndexInText='28' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<CodeBlockEndMarker Name='}' Id='637795220125124032' IndexInText='56' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
		</OtherProperties>
	</CodeBlockExpressionItem>
	<ParseErrorData ObjectId='37' HasCriticalErrors='False' Interface='UniversalExpressionParser.IParseErrorData'>
		<AllCodeItemErrors ObjectId='38' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICodeItemParseErrorData]'/>
	</ParseErrorData>
	<SortedCommentedOutCodeInfos ObjectId='39' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICommentedOutCodeInfo]'/>
	<MainExpressionItem Id='637795220125121335' IndexInText='28' ItemLength='29' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'/>
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
<UniversalExpressionParser.ExpressionItems.IRootExpressionItemSeries ObjectId='0' PositionInTextOnCompletion='1572' IndexInText='332' ItemLength='1240'>
	<ExpressionItemSeries Id='637795220121633296' IndexInText='332' ItemLength='1170' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'>
		<RegularItems>
			<CodeBlock Id='637795220121740461' IndexInText='332' ItemLength='162' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
				<RegularItems>
					<CodeBlockStartMarker Name='BEGIN' Id='637795220121739617' IndexInText='332' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Braces Id='637795220121780451' IndexInText='343' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<Literal Id='637795220121774327' IndexInText='343' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='println' Id='637795220121773014' IndexInText='343' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<OpeningRoundBrace Name='(' Id='637795220121780489' IndexInText='350' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Literal Id='637795220121787336' IndexInText='351' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='x' Id='637795220121787318' IndexInText='351' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<ClosingRoundBrace Name=')' Id='637795220121802717' IndexInText='352' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<Literal Id='637795220121787336' IndexInText='351' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
						</Children>
						<OtherProperties>
							<NamedExpressionItem Id='637795220121774327' IndexInText='343' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							<OpeningBraceInfo Name='(' Id='637795220121780489' IndexInText='350' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=')' Id='637795220121802717' IndexInText='352' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Parameters ObjectId='11' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220121787336' IndexInText='351' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Braces>
					<ExpressionSeparator Name=';' Id='637795220121806327' IndexInText='353' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Braces Id='637795220121806565' IndexInText='355' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<Literal Id='637795220121806510' IndexInText='355' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='println' Id='637795220121806502' IndexInText='355' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Literal>
							<OpeningRoundBrace Name='(' Id='637795220121806569' IndexInText='362' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<BinaryOperator Name='+' Priority='30' Id='637795220121814363' IndexInText='363' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
								<Operand1.Literal Id='637795220121806779' IndexInText='363' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
									<NameExpressionItem Name='x' Id='637795220121806773' IndexInText='363' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</Operand1.Literal>
								<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637795220121808389' IndexInText='364' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
									<OperatorNameParts>
										<Name Name='+' Id='637795220121806860' IndexInText='364' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</OperatorNameParts>
								</OperatorInfo>
								<Operand2.Literal Id='637795220121809730' IndexInText='365' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
									<NameExpressionItem Name='y' Id='637795220121809716' IndexInText='365' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</Operand2.Literal>
							</BinaryOperator>
							<ClosingRoundBrace Name=')' Id='637795220121842385' IndexInText='366' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<BinaryOperator Name='+' Priority='30' Id='637795220121814363' IndexInText='363' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
						</Children>
						<OtherProperties>
							<NamedExpressionItem Id='637795220121806510' IndexInText='355' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
							<OpeningBraceInfo Name='(' Id='637795220121806569' IndexInText='362' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<ClosingBraceInfo Name=')' Id='637795220121842385' IndexInText='366' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Parameters ObjectId='25' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name='+' Priority='30' Id='637795220121814363' IndexInText='363' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Braces>
					<CodeBlockEndMarker Name='END' Id='637795220121842588' IndexInText='491' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Children>
					<Braces Id='637795220121780451' IndexInText='343' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
					<Braces Id='637795220121806565' IndexInText='355' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
				</Children>
				<OtherProperties>
					<CodeBlockStartMarker Name='BEGIN' Id='637795220121739617' IndexInText='332' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<CodeBlockEndMarker Name='END' Id='637795220121842588' IndexInText='491' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</OtherProperties>
			</CodeBlock>
			<Literal Id='637795220121845250' IndexInText='716' ItemLength='192' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
				<NameExpressionItem Name='DOG' Id='637795220121845231' IndexInText='729' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				<AppliedKeywords>
					<Keyword Name='PUBLIC' Id='637795220121844546' IndexInText='716' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
						<LanguageKeywordInfo ObjectId='30' Id='637783669980406419' Keyword='public' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
					</Keyword>
					<Keyword Name='Class' Id='637795220121844703' IndexInText='723' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
						<LanguageKeywordInfo ObjectId='32' Id='637783670388745402' Keyword='class' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
					</Keyword>
				</AppliedKeywords>
				<Postfixes>
					<CodeBlock Id='637795220121847369' IndexInText='735' ItemLength='173' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
						<RegularItems>
							<CodeBlockStartMarker Name='BEGIN' Id='637795220121847354' IndexInText='735' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<Braces Id='637795220121850473' IndexInText='818' ItemLength='18' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
								<AppliedKeywords>
									<Keyword Name='PUBLIc' Id='637795220121849641' IndexInText='818' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
										<LanguageKeywordInfo ObjectId='30' Id='637783669980406419' Keyword='public' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
									</Keyword>
									<Keyword Name='static' Id='637795220121849666' IndexInText='825' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
										<LanguageKeywordInfo ObjectId='38' Id='637789181872233115' Keyword='static' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
									</Keyword>
								</AppliedKeywords>
								<RegularItems>
									<Literal Id='637795220121849843' IndexInText='832' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='F1' Id='637795220121849836' IndexInText='832' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Literal>
									<OpeningRoundBrace Name='(' Id='637795220121850494' IndexInText='834' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ClosingRoundBrace Name=')' Id='637795220121850578' IndexInText='835' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<OtherProperties>
									<NamedExpressionItem Id='637795220121849843' IndexInText='832' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
									<OpeningBraceInfo Name='(' Id='637795220121850494' IndexInText='834' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<ClosingBraceInfo Name=')' Id='637795220121850578' IndexInText='835' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Parameters ObjectId='43' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'/>
								</OtherProperties>
							</Braces>
							<ExpressionSeparator Name=';' Id='637795220121850632' IndexInText='836' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='end' Id='637795220121850685' IndexInText='905' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<Braces Id='637795220121850473' IndexInText='818' ItemLength='18' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
						</Children>
						<OtherProperties>
							<CodeBlockStartMarker Name='BEGIN' Id='637795220121847354' IndexInText='735' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='end' Id='637795220121850685' IndexInText='905' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OtherProperties>
					</CodeBlock>
				</Postfixes>
			</Literal>
			<BinaryOperator Name='=' Priority='2000' Id='637795220121861012' IndexInText='968' ItemLength='16' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
				<Operand1.Literal Id='637795220121850845' IndexInText='968' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
					<NameExpressionItem Name='x' Id='637795220121850840' IndexInText='972' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<AppliedKeywords>
						<Keyword Name='VaR' Id='637795220121850722' IndexInText='968' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
							<LanguageKeywordInfo ObjectId='50' Id='637781064051574641' Keyword='var' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
						</Keyword>
					</AppliedKeywords>
				</Operand1.Literal>
				<OperatorInfo OperatorType='BinaryOperator' Name='=' Priority='2000' Id='637795220121850954' IndexInText='973' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
					<OperatorNameParts>
						<Name Name='=' Id='637795220121850946' IndexInText='973' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</OperatorNameParts>
				</OperatorInfo>
				<Operand2.Custom Id='637795220121859232' IndexInText='974' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.Custom.IKeywordBasedCustomExpressionItem'>
					<RegularItems>
						<Keyword Name='::PRagma' Id='637795220121851004' IndexInText='974' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
							<LanguageKeywordInfo ObjectId='55' Id='637781063127646487' Keyword='::pragma' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
						</Keyword>
						<Name Name='y' Id='637795220121857068' IndexInText='983' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</RegularItems>
					<OtherProperties>
						<LastKeywordExpressionItem Name='::PRagma' Id='637795220121851004' IndexInText='974' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'/>
						<ErrorsPositionDisplayValue value='974' type='System.Int32' />
					</OtherProperties>
				</Operand2.Custom>
			</BinaryOperator>
			<ExpressionSeparator Name=';' Id='637795220121861267' IndexInText='984' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<Braces Id='637795220121861471' IndexInText='989' ItemLength='63' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
				<RegularItems>
					<Literal Id='637795220121861427' IndexInText='989' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='PRintLN' Id='637795220121861421' IndexInText='989' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<OpeningRoundBrace Name='(' Id='637795220121861475' IndexInText='996' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<BinaryOperator Name='&&' Priority='80' Id='637795220121874077' IndexInText='997' ItemLength='54' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.BinaryOperator Name='+' Priority='30' Id='637795220121874036' IndexInText='997' ItemLength='32' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
							<Operand1.ConstantText Id='637795220121865713' IndexInText='997' ItemLength='16' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
								<NameExpressionItem Name='"X IS NOT NULL="' Id='637795220121865694' IndexInText='997' ItemLength='16' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</Operand1.ConstantText>
							<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637795220121865896' IndexInText='1014' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
								<OperatorNameParts>
									<Name Name='+' Id='637795220121865884' IndexInText='1014' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</OperatorNameParts>
							</OperatorInfo>
							<Operand2.PostfixUnaryOperator Name='IS NOT NULL' Priority='1' Id='637795220121874059' IndexInText='1016' ItemLength='13' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
								<Operand1.Literal Id='637795220121867002' IndexInText='1016' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
									<NameExpressionItem Name='X' Id='637795220121866987' IndexInText='1016' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</Operand1.Literal>
								<OperatorInfo OperatorType='PostfixUnaryOperator' Name='IS NOT NULL' Priority='1' Id='637795220121867137' IndexInText='1018' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
									<OperatorNameParts>
										<Name Name='Is' Id='637795220121867066' IndexInText='1018' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										<Name Name='noT' Id='637795220121867119' IndexInText='1021' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										<Name Name='Null' Id='637795220121867131' IndexInText='1025' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</OperatorNameParts>
								</OperatorInfo>
							</Operand2.PostfixUnaryOperator>
						</Operand1.BinaryOperator>
						<OperatorInfo OperatorType='BinaryOperator' Name='&&' Priority='80' Id='637795220121867257' IndexInText='1030' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<Name Name='&&' Id='637795220121867251' IndexInText='1030' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							</OperatorNameParts>
						</OperatorInfo>
						<Operand2.PostfixUnaryOperator Name='IS NULL' Priority='1' Id='637795220121874090' IndexInText='1033' ItemLength='18' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
							<Operand1.Custom Id='637795220121867591' IndexInText='1033' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.Custom.IKeywordBasedCustomExpressionItem'>
								<RegularItems>
									<Keyword Name='::pRAGMA' Id='637795220121867362' IndexInText='1033' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
										<LanguageKeywordInfo ObjectId='55' Id='637781063127646487' Keyword='::pragma' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
									</Keyword>
									<Name Name='y' Id='637795220121867584' IndexInText='1042' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</RegularItems>
								<OtherProperties>
									<LastKeywordExpressionItem Name='::pRAGMA' Id='637795220121867362' IndexInText='1033' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'/>
									<ErrorsPositionDisplayValue value='1033' type='System.Int32' />
								</OtherProperties>
							</Operand1.Custom>
							<OperatorInfo OperatorType='PostfixUnaryOperator' Name='IS NULL' Priority='1' Id='637795220121873835' IndexInText='1044' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
								<OperatorNameParts>
									<Name Name='is' Id='637795220121872752' IndexInText='1044' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									<Name Name='NULL' Id='637795220121873813' IndexInText='1047' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
								</OperatorNameParts>
							</OperatorInfo>
						</Operand2.PostfixUnaryOperator>
					</BinaryOperator>
					<ClosingRoundBrace Name=')' Id='637795220121875138' IndexInText='1051' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Children>
					<BinaryOperator Name='&&' Priority='80' Id='637795220121874077' IndexInText='997' ItemLength='54' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
				</Children>
				<OtherProperties>
					<NamedExpressionItem Id='637795220121861427' IndexInText='989' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					<OpeningBraceInfo Name='(' Id='637795220121861475' IndexInText='996' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ClosingBraceInfo Name=')' Id='637795220121875138' IndexInText='1051' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Parameters ObjectId='85' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Name='&&' Priority='80' Id='637795220121874077' IndexInText='997' ItemLength='54' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
					</Parameters>
				</OtherProperties>
			</Braces>
			<ExpressionSeparator Name=';' Id='637795220121875226' IndexInText='1052' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
			<Braces Id='637795220121875401' IndexInText='1057' ItemLength='445' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
				<RegularItems>
					<Literal Id='637795220121875355' IndexInText='1057' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='f1' Id='637795220121875349' IndexInText='1057' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<OpeningRoundBrace Name='(' Id='637795220121875405' IndexInText='1059' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Literal Id='637795220121875518' IndexInText='1060' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='x1' Id='637795220121875513' IndexInText='1060' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<Comma Name=',' Id='637795220121876310' IndexInText='1062' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Literal Id='637795220121876485' IndexInText='1064' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
						<NameExpressionItem Name='y1' Id='637795220121876479' IndexInText='1064' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					</Literal>
					<ClosingRoundBrace Name=')' Id='637795220121876536' IndexInText='1066' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
				</RegularItems>
				<Postfixes>
					<CodeBlock Id='637795220121876587' IndexInText='1069' ItemLength='433' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
						<RegularItems>
							<CodeBlockStartMarker Name='BEGin' Id='637795220121876582' IndexInText='1069' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<PrefixUnaryOperator Name='return' Priority='2147483647' Id='637795220121877250' IndexInText='1386' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
								<OperatorInfo OperatorType='PrefixUnaryOperator' Name='return' Priority='2147483647' Id='637795220121876799' IndexInText='1386' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
									<OperatorNameParts>
										<Name Name='RETurN' Id='637795220121876789' IndexInText='1386' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</OperatorNameParts>
								</OperatorInfo>
								<Operand1.BinaryOperator Name='+' Priority='30' Id='637795220121877263' IndexInText='1393' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
									<Operand1.Literal Id='637795220121876945' IndexInText='1393' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='X1' Id='637795220121876940' IndexInText='1393' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Operand1.Literal>
									<OperatorInfo OperatorType='BinaryOperator' Name='+' Priority='30' Id='637795220121877030' IndexInText='1395' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
										<OperatorNameParts>
											<Name Name='+' Id='637795220121877023' IndexInText='1395' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
										</OperatorNameParts>
									</OperatorInfo>
									<Operand2.Literal Id='637795220121877201' IndexInText='1396' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'>
										<NameExpressionItem Name='Y1' Id='637795220121877196' IndexInText='1396' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
									</Operand2.Literal>
								</Operand1.BinaryOperator>
							</PrefixUnaryOperator>
							<ExpressionSeparator Name=';' Id='637795220121877447' IndexInText='1398' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='enD' Id='637795220121877516' IndexInText='1499' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</RegularItems>
						<Children>
							<PrefixUnaryOperator Name='return' Priority='2147483647' Id='637795220121877250' IndexInText='1386' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
						</Children>
						<OtherProperties>
							<CodeBlockStartMarker Name='BEGin' Id='637795220121876582' IndexInText='1069' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
							<CodeBlockEndMarker Name='enD' Id='637795220121877516' IndexInText='1499' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
						</OtherProperties>
					</CodeBlock>
				</Postfixes>
				<Children>
					<Literal Id='637795220121875518' IndexInText='1060' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					<Literal Id='637795220121876485' IndexInText='1064' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
				</Children>
				<OtherProperties>
					<NamedExpressionItem Id='637795220121875355' IndexInText='1057' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					<OpeningBraceInfo Name='(' Id='637795220121875405' IndexInText='1059' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<ClosingBraceInfo Name=')' Id='637795220121876536' IndexInText='1066' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.INameExpressionItem'/>
					<Parameters ObjectId='111' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220121875518' IndexInText='1060' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637795220121876485' IndexInText='1064' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
					</Parameters>
				</OtherProperties>
			</Braces>
		</RegularItems>
		<Children>
			<CodeBlock Id='637795220121740461' IndexInText='332' ItemLength='162' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'/>
			<Literal Id='637795220121845250' IndexInText='716' ItemLength='192' Interface='UniversalExpressionParser.ExpressionItems.INamedComplexExpressionItem'/>
			<BinaryOperator Name='=' Priority='2000' Id='637795220121861012' IndexInText='968' ItemLength='16' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
			<Braces Id='637795220121861471' IndexInText='989' ItemLength='63' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
			<Braces Id='637795220121875401' IndexInText='1057' ItemLength='445' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
		</Children>
	</ExpressionItemSeries>
	<ParseErrorData ObjectId='112' HasCriticalErrors='False' Interface='UniversalExpressionParser.IParseErrorData'>
		<AllCodeItemErrors ObjectId='113' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICodeItemParseErrorData]'/>
	</ParseErrorData>
	<SortedCommentedOutCodeInfos ObjectId='114' Count='17' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICommentedOutCodeInfo]'>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='115' IsLineComment='True' IndexInText='0' ItemLength='71'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='116' IsLineComment='False' IndexInText='73' ItemLength='59'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='117' IsLineComment='True' IndexInText='136' ItemLength='83'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='118' IsLineComment='True' IndexInText='221' ItemLength='107'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='119' IsLineComment='False' IndexInText='373' ItemLength='116'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='120' IsLineComment='True' IndexInText='498' ItemLength='59'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='121' IsLineComment='False' IndexInText='561' ItemLength='83'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='122' IsLineComment='True' IndexInText='648' ItemLength='66'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='123' IsLineComment='True' IndexInText='741' ItemLength='71'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='124' IsLineComment='True' IndexInText='838' ItemLength='65'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='125' IsLineComment='True' IndexInText='912' ItemLength='54'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='126' IsLineComment='True' IndexInText='1075' ItemLength='71'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='127' IsLineComment='True' IndexInText='1151' ItemLength='65'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='128' IsLineComment='True' IndexInText='1221' ItemLength='65'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='129' IsLineComment='False' IndexInText='1293' ItemLength='86'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='130' IsLineComment='True' IndexInText='1400' ItemLength='97'/>
		<UniversalExpressionParser.ICommentedOutCodeInfo ObjectId='131' IsLineComment='True' IndexInText='1503' ItemLength='69'/>
	</SortedCommentedOutCodeInfos>
	<MainExpressionItem Id='637795220121633296' IndexInText='332' ItemLength='1170' Interface='UniversalExpressionParser.ExpressionItems.IExpressionItemSeries'/>
</UniversalExpressionParser.ExpressionItems.IRootExpressionItemSeries>
```
</details>
