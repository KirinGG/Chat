using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatClientEngine
{
    interface IChatClient
    {
        public void Connect(string channelName);
        public void SendMessage(string channelName, string message);
    }
}
