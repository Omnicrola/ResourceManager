using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ResourceManagment.Data.Model;
using ResourceManagment.Windows.AlterResourceBlock;
using ResourceManagment.Windows.ManagePeople;
using ResourceManagment.Windows.ViewModels;

namespace ResourceManagment.Windows.ManageWeeklySchedule
{
    public class PersonalScheduleViewModel : ViewModel
    {

        private readonly ObservableCollection<WorkDayViewModel> _workDays;
        private PersonViewModel _personViewModel;
        private List<ResourceBlockViewModel> _allResourceBlocks;

        public PersonViewModel Person
        {
            get
            {
                return _personViewModel;
            }
            set
            {
                _personViewModel = value;
                FireOnPropertyChanged("Person");
            }
        }
        public ObservableCollection<WorkDayViewModel> Days
        {
            get
            {
                return _workDays;
            }
            set { }
        }

        public WorkDayViewModel Saturday { get { return _workDays[0]; } }
        public WorkDayViewModel Sunday { get { return _workDays[1]; } }
        public WorkDayViewModel Monday { get { return _workDays[2]; } }
        public WorkDayViewModel Tuesday { get { return _workDays[3]; } }
        public WorkDayViewModel Wednesday { get { return _workDays[4]; } }
        public WorkDayViewModel Thursday { get { return _workDays[5]; } }
        public WorkDayViewModel Friday { get { return _workDays[6]; } }

        public PersonalScheduleViewModel(PersonViewModel personViewModel)
        {
            _personViewModel = personViewModel;
            _workDays = new ObservableCollection<WorkDayViewModel>();
            _allResourceBlocks = new List<ResourceBlockViewModel>();
            _workDays.Add(new WorkDayViewModel(CreateBlock(1), CreateBlock(2)));
            _workDays.Add(new WorkDayViewModel(CreateBlock(3), CreateBlock(4)));
            _workDays.Add(new WorkDayViewModel(CreateBlock(5), CreateBlock(6)));
            _workDays.Add(new WorkDayViewModel(CreateBlock(7), CreateBlock(8)));
            _workDays.Add(new WorkDayViewModel(CreateBlock(9), CreateBlock(10)));
            _workDays.Add(new WorkDayViewModel(CreateBlock(11), CreateBlock(12)));
            _workDays.Add(new WorkDayViewModel(CreateBlock(13), CreateBlock(14)));


        }

        private ResourceBlockViewModel CreateBlock(int blockOrder)
        {
            var resourceBlockViewModel = new ResourceBlockViewModel(_personViewModel, blockOrder);
            _allResourceBlocks.Add(resourceBlockViewModel);
            return resourceBlockViewModel;
        }


        public void OverwriteBlock(ResourceBlockViewModel resourceBlock)
        {
            var resourceBlockViewModel = _allResourceBlocks.First(vm => vm.BlockOrder == resourceBlock.BlockOrder);
            resourceBlockViewModel.Overwrite(resourceBlock);
        }
    }
}