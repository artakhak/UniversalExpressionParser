using System.Text;
using FileInclude;
using OROptimizer.Diagnostics.Log;


// See https://aka.ms/new-console-template for more information
LogHelper.RegisterContext(new Log4NetHelperContext("UniversalExpressionParserReadMeGenerator.log4net.config"));

var templateProcessor = new TemplateProcessor();

var assemblyFilePath = typeof(Program).Assembly.Location;

var indexOfUniversalExpressionParserReadMeGenerator = assemblyFilePath.IndexOf(@"\UniversalExpressionParserReadMeGenerator\");

var templateFilePath = Path.Join(assemblyFilePath.Substring(0, indexOfUniversalExpressionParserReadMeGenerator), "README-TEMPLATE.md");
var readmeFileName = "README.md";

var errors = templateProcessor.GenerateFileFromTemplateAndSave(templateFilePath, readmeFileName);

if (errors.Count == 0)
{
    LogHelper.Context.Log.InfoFormat("Readme file '{0}' was successfully generated from template file '{1}'!",
        readmeFileName, templateFilePath);
}
else
{
    LogHelper.Context.Log.ErrorFormat("Generation of readme file '{0}' from template file '{1}' completed with errors!",
        readmeFileName, templateFilePath);

    foreach (var errorData in errors)
    {
        LogError(errorData);
    }
}

Console.Out.WriteLine("Type any character to exit!");
Console.In.Read();

void LogError(IErrorData errorData)
{
    var contextData = new StringBuilder();

    contextData.Append($"Context data: [{nameof(IErrorData.ErrorCode)}:{errorData.ErrorCode}");

    if (errorData.ErrorPosition != null)
        contextData.Append($", {nameof(IErrorData.ErrorPosition)}:{errorData.ErrorPosition}");

    contextData.Append($", {nameof(IErrorData.SourceFilePath)}:'{errorData.SourceFilePath}']");

    log4net.GlobalContext.Properties["context"] = contextData.ToString();

    if (errorData.Exception != null)
        LogHelper.Context.Log.Error(errorData.ErrorMessage, errorData.Exception);
    else
        LogHelper.Context.Log.Error(errorData.ErrorMessage);
}
