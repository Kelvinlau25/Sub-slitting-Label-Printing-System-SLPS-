using System.Collections.Generic;
using System.Text.Json;

namespace Library.Root.Control
{
    public class Convertion<T>
    {
        /// <summary>
        /// Convert List of T into String Format
        /// </summary>
        public static string Serializer(List<T> list)
        {
            string result = JsonSerializer.Serialize(list);
            return result;
        }

        /// <summary>
        /// Convert string into List of T
        /// </summary>
        public static List<T> Deserializer(string StringFormat)
        {
            List<T> result = JsonSerializer.Deserialize<List<T>>(StringFormat);
            return result;
        }
    }
}
