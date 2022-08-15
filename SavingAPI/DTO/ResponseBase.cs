using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class ResponseBase
    {
        public bool Success { get; private set; }
        public string Message { get; private set; }

        public ResponseBase(bool status, string message)
        {
            Success = status;
            Message = message;
        }


        public override string ToString()
        {
            return string.Format("{{ \"Success\": \"{0}\", \"Message\": \"{1}\" }}", Success, Message);
        }
    }
}
