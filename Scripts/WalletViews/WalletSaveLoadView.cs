using Assets.SimpleWallet.Scripts.Wallet;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.SimpleWallet.Scripts.WalletViews
{
    public class WalletSaveLoadView : MonoBehaviour
    {
        [SerializeField] private Button _saveButton;
        [SerializeField] private Button _loadButton;

        private IWalletSaveLoader _saveLoader;

        private void OnEnable()
        {
            _saveButton.onClick.AddListener(OnSaveButtonClick);
            _loadButton.onClick.AddListener(OnLoadButtonClick);
        }

        private void OnDisable()
        {
            _saveButton.onClick.RemoveListener(OnSaveButtonClick);
            _loadButton.onClick.RemoveListener(OnLoadButtonClick);
        }

        public void Construct(IWalletSaveLoader saveLoader) =>
            _saveLoader = saveLoader;

        private void OnSaveButtonClick() =>
            _saveLoader?.Save();

        private void OnLoadButtonClick() =>
            _saveLoader?.Load();
    }
}
