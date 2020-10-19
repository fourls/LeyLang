using System;
using System.Collections.Generic;
using System.Text;

namespace LeyLang {
    public static class LeyTypeUtility {
        public static string Number = "number";
        public static string Bool = "bool";
        public static string String = "string";
        public static string FuncRef = "funcref";
        public static string Undefined = "undefined";
        public static string Void = "void";
        public static string Object(LeyClass klass) => klass.Name;

        public static bool IsTypeValid(string type) {
            return (
                type == Number ||
                type == Bool ||
                type == String ||
                type == FuncRef ||
                Lookup.Instance.CustomClasses.ContainsKey(type)
            );
        }

        public static LeyValue DefaultOfType(string type) {
            if (type == Number || type == Bool || type == String || type == FuncRef)
                return NewInstanceOfType(type);
            else
                return null;
        }

        public static LeyValue NewInstanceOfType(string type) {
            if (type == Number) return new LeyNumber();
            if (type == Bool) return new LeyBool();
            if (type == String) return new LeyString();
            if (type == FuncRef) return new LeyFuncRef();

            if(Lookup.Instance.CustomClasses.ContainsKey(type)) {
                return new LeyObject(Lookup.Instance.CustomClasses[type]);
            }

            throw new LeyException("Cannot create instance of non-existent Ley type.");
        }
    }
}
