// Copyright (c) UniversalExpressionParser Project. All rights reserved.
// Licensed under the MIT License. See LICENSE in the solution root for license information.

using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using UniversalExpressionParser.ExpressionItems;
using UniversalExpressionParser.ExpressionItems.Custom;

namespace UniversalExpressionParser.Parser
{
    internal class GenerateExpressionItemHelper
    {
        [NotNull] private readonly ParseOperatorsHelper _parseOperatorsHelper;

        internal GenerateExpressionItemHelper([NotNull] ParseOperatorsHelper parseOperatorsHelper)
        {
            _parseOperatorsHelper = parseOperatorsHelper;
        }

        internal IExpressionItemBase GenerateExpressionItem([NotNull] ParseExpressionItemContext context,
                                                    [NotNull] ParseExpressionItemData parseExpressionItemData,
                                                    [CanBeNull] 
                                                    IgnoreBinaryOperatorWhenBreakingPostfixPrefixTieDelegate ignoreBinaryOperatorWhenBreakingPostfixPrefixTie,
                                                    ref int numberOfOperators)
        {
            var allExpressionItems = parseExpressionItemData.AllExpressionItems;

            // Add a a simple name expression for ';' if context.ExpressionItemParsingTerminationType == ExpressionItemParsingTerminationType.ExpressionItemSeparatorCharacter
            if (allExpressionItems.Count == 0)
                return null;

            // Add error if the last expression is custom expression and it is a prefix.
            {
                var lastExpressionItem = allExpressionItems[allExpressionItems.Count - 1];

                if (lastExpressionItem is ICustomExpressionItem customExpressionItem)
                {
                    if (customExpressionItem.CustomExpressionItemCategory == CustomExpressionItemCategory.Prefix &&
                        !context.ParseErrorData.HasCriticalErrors)
                        context.AddParseErrorItem(
                            new ParseErrorItem(lastExpressionItem.IndexInText,
                                () => ExpressionParserMessages.CustomPrefixExpressionHasNoTargetError, ParseErrorItemCode.InvalidUseOfPrefixes));
                }
            }

            SeriesOfExpressionItemsWithErrors CreateSeriesOfExpressionItemsWithErrors()
            {
                var operatorsExpressionItem = new SeriesOfExpressionItemsWithErrors();

                foreach (var expressionItem in allExpressionItems)
                    operatorsExpressionItem.AddExpressionItem(expressionItem);

                return operatorsExpressionItem;
            }

            this._parseOperatorsHelper.ProcessUnprocessedOperators(context, parseExpressionItemData, null,
                ignoreBinaryOperatorWhenBreakingPostfixPrefixTie, ref numberOfOperators);

            // DELETE THE COMMENTED OUT CODE
            //bool someSymbolsAreNotSeparated = false;
            //IOperatorInfoExpressionItem lastOperatorInfoExpressionItem = null;

            //for (int expressionIndex = 0; expressionIndex < allExpressionItems.Count; ++expressionIndex)
            //{
            //    var expressionItem = allExpressionItems[expressionIndex];

            //    if (expressionItem is IComplexExpressionItem complexExpressionItem)
            //    {
            //        foreach (var prefixExpressionItem in complexExpressionItem.Prefixes)
            //        {
            //            if (prefixExpressionItem is IBracesExpressionItem bracesExpressionItem &&
            //                bracesExpressionItem.Children.Count == 0)
            //                context.AddParseErrorItem(
            //                    new ParseErrorItem(prefixExpressionItem.IndexInText,
            //                        () => "Braces cannot be empty when used as a prefix.", CodeParseErrorCode.InvalidUseOfPrefixes));
            //        }

            //        if (expressionItem is IOperatorInfoExpressionItem operatorInfoExpressionItem)
            //        {
            //            lastOperatorInfoExpressionItem = operatorInfoExpressionItem;
            //        }
            //        else if (expressionIndex > 0 &&
            //                 allExpressionItems[expressionIndex - 1] != lastOperatorInfoExpressionItem)
            //        {
            //            someSymbolsAreNotSeparated = true;

            //            if (context.ParseErrorData.GetAllParseErrorItemsAtPosition(expressionItem.IndexInText).Count == 0)
            //                context.AddParseErrorItem(
            //                    new ParseErrorItem(expressionItem.IndexInText,
            //                    () => NoSeparationBetweenSymbolsError, CodeParseErrorCode.NoSeparationBetweenSymbols));
            //        }
            //    }
            //}

            if (parseExpressionItemData.OperatorsCannotBeEvaluated)
                return CreateSeriesOfExpressionItemsWithErrors();

            // If there are more than one operators, return an expression item of type ISeriesOfExpressionItemsWithErrors.
            // Otherwise, return the root item, if operator is correct.
            if (allExpressionItems.Count == 1 && numberOfOperators == 0)
                return allExpressionItems[0];

            var operatorExpressionItems = new LinkedList<OperatorExpressionItem>();
            var operatorPrioritiesSet = new HashSet<int>();

            OperatorExpressionItem currentLeftMostPrefixExpressionItem = null;
            OperatorExpressionItem prevOperatorExpressionItem = null;
            IExpressionItemBase prevExpressionItem = null;
            LinkedList<OperatorExpressionItem> previousPostfixOperatorItems = null;

            // Prepare operators
            for (var expressionIndex = 0; expressionIndex < allExpressionItems.Count; ++expressionIndex)
            {
                var expressionItem = allExpressionItems[expressionIndex];

                if (expressionItem is IComplexExpressionItem complexExpressionItem)
                {
                    foreach (var prefixExpressionItem in complexExpressionItem.Prefixes)
                    {
                        if (prefixExpressionItem is IBracesExpressionItem bracesExpressionItem &&
                            bracesExpressionItem.Children.Count == 0)
                            context.AddParseErrorItem(
                                new ParseErrorItem(prefixExpressionItem.IndexInText,
                                    () => "Braces cannot be empty when used as a prefix.", ParseErrorItemCode.InvalidUseOfPrefixes));
                    }
                }

                if (expressionItem is IOperatorInfoExpressionItem operatorInfoExpressionItem)
                {
                    var operatorInfo = operatorInfoExpressionItem.OperatorInfo;
                    var operatorExpressionItem = new OperatorExpressionItem(operatorInfoExpressionItem);

                    if (prevExpressionItem != null)
                    {
                        operatorExpressionItem.ExpressionItemToLeft = prevExpressionItem;
                        if (prevOperatorExpressionItem != null)
                            prevOperatorExpressionItem.ExpressionItemToRight = operatorExpressionItem;
                    }

                    switch (operatorInfo.OperatorType)
                    {
                        case OperatorType.PrefixUnaryOperator:
                            previousPostfixOperatorItems = null;

                            if (currentLeftMostPrefixExpressionItem != null)
                            {
                                currentLeftMostPrefixExpressionItem.BottomMostUnaryOperator = operatorExpressionItem;
                                prevOperatorExpressionItem.Operand1 = operatorExpressionItem;
                            }
                            else
                            {
                                currentLeftMostPrefixExpressionItem = operatorExpressionItem;

                                operatorExpressionItems.AddLast(operatorExpressionItem);
                            }

                            break;
                        case OperatorType.PostfixUnaryOperator:
                            currentLeftMostPrefixExpressionItem = null;

                            var nextExpressionItem = expressionIndex < allExpressionItems.Count - 1 ? allExpressionItems[expressionIndex + 1] : null;

                            if (!(nextExpressionItem is IOperatorInfoExpressionItem nextOperatorInfoExpression &&
                                  nextOperatorInfoExpression.OperatorInfo.OperatorType == OperatorType.PostfixUnaryOperator))
                            {
                                operatorExpressionItems.AddLast(operatorExpressionItem);

                                // Lets add all unary postfix operators to the left as children
                                if (previousPostfixOperatorItems != null)
                                {
                                    operatorExpressionItem.BottomMostUnaryOperator = previousPostfixOperatorItems.First.Value;
                                    var prevExpressionItemNode = previousPostfixOperatorItems.Last;

                                    while (prevExpressionItemNode != null)
                                    {
                                        var parentOperatorExpressionItem = prevExpressionItemNode.Next == null ? operatorExpressionItem : prevExpressionItemNode.Next.Value;
                                        var previousPostfixOperatorExpressionItem = prevExpressionItemNode.Value;

                                        previousPostfixOperatorExpressionItem.Parent = parentOperatorExpressionItem;
                                        parentOperatorExpressionItem.Operand1 = previousPostfixOperatorExpressionItem;

                                        prevExpressionItemNode = prevExpressionItemNode.Previous;
                                    }

                                    previousPostfixOperatorItems = null;
                                }
                            }
                            else
                            {
                                // Start new chain.
                                if (previousPostfixOperatorItems == null)
                                    previousPostfixOperatorItems = new LinkedList<OperatorExpressionItem>();

                                previousPostfixOperatorItems.AddLast(operatorExpressionItem);
                            }

                            break;
                        case OperatorType.BinaryOperator:
                            currentLeftMostPrefixExpressionItem = null;
                            previousPostfixOperatorItems = null;
                            operatorExpressionItems.AddLast(operatorExpressionItem);
                            break;
                        default:
                            throw new Exception($"Unhandled value '{operatorInfo.OperatorType}'.");
                    }

                    prevOperatorExpressionItem = operatorExpressionItem;
                    prevExpressionItem = prevOperatorExpressionItem;

                    if (operatorExpressionItems.Last != null && operatorExpressionItems.Last.Value == operatorExpressionItem)
                    {
                        // If we added the current operator to linked list, lets add a priority.
                        if (!operatorPrioritiesSet.Contains(operatorInfo.Priority))
                            operatorPrioritiesSet.Add(operatorInfo.Priority);
                    }
                }
                else
                {
                    if (prevOperatorExpressionItem != null)
                        prevOperatorExpressionItem.ExpressionItemToRight = expressionItem;

                    prevExpressionItem = expressionItem;
                    prevOperatorExpressionItem = null;
                    currentLeftMostPrefixExpressionItem = null;
                    previousPostfixOperatorItems = null;
                }
            }

            // Lets assume that we have the following operators with priorities shown in parentheses
            // Unary ?(0)
            // Binary operator *(0)
            // Unary prefix operator --(2)
            // Unary postfix operator >>(1)
            // Unary postfix operator ++(2)
            // Consider the expression (the priorities are displayed on top of operators)
            //   0 2,2 1  0   2,2
            // x ?----y>> * z++++
            // The parser should parse this expression in such a way that it is equivalent to the following expression, when braces are added to 
            // make the operator application order explicit: (x ? ((----(y>>)) * z))++++
            // The reason that ? operator with higher priority (0) is applied after lower priority unary prefix operators ----prefix is that
            // the unary operator with lower priority 2 should be applied after all higher priority operators >> and * are applied.
            // Another approach would be to apply the lower priority unary operators, when a sibling higher priority is applied.
            // Using that approach, the expression above would be parsed to an expression equivalent to ((x ? (----y))>>) * (z+++)
            // For now, we use the first approach.
            var sortedOperatorPriorities = operatorPrioritiesSet.ToList();
            sortedOperatorPriorities.Sort();

            LinkedListNode<OperatorExpressionItem> rootNode = null;

            IExpressionItemBase GetOperand(IExpressionItemBase siblingExpressionItem)
            {
                var operand = siblingExpressionItem;

                var currentParent = operand.Parent;

                while (currentParent != null)
                {
                    operand = currentParent;
                    currentParent = currentParent.Parent;
                }

                return operand;
            }

            void ProcessOperatorExpressionItem(LinkedListNode<OperatorExpressionItem> operatorExpressionItemNode)
            {
                OperatorExpressionItem operatorExpressionItem = operatorExpressionItemNode.Value;

                switch (operatorExpressionItem.OperatorInfoExpressionItem.OperatorInfo.OperatorType)
                {
                    case OperatorType.BinaryOperator:
                        var prevNode = operatorExpressionItemNode.Previous;
                        if (prevNode != null && prevNode.Value == operatorExpressionItem.ExpressionItemToLeft)
                        {
                            // If we have a operators as in example "x-- + y", and binary operator '+' is higher priority (applies earlier) than postfix '--',
                            // we still want to process the postfix '--' before processing '+', in case the postfix was not yet processed.
                            ProcessOperatorExpressionItem(prevNode);
                            operatorExpressionItems.Remove(prevNode);
                        }

                        operatorExpressionItem.Operand1 = GetOperand(operatorExpressionItem.ExpressionItemToLeft);
                        if (operatorExpressionItem.ExpressionItemToRight != null)
                        {
                            var nextNode = operatorExpressionItemNode.Next;
                            if (nextNode != null && nextNode.Value == operatorExpressionItem.ExpressionItemToRight)
                            {
                                // If we have a operators as in example "x + --y", and binary operator '+' is higher priority (applies earlier) than prefix '--',
                                // we still want to process the prefix '--' before processing '+', in case the prefix was not yet processed.
                                ProcessOperatorExpressionItem(nextNode);
                                operatorExpressionItems.Remove(nextNode);
                            }

                            operatorExpressionItem.Operand2 = GetOperand(operatorExpressionItem.ExpressionItemToRight);
                        }

                        break;
                    case OperatorType.PrefixUnaryOperator:

                        if (operatorExpressionItem.BottomMostUnaryOperator != null)
                            operatorExpressionItem.BottomMostUnaryOperator.Operand1 =
                                GetOperand(operatorExpressionItem.BottomMostUnaryOperator.ExpressionItemToRight);
                        else
                            operatorExpressionItem.Operand1 = GetOperand(operatorExpressionItem.ExpressionItemToRight);

                        break;

                    case OperatorType.PostfixUnaryOperator:

                        if (operatorExpressionItem.BottomMostUnaryOperator != null)
                            operatorExpressionItem.BottomMostUnaryOperator.Operand1 =
                                GetOperand(operatorExpressionItem.BottomMostUnaryOperator.ExpressionItemToLeft);
                        else
                            operatorExpressionItem.Operand1 = GetOperand(operatorExpressionItem.ExpressionItemToLeft);

                        break;
                }

                // Lets null out internal properties, since they will hold references that subclasses of public class
                // OperatorExpressionItem will not be able to clear.
                operatorExpressionItem.BottomMostUnaryOperator = null;
                operatorExpressionItem.ExpressionItemToLeft = null;
                operatorExpressionItem.ExpressionItemToRight = null;
            }

            for (int operatorPriorityInd = 0; operatorPriorityInd < sortedOperatorPriorities.Count; ++operatorPriorityInd)
            {
                var operatorPriority = sortedOperatorPriorities[operatorPriorityInd];

                var operatorExpressionItemNode = operatorExpressionItems.First;

                while (operatorExpressionItemNode != null)
                {
                    var operatorExpressionItem = operatorExpressionItemNode.Value;

                    var operatorInfo = operatorExpressionItem.OperatorInfoExpressionItem.OperatorInfo;

                    if (operatorInfo.Priority == operatorPriority)
                    {
                        rootNode = operatorExpressionItemNode;

                        ProcessOperatorExpressionItem(operatorExpressionItemNode);

                        // Lets remove current node from linked list, since we are not going to need it anymore

                        operatorExpressionItemNode = operatorExpressionItemNode.Next;
                        operatorExpressionItems.Remove(rootNode);
                    }
                    else
                    {
                        operatorExpressionItemNode = operatorExpressionItemNode.Next;
                    }
                }
            }

            if (rootNode != null && operatorExpressionItems.Count == 0)
                return rootNode.Value;

            var firstExpressionItem = allExpressionItems[0];

            context.AddParseErrorItem(
                    new ParseErrorItem(firstExpressionItem.IndexInText,
                        () => "Failed to determine operator priorities. This should normally not happened and is indicative of a bug in parser implementation.", ParseErrorItemCode.InvalidUseOfPrefixes));

            return CreateSeriesOfExpressionItemsWithErrors();
        }

