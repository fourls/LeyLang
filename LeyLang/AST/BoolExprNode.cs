using System;
using System.Collections.Generic;
using System.Text;

namespace LeyLang.AST {
    public class BoolExprNode : LiteralExprNode<bool> {
        public BoolExprNode(bool value) : base(value) {
        }
    }
}
