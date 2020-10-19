namespace LeyLang.Exec {
    public class ClassBlock {
        ClassStructure[] _statements;
        private ClassBlock(AST.BlockNode astBlock) {
            _statements = new ClassStructure[astBlock.Statements.Count];

            for (int i = 0; i < _statements.Length; i++) {
                _statements[i] = ClassStructure.Create(astBlock.Statements[i]);
            }
        }

        public static ClassBlock Create(AST.BlockNode astBlock) {
            return new ClassBlock(astBlock);
        }
        public void Execute(LeyClass klass) {
            for (int i = 0; i < _statements.Length; i++) {
                _statements[i].Execute(klass);
            }
        }
    }
}