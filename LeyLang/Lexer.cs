using System;
using System.IO;
using System.Linq;

namespace LeyLang {
    public class Lexer {
        private static char[] _identifierCharacters = new char[] { '_' };
        private StreamReader _stream;
        private char _lastChar = ' ';
        private bool _endOfStream = false;

        public Lexer(StreamReader stream) {
            this._stream = stream;
        }

        private char ReadChar() {
            int value = _stream.Read();
            if (value == -1) _endOfStream = true;
            return (char)value;
        }
        private char PeekChar() {
            int value = _stream.Peek();
            return (char)value;
        }

        public void Reset() {
            _stream.BaseStream.Position = 0;
            _lastChar = ' ';
            _endOfStream = false;
        }

        public Token GetToken() {
            while(char.IsWhiteSpace(_lastChar)) {
                _lastChar = ReadChar();
            }

            // identifier creator - an identifier must begin with a letter and then can contain letters, numbers and underscores
            if (char.IsLetter(_lastChar)) {
                string identifierStr = _lastChar.ToString();
                while(char.IsLetterOrDigit(_lastChar = ReadChar()) || _identifierCharacters.Contains(_lastChar)) {
                    identifierStr += _lastChar;
                }

                switch (identifierStr) {
                    case "func": return new Token(TokenKind.Func);
                    case "let": return new Token(TokenKind.Let);
                    case "if": return new Token(TokenKind.If);
                    case "for": return new Token(TokenKind.For);
                    case "else": return new Token(TokenKind.Else);
                    case "return": return new Token(TokenKind.Return);
                    case "while": return new Token(TokenKind.While);
                    case "break": return new Token(TokenKind.Break);
                    case "continue": return new Token(TokenKind.Continue);
                    case "class": return new Token(TokenKind.Class);
                    case "null": return new Token(TokenKind.Null);
                    case "true": return new Token(TokenKind.BoolLiteral,true);
                    case "false": return new Token(TokenKind.BoolLiteral,false);
                    default: return new Token(TokenKind.Identifier,identifierStr);
                }
            }

            // number creator - a number must begin with a number and can contain a single '.'
            if(char.IsDigit(_lastChar)) {
                string numStr = "";
                bool wholeNumber = true;

                do {
                    // makes sure a decimal point only happens once
                    if (_lastChar == '.') wholeNumber = false;
                    numStr += _lastChar;
                    _lastChar = ReadChar();
                // continue to read the number while the incoming chars are digits or a '.' (if not already used)
                } while (char.IsDigit(_lastChar) || wholeNumber && _lastChar == '.');

                decimal numberVal = decimal.Parse(numStr);
                return new Token(TokenKind.NumberLiteral,numberVal);
            }

            // string creator
            if(_lastChar == '"') {
                string strContents = "";

                _lastChar = ReadChar(); // eat '"'
                while(_lastChar != '"' && !_endOfStream) {
                    strContents += _lastChar;
                    _lastChar = ReadChar();
                }

                _lastChar = ReadChar(); // eat '"'

                return new Token(TokenKind.StringLiteral, strContents);
            }

            // ignores comments until end of line or stream
            if (_lastChar == '/' && PeekChar() == '/') {
                do {
                    _lastChar = ReadChar();
                } while (_lastChar != '\n' && _lastChar != '\r' && !_endOfStream);

                // get the next token after the comment
                if (!_endOfStream) {
                    return GetToken();
                }
            }

            if(_lastChar == '=') {
                if (PeekChar() == '=') {
                    _lastChar = ReadChar(); // eat first '='
                    _lastChar = ReadChar(); // eat second '='
                    return new Token(TokenKind.Punctuation, "==");
                }
            }

            if(_lastChar == '>') {
                if(PeekChar() == '=') {
                    _lastChar = ReadChar(); // eat '>'
                    _lastChar = ReadChar(); // eat '='
                    return new Token(TokenKind.Punctuation, ">=");
                }
            }

            if (_lastChar == '<') {
                if (PeekChar() == '=') {
                    _lastChar = ReadChar(); // eat '<'
                    _lastChar = ReadChar(); // eat '='
                    return new Token(TokenKind.Punctuation, "<=");
                }
            }

            if (_lastChar == '!') {
                if (PeekChar() == '=') {
                    _lastChar = ReadChar(); // eat '!'
                    _lastChar = ReadChar(); // eat '='
                    return new Token(TokenKind.Punctuation, "!=");
                }
            }

            if(_lastChar == '|') {
                if(PeekChar() == '|') {
                    _lastChar = ReadChar(); // eat '|'
                    _lastChar = ReadChar(); // eat '|'
                    return new Token(TokenKind.Punctuation, "||");
                }
            }

            if (_lastChar == '&') {
                if (PeekChar() == '&') {
                    _lastChar = ReadChar(); // eat '&'
                    _lastChar = ReadChar(); // eat '&'
                    return new Token(TokenKind.Punctuation, "&&");
                }
            }

            if (_endOfStream)
                return new Token(TokenKind.Eof);

            char thisChar = _lastChar;
            _lastChar = ReadChar();
            return new Token(TokenKind.Punctuation, thisChar.ToString());
        }
    }
}
