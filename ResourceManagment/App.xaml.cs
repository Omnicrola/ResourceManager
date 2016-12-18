using System;
using System.Configuration;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using DataApi.Extensions;
using DatabaseApi.SqlLite;
using ResourceManagment.Data;
using ResourceManagment.Data.Database;
using ResourceManagment.Operations;
using ResourceManagment.Windows.Main;
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
            var mainWindow = MainWindowFactory.Build(SqliteDataRepository.Instance(), Dispatcher);
            mainWindow.Show();

        }

    }
}
