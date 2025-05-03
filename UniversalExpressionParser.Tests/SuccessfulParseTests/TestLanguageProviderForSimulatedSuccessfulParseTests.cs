using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JetBrains.Annotations;
using NUnit.Framework;
using TestsSharedLibraryForCodeParsers.CodeGeneration;
using TextParser;
using UniversalExpressionParser.ExpressionItems.Custom;
using UniversalExpressionParser.Tests.OperatorTemplates;
using UniversalExpressionParser.Tests.TestLanguage;

namespace UniversalExpressionParser.Tests.SuccessfulParseTests
{
    public class TestLanguageProviderForSimulatedSuccessfulParseTests : ExpressionLanguageProviderBase
    {
        private readonly List<IOperatorInfo> _operators = new List<IOperatorInfo>();
        private readonly List<ILanguageKeywordInfo> _keywords = new List<ILanguageKeywordInfo>();

        private readonly HashSet<char> _setOfSpecialOperatorCharactersUsedInLiterals;

        /// <inheritdoc />
        public TestLanguageProviderForSimulatedSuccessfulParseTests([NotNull][ItemNotNull] IEnumerable<ICustomExpressionItemParser> customExpressionItemParsers)
        {
            CustomExpressionItemParsers = customExpressionItemParsers;
            IsLanguageCaseSensitive = TestSetup.IsLanguageCaseSensitive;

            _setOfSpecialOperatorCharactersUsedInLiterals = new HashSet<char>(TestSetup.SpecialOperatorCharactersUsedInLiterals);

            AddTextStartEndMarkerCharacters();
            AddCodeCommentMarkers();

            var specialOperatorCharactersUsedInNonLiterals = SpecialCharactersCacheThreadStaticContext.Context.SpecialOperatorCharacters.Where(
                x => !_setOfSpecialOperatorCharactersUsedInLiterals.Contains(x) &&
                   (TestSetup.CurrentCommentMarkersData == null ||
                    !TestSetup.CurrentCommentMarkersData.LineCommentMarker.Contains(x) &&
                    !TestSetup.CurrentCommentMarkersData.MultilineCommentStartMarker.Contains(x) &&
                    !TestSetup.CurrentCommentMarkersData.MultilineCommentEndMarker.Contains(x))).ToList();

            var specialOperatorCharactersForUsedInSeparators = new List<char>();
            var specialOperatorCharactersUsedInKeywords = new List<char>();
            var specialOperatorCharactersUsedInOperators = new List<char>();

            var operatorCharactersCount = specialOperatorCharactersUsedInNonLiterals.Count;

            #region Add keyword charactes used in predefined keywords
            // Lets add operator characters that are used in predefined keywords to specialOperatorCharactersUsedInKeywords
            // and remove these from specialOperatorCharactersUsedInNonLiterals
            foreach (var keywordCharacter in specialOperatorCharactersUsedInNonLiterals.Where(character =>
                TestSetup.PredefinedKeywords.Any(keywordInfo => keywordInfo.Keyword.Contains(character))))
                specialOperatorCharactersUsedInKeywords.Add(keywordCharacter);

            foreach (var keywordCharacter in specialOperatorCharactersUsedInKeywords)
                specialOperatorCharactersUsedInNonLiterals.Remove(keywordCharacter);
            #endregion

            while (specialOperatorCharactersUsedInNonLiterals.Count > 0)
            {
                var characterIndex = TestSetup.SimulationRandomNumberGenerator.Next(0, specialOperatorCharactersUsedInNonLiterals.Count - 1); var character = specialOperatorCharactersUsedInNonLiterals[characterIndex];

                if (specialOperatorCharactersUsedInOperators.Count < 0.6 * operatorCharactersCount)
                    specialOperatorCharactersUsedInOperators.Add(character);
                else if (specialOperatorCharactersForUsedInSeparators.Count < 0.1 * operatorCharactersCount && character != '|')
                    specialOperatorCharactersForUsedInSeparators.Add(character);
                else
                    specialOperatorCharactersUsedInKeywords.Add(character);

                specialOperatorCharactersUsedInNonLiterals.RemoveAt(characterIndex);
            }

            AddCodeSeparators(specialOperatorCharactersForUsedInSeparators);
            AddKeywords(specialOperatorCharactersUsedInKeywords);

            // Keywords should not match any operator part, however operator characters can be used. 
            // however for test purposes we exclude the special characters used in keywors when we generate operators, since we might end
            // up with auto-generated programs when the parser might parse the program in a different way then what the expoected 
            // outcome is intended to be
            // for example in the code [x oper_871::pragma y] the expected code is literal followed by operator 'oper_871', then followed by '::pragma y'.
            // The parser however might parse this as x followed by three operators (e.g., 'oper_871', ':', and ':'), than by 'pragma', and y.
            // In real program this issue can be handled using parenthesis like [x oper_871(::pragma y)], however it is harder to do this in simulated code.
            AddOperators(specialOperatorCharactersUsedInOperators);
        }

        private void AddTextStartEndMarkerCharacters()
        {
            var numberOfMarkers = TestSetup.SimulationRandomNumberGenerator.Next(0, 3);

            if (numberOfMarkers == 0)
                return;

            var candidateMarkers = new List<char> { '\'', '"', '`' };

            while (_constantTextStartEndMarkerCharacters.Count < numberOfMarkers)
            {
                var candidateMarker = candidateMarkers[TestSetup.SimulationRandomNumberGenerator.Next(candidateMarkers.Count - 1)];
                _constantTextStartEndMarkerCharacters.Add(candidateMarker);
                candidateMarkers = candidateMarkers.Where(x => x != candidateMarker).ToList();
            }
        }

        private void AddCodeCommentMarkers()
        {
            if (TestSetup.CurrentCommentMarkersData == null)
                return;

            _lineCommentText = TestSetup.CurrentCommentMarkersData.LineCommentMarker;
            _multilineCommentStartMarker = TestSetup.CurrentCommentMarkersData.MultilineCommentStartMarker;
            _multilineCommentEndMarker = TestSetup.CurrentCommentMarkersData.MultilineCommentEndMarker;
        }

