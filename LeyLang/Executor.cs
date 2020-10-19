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

            lookup.DeclareClass(new LeyClass(
                "Person",
                new Dictionary<string, string>() {
                    ["name"] = LeyTypeUtility.String,
                    ["age"] = LeyTypeUtility.Number,
                    ["gender"] = LeyTypeUtility.String,
                    ["alive"] = LeyTypeUtility.Bool,
                    ["partner"] = "Person"
                },
                new LeyExternFunc(
                    new LeyFuncParam[] {
                        new LeyFuncParam("this","Person"),
                        new LeyFuncParam("name",LeyTypeUtility.String),
                        new LeyFuncParam("age",LeyTypeUtility.Number),
                        new LeyFuncParam("gender",LeyTypeUtility.String),
                        new LeyFuncParam("alive",LeyTypeUtility.Bool),
                        new LeyFuncParam("partner","Person"),
                    },
                    "Person",
                    (args) => {
                        LeyObject thisObj = args[0] as LeyObject;
                        thisObj.SetVar("name", args[1]);
                        thisObj.SetVar("age", args[2]);
                        thisObj.SetVar("gender", args[3]);
                        thisObj.SetVar("alive", args[4]);
                        thisObj.SetVar("partner", args[5]);
                        return thisObj;
                    }
                ),
                new Dictionary<string, LeyFunc>() {
                    ["IsAdult"] = new LeyExternFunc(
                        new LeyFuncParam[] { new LeyFuncParam("this","Person") },
                        LeyTypeUtility.Bool,
                        (args) => {
                            LeyObject thisObj = args[0] as LeyObject;

                            return new LeyBool((thisObj.GetVar<LeyNumber>("age").Value > 18));
                        }
                    )
                }
            ));
        }

        public static void Execute(ProgramNode astRoot) {
            Exec.Program program = Exec.Program.Create(astRoot);
            GenerateStdLib();
            program.Execute();
        }
    }
}