        // TODO: DELETE THIS COMMENTED CODE AFTER THE FIRST COMMIT
        //IExpressionItemBase GenerateExpressionItem_OLD([NotNull] ParseExpressionItemContext context,
        //                                           [NotNull] ParseExpressionItemData parseExpressionItemData,
        //                                           ref int numberOfOperators)
        //{
        //    var allExpressionItems = parseExpressionItemData.AllExpressionItems;

        //    // Add a a simple name expression for ';' if context.ExpressionItemParsingTerminationType == ExpressionItemParsingTerminationType.ExpressionItemSeparatorCharacter
        //    if (allExpressionItems.Count == 0)
        //        return null;

        //    // Add error if the last expression is custom expression and it is a prefix.
        //    {
        //        var lastExpressionItem = allExpressionItems[^1];

        //        if (lastExpressionItem is ICustomExpressionItem customExpressionItem)
        //        {
        //            if (customExpressionItem.CustomExpressionItemCategory == CustomExpressionItemCategory.Prefix &&
        //                !context.ParseErrorData.HasCriticalErrors)
        //                context.AddParseErrorItem(
        //                    new ParseErrorItem(lastExpressionItem.IndexInText,
        //                        () => CustomPrefixExpressionHasNoTargetError, ParseErrorItemCode.InvalidUseOfPrefixes));
        //        }
        //    }

