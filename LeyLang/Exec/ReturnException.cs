using System;
using System.Collections.Generic;
using System.Text;

namespace LeyLang.Exec {
    public class FakeException : Exception {

    }

    public class ReturnException : FakeException {
        public LeyValue ReturnValue {get;}
        public ReturnException(LeyValue returnValue) {
            ReturnValue = returnValue; 
        }
    }


    [Serializable]
    public class BreakException : FakeException {
        public BreakException() { }
    }

    [Serializable]
    public class ContinueException : FakeException {
        public ContinueException() { }
    }
}
