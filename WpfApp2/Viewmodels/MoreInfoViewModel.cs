using ScottPlot;
using ScottPlot.Colormaps;
using ScottPlot.WPF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApp2.Interfaces;
using WpfApp2.Models;
using WpfApp2.Repository;
using WpfApp2.Views;

namespace WpfApp2.ViewModels
{
    public class MoreInfoViewModel : INotifyPropertyChanged
    {
        private CarResponse _selectedCar;
        public CarResponse SelectedCar
        {
            get => _selectedCar;
            set
            {
                _selectedCar = value;
                OnPropertyChanged(nameof(SelectedCar));
            }
        }


        private ObservableCollection<TareResponse> _taresByCar;
        public ObservableCollection<TareResponse> TaresByCar
        {
            get => _taresByCar;
            set
            {
                _taresByCar = value;
                OnPropertyChanged(nameof(TaresByCar));
            }
        }


        IRepositoryToFind<TareResponse> _dbTareResponse;
        private ScottPlot.WPF.WpfPlot _wpfPlot;

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="selectedCar">Принимает выбранный транспорт</param>
        /// <param name="wpfPlot">Принимате график из WPF</param>
        public MoreInfoViewModel(CarResponse selectedCar, ScottPlot.WPF.WpfPlot wpfPlot)
        {
            SelectedCar = selectedCar;
            _dbTareResponse =  new TareRepository();
            _wpfPlot = wpfPlot;

            GetData();
        }

        /// <summary>
        /// Метод для загрузки данных
        /// </summary>
        /// <returns></returns>
        private async Task GetData()
        {
            TaresByCar = new ObservableCollection<TareResponse>(await _dbTareResponse.GetFromId(SelectedCar.Id));
            GenerateDiagramm();
        }

        /// <summary>
        /// Метод для загрузки диаграммы по весу тары!
        /// </summary>
        /// <returns></returns>
        private async Task GenerateDiagramm()
        {
            DateTime[] dates = TaresByCar.Select(x => Convert.ToDateTime(x.TareDate)).ToArray();
            double[] ys =  TaresByCar.Select(x => x.TareWeight).ToArray();
            _wpfPlot.Plot.Add.Scatter(dates, ys);
            _wpfPlot.Plot.Axes.DateTimeTicksBottom();

            _wpfPlot.Plot.RenderManager.RenderStarting += (s, e) =>
            {
                Tick[] ticks = _wpfPlot.Plot.Axes.Bottom.TickGenerator.Ticks;
                for (int i = 0; i < ticks.Length; i++)
                {
                    DateTime dt = DateTime.FromOADate(ticks[i].Position);
                    string label = dt.ToString("d");
                    ticks[i] = new Tick(ticks[i].Position, label);
                }
            };
            _wpfPlot.Refresh();
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
