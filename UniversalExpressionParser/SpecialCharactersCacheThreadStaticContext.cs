using OROptimizer;

namespace UniversalExpressionParser
{
    /// <summary>
    /// Represents a thread-static context for <see cref="ISpecialCharactersCache"/> that provides
    /// special character caching functionalities. This class is used to manage a thread-local
    /// instance of <see cref="ISpecialCharactersCache"/> leveraging the <see cref="ThreadStaticAmbientContext{TContext, TContextDefaultImplementation}"/>
    /// for thread isolation.
    /// </summary>
    /// <remarks>
    /// This class extends the <see cref="ThreadStaticAmbientContext{TContext, TContextDefaultImplementation}"/>
    /// with <see cref="ISpecialCharactersCache"/> as the context type and <see cref="SpecialCharactersCache"/>
    /// as the default implementation. The thread-static mechanism ensures that all threads access their
    /// respective instance of the special characters cache, thereby avoiding cross-thread state interference.
    /// </remarks>
    internal class SpecialCharactersCacheThreadStaticContext : ThreadStaticAmbientContext<ISpecialCharactersCache, SpecialCharactersCache>
    {

    }
}