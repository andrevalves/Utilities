using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace AndiSoft.Utilities.Converters
{
    /// <summary>
    /// Converts objects to Json
    /// </summary>
    public static class JsonParser
    {
        /// <summary>
        /// Converts objects to Json string.
        /// </summary>
        /// <param name="obj">Object to be serialized.</param>
        /// <param name="ignoreNullValues">If true, null values will not be included in the Json string. Default is true.</param>
        /// <returns></returns>
        public static string ParseObject(object obj, bool ignoreNullValues = true)
        {
            var jsonSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                NullValueHandling = ignoreNullValues ? NullValueHandling.Ignore : NullValueHandling.Include
            };

            return JsonConvert.SerializeObject(obj, jsonSettings);
        }

        /// <summary>
        /// Try to parse object to the given type.
        /// </summary>
        /// <param name="obj">Object to be parsed.</param>
        /// <param name="jsonString">New parsed object.</param>
        /// <param name="ignoreNullValues">If true, null values will not be included in the Json string. Default is true.</param>
        /// <returns>True if sucessful. False otherwise.</returns>
        public static bool ParseObject(object obj, out string jsonString, bool ignoreNullValues = true)
        {
            var jsonSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                NullValueHandling = ignoreNullValues ? NullValueHandling.Ignore : NullValueHandling.Include
            };

            try
            {
                jsonString = JsonConvert.SerializeObject(obj, jsonSettings);
                return true;
            }
            catch
            {
                jsonString = null;
                return false;
            }
        }

        /// <summary>
        /// Converts Json string to the specified object
        /// </summary>
        /// <param name="jsonString"></param>
        /// <returns>Parsed object</returns>
        public static T ParseJson<T>(string jsonString)
        {
            return JsonConvert.DeserializeObject<T>(jsonString);
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
                obj = JsonConvert.DeserializeObject<T>(jsonString);
                return true;
            }
            catch
            {
                obj = JsonConvert.DeserializeObject<T>("{ }");
                return false;
            }
        }
    }
}