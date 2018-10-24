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
        const string ServerURL = "*:443";

        static void Main(string[] args)
        {
            System.Console.WriteLine($"Starting server...\n{ServerURL}");
            Task.Run(() => StartServer());
            System.Console.ReadKey();
        }

        private static void StartServer()
        {
            try
            {
                SignalR = WebApp.Start<Startup>(ServerURL);
            }
            catch (TargetInvocationException e)
            {
                System.Console.WriteLine($"A server is already running at " + ServerURL + $"\nMsg: {e.Message}");
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
            app.UseCors(CorsOptions.AllowAll);
            app.MapSignalR(URI, new HubConfiguration());


            //app.Map(URI , map =>
            //{
            //    map.UseCors(CorsOptions.AllowAll);
            //
            //    var hubConfiguration = new HubConfiguration
            //    {
            //        EnableDetailedErrors = true,
            //        EnableJSONP = true
            //    };
            //
            //    map.RunSignalR(hubConfiguration);
            //});
        }
    }
}
