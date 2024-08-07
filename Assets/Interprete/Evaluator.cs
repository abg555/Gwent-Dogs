// using System.ComponentModel.Design.Serialization;

// public class Evaluator
// {
//     public Object LiteralVisit(Literal expression)
//     {
//         return expression.Value;
//     }

//     public Object GroupingExpression(Expression expression)
//     {
//         // return Evaluate(expression);
//         if (expression is Grouping grouping)
//         {
//             return Evaluate(grouping.Expression);
//         }
//         else
//         {
//             throw new InvalidOperationException("Expresión de agrupación no válida");
//         }
//     }

//     public Object Evaluate(Expression expression)
//     {
//         return Accept(expression);
//     }

//     public object Accept(Expression expression)
//     {
//         return expression switch
//         {
//             Literal literal => LiteralVisit(literal),
//             Grouping grouping => GroupingExpression(grouping),
//             Unary unary => UnaryExpression(unary),
//             Binary binary => BinaryExpression(binary),
//             _ => throw new InvalidOperationException("Tipo de expresión no compatible"),
//         };
//     }

//     public Object UnaryExpression(Unary expression)
//     {
//         Object right = Evaluate(expression.Right);
//         switch (expression.Operator.type)


//         {
//             case TokenType.MINUS:
//                 CheckNumberOperand(expression.Operator, right);
//                 return -(double)right;
//             default: throw new InvalidOperationException("Unsupported operator ");
//             case TokenType.BANG: return !IsTrue(right);
//         }

//     }

//     bool IsTrue(Object ob)
//     {
//         if (ob == null) return false;
//         if (ob is bool) return (bool)ob;
//         return true;
//     }
//     bool IsEqual(Object a, Object b)
//     {
//         if (a == null && b == null) return true;
//         if (a == null) return false;
//         return a.Equals(b);
//     }

//     void CheckNumberOperand(Token op, object operand)
//     {
//         if (operand is double) return;
//         throw new RunTimeError(op, "Operando debe ser un numero");
//     }
//     void CheckNumberOperands(Token op, Object left, Object right)
//     {
//         if (left is double && right is double) return;
//         throw new RunTimeError(op, "Operando debe ser un numero");
//     }

//     public Object BinaryExpression(Binary expression)
//     {
//         Object left = Evaluate(expression.Left);
//         Object right = Evaluate(expression.Right);
//         switch (expression.Operator.type)
//         {
//             case TokenType.MINUS:
//                 CheckNumberOperands(expression.Operator, left, right);
//                 return (double)left - (double)right;
//             case TokenType.PLUS:
//                 if (left is Double && right is Double)
//                 {
//                     return (double)left + (double)right;
//                 }
//                 if (left is string && right is string)
//                 {
//                     return (string)left + (string)right;
//                 }
//                 throw new RunTimeError(expression.Operator, "Operando deben ser dos numeros o dos string");
//             case TokenType.SLASH:
//                 CheckNumberOperands(expression.Operator, left, right);
//                 return (double)left / (double)right;
//             case TokenType.STAR:
//                 CheckNumberOperands(expression.Operator, left, right);
//                 return (double)left * (double)right;
//             case TokenType.GREATER:
//                 CheckNumberOperands(expression.Operator, left, right);
//                 return (double)left > (double)right;
//             case TokenType.GREATER_EQUAL:
//                 CheckNumberOperands(expression.Operator, left, right);
//                 return (double)left >= (double)right;
//             case TokenType.LESS:
//                 CheckNumberOperands(expression.Operator, left, right);
//                 return (double)left < (double)right;
//             case TokenType.LESS_EQUAL:
//                 CheckNumberOperands(expression.Operator, left, right);
//                 return (double)left <= (double)right;
//             case TokenType.BANG_EQUAL: return !IsEqual(left, right);
//             case TokenType.EQUAL_EQUAL: return IsEqual(left, right);


//         }

//         return null!;
//     }
//     void Iterpret(Expression expression)
//     {
//         try
//         {
//             {
//                 Object value = Evaluate(expression);
//                 Console.WriteLine(Stringify(value));
//             }
//         }
//         catch (RunTimeError error)
//         {
//             RunError(error);
//         }
//     }

//     string Stringify(Object ob)
//     {
//         if (ob == null) return "null";
//         if (ob is double)
//         {
//             string text = ob.ToString()!;
//             if (text.EndsWith(".0"))
//             {
//                 text = text.Substring(0, text.Length - 2);
//                 return text;
//             }
//         }

//         return ob.ToString()!;
//     }

//     static void RunError(RunTimeError error)
//     {

//     }


// }