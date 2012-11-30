﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AP_HA
{
    public class ProjectException : ApplicationException
    {
        public ProjectException() { }
        public ProjectException(string message) : base(message) { }
        public ProjectException(string message, Exception inner) : base(message, inner) { }
    } 	  	
}
