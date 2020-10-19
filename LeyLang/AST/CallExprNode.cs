using System;
using System.Collections.Generic;
using System.Text;

namespace LeyLang.AST {
    public class CallExprNode : IdentifierExprNode {
        public string MethodName { get; }
        public List<ExprNode> Arguments { get; }

        public CallExprNode(string methodName, List<ExprNode> arguments) {
            MethodName = methodName;
            Arguments = arguments;
        }

        protected override Node[] ChildNodes => Arguments.ToArray();
        protected override string PrettyNodeContents => $"[CallExpr methodName='{MethodName}']";
    }
}
