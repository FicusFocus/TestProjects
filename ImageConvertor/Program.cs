using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace ImageConvertor
{
    class Program
    {
        private const double WIDTH_OFFSET = 1.5;
        private const int MAX_WIDTH = 350;

        [STAThread]

        static void Main(string[] args)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Images | *.bmp; *.png; *.jpg; *JPEG;"
            };

            while (true)
            {
                Console.ReadLine();

                if (openFileDialog.ShowDialog() != DialogResult.OK)
                    continue;

                Console.Clear();

                Bitmap bitmap = new Bitmap(openFileDialog.FileName);
                bitmap = ResizeBitmap(bitmap);
                bitmap.ToGrayscale();

                BitmapToAsciiConverter converter = new BitmapToAsciiConverter(bitmap);
                char[][] rows = converter.Convert();

                foreach (var row in rows)
                    Console.WriteLine(row);

                Console.SetCursorPosition(0, 0);
            }
        }

        public static Bitmap ResizeBitmap(Bitmap bitmap)
        {
            double newHeight = bitmap.Height / WIDTH_OFFSET * MAX_WIDTH * bitmap.Width;
            if (bitmap.Width > MAX_WIDTH || bitmap.Height > newHeight)
                bitmap = new Bitmap(bitmap, new Size(MAX_WIDTH, (int)newHeight));

            return bitmap;
        }
    }
}
