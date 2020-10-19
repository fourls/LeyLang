using System;
using System.Collections.Generic;
using System.Text;

namespace LeyLang.AST {
    class ExternCallExprNode : ExprNode {
        private Func<VarManagement,LeyValue> _externFunc { get; }

        public ExternCallExprNode(Func<VarManagement, LeyValue> externFunc) {
            _externFunc = externFunc;
        }
    }
}
