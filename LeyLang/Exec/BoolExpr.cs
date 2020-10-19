using System;
using System.Collections.Generic;
using System.Text;

namespace LeyLang.Exec {
    class BoolExpr : Expr {
        private bool _value;

        private BoolExpr(AST.BoolExprNode boolExpr) {
            _value = boolExpr.Value;
        }

        public override LeyValue CalculateValue() {
            return new LeyBool(_value);
        }

        public static BoolExpr Create(AST.BoolExprNode boolExpr) {
            return new BoolExpr(boolExpr);
        }
    }
}
