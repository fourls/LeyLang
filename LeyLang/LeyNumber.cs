using System;
using System.Collections.Generic;
using System.Text;

namespace LeyLang {
    public class LeyNumber : LeyValue {
        public LeyNumber(decimal value=0) : base(LeyPrimitiveType.Number,LeyTypeUtility.Number) {
            Value = value;
        }

        public decimal Value { get; set; }

        public override string ToString() {
            return Value.ToString();
        }

        public override object ConvertToExternType(Type externType) {
            if (externType == typeof(decimal))
                return Value;
            if (externType == typeof(int))
                return (int)Value;
            if (externType == typeof(float))
                return (float)Value;
            if (externType == typeof(double))
                return (double)Value;

            return base.ConvertToExternType(externType);
        }
    }
}
