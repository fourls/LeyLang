using System;
using System.Collections.Generic;
using System.Text;

namespace LeyLang.Exec {
    class VarDeclarationClassStructure : ClassStructure {
        private string _varName;
        private string _varType;
        private Expr _expr;

        private VarDeclarationClassStructure(AST.VarDeclarationStatementNode astDeclStmt) {
            _varName = astDeclStmt.Declaration.VarName;
            _varType = astDeclStmt.Declaration.VarType;
            _expr = astDeclStmt.Expression == null ? null : Expr.Create(astDeclStmt.Expression);
        }

        public override void Execute(LeyClass klass) {
            klass.InstanceVars.Add(_varName, new LeyInstanceVarInfo(_varType, _expr.CalculateValue()));
        }

        public static VarDeclarationClassStructure Create(AST.VarDeclarationStatementNode astDeclStmt) {
            return new VarDeclarationClassStructure(astDeclStmt);
        }
    }
}
