namespace Assets.SimpleWallet.Scripts.Wallet
{
    public interface IWallet
    {
        void AddBalance(Currency currencyType, float amount);
        void ClearBalance(Currency currencyType);
    }
}
