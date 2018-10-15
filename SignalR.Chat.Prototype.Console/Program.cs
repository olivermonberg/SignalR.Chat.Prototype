using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Hosting;
using Microsoft.Owin.Host.HttpListener;

namespace SignalR.Chat.Prototype.Server
{
    class Program
    {
        //static public IDisposable SignalR { get; set; }
        const string URI = "http://localhost:8080";

        static void Main(string[] args)
        {
            System.Console.WriteLine("Starting server...");
            Task.Run(() => StartServer());
            System.Console.ReadKey();
        }

        private static void StartServer()
        {
            try
            {
                /*SignalR = */WebApp.Start<Startup>(URI);
            }
            catch (TargetInvocationException)
            {
                System.Console.WriteLine("A server is already running at " + URI);
                return;
            }

            System.Console.WriteLine("Server started at " + URI);
        }
}

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR("/groupID", new HubConfiguration());
        }
    }
}
