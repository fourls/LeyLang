using System;
using System.Collections.Generic;
using System.Text;

namespace LeyLang {
    public abstract class LeyFunc {
        public LeyFunc(LeyFuncParam[] parameters, string returnValue) {
            Params = parameters;
            ReturnValue = returnValue;
        }

        public LeyFuncParam[] Params { get; }
        public string ReturnValue { get; }

        public LeyValue Invoke(LeyValue[] arguments) {
            Validate(arguments);

            var result = OnInvoke(arguments);

            return result;
        }

        protected virtual LeyValue OnInvoke(LeyValue[] arguments) {
            return LeyTypeUtility.DefaultOfType(ReturnValue);
        }

        private void Validate(LeyValue[] arguments) {
            if (Params.Length != arguments.Length)
                throw new LeyException($"Ley func expected {Params.Length} arguments, but received {arguments.Length}.");

            for(int i = 0; i < Params.Length; i++) {
                if (!arguments[i].IsLeyType(Params[i].ParamType))
                    throw new LeyException($"Ley func expected type {Params[i].ParamType}, but received incompatible type (primitive: {arguments[i].PrimitiveType}).");
            }
        }

        public LeyCustomFunc ToCustomFunc(Exec.Block block) {
            return new LeyCustomFunc(Params, ReturnValue, block);
        }

        public LeyExternFunc ToExternFunc(Func<LeyValue[], LeyValue> externFunc) {
            return new LeyExternFunc(Params, ReturnValue, externFunc);
        }
    }
}
