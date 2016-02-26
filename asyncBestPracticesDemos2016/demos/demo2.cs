using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace c1.demos
{
    class demo2
    {
        public static async void execute()
        {
            Console.Clear();
            Console.WriteLine(new String('*', 32));
            Console.WriteLine("** DEMO 2 - THE DANGERS     **");
            Console.WriteLine("** OF TASK.RUN IN LIBRARIES **");
            Console.WriteLine(new String('*', 32));
            Console.WriteLine("EXECUTING DEMO...");
            await demo();
            Console.WriteLine("EXECUTE AGAIN? (S/N)");
        }

        static async Task demo()
        {
            var tasks = new List<Task>();

            for (int i = 0; i < 100; i++)
            {
                int fileNum = i;
                tasks.Add(Library.FetchFileAsync1(fileNum));
                //tasks.Add(Library.FetchFileAsync2(fileNum));
            }

            await Task.WhenAll(tasks);
        }
    }


    // Library code...
    class Library
    {
        public static async Task FetchFileAsync1(int fileNum)
        {
            await Task.Run(() =>
            {
                var contents = IO.DownloadFile();
                Console.WriteLine("Fetched file #{0}: {1}", fileNum, contents);
            });
        }

        public static async Task FetchFileAsync2(int fileNum)
        {
            var contents = await IO.DownloadFileAsync();
            Console.WriteLine("Fetched file #{0}: {1}", fileNum, contents);
        }
    }

    // Underlying low-level OS implementations...
    class IO
    {
        public static string DownloadFile()
        {
            System.Threading.Thread.Sleep(1000);
            return "file contents";
        }

        public static async Task<string> DownloadFileAsync()
        {
            await Task.Delay(1000);
            return "file contents";
        }        
    }


}