        //    OperatorsExpressionItem GetOperatorsExpressionItemWithErrors()
        //    {
        //        var operatorsExpressionItem = new OperatorsExpressionItem();

        //        foreach (var expressionItem in allExpressionItems)
        //            operatorsExpressionItem.AddOperatorOrOperand(expressionItem);

        //        return operatorsExpressionItem;
        //    }

        //    ProcessUnprocessedOperators(context, parseExpressionItemData, null, ref numberOfOperators);

        //    //bool someSymbolsAreNotSeparated = false;
        //    //IOperatorInfoExpressionItem lastOperatorInfoExpressionItem = null;

        //    //for (int expressionIndex = 0; expressionIndex < allExpressionItems.Count; ++expressionIndex)
        //    //{
        //    //    var expressionItem = allExpressionItems[expressionIndex];

        //    //    if (expressionItem is IComplexExpressionItem complexExpressionItem)
        //    //    {
        //    //        foreach (var prefixExpressionItem in complexExpressionItem.Prefixes)
        //    //        {
        //    //            if (prefixExpressionItem is IBracesExpressionItem bracesExpressionItem &&
        //    //                bracesExpressionItem.Children.Count == 0)
        //    //                context.AddParseErrorItem(
        //    //                    new ParseErrorItem(prefixExpressionItem.IndexInText,
        //    //                        () => "Braces cannot be empty when used as a prefix.", ParseErrorItemCode.InvalidUseOfPrefixes));
        //    //        }

