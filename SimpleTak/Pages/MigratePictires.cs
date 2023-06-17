using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace SimpleTak.Pages
{
    class MigratePictires
    {
        private const string pathPrefix = @"url";
        public static void Migrate()
        {
            foreach (var supplier in MainWindow.Context.User)
            {
                var image = new BitmapImage(new Uri(pathPrefix + supplier.PathString));
                var encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(image));

                using (var stream = new MemoryStream())
                {
                    encoder.Save(stream);
                    supplier.Image = stream.ToArray();
                }
            }
            MainWindow.Context.SaveChanges();
        }
    }
}
