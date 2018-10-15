using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;

namespace SignalR.Chat.Prototype.Console
{
    public class ChatHub : Hub
    {
        public void Send(string UserName, string Msg)
        {
            Clients.All.broadcastMsg(UserName, Msg);

            //Clients.Group("grp1").broadcastMsg(UserName, Msg);

            System.Console.WriteLine($"{UserName}: {Msg}"); //Her skal der skrives til log/database for at loade chatten ved startup
        }
        /*
        public async Task AddToGroup(string groupName)
        {
            await Groups.Add(Context.ConnectionId, groupName);
        }*/
    }
}
