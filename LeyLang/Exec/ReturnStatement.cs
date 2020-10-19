using System;
using System.Collections.Generic;
using System.Text;

namespace LeyLang.Exec {
    class ReturnStatement : Statement {
        private Expr _expr;

        public ReturnStatement(AST.ReturnStatementNode astReturn) {
            _expr = Expr.Create(astReturn.Expr);
        }

        public override void Execute() {
            throw new ReturnException(_expr.CalculateValue());
        }

        public static ReturnStatement Create(AST.ReturnStatementNode astReturn) {
            return new ReturnStatement(astReturn);
        }
    }
}
