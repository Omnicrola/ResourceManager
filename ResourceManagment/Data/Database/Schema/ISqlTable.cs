namespace ResourceManagment.Data.Database.Schema
{
    public interface ISqlTable
    {
        string BuildCreateQuery();

    }
}