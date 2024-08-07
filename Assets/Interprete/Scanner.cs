// using System.Diagnostics;
// using System.Xml;


// public class Scanner
// {
//     private string source { get; set; }
//     private List<Token> tokens = new List<Token>();
//     private int current = 0;
//     private int start = 0;
//     private int line = 1;
//     public Dictionary<string, Token> reserved = new Dictionary<string, Token>();

//     public Scanner(string source)
//     {
//         this.source = source;
//         reserved["for"] = new Token(TokenType.FOR, "for", 0, 0);
//         reserved["false"] = new Token(TokenType.FALSE, "false", 0, 0);
//         reserved["while"] = new Token(TokenType.WHILE, "while", 0, 0);
//         reserved["true"] = new Token(TokenType.TRUE, "true", 0, 0);
//         reserved["Predicate"] = new Token(TokenType.PREDICATE, "predicate", 0, 0);
//         reserved["effect"] = new Token(TokenType.EFFECT, "effect", 0, 0);
//         reserved["card"] = new Token(TokenType.CARD, "card", 0, 0);
//         reserved["PostAction"] = new Token(TokenType.POSTACTION, "postaction", 0, 0);
//         reserved["in"] = new Token(TokenType.IN, "in", 0, 0);
//         reserved["Name"] = new Token(TokenType.NAME, "name", 0, 0);
//         reserved["Booltype"] = new Token(TokenType.BOOLTYPE, "Booltypr", 0, 0);
//         reserved["Faction"] = new Token(TokenType.FACTION, "Faction", 0, 0);
//         reserved["Params"] = new Token(TokenType.PARAMS, "Params", 0, 0);
//         reserved["Action"] = new Token(TokenType.ACTION, "Action", 0, 0);
//         reserved["Type"] = new Token(TokenType.TYPE, "Type", 0, 0);
//         reserved["Power"] = new Token(TokenType.POWER, "Power", 0, 0);
//         reserved["Range"] = new Token(TokenType.RANGE, "Range", 0, 0);
//         reserved["OneActivation"] = new Token(TokenType.ONACTIVATION, "Oneactivation", 0, 0);
//         reserved["Fun"] = new Token(TokenType.FUN, "Fun", 0, 0);
//         reserved["Selector"] = new Token(TokenType.SELECTOR, "Selector", 0, 0);
//         reserved["Single"] = new Token(TokenType.SINGLE, "Single", 0, 0);
//         reserved["Pointer"] = new Token(TokenType.POINTER, "Pointer", 0, 0);
//         reserved["Method"] = new Token(TokenType.METHOD, "Method", 0, 0);
//         reserved["Stringtype"] = new Token(TokenType.STRINGTYPE, "Stringtype", 0, 0);
//         reserved["Numbertype"] = new Token(TokenType.NUMBERTYPE, "Numbertype", 0, 0);
//         reserved["Source"] = new Token(TokenType.SOURCE, "Source", 0, 0);
//         reserved["Effect"] = new Token(TokenType.FUN, "Effect", 0, 0);
//         reserved["TriggerPlayer"] = new Token(TokenType.POINTER, "TriggerPlayer", 0, 0);
//         reserved["Board"] = new Token(TokenType.POINTER, "Board", 0, 0);
//         reserved["HandOfPlayer"] = new Token(TokenType.POINTER, "HandOfPlayer", 0, 0);
//         reserved["DeckOfPlayer"] = new Token(TokenType.POINTER, "DeckOfPlayer", 0, 0);
//         reserved["FieldOfPlayer"] = new Token(TokenType.POINTER, "FieldOfPlayer", 0, 0);
//         reserved["GraveyardOfPlayer"] = new Token(TokenType.POINTER, "GraveyardOfPlayer", 0, 0);

//         reserved["Find"] = new Token(TokenType.METHOD, "Find", 0, 0);
//         reserved["Push"] = new Token(TokenType.METHOD, "Push", 0, 0);
//         reserved["SendBottom"] = new Token(TokenType.METHOD, "SendBottom", 0, 0);
//         reserved["Pop"] = new Token(TokenType.METHOD, "Pop", 0, 0);
//         reserved["Remove"] = new Token(TokenType.METHOD, "Remove", 0, 0);
//         reserved["Shuffle"] = new Token(TokenType.METHOD, "Shuffle", 0, 0);


//     }

//     private bool isAtEnd()
//     {
//         return current >= source.Length;
//     }

//     private char Advance()
//     {
//         return source[current++];
//     }

//     private void addToken(TokenType type)
//     {
//         string text = source.Substring(start, current - start);
//         tokens.Add(new Token(type, text, line, current));
//     }

