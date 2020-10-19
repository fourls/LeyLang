using LeyLang.AST;
using System;
using System.Collections.Generic;
using System.Text;

namespace LeyLang {
    public static class Executor {
        private static void GenerateStdLib() {
            Lookup.ResetInstance();
            Lookup lookup = Lookup.Instance;

            lookup.DeclareFunc(
                "WriteStr",
                LeyExternFunc.FastCreateExtern(typeof(Console).GetMethod("WriteLine", new Type[] { typeof(string) }))
            );

            lookup.DeclareFunc(
                "ReadStr",
                LeyExternFunc.FastCreateExtern(typeof(Console).GetMethod("ReadLine", new Type[0]))
            );

            Func<decimal, string> numToStr = x => x.ToString();
            lookup.DeclareFunc(
                "NumToStr",
                LeyExternFunc.FastCreateExtern(numToStr, numToStr.Target)
            );

            Func<string, decimal> strToNum = x => decimal.Parse(x);
            lookup.DeclareFunc(
                "StrToNum",
                LeyExternFunc.FastCreateExtern(strToNum, strToNum.Target)
            );

            Func<bool, string> boolToStr = x => x.ToString();
            lookup.DeclareFunc(
                "BoolToStr",
                LeyExternFunc.FastCreateExtern(boolToStr, boolToStr.Target)
            );

            lookup.DeclareFunc(
                "DebugVars",
                new LeyExternFunc(
                    new LeyFuncParam[0],
                    LeyTypeUtility.Undefined,
                    (args) => {
                        Lookup.Instance.Vars.PrettyPrint();

                        return null;
                    }
                )
            );
        }

        public static void Execute(ProgramNode astRoot) {
            Exec.Program program = Exec.Program.Create(astRoot);
            GenerateStdLib();
            program.Execute();
        }
    }
}