        private void AddCodeSeparators([NotNull] IReadOnlyList<char> specialOperatorCharactersForUsedInSeparators)
        {
            var randomNumber = TestSetup.SimulationRandomNumberGenerator.Next(100);

            string expressionSeparatorCharacterToString = null;

            while (true)
            {
                if (!TestSetup.MustSupportCodeBlocks && randomNumber <= 20)
                {
                    // multiple expressions not supported
                    _expressionSeparatorCharacter = '\0';
                    return;
                }

                if (TestSetup.SimulateNiceCode || TestSetup.SimulationRandomNumberGenerator.Next(100) <= 30)
                {
                    _expressionSeparatorCharacter = ';';
                }
                else
                {
                    var expressionSeparatorOperatorCharacters =
                        specialOperatorCharactersForUsedInSeparators.Where(
                            operatorCharacter =>
                            {
                                // Predefined keywords use : character such as ::types. Lets exclude these characters
                                if (TestSetup.PredefinedKeywords.Any(keyword =>
                                    keyword.Keyword.Contains(operatorCharacter)))
                                    return false;

                                return true;
                            }).ToList();

                    var expressionSeparatorCharacter = expressionSeparatorOperatorCharacters[
                        TestSetup.SimulationRandomNumberGenerator.Next(expressionSeparatorOperatorCharacters.Count - 1)];

                    _expressionSeparatorCharacter = expressionSeparatorCharacter;

                    specialOperatorCharactersForUsedInSeparators = specialOperatorCharactersForUsedInSeparators.Where(x => x != expressionSeparatorCharacter).ToList();
                }

                expressionSeparatorCharacterToString = _expressionSeparatorCharacter.ToString();

                if (CodeGenerator.ContainsSpecialNonOperatorCharacters(expressionSeparatorCharacterToString, ';'))
                    continue;

                if (CodeGenerator.ConflictsWithReservedWords(this, expressionSeparatorCharacterToString))
                    continue;

                if (CodeGenerator.ConflictsWithComments(this, expressionSeparatorCharacterToString))
                    continue;

                break;
            }

            Assert.IsNotNull(expressionSeparatorCharacterToString);

            if (!TestSetup.MustSupportCodeBlocks && TestSetup.SimulationRandomNumberGenerator.Next(100) <= 20)
            {
                // Code blocks not supported.
                return;
            }

            //while (true)
            //{
            randomNumber = TestSetup.SimulationRandomNumberGenerator.Next(100);

            if (TestSetup.SimulateNiceCode)
            {
                if (randomNumber <= 50)
                {
                    _codeBlockStartMarker = "{";
                    _codeBlockEndMarker = "}";
                }
                else
                {
                    _codeBlockStartMarker = "BEGIN";
                    _codeBlockEndMarker = "END";
                }
            }
            else
            {
                if (randomNumber <= 33)
                {
                    _codeBlockStartMarker = "{";
                    _codeBlockEndMarker = "}";
                }
                else if (randomNumber <= 66)
                {
                    _codeBlockStartMarker = "BEGIN";
                    _codeBlockEndMarker = "END";
                }
                else
                {
                    _codeBlockStartMarker = TestHelpers.GenerateRandomWord(
                        TestSetup.SimulationRandomNumberGenerator.Next(3, 6),
                        specialOperatorCharactersForUsedInSeparators,
                        specialOperatorCharactersForUsedInSeparators,
                        specialOperatorCharactersForUsedInSeparators,
                        identifier =>
                        {
                            if (CodeGenerator.ConflictsWithReservedWords(this, identifier) ||
                                CodeGenerator.ConflictsWithComments(this, identifier) ||
                                Helpers.CheckIfIdentifiersConflict(this, identifier, expressionSeparatorCharacterToString) ||
                                CodeGenerator.ContainsSpecialNonOperatorCharacters(identifier, '{'))
                                return true;

                            return false;
                        });

                    _codeBlockEndMarker = TestHelpers.GenerateRandomWord(TestSetup.SimulationRandomNumberGenerator.Next(3, 6),
                        specialOperatorCharactersForUsedInSeparators,
                        specialOperatorCharactersForUsedInSeparators,
                        specialOperatorCharactersForUsedInSeparators,
                        identifier =>
                        {
                            if (CodeGenerator.ConflictsWithReservedWords(this, identifier) ||
                                CodeGenerator.ConflictsWithComments(this, identifier) ||
                                Helpers.CheckIfIdentifiersConflict(this, identifier,
                                    expressionSeparatorCharacterToString) ||
                                CodeGenerator.ContainsSpecialNonOperatorCharacters(identifier, '{'))
                                return true;

                            if (Helpers.CheckIfIdentifiersConflict(this, identifier, _codeBlockStartMarker))
                                return true;

                            return false;
                        });
                }
            }

            //return;
            //}
        }

        private StringComparison GetStringComparison()
        {
            return IsLanguageCaseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;
        }


        private void AddKeywords([NotNull] IReadOnlyList<char> specialOperatorCharactersUsedInKeywords)
        {
            _keywords.AddRange(TestSetup.PredefinedKeywords);

            var numberOfKeywords = _keywords.Count + TestSetup.SimulationRandomNumberGenerator.Next(15, 30);

            var keywordsSet = new HashSet<string>(TestSetup.PredefinedKeywords.Select(x => x.Keyword), this.IsLanguageCaseSensitive ? StringComparer.Ordinal : StringComparer.OrdinalIgnoreCase);

            bool KeywordHasConflictsWithOtherIdentifiers(string identifier)
            {
                if (keywordsSet.Contains(identifier) ||
                    CodeGenerator.ConflictsWithReservedWords(this, identifier) ||
                    CodeGenerator.ContainsSpecialNonOperatorCharacters(identifier) ||
                    CodeGenerator.ConflictsWithComments(this, identifier) ||
                    CodeGenerator.ConflictsWithCodeSeparatorMarkers(this, identifier))
                    return true;

                return false;
            }

            int numberOfTrials = 0;
            int maxKeywordSize = 12;
            while (_keywords.Count < numberOfKeywords)
            {
                var keyword = TestSetup.SimulateNiceCode
                    ? $"kwd_{_keywords.Count}"
                    : TestHelpers.GenerateRandomWord(
                        TestSetup.SimulationRandomNumberGenerator.Next(4, maxKeywordSize),
                        specialOperatorCharactersUsedInKeywords,
                        specialOperatorCharactersUsedInKeywords,
                        specialOperatorCharactersUsedInKeywords,
                        KeywordHasConflictsWithOtherIdentifiers);

                if (keyword.All(x => SpecialCharactersCacheThreadStaticContext.Context.IsSpecialOperatorCharacter(x)))
                    keyword = string.Format("{0}{1}", keyword,
                        TestSetup.CodeGenerationHelper.GenerateCharacter(
                        new CharacterTypeProbabilityData(GeneratedCharacterType.Letter, 34),
                        new CharacterTypeProbabilityData(GeneratedCharacterType.Number, 33),
                        new CharacterTypeProbabilityData(GeneratedCharacterType.Underscore, 33)));

                if (KeywordHasConflictsWithOtherIdentifiers(keyword))
                {
                    ++numberOfTrials;

                    if (numberOfTrials % 10 == 0)
                        ++maxKeywordSize;

                    continue;
                }

                numberOfTrials = 0;

                var keywordInfo = new LanguageKeywordInfo(_keywords.Count, keyword);
                _keywords.Add(keywordInfo);
                keywordsSet.Add(keywordInfo.Keyword);
            }
        }

        private string GenerateOperatorPartFromSpecialOperatorCharactersOnly([NotNull] IReadOnlyList<char> specialOperatorCharactersUsedInNonLiterals,
                                                                             [NotNull] Func<string, bool> hasConflictsWithOtherIdentifiers)
        {
            while (true)
            {
                int partLength = TestSetup.SimulationRandomNumberGenerator.Next(1, 3);

                var operatorPartStrBldr = new StringBuilder();

                while (operatorPartStrBldr.Length < partLength)
                {
                    operatorPartStrBldr.Append(specialOperatorCharactersUsedInNonLiterals[
                        TestSetup.SimulationRandomNumberGenerator.Next(specialOperatorCharactersUsedInNonLiterals.Count - 1)]);
                }

                var operatorPart = operatorPartStrBldr.ToString();

                if (!hasConflictsWithOtherIdentifiers(operatorPart))
                    return operatorPart;
            }

        }

        private string GenerateOperatorPart([NotNull] IReadOnlyList<char> specialOperatorCharactersUsedInNonLiterals)
        {
            bool HasConflictsWithOtherIdentifiers(string operatorNamePart)
            {
                if (CodeGenerator.ConflictsWithReservedWords(this, operatorNamePart) ||
                    CodeGenerator.ContainsSpecialNonOperatorCharacters(operatorNamePart) ||
                    CodeGenerator.ConflictsWithComments(this, operatorNamePart) ||
                    CodeGenerator.ConflictsWithCodeSeparatorMarkers(this, operatorNamePart) ||
                    CodeGenerator.ConflictsWithKeywords(this, operatorNamePart))
                    return true;

                return false;
            }

            if (TestSetup.SimulationRandomNumberGenerator.Next(100) <= 50)
                return GenerateOperatorPartFromSpecialOperatorCharactersOnly(specialOperatorCharactersUsedInNonLiterals, HasConflictsWithOtherIdentifiers);

            if (TestSetup.SimulateNiceCode)
            {
                var operatorPartStrBldr = new StringBuilder();

                operatorPartStrBldr.Append($"oper_{TestSetup.SimulationRandomNumberGenerator.Next(0, 1000)}");
                return operatorPartStrBldr.ToString();
            }

            return TestHelpers.GenerateRandomWord(
                TestSetup.SimulationRandomNumberGenerator.Next(3, 8),
                specialOperatorCharactersUsedInNonLiterals,
                specialOperatorCharactersUsedInNonLiterals,
                specialOperatorCharactersUsedInNonLiterals, HasConflictsWithOtherIdentifiers);
        }

        private void AddOperators([NotNull] IReadOnlyList<char> specialOperatorCharactersUsedInOperators)
        {
            if (TestSetup.OperatorNameType == OperatorNameType.NotNiceOperatorNamesWithMultipleParts)
                AddNonNiceOperators(specialOperatorCharactersUsedInOperators);
            else
                AddNiceOperators();

        }

