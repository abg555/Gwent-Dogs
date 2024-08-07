using System.Collections.Concurrent;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Text.RegularExpressions;

public class Parser
{
    private List<Token> tokens;
    private int current = 0;

    public Parser(List<Token> tokens)
    {
        this.tokens = tokens;
    }

    public Expression ParseExpression()
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

    public Node Parse()
    {
        Program program = new Program();
        while (!isAtEnd())
        {
            if (Match(TokenType.CARD))
            {
                program.card.Add(ParseCard());

            }
            else if (Match(TokenType.EFFECT))
            {
                if (Match(TokenType.LEFT_BRACE))
                {
                    program.effects.Add(ParseEffect());
                    Consume(TokenType.RIGHT_BRACE, "Expect '}' after card declaration.");
                }
            }
            else
            {
                throw new Error($"'{Peek().lexeme}' in {Peek().line} : effect or card expected", ErrorType.SYNTAX);
            }
        }
        return program;
    }

    Card ParseCard()
    {
        Consume(TokenType.LEFT_BRACE, "Expected '{' after card");
        Card card = new Card();
        int[] counter = new int[6];
        while (!Check(TokenType.RIGHT_BRACE) && !isAtEnd())
        {
            if (Match(TokenType.TYPE))
            {
                counter[0] += 1;
                Consume(TokenType.COLON, "Expected ':' after Type");
                card.Type = ParseExpression();
                Consume(TokenType.COMMA, "Expected ',' after expression");

            }
            else if (Match(TokenType.NAME))
            {
                counter[1] += 1;
                Consume(TokenType.COLON, "Expected ':' after Name");
                card.Name = ParseExpression();
                Consume(TokenType.COMMA, "Expected ',' after expression");

            }
            else if (Match(TokenType.FACTION))
            {
                counter[2] += 1;
                Consume(TokenType.COLON, "Expected ':' after faction");
                card.Faction = ParseExpression();
                Consume(TokenType.COMMA, "Expected ',' after expression");

            }
            else if (Match(TokenType.POWER))
            {
                counter[3] += 1;
                Consume(TokenType.COLON, "Expected ':' after power");
                card.Power = ParseExpression();
                Consume(TokenType.COMMA, "Expected ',' after expression");

            }
            else if (Match(TokenType.RANGE))
            {
                counter[4] += 1;
                Consume(TokenType.COLON, "Expected ':' after range");
                Consume(TokenType.COMMA, "Expected ',' after expression");
                List<Expression> expressions = new List<Expression>();
                for (int i = 0; i < 3; i++)
                {
                    expressions.Add(ParseExpression());
                    if (Match(TokenType.COMMA)) continue;
                    else break;
                }
                Consume(TokenType.RIGHT_BRACKET, "Expect ']'");
                card.Range = expressions.ToArray();

            }
            else if (Match(TokenType.ONACTIVATION))
            {
                counter[5] += 1;
                card.OnActivation = ParseOnActivation();

            }

            else
            {
                throw new Error($"'{Peek().lexeme}' in {Peek().line} : Invalid card property", ErrorType.SYNTAX);
            }
        }
        Consume(TokenType.RIGHT_BRACE, "Expected '}' after card declaration");
        if (counter[0] < 1) throw new Error("A Type property is missing from card", ErrorType.SYNTAX);
        else if (counter[0] > 1) throw new Error("Only one Type is allowed", ErrorType.SYNTAX);

        if (counter[1] < 1) throw new Error("A Name property is missing from card", ErrorType.SYNTAX);
        else if (counter[1] > 1) throw new Error("Only one Name is allowed", ErrorType.SYNTAX);

        if (counter[2] < 1) throw new Error("A Faction property is missing from card", ErrorType.SYNTAX);
        else if (counter[2] > 1) throw new Error("Only one Faction is allowed", ErrorType.SYNTAX);

        if (counter[3] < 1) throw new Error("A Power property is missing from card", ErrorType.SYNTAX);
        else if (counter[3] > 1) throw new Error("Only one Power is allowed", ErrorType.SYNTAX);

        if (counter[4] < 1) throw new Error("A Range property is missing from card", ErrorType.SYNTAX);
        else if (counter[4] > 1) throw new Error("Only one Range is allowed", ErrorType.SYNTAX);

        if (counter[5] < 1) throw new Error("A OnActivation property is missing from card", ErrorType.SYNTAX);
        else if (counter[5] > 1) throw new Error("Only one OnActivation is allowed", ErrorType.SYNTAX);
        return card;
    }

