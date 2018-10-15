using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;

namespace SignalR.Chat.Prototype.Server
{
    public class ChatHub : Hub
    {
        public void Send(string UserName, string Msg)
        {
            Clients.All.broadcastMsg(UserName, Msg);
            Console.WriteLine($"{UserName}: {Msg}");
        }
    }
}
