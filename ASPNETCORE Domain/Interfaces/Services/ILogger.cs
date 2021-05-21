using System;
using System.Collections.Generic;
using System.Text;

namespace ASPNETCORE_Domain.Interfaces.Services
{
    public interface ILogger 
    {
        void Log(string message);
    }
}
