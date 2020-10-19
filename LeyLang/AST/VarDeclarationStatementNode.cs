using System;
using System.Collections.Generic;
using System.Text;

namespace LeyLang.AST {
    class VarDeclarationStatementNode : StatementNode {
        public VarDeclarationStatementNode(VarDeclarationNode declaration, ExprNode expression = null) {
            Declaration = declaration;
            Expression = expression;
        }

        public VarDeclarationNode Declaration { get; }
        public ExprNode Expression { get; }

        protected override Node[] ChildNodes => new Node[] { Declaration, Expression };
    }
}