        private void AddNiceOperators()
        {
            foreach (var operatorTypeObj in Enum.GetValues(typeof(OperatorType)))
            {
                var operatorType = (OperatorType)operatorTypeObj;

                var operatorPriorities = OperatorTemplateHelpers.GetOperatorPriorities(operatorType);

                var operatorIndex = 0;

                string operatorTypeText;

                switch (operatorType)
                {
                    case OperatorType.BinaryOperator:
                        operatorTypeText = "bin";
                        break;
                    case OperatorType.PrefixUnaryOperator:
                        operatorTypeText = "pref";
                        break;
                    case OperatorType.PostfixUnaryOperator:
                        operatorTypeText = "post";
                        break;
                    default:
                        throw new Exception($"Unsupported value {operatorType}.");
                }

                foreach (var operatorPriority in operatorPriorities)
                {
                    int numberOfOperatorsForPriority = TestSetup.SimulationRandomNumberGenerator.Next(5, 8);

                    for (int i = 0; i < numberOfOperatorsForPriority; ++i)
                    {
                        int numberOfParts = 1;
                        if (TestSetup.OperatorNameType == OperatorNameType.NiceOperatorNamesWithMultipleParts)
                        {
                            var randomNumber = TestSetup.SimulationRandomNumberGenerator.Next(100);
                            if (randomNumber > 50)
                            {
                                if (randomNumber < 80)
                                    numberOfParts = 2;
                                else
                                    numberOfParts = 3;
                            }
                        }

                        var operatorNamePrefix = new StringBuilder();
                        operatorNamePrefix.Append($"op{operatorIndex}_{operatorTypeText}_pr{(short)(operatorPriority-OperatorPriority.Priority0)}");
                        //operatorNamePrefix.Append($"op{operatorIndex}_{operatorTypeText}_pr{OperatorPriorities.GetPriority(operatorPriority)}");

                        List<string> operatorNameParts = new List<string>();

                        operatorNameParts.Add(operatorNamePrefix.ToString());

                        for (int partInd = 1; partInd < numberOfParts; ++partInd)
                        {
                            operatorNameParts.Add($"{operatorNamePrefix}_part{(partInd + 1)}");
                        }

                        _operators.Add(new OperatorInfoForTesting(operatorNameParts, operatorType, operatorPriority, SpecialOperatorNameType.None));
                        ++operatorIndex;
                    }
                }
            }
        }


        private class PriorityOperatorData
        {
            private static int _currentId;

            public PriorityOperatorData(int operatorPriorityIndex)
            {
                OperatorPriorityIndex = operatorPriorityIndex;
            }

            public int Id { get; } = _currentId++;

            public int OperatorPriorityIndex { get; }

            public List<OperatorNameParts> PriorityOperatorsData { get; } = new List<OperatorNameParts>();

            public override string ToString()
            {
                return $"{nameof(PriorityOperatorData)}, {nameof(Id)}={Id}, {nameof(OperatorPriorityIndex)}={OperatorPriorityIndex}";
            }
        }

        private class OperatorNameParts
        {
            private static int _currentId;
            public int Id { get; } = _currentId++;
            public List<string> NameParts { get; } = new List<string>();

            public override string ToString()
            {
                return $"{nameof(OperatorNameParts)}, {nameof(Id)}={Id}";
            }
        }

        //[Obsolete]
        //private void AddNonNiceOperators_0([NotNull] IReadOnlyList<char> specialOperatorCharactersUsedInOperators)
        //{
        //    var operatorNamePartsToSpecialOperatorTypeMap = new Dictionary<OperatorNameParts, SpecialOperatorNameType>();

        //    var numberOfOperatorsForPriority = TestSetup.SimulationRandomNumberGenerator.Next(15, 25);

        //    var operatorPriorities = Enum.GetValues(typeof(OperatorPriority));

        //    var operatorsPerPriority = new PriorityOperatorData[operatorPriorities.Length];

        //    var stringComparison = this.IsLanguageCaseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;

        //    for (var priorityIndex = 0; priorityIndex < operatorsPerPriority.Length; ++priorityIndex)
        //        operatorsPerPriority[priorityIndex] = new PriorityOperatorData(priorityIndex);

        //    var usedOperatorNames = new HashSet<string>(IsLanguageCaseSensitive ? StringComparer.Ordinal : StringComparer.OrdinalIgnoreCase);

        //    while (operatorsPerPriority[0].PriorityOperatorsData.Count < numberOfOperatorsForPriority)
        //    {
        //        for (var priorityIndex = 0; priorityIndex < operatorPriorities.Length; ++priorityIndex)
        //        {
        //            var operatorsForPriority = operatorsPerPriority[priorityIndex];

        //            bool IsValidOperatorNamePart(string operatorNamePart)
        //            {
        //                if (CodeGenerator.ConflictsWithReservedWords(this, operatorNamePart) ||
        //                    CodeGenerator.ContainsSpecialNonOperatorCharacters(operatorNamePart) ||
        //                    CodeGenerator.ConflictsWithComments(this, operatorNamePart) ||
        //                    CodeGenerator.ConflictsWithCodeSeparatorMarkers(this, operatorNamePart) ||
        //                    CodeGenerator.ConflictsWithKeywords(this, operatorNamePart))
        //                    return false;

        //                //foreach (var priorityOperatorsData in operatorsPerPriority)
        //                //{
        //                //    if (priorityOperatorsData == operatorsForPriority)
        //                //        continue;

        //                //    foreach (var priorityOperatorData in priorityOperatorsData.PriorityOperatorsData)
        //                //    {
        //                //        foreach (var operatorPriorityNamePart in priorityOperatorData.NameParts)
        //                //        {
        //                //            if (string.Equals(operatorNamePart, operatorPriorityNamePart, stringComparison) ||
        //                //                operatorNamePart.Contains(operatorPriorityNamePart) ||
        //                //                operatorPriorityNamePart.Contains(operatorNamePart))
        //                //            {
        //                //                return false;
        //                //            }
        //                //        }
        //                //    }
        //                //}

        //                return true;
        //            }

        //            string GenerateOperatorPartLocal()
        //            {
        //                return GenerateOperatorPart(specialOperatorCharactersUsedInOperators); //, identifier => !IsValidOperatorNamePart(identifier));
        //            }

        //            while (true)
        //            {
        //                var operatorsWithOnePart = new List<OperatorNameParts>();
        //                var operatorsWithTwoParts = new List<OperatorNameParts>();

        //                var operatorsWith1PartOrMore = new List<OperatorNameParts>();
        //                var operatorsWith2PartsOrMore = new List<OperatorNameParts>();

        //                foreach (var currentNameParts in operatorsForPriority.PriorityOperatorsData)
        //                {
        //                    switch (currentNameParts.NameParts.Count)
        //                    {
        //                        case 1:
        //                            operatorsWithOnePart.Add(currentNameParts);
        //                            operatorsWith1PartOrMore.Add(currentNameParts);
        //                            break;

        //                        case 2:
        //                            operatorsWithTwoParts.Add(currentNameParts);
        //                            operatorsWith1PartOrMore.Add(currentNameParts);
        //                            operatorsWith2PartsOrMore.Add(currentNameParts);
        //                            break;

        //                        case 3:
        //                            operatorsWith1PartOrMore.Add(currentNameParts);
        //                            operatorsWith2PartsOrMore.Add(currentNameParts);
        //                            break;
        //                        default:
        //                            throw new Exception("List of name parts should be between 1 and 3.");
        //                    }
        //                }

        //                var specialOperatorNameType = SpecialOperatorNameType.None;

        //                var operatorNameParts = new OperatorNameParts();
        //                var nameParts = operatorNameParts.NameParts;

        //                int operatorPartsCount;

        //                // Lets add 1 part operators first, then two part operators and then 3 part operators
        //                if (operatorsForPriority.PriorityOperatorsData.Count < (numberOfOperatorsForPriority / 3))
        //                    operatorPartsCount = 1;
        //                else if (operatorsForPriority.PriorityOperatorsData.Count < ((numberOfOperatorsForPriority << 1) / 3))
        //                    operatorPartsCount = 2;
        //                else
        //                    operatorPartsCount = 3;

