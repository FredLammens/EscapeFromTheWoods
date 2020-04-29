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
        public string GeefMessage(Aap aap) 
        {
            StringBuilder message = new StringBuilder();
            string naam = aap.naam;
            foreach (Boom bezochteBoom in aap.bezochteBomen)
            {
                message.Append($"{naam} is now in tree {bezochteBoom.id} at location ({bezochteBoom.xCoordinaat},{bezochteBoom.yCoordinaat})\n");
            }
            return message.ToString();
        }
    }
}
