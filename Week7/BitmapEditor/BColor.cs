using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace Week7
{
    public class BColor
    {       
        private int size;

        public IntPtr Ptr { get; }
        public int Size { get { return size / 8; } }

        public BColor(Color color, PixelFormat pixelFormat)
        {
            switch (pixelFormat)
            {
                //here comes cases with pixel formats
                case PixelFormat.Format16bppGrayScale:
                    Ptr = new IntPtr((char)(0.299 * color.R + 0.587 * color.G + 0.114 * color.B));
                    size = 16;
                    break;
                case PixelFormat.Format24bppRgb:
                    Ptr = Marshal.UnsafeAddrOfPinnedArrayElement(new byte[] { color.B, color.G, color.R }, 0);
                    size = 24;
                    break;
                case PixelFormat.Format32bppRgb:
                case PixelFormat.Format32bppArgb:
                    Ptr = new IntPtr(color.ToArgb());
                    size = 32;
                    break;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
