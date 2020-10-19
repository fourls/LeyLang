using LeyLang.Exec;
using System;
using System.Collections.Generic;
using System.Text;

namespace LeyLang {
    public class LeyCustomFunc : LeyFunc {
        private Block _block;

        public LeyCustomFunc(LeyFuncParam[] parameters, string returnValue, Block block) : base(parameters,returnValue) {
            _block = block;
        }

        public void SetBlock(Block block) {
            _block = block;
        }

        protected override LeyValue OnInvoke(LeyValue[] arguments) {
            try {
                if (_block != null) {
                    _block.Execute(GetInjectedVars(arguments));
                }
            } catch (ReturnException e) {
                return e.ReturnValue;
            }

            return null;
        }

        private Dictionary<string,LeyValue> GetInjectedVars(LeyValue[] arguments) {
            var injectedVars = new Dictionary<string, LeyValue>();

            for(int i = 0; i < arguments.Length; i++) {
                injectedVars.Add(Params[i].ParamName, arguments[i]);
            }

            return injectedVars;
        }
    }
}
