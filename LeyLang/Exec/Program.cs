using System;
using System.Collections.Generic;
using System.Text;

namespace LeyLang.Exec {
    public class Program : Node {
        private Block _block;

        private Program(AST.ProgramNode astProgram) {
            _block = Block.Create(astProgram.Main);
        }

        public static Program Create(AST.ProgramNode astProgram) {
            return new Program(astProgram);
        }

        public void Execute() {
            try {
                _block.Execute();
            } catch(FakeException) {
                throw new LeyException("Unhandled return, break or continue.");
            }
        }
    }
}
