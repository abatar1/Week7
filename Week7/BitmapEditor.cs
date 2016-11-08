using System;
using System.Drawing;
using System.Drawing.Imaging;

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

        public unsafe void SetPixel(int x, int y, Color color)
        {
            if (x > bitmap.Width || y > bitmap.Height)
                throw new IndexOutOfRangeException();

            var ptr = bitmapData.Scan0;
            var offset = bitmapData.Stride;

            switch (bitmapData.PixelFormat)
            {
                //here comes cases with pixel formats
                //case PixelFormat.Format16bppRgb:
                //case PixelFormat.Format24bppRgb:
                //etc.
                case PixelFormat.Format32bppRgb:
                case PixelFormat.Format32bppArgb:
                    *((int*)(ptr + offset * y) + x) = color.ToArgb();
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
