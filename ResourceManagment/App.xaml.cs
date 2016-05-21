using System;
using System.Configuration;
using System.Windows;
using System.Windows.Media;
using DatabaseApi.SqlLite;
using ResourceManagment.Data.Database;
using ResourceManagment.Operations;
using ResourceManagment.Windows.Main;
using ResourceManagment.Windows.ManagePeople;
using ResourceManagment.Windows.ManageProjects;
using ResourceManagment.Windows.ManageWeeklySchedule;
using ResourceManagment.Windows.ViewModels;

namespace ResourceManagment
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Start(object sender, StartupEventArgs args)
        {
            var schemaVersion = ConfigurationManager.AppSettings["sql.schema.version"];

            var sqlSchemaVerifier = new SqlSchemaVerifier(schemaVersion);
            var databaseLocation = Environment.ExpandEnvironmentVariables(ConfigurationManager.AppSettings["sql.database.location"]);
            var databaseSchema = new ResourceManagerDatabaseSchema(databaseLocation, sqlSchemaVerifier);

            MainWindowViewModel mainWindowViewModel = new MainWindowViewModel(new ModelDataBinder(databaseSchema));

            var mainWindow = new MainWindow(mainWindowViewModel, new OperationsQueue(Dispatcher));
            mainWindow.Show();
        }



    }
}
