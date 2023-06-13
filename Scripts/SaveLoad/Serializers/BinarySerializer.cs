using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Assets.SimpleWallet.Scripts.SaveLoad.Serializers
{
    public class BinarySerializer : ISerializer<byte[]>
    {
        public TData Deserialize<TData>(byte[] data)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (MemoryStream stream = new MemoryStream(data))
            {
                return (TData)formatter.Deserialize(stream);
            }
        }

        public byte[] Serialize<TData>(TData data)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (MemoryStream stream = new MemoryStream())
            {
                formatter.Serialize(stream, data);
                return stream.ToArray();
            }
        }
    }
}
