using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace Week7
{
    public class BColor
    {
        public IntPtr Ptr { get; }
        public int Size { get; }

        public BColor(IntPtr cPtr, int cSize)
        {
            Ptr = cPtr;
            Size = cSize;
        }

        public BColor(Color color, PixelFormat pixelFormat)
        {
            switch (pixelFormat)
            {
                //here comes cases with pixel formats
                case PixelFormat.Format16bppGrayScale:
                    Ptr = new IntPtr((char)(0.299 * color.R + 0.587 * color.G + 0.114 * color.B));
                    Size = 2;
                    break;
                case PixelFormat.Format24bppRgb:
                    Ptr = Marshal.UnsafeAddrOfPinnedArrayElement(new byte[] { color.B, color.G, color.R }, 0);
                    Size = 3;
                    break;
                case PixelFormat.Format32bppRgb:
                case PixelFormat.Format32bppArgb:
                    Ptr = new IntPtr(color.ToArgb());
                    Size = 4;
                    break;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
