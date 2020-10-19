using System;
using System.Collections.Generic;
using System.Text;

namespace LeyLang.AST {
    class VarDeclarationNode : Node {
        public VarDeclarationNode(string varName, string varType) {
            VarName = varName;
            VarType = varType;
        }

        public string VarName { get; }
        public string VarType { get; }

        protected override string PrettyNodeContents => $"[VarDeclaration name='{VarName}' type='{VarType}']";
    }
}
