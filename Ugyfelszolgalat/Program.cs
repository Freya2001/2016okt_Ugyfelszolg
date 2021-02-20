using System;
using System.Collections.Generic;
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
        /// <summary>
        /// Calculates the relation between the left and the right side of two times
        /// </summary>
        /// <param name="leftHour"></param>
        /// <param name="leftMin"></param>
        /// <param name="leftSec"></param>
        /// <param name="rightHour"></param>
        /// <param name="rightMin"></param>
        /// <param name="rightSec"></param>
        /// <returns>
        /// -1 if the left time values is smaller than the right
        /// 0 if the two time values are equal
        /// 1 if the left time value is greater than the right
        /// </returns>
        static int CompareTime(int leftHour, int leftMin, int leftSec, int rightHour, int rightMin, int rightSec)
        {
            if(leftHour > rightHour)
            {
                return 1;
            }
            else if(leftHour < rightHour)
            {
                return -1;
            }
            else
            {
                if (leftMin > rightMin)
                {
                    return 1;
                }
                else if (leftMin < rightMin)
                {
                    return -1;
                }
                else
                {
                    if (leftSec > rightSec)
                    {
                        return 1;
                    }
                    else if (leftSec < rightSec)
                    {
                        return -1;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            int[] callsPerHours = new int[24];
            List<CallData> calls = new List<CallData>();
            //CallData[] calls = new CallData[1000];
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
                CallData alma = new CallData();
                string[] lineData = line.Split(' ');
                alma.startH = Int32.Parse(lineData[0]);
                alma.startMin = Int32.Parse(lineData[1]);
                alma.startSec = Int32.Parse(lineData[2]);
                alma.endH = Int32.Parse(lineData[3]);
                alma.endMin = Int32.Parse(lineData[4]);
                alma.endSec = Int32.Parse(lineData[5]);
                calls.Add(alma);
                // 3. feladat
                callsPerHours[alma.startH] += 1;
                // 4. feladat
                int callLength = Mpbe(alma.endH, alma.endMin, alma.endSec) - Mpbe(alma.startH, alma.startMin, alma.startSec);
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
            string[] input = Console.ReadLine().Split(' ');
            int hour = Int32.Parse(input[0]);
            int min = Int32.Parse(input[1]);
            int sec = Int32.Parse(input[2]);
            int callIndexInTime = -1; // what's the index of the caller currenty in line
            for (int call = 0; call < calls.Count; call++)
            {
                bool isCorrectLine = false;
                if(calls[call].startH <= hour && hour <= calls[call].endH)  // óra
                {
                    if(calls[call].startMin <= min && min <= calls[call].endMin) // perc
                    {
                        if(calls[call].startSec <= sec && sec <= calls[call].endSec) // mp
                        {
                            isCorrectLine = true;
                        }
                    }
                }
                if(isCorrectLine == true)
                {
                    callIndexInTime = call;
                    break;
                }
            }
            if(callIndexInTime == -1)
            {
                Console.WriteLine("Nem volt beszélő.");
            }
            else
            {
                int waiting = 0;
                bool isWaiting = true;
                while(isWaiting)
                {
                    //if(!(CompareTime
                    //    (
                    //        calls[callIndexInTime + waiting + 1].startH, calls[callIndexInTime + waiting + 1].startMin, calls[callIndexInTime + waiting + 1].startSec, 
                    //        calls[callIndexInTime].endH, calls[callIndexInTime].endMin, calls[callIndexInTime].endSec
                    //    ) == -1 ))
                    //{
                    //    if(!(CompareTime
                    //    (
                    //        calls[callIndexInTime + waiting + 1].endH, calls[callIndexInTime + waiting + 1].endMin, calls[callIndexInTime + waiting + 1].endSec,
                    //        hour, min, sec
                    //    ) == -1))
                    //    {
                    //        isWaiting = false;
                    //    }
                    //}
                    if (isWaiting)
                    {
                        waiting += 1;
                    }
                }
                Console.WriteLine($"A varakozok szama: {waiting} a beszelo a {callIndexInTime + 1}. hivo.");
            }
            Console.Write("\n");
            #endregion
        }
    }
}
