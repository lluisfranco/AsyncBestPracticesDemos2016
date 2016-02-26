using System;
using System.Threading.Tasks;

namespace c1.demos
{
    class demo4
    {
        public static async void execute()
        {
            Console.Clear();
            Console.WriteLine(new String('*', 34));
            Console.WriteLine("** DEMO 4 - CONSIDER            **");
            Console.WriteLine("** .CONFIGUREAWAIT IN LIBRARIES **");
            Console.WriteLine(new String('*', 34));
            Console.WriteLine("EXECUTING DEMO...");
            demo();
            Console.WriteLine("EXECUTE AGAIN? (S/N)");
        }

        static void demo()
        {
            var f = new System.Windows.Forms.Form() { Size = new System.Drawing.Size(300, 100), Text = "Calculating" };
            f.Load += async delegate
              {
                  var syncContext = System.Threading.SynchronizationContext.Current;
                  await Task.WhenAll(WithSyncCtx(), WithoutSyncCtx()); // warm-up, to ensure they're JIT'd

                  var sw = new System.Diagnostics.Stopwatch();

                  sw.Restart();
                  await WithSyncCtx();
                  var withTime = sw.Elapsed;

                  sw.Restart();
                  await WithoutSyncCtx();
                  var withoutTime = sw.Elapsed;
                  f.Close();
                  Console.WriteLine(string.Format("With    : {0}\nWithout : {1}\n\nDiff    : {2:F2}x",
                    withTime, withoutTime, withTime.TotalSeconds / withoutTime.TotalSeconds));
              };
            f.ShowDialog();
        }

        const int ITERS = 20000;

        private static async Task WithSyncCtx()
        {
            for (int i = 0; i < ITERS; i++)
            {
                var t = Task.Run(() => { });
                await t;
            }
        }

        private static async Task WithoutSyncCtx()
        {
            for (int i = 0; i < ITERS; i++)
            {
                var t = Task.Run(() => { });
                await t.ConfigureAwait(false);
            }
        }
    }

}

