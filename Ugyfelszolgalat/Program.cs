using System;

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
            
        }
    }
}
