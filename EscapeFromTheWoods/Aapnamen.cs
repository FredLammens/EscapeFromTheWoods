using System;
using System.Collections.Generic;
using System.Text;

namespace EscapeFromTheWoods
{
    enum AapNamenEnum
    {
        Anna,
        Adam,
        Amy,
        Abel,
        Anne,
        Amber,
        Arthur,
        Peter,
        Ayden,
        Ahmed,
        Anthony,
        Daan,
        Aiko,
        Tom,
        Luis
    }
    class Aapnamen
    {
        public static string GetRandomNaam()
        {
            var namen = Enum.GetNames(typeof(AapNamenEnum));
            var rnd = new Random();
            return namen[rnd.Next(0, namen.Length)];
        }
    }

}
