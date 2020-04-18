using System;
using System.Collections.Generic;
using System.Text;

namespace EscapeFromTheWoods
{
    class Aap
    {
        private int id;
        private string naam;
        public List<Boom> bezochteBomen { get; private set; }
        public Aap(Boom startBoom, int id, string naam)
        {
            bezochteBomen = new List<Boom>
            {
                startBoom
            };
            this.id = id;
            this.naam = naam;
        }
        public void Spring(Boom dichtsteBoom) 
        {
            bezochteBomen.Add(dichtsteBoom);
            dichtsteBoom.apenInBoom.Remove(this);
        }

        public override bool Equals(object obj)
        {
            return obj is Aap aap &&
                   id == aap.id &&
                   naam == aap.naam;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(id, naam);
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
