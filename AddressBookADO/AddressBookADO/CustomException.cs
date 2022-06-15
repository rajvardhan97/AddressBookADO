using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBookADO
{
    internal class CustomException : Exception
    {
        public enum ExceptionType
        {
            No_data_found, Connection_Failed
        }
        public ExceptionType exceptionType;
        public CustomException(ExceptionType exceptionType, string message) : base(message)
        {
            this.exceptionType = exceptionType;
        }
    }
}
