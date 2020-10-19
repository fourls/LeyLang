using System;
using System.Collections.Generic;
using System.Text;

namespace LeyLang.Exec {
    class ClassStatement : Statement {
        private string _className;
        private ClassBlock _block;

        private ClassStatement(AST.ClassStatementNode astStmt) {
            _className = astStmt.ClassName;

            _block = ClassBlock.Create(astStmt.Body);
        }

        public override void Execute() {
            var klass = new LeyClass(_className);

            _block.Execute(klass);

            Lookup.Instance.DeclareClass(klass);
        }

        public static ClassStatement Create(AST.ClassStatementNode astStmt) {
            return new ClassStatement(astStmt);
        }
    }
}
