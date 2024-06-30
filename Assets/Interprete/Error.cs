public class Error : Exception
{
    public string Message { get; private set; }
    public ErrorType ErrorType { get; private set; }

    public Error(string message, ErrorType errorType)
    {
        Message = message;
        ErrorType = errorType;
    }

    public string Report()
    {
        return $"{ErrorType} Error: {Message}";
    }
}

public enum ErrorType
{
    LEXICAL, SYNTAX, SEMANTIC
}