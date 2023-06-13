using Assets.SimpleWallet.Scripts.Wallet;
using TMPro;
using UnityEngine;

namespace Assets.SimpleWallet.Scripts.WalletViews
{
    public class CurrencyView : MonoBehaviour, IWalletListener
    {
        [SerializeField] private TMP_Text _currencyAmount;
        [SerializeField] private TMP_Text _currencyName;
        [SerializeField] private Currency _currencyType;

        public Currency Currency => _currencyType;

        private void Awake() =>
            SetName();

        public void Construct(Currency currencyType)
        {
            _currencyType = currencyType;
            SetName();
        }

        public void UpdateValue(float value) =>
            _currencyAmount.text = value.ToString();

        private void SetName() =>
            _currencyName.text = _currencyType.ToString();
    }
}
