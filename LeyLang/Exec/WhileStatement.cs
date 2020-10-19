using System;
using System.Collections.Generic;
using System.Text;

namespace LeyLang.Exec {
    class WhileStatement : Statement {
        private Expr _condition;
        private Block _block;

        private WhileStatement(AST.WhileStatementNode astWhile) {
            _condition = Expr.Create(astWhile.Condition);
            _block = Block.Create(astWhile.Block);
        }

        public override void Execute() {
            LeyBool condValue = _condition.CalculateValue() as LeyBool;

            while (condValue != null && condValue.Value == true) {
                try {
                    _block.Execute();
                } catch (BreakException) {
                    break;
                } catch (ContinueException) {

                }

                condValue = _condition.CalculateValue() as LeyBool;
            }

            if (condValue == null)
                throw new LeyException("For clause condition was not a boolean.");
        }

        public static WhileStatement Create(AST.WhileStatementNode astWhile) {
            return new WhileStatement(astWhile);
        }
    }
}
