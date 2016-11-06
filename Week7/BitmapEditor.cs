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

        private int bytes;
        private byte[] rgbValues;

        public int Width { get { return bitmap.Width; } }
        public int Height { get { return bitmap.Height; } }

        public BitmapEditor(Bitmap _bitmap)
        {
            bitmap = new Bitmap(_bitmap);
            Rectangle rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            
            bitmapData = bitmap.LockBits(rect, ImageLockMode.ReadWrite, bitmap.PixelFormat);

            bytes = Math.Abs(bitmapData.Stride) * bitmap.Height;
            rgbValues = new byte[bytes];

            Marshal.Copy(bitmapData.Scan0, rgbValues, 0, bytes);
        }

        public unsafe void SetPixel(int x, int y, Color color)
        {
            if (x > bitmap.Height || y > bitmap.Width)
                throw new IndexOutOfRangeException();

            switch (bitmapData.PixelFormat)
            {
                case PixelFormat.Format24bppRgb:
                    byte* pos = (byte*)bitmapData.Scan0 + x * 3 + bitmapData.Stride * y;
                    int rawColor = color.ToArgb();
                    pos[0] = (byte)(rawColor & 0xFF);
                    pos[1] = (byte)((rawColor >> 8) & 0xFF);
                    pos[2] = (byte)((rawColor >> 16) & 0xFF);
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
                    Marshal.Copy(rgbValues, 0, bitmapData.Scan0, bytes);
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
