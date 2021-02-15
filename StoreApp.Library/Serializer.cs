using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;

namespace StoreApp.Library
{
    public class Serializer
    {
        /// <summary>
        /// Serializes the object to the specified path as a JSON file.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public async Task SerializeAsync<T>(T data, string fileName)
        {
            //using Stream stream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write);
            JsonSerializerOptions options = new JsonSerializerOptions();
            //options.IncludeFields = true;
            string json = JsonSerializer.Serialize(data, options);
            await File.WriteAllTextAsync(fileName, json);
            
        }

        /// <summary>
        /// Deserializes the object at the specified path using JSON.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <returns>Returns the deserialized object or creates a new object if the file does not exist. </returns>
        public async Task<T> DeserializeAsync<T>(string fileName) where T : new()
        {
            if (!File.Exists(fileName))
                return new();

            using Stream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);

            JsonSerializerOptions options = new JsonSerializerOptions();
            //options.IncludeFields = true;

            string json = await File.ReadAllTextAsync(fileName);
            T obj = JsonSerializer.Deserialize<T>(json, options);

            if(obj == null)
            {
                throw new NullReferenceException("Could not parse JSON data.");
            }

            return obj;
        }
    }
}
