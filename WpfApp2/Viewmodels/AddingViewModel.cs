using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApp2.Configs;
using WpfApp2.Database;
using WpfApp2.Interfaces;
using WpfApp2.Models;
using WpfApp2.Repository;

namespace WpfApp2.ViewModels
{
    public class AddingViewModel : INotifyPropertyChanged
    {

        private ObservableCollection<TareResponse> _tars;
        public ObservableCollection<TareResponse> Tars
        {
            get => _tars;
            set
            {
                _tars = value;
                OnPropertyChanged(nameof(Tars));
            }
        }

        private ObservableCollection<CarResponse> _carList;
        public ObservableCollection<CarResponse> CarList
        {
            get => _carList;
            set
            {
                _carList = value;
                OnPropertyChanged(nameof(CarList));
            }
        }


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

        private readonly IRepository<CarResponse> _dbCarResponse;
        private readonly IRepositoryToFind<TareResponse> _dbTareResponse;


        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="dbCarResponse">Принимает ссылку на конеткст репозитория работы с транспортом</param>
        /// <param name="carList">Принимает список транспортов</param>
        public AddingViewModel(IRepository<CarResponse> dbCarResponse, ObservableCollection<CarResponse> carList)
        {
            _dbCarResponse = dbCarResponse;
            _dbTareResponse = new TareRepository();
            CarList = carList;

            GetData();

            VisibilityCalendarTare = Visibility.Collapsed;
            VisibilityCalendarGross = Visibility.Collapsed;

        }

        private async Task GetData()
        {
            LoadTare();
        }
        private async Task LoadTare()
        {
            Tars = new ObservableCollection<TareResponse>(await _dbTareResponse.GetAll());
        }

        #region Работа с транспортом

        private string _nameCar;
        public string NameCar
        {
            get => _nameCar;
            set
            {
                _nameCar = value;
                OnPropertyChanged(nameof(NameCar));
            }
        }

        private string _numberCar;
        public string NumberCar
        {
            get => _numberCar;
            set
            {
                _numberCar = value;
                OnPropertyChanged(nameof(NumberCar));
            }
        }


        private RelayCommand _addingCar;

        /// <summary>
        /// Команда для доблавление нового транспорта
        /// </summary>
        public RelayCommand AddingCar
        {
            get
            {
                return _addingCar ?? (_addingCar = new RelayCommand(async obj =>
                {
                    try
                    {
                        CarResponse carResponse = new CarResponse()
                        {
                            Name = NameCar,
                            Number = NumberCar
                        };

                        var newItem = await _dbCarResponse.Create(carResponse);
                        await _dbCarResponse.Save();
                        CarList.Add(newItem);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка: " + ex.Message, "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                }));
            }
        }

        #endregion

        #region Работа с тарой
        private Visibility _visibilityCalendarTare;
        public Visibility VisibilityCalendarTare

        {
            get => _visibilityCalendarTare;
            set
            {
                _visibilityCalendarTare = value;
                OnPropertyChanged(nameof(VisibilityCalendarTare));
            }
        }


        private Visibility _visibilityCalendarGross;
        public Visibility VisibilityCalendarGross
        {
            get => _visibilityCalendarGross;
            set
            {
                _visibilityCalendarGross = value;
                OnPropertyChanged(nameof(VisibilityCalendarGross));
            }
        }

        private string _selectedDateGrossString;
        public string SelectedDateGrossString
        {
            get => _selectedDateGrossString;
            set
            {
                _selectedDateGrossString = value;
                OnPropertyChanged(nameof(SelectedDateGrossString));
            }
        }

        private DateTime _selectedDateGross;
        public DateTime SelectedDateGross
        {
            get => _selectedDateGross;
            set
            {
                _selectedDateGross = value;
                OnPropertyChanged(nameof(SelectedDateGross));
                SelectedDateGrossString = SelectedDateGross.ToString("dd.MM.yyyy");
                VisibilityCalendarGross = ChangeVisibility(VisibilityCalendarGross);
            }
        }



        private string _selectedDateTareString;
        public string SelectedDateTareString
        {
            get => _selectedDateTareString;
            set
            {
                _selectedDateTareString = value;
                OnPropertyChanged(nameof(SelectedDateTareString));
            }
        }

        private DateTime _selectedDateTare;
        public DateTime SelectedDateTare
        {
            get => _selectedDateTare;
            set
            {
                _selectedDateTare = value;
                OnPropertyChanged(nameof(SelectedDateTare));
                SelectedDateTareString = SelectedDateTare.ToString("dd.MM.yyyy");
                VisibilityCalendarTare = ChangeVisibility(VisibilityCalendarTare);
            }
        }



        private RelayCommand _showCalendarTare;
        /// <summary>
        /// Команда для смени параметра видимости для календаря тары
        /// </summary>
        public RelayCommand ShowCalendarTare
        {
            get
            {
                return _showCalendarTare ?? (_showCalendarTare = new RelayCommand(obj =>
                {
                    VisibilityCalendarTare = ChangeVisibility(VisibilityCalendarTare);
                }));
            }
        }

        
        private RelayCommand _showCalendarGross;
        /// <summary>
        /// Команда для смени параметра видимости для календаря брутто
        /// </summary>
        public RelayCommand ShowCalendarGross
        {
            get
            {
                return _showCalendarGross ?? (_showCalendarGross = new RelayCommand(obj =>
                {
                    VisibilityCalendarGross = ChangeVisibility(VisibilityCalendarGross);
                }));
            }
        }

        /// <summary>
        /// Метод для изменения видимости календаря с выбором даты
        /// </summary>
        /// <param name="visibility">Принимает параметр видимости календаря</param>
        /// <returns>Возвращает новый параметр видимости</returns>
        private Visibility ChangeVisibility(Visibility visibility)
        {
            if (visibility == Visibility.Collapsed)
                visibility = Visibility.Visible;
            else visibility = Visibility.Collapsed;

            return visibility;
        }

        private string _numberTare;
        public string NumberTare
        {
            get => _numberTare;
            set
            {
                _numberTare = value;
                OnPropertyChanged(nameof(NumberTare));
            }
        }


        private double _weightGrossTare;
        public double WeightGrossTare
        {
            get => _weightGrossTare;
            set
            {
                _weightGrossTare = value;
                OnPropertyChanged(nameof(WeightGrossTare));
            }
        }


        private double _weightTare;
        public double WeightTare
        {
            get => _weightTare;
            set
            {
                _weightTare = value;
                OnPropertyChanged(nameof(WeightTare));
            }
        }


        private double _weightNet;
        public double WeightNet
        {
            get => _weightNet;
            set
            {
                _weightNet = value;
                OnPropertyChanged(nameof(WeightNet));
            }
        }


        /// <summary>
        /// Метод для доблавение новой тары
        /// </summary>
        private RelayCommand _addingTares;
        public RelayCommand AddingTares
        {
            get
            {
                return _addingTares ?? (_addingTares = new RelayCommand(async obj =>
                {
                    try
                    {
                        TareResponse tareResponse = new TareResponse()
                        {
                            Number = NumberTare,
                            IdCar = SelectedCar.Id,
                            TareWeight = WeightTare,
                            NetWeight = WeightNet,
                            GrossWeight = WeightGrossTare,
                            DateGross = SelectedDateGross.Date,
                            TareDate = SelectedDateTare.Date,
                        };

                        var newItem = await _dbTareResponse.Create(tareResponse);
                        await _dbTareResponse.Save();
                        Tars.Add(newItem);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка: " + ex.Message, "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                }));
            }
        }



        #endregion



        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

    }
}
