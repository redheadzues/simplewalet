using Assets.SimpleWallet.Scripts.SaveLoad.Serializers;
using System.IO;
using UnityEngine;

namespace Assets.SimpleWallet.Scripts.SaveLoad.Savers
{
    public class FileSaver<TSerializeType> : ISaveLoader
    {
        private readonly string _path;
        private readonly ISerializer<TSerializeType> _walletSerializer;

        public FileSaver(string path, ISerializer<TSerializeType> walletSerializer)
        {
            _path = path;
            _walletSerializer = walletSerializer;
        }

        public TType Load<TType>()
        {
            if (!File.Exists(_path))
            {
                Debug.LogWarning($"File {_path} does not exist. Returning default value.");
                return default;
            }

            if (typeof(TSerializeType) == typeof(string))
            {
                var serializedData = File.ReadAllText(_path);
                return _walletSerializer.Deserialize<TType>((TSerializeType)(object)serializedData);
            }
            else if (typeof(TSerializeType) == typeof(byte[]))
            {
                var dataBytes = File.ReadAllBytes(_path);
                return _walletSerializer.Deserialize<TType>((TSerializeType)(object)dataBytes);
            }

            return default;
        }

        public void Save<TType>(TType data)
        {
            if (typeof(TSerializeType) == typeof(string))
            {
                var dataString = _walletSerializer.Serialize(data) as string;
                File.WriteAllText(_path, dataString);
            }
            else if (typeof(TSerializeType) == typeof(byte[]))
            {
                var dataBytes = _walletSerializer.Serialize(data) as byte[];
                File.WriteAllBytes(_path, dataBytes);
            }
        }
    }
}
