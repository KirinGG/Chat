using System;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleClient
{
    class Program
    {
        static async Task SendTicksRequest()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11;
            var ws = new ClientWebSocket();
            var uri = new Uri("wss://localhost:5001/ws");

            await ws.ConnectAsync(uri, CancellationToken.None);

            var reqAsBytes = Encoding.UTF8.GetBytes("{data}");
            var ticksRequest = new ArraySegment<byte>(reqAsBytes);

            await ws.SendAsync(ticksRequest,
                WebSocketMessageType.Text,
                true,
                CancellationToken.None);

            var buffer = new ArraySegment<byte>(new byte[1024]);
            var result = await ws.ReceiveAsync(buffer, CancellationToken.None);

            string response = Encoding.UTF8.GetString(buffer.Array, 0, result.Count);
            Console.WriteLine(response);
        }

        static async Task Main(string[] args)
        {
            await SendTicksRequest();
            Console.ReadLine();
        }
    }
}
