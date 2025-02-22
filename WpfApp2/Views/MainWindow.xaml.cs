using System.Windows;
using WpfApp2.Database;
using WpfApp2.Services;
using WpfApp2.ViewModels;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            BdtestTaskServerstalContext context = new BdtestTaskServerstalContext();
            var _fileReader = new ReadingFileBP();
            DataContext = new MainWindowViewModel(context, _fileReader);
        }
    }
}