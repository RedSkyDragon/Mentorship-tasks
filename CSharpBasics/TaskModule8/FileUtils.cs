using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskModule8
{   
    /// <summary>
    /// Class for working with files
    /// </summary>
    public class FileUtils
    {
        /// <summary>
        /// Renames file
        /// </summary>
        /// <param name="filePath">Path to file</param>
        /// <param name="newName">New name of file</param>
        public static void Rename(string filePath, string newName)
        {
            if (File.Exists(filePath))
            {
                string newPath = Path.GetDirectoryName(filePath) + Path.DirectorySeparatorChar + newName;
                if (File.Exists(newPath))
                {
                    string answer;
                    do
                    {
                        Console.WriteLine("File with name " + newName + " has already exists.Do you want to replace it ? y - yes, n - no");
                        answer = Console.ReadLine();
                    }
                    while (answer != "y" && answer != "n");
                    if (answer == "n")
                    {
                        return;
                    }
                }
                File.Copy(filePath, newPath, true);
                File.Delete(filePath);
            }
            else
            {
                Console.WriteLine("File does not exist");
            }
        }
        /// <summary>
        /// Renames all files in directory using a template
        /// </summary>
        /// <param name="folderPath">Path to directory</param>
        /// <param name="nameTemplate">Name Template</param>
        /// <param name="recursive">True if files in subdirectories also should be renamed</param>
        public static void RenameAll(string folderPath, string nameTemplate, bool recursive = false)
        {
            try
            {
                string[] files = Directory.GetFiles(folderPath);
                int counter = 1;
                foreach (var file in files)
                {
                    var newName = nameTemplate + counter++ + Path.GetExtension(file);
                    Rename(file, newName);
                }
                if (recursive)
                {
                    string[] directories = Directory.GetDirectories(folderPath);
                    foreach (var dir in directories)
                    {
                        RenameAll(dir, nameTemplate, recursive);
                    }
                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        /// <summary>
        /// Delets files from directory or deletes file
        /// </summary>
        /// <param name="path">Path to the file or directory</param>
        /// <param name="recursive">True if files in subdirectories also should be deleted</param>
        public static void Delete(string path, bool recursive = false)
        {
            try
            {
                if (File.GetAttributes(path) == FileAttributes.Directory)
                {
                    string[] files = Directory.GetFiles(path);
                    foreach (var file in files)
                    {
                        File.Delete(file);
                    }
                    if (recursive)
                    {
                        string[] directories = Directory.GetDirectories(path);
                        foreach (var dir in directories)
                        {
                            Delete(dir, recursive);
                            Directory.Delete(dir);
                        }
                    }
                }
                else
                {
                    if (File.Exists(path))
                    {
                        File.Delete(path);
                    }
                    else
                    {
                        Console.WriteLine("File does not exist");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
