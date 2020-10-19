using System;
using System.Collections.Generic;
using System.Text;

namespace LeyLang {
    public class LeyObject : LeyValue {
        public LeyObject(LeyClass klass) : base(LeyPrimitiveType.ObjectReference,klass.Name) {
            Klass = klass;

            InstanceVars = new Dictionary<string, LeyValueWithType>();
            foreach(KeyValuePair<string,string> kvp in klass.InstanceVarTypes) {
                InstanceVars.Add(kvp.Key, new LeyValueWithType(kvp.Value));
            }
        }

        public override bool IsLeyType(string typeName) {
            return Klass.Name == typeName;
        }

        public LeyClass Klass { get; }
        public Dictionary<string,LeyValueWithType> InstanceVars { get; }
    }
}
