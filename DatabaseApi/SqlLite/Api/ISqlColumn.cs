namespace DatabaseApi.SqlLite.Api
{
    public interface ISqlColumn
    {
        string BuildCreateQuery();
        string Name { get; }
        bool IsPrimaryKey { get; }
        string EncapsulateValue(object value);
    }
}