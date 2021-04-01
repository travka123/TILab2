using System;
using System.Numerics;

namespace RSA_Decrypt
{
    class Program
    {
        public static BigInteger ModPow(BigInteger Number, BigInteger Pow, BigInteger Mod)
        {
            BigInteger res = 1;
            while (Pow >= 1)
            {
                if ((Pow & 1) == 1)
                {
                    res = res * Number % Mod;
                }
                Number = Number * Number % Mod;
                Pow >>= 1;
            }
            return res;
        }

        static void Main(string[] args)
        {
            Console.Write("Введите d: ");
            int d = Convert.ToInt32(Console.ReadLine());
            Console.Write("Введите r: ");
            int r = Convert.ToInt32(Console.ReadLine());
            Console.Write("Введите шифр: ");
            string message = Console.ReadLine();
            foreach (var strnum in message.Split(' '))
            {
                BigInteger bnum = BigInteger.Parse(strnum);
                bnum = ModPow(bnum, d, r);
                Console.Write(Convert.ToChar(bnum.ToByteArray()[0]));
            }
            Console.WriteLine();
        }
    }
}
