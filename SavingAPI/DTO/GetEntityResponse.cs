using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class GetEntityResponse : ResponseBase
    {

        public Entity Entity { get; private set; }

        public GetEntityResponse(bool status, string message, Entity entity) 
            : base(status, message)
        {
            Entity = entity;
        }
    }
}
