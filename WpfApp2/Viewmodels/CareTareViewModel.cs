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
    public class CareTareViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<ViewCarTareResponse> _viewCarTareList;
        public ObservableCollection<ViewCarTareResponse> ViewCarTareList
        {
            get => _viewCarTareList;
            set
            {
                _viewCarTareList = value;
                OnPropertyChanged(nameof(ViewCarTareList));
            }
        }

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

        IRepository<ViewCarTareResponse> _dbViewCarTareResponse;

        public CareTareViewModel(BdtestTaskServerstalContext context, ReadingFileBP fileReader)
        {
            VisibilityOpenFileContext = Visibility.Collapsed;

            _dbViewCarTareResponse = new ViewCarTareRepository();
            ViewCarTareList = new ObservableCollection<ViewCarTareResponse>();
            _fileReader = fileReader;
            _fileReader.StartReading();
            _fileReader.PropertyChanged += FileReader_PropertyChanged;

            Start();
        }

        private async Task Start()
        {
            try
            {
                ViewCarTareList = new ObservableCollection<ViewCarTareResponse>(await _dbViewCarTareResponse.GetAll());
            }
            catch(Exception ex)
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
                    WindowAdding windowAdding = new WindowAdding(_dbViewCarTareResponse, ViewCarTareList);
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
                    if (SelectedCarTare == null)
                        return;

                    WindowMoreInfo windowMoreInfo = new WindowMoreInfo(SelectedCarTare);
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
