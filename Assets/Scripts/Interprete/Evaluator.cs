using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
public class Evaluator : MonoBehaviour
{
    private Dictionary<string, System.Object> environment = new Dictionary<string, object>();
    public object Evaluate(Node ast)
    {
        if (ast is Program program)
        {
            foreach (Effect effect in program.effects)
            {
                EvaluateEffect(effect);
            }
            foreach (var card in program.card)
            {
                EvaluateCard(card);
            }
        }
        return null;
    }

    private void EvaluateEffect(Effect effect)
    {
        Debug.Log($"Effect: {((StringExpression)effect.Name.name).Value}");

        if (effect.Params != null)
        {
            Debug.Log("Parameters:");
            foreach (var param in effect.Params.nodes)
            {
                if (param is Variable variable)
                {
                    Debug.Log($"  {variable.name}: {variable.type}");
                }
            }
        }

        Debug.Log("Action:");
        EvaluateAction(effect.Action);
    }
    private void EvaluateForStatement(ForStatement forStmt, int indent)
    {
        string indentation = new string(' ', indent * 2);
        Debug.Log($"{indentation}For loop: {forStmt.Variable.name} in {forStmt.Target.name}");
        foreach (var stmt in forStmt.Body.statements)
        {
            EvaluateStatement(stmt, indent + 1);
        }
    }

    private void EvaluateWhileStatement(WhileStatement whileStmt, int indent)
    {
        string indentation = new string(' ', indent * 2);
        Debug.Log($"{indentation}While loop:");
        Debug.Log($"{indentation}  Condition: {EvaluateExpression(whileStmt.condition)}");
        foreach (var stmt in whileStmt.body.statements)
        {
            EvaluateStatement(stmt, indent + 1);
        }
    }

    private void EvaluateAction(Action action)
    {
        foreach (var statement in action.Block.statements)
        {
            EvaluateStatement(statement, 1);
        }
    }

    private void EvaluateStatement(Statement statement, int indent)
    {
        string indentation = new string(' ', indent * 2);

        if (statement is ForStatement forStmt)
        {
            Debug.Log($"{indentation}For loop: {forStmt.Variable.name} in {forStmt.Target.name}");
            foreach (var stmt in forStmt.Body.statements)
            {
                EvaluateStatement(stmt, indent + 1);
            }
        }
        else if (statement is Assignment assignment)
        {
            EvaluateAssignment(assignment, indent);
        }
        else if (statement is WhileStatement whileStmt)
        {
            Debug.Log($"{indentation}While loop:");
            EvaluateExpression(whileStmt.condition, indent + 1);
            foreach (var stmt in whileStmt.body.statements)
            {
                EvaluateStatement(stmt, indent + 1);
            }
        }
        else if (statement is VariableCompound vc)
        {
            EvaluateVariableCompound(vc, indent);
        }
    }

    private void EvaluateAssignment(Assignment assignment, int indent)
    {
        string indentation = new string(' ', indent * 2);
        string leftSide = EvaluateExpression(assignment.Left);
        string rightSide = EvaluateExpression(assignment.Right);
        string operation = assignment.Operator.type == TokenType.PLUS_EQUAL ? "+=" :
                           assignment.Operator.type == TokenType.MINUS_EQUAL ? "-=" : "=";
        Debug.Log($"{indentation}Assignment: {leftSide} {operation} {rightSide}");
    }

    private void EvaluateVariableCompound(VariableCompound vc, int indent)
    {
        string indentation = new string(' ', indent * 2);
        string fullName = EvaluateExpression(vc);
        Debug.Log($"{indentation}Function call: {fullName}");
    }

    private void EvaluateExpression(Expression expr, int indent)
    {
        string indentation = new string(' ', indent * 2);

        if (expr is Binary binary)
        {
            if (binary.Left is Unary unary)
            {
                Debug.Log($"{indentation}Unary operation: {unary.Operator.lexeme}");
            }
            Debug.Log($"{indentation}Binary operation: {binary.Operator.lexeme}");
        }
        else if (expr is Unary unary)
        {
            Debug.Log($"{indentation}Unary operation: {unary.Operator.lexeme}");
        }
    }

