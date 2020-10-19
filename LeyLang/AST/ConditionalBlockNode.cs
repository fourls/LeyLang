using System;
using System.Collections.Generic;
using System.Text;

namespace LeyLang.AST {
    class ConditionalBlockNode : Node {
        public ExprNode Expr { get; }
        public BlockNode Block { get; }

        public ConditionalBlockNode(ExprNode expr, BlockNode block) {
            Expr = expr;
            Block = block;
        }

        protected override Node[] ChildNodes => new Node[] { Expr, Block };
    }
}
