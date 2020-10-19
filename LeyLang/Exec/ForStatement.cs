using System;
using System.Collections.Generic;
using System.Text;

namespace LeyLang.Exec {
    class ForStatement : Statement {
        VarDeclarationStatement _initialiser;
        Expr _condition;
        Expr _increment;
        Block _block;

        private ForStatement(AST.ForStatementNode astFor) {
            _initialiser = VarDeclarationStatement.Create(astFor.Initialiser);
            _condition = Expr.Create(astFor.Condition);
            _increment = Expr.Create(astFor.Increment);
            _block = Block.Create(astFor.Block);
        }

        public override void Execute() {
            _initialiser.Execute();

            LeyBool condValue = _condition.CalculateValue() as LeyBool;

            while (condValue != null && condValue.Value == true) {
                try {
                    _block.Execute();
                } catch(BreakException) {
                    break;
                } catch(ContinueException) {

                }

                _increment.CalculateValue();
                condValue = _condition.CalculateValue() as LeyBool;
            }

            if (condValue == null)
                throw new LeyException("For clause condition was not a boolean.");
        }

        public static ForStatement Create(AST.ForStatementNode astFor) {
            return new ForStatement(astFor);
        }
    }
}
