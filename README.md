# UniversalExpressionParser

**NOTE: This is a short summary of UniversalExpressionParser. For more complete documentation reference https://universalexpressionparser.readthedocs.io/en/latest/.

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
     
        public IParseExpressionResult ParseNonVerboseExpression(string expression)
        {
            /*
            The same instance _expressionParser of UniversalExpressionParser.IExpressionParser can be used
            to parse multiple expressions using different instances of UniversalExpressionParser.IExpressionLanguageProvider
            Example:

            var parsedExpression1 = _expressionParser.ParseExpression(_nonVerboseLanguageProvider.LanguageName, "var x=2*y; f1() {++x;} f1();");
            var parsedExpression2 = _expressionParser.ParseExpression(_verboseLanguageProvider.LanguageName, "var x=2*y; f1() BEGIN ++x;END f1();");
            */
            return _expressionParser.ParseExpression(_nonVerboseLanguageProvider.LanguageName, expression, _parseExpressionOptions);
        }
    }
}

```

- Expression is parsed to an instance of **UniversalExpressionParser.IParseExpressionResult** by calling the method  **IParseExpressionResult ParseExpression(string expressionLanguageProviderName, string expressionText, IParseExpressionOptions parseExpressionOptions)** in **UniversalExpressionParser.IExpressionParser**.

- The interface **UniversalExpressionParser.IParseExpressionResult** (i.e., result of parsing the expression) has a property **UniversalExpressionParser.ExpressionItems.IRooxExpressionItem RootExpressionItem { get; }** that stores the root expression item of a tree structure of parsed expression items.

- The code that evaluates the parsed expression can use the following properties in **UniversalExpressionParser.ExpressionItems.IRootExpressionItem** to iterate through all parsed expression items:

   - **IEnumerable&lt;IExpressionItemBase&gt; AllItems { get; }**
   - **IReadOnlyList&lt;IExpressionItemBase&gt; Prefixes { get; }**
   - **IReadOnlyList&lt;IKeywordExpressionItem&gt; AppliedKeywords { get; }**
   - **IReadOnlyList&lt;IExpressionItemBase&gt; RegularItems { get; }**
   - **IReadOnlyList&lt;IExpressionItemBase&gt; Children { get; }**
   - **IReadOnlyList&lt;IExpressionItemBase&gt; Postfixes { get; }**
   - **IReadOnlyList&lt;IExpressionItemBase&gt; Prefixes { get; }**

- All expressions are parsed either to expressions items of type **UniversalExpressionParser.ExpressionItems.IExpressionItemBase** or one of its subclasses for simple expressions or to expressions items of type **UniversalExpressionParser.ExpressionItems.IComplexExpressionItem** (which is a sub-interface of UniversalExpressionParser.ExpressionItems.IExpressionItemBase) or one of its subclasses for expression items that consists of other expression items.

- Some examples simple expression items are: **UniversalExpressionParser.ExpressionItems.ICommaExpressionItem** for commas, **UniversalExpressionParser.ExpressionItems.IOpeningBraceExpressionItem** and **UniversalExpressionParser.ExpressionItems.IClosingBraceExpressionItem** for opening and closing braces "(" and ")"

- Some complex expression items are: **UniversalExpressionParser.ExpressionItems.IBracesExpressionItem** for functions like "f1 (x1, x2)", **UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem** for operators like the binary operator with operands "f1(x)" and "y" in "f1(x) + y".

- All expressions are currently parsed to one of the following expression items (or intances of other sub-interfaces of these interfaces) in namespaces **UniversalExpressionParser.ExpressionItems** and **UniversalExpressionParser.ExpressionItems.Custom**: **ILiteralExpressionItem**, **ILiteralNameExpressionItem**, **IConstantTextExpressionItem**, **IConstantTextValueExpressionItem**, **INumericExpressionItem**, **INumericExpressionValueItem**, **IBracesExpressionItem**, **IOpeningBraceExpressionItem**, **IClosingBraceExpressionItem**, **ICommaExpressionItem**, **ICodeBlockExpressionItem**, **ICustomExpressionItem**, **IKeywordExpressionItem**, **ICodeBlockStartMarkerExpressionItem**, **ICodeBlockEndMarkerExpressionItem**, **ISeparatorCharacterExpressionItem**, **IOperatorExpressionItem**, **IOperatorInfoExpressionItem**, **IKeywordExpressionItem**, **UniversalExpressionParser.ExpressionItems.Custom.ICustomExpressionItem**, **IRootExpressionItem**, **IComplexExpressionItem**, **ITextExpressionItem**, **IExpressionItemBase**. The state of this expression items can be analyzed when evaluating the parsed expression.
 
- Below is the visualized instance of **UniversalExpressionParser.IParseExpressionResult**:

<details> <summary>Click to expand the parsed expression</summary>

```XML
<?xml version="1.0" encoding="utf-8"?>
<UniversalExpressionParser.IParseExpressionResult ObjectId='0' PositionInTextOnCompletion='740' IndexInText='0' ItemLength='736'>
	<ParseErrorData ObjectId='1' HasCriticalErrors='False' Interface='UniversalExpressionParser.IParseErrorData'>
		<AllParseErrorItems ObjectId='2' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.IParseErrorItem]'/>
	</ParseErrorData>
	<SortedCommentedTextData ObjectId='3' Count='3' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ICommentedTextData]'>
		<UniversalExpressionParser.ICommentedTextData ObjectId='4' IsLineComment='False' IndexInText='262' ItemLength='66'/>
		<UniversalExpressionParser.ICommentedTextData ObjectId='5' IsLineComment='False' IndexInText='338' ItemLength='29'/>
		<UniversalExpressionParser.ICommentedTextData ObjectId='6' IsLineComment='True' IndexInText='402' ItemLength='17'/>
	</SortedCommentedTextData>
	<RootExpressionItem Id='637873913526503401' IndexInText='0' ItemLength='736' Interface='UniversalExpressionParser.ExpressionItems.IRootExpressionItem'>
		<RegularItems>
			<IOperatorExpressionItem.Binary Name='=' Priority='2000' Id='637873913526504373' IndexInText='0' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
				<Operand1.ILiteralExpressionItem LiteralName.Text='z' Id='637873913526503743' IndexInText='0' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem'>
					<AppliedKeywords>
						<IKeywordExpressionItem Id='637873913526503619' IndexInText='0' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
							<LanguageKeywordInfo ObjectId='11' Id='637781064051574641' Keyword='var' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
						</IKeywordExpressionItem>
					</AppliedKeywords>
					<RegularItems>
						<ILiteralNameExpressionItem Text='z' Id='637873913526503737' IndexInText='4' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.ILiteralNameExpressionItem'/>
					</RegularItems>
					<OtherProperties>
						<LiteralName Text='z' Id='637873913526503737' IndexInText='4' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.ILiteralNameExpressionItem'/>
					</OtherProperties>
				</Operand1.ILiteralExpressionItem>
				<IOperatorInfoExpressionItem OperatorType='BinaryOperator' Name='=' Priority='2000' Id='637873913526503825' IndexInText='6' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
					<OperatorNameParts>
						<IOperatorNamePartExpressionItem Text='=' Id='637873913526503814' IndexInText='6' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorNamePartExpressionItem'/>
					</OperatorNameParts>
				</IOperatorInfoExpressionItem>
				<Operand2.IOperatorExpressionItem.Binary Name='+' Priority='30' Id='637873913526504395' IndexInText='8' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
					<Operand1.IOperatorExpressionItem.Binary Name='*' Priority='20' Id='637873913526504389' IndexInText='8' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.ILiteralExpressionItem LiteralName.Text='x1' Id='637873913526503938' IndexInText='8' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem'>
							<RegularItems>
								<ILiteralNameExpressionItem Text='x1' Id='637873913526503933' IndexInText='8' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.ILiteralNameExpressionItem'/>
							</RegularItems>
							<OtherProperties>
								<LiteralName Text='x1' Id='637873913526503933' IndexInText='8' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.ILiteralNameExpressionItem'/>
							</OtherProperties>
						</Operand1.ILiteralExpressionItem>
						<IOperatorInfoExpressionItem OperatorType='BinaryOperator' Name='*' Priority='20' Id='637873913526503989' IndexInText='10' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<IOperatorNamePartExpressionItem Text='*' Id='637873913526503984' IndexInText='10' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorNamePartExpressionItem'/>
							</OperatorNameParts>
						</IOperatorInfoExpressionItem>
						<Operand2.ILiteralExpressionItem LiteralName.Text='y1' Id='637873913526504082' IndexInText='11' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem'>
							<RegularItems>
								<ILiteralNameExpressionItem Text='y1' Id='637873913526504077' IndexInText='11' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.ILiteralNameExpressionItem'/>
							</RegularItems>
							<OtherProperties>
								<LiteralName Text='y1' Id='637873913526504077' IndexInText='11' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.ILiteralNameExpressionItem'/>
							</OtherProperties>
						</Operand2.ILiteralExpressionItem>
					</Operand1.IOperatorExpressionItem.Binary>
					<IOperatorInfoExpressionItem OperatorType='BinaryOperator' Name='+' Priority='30' Id='637873913526504127' IndexInText='13' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
						<OperatorNameParts>
							<IOperatorNamePartExpressionItem Text='+' Id='637873913526504122' IndexInText='13' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorNamePartExpressionItem'/>
						</OperatorNameParts>
					</IOperatorInfoExpressionItem>
					<Operand2.IOperatorExpressionItem.Binary Name='*' Priority='20' Id='637873913526504400' IndexInText='14' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
						<Operand1.ILiteralExpressionItem LiteralName.Text='x2' Id='637873913526504215' IndexInText='14' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem'>
							<RegularItems>
								<ILiteralNameExpressionItem Text='x2' Id='637873913526504211' IndexInText='14' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.ILiteralNameExpressionItem'/>
							</RegularItems>
							<OtherProperties>
								<LiteralName Text='x2' Id='637873913526504211' IndexInText='14' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.ILiteralNameExpressionItem'/>
							</OtherProperties>
						</Operand1.ILiteralExpressionItem>
						<IOperatorInfoExpressionItem OperatorType='BinaryOperator' Name='*' Priority='20' Id='637873913526504251' IndexInText='16' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
							<OperatorNameParts>
								<IOperatorNamePartExpressionItem Text='*' Id='637873913526504246' IndexInText='16' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorNamePartExpressionItem'/>
							</OperatorNameParts>
						</IOperatorInfoExpressionItem>
						<Operand2.ILiteralExpressionItem LiteralName.Text='y2' Id='637873913526504345' IndexInText='17' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem'>
							<RegularItems>
								<ILiteralNameExpressionItem Text='y2' Id='637873913526504341' IndexInText='17' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.ILiteralNameExpressionItem'/>
							</RegularItems>
							<OtherProperties>
								<LiteralName Text='y2' Id='637873913526504341' IndexInText='17' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.ILiteralNameExpressionItem'/>
							</OtherProperties>
						</Operand2.ILiteralExpressionItem>
					</Operand2.IOperatorExpressionItem.Binary>
				</Operand2.IOperatorExpressionItem.Binary>
			</IOperatorExpressionItem.Binary>
			<ISeparatorCharacterExpressionItem Text=';' Id='637873913526504463' IndexInText='19' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.ISeparatorCharacterExpressionItem'/>
			<IOperatorExpressionItem.Binary Name='=' Priority='2000' Id='637873913526506006' IndexInText='24' ItemLength='116' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
				<Operand1.ILiteralExpressionItem LiteralName.Text='matrixMultiplicationResult' Id='637873913526504559' IndexInText='24' ItemLength='30' Interface='UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem'>
					<AppliedKeywords>
						<IKeywordExpressionItem Id='637873913526504485' IndexInText='24' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
							<LanguageKeywordInfo ObjectId='11' Id='637781064051574641' Keyword='var' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
						</IKeywordExpressionItem>
					</AppliedKeywords>
					<RegularItems>
						<ILiteralNameExpressionItem Text='matrixMultiplicationResult' Id='637873913526504554' IndexInText='28' ItemLength='26' Interface='UniversalExpressionParser.ExpressionItems.ILiteralNameExpressionItem'/>
					</RegularItems>
					<OtherProperties>
						<LiteralName Text='matrixMultiplicationResult' Id='637873913526504554' IndexInText='28' ItemLength='26' Interface='UniversalExpressionParser.ExpressionItems.ILiteralNameExpressionItem'/>
					</OtherProperties>
				</Operand1.ILiteralExpressionItem>
				<IOperatorInfoExpressionItem OperatorType='BinaryOperator' Name='=' Priority='2000' Id='637873913526504622' IndexInText='55' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
					<OperatorNameParts>
						<IOperatorNamePartExpressionItem Text='=' Id='637873913526504616' IndexInText='55' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorNamePartExpressionItem'/>
					</OperatorNameParts>
				</IOperatorInfoExpressionItem>
				<Operand2.IOperatorExpressionItem.Binary Name='*' Priority='20' Id='637873913526506019' IndexInText='57' ItemLength='83' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
					<Operand1.IBracesExpressionItem Id='637873913526504695' IndexInText='57' ItemLength='40' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<IOpeningBraceExpressionItem Text='[' Id='637873913526504701' IndexInText='57' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOpeningBraceExpressionItem'/>
							<IBracesExpressionItem Id='637873913526504730' IndexInText='58' ItemLength='18' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
								<RegularItems>
									<IOpeningBraceExpressionItem Text='[' Id='637873913526504734' IndexInText='58' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOpeningBraceExpressionItem'/>
									<ILiteralExpressionItem LiteralName.Text='x1_1' Id='637873913526504794' IndexInText='59' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem'>
										<RegularItems>
											<ILiteralNameExpressionItem Text='x1_1' Id='637873913526504790' IndexInText='59' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.ILiteralNameExpressionItem'/>
										</RegularItems>
										<OtherProperties>
											<LiteralName Text='x1_1' Id='637873913526504790' IndexInText='59' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.ILiteralNameExpressionItem'/>
										</OtherProperties>
									</ILiteralExpressionItem>
									<ICommaExpressionItem Text=',' Id='637873913526504824' IndexInText='63' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.ICommaExpressionItem'/>
									<ILiteralExpressionItem LiteralName.Text='x1_2' Id='637873913526504884' IndexInText='65' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem'>
										<RegularItems>
											<ILiteralNameExpressionItem Text='x1_2' Id='637873913526504880' IndexInText='65' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.ILiteralNameExpressionItem'/>
										</RegularItems>
										<OtherProperties>
											<LiteralName Text='x1_2' Id='637873913526504880' IndexInText='65' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.ILiteralNameExpressionItem'/>
										</OtherProperties>
									</ILiteralExpressionItem>
									<ICommaExpressionItem Text=',' Id='637873913526504903' IndexInText='69' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.ICommaExpressionItem'/>
									<ILiteralExpressionItem LiteralName.Text='x1_3' Id='637873913526504961' IndexInText='71' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem'>
										<RegularItems>
											<ILiteralNameExpressionItem Text='x1_3' Id='637873913526504958' IndexInText='71' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.ILiteralNameExpressionItem'/>
										</RegularItems>
										<OtherProperties>
											<LiteralName Text='x1_3' Id='637873913526504958' IndexInText='71' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.ILiteralNameExpressionItem'/>
										</OtherProperties>
									</ILiteralExpressionItem>
									<IClosingBraceExpressionItem Text=']' Id='637873913526504982' IndexInText='75' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IClosingBraceExpressionItem'/>
								</RegularItems>
								<Children>
									<ILiteralExpressionItem LiteralName.Text='x1_1' Id='637873913526504794' IndexInText='59' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem'/>
									<ILiteralExpressionItem LiteralName.Text='x1_2' Id='637873913526504884' IndexInText='65' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem'/>
									<ILiteralExpressionItem LiteralName.Text='x1_3' Id='637873913526504961' IndexInText='71' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem'/>
								</Children>
								<OtherProperties>
									<OpeningBrace Text='[' Id='637873913526504734' IndexInText='58' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOpeningBraceExpressionItem'/>
									<ClosingBrace Text=']' Id='637873913526504982' IndexInText='75' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IClosingBraceExpressionItem'/>
									<NameLiteral value='null' interface='UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem' />
									<Parameters ObjectId='53' Count='3' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase LiteralName.Text='x1_1' Id='637873913526504794' IndexInText='59' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem'/>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase LiteralName.Text='x1_2' Id='637873913526504884' IndexInText='65' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem'/>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase LiteralName.Text='x1_3' Id='637873913526504961' IndexInText='71' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem'/>
									</Parameters>
								</OtherProperties>
							</IBracesExpressionItem>
							<ICommaExpressionItem Text=',' Id='637873913526505011' IndexInText='76' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.ICommaExpressionItem'/>
							<IBracesExpressionItem Id='637873913526505027' IndexInText='78' ItemLength='18' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
								<RegularItems>
									<IOpeningBraceExpressionItem Text='[' Id='637873913526505031' IndexInText='78' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOpeningBraceExpressionItem'/>
									<ILiteralExpressionItem LiteralName.Text='x2_1' Id='637873913526505089' IndexInText='79' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem'>
										<RegularItems>
											<ILiteralNameExpressionItem Text='x2_1' Id='637873913526505085' IndexInText='79' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.ILiteralNameExpressionItem'/>
										</RegularItems>
										<OtherProperties>
											<LiteralName Text='x2_1' Id='637873913526505085' IndexInText='79' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.ILiteralNameExpressionItem'/>
										</OtherProperties>
									</ILiteralExpressionItem>
									<ICommaExpressionItem Text=',' Id='637873913526505109' IndexInText='83' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.ICommaExpressionItem'/>
									<ILiteralExpressionItem LiteralName.Text='x2_2' Id='637873913526505166' IndexInText='85' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem'>
										<RegularItems>
											<ILiteralNameExpressionItem Text='x2_2' Id='637873913526505162' IndexInText='85' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.ILiteralNameExpressionItem'/>
										</RegularItems>
										<OtherProperties>
											<LiteralName Text='x2_2' Id='637873913526505162' IndexInText='85' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.ILiteralNameExpressionItem'/>
										</OtherProperties>
									</ILiteralExpressionItem>
									<ICommaExpressionItem Text=',' Id='637873913526505183' IndexInText='89' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.ICommaExpressionItem'/>
									<ILiteralExpressionItem LiteralName.Text='x2_3' Id='637873913526505241' IndexInText='91' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem'>
										<RegularItems>
											<ILiteralNameExpressionItem Text='x2_3' Id='637873913526505238' IndexInText='91' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.ILiteralNameExpressionItem'/>
										</RegularItems>
										<OtherProperties>
											<LiteralName Text='x2_3' Id='637873913526505238' IndexInText='91' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.ILiteralNameExpressionItem'/>
										</OtherProperties>
									</ILiteralExpressionItem>
									<IClosingBraceExpressionItem Text=']' Id='637873913526505261' IndexInText='95' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IClosingBraceExpressionItem'/>
								</RegularItems>
								<Children>
									<ILiteralExpressionItem LiteralName.Text='x2_1' Id='637873913526505089' IndexInText='79' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem'/>
									<ILiteralExpressionItem LiteralName.Text='x2_2' Id='637873913526505166' IndexInText='85' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem'/>
									<ILiteralExpressionItem LiteralName.Text='x2_3' Id='637873913526505241' IndexInText='91' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem'/>
								</Children>
								<OtherProperties>
									<OpeningBrace Text='[' Id='637873913526505031' IndexInText='78' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOpeningBraceExpressionItem'/>
									<ClosingBrace Text=']' Id='637873913526505261' IndexInText='95' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IClosingBraceExpressionItem'/>
									<NameLiteral value='null' interface='UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem' />
									<Parameters ObjectId='66' Count='3' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase LiteralName.Text='x2_1' Id='637873913526505089' IndexInText='79' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem'/>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase LiteralName.Text='x2_2' Id='637873913526505166' IndexInText='85' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem'/>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase LiteralName.Text='x2_3' Id='637873913526505241' IndexInText='91' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem'/>
									</Parameters>
								</OtherProperties>
							</IBracesExpressionItem>
							<IClosingBraceExpressionItem Text=']' Id='637873913526505280' IndexInText='96' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IClosingBraceExpressionItem'/>
						</RegularItems>
						<Children>
							<IBracesExpressionItem Id='637873913526504730' IndexInText='58' ItemLength='18' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
							<IBracesExpressionItem Id='637873913526505027' IndexInText='78' ItemLength='18' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
						</Children>
						<OtherProperties>
							<OpeningBrace Text='[' Id='637873913526504701' IndexInText='57' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOpeningBraceExpressionItem'/>
							<ClosingBrace Text=']' Id='637873913526505280' IndexInText='96' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IClosingBraceExpressionItem'/>
							<NameLiteral value='null' interface='UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem' />
							<Parameters ObjectId='68' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637873913526504730' IndexInText='58' ItemLength='18' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637873913526505027' IndexInText='78' ItemLength='18' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Operand1.IBracesExpressionItem>
					<IOperatorInfoExpressionItem OperatorType='BinaryOperator' Name='*' Priority='20' Id='637873913526505319' IndexInText='97' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
						<OperatorNameParts>
							<IOperatorNamePartExpressionItem Text='*' Id='637873913526505313' IndexInText='97' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorNamePartExpressionItem'/>
						</OperatorNameParts>
					</IOperatorInfoExpressionItem>
					<Operand2.IBracesExpressionItem Id='637873913526505404' IndexInText='98' ItemLength='42' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<RegularItems>
							<IOpeningBraceExpressionItem Text='[' Id='637873913526505409' IndexInText='98' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOpeningBraceExpressionItem'/>
							<IBracesExpressionItem Id='637873913526505428' IndexInText='99' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
								<RegularItems>
									<IOpeningBraceExpressionItem Text='[' Id='637873913526505432' IndexInText='99' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOpeningBraceExpressionItem'/>
									<ILiteralExpressionItem LiteralName.Text='y1_1' Id='637873913526505489' IndexInText='100' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem'>
										<RegularItems>
											<ILiteralNameExpressionItem Text='y1_1' Id='637873913526505486' IndexInText='100' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.ILiteralNameExpressionItem'/>
										</RegularItems>
										<OtherProperties>
											<LiteralName Text='y1_1' Id='637873913526505486' IndexInText='100' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.ILiteralNameExpressionItem'/>
										</OtherProperties>
									</ILiteralExpressionItem>
									<ICommaExpressionItem Text=',' Id='637873913526505509' IndexInText='104' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.ICommaExpressionItem'/>
									<ILiteralExpressionItem LiteralName.Text='x1_2' Id='637873913526505567' IndexInText='106' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem'>
										<RegularItems>
											<ILiteralNameExpressionItem Text='x1_2' Id='637873913526505563' IndexInText='106' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.ILiteralNameExpressionItem'/>
										</RegularItems>
										<OtherProperties>
											<LiteralName Text='x1_2' Id='637873913526505563' IndexInText='106' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.ILiteralNameExpressionItem'/>
										</OtherProperties>
									</ILiteralExpressionItem>
									<IClosingBraceExpressionItem Text=']' Id='637873913526505584' IndexInText='110' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IClosingBraceExpressionItem'/>
								</RegularItems>
								<Children>
									<ILiteralExpressionItem LiteralName.Text='y1_1' Id='637873913526505489' IndexInText='100' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem'/>
									<ILiteralExpressionItem LiteralName.Text='x1_2' Id='637873913526505567' IndexInText='106' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem'/>
								</Children>
								<OtherProperties>
									<OpeningBrace Text='[' Id='637873913526505432' IndexInText='99' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOpeningBraceExpressionItem'/>
									<ClosingBrace Text=']' Id='637873913526505584' IndexInText='110' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IClosingBraceExpressionItem'/>
									<NameLiteral value='null' interface='UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem' />
									<Parameters ObjectId='81' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase LiteralName.Text='y1_1' Id='637873913526505489' IndexInText='100' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem'/>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase LiteralName.Text='x1_2' Id='637873913526505567' IndexInText='106' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem'/>
									</Parameters>
								</OtherProperties>
							</IBracesExpressionItem>
							<ICommaExpressionItem Text=',' Id='637873913526505604' IndexInText='111' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.ICommaExpressionItem'/>
							<IBracesExpressionItem Id='637873913526505624' IndexInText='113' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
								<RegularItems>
									<IOpeningBraceExpressionItem Text='[' Id='637873913526505628' IndexInText='113' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOpeningBraceExpressionItem'/>
									<ILiteralExpressionItem LiteralName.Text='y2_1' Id='637873913526505680' IndexInText='114' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem'>
										<RegularItems>
											<ILiteralNameExpressionItem Text='y2_1' Id='637873913526505677' IndexInText='114' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.ILiteralNameExpressionItem'/>
										</RegularItems>
										<OtherProperties>
											<LiteralName Text='y2_1' Id='637873913526505677' IndexInText='114' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.ILiteralNameExpressionItem'/>
										</OtherProperties>
									</ILiteralExpressionItem>
									<ICommaExpressionItem Text=',' Id='637873913526505704' IndexInText='118' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.ICommaExpressionItem'/>
									<ILiteralExpressionItem LiteralName.Text='x2_2' Id='637873913526505757' IndexInText='120' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem'>
										<RegularItems>
											<ILiteralNameExpressionItem Text='x2_2' Id='637873913526505753' IndexInText='120' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.ILiteralNameExpressionItem'/>
										</RegularItems>
										<OtherProperties>
											<LiteralName Text='x2_2' Id='637873913526505753' IndexInText='120' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.ILiteralNameExpressionItem'/>
										</OtherProperties>
									</ILiteralExpressionItem>
									<IClosingBraceExpressionItem Text=']' Id='637873913526505778' IndexInText='124' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IClosingBraceExpressionItem'/>
								</RegularItems>
								<Children>
									<ILiteralExpressionItem LiteralName.Text='y2_1' Id='637873913526505680' IndexInText='114' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem'/>
									<ILiteralExpressionItem LiteralName.Text='x2_2' Id='637873913526505757' IndexInText='120' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem'/>
								</Children>
								<OtherProperties>
									<OpeningBrace Text='[' Id='637873913526505628' IndexInText='113' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOpeningBraceExpressionItem'/>
									<ClosingBrace Text=']' Id='637873913526505778' IndexInText='124' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IClosingBraceExpressionItem'/>
									<NameLiteral value='null' interface='UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem' />
									<Parameters ObjectId='91' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase LiteralName.Text='y2_1' Id='637873913526505680' IndexInText='114' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem'/>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase LiteralName.Text='x2_2' Id='637873913526505757' IndexInText='120' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem'/>
									</Parameters>
								</OtherProperties>
							</IBracesExpressionItem>
							<ICommaExpressionItem Text=',' Id='637873913526505797' IndexInText='125' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.ICommaExpressionItem'/>
							<IBracesExpressionItem Id='637873913526505813' IndexInText='127' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
								<RegularItems>
									<IOpeningBraceExpressionItem Text='[' Id='637873913526505817' IndexInText='127' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOpeningBraceExpressionItem'/>
									<ILiteralExpressionItem LiteralName.Text='y3_1' Id='637873913526505873' IndexInText='128' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem'>
										<RegularItems>
											<ILiteralNameExpressionItem Text='y3_1' Id='637873913526505869' IndexInText='128' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.ILiteralNameExpressionItem'/>
										</RegularItems>
										<OtherProperties>
											<LiteralName Text='y3_1' Id='637873913526505869' IndexInText='128' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.ILiteralNameExpressionItem'/>
										</OtherProperties>
									</ILiteralExpressionItem>
									<ICommaExpressionItem Text=',' Id='637873913526505892' IndexInText='132' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.ICommaExpressionItem'/>
									<ILiteralExpressionItem LiteralName.Text='x3_2' Id='637873913526505949' IndexInText='134' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem'>
										<RegularItems>
											<ILiteralNameExpressionItem Text='x3_2' Id='637873913526505946' IndexInText='134' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.ILiteralNameExpressionItem'/>
										</RegularItems>
										<OtherProperties>
											<LiteralName Text='x3_2' Id='637873913526505946' IndexInText='134' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.ILiteralNameExpressionItem'/>
										</OtherProperties>
									</ILiteralExpressionItem>
									<IClosingBraceExpressionItem Text=']' Id='637873913526505967' IndexInText='138' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IClosingBraceExpressionItem'/>
								</RegularItems>
								<Children>
									<ILiteralExpressionItem LiteralName.Text='y3_1' Id='637873913526505873' IndexInText='128' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem'/>
									<ILiteralExpressionItem LiteralName.Text='x3_2' Id='637873913526505949' IndexInText='134' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem'/>
								</Children>
								<OtherProperties>
									<OpeningBrace Text='[' Id='637873913526505817' IndexInText='127' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOpeningBraceExpressionItem'/>
									<ClosingBrace Text=']' Id='637873913526505967' IndexInText='138' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IClosingBraceExpressionItem'/>
									<NameLiteral value='null' interface='UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem' />
									<Parameters ObjectId='101' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase LiteralName.Text='y3_1' Id='637873913526505873' IndexInText='128' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem'/>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase LiteralName.Text='x3_2' Id='637873913526505949' IndexInText='134' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem'/>
									</Parameters>
								</OtherProperties>
							</IBracesExpressionItem>
							<IClosingBraceExpressionItem Text=']' Id='637873913526505987' IndexInText='139' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IClosingBraceExpressionItem'/>
						</RegularItems>
						<Children>
							<IBracesExpressionItem Id='637873913526505428' IndexInText='99' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
							<IBracesExpressionItem Id='637873913526505624' IndexInText='113' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
							<IBracesExpressionItem Id='637873913526505813' IndexInText='127' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
						</Children>
						<OtherProperties>
							<OpeningBrace Text='[' Id='637873913526505409' IndexInText='98' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOpeningBraceExpressionItem'/>
							<ClosingBrace Text=']' Id='637873913526505987' IndexInText='139' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IClosingBraceExpressionItem'/>
							<NameLiteral value='null' interface='UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem' />
							<Parameters ObjectId='103' Count='3' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637873913526505428' IndexInText='99' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637873913526505624' IndexInText='113' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase Id='637873913526505813' IndexInText='127' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Operand2.IBracesExpressionItem>
				</Operand2.IOperatorExpressionItem.Binary>
			</IOperatorExpressionItem.Binary>
			<ISeparatorCharacterExpressionItem Text=';' Id='637873913526506039' IndexInText='140' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.ISeparatorCharacterExpressionItem'/>
			<IBracesExpressionItem NameLiteral.LiteralName.Text='println' Id='637873913526506124' IndexInText='145' ItemLength='35' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
				<RegularItems>
					<ILiteralExpressionItem LiteralName.Text='println' Id='637873913526506100' IndexInText='145' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem'>
						<RegularItems>
							<ILiteralNameExpressionItem Text='println' Id='637873913526506096' IndexInText='145' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.ILiteralNameExpressionItem'/>
						</RegularItems>
						<OtherProperties>
							<LiteralName Text='println' Id='637873913526506096' IndexInText='145' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.ILiteralNameExpressionItem'/>
						</OtherProperties>
					</ILiteralExpressionItem>
					<IOpeningBraceExpressionItem Text='(' Id='637873913526506129' IndexInText='152' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOpeningBraceExpressionItem'/>
					<ILiteralExpressionItem LiteralName.Text='matrixMultiplicationResult' Id='637873913526506218' IndexInText='153' ItemLength='26' Interface='UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem'>
						<RegularItems>
							<ILiteralNameExpressionItem Text='matrixMultiplicationResult' Id='637873913526506214' IndexInText='153' ItemLength='26' Interface='UniversalExpressionParser.ExpressionItems.ILiteralNameExpressionItem'/>
						</RegularItems>
						<OtherProperties>
							<LiteralName Text='matrixMultiplicationResult' Id='637873913526506214' IndexInText='153' ItemLength='26' Interface='UniversalExpressionParser.ExpressionItems.ILiteralNameExpressionItem'/>
						</OtherProperties>
					</ILiteralExpressionItem>
					<IClosingBraceExpressionItem Text=')' Id='637873913526506245' IndexInText='179' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IClosingBraceExpressionItem'/>
				</RegularItems>
				<Children>
					<ILiteralExpressionItem LiteralName.Text='matrixMultiplicationResult' Id='637873913526506218' IndexInText='153' ItemLength='26' Interface='UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem'/>
				</Children>
				<OtherProperties>
					<NameLiteral LiteralName.Text='println' Id='637873913526506100' IndexInText='145' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem'/>
					<OpeningBrace Text='(' Id='637873913526506129' IndexInText='152' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOpeningBraceExpressionItem'/>
					<ClosingBrace Text=')' Id='637873913526506245' IndexInText='179' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IClosingBraceExpressionItem'/>
					<Parameters ObjectId='112' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
						<UniversalExpressionParser.ExpressionItems.IExpressionItemBase LiteralName.Text='matrixMultiplicationResult' Id='637873913526506218' IndexInText='153' ItemLength='26' Interface='UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem'/>
					</Parameters>
				</OtherProperties>
			</IBracesExpressionItem>
			<ISeparatorCharacterExpressionItem Text=';' Id='637873913526506264' IndexInText='180' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.ISeparatorCharacterExpressionItem'/>
			<IOperatorExpressionItem.Binary Name='=&gt;' Priority='1000' Id='637873913526569741' IndexInText='185' ItemLength='237' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
				<Operand1.IOperatorExpressionItem.Binary Name=':' Priority='0' Id='637873913526569724' IndexInText='185' ItemLength='58' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
					<Operand1.IBracesExpressionItem NameLiteral.LiteralName.Text='F1' Id='637873913526506720' IndexInText='185' ItemLength='52' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
						<Prefixes>
							<IBracesExpressionItem Id='637873913526506282' IndexInText='185' ItemLength='9' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
								<RegularItems>
									<IOpeningBraceExpressionItem Text='[' Id='637873913526506286' IndexInText='185' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOpeningBraceExpressionItem'/>
									<ILiteralExpressionItem LiteralName.Text='NotNull' Id='637873913526506345' IndexInText='186' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem'>
										<RegularItems>
											<ILiteralNameExpressionItem Text='NotNull' Id='637873913526506341' IndexInText='186' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.ILiteralNameExpressionItem'/>
										</RegularItems>
										<OtherProperties>
											<LiteralName Text='NotNull' Id='637873913526506341' IndexInText='186' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.ILiteralNameExpressionItem'/>
										</OtherProperties>
									</ILiteralExpressionItem>
									<IClosingBraceExpressionItem Text=']' Id='637873913526506367' IndexInText='193' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IClosingBraceExpressionItem'/>
								</RegularItems>
								<Children>
									<ILiteralExpressionItem LiteralName.Text='NotNull' Id='637873913526506345' IndexInText='186' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem'/>
								</Children>
								<OtherProperties>
									<OpeningBrace Text='[' Id='637873913526506286' IndexInText='185' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOpeningBraceExpressionItem'/>
									<ClosingBrace Text=']' Id='637873913526506367' IndexInText='193' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IClosingBraceExpressionItem'/>
									<NameLiteral value='null' interface='UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem' />
									<Parameters ObjectId='122' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase LiteralName.Text='NotNull' Id='637873913526506345' IndexInText='186' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem'/>
									</Parameters>
								</OtherProperties>
							</IBracesExpressionItem>
							<IBracesExpressionItem Id='637873913526506387' IndexInText='195' ItemLength='25' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
								<RegularItems>
									<IOpeningBraceExpressionItem Text='[' Id='637873913526506400' IndexInText='195' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOpeningBraceExpressionItem'/>
									<IBracesExpressionItem NameLiteral.LiteralName.Text='PublicName' Id='637873913526506476' IndexInText='196' ItemLength='23' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
										<RegularItems>
											<ILiteralExpressionItem LiteralName.Text='PublicName' Id='637873913526506459' IndexInText='196' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem'>
												<RegularItems>
													<ILiteralNameExpressionItem Text='PublicName' Id='637873913526506455' IndexInText='196' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.ILiteralNameExpressionItem'/>
												</RegularItems>
												<OtherProperties>
													<LiteralName Text='PublicName' Id='637873913526506455' IndexInText='196' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.ILiteralNameExpressionItem'/>
												</OtherProperties>
											</ILiteralExpressionItem>
											<IOpeningBraceExpressionItem Text='(' Id='637873913526506481' IndexInText='206' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOpeningBraceExpressionItem'/>
											<IConstantTextExpressionItem TextValue.Text='&quot;Calculate&quot;' TextValue.CSharpText='&quot;Calculate&quot;' Id='637873913526506554' IndexInText='207' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.IConstantTextExpressionItem'>
												<RegularItems>
													<IConstantTextValueExpressionItem Text='&quot;Calculate&quot;' CSharpText='Calculate' Id='637873913526506550' IndexInText='207' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.IConstantTextValueExpressionItem'/>
												</RegularItems>
												<OtherProperties>
													<TextValue Text='&quot;Calculate&quot;' CSharpText='Calculate' Id='637873913526506550' IndexInText='207' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.IConstantTextValueExpressionItem'/>
												</OtherProperties>
											</IConstantTextExpressionItem>
											<IClosingBraceExpressionItem Text=')' Id='637873913526506580' IndexInText='218' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IClosingBraceExpressionItem'/>
										</RegularItems>
										<Children>
											<IConstantTextExpressionItem TextValue.Text='&quot;Calculate&quot;' TextValue.CSharpText='&quot;Calculate&quot;' Id='637873913526506554' IndexInText='207' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.IConstantTextExpressionItem'/>
										</Children>
										<OtherProperties>
											<NameLiteral LiteralName.Text='PublicName' Id='637873913526506459' IndexInText='196' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem'/>
											<OpeningBrace Text='(' Id='637873913526506481' IndexInText='206' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOpeningBraceExpressionItem'/>
											<ClosingBrace Text=')' Id='637873913526506580' IndexInText='218' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IClosingBraceExpressionItem'/>
											<Parameters ObjectId='132' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
												<UniversalExpressionParser.ExpressionItems.IExpressionItemBase TextValue.Text='&quot;Calculate&quot;' TextValue.CSharpText='&quot;Calculate&quot;' Id='637873913526506554' IndexInText='207' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.IConstantTextExpressionItem'/>
											</Parameters>
										</OtherProperties>
									</IBracesExpressionItem>
									<IClosingBraceExpressionItem Text=']' Id='637873913526506604' IndexInText='219' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IClosingBraceExpressionItem'/>
								</RegularItems>
								<Children>
									<IBracesExpressionItem NameLiteral.LiteralName.Text='PublicName' Id='637873913526506476' IndexInText='196' ItemLength='23' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
								</Children>
								<OtherProperties>
									<OpeningBrace Text='[' Id='637873913526506400' IndexInText='195' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOpeningBraceExpressionItem'/>
									<ClosingBrace Text=']' Id='637873913526506604' IndexInText='219' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IClosingBraceExpressionItem'/>
									<NameLiteral value='null' interface='UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem' />
									<Parameters ObjectId='134' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase NameLiteral.LiteralName.Text='PublicName' Id='637873913526506476' IndexInText='196' ItemLength='23' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
									</Parameters>
								</OtherProperties>
							</IBracesExpressionItem>
						</Prefixes>
						<AppliedKeywords>
							<IKeywordExpressionItem Id='637873913526506618' IndexInText='222' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
								<LanguageKeywordInfo ObjectId='136' Id='637783669980406419' Keyword='public' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
							</IKeywordExpressionItem>
						</AppliedKeywords>
						<RegularItems>
							<ILiteralExpressionItem LiteralName.Text='F1' Id='637873913526506688' IndexInText='229' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem'>
								<RegularItems>
									<ILiteralNameExpressionItem Text='F1' Id='637873913526506683' IndexInText='229' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.ILiteralNameExpressionItem'/>
								</RegularItems>
								<OtherProperties>
									<LiteralName Text='F1' Id='637873913526506683' IndexInText='229' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.ILiteralNameExpressionItem'/>
								</OtherProperties>
							</ILiteralExpressionItem>
							<IOpeningBraceExpressionItem Text='(' Id='637873913526506726' IndexInText='231' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOpeningBraceExpressionItem'/>
							<ILiteralExpressionItem LiteralName.Text='x' Id='637873913526506783' IndexInText='232' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem'>
								<RegularItems>
									<ILiteralNameExpressionItem Text='x' Id='637873913526506780' IndexInText='232' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.ILiteralNameExpressionItem'/>
								</RegularItems>
								<OtherProperties>
									<LiteralName Text='x' Id='637873913526506780' IndexInText='232' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.ILiteralNameExpressionItem'/>
								</OtherProperties>
							</ILiteralExpressionItem>
							<ICommaExpressionItem Text=',' Id='637873913526506807' IndexInText='233' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.ICommaExpressionItem'/>
							<ILiteralExpressionItem LiteralName.Text='y' Id='637873913526506861' IndexInText='235' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem'>
								<RegularItems>
									<ILiteralNameExpressionItem Text='y' Id='637873913526506857' IndexInText='235' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.ILiteralNameExpressionItem'/>
								</RegularItems>
								<OtherProperties>
									<LiteralName Text='y' Id='637873913526506857' IndexInText='235' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.ILiteralNameExpressionItem'/>
								</OtherProperties>
							</ILiteralExpressionItem>
							<IClosingBraceExpressionItem Text=')' Id='637873913526506882' IndexInText='236' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IClosingBraceExpressionItem'/>
						</RegularItems>
						<Children>
							<ILiteralExpressionItem LiteralName.Text='x' Id='637873913526506783' IndexInText='232' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem'/>
							<ILiteralExpressionItem LiteralName.Text='y' Id='637873913526506861' IndexInText='235' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem'/>
						</Children>
						<OtherProperties>
							<NameLiteral LiteralName.Text='F1' Id='637873913526506688' IndexInText='229' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem'/>
							<OpeningBrace Text='(' Id='637873913526506726' IndexInText='231' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOpeningBraceExpressionItem'/>
							<ClosingBrace Text=')' Id='637873913526506882' IndexInText='236' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IClosingBraceExpressionItem'/>
							<Parameters ObjectId='146' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase LiteralName.Text='x' Id='637873913526506783' IndexInText='232' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem'/>
								<UniversalExpressionParser.ExpressionItems.IExpressionItemBase LiteralName.Text='y' Id='637873913526506861' IndexInText='235' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem'/>
							</Parameters>
						</OtherProperties>
					</Operand1.IBracesExpressionItem>
					<IOperatorInfoExpressionItem OperatorType='BinaryOperator' Name=':' Priority='0' Id='637873913526506918' IndexInText='238' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
						<OperatorNameParts>
							<IOperatorNamePartExpressionItem Text=':' Id='637873913526506912' IndexInText='238' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorNamePartExpressionItem'/>
						</OperatorNameParts>
					</IOperatorInfoExpressionItem>
					<Operand2.ILiteralExpressionItem LiteralName.Text='int' Id='637873913526507016' IndexInText='240' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem'>
						<RegularItems>
							<ILiteralNameExpressionItem Text='int' Id='637873913526507012' IndexInText='240' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.ILiteralNameExpressionItem'/>
						</RegularItems>
						<OtherProperties>
							<LiteralName Text='int' Id='637873913526507012' IndexInText='240' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.ILiteralNameExpressionItem'/>
						</OtherProperties>
					</Operand2.ILiteralExpressionItem>
				</Operand1.IOperatorExpressionItem.Binary>
				<IOperatorInfoExpressionItem OperatorType='BinaryOperator' Name='=&gt;' Priority='1000' Id='637873913526507072' IndexInText='244' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
					<OperatorNameParts>
						<IOperatorNamePartExpressionItem Text='=&gt;' Id='637873913526507066' IndexInText='244' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.IOperatorNamePartExpressionItem'/>
					</OperatorNameParts>
				</IOperatorInfoExpressionItem>
				<Operand2.ICodeBlockExpressionItem Id='637873913526507150' IndexInText='249' ItemLength='173' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
					<RegularItems>
						<ICodeBlockStartMarkerExpressionItem Text='{' Id='637873913526507145' IndexInText='249' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockStartMarkerExpressionItem'/>
						<IOperatorExpressionItem.Prefix Name='++' Priority='0' Id='637873913526507328' IndexInText='334' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
							<IOperatorInfoExpressionItem OperatorType='PrefixUnaryOperator' Name='++' Priority='0' Id='637873913526507211' IndexInText='334' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
								<OperatorNameParts>
									<IOperatorNamePartExpressionItem Text='++' Id='637873913526507205' IndexInText='334' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.IOperatorNamePartExpressionItem'/>
								</OperatorNameParts>
							</IOperatorInfoExpressionItem>
							<Operand1.ILiteralExpressionItem LiteralName.Text='y' Id='637873913526507288' IndexInText='336' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem'>
								<RegularItems>
									<ILiteralNameExpressionItem Text='y' Id='637873913526507283' IndexInText='336' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.ILiteralNameExpressionItem'/>
								</RegularItems>
								<OtherProperties>
									<LiteralName Text='y' Id='637873913526507283' IndexInText='336' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.ILiteralNameExpressionItem'/>
								</OtherProperties>
							</Operand1.ILiteralExpressionItem>
						</IOperatorExpressionItem.Prefix>
						<ISeparatorCharacterExpressionItem Text=';' Id='637873913526507349' IndexInText='367' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.ISeparatorCharacterExpressionItem'/>
						<IOperatorExpressionItem.Prefix Name='return' Priority='2147483647' Id='637873913526564080' IndexInText='374' ItemLength='26' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
							<IOperatorInfoExpressionItem OperatorType='PrefixUnaryOperator' Name='return' Priority='2147483647' Id='637873913526507385' IndexInText='374' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
								<OperatorNameParts>
									<IOperatorNamePartExpressionItem Text='return' Id='637873913526507379' IndexInText='374' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IOperatorNamePartExpressionItem'/>
								</OperatorNameParts>
							</IOperatorInfoExpressionItem>
							<Operand1.IOperatorExpressionItem.Binary Name='+' Priority='30' Id='637873913526564117' IndexInText='381' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
								<Operand1.IOperatorExpressionItem.Binary Name='+' Priority='30' Id='637873913526564106' IndexInText='381' ItemLength='15' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
									<Operand1.INumericExpressionItem Value.NumericValue='1.3EXP-2.7' Id='637873913526563354' IndexInText='381' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.INumericExpressionItem'>
										<RegularItems>
											<INumericExpressionValueItem NumericValue='1.3EXP-2.7' Id='637873913526563302' IndexInText='381' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.INumericExpressionValueItem'/>
										</RegularItems>
										<OtherProperties>
											<Value NumericValue='1.3EXP-2.7' Id='637873913526563302' IndexInText='381' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.INumericExpressionValueItem'/>
											<SucceededNumericTypeDescriptor ObjectId='168' NumberTypeId='1581134225786' Interface='UniversalExpressionParser.NumericTypeDescriptor'>
												<RegularExpressions ObjectId='169' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[System.String]'>
													<System.String value='^(\d+\.\d+|\d+\.|\.\d+|\d+)(EXP|exp|E|e)[+|-]?(\d+\.\d+|\d+\.|\.\d+|\d+)' />
												</RegularExpressions>
											</SucceededNumericTypeDescriptor>
										</OtherProperties>
									</Operand1.INumericExpressionItem>
									<IOperatorInfoExpressionItem OperatorType='BinaryOperator' Name='+' Priority='30' Id='637873913526563579' IndexInText='391' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
										<OperatorNameParts>
											<IOperatorNamePartExpressionItem Text='+' Id='637873913526563564' IndexInText='391' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorNamePartExpressionItem'/>
										</OperatorNameParts>
									</IOperatorInfoExpressionItem>
									<Operand2.IOperatorExpressionItem.Prefix Name='++' Priority='0' Id='637873913526564111' IndexInText='393' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
										<IOperatorInfoExpressionItem OperatorType='PrefixUnaryOperator' Name='++' Priority='0' Id='637873913526563664' IndexInText='393' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
											<OperatorNameParts>
												<IOperatorNamePartExpressionItem Text='++' Id='637873913526563657' IndexInText='393' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.IOperatorNamePartExpressionItem'/>
											</OperatorNameParts>
										</IOperatorInfoExpressionItem>
										<Operand1.ILiteralExpressionItem LiteralName.Text='x' Id='637873913526563766' IndexInText='395' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem'>
											<RegularItems>
												<ILiteralNameExpressionItem Text='x' Id='637873913526563762' IndexInText='395' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.ILiteralNameExpressionItem'/>
											</RegularItems>
											<OtherProperties>
												<LiteralName Text='x' Id='637873913526563762' IndexInText='395' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.ILiteralNameExpressionItem'/>
											</OtherProperties>
										</Operand1.ILiteralExpressionItem>
									</Operand2.IOperatorExpressionItem.Prefix>
								</Operand1.IOperatorExpressionItem.Binary>
								<IOperatorInfoExpressionItem OperatorType='BinaryOperator' Name='+' Priority='30' Id='637873913526563832' IndexInText='396' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
									<OperatorNameParts>
										<IOperatorNamePartExpressionItem Text='+' Id='637873913526563826' IndexInText='396' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorNamePartExpressionItem'/>
									</OperatorNameParts>
								</IOperatorInfoExpressionItem>
								<Operand2.IOperatorExpressionItem.Binary Name='*' Priority='20' Id='637873913526564124' IndexInText='397' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
									<Operand1.ILiteralExpressionItem LiteralName.Text='y' Id='637873913526563921' IndexInText='397' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem'>
										<RegularItems>
											<ILiteralNameExpressionItem Text='y' Id='637873913526563916' IndexInText='397' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.ILiteralNameExpressionItem'/>
										</RegularItems>
										<OtherProperties>
											<LiteralName Text='y' Id='637873913526563916' IndexInText='397' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.ILiteralNameExpressionItem'/>
										</OtherProperties>
									</Operand1.ILiteralExpressionItem>
									<IOperatorInfoExpressionItem OperatorType='BinaryOperator' Name='*' Priority='20' Id='637873913526563958' IndexInText='398' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
										<OperatorNameParts>
											<IOperatorNamePartExpressionItem Text='*' Id='637873913526563952' IndexInText='398' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorNamePartExpressionItem'/>
										</OperatorNameParts>
									</IOperatorInfoExpressionItem>
									<Operand2.ILiteralExpressionItem LiteralName.Text='z' Id='637873913526564047' IndexInText='399' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem'>
										<RegularItems>
											<ILiteralNameExpressionItem Text='z' Id='637873913526564043' IndexInText='399' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.ILiteralNameExpressionItem'/>
										</RegularItems>
										<OtherProperties>
											<LiteralName Text='z' Id='637873913526564043' IndexInText='399' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.ILiteralNameExpressionItem'/>
										</OtherProperties>
									</Operand2.ILiteralExpressionItem>
								</Operand2.IOperatorExpressionItem.Binary>
							</Operand1.IOperatorExpressionItem.Binary>
						</IOperatorExpressionItem.Prefix>
						<ISeparatorCharacterExpressionItem Text=';' Id='637873913526569607' IndexInText='400' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.ISeparatorCharacterExpressionItem'/>
						<ICodeBlockEndMarkerExpressionItem Text='}' Id='637873913526569683' IndexInText='421' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockEndMarkerExpressionItem'/>
					</RegularItems>
					<Children>
						<IOperatorExpressionItem.Prefix Name='++' Priority='0' Id='637873913526507328' IndexInText='334' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
						<IOperatorExpressionItem.Prefix Name='return' Priority='2147483647' Id='637873913526564080' IndexInText='374' ItemLength='26' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
					</Children>
					<OtherProperties>
						<CodeBlockStartMarker Text='{' Id='637873913526507145' IndexInText='249' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockStartMarkerExpressionItem'/>
						<CodeBlockEndMarker Text='}' Id='637873913526569683' IndexInText='421' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockEndMarkerExpressionItem'/>
					</OtherProperties>
				</Operand2.ICodeBlockExpressionItem>
			</IOperatorExpressionItem.Binary>
			<ILiteralExpressionItem LiteralName.Text='Animal' Id='637873913526570881' IndexInText='426' ItemLength='218' Interface='UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem'>
				<Prefixes>
					<ICustomExpressionItem Id='637873913526570341' IndexInText='426' ItemLength='60' Interface='UniversalExpressionParser.ExpressionItems.Custom.IKeywordBasedCustomExpressionItem'>
						<AppliedKeywords>
							<IKeywordExpressionItem Id='637873913526569778' IndexInText='426' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
								<LanguageKeywordInfo ObjectId='136' Id='637783669980406419' Keyword='public' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
							</IKeywordExpressionItem>
							<IKeywordExpressionItem Id='637873913526569798' IndexInText='433' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
								<LanguageKeywordInfo ObjectId='192' Id='637793546145647499' Keyword='abstract' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
							</IKeywordExpressionItem>
							<IKeywordExpressionItem Id='637873913526569802' IndexInText='442' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
								<LanguageKeywordInfo ObjectId='194' Id='637783670388745402' Keyword='class' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
							</IKeywordExpressionItem>
						</AppliedKeywords>
						<RegularItems>
							<IKeywordExpressionItem Id='637873913526569815' IndexInText='448' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
								<LanguageKeywordInfo ObjectId='196' Id='637781063212876967' Keyword='::metadata' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
							</IKeywordExpressionItem>
							<ICodeBlockExpressionItem Id='637873913526569903' IndexInText='458' ItemLength='28' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
								<RegularItems>
									<ICodeBlockStartMarkerExpressionItem Text='{' Id='637873913526569897' IndexInText='458' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockStartMarkerExpressionItem'/>
									<IOperatorExpressionItem.Binary Name=':' Priority='0' Id='637873913526570310' IndexInText='459' ItemLength='26' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
										<Operand1.ILiteralExpressionItem LiteralName.Text='description' Id='637873913526570108' IndexInText='459' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem'>
											<RegularItems>
												<ILiteralNameExpressionItem Text='description' Id='637873913526570102' IndexInText='459' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.ILiteralNameExpressionItem'/>
											</RegularItems>
											<OtherProperties>
												<LiteralName Text='description' Id='637873913526570102' IndexInText='459' ItemLength='11' Interface='UniversalExpressionParser.ExpressionItems.ILiteralNameExpressionItem'/>
											</OtherProperties>
										</Operand1.ILiteralExpressionItem>
										<IOperatorInfoExpressionItem OperatorType='BinaryOperator' Name=':' Priority='0' Id='637873913526570152' IndexInText='470' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
											<OperatorNameParts>
												<IOperatorNamePartExpressionItem Text=':' Id='637873913526570144' IndexInText='470' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorNamePartExpressionItem'/>
											</OperatorNameParts>
										</IOperatorInfoExpressionItem>
										<Operand2.IConstantTextExpressionItem TextValue.Text='&quot;Demo prefix&quot;' TextValue.CSharpText='&quot;Demo prefix&quot;' Id='637873913526570277' IndexInText='472' ItemLength='13' Interface='UniversalExpressionParser.ExpressionItems.IConstantTextExpressionItem'>
											<RegularItems>
												<IConstantTextValueExpressionItem Text='&quot;Demo prefix&quot;' CSharpText='Demo prefix' Id='637873913526570271' IndexInText='472' ItemLength='13' Interface='UniversalExpressionParser.ExpressionItems.IConstantTextValueExpressionItem'/>
											</RegularItems>
											<OtherProperties>
												<TextValue Text='&quot;Demo prefix&quot;' CSharpText='Demo prefix' Id='637873913526570271' IndexInText='472' ItemLength='13' Interface='UniversalExpressionParser.ExpressionItems.IConstantTextValueExpressionItem'/>
											</OtherProperties>
										</Operand2.IConstantTextExpressionItem>
									</IOperatorExpressionItem.Binary>
									<ICodeBlockEndMarkerExpressionItem Text='}' Id='637873913526570332' IndexInText='485' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockEndMarkerExpressionItem'/>
								</RegularItems>
								<Children>
									<IOperatorExpressionItem.Binary Name=':' Priority='0' Id='637873913526570310' IndexInText='459' ItemLength='26' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
								</Children>
								<OtherProperties>
									<CodeBlockStartMarker Text='{' Id='637873913526569897' IndexInText='458' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockStartMarkerExpressionItem'/>
									<CodeBlockEndMarker Text='}' Id='637873913526570332' IndexInText='485' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockEndMarkerExpressionItem'/>
								</OtherProperties>
							</ICodeBlockExpressionItem>
						</RegularItems>
						<OtherProperties>
							<LastKeywordExpressionItem Id='637873913526569815' IndexInText='448' ItemLength='10' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'/>
							<ErrorsPositionDisplayValue value='448' type='System.Int32' />
						</OtherProperties>
					</ICustomExpressionItem>
					<ICustomExpressionItem Id='637873913526570778' IndexInText='487' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.Custom.IKeywordBasedCustomExpressionItem'>
						<RegularItems>
							<IKeywordExpressionItem Id='637873913526570381' IndexInText='487' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
								<LanguageKeywordInfo ObjectId='209' Id='637781062811583137' Keyword='::types' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
							</IKeywordExpressionItem>
							<IBracesExpressionItem Id='637873913526570414' IndexInText='494' ItemLength='12' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
								<RegularItems>
									<IOpeningBraceExpressionItem Text='[' Id='637873913526570420' IndexInText='494' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOpeningBraceExpressionItem'/>
									<ILiteralExpressionItem LiteralName.Text='T1' Id='637873913526570533' IndexInText='495' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem'>
										<RegularItems>
											<ILiteralNameExpressionItem Text='T1' Id='637873913526570528' IndexInText='495' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.ILiteralNameExpressionItem'/>
										</RegularItems>
										<OtherProperties>
											<LiteralName Text='T1' Id='637873913526570528' IndexInText='495' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.ILiteralNameExpressionItem'/>
										</OtherProperties>
									</ILiteralExpressionItem>
									<ICommaExpressionItem Text=',' Id='637873913526570564' IndexInText='497' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.ICommaExpressionItem'/>
									<ILiteralExpressionItem LiteralName.Text='T2' Id='637873913526570624' IndexInText='499' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem'>
										<RegularItems>
											<ILiteralNameExpressionItem Text='T2' Id='637873913526570620' IndexInText='499' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.ILiteralNameExpressionItem'/>
										</RegularItems>
										<OtherProperties>
											<LiteralName Text='T2' Id='637873913526570620' IndexInText='499' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.ILiteralNameExpressionItem'/>
										</OtherProperties>
									</ILiteralExpressionItem>
									<ICommaExpressionItem Text=',' Id='637873913526570657' IndexInText='501' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.ICommaExpressionItem'/>
									<ILiteralExpressionItem LiteralName.Text='T3' Id='637873913526570743' IndexInText='503' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem'>
										<RegularItems>
											<ILiteralNameExpressionItem Text='T3' Id='637873913526570739' IndexInText='503' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.ILiteralNameExpressionItem'/>
										</RegularItems>
										<OtherProperties>
											<LiteralName Text='T3' Id='637873913526570739' IndexInText='503' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.ILiteralNameExpressionItem'/>
										</OtherProperties>
									</ILiteralExpressionItem>
									<IClosingBraceExpressionItem Text=']' Id='637873913526570766' IndexInText='505' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IClosingBraceExpressionItem'/>
								</RegularItems>
								<Children>
									<ILiteralExpressionItem LiteralName.Text='T1' Id='637873913526570533' IndexInText='495' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem'/>
									<ILiteralExpressionItem LiteralName.Text='T2' Id='637873913526570624' IndexInText='499' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem'/>
									<ILiteralExpressionItem LiteralName.Text='T3' Id='637873913526570743' IndexInText='503' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem'/>
								</Children>
								<OtherProperties>
									<OpeningBrace Text='[' Id='637873913526570420' IndexInText='494' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOpeningBraceExpressionItem'/>
									<ClosingBrace Text=']' Id='637873913526570766' IndexInText='505' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IClosingBraceExpressionItem'/>
									<NameLiteral value='null' interface='UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem' />
									<Parameters ObjectId='221' Count='3' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase LiteralName.Text='T1' Id='637873913526570533' IndexInText='495' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem'/>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase LiteralName.Text='T2' Id='637873913526570624' IndexInText='499' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem'/>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase LiteralName.Text='T3' Id='637873913526570743' IndexInText='503' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem'/>
									</Parameters>
								</OtherProperties>
							</IBracesExpressionItem>
						</RegularItems>
						<OtherProperties>
							<LastKeywordExpressionItem Id='637873913526570381' IndexInText='487' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'/>
							<ErrorsPositionDisplayValue value='487' type='System.Int32' />
						</OtherProperties>
					</ICustomExpressionItem>
				</Prefixes>
				<RegularItems>
					<ILiteralNameExpressionItem Text='Animal' Id='637873913526570876' IndexInText='507' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.ILiteralNameExpressionItem'/>
				</RegularItems>
				<Postfixes>
					<ICustomExpressionItem Id='637873913526570928' IndexInText='518' ItemLength='46' Interface='UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IWhereCustomExpressionItem'>
						<RegularItems>
							<IComplexExpressionItem Id='637873913526570934' IndexInText='518' ItemLength='16' Interface='UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem'>
								<RegularItems>
									<IKeywordExpressionItem Id='637873913526570900' IndexInText='518' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
										<LanguageKeywordInfo ObjectId='226' Id='637781062905654923' Keyword='where' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
									</IKeywordExpressionItem>
									<ITextExpressionItem Text='T1' Id='637873913526570985' IndexInText='524' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.ITextExpressionItem'/>
									<ITextExpressionItem Text=':' Id='637873913526570994' IndexInText='526' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.ITextExpressionItem'/>
									<ILiteralExpressionItem LiteralName.Text='IType1' Id='637873913526571010' IndexInText='528' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem'>
										<RegularItems>
											<ILiteralNameExpressionItem Text='IType1' Id='637873913526571002' IndexInText='528' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.ILiteralNameExpressionItem'/>
										</RegularItems>
										<OtherProperties>
											<LiteralName Text='IType1' Id='637873913526571002' IndexInText='528' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.ILiteralNameExpressionItem'/>
										</OtherProperties>
									</ILiteralExpressionItem>
								</RegularItems>
								<OtherProperties>
									<WhereKeyword Id='637873913526570900' IndexInText='518' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'/>
									<TypeName Text='T1' Id='637873913526570985' IndexInText='524' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.ITextExpressionItem'/>
									<TypeConstraints ObjectId='231' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase LiteralName.Text='IType1' Id='637873913526571010' IndexInText='528' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem'/>
									</TypeConstraints>
								</OtherProperties>
							</IComplexExpressionItem>
							<IComplexExpressionItem Id='637873913526571034' IndexInText='535' ItemLength='20' Interface='UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem'>
								<RegularItems>
									<IKeywordExpressionItem Id='637873913526571030' IndexInText='535' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
										<LanguageKeywordInfo ObjectId='226' Id='637781062905654923' Keyword='where' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
									</IKeywordExpressionItem>
									<ITextExpressionItem Text='T2' Id='637873913526571049' IndexInText='541' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.ITextExpressionItem'/>
									<ITextExpressionItem Text=':' Id='637873913526571054' IndexInText='543' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.ITextExpressionItem'/>
									<ILiteralExpressionItem LiteralName.Text='T1' Id='637873913526571064' IndexInText='545' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem'>
										<RegularItems>
											<ILiteralNameExpressionItem Text='T1' Id='637873913526571060' IndexInText='545' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.ILiteralNameExpressionItem'/>
										</RegularItems>
										<OtherProperties>
											<LiteralName Text='T1' Id='637873913526571060' IndexInText='545' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.ILiteralNameExpressionItem'/>
										</OtherProperties>
									</ILiteralExpressionItem>
									<ICommaExpressionItem Text=',' Id='637873913526571070' IndexInText='547' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.ICommaExpressionItem'/>
									<ILiteralExpressionItem LiteralName.Text='IType2' Id='637873913526571082' IndexInText='549' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem'>
										<RegularItems>
											<ILiteralNameExpressionItem Text='IType2' Id='637873913526571079' IndexInText='549' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.ILiteralNameExpressionItem'/>
										</RegularItems>
										<OtherProperties>
											<LiteralName Text='IType2' Id='637873913526571079' IndexInText='549' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.ILiteralNameExpressionItem'/>
										</OtherProperties>
									</ILiteralExpressionItem>
								</RegularItems>
								<OtherProperties>
									<WhereKeyword Id='637873913526571030' IndexInText='535' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'/>
									<TypeName Text='T2' Id='637873913526571049' IndexInText='541' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.ITextExpressionItem'/>
									<TypeConstraints ObjectId='241' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase LiteralName.Text='T1' Id='637873913526571064' IndexInText='545' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem'/>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase LiteralName.Text='IType2' Id='637873913526571082' IndexInText='549' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem'/>
									</TypeConstraints>
								</OtherProperties>
							</IComplexExpressionItem>
							<ITextExpressionItem Text='whereend' Id='637873913526571110' IndexInText='556' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.ITextExpressionItem'/>
						</RegularItems>
						<Children>
							<IComplexExpressionItem Id='637873913526570934' IndexInText='518' ItemLength='16' Interface='UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem'/>
							<IComplexExpressionItem Id='637873913526571034' IndexInText='535' ItemLength='20' Interface='UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem'/>
						</Children>
						<OtherProperties>
							<WhereEndMarker Text='whereend' Id='637873913526571110' IndexInText='556' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.ITextExpressionItem'/>
							<GenericTypeDataExpressionItems ObjectId='243' Count='2' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem]'>
								<UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem Id='637873913526570934' IndexInText='518' ItemLength='16'/>
								<UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem Id='637873913526571034' IndexInText='535' ItemLength='20'/>
							</GenericTypeDataExpressionItems>
							<ErrorsPositionDisplayValue value='518' type='System.Int32' />
						</OtherProperties>
					</ICustomExpressionItem>
					<ICustomExpressionItem Id='637873913526571165' IndexInText='569' ItemLength='25' Interface='UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IWhereCustomExpressionItem'>
						<RegularItems>
							<IComplexExpressionItem Id='637873913526571171' IndexInText='569' ItemLength='16' Interface='UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem'>
								<RegularItems>
									<IKeywordExpressionItem Id='637873913526571141' IndexInText='569' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
										<LanguageKeywordInfo ObjectId='226' Id='637781062905654923' Keyword='where' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
									</IKeywordExpressionItem>
									<ITextExpressionItem Text='T3' Id='637873913526571185' IndexInText='575' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.ITextExpressionItem'/>
									<ITextExpressionItem Text=':' Id='637873913526571191' IndexInText='577' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.ITextExpressionItem'/>
									<ILiteralExpressionItem LiteralName.Text='IType3' Id='637873913526571200' IndexInText='579' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem'>
										<RegularItems>
											<ILiteralNameExpressionItem Text='IType3' Id='637873913526571196' IndexInText='579' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.ILiteralNameExpressionItem'/>
										</RegularItems>
										<OtherProperties>
											<LiteralName Text='IType3' Id='637873913526571196' IndexInText='579' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.ILiteralNameExpressionItem'/>
										</OtherProperties>
									</ILiteralExpressionItem>
								</RegularItems>
								<OtherProperties>
									<WhereKeyword Id='637873913526571141' IndexInText='569' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'/>
									<TypeName Text='T3' Id='637873913526571185' IndexInText='575' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.ITextExpressionItem'/>
									<TypeConstraints ObjectId='251' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
										<UniversalExpressionParser.ExpressionItems.IExpressionItemBase LiteralName.Text='IType3' Id='637873913526571200' IndexInText='579' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem'/>
									</TypeConstraints>
								</OtherProperties>
							</IComplexExpressionItem>
							<ITextExpressionItem Text='whereend' Id='637873913526571211' IndexInText='586' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.ITextExpressionItem'/>
						</RegularItems>
						<Children>
							<IComplexExpressionItem Id='637873913526571171' IndexInText='569' ItemLength='16' Interface='UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem'/>
						</Children>
						<OtherProperties>
							<WhereEndMarker Text='whereend' Id='637873913526571211' IndexInText='586' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.ITextExpressionItem'/>
							<GenericTypeDataExpressionItems ObjectId='253' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem]'>
								<UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions.IGenericTypeDataExpressionItem Id='637873913526571171' IndexInText='569' ItemLength='16'/>
							</GenericTypeDataExpressionItems>
							<ErrorsPositionDisplayValue value='569' type='System.Int32' />
						</OtherProperties>
					</ICustomExpressionItem>
					<ICodeBlockExpressionItem Id='637873913526571239' IndexInText='596' ItemLength='48' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
						<RegularItems>
							<ICodeBlockStartMarkerExpressionItem Text='{' Id='637873913526571235' IndexInText='596' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockStartMarkerExpressionItem'/>
							<IOperatorExpressionItem.Binary Name=':' Priority='0' Id='637873913526571556' IndexInText='607' ItemLength='29' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
								<Operand1.IBracesExpressionItem NameLiteral.LiteralName.Text='Move' Id='637873913526571360' IndexInText='607' ItemLength='22' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
									<AppliedKeywords>
										<IKeywordExpressionItem Id='637873913526571256' IndexInText='607' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
											<LanguageKeywordInfo ObjectId='136' Id='637783669980406419' Keyword='public' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
										</IKeywordExpressionItem>
										<IKeywordExpressionItem Id='637873913526571267' IndexInText='614' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
											<LanguageKeywordInfo ObjectId='192' Id='637793546145647499' Keyword='abstract' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
										</IKeywordExpressionItem>
									</AppliedKeywords>
									<RegularItems>
										<ILiteralExpressionItem LiteralName.Text='Move' Id='637873913526571330' IndexInText='623' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem'>
											<RegularItems>
												<ILiteralNameExpressionItem Text='Move' Id='637873913526571326' IndexInText='623' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.ILiteralNameExpressionItem'/>
											</RegularItems>
											<OtherProperties>
												<LiteralName Text='Move' Id='637873913526571326' IndexInText='623' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.ILiteralNameExpressionItem'/>
											</OtherProperties>
										</ILiteralExpressionItem>
										<IOpeningBraceExpressionItem Text='(' Id='637873913526571367' IndexInText='627' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOpeningBraceExpressionItem'/>
										<IClosingBraceExpressionItem Text=')' Id='637873913526571390' IndexInText='628' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IClosingBraceExpressionItem'/>
									</RegularItems>
									<OtherProperties>
										<NameLiteral LiteralName.Text='Move' Id='637873913526571330' IndexInText='623' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem'/>
										<OpeningBrace Text='(' Id='637873913526571367' IndexInText='627' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOpeningBraceExpressionItem'/>
										<ClosingBrace Text=')' Id='637873913526571390' IndexInText='628' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IClosingBraceExpressionItem'/>
										<Parameters ObjectId='264' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'/>
									</OtherProperties>
								</Operand1.IBracesExpressionItem>
								<IOperatorInfoExpressionItem OperatorType='BinaryOperator' Name=':' Priority='0' Id='637873913526571428' IndexInText='630' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
									<OperatorNameParts>
										<IOperatorNamePartExpressionItem Text=':' Id='637873913526571421' IndexInText='630' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorNamePartExpressionItem'/>
									</OperatorNameParts>
								</IOperatorInfoExpressionItem>
								<Operand2.ILiteralExpressionItem LiteralName.Text='void' Id='637873913526571529' IndexInText='632' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem'>
									<RegularItems>
										<ILiteralNameExpressionItem Text='void' Id='637873913526571524' IndexInText='632' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.ILiteralNameExpressionItem'/>
									</RegularItems>
									<OtherProperties>
										<LiteralName Text='void' Id='637873913526571524' IndexInText='632' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.ILiteralNameExpressionItem'/>
									</OtherProperties>
								</Operand2.ILiteralExpressionItem>
							</IOperatorExpressionItem.Binary>
							<ISeparatorCharacterExpressionItem Text=';' Id='637873913526571576' IndexInText='636' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.ISeparatorCharacterExpressionItem'/>
							<ICodeBlockEndMarkerExpressionItem Text='}' Id='637873913526571595' IndexInText='643' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockEndMarkerExpressionItem'/>
						</RegularItems>
						<Children>
							<IOperatorExpressionItem.Binary Name=':' Priority='0' Id='637873913526571556' IndexInText='607' ItemLength='29' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
						</Children>
						<OtherProperties>
							<CodeBlockStartMarker Text='{' Id='637873913526571235' IndexInText='596' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockStartMarkerExpressionItem'/>
							<CodeBlockEndMarker Text='}' Id='637873913526571595' IndexInText='643' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockEndMarkerExpressionItem'/>
						</OtherProperties>
					</ICodeBlockExpressionItem>
				</Postfixes>
				<OtherProperties>
					<LiteralName Text='Animal' Id='637873913526570876' IndexInText='507' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.ILiteralNameExpressionItem'/>
				</OtherProperties>
			</ILiteralExpressionItem>
			<IOperatorExpressionItem.Binary Name=':' Priority='0' Id='637873913526572513' IndexInText='648' ItemLength='88' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
				<Operand1.ILiteralExpressionItem LiteralName.Text='Dog' Id='637873913526571683' IndexInText='648' ItemLength='16' Interface='UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem'>
					<AppliedKeywords>
						<IKeywordExpressionItem Id='637873913526571611' IndexInText='648' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
							<LanguageKeywordInfo ObjectId='136' Id='637783669980406419' Keyword='public' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
						</IKeywordExpressionItem>
						<IKeywordExpressionItem Id='637873913526571623' IndexInText='655' ItemLength='5' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
							<LanguageKeywordInfo ObjectId='194' Id='637783670388745402' Keyword='class' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
						</IKeywordExpressionItem>
					</AppliedKeywords>
					<RegularItems>
						<ILiteralNameExpressionItem Text='Dog' Id='637873913526571679' IndexInText='661' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.ILiteralNameExpressionItem'/>
					</RegularItems>
					<OtherProperties>
						<LiteralName Text='Dog' Id='637873913526571679' IndexInText='661' ItemLength='3' Interface='UniversalExpressionParser.ExpressionItems.ILiteralNameExpressionItem'/>
					</OtherProperties>
				</Operand1.ILiteralExpressionItem>
				<IOperatorInfoExpressionItem OperatorType='BinaryOperator' Name=':' Priority='0' Id='637873913526571719' IndexInText='665' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
					<OperatorNameParts>
						<IOperatorNamePartExpressionItem Text=':' Id='637873913526571713' IndexInText='665' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorNamePartExpressionItem'/>
					</OperatorNameParts>
				</IOperatorInfoExpressionItem>
				<Operand2.IBracesExpressionItem Id='637873913526571811' IndexInText='667' ItemLength='69' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
					<RegularItems>
						<IOpeningBraceExpressionItem Text='(' Id='637873913526571817' IndexInText='667' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOpeningBraceExpressionItem'/>
						<ILiteralExpressionItem LiteralName.Text='Animal' Id='637873913526571876' IndexInText='668' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem'>
							<RegularItems>
								<ILiteralNameExpressionItem Text='Animal' Id='637873913526571872' IndexInText='668' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.ILiteralNameExpressionItem'/>
							</RegularItems>
							<OtherProperties>
								<LiteralName Text='Animal' Id='637873913526571872' IndexInText='668' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.ILiteralNameExpressionItem'/>
							</OtherProperties>
						</ILiteralExpressionItem>
						<IClosingBraceExpressionItem Text=')' Id='637873913526571901' IndexInText='674' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IClosingBraceExpressionItem'/>
					</RegularItems>
					<Postfixes>
						<ICodeBlockExpressionItem Id='637873913526571923' IndexInText='677' ItemLength='59' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockExpressionItem'>
							<RegularItems>
								<ICodeBlockStartMarkerExpressionItem Text='{' Id='637873913526571920' IndexInText='677' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockStartMarkerExpressionItem'/>
								<IOperatorExpressionItem.Binary Name='=&gt;' Priority='1000' Id='637873913526572464' IndexInText='684' ItemLength='48' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
									<Operand1.IOperatorExpressionItem.Binary Name=':' Priority='0' Id='637873913526572456' IndexInText='684' ItemLength='29' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'>
										<Operand1.IBracesExpressionItem NameLiteral.LiteralName.Text='Move' Id='637873913526572037' IndexInText='684' ItemLength='22' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
											<AppliedKeywords>
												<IKeywordExpressionItem Id='637873913526571937' IndexInText='684' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
													<LanguageKeywordInfo ObjectId='136' Id='637783669980406419' Keyword='public' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
												</IKeywordExpressionItem>
												<IKeywordExpressionItem Id='637873913526571949' IndexInText='691' ItemLength='8' Interface='UniversalExpressionParser.ExpressionItems.IKeywordExpressionItem'>
													<LanguageKeywordInfo ObjectId='290' Id='637793548069818537' Keyword='override' Interface='UniversalExpressionParser.ILanguageKeywordInfo'/>
												</IKeywordExpressionItem>
											</AppliedKeywords>
											<RegularItems>
												<ILiteralExpressionItem LiteralName.Text='Move' Id='637873913526572007' IndexInText='700' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem'>
													<RegularItems>
														<ILiteralNameExpressionItem Text='Move' Id='637873913526572003' IndexInText='700' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.ILiteralNameExpressionItem'/>
													</RegularItems>
													<OtherProperties>
														<LiteralName Text='Move' Id='637873913526572003' IndexInText='700' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.ILiteralNameExpressionItem'/>
													</OtherProperties>
												</ILiteralExpressionItem>
												<IOpeningBraceExpressionItem Text='(' Id='637873913526572043' IndexInText='704' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOpeningBraceExpressionItem'/>
												<IClosingBraceExpressionItem Text=')' Id='637873913526572061' IndexInText='705' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IClosingBraceExpressionItem'/>
											</RegularItems>
											<OtherProperties>
												<NameLiteral LiteralName.Text='Move' Id='637873913526572007' IndexInText='700' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem'/>
												<OpeningBrace Text='(' Id='637873913526572043' IndexInText='704' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOpeningBraceExpressionItem'/>
												<ClosingBrace Text=')' Id='637873913526572061' IndexInText='705' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IClosingBraceExpressionItem'/>
												<Parameters ObjectId='295' Count='0' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'/>
											</OtherProperties>
										</Operand1.IBracesExpressionItem>
										<IOperatorInfoExpressionItem OperatorType='BinaryOperator' Name=':' Priority='0' Id='637873913526572098' IndexInText='707' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
											<OperatorNameParts>
												<IOperatorNamePartExpressionItem Text=':' Id='637873913526572092' IndexInText='707' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOperatorNamePartExpressionItem'/>
											</OperatorNameParts>
										</IOperatorInfoExpressionItem>
										<Operand2.ILiteralExpressionItem LiteralName.Text='void' Id='637873913526572194' IndexInText='709' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem'>
											<RegularItems>
												<ILiteralNameExpressionItem Text='void' Id='637873913526572190' IndexInText='709' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.ILiteralNameExpressionItem'/>
											</RegularItems>
											<OtherProperties>
												<LiteralName Text='void' Id='637873913526572190' IndexInText='709' ItemLength='4' Interface='UniversalExpressionParser.ExpressionItems.ILiteralNameExpressionItem'/>
											</OtherProperties>
										</Operand2.ILiteralExpressionItem>
									</Operand1.IOperatorExpressionItem.Binary>
									<IOperatorInfoExpressionItem OperatorType='BinaryOperator' Name='=&gt;' Priority='1000' Id='637873913526572248' IndexInText='714' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.IOperatorInfoExpressionItem'>
										<OperatorNameParts>
											<IOperatorNamePartExpressionItem Text='=&gt;' Id='637873913526572242' IndexInText='714' ItemLength='2' Interface='UniversalExpressionParser.ExpressionItems.IOperatorNamePartExpressionItem'/>
										</OperatorNameParts>
									</IOperatorInfoExpressionItem>
									<Operand2.IBracesExpressionItem NameLiteral.LiteralName.Text='println' Id='637873913526572346' IndexInText='717' ItemLength='15' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'>
										<RegularItems>
											<ILiteralExpressionItem LiteralName.Text='println' Id='637873913526572325' IndexInText='717' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem'>
												<RegularItems>
													<ILiteralNameExpressionItem Text='println' Id='637873913526572321' IndexInText='717' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.ILiteralNameExpressionItem'/>
												</RegularItems>
												<OtherProperties>
													<LiteralName Text='println' Id='637873913526572321' IndexInText='717' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.ILiteralNameExpressionItem'/>
												</OtherProperties>
											</ILiteralExpressionItem>
											<IOpeningBraceExpressionItem Text='(' Id='637873913526572350' IndexInText='724' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOpeningBraceExpressionItem'/>
											<IConstantTextExpressionItem TextValue.Text='&quot;Jump&quot;' TextValue.CSharpText='&quot;Jump&quot;' Id='637873913526572414' IndexInText='725' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IConstantTextExpressionItem'>
												<RegularItems>
													<IConstantTextValueExpressionItem Text='&quot;Jump&quot;' CSharpText='Jump' Id='637873913526572410' IndexInText='725' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IConstantTextValueExpressionItem'/>
												</RegularItems>
												<OtherProperties>
													<TextValue Text='&quot;Jump&quot;' CSharpText='Jump' Id='637873913526572410' IndexInText='725' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IConstantTextValueExpressionItem'/>
												</OtherProperties>
											</IConstantTextExpressionItem>
											<IClosingBraceExpressionItem Text=')' Id='637873913526572436' IndexInText='731' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IClosingBraceExpressionItem'/>
										</RegularItems>
										<Children>
											<IConstantTextExpressionItem TextValue.Text='&quot;Jump&quot;' TextValue.CSharpText='&quot;Jump&quot;' Id='637873913526572414' IndexInText='725' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IConstantTextExpressionItem'/>
										</Children>
										<OtherProperties>
											<NameLiteral LiteralName.Text='println' Id='637873913526572325' IndexInText='717' ItemLength='7' Interface='UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem'/>
											<OpeningBrace Text='(' Id='637873913526572350' IndexInText='724' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOpeningBraceExpressionItem'/>
											<ClosingBrace Text=')' Id='637873913526572436' IndexInText='731' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IClosingBraceExpressionItem'/>
											<Parameters ObjectId='309' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
												<UniversalExpressionParser.ExpressionItems.IExpressionItemBase TextValue.Text='&quot;Jump&quot;' TextValue.CSharpText='&quot;Jump&quot;' Id='637873913526572414' IndexInText='725' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.IConstantTextExpressionItem'/>
											</Parameters>
										</OtherProperties>
									</Operand2.IBracesExpressionItem>
								</IOperatorExpressionItem.Binary>
								<ISeparatorCharacterExpressionItem Text=';' Id='637873913526572483' IndexInText='732' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.ISeparatorCharacterExpressionItem'/>
								<ICodeBlockEndMarkerExpressionItem Text='}' Id='637873913526572506' IndexInText='735' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockEndMarkerExpressionItem'/>
							</RegularItems>
							<Children>
								<IOperatorExpressionItem.Binary Name='=&gt;' Priority='1000' Id='637873913526572464' IndexInText='684' ItemLength='48' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
							</Children>
							<OtherProperties>
								<CodeBlockStartMarker Text='{' Id='637873913526571920' IndexInText='677' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockStartMarkerExpressionItem'/>
								<CodeBlockEndMarker Text='}' Id='637873913526572506' IndexInText='735' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.ICodeBlockEndMarkerExpressionItem'/>
							</OtherProperties>
						</ICodeBlockExpressionItem>
					</Postfixes>
					<Children>
						<ILiteralExpressionItem LiteralName.Text='Animal' Id='637873913526571876' IndexInText='668' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem'/>
					</Children>
					<OtherProperties>
						<OpeningBrace Text='(' Id='637873913526571817' IndexInText='667' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IOpeningBraceExpressionItem'/>
						<ClosingBrace Text=')' Id='637873913526571901' IndexInText='674' ItemLength='1' Interface='UniversalExpressionParser.ExpressionItems.IClosingBraceExpressionItem'/>
						<NameLiteral value='null' interface='UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem' />
						<Parameters ObjectId='312' Count='1' Interface='System.Collections.Generic.IReadOnlyList`1[UniversalExpressionParser.ExpressionItems.IExpressionItemBase]'>
							<UniversalExpressionParser.ExpressionItems.IExpressionItemBase LiteralName.Text='Animal' Id='637873913526571876' IndexInText='668' ItemLength='6' Interface='UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem'/>
						</Parameters>
					</OtherProperties>
				</Operand2.IBracesExpressionItem>
			</IOperatorExpressionItem.Binary>
		</RegularItems>
		<Children>
			<IOperatorExpressionItem.Binary Name='=' Priority='2000' Id='637873913526504373' IndexInText='0' ItemLength='19' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
			<IOperatorExpressionItem.Binary Name='=' Priority='2000' Id='637873913526506006' IndexInText='24' ItemLength='116' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
			<IBracesExpressionItem NameLiteral.LiteralName.Text='println' Id='637873913526506124' IndexInText='145' ItemLength='35' Interface='UniversalExpressionParser.ExpressionItems.IBracesExpressionItem'/>
			<IOperatorExpressionItem.Binary Name='=&gt;' Priority='1000' Id='637873913526569741' IndexInText='185' ItemLength='237' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
			<ILiteralExpressionItem LiteralName.Text='Animal' Id='637873913526570881' IndexInText='426' ItemLength='218' Interface='UniversalExpressionParser.ExpressionItems.ILiteralExpressionItem'/>
			<IOperatorExpressionItem.Binary Name=':' Priority='0' Id='637873913526572513' IndexInText='648' ItemLength='88' Interface='UniversalExpressionParser.ExpressionItems.IOperatorExpressionItem'/>
		</Children>
	</RootExpressionItem>
