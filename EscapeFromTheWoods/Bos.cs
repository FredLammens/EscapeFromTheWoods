using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscapeFromTheWoods
{
    class Bos
    {
        private int xMaxWaarde;
        private int xMinWaarde = 0;
        private int yMaxWaarde;
        private int yMinWaarde = 0;
        private int id;
        public List<Aap> apen { get; private set; }
        public List<Boom> bomen { get; private set; }
        /// <summary>
        /// Initialiseerd bos zonder apen
        /// indien begrenzin maar 2 waardes heeft : xmax,ymax
        /// </summary>
        /// <param name="begrenzing">xmin,xmax,ymin,ymax</param>
        /// <param name="aantalBomen">generate aantal bomen</param>
        public Bos(List<int> begrenzing, int id, int aantalBomen)
        {
            this.id = id;
            bomen = new List<Boom>();
            apen = new List<Aap>();
            if (begrenzing.Count > 2)
            {
                this.xMinWaarde = begrenzing[0];
                this.xMaxWaarde = begrenzing[1];
                this.yMinWaarde = begrenzing[2];
                this.yMaxWaarde = begrenzing[3];
            }
            else
            {
                this.xMaxWaarde = begrenzing[0];
                this.yMaxWaarde = begrenzing[1];
            }
            //Initialiseer random bomen 
            Random r = new Random();
            for (int i = 1; i < aantalBomen; i++)
            {
                int xCoordinaat = r.Next(xMinWaarde, xMaxWaarde);
                int yCoordinaat = r.Next(yMinWaarde, yMaxWaarde);
                Boom randomBoom = new Boom(xCoordinaat, yCoordinaat, i);
                bomen.Add(randomBoom);
            }

        }
        /// <summary>
        /// Initialiseerd bos met gegenereerde apen
        /// indien begrenzin maar 2 waardes heeft : xmax,ymax
        /// </summary>
        /// <param name="begrenzing">xmin,xmax,ymin,ymax</param>
        /// <param name="aantalBomen">generate aantal bomen</param>
        /// <param name="aantalApen">generate aantal apen</param>
        public Bos(List<int> begrenzing, int id , int aantalBomen, int aantalApen) : this(begrenzing, id,aantalBomen)
        {
            if (aantalApen <= bomen.Count + 1)
            {
                for (int i = 0; i < bomen.Count; i++)
                {
                    InitialiseerAap(new Aap(bomen[i], i, Aapnamen.GetRandomNaam()));
                }
            }
            else
            {
                Console.WriteLine("er kunnen niet meer apen dan bomen zijn voor initialisatie");
                throw new ArgumentException();
            }
        }
        /// <summary>
        /// initialiseerd bos met zelf ingestoken apen
        /// indien begrenzin maar 2 waardes heeft : xmax,ymax
        /// </summary>
        /// <param name="begrenzing">xmin,xmax,ymin,ymax</param>
        /// <param name="aantalBomen">generate aantal bomen</param>
        /// <param name="apen">eigen lijst van apen</param>
        public Bos(List<int> begrenzing, int id ,int aantalBomen, List<Aap> apen) : this(begrenzing,id,aantalBomen)
        {
            InitialiseerApen(apen);
        }
        /// <summary>
        /// Laat alle apen springen tot er geen meer in het bos zijn.
        /// </summary>
        public void Start()
        {
            Console.WriteLine("gestart");
            while (apen.Count != 0) 
            {
                foreach (Aap aap in apen.ToList())
                {
                    Tuple<double, Boom> result = ZoekDichtsteBoom(aap);
                    double dichtsteLengte = result.Item1;
                    Boom dichtsteBoom = result.Item2;
                    double lengteRand = LengteTussenAapEnRand(aap);
                    if (dichtsteLengte < lengteRand)
                    {
                        aap.Spring(dichtsteBoom);
                    }
                    else
                    {
                        VerwijderAap(aap);
                    }
                }
            }
        }

        private void InitialiseerAap(Aap aap)
        {
            //check of aap wel in een boom van het bos zit
            if (bomen.Contains(aap.bezochteBomen[0]))
            {
                //indien in de boom waar de aap in wilt zitten geen apen zit
                if (aap.bezochteBomen[0].apenInBoom.Count == 0)
                {
                    //voeg aap toe aan lijst van apen
                    apen.Add(aap);
                    //update lijst van bomen met boom met aap in 
                    int teUpdatenBoom = bomen.IndexOf(aap.bezochteBomen[0]);
                    bomen[teUpdatenBoom].apenInBoom.Add(aap);
                }
                else
                {
                    Console.WriteLine("Er zit al een aap in de boom");
                }
            }
            else
            {
                Console.WriteLine("Aap zit niet in een boom in dit bos.");
            }
        }
        private void InitialiseerApen(List<Aap> apen)
        {
            foreach (Aap aap in apen)
            {
                InitialiseerAap(aap);
            }
        }
        private void VerwijderAap(Aap aap)
        {
            if (apen.Contains(aap))
            {
                apen.Remove(aap);
                Console.WriteLine($"Aap {aap} verwijdert uit lijst van apen.");
            }
            else
            {
                Console.WriteLine($"Aap {aap} niet gevonden in lijst van apen");
                return;
            }
            foreach (Boom boom in bomen)
            {
                if (boom.apenInBoom.Contains(aap))
                {
                    boom.apenInBoom.Remove(aap);
                    Console.WriteLine($"aap : {aap} verwijdert uit boom {boom}");
                    return;
                }
            }
            Console.WriteLine($"aap is niet gevonden in een boom");
        }
        private Tuple<double, Boom> ZoekDichtsteBoom(Aap aap)
        {
            Boom huidigeBoom = aap.bezochteBomen.Last();
            double dichtste = double.MaxValue;
            Boom dichtsteBoom = null;
            foreach (Boom boom in bomen)
            {
                if (boom != huidigeBoom)
                {
                    double lengte = LengteTussenBomen(boom, huidigeBoom);
                    if (lengte < dichtste)
                    {
                        dichtste = lengte;
                        dichtsteBoom = boom;
                    }
                }
            }
            return new Tuple<double, Boom>(dichtste, dichtsteBoom);
        }
        private double LengteTussenBomen(Boom boom1, Boom boom2)
        {
            int verschilXWaarden = boom1.xCoordinaat - boom2.xCoordinaat;
            int verschilYWaarden = boom1.yCoordinaat - boom2.yCoordinaat;
            double lengte = Math.Sqrt(Math.Pow(verschilXWaarden, 2) + Math.Pow(verschilYWaarden, 2));
            return lengte;
        }
        private int LengteTussenAapEnRand(Aap aap)
        {
            Boom huidigeBoom = aap.bezochteBomen.Last();
            if (huidigeBoom.xCoordinaat > huidigeBoom.yCoordinaat) //punt dichter bij y-as => aspunt => x = 0 en y = hetzelfde
            {
                return huidigeBoom.xCoordinaat;
            }
            else
            {
                return huidigeBoom.yCoordinaat;
            }
        }

        public override bool Equals(object obj)
        {
            return obj is Bos bos &&
                   xMaxWaarde == bos.xMaxWaarde &&
                   xMinWaarde == bos.xMinWaarde &&
                   yMaxWaarde == bos.yMaxWaarde &&
                   yMinWaarde == bos.yMinWaarde &&
                   id == bos.id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(xMaxWaarde, xMinWaarde, yMaxWaarde, yMinWaarde, id);
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
