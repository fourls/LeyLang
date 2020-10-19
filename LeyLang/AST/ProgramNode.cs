using System;
using System.Collections.Generic;
using System.Text;

namespace LeyLang.AST {
    public class ProgramNode : Node {
        public ProgramNode(BlockNode main) {
            Main = main;
        }

        public BlockNode Main { get; }


        protected override Node[] ChildNodes => new Node[] { Main };
    }
}
