using System;
using System.Collections.Generic;
using System.Text;

namespace LeyLang.AST {
    public class FuncPrototypeNode : Node {
        public FuncPrototypeNode(string funcName, string returnType, List<FuncPrototypeParameterNode> parameters) {
            FuncName = funcName;
            ReturnType = returnType;
            Parameters = parameters;
        }

        public string FuncName { get; }
        public string ReturnType { get; }
        public List<FuncPrototypeParameterNode> Parameters { get; }

        protected override Node[] ChildNodes => Parameters.ToArray();
        protected override string PrettyNodeContents => $"[FuncPrototype name='{FuncName}']";
    }
}
