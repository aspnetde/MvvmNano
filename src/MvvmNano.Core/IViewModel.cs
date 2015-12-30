using System.ComponentModel;

namespace MvvmNano
{
    public interface IViewModel : INotifyPropertyChanged
    {
        void Initialize(object parameter);
    }
}

