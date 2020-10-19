using System;
using System.Collections.Generic;
using System.Text;

namespace LeyLang.Exec {
    public class Block : Node {
        Statement[] _statements;
        private Block(AST.BlockNode astBlock) {
            _statements = new Statement[astBlock.Statements.Count];
            for(int i = 0; i < _statements.Length; i++) {
                _statements[i] = Statement.Create(astBlock.Statements[i]);
            }
        }

        public static Block Create(AST.BlockNode astBlock) {
            return new Block(astBlock);
        }

        private void ExecuteStatements() {
            for(int i = 0; i < _statements.Length; i++) {
                _statements[i].Execute();
            }
        }

        public void Execute() {
            Lookup.Instance.Vars.EnterScope();

            try {
                ExecuteStatements();
            } catch(FakeException e) {
                Lookup.Instance.Vars.ExitScope();
                throw e;
            }

            Lookup.Instance.Vars.ExitScope();
        }

        public void ExecuteScopeless() {
            ExecuteStatements();
        }

        public void Execute(Dictionary<string,LeyValue> injectedVars) {
            Lookup.Instance.Vars.EnterScope();

            foreach(var kvp in injectedVars) {
                Lookup.Instance.Vars.DeclareVar(kvp.Key, kvp.Value.Type, kvp.Value);
            }

            try {
                ExecuteStatements();
            }
            catch (FakeException e) {
                Lookup.Instance.Vars.ExitScope();
                throw e;
            }

            Lookup.Instance.Vars.ExitScope();
        }
    }
}
