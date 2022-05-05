// Copyright (c) UniversalExpressionParser Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using System;
using JetBrains.Annotations;
using System.Collections.Generic;
using UniversalExpressionParser.ExpressionItems.Custom;

namespace UniversalExpressionParser.ExpressionItems
{
    /// <summary>
    /// Stores the data for parsed expression item that might contain other expression items, such as braces, commas, etc.
    /// Examples of subclasses and implementations are <see cref="IBracesExpressionItem"/> and <see cref="BracesExpressionItem"/>.
    /// </summary>
    public interface IComplexExpressionItem : IExpressionItemBase 
    {
        /// <summary>
        /// All expression items contained in parsed expression item. This include prefixes, keywords, braces, commas, children, and postfixes.<br/>
        /// For example the expression "[NotNull] public F1(x1, x2) { return x1 + x2; }", will be parsed to an expression of type <see cref="IBracesExpressionItem"/> for text "F1(x1, x2)" that has the following expression items in <see cref="AllItems"/>:<br/>
        /// -Expression item of type <see cref="IBracesExpressionItem"/> for a prefix "[NotNull]" (this will be also in <see cref="Prefixes"/>).<br/>
        /// -Expression item of type <see cref="IKeywordExpressionItem"/> for a a keyword  "public" (this will be also in <see cref="AppliedKeywords"/>).<br/>
        /// -Expression item of type <see cref="ILiteralExpressionItem"/> for function name "F1".<br/>
        /// -Expression item of type <see cref="ILiteralExpressionItem"/> for opening brace "(".<br/>
        /// -Expression item of type <see cref="ILiteralExpressionItem"/> for parameter "x1".<br/>
        /// -Expression item of type <see cref="ICommaExpressionItem"/> for comma "," between x1 and "2".<br/>
        /// -Expression item of type <see cref="INumericExpressionItem"/> for parameter "2".<br/>
        /// -Expression item of type <see cref="ICommaExpressionItem"/> for comma "," between "2" and "y+1 /*this is a comment*/".<br/>
        /// -Expression item of type <see cref="IOperatorExpressionItem"/> for the binary operator "+" that has two operands "y" and "1".<br/>
        /// -Expression item of type <see cref="ILiteralExpressionItem"/> for closing brace ")".<br/>
        /// -Expression item of type <see cref="ICodeBlockExpressionItem"/> for a postfix in "{ return x1 + x2; }" (will be also added to <see cref="Postfixes"/>).<br/> 
        /// Note: The difference between <see cref="AllItems"/> and <see cref="RegularItems"/> is that <see cref="AllItems"/> contains all expression items in <see cref="RegularItems"/> in addition to prefixes, keywords, and postfixes. IN other words, <see cref="RegularItems"/> does not include prefixes, keywords, and postfixes.
        /// </summary>
        [NotNull, ItemNotNull]
        IEnumerable<IExpressionItemBase> AllItems { get; }

        /// <summary>
        /// List of prefixes.<br/>
        /// Prefixes are one or more expression items that precede some other expression item. <br/>
        /// Prefixes should be before keywords in expression. Prefixes are either expressions that are parsed to instances of <see cref="IBracesExpressionItem"/> with <see cref="IBracesExpressionItem.NameLiteral"/> equal to null (nameless braces), or are expressions that are parsed to <see cref="ICustomExpressionItem"/> with <see cref="ICustomExpressionItem.CustomExpressionItemCategory"/> equal to <see cref="CustomExpressionItemCategory.Prefix"/>.<br/>
        /// Consider the expression "[NotNull, ItemNotNull] (Attribute("MarkedFunction")) F1(x, x2);". The parser will parse "F1(x, x2)" to an expression of type "IBracesExpressionItem", and will add expression items of types "IBracesExpressionItem" for expressions "[NotNull, ItemNotNull]" and "(Attribute("MarkedFunction"))" as prefixes to list in <see cref="Prefixes"/>.<br/>
        /// Prefixes are also in <see cref="AllItems"/><br/>
        /// Note: Prefixes are supported only if the value of property <see cref="IExpressionLanguageProvider.SupportsPrefixes"/> is true.
        /// </summary>
        [NotNull, ItemNotNull]
        IReadOnlyList<IExpressionItemBase> Prefixes { get; }

