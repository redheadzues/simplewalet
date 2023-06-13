using Assets.SimpleWallet.Scripts.Wallet;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.SimpleWallet.Scripts.WalletViews
{
    public class WalletView : MonoBehaviour
    {
        private List<IWalletListener> _currencyViews;
        private List<IWalletChanger> _changerViews;
        private IWallet _wallet;
        private IBalanceNotifyer _notifyer;

        private void OnDestroy()
        {
            UnRegisterViews();
            UnsubscribeOnChangers();
        }

        public void Construct(IWallet wallet, IBalanceNotifyer notifyer)
        {
            _wallet = wallet;
            _notifyer = notifyer;
            Setup();
        }

        private void Setup()
        {
            _currencyViews = GetComponentsInChildren<IWalletListener>().ToList();
            _changerViews = GetComponentsInChildren<IWalletChanger>().ToList();
            SubscribeOnChangers();
            RegisterListeners();
        }

        private void RegisterListeners() =>
            _currencyViews.ForEach(x => _notifyer?.AddListener(x));

        private void UnRegisterViews() =>
            _currencyViews.ForEach(x => _notifyer?.RemoveListener(x));

        private void OnBalanceCleared(Currency currencyType) =>
            _wallet?.ClearBalance(currencyType);

        private void OnBalanceChanged(Currency currencyType, float amount) =>
            _wallet?.AddBalance(currencyType, amount);

        private void SubscribeOnChangers()
        {
            foreach (IWalletChanger changer in _changerViews)
            {
                changer.BalanceChanged += OnBalanceChanged;
                changer.BalanceCleared += OnBalanceCleared;
            }
        }

        private void UnsubscribeOnChangers()
        {
            foreach (IWalletChanger changer in _changerViews)
            {
                changer.BalanceChanged -= OnBalanceChanged;
                changer.BalanceCleared -= OnBalanceCleared;
            }
        }
    }
}
