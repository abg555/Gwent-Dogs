using System.Collections.Concurrent;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

public class Parser
{
    private List<Token> tokens;
    private int current = 0;

    public Parser(List<Token> tokens)
    {
        this.tokens = tokens;
    }

    public Expression Parse()
    {
        try
        {
            var result = Equality();
            if (!isAtEnd())
            {
                throw new Error($"Unexpected token '{Peek().lexeme}' at line {Peek().line}", ErrorType.LEXICAL);
            }
            return result;
        }
        catch (Error exception)
        {
            Console.WriteLine($"Semantical error:{exception.Message}");
            throw;
        }
    }


    private Expression Equality()
    {
        Expression expression = Comparison();
        while (Match(TokenType.BANG_EQUAL) || Match(TokenType.EQUAL_EQUAL))
        {
            Token ope = Previous();
            Expression right = Comparison();
            expression = new Binary(expression, ope, right);
        }
        return expression;
    }

    private bool Match(TokenType type)
    {

        if (Check(type))
        {
            Advance();
            return true;
        }


        return false;
    }
    private bool Check(TokenType type)
    {
        if (isAtEnd()) return false;
        return Peek().type == type;
    }
    private Token Advance()
    {
        if (!isAtEnd()) current++;
        return Previous();
    }

    private bool isAtEnd()
    {
        return Peek().type == TokenType.EOF;
    }
    private Token Peek()
    {
        return tokens[current];
    }
    private Token Previous()
    {
        return tokens[current - 1];
    }

    private Expression Comparison()
    {
        Expression expression = Term();
        while (Match(TokenType.GREATER) || Match(TokenType.GREATER_EQUAL) || Match(TokenType.LESS) || Match(TokenType.LESS_EQUAL))
        {
            Token ope = Previous();
            Expression right = Term();
            expression = new Binary(expression, ope, right);
        }
        return expression;
    }

    private Expression Term()
    {
        Expression expression = Factor();
        while (Match(TokenType.MINUS) || Match(TokenType.PLUS))
        {
            Token ope = Previous();
            Expression right = Factor();
            expression = new Binary(expression, ope, right);
        }
        return expression;
    }

    private Expression Factor()
    {
        Expression expression = Unary();
        while (Match(TokenType.SLASH) || Match(TokenType.STAR))
        {
            Token oper = Previous();
            Expression right = Unary();
            expression = new Binary(expression, oper, right);
        }
        return expression;
    }
    private Expression Unary()
    {
        if (Match(TokenType.MINUS) || Match(TokenType.BANG))
        {
            Token ope = Previous();
            Expression right = Unary();
            return new Unary(ope, right);
        }
        return Primary();
    }

    private Expression Primary()
    {
        if (Match(TokenType.FALSE)) return new Literal(false);
        if (Match(TokenType.TRUE)) return new Literal(true);
        if (Match(TokenType.NUMBER)) return new Literal(double.Parse(Previous().lexeme));
        if (Match(TokenType.STRING)) return new Literal(Previous().lexeme);
        if (Match(TokenType.LEFT_PAREN))
        {
            Expression expression = Equality();
            Consume(TokenType.RIGHT_PAREN, "Expect ) after expression");
            return new Grouping(expression);
        }
        throw new Exception("Expected expression");

    }

    private Token Consume(TokenType type, string message)
    {
        if (Check(type)) return Advance();
        throw new Exception($"{message} but got {Peek().type}");

    }







}