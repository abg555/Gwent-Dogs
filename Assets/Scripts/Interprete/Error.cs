using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Error : Exception
{
    public string Mess { get; private set; }
    public ErrorType ErrorType { get; private set; }

    public Error(string message, ErrorType errorType)
    {
        Mess = message;
        ErrorType = errorType;
    }

    public string Report()
    {
        return $"{ErrorType} Error: {Mess}";
    }
}
public enum ErrorType
{
    LEXICAL, SYNTAX, SEMANTIC
}