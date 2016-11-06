using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;

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
    }
}
