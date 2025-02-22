﻿using System;
using System.Collections.ObjectModel;
using System.Windows;
using WpfApp2.Interfaces;
using WpfApp2.Models;
using WpfApp2.ViewModels;

namespace WpfApp2.Views
{
    /// <summary>
    /// Логика взаимодействия для WindowAdding.xaml
    /// </summary>
    public partial class WindowAdding : Window
    {
        public WindowAdding(IRepository<CarResponse> dbCarResponse, ObservableCollection<CarResponse> carList)
        {
            InitializeComponent();
            DataContext = new AddingViewModel(dbCarResponse, carList);
        }
    }
}
