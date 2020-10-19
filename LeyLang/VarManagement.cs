using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace LeyLang {
    public class VarManagement {
        private List<VarTable> _tables;
        private int _currentScope => _tables.Count - 1;

        public VarManagement() {
            _tables = new List<VarTable>();
            _tables.Add(new VarTable());
        }

        private VarTable GetTableAt(int index) {
            return _tables.Count > index ? _tables[index] : null;
        }

        public VarTable GetCurrentTable() {
            return GetTableAt(_currentScope);
        }

        public LeyValueWithType GetVar(string varName) {
            for(int i = _currentScope; i >= 0; i--) {
                var table = GetTableAt(i);

                if (table.ContainsVar(varName))
                    return table.GetVar(varName);
            }

            return null;
        }

        public void EnterScope() {
            _tables.Add(new VarTable());
        }

        public void ExitScope() {
            if(_currentScope > 0)
                _tables.RemoveAt(_currentScope);
        }

        public bool DeclareVar(string varName, string type, bool global = false) {
            if(global) {
                return GetTableAt(0).DeclareVar(varName, type);
            }

            return GetTableAt(_currentScope).DeclareVar(varName, type);
        }

        public bool DeclareVar(string varName, string type, LeyValue initialValue, bool global = false) {
            if(DeclareVar(varName, type, global)) {
                GetVar(varName).Set(initialValue);
                return true;
            }
            return false;
        }

        public void PrettyPrint() {
            Console.WriteLine("==================| LeyLang Variables |========================");
            for(int i = 0; i < _tables.Count; i++) {
                Console.WriteLine($"Table {i}{(i == 0 ? " (global)" : "")}");
                _tables[i].PrettyPrint();
                Console.WriteLine();
                
            }
        }
    }
}
