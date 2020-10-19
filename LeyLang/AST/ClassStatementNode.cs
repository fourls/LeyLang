using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LeyLang.AST {
    class ClassStatementNode : StatementNode {
        public ClassStatementNode(string className, BlockNode body) {
            ClassName = className;
            Body = body;
        }

        public string ClassName { get; }
        public BlockNode Body { get; }

        protected override Node[] ChildNodes => new Node[] { Body };
        protected override string PrettyNodeContents => $"[ClassStatement name='{ClassName}']";
    }
}
