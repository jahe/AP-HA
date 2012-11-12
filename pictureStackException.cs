using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AP_HA
{
    public class PictureStackException : ApplicationException
    {
        public PictureStackException() { }
        public PictureStackException(string message) : base(message) { }
        public PictureStackException(string message, Exception inner) : base(message, inner) { }
    }
}
