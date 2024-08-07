using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Reflection.Metadata;

public abstract class Expression
{
    public abstract void Print(int indent = 0);
    public abstract object Evaluate();

}
public abstract class Node
{


}

public class Number : Expression
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

    public override void Print(int indent = 0)
    {
        Console.WriteLine(new string(' ', indent) + Value.ToString());
    }
}
public class StringExpression : Expression
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

    public override void Print(int indent = 0)
    {
        Console.WriteLine(new string(' ', indent) + Value);
    }
}
public class Bool : Expression
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
        Console.WriteLine(new string(' ', indent) + Value.ToString());
    }
}


public class Binary : Expression
{
    public Expression Left { get; }

    public Token Operator { get; }
    public Expression Right { get; }

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
        object leftValue = Left.Evaluate();
        object rightValue = Right.Evaluate();
        if (leftValue is double && rightValue is double)
        {
            switch (Operator.type)
            {
                case TokenType.PLUS:
                    return Convert.ToDouble(leftValue) + Convert.ToDouble(rightValue);
                case TokenType.MINUS:
                    return Convert.ToDouble(leftValue) - Convert.ToDouble(rightValue);
                case TokenType.STAR:
                    return Convert.ToDouble(leftValue) * Convert.ToDouble(rightValue);
                case TokenType.SLASH:
                    return Convert.ToDouble(leftValue) / Convert.ToDouble(rightValue);
                case TokenType.PERCENT:
                    return Convert.ToDouble(leftValue) % Convert.ToDouble(rightValue);
                case TokenType.POW:
                    return Math.Pow(Convert.ToDouble(leftValue), Convert.ToDouble(rightValue));
                case TokenType.GREATER:
                    return Convert.ToDouble(leftValue) > Convert.ToDouble(rightValue);
                case TokenType.GREATER_EQUAL:
                    return Convert.ToDouble(leftValue) >= Convert.ToDouble(rightValue);
                case TokenType.LESS:
                    return Convert.ToDouble(leftValue) < Convert.ToDouble(rightValue);
                case TokenType.LESS_EQUAL:
                    return Convert.ToDouble(leftValue) <= Convert.ToDouble(rightValue);
                case TokenType.BANG_EQUAL: return !leftValue.Equals(rightValue);
                case TokenType.EQUAL_EQUAL: return leftValue.Equals(rightValue);
                default:
                    throw new InvalidOperationException("Unsupported operator: " + Operator.lexeme);
            }
        }
        else if (leftValue is string && rightValue is string)
        {
            switch (Operator.type)
            {
                case TokenType.ATSIGN: return leftValue.ToString() + rightValue.ToString();
                case TokenType.ATSIGN_ATSIGN: return leftValue.ToString() + " " + rightValue.ToString();
                default:
                    throw new InvalidOperationException("Unsupported operator: " + Operator.lexeme);
            }
        }
        else throw new InvalidOperationException("Unsupported operator: " + Operator.lexeme);
    }

}


public class Unary : Expression
{


    public Token Operator { get; }
    public Expression Right { get; }
    public Unary(Token Operator, Expression Right)
    {

        this.Operator = Operator;
        this.Right = Right;
    }

    public override void Print(int indent = 0)
    {
        Console.WriteLine(new string(' ', indent) + Operator.lexeme);
        Right.Print(indent + 2);
    }
    public override object Evaluate()
    {
        object rightValue = Right.Evaluate();

        switch (Operator.type)
        {
            case TokenType.MINUS:
                return -Convert.ToDouble(rightValue);
            case TokenType.BANG:
                return !(bool)rightValue;
            default:
                throw new InvalidOperationException("Unsupported operator: " + Operator.lexeme);
        }
    }
}

public class Literal : Expression
{
    public Object Value { get; }

    public Literal(Object Value)
    {
        this.Value = Value;
    }

    public override void Print(int indent = 0)
    {

        Console.WriteLine(new string(' ', indent) + Value.ToString());
    }
    public override object Evaluate()
    {
        throw new NotImplementedException();
    }
}




public class Grouping : Expression
{
    public Expression Expression { get; }

    public Grouping(Expression Expression)
    {
        this.Expression = Expression;
    }

    public override void Print(int indent = 0)
    {

        Expression.Print(indent);

    }
    public override object Evaluate()
    {
        return Expression.Evaluate();
    }
}

public class Assing : Expression
{
    public Token ID { get; }
    public Expression Value { get; }

    public Assing(Token ID, Expression Value)
    {
        this.ID = ID;
        this.Value = Value;
    }

    public override void Print(int indent = 0)
    {
        throw new NotImplementedException();
    }
    public override object Evaluate()
    {
        throw new NotImplementedException();
    }
}


