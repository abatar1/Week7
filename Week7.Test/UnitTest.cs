using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using System.Drawing;
using System.Linq;

namespace Week7.Test
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void Timer_SimpleTest()
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
        public void Timer_CallContinueBeforeStart_Exception()
        {
            var timer = new Timer();
            try
            {
                using (timer.Continue())
                {
                    Thread.Sleep(TimeSpan.FromSeconds(2));
                }
                Assert.Fail();
            }
            catch (Exception e)
            {
                if (!(e is TimerNotRunningException))
                    Assert.Fail();
            }
        }

        [TestMethod]
        public void Timer_StartTest()
        {
            var timer = new Timer();
            using (timer.Start())
            {
                Thread.Sleep(TimeSpan.FromSeconds(1));
            }
            Assert.AreEqual(timer.ElapsedSeconds, 1);

            using (timer.Start())
            {
                Thread.Sleep(TimeSpan.FromSeconds(2));
            }
            Assert.AreEqual(timer.ElapsedSeconds, 2);
        }

        [TestMethod]
        public void Bitmap_TimeTest()
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