        //    //        if (expressionItem is IOperatorInfoExpressionItem operatorInfoExpressionItem)
        //    //        {
        //    //            lastOperatorInfoExpressionItem = operatorInfoExpressionItem;
        //    //        }
        //    //        else if (expressionIndex > 0 &&
        //    //                 allExpressionItems[expressionIndex - 1] != lastOperatorInfoExpressionItem)
        //    //        {
        //    //            someSymbolsAreNotSeparated = true;

        //    //            if (context.ParseErrorData.GetAllParseErrorItemsAtPosition(expressionItem.IndexInText).Count == 0)
        //    //                context.AddParseErrorItem(
        //    //                    new ParseErrorItem(expressionItem.IndexInText,
        //    //                    () => NoSeparationBetweenSymbolsError, ParseErrorItemCode.NoSeparationBetweenSymbols));
        //    //        }
        //    //    }
        //    //}

        //    if (parseExpressionItemData.OperatorsCannotBeEvaluated)
        //        return GetOperatorsExpressionItemWithErrors();

        //    // If there are more than one operators, return an expression item of type ExpressionItemType.Operators.
        //    // Otherwise, return the root item, if operator is correct.
        //    if (allExpressionItems.Count == 1 && numberOfOperators == 0)
        //        return allExpressionItems[0];

