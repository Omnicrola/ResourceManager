using DataApi.Extensions;
using DataApi.Models;
using ResourceManagment.Data.Model;
using ResourceManagment.Windows.ManagePeople;

namespace ResourceManagment.Windows.ViewModels
{
    internal class PeopleViewModelFactory : IConversionFactory<IPerson, PersonViewModel>
    {
        public PersonViewModel Build(IPerson source)
        {
            return new PersonViewModel(source.FirstName, source.LastName) { ID = source.ID };
        }
    }
}