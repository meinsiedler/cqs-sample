using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace CqsSample.Domain.Exceptions
{
    public class ForbiddenException : Exception
    {
        public ForbiddenException()
            : base()
        {
        }

        public ForbiddenException(string message)
            : base(message)
        {
        }

        public ForbiddenException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected ForbiddenException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
