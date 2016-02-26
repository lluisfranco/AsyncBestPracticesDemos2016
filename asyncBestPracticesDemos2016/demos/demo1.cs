using System;
using System.Runtime.CompilerServices;

namespace c1.demos
{
    class demo1
    {
        public static void execute()
        {
            Console.Clear();
            Console.WriteLine(new String('*', 32));
            Console.WriteLine("** DEMO 1 - SYNC Vs. ASYNC    **");
            Console.WriteLine("** METHOD INVOCATION OVERHEAD **");
            Console.WriteLine(new String('*', 32));
            Console.WriteLine("EXECUTING DEMO...");
            demo();
            Console.WriteLine("EXECUTE AGAIN? (S/N)");
        }

        static void demo()
        {
            var sw = new System.Diagnostics.Stopwatch();
            const int ITERS = 1000000;

            EmptyBody();
            EmptyBodyAsync();

            sw.Restart();
            for (int i = 0; i < ITERS; i++) EmptyBody();
            var syncTime = sw.Elapsed;

            sw.Restart();
            for (int i = 0; i < ITERS; i++) EmptyBodyAsync();
            var asyncTime = sw.Elapsed;

            Console.WriteLine(string.Format("\nSYNC  : {0}\nASYNC : {1}\n\nDiff  : {2:F1}x\n",
                syncTime, asyncTime, asyncTime.TotalSeconds / syncTime.TotalSeconds));

        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static void EmptyBody() { }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static async void EmptyBodyAsync() { }
    }
}
