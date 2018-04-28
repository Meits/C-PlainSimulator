using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlainSimulator
{

    [Serializable]
    public class PlainException : Exception
    {
        public PlainException() { }
        public PlainException(string message) : base(message) { }
        public PlainException(string message, Exception inner) : base(message, inner) { }
        protected PlainException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