        //                int randomNumber;

        //                if (operatorPartsCount == 1)
        //                {
        //                    nameParts.Add(GenerateOperatorPartLocal());
        //                }
        //                else if (operatorPartsCount == 2)
        //                {
        //                    randomNumber = TestSetup.SimulationRandomNumberGenerator.Next(100);

        //                    if (randomNumber >= 20 && operatorsWithTwoParts.Count > 0)
        //                    {
        //                        // Case when two part operator is a permutation of some other two part operator.
        //                        // or uses one of the parts from the other operator
        //                        var twoPartOperatorNameParts = operatorsWithTwoParts[TestSetup.SimulationRandomNumberGenerator.Next(operatorsWithTwoParts.Count - 1)].NameParts;

        //                        randomNumber = TestSetup.SimulationRandomNumberGenerator.Next(100);

        //                        if (randomNumber <= 10)
        //                        {
        //                            nameParts.Add(twoPartOperatorNameParts[1]);
        //                            nameParts.Add(twoPartOperatorNameParts[0]);

        //                            specialOperatorNameType = SpecialOperatorNameType.UsesOtherOperatorParts;
        //                        }
        //                        else if (randomNumber <= 30)
        //                        {
        //                            nameParts.Add(twoPartOperatorNameParts[0]);
        //                            nameParts.Add(GenerateOperatorPartLocal());
        //                            specialOperatorNameType = SpecialOperatorNameType.UsesOtherOperatorParts;
        //                        }
        //                        else if (randomNumber <= 50)
        //                        {
        //                            nameParts.Add(GenerateOperatorPartLocal());
        //                            nameParts.Add(twoPartOperatorNameParts[0]);
        //                            specialOperatorNameType = SpecialOperatorNameType.UsesOtherOperatorParts;
        //                        }
        //                        else if (randomNumber <= 70)
        //                        {
        //                            nameParts.Add(twoPartOperatorNameParts[1]);
        //                            nameParts.Add(GenerateOperatorPartLocal());
        //                            specialOperatorNameType = SpecialOperatorNameType.UsesOtherOperatorParts;
        //                        }
        //                        else //if (randomNumber <= 70)
        //                        {
        //                            nameParts.Add(GenerateOperatorPartLocal());
        //                            nameParts.Add(twoPartOperatorNameParts[1]);
        //                            specialOperatorNameType = SpecialOperatorNameType.UsesOtherOperatorParts;
        //                        }
        //                    }
        //                    else if (randomNumber <= 50)
        //                    {
        //                        // Cases when 2 part operator starts with the same part or ends with same part that is used in some other one part operator.
        //                        var onePartOperatorNameParts = operatorsWithOnePart[TestSetup.SimulationRandomNumberGenerator.Next(operatorsWithOnePart.Count - 1)].NameParts;

        //                        randomNumber = TestSetup.SimulationRandomNumberGenerator.Next(100);

        //                        if (randomNumber <= 50)
        //                        {
        //                            nameParts.Add(GenerateOperatorPartLocal());
        //                            nameParts.Add(onePartOperatorNameParts[0]);
        //                            specialOperatorNameType = SpecialOperatorNameType.EndsWithOtherOperatorName;
        //                        }
        //                        else
        //                        {
        //                            nameParts.Add(onePartOperatorNameParts[0]);
        //                            nameParts.Add(GenerateOperatorPartLocal());
        //                            specialOperatorNameType = SpecialOperatorNameType.StartsWithOtherOperatorName;
        //                        }
        //                    }
        //                    else
        //                    {
        //                        // Cases when two part operator uses unique parts
        //                        nameParts.Add(GenerateOperatorPartLocal());
        //                        nameParts.Add(GenerateOperatorPartLocal());
        //                    }
        //                }
        //                else
        //                {
        //                    randomNumber = TestSetup.SimulationRandomNumberGenerator.Next(100);

        //                    if (randomNumber <= 30)
        //                    {
        //                        // Cases when 3 part operator uses unique parts
        //                        nameParts.Add(GenerateOperatorPartLocal());
        //                        nameParts.Add(GenerateOperatorPartLocal());
        //                        nameParts.Add(GenerateOperatorPartLocal());
        //                    }
        //                    else if (randomNumber <= 60)
        //                    {
        //                        List<string> otherOperatorNameParts;
        //                        // Cases when 3 part operator starts or ends with parts in some other 1 or 2 part operator.
        //                        while (true)
        //                        {
        //                            otherOperatorNameParts = operatorsWith1PartOrMore[TestSetup.SimulationRandomNumberGenerator.Next(operatorsWith1PartOrMore.Count - 1)].NameParts;

        //                            if (otherOperatorNameParts.Count < 3)
        //                                break;
        //                        }

        //                        foreach (var part in otherOperatorNameParts)
        //                            nameParts.Add(part);

        //                        randomNumber = TestSetup.SimulationRandomNumberGenerator.Next(100);

        //                        if (randomNumber < 75)
        //                            specialOperatorNameType = SpecialOperatorNameType.StartsWithOtherOperatorName;
        //                        else
        //                            specialOperatorNameType = SpecialOperatorNameType.EndsWithOtherOperatorName;

        //                        while (nameParts.Count < 3)
        //                        {
        //                            var randomPart = GenerateOperatorPartLocal();
        //                            if (specialOperatorNameType == SpecialOperatorNameType.StartsWithOtherOperatorName)
        //                                nameParts.Add(randomPart);
        //                            else
        //                                nameParts.Insert(0, randomPart);
        //                        }
        //                    }
        //                    else
        //                    {
        //                        // Cases when 3 part operator is a permutation of some other operator parts plus some random parts

        //                        var otherOperatorNameParts = operatorsWith2PartsOrMore[TestSetup.SimulationRandomNumberGenerator.Next(operatorsWith2PartsOrMore.Count - 1)].NameParts;

        //                        int[] mappedIndexes;
        //                        while (true)
        //                        {
        //                            mappedIndexes = new[] { -1, -1, -1 };
        //                            var candidateIndexes = new List<int>(3);

        //                            for (var i = 0; i < otherOperatorNameParts.Count; ++i)
        //                                candidateIndexes.Add(i);

        //                            for (int operatorPartInd = 0; operatorPartInd < 3; ++operatorPartInd)
        //                            {
        //                                if (TestSetup.SimulationRandomNumberGenerator.Next(100) <= 50)
        //                                {
        //                                    // Do not use part at index operator2PartInd
        //                                    continue;
        //                                }

        //                                var mappedIndex = candidateIndexes[TestSetup.SimulationRandomNumberGenerator.Next(candidateIndexes.Count - 1)];

        //                                mappedIndexes[operatorPartInd] = mappedIndex;

        //                                if (operatorPartInd < 2)
        //                                {
        //                                    candidateIndexes = candidateIndexes.Where(x => x != mappedIndex).ToList();

        //                                    if (candidateIndexes.Count == 0)
        //                                        break;
        //                                }
        //                            }

        //                            // If mapped indexes are [0, 1, 2] then the generated operator info will be the same as some existing operator info
        //                            if (!(mappedIndexes[0] == 0 &&
        //                                  mappedIndexes[1] == 1 && mappedIndexes[2] == 2))
        //                                break;
        //                        }

        //                        for (int i = 0; i < 3; ++i)
        //                        {
        //                            var operator2PartIndex = mappedIndexes[i];

        //                            if (operator2PartIndex >= 0)
        //                                nameParts.Add(otherOperatorNameParts[operator2PartIndex]);
        //                            else
        //                                nameParts.Add(GenerateOperatorPartLocal());
        //                        }

        //                        specialOperatorNameType = SpecialOperatorNameType.UsesOtherOperatorParts;
        //                    }
        //                }

        //                if ((specialOperatorNameType & SpecialOperatorNameType.StartsWithOtherOperatorName) > 0 &&
        //                    TestSetup.SimulationRandomNumberGenerator.Next(100) <= 10)
        //                {
        //                    int numberOfTrials = 0;
        //                    const int maxNumberOfTrials = 300;
        //                    while (numberOfTrials < maxNumberOfTrials)
        //                    {
        //                        var operatorPart = GenerateOperatorPartFromSpecialOperatorCharactersOnly(specialOperatorCharactersUsedInOperators, IsValidOperatorNamePart);

