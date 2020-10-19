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

            lookup.QuickDeclareFunc(
                "BoolToStr",
                new LeyExternFunc(
                    new LeyFuncParam[] { new LeyFuncParam("msg", LeyTypeUtility.Number) },
                    LeyTypeUtility.String,
                    (args) => {
                        return new LeyString((args[0] as LeyBool).Value.ToString());
                    }
                )
            );

            lookup.QuickDeclareFunc(
                "WriteStr",
                LeyExternFunc.GenerateLeyFunc(typeof(Console).GetMethod("WriteLine",new Type[] {typeof(string)}))
            );

            lookup.QuickDeclareFunc(
                "ReadStr",
                LeyExternFunc.GenerateLeyFunc(typeof(Console).GetMethod("ReadLine", new Type[0]))
            );

            Func<decimal, string> numToStr = x => x.ToString();
            lookup.QuickDeclareFunc(
                "NumToStr",
                LeyExternFunc.GenerateLeyFunc(numToStr, numToStr.Target)
            );

            lookup.QuickDeclareFunc(
                "StrToNum",
                new LeyExternFunc(
                    new LeyFuncParam[] { new LeyFuncParam("prompt", LeyTypeUtility.String) },
                    LeyTypeUtility.Number,
                    (args) => {
                        string str = (args[0] as LeyString).Value;

                        if (decimal.TryParse(str, out decimal result))
                            return new LeyNumber(result);
                        else
                            return new LeyNumber(0);
                    }
                )
            );

            lookup.QuickDeclareFunc(
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
