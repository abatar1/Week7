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

            var ptr = bitmapData.Scan0;
            var offset = bitmapData.Stride * y;

            switch (bitmapData.PixelFormat)
            {
                //here comes cases with pixel formats
                case PixelFormat.Format24bppRgb:
                    cPtr = Marshal.AllocHGlobal(24);
                    Marshal.WriteByte(cPtr, color.B);
                    Marshal.WriteByte(cPtr + 1, color.G);
                    Marshal.WriteByte(cPtr + 2, color.R);
                    Marshal.WriteIntPtr(ptr + offset + x * 3, cPtr);
                    Marshal.FreeHGlobal(cPtr);
                    break;
                case PixelFormat.Format32bppRgb:                
                case PixelFormat.Format32bppArgb:
                    Marshal.WriteInt32(ptr + offset + x * 4, color.ToArgb());
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
