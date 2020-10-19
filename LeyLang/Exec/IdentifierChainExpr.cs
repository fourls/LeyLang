using System;
using System.Collections.Generic;
using System.Text;

namespace LeyLang.Exec {
    public class IdentifierChainExpr : Expr {
        private IdentifierExpr[] _exprs;
        private IdentifierChainExpr(AST.IdentifierChainExprNode astChain) {
            _exprs = new IdentifierExpr[astChain.Exprs.Count];

            for(int i = 0; i < _exprs.Length; i++) {
                _exprs[i] = IdentifierExpr.Create(astChain.Exprs[i]);
            }

            if (_exprs.Length == 0)
                throw new Exception("Zero length identifier chain created.");
        }

        public static IdentifierChainExpr Create(AST.IdentifierChainExprNode astChain) {
            return new IdentifierChainExpr(astChain);
        }

        private LeyObject GetCallee() {
            LeyObject currentObj = null;
            for (int i = 0; i < _exprs.Length - 1; i++) {
                currentObj = _exprs[i].CalculateValue(currentObj) as LeyObject;

                if (currentObj == null)
                    throw new LeyException($"Dot operator used on non-object at position {i} in a chain of {_exprs.Length}.");
            }

            return currentObj;
        }

        public override LeyValue CalculateValue() {
            LeyObject callee = GetCallee();
            var lastExpr = _exprs[_exprs.Length - 1];
            return callee == null ? lastExpr.CalculateValue() : lastExpr.CalculateValue(callee);
        }

        public void SetVar(LeyValue newValue) {
            LeyObject secondLast = GetCallee();

            if(_exprs[_exprs.Length-1] is VarExpr varExpr) {
                varExpr.SetVar(newValue,secondLast);
            } else {
                throw new LeyException("Variable assignment attempted on method call.");
            }
        }
    }
}
