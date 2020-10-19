using System;
using System.Collections.Generic;
using System.Text;

namespace LeyLang.AST {
    class WhileStatementNode : StatementNode {
        public ExprNode Condition { get; }
        public BlockNode Block { get; }

        public WhileStatementNode(ExprNode condition, BlockNode block) {
            Condition = condition;
            Block = block;
        }

        protected override Node[] ChildNodes => new Node[] { Condition, Block };
    }
}
