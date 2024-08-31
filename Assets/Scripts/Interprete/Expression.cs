using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
[System.Serializable]
public abstract class Expression : Node  //revisado
{
    public abstract void Print(int indent = 0);
    public abstract object Evaluate(Scope scope);

}

public interface Node     //revisado
{
    public void Print(int indent = 0);

}

[System.Serializable]
public class Number : Expression        //revisado
{
    public int Value;
    public Number(int value)
    {
        Value = value;
    }

    public override object Evaluate(Scope scope)
    {
        return Value;
    }

    public override void Print(int ident = 0)
    {
        Debug.Log(new string(' ', ident) + "Number:" + Value);
    }
}
[System.Serializable]
public class StringExpression : Expression       //revisado
{
    public string Value;
    public StringExpression(string value)
    {
        Value = value;
    }

    public override object Evaluate(Scope scope)
    {
        return Value;
    }

    public override void Print(int ident = 0)
    {
        Debug.Log(new string(' ', ident) + "String:" + Value);
    }
}
public class Bool : Expression           //revisado
{
    public bool Value;
    public Bool(bool value)
    {
        Value = value;
    }

    public override object Evaluate(Scope scope)
    {
        return Value;
    }

    public override void Print(int indent = 0)
    {
        Debug.Log(new string(' ', indent) + "Bool:" + Value);
    }
}
[System.Serializable]
public class Binary : Expression      //revisado
{
    public Expression Left;

    public Token Operator;
    public Expression Right;

    public Binary(Expression Left, Token Operator, Expression Right)
    {
        this.Left = Left;
        this.Operator = Operator;
        this.Right = Right;
    }

    public override void Print(int indent = 0)
    {
        Debug.Log(new string(' ', indent) + Operator.lexeme);
        Left.Print(indent + 2);
        Right.Print(indent + 2);
    }

