using System;
using System.Collections.Generic;
using System.Text;

namespace LeyLang.AST {
    public class FuncPrototypeNode : Node {
        public FuncPrototypeNode(string protoName, string returnType, List<PrototypeParameterNode> parameters) {
            ProtoName = protoName;
            ReturnType = returnType;
            Parameters = parameters;
        }

        public string ProtoName { get; }
        public string ReturnType { get; }
        public List<PrototypeParameterNode> Parameters { get; }

        protected override Node[] ChildNodes => Parameters.ToArray();
        protected override string PrettyNodeContents => $"[FuncPrototype name='{ProtoName}' returnType='{ReturnType}']";
    }
}
