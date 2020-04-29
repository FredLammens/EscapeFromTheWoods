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
