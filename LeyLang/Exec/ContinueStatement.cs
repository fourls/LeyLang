using System;
using System.Collections.Generic;
using System.Text;

namespace LeyLang.Exec {
    class ContinueStatement : Statement {
        private ContinueStatement() {

        }

        public override void Execute() {
            throw new ContinueException();
        }

        public static ContinueStatement Create(AST.ContinueStatementNode astCont) {
            return new ContinueStatement();
        }
    }
}
