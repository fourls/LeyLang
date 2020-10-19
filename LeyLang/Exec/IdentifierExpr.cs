using System;

namespace LeyLang.Exec {
    public abstract class IdentifierExpr : Expr {


        public static IdentifierExpr Create(AST.IdentifierExprNode astIdent) {
            if (astIdent is AST.VarExprNode astVar)
                return VarExpr.Create(astVar);
            if (astIdent is AST.CallExprNode astCall)
                return CallExpr.Create(astCall);

            throw new Exception("Unknown AST expression node encountered.");
        }

        public abstract LeyValue CalculateValue(LeyObject callee);
    }
}