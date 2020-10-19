using System;
using System.Collections.Generic;
using System.Text;

namespace LeyLang {
    class LeyString : LeyValue {
        public LeyString(string value="") : base(LeyPrimitiveType.String,LeyTypeUtility.String) {
            Value = value;
        }

        public override string ToString() {
            return Value;
        }

        public string Value { get; set; }

        public override object ConvertToExternType(Type externType) {
            if (externType == typeof(string))
                return Value;

            return base.ConvertToExternType(externType);
        }
    }
}