        /// <summary>
        /// List of keywords that are applied to expression item.<br/>
        /// For example if keywords "public", "static", and "var" and and a binary operator ":" are defined in <see cref="IExpressionLanguageProvider.Keywords"/>,
        /// then in expression "public static var x: int = 5;" expressions "public", "static", and "var" will be parsed to expressions of type <see cref="IKeywordExpressionItem"/> and will be added to property <see cref="AppliedKeywords"/> in a literal expression of type <see cref="ILiteralExpressionItem"/> parsed from text "x".<br/>
        /// Keywords are also in <see cref="AllItems"/><br/>
        /// Note: keywords are supported only if the value of property <see cref="IExpressionLanguageProvider.SupportsKeywords"/> is true.
        /// </summary>
        [NotNull, ItemNotNull]
        IReadOnlyList<IKeywordExpressionItem> AppliedKeywords { get; }

        /// <summary>
        /// List of expression items that are not parts of keywords, prefixes, or postfixes.<br/>
        /// For example the expression "[NotNull] public F1(x1, x2) { return x1 + x2; }", will be parsed to an expression of type <see cref="IBracesExpressionItem"/> for text "F1(x1, x2)" that has the following expression items in <see cref="RegularItems"/>:<br/>
        /// -Expression item of type <see cref="ILiteralExpressionItem"/> for function name "F1".<br/>
        /// -Expression item of type <see cref="IOpeningBraceExpressionItem"/> for opening brace "(".<br/>
        /// -Expression item of type <see cref="ILiteralExpressionItem"/> for parameter "x1".<br/>
        /// -Expression item of type <see cref="ICommaExpressionItem"/> for comma "," between x1 and "2".<br/>
        /// -Expression item of type <see cref="INumericExpressionItem"/> for parameter "2".<br/>
        /// -Expression item of type <see cref="ICommaExpressionItem"/> for comma "," between "2" and "y+1 /*this is a comment*/".<br/>
        /// -Expression item of type <see cref="IOperatorExpressionItem"/> for the binary operator "+" that has two operands "y" and "1".<br/>
        /// -Expression item of type <see cref="IClosingBraceExpressionItem"/> for closing brace ")".<br/>
        /// Note: expression items parsed from prefix "[NotNull]", keyword "public", and postfix { return x1 + x2; }" will not be included in <see cref="RegularItems"/>, but will be in <see cref="AllItems"/>.
        /// </summary>
        [NotNull, ItemNotNull]
        IReadOnlyList<IExpressionItemBase> RegularItems { get; }

        /// <summary>
        /// Child expression items. Only some expression items such as functions, operators or
        /// arrays will have child expression items.
        /// For example the expression "[NotNull] public F1(x1, f : function (x) ) { return x1 + f(5); }", will be parsed to an expression of type <see cref="IBracesExpressionItem"/> for text "F1(x1, f : function (x) )" that has the following expression items in <see cref="Children"/>:<br/>
        /// -Expression item of type <see cref="ILiteralExpressionItem"/> for parameter "x1".<br/>
        /// -Expression item of type <see cref="IOperatorExpressionItem"/> for binary type operator ":" with parameters "f" and "function (x)" in text "f : function (x)".<br/>
        /// Note: <see cref="Children"/> does not include prefixes, keywords, postfixes, braces, comma, etc. 
        /// </summary>
        [NotNull, ItemNotNull]
        IReadOnlyList<IExpressionItemBase> Children { get; }

        /// <summary>
        /// List of postfixes. Postfixes are one or more expression items that are placed after some other expression item. <br/>
        /// Currently two types of postfixes are supported:<br/>
        /// -Code block expression items that are parsed to <see cref="ICodeBlockExpressionItem"/> that succeed another expression item.<br/>
        /// -Expressions that are parsed to custom expression items of <see cref="ICustomExpressionItem"/> with property <see cref="ICustomExpressionItem.CustomExpressionItemCategory"/> equal to <see cref="CustomExpressionItemCategory.Postfix"/>.<br/>
        /// Postfixes are also in <see cref="AllItems"/> after non-postfix expression items.<br/>
        /// For example the expression "[NotNull] public F1(x1, x2) { return x1 + x2; }", will be parsed to an expression of type <see cref="IBracesExpressionItem"/> for text "F1(x1, x2)" which has one postfix in <see cref="Postfixes"/> of type <see cref="ICodeBlockExpressionItem"/> parsed from text { return x1 + x2; }.
        /// </summary>
        [NotNull, ItemNotNull]
        IReadOnlyList<IExpressionItemBase> Postfixes { get; }

