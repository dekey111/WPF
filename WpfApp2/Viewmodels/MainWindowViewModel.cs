using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using WpfApp2.Configs;
using WpfApp2.Interfaces;
using WpfApp2.Models;
using WpfApp2.Repository;
using WpfApp2.Services;
using WpfApp2.Views;
using WpfApp2.Database;

namespace WpfApp2.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
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


        private TareResponse _selectedTare;
        public TareResponse SelectedTare
        {
            get => _selectedTare;
            set
            {
                _selectedTare = value;
                OnPropertyChanged(nameof(SelectedTare));
            }
        }


        private Visibility visibilityOpenFileContext;
        public Visibility VisibilityOpenFileContext
        {
            get => visibilityOpenFileContext;
            set
            {
                visibilityOpenFileContext = value;
                OnPropertyChanged(nameof(VisibilityOpenFileContext));
            }
        }


        private readonly ReadingFileBP _fileReader;

        public string Status => _fileReader.Status;
        public string FileContent => _fileReader.FileContent;
        public bool IsFileReadComplete => _fileReader.IsFileReadComplete;

        IRepository<CarResponse> _dbCarResponse;

        public MainWindowViewModel(BdtestTaskServerstalContext context, ReadingFileBP fileReader)
        {
            VisibilityOpenFileContext = Visibility.Collapsed;

            _dbCarResponse = new CarRepository();
            _fileReader = fileReader;
            _fileReader.StartReading();
            _fileReader.PropertyChanged += FileReader_PropertyChanged;

            Start();
        }

        private async Task Start()
        {
            try
            {
                CarList = new ObservableCollection<CarResponse>(await _dbCarResponse.GetAll());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при загрузке данных! \nОписание ошибки: " + ex.Message);
            }
        }

        private void FileReader_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_fileReader.IsFileReadComplete))
                VisibilityOpenFileContext = Visibility.Visible;
        }


        private RelayCommand _openFileContext;
        public RelayCommand OpenFileContext
        {
            get
            {
                return _openFileContext ?? (_openFileContext = new RelayCommand(obj =>
                {
                    MessageBox.Show(_fileReader.FileContent, "Содержимое файла", MessageBoxButton.OK, MessageBoxImage.Information);
                }));
            }
        }


        private RelayCommand _openWindowAdding;
        public RelayCommand OpenWindowAdding
        {
            get
            {
                return _openWindowAdding ?? (_openWindowAdding = new RelayCommand(obj =>
                {
                    WindowAdding windowAdding = new WindowAdding(_dbCarResponse, CarList);
                    windowAdding.Show();
                }));
            }
        }


        private RelayCommand _openWindowMoreInfo;
        public RelayCommand OpenWindowMoreInfo
        {
            get
            {
                return _openWindowMoreInfo ?? (_openWindowMoreInfo = new RelayCommand(obj =>
                {
                    if (SelectedCar == null)
                        return;

                    WindowMoreInfo windowMoreInfo = new WindowMoreInfo(SelectedCar);
                    windowMoreInfo.Show();
                }));
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
