using ScottPlot;
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
        public MoreInfoViewModel(ViewCarTareResponse selectedCarTare, ScottPlot.WPF.WpfPlot wpfPlot)
        {
            SelectedCarTare = selectedCarTare;


            Dictionary<string, double> dict = new Dictionary<string, double>()
            {
                {"Нетто", SelectedCarTare.NetWeight },
                {"Брутто", SelectedCarTare.GrossWeight },
                {"Тара", SelectedCarTare.TareWeight }
            };

            var barPlot = wpfPlot.Plot.Add.Bars(dict.Values.ToArray());

            var dictKeys = dict.Keys.ToArray();
            int i = 0;
            foreach (var bar in barPlot.Bars)
            {
                bar.Label = dictKeys[i];
                i++;
            }

            barPlot.ValueLabelStyle.Bold = true;
            barPlot.ValueLabelStyle.FontSize = 18;

            wpfPlot.Plot.Axes.Margins(bottom: 0, top: .2);
            wpfPlot.Refresh();
        }



        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