    Effect ParseEffect()
    {
        Consume(TokenType.LEFT_BRACE, "Expected '{' after effect");
        Effect effect = new Effect();
        int[] counter = new int[3];
        while (!Check(TokenType.RIGHT_BRACE) && !isAtEnd())
        {

            if (Match(TokenType.NAME))
            {
                counter[0] += 1;
                Consume(TokenType.COLON, "Expected ':' after Name");
                effect.Name = ParseExpression();
                Consume(TokenType.COMMA, "Expected ',' after expression");

            }
            else if (Match(TokenType.PARAMS))
            {
                counter[1] += 1;
                Consume(TokenType.COLON, "Expected ':' after Params");
                effect.Params = ParseParams();


            }
            else if (Match(TokenType.ACTION))
            {
                counter[1] += 1;
                Consume(TokenType.COLON, "Expected ':' after action");
                effect.Action = ParseAction();


            }

            else
            {
                throw new Error($"'{Peek().lexeme}' in {Peek().line} : Invalid effect property", ErrorType.SYNTAX);
            }
        }
        Consume(TokenType.RIGHT_BRACE, "Expected '}' after effect declaration");

        if (counter[0] < 1) throw new Error("A Name property is missing from effect", ErrorType.SYNTAX);
        else if (counter[0] > 1) throw new Error("Only one Name is allowed", ErrorType.SYNTAX);

        if (counter[1] < 1) throw new Error("A Params property is missing from effect", ErrorType.SYNTAX);
        else if (counter[1] > 1) throw new Error("Only one Params is allowed", ErrorType.SYNTAX);

        if (counter[2] < 1) throw new Error("A Action property is missing from effect", ErrorType.SYNTAX);
        else if (counter[2] > 1) throw new Error("Only one Action is allowed", ErrorType.SYNTAX);

        return effect;
    }

    OnActivation ParseOnActivation()
    {
        Consume(TokenType.COLON, "Expected ':' after Range");
        Consume(TokenType.LEFT_BRACKET, "Expected '['");
        OnActivation onActivation = new OnActivation();
        while (!Check(TokenType.RIGHT_BRACKET) && !isAtEnd())
        {
            onActivation.Elements.Add(ParseOnActivationElements());


        }
        Consume(TokenType.RIGHT_BRACKET, "Expected ']'");
        return onActivation;
    }

    List<Variable> ParseParams()
    {
        Consume(TokenType.LEFT_BRACE, "Expected '{' after Params");
        List<Variable> param = new List<Variable>();
        while (!Check(TokenType.RIGHT_BRACE) && !isAtEnd())
        {
            param.Add(ParseVariable());
        }
        Consume(TokenType.RIGHT_BRACE, "Expected '}' after Params declaration");
        return param;
    }

    Variable ParseVariable()
    {
        if (Match(TokenType.IDENTIFIER))
        {
            Token name = Previous();
            return new Variable(name);
        }
        throw new Error($"Expected variable name but got '{Peek().lexeme}' at line {Peek().line}", ErrorType.SYNTAX);
    }

