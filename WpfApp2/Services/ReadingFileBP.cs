using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfApp2.Configs;
using WpfApp2.Interfaces;
using WpfApp2.Views;

namespace WpfApp2.Services
{
    public class ReadingFileBP : IFileReader
    {
        private string _status;
        private string _fileContent;
        private bool _isFileReadComplete;

        public string Status
        {
            get => _status;
            set
            {
                _status = value;
                OnPropertyChanged(nameof(Status));
            }
        }
        public string FileContent
        {
            get => _fileContent;
            set
            {
                _fileContent = value;
                OnPropertyChanged(nameof(FileContent));
            }
        }
        public bool IsFileReadComplete
        {
            get => _isFileReadComplete;
            set
            {
                _isFileReadComplete = value;
                OnPropertyChanged(nameof(IsFileReadComplete));
            }
        }


        private BackgroundWorker _backgroundWorker;

        public ReadingFileBP()
        {
            IsFileReadComplete = false;

            _backgroundWorker = new BackgroundWorker
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = true
            };

            _backgroundWorker.DoWork += BackgroundWorker_DoWork;
            _backgroundWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;
        }
        public void StartReading()
        {
            if (_backgroundWorker.IsBusy)
                return;

            Status = "Запустился бэк";
            _backgroundWorker.RunWorkerAsync();
        }

        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                _backgroundWorker.ReportProgress(1, "Чтение файла");
                Thread.Sleep(5000);
                string filePath = "File.txt";
                string fileContent = File.ReadAllText(filePath);

                // Сохраняем содержимое файла
                FileContent = fileContent;
            }
            catch (Exception ex)
            {
                FileContent = $"Ошибка: {ex.Message}";
            }
        }

        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Status = "Файл успешно прочитан";
            IsFileReadComplete = true;
            OnPropertyChanged(nameof(IsFileReadComplete));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
