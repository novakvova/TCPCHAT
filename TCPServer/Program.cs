using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TCPServer
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpListener server = null;
            try
            {
                int maxThreadCount = Environment.ProcessorCount * 4;
                Console.WriteLine($"Максиальна кількість потоків {maxThreadCount}");
                ThreadPool.SetMaxThreads(maxThreadCount, maxThreadCount);
                ThreadPool.SetMinThreads(2, 2);
                int port = 9999;
                IPAddress localAddr = IPAddress.Parse("127.0.0.1");
                //IPAddress localAddr = IPAddress.Parse("10.7.180.103");

                int counter = 0;
                server = new TcpListener(localAddr, port);
                server.Start();
                while(true)
                {
                    Console.WriteLine("Server for connection ...");
                    ThreadPool.QueueUserWorkItem(ObrabotkaCleint, 
                        server.AcceptTcpClient());
                    counter++;
                    Console.WriteLine($"\n Connection #{counter}");
                }

            }
            catch (Exception)
            {

                ;
            }
        }

        private static void ObrabotkaCleint(object client_obj)
        {
            Byte[] bytes = new Byte[256];
            String data = null;
            Thread.Sleep(1000);
            TcpClient client = client_obj as TcpClient;
            NetworkStream stream = client.GetStream();
            Int32 i = 0;
            while ((i=stream.Read(bytes, 0, bytes.Length))!=0)
            {
                data = System.Text.Encoding.Unicode.GetString(bytes, 0, i);
                Console.WriteLine(data);
                data = data.ToUpper();
                Byte[] message = Encoding.Unicode.GetBytes(data);
                stream.Write(message, 0, message.Length);
            }
            stream.Close();
            client.Close();
        }
    }
}
