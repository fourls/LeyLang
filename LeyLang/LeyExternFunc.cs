using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace LeyLang {
    public class LeyExternFunc : LeyFunc {
        private static Dictionary<Type, string> _externToLeyType = new Dictionary<Type, string>() {
            [typeof(decimal)] = "number",
            [typeof(string)] = "string",
            [typeof(bool)] = "bool",
        };

        private Func<LeyValue[], LeyValue> _func;
        public LeyExternFunc(LeyFuncParam[] parameters, string returnValue, Func<LeyValue[], LeyValue> func) : base(parameters, returnValue) {
            _func = func;
        }

        protected override LeyValue OnInvoke(LeyValue[] arguments) {
            return _func.Invoke(arguments);
        }

        private static string ExternToLeyType(Type externType) {
            return _externToLeyType.ContainsKey(externType) ? _externToLeyType[externType] : externType.Name;
        }

        public static LeyExternFunc GenerateLeyFunc(MethodInfo externMethod, object target=null) {
            string returnValue = ExternToLeyType(externMethod.ReturnType);
            var externParams = externMethod.GetParameters();

            var leyParams = new LeyFuncParam[externParams.Length];

            for (int i = 0; i < externParams.Length; i++) {
                leyParams[i] = new LeyFuncParam(
                    externParams[i].Name,
                    ExternToLeyType(externParams[i].ParameterType)
                );
            }

            Func<LeyValue[], LeyValue> func = (leyArgs) => {
                object[] externArgs = new object[leyArgs.Length];

                for (int i = 0; i < leyArgs.Length; i++) {
                    externArgs[i] = leyArgs[i].ConvertToExternType(externParams[i].ParameterType);
                }
                
                object result = externMethod.Invoke(target, externArgs);

                if (result is decimal d)
                    return new LeyNumber(d);
                if (result is string s)
                    return new LeyString(s);
                if (result is bool b)
                    return new LeyBool(b);
                else
                    return null;
            };

            return new LeyExternFunc(leyParams, returnValue, func);
        }

        public static LeyExternFunc GenerateLeyFunc(Delegate dele, object target) {
            return GenerateLeyFunc(dele.GetMethodInfo(), target);
        }
    }
}
