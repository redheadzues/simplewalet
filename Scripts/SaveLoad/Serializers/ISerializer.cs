namespace Assets.SimpleWallet.Scripts.SaveLoad.Serializers
{
    public interface ISerializer<TSerializeType>
    {
        TSerializeType Serialize<TData>(TData data);
        TData Deserialize<TData>(TSerializeType data);
    }
}
