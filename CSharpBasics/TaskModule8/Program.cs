/*Please create a console application to work with files.
  Add several options:
  Renaming a single file
  Renaming all files in a selected folder (recursion into sub folders must be an optional param)
  Deletion of files/folders by name (recursion into sub folders must be an optional param)
  The application should be compiled as an .EXE file, which the user calls with parameters. 
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskModule8
{
    class Program
    {
        static void Main(string[] args)
        {
            bool isRecursive = false;

            if (args.Length < 2)
            {
                Console.WriteLine
                    ("Must be args: \n" +
                    "For Rename: Rename filePath newName(with extention)\n" +
                    "For RenameAll (added number to template): RenameAll folderPath nameTemplate [recurs]\n" +
                    "For Delete: Delete path [recurs]\n");
                return;
            }

            try
            {
                switch (args[0])
                {
                    case "Rename":
                        FileUtils.Rename(args[1], args[2]);
                        Console.WriteLine("File was renamed");
                        break;
                    case "RenameAll":
                        if (args.Count() > 3)
                        {
                            if (args[3] != "recurs")
                            {
                                throw new ArgumentException("Invalid last argument. It must be \"recurs\".");
                            }
                            isRecursive = true;
                        }
                        FileUtils.RenameAll(args[1], args[2], isRecursive);
                        Console.WriteLine("All files were renamed");
                        break;
                    case "Delete":
                        if (args.Count() > 2)
                        {
                            if (args[2] != "recurs")
                            {
                                throw new ArgumentException("Invalid last argument. It must be \"recurs\".");
                            }
                            isRecursive = true;
                        }
                        FileUtils.Delete(args[1], isRecursive);
                        Console.WriteLine("Deletion complete");
                        break;
                    default:
                        break;
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
