using System;
using System.Collections.Generic;
using System.Text;

namespace LeyLang.Exec {
    class VarExpr : Expr {
        public string VarName => _varName;
        private string _varName;

        private VarExpr(AST.VarExprNode astVarExpr) {
            _varName = astVarExpr.Name;
        }

        public override LeyValue CalculateValue() {
            return Lookup.Instance.Vars.GetVar(_varName).Value;
        }

        public static VarExpr Create(AST.VarExprNode astVarExpr) {
            return new VarExpr(astVarExpr);
        }
    }
}
