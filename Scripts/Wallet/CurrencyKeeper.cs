using System;

namespace Assets.SimpleWallet.Scripts.Wallet
{
    [Serializable]
    public class CurrencyKeeper
    {
        private readonly Currency _currency;
        private float _amount;

        public Currency Currency => _currency;
        public float Amount => _amount;

        public CurrencyKeeper(Currency currency, float amount = 0)
        {
            _currency = currency;
            _amount = amount;
        }

        public void AddBalance(float amount) =>
            _amount += amount;

        public void ClearBalance() =>
            _amount = 0;
    }
}
