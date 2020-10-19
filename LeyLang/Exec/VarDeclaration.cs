using System;
using System.Collections.Generic;
using System.Text;

namespace LeyLang.Exec {
    class VarDeclaration {
        private string _varName;
        private string _varType;
        
        private VarDeclaration(AST.VarDeclarationNode astDecl) {
            _varName = astDecl.VarName;
            _varType = astDecl.VarType;
        }

        public void Execute() {
            if (!LeyTypeUtility.IsTypeValid(_varType))
                throw new LeyException($"Variable '{_varName}' has nonexistent type '{_varType}'.");

            Lookup.Instance.Vars.DeclareVar(_varName, _varType);
        }

        public static VarDeclaration Create(AST.VarDeclarationNode astDecl) {
            return new VarDeclaration(astDecl);
        }
    }
}
