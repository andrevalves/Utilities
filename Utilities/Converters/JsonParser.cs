using System.IO;
using System.Text.Json;

namespace AndiSoft.Utilities.Converters
{
    /// <summary>
    /// Converts objects to Json
    /// </summary>
    public static class JsonParser
    {
        /// <summary>
        /// Converts object to a Json string.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="identJson">Ident json result.</param>
        /// <param name="ignoreNullValues"></param>
        /// <returns></returns>
        internal static string ToJson(object obj, bool identJson = false, bool ignoreNullValues = false)
        {
            return ParseObject(obj, identJson, ignoreNullValues);
        }

        /// <summary>
        /// Converts Json string to a minified version
        /// </summary>
        /// <param name="json">Json string to be minified</param>
        /// <returns></returns>
        public static string MinifyJson(string json)
        {
            var obj = ParseJson<object>(json);
            return ParseObject(obj);
        }

        /// <summary>
        /// Converts Json string to a pretty version
        /// </summary>
        /// <param name="json">Json string to be beautified</param>
        /// <returns></returns>
        public static string BeautifyJson(string json)
        {
            var obj = ParseJson<object>(json);
            return ParseObject(obj, true);
        }

        /// <summary>
        /// Read object from a json file
        /// </summary>
        /// <param name="filePath">File path</param>
        /// <typeparam name="T">Target object</typeparam>
        /// <returns>Result object</returns>
        public static T JsonFromFile<T>(string filePath)
        {
            var json = File.ReadAllText(filePath);
            return ParseJson<T>(json);
        }

        /// <summary>
        /// Converts objects to Json string.
        /// </summary>
        /// <param name="obj">Object to be serialized.</param>
        /// <param name="identJson">Ident json result.</param>
        /// <param name="ignoreNullValues">If true, null values will not be included in the Json string. Default is false.</param>
        /// <returns></returns>
        public static string ParseObject(object obj, bool identJson = false, bool ignoreNullValues = false)
        {
            var jsonSettings = new JsonSerializerOptions()
            {
                WriteIndented = identJson,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                IgnoreNullValues = ignoreNullValues
            };

            return JsonSerializer.Serialize(obj, jsonSettings);
        }

        /// <summary>
        /// Converts Json string to the specified object
        /// </summary>
        /// <param name="jsonString">Json string</param>
        /// <returns>Parsed object</returns>
        public static T ParseJson<T>(string jsonString)
        {
            var jsonSettings = new JsonSerializerOptions()
            {
                AllowTrailingCommas = true
            };

            return JsonSerializer.Deserialize<T>(jsonString, jsonSettings);
        }

        #region TryParse

        /// <summary>
        /// Try to parse object to the given type.
        /// </summary>
        /// <param name="obj">Object to be parsed.</param>
        /// <param name="jsonString">New parsed object.</param>
        /// <param name="ignoreNullValues">If true, null values will not be included in the Json string. Default is false.</param>
        /// <returns>True if successful. False otherwise.</returns>
        public static bool TryParse(object obj, out string jsonString, bool ignoreNullValues = false)
        {
            try
            {
                jsonString = ParseObject(obj, false, ignoreNullValues);
                return true;
            }
            catch
            {
                jsonString = null;
                return false;
            }
        }

        /// <summary>
        /// Try to parse object to the given type.
        /// </summary>
        /// <param name="jsonString">Object to be parsed.</param>
        /// <param name="obj">New parsed object.</param>
        /// <returns>True if sucessful. False otherwise.</returns>
        public static bool TryParse<T>(string jsonString, out T obj)
        {
            try
            {
                obj = ParseJson<T>(jsonString);
                return true;
            }
            catch
            {
                obj = ParseJson<T>("{ }");
                return false;
            }
        }

        #endregion

        
    }
}