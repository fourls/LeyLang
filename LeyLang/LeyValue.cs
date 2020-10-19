using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;

namespace LeyLang {
    public abstract class LeyValue {
        public LeyPrimitiveType PrimitiveType { get; }
        public string Type { get; }
        public LeyValue(LeyPrimitiveType primitiveType, string type) {
            PrimitiveType = primitiveType;
            Type = type;
        }

        public virtual bool IsLeyType(string typeName) {
            return Type == typeName;
        }

        public virtual object ConvertToExternType(Type externType) {
            throw new Exception("Cannot convert to extern type.");
        }
    }

    public enum LeyPrimitiveType {
        Number,
        String,
        Boolean,
        ObjectReference,
        FunctionReference
    }
}
