using AndiSoft.Utilities.Converters;

namespace AndiSoft.Utilities.Extensions
{
    public static class JsonExtension
    {
        /// <summary>
        /// Converts object to a Json string.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="ignoreNullValues"></param>
        /// <returns></returns>
        public static string ToJson(this object obj, bool ignoreNullValues = false)
        {
            return JsonParser.ParseObject(obj, ignoreNullValues);
        }
    }
}