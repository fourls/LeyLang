using System;
using System.Collections.Generic;
using System.Text;

namespace LeyLang.AST {
    class ReturnStatementNode : StatementNode {
        public ExprNode Expr { get; }

        public ReturnStatementNode(ExprNode expr) {
            Expr = expr;
        }

        protected override Node[] ChildNodes => new Node[] { Expr };
    }
}
