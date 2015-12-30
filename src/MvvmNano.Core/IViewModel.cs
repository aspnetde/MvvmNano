using System.ComponentModel;

namespace MvvmNano.Core
{
    public interface IViewModel : INotifyPropertyChanged
    {
        void Initialize(object parameter);
    }
}

