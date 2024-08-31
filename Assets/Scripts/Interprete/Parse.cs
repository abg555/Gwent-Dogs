using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using System.Text;
public class Parser : MonoBehaviour
{
    private List<Token> tokens { get; }
    private int current = 0;
    // private bool isInsideActionBlock = false;

    public Parser(List<Token> tokens)
    {
        this.tokens = tokens;
    }

    private void MostrarError(string errorMessage)
    {
        PlayerPrefs.SetString("ErrorMessage", errorMessage);
        SceneManager.LoadScene("Error", LoadSceneMode.Additive);
        AudioListener[] listeners = FindObjectsOfType<AudioListener>();

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
    public Expression ParseExpression()
    {
        try
        {
            var equal = Equality();

            return equal;
        }
        catch (Error errorM)
        {
            string errorMessage = $"Semantical error:{errorM.Message}";
            MostrarError(errorMessage);
            Debug.Log($"Semantical error:{errorM.Message}");
            throw;
        }
    }
    Assignment ParseAssignment(Variable variable)
    {
        Token opera = Advance();
        Expression expression = ParseExpression();

        Consume(TokenType.SEMICOLON, "Expected ';'");
        return new Assignment(variable, opera, expression);
    }
    public Node Parse()
    {
        Program program = new Program();
        while (!isAtEnd())
        {

            if (Match(TokenType.EFFECT))
            {
                Consume(TokenType.LEFT_BRACE, "Expected '{' after effect");
                program.effects.Add(ParseEffect());
                Consume(TokenType.RIGHT_BRACE, "Expect '}' after effect finished.");

            }
            else if (Match(TokenType.CARD))
            {
                Consume(TokenType.LEFT_BRACE, "Expected '{' after card");
                program.card.Add(ParseCard());
                Consume(TokenType.RIGHT_BRACE, "Expected '}' after card finished");

            }
            else
            {
                string error = $"'{Peek().lexeme}' in {Peek().line} : effect or card expected";
                MostrarError(error);
                throw new Error($"'{Peek().lexeme}' in {Peek().line} : effect or card expected", ErrorType.SYNTAX);
            }
        }
        return program;
    }


    Card ParseCard()
    {
        Card card = new Card();
        int[] counter = new int[6];
        while (!Check(TokenType.RIGHT_BRACE) && !isAtEnd())
        {
            if (Match(TokenType.TYPE))
            {
                counter[0] += 1;
                Consume(TokenType.COLON, "Expected ':' after Type");
                card.Type = new CardType(ParseExpression());
                Consume(TokenType.COMMA, "Expected ',' after expression");

            }
            else if (Match(TokenType.NAME))
            {
                counter[1] += 1;
                Consume(TokenType.COLON, "Expected ':' after Name");
                card.Name = new Name(ParseExpression());
                Consume(TokenType.COMMA, "Expected ',' after expression");

            }
            else if (Match(TokenType.FACTION))
            {
                counter[2] += 1;
                Consume(TokenType.COLON, "Expected ':' after faction");
                card.Faction = new Faction(ParseExpression());
                Consume(TokenType.COMMA, "Expected ',' after expression");

            }
            else if (Match(TokenType.POWER))
            {
                counter[3] += 1;
                Consume(TokenType.COLON, "Expected ':' after power");
                card.Power = new Power(ParseExpression());
                Consume(TokenType.COMMA, "Expected ',' after expression");

            }
            else if (Match(TokenType.RANGE))
            {
                counter[4] += 1;
                Consume(TokenType.COLON, "Expected ':' after range");
                Consume(TokenType.LEFT_BRACKET, "Expected '['");
                List<Expression> expressions = new List<Expression>();
                for (int i = 0; i < 3; i++)
                {
                    expressions.Add(ParseExpression());
                    if (Match(TokenType.COMMA)) continue;
                    else break;
                }
                Consume(TokenType.RIGHT_BRACKET, "Expect ']'");
                Consume(TokenType.COMMA, "Expected ',' after range");
                card.Range = new Range(expressions.ToArray());

            }
            else if (Match(TokenType.ONACTIVATION))
            {
                counter[5] += 1;
                card.OnActivation = ParseOnActivation();

            }

            else
            {
                string error = $"'{Peek().lexeme}' in {Peek().line} : Invalid card property";
                MostrarError(error);
                throw new Error($"'{Peek().lexeme}' in {Peek().line} : Invalid card property", ErrorType.SYNTAX);
            }
        }

        if (counter[0] < 1)
        {
            string error = $"A Type property is missing from card";
            MostrarError(error);
            throw new Error("A Type property is missing from card", ErrorType.SYNTAX);
        }
        else if (counter[0] > 1)
        {
            string error = $"Only one Type is allowed";
            MostrarError(error);
            throw new Error("Only one Type is allowed", ErrorType.SYNTAX);
        }
        if (counter[1] < 1)
        {
            string error = $"A Name property is missing from card";
            MostrarError(error);
            throw new Error("A Name property is missing from card", ErrorType.SYNTAX);
        }
        else if (counter[1] > 1)
        {
            string error = $"Only one Name is allowed";
            MostrarError(error);
            throw new Error("Only one Name is allowed", ErrorType.SYNTAX);
        }
        if (counter[2] < 1)
        {
            string error = $"A Faction property is missing from card";
            MostrarError(error);
            throw new Error("A Faction property is missing from card", ErrorType.SYNTAX);
        }
        else if (counter[2] > 1)
        {
            string error = $"Only one Faction is allowed";
            MostrarError(error);
            throw new Error("Only one Faction is allowed", ErrorType.SYNTAX);
        }
        if (counter[3] < 1)
        {
            string error = $"A Power property is missing from card";
            MostrarError(error);
            throw new Error("A Power property is missing from card", ErrorType.SYNTAX);
        }
        else if (counter[3] > 1)
        {
            string error = $"Only one Power is allowed";
            MostrarError(error);
            throw new Error("Only one Power is allowed", ErrorType.SYNTAX);
        }
        if (counter[4] < 1)
        {
            string error = $"A Range property is missing from card";
            MostrarError(error);
            throw new Error("A Range property is missing from card", ErrorType.SYNTAX);
        }
        else if (counter[4] > 1)
        {
            string error = $"Only one Range is allowed";
            MostrarError(error);
            throw new Error("Only one Range is allowed", ErrorType.SYNTAX);
        }
        if (counter[5] < 1)
        {
            string error = $"A OnActivation property is missing from card";
            MostrarError(error);
            throw new Error("A OnActivation property is missing from card", ErrorType.SYNTAX);
        }
        else if (counter[5] > 1)
        {
            string error = $"Only one OnActivation is allowed";
            MostrarError(error);
            throw new Error("Only one OnActivation is allowed", ErrorType.SYNTAX);
        }
        return card;
    }
    Effect ParseEffect()
    {
        Effect effect = new Effect();
        int[] counter = new int[3];
        while (!Check(TokenType.RIGHT_BRACE) && !isAtEnd())
        {

            if (Match(TokenType.NAME))
            {
                counter[0] += 1;
                Consume(TokenType.COLON, "Expected ':' after Name");
                effect.Name = new Name(ParseExpression());
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

                counter[2] += 1;
                Consume(TokenType.COLON, "Expected ':' after action");
                effect.Action = ParseAction();


            }

            else
            {
                string message = $"'{Peek().lexeme}' in {Peek().line} : Invalid effect property";
                MostrarError(message);
                throw new Error($"'{Peek().lexeme}' in {Peek().line} : Invalid effect property", ErrorType.SYNTAX);
            }
        }


        if (counter[0] < 1)
        {
            string message = "A Name property is missing from effect";
            MostrarError(message);
            throw new Error("A Name property is missing from effect", ErrorType.SYNTAX);
        }
        else if (counter[0] > 1)
        {
            string message = "Only one Name is allowed";
            MostrarError(message);
            throw new Error("Only one Name is allowed", ErrorType.SYNTAX);
        }

        else if (counter[1] > 1)
        {
            string message = "Only one Params is allowed";
            MostrarError(message);
            throw new Error("Only one Params is allowed", ErrorType.SYNTAX);
        }
        if (counter[2] < 1)
        {
            string message = "A Action property is missing from effect";
            MostrarError(message);
            throw new Error("A Action property is missing from effect", ErrorType.SYNTAX);
        }
        else if (counter[2] > 1)
        {
            string message = "Only one Action is allowed";
            MostrarError(message);
            throw new Error("Only one Action is allowed", ErrorType.SYNTAX);
        }
        return effect;
    }
    OnActivation ParseOnActivation()
    {
        Consume(TokenType.COLON, "Expected ':' after OnACtivation");
        Consume(TokenType.LEFT_BRACKET, "Expected '['");
        OnActivation onActivation = new OnActivation();
        while (!Check(TokenType.RIGHT_BRACKET) && !isAtEnd())
        {
            onActivation.Elements.Add(ParseOnActivationElements());
            if (!Check(TokenType.RIGHT_BRACKET) && !isAtEnd()) Consume(TokenType.COMMA, "Expected ',' after OnActivation element");
        }
        Consume(TokenType.RIGHT_BRACKET, "Expected ']'");
        return onActivation;
    }
    Params ParseParams()
    {
        Consume(TokenType.LEFT_BRACE, "Expected '{' after Params");
        Params param = new Params();
        while (!Check(TokenType.RIGHT_BRACE) && !isAtEnd())
        {
            Variable variables = ParseVariable();
            Consume(TokenType.COLON, "Expected ':' after parameter");                               //verificar
            if (Check(TokenType.NUMBERTYPE) || Check(TokenType.STRINGTYPE) || Check(TokenType.BOOLTYPE))
            {
                variables.SetType(Advance().type);
                param.nodes.Add(variables);
                if (!Check(TokenType.RIGHT_BRACE))
                {
                    Consume(TokenType.COMMA, "Expected ','");
                }
            }
            else
            {
                String error = $"Expected type but got '{Peek().lexeme}' at line {Peek().line}";
                MostrarError(error);
                throw new Error($"Expected type but got '{Peek().lexeme}' at line {Peek().line}", ErrorType.SYNTAX); // Verificar
            }

        }
        Consume(TokenType.RIGHT_BRACE, "Expected '}' after Params declaration");
        Consume(TokenType.COMMA, "Expected ',' after '}'");
        return param;

    }
    Variable ParseVariable()
    {

        Variable variable = new Variable(Advance());
        if (Check(TokenType.DOT))
        {
            VariableCompound variableComp = new VariableCompound(variable.ID);
            while (Match(TokenType.DOT) && !isAtEnd())
            {
                if (Match(TokenType.FUN))
                {
                    Function function = ParseFunctionDeclaration(Previous().lexeme);
                    variableComp.argument.nodes.Add(function);
                }
                else
                {

                    if (Match(TokenType.TYPE))
                    {
                        CardType type = new CardType(new StringExpression(Previous().lexeme));
                        variableComp.argument.nodes.Add(type);
                    }
                    else if (Match(TokenType.NAME))
                    {
                        Name name = new Name(new StringExpression(Previous().lexeme));
                        variableComp.argument.nodes.Add(name);
                    }
                    else if (Match(TokenType.FACTION))
                    {
                        Faction faction = new Faction(new StringExpression(Previous().lexeme));
                        variableComp.argument.nodes.Add(faction);
                    }
                    else if (Match(TokenType.POWER))
                    {
                        Pow power = new Pow();
                        variableComp.argument.nodes.Add(power);

                    }
                    else if (Match(TokenType.RANGE))
                    {
                        Range range = new Range(Previous().lexeme);
                        variableComp.argument.nodes.Add(range);
                    }
                    else if (Match(TokenType.POINTER))
                    {
                        Pointer pointer = new Pointer(Previous().lexeme);
                        variableComp.argument.nodes.Add(pointer);
                        if (Match(TokenType.LEFT_BRACKET))
                        {
                            Inde inde = new Inde(Convert.ToInt32(Advance().Literal));
                            Consume(TokenType.RIGHT_BRACKET, "Expected ']' after Pointer");
                            variableComp.argument.nodes.Add(inde);
                        }
                    }
                    else if (Match(TokenType.OWNER))
                    {
                        Owner owner = new Owner(Previous().Literal as string);
                        variableComp.argument.nodes.Add(owner);
                    }
                    else
                    {

                    }
                }
            }
            variable = variableComp;
        }
        return variable;
    }
    OnActivationElements ParseOnActivationElements()
    {
        Consume(TokenType.LEFT_BRACE, "Expected '{'");
        OnActivationEffect onActivationEffect = null;
        Selector selector = null;
        List<PostAction> postAction = new List<PostAction>();
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
                    if (selector.Source == null) throw new Error($"'{Peek().lexeme}' in {Peek().line}: Invalid Selector field.", ErrorType.SYNTAX);

                }
                else { }
            }
            else if (Match(TokenType.POSTACTION))
            {

                Consume(TokenType.COLON, "Expected ':'");
                postAction.Add(ParsePostAction());
                foreach (var uuu in postAction)
                {
                    Debug.LogError("xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx");
                }


            }
            else
            {
                string error = $"'{Peek().lexeme}' in {Peek().line}: Invalid OnActivation field.";
                MostrarError(error);
                throw new Error($"'{Peek().lexeme}' in {Peek().line}: Invalid OnActivation field.", ErrorType.SYNTAX);
            }
        }
        Consume(TokenType.RIGHT_BRACE, "Expected '}' after OnActivation declaration");
        return new OnActivationElements(onActivationEffect, selector, postAction);
    }
    OnActivationEffect ParseOnActivationEffect()
    {
        string value = null;
        List<Assignment> assi = new List<Assignment>();
        if (Check(TokenType.STRING))
        {
            value = Advance().lexeme;
            if (!Check(TokenType.RIGHT_BRACE)) Consume(TokenType.COMMA, "Expected ','");
            while (!Check(TokenType.SELECTOR) && !Check(TokenType.RIGHT_BRACE))
            {
                if (Check(TokenType.IDENTIFIER))
                {
                    Variable variable = ParseVariable();
                    Token token = Peek();
                    Consume(TokenType.COLON, "Expected ':");
                    Expression expre = ParseExpression();
                    Assignment assig = new Assignment(variable, token, expre);
                    assi.Add(assig);
                    Consume(TokenType.COMMA, "Expected ',");
                }
                else { }
            }
        }
        else
        {
            Consume(TokenType.LEFT_BRACE, "Expected '{'");
            while (!Check(TokenType.RIGHT_BRACE) && !isAtEnd())
            {
                if (Match(TokenType.NAME))
                {
                    if (Match(TokenType.COLON))
                    {
                        if (value == null)
                        {
                            value = Advance().lexeme;
                            if (!Check(TokenType.RIGHT_BRACE)) Consume(TokenType.COMMA, "Expected ','");
                        }
                        else
                        {
                            string error = $"'{Peek().lexeme}' in {Peek().line}: Duplicate";
                            MostrarError(error);
                            throw new Error($"'{Peek().lexeme}' in {Peek().line}: Duplicate", ErrorType.SYNTAX);
                        }
                    }
                    else
                    {
                        if (value == null)
                        {
                            value = Advance().lexeme;
                            if (!Check(TokenType.RIGHT_BRACE)) Consume(TokenType.COMMA, "Expected ','");
                        }
                        else
                        {
                            string error = $"'{Peek().lexeme}' in {Peek().line}: Duplicate";
                            MostrarError(error);
                            throw new Error($"'{Peek().lexeme}' in {Peek().line}: Duplicate", ErrorType.SYNTAX);
                        }
                    }
                }
                else if (Check(TokenType.IDENTIFIER))
                {
                    Variable variable = ParseVariable();
                    Token token = Peek();
                    Consume(TokenType.COLON, "Expected ':'");
                    Expression expression = ParseExpression();
                    Assignment assignment = new Assignment(variable, token, expression);
                    assi.Add(assignment);
                    if (!Check(TokenType.RIGHT_BRACE)) Consume(TokenType.COMMA, "Expected ','");
                }
                else
                {

                }
            }
            Consume(TokenType.RIGHT_BRACE, "Expected '}'");
            Consume(TokenType.COMMA, "Expected ','");
        }
        if (value == null) throw new Error($"'{Peek().lexeme}' in {Peek().line}: No value", ErrorType.SYNTAX);
        return new OnActivationEffect(value, assi);
    }
    Selector ParseSelector()
    {
        Consume(TokenType.LEFT_BRACE, "Expected '{'");
        string source = null;
        Single single = null;
        Predicate predicate = null;
        while (!Check(TokenType.RIGHT_BRACE) && !isAtEnd())
        {
            if (Match(TokenType.SOURCE))
            {
                Consume(TokenType.COLON, "Expected ':'");
                if (source == null)
                {
                    if (Convert.ToString(Peek().Literal) == "deck" || Convert.ToString(Peek().Literal) == "otherDeck" || Convert.ToString(Peek().Literal) == "hand" || Convert.ToString(Peek().Literal) == "otherHand" || Convert.ToString(Peek().Literal) == "field" || Convert.ToString(Peek().Literal) == "otherField" || Convert.ToString(Peek().Literal) == "parent" || Convert.ToString(Peek().Literal) == "board")
                    {
                        source = Advance().lexeme;
                    }
                    else
                    {
                        throw new Error("", ErrorType.SYNTAX);
                    }
                }
                else
                {
                    string error = $"'{Peek().lexeme}' in {Peek().line}: Duplicate";
                    MostrarError(error);
                    throw new Error($"'{Peek().lexeme}' in {Peek().line}: Duplicate", ErrorType.SYNTAX);
                }
                if (!Check(TokenType.RIGHT_BRACE)) Consume(TokenType.COMMA, "Expected ','");
            }
            else if (Match(TokenType.SINGLE))
            {
                Consume(TokenType.COLON, "Expected ':'");
                if (single == null)
                {

                    single = new Single(Advance());
                }
                else
                {
                    string error = $"'{Peek().lexeme}' in {Peek().line}: Duplicate";
                    MostrarError(error);
                    throw new Error($"'{Peek().lexeme}' in {Peek().line}: Duplicate", ErrorType.SYNTAX);
                }
                if (!Check(TokenType.RIGHT_BRACE)) Consume(TokenType.COMMA, "Expected ','");
            }
            else if (Match(TokenType.PREDICATE))
            {
                Consume(TokenType.COLON, "Expected ':'");
                if (predicate == null)
                {
                    predicate = ParsePredicate();
                }
                else
                {
                    string error = $"'{Peek().lexeme}' in {Peek().line}: Duplicate";
                    MostrarError(error);
                    throw new Error($"'{Peek().lexeme}' in {Peek().line}: Duplicate", ErrorType.SYNTAX);
                }
                if (!Check(TokenType.RIGHT_BRACE)) Consume(TokenType.COMMA, "Expected ','");
            }
            else
            {
                string error = $"'{Peek().lexeme}' in {Peek().line}: Inavlid field";
                MostrarError(error);
                throw new Error($"'{Peek().lexeme}' in {Peek().line}: Inavlid field", ErrorType.SYNTAX);
            }
        }
        Consume(TokenType.RIGHT_BRACE, "Expected '}' after Selector declaration");
        Consume(TokenType.COMMA, "Expected ','");
        if (single == null! || predicate == null!) throw new Error($"'{Peek().lexeme}' in {Peek().line}: Missing field", ErrorType.SYNTAX);
        return new Selector(source, single, predicate);

    }
    PostAction ParsePostAction()
    {
        Debug.LogError("UYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYYY");
        Consume(TokenType.LEFT_BRACE, "Expected '{' to start post action block");
        Expression type = null;
        Selector selector = null;
        List<Assignment> assi = new List<Assignment>();
        while (!Check(TokenType.RIGHT_BRACE) && !isAtEnd())
        {
            if (Match(TokenType.TYPE))
            {
                Consume(TokenType.COLON, "Expected ':'");
                type = ParseExpression();
                if (!Check(TokenType.RIGHT_BRACE)) Consume(TokenType.COMMA, "Expected ','");
            }
            else if (Match(TokenType.SELECTOR))
            {
                Consume(TokenType.COLON, "Expected ':'");
                selector = ParseSelector();
                if (selector.Source == null)
                {
                    selector.Source = "parent";
                }
                if (!Check(TokenType.RIGHT_BRACE)) Consume(TokenType.COMMA, "Expected ','");

            }
            else if (Check(TokenType.IDENTIFIER))
            {
                Variable var = ParseVariable();
                Token token = Peek();
                Consume(TokenType.COLON, "Expected ':'");
                Expression expression = ParseExpression();
                Assignment ass = new Assignment(var, token, expression);
                assi.Add(ass);
                if (!Check(TokenType.RIGHT_BRACE)) Consume(TokenType.COMMA, "Expected ','");
            }
            else
            {
                string error = $"Expected 'SELECTOR' but found '{Peek().lexeme}' at line {Peek().line}";
                MostrarError(error);
                throw new Error($"Expected 'SELECTOR' but found '{Peek().lexeme}' at line {Peek().line}", ErrorType.SYNTAX);
            }
        }

        Consume(TokenType.RIGHT_BRACE, "Expected '}' after post action block");
        if (type == null || selector == null) throw new Error("", ErrorType.SYNTAX);
        return new PostAction(type, selector);
    }
    Predicate ParsePredicate()
    {
        Consume(TokenType.LEFT_PAREN, "Expected '(' to start a predicate");
        Variable variable = ParseVariable();
        Consume(TokenType.RIGHT_PAREN, "Expected ')' after variable");
        Consume(TokenType.EQUAL_GREATER, "Expected '=>' afte ')");
        Expression condition = ParseExpression();
        return new Predicate(variable, condition);
    }
    Action ParseAction()
    {
        Consume(TokenType.LEFT_PAREN, "Expected '(' after Action");
        Variable targets = ParseVariable();
        Consume(TokenType.COMMA, "Expected ',' between parameters");
        Variable context = ParseVariable();
        Consume(TokenType.RIGHT_PAREN, "Expected ')' after parameters");
        Consume(TokenType.EQUAL_GREATER, "Expected '=>' after parameters");
        Consume(TokenType.LEFT_BRACE, "Expected '{' before action body");
        //isInsideActionBlock = true;
        StatementBlock body = ParseStatementBlock();
        //isInsideActionBlock = false;
        Consume(TokenType.RIGHT_BRACE, "Expected '}' after action body");
        return new Action(targets, context, body);
    }
    StatementBlock ParseStatementBlock()
    {
        StatementBlock block = new StatementBlock();
        while (!Check(TokenType.RIGHT_BRACE) && !isAtEnd())
        {
            block.statements.Add(ParseStatement());
        }
        return block;


    }
    Statement ParseStatement()
    {
        if (Match(TokenType.FOR))
        {
            return ParseForStatement();
        }
        else if (Match(TokenType.WHILE))
        {
            return ParseWhileStatement();
        }

        else if (Check(TokenType.IDENTIFIER))
        {
            Variable variable = ParseVariable();
            if (variable is VariableCompound && Check(TokenType.SEMICOLON))
            {
                VariableCompound v = variable as VariableCompound;
                if (v.argument.nodes[v.argument.nodes.Count - 1].GetType() == typeof(Function))
                {
                    Function function = v.argument.nodes[v.argument.nodes.Count - 1] as Function;
                    //if (function.type != Variable.Type.VOID) throw new Error("", ErrorType.SYNTAX);
                }
                else
                {

                }
                Consume(TokenType.SEMICOLON, "Expected ';'");
                return variable as VariableCompound;
            }
            else
            {
                return ParseAssignment(variable);
            }
        }
        else if (Check(TokenType.FUN))
        {
            return ParseFunctionDeclaration(Previous().lexeme);
        }
        else
        {
            throw new Error("", ErrorType.SYNTAX);
        }

    }
    Statement ParseWhileStatement()
    {
        Consume(TokenType.LEFT_PAREN, "Expected '('");
        Expression expression = ParseExpression();
        Consume(TokenType.RIGHT_PAREN, "Expected ')'");
        StatementBlock statement = ParseStatementBlock();
        return new WhileStatement(expression, statement);
    }
    Statement ParseForStatement()
    {

        Variable target1 = ParseVariable();
        Consume(TokenType.IN, "Expected 'in'");
        Variable target2 = ParseVariable();
        Consume(TokenType.LEFT_BRACE, "Expected {");
        StatementBlock statement = ParseStatementBlock();
        Consume(TokenType.RIGHT_BRACE, "Expected }");
        Consume(TokenType.SEMICOLON, "Expected ';'");
        return new ForStatement(target1, target2, statement);



    }
    Function ParseFunctionDeclaration(string value)
    {
        Consume(TokenType.LEFT_PAREN, "Expected '('");
        Params param = new Params();
        while (!Check(TokenType.RIGHT_PAREN) && !isAtEnd())
        {
            if (Check(TokenType.IDENTIFIER))
            {
                param.nodes.Add(ParseVariable());
            }
            else if (Match(TokenType.EQUAL_GREATER))
            {
                Predicate predicate = new Predicate(param.nodes[param.nodes.Count - 1] as Variable, ParseExpression());
                param.nodes.RemoveAt(param.nodes.Count - 1);
                param.nodes.Add(predicate);
            }
            else if (Check(TokenType.FUN))
            {
                param.nodes.Add(ParseFunctionDeclaration(Advance().lexeme));
            }
            else
            {
                param.nodes.Add(ParseExpression());
            }
            if (!Check(TokenType.RIGHT_PAREN)) Consume(TokenType.COMMA, "Expected ','");
        }
        Consume(TokenType.RIGHT_PAREN, "Expected ')'");
        Function function = new Function(value, param);
        return function;
    }
    Expression Equality()
    {
        Expression expression = Comparison();
        while (Match(TokenType.BANG_EQUAL) || Match(TokenType.EQUAL_EQUAL))
        {
            Token ope = Previous();
            Expression right = Comparison();
            expression = new BinaryBoolean(expression, ope, right);
        }
        return expression;
    }
    bool Match(TokenType[] type)
    {
        for (int i = 0; i < type.Length; i++)
        {
            if (Check(type[i]))
            {
                Advance();
                return true;
            }
        }
        return false;
    }
    bool Match(TokenType type)
    {
        if (Check(type))
        {
            Advance();
            return true;
        }
        return false;
    }
    bool Check(TokenType type)
    {
        if (isAtEnd()) return false;
        return Peek().type == type;
    }
    Token Advance()
    {
        if (!isAtEnd()) current++;
        return Previous();
    }
    bool isAtEnd()
    {
        return Peek().type == TokenType.EOF;
    }
    Token Peek()
    {
        return tokens[current];
    }
    Token Previous()
    {
        return tokens[current - 1];
    }
    bool Continue(TokenType type)
    {
        if (isAtEnd()) return false;
        return tokens[current + 1].type == type;
    }
    Expression Comparison()
    {
        Expression expression = Term();
        while (Match(TokenType.GREATER) || Match(TokenType.GREATER_EQUAL) || Match(TokenType.LESS) || Match(TokenType.LESS_EQUAL))
        {
            Token ope = Previous();
            Expression right = Term();
            expression = new BinaryBoolean(expression, ope, right);
        }
        return expression;
    }
    Expression Term()
    {
        // Expression expression = Factor();
        // while (Match(TokenType.MINUS) || Match(TokenType.PLUS) || Match(TokenType.ATSIGN) || Match(TokenType.ATSIGN_ATSIGN))
        // {
        //     Token ope = Previous();
        //     Expression right = Factor();
        //     expression = new Binary(expression, ope, right);
        // }
        // return expression;
        Expression expression = Factor();
        TokenType[] tokenTypes = { TokenType.PLUS, TokenType.MINUS };
        if (Check(TokenType.PLUS) || Check(TokenType.MINUS))
        {
            while (Match(tokenTypes))
            {
                Token operators = Previous();
                Expression right = Factor();
                expression = new BinaryInterger(expression, operators, right);
            }
        }
        else if (Check(TokenType.ATSIGN) || Check(TokenType.ATSIGN_ATSIGN))
        {
            while (Match(TokenType.ATSIGN) || Match(TokenType.ATSIGN_ATSIGN))
            {
                Token operators = Previous();
                Expression right = Factor();
                expression = new BinaryString(expression, operators, right);
            }
        }
        return expression;

    }
    Expression Factor()
    {
        Expression expression = Unary();
        while (Match(TokenType.SLASH) || Match(TokenType.STAR) || Match(TokenType.PERCENT))
        {
            Token oper = Previous();
            Expression right = Unary();
            expression = new BinaryInterger(expression, oper, right);
        }
        return expression;
    }
    Expression Unary()
    {
        if (Match(TokenType.MINUS) || Match(TokenType.BANG) || Match(TokenType.PLUS_PLUS))
        {
            Token ope = Previous();
            Expression right = Unary();
            return new Unary(ope, right);
        }
        else if (Check(TokenType.IDENTIFIER) && Continue(TokenType.PLUS_PLUS))
        {
            Expression left = ParseVariable();
            Token token = Advance();
            return new Unary(token, left);
        }
        return Primary();
    }
    Expression Primary()
    {
        //Debug.Log($"Entering Primary. Current token: {Peek().type} with lexeme: {Peek().lexeme}");

        if (Match(TokenType.FALSE)) return new Bool(false);
        if (Match(TokenType.TRUE)) return new Bool(true);
        if (Match(TokenType.NUMBER)) return new Number(Convert.ToInt32(Previous().Literal));
        if (Match(TokenType.STRING))
        {
            Debug.Log(Previous().lexeme);
            return new StringExpression(Previous().lexeme);
        }

        if (Match(TokenType.LEFT_PAREN))
        {
            Expression expression = Equality();
            Consume(TokenType.RIGHT_PAREN, "Expect ')' after expression.");
            return new Grouping(expression);
        }

        if (Check(TokenType.IDENTIFIER)) //Tenias puesto Match y era check
        {
            return ParseVariable();
        }

        string error = $"Unexpected token: {Peek().type} with lexeme: {Peek().lexeme}";
        MostrarError(error);
        throw new Error($"Unexpected token: {Peek().type} with lexeme: {Peek().lexeme}", ErrorType.SYNTAX);
    }
    Token Consume(TokenType type, string message)
    {
        Debug.Log(Peek().type + " " + Peek().lexeme);
        if (Check(type)) return Advance();
        string error = $"{message}. Expected token of type '{type}' but got '{Peek().type}' at line {Peek().line}";
        MostrarError(error);
        throw new Exception($"{message}. Expected token of type '{type}' but got '{Peek().type}' at line {Peek().line}");

    }
}
