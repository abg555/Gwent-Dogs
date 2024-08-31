using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


public class Semantic : MonoBehaviour
{
    private Dictionary<string, Variable> variables = new Dictionary<string, Variable>();
    private Stack<Dictionary<string, Variable>> scopes = new Stack<Dictionary<string, Variable>>();
    private Dictionary<Expression, Effect> effects = new Dictionary<Expression, Effect>();
    private Dictionary<string, Effect> effectNames = new Dictionary<string, Effect>();
    private Dictionary<string, Card> cardNames = new Dictionary<string, Card>();
    private Effect currentEffect;
    public Menu crear;


    private void MostrarError(string errorMessage)
    {
        PlayerPrefs.SetString("ErrorMessage", errorMessage);
        SceneManager.LoadScene("Error");
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
    public Variable GetVariable(string name)
    {
        if (!variables.TryGetValue(name, out var variable))
        {
            string errorMessage = $"Variable '{name}' no está definida.";
            MostrarError(errorMessage);
            throw new SemanticError(errorMessage);
        }
        return variable;
    }
    public void DefineEffect(Effect effect)
    {
        if (effects.ContainsKey(effect.Name.name))
        {
            string errorMessage = $"Efecto '{effect.Name}' ya está definido.";
            MostrarError(errorMessage);
            throw new SemanticError(errorMessage);
        }
        effects[effect.Name.name] = effect;
    }
    public Effect GetEffect(Expression name)
    {
        if (!effects.TryGetValue(name, out var effect))
        {
            string errorMessage = $"Efecto '{name}' no está definido.";
            MostrarError(errorMessage);
            throw new SemanticError(errorMessage);
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
        try
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
                            string errorMessage = $"El efecto '{effectName}' mencionado en la carta '{cardName}' no está definido.";
                            MostrarError(errorMessage);
                            throw new SemanticError(errorMessage);
                        }
                        Effect declaredEffect = effectNames[effectName];
                        foreach (var param in element.oae.Params)
                        {
                            var declaredParam = declaredEffect.Params.nodes.FirstOrDefault(p => (p as Variable)?.name == param.Left.name) as Variable;
                            if (declaredParam == null)
                            {
                                string errorMessage = $"El parámetro '{param.Left.name}' no está definido en el efecto '{effectName}'.";
                                MostrarError(errorMessage);
                                throw new SemanticError(errorMessage);
                            }

                            // Verificar el tipo del parámetro
                            if (param.Right is Bool && declaredParam.type != Variable.Type.BOOL)
                            {
                                string errorMessage = $"El parámetro '{param.Left.name}' se le asignó el tipo incorrecto.";
                                MostrarError(errorMessage);
                                throw new SemanticError(errorMessage);
                            }
                            else if (param.Right is Number && declaredParam.type != Variable.Type.INT)
                            {
                                string errorMessage = $"El parámetro '{param.Left.name}' se le asignó el tipo incorrecto.";
                                MostrarError(errorMessage);
                                throw new SemanticError(errorMessage);
                            }
                            else if (param.Right is StringExpression && declaredParam.type != Variable.Type.STRING)
                            {
                                string errorMessage = $"El parámetro '{param.Left.name}' se le asignó el tipo incorrecto.";
                                MostrarError(errorMessage);
                                throw new SemanticError(errorMessage);
                            }
                        }
                    }
                    else if (element.postAction != null)
                    {
                        foreach (var postAction in element.postAction)
                        {
                            string postActionEffectName = (string)postAction.Type.Evaluate(new Scope());

                            if (!effectNames.ContainsKey(postActionEffectName))
                            {
                                string errorMessage = $"El efecto '{postActionEffectName}' mencionado en el PostAction de la carta '{cardName}' no está definido.";
                                MostrarError(errorMessage);
                                throw new SemanticError(errorMessage);
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            MostrarError($"Error al verificar la carta: {ex.Message}");

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
            string errorMessage = $"Variable '{variable.name}' is not declared.";
            MostrarError(errorMessage);
            throw new SemanticError(errorMessage);
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
                    string errorMessage = $"Se está accediendo a la propiedad {propertyName} sin asignarle un valor.";
                    MostrarError(errorMessage);
                    throw new SemanticError(errorMessage);
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
                    string errorMessage = $"El efecto '{effectName}' ya está definido.";
                    MostrarError(errorMessage);
                    throw new SemanticError(errorMessage);
                }
                effectNames[effectName] = effect;
                CheckEffect(effect);
                effect.Print();
                Debug.Log("Análisis semántico del efecto completado con éxito.");
            }

            // Luego, procesar todas las cartas
            foreach (Card card in program.card)
            {
                string cardName = ((StringExpression)card.Name.name).Value;
                cardNames[cardName] = card;
                CheckCard(card);
                card.Print();
                Debug.Log("Análisis semántico de la carta completado con éxito.");
            }
            if (program.effects.Count == 0 && program.card.Count == 0)
            {
                Debug.Log("El AST analizado no contiene efectos ni cartas. Análisis semántico omitido.");
            }
        }
        else
        {
            Debug.Log("El AST analizado no es un programa válido. Análisis semántico omitido.");
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
                            string errorMessage = $"La variable '{rightVar.name}' no está definida en los parámetros del efecto.";
                            MostrarError(errorMessage);
                            throw new UnityException(errorMessage);
                        }

                    }
                    else if (!(assignment.Right is Number))
                    {
                        string errorMessage = "La propiedad Power debe ser asignada con un número o una variable de tipo Number definida en los parámetros.";
                        MostrarError(errorMessage);
                        throw new SemanticError(errorMessage);
                    }
                    break;
                case "type":
                case "name":
                case "faction":
                    if (assignment.Right is Variable rightVa)
                    {
                        if (!IsVariableDeclaredInParams(rightVa.name))
                        {
                            string errorMessage = $"La variable '{rightVa.name}' no está definida en los parámetros del efecto.";
                            MostrarError(errorMessage);
                            throw new UnityException(errorMessage);
                        }

                    }
                    else if (!(assignment.Right is StringExpression))
                    {
                        string errorMessage = "La propiedad Name debe ser asignada con un string o una variable de tipo String.";
                        MostrarError(errorMessage);
                        throw new SemanticError(errorMessage);
                    }
                    break;
                default:
                    string defaultErrorMessage = $"Asignación no permitida a la propiedad {propertyName}.";
                    MostrarError(defaultErrorMessage);
                    throw new SemanticError(defaultErrorMessage);

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
            string errorMessage = $"El efecto '{effectCall.Name}' ya está definido.";
            MostrarError(errorMessage);
            throw new SemanticError($"Efecto '{effectCall.Name}' no está definido.");
        }
        // Verifica que los argumentos coincidan con los parámetros del efecto...
    }

    public void CheckStatement(Statement statement)
    {
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
                string errorMessage = $"Variable '{variable.name}' is not declared.";
                MostrarError(errorMessage);
                throw new SemanticError(errorMessage);
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