public class Variable : Expression
{
    public Token ID { get; }
    public string Value { get; }
    public Type type;
    public enum Type
    {
        TARGETS, CONTEXT, CARD, FIELD, INT, STRING, BOOL, VOID, NULL
    }

    public void TypeParam(TokenType tokenType)
    {
        if (tokenType == TokenType.BOOLTYPE)
        {
            type = Type.BOOL;
        }
        if (tokenType == TokenType.NUMBERTYPE)
        {
            type = Type.INT;
        }
        if (tokenType == TokenType.STRINGTYPE)
        {
            type = Type.STRING;
        }
    }

    public Variable(Token ID)
    {
        this.ID = ID;
        Value = ID.lexeme;
    }

    public override void Print(int indent = 0)
    {
        throw new NotImplementedException();
    }
    public override object Evaluate()
    {
        throw new NotImplementedException();
    }
}

public class LetIn : Expression
{
    public List<Assing> Assigments { get; }
    public Expression Body { get; }
    public LetIn(List<Assing> Assigments, Expression Body)
    {
        this.Assigments = Assigments;
        this.Body = Body;
    }

    public override void Print(int indent = 0)
    {
        throw new NotImplementedException();
    }
    public override object Evaluate()
    {
        throw new NotImplementedException();
    }
}

public class FunctionDeclaration : Expression
{
    public string Identifier { get; }
    public List<FunctionDeclaration> Arguments;
    public Expression Body { get; }

    public FunctionDeclaration(string indentifier, Expression Body, List<FunctionDeclaration> Arguments)
    {
        Identifier = indentifier;
        this.Body = Body;
        this.Arguments = Arguments;
    }


    public override void Print(int indent = 0)
    {
        throw new NotImplementedException();
    }
    public override object Evaluate()
    {
        throw new NotImplementedException();
    }
}

public class VariableExpression : Expression
{
    public Token name { get; }

    public VariableExpression(Token name)
    {
        this.name = name;
    }

    public override void Print(int indent = 0)
    {
        Console.WriteLine(new string(' ', indent) + name.lexeme);
    }
    public override object Evaluate()
    {
        throw new NotImplementedException();
    }
}

public class FunctionCall : Expression
{
    public string Identifier { get; }
    public List<FunctionCall> Arguments;

    public FunctionCall(string Identifier, List<FunctionCall> Arguments)
    {
        this.Identifier = Identifier;
        this.Arguments = Arguments;

    }

    public override void Print(int ident = 0)
    {
        throw new NotImplementedException();
    }
    public override object Evaluate()
    {
        throw new NotImplementedException();
    }
}

public class Name : Expression
{
    public string name { get; }

    public Name(string name)
    {
        this.name = name;
    }

    public override void Print(int ident = 0)
    {
        throw new NotImplementedException();
    }
    public override object Evaluate()
    {
        throw new NotImplementedException();
    }
}
public class Type : Expression
{
    public string type { get; }

    public Type(string type)
    {
        this.type = type;
    }

    public override void Print(int ident = 0)
    {
        throw new NotImplementedException();
    }
    public override object Evaluate()
    {
        throw new NotImplementedException();
    }
}
public class Faction : Expression
{
    public string faction { get; }

    public Faction(string faction)
    {
        this.faction = faction;
    }

    public override void Print(int ident = 0)
    {
        throw new NotImplementedException();
    }
    public override object Evaluate()
    {
        throw new NotImplementedException();
    }
}

public class Power : Expression
{
    public int power { get; }

    public Power(int power)
    {
        this.power = power;
    }

    public override void Print(int ident = 0)
    {
        throw new NotImplementedException();
    }
    public override object Evaluate()
    {
        throw new NotImplementedException();
    }
}

public class Range : Expression
{
    public string range { get; }

    public Range(string range)
    {
        this.range = range;
    }

    public override void Print(int ident = 0)
    {
        throw new NotImplementedException();
    }
    public override object Evaluate()
    {
        throw new NotImplementedException();
    }
}




public class Effect : Node
{
    public Expression Name;
    public List<Variable> Params;
    public Action Action;

    public Effect()
    {


    }


}


public class OnActivationEffect
{
    public string name { get; }
    public List<Assignment> Params { get; }

    public OnActivationEffect(string name, List<Assignment> Params)
    {
        this.name = name;
        this.Params = Params;

    }


}

public class Selector
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



}
public class Single
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

}
public class Predicate
{
    public Variable Variable;
    public Expression Condition;
    public Predicate(Variable variable, Expression condition)
    {
        Variable = variable;
        Condition = condition;
    }
}

public class PostAction
{
    public Expression Type;
    public Selector Selector;
    public PostAction(Expression type, Selector selector)
    {
        Type = type;
        Selector = selector;
    }
}

