using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalTask
{
    class Program
    {
        static void Main(string[] args)
        {
            string mask = string.Empty;
            string pathFrom = string.Empty;
            bool recursive = false;
            string savepath = string.Empty;
            //bool stopped = false;
            bool overwrite = false;
            try
            {
                Console.WriteLine("Please specify name or mask to search for:");
                mask = Console.ReadLine();
                Console.WriteLine("Please specify path to start from:");
                pathFrom = Console.ReadLine();
                Console.WriteLine("Should search be recursive? y - yes");
                recursive = Console.ReadLine().ToLower() == "y";
                do
                {
                    Console.WriteLine("Please specify filepath where search results must be saved (if empty results would be printed to console):");
                    savepath = Console.ReadLine();
                    if (File.Exists(savepath))
                    {
                        Console.WriteLine(savepath + " has already exists. Do you want to overwrite it? y - yes");
                        overwrite = Console.ReadLine().ToLower() == "y";
                    }
                }
                while (File.Exists(savepath) && !overwrite);

                Console.WriteLine("Search in progress!");
                if (savepath == string.Empty)
                {
                    var files = SearchUtils.Search(mask, pathFrom, recursive);
                    foreach (var file in files)
                    {
                        Console.WriteLine(file);
                    }
                }
                else
                {
                    SearchUtils.SearchAndSave(mask, pathFrom, savepath, recursive);
                }
                Console.WriteLine("Search is done!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
    }
}
