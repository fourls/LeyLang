using System;
using System.Collections.Generic;
using System.Text;

namespace LeyLang.Exec {
    class ConditionalBlock : Node {
        private Expr _condition;
        private Block _block;

        private ConditionalBlock(AST.ConditionalBlockNode astCondBlock) {
            _condition = astCondBlock.Expr == null ? null : Expr.Create(astCondBlock.Expr);
            _block = Block.Create(astCondBlock.Block);
        }

        public bool Execute() {
            bool valid = true;

            if (_condition == null) {
                valid = true;
            } else {
                var condResult = _condition.CalculateValue() as LeyBool;
                if (condResult == null)
                    throw new LeyException("Condition invalid.");

                valid = condResult.Value;
            }

            if (valid) {
                _block.Execute();
                return true;
            }
            return false;
        }

        public static ConditionalBlock Create(AST.ConditionalBlockNode astCondBlock) {
            return new ConditionalBlock(astCondBlock);
        }
    }
}
