public class Token
{
    public TokenType type { get; private set; }
    public string lexeme { get; private set; }
    public int line { get; private set; }
    public int column { get; private set; }


    public Token(TokenType type, string lexeme, int line, int column)
    {
        this.type = type;
        this.lexeme = lexeme;
        this.line = line;
        this.column = column;
    }






}
public enum TokenType
{
    //SINGLE_CHARACTER TOKENS
    LEFT_PAREN, RIGHT_PAREN, LEFT_BRACE, RIGHT_BRACE,
    COMMA, DOT, SEMICOLON, SLASH, STAR,
    COLON, LEFT_BRACKET, RIGHT_BRACKET,

    //ONE OR TWO CHARACTER TOKENS
    BANG, BANG_EQUAL, EQUAL, EQUAL_EQUAL, GREATER,
    GREATER_EQUAL, LESS, LESS_EQUAL, PLUS, MINUS,
    PLUS_PLUS, MINUS_MINUS, PLUS_EQUAL, MINUS_EQUAL,
    EQUAL_GREATER,

    //LITERALS
    IDENTIFIER, STRING, NUMBER,

    //KEYWORDS
    AND, FALSE, FOR, OR, BOOLTYPE, FACTION,
    TRUE, WHILE, EFFECT, CARD, POSTACTION, PREDICATE,
    NAME, IN, PARAMS, ACTION, TYPE, POWER, RANGE, ONACTIVATION,
    FUN, SELECTOR, SINGLE, POINTER, METHOD, STRINGTYPE, NUMBERTYPE,
    SOURCE,


    EOF

}