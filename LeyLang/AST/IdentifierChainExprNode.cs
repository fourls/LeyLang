using System;
using System.Collections.Generic;
using System.Text;

namespace LeyLang.AST {
    public class IdentifierChainExprNode : ExprNode {
        public List<IdentifierExprNode> Exprs { get; }

        public IdentifierChainExprNode(List<IdentifierExprNode> exprs) {
            Exprs = exprs;
        }

        protected override Node[] ChildNodes => Exprs.ToArray();
    }
}
