using Syroot.Windows.IO;
using System;
using System.Collections.Generic;
using System.Text;

namespace EscapeFromTheWoods
{
    class OutputFactory
    {
        public OutputFactory(Bos bos,string output)
        {
            KnownFolder downloads = new KnownFolder(KnownFolderType.Downloads);

            switch (output.ToLower().Trim())
            {
                case "databank":
                    Databank db = new Databank(@"Data Source=DESKTOP-OF28PIK\SQLEXPRESS;Initial Catalog=EscapeFromTheWoods;Integrated Security=True");
                    db.InsertAll(bos);
                    break;
                case "bitmap":
                    BitmapMaker bmm = new BitmapMaker(bos);
                    bmm.maakBitmap(downloads.Path);
                    break;
                case "tekstbestand":
                    TekstBestand.MaakTekstBestand(bos, downloads.Path);
                    break;
                case "xml":
                    Databank.MakeXMlFile(downloads.Path, bos);
                    break;
                default:
                    Console.WriteLine("geen correcte output ingegeven.");
                    break;
            }
        }
    }
}
