using System;
using System.Collections.Generic;
using System.Text;

namespace LeyLang.Exec {
    class FuncPrototype : Prototype {
        private FuncPrototype(AST.FuncPrototypeNode astProto) : base(astProto) {
            
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
