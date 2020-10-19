using System;
using System.Collections.Generic;
using System.Text;

namespace LeyLang {
    public class LeyClass {
        public string Name { get; }
        public Dictionary<string, string> InstanceVarTypes { get; }
        public Dictionary<string,LeyFunc> Methods { get; }
    }
}