    OnActivationElements ParseOnActivationElements()
    {
        Consume(TokenType.LEFT_BRACE, "Expected '{'");
        OnActivationEffect onActivationEffect = null!;
        Selector selector = null!;
        PostAction postAction = null!;
        while (!Check(TokenType.RIGHT_BRACE) && !isAtEnd())
        {
            if (Match(TokenType.ONACTIVATIONEFFECT))
            {
                if (onActivationEffect == null)
                {
                    Consume(TokenType.COLON, "Expected ':'");
                    onActivationEffect = ParseOnActivationEffect();
                }
                else { }
            }
            else if (Match(TokenType.SELECTOR))
            {
                if (selector == null)
                {
                    Consume(TokenType.COLON, "Expected ':'");
                    selector = ParseSelector();
                }
                else { }
            }
            else if (Match(TokenType.POSTACTION))
            {
                if (postAction == null)
                {
                    Consume(TokenType.COLON, "Expected ':'");
                    postAction = ParsePostAction();
                }
                else { }
            }
            else
            {
                throw new Error($"'{Peek().lexeme}' in {Peek().line}: Invalid OnActivation field.", ErrorType.SYNTAX);
            }
        }
        Consume(TokenType.RIGHT_BRACE, "Expected '}' after OnActivation declaration");
        return new OnActivationElements(onActivationEffect, selector!, postAction!);
    }
    OnActivationEffect ParseOnActivationEffect()
    {
        Consume(TokenType.LEFT_PAREN, "Expected '{'");
        string name = null!;
        List<Assignment> assignments = new List<Assignment>();
        while (!Check(TokenType.RIGHT_BRACE) && !isAtEnd())
        {
            if (Match(TokenType.NAME))
            {
                if (Match(TokenType.COLON))
                {
                    if (name == null)
                    {
                        name = Advance().lexeme;
                        if (!Check(TokenType.RIGHT_BRACE)) Consume(TokenType.COMMA, "Expected ','");
                    }
                    else
                    {
                        throw new Error($"'{Peek().lexeme}' in {Peek().line}: Duplicate", ErrorType.SYNTAX);
                    }
                }
                else
                {
                    if (name == null)
                    {
                        name = Advance().lexeme;
                        if (!Check(TokenType.RIGHT_BRACE)) Consume(TokenType.COMMA, "Expected ','");
                    }
                    else
                    {
                        throw new Error($"'{Peek().lexeme}' in {Peek().line}: Duplicate", ErrorType.SYNTAX);
                    }
                }
            }
            else if (Match(TokenType.IDENTIFIER))
            {
                Variable variable = ParseVariable();
                Token token = Peek();
                Consume(TokenType.COLON, "Expected ','");
                Expression expression = ParseExpression();
                Assignment assignment = new Assignment(variable, token, expression);
                assignments.Add(assignment);
                if (!Check(TokenType.RIGHT_BRACE)) Consume(TokenType.COMMA, "Expected ','");
                else { }
            }


        }
        if (name == null) throw new Error($"'{Peek().lexeme}' in {Peek().line}: No name", ErrorType.SYNTAX);
        Consume(TokenType.RIGHT_BRACE, "Expected '}'");
        return new OnActivationEffect(name, assignments);
    }