    public override object Evaluate(Scope scope)
    {
        object left = Left.Evaluate(scope);
        object right = Right.Evaluate(scope);
        Debug.Log($"Comparing: {left} (Type: {left?.GetType()}) with {right} (Type: {right?.GetType()})");

        if (left is double && right is double)
        {
            Debug.Log("xc vkzxnvkjndskjngjbsgihbfiudwutifhsejnvkjvfiefje");
            switch (Operator.type)
            {
                case TokenType.PLUS:
                    return Convert.ToDouble(left) + Convert.ToDouble(right);
                case TokenType.MINUS:
                    return Convert.ToDouble(left) - Convert.ToDouble(right);
                case TokenType.STAR:
                    return Convert.ToDouble(left) * Convert.ToDouble(right);
                case TokenType.SLASH:
                    return Convert.ToDouble(left) / Convert.ToDouble(right);
                case TokenType.PERCENT:
                    return Convert.ToDouble(left) % Convert.ToDouble(right);
                case TokenType.POW:
                    return Math.Pow(Convert.ToDouble(left), Convert.ToDouble(right));
                case TokenType.GREATER:
                    return Convert.ToDouble(left) > Convert.ToDouble(right);
                case TokenType.GREATER_EQUAL:
                    return Convert.ToDouble(left) >= Convert.ToDouble(right);
                case TokenType.LESS:
                    return Convert.ToDouble(left) < Convert.ToDouble(right);
                case TokenType.LESS_EQUAL:
                    return Convert.ToDouble(left) <= Convert.ToDouble(right);
                case TokenType.BANG_EQUAL: return !left.Equals(right);
                case TokenType.EQUAL_EQUAL: return left.Equals(right);
                default:
                    throw new InvalidOperationException("Unsupported operator: " + Operator.lexeme);
            }
        }
        else if (left is string && right is string)
        {
            switch (Operator.type)
            {
                case TokenType.ATSIGN: return left.ToString() + right.ToString();
                case TokenType.ATSIGN_ATSIGN: return left.ToString() + " " + right.ToString();
                case TokenType.EQUAL_EQUAL: return left.Equals(right);
                default:
                    throw new InvalidOperationException("Unsupported operator: " + Operator.lexeme);
            }
        }
        else if (left is Cards card && right is string faction)
        {
            switch (Operator.type)
            {
                case TokenType.EQUAL_EQUAL: return left.Equals(right);
                case TokenType.LESS:
                    return Convert.ToDouble(left) < Convert.ToDouble(right);
                case TokenType.GREATER:
                    return Convert.ToDouble(left) > Convert.ToDouble(right);
                case TokenType.GREATER_EQUAL:
                    return Convert.ToDouble(left) >= Convert.ToDouble(right);
                case TokenType.LESS_EQUAL:
                    return Convert.ToDouble(left) <= Convert.ToDouble(right);
                case TokenType.BANG_EQUAL: return !left.Equals(right);
                default:
                    throw new InvalidOperationException("Unsupported operator for Cards and string: " + Operator.lexeme);
            }
        }
        else if (left is Cards cards && right is int factionas)
        {
            switch (Operator.type)
            {
                case TokenType.EQUAL_EQUAL: return left.Equals(right);
                case TokenType.LESS:
                    return Convert.ToDouble(left) < Convert.ToDouble(right);
                case TokenType.GREATER:
                    return Convert.ToDouble(left) > Convert.ToDouble(right);

                case TokenType.GREATER_EQUAL:
                    return Convert.ToDouble(left) >= Convert.ToDouble(right);
                case TokenType.LESS_EQUAL:
                    return Convert.ToDouble(left) <= Convert.ToDouble(right);
                case TokenType.BANG_EQUAL: return !left.Equals(right);
                default: throw new InvalidOperationException("Unsupported operator for Cards and string: " + Operator.lexeme);
            }
        }
        else if (left is int leftInt && right is int rightInt)
        {
            switch (Operator.type)
            {
                case TokenType.EQUAL_EQUAL: return left.Equals(right);
                case TokenType.LESS:
                    return Convert.ToDouble(left) < Convert.ToDouble(right);
                case TokenType.GREATER:
                    return Convert.ToDouble(left) > Convert.ToDouble(right);
                case TokenType.PLUS:
                    return Convert.ToDouble(left) + Convert.ToDouble(right);
                case TokenType.MINUS:
                    return Convert.ToDouble(left) - Convert.ToDouble(right);
                case TokenType.STAR:
                    return Convert.ToDouble(left) * Convert.ToDouble(right);
                case TokenType.SLASH:
                    return Convert.ToDouble(left) / Convert.ToDouble(right);
                case TokenType.PERCENT:
                    return Convert.ToDouble(left) % Convert.ToDouble(right);
                case TokenType.POW:
                    return Math.Pow(Convert.ToDouble(left), Convert.ToDouble(right));
                case TokenType.GREATER_EQUAL:
                    return Convert.ToDouble(left) >= Convert.ToDouble(right);
                case TokenType.LESS_EQUAL:
                    return Convert.ToDouble(left) <= Convert.ToDouble(right);
                case TokenType.BANG_EQUAL: return !left.Equals(right);
                default:
                    throw new InvalidOperationException("Unsupported operator for Cards and string: " + Operator.lexeme);
            }
        }
        else throw new InvalidOperationException("Unsupported operator: " + Operator.lexeme);
    }

}
public class BinaryBoolean : Binary         //revisado
{
    public BinaryBoolean(Expression left, Token operators, Expression right) : base(left, operators, right)
    { }
}
public class BinaryInterger : Binary         //revisado
{
    public BinaryInterger(Expression left, Token operators, Expression right) : base(left, operators, right)
    { }
}
public class BinaryString : Binary          //revisado
{
    public BinaryString(Expression left, Token operators, Expression right) : base(left, operators, right)
    { }
}
public class Unary : Expression     //revisado
{


    public Token Operator;
    public Expression Right;
    public Unary(Token Operator, Expression Right)
    {

        this.Operator = Operator;
        this.Right = Right;
    }

    public override void Print(int indent = 0)
    {
        Debug.Log(new string(' ', indent) + "Unary:" + Operator.lexeme);
        Right.Print(indent + 2);
    }
    public override object Evaluate(Scope scope)
    {
        object right = Right.Evaluate(scope);

        switch (Operator.type)
        {
            case TokenType.MINUS:
                return -Convert.ToDouble(right);
            case TokenType.BANG:
                return !(bool)right;
            case TokenType.PLUS_PLUS:
                int value = (int)right;
                int newv = value + 1;
                scope.value[(Right as Variable).name] = newv;
                return value;
            case TokenType.MINUS_MINUS:
                int value2 = (int)right;
                int newv2 = value2 + 1;
                scope.value[(Right as Variable).name] = newv2;
                return value2;

            default:
                throw new InvalidOperationException("Unsupported operator: " + Operator.lexeme);
        }
    }
}
public class UnaryBoolean : Unary       //revisado   
{
    public UnaryBoolean(Token operators, Expression right) : base(operators, right) { }
}
public class UnaryInterger : Unary          //revisado
{
    public UnaryInterger(Token operators, Expression right) : base(operators, right) { }
}
public class Grouping : Expression         //revisado
{
    public Expression Expression;

