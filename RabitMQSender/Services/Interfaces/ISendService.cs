using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RabitMQSender.Services
{
    public interface ISendService
    {
        string SendMessage();
    }
}
