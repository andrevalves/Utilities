using Newtonsoft.Json;

namespace AndiSoft.Utilities.Converters
{
    /// <summary>
    /// Converts object types.
    /// </summary>
    /// <typeparam name="T">Destination type for the conversion.</typeparam>
    internal class GenericsConverter<T>
    {
        /// <summary>
        /// Converts the object to the given type.
        /// </summary>
        /// <param name="obj">Object to be converted.</param>
        /// <returns>New object of the given type.</returns>
        public static T Convert(object obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            return JsonConvert.DeserializeObject<T>(json);
        }

        /// <summary>
        /// Try to parse object to the given type.
        /// </summary>
        /// <param name="obj">Object to be parsed.</param>
        /// <param name="newObj">New parsed object.</param>
        /// <returns>True if sucessful. False otherwise.</returns>
        public static bool TryParse(object obj, out T newObj)
        {
            try
            {
                var json = JsonConvert.SerializeObject(obj);
                newObj = JsonConvert.DeserializeObject<T>(json);
            }
            catch
            {
                newObj = JsonConvert.DeserializeObject<T>("{}");
                return false;
            }

            return true;
        }
    }
}