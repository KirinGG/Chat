using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChatClientEngine
{
    public class ChatClient : IChatClient
    {
        public readonly ClientWebSocket ws = new ClientWebSocket();

        public delegate void MessageHandler(string message);
        public event MessageHandler NewMessageRecieved;

        public async Task Connect(string serverAddress)
        {
            var uri = new Uri(serverAddress);
            await ws.ConnectAsync(uri, CancellationToken.None);
        }

        public async Task Disconnect()
        {
            await ws.CloseAsync(WebSocketCloseStatus.NormalClosure, "client disconnect", CancellationToken.None);
        }

        public async Task SendMessage(string message)
        {
            var reqAsBytes = Encoding.UTF8.GetBytes(message);
            var buff = new ArraySegment<byte>(reqAsBytes);

            await ws.SendAsync(buff, WebSocketMessageType.Text, true, CancellationToken.None);
        }

        public async Task<string> RecieveMessage()
        {
            if (ws.State != WebSocketState.Open)
            {
                return null;
            }

            var buffer = new ArraySegment<byte>(new byte[1024]);
            var result = await ws.ReceiveAsync(buffer, CancellationToken.None);
            var response = Encoding.UTF8.GetString(buffer.Array, 0, result.Count);
            return response;
        }

        public void StartTrackingIncomingMessages()
        {
            Thread messageTrackingThread = new Thread(async () => 
            {
                while (ws.State == WebSocketState.Open)
                {
                    var message = await RecieveMessage();
                    NewMessageRecieved(message);
                }
            });

            messageTrackingThread.Start();
        }
    }
}
