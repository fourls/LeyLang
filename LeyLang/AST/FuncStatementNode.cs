using System;
using System.Collections.Generic;
using System.Text;

namespace LeyLang.AST {
    public class FuncStatementNode : StatementNode {
        public FuncStatementNode(FuncPrototypeNode prototype, BlockNode body) {
            Prototype = prototype;
            Body = body;
        }

        public FuncPrototypeNode Prototype { get; }
        public BlockNode Body { get; }

        protected override Node[] ChildNodes => new Node[] { Prototype, Body };
    }
}
