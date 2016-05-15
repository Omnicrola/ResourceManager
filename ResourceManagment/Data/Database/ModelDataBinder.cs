using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using ResourceManagment.Data.Model;

namespace ResourceManagment.Data.Database
{
    public class ModelDataBinder
    {
        private readonly ResourceManagerDatabaseSchema _databaseSchema;

        public ModelDataBinder(ResourceManagerDatabaseSchema databaseSchema)
        {
            _databaseSchema = databaseSchema;
        }

        public void PersistSchedules(object sender, NotifyCollectionChangedEventArgs e)
        {
            _databaseSchema.WeeklyScheduleTable.Create(e.NewItems.Cast<IWeeklySchedule>().ToList());
        }

        public void PersistProject(object sender, NotifyCollectionChangedEventArgs e)
        {
            var projects = e.NewItems.Cast<IProject>().ToList();
            _databaseSchema.ProjectTable.Create(projects);
            projects.ForEach(p => p.PropertyChanged += _databaseSchema.ProjectTable.DataChanged);
        }

        private void BindProject(IProject project)
        {
            project.PropertyChanged += UpdateProject;
        }

        public void PersistPerson(object sender, NotifyCollectionChangedEventArgs e)
        {
            _databaseSchema.PersonTable.Create(e.NewItems.Cast<IPerson>().ToList());
        }


    }
}