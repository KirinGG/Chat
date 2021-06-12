using ChatClientEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfClient
{
    /// <summary>
    /// Interaction logic for ChatWindow.xaml
    /// </summary>
    public partial class ChatWindow : Window
    {
        private readonly ChatClient chatClient;

        public ChatWindow()
        {
            InitializeComponent();
            chatClient = (ChatClient)App.Current.Properties["ChatClient"];
            chatClient.NewMessageRecieved += addRecievedMessageToChatLog;
            chatClient.StartTrackingIncomingMessages();
        }

        private async void sendButton_Click(object sender, RoutedEventArgs e)
        {
            var textBox = (TextBox)this.FindName("messageBox");
            var message = textBox.Text;
            textBox.Text = "";
            await chatClient.SendMessage(message);
        }

        private void addRecievedMessageToChatLog(string message)
        {
            this.Dispatcher.Invoke(() =>
            {
                var chatLog = (RichTextBox)this.FindName("chatLogTextBox");
                chatLog.AppendText($"{DateTime.Now}: {message}\r");
            });            
        }

        private void messageBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                sendButton_Click(null, null);
            }
        }
    }
}
