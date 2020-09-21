using System.IO;

namespace AndiSoft.Utilities
{
    /// <summary>
    /// Other useful functions
    /// </summary>
    public static class AndiUtils
    {
        /// <summary>
        /// Reads a text file to a string
        /// </summary>
        /// <param name="filePath">File path</param>
        /// <returns></returns>
        public static string ReadTextFile(string filePath)
        {
            return File.ReadAllText(filePath);
        }
    }
}