using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Linq;

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

        [TestMethod]
        public void BitmapTest()
        {
            var assembly = AppDomain.CurrentDomain.GetAssemblies()
                .SingleOrDefault(a => a.GetName().Name == "Week7");         
            using (var stream = assembly.GetManifestResourceStream("Week7.example.bmp"))
            {
                var bitmap = (Bitmap)Image.FromStream(stream);

                var timer1 = new Timer();
                using (timer1.Start())
                {
                    using (var bitmapEditor = new BitmapEditor(bitmap))
                    {
                        for (int i = 0; i < bitmapEditor.Height; i++)
                            for (int j = 0; j < bitmapEditor.Width; j++)
                            {
                                bitmapEditor.SetPixel(j, i, Color.FromArgb(0, 0, 0));
                            }
                    }
                }

                var timer2 = new Timer();
                using (timer2.Start())
                {

                    for (int i = 0; i < bitmap.Height; i++)
                        for (int j = 0; j < bitmap.Width; j++)
                        {
                            bitmap.SetPixel(j, i, Color.FromArgb(0, 0, 0));
                        }
                }
                Assert.IsTrue(timer1.ElapsedMilliseconds - timer2.ElapsedMilliseconds < 0);
            }                     
        }
    }
}
