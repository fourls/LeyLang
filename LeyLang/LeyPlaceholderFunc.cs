using LeyLang.Exec;
using System;
using System.Collections.Generic;
using System.Text;

namespace LeyLang {
    class LeyPlaceholderFunc : LeyFunc {
        public LeyPlaceholderFunc(LeyFuncParam[] parameters, string returnValue) : base(parameters, returnValue) {
        }

    }
}