public class Action
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

}

public abstract class Statement
{

}


public class StatementBlock
{
    public List<Statement> statements;

    public StatementBlock()
    {
        statements = new List<Statement>();
    }

    public void AddStatment(Statement statement)
    {
        statements.Add(statement);
    }

}

public class FunctionStatement : Statement
{
    public Token Name { get; }
    public List<Variable> Parameters { get; }
    public List<Statement> Body { get; }

    public FunctionStatement(Token name, List<Variable> parameters, List<Statement> body)
    {
        Name = name;
        Parameters = parameters;
        Body = body;
    }
}
public class ExpressionStatement : Statement
{
    public Expression Expression { get; }

    public ExpressionStatement(Expression expression)
    {
        Expression = expression;
    }
}
public class Assignment : Statement
{
    public Variable Left;
    public Token Op;
    public Expression Right;
    public Assignment(Variable left, Token op, Expression right)
    {
        Left = left;
        Op = op;
        Right = right;
    }
}

public class WhileStatement : Statement
{
    public Expression condition;
    public StatementBlock body;
    public WhileStatement(Expression condition, StatementBlock body)
    {
        this.condition = condition;
        this.body = body;
    }
}
public class ForStatement : Statement
{
    public Statement Initializer { get; }
    public Expression Condition { get; }
    public Expression Increment { get; }
    public StatementBlock Body { get; }

    public ForStatement(Statement initializer, Expression condition, Expression increment, StatementBlock body)
    {
        Initializer = initializer;
        Condition = condition;
        Increment = increment;
        Body = body;
    }

}




public class Program : Node
{
    public List<Effect> effects;
    public List<Card> card;

    public Program()
    {
        card = new List<Card>();
        effects = new List<Effect>();
    }



}
public class Function
{
    public Token FunctionName;
}
public class OnActivation
{
    public List<OnActivationElements> Elements;

    public OnActivation()
    {
        Elements = new List<OnActivationElements>();
    }
}
public class OnActivationElements
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

}


public class Card : Node
{
    public Expression Type;
    public Expression Name;
    public Expression Faction;
    public Expression Power;
    public Expression[] Range;
    public OnActivation OnActivation;

    public Card()
    {


    }


}

public class Compound : Expression
{
    public List<Expression> Children;


    public Compound()
    {
        Children = new List<Expression>();
    }

    public override void Print(int ident = 0)
    {
        throw new NotImplementedException();
    }
    public override object Evaluate()
    {
        throw new NotImplementedException();
    }

    public bool ValidCard()
    {
        Dictionary<TokenType, int> check = new Dictionary<TokenType, int>();
        foreach (var child in Children)
        {
            if (child.GetType() == typeof(Name)) check[TokenType.NAME]++;
            if (child.GetType() == typeof(Faction)) check[TokenType.FACTION]++;
            if (child.GetType() == typeof(Type)) check[TokenType.TYPE]++;
            if (child.GetType() == typeof(Range)) check[TokenType.RANGE]++;
            if (child.GetType() == typeof(Power)) check[TokenType.POWER]++;
            if (child.GetType() == typeof(Compound)) check[TokenType.ONACTIVATION]++;
        }
        if (Children.Count != 6) return false;
        if (check.ContainsKey(TokenType.NAME) && check[TokenType.NAME] != 1) return false;
        if (check.ContainsKey(TokenType.POWER) && check[TokenType.POWER] != 1) return false;
        if (check.ContainsKey(TokenType.RANGE) && check[TokenType.RANGE] != 1) return false;
        if (check.ContainsKey(TokenType.TYPE) && check[TokenType.NAME] != 1) return false;
        if (check.ContainsKey(TokenType.FACTION) && check[TokenType.FACTION] != 1) return false;
        if (check.ContainsKey(TokenType.ONACTIVATION) && check[TokenType.ONACTIVATION] != 1) return false;

        return true;
    }

    public bool ValidEffect()
    {
        Dictionary<TokenType, int> check = new Dictionary<TokenType, int>();
        foreach (var child in Children)
        {
            if (child.GetType() == typeof(Name)) check[TokenType.NAME]++;
            if (child.GetType() == typeof(Compound)) check[TokenType.PARAMS]++;
            if (child.GetType() == typeof(Compound)) check[TokenType.ACTION]++;

        }
        if (Children.Count > 3) return false;
        if (check.ContainsKey(TokenType.NAME) && check[TokenType.NAME] != 1) return false;
        if (check.ContainsKey(TokenType.PARAMS) && check[TokenType.PARAMS] != 1) return false;
        if (check.ContainsKey(TokenType.ACTION) && check[TokenType.ACTION] != 1) return false;


        return true;
    }
}