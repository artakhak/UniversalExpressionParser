// Copyright (c) UniversalExpressionParser Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using JetBrains.Annotations;
using System;
using TextParser;
using UniversalExpressionParser.ExpressionItems;

namespace UniversalExpressionParser
{
    //Documented.
    /// <summary>
    /// Universal expression parser. Evaluates textual expressions of type: x+(y*3) == 3 OR MyFunc(z1, x1) == SomeOtherFunc(1.314E+1, Const1).
    /// </summary>
    public interface IExpressionParser
    {
        /// <summary>
        /// Parses an expression in <paramref name="expressionText"/> and returns an instance of <see cref="IParseExpressionResult"/>.<br/>
        /// The language specification used to parse the expression is specified in an implementation of <see cref="IExpressionLanguageProvider"/> which has
        /// value of <see cref="IExpressionLanguageProvider.LanguageName"/> equal to <paramref name="expressionLanguageProviderName"/>.<br/>
        /// An instance of <see cref="IExpressionLanguageProvider"/> should
        /// be validated and registered in some cache for the parser to locate the <see cref="IExpressionLanguageProvider"/> to use for parsing. For example
        /// <see cref="ExpressionParser"/> using <see cref="IExpressionLanguageProviderCache"/> (injected in constructor as dependency), for getting the expression language provider by name,
        /// and it is expected that <see cref="IExpressionLanguageProviderCache.RegisterExpressionLanguageProvider(IExpressionLanguageProvider)"/> is executed for an instance of <see cref="IExpressionLanguageProvider"/>.<br/>
        /// An example of expression that the parser can parse for some instance of <see cref="IExpressionLanguageProvider"/> that extends <see cref="ExpressionLanguageProviderBase"/> is displayed below:<br/>
        ///<br/>
        /// var z = x1*y1+x2*y2;<br/>
        ///<br/>
        /// var matrixMultiplicationResult = [[x1_1, x1_2, x1_3], [x2_1, x2_2, x2_3]]*[[y1_1, x1_2], [y2_1, x2_2], [y3_1, x3_2]];<br/>
        /// <br/>
        /// println(matrixMultiplicationResult);<br/>
        /// <br/>
        /// [NotNull]<br/>
        /// [PublicName("Calculate")]<br/>
        /// public F1(x, y) : int => <br/>
        /// {<br/>
        /// <br/>
        ///     /*This demos multiline<br/>
        ///     comments that can be placed anywhere*/<br/>
        ///     ++y /*another multiline comment*/;<br/>
        ///     return 1.3EXP-2.7+ ++x+y* z; // Line comments.<br/>
        /// }<br/>
        /// <br/>
        /// public abstract class ::metadata{description: "Demo prefix"} ::types[T1, T2, T3] Animal<br/>
        ///     where T1: IType1 where T2: T1, IType2 whereend<br/>
        /// where T3: IType3 whereend<br/>
        /// {<br/>
        ///     public abstract Move() : void;  <br/>  
        /// }<br/>
        /// <br/>
        /// public class Dog : (Animal)<br/>
        /// {<br/>
        ///     public override Move() : void => println("Jump");<br/>
        /// }<br/>
        /// </summary>
        /// <param name="expressionLanguageProviderName">Expression language provider name. Should be equal to <see cref="IExpressionLanguageProvider.LanguageName"/> for some instance of <see cref="IExpressionLanguageProvider"/> registered in some cache such as
        /// <see cref="IExpressionLanguageProviderCache"/> used by the parser.</param>
        /// <param name="expressionText">Expression to parse.<br/>
        /// </param>
        /// <param name="parseExpressionOptions">Options used when parsing. For example <see cref="IParseExpressionOptions.StartIndex"/> can be used to specify where in parsed text <paramref name="expressionText"/> parsing should start,
        /// and <see cref="IParseExpressionOptions.IsExpressionParsingComplete"/> delegate can be used to stop parsing before the parser parser all the text in <paramref name="expressionText"/> say by checking some marker in text.
        /// </param>
        /// <returns>Returns an instance of <see cref="IParseExpressionResult"/> which contains all parse data. Some important properties in <see cref="IParseExpressionResult"/> that the caller can evaluate are:<br/>
        /// -<see cref="IParseExpressionResult.RootExpressionItem"/>: An instance of <see cref="IRootExpressionItem"/> that stores the root expression item that contains other parsed child expression items in properties
        /// <see cref="IComplexExpressionItem.AllItems"/> and <see cref="IComplexExpressionItem.Children"/>.<br/>
        /// Any expression item in <see cref="IComplexExpressionItem.AllItems"/> is either an instance of <see cref="IComplexExpressionItem"/> and can have other
        /// expression items as parts or an instance of <see cref="IExpressionItemBase"/> for simpler expression items that do not have other parts (note, <see cref="IComplexExpressionItem"/> extends <see cref="IExpressionItemBase"/> as well).<br/>
        /// For example <see cref="IBracesExpressionItem"/> is a subclass of <see cref="IComplexExpressionItem"/> and have items of types <see cref="ILiteralExpressionItem"/>, <see cref="IOpeningBraceExpressionItem"/>,
        /// <see cref="ICommaExpressionItem"/>, <see cref="IClosingBraceExpressionItem"/> in <see cref="IComplexExpressionItem.AllItems"/> for function name, braces and commas separating the parameters, as well as it can have
        /// instance of <see cref="IExpressionItemBase"/> for function parameters both in <see cref="IComplexExpressionItem.AllItems"/> and <see cref="IComplexExpressionItem.Children"/>. On the other hand <see cref="ICommaExpressionItem"/> is a simple expression item for
        /// commas and does not contain other expression items as parts (e.g., <see cref="ICommaExpressionItem"/> is a subclass of <see cref="IExpressionItemBase"/>, but not of <see cref="IComplexExpressionItem"/>.<br/>
        /// -<see cref="IParseExpressionResult.ParseErrorData"/>: Contains data on parse errors.<br/>
        /// -<see cref="IParseExpressionResult.SortedCommentedTextData"/>: Contains data on commented out code parsed from an expression.<br/>
        /// </returns>
        /// <exception cref="ExpressionLanguageProviderException">Throws this exception.</exception>
        /// <exception cref="ArgumentException">Throws this exception.</exception>
        [NotNull]
        IParseExpressionResult ParseExpression([NotNull] string expressionLanguageProviderName, [NotNull] string expressionText, [NotNull] IParseExpressionOptions parseExpressionOptions);

