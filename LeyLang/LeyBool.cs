using System;
using System.Collections.Generic;
using System.Text;

namespace LeyLang {
    class LeyBool : LeyValue {
        public LeyBool(bool value = false) : base(LeyPrimitiveType.Boolean,"bool") {
            Value = value;
        }

        public bool Value { get; set; }

        public override object ConvertToExternType(Type externType) {
            if (externType == typeof(bool))
                return Value;

            return base.ConvertToExternType(externType);
        }
    }
}
