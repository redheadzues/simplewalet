using Newtonsoft.Json;

namespace Assets.SimpleWallet.Scripts.SaveLoad.Serializers
{
    public class JsonSerializer : ISerializer<string>
    {
        public string Serialize<TData>(TData data) =>
            JsonConvert.SerializeObject(data);

        public TData Deserialize<TData>(string data) =>
            JsonConvert.DeserializeObject<TData>(data);
    }
}
