using System;
using System.Collections.Generic;
using System.IO;

namespace BruteFroceIpFinder
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> Ips = new List<string>();
            int firstNumber = 30;
            int secondNumber = 1;
            int thirdNumber = 1;
            int fourthNumber = 1;
            while (firstNumber <= 185)
            {
                secondNumber = 1;
                while (secondNumber <= 255)
                {
                    thirdNumber = 1;
                    while (thirdNumber <= 255)
                    {
                        fourthNumber = 1;
                        while (fourthNumber <= 255)
                        {
                            Ips.Add(firstNumber + "." + secondNumber + "." + thirdNumber + "." + fourthNumber);
                            fourthNumber++;
                        }
                        thirdNumber++;
                    }
                    secondNumber++;
                }
                firstNumber++;
            }
            File.WriteAllLines(@"IPList", Ips);
        }
    }
}
