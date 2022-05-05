using JetBrains.Annotations;

namespace UniversalExpressionParser.Tests.TestLanguage
{
    public class KeywordInfoForTests : ILanguageKeywordInfo
    {
        private static long _idCurrent = 100000;

        public KeywordInfoForTests([NotNull] string keyword) : this(++_idCurrent, keyword)
        {
        }
        public KeywordInfoForTests(long id, [NotNull] string keyword)
        {
            Id = id;
            Keyword = keyword;
        }

        /// <inheritdoc />
        public long Id { get; }

        /// <inheritdoc />
        public string Keyword { get; }
    }
}