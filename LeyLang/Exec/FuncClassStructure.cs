using System;
using System.Collections.Generic;
using System.Text;

namespace LeyLang.Exec {
    public class FuncClassStructure : ClassStructure {
        private string _funcName;
        private string _returnType;
        private LeyFuncParam[] _leyParams;
        private Block _block;

        private FuncClassStructure(AST.FuncStatementNode astFunc) {
            _funcName = astFunc.Prototype.ProtoName;
            _returnType = astFunc.Prototype.ReturnType;

            _leyParams = new LeyFuncParam[astFunc.Prototype.Parameters.Count];
            
            for(int i = 0; i < _leyParams.Length; i++) {
                _leyParams[i] = new LeyFuncParam(astFunc.Prototype.Parameters[i].ParamName, astFunc.Prototype.Parameters[i].ParamType);
            }

            _block = Block.Create(astFunc.Body);    
        }

        public override void Execute(LeyClass klass) {
            klass.Methods.Add(_funcName,new LeyCustomFunc(_leyParams, _returnType, _block));
        }

        public static FuncClassStructure Create(AST.FuncStatementNode astFunc) {
            return new FuncClassStructure(astFunc);
        }
    }
}
