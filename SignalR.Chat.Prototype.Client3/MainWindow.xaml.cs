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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Client;
using Owin;
using Microsoft.Owin.Cors;

using Microsoft.Owin.Host.HttpListener;

namespace SignalR.Chat.Prototype.Client3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public String UserName { get; set; }
        public string roomName { get; set; }
        public IHubProxy HubProxy { get; set; }
        //const string ServerURL = "http://localhost:8080";
        const string ServerURL = "http://85.218.241.69:8080";
        public HubConnection Connection { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            UserName = "Client_3";
            roomName = "grpTest2";

            Connect();
        }

        private void Connect()
        {
            Connection = new HubConnection(ServerURL + "/chat", useDefaultUrl: false);
            HubProxy = Connection.CreateHubProxy("ChatHub");

            

            //Handle incoming event from server: use Invoke to write to console from SignalR's thread
            HubProxy.On<string, string>("broadcastMsg", (name, message) =>
                this.Dispatcher.Invoke(() =>
                    MessageRichTextBox.AppendText(String.Format($"{name}: {message}\r"))));

            Connection.Start().Wait();

            HubProxy.Invoke("JoinRoom", roomName).Wait();
        }

        private void SendMsgBtn_Click(object sender, RoutedEventArgs e)
        {
            HubProxy.Invoke("Send", UserName, MessageTextBox.Text, roomName);
            MessageTextBox.Clear();
            MessageTextBox.Focus();
        }
    }
}
