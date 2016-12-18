namespace DataApi.Extensions
{
    public interface IConversionFactory<in T, out K>
    {
        K Build(T source);
    }
}