    public Grouping(Expression Expression)
    {
        this.Expression = Expression;
    }

    public override void Print(int ident = 0)
    {
        Debug.Log(new string(' ', ident) + "ExpressionGroup:");
        Expression.Print(ident + 2);

    }
    public override object Evaluate(Scope scope)
    {
        return Expression.Evaluate(scope);
    }
}
public class Variable : Expression      //revisado
{
    public Token ID { get; }
    public string name { get; }
    public Type type;
    public bool isConstant { get; set; }
    public enum Type
    {
        INT, STRING, BOOL, NULL, FIELD, TARGETS, VOID, CARD, CONTEXT
    }
    public void SetType(TokenType typeName)
    {
        if (typeName == TokenType.BOOLTYPE)
        {
            type = Type.BOOL;
        }
        if (typeName == TokenType.NUMBERTYPE)
        {
            type = Type.INT;
        }
        if (typeName == TokenType.STRINGTYPE)
        {
            type = Type.STRING;
        }
    }

    public Variable(Token ID)
    {
        this.ID = ID;
        name = ID.lexeme;
        type = Type.NULL;
    }

    public override void Print(int indent = 0)
    {
        Debug.Log(new string(' ', indent) + "Variable: " + name + " (" + type.ToString() + ")");
    }
    public override object Evaluate(Scope scope)
    {
        return scope.value[name];
    }
}
public class VariableCompound : Variable, Statement             //revisado
{
    public Params argument;

    public VariableCompound(Token token) : base(token)
    {
        argument = new Params();
    }

    public override void Print(int ident = 0)
    {
        Debug.Log(new string(' ', ident) + "VariableComp: ");
        argument?.Print(ident + 2);
    }
    public void Evaluater(Scope scope)
    {
        object last = null;
        foreach (var arg in argument.nodes)
        {
            if (arg is Function)
            {
                last = (arg as Function).ValueReturn(scope, last);
            }
            else if (arg is Pointer)
            {
                Pointer pointer = arg as Pointer;
                switch (pointer.pointer)
                {
                    case "Hand": last = scope.gameManager.HandOfPlayer(scope.gameManager.TriggerPlayer()); break;
                    case "Deck": last = scope.gameManager.DeckOfPlayer(scope.gameManager.TriggerPlayer()); break;
                    case "Graveyard": last = scope.gameManager.GraveyardOfPlayer(scope.gameManager.TriggerPlayer()); break;
                    case "Field": last = scope.gameManager.FieldOfPlayer(scope.gameManager.TriggerPlayer()); break;
                    case "Board": last = scope.gameManager.Board(); break;
                }
            }
        }
    }
    public override object Evaluate(Scope context)
    {
        object last = null;
        if (name != "context") last = context.value[name];
        foreach (var arg in argument.nodes)
        {
            if (arg is Function)
            {
                last = (arg as Function).ValueReturn(context, last);
            }
            else if (arg is Inde)
            {
                Debug.Log(last);
                if (last is List<Cards> porfavor)
                {
                    List<Cards> cards = porfavor;
                    foreach (var card in cards)
                    {
                        Debug.Log("hooooooooooooooooooooooooooooooolaZ");
                    }
                    Inde indexer = arg as Inde;
                    last = cards[indexer.index];
                }
                else
                {
                    string[] range = last as string[];
                    Inde indexer = arg as Inde;
                    last = range[indexer.index];
                }
            }
            else if (arg is Pointer)
            {
                Pointer pointer = arg as Pointer;
                switch (pointer.pointer)
                {
                    case "Hand": last = context.gameManager.HandOfPlayer(context.gameManager.TriggerPlayer()); break;
                    case "Deck": last = context.gameManager.DeckOfPlayer(context.gameManager.TriggerPlayer()); break;
                    case "Graveyard": last = context.gameManager.GraveyardOfPlayer(context.gameManager.TriggerPlayer()); break;
                    case "Field": last = context.gameManager.FieldOfPlayer(context.gameManager.TriggerPlayer()); break;
                    case "Board": last = context.gameManager.Board(); break;
                }
            }
            else
            {
                Cards cardss = last as Cards;
                switch (arg)
                {
                    case CardType: last = cardss.type; break;
                    case Name: last = cardss.cardName; break;
                    case Faction: last = cardss.faction; break;
                    case Pow: last = cardss.cardPower; break;
                    case Range: last = cardss.cardRange; break;
                    case Owner: last = cardss.Owner; break;
                }
            }
        }
        return last;
    }


