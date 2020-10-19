using System;
using System.Collections.Generic;
using System.Text;

namespace LeyLang.Exec {
    class NumberExpr : Expr {
        private decimal _value;

        private NumberExpr(AST.NumberExprNode numExpr) {
            _value = numExpr.Value;
        }

        public override LeyValue CalculateValue() {
            return new LeyNumber(_value);
        }

        public static NumberExpr Create(AST.NumberExprNode numExpr) {
            return new NumberExpr(numExpr);
        }
    }
}
