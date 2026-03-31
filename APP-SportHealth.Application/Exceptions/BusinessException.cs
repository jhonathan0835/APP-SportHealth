using System;
using System.Collections.Generic;
using System.Text;

namespace APP_SportHealth.Application.Exceptions
{
    public class BusinessException : Exception
    {
        public BusinessException(string message) : base(message) { }
    }
}
