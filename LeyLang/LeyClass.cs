using System;
using System.Collections.Generic;
using System.Text;

namespace LeyLang {
    public class LeyClass {
        public string Name { get; }
        public Dictionary<string, LeyInstanceVarInfo> InstanceVars { get; }
        public Dictionary<string,LeyFunc> Methods { get; }
        public LeyFunc Constructor { get; }

        public LeyClass(string name) {
            Name = name;

            InstanceVars = new Dictionary<string, LeyInstanceVarInfo>();
            Methods = new Dictionary<string, LeyFunc>();

            Constructor = new LeyExternFunc(new LeyFuncParam[0], name, (args) => Instantiate());
        }

        private LeyObject Instantiate() {
            LeyObject leyObj = new LeyObject(this);

            return leyObj;
        }

        public LeyValue CallMethod(LeyObject leyObj, LeyValue[] args, string methodName) {
            LeyValue[] modArgs = new LeyValue[args.Length + 1];
            modArgs[0] = leyObj;
            args.CopyTo(modArgs, 1);

            return Methods[methodName].Invoke(modArgs);
        }
    }

    public class LeyInstanceVarInfo {
        public LeyInstanceVarInfo(string type, LeyValue defaultValue) {
            Type = type;
            DefaultValue = defaultValue;
        }

        public string Type { get; }
        public LeyValue DefaultValue { get; }
    }
}
