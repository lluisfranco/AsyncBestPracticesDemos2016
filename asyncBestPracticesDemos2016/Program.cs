using System;

namespace c1
{
    class Program
    {
        static void Main(string[] args)
        {
            var command = string.Empty;
            while (command.Trim().ToUpper() != "Q")
            {
                showDemosMainMenu();
                command = Console.ReadLine();
                if (command.Trim().ToUpper() == "1")
                    command = executeAndWaitForDemo(command, demos.demo1.execute);
                if (command.Trim().ToUpper() == "2")
                    command = executeAndWaitForDemo(command, demos.demo2.execute);
                if (command.Trim().ToUpper() == "3")
                    command = executeAndWaitForDemo(command, demos.demo3.execute);
                if (command.Trim().ToUpper() == "4")
                    command = executeAndWaitForDemo(command, demos.demo4.execute);
            }
        }

        private static string executeAndWaitForDemo(string command, Action demoToExecute)
        {
            while (command.Trim().ToUpper() != "N")
            {
                demoToExecute();
                command = Console.ReadLine();
            }
            return command;
        }

        private static void showDemosMainMenu()
        {
            Console.Clear();
            Console.WriteLine("**********************************************************");
            Console.WriteLine("**                                                      **");
            Console.WriteLine("**             ASYNC BEST PRACTICES DEMOS               **");
            Console.WriteLine("**                                                      **");
            Console.WriteLine("**********************************************************");
            Console.WriteLine("");
            Console.WriteLine("**********************************************************");
            Console.WriteLine("**                                                      **");
            Console.WriteLine("** DEMO 1 - SYNC Vs. ASYNC / METHOD INVOCATION OVERHEAD **");
            Console.WriteLine("** DEMO 2 - THE DANGERS OF TASK.RUN IN LIBRARIES        **");
            Console.WriteLine("** DEMO 3 - TRACKING THE GARBAGE COLLECTOR              **");
            Console.WriteLine("** DEMO 4 - CONSIDER .CONFIGUREAWAIT IN LIBRARIES       **");
            Console.WriteLine("**                                                      **");
            Console.WriteLine("**********************************************************");
            Console.WriteLine("");
            Console.WriteLine("CHOOSE DEMO NUMBER OR PRESS 'Q' TO QUIT");
        }

    }
}
