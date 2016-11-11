using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace Week7
{
    public class BitmapEditor : IDisposable
    {
        private BitmapData bitmapData;
        private Bitmap bitmap;

        public int Width { get { return bitmap.Width; } }
        public int Height { get { return bitmap.Height; } }
        public Bitmap Bitmap { get { return bitmap; } }

        public BitmapEditor(Bitmap _bitmap)
        {
            bitmap = new Bitmap(_bitmap);
            Rectangle rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);

            bitmapData = bitmap.LockBits(rect, ImageLockMode.ReadWrite, bitmap.PixelFormat);
        }
       
        public void SetPixel(int x, int y, Color color)
        {
            if (x > bitmap.Width || y > bitmap.Height)
                throw new IndexOutOfRangeException();

            var bColor = new BColor(color, bitmap.PixelFormat);
            var ptr = bitmapData.Scan0;
            var offset = Math.Abs(bitmapData.Stride) * y + bColor.Size * x;
            Marshal.WriteIntPtr(ptr + offset, bColor.Ptr);
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