    private string EvaluateVariableCompound(VariableCompound vc)
    {
        string result = vc.name;
        foreach (var node in vc.argument.nodes)
        {
            if (node is Function function)
            {
                string args = function.Params?.nodes != null
                    ? string.Join(", ", function.Params.nodes.Select(n => EvaluateExpression((Expression)n)))
                    : "";
                result += $".{function.Name}({args})";
            }
            else if (node is Pow)
            {
                result += ".Power";
            }
            else
            {
                result += $".{node}";
            }
        }
        return result;
    }


    private string EvaluateExpression(Expression expr)
    {
        if (expr is VariableCompound vc)
        {
            return EvaluateVariableCompound(vc);
        }
        else if (expr is Variable variable)
        {
            return variable.name;
        }
        else if (expr is Number number)
        {
            return number.Value.ToString();
        }
        else if (expr is StringExpression stringExpr)
        {
            return stringExpr.Value;
        }
        else if (expr is Binary binary)
        {
            var left = EvaluateExpression(binary.Left);
            var right = EvaluateExpression(binary.Right);
            return $"{left} {binary.Operator.lexeme} {right}";
        }
        else if (expr is Unary unary)
        {
            string operand = EvaluateExpression(unary.Right);
            return $"{unary.Operator.lexeme}{operand}";
        }
        else if (expr is Bool boolExpr)
        {
            return boolExpr.Value.ToString();
        }
        return expr.ToString();
    }
    private void EvaluateSelector(Selector selector)
    {
        Debug.Log($"Selector:");
        Debug.Log($"  Source: {selector.Source}");
        Debug.Log($"  Single: {selector.Singles.Value}");
        Debug.Log($"  Predicate: {EvaluatePredicate(selector.Predicate)}");
    }

    private string EvaluatePredicate(Predicate predicate)
    {
        return $"({predicate.Variable.name}) => {EvaluateExpression(predicate.Condition)}";
    }

    public void EvaluateCard(Card card)
    {
        Debug.Log("Card:");
        Debug.Log($"  Type: {EvaluateExpression(card.Type.type)}");
        Debug.Log($"  Name: {EvaluateExpression(card.Name.name)}");
        Debug.Log($"  Faction: {EvaluateExpression(card.Faction.faction)}");
        Debug.Log($"  Power: {EvaluateExpression(card.Power.power)}");
        Debug.Log("  Range: [" + string.Join(", ", card.Range.expressionsRange.Select(EvaluateExpression)) + "]");

        if (card.OnActivation != null)
        {
            Debug.Log("  OnActivation:");
            foreach (var element in card.OnActivation.Elements)
            {
                EvaluateOnActivationElement(element);
            }
        }
    }




    private void EvaluateStatementBlock(StatementBlock block, int ident)
    {
        foreach (var stmt in block.statements)
        {
            EvaluateStatement(stmt, ident);
        }
    }
    private void EvaluateOnActivationElement(OnActivationElements element)
    {
        Debug.Log($"    Effect: {element.oae.name}");
        foreach (var param in element.oae.Params)
        {
            Debug.Log($"      {param.Left.name}: {EvaluateExpression(param.Right)}");
        }
        if (element.selector != null)
        {
            Debug.Log("    Selector:");
            Debug.Log($"      Source: {element.selector.Source}");
            Debug.Log($"      Single: {element.selector.Singles.Value}");
            Debug.Log($"      Predicate: {EvaluateExpression(element.selector.Predicate.Condition)}");
        }
        if (element.postAction != null)
        {
            Debug.Log("    PostAction:");
            Debug.Log($"      Type: {EvaluateExpression(element.postAction.Type)}");
            foreach (var assignment in element.postAction.Assignments)
            {
                Debug.Log($"      {assignment.Left.name}: {EvaluateExpression(assignment.Right)}");
            }
            if (element.postAction.Selector != null)
            {
                Debug.Log("      Selector:");
                Debug.Log($"        Source: {element.postAction.Selector.Source}");
                Debug.Log($"        Single: {element.postAction.Selector.Singles.Value}");
                Debug.Log($"        Predicate: {EvaluateExpression(element.postAction.Selector.Predicate.Condition)}");
            }
        }
    }



