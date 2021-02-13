using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace StoreApp.Library
{
    public class Serializer
    {
        public void Serialize<T>(T data, string fileName)
        {
            string json = JsonConvert.SerializeObject(data);
            File.WriteAllTextAsync(fileName, json, Encoding.UTF8);
        }

        public async Task<T> DeserializeAsync<T>(string fileName) where T : new()
        {
            if (!File.Exists(fileName))
                return default(T);

            string json = await File.ReadAllTextAsync(fileName);

            T obj =  JsonConvert.DeserializeObject<T>(json);

            if(obj == null)
            {
                throw new System.NullReferenceException("Could not parse JSON data.");
            }

            return obj;
        }
    }
}
