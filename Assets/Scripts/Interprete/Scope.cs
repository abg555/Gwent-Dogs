using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scope<T> : MonoBehaviour
{
    public Dictionary<string, T> variable;
    public Scope<T> scope;
    public Scope()
    {
        scope = null;
        variable = new Dictionary<string, T>();
    }
    public Scope(Scope<T> Scope)
    {
        scope = Scope;
        variable = new Dictionary<string, T>();
    }

    public bool IsInScope(string name)
    {
        if (scope == null) return variable.ContainsKey(name);
        else if (variable.ContainsKey(name)) return true;
        else return scope.IsInScope(name);

    }

    public T Get(string name)
    {
        if (scope == null)
        {
            if (variable.ContainsKey(name)) return variable[name];
            else
            {
                Debug.Log($"'{name}' not found");
                return default;
            }
        }
        else if (variable.ContainsKey(name)) return variable[name];
        else return scope.Get(name);

    }

    public void Set(string name, T value)
    {
        if (!IsInScope(name) || variable.ContainsKey(name)) variable[name] = value;
        else scope.Set(name, value);
    }
}