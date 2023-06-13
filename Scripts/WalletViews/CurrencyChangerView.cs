using Assets.SimpleWallet.Scripts.Wallet;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.SimpleWallet.Scripts.WalletViews
{
    public class CurrencyChangerView : MonoBehaviour, IWalletChanger
    {
        [SerializeField] private Button _addButton;
        [SerializeField] private Button _clearButton;
        [SerializeField] private Currency _currencyType;
        [SerializeField] private float _changeValue;

        public Currency Currency => _currencyType;

        public event Action<Currency> BalanceCleared;
        public event Action<Currency, float> BalanceChanged;

        private void OnEnable()
        {
            _addButton.onClick.AddListener(OnAddButtonClicked);
            _clearButton.onClick.AddListener(OnClearButtonClicked);
        }

        private void OnDisable()
        {
            _addButton.onClick.AddListener(OnAddButtonClicked);
            _clearButton.onClick.AddListener(OnClearButtonClicked);
        }

        private void OnClearButtonClicked() =>
            BalanceCleared?.Invoke(_currencyType);

        private void OnAddButtonClicked() =>
            BalanceChanged?.Invoke(_currencyType, _changeValue);
    }
}
