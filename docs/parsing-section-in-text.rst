=======================
Parsing Section in Text
=======================

.. contents::
   :local:
   :depth: 2
   
- Sometimes we want to parse a single braces expression at specific location in text (i.e., an expression starting with "(" or "[" and ending in ")" or "]" correspondingly) or single code block expression (i.e., an expression starting with **UniversalExpressionParser.IExpressionLanguageProvider.CodeBlockStartMarker** and ending in **UniversalExpressionParser.IExpressionLanguageProvider.CodeBlockEndMarker**). In these scenarios, we want the parser to stop right after fully parsing the braces or code block expression.

- The interface **UniversalExpressionParser.IExpressionParser** has two methods for doing just that. 
 
- The methods for parsing single braces or code block expression are **UniversalExpressionParser.IParseExpressionResult ParseBracesExpression(string expressionLanguageProviderName, string expressionText, IParseExpressionOptions parseExpressionOptions)** and **UniversalExpressionParser.IParseExpressionResult ParseCodeBlockExpression(string expressionLanguageProviderName, string expressionText, IParseExpressionOptions parseExpressionOptions)**, and are demonstrated in sub-sections below.

- The parsed expression of type **UniversalExpressionParser.IParseExpressionResult** returned by these methods has a property **int PositionInTextOnCompletion { get; }** that stores the position in text, after the parsing is complete (i.e., the position after closing brace or code block end marker).

Example of parsing single braces expression
===========================================

- Below is an an SQLite table definition in which we want to parse only the braces expression **(SALARY > 0 AND SALARY > MAX_SALARY/2 AND f1(SALARY) < f2(MAX_SALARY))**, and stop parsing right after the closing brace ')'.

.. sourcecode::
     :linenos:
     
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

.. raw:: html

    <a href="https://github.com/artakhak/UniversalExpressionParser/blob/main/UniversalExpressionParser.Tests/Demos/DemoExpressions/ParsingSectionInText/ParseSingleRoundBracesExpressionDemo.parsed"><p class="codeSnippetRefText">Click here to see the visualized instance of UniversalExpressionParser.IParseExpressionResult</p></a>

- The method **ParseBracesAtCurrentPosition(string expression, int positionInText)** in class **UniversalExpressionParser.Tests.Demos.ParseSingleBracesExpressionAtPositionDemo** (shown below) demonstrates how to parse the braces expression **(SALARY > 0 AND SALARY > MAX_SALARY/2 AND f1(SALARY) < f2(MAX_SALARY))**, by passing the position of opening brace in parameter **positionInText**.

.. raw:: html

    <a href="https://github.com/artakhak/UniversalExpressionParser/blob/main/UniversalExpressionParser.Tests/Demos/ParseSingleBracesExpressionAtPositionDemo.cs"><p class="codeSnippetRefText">Click here to see definition of class UniversalExpressionParser.Tests.Demos.ParseSingleBracesExpressionAtPositionDemo</p></a>

- Here is square braces expression **[f1()+m1[], f2{++i;}]** between texts 'any text before braces' and 'any text after braces...', which can also be parsed using the code in class **UniversalExpressionParser.Tests.Demos.ParseSingleBracesExpressionAtPositionDemo**.

.. sourcecode::
     :linenos:
     
     any text before braces[f1()+m1[], f2
     {
     	++i;
     }]any text after braces including more braces expressions that will not be parsed

.. raw:: html

    <a href="https://github.com/artakhak/UniversalExpressionParser/blob/main/UniversalExpressionParser.Tests/Demos/DemoExpressions/ParsingSectionInText/ParseSingleSquareBracesExpressionDemo.parsed"><p class="codeSnippetRefText">Click here to see the visualized instance of UniversalExpressionParser.IParseExpressionResult</p></a>

Example of parsing single code block expression
===============================================

Below is a text with code block expression **{f1(f2()+m1[], f2{++i;})}** between texts 'any text before code block' and 'any text after code block...' that we want to parse.

.. sourcecode::
     :linenos:
     
     any text before braces[f1()+m1[], f2
     {
     	++i;
     }]any text after braces including more braces expressions that will not be parsed

.. raw:: html

    <a href="https://github.com/artakhak/UniversalExpressionParser/blob/main/UniversalExpressionParser.Tests/Demos/DemoExpressions/ParsingSectionInText/ParseSingleCodeBlockExpressionDemo.parsed"><p class="codeSnippetRefText">Click here to see the visualized instance of UniversalExpressionParser.IParseExpressionResult</p></a>
    
- The method **IParseExpressionResult ParseCodeBlockExpressionAtCurrentPosition(string expression, int positionInText)** in class **UniversalExpressionParser.Tests.Demos.ParseSingleCodeBlockExpressionAtPositionDemo** demonstrates how to parse the single code block expression **{f1(f2()+m1[], f2{++i;})}**, by passing the position of code block start marker '{' in parameter **positionInText**.

.. raw:: html

    <a href="https://github.com/artakhak/UniversalExpressionParser/blob/main/UniversalExpressionParser.Tests/Demos/ParseSingleCodeBlockExpressionAtPositionDemo.cs"><p class="codeSnippetRefText">Click here to see definition of class UniversalExpressionParser.Tests.Demos.ParseSingleCodeBlockExpressionAtPositionDemo</p></a>

