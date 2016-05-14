namespace ResourceManagment.Operations
{
    public interface IDiscreetOperation
    {
        void DoWork();

        string Description { get; }
    }
}