UniversalExpressionParser is a library for parsing functional expressions.

The parsed language format is specified in implementation of UniversalExpressionParser.IExpressionLanguageProvider.

The library supports expressions of the following types among others: functions, literals (variables), numeric values (parsed by specifying regular expressions), texts keywords, prefixes, postfixes, code blocks and code separators, (e.g., {var x = y+z++;++x;}), line and block comments, operators with priorities specified in UniversalExpressionParser.IExpressionLanguageProvider, custom expression parsers to handle non-standard situations, etc.

The parser returns a structure that stores the results of parsing which also includes the list of parse errors, if any.

The parser was tested by simulated tests that randomly generate thousands of parsed expressions and parse those expressions.

NOTE: The source code is available at https://github.com/artakhak/UniversalExpressionParser. The documentation with good demos of example expressions, code to parse the expressions and visualized parse results reference https://universalexpressionparser.readthedocs.io/en/latest/summary.html.