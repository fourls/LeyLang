using System;
using System.Collections.Generic;
using System.Text;

namespace LeyLang.Exec {
    class ConditionalStatement : Statement {
        private ConditionalBlock[] _ifs;

        private ConditionalStatement(AST.ConditionalStatementNode astCondStmt) {
            _ifs = new ConditionalBlock[astCondStmt.Ifs.Length];

            for(int i = 0; i < astCondStmt.Ifs.Length; i++) {
                _ifs[i] = ConditionalBlock.Create(astCondStmt.Ifs[i]);
            }
        }

        public override void Execute() {
            for(int i = 0; i < _ifs.Length; i++) {
                if (_ifs[i].Execute())
                    return;
            }
        }

        public static ConditionalStatement Create(AST.ConditionalStatementNode astCondStmt) {
            return new ConditionalStatement(astCondStmt);
        }
    }
}
