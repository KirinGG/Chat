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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private readonly ChatClient chatClient;

        public Login()
        {
            InitializeComponent();
            chatClient = (ChatClient)App.Current.Properties["ChatClient"];
        }

        private async void connectButton_Click(object sender, RoutedEventArgs e)
        {
            var serverAddress = ((TextBox)this.FindName("serverAddressBox")).Text;
            try
            {
                await chatClient.Connect(serverAddress);
                ChatWindow win = new ChatWindow();
                this.Hide();
                win.ShowDialog();
                await chatClient.Disconnect();
                this.Show();
            }
            catch(Exception ex)
            {
                var tmp = ex;
            }            
        }
    }
}
