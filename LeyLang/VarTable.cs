using LeyLang.AST;
using System;
using System.Collections.Generic;
using System.Text;

namespace LeyLang {
    public class VarTable {
        private Dictionary<string, LeyValueWithType> _variables = new Dictionary<string, LeyValueWithType>();

        public bool ContainsVar(string varName) {
            return _variables.ContainsKey(varName);
        }

        public LeyValueWithType GetVar(string varName) {
            return _variables[varName];
        }

        public bool DeclareVar(string varName, string type) {
            if (ContainsVar(varName)) return false;
            if (!LeyTypeUtility.IsTypeValid(type)) return false;

            LeyValueWithType leyVar = new LeyValueWithType(type);
            _variables[varName] = leyVar;
            
            return true;
        }

        public void PrettyPrint() {
            foreach(var kvp in _variables) {
                Console.WriteLine($"| {kvp.Key,15} | {kvp.Value.Type,15} | {kvp.Value.Value,15} |");
            }
        }
    }
}
