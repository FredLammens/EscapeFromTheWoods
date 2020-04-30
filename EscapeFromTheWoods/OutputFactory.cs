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
            KnownFolder documents = new KnownFolder(KnownFolderType.Documents);

            switch (output.ToLower().Trim())
            {
                case "databank":
                    Databank db = new Databank(@"Data Source=DESKTOP-OF28PIK\SQLEXPRESS;Initial Catalog=EscapeFromTheWoods;Integrated Security=True");
                    db.InsertAll(bos);
                    break;
                case "bitmap":
                    BitmapMaker bmm = new BitmapMaker(bos);
                    bmm.maakBitmap(documents.Path);
                    break;
                case "tekstbestand":
                    break;
                default:
                    Console.WriteLine("geen correcte output ingegeven.");
                    break;
            }
        }
    }
}
