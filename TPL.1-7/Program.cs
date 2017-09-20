using System;
using System.Diagnostics;
using System.Threading;

namespace TPL._1_7
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Current Thread Priority {0}", Thread.CurrentThread.Priority);
            Console.WriteLine("Running on all cores available");
            RunThreads();
            Thread.Sleep(TimeSpan.FromSeconds(2));
            Console.WriteLine("Running on a single core");
            //force arrange 1 cpu to process this code
            Process.GetCurrentProcess().ProcessorAffinity = new IntPtr(1);
            RunThreads();
            Console.Read();
        }

        private static void RunThreads()
        {
            var sample = new ThreadSample();
            var threadOne = new Thread(sample.CountNumbers);
            threadOne.Name = "Thread One";
            var threadTwo = new Thread(sample.CountNumbers);
            threadTwo.Name = "Thread Two";
            threadOne.Priority = ThreadPriority.Highest;
            threadTwo.Priority = ThreadPriority.Lowest;
            threadOne.Start();
            threadTwo.Start();
            Thread.Sleep(TimeSpan.FromSeconds(2));
            sample.Stop();
        }
    }

    internal class ThreadSample
    {
        private bool _isStopped;

        public void Stop()
        {
            _isStopped = true;
        }

        public void CountNumbers()
        {
            long counter = 0;
            while (!_isStopped)
                counter++;
            Console.WriteLine("{0} with {1,11} priority has a count = {2,13:N0}", Thread.CurrentThread.Name,
                Thread.CurrentThread.Priority, counter);
        }
    }
}