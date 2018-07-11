/*Write your own console application to perform a search in the selected directory.
  The console application must support parameters from the console (as it is called)
  The application writes any found files to the console and to a separate file on disk.
  When the application is started it must provide the user with interactive support regarding what, where, and how to save the results.
  Please make sure the app supports Cyrillic and spaces in params and in the search path.
  As a sub task to consider: Can you provide a way to stop a search
*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace FinalTask
{
    class Program
    {
        private static string _mask = string.Empty;
        private static string _pathFrom = string.Empty;
        private static bool _recursive = false;
        private static string _savepath = string.Empty;
       
        static void Main(string[] args)
        {
            CancellationTokenSource source = new CancellationTokenSource();
            try
            {
                if (args.Length > 0)
                {
                    ArgsToValues(args);
                }
                else
                {
                    PrintMenuAndGetValues();
                }
                Console.WriteLine("Press esc to stop");
                Task search = Search(source.Token);
                do
                {
                    while (!Console.KeyAvailable && !search.IsCompleted)
                    {
                        Thread.Sleep(100);
                    }
                    if (search.IsCompleted)
                    {
                        break;
                    }
                }
                while (Console.ReadKey().Key != ConsoleKey.Escape);
                if (!search.IsCompleted)
                {
                    source.Cancel();
                    Console.WriteLine("Search was stopped");
                }                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.ReadLine();
            }
        }

        private static void ArgsToValues(string[] args)
        {
            _mask = args[0];
            _pathFrom = args[1];
            _recursive = args.Length > 2 ? args[2] == "\r" : false;
            if (args.Length > 2)
            {
                _savepath = _recursive && args.Length == 4 ? args[3] : args[2];
            }
        }

        private static void PrintMenuAndGetValues()
        {
            Console.WriteLine("Please specify name or mask to search for:");
            _mask = Console.ReadLine();
            Console.WriteLine("Please specify path to start from:");
            _pathFrom = Console.ReadLine();
            Console.WriteLine("Should search be recursive? y - yes");
            _recursive = Console.ReadLine().ToLower() == "y";

            bool overwrite = false;
            do
            {
                Console.WriteLine("Please specify filepath where search results must be saved (if empty results would be printed to console):");
                _savepath = Console.ReadLine();
                if (File.Exists(_savepath))
                {
                    Console.WriteLine(_savepath + " has already exists. Do you want to overwrite it? y - yes");
                    overwrite = Console.ReadLine().ToLower() == "y";
                }
            }
            while (File.Exists(_savepath) && !overwrite);
        }

        private static async Task Search(CancellationToken token)
        {
            bool showResult = string.IsNullOrEmpty(_savepath);
            StreamWriter streamWriter = showResult ? new StreamWriter(Console.OpenStandardOutput()) : new StreamWriter(_savepath);
            await SearchUtils.SearchTask(token, streamWriter, _mask, _pathFrom, _recursive);
            if (!token.IsCancellationRequested)
            {
                Console.WriteLine("Search is done!");
            }
        }
    }
}
