using System;
using System.Collections.Generic;
using System.Text;

namespace LeyLang {
    public class LeyClass {
        public string Name { get; }
        public Dictionary<string, string> InstanceVarTypes { get; }
        public Dictionary<string,LeyFunc> Methods { get; }
        public LeyFunc Constructor { get; }

        public LeyClass(string name, Dictionary<string,string> instanceVarTypes, LeyFunc ctor, Dictionary<string,LeyFunc> methods) {
            Name = name;
            InstanceVarTypes = instanceVarTypes;
            Methods = methods;
            Constructor = ctor;
        }

        public LeyObject Instantiate(LeyValue[] args) {
            LeyObject leyObj = new LeyObject(this);
            
            LeyValue[] modArgs = new LeyValue[] { leyObj };
            args.CopyTo(modArgs, 1);

            return Constructor.Invoke(modArgs) as LeyObject;
        }

        public LeyValue CallMethod(LeyObject leyObj, LeyValue[] args, string methodName) {
            LeyValue[] modArgs = new LeyValue[] { leyObj };
            args.CopyTo(modArgs, 1);

            return Methods[methodName].Invoke(modArgs);
        }
    }
}
