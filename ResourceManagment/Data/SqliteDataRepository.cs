using System;
using System.Collections.ObjectModel;
using System.Configuration;
using DataApi.Api;
using DataApi.Models;
using DatabaseApi.SqlLite;
using ResourceManagment.Data.Database;
using ResourceManagment.Data.Model;
using ResourceManagment.Data.Models;

namespace ResourceManagment.Data
{
    public class SqliteDataRepository : IDataRepository

    {
        private readonly ResourceManagerDatabaseSchema _databaseSchema;
        private static IDataRepository _instance;

        private readonly ObservableCollection<IPerson> _allPeople;
        private readonly ObservableCollection<IProject> _allProjects;
        private readonly ObservableCollection<IWeeklySchedule> _allSchedules;
        private readonly ObservableCollection<IResourceBlock> _allResourceBlocks;

        public DataCollection<IPerson> AllPeople { get; }
        public DataCollection<IProject> AllProjects { get; }
        public DataCollection<IWeeklySchedule> AllWeeklySchedules { get; }
        public DataCollection<IResourceBlock> AllResourceBlocks { get; }

        private SqliteDataRepository(ResourceManagerDatabaseSchema databaseSchema)
        {
            _databaseSchema = databaseSchema;
            _allPeople = new ObservableCollection<IPerson>();
            AllPeople = new DataCollection<IPerson>(_allPeople);

            _allProjects = new ObservableCollection<IProject>();
            AllProjects = new DataCollection<IProject>(_allProjects);

            _allSchedules = new ObservableCollection<IWeeklySchedule>();
            AllWeeklySchedules = new DataCollection<IWeeklySchedule>(_allSchedules);

            _allResourceBlocks = new ObservableCollection<IResourceBlock>();
            AllResourceBlocks = new DataCollection<IResourceBlock>(_allResourceBlocks);

            LoadInitalData();
        }

        private void LoadInitalData()
        {
            _databaseSchema.PersonTable.GetAll<SqlPerson>().ForEach(p => _allPeople.Add(p));
            _databaseSchema.ProjectTable.GetAll<SqlProject>().ForEach(p => _allProjects.Add(p));
            _databaseSchema.WeeklyScheduleTable.GetAll<SqlSchedules>().ForEach(s => _allSchedules.Add(s));
            _databaseSchema.ResourceBlockTable.GetAll<SqlResourceBlock>().ForEach(b => _allResourceBlocks.Add(b));
        }

        public static IDataRepository Instance()
        {
            if (_instance == null)
            {
                var schemaVersion = ConfigurationManager.AppSettings["sql.schema.version"];

                var sqlSchemaVerifier = new SqlSchemaVerifier(schemaVersion);
                var databaseLocation = Environment.ExpandEnvironmentVariables(ConfigurationManager.AppSettings["sql.database.location"]);
                var databaseSchema = new ResourceManagerDatabaseSchema(databaseLocation, sqlSchemaVerifier);
                _instance = new SqliteDataRepository(databaseSchema);
            }
            return _instance;
        }
    }
}