using System;
using System.Collections.Generic;
using System.Text;

namespace LeyLang.Exec {
    class StringExpr : Expr {
        private string _value;

        private StringExpr(AST.StringExprNode strExpr) {
            _value = strExpr.Value;
        }

        public override LeyValue CalculateValue() {
            return new LeyString(_value);
        }

        public static StringExpr Create(AST.StringExprNode strExpr) {
            return new StringExpr(strExpr);
        }
    }
}
