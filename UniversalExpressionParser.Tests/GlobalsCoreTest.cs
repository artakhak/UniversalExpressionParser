using OROptimizer;
using OROptimizer.DynamicCode;
using System;
using System.Collections.Generic;
using System.Reflection;
using ParameterInfo = OROptimizer.ParameterInfo;

namespace UniversalExpressionParser.Tests
{
    public class GlobalsCoreTest : IGlobalsCore
    {
        private long _currentUniqueId;
        public GlobalsCoreTest()
        {

        }
       
        /// <inheritdoc />
        public bool CheckTypeConstructorExistence(Type serviceType, Type implementationType, Type[] constructorParametersTypes, out ConstructorInfo constructorInfo, out string errorMessage)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public bool CheckTypeConstructorExistence(Type type, Type[] constructorParametersTypes, out ConstructorInfo constructorInfo, out string errorMessage)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public T CreateInstance<T>(string classFullName, string assemblyFilePath, ParameterInfo[] constructorParameters) where T : class
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public object CreateInstance(Type serviceType, Type implementationType, ParameterInfo[] constructorParameters, out string errorMessage)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public object CreateInstance(Type type, ParameterInfo[] constructorParameters, out string errorMessage)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IDynamicAssemblyBuilder CurrentInProgressDynamicAssemblyBuilder { get; }

        /// <inheritdoc />
        public void EnsureParameterNotNull(string parameterName, object parameterValue)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public string EntryAssemblyFolder
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        /// <inheritdoc />
        public long GenerateUniqueId()
        {
            return _currentUniqueId++;
        }

        /// <inheritdoc />
        public IEnumerable<Assembly> GetAllLoadedAssemblies()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void LogAnErrorAndThrowException(string loggedErrorMessage, string exceptionMessage = null, Func<string, Exception> createException = null)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public IDynamicAssemblyBuilder StartDynamicAssemblyBuilder(string dynamicAssemblyPath, OROptimizer.Delegates.OnDynamicAssemblyEmitComplete onDynamicAssemblyEmitComplete, bool addAllLoadedAssembliesAsReferences, params string[] referencedAssemblyPaths)
        {
            throw new NotImplementedException();
        }

        public IDynamicAssemblyBuilder StartDynamicAssemblyBuilder(string dynamicAssemblyPath,
            OROptimizer.Delegates.OnDynamicAssemblyEmitComplete onDynamicAssemblyEmitComplete, ILoadedAssemblies loadedAssemblies,
            params string[] referencedAssemblyPaths)
        {
            throw new NotImplementedException();
        }

        public Assembly LoadAssembly(string assemblyFilePath)
        {
            throw new NotImplementedException();
        }
    }
}