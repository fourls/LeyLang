using System;
using System.Collections.Generic;
using System.Text;

namespace LeyLang {
    public class LeyException : Exception {
        public LeyException() { }
        public LeyException(string message) : base(message) { }
        public LeyException(string message, Exception inner) : base(message, inner) { }
        protected LeyException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
