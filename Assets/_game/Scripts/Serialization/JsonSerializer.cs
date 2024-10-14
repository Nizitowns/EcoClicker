using Newtonsoft.Json;

namespace _game.Scripts.Serialization
{
    public class JsonSerializer : ISerializer
    {
        public string Serialize<T>(T data)
        {
            return JsonConvert.SerializeObject(data, Formatting.Indented);
        }

        public T Deserialize<T>(string data)
        {
            return JsonConvert.DeserializeObject<T>(data);
        }
    }
}