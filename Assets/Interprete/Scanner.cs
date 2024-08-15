using System.Diagnostics;
using System.Xml;


public class Scanner
{
    private string source { get; }
    private readonly List<Token> tokens = new List<Token>();
    private int current = 0;
    private int start = 0;
    private int line = 1;
    private static Dictionary<string, Token> reserved = new Dictionary<string, Token>();

    public Scanner(string source)
    {
        this.source = source;
        reserved.Add("while", new Token(TokenType.WHILE, "while", "while", 0, 0));
        reserved.Add("for", new Token(TokenType.FOR, "for", "for", 0, 0));
        reserved.Add("in", new Token(TokenType.IN, "in", "in", 0, 0));
        reserved.Add("true", new Token(TokenType.TRUE, "true", "true", 0, 0));
        reserved.Add("false", new Token(TokenType.FALSE, "false", "false", 0, 0));
        reserved.Add("card", new Token(TokenType.CARD, "card", "card", 0, 0));
        reserved.Add("effect", new Token(TokenType.EFFECT, "effect", "effect", 0, 0));
        reserved.Add("Name", new Token(TokenType.NAME, "Name", "Name", 0, 0));
        reserved.Add("Params", new Token(TokenType.PARAMS, "Params", "Params", 0, 0));
        reserved.Add("Action", new Token(TokenType.ACTION, "Action", "Action", 0, 0));
        reserved.Add("Type", new Token(TokenType.TYPE, "Type", "Type", 0, 0));
        reserved.Add("Faction", new Token(TokenType.FACTION, "Faction", "Faction", 0, 0));
        reserved.Add("Power", new Token(TokenType.POWER, "Power", "Power", 0, 0));
        reserved.Add("Range", new Token(TokenType.RANGE, "Range", "Range", 0, 0));
        reserved.Add("OnActivation", new Token(TokenType.ONACTIVATION, "OnActivation", "OnActivation", 0, 0));
        reserved.Add("Effect", new Token(TokenType.ONACTIVATIONEFFECT, "Effect", "Effect", 0, 0));
        reserved.Add("Selector", new Token(TokenType.SELECTOR, "Selector", "Selector", 0, 0));
        reserved.Add("Single", new Token(TokenType.SINGLE, "Single", "Single", 0, 0));
        reserved.Add("Predicate", new Token(TokenType.PREDICATE, "Predicate", "Predicate", 0, 0));
        reserved.Add("PostAction", new Token(TokenType.POSTACTION, "PostAction", "PostAction", 0, 0));
        reserved.Add("Source", new Token(TokenType.SOURCE, "Source", "Source", 0, 0));
        reserved.Add("Hand", new Token(TokenType.POINTER, "Hand", "Hand", 0, 0));
        reserved.Add("Field", new Token(TokenType.POINTER, "Field", "Field", 0, 0));
        reserved.Add("Deck", new Token(TokenType.POINTER, "Deck", "Deck", 0, 0));
        reserved.Add("Graveyard", new Token(TokenType.POINTER, "Graveyard", "Graveyard", 0, 0));
        reserved.Add("Board", new Token(TokenType.POINTER, "Board", "Board", 0, 0));
        reserved.Add("TriggerPlayer", new Token(TokenType.FUN, "TriggerPlayer", "TriggerPlayer", 0, 0));
        reserved.Add("HandOfPlayer", new Token(TokenType.FUN, "HandOfPlayer", "HandOfPlayer", 0, 0));
        reserved.Add("DeckOfPlayer", new Token(TokenType.FUN, "DeckOfPlayer", "DeckOfPlayer", 0, 0));
        reserved.Add("FieldOfPlayer", new Token(TokenType.FUN, "FieldOfPlayer", "FieldOfPlayer", 0, 0));
        reserved.Add("GraveyardOfPlayer", new Token(TokenType.FUN, "GraveyardOfPlayer", "GraveyardOfPlayer", 0, 0));
        reserved.Add("Find", new Token(TokenType.FUN, "Find", "Find", 0, 0));
        reserved.Add("Push", new Token(TokenType.FUN, "Push", "Push", 0, 0));
        reserved.Add("SendBottom", new Token(TokenType.FUN, "SendBottom", "SendBottom", 0, 0));
        reserved.Add("Pop", new Token(TokenType.FUN, "Pop", "Pop", 0, 0));
        reserved.Add("Remove", new Token(TokenType.FUN, "Remove", "Remove", 0, 0));
        reserved.Add("Shuffle", new Token(TokenType.FUN, "Shuffle", "Shuffle", 0, 0));
        reserved.Add("Number", new Token(TokenType.NUMBERTYPE, "Number", "Number", 0, 0));
        reserved.Add("String", new Token(TokenType.STRINGTYPE, "String", "String", 0, 0));
        reserved.Add("Bool", new Token(TokenType.BOOLTYPE, "Bool", "Bool", 0, 0));
    }
    bool isAtEnd()
    {
        return current >= source.Length;
    }
    char Advance()
    {
        current++;
        return source[current - 1];
    }
    void addToken(TokenType type)
    {
        addToken(type, "");
    }
    void addToken(TokenType type, Object literal)
    {
        string text = source.Substring(start, current - start);
        tokens.Add(new Token(type, text, literal, line, current));
    }
    public void scanToken()
    {
        char c = Advance();
        switch (c)
        {
            case '(': addToken(TokenType.LEFT_PAREN); break;
            case ')': addToken(TokenType.RIGHT_PAREN); break;
            case '{': addToken(TokenType.LEFT_BRACE); break;
            case '}': addToken(TokenType.RIGHT_BRACE); break;
            case '[': addToken(TokenType.LEFT_BRACKET); break;
            case ']': addToken(TokenType.RIGHT_BRACKET); break;
            case ',': addToken(TokenType.COMMA); break;
            case '.': addToken(TokenType.DOT); break;
            case ':': addToken(TokenType.COLON); break;
            case ';': addToken(TokenType.SEMICOLON); break;
            case '*': addToken(TokenType.STAR); break;
            case '%': addToken(TokenType.PERCENT); break;
            case '^': addToken(TokenType.POW); break;
            case '!': addToken(Macth('=') ? TokenType.BANG_EQUAL : TokenType.BANG); break;
            case '<': addToken(Macth('=') ? TokenType.LESS_EQUAL : TokenType.LESS); break;
            case '>': addToken(Macth('=') ? TokenType.GREATER_EQUAL : TokenType.GREATER); break;
            case '|': if (Macth('|')) addToken(TokenType.OR); break;
            case '&': if (Macth('&')) addToken(TokenType.AND); break;
            case '=':
                if (Macth('=')) addToken(TokenType.EQUAL_EQUAL);
                else if (Macth('>')) addToken(TokenType.EQUAL_GREATER);
                else addToken(TokenType.EQUAL); break;
            case '+':
                if (Macth('+')) addToken(TokenType.PLUS_PLUS);
                else if (Macth('=')) addToken(TokenType.PLUS_EQUAL);
                else addToken(TokenType.PLUS); break;
            case '-':
                if (Macth('-')) addToken(TokenType.MINUS_MINUS);
                else if (Macth('=')) addToken(TokenType.MINUS_EQUAL);
                else addToken(TokenType.MINUS);
                break;
            case '/':
                if (Macth('/'))
                {
                    while (peek() != '\n' && !isAtEnd())
                    {
                        Advance();
                    }
                }
                else addToken(TokenType.SLASH);
                break;
            case ' ':
            case '\r':
            case '\t':
            case '\n': line++; break;
            case '"': String(); break;
            case '@': addToken(Macth('@') ? TokenType.ATSIGN_ATSIGN : TokenType.ATSIGN); break;
            default:
                if (isDigit(c))
                {
                    number();
                }
                else if (isAlpha(c))
                {
                    identifier();
                }
                else
                {
                    throw new Error(line + "wrong letter" + start + "" + current, ErrorType.SYNTAX);
                }
                break;

        }
    }
    public List<Token> ScanToken()
    {
        try
        {
            while (!isAtEnd())
            {
                start = current;
                scanToken();
            }
            tokens.Add(new Token(TokenType.EOF, "", "", line, current));
            return tokens;
        }
        catch (Error error)
        {
            Console.WriteLine(error.Message);
            throw;
        }
    }
    bool Macth(char character)
    {
        if (isAtEnd()) return false;
        if (source[current] != character) return false;
        current++;
        return true;
    }
    char peek()
    {
        if (isAtEnd()) return '\0';
        return source[current];
    }
    void String()
    {
        while (peek() != '"' && !isAtEnd())
        {
            if (peek() == '\n') line++;
            Advance();
        }

        if (isAtEnd())
        {
            throw new Error($"{line} : string incomplete", ErrorType.SYNTAX);
        }
        Advance();
        string value = source.Substring(start + 1, current - start - 2);
        addToken(TokenType.STRING, value);

    }
    bool isDigit(char c)
    {
        return c >= '0' && c <= '9';
    }
    void number()
    {
        while (isDigit(peek())) Advance();
        if (peek() == '.' && isDigit(peekNext()))
        {
            Advance();
            while (isDigit(peek())) Advance();
        }
        addToken(TokenType.NUMBER, Double.Parse(source.Substring(start, current - start)));
    }
    char peekNext()
    {
        if (current + 1 >= source.Length) return '\0';
        return source[current + 1];
    }
    void identifier()
    {
        while (isAlphaNumeric(peek())) Advance();
        string text = source.Substring(start, current - start);
        TokenType type = TokenType.IDENTIFIER;
        if (reserved.ContainsKey(text))
        {
            Token token = reserved[text];
            tokens.Add(new Token(token.type, token.lexeme, token.lexeme, line, current / line));
        }
        else
        {
            addToken(type);
        }
    }
    bool isAlpha(char c)
    {
        return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || c == '_';
    }
    bool isAlphaNumeric(char c)
    {
        return isAlpha(c) || isDigit(c);
    }

}



