// Copyright (c) UniversalExpressionParser Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace UniversalExpressionParser.ExpressionItems
{
    /// <summary>
    /// Represents an evaluated expression item that might contain other expression items, such as comments, braces, commas, etc.
    /// </summary>
    public abstract class ComplexExpressionItemBase : ExpressionItemBase, IComplexExpressionItem
    {
        [NotNull, ItemNotNull]
        private readonly List<IExpressionItemBase> _prefixes = new List<IExpressionItemBase>(5);

        [NotNull, ItemNotNull]
        private readonly List<IKeywordExpressionItem> _appliedKeywords = new List<IKeywordExpressionItem>(5);

        [NotNull, ItemNotNull]
        private readonly List<IExpressionItemBase> _children = new List<IExpressionItemBase>(5);

        [NotNull, ItemNotNull]
        private readonly List<IExpressionItemBase> _regularItems = new List<IExpressionItemBase>(5);

        [NotNull, ItemNotNull]
        private readonly List<IExpressionItemBase> _postfixes = new List<IExpressionItemBase>(5);

        /// <summary>
        /// Base class for expression items that might contain other expression items.
        /// </summary>
        /// <param name="prefixExpressionItems">
        /// Prefix expression items. For example in "[Attribute1][Attribute2] public class MyClass",
        /// [Attribute1] and [Attribute2] will be parsed as prefixes, and "public", and "class" might be parsed as keywords for "MyClass"
        /// </param>
        /// <param name="keywordExpressionItems">Applied keyword expression items.
        /// For example in public static _counter: int = 5; public and int are keywords applied
        /// to expression _counter.</param>
        protected ComplexExpressionItemBase([NotNull, ItemNotNull] IEnumerable<IExpressionItemBase> prefixExpressionItems,
                                            [NotNull, ItemNotNull] IEnumerable<IKeywordExpressionItem> keywordExpressionItems)
        {
            foreach (var prefixExpressionItem in prefixExpressionItems)
                AddPrefix(prefixExpressionItem);

            foreach (var keywordExpressionItem in keywordExpressionItems)
                AddKeyword(keywordExpressionItem);
        }

        /// <inheritdoc />
        public override int IndexInText
        {
            get
            {
                var firstExpressionItem = this.GetFirstExpressionItem();

                if (firstExpressionItem == null)
                    return -1;

                return firstExpressionItem.IndexInText;
            }
        }

        /// <inheritdoc />
        public override int ItemLength
        {
            get
            {
                var firstExpressionItem = this.GetFirstExpressionItem();

                if (firstExpressionItem == null)
                    return 0;

                var lastExpressionItem = this.GetLastExpressionItem();

                if (lastExpressionItem == null)
                    return firstExpressionItem.ItemLength;

                return lastExpressionItem.IndexInText + lastExpressionItem.ItemLength - firstExpressionItem.IndexInText;
            }
        }

        /// <inheritdoc />
        public IReadOnlyList<IExpressionItemBase> RegularItems => _regularItems;

        /// <inheritdoc />
        public IReadOnlyList<IExpressionItemBase> Children => _children;

        /// <inheritdoc />
        public IEnumerable<IExpressionItemBase> AllItems
        {
            get
            {
                foreach (var prefix in _prefixes)
                    yield return prefix;

                foreach (var keyword in _appliedKeywords)
                    yield return keyword;

                foreach (var expressionItem in _regularItems)
                    yield return expressionItem;

                foreach (var postfix in _postfixes)
                    yield return postfix;
            }
        }

        /// <inheritdoc />
        public IReadOnlyList<IKeywordExpressionItem> AppliedKeywords => _appliedKeywords;

        /// <inheritdoc />
        public IReadOnlyList<IExpressionItemBase> Prefixes => _prefixes;

        /// <inheritdoc />
        public IReadOnlyList<IExpressionItemBase> Postfixes => _postfixes;

        /// <inheritdoc />
        public void RemovePrefixes()
        {
            _prefixes.Clear();
        }

        /// <inheritdoc />
        public void AddPrefix(IExpressionItemBase prefixExpressionItem)
        {
            if (ExpressionItemSettingsAmbientContext.Context.IsAddExpressionItemValidationOn)
            {
                if (_appliedKeywords.Count > 0 && prefixExpressionItem.IndexInText >= _appliedKeywords[_appliedKeywords.Count - 1].IndexInText ||
                    _regularItems.Count > 0 && prefixExpressionItem.IndexInText >= _regularItems[0].IndexInText ||
                    _postfixes.Count > 0 && prefixExpressionItem.IndexInText >= _postfixes[0].IndexInText)
                    throw new ArgumentException($"Items in list {nameof(IComplexExpressionItem.Prefixes)} should be before items in {nameof(IComplexExpressionItem.AppliedKeywords)}, {nameof(IComplexExpressionItem.RegularItems)}, and {nameof(IComplexExpressionItem.Postfixes)}.", nameof(prefixExpressionItem));
                 
                Helpers.AddItemSorted(_prefixes, prefixExpressionItem);
            }
            else
            {
                _prefixes.Add(prefixExpressionItem);
            }

            OnItemAdded(prefixExpressionItem);
        }

        /// <inheritdoc />
        public void RemoveKeywords()
        {
            _appliedKeywords.Clear();
        }

        /// <inheritdoc />
        public void AddKeyword(IKeywordExpressionItem keywordExpressionItem)
        {
            if (ExpressionItemSettingsAmbientContext.Context.IsAddExpressionItemValidationOn)
            {
                if (_prefixes.Count > 0 && keywordExpressionItem.IndexInText <= _prefixes[_prefixes.Count - 1].IndexInText ||
                    _regularItems.Count > 0 && keywordExpressionItem.IndexInText >= _regularItems[0].IndexInText ||
                    _postfixes.Count > 0 && keywordExpressionItem.IndexInText >= _postfixes[0].IndexInText)
                    throw new ArgumentException($"Items in list {nameof(IComplexExpressionItem.AppliedKeywords)} should be after items in {nameof(IComplexExpressionItem.Prefixes)} and before items in {nameof(IComplexExpressionItem.RegularItems)} and {nameof(IComplexExpressionItem.Postfixes)}.", nameof(keywordExpressionItem));

                Helpers.AddItemSorted(_appliedKeywords, keywordExpressionItem);
            }
            else
            {
                _appliedKeywords.Add(keywordExpressionItem);
            }

            OnItemAdded(keywordExpressionItem);
        }

        /// <summary>
        /// Adds regular expression item to <see cref="RegularItems"/>. The item will be included in <see cref="AllItems"/> as well.
        /// </summary>
        /// <param name="expressionItem">Expression item to add.</param>
        /// <exception cref="ArgumentException">Throws this exception.</exception>
        protected void AddRegularItem([NotNull] IExpressionItemBase expressionItem)
        {
            if (ExpressionItemSettingsAmbientContext.Context.IsAddExpressionItemValidationOn)
            {
                if (_prefixes.Count > 0 && expressionItem.IndexInText <= _prefixes[_prefixes.Count - 1].IndexInText ||
                    _appliedKeywords.Count > 0 && expressionItem.IndexInText <= _appliedKeywords[_appliedKeywords.Count - 1].IndexInText ||
                   _postfixes.Count > 0 && expressionItem.IndexInText >= _postfixes[0].IndexInText)
                    throw new ArgumentException($"Items in list {nameof(IComplexExpressionItem.RegularItems)} should be after items in {nameof(IComplexExpressionItem.Prefixes)} and {nameof(IComplexExpressionItem.AppliedKeywords)}, and before items in '{nameof(IComplexExpressionItem.Postfixes)}'.", nameof(expressionItem));

                Helpers.AddItemSorted(_regularItems, expressionItem);
            }
            else
            {
                _regularItems.Add(expressionItem);
            }

            OnItemAdded(expressionItem);
        }

        /// <inheritdoc />
        public void AddPostfix(IExpressionItemBase postfixExpressionItem)
        {
            if (ExpressionItemSettingsAmbientContext.Context.IsAddExpressionItemValidationOn)
            {
                if (_prefixes.Count > 0 && postfixExpressionItem.IndexInText <= _prefixes[_prefixes.Count - 1].IndexInText ||
                    _appliedKeywords.Count > 0 && postfixExpressionItem.IndexInText <= _appliedKeywords[_appliedKeywords.Count - 1].IndexInText ||
                    _regularItems.Count > 0 && postfixExpressionItem.IndexInText <= _regularItems[_regularItems.Count - 1].IndexInText)
                    throw new ArgumentException($"Items in list {nameof(IComplexExpressionItem.Postfixes)} should be after items in {nameof(IComplexExpressionItem.Prefixes)}, {nameof(IComplexExpressionItem.AppliedKeywords)}, and '{nameof(IComplexExpressionItem.RegularItems)}'.", nameof(postfixExpressionItem));

                Helpers.AddItemSorted(_postfixes, postfixExpressionItem);
            }
            else
            {
                _postfixes.Add(postfixExpressionItem);
            }

            OnItemAdded(postfixExpressionItem);
        }

        /// <summary>
        /// Adds <paramref name="childExpressionItem"/> to <see cref="Children"/>. The item will be included in <see cref="AllItems"/> as well.
        /// </summary>
        /// <param name="childExpressionItem">Child expression item.</param>
        /// <exception cref="ArgumentException">Throws this exception.</exception>
        protected void AddChild([NotNull] IExpressionItemBase childExpressionItem)
        {
            AddRegularItem(childExpressionItem);

            if (ExpressionItemSettingsAmbientContext.Context.IsAddExpressionItemValidationOn)
                Helpers.AddItemSorted(_children, childExpressionItem);
            else
                _children.Add(childExpressionItem);
        }

        private void OnItemAdded([NotNull] IExpressionItemBase expressionItem)
        {
            expressionItem.Parent = this;
        }
    }
}