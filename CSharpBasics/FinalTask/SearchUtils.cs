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
        public static void Search(CancellationToken token, StreamWriter saveStream, string mask, string startPath, bool recursive = false)
        {
            using (saveStream)
            {
                foreach (var file in Search(token, mask, startPath, recursive))
                {
                    saveStream.WriteLine(file);
                }
            }
        }

        /// <summary>
        /// Searches for files that match the mask and returns IEnumerable collection of filenames
        /// </summary>
        /// <param name="mask">Mask for files or filename</param>
        /// <param name="startPath">Start directory</param>
        /// <param name="recursive">True if search should be recursive</param>
        /// <returns>IEnumerable collection of found filenames</returns>
        private static IEnumerable<string> Search(CancellationToken token, string mask, string startPath, bool recursive = false)
        {
            var files = Directory.EnumerateFiles(startPath, mask);
            if (recursive)
            {
                var directories = Directory.GetDirectories(startPath);
                foreach (var dir in directories)
                {
                    files = files.Concat(Search(token, mask, dir, recursive));
                    if (token.IsCancellationRequested)
                    {
                        break;
                    }
                }
            }
            return files;
        }

        /// <summary>
        /// Returns Task which searches for files that match the mask and saves them to file
        /// </summary>
        /// <param name="token">Token that stops the search</param>
        /// <param name="mask">Mask for files or filename</param>
        /// <param name="startPath">Start directory</param>
        /// <param name="savePath">Path to savefile</param>
        /// <param name="recursive">True if search should be recursive</param>
        /// <returns>Task for search</returns>
        public static async Task SearchTask(CancellationToken token, StreamWriter saveStream, string mask, string startPath, bool recursive = false)
        {
            await Task.Run(() =>
            {
                SearchUtils.Search(token, saveStream, mask, startPath, recursive);
            });
        }
    }
}
