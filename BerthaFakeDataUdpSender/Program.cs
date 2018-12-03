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
            Console.WriteLine("Enter -T- for temp Data, -H- for health data or stop \r\n");

            string option = Console.ReadLine();

            try
            {
                while (!string.Equals(option, "stop", StringComparison.Ordinal))
                {
                    if (option!=null)
                    {
                        switch (option.ToUpper())
                        {
                            case "T":
                                temperature().GetAwaiter().GetResult();
                                break;
                            case "H":
                                health().GetAwaiter().GetResult();

                                break;
                        }
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }


        static async Task temperature()
        {
            UdpClient udpServer = new UdpClient(0);
            udpServer.EnableBroadcast = true;
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Broadcast, 9000);
            Console.WriteLine("Broadcast for environment ready. Get started Press Enter");
            Console.ReadLine();

            int number = 1;

            int UserId = 93;
            decimal Humidity = (decimal) 0.20;
            decimal Temperature = (decimal) 0.30;
            string Location = "Cph";
            Random rnhumidity=new Random();
            Random rntemperature=new Random();


            while (true)
            {
                Humidity = (decimal) (0.20 + rnhumidity.Next(1, 50) );
                Temperature = (decimal) (0.30+ rntemperature.Next(1, 70));
                Console.WriteLine("hum::::"+Humidity+"\r\n Temp::"+Temperature);

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
                Thread.Sleep(10000);
            }

        }


        static  async  Task health()
        {
            int number = 1;
            UdpClient udpServer = new UdpClient(0);
            udpServer.EnableBroadcast = true;
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Broadcast, 7000);
            Console.WriteLine("Broadcast for health is ready. Get started Press Enter");
            Console.ReadLine();

            int UserId = 90;
            int BloodPressure = 2;
            int HeartBeat = 4;
            int Age = 22;
            int weight = 70;

            Random rnbloodpressure = new Random();
            Random rnheartbeat = new Random();


            while (true)
            {
                BloodPressure = 3 + rnbloodpressure.Next(1, 50);
                HeartBeat= 5 + rnheartbeat.Next(1, 70);

                Console.WriteLine(BloodPressure+"...."+HeartBeat);

                DateTime currentTime = DateTime.Now;
                string timeTxt = "Time: " + currentTime + "\n";
                string data = "UserId " + UserId + "\n" + "BloodPressure " + BloodPressure + "\n" + "HeartBeat " + HeartBeat + "\n" + "Age " + Age + "\n" + "weight " + weight + "\n";
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
                Thread.Sleep(10000);
            }

        }

    }

}
