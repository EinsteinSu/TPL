using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TPL._2_5
{
    class Program
    {
        private static AutoResetEvent _workEvent = new AutoResetEvent(false);

        private static AutoResetEvent _mainEvent = new AutoResetEvent(false);

        static void Process(int seconds)
        {
            Console.WriteLine("Starting a long running work...");
            Thread.Sleep(TimeSpan.FromSeconds(seconds));
            Console.WriteLine("Wrok is done!");
            _workEvent.Set();
            Console.WriteLine("Waiting for a main thread to complete its work");
            _mainEvent.WaitOne();
            Console.WriteLine("Starting second operation...");
            Thread.Sleep(TimeSpan.FromSeconds(seconds));
            Console.WriteLine("Work is done!");
            _workEvent.Set();
        }

        static void Main(string[] args)
        {
            var t = new Thread(()=>Process(3));
            t.Start();
            Console.WriteLine("Waiting for another thread to complete work");
            _workEvent.WaitOne();
            Console.WriteLine("First operation is completed!");
            Console.WriteLine("Performing an operation on a main thread");
            _mainEvent.Set();
            Console.WriteLine("Now running the second operation on a second thread");
            _workEvent.WaitOne();
            Console.WriteLine("Second operation is completed!");
        }
    }
}
