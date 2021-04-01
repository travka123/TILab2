using System;
using System.Numerics;
using System.Text;

namespace RSA_CS
{
    class Program
    {
        private static BigInteger q;
        private static BigInteger p;
        private static BigInteger r;
        private static BigInteger[] message;
        private static BigInteger fEuler;
        private static BigInteger e;
        private static BigInteger d;

        public static BigInteger GetD()
        {
            BigInteger d0 = fEuler, d1 = e, x0 = 1, x1 = 0, y0 = 0, y1 = 1, q, d2, x2, y2;
            while (d1 > 1)
            {
                q = d0 / d1;
                d2 = d0 % d1;
                x2 = x0 - q * x1;
                y2 = y0 - q * y1;
                d0 = d1;
                d1 = d2;
                x0 = x1;
                x1 = x2;
                y0 = y1;
                y1 = y2;
            }
            return (y1 > 0) ? y1 : y1 + fEuler;
        }

        public static bool IsCoprime(BigInteger num1, BigInteger num2)
        {
            BigInteger temp;
            while (num2 != 0)
            {
                num1 %= num2;
                temp = num2;
                num2 = num1;
                num1 = temp;
            }
            return num1 == 1;
        }

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

        static void Encrypt()
        {
            r = BigInteger.Multiply(q, p);
            fEuler = BigInteger.Multiply(p - 1, q - 1);
            e = 7;
            while (!IsCoprime(fEuler, e) && (fEuler > e))
            {
                e++;
            }
            if (e == fEuler)
            {
                Console.WriteLine("Не удалось подобрать e");
                return;
            }
            Console.WriteLine("Открытый ключ (e, r): " + e.ToString() + ", " + r.ToString());

            d = GetD();
            Console.WriteLine("Закрытый ключ (d, r): " + d.ToString() + ", " + r.ToString());
            foreach (var s in message)
            {
                BigInteger sh = ModPow(s, e, r);
                Console.Write(sh.ToString() + " ");
                Console.WriteLine(Convert.ToChar( ModPow(sh, d, r).ToByteArray()[0] ));
            }
        }

        static void Main(string[] args)
        {

            Console.Write("Введите p: ");
            p = BigInteger.Parse(Console.ReadLine());
            Console.Write("Введите q: ");
            q = BigInteger.Parse(Console.ReadLine());

            Console.Write("Введите сообщение: ");
            byte[] bytes = Encoding.ASCII.GetBytes(Console.ReadLine());
            message = new BigInteger[bytes.Length];
            int i = 0;
            foreach (byte val in bytes)
            {
                message[i] = val;
                i++;
            }

            Encrypt();
        }
    }
}
