using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
        public static void SearchAndSave(CancellationToken token, string mask, string startPath, string savePath, bool recursive = false)
        {
            using (var save = new StreamWriter(savePath, append: true))
            {
                foreach (var file in Search(token, mask, startPath, recursive))
                {
                    save.WriteLine(file);
                }
            }
        }

        /// <summary>
        /// Searches for files that match the mask and returns IEnumerable collection of filenames
        /// </summary>
        /// <param name="mask">>Mask for files or filename</param>
        /// <param name="startPath">Start directory</param>
        /// <param name="recursive">True if search should be recursive</param>
        /// <returns></returns>
        public static IEnumerable<string> Search(CancellationToken token, string mask, string startPath, bool recursive = false)
        {
            var files = Directory.EnumerateFiles(startPath, mask);
            if (recursive)
            {
                var directories = Directory.GetDirectories(startPath);
                foreach (var dir in directories)
                {
                    files = files.Concat(Search(token, mask, dir, recursive));
                    token.ThrowIfCancellationRequested();
                }
            }
            return files;
        }

        //private static void SearchAndSaveRec(string mask, string startPath, string savePath, bool recursive = false)
        //{
        //    var filesToSave = Directory.EnumerateFiles(startPath, mask);
        //    foreach (var file in filesToSave)
        //    {
        //        using (var save = new StreamWriter(savePath, append: true))
        //        {
        //            save.WriteLine(file);
        //        }
        //    }
        //    if (recursive)
        //    {
        //        var directories = Directory.GetDirectories(startPath);
        //        foreach (var dir in directories)
        //        {
        //            SearchAndSaveRec(mask, dir, savePath, recursive);
        //        }
        //    }
        //}
    }
}
