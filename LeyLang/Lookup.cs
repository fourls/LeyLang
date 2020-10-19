using System;
using System.Collections.Generic;
using System.Text;

namespace LeyLang {
    public class Lookup {
        private static Lookup _singleton;

        public static Lookup Instance {
            get {
                if (_singleton == null)
                    _singleton = new Lookup();

                return _singleton;
            }
        }

        public static void ResetInstance() {
            _singleton = new Lookup();
        }

        public VarManagement Vars { get; }
        public Dictionary<int,LeyFunc> Funcs { get; }
        public Dictionary<string,LeyClass> CustomClasses { get; }

        private Random _random;

        private Lookup() {
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
