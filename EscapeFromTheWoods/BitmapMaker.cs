using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace EscapeFromTheWoods
{
    class BitmapMaker
    {
        Bos bos;
        public BitmapMaker(Bos bos)
        {
            this.bos = bos;
        }
        public void MaakBitmap(string path)
        {
            using (MemoryStream Vista = new MemoryStream())
            {
                Bitmap bm = new Bitmap((bos.xMaxWaarde - bos.xMinWaarde) * 20, (bos.yMaxWaarde - bos.yMinWaarde) * 20);
                Draw(bm);
                bm.Save(Path.Combine(path, bos.id + "_escapeRoutes.png"), ImageFormat.Png);
            }
        }
        private void Draw(Bitmap bm)
        {
            Graphics g = Graphics.FromImage(bm);
            //bomen
            Pen pen = new Pen(Color.GreenYellow, 1);
            foreach (Boom boom in bos.bomen)
            {
                g.DrawEllipse(pen, boom.xCoordinaat * 20, boom.yCoordinaat * 20, 17, 17);
            }
            //path
            Random r = new Random();
            Color kleurke;
            foreach (Aap aap in bos.log.ontsnapteApen)
            {
                kleurke = Color.FromArgb(r.Next(1, 256), r.Next(1, 256), r.Next(1, 256));
                for (int i = 0; i < aap.bezochteBomen.Count - 1; i++)
                {
                    pen.Color = kleurke;
                    Boom currentBoom = aap.bezochteBomen[i];
                    Boom nextBoom = aap.bezochteBomen[i + 1];
                    g.DrawLine(pen, currentBoom.xCoordinaat * 20 + 10, currentBoom.yCoordinaat * 20 + 10, nextBoom.xCoordinaat * 20 + 10, nextBoom.yCoordinaat * 20 + 10);
                }
                Boom eersteBoom = aap.bezochteBomen[0];
                g.FillEllipse(new SolidBrush(kleurke), eersteBoom.xCoordinaat * 20, eersteBoom.yCoordinaat * 20, 17, 17);
            }
        }
    }
}
