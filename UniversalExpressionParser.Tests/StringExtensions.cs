using System;
using JetBrains.Annotations;

namespace UniversalExpressionParser.Tests
{
    public static class StringExtensions
    {
        public static int OrdinalIndexOf([NotNull] this string text, [NotNull] string searchedText, int numberOfPreviousOccurrencesToSkip = 0)
        {
            int currentIndex = text.IndexOf(searchedText, StringComparison.Ordinal);

            int prvOccurrencesCount = 0;

            while (currentIndex >= 0 && prvOccurrencesCount < numberOfPreviousOccurrencesToSkip)
            {
                ++prvOccurrencesCount;
                currentIndex = text.IndexOf(searchedText, currentIndex + searchedText.Length, StringComparison.Ordinal);
            }
            return currentIndex;
        }

      
        public static int OrdinalLastIndexOf([NotNull] this string text, [NotNull] string searchedText) => text.LastIndexOf(searchedText, StringComparison.Ordinal);
    }
}