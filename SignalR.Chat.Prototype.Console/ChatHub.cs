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
        public void Send(string UserName, string Msg, string roomName)
        {
            //Clients.All.broadcastMsg(UserName, Msg);

            Clients.Group(roomName).broadcastMsg(UserName, Msg);

            System.Console.WriteLine($"{UserName}: {Msg}"); //Her skal der skrives til log/database for at loade chatten ved startup
        }

        public Task JoinRoom(string roomName)
        {
            return Groups.Add(Context.ConnectionId, roomName);
        }

        public Task LeaveRoom(string roomName)
        {
            return Groups.Remove(Context.ConnectionId, roomName);
        }
    }
}