//     public void scanToken()
//     {
//         char c = Advance();
//         switch (c)
//         {
//             case '(': addToken(TokenType.LEFT_PAREN); break;
//             case ')': addToken(TokenType.RIGHT_PAREN); break;
//             case '{': addToken(TokenType.LEFT_BRACE); break;
//             case '}': addToken(TokenType.RIGHT_BRACE); break;
//             case '[': addToken(TokenType.LEFT_BRACKET); break;
//             case ']': addToken(TokenType.RIGHT_BRACKET); break;
//             case ',': addToken(TokenType.COMMA); break;
//             case '.': addToken(TokenType.DOT); break;
//             case ':': addToken(TokenType.COLON); break;
//             case ';': addToken(TokenType.SEMICOLON); break;
//             case '*': addToken(TokenType.STAR); break;
//             case '!': addToken(Macth('=') ? TokenType.BANG_EQUAL : TokenType.BANG); break;
//             case '<': addToken(Macth('=') ? TokenType.LESS_EQUAL : TokenType.LESS); break;
//             case '>': addToken(Macth('=') ? TokenType.GREATER_EQUAL : TokenType.GREATER); break;
//             case '|': if (Macth('|')) addToken(TokenType.OR); break;
//             case '&': if (Macth('&')) addToken(TokenType.OR); break;
//             case '=':
//                 if (Macth('=')) addToken(TokenType.EQUAL_EQUAL);
//                 else if (Macth('>')) addToken(TokenType.EQUAL_GREATER);
//                 else addToken(TokenType.EQUAL); break;
//             case '+':
//                 if (Macth('=')) addToken(TokenType.PLUS_EQUAL);
//                 else if (Macth('+')) addToken(TokenType.PLUS_PLUS);
//                 else addToken(TokenType.PLUS); break;
//             case '-':
//                 if (Macth('=')) addToken(TokenType.MINUS_EQUAL);
//                 else if (Macth('-')) addToken(TokenType.MINUS_MINUS);
//                 else addToken(TokenType.MINUS); break;
//             case '/':
//                 if (Macth('/'))
//                 {
//                     while (peek() != '\n' && !isAtEnd()) Advance();
//                 }
//                 else addToken(TokenType.SLASH);
//                 break;
//             case ' ':
//             case '\r': break;
//             case '\t': break;
//             case '\n': line++; break;
//             case '"': String(); break;

//             default:
//                 if (isDigit(c))
//                 {
//                     number();
//                 }
//                 else if (isAlpha(c))
//                 {
//                     identifier();
//                 }
//                 else
//                 {
//                     Console.WriteLine("Error letra");
//                 }
//                 break;

//         }
//     }

//     public List<Token> ScanToken()
//     {
//         while (!isAtEnd())
//         {
//             start = current;
//             scanToken();
//         }
//         tokens.Add(new Token(TokenType.EOF, "", line, current));
//         return tokens;
//     }

//     private bool Macth(char expected)
//     {
//         if (isAtEnd()) return false;
//         if (source[current] != expected) return false;
//         current++;
//         return true;
//     }

//     private char peek()
//     {
//         if (isAtEnd()) return '\0';
//         return source[current];
//     }

//     private void String()
//     {
//         while (peek() != '"' && !isAtEnd())
//         {
//             if (peek() == '\n') line++;
//             Advance();
//         }

//         if (isAtEnd())
//         {
//             Console.WriteLine("Error");
//             return;
//         }
//         Advance();

//         string value = source.Substring(start + 1, current - 1);
//         tokens.Add(new Token(TokenType.STRING, value, line, current));
//     }
//     private bool isDigit(char c)
//     {
//         return c >= '0' && c <= '9';
//     }

//     private void number()
//     {
//         while (isDigit(peek())) Advance();
//         if (peek() == '.' && isDigit(peekNext()))
//         {
//             Advance();
//             while (isDigit(peek())) Advance();
//         }
//         tokens.Add(new Token(TokenType.NUMBER, (source.Substring(start, current - start)), line, current));
//     }

//     private char peekNext()
//     {
//         if (current + 1 >= source.Length) return '\0';
//         return source[current + 1];
//     }

//     private void identifier()
//     {
//         while (isAlphaNumeric(peek())) Advance();
//         string text = source.Substring(start, current - start);
//         TokenType type = TokenType.IDENTIFIER;
//         if (reserved.ContainsKey(text))
//         {
//             Token token = reserved[text];
//             tokens.Add(new Token(token.type, token.lexeme, line, current / line));
//         }
//         addToken(type);
//     }

//     private bool isAlpha(char c)
//     {
//         return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || c == '_';
//     }

//     private bool isAlphaNumeric(char c)
//     {
//         return isAlpha(c) || isDigit(c);
//     }

// }



