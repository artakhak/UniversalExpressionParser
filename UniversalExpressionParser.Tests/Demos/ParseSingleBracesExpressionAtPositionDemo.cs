using OROptimizer.Diagnostics.Log;
using TextParser;
using UniversalExpressionParser.DemoExpressionLanguageProviders;

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
            
            _expressionParser = new ExpressionParser(new TextSymbolsParserFactory(), expressionLanguageProviderCache, LogHelper.Context.Log);
            expressionLanguageProviderCache.RegisterExpressionLanguageProvider(_expressionLanguageProvider);
        }

        public IParseExpressionResult ParseBracesAtCurrentPosition(string expression, int positionInText)
        {
            // _expressionParser.ParseBracesExpression() tries to parse single square or round braces expression
            // like "[f1()+m1[], f2{++i;}]" within
            // which there can be any number of other valid expressions, such as operators, other braces expressions,
            // code block expressions, etc.
            // For example the text expression can be
            // "any text before braces[f1()+m1[], f2{++i;}]any text after braces".
            // Parsing starts at position IParseExpressionOptions.StartIndex, and stops at position after the closing square or round braces
            // corresponding to opening square or round brace at position IParseExpressionOptions.StartIndex.
            // Note, if the text has more braces expressions after the closing square or round brace, they will not be parsed (i.e., the method
            // parses only the first braces expression at position ParseExpressionOptions.StartIndex).
            // When parsing is complete, IParseExpressionResult.PositionInTextOnCompletion has the position after the closing brace
            // for the parsed braces expression at position IParseExpressionOptions.StartIndex.

            return _expressionParser.ParseBracesExpression(_expressionLanguageProvider.LanguageName, 
                expression, new ParseExpressionOptions
            {
                StartIndex = positionInText
            });
        }
    }
}
