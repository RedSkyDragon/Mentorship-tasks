using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalTask
{
    /// <summary>
    /// Class of utils for search in directories
    /// </summary>
    public class SearchUtils
    {
        /// <summary>
        /// Searches for files that match the mask and saves them to file
        /// </summary>
        /// <param name="mask">Mask for files or filename</param>
        /// <param name="startPath">Start directory</param>
        /// <param name="savePath">Path to savefile</param>
        /// <param name="recursive">True if search should be recursive</param>
        public static void SearchAndSave(string mask, string startPath, string savePath, bool recursive = false)
        {
            using (StreamWriter saveFile = new StreamWriter(savePath))
            {
                SearchAndSaveRec(mask, startPath, saveFile, recursive);
            }
        }

        /// <summary>
        /// Searches for files that match the mask and returns IEnumerable collection of filenames
        /// </summary>
        /// <param name="mask">>Mask for files or filename</param>
        /// <param name="startPath">Start directory</param>
        /// <param name="recursive">True if search should be recursive</param>
        /// <returns></returns>
        public static IEnumerable<string> Search(string mask, string startPath, bool recursive = false)
        {
            var files = Directory.EnumerateFiles(startPath, mask);
            if (recursive)
            {
                var directories = Directory.GetDirectories(startPath);
                foreach (var dir in directories)
                {
                    files = files.Concat(Search(mask, dir, recursive));
                }
            }
            return files;
        }

        private static void SearchAndSaveRec(string mask, string startPath, StreamWriter save, bool recursive = false)
        {
            var filesToSave = Directory.EnumerateFiles(startPath, mask);
            foreach (var file in filesToSave)
            {
                save.WriteLine(file);
            }
            if (recursive)
            {
                var directories = Directory.GetDirectories(startPath);
                foreach (var dir in directories)
                {
                    SearchAndSaveRec(mask, dir, save, recursive);
                }
            }
        }
    }
}
