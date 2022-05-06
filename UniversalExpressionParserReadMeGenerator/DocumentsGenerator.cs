using FileInclude;
using OROptimizer.Diagnostics.Log;

namespace DocumentationGenerator;

internal class DocumentsGenerator
{
    private readonly (string teplateFileRelativePath, string generatedFileRelativePath)[] _filesRelativePathsData = 
    {
        ("README.md.template", "README.md"),
        (@"UniversalExpressionParser.Tests\Demos\DemoExpressions\index.rst.template", @"docs\index.rst"),
        (@"UniversalExpressionParser.Tests\Demos\DemoExpressions\Literals\literals.rst.template", @"docs\literals.rst"),
        (@"UniversalExpressionParser.Tests\Demos\DemoExpressions\FunctionsAndBraces\functions-and-braces.rst.template", @"docs\functions-and-braces.rst")
    };


    private readonly string _solutionFolderPath;
    private readonly ITemplateProcessor _templateProcessor = new TemplateProcessor();
    private readonly DocumentGenerator _documentGenerator;

    public DocumentsGenerator()
    {
        var assemblyFilePath = typeof(Program).Assembly.Location;

        if (assemblyFilePath == null)
            throw new Exception($"Could not initialioze the value of {nameof(_solutionFolderPath)}.");

        var indexOfDocumentationGenerator = assemblyFilePath.IndexOf(@"\UniversalExpressionParserReadMeGenerator\");

        _solutionFolderPath = assemblyFilePath.Substring(0, indexOfDocumentationGenerator);

        _documentGenerator = new DocumentGenerator(_templateProcessor, _solutionFolderPath);
    }

    public bool GenerateDocumentsFromTemplates()
    {
        foreach (var filesRelativePathData in _filesRelativePathsData)
        {
            if (!_documentGenerator.GenerateFileFromTemplate(filesRelativePathData.teplateFileRelativePath, filesRelativePathData.generatedFileRelativePath))
            {
                LogHelper.Context.Log.Error("Template generation failed.");
                return false;
            }
        }

        return true;
    }
}