    Selector ParseSelector()
    {
        Consume(TokenType.LEFT_BRACE, "Expected '{'");
        string source = null!;
        Single single = null!;
        Predicate predicate = null!;
        while (!Check(TokenType.RIGHT_BRACE) && !isAtEnd())
        {
            if (Match(TokenType.SOURCE))
            {
                Consume(TokenType.COLON, "Expected ':'");
                if (source == null)
                {
                    source = Advance().lexeme;
                }
                else
                {
                    throw new Error($"'{Peek().lexeme}' in {Peek().line}: Duplicate", ErrorType.SYNTAX);
                }
                if (!Check(TokenType.RIGHT_BRACE)) Consume(TokenType.COMMA, "Expected ','");
            }
            else if (Match(TokenType.SINGLE))
            {
                Consume(TokenType.COLON, "Expected ':'");
                if (single == null)
                {
                    Token boolToken = Advance(); // Avanza el token actual y asígnalo a boolToken.
                    single = new Single(boolToken);
                }
                else
                {
                    throw new Error($"'{Peek().lexeme}' in {Peek().line}: Duplicate", ErrorType.SYNTAX);
                }
                if (!Check(TokenType.RIGHT_BRACE)) Consume(TokenType.COMMA, "Expected ','");
            }
            else
            {
                throw new Error($"'{Peek().lexeme}' in {Peek().line}: Inavlid field", ErrorType.SYNTAX);
            }
        }
        Consume(TokenType.RIGHT_BRACE, "Expected '}' after Selector declaration");
        if (source == null! || single == null! || predicate == null!) throw new Error($"'{Peek().lexeme}' in {Peek().line}: Missing field", ErrorType.SYNTAX);
        return new Selector(source, single, predicate);

    }
    PostAction ParsePostAction()
    {
        Consume(TokenType.LEFT_BRACE, "Expected '{' to start post action block");
        Expression type = null!;
        Selector selector = null!;
        if (Match(TokenType.TYPE))
        {
            type = ParseExpression();
            Consume(TokenType.COLON, "Expected ':' after type");
        }
        else
        {
            throw new Error($"Expected 'TYPE' but found '{Peek().lexeme}' at line {Peek().line}", ErrorType.SYNTAX);
        }

        // Luego parsea el selector
        if (Match(TokenType.SELECTOR))
        {
            selector = ParseSelector();
            Consume(TokenType.COLON, "Expected ':' after selector");
        }
        else
        {
            throw new Error($"Expected 'SELECTOR' but found '{Peek().lexeme}' at line {Peek().line}", ErrorType.SYNTAX);
        }

        Consume(TokenType.RIGHT_BRACE, "Expected '}' after post action block");
        return new PostAction(type, selector);
    }

    Predicate ParsePredicate()
    {
        Consume(TokenType.LEFT_BRACE, "Expected '{' to start a predicate");
        Variable variable = null!;
        if (Match(TokenType.IDENTIFIER))
        {
            variable = ParseVariable();
            Consume(TokenType.COLON, "Expected ':' after variable");
        }
        else
        {
            throw new Error($"Expected IDENTIFIER but found '{Peek().lexeme}' at line {Peek().line}", ErrorType.SYNTAX);
        }
        Expression condition = null!;
        if (!isAtEnd() && !Check(TokenType.RIGHT_BRACE))
        {
            condition = ParseExpression();
        }
        else
        {
            throw new Error($"Expected CONDITION but found '{Peek().lexeme}' at line {Peek().line}", ErrorType.SYNTAX);
        }

        Consume(TokenType.RIGHT_BRACE, "Expected '}' after predicate block");

        return new Predicate(variable, condition);
    }


    Action ParseAction()
    {
        Consume(TokenType.LEFT_PAREN, "Expected ')'");
        Variable target = ParseVariable();
        Consume(TokenType.COMMA, "Expected ','");
        Variable context = ParseVariable();
        Consume(TokenType.RIGHT_PAREN, "Expected ')'");
        Consume(TokenType.EQUAL_GREATER, "Expected '=>'");
        Consume(TokenType.LEFT_BRACE, "Expected '{'");
        StatementBlock StatementBlock = new StatementBlock();
        while (!Check(TokenType.RIGHT_BRACE) && !isAtEnd())
        {
            StatementBlock.statements.Add(ParseStatement());
        }
        Consume(TokenType.RIGHT_BRACE, "Expected '}'");
        return new Action(target, context, StatementBlock);
    }