        //                        var modifiedLastNamePart = $"{nameParts[nameParts.Count - 1]}{operatorPart}";

        //                        if (IsValidOperatorNamePart(modifiedLastNamePart))
        //                        {
        //                            specialOperatorNameType = SpecialOperatorNameType.StartsWithOtherOperatorNameWithConcatenatedText;
        //                            nameParts[nameParts.Count - 1] = modifiedLastNamePart;
        //                            break;
        //                        }

        //                        ++numberOfTrials;
        //                    }

        //                    if (numberOfTrials == maxNumberOfTrials)
        //                    {
        //                        LogHelper.Context.Log.WarnFormat("Could not create an operator which starts with other operator name after {0} trials.",
        //                            maxNumberOfTrials);
        //                        continue;
        //                    }
        //                }

        //                var operatorName = string.Join(' ', nameParts);

        //                if (usedOperatorNames.Contains(operatorName))
        //                    continue;

        //                usedOperatorNames.Add(operatorName);

        //                operatorsForPriority.PriorityOperatorsData.Add(operatorNameParts);
        //                operatorNamePartsToSpecialOperatorTypeMap[operatorNameParts] = specialOperatorNameType;
        //                break;
        //            }
        //        }
        //    }

        //    foreach (var operatorTypeObj in Enum.GetValues(typeof(OperatorType)))
        //    {
        //        var operatorType = (OperatorType)operatorTypeObj;

        //        var listOfOperatorsForPriorities = new List<PriorityOperatorData>(operatorsPerPriority);

        //        foreach (var operatorPriorityObj in operatorPriorities)
        //        {
        //            var operatorPriority = (OperatorPriority)operatorPriorityObj;

        //            if (!OperatorTemplateHelpers.ContainsPriority(operatorType, operatorPriority))
        //                continue;

        //            var priorityIndex = TestSetup.SimulationRandomNumberGenerator.Next(0, listOfOperatorsForPriorities.Count - 1);
        //            var priorityOperatorData = listOfOperatorsForPriorities[priorityIndex];

        //            listOfOperatorsForPriorities.RemoveAt(priorityIndex);

        //            foreach (var operatorNameParts in priorityOperatorData.PriorityOperatorsData)
        //            {
        //                var specialOperatorNameType = operatorNamePartsToSpecialOperatorTypeMap[operatorNameParts];

        //                var operatorInfo = new OperatorInfoForTesting(operatorNameParts.NameParts,
        //                    operatorType, operatorPriority, specialOperatorNameType);
        //                _operators.Add(operatorInfo);
        //            }
        //        }
        //    }

        //    foreach (var operatorInfo in _operators)
        //    {
        //        var operatorsWithSimilarName = _operators.Where(x => x.OperatorType != operatorInfo.OperatorType &&
        //                                                             string.Equals(x.Name, operatorInfo.Name, stringComparison)).ToList();

        //        var operatorInfoForTesting = (OperatorInfoForTesting)operatorInfo;

        //        if (operatorsWithSimilarName.Count > 1)
        //            operatorInfoForTesting.SpecialOperatorNameType |= SpecialOperatorNameType.NameSimilarToTwoOtherOperatorTypes;

        //        foreach (var operatorWithSimilarName in operatorsWithSimilarName)
        //        {
        //            switch (operatorWithSimilarName.OperatorType)
        //            {
        //                case OperatorType.BinaryOperator:
        //                    operatorInfoForTesting.SpecialOperatorNameType |= SpecialOperatorNameType.NameSimilarToOtherBinaryOperator;
        //                    break;

        //                case OperatorType.PrefixUnaryOperator:
        //                    operatorInfoForTesting.SpecialOperatorNameType |= SpecialOperatorNameType.NameSimilarToOtherPrefixUnaryOperator;
        //                    break;

        //                case OperatorType.PostfixUnaryOperator:
        //                    operatorInfoForTesting.SpecialOperatorNameType |= SpecialOperatorNameType.NameSimilarToOtherPostfixUnaryOperator;
        //                    break;
        //            }
        //        }
        //    }
        //}

