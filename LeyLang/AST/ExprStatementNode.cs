using System;
using System.Collections.Generic;
using System.Text;

namespace LeyLang.AST {
    public class ExprStatementNode : StatementNode {
        public ExprStatementNode(ExprNode expr) {
            Expr = expr;
        }

        public ExprNode Expr { get; }

        protected override Node[] ChildNodes => new Node[] { Expr };
    }
}
