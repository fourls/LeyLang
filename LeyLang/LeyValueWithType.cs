using System;
using System.Collections.Generic;
using System.Text;

namespace LeyLang {
    public class LeyValueWithType {
        public string Type { get; }
        public LeyValue Value { get; private set; }

        public LeyValueWithType(string varType) {
            Type = varType;
            Value = LeyTypeUtility.DefaultOfType(varType);
        }

        public LeyValueWithType(string varType, LeyValue value) {
            Type = varType;

            if (value != null && value.IsLeyType(Type)) {
                Value = value;
            } else {
                Value = LeyTypeUtility.DefaultOfType(varType);
            }
        }

        public void Set(LeyValue newValue) {
            if(newValue.IsLeyType(Type)) {
                Value = newValue;
            }
        }
    }
}
