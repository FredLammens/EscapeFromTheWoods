using System;
using System.Collections.Generic;
using System.Text;

namespace EscapeFromTheWoods
{
    class Boom
    {
        public int xCoordinaat { get; private set; }
        public int yCoordinaat { get; private set; }
        public int id { get; private set; }
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

        public override bool Equals(object obj)
        {
            return obj is Boom boom &&
                   xCoordinaat == boom.xCoordinaat &&
                   yCoordinaat == boom.yCoordinaat &&
                   id == boom.id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(xCoordinaat, yCoordinaat, id);
        }

        public override string ToString()
        {
            return $"id: {id} met x: {xCoordinaat} en y: {yCoordinaat}";
        }
    }
}
