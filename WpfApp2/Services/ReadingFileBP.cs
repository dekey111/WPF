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

        /// <summary>
        /// Конструктор класса
        /// </summary>
        public ReadingFileBP()
        {
            IsFileReadComplete = false;

            _backgroundWorker = new BackgroundWorker
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = true
            };

            //Привязываем события воркера
            _backgroundWorker.DoWork += BackgroundWorker_DoWork;
            _backgroundWorker.ProgressChanged += BackgroundWorker_ProgressChanged; ;
            _backgroundWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;
        }

        /// <summary>
        /// Метод для изменения статуса
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackgroundWorker_ProgressChanged(object? sender, ProgressChangedEventArgs e)
        {
            Status = e.UserState.ToString();
        }

        /// <summary>
        /// Метод запуска фоновой задачи
        /// </summary>
        public void StartReading()
        {
            if (_backgroundWorker.IsBusy)
                return;

            Status = "Запустился бэк";
            _backgroundWorker.RunWorkerAsync();
        }


        /// <summary>
        /// Тело выполнения фоновой задачи
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                _backgroundWorker.ReportProgress(1, "Чтение файла");

                //Специально стоит задержка, для проверки изменения статуса
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

        /// <summary>
        /// Метод прекращения выполнения задачи служит для вызова OnPropertyChanged, чтобы появилась кнопка на главном экране
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
