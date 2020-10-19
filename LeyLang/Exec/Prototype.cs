using System;
using System.Collections.Generic;
using System.Text;

namespace LeyLang.Exec {
    public abstract class Prototype {
        protected string _funcName;
        protected string _returnType;
        protected LeyFuncParam[] _params;

        protected Prototype(AST.FuncPrototypeNode astProto) {
            _funcName = astProto.ProtoName;
            _returnType = astProto.ReturnType;

            _params = new LeyFuncParam[astProto.Parameters.Count];

            for (int i = 0; i < astProto.Parameters.Count; i++) {
                _params[i] = new LeyFuncParam(astProto.Parameters[i].ParamName, astProto.Parameters[i].ParamType);
            }
        }
    }
}
