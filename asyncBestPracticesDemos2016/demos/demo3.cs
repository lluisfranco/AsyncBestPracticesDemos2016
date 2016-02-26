using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace c1.demos
{
    class demo3
    {
        public static void execute()
        {
            Console.Clear();
            Console.WriteLine(new String('*', 32));
            Console.WriteLine("** DEMO 3 - TRACKING     **");
            Console.WriteLine("** THE GARBAGE COLLECTOR **");
            Console.WriteLine(new String('*', 32));
            Console.WriteLine("EXECUTING DEMO...");
            demo();
            Console.WriteLine("EXECUTE AGAIN? (S/N)");
        }

        static void demo()
        {
            TrackGcs(new MemStream1(data));
            TrackGcs(new MemStream2(data));
        }


        static byte[] data = new byte[0x10000000];

        static void TrackGcs(Stream input)
        {
            const int ITERS = 100;
            GC.Collect();
            int gen0 = GC.CollectionCount(0), gen1 = GC.CollectionCount(1), gen2 = GC.CollectionCount(2);
            for (int i = 0; i < ITERS; i++)
            {
                input.Position = 0;
                input.CopyToAsync(Stream.Null).Wait();
            }
            int newGen0 = GC.CollectionCount(0), newGen1 = GC.CollectionCount(1), newGen2 = GC.CollectionCount(2);
            Console.WriteLine("{0}\tGen0:{1}   Gen1:{2}   Gen2:{3}", input.GetType().Name, newGen0 - gen0, newGen1 - gen1, newGen2 - gen2);
        }
    }

    class MemStream1 : MemoryStream
    {
        public MemStream1(byte[] data) : base(data) { }

        public override async Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            return Read(buffer, offset, count);
        }
    }

    class MemStream2 : MemoryStream
    {
        public MemStream2(byte[] data) : base(data) { }

        private Task<int> m_cachedTask;

        public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            int numRead = Read(buffer, offset, count);
            if (m_cachedTask != null && m_cachedTask.Result == numRead)
            {
                return m_cachedTask;
            }

            m_cachedTask = Task.FromResult(numRead);
            return m_cachedTask;
        }
    }

}

