using System;
using System.Collections.Generic;
using System.Text;

namespace ASPNET_CORE.Entities
{
     public class MessageEntity
    {
        public int Idmessage { get; set; }
        public string Author { get; set; }
        public string LastName { get; set; }
        public string Message { get; set; }
    }
}
