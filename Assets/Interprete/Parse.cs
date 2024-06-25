public class Parse{
private List<Token> tokens;
private int current = 0;

public Parse(List<Token> tokens) {
this.tokens = tokens;
}

private Expression expression() {
return equality();
}

private Expression equality() {
expression expression = comparison();
while (match(BANG_EQUAL, EQUAL_EQUAL)) {
Token operator = previous();
expression right = comparison();
expression = new expression.Binary(expression, operator, right);
}
return expression;
}

private boolean match(TokenType... types) {
for (TokenType type : types) {
if (check(type)) {
advance();
return true;
}
}
return false;
}
private boolean check(TokenType type) {
if (isAtEnd()) return false;
return peek().type == type;
}
private Token advance() {
if (!isAtEnd()) current++;
return previous();
}

private boolean isAtEnd() {
return peek().type == EOF;
}
private Token peek() {
return tokens.get(current);
}
private Token previous() {
return tokens.get(current - 1);
}

private expression comparison() {
expression expression = term();
while (match(GREATER, GREATER_EQUAL, LESS, LESS_EQUAL)) {
Token operator = previous();
expression right = term();
expression = new expression.Binary(expression, operator, right);
}
return expression;
}

private expression term() {
expression expression = factor();
while (match(MINUS, PLUS)) {
Token operator = previous();
expression right = factor();
expression = new expression.Binary(expression, operator, right);
}
return expression;
}

private expression factor() {
expression expression = unary();
while (match(SLASH, STAR)) {
Token operator = previous();
expression right = unary();
expression = new expression.Binary(expression, operator, right);
}
return expression;
}







}