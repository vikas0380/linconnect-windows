using System;
using System.IO;
using System.Net;
using HttpServer;
using HttpListener = HttpServer.HttpListener;

namespace LinConnect
{
    class Server
    {
        private HttpListener _listener;
        readonly Utilities _utilities = new Utilities();
        private static Server _instance;
        private static readonly object Padlock = new object();

   private Server()
    {
    }

    public static Server Instance
    {
        get
        {
            lock (Padlock)
            {
                if (_instance == null)
                {
                    _instance = new Server();
                }
                return _instance;
            }
        }
    }


        public  void StartServer(String ipAddress,String currentPort)
        {
          
            _listener = HttpListener.Create(IPAddress.Any, Int16.Parse(currentPort));
            _listener.RequestReceived += OnRequest;

            _listener.Start(0);
            if (ipAddress != "Wireless Not Connected")
                _utilities.Notify("LinConnect", null, "Server Started at " + ipAddress + ":" + currentPort);
            else
            {
                _utilities.Notify("LinConnect", null, "Wireless Not Connected");
            }
        }


        public void RestartServer(String ipAddress,String currentPort)
        {
            _listener.Stop();
            StartServer(ipAddress,currentPort);
            
        }


        public void StopServer(String port)
        {
            _listener.Stop();
        }



        private void OnRequest(object sender, RequestEventArgs e)
        {
            try
            {
                IHttpClientContext context = (IHttpClientContext)sender;
                IHttpRequest request = e.Request;
                String notifTitle = _utilities.DecodeString(request.Headers["notifheader"]);
                String notifDescription = _utilities.DecodeString(request.Headers["notifdescription"]);
                // request.GetBody();
                IHttpResponse response = request.CreateResponse(context);
                StreamWriter writer = new StreamWriter(response.Body);

                //Byte[] bt = request.GetBody();
                //Console.WriteLine("header data " + request.Headers["Content-Type"]);

                //Console.WriteLine(request.Method);

                _utilities.Notify(notifTitle, null, notifDescription);

                writer.WriteLine("true");
                writer.Flush();
                response.Send();
            }
            catch (Exception exception)
            {

                Console.WriteLine(exception.Message);
            }

        }

    }
}
