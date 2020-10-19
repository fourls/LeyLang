using System;
using System.Collections.Generic;
using System.Text;

namespace LeyLang.Exec {
    public class CallExpr : Expr {
        private string _funcName;
        private Expr[] _args;

        private CallExpr(AST.CallExprNode astCallExpr) {
            _funcName = astCallExpr.MethodName;
            _args = new Expr[astCallExpr.Arguments.Count];

            for(int i = 0; i < astCallExpr.Arguments.Count; i++) {
                _args[i] = Expr.Create(astCallExpr.Arguments[i]);
            }
        }

        public static CallExpr Create(AST.CallExprNode astCallExpr) {
            return new CallExpr(astCallExpr);
        }

        public override LeyValue CalculateValue() {
            var funcRefValue = Lookup.Instance.Vars.GetVar(_funcName).Value;

            if (funcRefValue.PrimitiveType != LeyPrimitiveType.FunctionReference)
                throw new LeyException("Function call attempted on non-function variable.");

            int funcRef = (funcRefValue as LeyFuncRef).Value;

            LeyFunc func = Lookup.Instance.Funcs[funcRef];

            if (func == null)
                throw new LeyException("Function was not defined.");

            LeyValue[] calcedArgs = new LeyValue[_args.Length];
            for(int i = 0; i < _args.Length; i++) {
                calcedArgs[i] = _args[i].CalculateValue();
            }

            return func.Invoke(calcedArgs);
        }
    }
}