    public void GetValue(Scope scope, object value)
    {
        object last = scope.value[name]; ;
        if (name == "target")
        {
            last = scope.value[name];
        }
        foreach (var arg in argument.nodes)
        {
            if (arg is Function)
            {
                last = (arg as Function).ValueReturn(scope, last);
            }
            else if (arg is Inde)
            {
                if (last is List<Cards> cola)
                {
                    List<Cards> cards = cola;
                    Inde Inde = arg as Inde;
                    last = cards[Inde.index];
                }
                else
                {
                    string[] range = last as string[];
                    Inde Inde = arg as Inde;
                    range[Inde.index] = value as string;
                }
            }
            else if (arg is Pointer)
            {
                Pointer pointer = arg as Pointer;
                switch (pointer.pointer)
                {
                    case "Hand": last = scope.gameManager.HandOfPlayer(scope.gameManager.TriggerPlayer()); break;
                    case "Deck": last = scope.gameManager.DeckOfPlayer(scope.gameManager.TriggerPlayer()); break;
                    case "Graveyard": last = scope.gameManager.GraveyardOfPlayer(scope.gameManager.TriggerPlayer()); break;
                    case "Field": last = scope.gameManager.FieldOfPlayer(scope.gameManager.TriggerPlayer()); break;
                    case "Board": last = scope.gameManager.Board(); break;
                }
            }
            else
            {
                Cards card = last as Cards;
                switch (arg)
                {
                    case CardType: last = card.type; break;
                    case Name: last = card.cardName; break;
                    case Faction: last = card.faction; break;
                    case Pow: last = card.cardPower; break;
                    case Range: last = card.cardRange; break;
                    case Owner: last = card.Owner; break;
                }
            }
        }
    }
}
public class Params : Node              //revisado
{
    public List<Node> nodes;
    public Params()
    {
        nodes = new List<Node>();
    }
    public void Print(int ident = 0)
    {
        Debug.Log(new string(' ', ident) + "Args:");
        foreach (var arg in nodes)
        {
            arg.Print(ident + 2);
        }
    }

}
[System.Serializable]
public class Name : Node      // revisado
{
    public Expression name;

    public Name(Expression name)
    {
        this.name = name;
    }

    public void Print(int ident = 0)
    {
        Debug.Log(new string(' ', ident) + "Name:");
        name.Print(ident + 2);
    }

}
public class CardType : Node       //revisado
{
    public Expression type;

    public CardType(Expression type)
    {
        this.type = type;
    }

    public void Print(int ident = 0)
    {
        Debug.Log(new string(' ', ident) + "Name:");
        type.Print(ident + 2);
    }

}
public class Pow : Node //revisado
{
    public Pow() { }
    public void Print(int indent) { }

}
public class Faction : Node      //revisado
{
    public Expression faction;

    public Faction(Expression faction)
    {
        this.faction = faction;
    }

    public void Print(int ident = 0)
    {
        Debug.Log(new string(' ', ident) + "Name:");
        faction.Print(ident + 2);
    }

}
public class Power : Node        //revisado
{
    public Expression power;

    public Power(Expression power)
    {
        this.power = power;
    }

    public void Print(int ident = 0)
    {
        Debug.Log(new string(' ', ident) + "Name:");
        power.Print(ident + 2);
    }

}
public class Range : Node        //revisado
{
    public Expression[] expressionsRange;
    public string range;
    public Range(Expression[] expressionsRange)
    {
        this.expressionsRange = expressionsRange;
    }
    public Range(string range)
    {
        this.range = range;

    }


    public void Print(int ident = 0)
    {
        Debug.Log(new string(' ', ident) + "Range:");
        if (range != null)
        {
            foreach (var expr in expressionsRange)
            {
                expr.Print(ident + 2);
            }
        }
        else
        {
            Debug.Log(new string(' ', ident) + "Lexeme: " + range);
        }
    }


}
public class Effect : Node  //revisado
{
    public Name Name;
    public Params Params;
    public Action Action;

    public Effect()
    {


    }
    public void Print(int ident = 0)
    {
        Debug.Log(new string(' ', ident) + "Effect:");
        Name?.Print(ident + 2);
        Params?.Print(ident + 2);
        Action?.Print(ident + 2);
    }


}
public class OnActivationEffect : Node  // revisado
{
    public string name;
    public List<Assignment> Params;

    public OnActivationEffect(string name, List<Assignment> Params)
    {
        this.name = name;
        this.Params = Params;

    }
    public void Print(int ident = 0)
    {
        Debug.Log(new string(' ', ident) + "OnActivationEffect:");
        Debug.Log(new string(' ', ident + 2) + "Name: " + name);
        foreach (var assignment in Params)
        {
            assignment.Print(ident + 2);
        }
    }

}
public class Selector : Node       //revisado
{
    public string Source;

