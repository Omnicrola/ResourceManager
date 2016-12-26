
using DataApi.Models;

namespace DataApi.Api
{
    public interface IDataRepositoryWrite : IDataRepository
    {
        void PutPerson(IPerson person);
    }
}