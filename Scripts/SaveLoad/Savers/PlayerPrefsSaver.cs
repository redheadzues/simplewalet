using Assets.SimpleWallet.Scripts.SaveLoad.Serializers;
using UnityEngine;

namespace Assets.SimpleWallet.Scripts.SaveLoad.Savers
{
    public class PlayerPrefsSaver : ISaveLoader
    {
        private readonly string _saveKey;
        private readonly ISerializer<string> _walletSerializer;

        public PlayerPrefsSaver(string saveKey, ISerializer<string> walletSerializer)
        {
            _saveKey = saveKey;
            _walletSerializer = walletSerializer;
        }

        public void Save<T>(T data)
        {
            string dataString = _walletSerializer.Serialize(data);

            PlayerPrefs.SetString(_saveKey, dataString);
            PlayerPrefs.Save();
        }

        public TType Load<TType>()
        {
            return _walletSerializer.Deserialize<TType>(PlayerPrefs.GetString(_saveKey));
        }
    }
}