</UniversalExpressionParser.IParseExpressionResult>
```
</details>

- The format of valid expressions is defined by properties and methods in interface **UniversalExpressionParser.IExpressionLanguageProvider**. The expression language name **UniversalExpressionParser.IExpressionLanguageProvider.LanguageName** of some instance **UniversalExpressionParser.IExpressionLanguageProvider** is passed as a parameter to method **ParseExpression(...)** in **UniversalExpressionParser.IExpressionParser**, as demonstrated in example above. Most of the properties and methods of this interface are demonstrated in examples in sections below.

- The default abstract implementation of this interface in this package is **UniversalExpressionParser.ExpressionLanguageProviderBase**. In most cases, this abstract class can be extended and abstract methods and properties can be implemented, rather than providing a brand new implementation of **UniversalExpressionParser.IExpressionLanguageProvider**.

- The test project **UniversalExpressionParser.Tests** in git repository has a number of tests for testing successful parsing, as well as tests for testing expressions that result in errors (see section **Error Reporting** below). These tests generate random expressions as well as generate randomly configured instances of **UniversalExpressionParser.IExpressionLanguageProvider** to validate parsing of thousands of all possible languages and expressions (see the test classes **UniversalExpressionParser.Tests.SuccessfulParseTests.ExpressionParserSuccessfulTests** and **UniversalExpressionParser.Tests.ExpressionParseErrorTests.ExpressionParseErrorTests**).

- The demo expressions and tests used to parse the demo expressions in this documentation are in folder **Demos** in test project **UniversalExpressionParser.Tests**. This documentation uses implementations of **UniversalExpressionParser.IExpressionLanguageProvider** in project **UniversalExpressionParser.DemoExpressionLanguageProviders** in **git** repository.

- The parsed expressions in this documentation (i.e., instances of **UniversalExpressionParser.ExpressionItems.IParseExpressionResult**) are visualized into xml texts, that contain values of most properties of the parsed expression. However, to make the files shorter, the visualized xml files do not include all the property values.