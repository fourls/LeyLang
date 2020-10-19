using LeyLang.AST;
using System;
using System.Collections.Generic;
using System.Text;

namespace LeyLang {
    public static class Executor {
        public static void Execute(ProgramNode astRoot) {
            Exec.Program program = Exec.Program.Create(astRoot);
            ExecLookup.ResetInstance();
            ExecLookup lookup = ExecLookup.Instance;

            //lookup.QuickDeclareFunc(
            //    "NumToStr",
            //    new LeyExternFunc(
            //        new LeyFuncParam[] { new LeyFuncParam("msg", LeyTypeUtility.Number) },
            //        LeyTypeUtility.String,
            //        (args) => {
            //            return new LeyString((args[0] as LeyNumber).Value.ToString());
            //        }
            //    )
            //);

            lookup.DeclareFunc(
                "WriteStr",
                LeyExternFunc.FastCreateExtern(typeof(Console).GetMethod("WriteLine",new Type[] {typeof(string)}))
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
                        ExecLookup.Instance.Vars.PrettyPrint();

                        return null;
                    }
                )
            );

            program.Execute();
        }
    }
}
