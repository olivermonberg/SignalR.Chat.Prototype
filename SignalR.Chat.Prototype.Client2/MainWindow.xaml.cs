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

namespace SignalR.Chat.Prototype.Client2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public String UserName { get; set; }
        public IHubProxy HubProxy { get; set; }
        const string URI = "http://localhost:8080";
        public HubConnection Connection { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            UserName = "User_2";

            Connect();
        }

        private void Connect()
        {
            Connection = new HubConnection(URI + "/groupID", useDefaultUrl: false);
            HubProxy = Connection.CreateHubProxy("ChatHub");

            //Handle incoming event from server: use Invoke to write to console from SignalR's thread
            HubProxy.On<string, string>("broadcastMsg", (name, message) =>
                this.Dispatcher.Invoke(() =>
                    MessageRichTextBox.AppendText(String.Format($"{name}: {message}\r"))));

            Connection.Start();
        }

        private void SendMsgBtn_Click(object sender, RoutedEventArgs e)
        {
            HubProxy.Invoke("Send", UserName, MessageTextBox.Text);
            MessageTextBox.Clear();
            MessageTextBox.Focus();
        }
    }
}
