using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace EscapeFromTheWoods
{
    class TekstBestand
    {
        public static async Task MaakTekstBestand(Bos bos, string path) 
        {
            using (StreamWriter sw = new StreamWriter(Path.Combine(path, $"{bos.id}_log.txt")))
            {
                    await sw.WriteLineAsync(bos.log.getSprongenLog());
            }
        }
    }
}
