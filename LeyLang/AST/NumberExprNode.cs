using System;
using System.Collections.Generic;
using System.Text;

namespace LeyLang.AST {
    public class NumberExprNode : LiteralExprNode<decimal> {
        public NumberExprNode(decimal value) : base(value) {
        }
    }
}
