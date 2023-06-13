namespace Assets.SimpleWallet.Scripts.Wallet
{
    public interface IWalletListener
    {
        public Currency Currency { get; }
        void UpdateValue(float value);
    }
}