        /// <summary>
        /// This method is pretty similar to <see cref="ParseExpression(string, string, IParseExpressionOptions)"/> except the text at
        /// parse start position specified in <see cref="IParseExpressionOptions.StartIndex"/> in parameter <paramref name="parseExpressionOptions"/> is expected to be either
        /// '(' or '[' (otherwise an exception <see cref="ParseTextException"/> is thrown).<br/>
        /// This method parses the text in <paramref name="expressionText"/> the same way as <see cref="ParseExpression(string, string, IParseExpressionOptions)"/> does, and returns
        /// an instance of <see cref="IParseExpressionResult"/>, however stops the parsing as soon as the brace expression at current position is parsed (the brace expression can have child braces as well).<br/>
        /// If parsing succeeds, <see cref="IParseExpressionResult.RootExpressionItem"/> will have single item in each of properties
        /// <see cref="IComplexExpressionItem.AllItems"/>, <see cref="IComplexExpressionItem.RegularItems"/>, and <see cref="IComplexExpressionItem.Children"/> and all of them will be the same instance of type <see cref="IBracesExpressionItem"/>.
        /// Below is an example of SQLite table definition with subtext starting with "CHECK(" for specifying constraints on column SALARY.
        /// We can use <see cref="ParseBracesExpression(string, string, IParseExpressionOptions)"/> to parse only section of this text
        /// that starts with braces after CHECK and ends with closing round brace ')' right before comma ',' by passing an instance of
        /// <see cref="IParseExpressionOptions"/> for parameter <paramref name="parseExpressionOptions"/> in which <see cref="IParseExpressionOptions.StartIndex"/> is equal to the index
        /// of first '(' after text "CHECK"<br/> 
        /// CREATE TABLE COMPANY(<br/>
        /// 	ID INT PRIMARY KEY     NOT NULL,<br/>
        /// 	MAX_SALARY     REAL,<br/>
        /// 	/* The parser will only parse expression<br/> 
        /// 	(SALARY > 0 AND SALARY > MAX_SALARY/2 AND f1(SALARY)&lt;f2(MAX_SALARY)) and will stop right after the<br/> 
        /// 	closing round brace ')' of in this expression. */<br/>
        /// 	AVG_SALARY     REAL<br/>    
        /// 			CHECK (SALARY &gt; 0 AND<br/> 
        /// 				  SALARY &gt; MAX_SALARY/2 AND<br/> 
        /// 				  f1(SALARY) &lt; f2(MAX_SALARY)),
        ///     ADDRESS        CHAR(50)<br/>
        /// );
        /// </summary>
        /// <param name="expressionLanguageProviderName">Expression language provider name. Should be equal to <see cref="IExpressionLanguageProvider.LanguageName"/> for some instance of <see cref="IExpressionLanguageProvider"/> registered in some cache such as
        /// <see cref="IExpressionLanguageProviderCache"/> used by the parser.</param>
        /// <param name="expressionText">Expression to parse.<br/>
        /// </param>
        /// <param name="parseExpressionOptions">Options used when parsing. For example <see cref="IParseExpressionOptions.StartIndex"/> can be used to specify where in parsed text <paramref name="expressionText"/> parsing should start,
        /// and <see cref="IParseExpressionOptions.IsExpressionParsingComplete"/> delegate can be used to stop parsing before the parser parser all the text in <paramref name="expressionText"/> say by checking some marker in text.
        /// </param>
        /// <returns>Returns an instance of <see cref="IParseExpressionResult"/> which contains all parse data. Some important properties in <see cref="IParseExpressionResult"/> that the caller can evaluate are:<br/>
        /// -<see cref="IParseExpressionResult.RootExpressionItem"/>: An instance of <see cref="IRootExpressionItem"/> that stores the root expression item that contains other parsed child expression items in properties
        /// <see cref="IComplexExpressionItem.AllItems"/> and <see cref="IComplexExpressionItem.Children"/>.<br/>
        /// Any expression item in <see cref="IComplexExpressionItem.AllItems"/> is either an instance of <see cref="IComplexExpressionItem"/> and can have other
        /// expression items as parts or an instance of <see cref="IExpressionItemBase"/> for simpler expression items that do not have other parts (note, <see cref="IComplexExpressionItem"/> extends <see cref="IExpressionItemBase"/> as well).<br/>
        /// For example <see cref="IBracesExpressionItem"/> is a subclass of <see cref="IComplexExpressionItem"/> and have items of types <see cref="ILiteralExpressionItem"/>, <see cref="IOpeningBraceExpressionItem"/>,
        /// <see cref="ICommaExpressionItem"/>, <see cref="IClosingBraceExpressionItem"/> in <see cref="IComplexExpressionItem.AllItems"/> for function name, braces and commas separating the parameters, as well as it can have
        /// instance of <see cref="IExpressionItemBase"/> for function parameters both in <see cref="IComplexExpressionItem.AllItems"/> and <see cref="IComplexExpressionItem.Children"/>. On the other hand <see cref="ICommaExpressionItem"/> is a simple expression item for
        /// commas and does not contain other expression items as parts (e.g., <see cref="ICommaExpressionItem"/> is a subclass of <see cref="IExpressionItemBase"/>, but not of <see cref="IComplexExpressionItem"/>.<br/>
        /// -<see cref="IParseExpressionResult.ParseErrorData"/>: Contains data on parse errors.<br/>
        /// -<see cref="IParseExpressionResult.SortedCommentedTextData"/>: Contains data on commented out code parsed from an expression.<br/>
        /// </returns>
        /// <exception cref="ExpressionLanguageProviderException">Throws this exception if the text at position does not start with '(' or '['.</exception>
        /// <exception cref="ArgumentException">Throws this exception.</exception>
        [NotNull]
        IParseExpressionResult ParseBracesExpression([NotNull] string expressionLanguageProviderName, [NotNull] string expressionText, [NotNull] IParseExpressionOptions parseExpressionOptions);

