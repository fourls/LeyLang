using System;
using System.Collections.Generic;
using System.Text;

namespace LeyLang.AST {
    public class PrototypeParameterNode : Node {
        public PrototypeParameterNode(string paramType, string paramName) {
            ParamType = paramType;
            ParamName = paramName;
        }

        public string ParamType { get; }
        public string ParamName { get; }

        protected override string PrettyNodeContents => $"[FuncPrototypeParameter argName='{ParamName}' argType='{ParamType}']";
    }
}
