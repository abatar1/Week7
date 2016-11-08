using System;
using System.Diagnostics;

namespace Week7
{
    public class Timer : IDisposable 
    {
        private Stopwatch stopWatch;

        public long ElapsedMilliseconds
        {
            get
            {
                return stopWatch.ElapsedMilliseconds;
            }
        }    
        
        public long ElapsedSeconds
        {
            get
            {
                return (long)Math.Round(stopWatch.ElapsedMilliseconds / 1000.0);
            }
        }

        public long ElapsedTicks
        {
            get
            {
                return stopWatch.ElapsedTicks;
            }
        }

        public Timer Start()
        {
            stopWatch = new Stopwatch();
            stopWatch.Start();
            return this;
        }

        public Timer Continue()
        {
            stopWatch.Start();
            return this;
        }

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    stopWatch.Stop();                   
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
