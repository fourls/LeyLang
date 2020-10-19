using System;
using System.Collections.Generic;
using System.Text;

namespace LeyLang.Exec {
    public abstract class Statement : Node {
        public abstract void Execute();

        public static Statement Create(AST.StatementNode stmt) {
            if (stmt is AST.ExprStatementNode exprStmt)
                return ExprStatement.Create(exprStmt);
            if (stmt is AST.ConditionalStatementNode condStmt)
                return ConditionalStatement.Create(condStmt);
            if (stmt is AST.ForStatementNode forStmt)
                return ForStatement.Create(forStmt);
            if (stmt is AST.VarDeclarationStatementNode varStmt)
                return VarDeclarationStatement.Create(varStmt);
            if (stmt is AST.FuncStatementNode funcStmt)
                return FuncStatement.Create(funcStmt);
            if (stmt is AST.ReturnStatementNode returnStmt)
                return ReturnStatement.Create(returnStmt);
            if (stmt is AST.WhileStatementNode whileStmt)
                return WhileStatement.Create(whileStmt);
            if (stmt is AST.BreakStatementNode breakStmt)
                return BreakStatement.Create(breakStmt);
            if (stmt is AST.ContinueStatementNode contStmt)
                return ContinueStatement.Create(contStmt);
            if (stmt is AST.ClassStatementNode classStmt)
                return ClassStatement.Create(classStmt);

            throw new Exception("Unknown AST statement node encountered.");
        }

    }
}
