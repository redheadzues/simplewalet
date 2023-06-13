using System;

namespace Assets.SimpleWallet.Scripts.Wallet
{
    public interface IWalletChanger
    {
        public Currency Currency { get; }
        event Action<Currency> BalanceCleared;
        event Action<Currency, float> BalanceChanged;
    }
}
