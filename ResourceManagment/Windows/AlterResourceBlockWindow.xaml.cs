using ResourceManagment.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ResourceManagment.Windows
{
    /// <summary>
    /// Interaction logic for AlterResourceBlockWindow.xaml
    /// </summary>
    public partial class AlterResourceBlockWindow : Window
    {
        private ResourceBlockViewModel _resourceBlock;

        public AlterResourceBlockWindow(ObservableCollection<PersonViewModel> people, ObservableCollection<ProjectViewModel> projects, ResourceBlockViewModel resourceBlock)
        {
            InitializeComponent();
            People = people;
            Projects = projects;
            this._resourceBlock = resourceBlock;
            DataContext = this;
        }

        public string ResourceName
        {
            get
            {
                return "N/A";
            }
            set { }
        }
        public string BlockTime
        {
            get
            {
                string morningEvening = _resourceBlock.Date.Hour < 12 ? "Morning" : "Afternoon";
                string dayOfWeek = _resourceBlock.Date.DayOfWeek.ToString();
                int dayOfMonth = _resourceBlock.Date.Day;
                int month = _resourceBlock.Date.Month;
                return String.Format("{0} of {1} on {2}/{3}", morningEvening, dayOfWeek, month, dayOfMonth);
            }
            set
            {

            }
        }

        public ObservableCollection<PersonViewModel> People { get; private set; }
        public ObservableCollection<ProjectViewModel> Projects { get; private set; }
    }
}
