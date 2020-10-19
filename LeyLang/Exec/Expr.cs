using System;
using System.Collections.Generic;
using System.Text;

namespace LeyLang.Exec {
    public abstract class Expr {
        public abstract LeyValue CalculateValue();

        public static Expr Create(AST.ExprNode astExpr) {
            // binary operation expression
            if (astExpr is AST.BinaryExprNode astBinaryExpr)
                return BinaryExpr.Create(astBinaryExpr);

            // identifier chain expression
            if (astExpr is AST.IdentifierChainExprNode astChainExpr)
                return IdentifierChainExpr.Create(astChainExpr);

            // variable expression
            if (astExpr is AST.VarExprNode astVarExpr)
                return VarExpr.Create(astVarExpr);

            // number literal expression
            if (astExpr is AST.NumberExprNode numExpr)
                return NumberExpr.Create(numExpr);

            // function call expression
            if (astExpr is AST.CallExprNode callExpr)
                return CallExpr.Create(callExpr);

            // string literal expression
            if (astExpr is AST.StringExprNode strExpr)
                return StringExpr.Create(strExpr);

            // bool literal expression
            if (astExpr is AST.BoolExprNode boolExpr)
                return BoolExpr.Create(boolExpr);

            throw new Exception("Unknown AST expression node encountered.");
        }
    }
}
