using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskModule8
{
    class Program
    {
        public enum FunctionType { Rename, RenameAll, Delete }
        static void Main(string[] args)
        {
            bool isRecursive = false;

            if (args.Length < 2)
            {
                Console.WriteLine
                    ("Must be args: \n" +
                    "For Rename: Rename filePath newName\n" +
                    "For RenameAll (added number to template): RenameAll folderPath nameTemplate [recurs]\n" +
                    "For Delete: Delete path [recurs]\n");
                return;
            } 
            
            switch (args[0])
            {
                case "Rename":
                    FileUtils.Rename(args[1], args[2]);
                    break;
                case "RenameAll":
                    if (args.Count() > 3)
                    {
                        isRecursive = args[3] == "recurs";
                    }
                    FileUtils.RenameAll(args[1], args[2], isRecursive);
                    break;
                case "Delete":
                    if (args.Count() > 2)
                    {
                        isRecursive = args[2] == "recurs";
                    }
                    FileUtils.Delete(args[1], isRecursive);
                    break;
                default:
                    break;
            }
        }
    }
}
