using System;
using System.Collections.Generic;
using System.Text;

namespace LeyLang {
    public class LeyObject : LeyValue {
        public LeyObject(LeyClass klass) : base(LeyPrimitiveType.ObjectReference,klass.Name) {
            Klass = klass;

            _instanceVars = new Dictionary<string, LeyValueWithType>();
            foreach(KeyValuePair<string,LeyInstanceVarInfo> kvp in klass.InstanceVars) {
                _instanceVars.Add(kvp.Key, new LeyValueWithType(kvp.Value.Type,kvp.Value.DefaultValue));
            }
        }

        public override bool IsLeyType(string typeName) {
            return Klass.Name == typeName;
        }

        public LeyClass Klass { get; }
        private Dictionary<string,LeyValueWithType> _instanceVars { get; }

        public LeyValueWithType GetVarWithType(string varName) {
            return _instanceVars[varName];
        }
        public T GetVar<T>(string varName) where T : LeyValue {
            return _instanceVars[varName].Value as T;
        }

        public void SetVar(string varName, LeyValue varValue) {
            _instanceVars[varName].Set(varValue);
        }

        public LeyValue CallMethod(string methodName, LeyValue[] args) {
            return Klass.CallMethod(this, args,methodName);
        }
    }
}
