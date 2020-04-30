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
        public Log()
        {
            ontsnapteApen = new List<Aap>();
        }
        public void AddAap(Aap aap)
        {
            ontsnapteApen.Add(aap);
        }
        public string getSprongenLog()
        {
            StringBuilder sprongenLog = new StringBuilder();
            int meestesprongen = ontsnapteApen.Max(a => a.bezochteBomen.Count);
            for (int i = 0; i < meestesprongen; i++)
            {
                foreach (Aap aap in ontsnapteApen)
                {
                    if (i < aap.bezochteBomen.Count)
                    {
                        Boom currentBoom = aap.bezochteBomen[i];
                        sprongenLog.Append($"{aap.naam} is in tree {currentBoom.id} at ({currentBoom.xCoordinaat},{currentBoom.yCoordinaat})\n");
                    }
                }
            }
            return sprongenLog.ToString();
        }

    }
}
