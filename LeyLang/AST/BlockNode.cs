using System;
using System.Collections.Generic;
using System.Text;

namespace LeyLang.AST {
    public class BlockNode : Node {
        public BlockNode(List<StatementNode> statements) {
            Statements = statements;
        }

        public List<StatementNode> Statements { get; }

        protected override Node[] ChildNodes => Statements.ToArray();
    }
}