        //    var operatorExpressionItems = new LinkedList<OperatorExpressionItem>();

        //    var operatorPrioritiesSet = new HashSet<int>();

        //    OperatorExpressionItem currentLeftMostPrefixExpressionItem = null;
        //    OperatorExpressionItem prevOperatorExpressionItem = null;
        //    IExpressionItemBase prevExpressionItem = null;
        //    LinkedList<OperatorExpressionItem> previousPostfixOperatorItems = null;

        //    // Prepare operators
        //    for (int expressionIndex = 0; expressionIndex < allExpressionItems.Count; ++expressionIndex)
        //    {
        //        var expressionItem = allExpressionItems[expressionIndex];

        //        if (expressionItem is IComplexExpressionItem complexExpressionItem)
        //        {
        //            foreach (var prefixExpressionItem in complexExpressionItem.Prefixes)
        //            {
        //                if (prefixExpressionItem is IBracesExpressionItem bracesExpressionItem &&
        //                    bracesExpressionItem.Children.Count == 0)
        //                    context.AddParseErrorItem(
        //                        new ParseErrorItem(prefixExpressionItem.IndexInText,
        //                            () => "Braces cannot be empty when used as a prefix.", ParseErrorItemCode.InvalidUseOfPrefixes));
        //            }
        //        }

