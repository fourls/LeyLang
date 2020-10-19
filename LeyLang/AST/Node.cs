using System;
using System.Collections.Generic;
using System.Text;

namespace LeyLang.AST {
    public abstract class Node {
        public string PrettyPrint(int indent) {
            string ind = IndentToString(indent);
            StringBuilder outputBld = new StringBuilder($"{ind}{PrettyNodeContents}\n");

            Node[] childNodes = ChildNodes;

            if(childNodes != null) {
                foreach (Node node in childNodes) {
                    outputBld.Append(node == null ? $"{IndentToString(indent+1)}[NULL]\n" : node.PrettyPrint(indent + 1));
                }
            }

            return outputBld.ToString();
        }

        private string IndentToString(int indent) {
            StringBuilder outputBld = new StringBuilder();
            for (int i = 0; i < indent; i++) {
                outputBld.Append("\t");
            }
            return outputBld.ToString();
        }

        protected virtual string PrettyNodeContents => $"[{GetType().Name.Replace("Node","")}]";
        protected virtual Node[] ChildNodes => null;
    }
}
