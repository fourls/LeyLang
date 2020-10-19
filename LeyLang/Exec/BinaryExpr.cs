using System;
using System.Collections.Generic;
using System.Text;

namespace LeyLang.Exec {
    public abstract class BinaryExpr : Expr {
        protected Expr _lhs;
        protected Expr _rhs;

        protected BinaryExpr(AST.BinaryExprNode astExpr) {
            _lhs = Expr.Create(astExpr.LHS);
            _rhs = Expr.Create(astExpr.RHS);
        }

        public override LeyValue CalculateValue() {
            return DoOperation();
        }

        protected T ExprToT<T>(Expr expr) where T : LeyValue {
            var value = expr.CalculateValue() as T;

            if(value == null)
                throw new LeyException("Binary operation had unsuitable arguments.");

            return value;
        }

        public static BinaryExpr Create(AST.BinaryExprNode astExpr) {
            switch(astExpr.Operation) {
                case "+":
                    return new BinaryExprAdd(astExpr);
                case "-":
                    return new BinaryExprSubtract(astExpr);
                case "*":
                    return new BinaryExprMultiply(astExpr);
                case "/":
                    return new BinaryExprDivide(astExpr);
                case "=":
                    return new BinaryExprAssign(astExpr);
                case "==":
                case "!=":
                    return new BinaryExprEqual(astExpr,astExpr.Operation == "==");
                case ">":
                case "<":
                case ">=":
                case "<=":
                    return new BinaryExprInequality(astExpr);
                case "&&":
                    return new BinaryExprAnd(astExpr);
                case "||":
                    return new BinaryExprOr(astExpr);
            }

            throw new LeyException($"Unrecognised binary operation '{astExpr.Operation}'.");
        }

        protected abstract LeyValue DoOperation();
    }

    public class BinaryExprAdd : BinaryExpr {
        public BinaryExprAdd(AST.BinaryExprNode astExpr) : base(astExpr) { }

        protected override LeyValue DoOperation() {
            var lhsVal = _lhs.CalculateValue();
            var rhsVal = _rhs.CalculateValue();

            if (lhsVal is LeyNumber lhsNum && rhsVal is LeyNumber rhsNum) {
                return new LeyNumber(
                    lhsNum.Value + rhsNum.Value
                );
            }

            if(lhsVal is LeyString lhsStr && rhsVal is LeyString rhsStr) {
                return new LeyString(
                    lhsStr.Value + rhsStr.Value
                );
            }

            throw new LeyException("Binary operation had unsuitable arguments.");
        }
    }


    public class BinaryExprSubtract : BinaryExpr {
        public BinaryExprSubtract(AST.BinaryExprNode astExpr) : base(astExpr) { }

        protected override LeyValue DoOperation() {
            return new LeyNumber(
                ExprToT<LeyNumber>(_lhs).Value
                -
                ExprToT<LeyNumber>(_rhs).Value
            );
        }
    }

    public class BinaryExprMultiply : BinaryExpr {
        public BinaryExprMultiply(AST.BinaryExprNode astExpr) : base(astExpr) { }

        protected override LeyValue DoOperation() {
            return new LeyNumber(
                ExprToT<LeyNumber>(_lhs).Value
                *
                ExprToT<LeyNumber>(_rhs).Value
            );
        }
    }

    public class BinaryExprDivide : BinaryExpr {
        public BinaryExprDivide(AST.BinaryExprNode astExpr) : base(astExpr) { }

        protected override LeyValue DoOperation() {
            return new LeyNumber(
                ExprToT<LeyNumber>(_lhs).Value
                /
                ExprToT<LeyNumber>(_rhs).Value
            );
        }
    }

    public class BinaryExprAssign : BinaryExpr {
        public BinaryExprAssign(AST.BinaryExprNode astExpr) : base(astExpr) { }

        protected override LeyValue DoOperation() {
            var varName = (_lhs as VarExpr)?.VarName;
            var rhsVal = _rhs.CalculateValue();

            if(varName == null)
                throw new LeyException("Must assign to a variable.");

            var leyVar = Lookup.Instance.Vars.GetVar(varName);

            if(!rhsVal.IsLeyType(leyVar.Type))
                throw new LeyException("Type mismatch in variable assignment.");

            leyVar.Set(rhsVal);
            return rhsVal;
        }
    }

    public class BinaryExprEqual : BinaryExpr {
        private bool _returnTrueIfEqual;
        public BinaryExprEqual(AST.BinaryExprNode astExpr, bool returnTrueIfEqual) : base(astExpr) {
            _returnTrueIfEqual = returnTrueIfEqual;
        }

        protected override LeyValue DoOperation() {
            var lhsVal = _lhs.CalculateValue();
            var rhsVal = _rhs.CalculateValue();

            if (lhsVal is LeyBool lhsBool && rhsVal is LeyBool rhsBool)
                return new LeyBool((lhsBool.Value == rhsBool.Value) == _returnTrueIfEqual);
            if (lhsVal is LeyNumber lhsNum && rhsVal is LeyNumber rhsNum)
                return new LeyBool((lhsNum.Value == rhsNum.Value) == _returnTrueIfEqual);
            if (lhsVal is LeyString lhsStr && rhsVal is LeyString rhsStr)
                return new LeyBool((lhsStr.Value == rhsStr.Value) == _returnTrueIfEqual);

            throw new LeyException("Binary operation had unsuitable arguments.");
        }
    }

    public class BinaryExprInequality : BinaryExpr {
        private string _op;
        public BinaryExprInequality(AST.BinaryExprNode astExpr) : base(astExpr) {
            _op = astExpr.Operation;
        }

        protected override LeyValue DoOperation() {
            var lhsNum = ExprToT<LeyNumber>(_lhs).Value;
            var rhsNum = ExprToT<LeyNumber>(_rhs).Value;

            switch(_op) {
                case ">":
                    return new LeyBool(lhsNum > rhsNum);
                case "<":
                    return new LeyBool(lhsNum < rhsNum);
                case ">=":
                    return new LeyBool(lhsNum >= rhsNum);
                case "<=":
                    return new LeyBool(lhsNum <= rhsNum);
            }

            throw new LeyException("Binary operation had unsuitable arguments.");
        }
    }

    public class BinaryExprAnd : BinaryExpr {
        public BinaryExprAnd(AST.BinaryExprNode astExpr) : base(astExpr) { }

        protected override LeyValue DoOperation() {
            var lhsVal = ExprToT<LeyBool>(_lhs);
            var rhsVal = ExprToT<LeyBool>(_rhs);

            return new LeyBool(lhsVal.Value && rhsVal.Value);
        }
    }

    public class BinaryExprOr : BinaryExpr {
        public BinaryExprOr(AST.BinaryExprNode astExpr) : base(astExpr) { }

        protected override LeyValue DoOperation() {
            var lhsVal = ExprToT<LeyBool>(_lhs);
            var rhsVal = ExprToT<LeyBool>(_rhs);

            return new LeyBool(lhsVal.Value || rhsVal.Value);
        }
    }
}
