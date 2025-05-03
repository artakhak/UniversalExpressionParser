using OROptimizer;
using OROptimizer.Diagnostics.Log;

namespace UniversalExpressionParser
{
    /// <summary>
    /// Provides a thread-static context for handling logging operations in the application.
    /// This context ensures that logging is managed on a per-thread basis, allowing each thread to independently access
    /// and configure its logging instance.
    /// </summary>
    /// <remarks>
    /// The <see cref="LoggerThreadStaticContext"/> class extends the <see cref="ThreadStaticAmbientContext{TContext, TContextDefaultImplementation}"/>
    /// to provide a default implementation for logging using the <see cref="ILog"/> interface and the <see cref="LogToConsole"/> class.
    /// It allows components within the application to obtain and utilize the logging instance specific to the current thread.
    /// </remarks>
    /// <example>
    /// The logging context can be accessed and configured via the <c>Context</c> static property defined in the base class.
    /// This allows code to log information, warnings, errors, or other log messages independently within the current execution thread.
    /// </example>
    internal class LoggerThreadStaticContext : ThreadStaticAmbientContext<ILog, LogToConsole>
    {

    }
}