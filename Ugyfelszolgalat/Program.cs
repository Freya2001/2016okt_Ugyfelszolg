using System;
using System.IO;

namespace telefon
{
    class CallData
    {
        public int startH, startMin, startSec, endH, endMin, endSec;
    }
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
            int maxCallLength = 0;
            int maxCallIndex = 0;
            int callIndex = 0;
            while (readLineSuccesful)
            {
                string line = file.ReadLine();
                if( line == null)
                {
                    readLineSuccesful = false;
                    break;
                }
                CallData callData = new CallData();
                string[] lineData = line.Split(' ');
                callData.startH = Int32.Parse(lineData[0]);
                callData.startMin = Int32.Parse(lineData[1]);
                callData.startSec = Int32.Parse(lineData[2]);
                callData.endH = Int32.Parse(lineData[3]);
                callData.endMin = Int32.Parse(lineData[4]);
                callData.endSec = Int32.Parse(lineData[5]);

                // 3. feladat
                callsPerHours[callData.startH] += 1;
                // 4. feladat
                int callLength = Mpbe(callData.endH, callData.endMin, callData.endSec) - Mpbe(callData.startH, callData.startMin, callData.startSec);
                if (maxCallLength < callLength)
                {
                    maxCallLength = callLength;
                    maxCallIndex = callIndex;
                }
                callIndex = callIndex + 1;
            }
            #region Harmadik feladat
            // Megszámlálás
            Console.WriteLine("3. feladat");
            for (int currentHour = 0; currentHour < callsPerHours.Length; currentHour++)
            {
                if(callsPerHours[currentHour] != 0)
                {
                    Console.WriteLine($"{currentHour} ora {callsPerHours[currentHour]} hivas");
                }
            }
            Console.Write("\n");
            #endregion
            #region Negyedik feladat
            // Maximum keresés
            Console.WriteLine("4. feladat");
            Console.WriteLine($"A leghosszabb ideig vonalban levo hivo {maxCallIndex + 1}. sorban szerepel, a hivas hossza: {maxCallLength} masodperc.");
            Console.Write("\n");
            #endregion
            #region Otodik feladat
            // Maximum keresés
            Console.WriteLine("5. feladat");
            Console.Write("Adjon meg egy idopontot 8 es 12 kozott! (ora perc masodperc) ");
            Console.ReadLine();
            Console.Write("\n");
            #endregion
        }
    }
}
