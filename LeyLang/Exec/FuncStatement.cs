using System;
using System.Collections.Generic;
using System.Text;

namespace LeyLang.Exec {
    class FuncStatement : Statement {
        private FuncPrototype _proto;
        private Block _body;

        private FuncStatement(AST.FuncStatementNode astStmt) {
            _proto = FuncPrototype.Create(astStmt.Prototype);
            _body = astStmt.Body == null ? null : Block.Create(astStmt.Body);
        }

        public override void Execute() {
            int index = _proto.Execute();

            if(_body != null) {
                LeyFunc func = ExecLookup.Instance.Funcs[index];
                ExecLookup.Instance.Funcs[index] = func.ToCustomFunc(_body);
            }
        }

        public static FuncStatement Create(AST.FuncStatementNode astStmt) {
            return new FuncStatement(astStmt);
        }
    }
}