        private void AddNonNiceOperators([NotNull] IReadOnlyList<char> specialOperatorCharactersUsedInOperators)
        {
            var operatorTypeToOperatorNames = new Dictionary<OperatorType, HashSet<string>>();

            var stringComparer = IsLanguageCaseSensitive ? StringComparer.Ordinal : StringComparer.OrdinalIgnoreCase;

            operatorTypeToOperatorNames[OperatorType.BinaryOperator] = new HashSet<string>(stringComparer);
            operatorTypeToOperatorNames[OperatorType.PostfixUnaryOperator] = new HashSet<string>(stringComparer);
            operatorTypeToOperatorNames[OperatorType.PrefixUnaryOperator] = new HashSet<string>(stringComparer);

            var operatorsWith1Part = new List<OperatorInfoForTesting>();
            var operatorsWith2Parts = new List<OperatorInfoForTesting>();

            var operatorsWith1PartOrMore = new List<OperatorInfoForTesting>();
            var operatorsWith2PartsOrMore = new List<OperatorInfoForTesting>();

            string GenerateOperatorPartLocal() => this.GenerateOperatorPart(specialOperatorCharactersUsedInOperators);

            foreach (var operatorPriorityObj in Enum.GetValues(typeof(OperatorPriority)))
            {
                var operatorPriorityEnum = (OperatorPriority)operatorPriorityObj;

                foreach (var operatorTypeObj in Enum.GetValues(typeof(OperatorType)))
                {
                    OperatorType operatorType = (OperatorType)operatorTypeObj;

                    if (!OperatorTemplateHelpers.ContainsPriority(operatorType, operatorPriorityEnum))
                        continue;

                    var usedOperatorNames = operatorTypeToOperatorNames[operatorType];

                    int numberOfOperatorsForPriority = TestSetup.SimulationRandomNumberGenerator.Next(10, 15);

                    for (int operatorIndex = 0; operatorIndex < numberOfOperatorsForPriority; ++operatorIndex)
                    {
                        int operatorPartsCount;

                        // Lets add 1 part operators first, then two part operators and then 3 part operators
                        if (operatorIndex < (numberOfOperatorsForPriority / 3))
                            operatorPartsCount = 1;
                        else if (operatorIndex < ((numberOfOperatorsForPriority << 1) / 3))
                            operatorPartsCount = 2;
                        else
                            operatorPartsCount = 3;

                        while (true)
                        {
                            SpecialOperatorNameType specialOperatorNameType = SpecialOperatorNameType.None;

                            int randomNumber;

                            var operatorParts = new List<string>();

                            if (operatorPartsCount == 1)
                            {
                                operatorParts.Add(GenerateOperatorPartLocal());
                            }
                            else if (operatorPartsCount == 2)
                            {
                                randomNumber = TestSetup.SimulationRandomNumberGenerator.Next(100);

                                if (randomNumber >= 20 && operatorsWith2Parts.Count > 0)
                                {
                                    // Case when two part operator is a permutation of some other two part operator.
                                    // or uses one of the parts from the other operator
                                    var operatorInfo2 = operatorsWith2Parts[TestSetup.SimulationRandomNumberGenerator.Next(operatorsWith2Parts.Count - 1)];

                                    randomNumber = TestSetup.SimulationRandomNumberGenerator.Next(100);

                                    if (randomNumber <= 10)
                                    {
                                        operatorParts.Add(operatorInfo2.NameParts[1]);
                                        operatorParts.Add(operatorInfo2.NameParts[0]);
                                    }
                                    else if (randomNumber <= 30)
                                    {
                                        operatorParts.Add(operatorInfo2.NameParts[0]);
                                        operatorParts.Add(GenerateOperatorPartLocal());
                                    }
                                    else if (randomNumber <= 50)
                                    {
                                        operatorParts.Add(GenerateOperatorPartLocal());
                                        operatorParts.Add(operatorInfo2.NameParts[0]);
                                    }
                                    else if (randomNumber <= 70)
                                    {
                                        operatorParts.Add(operatorInfo2.NameParts[1]);
                                        operatorParts.Add(GenerateOperatorPartLocal());
                                    }
                                    else //if (randomNumber <= 70)
                                    {
                                        operatorParts.Add(GenerateOperatorPartLocal());
                                        operatorParts.Add(operatorInfo2.NameParts[1]);
                                    }
                                }
                                else if (randomNumber <= 50)
                                {
                                    // Cases when 2 part operator starts with the same part or ends with same part that is used in some other one part operator.
                                    var operatorInfo2 = operatorsWith1Part[TestSetup.SimulationRandomNumberGenerator.Next(operatorsWith1Part.Count - 1)];

                                    randomNumber = TestSetup.SimulationRandomNumberGenerator.Next(100);

                                    if (randomNumber <= 50)
                                    {
                                        operatorParts.Add(GenerateOperatorPartLocal());
                                        operatorParts.Add(operatorInfo2.NameParts[0]);
                                        specialOperatorNameType = SpecialOperatorNameType.EndsWithOtherOperatorName;
                                    }
                                    else
                                    {
                                        operatorParts.Add(operatorInfo2.NameParts[0]);
                                        operatorParts.Add(GenerateOperatorPartLocal());
                                        specialOperatorNameType = SpecialOperatorNameType.StartsWithOtherOperatorName;
                                    }
                                }
                                else
                                {
                                    // Cases when two part operator uses unique parts
                                    operatorParts.Add(GenerateOperatorPartLocal());
                                    operatorParts.Add(GenerateOperatorPartLocal());
                                }
                            }
                            else
                            {
                                randomNumber = TestSetup.SimulationRandomNumberGenerator.Next(100);

                                if (randomNumber <= 30)
                                {
                                    // Cases when 3 part operator uses unique parts
                                    operatorParts.Add(GenerateOperatorPartLocal());
                                    operatorParts.Add(GenerateOperatorPartLocal());
                                    operatorParts.Add(GenerateOperatorPartLocal());
                                }
                                else if (randomNumber <= 60)
                                {
                                    IOperatorInfo operatorInfo2;
                                    // Cases when 3 part operator starts or ends with parts in some other 1 or 2 part operator.
                                    while (true)
                                    {
                                        operatorInfo2 = operatorsWith1PartOrMore[TestSetup.SimulationRandomNumberGenerator.Next(operatorsWith1PartOrMore.Count - 1)];

                                        if (operatorInfo2.NameParts.Count < 3)
                                            break;
                                    }

                                    foreach (var part in operatorInfo2.NameParts)
                                        operatorParts.Add(part);

                                    randomNumber = TestSetup.SimulationRandomNumberGenerator.Next(100);

                                    if (randomNumber < 75)
                                        specialOperatorNameType = SpecialOperatorNameType.StartsWithOtherOperatorName;
                                    else
                                        specialOperatorNameType = SpecialOperatorNameType.EndsWithOtherOperatorName;

                                    while (operatorParts.Count < 3)
                                    {
                                        var randomPart = GenerateOperatorPartLocal();
                                        if (specialOperatorNameType == SpecialOperatorNameType.StartsWithOtherOperatorName)
                                            operatorParts.Add(randomPart);
                                        else
                                            operatorParts.Insert(0, randomPart);
                                    }
                                }
                                else
                                {
                                    // Cases when 3 part operator is a permutation of some other operator parts plus some random parts

                                    var operatorInfo2 = operatorsWith2PartsOrMore[TestSetup.SimulationRandomNumberGenerator.Next(operatorsWith2PartsOrMore.Count - 1)];

                                    int[] mappedIndexes;
                                    while (true)
                                    {
                                        mappedIndexes = new[] { -1, -1, -1 };
                                        var candidateIndexes = new List<int>(3);

                                        for (var i = 0; i < operatorInfo2.NameParts.Count; ++i)
                                            candidateIndexes.Add(i);

                                        for (int operatorPartInd = 0; operatorPartInd < 3; ++operatorPartInd)
                                        {
                                            if (TestSetup.SimulationRandomNumberGenerator.Next(100) <= 50)
                                            {
                                                // Do not use part at index operator2PartInd
                                                continue;
                                            }

                                            var mappedIndex = candidateIndexes[TestSetup.SimulationRandomNumberGenerator.Next(candidateIndexes.Count - 1)];

                                            mappedIndexes[operatorPartInd] = mappedIndex;

                                            if (operatorPartInd < 2)
                                            {
                                                candidateIndexes = candidateIndexes.Where(x => x != mappedIndex).ToList();

                                                if (candidateIndexes.Count == 0)
                                                    break;
                                            }
                                        }

                                        // If mapped indexes are [0, 1, 2] then the generated operator info will be the same as some existing operator info
                                        if (!(mappedIndexes[0] == 0 &&
                                              mappedIndexes[1] == 1 && mappedIndexes[2] == 2))
                                            break;
                                    }

                                    for (int i = 0; i < 3; ++i)
                                    {
                                        var operator2PartIndex = mappedIndexes[i];

                                        if (operator2PartIndex >= 0)
                                            operatorParts.Add(operatorInfo2.NameParts[operator2PartIndex]);
                                        else
                                            operatorParts.Add(GenerateOperatorPartLocal());
                                    }
                                }
                            }

                            ////var stringComparison = this.IsLanguageCaseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;

                            //if (operatorParts.Any(operatorPart => ContainsCommentMarkers(operatorPart))) //||
                            //                                                                             //  _keywords.Any(keywordInfo => string.Equals(operatorPart, keywordInfo.Keyword, stringComparison))))
                            //    continue;

                            // Check if operator matches exactly some other operator.
                            var operatorName = string.Join(' ', operatorParts);

                            if (usedOperatorNames.Contains(operatorName))
                                continue;

                            usedOperatorNames.Add(operatorName);

                            var operatorInfo = new OperatorInfoForTesting(operatorParts, operatorType, operatorPriorityEnum, specialOperatorNameType);
                            _operators.Add(operatorInfo);

                            if (operatorParts.Count >= 1)
                            {
                                if (operatorParts.Count == 1)
                                    operatorsWith1Part.Add(operatorInfo);

                                operatorsWith1PartOrMore.Add(operatorInfo);
                            }

                            if (operatorParts.Count >= 2)
                            {
                                if (operatorParts.Count == 2)
                                    operatorsWith2Parts.Add(operatorInfo);

                                operatorsWith2PartsOrMore.Add(operatorInfo);
                            }

                            break;
                        }
                    }
                }
            }

            AddSpecialOperators(specialOperatorCharactersUsedInOperators, operatorTypeToOperatorNames);
        }