    public Single Singles;
    public Predicate Predicate;
    public Selector(string source, Single singles, Predicate predicate)
    {
        Source = source;
        Singles = singles;
        Predicate = predicate;
    }

    public void Print(int ident = 0)
    {
        Debug.Log(new string(' ', ident) + "Selector:");
        Debug.Log(new string(' ', ident + 2) + "Source: " + Source);
        Singles?.Print(ident + 2);
        Predicate?.Print(ident + 2);
    }

}
public class Single : Node         //revisado
{
    public bool Value;
    public Single(Token token)
    {
        Debug.Log($"cksjdbckajsgBZXCJsbkshfyugeasdibvkjasbxzncb dsrkhj, {token.type}");
        if (token.type == TokenType.TRUE)
        {
            Value = true;


        }
        else if (token.type == TokenType.FALSE)
        {
            Value = false;
        }
        else
        {
            MostrarError("la asignacion de single es incorrecta, se esperaba un bool");
        }
    }
    private void MostrarError(string errorMessage)
    {
        PlayerPrefs.SetString("ErrorMessage", errorMessage);
        SceneManager.LoadScene("Error", LoadSceneMode.Additive);
        AudioListener[] listeners = UnityEngine.Object.FindObjectsOfType<AudioListener>();

        // Desactiva todos los Audio Listeners excepto el principal
        foreach (AudioListener listener in listeners)
        {
            if (listener.gameObject.CompareTag("MainAudioListener"))
            {
                listener.enabled = true;
            }
            else
            {
                listener.enabled = false;
            }
        }
    }


    public void Print(int ident = 0)
    {
        Debug.Log(new string(' ', ident) + "Single:" + Value);
    }

}
public class Predicate : Node   //revisado
{
    public Variable Variable;
    public Expression Condition;
    public Predicate(Variable variable, Expression condition)
    {
        Variable = variable;
        Condition = condition;
    }
    public void Print(int ident = 0)
    {
        Debug.Log(new string(' ', ident) + "Predicate:");
        Variable?.Print(ident + 2);
        Condition?.Print(ident + 2);
    }

}
public class PostAction : Node   //revisado
{
    public Expression Type;
    public Selector Selector;

    public List<Assignment> Assignments;
    public PostAction(Expression type, Selector selector)
    {
        Type = type;
        Selector = selector;
        Assignments = new List<Assignment>();
    }

    public void Print(int ident = 0)
    {
        Debug.Log(new string(' ', ident) + "PostAction:");
        Type?.Print(ident + 2);
        Selector?.Print(ident + 2);
    }
}
public class Action : Node     //revisado
{
    public Variable Targets;
    public Variable Context;

    public StatementBlock Block;

    public Action(Variable targets, Variable context, StatementBlock block)
    {
        Targets = targets;
        Context = context;
        Block = block;
    }
    public void Print(int ident = 0)
    {
        Debug.Log(new string(' ', ident) + "Action:");
        Targets?.Print(ident + 2);
        Context?.Print(ident + 2);
        Block?.Print(ident + 2);
    }



}
public interface Statement : Node //revisado
{
    public void Evaluater(Scope scope);
}
public class StatementBlock : Node        //revisado
{
    public List<Statement> statements;

    public StatementBlock()
    {
        statements = new List<Statement>();
    }

