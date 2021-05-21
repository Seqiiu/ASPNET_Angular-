using System;
using System.Collections.Generic;
using System.Text;

namespace ASPNET_CORE_Domain
{
     public class MessageEntity
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public string LastName { get; set; }
        public string Message { get; set; }
    }
}
