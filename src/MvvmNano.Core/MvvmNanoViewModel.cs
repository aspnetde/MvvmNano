using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System;

namespace MvvmNano
{
    public class MvvmNanoViewModel : IViewModel
    {
        // Workaround, as long as we don't have an IoC Container ...
        public static IPresenter Presenter { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void Initialize(object parameter)
        {
            // Hook
        }

        protected void NotifyPropertyChanged([CallerMemberName]string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        protected Task ShowViewModelAsync<TViewModel>(object parameter) 
            where TViewModel : IViewModel
        {
            if (Presenter == null)
                throw new InvalidOperationException("Please set MvvmNanoViewModel.Presenter.");

            return Presenter.ShowViewModelAsync<TViewModel>(parameter);
        }

        public virtual void Dispose()
        {
            // Hook
        }
    }
}

