public class Token
{
    public TokenType type { get; set; }
    public string lexeme { get; set; }
    public int line { get; set; }
    public int column { get; set; }
    public Object Literal { get; set; }



    public Token(TokenType type, string lexeme, Object literal, int line, int column)
    {
        this.type = type;
        this.lexeme = lexeme;
        this.line = line;
        this.column = column;
        this.Literal = literal;
    }

}


public enum TokenType
{
    //SINGLE_CHARACTER TOKENS
    LEFT_PAREN, RIGHT_PAREN, LEFT_BRACE, RIGHT_BRACE,
    COMMA, DOT, SEMICOLON, SLASH, STAR,
    COLON, LEFT_BRACKET, RIGHT_BRACKET, PERCENT, POW,

    //ONE OR TWO CHARACTER TOKENS
    BANG, BANG_EQUAL, EQUAL, EQUAL_EQUAL, GREATER,
    GREATER_EQUAL, LESS, LESS_EQUAL, PLUS, MINUS,
    PLUS_PLUS, MINUS_MINUS, PLUS_EQUAL, MINUS_EQUAL,
    EQUAL_GREATER, ATSIGN_ATSIGN, ATSIGN,

    //LITERALS
    IDENTIFIER, STRING, NUMBER, BOOL,

    //KEYWORDS
    AND, FALSE, FOR, OR, BOOLTYPE, FACTION,
    TRUE, WHILE, EFFECT, CARD, POSTACTION, PREDICATE,
    NAME, IN, PARAMS, ACTION, TYPE, POWER, RANGE, ONACTIVATION,
    FUN, SELECTOR, SINGLE, POINTER, STRINGTYPE, NUMBERTYPE,
    SOURCE, ONACTIVATIONEFFECT, ZONE,


    EOF

}