        //        if (expressionItem is IOperatorInfoExpressionItem operatorInfoExpressionItem)
        //        {
        //            var operatorInfo = operatorInfoExpressionItem.OperatorInfo;
        //            var operatorExpressionItem = new OperatorExpressionItem(operatorInfoExpressionItem);

        //            if (prevExpressionItem != null)
        //            {
        //                operatorExpressionItem.ExpressionItemToLeft = prevExpressionItem;
        //                if (prevOperatorExpressionItem != null)
        //                    prevOperatorExpressionItem.ExpressionItemToRight = operatorExpressionItem;

        //            }

        //            switch (operatorInfo.OperatorType)
        //            {
        //                case OperatorType.PrefixUnaryOperator:
        //                    previousPostfixOperatorItems = null;

        //                    if (currentLeftMostPrefixExpressionItem != null)
        //                    {
        //                        currentLeftMostPrefixExpressionItem.BottomMostUnaryOperator = operatorExpressionItem;
        //                        prevOperatorExpressionItem.Operand1 = operatorExpressionItem;
        //                    }
        //                    else
        //                    {
        //                        currentLeftMostPrefixExpressionItem = operatorExpressionItem;
        //                        operatorExpressionItems.AddLast(operatorExpressionItem);
        //                    }

        //                    break;
        //                case OperatorType.PostfixUnaryOperator:
        //                    currentLeftMostPrefixExpressionItem = null;

        //                    var nextExpressionItem = expressionIndex < allExpressionItems.Count - 1 ? allExpressionItems[expressionIndex + 1] : null;

