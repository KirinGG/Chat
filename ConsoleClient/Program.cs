using ChatClientEngine;
using System;
using System.Threading.Tasks;

namespace ConsoleClient
{
    class Program
    {
        private static void ProcessMessage(string message)
        {
            Console.WriteLine(message);
        }

        static async Task Main(string[] args)
        {
            var cc = new ChatClient();
            cc.NewMessageRecieved += ProcessMessage;

            AppDomain.CurrentDomain.ProcessExit += async (s, ev) =>
            {
                Console.WriteLine("process exit");
                await cc.Disconnect();
            };

            Console.CancelKeyPress += async (s, ev) =>
            {
                Console.WriteLine("Ctrl+C pressed");
                await cc.Disconnect();
                ev.Cancel = true;
            };

            try
            {               
                await cc.Connect("wss://localhost:5001/ws");
                await cc.SendMessage("current time is: " + DateTime.Now.ToString());
                
                cc.StartTrackingIncomingMessages();

                while (true)
                {
                    await cc.SendMessage(Console.ReadLine());
                }
            }
            finally
            {
                await cc.Disconnect();
            }
        }
    }
}
