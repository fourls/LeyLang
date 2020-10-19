using System;
using System.Collections.Generic;
using System.Text;

namespace LeyLang.AST {
    public class VarExprNode : IdentifierExprNode {
        public string Name { get; }

        public VarExprNode(string name) {
            Name = name;
        }

        protected override string PrettyNodeContents => $"[VariableExpr var='{Name}']";
    }
}
