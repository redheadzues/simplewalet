using Assets.SimpleWallet.Scripts.SaveLoad.Savers;
using System;

namespace Assets.SimpleWallet.Scripts.Wallet
{
    public class WalletOperator : IWallet, IWalletSaveLoader
    {
        private readonly ISaveLoader _saveLoader;

        private WalletData _wallet;
        private BalanceNotifyer _balanceNotifyer;

        public IBalanceNotifyer Notifyer => _balanceNotifyer;

        public WalletOperator(ISaveLoader saveLoader)
        {
            _saveLoader = saveLoader;
            _balanceNotifyer = new BalanceNotifyer(this);
            _wallet = new WalletData();
            SyncronizeCurrencyAndWallet();
        }


        public void AddBalance(Currency currencyType, float amount)
        {
            if (amount <= 0)
                return;

            CurrencyKeeper keeper = GetKeeper(currencyType);

            if (keeper == null)
                return;

            keeper.AddBalance(amount);
            _balanceNotifyer.UpdateListeners(currencyType, keeper.Amount);
        }

        public void ClearBalance(Currency currencyType)
        {
            CurrencyKeeper keeper = GetKeeper(currencyType);

            if (keeper == null)
                return;

            keeper.ClearBalance();
            _balanceNotifyer.UpdateListeners(currencyType, keeper.Amount);
        }

        public void Save() =>
            _saveLoader.Save(_wallet);

        public void Load()
        {
            _wallet = _saveLoader.Load<WalletData>() ?? new WalletData();

            SyncronizeCurrencyAndWallet();
            _balanceNotifyer.UpdateAllListeners();
        }

        public float GetCurrencyAmount(Currency currencyType) =>
            GetKeeper(currencyType)?.Amount ?? 0;

        private CurrencyKeeper GetKeeper(Currency currencyType) =>
            _wallet.Keepers.TryGetValue(currencyType, out CurrencyKeeper keeper) ? keeper : null;

        private void SyncronizeCurrencyAndWallet()
        {
            foreach (Currency currency in Enum.GetValues(typeof(Currency)))
                _wallet.Keepers.TryAdd(currency, new CurrencyKeeper(currency));
        }
    }
}
