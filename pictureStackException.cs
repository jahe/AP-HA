using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AP_HA
{
    public class pictureStackException : ApplicationException
    {
        public pictureStackException() { }
        public pictureStackException(string message) : base(message) { }
        public pictureStackException(string message, Exception inner) : base(message, inner) { }
    }
}