        private void AddSpecialOperators([NotNull] IReadOnlyList<char> specialOperatorCharactersUsedInNonLiterals,
                                         [NotNull] Dictionary<OperatorType, HashSet<string>> operatorTypeToOperatorNames)
        {
            List<string> GenerateOperatorParts()
            {
                var operatorParts = new List<string>();

                var randomNumber = TestSetup.SimulationRandomNumberGenerator.Next(100);

                operatorParts.Add(GenerateOperatorPart(specialOperatorCharactersUsedInNonLiterals));

                if (randomNumber >= 33)
                {
                    operatorParts.Add(GenerateOperatorPart(specialOperatorCharactersUsedInNonLiterals));

                    if (randomNumber >= 66)
                        operatorParts.Add(GenerateOperatorPart(specialOperatorCharactersUsedInNonLiterals));
                }

                return operatorParts;
            }

            var numberOfSpecialOperators = (int) (_operators.Count * 0.20 + 1);

            void AddOperatorInfo(List<string> operatorParts, OperatorType operatorType, SpecialOperatorNameType specialOperatorNameType)
            {
                var operatorTypePriorities = OperatorTemplateHelpers.GetOperatorPriorities(operatorType);
                var operatorPriorityEnum = operatorTypePriorities[TestSetup.SimulationRandomNumberGenerator.Next(0, operatorTypePriorities.Count - 1)];
                _operators.Add(new OperatorInfoForTesting(operatorParts, operatorType, operatorPriorityEnum, specialOperatorNameType));
            }

            // Add operator name that are used in binary, unary prefix and unary postfix operators 
            for (int specialOperatorsInd = 0; specialOperatorsInd < numberOfSpecialOperators; ++specialOperatorsInd)
            {
                var randomNumber = TestSetup.SimulationRandomNumberGenerator.Next(100);

                bool operatorsAdded = false;

                bool AddOperatorsOnOperatorNameIsValid(Action addOperatorOnNameIsValidAction, List<string> operatorNameParts, params OperatorType[] operatorTypesToCheck)
                {
                    if (operatorTypesToCheck == null || operatorTypesToCheck.Length == 0 || operatorTypesToCheck.Any(x =>
                        operatorTypesToCheck.Where(y => y == x).ToList().Count > 1))
                        throw new ArgumentException(nameof(operatorTypesToCheck));

                    var operatorName = string.Join(' ', operatorNameParts);

                    if (operatorTypesToCheck.Any(operatorType => operatorTypeToOperatorNames[operatorType].Contains(operatorName)))
                        return false;

                    foreach (var operatorType in operatorTypesToCheck)
                        operatorTypeToOperatorNames[operatorType].Add(operatorName);

                    addOperatorOnNameIsValidAction();
                    operatorsAdded = true;
                    return true;
                }

                var maxNumberOfTrials = 100;
                for (var trial = 0; trial < maxNumberOfTrials; ++trial)
                {
                    var operatorParts = GenerateOperatorParts();

                    if (randomNumber <= 25)
                    {
                        if (AddOperatorsOnOperatorNameIsValid(() =>
                        {
                            AddOperatorInfo(operatorParts, OperatorType.BinaryOperator, SpecialOperatorNameType.NameSimilarToTwoOtherOperatorTypes);
                            AddOperatorInfo(operatorParts, OperatorType.PrefixUnaryOperator, SpecialOperatorNameType.NameSimilarToTwoOtherOperatorTypes);
                            AddOperatorInfo(operatorParts, OperatorType.PostfixUnaryOperator, SpecialOperatorNameType.NameSimilarToTwoOtherOperatorTypes);
                        }, operatorParts, OperatorType.BinaryOperator, OperatorType.PrefixUnaryOperator, OperatorType.PostfixUnaryOperator))
                            break;
                    }
                    else if (randomNumber <= 50)
                    {
                        if (AddOperatorsOnOperatorNameIsValid(() =>
                        {
                            AddOperatorInfo(operatorParts, OperatorType.BinaryOperator, SpecialOperatorNameType.NameSimilarToOtherPrefixUnaryOperator);
                            AddOperatorInfo(operatorParts, OperatorType.PrefixUnaryOperator, SpecialOperatorNameType.NameSimilarToOtherBinaryOperator);
                        }, operatorParts, OperatorType.BinaryOperator, OperatorType.PrefixUnaryOperator))
                            break;
                    }
                    else if (randomNumber <= 75)
                    {
                        if (AddOperatorsOnOperatorNameIsValid(() =>
                        {
                            AddOperatorInfo(operatorParts, OperatorType.BinaryOperator, SpecialOperatorNameType.NameSimilarToOtherPostfixUnaryOperator);
                            AddOperatorInfo(operatorParts, OperatorType.PostfixUnaryOperator, SpecialOperatorNameType.NameSimilarToOtherBinaryOperator);
                        }, operatorParts, OperatorType.BinaryOperator, OperatorType.PostfixUnaryOperator))
                            break;
                    }
                    else
                    {
                        if (AddOperatorsOnOperatorNameIsValid(() =>
                        {
                            AddOperatorInfo(operatorParts, OperatorType.PrefixUnaryOperator, SpecialOperatorNameType.NameSimilarToOtherPostfixUnaryOperator);
                            AddOperatorInfo(operatorParts, OperatorType.PostfixUnaryOperator, SpecialOperatorNameType.NameSimilarToOtherPrefixUnaryOperator);
                        }, operatorParts, OperatorType.PrefixUnaryOperator, OperatorType.PostfixUnaryOperator))
                            break;
                    }
                }

                if (!operatorsAdded)
                    throw new Exception($"Failed to add special operators after {maxNumberOfTrials} trials. {nameof(specialOperatorsInd)}={randomNumber}, {nameof(specialOperatorsInd)}={randomNumber}.");
            }
        }

        private readonly List<char> _constantTextStartEndMarkerCharacters = new List<char>(3);
        public override IReadOnlyList<char> ConstantTextStartEndMarkerCharacters => _constantTextStartEndMarkerCharacters;


        private string _lineCommentText;

        /// <inheritdoc />
        public override string LanguageName => "Test successful parsing";

        /// <inheritdoc />
        public override string Description => "Test successful parsing";

        /// <inheritdoc />
        public override string LineCommentMarker => _lineCommentText;

        private string _multilineCommentStartMarker;

        /// <inheritdoc />
        public override string MultilineCommentStartMarker => _multilineCommentStartMarker;

        private string _multilineCommentEndMarker;

        /// <inheritdoc />
        public override string MultilineCommentEndMarker => _multilineCommentEndMarker;

        /// <inheritdoc />
        public override IReadOnlyList<IOperatorInfo> Operators => _operators;

        /// <inheritdoc />
        public override IReadOnlyList<ILanguageKeywordInfo> Keywords => _keywords;

        private string _codeBlockStartMarker;
        public override string CodeBlockStartMarker => _codeBlockStartMarker;

        private string _codeBlockEndMarker;
        public override string CodeBlockEndMarker => _codeBlockEndMarker;

        private char _expressionSeparatorCharacter;
        public override char ExpressionSeparatorCharacter => _expressionSeparatorCharacter;

        public override bool SupportsKeywords => true;
        public override bool SupportsPrefixes => true;

        //char characterAfterMatchedText, int positionInText
        public override bool IsValidLiteralCharacter(char character, int positionInLiteral, ITextSymbolsParserState textSymbolsParserState)
        {
            //if (textSymbolsParserState != null && TestSetup.CurrentCommentMarkersData != null &&
            //    TestSetup.CurrentCommentMarkersData.CommentMarkerType == TestSetup.CommentMarkerType.RemarkText)
            //{
            //    bool IsValidTextAfterMatchedText(char characterAfterMatchedText, int positionInText)
            //    {
            //        return true;
            //    }

            //    // This is not efficient, but takes care of the case when the comments start with non special characters (e.g., rem, or rem*);
            //    if (_lineCommentText != null &&
            //        Helpers.StartsWith(textSymbolsParserState.TextToParse, _lineCommentText, textSymbolsParserState.PositionInText,
            //            textSymbolsParserState.ParsedTextEnd, this.IsLanguageCaseSensitive, IsValidTextAfterMatchedText) ||

            //        _multilineCommentStartMarker != null &&
            //        Helpers.StartsWith(textSymbolsParserState.TextToParse, _multilineCommentStartMarker, textSymbolsParserState.PositionInText,
            //            textSymbolsParserState.ParsedTextEnd, this.IsLanguageCaseSensitive, IsValidTextAfterMatchedText))
            //        return false;
            //}

            if (Helpers.IsLatinLetter(character))
                return true;

            if (Char.IsNumber(character) || character == '.')
                return positionInLiteral > 0;

            return _setOfSpecialOperatorCharactersUsedInLiterals.Contains(character) ||
                   character == '_';
        }

        //public override bool IsCriticalError(int parseErrorItemCode)
        //{
        //    if (base.IsCriticalError(parseErrorItemCode))
        //        return true;

        //    switch (parseErrorItemCode)
        //    {
        //        case TestLanguageErrorCodes.TypeNameMissingInWhereExpression:
        //        case TestLanguageErrorCodes.ColonMissingInWhereExpression:
        //        case TestLanguageErrorCodes.TypeConstraintMissingInWhereExpression:
        //        case TestLanguageErrorCodes.WhereExpressionFollowedByInvalidSymbol:
        //        case TestLanguageErrorCodes.TypeNameOccursMultipleTimesInWhereExpression:

        //        case TestLanguageErrorCodes.GenericTypesKeywordShouldBeFollowedByRoundBraces:
        //        case TestLanguageErrorCodes.GenericTypesKeywordMissesTypeNames:

