using System;
using System.Collections.Generic;
using System.Text;

namespace LeyLang.Exec {
    public abstract class ClassStructure {
        public abstract void Execute(LeyClass klass);

        public static ClassStructure Create(AST.StatementNode stmt) {
            if (stmt is AST.FuncStatementNode funcStmt)
                return FuncClassStructure.Create(funcStmt);
            if (stmt is AST.VarDeclarationStatementNode varStmt)
                return VarDeclarationClassStructure.Create(varStmt);

            throw new Exception("Unknown AST class structure node encountered.");
        }
    }
}
