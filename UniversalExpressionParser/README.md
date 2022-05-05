UniversalExpressionParser is a library for parsing functional expressions. 

The parsed language format is specified in implementation of UniversalExpressionParser.IExpressionLanguageProvider. 

The library supports expressions of the following types among others: functions, literals (variables), numeric values (parsed by specifying regular expressions), texts keywords, prefixes, postfixes, code blocks and code separators, (e.g., {var x = y+z++;++x;}), line and block comments, operators with priorities specified in UniversalExpressionParser.IExpressionLanguageProvider, custom expression parsers to handle non-standard situations, etc.

The parser returns a struture that stores the results of parsing which also includes the list of parse errors, if any.

NOTE: The source code is available at https://github.com/artakhak/UniversalExpressionParser.

The parser was tested by simulated tests that randomly generate thousands of parsed expressions and parse those expressions.

The the README.md https://github.com/artakhak/UniversalExpressionParser has good demoes (the code and parse results for the demos are available at https://github.com/artakhak/UniversalExpressionParser/tree/main/UniversalExpressionParser.Tests/Demos). 

Unfortunately github cuts off part of the README content on main page at this point because of file size limitation. Will try to find some workaround for this soon.

However, one solution to view the README.md file in its entirety would be to copy the file at https://github.com/artakhak/UniversalExpressionParser/blob/main/README.md and view it using some MD viewer in Chrome or Edge browser.