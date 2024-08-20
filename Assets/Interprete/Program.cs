public class Evaluator2
{
    private Dictionary<string, object> environment = new Dictionary<string, object>();

    public object EvaluateEffect(Effect effect)
    {
        // Evaluar los parámetros del efecto
        foreach (var param in effect.Params.nodes)
        {
            if (param is Variable variable)
            {
                environment[variable.name] = null; // Inicializar variables
            }
        }

        // Evaluar la acción del efecto
        return EvaluateAction(effect.Action);
    }

    public object EvaluateCard(Card card)
    {
        // Evaluar las propiedades de la carta
        environment["Type"] = EvaluateExpression(card.Type.type);
        environment["Name"] = EvaluateExpression(card.Name.name);
        environment["Faction"] = EvaluateExpression(card.Faction.faction);
        environment["Power"] = EvaluateExpression(card.Power.power);

        // Evaluar OnActivation si existe
        if (card.OnActivation != null)
        {
            foreach (var element in card.OnActivation.Elements)
            {
                EvaluateOnActivationElement(element);
            }
        }

        return environment;
    }

    private object EvaluateAction(Action action)
    {
        foreach (var statement in action.Block.statements)
        {
            EvaluateStatement(statement);
        }
        return null;
    }

    private void EvaluateStatement(Statement statement)
    {
        if (statement is Assignment assignment)
        {
            var value = EvaluateExpression(assignment.Right);
            environment[assignment.Left.name] = value;
        }
        else if (statement is ForStatement forStatement)
        {
            // Implementar lógica para el bucle for
        }
        // Agregar más tipos de statements según sea necesario
    }

    private object EvaluateExpression(Expression expr)
    {
        if (expr is Number number)
        {
            return number.Value;
        }
        else if (expr is StringExpression stringExpr)
        {
            return stringExpr.Value;
        }
        else if (expr is Variable variable)
        {
            return environment[variable.name];
        }
        // Agregar más tipos de expresiones según sea necesario
        return null;
    }

    private void EvaluateOnActivationElement(OnActivationElements element)
    {
        // Implementar la lógica para evaluar OnActivationElements
    }
}
