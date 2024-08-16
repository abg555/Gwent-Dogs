public class Semantic
{
    private Dictionary<string, Variable> variables = new Dictionary<string, Variable>();
    private Stack<Dictionary<string, Variable>> scopes = new Stack<Dictionary<string, Variable>>();
    private Dictionary<Expression, Effect> effects = new Dictionary<Expression, Effect>();
    private Dictionary<string, Effect> effectNames = new Dictionary<string, Effect>();
    private Dictionary<string, Card> cardNames = new Dictionary<string, Card>();

    private Effect currentEffect;


    public Variable GetVariable(string name)
    {
        if (!variables.TryGetValue(name, out var variable))
        {
            throw new SemanticError($"Variable '{name}' no está definida.");
        }
        return variable;
    }
    public void DefineEffect(Effect effect)
    {
        if (effects.ContainsKey(effect.Name.name))
        {
            throw new SemanticError($"Efecto '{effect.Name}' ya está definido.");
        }
        effects[effect.Name.name] = effect;
    }
    public Effect GetEffect(Expression name)
    {
        if (!effects.TryGetValue(name, out var effect))
        {
            throw new SemanticError($"Efecto '{name}' no está definido.");
        }
        return effect;
    }
    public void CheckEffect(Effect effect)
    {
        currentEffect = effect;
        PushScope();
        if (effect.Params != null)
        {
            foreach (var param in effect.Params.nodes)
            {
                if (param is Variable variable)
                {
                    DefineVariable(variable);
                }
            }
        }

        CheckAction(effect.Action);

        PopScope();
        currentEffect = null;
    }
    public void CheckDeclaration(Variable variable, Expression value)
    {
        DefineVariable(variable);
        CheckExpression(value);
    }
    public void CheckCard(Card card)
    {
        string cardName = ((StringExpression)card.Name.name).Value;
        if (card.OnActivation != null)
        {
            foreach (var element in card.OnActivation.Elements)
            {
                if (element.oae != null)
                {
                    string effectName = element.oae.name;
                    if (!effectNames.ContainsKey(effectName))
                    {
                        throw new SemanticError($"El efecto '{effectName}' mencionado en la carta '{cardName}' no está definido.");
                    }
                    Effect declaredEffect = effectNames[effectName];
                    foreach (var param in element.oae.Params)
                    {
                        var declaredParam = declaredEffect.Params.nodes.FirstOrDefault(p => (p as Variable)?.name == param.Left.name) as Variable;
                        if (declaredParam == null)
                        {
                            throw new SemanticError($"El parámetro '{param.Left.name}' no está definido en el efecto '{effectName}'.");
                        }

                        // Verificar el tipo del parámetro
                        if (param.Right is Bool && declaredParam.type != Variable.Type.BOOL)
                        {
                            throw new SemanticError($"El parámetro '{param.Left.name}' se le asignó el tipo incorrecto.");
                        }
                        else if (param.Right is Number && declaredParam.type != Variable.Type.INT)
                        {
                            throw new SemanticError($"El parámetro '{param.Left.name}' se le asignó el tipo incorrecto..");
                        }
                        else if (param.Right is StringExpression && declaredParam.type != Variable.Type.STRING)
                        {
                            throw new SemanticError($"El parámetro '{param.Left.name}' se le asignó el tipo incorrecto..");
                        }
                    }
                }
            }
        }
    }
    public void CheckAction(Action action)
    {
        PushScope();

        DefineVariable(new Variable(new Token(TokenType.IDENTIFIER, "targets", "targets", 0, 0)));
        DefineVariable(new Variable(new Token(TokenType.IDENTIFIER, "context", "context", 0, 0)));

        foreach (var statement in action.Block.statements)
        {
            if (statement is Assignment assignment)
            {
                CheckAssignment(assignment);
            }
            else if (statement is ForStatement forStmt)
            {
                CheckForStatement(forStmt);
            }
        }

        PopScope();
    }
    private void CheckForStatement(ForStatement forStmt)
    {
        PushScope();
        DefineVariable(forStmt.Variable);
        CheckVariable(forStmt.Target);
        CheckStatementBlock(forStmt.Body);
        PopScope();
    }
    public void CheckVariable(Variable variable)
    {
        if (!IsVariableDeclared(variable.name))
        {
            throw new SemanticError($"Variable '{variable.name}' is not declared.");
        }

        if (variable is VariableCompound compoundVar)
        {
            foreach (var node in compoundVar.argument.nodes)
            {
                if (node is Function function)
                {
                    CheckFunctionCall(function);
                }
                else if (node is Expression expression)
                {
                    CheckExpression(expression);
                }
            }
        }
    }

    private void CheckStatementBlock(StatementBlock block)
    {
        foreach (var statement in block.statements)
        {
            if (statement is Assignment assignment)
            {
                CheckAssignment(assignment);
            }
            else if (statement is ForStatement forStmt)
            {
                CheckForStatement(forStmt);
            }
            else if (statement is VariableCompound variableCompound)
            {
                // Verificar si se está accediendo a una propiedad sin asignación
                string propertyName = variableCompound.argument.nodes.Last().ToString().ToLower();
                if (propertyName == "power" || propertyName == "pow" || propertyName == "name" || propertyName == "type" || propertyName == "faction" || propertyName == "range")
                {
                    throw new SemanticError($"Se está accediendo a la propiedad {propertyName} sin asignarle un valor.");
                }
            }
        }
    }

    private bool IsPowerAssignment(Assignment assignment)
    {
        if (assignment.Left is VariableCompound variableCompound)
        {
            string propertyName = variableCompound.argument.nodes.Last().ToString().ToLower();
            return propertyName == "power";
        }
        return false;
    }

    private void CheckFunctionCall(Function function)
    {
        foreach (var param in function.Params.nodes)
        {
            if (param is Expression expression)
            {
                CheckExpression(expression);
            }
            else if (param is Variable variable)
            {
                CheckVariable(variable);
            }
        }
    }

    public void CheckNode(Node ast)
    {
        if (ast is Program program)
        {
            // Primero, procesar todos los efectos
            foreach (Effect effect in program.effects)
            {
                string effectName = ((StringExpression)effect.Name.name).Value;
                if (effectNames.ContainsKey(effectName))
                {
                    throw new SemanticError($"El efecto '{effectName}' ya está definido.");
                }
                effectNames[effectName] = effect;
                CheckEffect(effect);
            }

            // Luego, procesar todas las cartas
            foreach (Card card in program.card)
            {
                string cardName = ((StringExpression)card.Name.name).Value;
                if (cardNames.ContainsKey(cardName))
                {
                    throw new SemanticError($"La carta '{cardName}' ya está definida.");
                }
                cardNames[cardName] = card;
                CheckCard(card);
            }
        }
    }

    private void CheckAssignment(Assignment assignment)
    {
        if (assignment.Left is VariableCompound variableCompound)
        {
            string propertyName = variableCompound.argument.nodes.Last().ToString().ToLower();

            switch (propertyName)
            {
                case "power":
                case "pow":
                    if (assignment.Right is Variable rightVar)
                    {
                        if (!IsVariableDeclaredInParams(rightVar.name))
                        {
                            throw new SemanticError($"La variable '{rightVar.name}' no está definida en los parámetros del efecto.");
                        }

                    }
                    else if (!(assignment.Right is Number))
                    {
                        throw new SemanticError("La propiedad Power debe ser asignada con un número o una variable de tipo Number definida en los parámetros.");
                    }
                    break;
                case "type":
                case "name":
                case "faction":
                    if (assignment.Right is Variable rightVa)
                    {
                        if (!IsVariableDeclaredInParams(rightVa.name))
                        {
                            throw new SemanticError($"La variable '{rightVa.name}' no está definida en los parámetros del efecto.");
                        }

                    }
                    else if (!(assignment.Right is StringExpression))
                    {
                        throw new SemanticError("La propiedad Name debe ser asignada con un string o una variable de tipo String.");
                    }
                    break;
                default:
                    throw new SemanticError($"Asignación no permitida a la propiedad {propertyName}.");
            }
        }
    }
    private bool IsVariableDeclaredInParams(string variableName)
    {
        if (currentEffect == null || currentEffect.Params == null)
        {
            return false;
        }

        return currentEffect.Params.nodes.Any(node => node is Variable v && v.name == variableName);
    }
    private bool IsNumberTypeVariable(Expression expr)
    {
        if (expr is Variable variable)
        {
            return variable.type == Variable.Type.INT || variable.type == Variable.Type.NULL;
        }
        return false;
    }






    private bool IsStringTypeVariable(Expression expr)
    {
        return expr is Variable variable && variable.type == Variable.Type.STRING;
    }

    public void CheckEffectCall(Effect effectCall)
    {
        if (effects.ContainsKey(effectCall.Name.name))
        {
            throw new SemanticError($"Efecto '{effectCall.Name}' no está definido.");
        }
        // Verifica que los argumentos coincidan con los parámetros del efecto...
    }

    public void CheckStatement(Statement statement)
    {
        // if (statement is ForStatement forStmt)
        // {
        //     PushScope();
        //     DefineVariable(forStmt.Variable);
        //     CheckVariable(forStmt.Target);
        //     CheckStatementBlock(forStmt.Body);
        //     PopScope();
        // }
        // else if (statement is Assignment assignment)
        // {
        //     CheckAssignment(assignment);
        // }
        if (statement is Assignment assignment)
        {
            if (!IsVariableDeclared(assignment.Left.name))
            {
                CheckDeclaration(assignment.Left, assignment.Right);
            }
            else
            {
                CheckAssignment(assignment);
            }
        }
        else if (statement is ForStatement forStmt)
        {
            PushScope();
            DefineVariable(forStmt.Variable);
            CheckVariable(forStmt.Target);
            CheckStatementBlock(forStmt.Body);
            PopScope();
        }
    }


    private void PushScope()
    {
        scopes.Push(new Dictionary<string, Variable>());
    }

    private void PopScope()
    {
        scopes.Pop();
    }

    public void DefineVariable(Variable variable, bool isConstant = false)
    {
        if (scopes.Count == 0)
        {
            PushScope();
        }
        variable.isConstant = isConstant;
        scopes.Peek()[variable.name] = variable;
    }

    public bool IsVariableDeclared(string name)
    {
        foreach (var scope in scopes)
        {
            if (scope.ContainsKey(name))
            {
                return true;
            }
        }
        return false;
    }


    public void CheckExpression(Expression expr)
    {
        if (expr is Variable variable)
        {
            if (!IsVariableDeclared(variable.name))
            {
                throw new SemanticError($"Variable '{variable.name}' is not declared.");
            }
        }
        else if (expr is Binary binary)
        {
            CheckExpression(binary.Left);
            CheckExpression(binary.Right);
        }

    }


}
public class SemanticError : Exception
{
    public SemanticError(string message) : base(message) { }
}