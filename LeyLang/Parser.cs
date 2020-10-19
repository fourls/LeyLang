using LeyLang.AST;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace LeyLang {
    public class Parser {
        private static Dictionary<string, int> binopPrecedence = new Dictionary<string, int>() {
            ["="] = 2,
            ["&&"] = 5,
            ["||"] = 5,
            ["=="] = 10,
            ["!="] = 10,
            [">"] = 10,
            ["<"] = 10,
            [">="] = 10,
            ["<="] = 10,
            ["+"] = 120,
            ["-"] = 120,
            ["*"] = 140,
            ["/"] = 140,
        };

        private Lexer _lexer;
        private Token _curTok;

        public Parser(Lexer lexer) {
            _lexer = lexer;
        }

        private Token GetNextToken() {
            return _curTok = _lexer.GetToken();
        }

        private void ValidateToken(TokenKind kind) {
            if (_curTok.Kind != kind) throw new Exception($"Expected {kind} token, but token was {_curTok.Kind}.");
        }

        //private void ValidateToken(char value) {
        //    if (_curTok.CharValue != value) throw new Exception($"Expected token to have char value '{value}', but token had char value '{_curTok.CharValue}'.");
        //}

        private void ValidateToken(decimal value) {
            if (_curTok.DecimalValue != value) throw new Exception($"Expected token to have decimal value {value}, but token had decimal value {_curTok.DecimalValue}.");
        }

        private void ValidateToken(string value) {
            if (_curTok.StringValue != value) throw new Exception($"Expected token to have string value \"{value}\", but token had string value \"{_curTok.StringValue}\".");
        }

        public ProgramNode ParseProgram() {
            GetNextToken();
            return new ProgramNode(ParseBlockBody());
        }

        // a set of statements
        private BlockNode ParseBlockBody() {
            List<StatementNode> statements = new List<StatementNode>();

            while (_curTok.Kind != TokenKind.Eof) {
                statements.Add(ParseStatement());
            }

            return new BlockNode(statements);
        }

        // a set of statements between '{' and '}'
        private BlockNode ParseBlock() {
            ValidateToken("{");
            GetNextToken();

            List<StatementNode> statements = new List<StatementNode>();

            while(_curTok.StringValue != "}") {
                statements.Add(ParseStatement());
            }

            GetNextToken(); // eat '}'
            return new BlockNode(statements);
        }

        private ForStatementNode ParseForStatement() {
            ValidateToken(TokenKind.For);

            GetNextToken(); // eat 'for'
            ValidateToken("(");
            GetNextToken(); // eat '('

            var initialiser = ParseVarDeclarationStatement();
            var condition = ParseExpression();

            ValidateToken(";");
            GetNextToken(); // eat ';'

            var increment = ParseExpression();

            ValidateToken(")");
            GetNextToken(); // eat ')'

            var block = ParseBlock();

            return new ForStatementNode(initialiser, condition, increment, block);
        }

        private ConditionalStatementNode ParseConditionalStatement() {
            ValidateToken(TokenKind.If);

            List<ConditionalBlockNode> condBlocks = new List<ConditionalBlockNode>();

            GetNextToken(); // eat 'if'
            ValidateToken("(");
            GetNextToken(); // eat '('

            ExprNode ifExpr = ParseExpression();

            ValidateToken(")");
            GetNextToken(); // eat ')';

            BlockNode ifBlock = ParseBlock();

            condBlocks.Add(new ConditionalBlockNode(ifExpr, ifBlock));

            while(_curTok.Kind == TokenKind.Else) {
                GetNextToken(); // eat 'else'
                bool isElseIf = false;

                ExprNode expr = null;

                if(_curTok.Kind == TokenKind.If) {
                    isElseIf = true;
                    GetNextToken(); // eat 'if'

                    ValidateToken("(");
                    GetNextToken(); // eat '('

                    expr = ParseExpression();

                    ValidateToken(")");
                    GetNextToken(); // eat ')';
                }

                BlockNode block = ParseBlock();

                condBlocks.Add(new ConditionalBlockNode(expr, block));

                if (!isElseIf) break;
            }

            return new ConditionalStatementNode(condBlocks.ToArray());
        }

        // a line
        private StatementNode ParseStatement() {
            if (_curTok.Kind == TokenKind.Let)
                return ParseVarDeclarationStatement();
            if (_curTok.Kind == TokenKind.If)
                return ParseConditionalStatement();
            if (_curTok.Kind == TokenKind.While)
                return ParseWhileStatement();
            if (_curTok.Kind == TokenKind.For)
                return ParseForStatement();
            if (_curTok.Kind == TokenKind.Func)
                return ParseFuncStatement();
            if (_curTok.Kind == TokenKind.Return)
                return ParseReturnStatement();
            if (_curTok.Kind == TokenKind.Break)
                return ParseBreakStatement();
            if (_curTok.Kind == TokenKind.Continue)
                return ParseContinueStatement();

            return ParseExprStatement();
        }

        private FuncStatementNode ParseFuncStatement() {
            var proto = ParseFuncPrototype();
            var block = ParseBlock();

            return new FuncStatementNode(proto, block);
        }

        private FuncPrototypeNode ParseFuncPrototype() {
            ValidateToken(TokenKind.Func);

            GetNextToken(); // eat 'func'
            ValidateToken(TokenKind.Identifier);

            string funcName = _curTok.StringValue;

            GetNextToken(); // eat func name
            ValidateToken("(");
            GetNextToken(); // eat '('

            List<FuncPrototypeParameterNode> protoParams = new List<FuncPrototypeParameterNode>();

            if(_curTok.StringValue != ")") {
                while (true) {
                    ValidateToken(TokenKind.Identifier);

                    string paramName = _curTok.StringValue;

                    GetNextToken(); // eat param name
                    ValidateToken(":");
                    GetNextToken(); // eat ':'

                    ValidateToken(TokenKind.Identifier);

                    string paramType = _curTok.StringValue;

                    protoParams.Add(new FuncPrototypeParameterNode(paramType, paramName));

                    GetNextToken(); // eat param type

                    if (_curTok.StringValue == ")") break;

                    ValidateToken(",");
                    GetNextToken(); // eat ','
                }
            }

            GetNextToken(); // eat ')'

            ValidateToken(":");
            GetNextToken(); // eat ':'

            ValidateToken(TokenKind.Identifier);
            string returnType = _curTok.StringValue;
            GetNextToken(); // eat return type

            return new FuncPrototypeNode(funcName, returnType, protoParams);
        }


        private WhileStatementNode ParseWhileStatement() {
            ValidateToken(TokenKind.While);

            GetNextToken(); // eat 'if'
            ValidateToken("(");
            GetNextToken(); // eat '('

            ExprNode expr = ParseExpression();

            ValidateToken(")");
            GetNextToken(); // eat ')';

            BlockNode block = ParseBlock();

            return new WhileStatementNode(expr, block);
        }


        private BreakStatementNode ParseBreakStatement() {
            ValidateToken(TokenKind.Break);

            var node = new BreakStatementNode();
            GetNextToken(); // eat 'break'

            ValidateToken(";");
            GetNextToken(); // eat ';'


            return node;
        }

        private ContinueStatementNode ParseContinueStatement() {
            ValidateToken(TokenKind.Continue);

            var node = new ContinueStatementNode();
            GetNextToken(); // eat 'continue'

            ValidateToken(";");
            GetNextToken(); // eat ';'

            return node;
        }

        // '(let varname: type) = (expr.)'
        private VarDeclarationStatementNode ParseVarDeclarationStatement() {
            var declaration = ParseVarDeclaration();


            ExprNode varValue = null;

            if(_curTok.StringValue == "=") {
                GetNextToken(); // eat '='

                varValue = ParseExpression();
            }

            ValidateToken(";");
            GetNextToken(); // eat ';'

            return new VarDeclarationStatementNode(declaration, varValue);
        }

        // 'let varname: type'
        private VarDeclarationNode ParseVarDeclaration() {
            ValidateToken(TokenKind.Let);

            GetNextToken(); // eat 'let'

            ValidateToken(TokenKind.Identifier);
            string varName = _curTok.StringValue;

            GetNextToken(); // eat varName

            ValidateToken(":");
            GetNextToken(); // eat ':'

            string varType = _curTok.StringValue;

            GetNextToken(); // eat varType

            return new VarDeclarationNode(varName, varType);
        }

        // '<expr>;'
        private ExprStatementNode ParseExprStatement() {
            ExprNode expr = ParseExpression();

            ValidateToken(";");
            GetNextToken(); // eat ';'

            return new ExprStatementNode(expr);
        }

        // 'return <expr>;'
        private ReturnStatementNode ParseReturnStatement() {
            ValidateToken(TokenKind.Return);
            GetNextToken(); // eat 'return'

            var node = new ReturnStatementNode(ParseExpression());

            ValidateToken(";");
            GetNextToken(); // eat ';'

            return node;
        }

        // '<number>' or '-<number>'
        private NumberExprNode ParseNumberExpr() {
            // handle negative numbers - if the first character is a '-' then it is negative
            int multiplier = 1;
            if(_curTok.StringValue == "-") {
                multiplier = -1;
                GetNextToken(); // eat '-'
            }

            ValidateToken(TokenKind.NumberLiteral);

            var node = new NumberExprNode(_curTok.DecimalValue * multiplier);
            GetNextToken(); // eat number

            return node;
        }

        private StringExprNode ParseStringExpr() {
            ValidateToken(TokenKind.StringLiteral);

            var node = new StringExprNode(_curTok.StringValue);

            GetNextToken(); // eat string

            return node;
        }

        private BoolExprNode ParseBoolExpr() {
            ValidateToken(TokenKind.BoolLiteral);

            var node = new BoolExprNode(_curTok.BoolValue);

            GetNextToken(); // eat string

            return node;
        }

        // (<expr>)
        private ExprNode ParseParenExpr() {
            ValidateToken("(");

            GetNextToken(); // eat (

            var expr = ParseExpression();
            if (expr == null) return null;

            ValidateToken(")");

            GetNextToken(); // eat )

            return expr;
        }

        // '<func>(<args>...)' or '<var>'
        private ExprNode ParseIdentifierExpr() {
            ValidateToken(TokenKind.Identifier);

            string idName = _curTok.StringValue;

            GetNextToken();

            if(_curTok.StringValue != "(") { // not a function call
                return new VarExprNode(idName);
            }

            GetNextToken(); // eat (
            List<ExprNode> args = new List<ExprNode>();
            if (_curTok.StringValue != ")") {
                while (true) {
                    var arg = ParseExpression();
                    if (arg != null) {
                        args.Add(arg);
                    }

                    if (_curTok.StringValue == ")") break;

                    ValidateToken(",");
                    GetNextToken();
                }
            }

            GetNextToken(); // eat )

            return new CallExprNode(idName, args);
        }

        private ExprNode ParsePrimaryExpr() {
            if (_curTok.Kind == TokenKind.Identifier)
                return ParseIdentifierExpr();

            if (_curTok.Kind == TokenKind.NumberLiteral || _curTok.StringValue == "-")
                return ParseNumberExpr();

            if (_curTok.Kind == TokenKind.StringLiteral)
                return ParseStringExpr();

            if (_curTok.Kind == TokenKind.BoolLiteral)
                return ParseBoolExpr();

            if (_curTok.Kind == TokenKind.Punctuation) {
                if (_curTok.StringValue == ")")
                    return ParseParenExpr();
            }

            throw new Exception("Unhandled expression type.");
        }

        private ExprNode ParseExpression() {
            var lhs = ParsePrimaryExpr();

            return ParseBinOpRHS(0, lhs);
        }

        private ExprNode ParseBinOpRHS(int exprPrec, ExprNode lhs) {
            while(true) {
                int tokPrec = GetTokenBinopPrecedence();

                // if this binop is as strong or stronger as the LHS's binop, then consume it - there is more to be done
                if (tokPrec < exprPrec)
                    return lhs;

                // we know it's a binary operation now - because non-binops lead to -1s
                string binOp = _curTok.StringValue;

                GetNextToken(); // eat binop

                var rhs = ParsePrimaryExpr();

                // if the binop binds less tightly with RHS than the operator after RHS, let the pending operator take the RHS as its LHS
                int nextPrec = GetTokenBinopPrecedence();
                if(tokPrec < nextPrec) {
                    rhs = ParseBinOpRHS(tokPrec + 1, rhs);
                }

                lhs = new BinaryExprNode(binOp, lhs, rhs);
            }
        }

        private int GetTokenBinopPrecedence() {
            if (_curTok.Kind != TokenKind.Punctuation) return -1;

            if(binopPrecedence.ContainsKey(_curTok.StringValue)) {
                return binopPrecedence[_curTok.StringValue];
            }

            return -1;
        }
    }
}