    public System.Object GroupingExpression(Expression expression)
    {
        // return Evaluate(expression);
        if (expression is Grouping grouping)
        {
            return Evaluate(grouping.Expression);
        }
        else
        {
            throw new InvalidOperationException("Expresión de agrupación no válida");
        }
    }

    public System.Object Evaluate(Expression expression)
    {
        return Accept(expression);
    }

    public System.Object Accept(Expression expression)
    {
        return expression switch
        {
            Grouping grouping => GroupingExpression(grouping),
            Unary unary => UnaryExpression(unary),
            Binary binary => BinaryExpression(binary),
            _ => throw new InvalidOperationException("Tipo de expresión no compatible"),
        };
    }

    public System.Object UnaryExpression(Unary expression)
    {
        System.Object right = Evaluate(expression.Right);
        switch (expression.Operator.type)


        {
            case TokenType.MINUS:
                CheckNumberOperand(expression.Operator, right);
                return -(double)right;
            default: throw new InvalidOperationException("Unsupported operator ");
            case TokenType.BANG: return !IsTrue(right);
        }

    }

    bool IsTrue(System.Object ob)
    {
        if (ob == null) return false;
        if (ob is bool) return (bool)ob;
        return true;
    }
    bool IsEqual(System.Object a, System.Object b)
    {
        if (a == null && b == null) return true;
        if (a == null) return false;
        return a.Equals(b);
    }

    void CheckNumberOperand(Token op, System.Object operand)
    {
        if (operand is double) return;
        throw new RunTimeError(op, "Operando debe ser un numero");
    }
    void CheckNumberOperands(Token op, System.Object left, System.Object right)
    {
        if (left is double && right is double) return;
        throw new RunTimeError(op, "Operando debe ser un numero");
    }

    public System.Object BinaryExpression(Binary expression)
    {
        System.Object left = Evaluate(expression.Left);
        System.Object right = Evaluate(expression.Right);
        switch (expression.Operator.type)
        {
            case TokenType.MINUS:
                CheckNumberOperands(expression.Operator, left, right);
                return (double)left - (double)right;
            case TokenType.PLUS:
                if (left is Double && right is Double)
                {
                    return (double)left + (double)right;
                }
                if (left is string && right is string)
                {
                    return (string)left + (string)right;
                }
                throw new RunTimeError(expression.Operator, "Operando deben ser dos numeros o dos string");
            case TokenType.SLASH:
                CheckNumberOperands(expression.Operator, left, right);
                return (double)left / (double)right;
            case TokenType.STAR:
                CheckNumberOperands(expression.Operator, left, right);
                return (double)left * (double)right;
            case TokenType.GREATER:
                CheckNumberOperands(expression.Operator, left, right);
                return (double)left > (double)right;
            case TokenType.GREATER_EQUAL:
                CheckNumberOperands(expression.Operator, left, right);
                return (double)left >= (double)right;
            case TokenType.LESS:
                CheckNumberOperands(expression.Operator, left, right);
                return (double)left < (double)right;
            case TokenType.LESS_EQUAL:
                CheckNumberOperands(expression.Operator, left, right);
                return (double)left <= (double)right;
            case TokenType.BANG_EQUAL: return !IsEqual(left, right);
            case TokenType.EQUAL_EQUAL: return IsEqual(left, right);


        }

        return null!;
    }
    void Iterpret(Expression expression)
    {
        try
        {
            {
                System.Object value = Evaluate(expression);
                Debug.Log(Stringify(value));
            }
        }
        catch (RunTimeError error)
        {
            RunError(error);
        }
    }

    string Stringify(System.Object ob)
    {
        if (ob == null) return "null";
        if (ob is double)
        {
            string text = ob.ToString()!;
            if (text.EndsWith(".0"))
            {
                text = text.Substring(0, text.Length - 2);
                return text;
            }
        }

        return ob.ToString()!;
    }

    static void RunError(RunTimeError error)
    {

    }
}
public class RunTimeError : Exception
{
    public Token Token { get; }

    public RunTimeError(Token token, string message) : base(message)
    {
        Token = token;
    }
}

