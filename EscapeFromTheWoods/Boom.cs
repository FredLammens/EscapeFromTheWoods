using System;
using System.Collections.Generic;
using System.Text;

namespace EscapeFromTheWoods
{
    class Boom
    {
        Random r = new Random();
        public int xCoordinaat { get; private set; }
        public int yCoordinaat { get; private set; }
        private int id;
        public List<Aap> apenInBoom { get; private set; }
        public Boom(int xCoordinaat,int yCoordinaat, int id)
        {
            this.xCoordinaat = xCoordinaat;
            this.yCoordinaat = yCoordinaat;
            this.id = id;
            apenInBoom = new List<Aap>();
        }
        public Boom(Boom obj) //voor deepcopy
        {
            this.xCoordinaat = obj.xCoordinaat;
            this.yCoordinaat = obj.yCoordinaat;
            this.id = obj.id;
            this.apenInBoom = obj.apenInBoom;
        }
    }
}
