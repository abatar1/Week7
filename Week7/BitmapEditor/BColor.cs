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
                    var c16 = (short)((color.R * 31 / 255) << (11) | ((color.G * 63 / 255) << 5) | color.B);
                    Ptr = new IntPtr(c16);
                    size = 16;
                    break;
                case PixelFormat.Format24bppRgb:
                    var c24 = color.ToArgb() << 8;
                    Ptr = new IntPtr(c24);
                    size = 24;
                    break;
                case PixelFormat.Format32bppRgb:
                case PixelFormat.Format32bppArgb:
                    var c32 = color.ToArgb();
                    Ptr = new IntPtr(c32);
                    size = 32;
                    break;
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
