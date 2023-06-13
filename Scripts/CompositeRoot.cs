using Assets.SimpleWallet.Scripts.SaveLoad.Savers;
using Assets.SimpleWallet.Scripts.SaveLoad.Serializers;
using Assets.SimpleWallet.Scripts.Wallet;
using Assets.SimpleWallet.Scripts.WalletViews;
using UnityEngine;

namespace Assets.SimpleWallet.Scripts
{
    public class CompositeRoot : MonoBehaviour
    {
        [Header("Views")]
        [SerializeField] private WalletView _walletView;
        [SerializeField] private WalletSaveLoadView _walletSaveLoadView;

        [Header("SaveLoad")]
        [SerializeField] private SaveLoadMethod _method;
        [SerializeField] private Serializer _serializer;


        [Header("Paths")]
        [SerializeField] private string _stringDataPath;
        [SerializeField] private string _binaryDataPath;
        [SerializeField] private string _playerPrefsKey;

        private WalletOperator _wallet;

        private enum SaveLoadMethod
        {
            PlayerPrefs,
            File
        }

        private enum Serializer
        {
            Json,
            Binary
        }

        private void OnValidate()
        {
            if (_method == SaveLoadMethod.PlayerPrefs)
                _serializer = Serializer.Json;
        }

        private void Awake()
        {
            CreateWallet();
            InitViews();
        }

        private void CreateWallet()
        {
            ISaveLoader saveLoader = CreateSaveLoader();
            _wallet = new WalletOperator(saveLoader);
        }

        private void InitViews()
        {
            _walletView.Construct(_wallet, _wallet.Notifyer);
            _walletSaveLoadView.Construct(_wallet);
        }

        private ISaveLoader CreateSaveLoader()
        {
            switch (_method)
            {
                case SaveLoadMethod.PlayerPrefs:
                    return new PlayerPrefsSaver(_playerPrefsKey, new JsonSerializer());
                case SaveLoadMethod.File:
                    switch (_serializer)
                    {
                        case Serializer.Json:
                            return new FileSaver<string>(_stringDataPath, new JsonSerializer());
                        case Serializer.Binary:
                            return new FileSaver<byte[]>(_binaryDataPath, new BinarySerializer());
                    }
                    break;
            }

            return new PlayerPrefsSaver(_playerPrefsKey, new JsonSerializer());
        }
    }
}