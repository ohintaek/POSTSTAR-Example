using Mina.Core.Service;
using Mina.Core.Session;
using Mina.Filter.Codec;
using Mina.Filter.Codec.TextLine;
using Mina.Filter.Logging;
using Mina.Transport.Socket;
using System;
using System.Net;
using System.Text;

namespace SampleMinaDotNet
{
    class Program
    {
        private static readonly Int32 port = 9123;

        static void Main(string[] args)
        {
            try
            {
                IoAcceptor acceptor = new AsyncSocketAcceptor();

                acceptor.FilterChain.AddLast("logger", new LoggingFilter());
                acceptor.FilterChain.AddLast("codec", new ProtocolCodecFilter(new TextLineCodecFactory(Encoding.UTF8)));

                acceptor.ExceptionCaught += (s, e) => Console.WriteLine(e.Exception);
                acceptor.SessionIdle += (s, e) => Console.WriteLine("IDLE " + e.Session.GetIdleCount(e.IdleStatus));
                acceptor.MessageReceived += (s, e) =>
                {
                    String receiveMsg = e.Message.ToString();

                    // "Quit" ? let's get out ...
                    if (receiveMsg.Trim().Equals("quit", StringComparison.OrdinalIgnoreCase))
                    {
                        e.Session.Close(true);
                        return;
                    }

                    // Send the current date back to the client
                    e.Session.Write(DateTime.Now.ToString());
                    Console.WriteLine(receiveMsg);
                };

                acceptor.SessionConfig.ReadBufferSize = 2048;
                acceptor.SessionConfig.SetIdleTime(IdleStatus.BothIdle, 10);

                acceptor.Bind(new IPEndPoint(IPAddress.Any, port));

                Console.ReadLine();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.ReadLine();
            }

        }
    }
}
