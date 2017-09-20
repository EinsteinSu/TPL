using System;
using System.Threading;

namespace TPL._1_9
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var sample = new ThreadSample(10);
            var threadOne = new Thread(sample.CountNumbers);
            threadOne.Name = "Thread One";
            threadOne.Start();
            threadOne.Join();
            Console.WriteLine("--------------------------------------------");
            var threadTwo = new Thread(Count);
            threadTwo.Name = "Thread Two";
            threadTwo.Start(8);
            threadTwo.Join(); //waiting for this thread executed
            Console.WriteLine("--------------------------------------------");
            var threadThree = new Thread(() => CountNumbers(12));
            threadThree.Name = "Thread Three";
            threadThree.Start();
            threadThree.Join();
            Console.WriteLine("--------------------------------------------");

            var i = 10;
            var threadFour = new Thread(() => PrintNumber(i));
            threadFour.Start();
            i = 20;
            var threadFive = new Thread(() => PrintNumber(i));
            threadFive.Start();
            //thread four and five will both print 20 numbers, cause i changed after thread four initialized
        }

        private static void Count(object iterations)
        {
            CountNumbers((int) iterations);
        }

        private static void CountNumbers(int iterations)
        {
            for (var i = 0; i < iterations; i++)
            {
                Thread.Sleep(TimeSpan.FromSeconds(0.5));
                Console.WriteLine("{0} prints {1}", Thread.CurrentThread.Name, i);
            }
        }

        private static void PrintNumber(int number)
        {
            Console.WriteLine(number);
        }
    }

    internal class ThreadSample
    {
        private readonly int _iterations;

        public ThreadSample(int iterations)
        {
            _iterations = iterations;
        }

        public void CountNumbers()
        {
            for (var i = 0; i < _iterations; i++)
            {
                Thread.Sleep(TimeSpan.FromSeconds(0.5));
                Console.WriteLine("{0} prints {1}", Thread.CurrentThread.Name, i);
            }
        }
    }
}