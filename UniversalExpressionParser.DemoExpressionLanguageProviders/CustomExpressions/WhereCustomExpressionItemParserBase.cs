using System;
using JetBrains.Annotations;
using System.Collections.Generic;
using UniversalExpressionParser.ExpressionItems;
using UniversalExpressionParser.ExpressionItems.Custom;

namespace UniversalExpressionParser.DemoExpressionLanguageProviders.CustomExpressions
{
    /// <summary>
    ///  Example: public abstract ::types(T1, T2, T3) Func1(x: T1, y:T2) : T3
    ///                  where T1: class where T2: IInterface2, new() where T3: IInterface3;
    /// </summary>
    public abstract class WhereCustomExpressionItemParserBase : CustomExpressionItemParserByKeywordId
    {
        protected WhereCustomExpressionItemParserBase() : base(KeywordIds.Where)
        {
        }

        protected abstract bool IsWhereEndReached([NotNull] IParseExpressionItemContext context);

        protected virtual void OnWhereExpressionParsed([NotNull] IWhereCustomExpressionItem whereCustomExpressionItem, [NotNull] IParseExpressionItemContext context)
        {

        }

        /// <inheritdoc />
        protected override ICustomExpressionItem DoParseCustomExpressionItem(IParseExpressionItemContext context,
                                                                           IReadOnlyList<IExpressionItemBase> parsedPrefixExpressionItems,
                                                                           IReadOnlyList<IKeywordExpressionItem> parsedKeywordExpressionItemsWithoutLastKeyword,
                                                                           IKeywordExpressionItem whereKeywordExpressionItem)
        {
            var whereKeywordInfo = whereKeywordExpressionItem.LanguageKeywordInfo;

            var parseState = ParseState.WhereParsed;

            var whereCustomExpressionItem =
                new WhereCustomExpressionItem(parsedPrefixExpressionItems, parsedKeywordExpressionItemsWithoutLastKeyword);
            
            GenericTypeDataExpressionItem genericTypeDataExpressionItem = new GenericTypeDataExpressionItem(whereKeywordExpressionItem);

            whereCustomExpressionItem.AddGenericTypeData(genericTypeDataExpressionItem);

            var textSymbolsParser = context.TextSymbolsParser;

            var typeNameToGenericTypeDataExpressionItem = new HashSet<string>(System.StringComparer.Ordinal);

            while (true)
            {
                // Parse next where constraint.
                if (!context.SkipSpacesAndComments())
                    break;

                int currentPositionInText = context.TextSymbolsParser.PositionInText;

                if (!context.TryParseSymbol(out var parsedLiteral))
                    break;

                if (typeNameToGenericTypeDataExpressionItem.Contains(parsedLiteral))
                {
                    var parsedLiteralLocal = parsedLiteral;
                    context.AddParseErrorItem(new ParseErrorItem(currentPositionInText,
                        () => $"Type name '{parsedLiteralLocal}' appears multiple times in '{whereKeywordInfo.Keyword}' expression.",
                        CustomExpressionParseErrorCodes.TypeNameOccursMultipleTimesInWhereExpression));
                }
                else
                    typeNameToGenericTypeDataExpressionItem.Add(parsedLiteral);

                parseState = ParseState.TypeNameParsed;
                genericTypeDataExpressionItem.AddTypeName(new NameExpressionItem(parsedLiteral,
                    textSymbolsParser.PositionInText - parsedLiteral.Length));

                if (!context.SkipSpacesAndComments() || textSymbolsParser.CurrentChar != ':')
                    break;

                parseState = ParseState.ColonParsed;

                genericTypeDataExpressionItem.AddColon(textSymbolsParser.PositionInText);
                if (!textSymbolsParser.SkipCharacters(1))
                    break;

                // Parse current type constraints
                while (true)
                {
                    if (!context.SkipSpacesAndComments())
                        break;

                    if (!context.TryParseSymbol(out parsedLiteral))
                        break;

                    var literalNameExpressionItem = new LiteralNameExpressionItem(parsedLiteral,
                        textSymbolsParser.PositionInText - parsedLiteral.Length);

                    IExpressionItemBase typeConstraintExpressionItem;

                    if (!context.SkipSpacesAndComments())
                        break;

                    switch (textSymbolsParser.CurrentChar)
                    {
                        case '(':
                        case '[':
                            typeConstraintExpressionItem = context.ParseBracesExpression(
                                new LiteralExpressionItem(Array.Empty<IBracesExpressionItem>(),
                                    Array.Empty<IKeywordExpressionItem>(), literalNameExpressionItem));
                            break;
                        default:
                            typeConstraintExpressionItem = new LiteralExpressionItem(Array.Empty<ExpressionItemBase>(), Array.Empty<KeywordExpressionItem>(), literalNameExpressionItem);
                            break;
                    }

                    if (context.ParseErrorData.HasCriticalErrors)
                        break;

                    genericTypeDataExpressionItem.AddTypeConstraint(typeConstraintExpressionItem);

                    parseState = ParseState.TypeConstraintParsed;

                    if (!context.SkipSpacesAndComments())
                        break;

                    if (textSymbolsParser.CurrentChar == ',')
                    {
                        parseState = ParseState.CommaParsed;
                        genericTypeDataExpressionItem.AddComma(textSymbolsParser.PositionInText);

                        if (!textSymbolsParser.SkipCharacters(1) || !context.SkipSpacesAndComments())
                            break;

                        continue;
                    }

                    if (context.StartsWithSymbol(whereKeywordInfo.Keyword, out var matchedText))
                    {
                        parseState = ParseState.WhereParsed;

                        genericTypeDataExpressionItem =
                            new GenericTypeDataExpressionItem(new KeywordExpressionItem(whereKeywordInfo, matchedText,
                                textSymbolsParser.PositionInText));

                        whereCustomExpressionItem.AddGenericTypeData(genericTypeDataExpressionItem);

                        context.TextSymbolsParser.SkipCharacters(matchedText.Length);
                    }
                    
                    else if (IsWhereEndReached(context))
                    {
                        parseState = ParseState.Completed;
                        this.OnWhereExpressionParsed(whereCustomExpressionItem, context);
                    }
                    else
                    {
                        context.AddParseErrorItem(new ParseErrorItem(textSymbolsParser.PositionInText,
                            () => "Invalid symbol.", CustomExpressionParseErrorCodes.WhereExpressionFollowedByInvalidSymbol));
                        return whereCustomExpressionItem;
                    }

                    break;
                }

                if (parseState == ParseState.WhereParsed)
                    continue;

                break;
            }

            if (!context.ParseErrorData.HasCriticalErrors)
            {
                switch (parseState)
                {
                    case ParseState.WhereParsed:
                        context.AddParseErrorItem(new ParseErrorItem(textSymbolsParser.PositionInText,
                            () => $"Type name missing after '{whereKeywordInfo.Keyword}' expression.", CustomExpressionParseErrorCodes.TypeNameMissingInWhereExpression));
                        break;

                    case ParseState.TypeNameParsed:
                        context.AddParseErrorItem(new ParseErrorItem(textSymbolsParser.PositionInText,
                            () => $"Colon character ':' missing in '{whereKeywordInfo.Keyword}' expression after type name.", CustomExpressionParseErrorCodes.ColonMissingInWhereExpression));
                        break;

                    case ParseState.ColonParsed:
                    case ParseState.CommaParsed:
                        context.AddParseErrorItem(new ParseErrorItem(textSymbolsParser.PositionInText,
                            () => $"Type constraint missing in '{whereKeywordInfo.Keyword}' expression after {(parseState == ParseState.CommaParsed ? "comma" : "colon")}. Valid example is: 'where T1:class, new() where T2: class'", CustomExpressionParseErrorCodes.TypeConstraintMissingInWhereExpression));
                        break;

                    case ParseState.TypeConstraintParsed:
                        context.AddParseErrorItem(new ParseErrorItem(textSymbolsParser.PositionInText, () => "Type constraints is missing after comma. Example: where T: class, new().", 
                            CustomExpressionParseErrorCodes.ColonMissingAfterTypeConstraintInWhereExpression));
                        break;
                }
            }

            return whereCustomExpressionItem;
        }

        private enum ParseState
        {
            WhereParsed,
            TypeNameParsed,
            ColonParsed,
            TypeConstraintParsed,
            CommaParsed,
            Completed
        }
    }
}