        /// <summary>
        /// This method is pretty similar to <see cref="ParseExpression(string, string, IParseExpressionOptions)"/> except the text at
        /// parse start position specified in <see cref="IParseExpressionOptions.StartIndex"/> in parameter <paramref name="parseExpressionOptions"/> is expected to
        /// be code block start marker <see cref="IExpressionLanguageProvider.CodeBlockStartMarker"/> (otherwise an exception <see cref="ParseTextException"/> is thrown).<br/>
        /// This method parses the text in <paramref name="expressionText"/> the same way as <see cref="ParseExpression(string, string, IParseExpressionOptions)"/> does, and returns
        /// an instance of <see cref="IParseExpressionResult"/>, however stops the parsing as soon as the code block expression at current position is parsed (code block can have other child code block expressions as well).<br/>
        /// If parsing succeeds, <see cref="IParseExpressionResult.RootExpressionItem"/> will have single item in each of properties
        /// <see cref="IComplexExpressionItem.AllItems"/>, <see cref="IComplexExpressionItem.RegularItems"/>, and <see cref="IComplexExpressionItem.Children"/> and all of them will be the same instance of type <see cref="ICodeBlockExpressionItem"/>.
        /// Below is an example of expression that has a code block that starts with code block start marker "{" after text "block" and ends with code block end marker "}" before text "any text after code block".
        /// We can use <see cref="ParseCodeBlockExpression(string, string, IParseExpressionOptions)"/> to parse only section of this text
        /// by passing an instance of <see cref="IParseExpressionOptions"/> for parameter <paramref name="parseExpressionOptions"/> in which <see cref="IParseExpressionOptions.StartIndex"/> is equal to the index
        /// of first code block start marker "{" after text "block"<br/>
        /// any text before code<br/>  
        /// block {<br/> 
        /// f1(f2()+m1[], f2{++i;})<br/> 
        /// }any text after code block including more code blocks that will not be parsed<br/> 
        /// </summary>
        /// <param name="expressionLanguageProviderName">Expression language provider name. Should be equal to <see cref="IExpressionLanguageProvider.LanguageName"/> for some instance of <see cref="IExpressionLanguageProvider"/> registered in some cache such as
        /// <see cref="IExpressionLanguageProviderCache"/> used by the parser.</param>
        /// <param name="expressionText">Expression to parse.<br/>
        /// </param>
        /// <param name="parseExpressionOptions">Options used when parsing. For example <see cref="IParseExpressionOptions.StartIndex"/> can be used to specify where in parsed text <paramref name="expressionText"/> parsing should start,
        /// and <see cref="IParseExpressionOptions.IsExpressionParsingComplete"/> delegate can be used to stop parsing before the parser parser all the text in <paramref name="expressionText"/> say by checking some marker in text.
        /// </param>
        /// <returns>Returns an instance of <see cref="IParseExpressionResult"/> which contains all parse data. Some important properties in <see cref="IParseExpressionResult"/> that the caller can evaluate are:<br/>
        /// -<see cref="IParseExpressionResult.RootExpressionItem"/>: An instance of <see cref="IRootExpressionItem"/> that stores the root expression item that contains other parsed child expression items in properties
        /// <see cref="IComplexExpressionItem.AllItems"/> and <see cref="IComplexExpressionItem.Children"/>.<br/>
        /// Any expression item in <see cref="IComplexExpressionItem.AllItems"/> is either an instance of <see cref="IComplexExpressionItem"/> and can have other
        /// expression items as parts or an instance of <see cref="IExpressionItemBase"/> for simpler expression items that do not have other parts (note, <see cref="IComplexExpressionItem"/> extends <see cref="IExpressionItemBase"/> as well).<br/>
        /// For example <see cref="IBracesExpressionItem"/> is a subclass of <see cref="IComplexExpressionItem"/> and have items of types <see cref="ILiteralExpressionItem"/>, <see cref="IOpeningBraceExpressionItem"/>,
        /// <see cref="ICommaExpressionItem"/>, <see cref="IClosingBraceExpressionItem"/> in <see cref="IComplexExpressionItem.AllItems"/> for function name, braces and commas separating the parameters, as well as it can have
        /// instance of <see cref="IExpressionItemBase"/> for function parameters both in <see cref="IComplexExpressionItem.AllItems"/> and <see cref="IComplexExpressionItem.Children"/>. On the other hand <see cref="ICommaExpressionItem"/> is a simple expression item for
        /// commas and does not contain other expression items as parts (e.g., <see cref="ICommaExpressionItem"/> is a subclass of <see cref="IExpressionItemBase"/>, but not of <see cref="IComplexExpressionItem"/>.<br/>
        /// -<see cref="IParseExpressionResult.ParseErrorData"/>: Contains data on parse errors.<br/>
        /// -<see cref="IParseExpressionResult.SortedCommentedTextData"/>: Contains data on commented out code parsed from an expression.<br/>
        /// </returns>
        /// <exception cref="ExpressionLanguageProviderException">Throws this exception if the text at position does not start with code block start marker <see cref="IExpressionLanguageProvider.CodeBlockStartMarker"/>.</exception>
        /// <exception cref="ArgumentException">Throws this exception.</exception>
        [NotNull]
        IParseExpressionResult ParseCodeBlockExpression([NotNull] string expressionLanguageProviderName, [NotNull] string expressionText, [NotNull] IParseExpressionOptions parseExpressionOptions);
    }
}