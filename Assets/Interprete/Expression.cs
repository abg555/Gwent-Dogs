using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.Marshalling;

public abstract class Expression : Node  //revisado
{
    public abstract void Print(int indent = 0);
    public abstract object Evaluate();

}
public interface Node     //revisado
{
    public void Print(int indent = 0);

}
public class Number : Expression        //revisado
{
    public int Value;
    public Number(int value)
    {
        Value = value;
    }

    public override object Evaluate()
    {
        return Value;
    }

    public override void Print(int ident = 0)
    {
        Console.WriteLine(new string(' ', ident) + "Number:" + Value);
    }
}
public class StringExpression : Expression       //revisado
{
    public string Value;
    public StringExpression(string value)
    {
        Value = value;
    }

    public override object Evaluate()
    {
        return Value;
    }

    public override void Print(int ident = 0)
    {
        Console.WriteLine(new string(' ', ident) + "String:" + Value);
    }
}
public class Bool : Expression           //revisado
{
    public bool Value;
    public Bool(bool value)
    {
        Value = value;
    }

    public override object Evaluate()
    {
        return Value;
    }

    public override void Print(int indent = 0)
    {
        Console.WriteLine(new string(' ', indent) + "Bool:" + Value);
    }
}
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
        Console.WriteLine(new string(' ', indent) + Operator.lexeme);
        Left.Print(indent + 2);
        Right.Print(indent + 2);
    }

    public override object Evaluate()
    {
        object left = Left.Evaluate();
        object right = Right.Evaluate();
        if (left is double && right is double)
        {
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
                default:
                    throw new InvalidOperationException("Unsupported operator: " + Operator.lexeme);
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
        Console.WriteLine(new string(' ', indent) + "Unary:" + Operator.lexeme);
        Right.Print(indent + 2);
    }
    public override object Evaluate()
    {
        object right = Right.Evaluate();

        switch (Operator.type)
        {
            case TokenType.MINUS:
                return -Convert.ToDouble(right);
            case TokenType.BANG:
                return !(bool)right;
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
        Console.WriteLine(new string(' ', ident) + "ExpressionGroup:");
        Expression.Print(ident + 2);

    }
    public override object Evaluate()
    {
        return Expression.Evaluate();
    }
}
public class Variable : Expression      //revisado
{
    public Token ID { get; }
    public string name { get; }
    public Type type;
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
        Console.WriteLine(new string(' ', indent) + "Variable: " + name + " (" + type.ToString() + ")");
    }
    public override object Evaluate()
    {
        throw new NotImplementedException();
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
        Console.WriteLine(new string(' ', ident) + "VariableComp: ");
        argument?.Print(ident + 2);
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
        Console.WriteLine(new string(' ', ident) + "Args:");
        foreach (var arg in nodes)
        {
            arg.Print(ident + 2);
        }
    }

}
public class Name : Node      // revisado
{
    public Expression name;

    public Name(Expression name)
    {
        this.name = name;
    }

    public void Print(int ident = 0)
    {
        Console.WriteLine(new string(' ', ident) + "Name:");
        name.Print(ident + 2);
    }

}
public class Type : Node       //revisado
{
    public Expression type;

    public Type(Expression type)
    {
        this.type = type;
    }

    public void Print(int ident = 0)
    {
        Console.WriteLine(new string(' ', ident) + "Name:");
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
        Console.WriteLine(new string(' ', ident) + "Name:");
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
        Console.WriteLine(new string(' ', ident) + "Name:");
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
        Console.WriteLine(new string(' ', ident) + "Range:");
        if (range != null)
        {
            foreach (var expr in expressionsRange)
            {
                expr.Print(ident + 2);
            }
        }
        else
        {
            Console.WriteLine(new string(' ', ident) + "Lexeme: " + range);
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
        Console.WriteLine(new string(' ', ident) + "Effect:");
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
        Console.WriteLine(new string(' ', ident) + "OnActivationEffect:");
        Console.WriteLine(new string(' ', ident + 2) + "Name: " + name);
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
        Console.WriteLine(new string(' ', ident) + "Selector:");
        Console.WriteLine(new string(' ', ident + 2) + "Source: " + Source);
        Singles?.Print(ident + 2);
        Predicate?.Print(ident + 2);
    }

}
public class Single : Node         //revisado
{
    public bool Value;
    public Single(Token token)
    {
        if (token.type == TokenType.BOOL)
        {
            if (token.lexeme == "true") Value = true;
            else Value = false;

        }
    }

    public void Print(int ident = 0)
    {
        Console.WriteLine(new string(' ', ident) + "Single:" + Value);
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
        Console.WriteLine(new string(' ', ident) + "Predicate:");
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
        Console.WriteLine(new string(' ', ident) + "PostAction:");
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
        Console.WriteLine(new string(' ', ident) + "Action:");
        Targets?.Print(ident + 2);
        Context?.Print(ident + 2);
        Block?.Print(ident + 2);
    }



}
public interface Statement : Node //revisado
{

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
        Console.WriteLine(new string(' ', ident) + "StatementsBlock:");
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
        Console.WriteLine(new string(' ', ident) + "Assignment:");
        Left?.Print(ident + 2);
        Console.WriteLine(new string(' ', ident + 2) + "Op: " + Operator.lexeme);
        Right?.Print(ident + 2);
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
        Console.WriteLine(new string(' ', ident) + "WhileStatement:");
        Console.WriteLine(new string(' ', ident + 2) + "Condition:");
        condition?.Print(ident + 2);
        Console.WriteLine(new string(' ', ident + 2) + "Body:");
        body?.Print(ident + 2);
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
        Console.WriteLine(new string(' ', ident) + "ForStatement:");
        Console.WriteLine(new string(' ', ident + 2) + "Variable:");
        Variable?.Print(ident + 2);
        Console.WriteLine(new string(' ', ident + 2) + "Target:");
        Target?.Print(ident + 2);
        Console.WriteLine(new string(' ', ident + 2) + "Body:");
        Body?.Print(ident + 2);
    }

}
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
        Console.WriteLine(new string(' ', indent) + "Program:");
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
        Console.WriteLine(new string(' ', indent) + "Function:");
        Console.WriteLine(new string(' ', indent + 2) + "FunctionName: " + Name);
        Params?.Print(indent + 2);
        Console.WriteLine(new string(' ', indent + 2) + "Return Type: " + type.ToString());
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
        Console.WriteLine(new string(' ', ident) + "Pointer: " + pointer);
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
        Console.WriteLine(new string(' ', ident) + "OnActivation:");
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
    public PostAction postAction;

    public OnActivationElements(OnActivationEffect oae, Selector selector, PostAction postAction)
    {
        this.oae = oae;
        this.selector = selector;
        this.postAction = postAction;
    }
    public void Print(int ident = 0)
    {
        Console.WriteLine(new string(' ', ident) + "OnActivationElements:");
        oae?.Print(ident + 2);
        selector?.Print(ident + 2);
        postAction?.Print(ident + 2);
    }
}
public class Card : Node     //revisado
{
    public Type Type;
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
        Console.WriteLine(new string(' ', indent) + "Card:");
        Type?.Print(indent + 2);
        Name?.Print(indent + 2);
        Faction?.Print(indent + 2);
        Power?.Print(indent + 2);
        Range?.Print(indent + 2);
        OnActivation?.Print(indent + 2);
    }

}

