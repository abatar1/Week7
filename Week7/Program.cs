using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;

namespace Week7
{
    public class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            var assembly = AppDomain.CurrentDomain.GetAssemblies()
                .SingleOrDefault(a => a.GetName().Name == "Week7");
            var stream = assembly.GetManifestResourceStream("Week7.example.bmp");
            Bitmap bitmap = new Bitmap(stream);
            BitmapEditor bitmapEditor;
            using (bitmapEditor = new BitmapEditor(bitmap))
            {
                for (int i = 0; i < bitmapEditor.Height; i++)
                    for (int j = 0; j < bitmapEditor.Width; j++)
                    {
                        bitmapEditor.SetPixel(j, i, Color.FromArgb(255, 0, 0));
                    }
            }

            var form = new Form();
            form.Size = bitmap.Size;
            var pBox = new PictureBox();
            pBox.Image = bitmapEditor.Bitmap;
            pBox.Size = bitmap.Size;
            form.Controls.Add(pBox);
            form.ShowDialog();
        }
    }
}
