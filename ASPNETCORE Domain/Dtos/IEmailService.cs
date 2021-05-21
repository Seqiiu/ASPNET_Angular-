using System;
using System.Collections.Generic;
using System.Text;

namespace ASPNETCORE_Domain.Dtos
{
    public interface IEmailService
    {
        void SendMessageInEamil(string email, string message);
    }
}
