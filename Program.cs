using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;

namespace BruteFroceIpFinder
{
    class Program
    {
        static void Main(string[] args)
        {
            int firstNumber = 172;
            int secondNumber = 1;
            int thirdNumber = 1;
            int fourthNumber = 1;
            string Ip = "";
            while (firstNumber <= 192)
            {
                secondNumber = 1;
                while (secondNumber <= 168)
                {
                    thirdNumber = 1;
                    while (thirdNumber <= 255)
                    {
                        fourthNumber = 1;
                        while (fourthNumber <= 255)
                        {
                            Ip = firstNumber + "." + secondNumber + "." + thirdNumber + "." + fourthNumber;
                            Int32 port = 25565;
                            TcpClient client = new TcpClient();
                            if (client.ConnectAsync(Ip, port).Wait(TimeSpan.FromMilliseconds(1)))
                            {
                                Console.WriteLine("FOUND ONE:" + Ip);
                            }
                            client.Close();
                            fourthNumber++;
                        }
                        thirdNumber++;
                    }
                    secondNumber++;
                }
                firstNumber++;
                Console.WriteLine(Ip);
            }
        }
    }
}
