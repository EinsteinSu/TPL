using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TPL._2_8
{
    class Program
    {
        static Barrier _barrier = new Barrier(3, b => Console.WriteLine("End of phase {0}", b.CurrentPhaseNumber + 1));

        static void PlayMusic(string name, string message, int seconds)
        {
            for (int i = 0; i < 9; i++)
            {
                Console.WriteLine("---------------------------------------------");
                Thread.Sleep(TimeSpan.FromSeconds(seconds));
                Console.WriteLine("{0} starts to {1}", name, message);
                Thread.Sleep(TimeSpan.FromSeconds(seconds));
                Console.WriteLine("{0} finishes to {1}", name, message);
                _barrier.SignalAndWait();
            }

        }

        static void Main(string[] args)
        {
            var t1 = new Thread(() => PlayMusic("the guitarist", "play an amazing solo", 5));
            var t2 = new Thread(() => PlayMusic("the singer", "sing his song", 2));
            var t3 = new Thread(() => PlayMusic("the normal person", "sing someone's song", 2));
            t1.Start();
            t2.Start();
            t3.Start();
        }
    }
}
