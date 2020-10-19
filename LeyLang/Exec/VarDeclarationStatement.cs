using System;
using System.Collections.Generic;
using System.Text;

namespace LeyLang.Exec {
    class VarDeclarationStatement : Statement {
        private VarDeclaration _declaration;
        private Expr _expr;
        private string _varName;

        private VarDeclarationStatement(AST.VarDeclarationStatementNode astDeclStmt) {
            _varName = astDeclStmt.Declaration.VarName;
            _declaration = VarDeclaration.Create(astDeclStmt.Declaration);
            _expr = astDeclStmt.Expression == null ? null : Expr.Create(astDeclStmt.Expression);
        }

        public override void Execute() {
            // declare the variable
            _declaration.Execute();

            // set the variable value
            var value = _expr.CalculateValue();
            if (value == null)
                throw new LeyException("Attempted assignment of variable to undefined value.");

            ExecLookup.Instance.Vars.GetVar(_varName).Set(value);
        }

        public static VarDeclarationStatement Create(AST.VarDeclarationStatementNode astDeclStmt) {
            return new VarDeclarationStatement(astDeclStmt);
        }
    }
}
