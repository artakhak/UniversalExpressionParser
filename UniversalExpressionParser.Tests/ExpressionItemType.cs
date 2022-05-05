using UniversalExpressionParser.ExpressionItems;
using UniversalExpressionParser.ExpressionItems.Custom;

namespace UniversalExpressionParser.Tests;

/// <summary>
/// Type of parsed expression item.
/// This enum type used to be in UniversalExpressionParser, and was tne type of property ItemType in <see cref="IExpressionItemBase"/> (now removed).
/// Eventually, removed the property, and moved the enum type to test project. At some point will remove the dependency on this type in this project too,
/// and will remove this type.
/// </summary>
public enum ExpressionItemType
{
    /// <summary>
    /// Custom expression item parsed by custom expression item parser <see cref="ICustomExpressionItemParser"/>. <br/>
    /// For example in expression "::types[T1,T2] F1(x:T1, y:T2) where T1:int where T2:double whereend" one of custom expression parsers might parse text "::types[T1,T2]" as a prefix custom expression item to be added to <see cref="IComplexExpressionItem.Prefixes"/> in expression of type <see cref="IBracesExpressionItem"/> parsed from "F1(x:T1, y:T2)".
    /// Also, some other custom expression parser might parse text "where T1:int where T2:double whereend" as a postfix custom expression item to be added to <see cref="IComplexExpressionItem.Postfixes"/> in expression of type <see cref="IBracesExpressionItem"/> parsed from "F1(x:T1, y:T2)"
    /// Another example is "::pragma x+y". In this example one of custom expression parser might parse "::pragma x" as a regular custom expression item (i.e., expression item for which <see cref="ICustomExpressionItem.CustomExpressionItemCategory"/> is <see cref="CustomExpressionItemCategory.Regular"/>) which will be one of the operands in binary operator "+".
    /// </summary>
    Custom,

    /// <summary>
    /// Expression item is part of another custom expression. For example in expression "::types[T1,T2] F1(x:T1, y:T2) where T1:int where T2:double whereend" custom expression
    /// "where T1:int" might be an expression with .... 
    /// </summary>
    CustomExpressionItemPart,

    /// <summary>
    /// Expression items without any code block start/end markers
    /// </summary>
    RootExpressionItem,

    /// <summary>
    /// Opening round brace "(".
    /// </summary>
    OpeningRoundBrace,

    /// <summary>
    /// Closing round brace ")".
    /// </summary>
    ClosingRoundBrace,

    /// <summary>
    /// Opening square brace "[".
    /// </summary>
    OpeningSquareBrace,

    /// <summary>
    /// Closing square brace "]".
    /// </summary>
    ClosingSquareBrace,

    /// <summary>
    /// Code block start marker expression item. Examples are "{", "BEGIN".
    /// </summary>
    CodeBlockStartMarker,

    /// <summary>
    /// Code block end marker expression item. Examples are "}", "END", 
    /// </summary>
    CodeBlockEndMarker,

    /// <summary>
    /// Comma. Normally is used to separate function parameters, matrix indexes, or array items.
    /// </summary>
    Comma,

    /// <summary>
    /// Expression separator such as ";"
    /// </summary>
    ExpressionSeparator,

    /// <summary>
    /// A code block. Examples are {y=y+3;} or {y=y+3; {var z = 5;}}.
    /// </summary>
    CodeBlock,
        
    /// <summary>
    /// Keyword. Examples of language keywords are public, class, namespace, ref, out, _, etc
    /// </summary>
    Keyword,

    /// <summary>
    /// The value is a text. Examples are:
    /// x="This is will be parsed to text"
    /// y='This will be parsed to text too. The text is "This will be parsed to text too."'
    /// </summary>
    ConstantText,

    /// <summary>
    /// The value is a numeric value. Examples are:
    /// x=5.3; 
    /// y=.2;
    /// z=-2.5exp+300;
    /// <see cref="INumericExpressionItem"/> provides more details of numeric value type
    /// </summary>
    ConstantNumericValue,

    /// <summary>
    /// Any name (e.g., variable name, constant name, database table name, etc)
    /// </summary>
    Literal,

    /// <summary>
    /// Expression item that has opening and possibly closing square or round braces (if the expression evaluation succeeds,
    /// the closing brace should be present), and another expression item (or multiple expression items separated by commas) enclosed in braces.
    /// Also, there might be a name before the braces.
    /// Examples are: F1(x, y), Matrix1[1, 2, x+y], (y+z) in x*(y+z), (#x) in !(#x), [x1, 1, y+3], Func1(x, 2).
    /// </summary>
    Braces,

    SeriesOfExpressionItemsWithErrors,


    /// <summary>
    /// Binary operator. Example: x+y.
    /// </summary>
    BinaryOperator,

    /// <summary>
    /// Unary prefix operator applied placed the operand. Example ++x
    /// </summary>
    PrefixUnaryOperator,

    /// <summary>
    /// Unary postfix operator placed after the operand. Example x++
    /// </summary>
    PostfixUnaryOperator,

    /// <summary>
    /// Operator data in binary or unary operator. Examples are "+", "+=", "IS NOT". Normally, this expression item will be part of an expression
    /// item of types <see cref="BinaryOperator"/>, <see cref="PostfixUnaryOperator"/>, or <see cref="PrefixUnaryOperator"/>,
    /// which will have both expression items for operand(s) as well as an item of type <see cref="OperatorInfo"/>
    /// </summary>
    OperatorInfo,

    /// <summary>
    /// Normally this will be used for name expression item in function or 
    /// </summary>
    Name
}

public static class ExpressionItemTypeExtensions
{
    /// <summary>
    /// Returns true if <paramref name="expressionItemType"/> is not part of other expression type.
    /// For example the method will return for values such as <see cref="ExpressionItemType.Comma"/>, <see cref="ExpressionItemType.ClosingRoundBrace"/>,
    /// or <see cref="ExpressionItemType.Name"/>, since these expression item types are components in main expression item types such as
    /// <see cref="ExpressionItemType.Braces"/> or <see cref="ExpressionItemType.Literal"/>.
    /// On the other hand, the method will return true for parameter values such as <see cref="ExpressionItemType.Literal"/>,
    /// <see cref="ExpressionItemType.BinaryOperator"/>, <see cref="ExpressionItemType.Braces"/>, etc.
    /// </summary>
    /// <param name="expressionItemType"></param>
    /// <returns></returns>
    public static bool IsMainExpressionType(this ExpressionItemType expressionItemType)
    {
        switch (expressionItemType)
        {
            case ExpressionItemType.RootExpressionItem:
            case ExpressionItemType.BinaryOperator:
            case ExpressionItemType.PrefixUnaryOperator:
            case ExpressionItemType.PostfixUnaryOperator:
            case ExpressionItemType.Braces:
            case ExpressionItemType.CodeBlock:
            case ExpressionItemType.Literal:
            case ExpressionItemType.ConstantNumericValue:
            case ExpressionItemType.ConstantText:
            case ExpressionItemType.Custom:
            case ExpressionItemType.Keyword:
                return true;
            default:
                return false;
        }
    }
}