        //                    if (!(nextExpressionItem is IOperatorInfoExpressionItem nextOperatorInfoExpression &&
        //                          nextOperatorInfoExpression.OperatorInfo.OperatorType == OperatorType.PostfixUnaryOperator))
        //                    {
        //                        operatorExpressionItems.AddLast(operatorExpressionItem);

        //                        // Lets add all unary postfix operators to the left as children
        //                        if (previousPostfixOperatorItems != null)
        //                        {
        //                            operatorExpressionItem.BottomMostUnaryOperator = previousPostfixOperatorItems.First.Value;
        //                            var prevExpressionItemNode = previousPostfixOperatorItems.Last;

        //                            while (prevExpressionItemNode != null)
        //                            {
        //                                var parentOperatorExpressionItem = prevExpressionItemNode.Next == null ? operatorExpressionItem : prevExpressionItemNode.Next.Value;
        //                                var previousPostfixOperatorExpressionItem = prevExpressionItemNode.Value;

        //                                previousPostfixOperatorExpressionItem.Parent = parentOperatorExpressionItem;
        //                                parentOperatorExpressionItem.Operand1 = previousPostfixOperatorExpressionItem;

        //                                prevExpressionItemNode = prevExpressionItemNode.Previous;
        //                            }

        //                            previousPostfixOperatorItems = null;
        //                        }
        //                    }
        //                    else
        //                    {
        //                        // Start new chain.
        //                        if (previousPostfixOperatorItems == null)
        //                            previousPostfixOperatorItems = new LinkedList<OperatorExpressionItem>();

        //                        previousPostfixOperatorItems.AddLast(operatorExpressionItem);
        //                    }

        //                    break;
        //                case OperatorType.BinaryOperator:
        //                    currentLeftMostPrefixExpressionItem = null;
        //                    previousPostfixOperatorItems = null;
        //                    operatorExpressionItems.AddLast(operatorExpressionItem);
        //                    break;
        //                default:
        //                    throw new Exception($"Unhandled value '{operatorInfo.OperatorType}'.");
        //            }

        //            prevOperatorExpressionItem = operatorExpressionItem;
        //            prevExpressionItem = prevOperatorExpressionItem;

        //            if (operatorExpressionItems.Last.Value == operatorExpressionItem)
        //            {
        //                // If we added the current operator to linked list, lets add a priority.
        //                if (!operatorPrioritiesSet.Contains(operatorInfo.Priority))
        //                    operatorPrioritiesSet.Add(operatorInfo.Priority);
        //            }
        //        }
        //        else
        //        {
        //            if (prevOperatorExpressionItem != null)
        //                prevOperatorExpressionItem.ExpressionItemToRight = expressionItem;

        //            prevExpressionItem = expressionItem;
        //            prevOperatorExpressionItem = null;
        //            currentLeftMostPrefixExpressionItem = null;
        //            previousPostfixOperatorItems = null;
        //        }
        //    }

        //    // Lets assume that we have the following operators with priorities shown in parentheses
        //    // Unary ?(0)
        //    // Binary operator *(0)
        //    // Unary prefix operator --(2)
        //    // Unary postfix operator >>(1)
        //    // Unary postfix operator ++(2)
        //    // Consider the expression (the priorities are displayed on top of operators)
        //    //   0 2,2 1  0   2,2
        //    // x ?----y>> * z++++
        //    // The parser should parse this expression in such a way that it is equivalent to the following expression, when braces are added to 
        //    // make the operator application order explicit: (x ? ((----(y>>)) * z))++++
        //    // The reason that ? operator with higher priority (0) is applied after lower priority unary prefix operators ----prefix is that
        //    // the unary operator with lower priority 2 should be applied after all higher priority operators >> and * are applied.
        //    // Another approach would be to apply the lower priority unary operators, when a sibling higher priority is applied.
        //    // Using that approach, the expression above would be parsed to an expression equivalent to ((x ? (----y))>>) * (z+++)
        //    // For now, we use the first approach.
        //    var sortedOperatorPriorities = operatorPrioritiesSet.ToList();
        //    sortedOperatorPriorities.Sort();

