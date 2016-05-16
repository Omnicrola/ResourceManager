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
            var weeklySchedules = e.NewItems.Cast<IWeeklySchedule>().ToList();
            _databaseSchema.WeeklyScheduleTable.Create(weeklySchedules);
            weeklySchedules.ForEach(s => s.PropertyChanged += _databaseSchema.WeeklyScheduleTable.DataChanged);
        }

        public void PersistProject(object sender, NotifyCollectionChangedEventArgs e)
        {
            var projects = e.NewItems.Cast<IProject>().ToList();
            _databaseSchema.ProjectTable.Create(projects);
            projects.ForEach(p => p.PropertyChanged += _databaseSchema.ProjectTable.DataChanged);
        }

        public void PersistPerson(object sender, NotifyCollectionChangedEventArgs e)
        {
            var people = e.NewItems.Cast<IPerson>().ToList();
            _databaseSchema.PersonTable.Create(people);
            people.ForEach(p => p.PropertyChanged += _databaseSchema.PersonTable.DataChanged);
        }
    }
}