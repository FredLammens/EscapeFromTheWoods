using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace EscapeFromTheWoods
{
    class Aap
    {
        public int id { get; private set; }
        public string naam { get; private set; }
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
                Console.WriteLine($"aap : {naam} is van {bezochteBomen.Last().xCoordinaat} , {bezochteBomen.Last().yCoordinaat} naar {dichtsteBoom.xCoordinaat}, {dichtsteBoom.yCoordinaat} Gesprongen");
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
            return $"naam: {naam} id: {id}";
        }
    }
}
