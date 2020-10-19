using System;
using System.Collections.Generic;
using System.Text;

namespace LeyLang.AST {
    class ForStatementNode : StatementNode {
        public VarDeclarationStatementNode Initialiser { get; }
        public ExprNode Condition { get; }
        public ExprNode Increment { get; }
        public BlockNode Block { get; }

        public ForStatementNode(VarDeclarationStatementNode initialiser, ExprNode condition, ExprNode increment, BlockNode block) {
            Initialiser = initialiser;
            Condition = condition;
            Increment = increment;
            Block = block;
        }

        protected override Node[] ChildNodes => new Node[] { Initialiser, Condition, Increment, Block };
    }
}
