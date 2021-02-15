using System;
using System.IO;

namespace telefon
{
    class Program
    {
        /// <summary>
        /// Changes the time given as parameter to secunds. 
        /// </summary>
        /// <param name="o"> hours </param>
        /// <param name="p"> minutes </param>
        /// <param name="mp"> secunds </param>
        /// <returns> The parameter in secunds </returns>
        static int Mpbe (int o, int p, int mp)
        {
            return mp + p * 60 + o * 60 * 60;
        }
        static void Main(string[] args)
        {
            int[] callsPerHours = new int[24];
            StreamReader file = new StreamReader("hivas.txt");
            bool readLineSuccesful = true;
            while (readLineSuccesful)
            {
                string line = file.ReadLine();
                if( line == null)
                {
                    readLineSuccesful = false;
                    break;
                }
                int startH, startMin, startSec, endH, endMin, endSec; 
                string[] lineData = line.Split(' ');
                startH = Int32.Parse(lineData[0]);
                startMin = Int32.Parse(lineData[1]);
                startSec = Int32.Parse(lineData[2]);
                endH = Int32.Parse(lineData[3]);
                endMin = Int32.Parse(lineData[4]);
                endSec = Int32.Parse(lineData[5]);

                callsPerHours[startH] += 1;
            }
            #region Harmadik feladat
            Console.WriteLine("3. feladat");
            for (int currentHour = 0; currentHour < callsPerHours.Length; currentHour++)
            {
                if(callsPerHours[currentHour] != 0)
                {
                    Console.WriteLine($"{currentHour} ora {callsPerHours[currentHour]} hivas");
                }
            }
            #endregion
        }
    }
}
