using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatClientEngine
{
    interface IChatClient
    {
        Task Connect(string serverAddress);
        Task SendMessage(string message);
        Task<string> RecieveMessage();
    }
}
