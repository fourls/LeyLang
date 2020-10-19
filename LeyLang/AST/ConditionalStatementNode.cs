using System;
using System.Collections.Generic;
using System.Text;

namespace LeyLang.AST {
    class ConditionalStatementNode : StatementNode {
        public ConditionalBlockNode[] Ifs { get; }

        public ConditionalStatementNode(ConditionalBlockNode[] ifs) {
            Ifs = ifs;
        }

        protected override Node[] ChildNodes => Ifs;
    }
}
