using MVVMTask.BL.Models;
using MVVMTask.BL.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMTask.BL.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Area> areas;
        public ObservableCollection<Area> Areas
        {
            get
            {
                return areas;
            }
            set
            {
                areas = value;
                NotifyPropertyChanged("Areas");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindowViewModel()
        {
            ILogger logger = new Logger();
            IAreaService Service = new AreaService(logger);
            IEnumerable<Area> areaList = Task.Run(async () => await Service.GetAreas()).Result;
            Areas = new ObservableCollection<Area>(areaList);
        }

        private void NotifyPropertyChanged(string v)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(v));
            }
        }
    }
}
