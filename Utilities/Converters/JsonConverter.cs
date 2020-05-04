using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace AndiSoft.Utilities.Converters
{
    public static class JsonConverter
    {
        /// <summary>
        /// Converts objects to Json string.
        /// </summary>
        /// <param name="objeto">Object to be serialized.</param>
        /// <param name="ignoreNullValues">If true, null values will not be included in the Json string. Default is true.</param>
        /// <returns></returns>
        public static string ToJson(object objeto, bool ignoreNullValues = true)
        {
            var jsonSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                NullValueHandling = ignoreNullValues ? NullValueHandling.Ignore : NullValueHandling.Include
            };

            return JsonConvert.SerializeObject(objeto, jsonSettings);
        }
    }

    public static class JsonConverter<T>
    {
        /// <summary>
        /// Converts Json string to the specified object
        /// </summary>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        public static T ToObject(string jsonString)
        {
            return JsonConvert.DeserializeObject<T>(jsonString);
        }

        /// <summary>
        /// Uses Json Serializer to convert objects
        /// </summary>
        /// <param name="objeto">Object to be converted.</param>
        /// <returns>New converted object.</returns>
        public static T ConvertObject(object objeto)
        {
            var json = JsonConvert.SerializeObject(objeto);
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}