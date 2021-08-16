using AndiSoft.Utilities.Converters;

namespace AndiSoft.Utilities.Extensions
{
    /// <summary>
    /// Json Extension methods
    /// </summary>
    public static class JsonExtension
    {
        /// <summary>
        /// Converts object to a Json string.
        /// </summary>
        /// <param name="obj">Object to be converted</param>
        /// <param name="identJson">Whether the json should be indented</param>
        /// <param name="ignoreNullValues">If true, null objects will not be present</param>
        /// <returns></returns>
        public static string ToJson(this object obj, bool identJson = false, bool ignoreNullValues = false)
        {
            return JsonParser.ParseObject(obj, identJson, ignoreNullValues);
        }
    }
}