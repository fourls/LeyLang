using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using LeyLang;

namespace LeyConsole {
    class Program {
        static void DoLexer(Lexer lexer) {
            Token token;

            do {
                token = lexer.GetToken();
                Console.WriteLine($" {token.Kind,20} | STR {token.StringValue,-20} | DEC {token.DecimalValue,-3} | BOOL {token.BoolValue}");
            } while (token.Kind != TokenKind.Eof);

            lexer.Reset();
        }

        static void Main(string[] args) {
            string path = args[0].ToString();
            if (!File.Exists(path)) 
                throw new FileNotFoundException("Sample file does not exist.");
            StreamReader reader = new StreamReader(args[0].ToString());

            Lexer lexer = new Lexer(reader);

            //DoLexer(lexer);

            Parser parser = new Parser(lexer);

            LeyLang.AST.ProgramNode program = parser.ParseProgram();

            //Console.WriteLine(program.PrettyPrint(0));

            Executor.Execute(program);

            Console.ReadLine();
        }
    }
}
