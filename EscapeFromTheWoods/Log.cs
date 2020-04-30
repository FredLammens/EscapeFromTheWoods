using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;

namespace EscapeFromTheWoods
{
    class Log
    {
        public List<Aap> ontsnapteApen { get; private set; }
        public List<String> sprongenLogs { get; private set; }
        public Log()
        {
            ontsnapteApen = new List<Aap>();
            sprongenLogs = new List<string>();
        }
        public void AddAap(Aap aap)
        {
            ontsnapteApen.Add(aap);
        }
        public void AddLog(Aap aap) 
        {
            Boom currentBoom = aap.bezochteBomen.Last();
            sprongenLogs.Add($"{aap.naam} is in tree {currentBoom.id} at ({currentBoom.xCoordinaat},{currentBoom.yCoordinaat})");
        }
    }
}
