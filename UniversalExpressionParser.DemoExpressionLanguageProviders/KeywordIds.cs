using System;
using UniversalExpressionParser.ExpressionItems.Custom;

namespace UniversalExpressionParser.DemoExpressionLanguageProviders
{
    public static class KeywordIds
    {

        /// <summary>
        /// Keyword: ::types.
        /// <see cref="CustomExpressionItemCategory"/>=<see cref="CustomExpressionItemCategory.Prefix"/>
        ///  Example: public abstract ::types(T1, T2, T3) Func1(x: T1, y:T2) : T3
        ///                  where T1: class where T2: IInterface2, new() where T3: IInterface3;
        /// </summary>
        public const long GenericTypes = 637781062811583137;

        /// <summary>
        /// Keyword: where.
        /// <see cref="CustomExpressionItemCategory"/>=<see cref="CustomExpressionItemCategory.Postfix"/>
        ///    Example: public abstract ::types(T1, T2, T3) Func1(x: T1, y:T2) : T3
        ///                  where T1: class where T2: IInterface2, new() where T3: IInterface3 whereend;
        /// </summary>
        public const long Where = 637781062905654923;

        /// <summary>
        /// Keyword: ::performance.
        /// <see cref="CustomExpressionItemCategory"/>=<see cref="CustomExpressionItemCategory.Prefix"/>
        /// Example: F1(x, ::performance("AnonymousFuncStatistics1") () =>  x^2 + factorial(x)}
        /// </summary>
        public const long Performance = 637781063051478473;

        /// <summary>
        /// Keyword: ::pragma.
        /// <see cref="CustomExpressionItemCategory"/>=<see cref="CustomExpressionItemCategory.Regular"/>
        /// Example: print("Is in debug mode=" + ::pragma IsDebugMode)
        /// </summary>
        public const long Pragma = 637781063127646487;

        /// <summary>
        /// Keyword: ::metadata.
        /// <see cref="CustomExpressionItemCategory"/>=<see cref="CustomExpressionItemCategory.Prefix"/>
        /// Example public ::metadata({attributes: [Attribute1, Attribute2]; DisplayName: "Factorial"}) Fact(x: int): int
        /// {
        ///      if (x &lt;= 1) return 1;
        ///      return x: Factorial(x - 1);
        /// }
        /// </summary>
        public const long Metadata = 637781063212876967;

        /// <summary>
        /// Keyword: ::int
        /// <see cref="CustomExpressionItemCategory"/>=<see cref="CustomExpressionItemCategory.Postfix"/>
        /// Example: 
        /// if (x::int == 0) ++x;
        ///  
        /// void TestGlobalInlineVar()
        /// {
        ///     Println('x is {x::int}');
        /// } 
        /// </summary>
        public const long GlobalIntInlineVarDeclaration = 637781063392694390;

        /// <summary>
        /// Keyword: ::marker.
        /// Example
        /// ::marker F(x, y)+z+ ::marker y. 
        /// </summary>
        public const long Marker = 637781063484978254;

        /// <summary>
        /// Keyword: var.
        /// Example
        /// var x = y + z. 
        /// </summary>
        public const long Var = 637781064051574641;

        /// <summary>
        /// Keyword: public.
        /// Example
        /// public f1()
        /// {
        /// } 
        /// </summary>
        public const long Public = 637783669980406419;

        /// <summary>
        /// Keyword: abstract.
        /// Example
        /// public abstract f1()
        /// {
        /// } 
        /// </summary>
        public const long Abstract = 637793546145647499;

        /// <summary>
        /// Keyword: virtual.
        /// Example
        /// public virtual f1()
        /// {
        /// } 
        /// </summary>
        public const long Virtual = 637793546374846140;

        /// <summary>
        /// Keyword: override.
        /// Example
        /// public override f1()
        /// {
        /// } 
        /// </summary>
        public const long Override = 637793548069818537;

        /// <summary>
        /// Keyword: static.
        /// Example
        /// public static f1()
        /// {
        /// } 
        /// </summary>
        public const long Static = 637789181872233115;


        /// <summary>
        /// Keyword: public.
        /// Example
        /// public class Dog
        /// {
        /// } 
        /// </summary>
        public const long Class = 637783670388745402;

        /// <summary>
        /// Demo keyword to demonstrate usage of keywords with code blocks, matrixes, etc.
        /// Example: ::codeMarker {}
        /// </summary>
        public const long CodeMarker = 637783880924005707;
        
       
    }

    public static class KeywordHelpers
    {
        /// <summary>
        /// Creates a predefined keyword.
        /// </summary>
        /// <param name="predefinedKeywordId">A keyword from <see cref="KeywordIds"/></param>
        public static ILanguageKeywordInfo CreateKeyword(long predefinedKeywordId)
        {
            switch(predefinedKeywordId)
            {
                case KeywordIds.GenericTypes:
                    return new LanguageKeywordInfo(KeywordIds.GenericTypes, "::types");

                case KeywordIds.Where:
                    return new LanguageKeywordInfo(KeywordIds.Where, "where");

                case KeywordIds.Performance:
                    return new LanguageKeywordInfo(KeywordIds.Performance, "::performance");

                case KeywordIds.Pragma:
                    return new LanguageKeywordInfo(KeywordIds.Pragma, "::pragma");

                case KeywordIds.Metadata:
                    return new LanguageKeywordInfo(KeywordIds.Metadata, "::metadata");

                case KeywordIds.GlobalIntInlineVarDeclaration:
                    return new LanguageKeywordInfo(KeywordIds.GlobalIntInlineVarDeclaration, "::int");

                case KeywordIds.Marker:
                    return new LanguageKeywordInfo(KeywordIds.Marker, "::marker");

                case KeywordIds.Var:
                    return new LanguageKeywordInfo(KeywordIds.Var, "var");

                case KeywordIds.Public:
                    return new LanguageKeywordInfo(KeywordIds.Public, "public");

                case KeywordIds.Abstract:
                    return new LanguageKeywordInfo(KeywordIds.Abstract, "abstract");

                case KeywordIds.Virtual:
                    return new LanguageKeywordInfo(KeywordIds.Virtual, "virtual");

                case KeywordIds.Override:
                    return new LanguageKeywordInfo(KeywordIds.Override, "override");

                case KeywordIds.Static:
                    return new LanguageKeywordInfo(KeywordIds.Static, "static");

                case KeywordIds.Class:
                    return new LanguageKeywordInfo(KeywordIds.Class, "class");

                case KeywordIds.CodeMarker:
                    return new LanguageKeywordInfo(KeywordIds.CodeMarker, "::codeMarker");

                default:
                    throw new ArgumentException(nameof(predefinedKeywordId));
            }
        }
    }
}
