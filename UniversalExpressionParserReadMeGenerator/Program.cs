using DocumentationGenerator;
using OROptimizer.Diagnostics.Log;

// See https://aka.ms/new-console-template for more information
LogHelper.RegisterContext(new Log4NetHelperContext("UniversalExpressionParserReadMeGenerator.log4net.config"));

new DocumentsGenerator().GenerateDocumentsFromTemplates();

Console.Out.WriteLine("Type any character to exit!");
Console.In.Read();