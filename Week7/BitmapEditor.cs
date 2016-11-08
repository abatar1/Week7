using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace Week7
{
    public class BitmapEditor : IDisposable
    {
        private BitmapData bitmapData;
        private Bitmap bitmap;  
            
        private IntPtr ptr;

        public int Width { get { return bitmap.Width; } }
        public int Height { get { return bitmap.Height; } }
        public Bitmap Bitmap { get { return bitmap; } }

        public BitmapEditor(Bitmap _bitmap)
        {
            bitmap = new Bitmap(_bitmap);
            Rectangle rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            
            bitmapData = bitmap.LockBits(rect, ImageLockMode.ReadWrite, bitmap.PixelFormat);
            ptr = bitmapData.Scan0;
        }

        public unsafe void SetPixel(int x, int y, Color color)
        {
            if (x > bitmap.Width || y > bitmap.Height)
                throw new IndexOutOfRangeException();

            var rawColor = color.ToArgb();
            var offset = bitmapData.Stride * y + x;

            switch (bitmapData.PixelFormat)
            {
                case PixelFormat.Format32bppRgb:
                case PixelFormat.Format32bppArgb:
                    *((int*)(ptr + bitmapData.Stride * y) + x) = rawColor;
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    bitmap.UnlockBits(bitmapData);
                }
                           
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
