using System;
using System.Collections.Generic;

namespace EscapeFromTheWoods
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Bos bos = new Bos(new List<int>() { 5, 5 }, 5, 10,10);
            bos.Start();
        }
    }
}
