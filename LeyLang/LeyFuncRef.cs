using System;
using System.Collections.Generic;
using System.Text;

namespace LeyLang {
    class LeyFuncRef : LeyValue {
        public int Value { get; set; }

        public LeyFuncRef(int value=0) : base(LeyPrimitiveType.FunctionReference,LeyTypeUtility.FuncRef) {
            Value = value;
        }

        public override string ToString() {
            return Value.ToString();
        }
    }
}
