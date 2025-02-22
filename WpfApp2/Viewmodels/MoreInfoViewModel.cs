using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApp2.Models;
using WpfApp2.Views;

namespace WpfApp2.ViewModels
{
    public class MoreInfoViewModel : INotifyPropertyChanged
    {
        private ViewCarTareResponse _selectedCarTare;
        public ViewCarTareResponse SelectedCarTare
        {
            get => _selectedCarTare;
            set
            {
                _selectedCarTare = value;
                OnPropertyChanged(nameof(SelectedCarTare));
            }
        }
        public MoreInfoViewModel(ViewCarTareResponse selectedCarTare)
        {
            SelectedCarTare = selectedCarTare;
        }



        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
