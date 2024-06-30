using System.Diagnostics.Contracts;
using System.Reflection.Metadata;

public abstract class Expression
{

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
}

public class Literal : Expression
{
    public object Value { get; }

    public Literal(object Value)
    {
        this.Value = Value;
    }
}


public class Grouping : Expression
{
    public Expression Expression { get; }

    public Grouping(Expression Expression)
    {
        this.Expression = Expression;
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
}


public class Variable : Expression
{
    public Token ID { get; }

    public Variable(Token ID)
    {
        this.ID = ID;
    }
}

public class IfElseStatement : Expression
{
    public Expression Condition { get; }
    public Expression thenBranch { get; }
    public Expression elseBranch { get; }

    public IfElseStatement(Expression Condition, Expression thenBranch, Expression elseBranch)
    {
        this.Condition = Condition;
        this.thenBranch = thenBranch;

        this.elseBranch = elseBranch;
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


}

public class FunctionDeclaration : Expression
{
    public string Identifier { get; }
    public List<FunctionDeclaration> Arguments;
    public Expression Body { get; }

    public FunctionDeclaration(string Identifier, Expression Body, List<FunctionDeclaration> Arguments)
    {
        this.Identifier = Identifier;
        this.Body = Body;
        this.Arguments = Arguments;
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
}

public class Name : Expression
{
    public string name { get; }

    public Name(string name)
    {
        this.name = name;
    }
}
public class Type : Expression
{
    public string type { get; }

    public Type(string type)
    {
        this.type = type;
    }
}
public class Faction : Expression
{
    public string faction { get; }

    public Faction(string faction)
    {
        this.faction = faction;
    }
}

public class Power : Expression
{
    public int power { get; }

    public Power(int power)
    {
        this.power = power;
    }
}

public class Range : Expression
{
    public string range { get; }

    public Range(string range)
    {
        this.range = range;
    }
}

public class Compound : Expression
{
    public List<Expression> Children;


    public Compound()
    {
        Children = new List<Expression>();
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