    public void Print(int ident = 0)
    {
        Debug.Log(new string(' ', ident) + "StatementsBlock:");
        foreach (var stmt in statements)
        {
            stmt.Print(ident + 2);
        }
    }

}
public class Assignment : Statement  //revisado
{
    public Variable Left;
    public Token Operator;
    public Expression Right;
    public Assignment(Variable left, Token op, Expression right)
    {
        Left = left;
        Operator = op;
        Right = right;
    }
    public void Print(int ident = 0)
    {
        Debug.Log(new string(' ', ident) + "Assignment:");
        Left?.Print(ident + 2);
        Debug.Log(new string(' ', ident + 2) + "Op: " + Operator.lexeme);
        Right?.Print(ident + 2);
    }
    public void Evaluater(Scope scope)
    {
        object rightValue = Right.Evaluate(scope);

        if (Operator.type == TokenType.EQUAL)
        {
            if (Left is VariableCompound)
            {
                (Left as VariableCompound).GetValue(scope, rightValue);
            }
            else
            {
                scope.value[Left.name] = rightValue;
            }
        }
        else if (Operator.type == TokenType.PLUS_EQUAL || Operator.type == TokenType.MINUS_EQUAL)
        {
            object leftValue = scope.value[Left.name];
            if (leftValue is int leftInt && rightValue is int rightInt)
            {
                int result = (Operator.type == TokenType.PLUS_EQUAL) ? leftInt + rightInt : leftInt - rightInt;
                scope.value[Left.name] = result;
            }
            else if (leftValue is double leftDouble && rightValue is double rightDouble)
            {
                double result = (Operator.type == TokenType.PLUS_EQUAL) ? leftDouble + rightDouble : leftDouble - rightDouble;
                scope.value[Left.name] = result;
            }
            else if (leftValue is Cards card && rightValue is int value)
            {
                if (Operator.type == TokenType.PLUS_EQUAL)
                {
                    card.cardPower += value;
                }
                else if (Operator.type == TokenType.MINUS_EQUAL)
                {
                    card.cardPower -= value;
                }
                scope.value[Left.name] = card;
            }
            else
            {
                throw new InvalidOperationException($"Operación no soportada para los tipos {leftValue?.GetType()} y {rightValue?.GetType()}");
            }
        }




        //     if (Operator.type == TokenType.EQUAL)
        //     {
        //         if (Left is VariableCompound)
        //         {
        //             (Left as VariableCompound).GetValue(scope, Right.Evaluate(scope));
        //         }
        //         else
        //         {
        //             scope.value[Left.name] = Right.Evaluate(scope);
        //         }
        //     }
        //     else if (Operator.type == TokenType.PLUS_EQUAL)
        //     {
        //         if (Left is VariableCompound)
        //         {
        //             (Left as VariableCompound).GetValue(scope, Convert.ToInt32(Left.Evaluate(scope)) + Convert.ToInt32(Right.Evaluate(scope)));
        //         }
        //         else
        //         {
        //             int result = Convert.ToInt32(scope.value[Left.name]);
        //             result += Convert.ToInt32(Right.Evaluate(scope));
        //             scope.value[Left.name] = result;
        //         }
        //     }
        //     else if (Operator.type == TokenType.MINUS_EQUAL)
        //     {
        //         if (Left is VariableCompound)
        //         {
        //             (Left as VariableCompound).GetValue(scope, Convert.ToInt32(Left.Evaluate(scope)) - Convert.ToInt32(Right.Evaluate(scope)));
        //         }
        //         else
        //         {
        //             int result = Convert.ToInt32(scope.value[Left.name]);
        //             result -= Convert.ToInt32(Right.Evaluate(scope));
        //             scope.value[Left.name] = result;
        //         }
        //     }
        // }
    }
}
public class WhileStatement : Statement      //revisado
{
    public Expression condition;
    public StatementBlock body;
    public WhileStatement(Expression condition, StatementBlock body)
    {
        this.condition = condition;
        this.body = body;
    }
    public void Print(int ident = 0)
    {
        Debug.Log(new string(' ', ident) + "WhileStatement:");
        Debug.Log(new string(' ', ident + 2) + "Condition:");
        condition?.Print(ident + 2);
        Debug.Log(new string(' ', ident + 2) + "Body:");
        body?.Print(ident + 2);
    }
    public void Evaluater(Scope scope)
    {
        while ((bool)condition.Evaluate(scope))
        {
            foreach (var stmt in body.statements)
            {
                stmt.Evaluater(scope);
            }
        }
    }
}
public class ForStatement : Statement     //revisado
{
    public Variable Variable;
    public Variable Target;
    public StatementBlock Body;

    public ForStatement(Variable condition, Variable increment, StatementBlock body)
    {
        Variable = condition;
        Target = increment;
        Body = body;
    }
    public void Print(int ident = 0)
    {
        Debug.Log(new string(' ', ident) + "ForStatement:");
        Debug.Log(new string(' ', ident + 2) + "Variable:");
        Variable?.Print(ident + 2);
        Debug.Log(new string(' ', ident + 2) + "Target:");
        Target?.Print(ident + 2);
        Debug.Log(new string(' ', ident + 2) + "Body:");
        Body?.Print(ident + 2);
    }
    public void Evaluater(Scope scope)
    {
        foreach (Cards target in scope.value["targets"] as List<Cards>)
        {
            scope.value["target"] = target;
            foreach (var stmt in Body.statements)
            {
                stmt.Evaluater(scope);
            }
            scope.value.Remove("target");
        }
    }

}
[System.Serializable]
public class Program : Node    //revisado
{
    public List<Effect> effects;
    public List<Card> card;

