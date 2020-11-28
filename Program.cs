using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace BruteFroceIpFinder
{
    class Program
    {

        public static void Main(string[] args)
        {
            string input = Console.ReadLine();
            if (input.Split(' ')[0] == "executeFunction")
            {
                Program program = new Program();
                program.StartTheBoom(Int32.Parse(input.Split(' ')[1]));
            }
        }

        public int currentThread = 1;
        public int totalThreadCount = 1;

        public void StartTheBoom(int totalThreads)
        {
            totalThreadCount = totalThreads;
            for (int x = 0; x < totalThreadCount; x++)
            {
                ParameterizedThreadStart parameterizedThreadStart = new ParameterizedThreadStart(TheBoom);
                Thread thread = new Thread(parameterizedThreadStart);
                string parems = currentThread + " " + totalThreadCount;
                thread.Start(parems);
                thread.Name = x.ToString();
                currentThread += (int)(MathF.Round(192 / totalThreadCount));
            }
        }


        public void TheBoom(object parems)
        {
            List<string> Ips = new List<string>();
            int LC_CurrentThread = Int32.Parse(((string)parems).Split(' ')[0]);
            int LC_TotalThreadCount = Int32.Parse(((string)parems).Split(' ')[1]);
            Console.WriteLine(LC_CurrentThread);
            int firstNumber = LC_CurrentThread;
            int secondNumber = 1;
            int thirdNumber = 1;
            int fourthNumber = 1;
            string Ip = "";
            while (firstNumber <= LC_CurrentThread + ((int)(MathF.Round(192 / LC_TotalThreadCount))))
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
                            if (client.ConnectAsync(Ip, port).Wait(TimeSpan.FromMilliseconds(15)))
                            {
                                Console.WriteLine("FOUND ONE:" + Ip);
                                Ips.Add(Ip);
                            }
                            client.Close();
                            fourthNumber++;
                        }
                        thirdNumber++;
                        Console.WriteLine(Ip);
                    }
                    secondNumber++;
                }
                firstNumber++;
            }
            if (!File.Exists("IPList"))
            {
                File.WriteAllText("IPList", "");
            }
            var currentIpsVAR = File.ReadAllLines("IPList");
            List<string> currentIps = new List<string>(currentIpsVAR);
            Parallel.ForEach(Ips, (loopIp) =>
            {
                currentIps.Add(loopIp);
            });
            File.WriteAllLines("IPList", currentIps);
        }
    }
}