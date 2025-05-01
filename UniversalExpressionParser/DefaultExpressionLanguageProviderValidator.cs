// Copyright (c) UniversalExpressionParser Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace UniversalExpressionParser
{
    /// <summary>
    /// Default implementation for <see cref="IExpressionLanguageProviderValidator"/>.
    /// </summary>
    public class DefaultExpressionLanguageProviderValidator : IExpressionLanguageProviderValidator
    {
        /// <summary>
        /// Validates the <paramref name="expressionLanguageProvider"/>. Thrown an exception <see cref="ExpressionLanguageProviderException"/>
        /// if validation fails.
        /// </summary>
        /// <param name="expressionLanguageProvider"></param>
        /// <exception cref="ExpressionLanguageProviderException">Throws this exception if validation fails.</exception>
        public void Validate(IExpressionLanguageProvider expressionLanguageProvider)
        {
            ValidateCommentMarkers(expressionLanguageProvider);
            ValidateCodeBlockMarkers(expressionLanguageProvider);

            ValidateKeywords(expressionLanguageProvider);
            ValidateOperators(expressionLanguageProvider);
            ValidateTextEnclosingCharacters(expressionLanguageProvider);

            ValidateNumericTypeDescriptors(expressionLanguageProvider);
            ValidateLanguageLiterals(expressionLanguageProvider);
        }

        private void ValidateCommentMarkers([NotNull] IExpressionLanguageProvider expressionLanguageProvider)
        {
            if (expressionLanguageProvider.LineCommentMarker == null ||
                expressionLanguageProvider.MultilineCommentStartMarker == null ||
                expressionLanguageProvider.MultilineCommentEndMarker == null)
            {
                if (expressionLanguageProvider.LineCommentMarker != null ||
                    expressionLanguageProvider.MultilineCommentStartMarker != null ||
                    expressionLanguageProvider.MultilineCommentEndMarker != null)
                    throw new ExpressionLanguageProviderException(expressionLanguageProvider,
                        $"Either the values of {nameof(IExpressionLanguageProvider.LineCommentMarker)}, {nameof(IExpressionLanguageProvider.MultilineCommentStartMarker)}, and {nameof(IExpressionLanguageProvider.MultilineCommentEndMarker)} should all be either null or non-null att the same time.");

                return;
            }

            var stringComparison = GetStringComparison(expressionLanguageProvider);

            if (!Helpers.ValidateTextValueIsNotNulOrEmptyAndHasNoSpaces(expressionLanguageProvider.LineCommentMarker, nameof(IExpressionLanguageProvider.LineCommentMarker), out var errorMessage))
                throw new ExpressionLanguageProviderException(expressionLanguageProvider, errorMessage);

            if (!Helpers.ValidateTextValueIsNotNulOrEmptyAndHasNoSpaces(expressionLanguageProvider.MultilineCommentStartMarker, nameof(IExpressionLanguageProvider.MultilineCommentStartMarker), out errorMessage))
                throw new ExpressionLanguageProviderException(expressionLanguageProvider, errorMessage);

            if (!Helpers.ValidateTextValueIsNotNulOrEmptyAndHasNoSpaces(expressionLanguageProvider.MultilineCommentEndMarker, nameof(IExpressionLanguageProvider.MultilineCommentEndMarker), out errorMessage))
                throw new ExpressionLanguageProviderException(expressionLanguageProvider, errorMessage);

            if (expressionLanguageProvider.LineCommentMarker.Equals(expressionLanguageProvider.MultilineCommentStartMarker, stringComparison))
                throw new ExpressionLanguageProviderException(expressionLanguageProvider, $"The value of {nameof(IExpressionLanguageProvider.LineCommentMarker)} cannot be the same as \"{nameof(IExpressionLanguageProvider.MultilineCommentStartMarker)}\".");

            if (expressionLanguageProvider.LineCommentMarker.Equals(expressionLanguageProvider.MultilineCommentEndMarker, stringComparison))
                throw new ExpressionLanguageProviderException(expressionLanguageProvider, $"The value of {nameof(IExpressionLanguageProvider.LineCommentMarker)} cannot be the same as \"{nameof(IExpressionLanguageProvider.MultilineCommentEndMarker)}\".");

            if (expressionLanguageProvider.MultilineCommentStartMarker.Equals(expressionLanguageProvider.MultilineCommentEndMarker, stringComparison))
                throw new ExpressionLanguageProviderException(expressionLanguageProvider, $"The value of {nameof(IExpressionLanguageProvider.MultilineCommentStartMarker)} cannot be the same as \"{nameof(IExpressionLanguageProvider.MultilineCommentEndMarker)}\".");

            ValidateWordDoesNotUseSpecialNonOperatorCharacters(expressionLanguageProvider,
                nameof(IExpressionLanguageProvider.LineCommentMarker), expressionLanguageProvider.LineCommentMarker);

            ValidateWordDoesNotUseSpecialNonOperatorCharacters(expressionLanguageProvider,
                nameof(IExpressionLanguageProvider.MultilineCommentStartMarker), expressionLanguageProvider.MultilineCommentStartMarker);

            ValidateWordDoesNotUseSpecialNonOperatorCharacters(expressionLanguageProvider,
                nameof(IExpressionLanguageProvider.MultilineCommentEndMarker), expressionLanguageProvider.MultilineCommentEndMarker);
        }

        private void ValidateCodeBlockMarkers([NotNull] IExpressionLanguageProvider expressionLanguageProvider)
        {
            var codeBlockStartText = expressionLanguageProvider.CodeBlockStartMarker;
            var codeBlockEndText = expressionLanguageProvider.CodeBlockEndMarker;
            var expressionSeparatorCharacter = expressionLanguageProvider.ExpressionSeparatorCharacter;

            var expressionSeparatorCharacterToString = expressionSeparatorCharacter.ToString();

            if (codeBlockStartText != null || codeBlockEndText != null)
            {
                if (codeBlockStartText == null || codeBlockEndText == null)
                    throw new ExpressionLanguageProviderException(expressionLanguageProvider,
                    $"Either the values of {nameof(IExpressionLanguageProvider.CodeBlockStartMarker)} and {nameof(IExpressionLanguageProvider.CodeBlockEndMarker)} should be both null, or both should be non null.");

                if (expressionSeparatorCharacter == '\0')
                    throw new ExpressionLanguageProviderException(expressionLanguageProvider,
                        $"If the values of {nameof(IExpressionLanguageProvider.CodeBlockStartMarker)} and {nameof(IExpressionLanguageProvider.CodeBlockEndMarker)} are not null, the value of {nameof(IExpressionLanguageProvider.ExpressionSeparatorCharacter)} should be a valid code separator character, and cannot be '\\0'.");

                if (!Helpers.ValidateTextValueIsNotNulOrEmptyAndHasNoSpaces(codeBlockStartText, nameof(IExpressionLanguageProvider.CodeBlockStartMarker), out var errorMessage))
                    throw new ExpressionLanguageProviderException(expressionLanguageProvider, errorMessage);

                if (!Helpers.ValidateTextValueIsNotNulOrEmptyAndHasNoSpaces(codeBlockEndText, nameof(IExpressionLanguageProvider.CodeBlockEndMarker), out errorMessage))
                    throw new ExpressionLanguageProviderException(expressionLanguageProvider, errorMessage);

                ValidateWordDoesNotHaveConflictsWithComments(expressionLanguageProvider, nameof(IExpressionLanguageProvider.CodeBlockStartMarker), codeBlockStartText);
                ValidateWordDoesNotUseSpecialNonOperatorCharacters(expressionLanguageProvider,
                        nameof(IExpressionLanguageProvider.CodeBlockStartMarker), codeBlockStartText, '{');

                ValidateWordDoesNotHaveConflictsWithComments(expressionLanguageProvider, nameof(IExpressionLanguageProvider.CodeBlockEndMarker), codeBlockEndText);
                ValidateWordDoesNotUseSpecialNonOperatorCharacters(expressionLanguageProvider,
                        nameof(IExpressionLanguageProvider.CodeBlockEndMarker), codeBlockEndText, '}');

                if (Helpers.CheckIfIdentifiersConflict(expressionLanguageProvider, codeBlockEndText,
                    codeBlockStartText,
                    nameof(IExpressionLanguageProvider.CodeBlockEndMarker), nameof(IExpressionLanguageProvider.CodeBlockStartMarker),
                    out errorMessage))
                    throw new ExpressionLanguageProviderException(expressionLanguageProvider, errorMessage);

                if (Helpers.CheckIfIdentifiersConflict(expressionLanguageProvider, codeBlockStartText,
                    expressionSeparatorCharacterToString,
                    nameof(IExpressionLanguageProvider.CodeBlockStartMarker), nameof(IExpressionLanguageProvider.ExpressionSeparatorCharacter),
                    out errorMessage))
                    throw new ExpressionLanguageProviderException(expressionLanguageProvider, errorMessage);

                if (Helpers.CheckIfIdentifiersConflict(expressionLanguageProvider, codeBlockEndText,
                    expressionSeparatorCharacterToString,
                    nameof(IExpressionLanguageProvider.CodeBlockEndMarker), nameof(IExpressionLanguageProvider.ExpressionSeparatorCharacter),
                    out errorMessage))
                    throw new ExpressionLanguageProviderException(expressionLanguageProvider, errorMessage);
            }

            if (expressionLanguageProvider.ExpressionSeparatorCharacter != '\0')
            {
                if (!Helpers.ValidateTextValueIsNotNulOrEmptyAndHasNoSpaces(expressionLanguageProvider.ExpressionSeparatorCharacter.ToString(),
                        nameof(IExpressionLanguageProvider.ExpressionSeparatorCharacter), out var errorMessage))
                    throw new ExpressionLanguageProviderException(expressionLanguageProvider, errorMessage);

                if (!(expressionLanguageProvider.ExpressionSeparatorCharacter == ';' ||
                      Helpers.IsSpecialOperatorCharacter(expressionLanguageProvider.ExpressionSeparatorCharacter)))
                    throw new ExpressionLanguageProviderException(expressionLanguageProvider,
                        $"The value of \"{nameof(IExpressionLanguageProvider.ExpressionSeparatorCharacter)}\" can be either ';' or one of operator characters [{string.Join(",", Helpers.SpecialOperatorCharacters)}].");

                ValidateWordDoesNotHaveConflictsWithComments(expressionLanguageProvider, nameof(IExpressionLanguageProvider.ExpressionSeparatorCharacter), expressionSeparatorCharacterToString);
            }
        }

        private void ValidateWordDoesNotHaveConflictsWithComments([NotNull] IExpressionLanguageProvider expressionLanguageProvider, [NotNull] string validatedObjectName, [NotNull] string validatedObject)
        {
            if (expressionLanguageProvider.LineCommentMarker != null &&
                Helpers.CheckIfIdentifierIsInAnotherIdentifier(expressionLanguageProvider,
                expressionLanguageProvider.LineCommentMarker, validatedObject,
                 nameof(IExpressionLanguageProvider.LineCommentMarker), validatedObjectName, out var errorMessage))
                throw new ExpressionLanguageProviderException(expressionLanguageProvider, errorMessage);

            if (expressionLanguageProvider.MultilineCommentStartMarker != null &&
                Helpers.CheckIfIdentifierIsInAnotherIdentifier(expressionLanguageProvider,
                    expressionLanguageProvider.MultilineCommentStartMarker, validatedObject,
                    nameof(IExpressionLanguageProvider.MultilineCommentStartMarker), validatedObjectName, out errorMessage))
                throw new ExpressionLanguageProviderException(expressionLanguageProvider, errorMessage);

            if (expressionLanguageProvider.MultilineCommentEndMarker != null &&
                Helpers.CheckIfIdentifierIsInAnotherIdentifier(expressionLanguageProvider,
                    expressionLanguageProvider.MultilineCommentEndMarker, validatedObject,
                    nameof(IExpressionLanguageProvider.MultilineCommentEndMarker), validatedObjectName, out errorMessage))
                throw new ExpressionLanguageProviderException(expressionLanguageProvider, errorMessage);
        }

        private void ValidateWordDoesNotHaveConflictsWithCodeBlockSeparatorMarkers([NotNull] IExpressionLanguageProvider expressionLanguageProvider,
                        [NotNull] string validatedObjectName, [NotNull] string validatedObject)
        {
            if (expressionLanguageProvider.ExpressionSeparatorCharacter != '\0' &&
                Helpers.CheckIfIdentifiersConflict(expressionLanguageProvider,
                    expressionLanguageProvider.ExpressionSeparatorCharacter.ToString(), validatedObject,
                    nameof(IExpressionLanguageProvider.ExpressionSeparatorCharacter), validatedObjectName, out var errorMessage))
                throw new ExpressionLanguageProviderException(expressionLanguageProvider, errorMessage);

            if (expressionLanguageProvider.CodeBlockStartMarker != null &&
                Helpers.CheckIfIdentifierIsInAnotherIdentifier(expressionLanguageProvider,
                    expressionLanguageProvider.CodeBlockStartMarker, validatedObject,
                    nameof(IExpressionLanguageProvider.CodeBlockStartMarker), validatedObjectName, out errorMessage))
                throw new ExpressionLanguageProviderException(expressionLanguageProvider, errorMessage);

            if (expressionLanguageProvider.CodeBlockEndMarker != null &&
                Helpers.CheckIfIdentifierIsInAnotherIdentifier(expressionLanguageProvider,
                    expressionLanguageProvider.CodeBlockEndMarker, validatedObject,
                    nameof(IExpressionLanguageProvider.CodeBlockEndMarker), validatedObjectName, out errorMessage))
                throw new ExpressionLanguageProviderException(expressionLanguageProvider, errorMessage);
        }

        private void ValidateWordDoesNotUseSpecialNonOperatorCharacters([NotNull] IExpressionLanguageProvider expressionLanguageProvider,
            [NotNull] string validatedObjectCapitalizedName, [NotNull] string validatedObject, params char[] allowedNonOperatorSpecialCharacters)
        {
            if (validatedObject.Any(x => (allowedNonOperatorSpecialCharacters == null || !allowedNonOperatorSpecialCharacters.Contains(x)) &&
                                         Helpers.IsSpecialNonOperatorCharacter(x)))
                throw new ExpressionLanguageProviderException(expressionLanguageProvider,
                    $"{validatedObjectCapitalizedName} \"{validatedObject}\" contains one of the special characters [{string.Join(",", Helpers.SpecialNonOperatorCharacters.Select(x => $"'{x}'"))}].");
        }

        private StringComparison GetStringComparison([NotNull] IExpressionLanguageProvider expressionLanguageProvider)
        {
            return expressionLanguageProvider.IsLanguageCaseSensitive
                ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;
        }

        private StringComparer GetStringComparer([NotNull] IExpressionLanguageProvider expressionLanguageProvider)
        {
            return expressionLanguageProvider.IsLanguageCaseSensitive
                ? StringComparer.Ordinal : StringComparer.OrdinalIgnoreCase;
        }

        private void ValidateKeywords([NotNull] IExpressionLanguageProvider expressionLanguageProvider)
        {
            var names = new HashSet<string>(GetStringComparer(expressionLanguageProvider));
            var keywordIds = new HashSet<long>();

            foreach (var keywordInfo in expressionLanguageProvider.Keywords)
            {
                var keyword = keywordInfo.Keyword;
                var keywordId = keywordInfo.Id;

                if (Helpers.IsNullOrEmptyOrHasSpaceCharacters(keywordInfo.Keyword))
                    throw new ExpressionLanguageProviderException(expressionLanguageProvider, $"The keywords in \"{typeof(IExpressionLanguageProvider).FullName}.{nameof(IExpressionLanguageProvider.Keywords)}\" cannot contain spaces. The invalid value is \"{keyword}\".");

                if (names.Contains(keywordInfo.Keyword))
                    throw new ExpressionLanguageProviderException(expressionLanguageProvider, $"The keywords in \"{typeof(IExpressionLanguageProvider).FullName}.{nameof(IExpressionLanguageProvider.Keywords)}\" should be unique. The invalid value is \"{keyword}\".");

                names.Add(keywordInfo.Keyword);

                if (keywordIds.Contains(keywordInfo.Id))
                    throw new ExpressionLanguageProviderException(expressionLanguageProvider, $"The keyword Ids in\"{typeof(IExpressionLanguageProvider).FullName}.{nameof(IExpressionLanguageProvider.Keywords)}\" should be unique. The invalid value is \"{keywordId}\".");

                const string keywordName = "Keyword";
                ValidateWordDoesNotHaveConflictsWithComments(expressionLanguageProvider, keywordName, keyword);
                ValidateWordDoesNotHaveConflictsWithCodeBlockSeparatorMarkers(expressionLanguageProvider, keywordName, keyword);
                ValidateWordDoesNotUseSpecialNonOperatorCharacters(expressionLanguageProvider, keywordName, keyword);

                keywordIds.Add(keywordInfo.Id);
            }
        }

        /// <summary>
        /// Validates that operator info <paramref name="operatorInfo"/> does not have conflicts with keyword <paramref name="languageKeywordInfo"/>.
        /// If there is 
        /// </summary>
        /// <param name="expressionLanguageProvider">Expression language provider being validated</param>
        /// <param name="operatorInfo">Operator to validate.</param>
        /// <param name="languageKeywordInfo">Keyword that needs to be checked for conflicts with operator <paramref name="operatorInfo"/>.</param>
        /// <exception cref="ExpressionLanguageProviderException">Throws this exception if validation fails.</exception>
        protected virtual void ValidateOperatorConflictsWithKeyword([NotNull] IExpressionLanguageProvider expressionLanguageProvider,
            [NotNull] IOperatorInfo operatorInfo, [NotNull] ILanguageKeywordInfo languageKeywordInfo)
        {
            if (operatorInfo.NameParts.Count == 0)
                return;

            if (Helpers.CheckIfIdentifierIsInAnotherIdentifierAtZeroPosition(expressionLanguageProvider,
                    languageKeywordInfo.Keyword, operatorInfo.NameParts[0]))
            {
                throw new ExpressionLanguageProviderException(expressionLanguageProvider,
                    $"Keyword '{languageKeywordInfo.Keyword}' cannot be contained in operator '{Helpers.GetOperatorName(operatorInfo.NameParts)}' at zero position. Operator Id={operatorInfo.Id}, keyword Id={languageKeywordInfo.Id}.");
            }
        }

        private void ValidateOperators([NotNull] IExpressionLanguageProvider expressionLanguageProvider)
        {
            var nameToOperatorInfos = new Dictionary<string, List<IOperatorInfo>>(GetStringComparer(expressionLanguageProvider));
            var operatorIdsToOperatorInfosMap = new Dictionary<long, IOperatorInfo>();

            foreach (var operatorInfo in expressionLanguageProvider.Operators)
            {
                // ReSharper disable once ConditionIsAlwaysTrueOrFalse
                if (operatorInfo.NameParts == null || operatorInfo.NameParts.Count == 0)
                    throw new ExpressionLanguageProviderException(expressionLanguageProvider, $"The value of \"{typeof(IOperatorInfo).FullName}.{nameof(IOperatorInfo.NameParts)}\" cannot be null or an empty list.");

                if (operatorIdsToOperatorInfosMap.TryGetValue(operatorInfo.Id, out var duplicateOperatorInfo))
                    throw new ExpressionLanguageProviderException(expressionLanguageProvider, $"Operator \"{operatorInfo.Name}\" has a value \"{typeof(IOperatorInfo).FullName}.{nameof(IOperatorInfo.Id)}\" which is the same as the Id of a different operator \"{duplicateOperatorInfo.Name}\".");

                foreach (var operatorNamePart in operatorInfo.NameParts)
                {
                    if (Helpers.IsNullOrEmptyOrHasSpaceCharacters(operatorNamePart))
                        throw new ExpressionLanguageProviderException(expressionLanguageProvider, $"Operators cannot contain space characters. The invalid value is \"{operatorNamePart}\" in operator {operatorInfo.Name}.");

                    var operatorNamePartName = "Operator part";
                    ValidateWordDoesNotHaveConflictsWithComments(expressionLanguageProvider, operatorNamePartName, operatorNamePart);
                    ValidateWordDoesNotHaveConflictsWithCodeBlockSeparatorMarkers(expressionLanguageProvider, operatorNamePartName, operatorNamePart);
                    ValidateWordDoesNotUseSpecialNonOperatorCharacters(expressionLanguageProvider, operatorNamePartName, operatorNamePart);
                }

                foreach (var keywordInfo in expressionLanguageProvider.Keywords)
                    ValidateOperatorConflictsWithKeyword(expressionLanguageProvider, operatorInfo, keywordInfo);

                operatorIdsToOperatorInfosMap[operatorInfo.Id] = operatorInfo;
                
                if (!nameToOperatorInfos.TryGetValue(operatorInfo.Name, out var operatorInfos))
                {
                    operatorInfos = new List<IOperatorInfo>(3);
                    nameToOperatorInfos[operatorInfo.Name] = operatorInfos;
                }
                else
                {
                    duplicateOperatorInfo = operatorInfos.FirstOrDefault(x => x.OperatorType == operatorInfo.OperatorType);

                    if (duplicateOperatorInfo != null)
                        throw new ExpressionLanguageProviderException(expressionLanguageProvider, $"Multiple occurrences of operator \"{operatorInfo.Name}\" of type \"{operatorInfo.OperatorType}\" in \"{nameof(IExpressionLanguageProvider.Operators)}\". Operators of the same type cannot be declared multiple times. For example it is possible to have operator '-' as a binary and unary prefix operators, but there cannot be multiple binary operators '-'.");
                }

                operatorInfos.Add(operatorInfo);
            }
        }

        private void ValidateLanguageLiterals([NotNull] IExpressionLanguageProvider expressionLanguageProvider)
        {
            var specialOperatorCharacterToCommentMarkers =
                new Dictionary<char, List<string>>();

            var specialOperatorCharacterToCodeSeparatorMarkers =
                new Dictionary<char, List<string>>();

            var specialOperatorCharacterToOperators = new Dictionary<char, List<IOperatorInfo>>();

            var specialOperatorCharacterToKeywords =
                new Dictionary<char, List<ILanguageKeywordInfo>>();

            for (var i = 0; i <= 9; ++i)
            {
                var numericCharacter = $"{i}"[0];
                if (expressionLanguageProvider.IsValidLiteralCharacter(numericCharacter, 0, null))
                    throw new ExpressionLanguageProviderException(expressionLanguageProvider,
                        $"Method call \"{expressionLanguageProvider.GetType().FullName}.{nameof(IExpressionLanguageProvider.IsValidLiteralCharacter)}()\" cannot return true for '{numericCharacter}' when parameter 'positionInLiteral' is 0. Literals cannot start with numeric values.");
            }

            foreach (var specialOperatorChar in Helpers.SpecialOperatorCharacters)
            {
                var commentMarkers = new List<string>();
                specialOperatorCharacterToCommentMarkers[specialOperatorChar] = commentMarkers;

                if (expressionLanguageProvider.LineCommentMarker?.Contains(specialOperatorChar) ?? false)
                    commentMarkers.Add(expressionLanguageProvider.LineCommentMarker);

                if (expressionLanguageProvider.MultilineCommentStartMarker?.Contains(specialOperatorChar) ?? false)
                    commentMarkers.Add(expressionLanguageProvider.MultilineCommentStartMarker);

                if (expressionLanguageProvider.MultilineCommentEndMarker?.Contains(specialOperatorChar) ?? false)
                    commentMarkers.Add(expressionLanguageProvider.MultilineCommentEndMarker);

                var codeSeparatorMarkers = new List<string>(3);
                specialOperatorCharacterToCodeSeparatorMarkers[specialOperatorChar] = codeSeparatorMarkers;

                if (specialOperatorChar == expressionLanguageProvider.ExpressionSeparatorCharacter)
                    codeSeparatorMarkers.Add(expressionLanguageProvider.ExpressionSeparatorCharacter.ToString());

                if (expressionLanguageProvider.CodeBlockStartMarker?.Contains(specialOperatorChar) ?? false)
                    codeSeparatorMarkers.Add(expressionLanguageProvider.CodeBlockStartMarker);

                if (expressionLanguageProvider.CodeBlockEndMarker?.Contains(specialOperatorChar) ?? false)
                    codeSeparatorMarkers.Add(expressionLanguageProvider.CodeBlockEndMarker);

                var operators = new List<IOperatorInfo>();
                specialOperatorCharacterToOperators[specialOperatorChar] = operators;

                foreach (var operatorInfo in expressionLanguageProvider.Operators)
                {
                    if (operatorInfo.NameParts.Any(namePart => namePart.Contains(specialOperatorChar)))
                        operators.Add(operatorInfo);
                }

                var keywords = new List<ILanguageKeywordInfo>();
                specialOperatorCharacterToKeywords[specialOperatorChar] = keywords;

                foreach (var keywordInfo in expressionLanguageProvider.Keywords)
                {
                    if (keywordInfo.Keyword.Contains(specialOperatorChar))
                        keywords.Add(keywordInfo);
                }
            }

            for (var positionInLiteral = 0; positionInLiteral < 100; ++positionInLiteral)
            {
                foreach (var specialNonOperatorCharacter in Helpers.SpecialNonOperatorCharacters)
                {
                    if (expressionLanguageProvider.IsValidLiteralCharacter(specialNonOperatorCharacter,
                        positionInLiteral, null))
                        throw new ExpressionLanguageProviderException(expressionLanguageProvider,
                            $"Method call \"{expressionLanguageProvider.GetType().FullName}.{nameof(IExpressionLanguageProvider.IsValidLiteralCharacter)}()\" cannot return true for a special character '{specialNonOperatorCharacter}'.");
                }

                foreach (var specialOperatorCharacter in Helpers.SpecialOperatorCharacters)
                {
                    if (!expressionLanguageProvider.IsValidLiteralCharacter(specialOperatorCharacter, positionInLiteral, null))
                        continue;

                    string GetErrorTextForOperatorCharacter(string reason)
                    {
                        return $"Method call \"{expressionLanguageProvider.GetType().FullName}.{nameof(IExpressionLanguageProvider.IsValidLiteralCharacter)}()\" cannot return true for a operator character '{specialOperatorCharacter}' for the following reason: {reason}.";
                    }

                    if (specialOperatorCharacterToCommentMarkers[specialOperatorCharacter].Count > 0)
                        throw new ExpressionLanguageProviderException(expressionLanguageProvider,
                        GetErrorTextForOperatorCharacter($"The operator character is used in comment markers [{string.Join(",", specialOperatorCharacterToCommentMarkers[specialOperatorCharacter].Select(x => $"\"{x}\""))}]"));

                    if (specialOperatorCharacterToCodeSeparatorMarkers[specialOperatorCharacter].Count > 0)
                        throw new ExpressionLanguageProviderException(expressionLanguageProvider,
                            GetErrorTextForOperatorCharacter($"The operator character is used in code separator markers [{string.Join(",", specialOperatorCharacterToCodeSeparatorMarkers[specialOperatorCharacter].Select(x => $"\"{x}\""))}]"));

                    if (specialOperatorCharacterToOperators[specialOperatorCharacter].Count > 0)
                        throw new ExpressionLanguageProviderException(expressionLanguageProvider,
                            GetErrorTextForOperatorCharacter($"The operator character is used in operators [{string.Join(",", specialOperatorCharacterToOperators[specialOperatorCharacter].Select(x => $"\"{x.Name}\""))}]"));

                    if (specialOperatorCharacterToKeywords[specialOperatorCharacter].Count > 0)
                        throw new ExpressionLanguageProviderException(expressionLanguageProvider,
                            GetErrorTextForOperatorCharacter($"The operator character is used in keywords [{string.Join(",", specialOperatorCharacterToKeywords[specialOperatorCharacter].Select(x => $"\"{x.Keyword}\""))}]"));
                }
            }
        }

        private void ValidateTextEnclosingCharacters([NotNull] IExpressionLanguageProvider expressionLanguageProvider)
        {
            var textEnclosingCharactersSet = new HashSet<char>();

            foreach (var textEnclosingCharacter in expressionLanguageProvider.ConstantTextStartEndMarkerCharacters)
            {
                switch (textEnclosingCharacter)
                {
                    case '"':
                    case '\'':
                    case '`':
                        break;
                    default:
                        throw new ExpressionLanguageProviderException(expressionLanguageProvider,
                            $"Invalid character '{textEnclosingCharacter}' in list \"{nameof(IExpressionLanguageProvider.ConstantTextStartEndMarkerCharacters)}\". Only one of the following characters can be used in this list: ', \", or `.");
                }

                if (textEnclosingCharactersSet.Contains(textEnclosingCharacter))
                    throw new ExpressionLanguageProviderException(expressionLanguageProvider,
                    $"Multiple occurrences of '{textEnclosingCharacter}' in list \"{nameof(IExpressionLanguageProvider.ConstantTextStartEndMarkerCharacters)}\".");

                textEnclosingCharactersSet.Add(textEnclosingCharacter);
            }
        }

        private void ValidateNumericTypeDescriptors([NotNull] IExpressionLanguageProvider expressionLanguageProvider)
        {
            string GetValidationText(NumericTypeDescriptor numericTypeDescriptor, string errorMessage) =>
                $"Validation of \"{typeof(IExpressionLanguageProvider).FullName}.{nameof(IExpressionLanguageProvider.NumericTypeDescriptors)}\" failed for descriptor with \"{nameof(NumericTypeDescriptor.NumberTypeId)}\"={numericTypeDescriptor.NumberTypeId}. {errorMessage}";

            var numericTypeIdsSet = new HashSet<long>();

            foreach (var numericTypeDescriptor in expressionLanguageProvider.NumericTypeDescriptors)
            {
                if (numericTypeDescriptor == null)
                    throw new ExpressionLanguageProviderException(expressionLanguageProvider,
                        $"Validation of \"{typeof(IExpressionLanguageProvider).FullName}.{nameof(IExpressionLanguageProvider.NumericTypeDescriptors)}\" failed. Values in \"{typeof(NumericTypeDescriptor).FullName}\" cannot contain null items.");

                if (numericTypeIdsSet.Contains(numericTypeDescriptor.NumberTypeId))
                    throw new ExpressionLanguageProviderException(expressionLanguageProvider,
                        GetValidationText(numericTypeDescriptor, $"Multiple instances of \"{typeof(NumericTypeDescriptor).FullName}\" with the value of \"{nameof(NumericTypeDescriptor.NumberTypeId)}\" equal to {numericTypeDescriptor.NumberTypeId}."));

                numericTypeIdsSet.Add(numericTypeDescriptor.NumberTypeId);

                // ReSharper disable once ConditionIsAlwaysTrueOrFalse
                if (numericTypeDescriptor.RegularExpressions == null || numericTypeDescriptor.RegularExpressions.Count == 0)
                    throw new ExpressionLanguageProviderException(expressionLanguageProvider,
                        GetValidationText(numericTypeDescriptor, $"The list \"{nameof(NumericTypeDescriptor.RegularExpressions)}\" cannot be empty."));

                foreach (var regularExpression in numericTypeDescriptor.RegularExpressions)
                {
                    // ReSharper disable once ConditionIsAlwaysTrueOrFalse
                    if (string.IsNullOrEmpty(regularExpression) || regularExpression.Any(Char.IsWhiteSpace))
                        throw new ExpressionLanguageProviderException(expressionLanguageProvider,
                            GetValidationText(numericTypeDescriptor, $"The list \"{nameof(NumericTypeDescriptor.RegularExpressions)}\" cannot contain null or empty strings, or whitespace characters."));

                    if (!regularExpression.StartsWith("^"))
                        throw new ExpressionLanguageProviderException(expressionLanguageProvider,
                            GetValidationText(numericTypeDescriptor,
                                $"The regular expressions in \"{nameof(NumericTypeDescriptor.RegularExpressions)}\" should start with '^' character."));

                    if (regularExpression.EndsWith("$"))
                        throw new ExpressionLanguageProviderException(expressionLanguageProvider,
                            GetValidationText(numericTypeDescriptor,
                                $"The list \"{nameof(NumericTypeDescriptor.RegularExpressions)}\" cannot contain regular expressions that end with character '$'."));
                }
            }
        }
    }
}
