using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using System.Drawing;

namespace Week7.Test
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void TimerTest()
        {
            var timer = new Timer();
            using (timer.Start())
            {
                Thread.Sleep(TimeSpan.FromSeconds(1));
            }
            Assert.AreEqual(timer.ElapsedSeconds, 1);

            using (timer.Continue())
            {
                Thread.Sleep(TimeSpan.FromSeconds(2));
            }
            Assert.AreEqual(timer.ElapsedSeconds, 3);

            using (timer.Start())
            {
                Thread.Sleep(TimeSpan.FromSeconds(3));
            }
            Assert.AreEqual(timer.ElapsedSeconds, 3);
        }

        public void BitmapTest()
        {
            string filename = "";
            var bitmap = (Bitmap)Image.FromFile(filename);

            var timer1 = new Timer();
            using (timer1.Start())
            {
                using (var bitmapEditor = new BitmapEditor(bitmap))
                {
                    for (int i = 0; i < bitmapEditor.Height; i++)
                        for (int j = 0; j < bitmapEditor.Width; j++)
                        {
                            bitmapEditor.SetPixel(i, j, Color.FromArgb(255, 255, 255));
                        }
                }
            }

            var timer2 = new Timer();
            using (timer2.Start())
            {
                for (int i = 0; i < bitmap.Height; i++)
                    for (int j = 0; j < bitmap.Width; j++)
                    {
                        bitmap.SetPixel(i, j, Color.FromArgb(255, 255, 255));
                    }
            }

            Assert.IsTrue(timer1.ElapsedMilliseconds - timer2.ElapsedMilliseconds < 0);
        }
    }
}
