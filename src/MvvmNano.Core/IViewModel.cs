using System.ComponentModel;
using System;

namespace MvvmNano
{
    public interface IViewModel : INotifyPropertyChanged, IDisposable
    {
        void Initialize(object parameter);
    }
}

