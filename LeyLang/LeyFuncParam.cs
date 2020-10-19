using System;
using System.Collections.Generic;
using System.Text;

namespace LeyLang {
    public class LeyFuncParam {
        public string ParamName { get; }
        public string ParamType { get; }

        public LeyFuncParam(string name, string type) {
            ParamName = name;
            ParamType = type;
        }
    }
}
