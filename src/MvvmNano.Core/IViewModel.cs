using System.ComponentModel;
using System;

namespace MvvmNano
{
    public interface IViewModel : INotifyPropertyChanged, IDisposable
    {
    }

    public interface IViewModel<TNavigationParameter> : IViewModel
    {
        void Initialize(TNavigationParameter parameter);
    }
}

