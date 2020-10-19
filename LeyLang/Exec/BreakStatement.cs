using System;
using System.Collections.Generic;
using System.Text;

namespace LeyLang.Exec {
    class BreakStatement : Statement {

        private BreakStatement() {

        }

        public override void Execute() {
            throw new BreakException();
        }

        public static BreakStatement Create(AST.BreakStatementNode astBreak) {
            return new BreakStatement();
        }
    }
}