        //        case TestLanguageErrorCodes.PerformanceKeywordShouldBeFollowedByRoundBraces:
        //        case TestLanguageErrorCodes.PragmaKeywordShouldBeFollowedByValidSymbol:
        //            return false;
        //        default:
        //            return true;
        //    }
        //}

        public sealed override bool IsLanguageCaseSensitive { get; }

        /// <inheritdoc />
        public override IEnumerable<ICustomExpressionItemParser> CustomExpressionItemParsers { get; }

        //private void AddKeywordsAndOperators()
        //{
        //    _operators = new List<IOperatorInfo>()
        //    {
        //        // Unary prefix operators
        //        new OperatorInfoWithAutoId("-", OperatorType.PrefixUnaryOperator, 0),
        //        new OperatorInfoWithAutoId("!", OperatorType.PrefixUnaryOperator, 0),
        //        new OperatorInfoWithAutoId("++", OperatorType.PrefixUnaryOperator, 0),
        //        new OperatorInfoWithAutoId("--", OperatorType.PrefixUnaryOperator, 0),
        //        new OperatorInfoWithAutoId("TO_-BIG_+NUM", OperatorType.PrefixUnaryOperator, 0),
        //        new OperatorInfoWithAutoId( new [] {"$TO+", "-INT" }, OperatorType.PrefixUnaryOperator, 0),
        //        new OperatorInfoWithAutoId( new [] {"TO+", "+INT?" }, OperatorType.PrefixUnaryOperator, 0),


        //        // Unary prefix operators with low priority (preceding and following binary operators will be applied first)
        //        new OperatorInfoWithAutoId("NOT", OperatorType.PrefixUnaryOperator, 10000),
        //        new OperatorInfoWithAutoId("NOT$", OperatorType.PrefixUnaryOperator, 10000),
        //        new OperatorInfoWithAutoId(new [] {"PREF-", "OP1" }, OperatorType.PrefixUnaryOperator, 10000),
        //        new OperatorInfoWithAutoId(new [] {"PREF+", "-OP2" }, OperatorType.PrefixUnaryOperator, 10000),

        //        // Unary postfix operators
        //        new OperatorInfoWithAutoId(new [] {"IS", "NULL" }, OperatorType.PrefixUnaryOperator, 100),
        //        new OperatorInfoWithAutoId(new [] {"IS", "NULL", "NOT" }, OperatorType.PrefixUnaryOperator, 100),
        //        new OperatorInfoWithAutoId(new [] {"$IS", "-NULL" }, OperatorType.PrefixUnaryOperator, 100),
        //        new OperatorInfoWithAutoId(new [] {"IS+", "-NULL$" }, OperatorType.PrefixUnaryOperator, 100),
        //        new OperatorInfoWithAutoId(new [] {"IS-", "-NOT+", "-NULL" }, OperatorType.PrefixUnaryOperator, 100),


        //        // Unary postfix operators with low priority (preceding and following binary operators will be applied first)
        //        new OperatorInfoWithAutoId("POST_PROCESS", OperatorType.PostfixUnaryOperator, 10000),
        //        new OperatorInfoWithAutoId("POST_PROCESS$", OperatorType.PostfixUnaryOperator, 10000),
        //        new OperatorInfoWithAutoId("$POST_PROCESS", OperatorType.PostfixUnaryOperator, 10000),
        //        new OperatorInfoWithAutoId(new [] {"POST+", "-PROCESS1" }, OperatorType.PostfixUnaryOperator, 10000),
        //        new OperatorInfoWithAutoId(new [] { "POST--", "++PROCESS2" }, OperatorType.PostfixUnaryOperator, 10000),

        //        // Binary operators
        //        new OperatorInfoWithAutoId("&", OperatorType.BinaryOperator, 200),
        //        new OperatorInfoWithAutoId("|", OperatorType.BinaryOperator, 300),

        //        new OperatorInfoWithAutoId("*", OperatorType.BinaryOperator, 400),
        //        new OperatorInfoWithAutoId("/", OperatorType.BinaryOperator, 400),

        //        new OperatorInfoWithAutoId("+", OperatorType.BinaryOperator, 200),
        //        new OperatorInfoWithAutoId("-", OperatorType.BinaryOperator, 200),

        //        new OperatorInfoWithAutoId(">", OperatorType.BinaryOperator, 600),
        //        new OperatorInfoWithAutoId(">=", OperatorType.BinaryOperator, 600),
        //        new OperatorInfoWithAutoId("<", OperatorType.BinaryOperator, 600),
        //        new OperatorInfoWithAutoId("<=", OperatorType.BinaryOperator, 600),


        //        new OperatorInfoWithAutoId("&&", OperatorType.BinaryOperator, 700),
        //        new OperatorInfoWithAutoId("AND", OperatorType.BinaryOperator, 700),

        //        new OperatorInfoWithAutoId("||", OperatorType.BinaryOperator, 800),
        //        new OperatorInfoWithAutoId("OR", OperatorType.BinaryOperator, 800),

        //        // Multi-part binary operators with special characters (not at the beginning or end)
        //        new OperatorInfoWithAutoId(new [] { "BIN--", "++op1" }, OperatorType.BinaryOperator, 200),
        //        new OperatorInfoWithAutoId(new [] { "BIN--", "++op1", "--op" }, OperatorType.BinaryOperator, 200),

        //        new OperatorInfoWithAutoId(new [] { "$BIN--", "-op2" }, OperatorType.BinaryOperator, 300),
        //        new OperatorInfoWithAutoId(new [] { "$BIN--", "*op2", "/op" }, OperatorType.BinaryOperator, 300),

        //        new OperatorInfoWithAutoId("=>", OperatorType.BinaryOperator, 1000),

        //        new OperatorInfoWithAutoId("=", OperatorType.BinaryOperator, int.MaxValue),
        //    };

        //    _keywords = new List<ILanguageKeywordInfo>()
        //    {
        //        // Example: public abstract ::types(T1, T2, T3) Func1(x: T1, y:T2) : T3
        //        //                  where T1: class where T2: IInterface2, new() where T3: IInterface3;
        //        new LanguageKeywordInfo(KeywordIds.GenericTypesKeywordId, "::types"),

        //        // Example: public abstract ::types(T1, T2, T3) Func1(x: T1, y:T2) : T3
        //        //                  where T1: class where T2: IInterface2, new() where T3: IInterface3;
        //        new LanguageKeywordInfo(KeywordIds.WhereKeywordId, "where"),

        //        // Example: F1(x, ::performance("AnonymousFuncStatistics1") () =>  x^2 + factorial(x)}
        //        new LanguageKeywordInfo(KeywordIds.PerformanceKeywordId, "::performance"),

        //        // Example: print("Is in debug mode=" + ::pragma IsDebugMode)
        //        new LanguageKeywordInfo(KeywordIds.PragmaKeywordId, "::pragma"),

        //        // Example public ::metadata({attributes: [Attribute1, Attribute2]; DisplayName: "Factorial"}) Fact(x: int): int
        //        // {
        //        //      if (x <= 1) return 1;
        //        //      return x: Factorial(x - 1);
        //        // }
        //        new LanguageKeywordInfo(KeywordIds.MetadataKeywordId, "::metadata"),

        //        new LanguageKeywordInfo(0, "include"),

        //        new LanguageKeywordInfo(1, "private"),
        //        new LanguageKeywordInfo(2, "protected"),
        //        new LanguageKeywordInfo(3, "public"),
        //        new LanguageKeywordInfo(4, "var"),

        //        new LanguageKeywordInfo(5, "out"),
        //        new LanguageKeywordInfo(6, "ref"),
        //        new LanguageKeywordInfo(7, "_"),

        //        new LanguageKeywordInfo(8, "class"),
        //        new LanguageKeywordInfo(9, "keyw1"),
        //        new LanguageKeywordInfo(10, "keyw2$"),
        //        new LanguageKeywordInfo(11, "$keyw3"),
        //        new LanguageKeywordInfo(12, "::section"),
        //        new LanguageKeywordInfo(13, "var"),
        //        new LanguageKeywordInfo(14, "out"),
        //        new LanguageKeywordInfo(15, "ref"),
        //        new LanguageKeywordInfo(16, "_"),
        //    };
        //}
    }
}