    public Program()
    {
        card = new List<Card>();
        effects = new List<Effect>();
    }
    public void Print(int indent = 0)
    {
        Debug.Log(new string(' ', indent) + "Program:");
        foreach (var card in card)
        {
            card.Print(indent + 2);
        }
        foreach (var effect in effects)
        {
            effect.Print(indent + 2);
        }
    }


}
public class Function : Statement  //revisado
{
    public string Name;
    public Params Params;
    public Variable.Type type;
    public Function(string name, Params Params)
    {
        Name = name;
        this.Params = Params;
        type = Variable.Type.NULL;
        VariableReturn();
    }

    public void VariableReturn()
    {
        if (Name == "FieldOfPlayer") type = Variable.Type.CONTEXT;
        if (Name == "HandOfPlayer") type = Variable.Type.FIELD;
        if (Name == "GraveyardOfPlayer") type = Variable.Type.FIELD;
        if (Name == "DeckOfPlayer") type = Variable.Type.FIELD;
        if (Name == "Find") type = Variable.Type.TARGETS;
        if (Name == "Push") type = Variable.Type.VOID;
        if (Name == "SendBottom") type = Variable.Type.VOID;
        if (Name == "Pop") type = Variable.Type.CARD;
        if (Name == "Remove") type = Variable.Type.VOID;
        if (Name == "Shuffle") type = Variable.Type.VOID;
        if (Name == "Add") type = Variable.Type.VOID;
    }
    public void Print(int indent = 0)
    {
        Debug.Log(new string(' ', indent) + "Function:");
        Debug.Log(new string(' ', indent + 2) + "FunctionName: " + Name);
        Params?.Print(indent + 2);
        Debug.Log(new string(' ', indent + 2) + "Return Type: " + type.ToString());
    }
    public object ValueReturn(Scope scope, object value)
    {
        switch (Name)
        {
            case "TriggerPlayer": return scope.gameManager.TriggerPlayer();
            case "HandOfPlayer":
                if (Params.nodes[0] is Function) return scope.gameManager.HandOfPlayer(Convert.ToInt32((Params.nodes[0] as Function).ValueReturn(scope, value)));
                else return scope.gameManager.HandOfPlayer(Convert.ToInt32((Params.nodes[0] as Expression).Evaluate(scope)));
            case "DeckOfPlayer":
                if (Params.nodes[0] is Function) return scope.gameManager.DeckOfPlayer(Convert.ToInt32((Params.nodes[0] as Function).ValueReturn(scope, value)));
                else return scope.gameManager.DeckOfPlayer(Convert.ToInt32((Params.nodes[0] as Expression).Evaluate(scope)));
            case "GraveyardOfPlayer":
                if (Params.nodes[0] is Function) return scope.gameManager.GraveyardOfPlayer(Convert.ToInt32((Params.nodes[0] as Function).ValueReturn(scope, value)));
                else return scope.gameManager.GraveyardOfPlayer(Convert.ToInt32((Params.nodes[0] as Expression).Evaluate(scope)));
            case "FieldOfPlayer":
                if (Params.nodes[0] is Function) return scope.gameManager.FieldOfPlayer(Convert.ToInt32((Params.nodes[0] as Function).ValueReturn(scope, value)));
                else return scope.gameManager.FieldOfPlayer(Convert.ToInt32((Params.nodes[0] as Expression).Evaluate(scope)));
            case "Find":
                List<Cards> cardLists = value as List<Cards>;
                Predicate predicate = Params.nodes[0] as Predicate;
                if (cardLists != null && predicate != null)
                {
                    return Find(predicate, cardLists);
                }
                return null;


            case "Push":
                Cards cardToPush = (Params.nodes[0] as Expression).Evaluate(scope) as Cards;
                if (cardToPush != null)
                {
                    (value as List<Cards>).Insert(0, cardToPush);
                    Debug.Log($"Carta {cardToPush.cardName} agregada al tope de la lista");
                }
                else
                {
                    Debug.LogWarning("La carta a agregar es nula");
                }
                return null;
            case "SendBottom":
                Cards cardToSendBottom = (Params.nodes[0] as Expression).Evaluate(scope) as Cards;
                if (cardToSendBottom != null)
                {
                    (value as List<Cards>).Add(cardToSendBottom);
                    Debug.Log($"Card {cardToSendBottom.cardName} added to the bottom of the list");
                }
                else
                {
                    Debug.LogWarning("The card to add is null");
                }
                return null;
            case "Pop":
                List<Cards> cardList = value as List<Cards>;
                if (cardList != null && cardList.Count > 0)
                {
                    Cards topCard = cardList[0];
                    cardList.RemoveAt(0);
                    Debug.Log($"Carta {topCard.cardName} removida del tope de la lista");
                    return topCard;
                }
                else
                {
                    Debug.LogWarning("La lista está vacía o es nula");
                    return null;
                }
            case "Remove":
                Cards cardToRemove = (Params.nodes[0] as Expression).Evaluate(scope) as Cards;
                Debug.Log($"Card to remove: {cardToRemove}");
                (value as List<Cards>).Remove(cardToRemove);
                Debug.Log($"Card gameObject: {cardToRemove?.gameObject}");
                if (cardToRemove.gameObject != null)
                {
                    cardToRemove.cardPower = 999;
                    Debug.LogError("Card Destroyed");
                }
                return null;
            case "Shuffle":
                List<Cards> deckToShuffle = value as List<Cards>;
                if (deckToShuffle != null && deckToShuffle.Count > 0)
                {
                    int n = deckToShuffle.Count;
                    System.Random rng = new System.Random();
                    while (n > 1)
                    {
                        n--;
                        int k = rng.Next(n + 1);
                        Cards temp = deckToShuffle[k];
                        deckToShuffle[k] = deckToShuffle[n];
                        deckToShuffle[n] = temp;
                    }
                    Debug.Log("La lista de cartas ha sido mezclada");
                }
                else
                {
                    Debug.LogWarning("La lista está vacía o es nula");
                }
                return null;
            default: return null;
        }
    }
    public void Evaluater(Scope scope)
    {
        throw new NotImplementedException();
    }
    public List<Cards> Find(Predicate predicate, List<Cards> cards)
    {
        List<Cards> cardsResult = new List<Cards>();
        foreach (var card in cards)
        {
            new Scope().value[predicate.Variable.name] = card;
            if ((bool)predicate.Condition.Evaluate(new Scope())) cardsResult.Add(card);
            new Scope().value.Remove(predicate.Variable.name);
        }
        return cardsResult;
    }


}
public class Pointer : Node    //revisado
{
    public string pointer;
    public Pointer(string pointer)
    {
        this.pointer = pointer;
    }