        /// <summary>
        /// Removes all prefixes.
        /// </summary>
        void RemovePrefixes();

        /// <summary>
        /// Adds a prefix to <see cref="Prefixes"/>.
        /// </summary>
        /// <param name="prefixExpressionItem">Prefix to add.</param>
        /// <exception cref="ArgumentException">Throws this exception.</exception>
        void AddPrefix([NotNull] IExpressionItemBase prefixExpressionItem);

        /// <summary>
        /// Removes all keywords.
        /// </summary>
        void RemoveKeywords();

        /// <summary>
        /// Adds a keyword to <see cref="AppliedKeywords"/>.
        /// </summary>
        /// <param name="keywordExpressionItem">Added keyword expression item.</param>
        /// <exception cref="ArgumentException">Throws this exception.</exception>
        void AddKeyword([NotNull] IKeywordExpressionItem keywordExpressionItem);

        /// <summary>
        /// Adds a postfix <paramref name="postfixExpressionItem"/> to <see cref="Postfixes"/> as well as to <see cref="AllItems"/>. 
        /// </summary>
        /// <param name="postfixExpressionItem">Postfix to add.</param>
        /// <exception cref="ArgumentException">Throws this exception.</exception>
        void AddPostfix([NotNull] IExpressionItemBase postfixExpressionItem);
    }

    /// <summary>
    /// Extension methods for <see cref="IComplexExpressionItem"/>
    /// </summary>
    public static class ComplexExpressionItemExtensionMethods
    {
        /// <summary>
        /// Returns the last expression item in <see cref="IComplexExpressionItem.AllItems"/> if <see cref="IComplexExpressionItem.AllItems"/> is not empty list. Returns null otherwise.
        /// </summary>
        /// <param name="complexExpressionItem">Expression item.</param>
        [CanBeNull]
        public static IExpressionItemBase GetLastExpressionItem(this IComplexExpressionItem complexExpressionItem)
        {
            if (complexExpressionItem.Postfixes.Count > 0)
                return complexExpressionItem.Postfixes[complexExpressionItem.Postfixes.Count - 1];

            if (complexExpressionItem.RegularItems.Count > 0)
                return complexExpressionItem.RegularItems[complexExpressionItem.RegularItems.Count - 1];

            if (complexExpressionItem.AppliedKeywords.Count > 0)
                return complexExpressionItem.AppliedKeywords[complexExpressionItem.AppliedKeywords.Count - 1];

            if (complexExpressionItem.Prefixes.Count > 0)
                return complexExpressionItem.Prefixes[complexExpressionItem.Prefixes.Count - 1];

            return null;
        }

        /// <summary>
        /// Returns the first expression item in <see cref="IComplexExpressionItem.AllItems"/> if <see cref="IComplexExpressionItem.AllItems"/> is not empty list. Returns null otherwise.
        /// </summary>
        /// <param name="complexExpressionItem">Expression item.</param>
        [CanBeNull]
        public static IExpressionItemBase GetFirstExpressionItem(this IComplexExpressionItem complexExpressionItem)
        {
            if (complexExpressionItem.Prefixes.Count > 0)
                return complexExpressionItem.Prefixes[0];

            if (complexExpressionItem.AppliedKeywords.Count > 0)
                return complexExpressionItem.AppliedKeywords[0];

            if (complexExpressionItem.RegularItems.Count > 0)
                return complexExpressionItem.RegularItems[0];

            if (complexExpressionItem.Postfixes.Count > 0)
                return complexExpressionItem.Postfixes[0];

            return null;
        }

        /// <summary>
        /// Returns the number of all expression items in <see cref="IComplexExpressionItem.AllItems"/>.
        /// </summary>
        /// <param name="complexExpressionItem">Expression item.</param>
        public static int GetAllItemsCount(this IComplexExpressionItem complexExpressionItem)
        {
            return complexExpressionItem.Prefixes.Count + complexExpressionItem.AppliedKeywords.Count +
                   complexExpressionItem.RegularItems.Count + complexExpressionItem.Postfixes.Count;
        }
    }
}