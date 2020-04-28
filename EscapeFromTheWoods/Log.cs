using System;
using System.Collections.Generic;
using System.Text;

namespace EscapeFromTheWoods
{
    class Log
    {
        public List<Aap> ontsnapteApen { get; private set; }
        public Log()
        {
            ontsnapteApen = new List<Aap>();
        }
        public void AddAap(Aap aap) 
        {
            ontsnapteApen.Add(aap);
        }
        public void PrintLog() 
        {
            foreach (Aap aap in ontsnapteApen)
            {
                Console.WriteLine(aap);
            }
        }
    }
}
