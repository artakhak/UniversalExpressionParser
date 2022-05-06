using OROptimizer;
using OROptimizer.DynamicCode;
using System;
using System.Collections.Generic;
using System.Reflection;
using ParameterInfo = OROptimizer.ParameterInfo;

namespace UniversalExpressionParser.Tests;

public class GlobalsCoreTest : IGlobalsCore
{
    private readonly IGlobalsCore _globalsCore;
    private long _currentUniqueId;
    public GlobalsCoreTest(IGlobalsCore globalsCore)
    {
        _globalsCore = globalsCore;
    }

    /// <inheritdoc />
    public bool CheckTypeConstructorExistence(Type serviceType, Type implementationType, Type[] constructorParametersTypes, out ConstructorInfo constructorInfo, out string errorMessage)
    {
        return _globalsCore.CheckTypeConstructorExistence(serviceType, implementationType, constructorParametersTypes, out constructorInfo, out errorMessage);
    }

    /// <inheritdoc />
    public bool CheckTypeConstructorExistence(Type type, Type[] constructorParametersTypes, out ConstructorInfo constructorInfo, out string errorMessage)
    {
        return _globalsCore.CheckTypeConstructorExistence(type, constructorParametersTypes, out constructorInfo, out errorMessage);
    }

    /// <inheritdoc />
    public T CreateInstance<T>(string classFullName, string assemblyFilePath, ParameterInfo[] constructorParameters) where T : class
    {
        return _globalsCore.CreateInstance<T>(classFullName, assemblyFilePath, constructorParameters);
    }

    /// <inheritdoc />
    public object CreateInstance(Type serviceType, Type implementationType, ParameterInfo[] constructorParameters, out string errorMessage)
    {
        return _globalsCore.CreateInstance(serviceType, implementationType, constructorParameters, out errorMessage);
    }

    /// <inheritdoc />
    public object CreateInstance(Type type, ParameterInfo[] constructorParameters, out string errorMessage)
    {
        return _globalsCore.CreateInstance(type, constructorParameters, out errorMessage);
    }

    /// <inheritdoc />
    public IDynamicAssemblyBuilder CurrentInProgressDynamicAssemblyBuilder => _globalsCore.CurrentInProgressDynamicAssemblyBuilder;

    /// <inheritdoc />
    public void EnsureParameterNotNull(string parameterName, object parameterValue)
    {
        _globalsCore.EnsureParameterNotNull(parameterName, parameterValue);
    }

    /// <inheritdoc />
    public string EntryAssemblyFolder => _globalsCore.EntryAssemblyFolder;

    /// <inheritdoc />
    public long GenerateUniqueId()
    {
        return _currentUniqueId++;
    }

    /// <inheritdoc />
    public IEnumerable<Assembly> GetAllLoadedAssemblies()
    {
        throw new NotImplementedException("This method is deprecated.");
    }

    /// <inheritdoc />
    public void LogAnErrorAndThrowException(string loggedErrorMessage, string exceptionMessage = null, Func<string, Exception> createException = null)
    {
        _globalsCore.LogAnErrorAndThrowException(loggedErrorMessage, exceptionMessage, createException);
    }

    /// <inheritdoc />
    public IDynamicAssemblyBuilder StartDynamicAssemblyBuilder(string dynamicAssemblyPath, OROptimizer.Delegates.OnDynamicAssemblyEmitComplete onDynamicAssemblyEmitComplete, bool addAllLoadedAssembliesAsReferences, params string[] referencedAssemblyPaths)
    {
        throw new NotImplementedException("This method is deprecated.");
    }

    public IDynamicAssemblyBuilder StartDynamicAssemblyBuilder(string dynamicAssemblyPath,
        OROptimizer.Delegates.OnDynamicAssemblyEmitComplete onDynamicAssemblyEmitComplete, ILoadedAssemblies loadedAssemblies,
        params string[] referencedAssemblyPaths) => _globalsCore.StartDynamicAssemblyBuilder(dynamicAssemblyPath,
        onDynamicAssemblyEmitComplete, loadedAssemblies, referencedAssemblyPaths);


    public Assembly LoadAssembly(string assemblyFilePath) => _globalsCore.LoadAssembly(assemblyFilePath);
}