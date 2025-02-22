using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2.Interfaces
{
    public interface IFileReader : INotifyPropertyChanged
    {
        string Status { get; }
        string FileContent { get; }
        bool IsFileReadComplete { get; }

        void StartReading();
    }
}
