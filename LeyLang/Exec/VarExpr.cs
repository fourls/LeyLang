using System;
using System.Collections.Generic;
using System.Text;

namespace LeyLang.Exec {
    class VarExpr : IdentifierExpr {
        private string _varName;

        private VarExpr(AST.VarExprNode astVarExpr) {
            _varName = astVarExpr.Name;
        }

        public override LeyValue CalculateValue() {
            return Lookup.Instance.Vars.GetVar(_varName).Value;
        }

        public override LeyValue CalculateValue(LeyObject callee) {
            return callee.GetVar<LeyValue>(_varName);
        }

        public void SetVar(LeyValue value, LeyObject callee=null) {
            if(callee == null) {
                Lookup.Instance.Vars.GetVar(_varName).Set(value);
            } else {
                callee.SetVar(_varName, value);
            }
        }

        public static VarExpr Create(AST.VarExprNode astVarExpr) {
            return new VarExpr(astVarExpr);
        }
    }
}