    Statement ParseStatement()
    {
        if (Match(TokenType.WHILE))
        {
            return ParseWhileStatement();
        }
        else if (Match(TokenType.FOR))
        {
            return ParseForStatement();
        }

        else if (Match(TokenType.FUN))
        {
            return ParseFunctionDeclaration();
        }

        else if (Match(TokenType.IDENTIFIER))
        {
            return ParseExpressionOrAssignment();
        }

        else
        {
            return ParseExpressionStatement();
        }

    }
    Statement ParseWhileStatement()
    {
        Consume(TokenType.LEFT_PAREN, "Expected '(' after 'while'.");
        Expression condition = ParseExpression();
        Consume(TokenType.RIGHT_PAREN, "Expected ')' after condition.");
        Consume(TokenType.LEFT_BRACE, "Expected '{' to start the body of the while loop.");
        StatementBlock body = new StatementBlock();
        while (!Check(TokenType.RIGHT_BRACE) && !isAtEnd())
        {
            body.statements.Add(ParseStatement());
        }
        Consume(TokenType.RIGHT_BRACE, "Expected '}' after the body of the while loop.");
        return new WhileStatement(condition, body);
    }
    Statement ParseForStatement()
    {
        Consume(TokenType.LEFT_PAREN, "Expected '(' after 'for'.");
        Statement initializer = ParseExpressionOrAssignment();
        Expression condition = null!;
        if (!Check(TokenType.SEMICOLON))
        {
            condition = ParseExpression();
        }
        Consume(TokenType.SEMICOLON, "Expected ';' after loop condition.");
        Expression increment = null!;
        if (!Check(TokenType.RIGHT_PAREN))
        {
            increment = ParseExpression();
        }
        Consume(TokenType.RIGHT_PAREN, "Expected ')' after for clauses.");
        Consume(TokenType.LEFT_BRACE, "Expected '{' to start the body of the for loop.");
        StatementBlock body = new StatementBlock();
        while (!Check(TokenType.RIGHT_BRACE) && !isAtEnd())
        {
            body.statements.Add(ParseStatement());
        }
        Consume(TokenType.RIGHT_BRACE, "Expected '}' after the body of the for loop.");

        return new ForStatement(initializer, condition, increment, body);

    }

    Statement ParseFunctionDeclaration()
    {
        Token name = Consume(TokenType.IDENTIFIER, "Expected function name.");
        Consume(TokenType.LEFT_PAREN, "Expected '(' after function name.");

        List<Variable> parameters = GetParams();

        Consume(TokenType.RIGHT_PAREN, "Expected ')' after parameters.");
        Consume(TokenType.LEFT_BRACE, "Expected '{' before function body.");

        List<Statement> body = new List<Statement>();
        while (!Check(TokenType.RIGHT_BRACE) && !isAtEnd())
        {
            body.Add(ParseStatement());
        }

        Consume(TokenType.RIGHT_BRACE, "Expected '}' after function body.");
        return new FunctionStatement(name, parameters, body);
    }

    Statement ParseExpressionOrAssignment()
    {
        Expression expr = ParseExpression();

        if (Match(TokenType.EQUAL))
        {
            Token equals = Previous();
            Expression value = ParseExpression();

            if (expr is VariableExpression variable)
            {
                Token name = variable.name;
                return new Assignment(new Variable(variable.name), equals, value);
            }

            throw new Error($"Invalid assignment target at line {equals.line}.", ErrorType.SYNTAX);
        }

        Consume(TokenType.SEMICOLON, "Expected ';' after expression.");
        return new ExpressionStatement(expr);
    }

    Statement ParseExpressionStatement()
    {
        Expression expr = ParseExpression();
        Consume(TokenType.SEMICOLON, "Expected ';' after expression.");
        return new ExpressionStatement(expr);
    }



    List<Variable> GetParams()
    {
        Consume(TokenType.LEFT_BRACE, "Expected '{' after Params");
        List<Variable> variables = new List<Variable>();
        while (!Check(TokenType.RIGHT_BRACE) && !isAtEnd())
        {
            var variable = ParseVariable();
            Consume(TokenType.COLON, "Expected ':' after parameter");
            if (Match(TokenType.STRINGTYPE) || Match(TokenType.NUMBERTYPE) || Match(TokenType.BOOLTYPE))
            {
                variable.TypeParam(Peek().type);
                Advance();
                variables.Add(variable);
                if (!Check(TokenType.RIGHT_BRACE))
                {
                    Consume(TokenType.COMMA, "Expected ','");
                }
            }
            else
            {
                throw new Exception("Expected type after parameter name");
                //return variables
            }
        }
        Consume(TokenType.RIGHT_BRACE, "Expected '}' after Params declaration");
        return variables;
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