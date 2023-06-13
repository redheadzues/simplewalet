using System.Collections.Generic;

namespace Assets.SimpleWallet.Scripts.Wallet
{
    public class BalanceNotifyer : IBalanceNotifyer
    {
        private readonly WalletOperator _walletOperator;

        private Dictionary<Currency, List<IWalletListener>> _listeners = new Dictionary<Currency, List<IWalletListener>>();

        public BalanceNotifyer(WalletOperator walletOperator)
        {
            _walletOperator = walletOperator;
        }

        public void AddListener(IWalletListener listener)
        {
            if (_listeners.TryGetValue(listener.Currency, out List<IWalletListener> listeners))
            {
                listeners.Add(listener);
            }
            else
            {
                listeners = new List<IWalletListener>();
                listeners.Add(listener);
                _listeners.Add(listener.Currency, listeners);
            }

            listener.UpdateValue(_walletOperator.GetCurrencyAmount(listener.Currency));
        }


        public void RemoveListener(IWalletListener listener)
        {
            if (_listeners.TryGetValue(listener.Currency, out List<IWalletListener> listeners))
                listeners.Remove(listener);
        }

        public void UpdateListeners(Currency currencyType, float amount)
        {
            if (_listeners.TryGetValue(currencyType, out List<IWalletListener> listeners))
                listeners.ForEach(listener => listener.UpdateValue(amount));
        }

        public void UpdateAllListeners()
        {
            foreach (Currency key in _listeners.Keys)
                UpdateListeners(key, _walletOperator.GetCurrencyAmount(key));
        }

    }

    public interface IBalanceNotifyer
    {
        void AddListener(IWalletListener listener);
        void RemoveListener(IWalletListener listener);
    }
}
