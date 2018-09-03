using System;
using System.Runtime.InteropServices;

namespace IdentityServer.Utils
{
    /// <summary>
    /// Creates sequential guids
    /// </summary>
    public static class SequentialGuidUtils
    {
        /// <summary>
        /// Creates the sequential unique identifier.
        /// </summary>
        /// <returns>Created identifier</returns>
        /// <exception cref="Exception">Error generating sequential GUID</exception>
        public static Guid CreateGuid()
        {
            Guid guid;
            int result = NativeMethods.UuidCreateSequential(out guid);
            if (result != 0)
            {
                throw new Exception("Error generating sequential GUID");
            }
            return guid;
        }
    }

    /// <summary>
    /// Methods of dll
    /// </summary>
    internal static class NativeMethods
    {
        /// <summary>
        /// Creates the sequential UUIDs.
        /// </summary>
        /// <param name="guid">The unique identifier.</param>
        /// <returns></returns>
        [DllImport("rpcrt4.dll", SetLastError = true)]
        public static extern int UuidCreateSequential(out Guid guid);
    }
}
