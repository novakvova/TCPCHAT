using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TCPClientCon
{
    class Program
    {
        static void Main(string[] args)
        {
            //Connect("10.7.180.103", "Kovbasa");
            Connect("127.0.0.1", "Kovbasa");
        }
        static void Connect(string server, string message)
        {
            try
            {
                int port = 9999;
                TcpClient client = new TcpClient(server, port);
                Byte[] data = Encoding.Unicode.GetBytes(message);
                NetworkStream stream = client.GetStream();
                stream.Write(data, 0, data.Length);
                Console.WriteLine($"Send: {message}");

                data = new byte[256];
                string resposeData = string.Empty;
                int bytes = stream.Read(data, 0, data.Length);
                resposeData = Encoding.Unicode.GetString(data, 0, bytes);
                Console.WriteLine($"Received: {resposeData}");
                stream.Close();
                client.Close();

            }
            catch (Exception ex)
            {

                ;
            }
        }
    }
}
