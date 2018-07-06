using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskModule8
{
    public enum FunctionType { Rename, RenameAll, Delete, DeleteAll }
    public class FileUtils
    {
        public static void Rename(string filePath, string newName) { }

        public static void RenameAll(string folderPath, string nameTemplate, bool recursive = false) { }

        public static void Delete(string path, bool recursive = false) { }
    }
}
