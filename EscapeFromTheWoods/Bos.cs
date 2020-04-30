using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EscapeFromTheWoods
{
    class Bos
    {
        public int xMaxWaarde { get; private set; }
        public int xMinWaarde { get; private set; } = 0;
        public int yMaxWaarde { get; private set; }
        public int yMinWaarde { get; private set; } = 0;
        public int id { get; private set; }
        public Log log { get; private set; }
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
            log = new Log();
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
            for (int i = 0; i < aantalBomen; i++)
            {
                int xCoordinaat = r.Next(xMinWaarde, xMaxWaarde);
                int yCoordinaat = r.Next(yMinWaarde, yMaxWaarde);
                Boom randomBoom = new Boom(xCoordinaat, yCoordinaat, i);
                if (!bomen.Contains(randomBoom))
                {
                    bomen.Add(randomBoom);
                }
                else
                {
                    i--;
                }

            }

        }
        /// <summary>
        /// Initialiseerd bos met gegenereerde apen
        /// indien begrenzin maar 2 waardes heeft : xmax,ymax
        /// </summary>
        /// <param name="begrenzing">xmin,xmax,ymin,ymax</param>
        /// <param name="aantalBomen">generate aantal bomen</param>
        /// <param name="aantalApen">generate aantal apen</param>
        public Bos(List<int> begrenzing, int id, int aantalBomen, int aantalApen) : this(begrenzing, id, aantalBomen)
        {
            if (aantalApen <= bomen.Count)
            {
                for (int i = 0; i < aantalApen; i++)
                {
                    InitialiseerAap(new Aap(bomen[i], i, Aapnamen.GetRandomNaam()));
                    //Aapnamen.AapNamenEnum naam = (Aapnamen.AapNamenEnum)i;
                    //InitialiseerAap(new Aap(bomen[i], i, naam.ToString()));
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
        public Bos(List<int> begrenzing, int id, int aantalBomen, List<Aap> apen) : this(begrenzing, id, aantalBomen)
        {
            InitialiseerApen(apen);
        }
        /// <summary>
        /// Laat alle apen springen tot er geen meer in het bos zijn.
        /// </summary>
        public void Start()
        {
            Console.WriteLine("gestart");
            for (int i = apen.Count - 1; i != -1; i--)
            {
                SpringAap(apen[i]);
            }
        }
        public void SpringAap(Aap aap)
        {
            Tuple<double, Boom> result = null;
            double lengteRand = 0;
            Task zoekBoom = Task.Run(() => { result = ZoekDichtsteBoom(aap); });
            Task lengte = Task.Run(() => { lengteRand = LengteTussenAapEnRand(aap);});
            Task.WaitAll(zoekBoom, lengte);
            double dichtsteLengte = result.Item1;
            Boom dichtsteBoom = result.Item2;
            if (dichtsteLengte < lengteRand)
            {
                aap.Spring(dichtsteBoom);
                SpringAap(aap);
            }
            else
            {
                VerwijderAap(aap);
            }
        }

        private void InitialiseerAap(Aap aap)
        {
            //voeg aap toe aan lijst van apen
            apen.Add(aap);
            //update lijst van bomen met boom met aap in 
            int teUpdatenBoom = bomen.IndexOf(aap.bezochteBomen[0]);
            bomen[teUpdatenBoom].apenInBoom.Add(aap);
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
                log.AddAap(aap);
                apen.Remove(aap);
                Console.WriteLine($"Aap {aap} verwijdert uit lijst van apen.");
            }
            else
            {
                Console.WriteLine($"Aap {aap} niet gevonden in lijst van apen");
            }
        }
        private Tuple<double,Boom> ZoekDichtsteBoom(Aap aap)
        {
            Boom huidigeBoom = aap.bezochteBomen.Last();
            double dichtste = double.MaxValue;
            Boom dichtsteBoom = null;
            foreach (Boom boom in bomen)
            {
                if (!aap.bezochteBomen.Contains(boom))
                {
                    double lengte = LengteTussenBomen(boom, huidigeBoom);
                    if (lengte != 0)
                    {
                        if (lengte < dichtste)
                        {
                            dichtste = lengte;
                            dichtsteBoom = boom;
                        }
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
            return Math.Abs(lengte);
        }
        private double LengteTussenAapEnRand(Aap aap)
        {
            Boom currentboom = aap.bezochteBomen.Last();
            double lengte = (new List<double>() { yMaxWaarde - currentboom.yCoordinaat,
                                                  xMaxWaarde - currentboom.xCoordinaat,
                                                  currentboom.yCoordinaat - yMinWaarde,
                                                  currentboom.xCoordinaat - xMinWaarde }).Min();
            return lengte;
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
