using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Client;
using Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Hosting;
using Microsoft.Owin.Host.HttpListener;

namespace SignalR.Chat.Prototype.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public String UserName { get; set; }
        public String roomName { get; set; }
        public IHubProxy HubProxy { get; set; }
        //const string ServerURL = "http://localhost:8080";
        const string ServerURL = "http://192.168.43.112:443";
        public HubConnection Connection { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            UserName = "Client_1";
            roomName = "grpTest";

            Connect();
        }

        private async void Connect()
        {
            Connection = new HubConnection(ServerURL + "/chat", useDefaultUrl: false);
            HubProxy = Connection.CreateHubProxy("ChatHub");

            

            //Handle incoming event from server: use Invoke to write to console from SignalR's thread
            HubProxy.On<string, string>("broadcastMsg", (name, message) =>
                this.Dispatcher.Invoke(() => 
                    MessageRichTextBox.AppendText(String.Format($"{name}: {message}\r"))));

            await Connection.Start();

            await HubProxy.Invoke("JoinRoom", roomName);

            await HubProxy.Invoke("Send", UserName, "Dette er sgu ogsaa en test", roomName);
        }

        private async void SendMsgBtn_Click(object sender, RoutedEventArgs e)
        {
            await SendMsg();
        }

        private async Task SendMsg()
        {
            await HubProxy.Invoke("Send", UserName, MessageTextBox.Text, roomName);
            MessageTextBox.Clear();
            MessageTextBox.Focus();
        }
    }
}
