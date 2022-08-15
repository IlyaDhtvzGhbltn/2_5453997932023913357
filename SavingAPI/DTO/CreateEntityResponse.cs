using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class CreateEntityResponse : ResponseBase
    {
        public CreateEntityResponse(bool status, string message) 
            : base(status, message)
        { }
    }
}
