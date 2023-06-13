namespace Assets.SimpleWallet.Scripts.SaveLoad.Savers
{
    public interface ISaveLoader
    {
        void Save<TType>(TType data);
        TType Load<TType>();
    }
}
