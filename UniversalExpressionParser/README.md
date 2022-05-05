UniversalExpressionParser is a library for parsing functional expressions. 

The parsed language format is specified in implementation of UniversalExpressionParser.IExpressionLanguageProvider. The library supports the following in parsed expression: functons, literals (variables), numeric values (parsed by specifying regular expressions), texts keywords, prefixes, postfixes, code blocks and code separators, (e.g., {var x = y+z++;++x;}), line and block comments, operators with priorities specified in UniversalExpressionParser.IExpressionLanguageProvider, custom expression parsers to handle non standard situations, etc.

Also, the parser returns list of parse errors.

See https://github.com/artakhak/UniversalExpressionParser for demos.

The parser was tested by simulated tests that randomly generate thousands of parsed expressions and parse those expressions.

NOTE: The source code and tests will be available at https://github.com/artakhak/UniversalExpressionParser soon. 

Also, this version was thoroughly tested, however this is the betta version, since the code docs are not completed yet, and couple of minor changes might be done soon.
