using System.ComponentModel.Design.Serialization;

public class Evaluator
{
    private Dictionary<string, object> environment = new Dictionary<string, object>();
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
        Console.WriteLine($"Effect: {((StringExpression)effect.Name.name).Value}");

        if (effect.Params != null)
        {
            Console.WriteLine("Parameters:");
            foreach (var param in effect.Params.nodes)
            {
                if (param is Variable variable)
                {
                    Console.WriteLine($"  {variable.name}: {variable.type}");
                }
            }
        }

        Console.WriteLine("Action:");
        EvaluateAction(effect.Action);
    }
    private void EvaluateForStatement(ForStatement forStmt, int indent)
    {
        string indentation = new string(' ', indent * 2);
        Console.WriteLine($"{indentation}For loop: {forStmt.Variable.name} in {forStmt.Target.name}");
        foreach (var stmt in forStmt.Body.statements)
        {
            EvaluateStatement(stmt, indent + 1);
        }
    }

    private void EvaluateWhileStatement(WhileStatement whileStmt, int indent)
    {
        string indentation = new string(' ', indent * 2);
        Console.WriteLine($"{indentation}While loop:");
        Console.WriteLine($"{indentation}  Condition: {EvaluateExpression(whileStmt.condition)}");
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
            Console.WriteLine($"{indentation}For loop: {forStmt.Variable.name} in {forStmt.Target.name}");
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
            Console.WriteLine($"{indentation}While loop:");
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
        Console.WriteLine($"{indentation}Assignment: {leftSide} {operation} {rightSide}");
    }

    private void EvaluateVariableCompound(VariableCompound vc, int indent)
    {
        string indentation = new string(' ', indent * 2);
        string fullName = EvaluateExpression(vc);
        Console.WriteLine($"{indentation}Function call: {fullName}");
    }

    private void EvaluateExpression(Expression expr, int indent)
    {
        string indentation = new string(' ', indent * 2);

        if (expr is Binary binary)
        {
            if (binary.Left is Unary unary)
            {
                Console.WriteLine($"{indentation}Unary operation: {unary.Operator.lexeme}");
            }
            Console.WriteLine($"{indentation}Binary operation: {binary.Operator.lexeme}");
        }
        else if (expr is Unary unary)
        {
            Console.WriteLine($"{indentation}Unary operation: {unary.Operator.lexeme}");
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
        Console.WriteLine($"Selector:");
        Console.WriteLine($"  Source: {selector.Source}");
        Console.WriteLine($"  Single: {selector.Singles.Value}");
        Console.WriteLine($"  Predicate: {EvaluatePredicate(selector.Predicate)}");
    }

    private string EvaluatePredicate(Predicate predicate)
    {
        return $"({predicate.Variable.name}) => {EvaluateExpression(predicate.Condition)}";
    }

    public void EvaluateCard(Card card)
    {
        Console.WriteLine("Card:");
        Console.WriteLine($"  Type: {EvaluateExpression(card.Type.type)}");
        Console.WriteLine($"  Name: {EvaluateExpression(card.Name.name)}");
        Console.WriteLine($"  Faction: {EvaluateExpression(card.Faction.faction)}");
        Console.WriteLine($"  Power: {EvaluateExpression(card.Power.power)}");
        Console.WriteLine("  Range: [" + string.Join(", ", card.Range.expressionsRange.Select(EvaluateExpression)) + "]");

        if (card.OnActivation != null)
        {
            Console.WriteLine("  OnActivation:");
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
        Console.WriteLine($"    Effect: {element.oae.name}");
        foreach (var param in element.oae.Params)
        {
            Console.WriteLine($"      {param.Left.name}: {EvaluateExpression(param.Right)}");
        }
        if (element.selector != null)
        {
            Console.WriteLine("    Selector:");
            Console.WriteLine($"      Source: {element.selector.Source}");
            Console.WriteLine($"      Single: {element.selector.Singles.Value}");
            Console.WriteLine($"      Predicate: {EvaluateExpression(element.selector.Predicate.Condition)}");
        }
        if (element.postAction != null)
        {
            Console.WriteLine("    PostAction:");
            Console.WriteLine($"      Type: {EvaluateExpression(element.postAction.Type)}");
            foreach (var assignment in element.postAction.Assignments)
            {
                Console.WriteLine($"      {assignment.Left.name}: {EvaluateExpression(assignment.Right)}");
            }
            if (element.postAction.Selector != null)
            {
                Console.WriteLine("      Selector:");
                Console.WriteLine($"        Source: {element.postAction.Selector.Source}");
                Console.WriteLine($"        Single: {element.postAction.Selector.Singles.Value}");
                Console.WriteLine($"        Predicate: {EvaluateExpression(element.postAction.Selector.Predicate.Condition)}");
            }
        }
    }



    public Object GroupingExpression(Expression expression)
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

    public Object Evaluate(Expression expression)
    {
        return Accept(expression);
    }

    public object Accept(Expression expression)
    {
        return expression switch
        {
            Grouping grouping => GroupingExpression(grouping),
            Unary unary => UnaryExpression(unary),
            Binary binary => BinaryExpression(binary),
            _ => throw new InvalidOperationException("Tipo de expresión no compatible"),
        };
    }

    public Object UnaryExpression(Unary expression)
    {
        Object right = Evaluate(expression.Right);
        switch (expression.Operator.type)


        {
            case TokenType.MINUS:
                CheckNumberOperand(expression.Operator, right);
                return -(double)right;
            default: throw new InvalidOperationException("Unsupported operator ");
            case TokenType.BANG: return !IsTrue(right);
        }

    }

    bool IsTrue(Object ob)
    {
        if (ob == null) return false;
        if (ob is bool) return (bool)ob;
        return true;
    }
    bool IsEqual(Object a, Object b)
    {
        if (a == null && b == null) return true;
        if (a == null) return false;
        return a.Equals(b);
    }

    void CheckNumberOperand(Token op, object operand)
    {
        if (operand is double) return;
        throw new RunTimeError(op, "Operando debe ser un numero");
    }
    void CheckNumberOperands(Token op, Object left, Object right)
    {
        if (left is double && right is double) return;
        throw new RunTimeError(op, "Operando debe ser un numero");
    }

    public Object BinaryExpression(Binary expression)
    {
        Object left = Evaluate(expression.Left);
        Object right = Evaluate(expression.Right);
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
                Object value = Evaluate(expression);
                Console.WriteLine(Stringify(value));
            }
        }
        catch (RunTimeError error)
        {
            RunError(error);
        }
    }

    string Stringify(Object ob)
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

