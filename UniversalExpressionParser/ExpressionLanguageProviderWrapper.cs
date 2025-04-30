// Copyright (c) UniversalExpressionParser Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace UniversalExpressionParser
{
    /// <inheritdoc />
    internal class ExpressionLanguageProviderWrapper : IExpressionLanguageProviderWrapper
    {
        [NotNull]
        private readonly Dictionary<long, IOperatorInfo> _operatorIdToOperatorInfo = new Dictionary<long, IOperatorInfo>();

        [NotNull]
        private readonly Dictionary<long, ILanguageKeywordInfo> _keywordIdToKeywordInfo = new Dictionary<long, ILanguageKeywordInfo>();

        [NotNull] private readonly List<ILanguageKeywordInfo> _sortedKeywordInfos;

        /// <summary>
        /// A wrapper around expression language providers.
        /// </summary>
        /// <param name="expressionLanguageProvider">Wrapped expression language provider.</param>
        public ExpressionLanguageProviderWrapper([NotNull] IExpressionLanguageProvider expressionLanguageProvider)
        {
            ExpressionLanguageProvider = expressionLanguageProvider;

            var stringComparer = ExpressionLanguageProvider.IsLanguageCaseSensitive ? StringComparer.Ordinal : StringComparer.OrdinalIgnoreCase;

            _sortedKeywordInfos = new List<ILanguageKeywordInfo>(expressionLanguageProvider.Keywords);

            _sortedKeywordInfos.Sort((x, y) =>
            {
                if (x.Keyword.Length > y.Keyword.Length)
                {
                    if (x.Keyword.StartsWith(y.Keyword))
                        return -1;
                }
                else if (y.Keyword.Length > x.Keyword.Length)
                {
                    if (y.Keyword.StartsWith(x.Keyword))
                        return 1;
                }

                return stringComparer.Compare(x.Keyword, y.Keyword);
            });

            foreach (var keywordInfo in _sortedKeywordInfos)
                _keywordIdToKeywordInfo[keywordInfo.Id] = keywordInfo;

            foreach (var operatorInfo in ExpressionLanguageProvider.Operators)
                _operatorIdToOperatorInfo[operatorInfo.Id] = operatorInfo;
        }

        /// <inheritdoc />
        public IExpressionLanguageProvider ExpressionLanguageProvider { get; }

        /// <inheritdoc />
        public IReadOnlyList<ILanguageKeywordInfo> SortedKeywordInfos => _sortedKeywordInfos;

        /// <inheritdoc />
        public ILanguageKeywordInfo GetKeywordInfo(long keywordId)
        {
            return _keywordIdToKeywordInfo[keywordId];
        }

        /// <inheritdoc />
        public IOperatorInfo GetOperatorInfo(long operatorId)
        {
            return _operatorIdToOperatorInfo[operatorId];
        }
    }
}