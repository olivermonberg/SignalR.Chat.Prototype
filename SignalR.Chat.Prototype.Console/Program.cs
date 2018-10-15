using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Hosting;
using Microsoft.Owin.Host.HttpListener;

namespace SignalR.Chat.Prototype.Console
{
    class Program
    {
        static public IDisposable SignalR { get; set; }
        const string ServerURL = "http://localhost:8080";

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
                SignalR = WebApp.Start<Startup>(ServerURL);
            }
            catch (TargetInvocationException)
            {
                System.Console.WriteLine("A server is already running at " + ServerURL);
                return;
            }

            System.Console.WriteLine("Server started at " + ServerURL);
        }
}

    public class Startup
    {
        public static string URI { get; set; } = "/chat";
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR(URI, new HubConfiguration());
        }
    }
}
