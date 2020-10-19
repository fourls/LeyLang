using System;
using System.Collections.Generic;
using System.Text;

namespace LeyLang.AST {
    public class BinaryExprNode : ExprNode {
        public string Operation { get; }
        public ExprNode LHS { get; }
        public ExprNode RHS { get; }

        public BinaryExprNode(string operation, ExprNode lhs, ExprNode rhs) {
            Operation = operation;
            LHS = lhs;
            RHS = rhs;
        }

        protected override Node[] ChildNodes => new Node[] { LHS, RHS };
        protected override string PrettyNodeContents => $"[BinaryExpr op='{Operation}']";
    }
}
