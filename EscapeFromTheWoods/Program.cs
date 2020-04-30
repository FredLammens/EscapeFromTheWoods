using System;
using System.Collections.Generic;

namespace EscapeFromTheWoods
{
    class Program
    {
        static void Main(string[] args)
        {
            Bos bos = new Bos(new List<int>() { 10, 10 }, 5, 20,5);
            bos.Start();
            OutputFactory o = new OutputFactory(bos,"databank");
            //Console.Clear();
            //bos.log.PrintLog();
            //BitmapMaker bm = new BitmapMaker(bos);
            //bm.maakBitmap(@"C:\Users\Biebem\Downloads");
        }
    }
}
