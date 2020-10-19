using System;
using System.Collections.Generic;
using System.Text;

namespace LeyLang.Exec {
    class FuncPrototype : Node {
        private string _funcName;
        private string _returnType;
        private LeyFuncParam[] _params;

        private FuncPrototype(AST.FuncPrototypeNode astProto) {
            _funcName = astProto.FuncName;
            _returnType = astProto.ReturnType;

            _params = new LeyFuncParam[astProto.Parameters.Count];

            for(int i = 0; i < astProto.Parameters.Count; i++) {
                _params[i] = new LeyFuncParam(astProto.Parameters[i].ParamName, astProto.Parameters[i].ParamType);
            }
        }

        public int Execute() {
            int index = Lookup.Instance.DeclareFunc(_funcName);
            Lookup.Instance.Funcs[index] = new LeyPlaceholderFunc(_params, _returnType);

            return index;
        }

        public static FuncPrototype Create(AST.FuncPrototypeNode astProto) {
            return new FuncPrototype(astProto);
        }
    }
}
