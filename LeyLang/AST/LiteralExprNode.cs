using System;
using System.Collections.Generic;
using System.Text;

namespace LeyLang.AST {
    public abstract class LiteralExprNode<T> : ExprNode {
        public T Value { get; }

        public LiteralExprNode(T value) {
            this.Value = value;
        }

        protected override string PrettyNodeContents => $"[{GetType().Name.Replace("Node","")} value='{Value}']";
    }
}