        //    LinkedListNode<OperatorExpressionItem> rootNode = null;

        //    IExpressionItemBase GetOperand(IExpressionItemBase siblingExpressionItem)
        //    {
        //        var operand = siblingExpressionItem;

        //        var currentParent = operand.Parent;

        //        while (currentParent != null)
        //        {
        //            operand = currentParent;
        //            currentParent = currentParent.Parent;
        //        }

        //        return operand;
        //    }

        //    for (int operatorPriorityInd = 0; operatorPriorityInd < sortedOperatorPriorities.Count; ++operatorPriorityInd)
        //    {
        //        var operatorPriority = sortedOperatorPriorities[operatorPriorityInd];

        //        var operatorExpressionItemNode = operatorExpressionItems.First;

        //        while (operatorExpressionItemNode != null)
        //        {
        //            var operatorExpressionItem = operatorExpressionItemNode.Value;

        //            var operatorInfo = operatorExpressionItem.OperatorInfoExpressionItem.OperatorInfo;

        //            if (operatorInfo.Priority == operatorPriority)
        //            {
        //                rootNode = operatorExpressionItemNode;

        //                switch (operatorInfo.OperatorType)
        //                {
        //                    case OperatorType.BinaryOperator:
        //                        operatorExpressionItem.Operand1 = GetOperand(operatorExpressionItem.ExpressionItemToLeft);
        //                        if (operatorExpressionItem.ExpressionItemToRight != null)
        //                            operatorExpressionItem.Operand2 = GetOperand(operatorExpressionItem.ExpressionItemToRight);

        //                        break;
        //                    case OperatorType.PrefixUnaryOperator:

        //                        if (operatorExpressionItem.BottomMostUnaryOperator != null)
        //                            operatorExpressionItem.BottomMostUnaryOperator.Operand1 =
        //                                GetOperand(operatorExpressionItem.BottomMostUnaryOperator.ExpressionItemToRight);
        //                        else
        //                            operatorExpressionItem.Operand1 = GetOperand(operatorExpressionItem.ExpressionItemToRight);

        //                        break;

        //                    case OperatorType.PostfixUnaryOperator:

        //                        if (operatorExpressionItem.BottomMostUnaryOperator != null)
        //                            operatorExpressionItem.BottomMostUnaryOperator.Operand1 =
        //                                GetOperand(operatorExpressionItem.BottomMostUnaryOperator.ExpressionItemToLeft);
        //                        else
        //                            operatorExpressionItem.Operand1 = GetOperand(operatorExpressionItem.ExpressionItemToLeft);

        //                        break;
        //                }

        //                // Lets null out internal properties, since they will hold references that subclasses of public class
        //                // OperatorExpressionItem will not be able to clear.
        //                operatorExpressionItem.BottomMostUnaryOperator = null;
        //                operatorExpressionItem.ExpressionItemToLeft = null;
        //                operatorExpressionItem.ExpressionItemToRight = null;

        //                // Lets remove current node from linked list, since we are not going to need it anymore

        //                operatorExpressionItemNode = operatorExpressionItemNode.Next;
        //                operatorExpressionItems.Remove(rootNode);
        //            }
        //            else
        //            {
        //                operatorExpressionItemNode = operatorExpressionItemNode.Next;
        //            }
        //        }
        //    }

        //    if (rootNode != null && operatorExpressionItems.Count == 0)
        //        return rootNode.Value;

        //    var firstExpressionItem = allExpressionItems[0];

        //    context.AddParseErrorItem(
        //        new ParseErrorItem(firstExpressionItem.IndexInText,
        //            () => "Failed to determine operator priorities. This should normally not happened and is indicative of a bug in parser implementation.", ParseErrorItemCode.InvalidUseOfPrefixes));

        //    return GetOperatorsExpressionItemWithErrors();
        //}
    }
}
