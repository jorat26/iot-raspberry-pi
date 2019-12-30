using System;
using System.Runtime.Serialization;

namespace IoT.RaspberryPi
{
    public class GpioPinException : Exception
    {
        public GpioPinException()
        {
        }

        public GpioPinException(string message) : base(message)
        {
        }

        public GpioPinException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected GpioPinException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