    public void Print(int ident = 0)
    {
        Debug.Log(new string(' ', ident) + "Pointer: " + pointer);
    }
}
public class OnActivation : Node   //revisado
{
    public List<OnActivationElements> Elements;

    public OnActivation()
    {
        Elements = new List<OnActivationElements>();
    }

    public void Print(int ident = 0)
    {
        Debug.Log(new string(' ', ident) + "OnActivation:");
        foreach (var element in Elements)
        {
            element.Print(ident + 2);
        }
    }
}
public class OnActivationElements : Node //revisado
{
    public OnActivationEffect oae;
    public Selector selector;
    public List<PostAction> postAction;

    public OnActivationElements(OnActivationEffect oae, Selector selector, List<PostAction> postAction)
    {
        this.oae = oae;
        this.selector = selector;
        this.postAction = postAction;
    }
    public void Print(int ident = 0)
    {
        Debug.Log(new string(' ', ident) + "OnActivationElements:");
        if (oae != null)
        {
            oae.Print(ident + 2);
        }
        if (selector != null)
        {
            selector.Print(ident + 2);
        }
        if (postAction != null)
        {
            foreach (var action in postAction)
            {
                if (action != null)
                {
                    action.Print(ident + 2);
                }
            }
        }
    }
}
[System.Serializable]
public class Card : Node   //revisado
{
    public CardType Type;
    public Name Name;
    public Faction Faction;
    public Power Power;
    public Range Range;
    public OnActivation OnActivation;

    public Card()
    {

    }
    public void Print(int indent = 0)
    {
        Debug.Log(new string(' ', indent) + "Card:");
        Type?.Print(indent + 2);
        Name?.Print(indent + 2);
        Faction?.Print(indent + 2);
        Power?.Print(indent + 2);
        Range?.Print(indent + 2);
        OnActivation?.Print(indent + 2);
    }

}
public class Owner : Node
{
    public string owner;
    public Owner(string owner)
    {
        this.owner = owner;
    }

    public void Print(int indent = 0)
    {
        Debug.Log(new string(' ', indent) + "Owner: " + owner);
    }
}
public class Inde : Node
{
    public int index;
    public Inde(int index)
    {
        this.index = index;
    }

    public void Print(int indent = 0)
    {
        Debug.Log(new string(' ', indent) + "Indexer: " + index);
    }
}
