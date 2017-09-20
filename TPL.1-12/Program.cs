using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TPL._1_12
{
    class Program
    {
        static void Main(string[] args)
        {
            var t = new Thread(FaultyThread);
            t.Start();
            t.Join();

            try
            {
                t = new Thread(BadFaultyThread);
                t.Start();
                //this exception could not be catched, it will throw to main thread, so if you 
                //want to control the thread exceptions, you should control it in inside of thread
            }
            catch (Exception e)
            {
                Console.WriteLine("We won't get here!");
            }
        }

        static void BadFaultyThread()
        {
            Console.WriteLine("Starting a faulty thread ...");
            Thread.Sleep(TimeSpan.FromSeconds(1));
            throw new Exception("Boom!");
        }

        static void FaultyThread()
        {
            try
            {
                Console.WriteLine("Starting a faulty thread ...");
                Thread.Sleep(TimeSpan.FromSeconds(1));
                throw new Exception("Boom!");
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception handle: {0}", e.Message);
            }
        }
    }
}
