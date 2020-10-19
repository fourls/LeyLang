using System;
using System.Collections.Generic;
using System.Text;

namespace LeyLang.AST {
    public class StringExprNode : LiteralExprNode<String> {
        public StringExprNode(string value) : base(value) {
        }
    }
}
