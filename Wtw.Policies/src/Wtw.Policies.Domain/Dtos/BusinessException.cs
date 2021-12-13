using System;
using System.Collections.Generic;
using System.Text;

namespace Wtw.Policies.Domain.Dtos
{
    public class BusinessException : Exception
    {
        public BusinessException() { }

        public BusinessException(string message)
            : base(String.Format("Error occured: {0}", message))
        {

        }
    }
}
