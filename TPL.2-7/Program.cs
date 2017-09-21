using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TPL._2_7
{
    class Program
    {
        static CountdownEvent _countdown = new CountdownEvent(2);

        static void PerformanOperation(string message, int seconds)
        {
            Thread.Sleep(TimeSpan.FromSeconds(seconds));
            Console.WriteLine(message);
            _countdown.Signal();
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Starting two operations");

            var t1 = new Thread(() => PerformanOperation("Operation 1 is completed", 4));
            var t2 = new Thread(() => PerformanOperation("Operation 2 is completed", 8));
            t1.Start();
            t2.Start();
            _countdown.Wait();//waiting for total 2 theards to complete.
            Console.WriteLine("Both operations have been completed.");
            _countdown.Dispose();
        }
    }
}
