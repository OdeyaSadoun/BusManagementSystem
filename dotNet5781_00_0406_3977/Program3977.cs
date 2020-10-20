using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_00_0406_3977
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Welcome3977();
            Welcome0406();
            Console.ReadKey();
        }

        static partial void Welcome0406();

        private static void Welcome3977()
        {
            Console.Write("Enter your name: ");
            string userName = Console.ReadLine();
            Console.WriteLine("{0}, welcome to my first console application", userName);
        }

    }
}
