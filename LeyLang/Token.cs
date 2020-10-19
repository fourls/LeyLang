namespace LeyLang {
    public class Token {
        public TokenKind Kind { get; }
        public string StringValue { get; }
        public decimal DecimalValue { get; }
        //public char CharValue { get; }
        public bool BoolValue { get; }

        public Token(TokenKind kind) {
            Kind = kind;
        }

        public Token(TokenKind kind, bool value) : this(kind) {
            BoolValue = value;
        }

        public Token(TokenKind kind, string value) : this(kind) {
            StringValue = value;
        }

        public Token(TokenKind kind, decimal value) : this(kind) {
            DecimalValue = value;
        }

        //public Token(TokenKind kind, char value) : this(kind) {
        //    CharValue = value;
        //}
    }
}
