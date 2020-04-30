using Syroot.Windows.IO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EscapeFromTheWoods
{
    class OutputFactory
    {
        public static void Keuze(Bos bos)
        {
            KnownFolder downloads = new KnownFolder(KnownFolderType.Downloads);
            Databank db = new Databank(@"Data Source=DESKTOP-OF28PIK\SQLEXPRESS;Initial Catalog=EscapeFromTheWoods;Integrated Security=True");
            Task datab = db.InsertAll(bos);
            BitmapMaker bmm = new BitmapMaker(bos);
            Task bitmap = Task.Run(() => bmm.MaakBitmap(downloads.Path));
            Task txt = TekstBestand.MaakTekstBestand(bos, downloads.Path);
            //Databank.MakeXMlFile(downloads.Path, bos);
            Task.WaitAll(datab,txt,bitmap);
        }
    }
}
