using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BerthaFakeDataUdpSender
{
    class Program
    {
        static void Main(string[] args)
        {
            int number = 1;
            int UserId = 93;
            decimal Humidity = 3;
            decimal Temperature = 5;


            string Location = "Cph";


            //Random rnCo = new Random();
            //Random rnNox = new Random();

            //string sensorLocation = "Pollution sensor v.1.0. \n" + "Location: Jernbanegade 3 1\n";

            UdpClient udpServer = new UdpClient(0);
            udpServer.EnableBroadcast = true;
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Broadcast, 9000);

            Console.WriteLine("Broadcast ready. Get started Press Enter");
            Console.ReadLine();

            while (true)
            {


                DateTime currentTime = DateTime.Now;
                string timeTxt = "Time: " + currentTime + "\n";
                string data = "UserId " + UserId + "\n" + "Humidity " + Humidity + "\n" + "Temperature " + Temperature + "\n" + "Location " + Location + "\n";
                string sensorData = timeTxt + data;

                Byte[] sendBytes = Encoding.ASCII.GetBytes(sensorData);

                try
                {
                    udpServer.Send(sendBytes, sendBytes.Length, endPoint); //, endPoint
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }

                Console.WriteLine("New Data has been sent " + number++);
                Thread.Sleep(100000);
            }

        }
    }
    
}
