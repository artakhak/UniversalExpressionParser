using System;
using JetBrains.Annotations;
using System.Collections.Generic;
using System.Linq;
using UniversalExpressionParser.ExpressionItems;
using UniversalExpressionParser.ExpressionItems.Custom;

namespace UniversalExpressionParser.Tests.TestLanguage
{
    public class CustomExpressionItemParserMock : ICustomExpressionItemParser
    {
        public const int CustomErrorCode = 15000;

        public delegate (int errorPosition, bool returnsNullForCustomExpressionItem, bool isCriticalError) AddParseErrorOnDelegate();

        [CanBeNull]
        public delegate CustomExpressionItemMock TryCreateCustomExpressionItemForKeywordDelegate([NotNull] IParseExpressionItemContext context,
            long keywordId,
            [NotNull, ItemNotNull] IReadOnlyList<IExpressionItemBase> parsedPrefixExpressionItems,
            [NotNull, ItemNotNull] IReadOnlyList<IKeywordExpressionItem> parsedKeywordExpressionItemsWithoutLastKeyword,
            [NotNull] IKeywordExpressionItem lastKeywordExpressionItem);
        
        [CanBeNull]
        public TryCreateCustomExpressionItemForKeywordDelegate TryCreateCustomExpressionItemForKeyword { get; set; }

        public bool ThrowException { get; set; }

        /// <summary>
        /// Set to delegate to make the parser add parse error.
        /// The first parameter re
        /// </summary>
        public AddParseErrorOnDelegate AddErrorOnParse { get; set; }

        public ICustomExpressionItem TryParseCustomExpressionItem(IParseExpressionItemContext context, 
            IReadOnlyList<IExpressionItemBase> parsedPrefixExpressionItems, IReadOnlyList<IKeywordExpressionItem> parsedKeywordExpressionItems)
        {
            if (ThrowException)
                throw new System.Exception("Testing custom expression parser exception.");

            if (parsedKeywordExpressionItems.Count == 0)
                return null;

            if (AddErrorOnParse != null)
            {
                var errorData = AddErrorOnParse();

                context.AddParseErrorItem(new ParseErrorItem(context.TextSymbolsParser.PositionInText,
                    () => "Parser generated error",
                    CustomErrorCode, errorData.isCriticalError));

                if (errorData.returnsNullForCustomExpressionItem)
                    return null;
            }

            var parsedKeywordExpressionItemsWithoutLastKeyword = parsedKeywordExpressionItems.Take(parsedKeywordExpressionItems.Count - 1).ToList();

            var lastKeywordExpressionItem = parsedKeywordExpressionItems[^1];
            var customExpressionItem = TryCreateCustomExpressionItemForKeyword?.Invoke(context, lastKeywordExpressionItem.LanguageKeywordInfo.Id,
                parsedPrefixExpressionItems, parsedKeywordExpressionItemsWithoutLastKeyword, lastKeywordExpressionItem);

            if (customExpressionItem == null)
                return null;

            if (!context.SkipSpacesAndComments())
                return customExpressionItem;

            IComplexExpressionItem childExpressionItem = null;

            var currentPositionInText = context.TextSymbolsParser.PositionInText;
           
            if (context.TextSymbolsParser.CurrentChar == '(' || context.TextSymbolsParser.CurrentChar == '[')
            {
                childExpressionItem = context.ParseBracesExpression(null);
            }
            else if (context.StartsWithSymbol(context.ExpressionLanguageProviderWrapper.ExpressionLanguageProvider.CodeBlockStartMarker))
            {
                childExpressionItem = context.ParseCodeBlockExpression();
            }
            else if (context.TryParseSymbol(out var parsedLiteral))
            {
                var literalExpressionItem = new LiteralExpressionItem(Array.Empty<IExpressionItemBase>(), Array.Empty<IKeywordExpressionItem>(), new LiteralNameExpressionItem(parsedLiteral, currentPositionInText));
                childExpressionItem = literalExpressionItem;

                if (context.SkipSpacesAndComments() && (context.TextSymbolsParser.CurrentChar == '(' || context.TextSymbolsParser.CurrentChar == '['))
                {
                    var bracesExpressionItem = context.ParseBracesExpression(null);

                    childExpressionItem = new BracesExpressionItem(Array.Empty<IExpressionItemBase>(), Array.Empty<IKeywordExpressionItem>(),
                        literalExpressionItem,
                        bracesExpressionItem.OpeningBrace.IsRoundBrace,
                        context.TextSymbolsParser.PositionInText);
                }
            }

            if (childExpressionItem != null)
                customExpressionItem.AddRegularExpressionItem(childExpressionItem);
            return customExpressionItem;
        }
    }
}