using System;
using System.Collections.Generic;
using System.Text;

namespace LeyLang {
    public class ExecLookup {
        private static ExecLookup _singleton;

        public static ExecLookup Instance {
            get {
                if (_singleton == null)
                    _singleton = new ExecLookup();

                return _singleton;
            }
        }

        public static void ResetInstance() {
            _singleton = new ExecLookup();
        }

        public VarManagement Vars { get; }
        public Dictionary<int,LeyFunc> Funcs { get; }
        public Dictionary<string,LeyClass> CustomClasses { get; }

        private Random _random;

        private ExecLookup() {
            Vars = new VarManagement();
            Funcs = new Dictionary<int, LeyFunc>();
            CustomClasses = new Dictionary<string, LeyClass>();
        }

        public int DeclareFunc(string funcName, LeyFunc func=null) {
            if (_random == null) _random = new Random();

            int funcIndex;
            do {
                funcIndex = _random.Next();
            } while (Funcs.ContainsKey(funcIndex));

            Funcs[funcIndex] = func;

            Vars.DeclareVar(funcName, LeyTypeUtility.FuncRef, new LeyFuncRef(funcIndex), true);

            return funcIndex;
        }
    }
}
