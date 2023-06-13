using System;
using System.Collections.Generic;

namespace Assets.SimpleWallet.Scripts.Wallet
{
    [Serializable]
    public class WalletData
    {
        public Dictionary<Currency, CurrencyKeeper> Keepers = new Dictionary<Currency, CurrencyKeeper>();
    }
}
