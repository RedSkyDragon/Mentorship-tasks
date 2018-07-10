/*Write your own console application to perform a search in the selected directory.
  The console application must support parameters from the console (as it is called)
  The application writes any found files to the console and to a separate file on disk.
  When the application is started it must provide the user with interactive support regarding what, where, and how to save the results.
  Please make sure the app supports Cyrillic and spaces in params and in the search path.
  As a sub task to consider: Can you provide a way to stop a search
*/
using System;
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
        private static bool _overwrite = false;

        static void Main(string[] args)
        {                      
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
                Task search;
                CancellationTokenSource source = new CancellationTokenSource();
                Console.WriteLine("Press esc to stop");
                if (string.IsNullOrEmpty(_savepath))
                {
                    search = SearchTask(source);
                }
                else
                {
                    search = SearchAndSaveTask(source);
                }
               Task.WhenAny(search, AcceptCancel(source)).Wait();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
        private static void ArgsToValues(string[] args)
        {
            _mask = args[0];
            _pathFrom = args[1];
            _recursive = args.Length > 2 ? args[2] == "\r" : false ;
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
            do
            {
                Console.WriteLine("Please specify filepath where search results must be saved (if empty results would be printed to console):");
                _savepath = Console.ReadLine();
                if (File.Exists(_savepath))
                {
                    Console.WriteLine(_savepath + " has already exists. Do you want to overwrite it? y - yes");
                    _overwrite = Console.ReadLine().ToLower() == "y";
                }
            }
            while (File.Exists(_savepath) && !_overwrite);
        }

        private static Task AcceptCancel(CancellationTokenSource source)
        {
            return Task.Run(() =>
            {
                ConsoleKeyInfo key;
                do
                {
                    source.Token.ThrowIfCancellationRequested();
                    key = Console.ReadKey(true);
                }
                while (key.Key != ConsoleKey.Escape);
                source.Cancel();
            });
        }

        private static Task SearchTask(CancellationTokenSource source)
        {
            return Task.Run(() =>
            {
                var files = SearchUtils.Search(source.Token, _mask, _pathFrom, _recursive);
                foreach (var file in files)
                {
                    Console.WriteLine(file);
                }
                Console.WriteLine("Search is done!");
                source.Cancel();
            });           
        }

        private static Task SearchAndSaveTask(CancellationTokenSource source)
        {
            return Task.Run(() =>
            {
                SearchUtils.SearchAndSave(source.Token, _mask, _pathFrom, _savepath, _recursive);
                Console.WriteLine("Search is done!");
                source.Cancel();
            });          
        }
    }
}
