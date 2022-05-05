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

        public IParseExpressionResult ParseCodeBlockExpressionAtCurrentPosition(string expression, int positionInText)
        {
            // _expressionParser.ParseCodeBlockExpression() tries to parse single code block expression like "{f1(f2()+m1[], f2{++i;})}",
            // within which there can be any number of other valid expressions, such as operators, another braces expression, code block
            // expressions, etc.
            // For example the text expression can be
            // "any text before code block{f1(f2()+m1[], f2{++i;})}any text after code block including more code blocks that will not be parsed".
            // Parsing starts at position IParseExpressionOptions.StartIndex, and stops at position after the
            // code block end marker (i.e., '}' in this example) corresponding to code block start marker at
            // position IParseExpressionOptions.StartIndex.
            // Note, if the text has more code block expressions after the code block end marker, they will not be
            // parsed (i.e., the method parses only the first code block expression at position IParseExpressionOptions.StartIndex).
            // When parsing is complete, IParseExpressionResult.PositionInTextOnCompletion has the position after the code block end marker
            // for the parsed code block expression at position IParseExpressionOptions.StartIndex.

            return _expressionParser.ParseCodeBlockExpression(_expressionLanguageProvider.LanguageName, expression, 
                new ParseExpressionOptions
            {
                StartIndex = positionInText
            });
        }
    }
}
