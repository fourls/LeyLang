using System;
using System.Collections.Generic;
using System.Text;

namespace LeyLang.Exec {
    class ExprStatement : Statement {
        private Expr _expr;

        private ExprStatement(AST.ExprNode astExpr) {
            _expr = Expr.Create(astExpr);
        }

        public override void Execute() {
            _expr.CalculateValue();
        }

        public static ExprStatement Create(AST.ExprStatementNode astExprStmt) {
            return new ExprStatement(astExprStmt.Expr);
        }
    }
}
