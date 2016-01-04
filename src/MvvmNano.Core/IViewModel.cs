using System.ComponentModel;
using System;

namespace MvvmNano
{
    public interface IViewModel : INotifyPropertyChanged, IDisposable
    {
    }

    public interface IViewModel<TParameter> : IViewModel
    {
        void Initialize(TParameter parameter);
    }
}

