using System;
using System.Collections.Concurrent;
using System.Threading;

namespace Threads
{

    public class Program
    {
        static public void Main()
        {
            new Program().Run();
        }

        BlockingCollection<string> q = new BlockingCollection<string>();

        void Run()
        {
            var threads = new[] { new Thread(Consumer), new Thread(Consumer) };
            foreach (var t in threads)
                t.Start();

            Console.WriteLine("Enter quantity of threads");
            string s;
            while ((s = Console.ReadLine()).Length != 0)
            {
                q.Add(s);
            }

            q.CompleteAdding(); // Stop

            foreach (var t in threads)
                t.Join();
        }

        void Consumer()
        {
            foreach (var s in q.GetConsumingEnumerable())
            {
                Console.WriteLine("Processing: {0}", s);
                Thread.Sleep(2000);
                Console.WriteLine("Processed: {0}", s);
            }
        }
    }
}
